using Kirilium.Controls.Elements;
using Kirilium.Themes;
using System;
using System.Windows.Forms;

namespace Kirilium.Controls
{
    public class KListBox : UserControl
    {
        // 非公開フィールド
        private readonly InternalListBox internalListBox;

        // コンストラクタ
        public KListBox()
        {
            this.Padding = new Padding(1);
            this.internalListBox = new InternalListBox();
            this.internalListBox.Parent = this;
            this.internalListBox.Dock = DockStyle.Fill;
            this.internalListBox.GotFocus += delegate
            {
                Invalidate();
            };
            this.internalListBox.LostFocus += delegate
            {
                Invalidate();
            };
            this.internalListBox.IntegralHeight = false;

            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.ResizeRedraw, true);

            ThemeManager.ThemeChanged += OnThemeChanged;
        }

        // デストラクタ
        ~KListBox()
        {
            ThemeManager.ThemeChanged -= OnThemeChanged;
        }

        private void OnThemeChanged(object sender, EventArgs e)
        {
            Invalidate();
        }

        #region プロパティ

        /// <summary>
        /// アイテム
        /// </summary>
        public ListBox.ObjectCollection Items
        {
            get
            {
                return this.internalListBox.Items;
            }
        }

        protected bool IsMouseOver
        {
            get
            {
                if (this.DisplayRectangle.Contains(PointToClient(Cursor.Position)) ||
                    this.internalListBox.DisplayRectangle.Contains(PointToClient(Cursor.Position)))
                {
                    return true;
                }

                return false;
            }
        }

        #endregion

        /// <summary>
        /// コントロールの描画処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            // コントロールを初期化
            e.Graphics.Clear(ThemeManager.CurrentTheme.GetColor(ColorKeys.ApplicationClearColor));

            // コントロールの境界線を描画する。
            Renderer.DrawRect(
                e.Graphics, 
                0,
                0,
                this.DisplayRectangle.Right,
                this.DisplayRectangle.Bottom,
                KControl.GetBorderColor(this.Focused || this.internalListBox.Focused, this.IsMouseOver, this.Enabled));
        }

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
    }
}
