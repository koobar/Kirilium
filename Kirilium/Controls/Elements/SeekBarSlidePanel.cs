using Kirilium.Themes;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.Versioning;
using System.Windows.Forms;

namespace Kirilium.Controls.Elements
{
    [SupportedOSPlatform("windows")]
    internal class SeekBarSlidePanel : Control
    {
        // 非公開定数
        private const int THUMB_SIZE = 15;

        // イベント
        public event EventHandler ValueChanged;
        public event EventHandler SeekCompleted;

        // 非公開フィールド
        private Rectangle thumbRect;
        private int maximumValue;
        private int minimumValue;
        private int value;

        // コンストラクタ
        public SeekBarSlidePanel()
        {
            this.LineWidth = 3f;

            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.ResizeRedraw, true);

            ThemeManager.ThemeChanged += OnThemeChanged;
        }

        ~SeekBarSlidePanel()
        {
            ThemeManager.ThemeChanged -= OnThemeChanged;
        }

        private void OnThemeChanged(object sender, EventArgs e)
        {
            Invalidate();
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            // 何もしなければつまみの描画位置がずれるので、
            // Valueプロパティを同じ値で再設定することで、無理やり再描画を促す。
            // なお、なぜかInvalidateで再描画しても位置ずれが改善されない。
            this.Value = this.value;

            base.OnHandleCreated(e);
        }

        #region プロパティ

        /// <summary>
        /// ユーザーがマウス操作でシーク中であるかどうかを示す。
        /// </summary>
        public bool IsSeeking { private set; get; }

        /// <summary>
        /// 最大値
        /// </summary>
        public int MaximumValue
        {
            set
            {
                this.maximumValue = value;

                UpdateThumb();
                Invalidate();
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

                UpdateThumb();
                Invalidate();
            }
            get
            {
                return this.minimumValue;
            }
        }
        
        /// <summary>
        /// 内部値
        /// </summary>
        protected int InternalValue
        {
            set
            {
                this.value = value;

                UpdateThumb();
                Invalidate();
            }
            get
            {
                return this.value;
            }
        }

        /// <summary>
        /// 値
        /// </summary>
        public int Value
        {
            set
            {
                if (!this.IsSeeking)
                {
                    this.InternalValue = value;
                }
            }
            get
            {
                return this.InternalValue;
            }
        }

        /// <summary>
        /// 線の幅
        /// </summary>
        public float LineWidth { set; get; }

        #endregion

        /// <summary>
        /// つまみを更新する。
        /// </summary>
        private void UpdateThumb()
        {
            var posRatio = (double)this.Value / this.MaximumValue;
            var trackAreaSize = this.Width - THUMB_SIZE;
            var pos = (int)(trackAreaSize * posRatio);

            this.thumbRect = new Rectangle(this.DisplayRectangle.Left + pos, this.DisplayRectangle.Top, THUMB_SIZE, Math.Max(this.Height - 3, THUMB_SIZE));
        }

        /// <summary>
        /// X座標から値に変換する。
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private int XPosToValue(int x)
        {
            var a = ((double)x / this.Width) * this.maximumValue;

            var value = this.MinimumValue + (int)a;
            if (value < this.MinimumValue)
            {
                value = this.MinimumValue;
            }
            else if (value > this.MaximumValue)
            {
                value = this.MaximumValue;
            }

            return value;
        }

        #region マウス関連の処理のオーバーライド

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (this.thumbRect.Contains(e.Location))
            {
                if (e.Button == MouseButtons.Left)
                {
                    this.IsSeeking = true;
                }

                Invalidate();
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            if (this.IsSeeking)
            {
                this.IsSeeking = false;
                this.ValueChanged?.Invoke(this, EventArgs.Empty);
                this.SeekCompleted?.Invoke(this, EventArgs.Empty);
            }

            Invalidate();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (this.IsSeeking)
            {
                this.InternalValue = XPosToValue(e.Location.X);
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
            e.Graphics.Clear(ThemeManager.CurrentTheme.GetColor(ColorKeys.ScrollBarSlideAreaBackColor));

            // トラック線を描画する。
            using (var pen = Renderer.CreateNormalPen(ThemeManager.CurrentTheme.GetColor(ColorKeys.SeekBarLineColor)))
            {
                pen.Width = this.LineWidth;
                pen.StartCap = LineCap.Round;
                pen.EndCap = LineCap.Round;

                Renderer.DrawLine(
                    e.Graphics,
                    this.DisplayRectangle.X + 5,
                    this.DisplayRectangle.Y + (this.DisplayRectangle.Height / 2),
                    this.DisplayRectangle.Right - 5,
                    this.DisplayRectangle.Y + (this.DisplayRectangle.Height / 2),
                    pen);
            }

            // つまみを描画する。
            Renderer.FillEllipse(e.Graphics, this.thumbRect, ThemeManager.CurrentTheme.GetColor(ColorKeys.SeekbarThumbColor));

            base.OnPaint(e);
        }
    }
}
