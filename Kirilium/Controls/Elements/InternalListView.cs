﻿using Kirilium.Themes;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.Versioning;
using System.Windows.Forms;
using static Kirilium.WinApi.User32;
using static Kirilium.WinApi.WindowMessages;

namespace Kirilium.Controls.Elements
{
    [SupportedOSPlatform("windows")]
    internal class InternalListView : ListView
    {
        // 非公開定数
        private const int MEASURE_TEXT_MARGIN = 5;

        // 非公開フィールド
        private KListViewColumnHeaderWindow columnHeaderWindow;
        private Point columnHeaderMousePoint;
        private bool flagColumnHeaderMouseMove;
        private ColumnHeaderAutoResizeStyle currentColumnHeaderAutoResizeStyle;

        // コンストラクタ
        public InternalListView()
        {
            this.columnHeaderMousePoint = Point.Empty;
            this.flagColumnHeaderMouseMove = false;

            this.MaximumColumnHeaderWidth = -1;

            // 描画設定
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            base.BorderStyle = BorderStyle.None;
            base.OwnerDraw = true;
            base.BackColor = ThemeManager.CurrentTheme.GetColor(ColorKeys.ListViewBackColor);

            // テーマ変更時の処理を設定
            ThemeManager.ThemeChanged += OnThemeChanged;
        }

        // デストラクタ
        ~InternalListView()
        {
            ThemeManager.ThemeChanged -= OnThemeChanged;
        }

