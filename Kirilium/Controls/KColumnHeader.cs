using Kirilium.Controls.Elements;
using System.Windows.Forms;

namespace Kirilium.Controls
{
    public class KColumnHeader
    {
        // 非公開フィールド
        private readonly KDetailsListColumnHeaderRenderer parent;
        private string text;
        private TextFormatFlags headerTextFormatFlags;
        private TextFormatFlags contentTextFormatFlags;
        private int width;
        private int maxWidth;
        private int minWidth;

        #region コンストラクタ

        public KColumnHeader(KDetailsList parent)
        {
            if (parent != null)
            {
                this.parent = parent.ColumnHeaderRenderer;
            }

            this.headerTextFormatFlags = TextFormatFlags.Left | TextFormatFlags.VerticalCenter;
            this.contentTextFormatFlags = TextFormatFlags.Default | TextFormatFlags.VerticalCenter;
        }

        public KColumnHeader() : this(null) { }

        #endregion

        #region プロパティ

        /// <summary>
        /// テキスト
        /// </summary>
        public string Text
        {
            set
            {
                this.text = value;
                Invalidate();
            }
            get
            {
                return this.text;
            }
        }

        /// <summary>
        /// 列ヘッダのテキストの表示情報およびレイアウト情報を示す。
        /// </summary>
        public TextFormatFlags HeaderTextFormatFlags
        {
            set
            {
                bool flag = this.headerTextFormatFlags != value;
                this.headerTextFormatFlags = value;

                if (flag)
                {
                    Invalidate();
                }
            }
            get
            {
                return this.headerTextFormatFlags;
            }
        }

        /// <summary>
        /// 列のコンテンツのテキストの表示情報およびレイアウト情報を示す。
        /// </summary>
        public TextFormatFlags ContentTextFormatFlags
        {
            set
            {
                bool flag = this.contentTextFormatFlags != value;
                this.contentTextFormatFlags = value;

                if (flag)
                {
                    Invalidate();
                }
            }
            get
            {
                return this.contentTextFormatFlags;
            }
        }

        /// <summary>
        /// 幅
        /// </summary>
        public int Width
        {
            set
            {
                bool flag = this.width != value;
                this.width = value;

                if (flag)
                {
                    Invalidate();
                }
            }
            get
            {
                return this.width;
            }
        }

        /// <summary>
        /// 幅の最大値
        /// </summary>
        public int MaxWidth
        {
            set
            {
                bool flag = this.maxWidth != value;
                this.maxWidth = value;

                if (flag)
                {
                    Invalidate();
                }
            }
            get
            {
                return this.maxWidth;
            }
        }

        /// <summary>
        /// 幅の最小値
        /// </summary>
        public int MinWidth
        {
            set
            {
                bool flag = this.minWidth != value;
                this.minWidth = value;

                if (flag)
                {
                    Invalidate();
                }
            }
            get
            {
                return this.minWidth;
            }
        }

        /// <summary>
        /// 実際に描画されているX座標
        /// </summary>
        public int ActualLeft { internal set; get; }

        /// <summary>
        /// 実際に描画されているY座標
        /// </summary>
        public int ActualTop { internal set; get; }

        /// <summary>
        /// 実際に描画されている幅
        /// </summary>
        public int ActualWidth { internal set; get; }

        /// <summary>
        /// 実際に描画されている高さ
        /// </summary>
        public int ActualHeight { internal set; get; }

        #endregion

        /// <summary>
        /// 再描画
        /// </summary>
        protected virtual void Invalidate()
        {
            if (this.parent == null)
            {
                return;
            }

            this.parent.Invalidate();
        }
    }
}
