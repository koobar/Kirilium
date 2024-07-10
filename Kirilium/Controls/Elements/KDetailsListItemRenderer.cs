using Kirilium.Controls.Collection;
using Kirilium.Themes;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Kirilium.Controls.Elements
{
    internal class KDetailsListItemRenderer : UserControl
    {
        // 非公開フィールド
        private readonly KDetailsListColumnHeaderRenderer columnHeaderRenderer;
        private readonly NotificationList<KDetailsListItem> items;
        private readonly VScrollBar verticalScrollBar;
        private int visibleFirstItemIndex;
        private int itemHeight;

        // イベント
        public event EventHandler SelectedIndexChanged;

        // コンストラクタ
        public KDetailsListItemRenderer(KDetailsListColumnHeaderRenderer columnHeaderRenderer)
        {
            this.ItemHeight = 20;
            this.ScrollSpeed = 3;
            this.AutoScroll = false;

            this.visibleFirstItemIndex = 0;

            this.columnHeaderRenderer = columnHeaderRenderer;

            this.items = new NotificationList<KDetailsListItem>();
            this.items.ValueCollectionChanged += OnItemCollectionChanged;

            this.verticalScrollBar = new VScrollBar();
            this.verticalScrollBar.Dock = DockStyle.Right;
            this.verticalScrollBar.Parent = this;
            this.verticalScrollBar.Minimum = 0;
            this.verticalScrollBar.Value = 0;

            // 描画設定
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
        }

        #region プロパティ

        /// <summary>
        /// アイテムの高さ
        /// </summary>
        public int ItemHeight
        {
            set
            {
                this.itemHeight = value;
                Invalidate();
            }
            get
            {
                return this.itemHeight;
            }
        }

        /// <summary>
        /// スクロール速度
        /// </summary>
        public int ScrollSpeed { set; get; }

        /// <summary>
        /// アイテムの一覧を示す。
        /// </summary>
        public NotificationList<KDetailsListItem> Items
        {
            get
            {
                return this.items;
            }
        }

        /// <summary>
        /// 選択されたアイテムのインデックス
        /// </summary>
        public int SelectedIndex
        {
            set
            {
                for (int i = 0; i < this.Items.Count; ++i)
                {
                    this.Items[i].IsSelected = value == i;
                }

                // 再描画
                Invalidate();

                // イベントを実行する。
                this.SelectedIndexChanged?.Invoke(this, EventArgs.Empty);
            }
            get
            {
                for (int i = 0; i < this.Items.Count; ++i)
                {
                    if (this.Items[i].IsSelected)
                    {
                        return i;
                    }
                }

                return -1;
            }
        }

        #endregion

        /// <summary>
        /// コントロールの領域内に表示可能なアイテムの個数を取得する。
        /// </summary>
        /// <returns></returns>
        protected virtual int GetNumItemsVisible()
        {
            return this.ClientRectangle.Height / this.ItemHeight;
        }

        /// <summary>
        /// コントロールの領域内に表示可能な最後のアイテムのインデックスを取得する。
        /// </summary>
        /// <returns></returns>
        protected virtual int GetVisibleLastItemIndex()
        {
            // コントロールの領域内に表示可能なアイテムの個数を求める。
            int result = this.visibleFirstItemIndex + GetNumItemsVisible();
            
            if (result >= this.Items.Count)
            {
                result = this.Items.Count - 1;
            }

            return result;
        }

        /// <summary>
        /// サブアイテムを描画する。
        /// </summary>
        /// <param name="deviceContext"></param>
        /// <param name="text"></param>
        /// <param name="bounds"></param>
        /// <param name="isSelected"></param>
        protected virtual void PaintSubItem(Graphics deviceContext, string text, Rectangle bounds, bool isSelected)
        {
            var textColor = ThemeManager.CurrentTheme.GetColor(ColorKeys.ApplicationTextNormal);
            if (isSelected)
            {
                textColor = ThemeManager.CurrentTheme.GetColor(ColorKeys.ApplicationTextHighlight);
            }

            Renderer.DrawText(deviceContext, text, this.Font, bounds, textColor, Color.Transparent, TextFormatFlags.Left | TextFormatFlags.VerticalCenter);
        }

        /// <summary>
        /// アイテムを描画する。
        /// </summary>
        /// <param name="deviceContext"></param>
        /// <param name="index"></param>
        /// <param name="bounds"></param>
        protected virtual void PaintItem(Graphics deviceContext, int index, Rectangle bounds)
        {
            var backColor = ThemeManager.CurrentTheme.GetColor(ColorKeys.KDetailsListBackColor);
            if (this.Items[index].IsSelected)
            {
                backColor = ThemeManager.CurrentTheme.GetColor(ColorKeys.KDetailsListSelectedColor);
            }

            // 背景を塗りつぶす。
            Renderer.FillRect(deviceContext, bounds, backColor);

            // すべてのサブアイテムを描画する。
            int x = 1;
            for (int columnIndex = 0; columnIndex < this.columnHeaderRenderer.ColumnHeaders.Count; ++columnIndex)
            {
                var columnHeader = this.columnHeaderRenderer.ColumnHeaders[columnIndex];
                var headerText = columnHeader.Text;
                var headerSize = Renderer.MeasureText(headerText, this.Font);
                var rect = new Rectangle(x, bounds.Y, headerSize.Width, this.ItemHeight);

                // サブアイテムを描画する。
                PaintSubItem(deviceContext, this.Items[index].SubItems[columnIndex], rect, this.Items[index].IsSelected);

                // 後始末
                x += headerSize.Width;
                x += KDetailsList.ELEMENTS_MARGIN;
            }
        }

        /// <summary>
        /// 表示可能なすべてのアイテムを描画する。
        /// </summary>
        /// <param name="deviceContext"></param>
        protected virtual void PaintItems(Graphics deviceContext)
        {
            if (this.visibleFirstItemIndex < 0)
            {
                //this.visibleFirstItemIndex = 0;
                throw new ArgumentOutOfRangeException("表示開始アイテムのインデックスは、負数でない値を指定する必要があります。");
            }

            int last = GetVisibleLastItemIndex();

            // 表示可能なアイテムを描画する。
            int y = 1;
            for (int i = this.visibleFirstItemIndex; i <= last; ++i)
            {
                var bounds = new Rectangle(0, y, this.ClientRectangle.Width, this.ItemHeight);

                // 描画処理
                PaintItem(deviceContext, i, bounds);

                // 後始末
                y += this.ItemHeight;
            }
        }

        /// <summary>
        /// 指定されたアイテムの領域を取得する。
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        protected virtual Rectangle GetItemBounds(int index)
        {
            int last = GetVisibleLastItemIndex();

            int y = 0;
            bool found = false;
            for (int i = this.visibleFirstItemIndex; i <= last; ++i)
            {
                if (i == index)
                {
                    found = true;
                    break;
                }

                // 後始末
                y += this.ItemHeight;
            }

            if (found)
            {
                return new Rectangle(0, y, this.ClientRectangle.Width, this.ItemHeight);
            }

            return Rectangle.Empty;
        }

        /// <summary>
        /// コントロールが読み込まれた場合の処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            this.verticalScrollBar.ValueChanged += OnVerticalScroll;
            base.OnLoad(e);
        }

        /// <summary>
        /// 描画処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            // 背景を塗りつぶす。
            Renderer.FillRect(e.Graphics, this.DisplayRectangle, ThemeManager.CurrentTheme.GetColor(ColorKeys.KDetailsListBackColor));

            // すべてのアイテムを描画する。
            PaintItems(e.Graphics);

            // 境界線の描画
            var borderColor = ThemeManager.CurrentTheme.GetColor(ColorKeys.ApplicationBorderNormal);
            Renderer.DrawLine(e.Graphics, 0, 0, 0, this.ClientRectangle.Bottom - 1, borderColor);
            Renderer.DrawLine(e.Graphics, 0, this.ClientRectangle.Bottom - 1, this.ClientRectangle.Right - 1, this.ClientRectangle.Bottom - 1, borderColor);
            Renderer.DrawLine(e.Graphics, this.ClientRectangle.Right - 1, 0, this.ClientRectangle.Right - 1, this.ClientRectangle.Bottom - 1, borderColor);

            base.OnPaint(e);
        }

        /// <summary>
        /// マウスのボタンが押下された場合の処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                var n = GetNumItemsVisible();
                var itemBounds = new Rectangle[n];
                for (int i = 0; i <= n; ++i)
                {
                    int index = i + this.visibleFirstItemIndex;
                    if (index >= this.Items.Count)
                    {
                        break;
                    }

                    itemBounds[i] = GetItemBounds(index);
                }

                int selected = -1;
                for (int i = 0; i < n; ++i)
                {
                    if (itemBounds[i].Contains(e.Location))
                    {
                        selected = i + this.visibleFirstItemIndex;
                        break;
                    }
                }

                this.SelectedIndex = selected;
            }

            base.OnMouseDown(e);
        }

        /// <summary>
        /// マウスホイールが回された場合の処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            int n = GetNumItemsVisible() + 1;
            int index = 0;

            if (e.Delta > 0)
            {
                index = this.visibleFirstItemIndex - this.ScrollSpeed;
            }
            else
            {
                index = this.visibleFirstItemIndex + this.ScrollSpeed;
            }

            if (index >= this.Items.Count - n)
            {
                index = this.Items.Count - n;
            }
            else if (index < 0)
            {
                index = 0;
            }

            if (index < 0)
            {
                index = 0;
            }

            // 縦スクロールバーの値を反映
            this.verticalScrollBar.Minimum = 0;         // これがないと、なぜかverticalScrollBarの最小値が負数になる。原因は不明。
            this.verticalScrollBar.Value = index;

            base.OnMouseWheel(e);
        }

        /// <summary>
        /// アイテムのコレクションが変更された場合の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnItemCollectionChanged(object sender, EventArgs e)
        {
            this.verticalScrollBar.Maximum = this.Items.Count - GetNumItemsVisible();
            this.verticalScrollBar.Visible = this.Items.Count > GetNumItemsVisible();

            Invalidate();
        }

        /// <summary>
        /// 垂直方向のスクロールが発生した場合の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnVerticalScroll(object sender, EventArgs e)
        {
            this.visibleFirstItemIndex = this.verticalScrollBar.Value;

            // 再描画
            Invalidate();
        }

        /// <summary>
        /// コマンドキーの処理
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            bool flagReturn = false;

            if (keyData == Keys.Down || keyData == Keys.Up || keyData == Keys.PageDown || keyData == Keys.PageUp)
            {
                Point current = this.AutoScrollPosition;
                Point scrolled = new Point(current.X, -current.Y + 50);
                this.AutoScrollPosition = scrolled;
                flagReturn = true;
            }

            if (keyData == Keys.Down || keyData == Keys.PageDown)
            {
                int newIndex = this.SelectedIndex + 1;
                if (newIndex >= this.Items.Count)
                {
                    newIndex = 0;
                }

                this.SelectedIndex = newIndex;
            }
            else if (keyData == Keys.Up || keyData == Keys.PageUp)
            {
                int newIndex = this.SelectedIndex - 1;
                if (newIndex < 0)
                {
                    newIndex = 0;
                }

                this.SelectedIndex = newIndex;
            }

            if (flagReturn)
            {
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
