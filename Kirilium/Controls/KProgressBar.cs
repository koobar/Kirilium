using Kirilium.Themes;
using System;
using System.Drawing;
using System.Runtime.Versioning;
using System.Windows.Forms;

namespace Kirilium.Controls
{
    [SupportedOSPlatform("windows")]
    public class KProgressBar : KControl
    {
        // 非公開フィールド
        private bool showPercentAsText;
        protected int maximumValue;
        protected int minimumValue;
        protected int value;

        // イベント
        public event EventHandler MaximumValueChanged;
        public event EventHandler MinimumValueChanged;
        public event EventHandler ValueChanged;

        // コンストラクタ
        public KProgressBar()
        {
            this.Width = 100;
            this.Height = 25;

            this.maximumValue = 100;
            this.minimumValue = 0;
            this.value = 0;
            this.showPercentAsText = true;
        }

        #region プロパティ

        /// <summary>
        /// パーセント
        /// </summary>
        public int Percent
        {
            get
            {
                var p = ((double)this.value - this.minimumValue) / ((double)this.maximumValue - this.minimumValue) * 100;

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
                this.maximumValue = value;

                Invalidate();
                OnMaximumValueChanged();
            }
            get
            {
                return this.maximumValue;
            }
        }

        /// <summary>
        /// 最小値
        /// </summary>
        public int MinimumValue
        {
            set
            {
                this.minimumValue = value;

                Invalidate();
                OnMinimumValueChanged();
            }
            get
            {
                return this.minimumValue;
            }
        }

        /// <summary>
        /// 値
        /// </summary>
        public int Value
        {
            set
            {
                if (value != this.value)
                {
                    this.value = value;

                    Invalidate();
                    OnValueChanged();
                }
            }
            get
            {
                return this.value;
            }
        }

        #endregion

        /// <summary>
        /// 進捗のパーセントをテキストとして描画するかどうか
        /// </summary>
        public bool ShowPercentAsText
        {
            set
            {
                this.showPercentAsText = value;
                Invalidate();
            }
            get
            {
                return this.showPercentAsText;
            }
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

        /// <summary>
        /// 描画処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            // コントロールを初期化
            e.Graphics.Clear(ThemeManager.CurrentTheme.GetColor(ColorKeys.ApplicationClearColor));

            // 背景を塗りつぶす。
            Renderer.FillRect(e.Graphics,
                this.DisplayRectangle, 
                ThemeManager.CurrentTheme.GetColor(ColorKeys.ProgressBarBackColorNormal));

            // 前景を塗りつぶす。
            int width = (int)Math.Round(this.ClientSize.Width * this.Percent * 0.01);
            Renderer.FillRect(
                e.Graphics, 
                this.DisplayRectangle.X + 1,
                this.DisplayRectangle.Y + 1,
                width, 
                this.DisplayRectangle.Height - 2,
                ThemeManager.CurrentTheme.GetColor(ColorKeys.ProgressBarForeColorNormal));

            // パーセントのテキスト表示が有効か？
            if (this.showPercentAsText)
            {
                Renderer.DrawText(
                       e.Graphics,
                       $"{this.Percent}%",
                       this.Font,
                       this.DisplayRectangle,
                       GetTextColor(),
                       Color.Transparent,
                       TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
            }

            // 境界線を描画する。
            Renderer.DrawRect(
                e.Graphics,
                this.DisplayRectangle.X,
                this.DisplayRectangle.Y,
                this.DisplayRectangle.Right - 1,
                this.DisplayRectangle.Bottom - 1,
                ThemeManager.CurrentTheme.GetColor(ColorKeys.ApplicationBorderNormal));

            base.OnPaint(e);
        }
    }
}