        #region プロパティ

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new bool GridLines { get; set; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new bool OwnerDraw { get; set; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new BorderStyle BorderStyle { get; set; }

        /// <summary>
        /// 列ヘッダのリサイズを無効化するかどうか
        /// </summary>
        public bool DisableColumnHeaderResize { set; get; }

        /// <summary>
        /// 列ヘッダの幅の最大値を示す。-1の場合は最大値を設定していないものとする。
        /// </summary>
        public int MaximumColumnHeaderWidth { set; get; }

        /// <summary>
        /// 最後（一番右）の列ヘッダをコントロールの幅に合わせるかどうか
        /// </summary>
        public bool FillLastColumnHeader { set; get; }

        #endregion

        /// <summary>
        /// 列の幅を、指定されたサイズ変更スタイルで示されたスタイルで変更する。
        /// </summary>
        /// <param name="columnHeaderAutoResizeStyle">サイズ変更スタイル</param>
        public new void AutoResizeColumns(ColumnHeaderAutoResizeStyle columnHeaderAutoResizeStyle)
        {
            if (columnHeaderAutoResizeStyle == ColumnHeaderAutoResizeStyle.ColumnContent)
            {
                int sumWidth = 0;

                for (int columnIndex = 0; columnIndex < this.Columns.Count; ++columnIndex)
                {
                    var columnSize = Renderer.MeasureText(this.Columns[columnIndex].Text, this.Font);
                    int maxWidth = columnSize.Width + MEASURE_TEXT_MARGIN;

                    for (int itemIndex = 0; itemIndex < this.Items.Count; ++itemIndex)
                    {
                        string text = string.Empty;

                        if (columnIndex == 0)
                        {
                            text = this.Items[itemIndex].Text;
                        }
                        else
                        {
                            text = this.Items[itemIndex].SubItems[columnIndex].Text;
                        }

                        var size = Renderer.MeasureText(text, this.Items[itemIndex].Font);
                        var width = 0;

                        if (columnIndex == this.Columns.Count - 1 && this.FillLastColumnHeader)
                        {
                            width = this.ClientSize.Width - sumWidth;
                        }
                        else
                        {
                            width = size.Width + MEASURE_TEXT_MARGIN;
                        }

                        if (width > maxWidth)
                        {
                            maxWidth = width;
                        }
                    }

                    // 幅に最大値を反映
                    maxWidth = LimitWidth(maxWidth);
                    
                    // 幅を設定
                    this.Columns[columnIndex].Width = maxWidth;
                    sumWidth += maxWidth;
                }
            }
            else if (columnHeaderAutoResizeStyle == ColumnHeaderAutoResizeStyle.HeaderSize)
            {
                for (int columnIndex = 0; columnIndex < this.Columns.Count; ++columnIndex)
                {
                    var size = Renderer.MeasureText(this.Columns[columnIndex].Text, this.Font);
                    var width = LimitWidth(size.Width + MEASURE_TEXT_MARGIN);

                    if (this.Columns[columnIndex].Width != -2)
                    {
                        this.Columns[columnIndex].Width = width;
                    }
                }
            }

            this.currentColumnHeaderAutoResizeStyle = columnHeaderAutoResizeStyle;
        }

        /// <summary>
        /// 指定された幅を、設定された有効範囲内で制限をかけて返す。
        /// </summary>
        /// <param name="width"></param>
        /// <returns></returns>
        protected virtual int LimitWidth(int width)
        {
            if (this.MaximumColumnHeaderWidth >= 0)
            {
                if (width > this.MaximumColumnHeaderWidth)
                {
                    width = this.MaximumColumnHeaderWidth;
                }
            }

            return width;
        }

        /// <summary>
        /// テーマが変更された場合の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnThemeChanged(object sender, EventArgs e)
        {
            base.BackColor = ThemeManager.CurrentTheme.GetColor(ColorKeys.ListViewBackColor);

            Refresh();          // Invalidate()だと列ヘッダ部分の再描画が行われない。
        }

        /// <summary>
        /// コントロールのハンドルが生成された場合の処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);

            this.columnHeaderWindow = new KListViewColumnHeaderWindow((IntPtr)SendMessage(Handle, LVM_GETHEADER, IntPtr.Zero, IntPtr.Zero), this);
            this.columnHeaderWindow.ColumnHeaderMouseMove += OnColumnHeaderMouseMove;
            this.columnHeaderWindow.ColumnHeaderMouseLeave += OnColumnHeaderMouseLeave;
        }

        /// <summary>
        /// 描画処理
        /// </summary>
        /// <param name="deviceContext"></param>
        /// <param name="clip"></param>
        private void Draw(Graphics deviceContext, Rectangle clip, IntPtr handle)
        {
            // 背景を塗りつぶす。
            Renderer.FillRect(deviceContext, clip, ThemeManager.CurrentTheme.GetColor(ColorKeys.ListViewBackColor));

            IntPtr hdc = deviceContext.GetHdc();

            if (hdc == IntPtr.Zero)
            {
                return;
            }

            try
            {
                var msg = Message.Create(this.Handle, WM_PAINT, hdc, (IntPtr)(PRF_CHILDREN | PRF_CLIENT | PRF_ERASEBKGND));

                // メッセージをウィンドウプロシージャに送信
                DefWndProc(ref msg);
            }
            finally
            {
                deviceContext.ReleaseHdc();
            }
        }

        /// <summary>
        /// コントロールを描画する。
        /// </summary>
        /// <param name="msg"></param>
        /// <exception cref="Exception"></exception>
        private void PaintControl(ref Message msg)
        {
            if (msg.WParam == IntPtr.Zero)
            {
                var paintStruct = new PAINTSTRUCT();
                var paintHandle = BeginPaint(msg.HWnd, ref paintStruct);

                try
                {
                    using (var bufferedGraphics = BufferedGraphicsManager.Current.Allocate(paintHandle, this.ClientRectangle))
                    {
                        var clip = Rectangle.FromLTRB(paintStruct.rcPaint.Left, paintStruct.rcPaint.Top, paintStruct.rcPaint.Right, paintStruct.rcPaint.Bottom);

                        // バッファ領域に描画する。
                        bufferedGraphics.Graphics.SetClip(clip);
                        Draw(bufferedGraphics.Graphics, clip, msg.HWnd);

                        // バッファ領域に描画された内容をデバイスに書き込む。
                        bufferedGraphics.Render();
                    }
                }
                catch
                {
                    throw new Exception("KListViewの描画中に例外が発生しました。");
                }
                finally
                {
                    EndPaint(msg.HWnd, ref paintStruct);
                }
            }
            else
            {
                using (var g = Graphics.FromHdc(msg.WParam))
                {
                    Draw(g, this.ClientRectangle, msg.WParam);
                }
            }
        }

        /// <summary>
        /// アイテムの描画処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnDrawItem(DrawListViewItemEventArgs e)
        {
            var backColor = ThemeManager.CurrentTheme.GetColor(ColorKeys.ListViewItemBackColorNormal);
            var textColor = ThemeManager.CurrentTheme.GetColor(ColorKeys.ApplicationTextNormal);

            if (e.Item.Selected)
            {
                backColor = ThemeManager.CurrentTheme.GetColor(ColorKeys.ListViewItemBackColorSelected);
                textColor = ThemeManager.CurrentTheme.GetColor(ColorKeys.ApplicationTextHighlight);
            }

            // 背景を塗りつぶす。
            Renderer.FillRect(e.Graphics, e.Bounds, backColor);

            // テキストを描画する。
            Renderer.DrawText(
                e.Graphics,
                e.Item.Text,
                this.Font,
                e.Bounds,
                textColor,
                backColor,
                TextFormatFlags.Left | TextFormatFlags.VerticalCenter);
        }

        /// <summary>
        /// サブアイテムの描画処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnDrawSubItem(DrawListViewSubItemEventArgs e)
        {
            var backColor = ThemeManager.CurrentTheme.GetColor(ColorKeys.ListViewItemBackColorNormal);
            var textColor = ThemeManager.CurrentTheme.GetColor(ColorKeys.ApplicationTextNormal);

            if (e.Item.Selected)
            {
                backColor = ThemeManager.CurrentTheme.GetColor(ColorKeys.ListViewItemBackColorSelected);
                textColor = ThemeManager.CurrentTheme.GetColor(ColorKeys.ApplicationTextHighlight);
            }

            // 背景を塗りつぶす。
            Renderer.FillRect(e.Graphics, e.Bounds, backColor);

            // テキストを描画する。
            Renderer.DrawText(
                e.Graphics,
                e.SubItem.Text,
                this.Font,
                e.Bounds,
                textColor,
                backColor,
                TextFormatFlags.Left | TextFormatFlags.VerticalCenter);
        }

        /// <summary>
        /// 列ヘッダの描画処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnDrawColumnHeader(DrawListViewColumnHeaderEventArgs e)
        {
            var backColor = ThemeManager.CurrentTheme.GetColor(ColorKeys.ListViewColumnHeaderBackColorNormal);
            if (e.State == ListViewItemStates.Selected)
            {
                backColor = ThemeManager.CurrentTheme.GetColor(ColorKeys.ListViewColumnHeaderBackColorMouseClick);
            }
            else if (this.flagColumnHeaderMouseMove && this.HeaderStyle == ColumnHeaderStyle.Clickable)
            {
                if (e.Bounds.Contains(this.columnHeaderMousePoint))
                {
                    backColor = ThemeManager.CurrentTheme.GetColor(ColorKeys.ListViewColumnHeaderBackColorMouseOver);
                }
            }

            // 背景を塗りつぶす。
            Renderer.FillRect(e.Graphics, e.Bounds, backColor);

            // 境界線を描画する。
            var borderColor = ThemeManager.CurrentTheme.GetColor(ColorKeys.ApplicationBorderNormal);
            if (e.ColumnIndex != 0)
            {
                Renderer.DrawLine(e.Graphics, e.Bounds.X, e.Bounds.Y, e.Bounds.X, e.Bounds.Bottom - 1, borderColor);
            }
            if (e.ColumnIndex == this.Columns.Count - 1 && !this.FillLastColumnHeader)
            {
                Renderer.DrawLine(e.Graphics, e.Bounds.Right - 1, e.Bounds.Y, e.Bounds.Right - 1, e.Bounds.Bottom - 1, borderColor);
            }

            TextFormatFlags textFormatFlags = TextFormatFlags.Default;
            switch (this.Columns[e.ColumnIndex].TextAlign)
            {
                case HorizontalAlignment.Center:
                    textFormatFlags = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter;
                    break;
                case HorizontalAlignment.Left:
                    textFormatFlags = TextFormatFlags.Left | TextFormatFlags.VerticalCenter;
                    break;
                case HorizontalAlignment.Right:
                    textFormatFlags = TextFormatFlags.Right | TextFormatFlags.VerticalCenter;
                    break;
            }

            // テキストを描画
            Renderer.DrawText(
                e.Graphics,
                e.Header.Text,
                e.Font,
                e.Bounds,
                ThemeManager.CurrentTheme.GetColor(ColorKeys.ApplicationTextNormal),
                backColor,
                textFormatFlags);
        }

        /// <summary>
        /// 列の幅が変更される場合の処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnColumnWidthChanging(ColumnWidthChangingEventArgs e)
        {
            if (this.DisableColumnHeaderResize)
            {
                e.Cancel = true;
                e.NewWidth = LimitWidth(this.Columns[e.ColumnIndex].Width);
                return;
            }
            else
            {
                e.NewWidth = LimitWidth(e.NewWidth);
            }

            base.OnColumnWidthChanging(e);
        }

        /// <summary>
        /// 列の幅が変更された場合の処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnColumnWidthChanged(ColumnWidthChangedEventArgs e)
        {
            Invalidate();
            base.OnColumnWidthChanged(e);
        }

        /// <summary>
        /// コントロールのサイズが変更された場合の処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnSizeChanged(EventArgs e)
        {
            if (this.FillLastColumnHeader)
            {
                AutoResizeColumns(this.currentColumnHeaderAutoResizeStyle);
            }

            base.OnSizeChanged(e);
        }

        /// <summary>
        /// コントロールの領域内でマウスカーソルの移動が発生した場合の処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            var item = GetItemAt(e.X, e.Y);

            if (item != null)
            {
                Invalidate(item.Bounds);
            }

            base.OnMouseMove(e);
        }

