using Kirilium.Themes;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Kirilium.Controls
{
    public class KButton : Button
    {
        // コンストラクタ
        public KButton() : base()
        {
            this.Size = new Size(75, 25);

            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            this.DoubleBuffered = true;

            ThemeManager.ThemeChanged += OnThemeChanged;
        }

        // デストラクタ
        ~KButton()
        {
            ThemeManager.ThemeChanged -= OnThemeChanged;
        }

        private void OnThemeChanged(object sender, EventArgs e)
        {
            Invalidate();
        }

        #region プロパティ

        /// <summary>
        /// コントロールがクリックされている最中であるかどうかを示す。
        /// </summary>
        protected bool IsMouseClick { private set; get; }

        /// <summary>
        /// マウスカーソルがコントロールの領域内に存在するかどうかを示す。
        /// </summary>
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

        #region マウス関連の処理のオーバーライド

        /// <summary>
        /// マウスカーソルがコントロールの領域内に侵入した場合の処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseEnter(EventArgs e)
        {
            Invalidate();
            base.OnMouseEnter(e);
        }

        /// <summary>
        /// マウスカーソルがコントロールの領域外に移動した場合の処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseLeave(EventArgs e)
        {
            Invalidate();
            base.OnMouseLeave(e);
        }

        /// <summary>
        /// マウスのボタンが押下された場合の処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (this.IsMouseOver)
            {
                if (e.Button == MouseButtons.Left)
                {
                    this.IsMouseClick = true;
                }
            }

            Invalidate();
            base.OnMouseDown(e);
        }

        /// <summary>
        /// マウスのボタンが離された場合の処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            this.IsMouseClick = false;

            Invalidate();
            base.OnMouseUp(e);
        }

        #endregion

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
                KControl.GetBorderColor(this.Focused, this.IsMouseOver, this.Enabled));
        }
    }
}
