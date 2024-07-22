using Kirilium.Collection;
using Kirilium.Controls.Elements;
using System;
using System.Runtime.Versioning;
using System.Windows.Forms;

namespace Kirilium.Controls
{
    [SupportedOSPlatform("windows")]
    public partial class KDetailsList : KControl
    {
        // 非公開定数
        internal const int ELEMENTS_MARGIN = 3;

        // 非公開フィールド
        private readonly KDetailsListColumnHeaderRenderer columnHeaderRenderer;
        private readonly KDetailsListItemRenderer itemRenderer;

        // イベント
        public event EventHandler SelectedIndexChanged;

        // コンストラクタ
        public KDetailsList()
        {
            this.Padding = new Padding(0);
            this.columnHeaderRenderer = new KDetailsListColumnHeaderRenderer();
            this.columnHeaderRenderer.Dock = DockStyle.Top;
            this.columnHeaderRenderer.Height = 25;
            this.columnHeaderRenderer.Paint += OnColumnHeaderPaint;

            this.itemRenderer = new KDetailsListItemRenderer(this.columnHeaderRenderer);
            this.itemRenderer.Dock = DockStyle.Fill;
            this.itemRenderer.SelectedIndexChanged += OnItemRendererSelectedIndexChanged;
            this.itemRenderer.Click += OnClick;
            this.itemRenderer.DoubleClick += OnDoubleClick;

            this.Controls.Add(this.itemRenderer);
            this.Controls.Add(this.columnHeaderRenderer);
        }

        #region プロパティ

        /// <summary>
        /// 列ヘッダの高さ
        /// </summary>
        public int ColumnHeaderHeight
        {
            set
            {
                this.columnHeaderRenderer.Height = value;
            }
            get
            {
                return this.columnHeaderRenderer.Height;
            }
        }

        /// <summary>
        /// アイテムの高さ
        /// </summary>
        public int ItemHeight
        {
            set
            {
                this.itemRenderer.ItemHeight = value;
            }
            get
            {
                return this.itemRenderer.Height;
            }
        }

        /// <summary>
        /// 水平方向のグリッド線を描画するかどうかを示す。
        /// </summary>
        public bool DrawHorizontalGridLines
        {
            set
            {
                this.itemRenderer.DrawHorizontalGridLines = value;
            }
            get
            {
                return this.itemRenderer.DrawHorizontalGridLines;
            }
        }

        /// <summary>
        /// 垂直方向のグリッド線を描画するかどうかを示す。
        /// </summary>
        public bool DrawVerticalGridLines
        {
            set
            {
                this.itemRenderer.DrawVerticalGridLines = value;
            }
            get
            {
                return this.itemRenderer.DrawVerticalGridLines;
            }
        }

        /// <summary>
        /// 選択されたアイテムのインデックスを示す。
        /// </summary>
        public int SelectedIndex
        {
            set
            {
                this.itemRenderer.SelectedIndex = value;
            }
            get
            {
                return this.itemRenderer.SelectedIndex;
            }
        }

        /// <summary>
        /// 列ヘッダの一覧を示す。
        /// </summary>
        public NotificationList<KColumnHeader> Columns
        {
            get
            {
                return this.columnHeaderRenderer.ColumnHeaders;
            }
        }

        /// <summary>
        /// アイテム一覧を示す。
        /// </summary>
        public NotificationList<KDetailsListItem> Items
        {
            get
            {
                return this.itemRenderer.Items;
            }
        }

        /// <summary>
        /// コンテキストメニュー
        /// </summary>
        public new ContextMenuStrip ContextMenuStrip
        {
            set
            {
                this.itemRenderer.ContextMenuStrip = value;
            }
            get
            {
                return this.itemRenderer.ContextMenuStrip;
            }
        }

        /// <summary>
        /// 列ヘッダのレンダラ
        /// </summary>
        internal KDetailsListColumnHeaderRenderer ColumnHeaderRenderer
        {
            get
            {
                return this.columnHeaderRenderer;
            }
        }

        #endregion

        /// <summary>
        /// 列の幅をコンテンツに合わせてリサイズする。
        /// </summary>
        private void AutoResizeColumnsByColumnContent()
        {
            for (int columnIndex = 0; columnIndex < this.Columns.Count; ++columnIndex)
            {
                int maxWidth = Renderer.MeasureText(this.Columns[columnIndex].Text, this.Font).Width;

                for (int i = 0; i < this.Items.Count; ++i)
                {
                    var item = this.Items[i];
                    var caption = item.SubItems[columnIndex];
                    var size = Renderer.MeasureText(caption, this.Font);
                    int width = size.Width + ELEMENTS_MARGIN;

                    if (maxWidth < width)
                    {
                        maxWidth = width;
                    }
                }

                this.Columns[columnIndex].Width = maxWidth;
            }
        }

        /// <summary>
        /// 列の幅を列ヘッダのコンテンツに合わせてリサイズする。
        /// </summary>
        private void AutoResizeColumnsByHeaderSize()
        {
            for (int i = 0; i < this.Columns.Count; ++i)
            {
                var column = this.Columns[i];
                var size = Renderer.MeasureText(column.Text, this.Font);
                int width = size.Width + ELEMENTS_MARGIN;

                if (width > column.MaxWidth)
                {
                    width = column.MaxWidth;
                }
                if (width < column.MinWidth)
                {
                    width = column.MinWidth;
                }

                this.Columns[i].Width = width;
            }
        }

        /// <summary>
        /// 指定されたサイズ変更のスタイルで、列の幅を自動リサイズする。
        /// </summary>
        /// <param name="autoResizeStyle"></param>
        public void AutoResizeColumns(KColumnHeaderAutoResizeStyle autoResizeStyle)
        {
            if (autoResizeStyle == KColumnHeaderAutoResizeStyle.ColumnContent)
            {
                AutoResizeColumnsByColumnContent();
            }
            else if (autoResizeStyle == KColumnHeaderAutoResizeStyle.HeaderContent)
            {
                AutoResizeColumnsByHeaderSize();
            }
        }

        /// <summary>
        /// クリックされた場合の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnClick(object sender, EventArgs e)
        {
            base.OnClick(e);
        }

        /// <summary>
        /// ダブルクリックされた場合の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnDoubleClick(object sender, EventArgs e)
        {
            base.OnDoubleClick(e);
        }

        /// <summary>
        /// 選択されたインデックスが変更された場合の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void OnSelectedIndexChanged(object sender, EventArgs e)
        {
            this.SelectedIndexChanged?.Invoke(sender, e);
        }

        /// <summary>
        /// アイテムレンダラで選択されたアイテムのインデックスが変更された場合の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnItemRendererSelectedIndexChanged(object sender, EventArgs e)
        {
            OnSelectedIndexChanged(sender, e);
        }

        /// <summary>
        /// 列ヘッダ部分の再描画が発生した場合の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnColumnHeaderPaint(object sender, PaintEventArgs e)
        {
            this.itemRenderer.Refresh();
        }
    }
}