        /// <summary>
        /// マウスカーソルがコントロールの領域外に侵入した場合の処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseEnter(EventArgs e)
        {
            this.flagColumnHeaderMouseMove = false;

            Invalidate();
            base.OnMouseEnter(e);
        }

        /// <summary>
        /// 列ヘッダの領域内でマウスカーソルの移動が発生した場合の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnColumnHeaderMouseMove(object sender, ListViewColumnHeaderWindowMouseMoveEventArgs e)
        {
            this.flagColumnHeaderMouseMove = true;
            this.columnHeaderMousePoint = e.Point;

            Refresh();
        }

        /// <summary>
        /// 列ヘッダの領域からマウスカーソルが脱出した場合の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnColumnHeaderMouseLeave(object sender, EventArgs e)
        {
            this.flagColumnHeaderMouseMove = false;

            Invalidate();
        }

        /// <summary>
        /// ウィンドウプロシージャ
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_PAINT:
                    PaintControl(ref m);
                    break;
                case WM_HSCROLL:
                case WM_VSCROLL:
                case WM_MOUSEWHEEL:
                    base.WndProc(ref m);
                    Invalidate();
                    break;
                case WM_ERASEBKGND:         // 無視
                    break;
                default:
                    base.WndProc(ref m);
                    break;
            }
        }
    }
}
