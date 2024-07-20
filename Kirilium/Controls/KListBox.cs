using Kirilium.Controls.Elements;
using Kirilium.Themes;
using System.Runtime.Versioning;
using System.Windows.Forms;

namespace Kirilium.Controls
{
    [SupportedOSPlatform("windows")]
    public class KListBox : KControl
    {
        // 非公開フィールド
        private readonly InternalListBox internalListBox;

        // コンストラクタ
        public KListBox()
        {
            this.Padding = new Padding(1);
            this.internalListBox = new InternalListBox();
            this.internalListBox.Parent = this;
            this.internalListBox.Left = 1;
            this.internalListBox.Top = 1;
            this.internalListBox.Width = this.DisplayRectangle.Width - 1 - this.internalListBox.Left;
            this.internalListBox.Height = this.DisplayRectangle.Height - 1 - this.internalListBox.Top;
            this.internalListBox.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
            this.internalListBox.GotFocus += delegate
            {
                Invalidate();
            };
            this.internalListBox.LostFocus += delegate
            {
                Invalidate();
            };
            this.internalListBox.IntegralHeight = false;
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
                this.DisplayRectangle.Right - 1,
                this.DisplayRectangle.Bottom - 1,
                GetBorderColor(this.Focused || this.internalListBox.Focused, this.IsMouseOver, this.Enabled));
        }
    }
}
