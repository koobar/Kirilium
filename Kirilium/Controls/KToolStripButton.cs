using Kirilium.Themes;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Kirilium.Controls
{
    public class KToolStripButton : ToolStripButton
    {
        // 非公開フィールド
        private bool flagMouseEnter;
        private bool flagMousePress;

        // コンストラクタ
        public KToolStripButton()
        {

        }

        /// <summary>
        /// マウスカーソルがコントロールと重なった場合の処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseEnter(EventArgs e)
        {
            this.flagMouseEnter = true;

            Invalidate();
            base.OnMouseEnter(e);
        }

        /// <summary>
        /// マウスカーソルがコントロールの領域外に移動した場合の処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseLeave(EventArgs e)
        {
            this.flagMouseEnter = false;

            Invalidate();
            base.OnMouseLeave(e);
        }

        /// <summary>
        /// コントロールの領域内でマウスのボタンが押下された場合の処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            this.flagMousePress = true;

            Invalidate();
            base.OnMouseDown(e);
        }

        /// <summary>
        /// 押下されているマウスのボタンが離された場合の処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            this.flagMousePress = false;

            Invalidate();
            base.OnMouseUp(e);
        }

        /// <summary>
        /// 描画処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            var backColor = ThemeManager.CurrentTheme.GetColor(ColorKeys.ToolStripBackColor);

            // 背景を初期化
            e.Graphics.Clear(backColor);

            if (this.flagMouseEnter)
            {
                backColor = ThemeManager.CurrentTheme.GetColor(ColorKeys.ButtonBackColorMouseOver);
                if (this.flagMousePress)
                {
                    backColor = ThemeManager.CurrentTheme.GetColor(ColorKeys.ButtonBackColorMouseClick);
                }

                // 塗りつぶし
                Renderer.FillRect(
                    e.Graphics,
                    e.ClipRectangle.X, 
                    e.ClipRectangle.Y, 
                    e.ClipRectangle.Right - 1, 
                    e.ClipRectangle.Bottom - 1,
                    backColor);

                // 境界線の描画
                Renderer.DrawRect(
                    e.Graphics,
                    e.ClipRectangle.X, 
                    e.ClipRectangle.Y, 
                    e.ClipRectangle.Right - 2, 
                    e.ClipRectangle.Bottom - 1,
                    ThemeManager.CurrentTheme.GetColor(ColorKeys.ApplicationBorderHighlight));
            }

            if (this.DisplayStyle == ToolStripItemDisplayStyle.Image)
            {
                // 画像の表示座標を取得する。
                var imgX = (this.Width / 2) - (this.Image.Width / 2);
                var imgY = (this.Height / 2) - (this.Image.Height / 2);

                // ボタンのサイズより画像のサイズのほうが小さいか、同じサイズならリサイズせず表示する。
                // そうでなければ、ボタンのサイズに合うようにリサイズして表示する。
                if (this.Image.Width <= this.Width && this.Image.Height <= this.Height)
                {
                    Renderer.DrawImageUnscaled(
                        e.Graphics,
                        imgX,
                        imgY,
                        this.Image.Width,
                        this.Image.Height,
                        this.Image);
                }
                else
                {
                    Renderer.DrawImage(
                        e.Graphics,
                        new Rectangle(e.ClipRectangle.X, e.ClipRectangle.Y, e.ClipRectangle.Right - 3, e.ClipRectangle.Bottom - 3),
                        this.Image);
                }
            }
            else if (this.DisplayStyle == ToolStripItemDisplayStyle.ImageAndText)
            {
                var imgX = 2;
                var imgY = (this.Height / 2) - (this.Image.Height / 2);

                // ボタンのサイズより画像のサイズのほうが小さいか、同じサイズならリサイズせず表示する。
                // そうでなければ、ボタンのサイズに合うようにリサイズして表示する。
                if (this.Image.Width <= this.Width && this.Image.Height <= this.Height)
                {
                    Renderer.DrawImageUnscaled(
                        e.Graphics,
                        imgX,
                        imgY,
                        this.Image.Width,
                        this.Image.Height,
                        this.Image);
                }
                else
                {
                    Renderer.DrawImage(
                        e.Graphics,
                        new Rectangle(e.ClipRectangle.X, e.ClipRectangle.Y, e.ClipRectangle.Right - 3, e.ClipRectangle.Bottom - 3),
                        this.Image);
                }

                // テキストを描画
                Renderer.DrawText(
                    e.Graphics,
                    this.Text,
                    this.Font,
                    new Rectangle(
                        e.ClipRectangle.Left + this.Height,
                        0,
                        e.ClipRectangle.Width - (e.ClipRectangle.Left + this.Height),
                        e.ClipRectangle.Height),
                    ThemeManager.CurrentTheme.GetColor(ColorKeys.ApplicationTextNormal),
                    backColor,
                    TextFormatFlags.Left | TextFormatFlags.VerticalCenter);
            }
            else if (this.DisplayStyle == ToolStripItemDisplayStyle.Text)
            {
                // テキストを描画
                Renderer.DrawText(
                    e.Graphics,
                    this.Text,
                    this.Font,
                    e.ClipRectangle,
                    ThemeManager.CurrentTheme.GetColor(ColorKeys.ApplicationTextNormal),
                    backColor,
                    TextFormatFlags.Left | TextFormatFlags.VerticalCenter);
            }
        }
    }
}
