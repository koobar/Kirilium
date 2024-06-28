using Kirilium.Controls.Elements;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Kirilium.Controls
{
    public class KSeekBar : Control
    {
        // 非公開フィールド
        private readonly SeekBarSlidePanel slidePanel;

        // イベント
        public event EventHandler MaximumValueChanged;
        public event EventHandler MinimumValueChanged;
        public event EventHandler ValueChanged;

        // コンストラクタ
        public KSeekBar()
        {
            this.slidePanel = new SeekBarSlidePanel();

            this.Size = new Size(100, 20);
            this.MaximumValue = 100;
            this.MinimumValue = 0;
            this.Value = 0;

            this.slidePanel.Dock = DockStyle.Fill;
            this.slidePanel.Parent = this;
            this.slidePanel.ValueChanged += delegate
            {
                OnValueChanged();
            };

            ThemeManager.ThemeChanged += OnThemeChanged;
        }

        // デストラクタ
        ~KSeekBar()
        {
            ThemeManager.ThemeChanged -= OnThemeChanged;
        }

        private void OnThemeChanged(object sender, EventArgs e)
        {
            Invalidate();
        }

        #region プロパティ

        /// <summary>
        /// マウスホイールの回転による値の変更を許可するかどうか
        /// </summary>
        public bool AllowMouseWheelValueChange { set; get; }

        /// <summary>
        /// 減少ステップ数
        /// </summary>
        public int DecrementStep { set; get; } = 3;

        /// <summary>
        /// 増加ステップ数
        /// </summary>
        public int IncrementStep { set; get; } = 3;

        /// <summary>
        /// パーセント
        /// </summary>
        public int Percent
        {
            get
            {
                var p = ((double)this.Value - this.MinimumValue) / ((double)this.MaximumValue - this.MinimumValue) * 100;

                return (int)Math.Round(p);
            }
        }

        /// <summary>
        /// 最大値
        /// </summary>
        public int MaximumValue
        {
            set
            {
                this.slidePanel.MaximumValue = value;

                OnMaximumValueChanged();
            }
            get
            {
                return this.slidePanel.MaximumValue;
            }
        }

        /// <summary>
        /// 最小値
        /// </summary>
        public int MinimumValue
        {

            set
            {
                this.slidePanel.MinimumValue = value;

                OnMinimumValueChanged();
            }
            get
            {
                return this.slidePanel.MinimumValue;
            }
        }

        /// <summary>
        /// 値
        /// </summary>
        public int Value
        {

            set
            {
                this.slidePanel.Value = value;

                OnValueChanged();
            }
            get
            {
                return this.slidePanel.Value;
            }
        }

        #endregion

        /// <summary>
        /// マウスのホイールが回転した場合の処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            if (this.AllowMouseWheelValueChange)
            {
                int value = this.Value;

                value += e.Delta < 0 ? this.IncrementStep : -this.DecrementStep;
                if (value < this.MinimumValue)
                {
                    value = this.MinimumValue;
                }
                else if (value > this.MaximumValue)
                {
                    value = this.MaximumValue;
                }

                this.Value = value;
            }

            base.OnMouseWheel(e);
        }

        /// <summary>
        /// 最大値が変更された場合の処理
        /// </summary>
        protected virtual void OnMaximumValueChanged()
        {
            this.MaximumValueChanged?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// 最小値が変更された場合の処理
        /// </summary>
        protected virtual void OnMinimumValueChanged()
        {
            this.MinimumValueChanged?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// 値が変更された場合の処理
        /// </summary>
        protected virtual void OnValueChanged()
        {
            this.ValueChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
