using Kirilium.Themes;
using System.Collections.Generic;
using System.Runtime.Versioning;
using System.Windows.Forms;

namespace Kirilium.Controls.Elements
{
    [SupportedOSPlatform("windows")]
    internal class TabControlContentRenderer : KControl
    {
        // 非公開フィールド
        private readonly List<KTabPage> tabPages;
        private int selectedIndex;

        // コンストラクタ
        public TabControlContentRenderer()
        {
            this.tabPages = new List<KTabPage>();
        }

        #region プロパティ

        /// <summary>
        /// タブページ一覧
        /// </summary>
        public List<KTabPage> TabPages
        {
            get
            {
                return this.tabPages;
            }
        }

        /// <summary>
        /// 選択されたページのインデックス
        /// </summary>
        public int SelectedIndex
        {
            set
            {
                this.selectedIndex = value;
                SwitchPage(value);
            }
            get
            {
                return this.selectedIndex;
            }
        }

        #endregion

        /// <summary>
        /// 指定されたページに切り替える。
        /// </summary>
        /// <param name="page"></param>
        private void SwitchPage(int page)
        {
            if (page > -1 && page < this.TabPages.Count)
            {
                for (int i = 0; i < this.tabPages.Count; ++i)
                {
                    if (this.tabPages[i].Content != null)
                    {
                        this.tabPages[i].Content.Parent = null;
                        this.tabPages[i].Content.Visible = false;
                    }
                }

                if (this.tabPages[page].Content != null)
                {
                    this.tabPages[page].Content.Parent = this;
                    this.tabPages[page].Content.Dock = DockStyle.Fill;
                    this.tabPages[page].Content.Visible = true;
                }
            }
            else
            {
                this.Controls.Clear();
            }
        }

        /// <summary>
        /// 描画処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            // コントロールの初期化
            e.Graphics.Clear(ThemeManager.CurrentTheme.GetColor(ColorKeys.TabControlContentAreaBackColor));

            // 境界線の描画
            Renderer.DrawRect(
                e.Graphics, 
                this.DisplayRectangle.X, 
                this.DisplayRectangle.Y,
                this.DisplayRectangle.Width - 1, 
                this.DisplayRectangle.Height - 1,
                ThemeManager.CurrentTheme.GetColor(ColorKeys.ApplicationBorderNormal));
        }
    }
}
