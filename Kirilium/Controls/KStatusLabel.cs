using Kirilium.Themes;
using System;
using System.Runtime.Versioning;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Kirilium.Controls
{
    [SupportedOSPlatform("windows")]
    [ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.StatusStrip)]
    public class KStatusLabel : ToolStripStatusLabel
    {
        // 非公開フィールド
        private KLabelTextLayout textLayout;

        // コンストラクタ
        public KStatusLabel()
        {
            this.textLayout = KLabelTextLayout.LeftCenter;

            ThemeManager.ThemeChanged += OnThemeChanged;
        }

        // デストラクタ
        ~KStatusLabel()
        {
            ThemeManager.ThemeChanged -= OnThemeChanged;
        }

        /// <summary>
        /// テキストの位置
        /// </summary>
        public KLabelTextLayout TextLayout
        {
            set
            {
                this.textLayout = value;
                Invalidate();
            }
            get
            {
                return this.textLayout;
            }
        }

        private TextFormatFlags GetTextFormatFlags()
        {
            switch (this.textLayout)
            {
                case KLabelTextLayout.LeftTop:
                    return TextFormatFlags.Left | TextFormatFlags.Top;
                case KLabelTextLayout.CenterTop:
                    return TextFormatFlags.HorizontalCenter | TextFormatFlags.Top;
                case KLabelTextLayout.RightTop:
                    return TextFormatFlags.Right | TextFormatFlags.Top;
                case KLabelTextLayout.LeftCenter:
                    return TextFormatFlags.Left | TextFormatFlags.VerticalCenter;
                case KLabelTextLayout.CenterCenter:
                    return TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter;
                case KLabelTextLayout.RightCenter:
                    return TextFormatFlags.Right | TextFormatFlags.VerticalCenter;
                case KLabelTextLayout.LeftBottom:
                    return TextFormatFlags.Left | TextFormatFlags.Bottom;
                case KLabelTextLayout.CenterBottom:
                    return TextFormatFlags.HorizontalCenter | TextFormatFlags.Bottom;
                case KLabelTextLayout.RightBottom:
                    return TextFormatFlags.Right | TextFormatFlags.Bottom;
                default:
                    return TextFormatFlags.Default;
            }
        }

        private void OnThemeChanged(object sender, EventArgs e)
        {
            Invalidate();
        }

        /// <summary>
        /// 描画処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.Clear(ThemeManager.CurrentTheme.GetColor(ColorKeys.StatusStripBackColor));

            Renderer.DrawText(
                e.Graphics,
                this.Text,
                this.Font,
                e.ClipRectangle,
                ThemeManager.CurrentTheme.GetColor(ColorKeys.ApplicationTextNormal),
                ThemeManager.CurrentTheme.GetColor(ColorKeys.StatusStripBackColor),
                GetTextFormatFlags());
        }
    }
}
