using Kirilium.Themes;
using System;
using System.Runtime.Versioning;
using System.Windows.Forms;

namespace Kirilium.Controls
{
    [SupportedOSPlatform("windows")]
    public class KRadioButton : KControl
    {
        // 非公開フィールド
        private bool isChecked;

        // コンストラクタ
        public KRadioButton() : base()
        {
            this.Width = 100;
            this.Height = 25;
        }

        #region イベント

        /// <summary>
        /// コントロールのチェック状態が変更された場合に発生するイベント
        /// </summary>
        public event EventHandler CheckedChanged;

        #endregion

        #region プロパティ

        /// <summary>
        /// チェックされているかどうかを示す。
        /// </summary>
        public bool Checked
        {
            set
            {
                this.isChecked = value;
                
                if (value)
                {
                    UncheckAnotherRadioButton();
                }

                // 再描画
                Invalidate();

                // イベントを発生させる。
                OnCheckedChanged();
            }
            get
            {
                return this.isChecked;
            }
        }

        #endregion

        /// <summary>
        /// 同じ親コントロールに配置された別のラジオボタンのチェックを解除する。
        /// </summary>
        private void UncheckAnotherRadioButton()
        {
            if (this.Parent != null)
            {
                foreach (var ctrl in this.Parent.Controls)
                {
                    if (ctrl == this)
                    {
                        continue;
                    }

                    if (ctrl is KRadioButton)
                    {
                        ((KRadioButton)ctrl).Checked = false;
                    }
                    else if (ctrl is RadioButton)
                    {
                        ((RadioButton)ctrl).Checked = false;
                    }
                }
            }
        }

        /// <summary>
        /// コントロールのチェック状態が変更された場合の処理
        /// </summary>
        protected virtual void OnCheckedChanged()
        {
            this.CheckedChanged?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// マウスのボタンが押下された場合の処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            this.Checked = !this.Checked;
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
            Renderer.FillRect(e.Graphics, this.DisplayRectangle, ThemeManager.CurrentTheme.GetColor(ColorKeys.RadioButtonBackColorNormal));

            // ラジオボタンのチェック図形を囲む円の描画
            var glyphBoxColor = GetBorderColor();
            var glyphBoxWidth = 12;
            var glyphBoxHeight = 12;
            var glyphBoxLeft = this.Padding.Left;
            var glyphBoxTop = (this.DisplayRectangle.Height / 2) - (glyphBoxHeight / 2);
            Renderer.DrawEllipse(e.Graphics, glyphBoxLeft, glyphBoxTop, glyphBoxWidth, glyphBoxHeight, glyphBoxColor);

            // チェック状態か？
            if (this.Checked)
            {
                Renderer.DrawImageUnscaled(
                    e.Graphics,
                    glyphBoxLeft + 1,
                    glyphBoxTop + 1,
                    glyphBoxWidth - 2,
                    glyphBoxHeight - 2,
                    IconRenderer.GetRadioButtonCheckedGlyph(glyphBoxWidth - 2, glyphBoxHeight - 2));
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
                ThemeManager.CurrentTheme.GetColor(ColorKeys.RadioButtonBackColorNormal),
                TextFormatFlags.Left | TextFormatFlags.VerticalCenter);

            //base.OnPaint(e);
        }
    }
}
