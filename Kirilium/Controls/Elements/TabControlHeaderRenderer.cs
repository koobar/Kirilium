using Kirilium.Themes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.Versioning;
using System.Windows.Forms;

namespace Kirilium.Controls.Elements
{
    [SupportedOSPlatform("windows")]
    internal class TabControlHeaderRenderer : KControl
    {
        // 非公開定数
        private const int ICON_SIZE = 16;
        private const int CLOSE_BUTTON_SIZE = 14;
        private const int PADDING = 3;

        // 非公開フィールド
        private readonly List<TabHeaderRenderingElement> tabHeaders;
        private int selectedIndex;

        // イベント
        public event EventHandler SelectedIndexChanged;
        public event EventHandler<TabCloseEventArgs> TabCloseButtonPressed;

        // コンストラクタ
        public TabControlHeaderRenderer() : base()
        {
            this.tabHeaders = new List<TabHeaderRenderingElement>();
        }

        #region プロパティ

        /// <summary>
        /// 描画されるタブヘッダの一覧
        /// </summary>
        public List<TabHeaderRenderingElement> TabHeaders
        {
            get
            {
                return this.tabHeaders;
            }
        }

        /// <summary>
        /// 選択されたタブのインデックス
        /// </summary>
        public int SelectedIndex
        {
            set
            {
                this.selectedIndex = value;

                if (this.selectedIndex >= 0 && this.selectedIndex < this.tabHeaders.Count)
                {
                    for (int i = 0; i < this.tabHeaders.Count; ++i)
                    {
                        this.tabHeaders[i].IsSelected = false;
                    }
                    this.tabHeaders[value].IsSelected = true;
                }

                Invalidate();
                OnSelectedIndexChanged();
            }
            get
            {
                return this.selectedIndex;
            }
        }

        #endregion

        /// <summary>
        /// 選択されたタブのインデックスが変更された場合の処理
        /// </summary>
        private void OnSelectedIndexChanged()
        {
            this.SelectedIndexChanged?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// 指定されたインデックスのタブヘッダの領域を示す矩形を取得する。
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        private Rectangle GetTabRect(int index)
        {
            int x = this.DisplayRectangle.X;
            for (int i = 0; i < this.tabHeaders.Count; ++i)
            {
                var tabIconSize = 0;
                if (this.tabHeaders[i].Icon != null)
                {
                    tabIconSize += ICON_SIZE + PADDING;
                }
                if (this.tabHeaders[i].DrawCloseButton)
                {
                    tabIconSize += CLOSE_BUTTON_SIZE + PADDING;
                }

                var captionSize = TextRenderer.MeasureText(this.tabHeaders[i].Text, this.Font);
                var tabSize = new Size(tabIconSize + captionSize.Width, Math.Max(Math.Max(Math.Max(ICON_SIZE, CLOSE_BUTTON_SIZE), captionSize.Height), this.DisplayRectangle.Height - 2));
                var tabRect = new Rectangle(x, this.DisplayRectangle.Y + 1, tabSize.Width, tabSize.Height - PADDING);

                if (index == i)
                {
                    return tabRect;
                }

                x += tabRect.Width;
            }

            return Rectangle.Empty;
        }

        /// <summary>
        /// 指定されたタブヘッダの領域のうち、テキストが描画される領域を示す矩形を取得する。
        /// </summary>
        /// <param name="tabRect"></param>
        /// <param name="tabIcon"></param>
        /// <returns></returns>
        private Rectangle GetCaptionRect(Rectangle tabRect, Image tabIcon)
        {
            if (tabIcon == null)
            {
                return tabRect;
            }

            return new Rectangle(tabRect.X + ICON_SIZE + PADDING, tabRect.Y, tabRect.Width - (tabRect.X + ICON_SIZE + PADDING), tabRect.Height);
        }

        /// <summary>
        /// 指定されたタブヘッダの領域のうち、閉じるボタンが描画される領域を示す矩形を取得する。
        /// </summary>
        /// <param name="tabRect"></param>
        /// <param name="drawCloseButton"></param>
        /// <returns></returns>
        private Rectangle GetCloseButtonRect(Rectangle tabRect, bool drawCloseButton)
        {
            if (!drawCloseButton)
            {
                return Rectangle.Empty;
            }

            return new Rectangle(
                tabRect.Right - (CLOSE_BUTTON_SIZE + PADDING),
                (tabRect.Height / 2) - (CLOSE_BUTTON_SIZE / 2) + PADDING, //(tabRect.Height / 2) - (CLOSE_BUTTON_SIZE / 2) + (PADDING / 2),
                CLOSE_BUTTON_SIZE, 
                tabRect.Height - PADDING);
        }

        /// <summary>
        /// 指定されたインデックスのタブヘッダの背景色を取得する。
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        private Color GetTabHeaderBackColor(int index)
        {
            var backColor = ThemeManager.CurrentTheme.GetColor(ColorKeys.TabControlHeaderBackColorNormal);

            if (this.tabHeaders[index].IsHot)
            {
                backColor = ThemeManager.CurrentTheme.GetColor(ColorKeys.TabControlHeaderBackColorHot);
            }

            if (this.tabHeaders[index].IsSelected)
            {
                backColor = ThemeManager.CurrentTheme.GetColor(ColorKeys.TabControlHeaderBackColorSelected);
            }

            return backColor;
        }

        /// <summary>
        /// 指定されたインデックスのタブヘッダの文字色を取得する。
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        private Color GetTabHeaderTextColor(int index)
        {
            var color = ThemeManager.CurrentTheme.GetColor(ColorKeys.ApplicationTextNormal);

            if (!this.Enabled)
            {
                color = ThemeManager.CurrentTheme.GetColor(ColorKeys.ApplicationTextDisabled);
            }

            if (this.tabHeaders[index].IsHot)
            {
                color = ThemeManager.CurrentTheme.GetColor(ColorKeys.ApplicationTextHighlight);
            }

            if (this.tabHeaders[index].IsSelected)
            {
                color = ThemeManager.CurrentTheme.GetColor(ColorKeys.ApplicationTextHighlight);
            }

            return color;
        }

        #region マウス系処理のオーバーライド

        /// <summary>
        /// マウスカーソルが移動した場合の処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            for (int i = 0; i < this.tabHeaders.Count; ++i)
            {
                var tabRect = GetTabRect(i);
                this.tabHeaders[i].IsHot = tabRect.Contains(PointToClient(Cursor.Position));
                this.tabHeaders[i].IsCloseButtonHot = this.tabHeaders[i].DrawCloseButton && GetCloseButtonRect(tabRect, true).Contains(PointToClient(Cursor.Position));
            }

            Invalidate();
        }

