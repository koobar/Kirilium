﻿using Kirilium.Controls.Elements;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Kirilium.Controls
{
    public class KHScrollBar : Control
    {
        // 非公開フィールド
        private readonly KButton decrementButton;
        private readonly KButton incrementButton;
        private readonly ScrollBarSlidePanel slidePanel;

        // イベント
        public event EventHandler MaximumValueChanged;
        public event EventHandler MinimumValueChanged;
        public event EventHandler ValueChanged;

        // コンストラクタ
        public KHScrollBar()
        {
            this.incrementButton = new KButton();
            this.decrementButton = new KButton();
            this.slidePanel = new ScrollBarSlidePanel();

            this.Size = new Size(100, 15);
            this.MaximumValue = 100;
            this.MinimumValue = 0;
            this.Value = 0;

            this.slidePanel.Dock = DockStyle.Fill;
            this.slidePanel.Parent = this;
            this.slidePanel.Slide += delegate
            {
                OnValueChanged();
            };
            this.decrementButton.Dock = DockStyle.Left;
            this.decrementButton.Parent = this;
            this.decrementButton.Image = IconRenderer.GetHScrollBarDecrementButtonIcon(5, 10);
            this.decrementButton.Click += delegate
            {
                Decrement();
            };
            this.incrementButton.Dock = DockStyle.Right;
            this.incrementButton.Parent = this;
            this.incrementButton.Image = IconRenderer.GetHScrollBarIncrementButtonIcon(5, 10);
            this.incrementButton.Click += delegate
            {
                Increment();
            };

            ThemeManager.ThemeChanged += OnThemeChanged;
        }

        // デストラクタ
        ~KHScrollBar()
        {
            ThemeManager.ThemeChanged -= OnThemeChanged;
        }

        private void OnThemeChanged(object sender, EventArgs e)
        {
            Invalidate();
        }

        #region プロパティ

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

        /// <summary>
        /// つまみのサイズ
        /// </summary>
        public int ThumbSize
        {
            set
            {
                this.slidePanel.ThumbSize = value;
            }
            get
            {
                return this.slidePanel.ThumbSize;
            }
        }

        #endregion

        /// <summary>
        /// 値のインクリメント
        /// </summary>
        private void Increment()
        {
            int value = this.Value + this.IncrementStep;

            if (value > this.MaximumValue)
            {
                value = this.MaximumValue;
            }

            this.Value = value;
        }

        /// <summary>
        /// 値のデクリメント
        /// </summary>
        private void Decrement()
        {
            int value = this.Value - this.DecrementStep;
            
            if (value < this.MinimumValue)
            {
                value = this.MinimumValue;
            }

            this.Value = value;
        }

        /// <summary>
        /// 増減ボタンのサイズを更新する。
        /// </summary>
        private void UpdateButtonSize()
        {
            this.decrementButton.Size = new Size(this.Height, this.Height);
            this.incrementButton.Size = new Size(this.Height, this.Height);
        }

        /// <summary>
        /// マウスのホイールが回転した場合の処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseWheel(MouseEventArgs e)
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

        protected override void OnSizeChanged(EventArgs e)
        {
            UpdateButtonSize();
            base.OnSizeChanged(e);
        }
    }
}
