using Kirilium.Themes;
using System;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Kirilium.Controls
{
    [ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.MenuStrip | ToolStripItemDesignerAvailability.ContextMenuStrip)]
    public class KToolStripSeparator : ToolStripSeparator
    {
        // コンストラクタ
        public KToolStripSeparator()
        {
            ThemeManager.ThemeChanged += OnThemeChanged;
        }

        // デストラクタ
        ~KToolStripSeparator()
        {
            ThemeManager.ThemeChanged -= OnThemeChanged;
        }

        private void OnThemeChanged(object sender, EventArgs e)
        {
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            // 背景を塗りつぶす。
            Renderer.FillRect(e.Graphics, e.ClipRectangle, ThemeManager.CurrentTheme.GetColor(ColorKeys.ToolStripMenuItemNormalBackColor));

            // 境界線の描画
            Renderer.DrawLine(
                e.Graphics,
                e.ClipRectangle.X + KToolStripMenuItem.MENUITEM_GLYPH_WIDTH + KToolStripMenuItem.PADDING * 2,
                e.ClipRectangle.Y + 3,
                e.ClipRectangle.Width - KToolStripMenuItem.PADDING,
                e.ClipRectangle.Y + 3,
                ThemeManager.CurrentTheme.GetColor(ColorKeys.ApplicationBorderNormal));
        }
    }
}
