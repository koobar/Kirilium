using Kirilium.Controls.Elements;
using Kirilium.Themes;
using System;
using System.Runtime.Versioning;
using System.Windows.Forms;

namespace Kirilium.Controls
{
    [SupportedOSPlatform("windows")]
    public class KStatusStrip : StatusStrip
    {
        // コンストラクタ
        public KStatusStrip()
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
        ~KStatusStrip()
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

            // なぜか上端に描画される白い線を境界線で上書きする。
            Kirilium.Renderer.DrawLine(
                e.Graphics,
                e.ClipRectangle.X,
                e.ClipRectangle.Y,
                e.ClipRectangle.Right,
                e.ClipRectangle.Y,
                ThemeManager.CurrentTheme.GetColor(ColorKeys.ApplicationBorderNormal));
        }
    }
}
