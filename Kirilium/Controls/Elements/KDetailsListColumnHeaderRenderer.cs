using Kirilium.Controls.Collection;
using Kirilium.Themes;
using System.Windows.Forms;
using System;
using System.Drawing;

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

        // デストラクタ
        ~KDetailsListColumnHeaderRenderer()
        {
            ThemeManager.ThemeChanged -= OnThemeChanged;
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
        /// 指定されたインデックスの列ヘッダの領域を取得する。
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        protected virtual Rectangle GetColumnBounds(int index)
        {
            int x = 1;
            for (int i = 0; i < this.columnHeaders.Count; ++i)
            {
                // 列ヘッダの幅を取得
                var width = this.columnHeaders[i].Width;
                if (width <= 0)
                {
                    width = Renderer.MeasureText(this.columnHeaders[i].Text, this.Font).Width;
                }
                if (this.columnHeaders[i].MaxWidth > 0 && width > this.columnHeaders[i].MaxWidth)
                {
                    width = this.columnHeaders[i].MaxWidth;
                }
                if (this.columnHeaders[i].MinWidth > 0 && width < this.columnHeaders[i].MinWidth)
                {
                    width = this.columnHeaders[i].MinWidth;
                }
                var size = new Size(width, this.Height);   
                var bounds = new Rectangle(x, 0, size.Width, this.Height);

                if (i == index)
                {
                    return bounds;
                }

                // 後始末
                x += bounds.Width;
                x += KDetailsList.ELEMENTS_MARGIN;
            }

            return Rectangle.Empty;
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
            foreach (var columnHeader in this.columnHeaders)
            {
                if (columnHeader.Parent == null)
                {
                    columnHeader.Parent = this;
                }
            }

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

            for (int i = 0; i < this.columnHeaders.Count; ++i)
            {
                var text = this.columnHeaders[i].Text;
                var bounds = GetColumnBounds(i);

                if (i > 0)
                {
                    Renderer.DrawLine(e.Graphics, bounds.X, bounds.Y, bounds.X, bounds.Height, ThemeManager.CurrentTheme.GetColor(ColorKeys.ApplicationBorderNormal));
                }

                // テキストを描画
                Renderer.DrawText(
                    e.Graphics,
                    text,
                    this.Font,
                    bounds,
                    ThemeManager.CurrentTheme.GetColor(ColorKeys.ApplicationTextNormal),
                    ThemeManager.CurrentTheme.GetColor(ColorKeys.KDetailsListBackColor),
                    this.columnHeaders[i].HeaderTextFormatFlags);

                // 後始末
                this.columnHeaders[i].ActualLeft = bounds.X;
                this.columnHeaders[i].ActualTop = bounds.Y;
                this.columnHeaders[i].ActualWidth = bounds.Width;
                this.columnHeaders[i].ActualHeight = bounds.Height;
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
