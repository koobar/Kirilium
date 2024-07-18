using System;
using System.Runtime.Versioning;

namespace Kirilium.Controls.Elements
{
    [SupportedOSPlatform("windows")]
    internal class TabCloseEventArgs : EventArgs
    {
        // コンストラクタ
        public TabCloseEventArgs(int requestedTabIndex)
        {
            this.CloseRequestedTabIndex = requestedTabIndex;
        }

        /// <summary>
        /// 閉じる要求がされたタブのインデックス
        /// </summary>
        public int CloseRequestedTabIndex { private set; get; }
    }
}
