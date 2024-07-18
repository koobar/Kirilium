using Kirilium.Themes;
using System;
using System.ComponentModel;
using System.Runtime.Versioning;
using System.Windows.Forms;

namespace Kirilium.Controls
{
    [SupportedOSPlatform("windows")]
    public class KRadioButton : RadioButton
    {
        // コンストラクタ
        public KRadioButton()
        {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.ResizeRedraw, true);

            this.DoubleBuffered = true;

            ThemeManager.ThemeChanged += OnThemeChanged;
        }

        // デストラクタ
        ~KRadioButton()
        {
            ThemeManager.ThemeChanged -= OnThemeChanged;
        }

        private void OnThemeChanged(object sender, EventArgs e)
        {
            Invalidate();
        }

        #region プロパティ

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new Padding Padding { get; set; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new FlatStyle FlatStyle { get; set; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new Appearance Appearance { get; set; }

        #endregion

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
            var glyphBoxColor = KControl.GetBorderColor(this.Focused, this.DisplayRectangle.Contains(PointToClient(Cursor.Position)), this.Enabled);
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
                KControl.GetTextColor(this.Enabled, false),
                ThemeManager.CurrentTheme.GetColor(ColorKeys.RadioButtonBackColorNormal),
                TextFormatFlags.Left | TextFormatFlags.VerticalCenter);

            //base.OnPaint(e);
        }
    }
}
