using Kirilium.Controls.Collection;
using Kirilium.Themes;
using System.Windows.Forms;
using System;

namespace Kirilium.Controls.Elements
{
    internal class KDetailsListColumnHeaderRenderer : UserControl
    {
        // 非公開フィールド
        private readonly NotificationList<KColumnHeader> columnHeaders;

        // コンストラクタ
        public KDetailsListColumnHeaderRenderer()
        {
            this.columnHeaders = new NotificationList<KColumnHeader>();
            this.columnHeaders.ValueCollectionChanged += OnColumnHeaderCollectionChanged;

            // 描画設定
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            ThemeManager.ThemeChanged += OnThemeChanged;
        }

        /// <summary>
        /// 列ヘッダの一覧を示す。
        /// </summary>
        public NotificationList<KColumnHeader> ColumnHeaders
        {
            get
            {
                return this.columnHeaders;
            }
        }

        /// <summary>
        /// テーマが変更された場合の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnThemeChanged(object sender, EventArgs e)
        {
            Refresh();
        }

        /// <summary>
        /// 列ヘッダのコレクションが変更された場合の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnColumnHeaderCollectionChanged(object sender, EventArgs e)
        {
            Invalidate();
        }

        /// <summary>
        /// 描画処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            // 背景を塗りつぶす。
            Renderer.FillRect(e.Graphics, this.DisplayRectangle, ThemeManager.CurrentTheme.GetColor(ColorKeys.KDetailsListBackColor));

            int x = 1;
            for (int i = 0; i < this.columnHeaders.Count; ++i)
            {
                var text = this.columnHeaders[i].Text;
                var size = Renderer.MeasureText(text, this.Font);

                if (i > 0)
                {
                    Renderer.DrawLine(e.Graphics, x, 0, x, this.Height, ThemeManager.CurrentTheme.GetColor(ColorKeys.ApplicationBorderNormal));
                }

                // テキストを描画
                Renderer.DrawText(
                    e.Graphics,
                    text,
                    this.Font,
                    x,
                    0,
                    size.Width,
                    this.Height,
                    ThemeManager.CurrentTheme.GetColor(ColorKeys.ApplicationTextNormal),
                    ThemeManager.CurrentTheme.GetColor(ColorKeys.KDetailsListBackColor),
                    TextFormatFlags.Left | TextFormatFlags.VerticalCenter);

                // 後始末
                this.columnHeaders[i].ActualLeft = x;
                this.columnHeaders[i].ActualTop = 0;
                this.columnHeaders[i].ActualWidth = size.Width;
                this.columnHeaders[i].ActualHeight = this.Height;
                x += size.Width;
                x += KDetailsList.ELEMENTS_MARGIN;
            }

            // 境界線の描画
            Renderer.DrawRect(
                e.Graphics,
                this.DisplayRectangle.X,
                this.DisplayRectangle.Y,
                this.DisplayRectangle.Width - 1,
                this.DisplayRectangle.Height - 1, 
                ThemeManager.CurrentTheme.GetColor(ColorKeys.ApplicationBorderNormal));

            base.OnPaint(e);
        }
    }
}
