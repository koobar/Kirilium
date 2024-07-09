using Kirilium.Themes;
using System;
using System.Windows.Forms;

namespace Kirilium.Controls
{
    public class KLabel : Control
    {
        // 非公開フィールド
        private KLabelTextLayout textLayout;

        // コンストラクタ
        public KLabel()
        {
            this.textLayout = KLabelTextLayout.LeftCenter;

            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.ResizeRedraw, true);

            ThemeManager.ThemeChanged += OnThemeChanged;
        }

        // デストラクタ
        ~KLabel()
        {
            ThemeManager.ThemeChanged -= OnThemeChanged;
        }

        #region プロパティ

        /// <summary>
        /// コントロールのテキスト
        /// </summary>
        public new string Text
        {
            set
            {
                base.Text = value;
                Invalidate();
            }
            get
            {
                return base.Text;
            }
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

        #endregion

        /// <summary>
        /// TextFormatFlagsを取得する。
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// テーマが変更された場合の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            e.Graphics.Clear(ThemeManager.CurrentTheme.GetColor(ColorKeys.LabelBackColor));

            Renderer.DrawText(
                e.Graphics,
                this.Text,
                this.Font,
                e.ClipRectangle,
                ThemeManager.CurrentTheme.GetColor(ColorKeys.ApplicationTextNormal),
                ThemeManager.CurrentTheme.GetColor(ColorKeys.LabelBackColor),
                GetTextFormatFlags());
        }
    }
}
