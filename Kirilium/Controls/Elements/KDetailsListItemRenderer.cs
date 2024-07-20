using Kirilium.Collection;
using Kirilium.Themes;
using System;
using System.Drawing;
using System.Runtime.Versioning;
using System.Windows.Forms;

namespace Kirilium.Controls.Elements
{
    [SupportedOSPlatform("windows")]
    internal class KDetailsListItemRenderer : KControl
    {
        // 非公開フィールド
        private readonly KDetailsListColumnHeaderRenderer columnHeaderRenderer;
        private readonly NotificationList<KDetailsListItem> items;
        private readonly VScrollBar verticalScrollBar;
        private int visibleFirstItemIndex;
        private int itemHeight;
        private bool drawVerticalGridLines;
        private bool drawHorizontalGridLines;
        private int scrollSpeed;

        // イベント
        public event EventHandler SelectedIndexChanged;

        // コンストラクタ
        public KDetailsListItemRenderer(KDetailsListColumnHeaderRenderer columnHeaderRenderer)
        {
            this.visibleFirstItemIndex = 0;
            this.columnHeaderRenderer = columnHeaderRenderer;
            this.items = new NotificationList<KDetailsListItem>();
            this.items.ValueCollectionChanged += OnItemCollectionChanged;

            // 縦スクロールバー
            this.verticalScrollBar = new VScrollBar();
            this.verticalScrollBar.Dock = DockStyle.Right;
            this.verticalScrollBar.Parent = this;
            this.verticalScrollBar.Minimum = 0;
            this.verticalScrollBar.Value = 0;

            // 初期設定
            this.ItemHeight = 15;
            this.ScrollSpeed = 3;
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

                UpdateVerticalScrollBarVisible();
                Invalidate();
            }
            get
            {
                return this.itemHeight;
            }
        }

        /// <summary>
        /// 垂直方向のグリッド線を描画するかどうか
        /// </summary>
        public bool DrawVerticalGridLines
        {
            set
            {
                this.drawVerticalGridLines = value;

                Invalidate();
            }
            get
            {
                return this.drawVerticalGridLines;
            }
        }

        /// <summary>
        /// 水平方向のグリッド線を描画するかどうか
        /// </summary>
        public bool DrawHorizontalGridLines
        {
            set
            {
                this.drawHorizontalGridLines = value;

                Invalidate();
            }
            get
            {
                return this.drawHorizontalGridLines;
            }
        }

        /// <summary>
        /// スクロール速度
        /// </summary>
        public int ScrollSpeed
        {
            set
            {
                this.scrollSpeed = value;
                this.verticalScrollBar.SmallChange = value;
                this.verticalScrollBar.LargeChange = value;
            }
            get
            {
                return this.scrollSpeed;
            }
        }

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
                OnSelectedIndexChanged();
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
        /// 縦スクロールバーの表示・非表示を更新する。
        /// </summary>
        private void UpdateVerticalScrollBarVisible()
        {
            int n = GetNumItemsVisible() + 1;
            int max = this.Items.Count - n;
            if (max < 0)
            {
                max = 0;
            }

            this.verticalScrollBar.Maximum = max + this.ScrollSpeed;
            this.verticalScrollBar.Visible = this.Items.Count >= n;
        }

        /// <summary>
        /// コントロールの領域内に表示可能なアイテムの個数を取得する。
        /// </summary>
        /// <returns></returns>
        protected virtual int GetNumItemsVisible()
        {
            return (int)Math.Round(this.DisplayRectangle.Height / (double)this.ItemHeight, MidpointRounding.AwayFromZero);
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
        protected virtual void PaintSubItem(Graphics deviceContext, string text, Rectangle bounds, bool isSelected, int columnIndex)
        {
            var textColor = ThemeManager.CurrentTheme.GetColor(ColorKeys.ApplicationTextNormal);
            if (isSelected)
            {
                textColor = ThemeManager.CurrentTheme.GetColor(ColorKeys.ApplicationTextHighlight);
            }

            // テキストを描画する。
            Renderer.DrawText(
                deviceContext, 
                text, 
                this.Font, 
                bounds, 
                textColor, 
                Color.Transparent, 
                this.columnHeaderRenderer.ColumnHeaders[columnIndex].ContentTextFormatFlags);

            // 垂直方向のグリッド線の描画が有効なら描画する。
            if (this.drawVerticalGridLines && columnIndex > 0)
            {
                Renderer.DrawLine(deviceContext, bounds.X, bounds.Y, bounds.X, bounds.Bottom, ThemeManager.CurrentTheme.GetColor(ColorKeys.ApplicationBorderNormal));
            }
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
                var rect = new Rectangle(x, bounds.Y, columnHeader.ActualWidth, this.ItemHeight);

                // サブアイテムを描画する。
                PaintSubItem(deviceContext, this.Items[index].SubItems[columnIndex], rect, this.Items[index].IsSelected, columnIndex);

                // 後始末
                x += columnHeader.ActualWidth;
                x += KDetailsList.ELEMENTS_MARGIN;
            }

            // 水平方向のグリッド線の描画が有効なら描画する。
            if (this.drawHorizontalGridLines)
            {
                Renderer.DrawLine(deviceContext, bounds.X, bounds.Bottom - 1, bounds.Right - 1, bounds.Bottom - 1, ThemeManager.CurrentTheme.GetColor(ColorKeys.ApplicationBorderNormal));
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
            if (e.Button == MouseButtons.Left || e.Button == MouseButtons.Right)
            {
                // 表示領域分の各アイテムの領域を取得する。
                var n = GetNumItemsVisible();
                var itemBounds = new Rectangle[n + 1];
                for (int i = 0; i <= n; ++i)
                {
                    int index = i + this.visibleFirstItemIndex;
                    if (index >= this.Items.Count)
                    {
                        break;
                    }

                    itemBounds[i] = GetItemBounds(index);
                }

                // 各アイテムの領域と、マウスの座標が接触しているかを判定し、
                // 接触していたアイテムのインデックスを、選択されたアイテムのインデックスとする。
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
            if (!this.verticalScrollBar.Visible)
            {
                return;
            }

            int n = GetNumItemsVisible();
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

            if (index > this.verticalScrollBar.Maximum)
            {
                index = this.verticalScrollBar.Maximum;
            }

            // 縦スクロールバーの値を反映
            this.verticalScrollBar.Minimum = 0;         // これがないと、なぜかverticalScrollBarの最小値が負数になる。原因は不明。
            this.verticalScrollBar.Value = index;

            base.OnMouseWheel(e);
        }

        /// <summary>
        /// 選択されたアイテムのインデックスが変更された場合の処理
        /// </summary>
        protected virtual void OnSelectedIndexChanged()
        {
            this.SelectedIndexChanged?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// クライアント領域のサイズが変更された場合の処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClientSizeChanged(EventArgs e)
        {
            UpdateVerticalScrollBarVisible();

            base.OnClientSizeChanged(e);
        }

        /// <summary>
        /// サイズが変更された場合の処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnSizeChanged(EventArgs e)
        {
            UpdateVerticalScrollBarVisible();

            base.OnSizeChanged(e);
        }

        /// <summary>
        /// アイテムのコレクションが変更された場合の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnItemCollectionChanged(object sender, EventArgs e)
        {
            UpdateVerticalScrollBarVisible();
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
                    newIndex = this.Items.Count - 1;
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