        /// <summary>
        /// マウスカーソルがコントロールの領域から脱出した場合の処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseLeave(EventArgs e)
        {
            for (int i = 0; i < this.tabHeaders.Count; ++i)
            {
                this.tabHeaders[i].IsHot = false;
                this.tabHeaders[i].IsCloseButtonHot = false;
            }

            Invalidate();
        }

        /// <summary>
        /// マウスのボタンが押下された場合の処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                for (int i = 0; i < this.tabHeaders.Count; ++i)
                {
                    var tabRect = GetTabRect(i);

                    if (tabRect.Contains(PointToClient(Cursor.Position)))
                    {
                        this.SelectedIndex = i;
                        
                        if (this.tabHeaders[i].DrawCloseButton)
                        {
                            if (GetCloseButtonRect(tabRect, true).Contains(PointToClient(Cursor.Position)))
                            {
                                this.TabCloseButtonPressed?.Invoke(this, new TabCloseEventArgs(i));
                            }
                        }

                        break;
                    }
                }
            }

            base.OnMouseDown(e);
        }

        #endregion

        /// <summary>
        /// 描画処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            // コントロールの初期化
            e.Graphics.Clear(ThemeManager.CurrentTheme.GetColor(ColorKeys.TabControlHeaderBackColorNormal));

            // タブの描画
            for (int i = 0; i < this.tabHeaders.Count; ++i)
            {
                var tabRect = GetTabRect(i);
                var backColor = GetTabHeaderBackColor(i);
                var textColor = GetTabHeaderTextColor(i);

                // 背景を塗りつぶす。
                Renderer.FillRect(e.Graphics, tabRect.X, tabRect.Y, tabRect.Width, tabRect.Height - 1, backColor);

                // タブのテキストの描画
                Renderer.DrawText(
                    e.Graphics,
                    this.tabHeaders[i].Text,
                    this.Font,
                    GetCaptionRect(tabRect, this.tabHeaders[i].Icon),
                    textColor,
                    backColor,
                    TextFormatFlags.Left | TextFormatFlags.VerticalCenter);

                // 閉じるボタンが有効なら描画する。
                if (this.tabHeaders[i].DrawCloseButton)
                {
                    var closeButtonBackColor = backColor;
                    if (this.tabHeaders[i].IsCloseButtonHot)
                    {
                        closeButtonBackColor = ThemeManager.CurrentTheme.GetColor(ColorKeys.TabControlHeaderCloseButtonBackColorHot);
                    }

                    using (var img = IconRenderer.GetCloseButton(CLOSE_BUTTON_SIZE, CLOSE_BUTTON_SIZE, closeButtonBackColor, textColor))
                    {
                        Renderer.DrawImageUnscaled(e.Graphics, GetCloseButtonRect(tabRect, true), img);
                    }
                }
            }

            using (var pen = Renderer.CreateBoldPen(ThemeManager.CurrentTheme.GetColor(ColorKeys.ApplicationBorderHighlight)))
            {
                Renderer.DrawLine(
                    e.Graphics,
                    this.DisplayRectangle.X,
                    this.DisplayRectangle.Bottom - PADDING,
                    this.DisplayRectangle.Right,
                    this.DisplayRectangle.Bottom - PADDING,
                    pen);
            }
        }
    }
}
