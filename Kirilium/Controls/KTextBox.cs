using Kirilium.Controls.Elements;
using Kirilium.Themes;
using System;
using System.Runtime.Versioning;
using System.Windows.Forms;

namespace Kirilium.Controls
{
    [SupportedOSPlatform("windows")]
    public class KTextBox : KControl
    {
        // 非公開フィールド
        private readonly InternalTextBox internalTextBox;

        // イベント
        public new event EventHandler TextChanged;
        public event EventHandler MultilineChanged;
        public event EventHandler ReadOnlyChanged;

        // コンストラクタ
        public KTextBox()
        {
            this.internalTextBox = new InternalTextBox();
            this.internalTextBox.Left = 1;
            this.internalTextBox.Top = 1;
            this.internalTextBox.Width = this.Width - 1 - this.internalTextBox.Left;
            this.internalTextBox.Height = this.Height - 1 - this.internalTextBox.Left;

            this.internalTextBox.TextChanged += delegate (object sender, EventArgs e)
            {
                this.TextChanged?.Invoke(sender, e);
            };
            this.internalTextBox.MultilineChanged += delegate (object sender, EventArgs e)
            {
                this.MultilineChanged?.Invoke(sender, e);
            };
            this.internalTextBox.ReadOnlyChanged += delegate (object sender, EventArgs e)
            {
                this.ReadOnlyChanged?.Invoke(sender, e);
            };

            this.Controls.Add(this.internalTextBox);
        }

        #region プロパティ

        /// <summary>
        /// 複数行モード
        /// </summary>
        public bool Multiline
        {
            set
            {
                this.internalTextBox.Multiline = value;
            }
            get
            {
                return this.internalTextBox.Multiline;
            }
        }

        /// <summary>
        /// 読み取り専用モード
        /// </summary>
        public bool ReadOnly
        {
            set
            {
                this.internalTextBox.ReadOnly = value;
            }
            get
            {
                return this.internalTextBox.ReadOnly;
            }
        }

        /// <summary>
        /// テキスト
        /// </summary>
        public new string Text
        {
            set
            {
                this.internalTextBox.Text = value;
            }
            get
            {
                return this.internalTextBox.Text;
            }
        }

        /// <summary>
        /// 複数行モードの場合に表示されるスクロールバーを設定する。
        /// </summary>
        public ScrollBars ScrollBars
        {
            set
            {
                this.internalTextBox.ScrollBars = value;
            }
            get
            {
                return this.internalTextBox.ScrollBars;
            }
        }

        /// <summary>
        /// コンテキストメニュー
        /// </summary>
        public new ContextMenuStrip ContextMenuStrip
        {
            set
            {
                this.internalTextBox.ContextMenuStrip = value;
            }
            get
            {
                return this.internalTextBox.ContextMenuStrip;
            }
        }

        #endregion

        /// <summary>
        /// 描画処理
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
                GetBorderColor(this.internalTextBox.Focused, this.IsMouseOver, this.internalTextBox.Enabled));
        }

        /// <summary>
        /// コントロールのサイズが変更された場合の処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnSizeChanged(EventArgs e)
        {
            this.internalTextBox.Left = 1;
            this.internalTextBox.Top = 1;
            this.internalTextBox.Width = this.Width - 1 - this.internalTextBox.Left;
            this.internalTextBox.Height = this.Height - 1 - this.internalTextBox.Left;

            base.OnSizeChanged(e);
        }
    }
}
