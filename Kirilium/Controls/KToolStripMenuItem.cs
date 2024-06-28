using Kirilium.Themes;
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Kirilium.Controls
{
    [ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.MenuStrip | ToolStripItemDesignerAvailability.ContextMenuStrip)]
    public class KToolStripMenuItem : ToolStripMenuItem
    {
        // 非公開定数
        internal const int PADDING = 3;
        internal const int MENUITEM_GLYPH_WIDTH = 16;
        internal const int MENUITEM_GLYPH_HEIGHT = 16;

        // 非公開フィールド
        private readonly KeysConverter keysConverter;
        private bool flagMouseEnter;
        private bool flagDropDownOpen;

        // コンストラクタ
        public KToolStripMenuItem()
        {
            this.keysConverter = new KeysConverter();
            this.DropDownOpened += delegate
            {
                this.flagDropDownOpen = true;
            };
            this.DropDownClosed += delegate
            {
                this.flagDropDownOpen = false;
            };

            ThemeManager.ThemeChanged += OnThemeChanged;
        }

        // デストラクタ
        ~KToolStripMenuItem()
        {
            ThemeManager.ThemeChanged -= OnThemeChanged;
        }

        /// <summary>
        /// テーマが変更された場合の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnThemeChanged(object sender, EventArgs e)
        {
            Invalidate();
        }

        #region マウス系の処理のオーバーライド

        protected override void OnMouseEnter(EventArgs e)
        {
            this.flagMouseEnter = true;

            Invalidate();
            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            this.flagMouseEnter = false;

            Invalidate();
            base.OnMouseLeave(e);
        }

        #endregion

        /// <summary>
        /// MenuStripのアイテムとして描画する。
        /// </summary>
        /// <param name="deviceContext"></param>
        /// <param name="clipRectangle"></param>
        protected virtual void PaintMenuStripItem(Graphics deviceContext, Rectangle clipRectangle)
        {
            var backColor = ThemeManager.CurrentTheme.GetColor(ColorKeys.ToolStripMenuItemNormalBackColor);
            if (this.flagMouseEnter)
            {
                backColor = ThemeManager.CurrentTheme.GetColor(ColorKeys.ToolStripMenuItemHotBackColor);
            }

            // 背景を塗りつぶす。
            Renderer.FillRect(deviceContext, clipRectangle, backColor);

            if (this.flagMouseEnter || this.flagDropDownOpen)
            {
                // 境界線を描画する。
                Renderer.DrawRect(
                    deviceContext,
                    clipRectangle.X, 
                    clipRectangle.Y,
                    clipRectangle.Right - 1, 
                    clipRectangle.Bottom - 1, 
                    ThemeManager.CurrentTheme.GetColor(ColorKeys.ApplicationBorderNormal));
            }

            // テキストの描画
            Renderer.DrawText(
                deviceContext,
                this.Text,
                this.Font,
                clipRectangle,
                ThemeManager.CurrentTheme.GetColor(ColorKeys.ApplicationTextNormal),
                backColor,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
        }

        /// <summary>
        /// ToolStripMenuItemのアイテムとして描画する。
        /// </summary>
        /// <param name="deviceContext"></param>
        /// <param name="clipRectangle"></param>
        protected virtual void PaintMenuItem(Graphics deviceContext, Rectangle clipRectangle)
        {
            var backColor = ThemeManager.CurrentTheme.GetColor(ColorKeys.ToolStripMenuItemNormalBackColor);
            if (this.flagMouseEnter)
            {
                backColor = ThemeManager.CurrentTheme.GetColor(ColorKeys.ToolStripMenuItemHotBackColor);
            }

            // 背景を塗りつぶす。
            Renderer.FillRect(deviceContext, clipRectangle, backColor);

            var shortcutKeysDisplayString = this.keysConverter.ConvertToString(this.ShortcutKeys);
            var shortcutKeysWidth = TextRenderer.MeasureText(shortcutKeysDisplayString, this.Font).Width;
            var glyphRect = new Rectangle(clipRectangle.X + PADDING, clipRectangle.Y + PADDING, MENUITEM_GLYPH_WIDTH, MENUITEM_GLYPH_HEIGHT);
            var shortcutKeysRect = new Rectangle(clipRectangle.Right - ((MENUITEM_GLYPH_WIDTH + PADDING * 2) + shortcutKeysWidth), clipRectangle.Y, shortcutKeysWidth, clipRectangle.Height);
            var textRect = new Rectangle(clipRectangle.X + MENUITEM_GLYPH_WIDTH + PADDING * 2, clipRectangle.Y, clipRectangle.Right - shortcutKeysRect.Width + PADDING * 2, clipRectangle.Height);

            // チェック状態の描画
            if (this.CheckState == CheckState.Unchecked)
            {
                if (this.Image != null)
                {
                    Renderer.DrawImageUnscaled(deviceContext, glyphRect, this.Image);
                }
            }
            else if (this.CheckState == CheckState.Checked)
            {
                Renderer.DrawImageUnscaled(deviceContext, glyphRect, IconRenderer.GetToolStripMenuItemCheckedGlyph(MENUITEM_GLYPH_WIDTH, MENUITEM_GLYPH_HEIGHT));
            }
            else if (this.CheckState == CheckState.Indeterminate)
            {
                Renderer.DrawImageUnscaled(deviceContext, glyphRect, IconRenderer.GetToolStripMenuItemIndeterminateGlyph(MENUITEM_GLYPH_WIDTH, MENUITEM_GLYPH_HEIGHT));
            }

            // ドロップダウンメニューが存在する場合、それを示す三角形を描画する。
            if (this.DropDownItems.Count > 0)
            {
                int glyphWidth = 5;
                int glyphHeight = 8;

                Renderer.DrawImageUnscaled(
                    deviceContext, 
                    textRect.Right + PADDING - glyphWidth,
                    (textRect.Bottom / 2) - (glyphHeight / 2) - (PADDING / 2),
                    glyphWidth, 
                    glyphHeight, 
                    IconRenderer.GetToolStripMenuItemHasDropdownGlyph(glyphWidth, glyphHeight));
            }

            // テキストの描画
            Renderer.DrawText(
                deviceContext,
                this.Text,
                this.Font,
                textRect,
                ThemeManager.CurrentTheme.GetColor(ColorKeys.ApplicationTextNormal),
                backColor,
                TextFormatFlags.Left | TextFormatFlags.VerticalCenter);

            // ショートカットキーの描画が有効か？
            if (this.ShowShortcutKeys && this.ShortcutKeys != Keys.None)
            {
                // ショートカットキーの描画
                Renderer.DrawText(
                    deviceContext,
                    shortcutKeysDisplayString,
                    this.Font,
                    shortcutKeysRect,
                    ThemeManager.CurrentTheme.GetColor(ColorKeys.ApplicationTextNormal),
                    backColor,
                    TextFormatFlags.Left | TextFormatFlags.VerticalCenter);
            }
        }

        /// <summary>
        /// 描画処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            // コントロールを初期化
            e.Graphics.Clear(ThemeManager.CurrentTheme.GetColor(ColorKeys.ApplicationClearColor));
            
            if (this.Owner != null)
            {
                if (this.Owner is MenuStrip)
                {
                    PaintMenuStripItem(e.Graphics, e.ClipRectangle);
                }
                else if (this.OwnerItem is ToolStripMenuItem || this.Owner is ContextMenuStrip)
                {
                    PaintMenuItem(e.Graphics, e.ClipRectangle);
                }
            }
        }

    }
}
