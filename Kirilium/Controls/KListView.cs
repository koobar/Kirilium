using Kirilium.Controls.Elements;
using Kirilium.Themes;
using System.Drawing;
using System.Windows.Forms;

namespace Kirilium.Controls
{
    public partial class KListView : UserControl
    {
        // 非公開フィールド
        private readonly InternalListView internalListView;

        // コンストラクタ
        public KListView()
        {
            InitializeComponent();

            base.BorderStyle = BorderStyle.None;
            base.Padding = new Padding(1);
            base.BackColor = ThemeManager.CurrentTheme.GetColor(ColorKeys.ListViewBackColor);

            this.internalListView = new InternalListView();
            this.internalListView.Parent = this;
            this.internalListView.Dock = DockStyle.Fill;

            // 描画設定
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
        }

        #region プロパティ

        /// <summary>
        /// 列ヘッダ
        /// </summary>
        public ListView.ColumnHeaderCollection Columns
        {
            get
            {
                return this.internalListView.Columns;
            }
        }

        /// <summary>
        /// 項目の表示スタイル
        /// </summary>
        public View View
        {
            set
            {
                this.internalListView.View = value;
            }
            get
            {
                return this.internalListView.View;
            }
        }

        /// <summary>
        /// アイテムが選択されたとき、選択されたアイテムに属するすべてのサブアイテムを選択状態にするかどうか
        /// </summary>
        public bool FullRowSelect
        {
            set
            {
                this.internalListView.FullRowSelect = value;
            }
            get
            {
                return this.internalListView.FullRowSelect;
            }
        }

        /// <summary>
        /// 列ヘッダのスタイル
        /// </summary>
        public ColumnHeaderStyle HeaderStyle
        {
            set
            {
                this.internalListView.HeaderStyle = value;
            }
            get
            {
                return this.internalListView.HeaderStyle;
            }
        }

        /// <summary>
        /// アイテム
        /// </summary>
        public ListView.ListViewItemCollection Items
        {
            get
            {
                return this.internalListView.Items;
            }
        }

        /// <summary>
        /// 列ヘッダのリサイズを無効化するかどうかを示す。
        /// </summary>
        public bool DisableColumnHeaderResize
        {
            set
            {
                this.internalListView.DisableColumnHeaderResize = value;
            }
            get
            {
                return this.internalListView.DisableColumnHeaderResize;
            }
        }

        /// <summary>
        /// 列ヘッダの幅の最大値
        /// </summary>
        public int MaximumColumnHeaderWidth
        {
            set
            {
                this.internalListView.MaximumColumnHeaderWidth = value;
            }
            get
            {
                return this.internalListView.MaximumColumnHeaderWidth;
            }
        }

        /// <summary>
        /// 一番右の列ヘッダの幅を、コントロールのクライアント領域の幅に合わせて自動的にリサイズするかどうか
        /// </summary>
        public bool FillLastColumnHeader
        {
            set
            {
                this.internalListView.FillLastColumnHeader = value;
            }
            get
            {
                return this.internalListView.FillLastColumnHeader;
            }
        }

        #endregion

        public void AutoResizeColumns(ColumnHeaderAutoResizeStyle autoResizeStyle)
        {
            this.internalListView.AutoResizeColumns(autoResizeStyle);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Renderer.DrawRect(
                e.Graphics,
                this.DisplayRectangle.X - 1,
                this.DisplayRectangle.Y - 1,
                this.DisplayRectangle.Right,
                this.DisplayRectangle.Bottom,
                ThemeManager.CurrentTheme.GetColor(ColorKeys.ApplicationBorderNormal));
        }
    }
}
