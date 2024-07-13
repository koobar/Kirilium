using Kirilium.Controls.Elements;

namespace Kirilium.Controls
{
    public class KColumnHeader
    {
        // 非公開フィールド
        private readonly KDetailsListColumnHeaderRenderer parent;
        private string text;
        private int width;
        private int maxWidth;
        private int minWidth;

        // コンストラクタ
        public KColumnHeader(KDetailsList parent)
        {
            if (parent != null)
            {
                this.parent = parent.ColumnHeaderRenderer;
            }
        }

        public KColumnHeader() : this(null) { }

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
