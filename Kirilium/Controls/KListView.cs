using Kirilium.Controls.Elements;
using Kirilium.Themes;
using System;
using System.Drawing;
using System.Runtime.Versioning;
using System.Windows.Forms;

namespace Kirilium.Controls
{
    [SupportedOSPlatform("windows")]
    public partial class KListView : KControl
    {
        // 非公開フィールド
        private readonly InternalListView internalListView;

        // イベント
        public new event EventHandler Click;
        public new event EventHandler DoubleClick;
        public event ColumnClickEventHandler ColumnClick;
        public event EventHandler SelectedIndexChanged;

        // コンストラクタ
        public KListView()
        {
            InitializeComponent();

            this.internalListView = new InternalListView();
            this.internalListView.Parent = this;
            this.internalListView.Dock = DockStyle.Fill;
            this.internalListView.SelectedIndexChanged += OnSelectedIndexChanged;
            this.internalListView.Click += OnClick;
            this.internalListView.ColumnClick += OnColumnClick;
            this.internalListView.DoubleClick += OnDoubleClick;
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
        /// ユーザーが列ヘッダをドラッグして、並び替えることができるかどうかを示す。
        /// </summary>
        public bool AllowColumnReorder
        {
            set
            {
                this.internalListView.AllowColumnReorder = value;
            }
            get
            {
                return this.internalListView.AllowColumnReorder;
            }
        }

        /// <summary>
        /// 複数のアイテムが選択できるかどうかを示す。
        /// </summary>
        public bool MultiSelect
        {
            set
            {
                this.internalListView.MultiSelect = value;
            }
            get
            {
                return this.internalListView.MultiSelect;
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
        /// コントロール内の選択された項目のインデックスを示す。
        /// </summary>
        public ListView.SelectedIndexCollection SelectedIndices
        {
            get
            {
                return this.internalListView.SelectedIndices;
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

        /// <summary>
        /// コントロールの表面全体を無効化して、コントロールを再描画する。
        /// </summary>
        public new void Invalidate()
        {
            this.internalListView.Invalidate();
            base.Invalidate();
        }

        /// <summary>
        /// コントロールの表面の指定された領域を無効化して、コントロールを再描画する。
        /// </summary>
        /// <param name="rectangle"></param>
        public new void Invalidate(Rectangle rectangle)
        {
            this.internalListView.Invalidate(rectangle);
            base.Invalidate(rectangle);
        }

        /// <summary>
        /// 列の幅を、指定されたサイズ変更スタイルで示されたスタイルで変更する。
        /// </summary>
        /// <param name="autoResizeStyle"></param>
        public void AutoResizeColumns(ColumnHeaderAutoResizeStyle autoResizeStyle)
        {
            this.internalListView.AutoResizeColumns(autoResizeStyle);
        }

        /// <summary>
        /// クリックされた場合の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnClick(object sender, EventArgs e)
        {
            this.Click?.Invoke(sender, e);
        }

        /// <summary>
        /// ダブルクリックされた場合の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnDoubleClick(object sender, EventArgs e)
        {
            this.DoubleClick?.Invoke(sender, e);
        }

        /// <summary>
        /// 列ヘッダがクリックされた場合の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnColumnClick(object sender, ColumnClickEventArgs e)
        {
            this.ColumnClick?.Invoke(sender, e);
        }

        /// <summary>
        /// 選択インデックスが変更された場合の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnSelectedIndexChanged(object sender, EventArgs e)
        {
            this.SelectedIndexChanged?.Invoke(sender, e);
        }

        /// <summary>
        /// 描画処理
        /// </summary>
        /// <param name="e"></param>
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
