using Kirilium.Controls.Collection;
using System.Runtime.Versioning;

namespace Kirilium.Controls
{
    [SupportedOSPlatform("windows")]
    public class KDetailsListItem
    {
        // 非公開フィールド
        private readonly NotificationList<string> subItems;
        private bool isSelected;

        // コンストラクタ
        public KDetailsListItem()
        {
            this.subItems = new NotificationList<string>();
        }

        /// <summary>
        /// サブアイテム一覧を示す。
        /// </summary>
        public NotificationList<string> SubItems
        {
            get
            {
                return this.subItems;
            }
        }

        /// <summary>
        /// 選択されたアイテムであるかどうかを示す。
        /// </summary>
        public bool IsSelected
        {
            set
            {
                this.isSelected = value;
            }
            get
            {
                return this.isSelected;
            }
        }

        /// <summary>
        /// タグ
        /// </summary>
        public object Tag { set; get; }
    }
}
