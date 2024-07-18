using System.Drawing;
using System.Runtime.Versioning;

namespace Kirilium.Controls.Elements
{
    [SupportedOSPlatform("windows")]
    internal class TabHeaderRenderingElement
    {
        /// <summary>
        /// テキスト
        /// </summary>
        public string Text { set; get; }

        /// <summary>
        /// アイコン
        /// </summary>
        public Image Icon { set; get; }

        /// <summary>
        /// タブヘッダが選択されているかどうか
        /// </summary>
        public bool IsSelected { set; get; }

        /// <summary>
        /// 閉じるボタンを描画するかどうか
        /// </summary>
        public bool DrawCloseButton { set; get; }

        /// <summary>
        /// 閉じるボタンにマウスカーソルが重なっているかどうか
        /// </summary>
        public bool IsCloseButtonHot { set; get; }

        /// <summary>
        /// タブヘッダにマウスカーソルが重なっているかどうか
        /// </summary>
        public bool IsHot { set; get; }
    }
}
