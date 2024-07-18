using Kirilium.Themes;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.Versioning;
using System.Windows.Forms;

namespace Kirilium.Controls
{
    [SupportedOSPlatform("windows")]
    public class KComboBox : ComboBox
    {
        // 非公開定数
        private const int COMBOBOX_TEXT_PADDING = 2;

        // コンストラクタ
        public KComboBox()
        {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.ResizeRedraw, true);

            base.FlatStyle = FlatStyle.Flat;
            base.DrawMode = DrawMode.OwnerDrawVariable;
            base.DropDownStyle = ComboBoxStyle.DropDownList;

            ThemeManager.ThemeChanged += OnThemeChanged;
        }

        // デストラクタ
        ~KComboBox()
        {
            ThemeManager.ThemeChanged -= OnThemeChanged;
        }

        private void OnThemeChanged(object sender, EventArgs e)
        {
            Invalidate();
        }

        #region プロパティ

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new Color ForeColor { get; set; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new Color BackColor { get; set; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new FlatStyle FlatStyle { get; set; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new ComboBoxStyle DropDownStyle { get; set; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new DrawMode DrawMode { get; set; }

        protected bool IsMouseOver
        {
            get
            {
                if (this.DisplayRectangle.Contains(PointToClient(Cursor.Position)))
                {
                    return true;
                }

                return false;
            }
        }

        #endregion

        /// <summary>
        /// 背景色を取得する。
        /// </summary>
        /// <returns></returns>
        protected virtual Color GetBackColor()
        {
            if (!this.Enabled)
            {
                return ThemeManager.CurrentTheme.GetColor(ColorKeys.ComboBoxBackColorDisabled);
            }

            return ThemeManager.CurrentTheme.GetColor(ColorKeys.ComboBoxBackColorNormal);
        }

        /// <summary>
        /// アイテムの背景色を取得する。
        /// </summary>
        /// <returns></returns>
        protected virtual Color GetItemBackColor(DrawItemState state)
        {
            if (state.HasFlag(DrawItemState.Selected))
            {
                return ThemeManager.CurrentTheme.GetColor(ColorKeys.ComboBoxItemBackColorSelected);
            }

            return ThemeManager.CurrentTheme.GetColor(ColorKeys.ComboBoxItemBackColorNormal);
        }

        /// <summary>
        /// コントロールの描画を行い、そのバッファであるビットマップを返す。
        /// </summary>
        /// <returns></returns>
        protected virtual Bitmap PaintControl()
        {
            var buffer = new Bitmap(this.ClientRectangle.Width, this.ClientRectangle.Height);
            var bufferRenderer = Graphics.FromImage(buffer);
            var rect = new Rectangle(0, 0, this.ClientRectangle.Width, this.ClientRectangle.Height);
            var backColor = GetBackColor();

            // コントロールの背景を塗りつぶす。
            Renderer.FillRect(bufferRenderer, rect, backColor);

            // コントロールの境界線を描画する。
            Renderer.DrawRect(bufferRenderer, rect.Left, rect.Top, rect.Width - 1, rect.Height - 1, KControl.GetBorderColor(this.Focused, this.IsMouseOver, this.Enabled));

            // アイコンを描画
            var iconWidth = 10;
            var iconHeight = 5;
            var icon = IconRenderer.GetComboBoxTriangle(iconWidth, iconHeight);
            Renderer.DrawImageUnscaled(
                bufferRenderer,
                rect.Width - (COMBOBOX_TEXT_PADDING * 2) - iconWidth,
                rect.Top + iconHeight + COMBOBOX_TEXT_PADDING * 2,
                iconWidth,
                iconHeight,
                icon);

            // テキストを描画する。
            Renderer.DrawText(
                bufferRenderer,
                this.Text,
                this.Font,
                rect.Left + COMBOBOX_TEXT_PADDING,
                rect.Top + COMBOBOX_TEXT_PADDING,
                rect.Width - (COMBOBOX_TEXT_PADDING * 2) - iconWidth,
                rect.Height - (COMBOBOX_TEXT_PADDING * 2),
                ThemeManager.CurrentTheme.GetColor(ColorKeys.ApplicationTextNormal),
                backColor,
                TextFormatFlags.Default | TextFormatFlags.VerticalCenter | TextFormatFlags.EndEllipsis);

            // 後始末
            bufferRenderer.Dispose();
            return buffer;
        }

        /// <summary>
        /// 描画処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            // コントロールを初期化
            e.Graphics.Clear(ThemeManager.CurrentTheme.GetColor(ColorKeys.ApplicationClearColor));

            using (var buffer = PaintControl())
            {
                Renderer.DrawImageUnscaled(e.Graphics, Point.Empty, buffer);
            }
        }
        
        /// <summary>
        /// アイテムの描画処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            var itemBackColor = GetItemBackColor(e.State);

            // 背景を塗りつぶす。
            Renderer.FillRect(e.Graphics, e.Bounds, itemBackColor);

            if (e.Index >= 0)
            {
                Renderer.DrawText(
                    e.Graphics,
                    this.Items[e.Index].ToString(),
                    this.Font,
                    e.Bounds.Left + COMBOBOX_TEXT_PADDING,
                    e.Bounds.Top + COMBOBOX_TEXT_PADDING,
                    e.Bounds.Width - (COMBOBOX_TEXT_PADDING * 2),
                    e.Bounds.Height - (COMBOBOX_TEXT_PADDING * 2),
                    ThemeManager.CurrentTheme.GetColor(ColorKeys.ApplicationTextNormal),
                    itemBackColor,
                    TextFormatFlags.Default | TextFormatFlags.EndEllipsis);
            }
        }

        protected override void OnLostFocus(EventArgs e)
        {
            Invalidate();
            base.OnLostFocus(e);
        }

        protected override void OnGotFocus(EventArgs e)
        {
            Invalidate();
            base.OnGotFocus(e);
        }
    }
}
