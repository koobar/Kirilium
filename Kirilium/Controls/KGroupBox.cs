using Kirilium.Themes;
using System.Runtime.Versioning;
using System.Windows.Forms;

namespace Kirilium.Controls
{
    [SupportedOSPlatform("windows")]
    public class KGroupBox : KControl
    {
        /// <summary>
        /// 描画処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            // テキストの描画
            var textSize = Renderer.MeasureText(this.Text, this.Font);
            var textX = this.DisplayRectangle.X + 5;
            var textY = this.DisplayRectangle.Y;
            Renderer.DrawText(
                e.Graphics,
                this.Text,
                this.Font,
                textX,
                textY,
                textSize.Width,
                textSize.Height,
                ThemeManager.CurrentTheme.GetColor(ColorKeys.ApplicationTextNormal),
                ThemeManager.CurrentTheme.GetColor(ColorKeys.GroupBoxBackColorNormal),
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);

            // 境界線の描画
            int borderY = textY + (textSize.Height / 2);
            var color = ThemeManager.CurrentTheme.GetColor(ColorKeys.ApplicationBorderNormal);
            Renderer.DrawLine(e.Graphics, this.DisplayRectangle.X, borderY, this.DisplayRectangle.X + 5, borderY, color);
            Renderer.DrawLine(e.Graphics, this.DisplayRectangle.X, borderY, this.DisplayRectangle.X, this.DisplayRectangle.Bottom - 1, color);
            Renderer.DrawLine(e.Graphics, this.DisplayRectangle.X, this.DisplayRectangle.Bottom - 1, this.DisplayRectangle.Right - 1, this.DisplayRectangle.Bottom - 1, color);
            Renderer.DrawLine(e.Graphics, this.DisplayRectangle.Right - 1, borderY, this.DisplayRectangle.Right - 1, this.DisplayRectangle.Bottom - 1, color);
            Renderer.DrawLine(e.Graphics, textSize.Width + textX, borderY, this.DisplayRectangle.Right - 1, borderY, color);

            base.OnPaint(e);
        }
    }
}
