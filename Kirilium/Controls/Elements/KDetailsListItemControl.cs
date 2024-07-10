using Kirilium.Themes;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Kirilium.Controls.Elements
{
    internal class KDetailsListItemControl : UserControl
    {
        // 非公開フィールド
        private readonly KDetailsListItem item;
        private readonly KDetailsListColumnHeaderRenderer columnHeaderRenderer;

        // イベント
        public event EventHandler Selected;
        public new event PreviewKeyDownEventHandler PreviewKeyDown;
        public new event KeyEventHandler KeyDown;

        // コンストラクタ
        public KDetailsListItemControl(KDetailsListItem item, KDetailsListColumnHeaderRenderer columnHeaderRenderer)
        {
            this.item = item;
            this.item.SubItems.ValueCollectionChanged += OnSubItemsChanged;
            this.columnHeaderRenderer = columnHeaderRenderer;

            // 描画設定
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            this.Height = 15;
        }

        /// <summary>
        /// 表示しているアイテムの実体
        /// </summary>
        public KDetailsListItem Item
        {
            get
            {
                return this.item;
            }
        }

        /// <summary>
        /// サブアイテムが変更された場合の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnSubItemsChanged(object sender, EventArgs e)
        {
            Invalidate();
        }

        /// <summary>
        /// マウスのボタンが押下された場合の処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.item.IsSelected = true;
                this.Selected?.Invoke(this, e);
            }

            base.OnMouseDown(e);
        }

        protected override void OnPreviewKeyDown(PreviewKeyDownEventArgs e)
        {
            this.PreviewKeyDown?.Invoke(this, e);
            base.OnPreviewKeyDown(e);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            this.KeyDown?.Invoke(this, e);
            base.OnKeyDown(e);
        }

        /// <summary>
        /// 描画処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            var backColor = ThemeManager.CurrentTheme.GetColor(ColorKeys.KDetailsListBackColor);
            var textColor = ThemeManager.CurrentTheme.GetColor(ColorKeys.ApplicationTextNormal);
            if (this.item.IsSelected)
            {
                backColor = ThemeManager.CurrentTheme.GetColor(ColorKeys.KDetailsListSelectedColor);
                textColor = ThemeManager.CurrentTheme.GetColor(ColorKeys.ApplicationTextHighlight);
            }

            // 背景を塗りつぶす。
            Renderer.FillRect(e.Graphics, this.DisplayRectangle, backColor);

            // すべての列を描画する。
            int max = Math.Min(this.item.SubItems.Count, this.columnHeaderRenderer.ColumnHeaders.Count);
            int x = 1;
            for (int columnIndex = 0; columnIndex < max; ++columnIndex)
            {
                var text = this.item.SubItems[columnIndex];
                var size = Renderer.MeasureText(text, this.Font);

                // テキストを描画する。
                Renderer.DrawText(
                    e.Graphics,
                    text,
                    this.Font,
                    new Rectangle(x, 0, size.Width, this.Height),
                    textColor,
                    backColor,
                    TextFormatFlags.Left | TextFormatFlags.VerticalCenter);

                // 後始末
                x += Math.Max(size.Width, this.columnHeaderRenderer.ColumnHeaders[columnIndex].ActualWidth);
                x += KDetailsList.ELEMENTS_MARGIN;
            }

            // 左右の境界線の描画
            Renderer.DrawLine(e.Graphics, 0, 0, 0, this.Height, ThemeManager.CurrentTheme.GetColor(ColorKeys.ApplicationBorderNormal));
            Renderer.DrawLine(e.Graphics, this.Right - 1, 0, this.Right - 1, this.Height, ThemeManager.CurrentTheme.GetColor(ColorKeys.ApplicationBorderNormal));
        }
    }
}
