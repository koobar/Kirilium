using Kirilium.Controls.Elements;
using Kirilium.Themes;
using System.Runtime.Versioning;
using System.Windows.Forms;
using static Kirilium.WinApi.Dwmapi;

namespace Kirilium.Controls
{
    [SupportedOSPlatform("windows")]
    public class KContextMenuStrip : ContextMenuStrip
    {
        // コンストラクタ
        public KContextMenuStrip()
        {
            base.RenderMode = ToolStripRenderMode.Professional;
            base.Renderer = new ToolStripProfessionalRenderer(new KColorTable());
            base.GripStyle = ToolStripGripStyle.Hidden;
            base.Padding = new Padding(3);

            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.ResizeRedraw, true);

            if (OperatingSystem.IsWindows11())
            {
                var preference = DWMWCP_ROUNDSMALL;
                DwmSetWindowAttribute(this.Handle, DWMWA_WINDOW_CORNER_PREFERENCE, ref preference, sizeof(uint));
            }
        }

        /// <summary>
        /// 描画処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            var backColor = ThemeManager.CurrentTheme.GetColor(ColorKeys.MenuStripBackColor);

            e.Graphics.Clear(backColor);
            Kirilium.Renderer.FillRect(e.Graphics, this.DisplayRectangle, backColor);

            base.OnPaint(e);
        }
    }
}
