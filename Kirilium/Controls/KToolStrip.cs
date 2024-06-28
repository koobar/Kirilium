using Kirilium.Controls.Elements;
using Kirilium.Themes;
using System;
using System.Windows.Forms;

namespace Kirilium.Controls
{
    public class KToolStrip : ToolStrip
    {
        // コンストラクタ
        public KToolStrip()
        {
            base.RenderMode = ToolStripRenderMode.Professional;
            base.Renderer = new ToolStripProfessionalRenderer(new KColorTable());
            base.GripStyle = ToolStripGripStyle.Hidden;
            base.Padding = new Padding(3);
            base.AutoSize = false;

            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.ResizeRedraw, true);

            ThemeManager.ThemeChanged += OnThemeChanged;
        }

        // デストラクタ
        ~KToolStrip()
        {
            ThemeManager.ThemeChanged -= OnThemeChanged;
        }

        private void OnThemeChanged(object sender, EventArgs e)
        {
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            // なぜか右端に描画される白い線を除去する。
            Kirilium.Renderer.DrawLine(
                e.Graphics, 
                e.ClipRectangle.Right - 1, 
                0, 
                e.ClipRectangle.Right - 1,
                e.ClipRectangle.Bottom - 1,
                ThemeManager.CurrentTheme.GetColor(ColorKeys.ToolStripBackColor));
        }
    }
}
