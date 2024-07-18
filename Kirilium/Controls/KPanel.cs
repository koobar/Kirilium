using Kirilium.Themes;
using System.Runtime.Versioning;
using System.Windows.Forms;

namespace Kirilium.Controls
{
    [SupportedOSPlatform("windows")]
    public class KPanel : Panel
    {
        // コンストラクタ
        public KPanel()
        {
            // 描画設定
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
        }

        /// <summary>
        /// 描画処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.Clear(ThemeManager.CurrentTheme.GetColor(ColorKeys.PanelBackColor));

            if (this.BorderStyle != BorderStyle.None)
            {
                Renderer.DrawRect(
                    e.Graphics,
                    this.DisplayRectangle.X,
                    this.DisplayRectangle.Y,
                    this.DisplayRectangle.Width - 1,
                    this.DisplayRectangle.Height - 1,
                    ThemeManager.CurrentTheme.GetColor(ColorKeys.ApplicationBorderNormal));
            }

            base.OnPaint(e);
        }
    }
}
