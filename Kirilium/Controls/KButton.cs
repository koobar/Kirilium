using Kirilium.Themes;
using System.Drawing;
using System.Runtime.Versioning;
using System.Windows.Forms;

namespace Kirilium.Controls
{
    [SupportedOSPlatform("windows")]
    public class KButton : KControl
    {
        // 非公開フィールド
        private Image image;

        // コンストラクタ
        public KButton() : base()
        {
            this.Width = 75;
            this.Height = 25;
        }

        #region プロパティ

        /// <summary>
        /// ボタンのイメージ
        /// </summary>
        public Image Image
        {
            set
            {
                this.image = value;
                Invalidate();
            }
            get
            {
                return this.image;
            }
        }

        #endregion

        /// <summary>
        /// ボタンの背景色を取得する。
        /// </summary>
        /// <returns></returns>
        protected virtual Color GetBackColor()
        {
            if (this.IsMouseClick)
            {
                return ThemeManager.CurrentTheme.GetColor(ColorKeys.ButtonBackColorMouseClick);
            }

            if (this.IsMouseOver)
            {
                return ThemeManager.CurrentTheme.GetColor(ColorKeys.ButtonBackColorMouseOver);
            }

            return ThemeManager.CurrentTheme.GetColor(ColorKeys.ButtonBackColorNormal);
        }

        /// <summary>
        /// 描画処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            // コントロールを初期化
            e.Graphics.Clear(ThemeManager.CurrentTheme.GetColor(ColorKeys.ApplicationClearColor));

            // ボタンの背景を塗りつぶす。
            Renderer.FillRect(e.Graphics, this.DisplayRectangle, GetBackColor());

            if (this.Image != null)
            {
                var rect = new Rectangle(
                    (this.Width / 2) - (this.Image.Width / 2),
                    (this.Height / 2) - (this.Image.Height / 2),
                    this.Image.Width,
                    this.Image.Height);
                e.Graphics.DrawImageUnscaled(this.Image, rect);
            }
            else
            {
                // テキストの描画
                Renderer.DrawText(
                    e.Graphics,
                    this.Text,
                    this.Font,
                    this.DisplayRectangle.X,
                    this.DisplayRectangle.Y,
                    this.DisplayRectangle.Width,
                    this.DisplayRectangle.Height,
                    ThemeManager.CurrentTheme.GetColor(ColorKeys.ApplicationTextNormal),
                    GetBackColor(),
                    TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            }

            // コントロールの境界線を描画する。
            Renderer.DrawRect(
                e.Graphics, 
                this.DisplayRectangle.X,
                this.DisplayRectangle.Y, 
                this.DisplayRectangle.Right - 1,
                this.DisplayRectangle.Bottom - 1, 
                GetBorderColor());
        }
    }
}
