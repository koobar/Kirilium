using System.Drawing;
using System.Windows.Forms;

namespace Kirilium.Controls
{
    public class KTabPage
    {
        // コンストラクタ
        public KTabPage(string title, Control control)
        {
            this.Text = title;
            this.Icon = null;
            this.Content = control;
        }

        // コンストラクタ
        public KTabPage(string title, Image icon, Control control)
        {
            this.Text = title;
            this.Icon = icon;
            this.Content = control;
        }

        /// <summary>
        /// ページのタイトル
        /// </summary>
        public string Text { private set; get; }

        /// <summary>
        /// アイコン
        /// </summary>
        public Image Icon { private set; get; }

        /// <summary>
        /// ページのコンテンツ
        /// </summary>
        public Control Content { private set; get; }
    }
}
