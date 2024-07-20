using Kirilium.Themes;
using System;
using System.Runtime.Versioning;
using System.Windows.Forms;

namespace Kirilium.Controls
{
    [SupportedOSPlatform("windows")]
    public class KCheckBox : KControl
    {
        // 非公開フィールド
        private CheckState checkState;

        #region イベント

        /// <summary>
        /// コントロールのチェック状態が変更された場合に発生するイベント
        /// </summary>
        public event EventHandler CheckedChanged;

        #endregion

        #region プロパティ

        /// <summary>
        /// チェック状態
        /// </summary>
        public CheckState CheckState
        {
            set
            {
                this.checkState = value;
                Invalidate();

                OnCheckedChanged();
            }
            get
            {
                return this.checkState;
            }
        }

        /// <summary>
        /// チェックされているかどうかを示す。
        /// </summary>
        public bool Checked
        {
            set
            {
                if (value)
                {
                    this.CheckState = CheckState.Checked;
                }
                else
                {
                    this.CheckState = CheckState.Unchecked;
                }
            }
            get
            {
                if (this.CheckState != CheckState.Unchecked)
                {
                    return true;
                }

                return false;
            }
        }

        /// <summary>
        /// 三状態チェックボックスとして振舞うかどうか
        /// </summary>
        public bool ThreeState { set; get; }

        #endregion

        /// <summary>
        /// コントロールのチェック状態が変更された場合の処理
        /// </summary>
        protected virtual void OnCheckedChanged()
        {
            this.CheckedChanged?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// マウスが押下された場合の処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (this.ThreeState)
            {
                switch (this.CheckState)
                {
                    case CheckState.Unchecked:
                        this.CheckState = CheckState.Checked;
                        break;
                    case CheckState.Checked:
                        this.CheckState = CheckState.Indeterminate;
                        break;
                    case CheckState.Indeterminate:
                        this.CheckState = CheckState.Unchecked;
                        break;
                }
            }
            else
            {
                if (this.CheckState == CheckState.Unchecked)
                {
                    this.CheckState = CheckState.Checked;
                }
                else
                {
                    this.CheckState = CheckState.Unchecked;
                }
            }

            base.OnMouseDown(e);
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
            Renderer.FillRect(e.Graphics, this.DisplayRectangle, ThemeManager.CurrentTheme.GetColor(ColorKeys.CheckBoxBackColorNormal));

            // 矩形の描画
            var glyphBoxColor = GetBorderColor();
            var glyphBoxWidth = 12;
            var glyphBoxHeight = 12;
            var glyphBoxLeft = this.Padding.Left;
            var glyphBoxTop = (this.DisplayRectangle.Height / 2) - (glyphBoxHeight / 2);
            Renderer.DrawRect(e.Graphics, glyphBoxLeft, glyphBoxTop, glyphBoxWidth, glyphBoxHeight, glyphBoxColor);

            if (this.CheckState == CheckState.Checked)
            {
                Renderer.DrawImageUnscaled(
                    e.Graphics,
                    glyphBoxLeft,
                    glyphBoxTop,
                    glyphBoxWidth,
                    glyphBoxHeight,
                    IconRenderer.GetCheckBoxCheckedGlyph(glyphBoxWidth, glyphBoxHeight));
            }
            else if (this.CheckState == CheckState.Indeterminate)
            {
                Renderer.DrawImageUnscaled(
                    e.Graphics,
                    glyphBoxLeft,
                    glyphBoxTop,
                    glyphBoxWidth,
                    glyphBoxHeight,
                    IconRenderer.GetCheckBoxIndeterminateGlyph(glyphBoxWidth, glyphBoxHeight));
            }

            // テキストの描画
            Renderer.DrawText(
                e.Graphics,
                this.Text,
                this.Font,
                glyphBoxLeft + glyphBoxWidth + 3,
                0,
                this.DisplayRectangle.Width - (glyphBoxLeft + glyphBoxWidth + 6),
                this.DisplayRectangle.Height,
                GetTextColor(),
                ThemeManager.CurrentTheme.GetColor(ColorKeys.CheckBoxBackColorNormal),
                TextFormatFlags.Left | TextFormatFlags.VerticalCenter);
        }
    }
}
