using Kirilium.Controls.Elements;

namespace Kirilium.Controls
{
    public class KColumnHeader
    {
        // 非公開フィールド
        private readonly KDetailsListColumnHeaderRenderer parent;
        private string text;

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
                OnTextChanged();
            }
            get
            {
                return this.text;
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
        /// テキストが変更された場合の処理
        /// </summary>
        protected virtual void OnTextChanged()
        {
            this.parent.Refresh();
        }
    }
}
