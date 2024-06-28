using Kirilium.Controls.Collection;
using Kirilium.Controls.Elements;
using System;
using System.Windows.Forms;

namespace Kirilium.Controls
{
    public class KTabControl : Control
    {
        // 非公開フィールド
        private readonly TabControlHeaderRenderer headerRenderer;
        private readonly TabControlContentRenderer contentRenderer;
        private readonly NotificationList<KTabPage> tabPageCollection;
        private int selectedIndex;

        // コンストラクタ
        public KTabControl()
        {
            this.tabPageCollection = new NotificationList<KTabPage>();
            this.tabPageCollection.ValueCollectionChanged += OnTabPageCollectionChanged;

            this.headerRenderer = new TabControlHeaderRenderer();
            this.headerRenderer.Dock = DockStyle.Top;
            this.headerRenderer.Height = 20;
            this.headerRenderer.SelectedIndexChanged += OnHeaderSelectedIndexChanged;
            this.headerRenderer.TabCloseButtonPressed += OnTabCloseButtonPressed;

            this.contentRenderer = new TabControlContentRenderer();
            this.contentRenderer.Dock = DockStyle.Fill;

            this.Controls.Add(this.contentRenderer);
            this.Controls.Add(this.headerRenderer);

            ThemeManager.ThemeChanged += OnThemeChanged;
        }

        // デストラクタ
        ~KTabControl()
        {
            ThemeManager.ThemeChanged -= OnThemeChanged;
        }

        #region プロパティ

        /// <summary>
        /// タブヘッダの高さ
        /// </summary>
        public int TabHeaderHeight
        {
            set
            {
                this.headerRenderer.Height = value;
            }
            get
            {
                return this.headerRenderer.Height;
            }
        }

        /// <summary>
        /// [BETA]タブページを閉じることができるかどうか
        /// </summary>
        public bool IsClosable { set; get; }

        /// <summary>
        /// 選択されたページのインデックス
        /// </summary>
        public int SelectedIndex
        {
            set
            {
                this.selectedIndex = value;
                this.headerRenderer.SelectedIndex = value;
            }
            get
            {
                return this.selectedIndex;
            }
        }

        /// <summary>
        /// タブページコレクション
        /// </summary>
        public NotificationList<KTabPage> TabPages
        {
            get
            {
                return this.tabPageCollection;
            }
        }

        #endregion

        /// <summary>
        /// 指定されたインデックスのタブを閉じる。
        /// </summary>
        /// <param name="index"></param>
        public void CloseTabAt(int index)
        {
            if (!this.IsClosable)
            {
                return;
            }

            this.SelectedIndex = index - 1;

            if (index > -1 && index < this.tabPageCollection.Count)
            {
                this.TabPages.RemoveAt(index);
            }
        }

        /// <summary>
        /// 指定されたタブページを追加する。
        /// </summary>
        /// <param name="tabPage"></param>
        private void AddTabPage(KTabPage tabPage)
        {
            var element = new TabHeaderRenderingElement();
            element.Text = tabPage.Text;
            element.Icon = tabPage.Icon;
            element.DrawCloseButton = this.IsClosable;

            this.headerRenderer.TabHeaders.Add(element);
            this.contentRenderer.TabPages.Add(tabPage);
        }

        /// <summary>
        /// タブコレクションが変更された場合の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnTabPageCollectionChanged(object sender, EventArgs e)
        {
            this.headerRenderer.TabHeaders.Clear();
            this.contentRenderer.TabPages.Clear();

            foreach (var page in this.tabPageCollection)
            {
                AddTabPage(page);
            }

            if (this.TabPages.Count > 0)
            {
                this.SelectedIndex = 0;
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
        /// 選択されているタブが変更された場合の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnHeaderSelectedIndexChanged(object sender, EventArgs e)
        {
            this.contentRenderer.SelectedIndex = this.headerRenderer.SelectedIndex;
        }

        /// <summary>
        /// 閉じるボタンがクリックされた場合の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnTabCloseButtonPressed(object sender, TabCloseEventArgs e)
        {
            CloseTabAt(e.CloseRequestedTabIndex);
        }
    }
}
