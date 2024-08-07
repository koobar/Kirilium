﻿using Kirilium.Themes;
using System;
using System.Drawing;
using System.Runtime.Versioning;
using System.Windows.Forms;

namespace Kirilium.Controls.Elements
{
    [SupportedOSPlatform("windows")]
    internal class ScrollBarSlidePanel : KControl
    {
        // イベント
        public event EventHandler Slide;

        // 非公開フィールド
        private Rectangle thumbRect;
        private bool isScrolling;
        private int maximumValue;
        private int minimumValue;
        private int value;
        private int thumbSize;
        private bool isVertical;

        /// <summary>
        /// コントロールのハンドルが生成された場合の処理
        /// </summary>
        /// <param name="e"></param>
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
        /// 垂直方向であるかどうかを示す。
        /// </summary>
        public bool IsVertical
        {
            set
            {
                this.isVertical = value;
                UpdateThumb();
                Invalidate();
            }
            get
            {
                return this.isVertical;
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
        /// 値
        /// </summary>
        public int Value
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

        #endregion

        /// <summary>
        /// つまみを更新する。
        /// </summary>
        private void UpdateThumb()
        {
            var posRatio = (double)this.Value / this.MaximumValue;

            const int minimumThumbSize = 15;

            if (this.IsVertical)
            {
                // つまみのサイズを計算する。
                int thumbSize = Math.Max(this.Height - Math.Abs(this.MaximumValue - this.MinimumValue), minimumThumbSize);

                var trackAreaSize = this.Height - thumbSize;
                var pos = (int)(trackAreaSize * posRatio);

                this.thumbRect = new Rectangle(this.DisplayRectangle.Left, pos, Math.Max(this.ClientSize.Width, thumbSize), thumbSize);
            }
            else
            {
                // つまみのサイズを計算する。
                int thumbSize = Math.Max(this.Width - Math.Abs(this.MaximumValue - this.MinimumValue), minimumThumbSize);

                // トラックの領域のサイズと位置を計算する。
                var trackAreaSize = this.Width - thumbSize;
                var pos = (int)(trackAreaSize * posRatio);

                this.thumbRect = new Rectangle(this.DisplayRectangle.Left + pos, this.DisplayRectangle.Top, thumbSize, Math.Max(this.Height, thumbSize));
            }
        }

        /// <summary>
        /// Y座標から値に変換する。
        /// </summary>
        /// <param name="y"></param>
        /// <returns></returns>
        private int YPosToValue(int y)
        {
            var a = ((double)y / this.Height) * this.maximumValue;

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
                    this.isScrolling = true;
                }

                Invalidate();
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            this.isScrolling = false;

            Invalidate();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (this.isScrolling)
            {
                if (this.IsVertical)
                {
                    this.Value = YPosToValue(e.Location.Y);
                }
                else
                {
                    this.Value = XPosToValue(e.Location.X);
                }

                this.Slide?.Invoke(this, EventArgs.Empty);
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

            // つまみの描画
            var thumbColor = ThemeManager.CurrentTheme.GetColor(ColorKeys.ScrollBarThumbColor);
            var thumbBorderColor = ThemeManager.CurrentTheme.GetColor(ColorKeys.ApplicationBorderNormal);
            Renderer.FillRect(e.Graphics, this.thumbRect.X, this.thumbRect.Y, this.thumbRect.Width, this.thumbRect.Height, thumbColor);
            Renderer.DrawRect(e.Graphics, this.thumbRect.X, this.thumbRect.Y, this.thumbRect.Width, this.thumbRect.Height, thumbBorderColor);

            if (this.IsVertical)
            {
                // 境界線の描画
                var lineColor = ThemeManager.CurrentTheme.GetColor(ColorKeys.ApplicationBorderNormal);
                Renderer.DrawLine(e.Graphics, this.DisplayRectangle.X, this.DisplayRectangle.Y, this.DisplayRectangle.X, this.DisplayRectangle.Bottom - 1, lineColor);
                Renderer.DrawLine(e.Graphics, this.DisplayRectangle.Right - 1, this.DisplayRectangle.Y, this.DisplayRectangle.Right - 1, this.DisplayRectangle.Bottom - 1, lineColor);
            }
            else
            {
                // 境界線の描画
                var lineColor = ThemeManager.CurrentTheme.GetColor(ColorKeys.ApplicationBorderNormal);
                Renderer.DrawLine(e.Graphics, this.DisplayRectangle.X, this.DisplayRectangle.Y, this.DisplayRectangle.Right - 1, this.DisplayRectangle.Y, lineColor);
                Renderer.DrawLine(e.Graphics, this.DisplayRectangle.X, this.DisplayRectangle.Bottom - 1, this.DisplayRectangle.Right - 1, this.DisplayRectangle.Bottom - 1, lineColor);
            }

            base.OnPaint(e);
        }
    }
}
