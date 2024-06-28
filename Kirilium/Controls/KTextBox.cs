using Kirilium.Controls.Elements;
using Kirilium.Themes;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Kirilium.Controls
{
    public class KTextBox : UserControl
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
            base.Padding = new Padding(1);
            
            this.internalTextBox = new InternalTextBox();
            this.internalTextBox.Dock = DockStyle.Fill;
            this.internalTextBox.Parent = this;
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

            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.ResizeRedraw, true);

            ThemeManager.ThemeChanged += OnThemeChanged;
        }

        // デストラクタ
        ~KTextBox()
        {
            ThemeManager.ThemeChanged -= OnThemeChanged;
        }

        private void OnThemeChanged(object sender, EventArgs e)
        {
            if (this.internalTextBox.ReadOnly)
            {
                this.internalTextBox.BackColor = ThemeManager.CurrentTheme.GetColor(ColorKeys.TextBoxBackColorReadOnly);
                this.internalTextBox.ForeColor = ThemeManager.CurrentTheme.GetColor(ColorKeys.TextBoxForeColorReadOnly);
            }
            else
            {
                this.internalTextBox.BackColor = ThemeManager.CurrentTheme.GetColor(ColorKeys.TextBoxBackColorNormal);
                this.internalTextBox.ForeColor = ThemeManager.CurrentTheme.GetColor(ColorKeys.ApplicationTextNormal);
            }

            Invalidate();
        }

        #region プロパティ

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new Padding Padding { get; set; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new Color BackColor { get; set; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new Color ForeColor { get; set; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new BorderStyle BorderStyle{ get; set; }

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

        protected bool IsMouseOver
        {
            get
            {
                if (this.DisplayRectangle.Contains(PointToClient(Cursor.Position)) ||
                    this.internalTextBox.DisplayRectangle.Contains(PointToClient(Cursor.Position)))
                {
                    return true;
                }

                return false;
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
                this.DisplayRectangle.X - 1,
                this.DisplayRectangle.Y - 1,
                this.DisplayRectangle.Right,
                this.DisplayRectangle.Bottom,
                KControl.GetBorderColor(this.internalTextBox.Focused, this.IsMouseOver, this.internalTextBox.Enabled));
        }
    }
}
