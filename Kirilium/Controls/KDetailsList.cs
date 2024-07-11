using Kirilium.Controls.Collection;
using Kirilium.Controls.Elements;
using System;
using System.Windows.Forms;

namespace Kirilium.Controls
{
    public partial class KDetailsList : UserControl
    {
        // 非公開定数
        internal const int ELEMENTS_MARGIN = 3;

        // 非公開フィールド
        private readonly KDetailsListColumnHeaderRenderer columnHeaderRenderer;
        private readonly KDetailsListItemRenderer itemRenderer;

        // イベント
        //public new event EventHandler Click;
        //public new event EventHandler DoubleClick;
        public event EventHandler SelectedIndexChanged;

        // コンストラクタ
        public KDetailsList()
        {
            InitializeComponent();

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
        /// アイテムレンダラで選択されたアイテムのインデックスが変更された場合の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnItemRendererSelectedIndexChanged(object sender, EventArgs e)
        {
            this.SelectedIndexChanged?.Invoke(sender, e);
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
