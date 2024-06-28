using System;
using System.Drawing;

namespace Kirilium.Controls.Elements
{
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
