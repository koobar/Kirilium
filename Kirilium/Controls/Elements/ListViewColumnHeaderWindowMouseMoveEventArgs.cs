using System;
using System.Drawing;
using System.Runtime.Versioning;

namespace Kirilium.Controls.Elements
{
    [SupportedOSPlatform("windows")]
    internal class ListViewColumnHeaderWindowMouseMoveEventArgs : EventArgs
    {
        // コンストラクタ
        public ListViewColumnHeaderWindowMouseMoveEventArgs(Point point)
        {
            this.Point = point;
        }

        /// <summary>
        /// マウス座標
        /// </summary>
        public Point Point { private set; get; }
    }
}
