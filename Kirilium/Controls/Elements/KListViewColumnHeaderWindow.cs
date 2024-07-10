using System;
using System.Drawing;
using System.Windows.Forms;
using static Kirilium.WinApi.WindowMessages;

namespace Kirilium.Controls.Elements
{
    internal class KListViewColumnHeaderWindow : NativeWindow
    {
        // イベント
        public event EventHandler<ListViewColumnHeaderWindowMouseMoveEventArgs> ColumnHeaderMouseMove;
        public event EventHandler ColumnHeaderMouseLeave;

        // コンストラクタ
        public KListViewColumnHeaderWindow(IntPtr hHeader, InternalListView parent)
        {
            this.Parent = parent;
            AssignHandle(hHeader);
        }

        #region プロパティ

        /// <summary>
        /// 親コントロール
        /// </summary>
        public InternalListView Parent { private set; get; }

        #endregion

        /// <summary>
        /// ウィンドウプロシージャ
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_MOUSELEAVE:
                    OnColumnHeaderMouseLeave();
                    break;
                case WM_MOUSEMOVE:
                    OnColumnHeaderMouseMove(new Point(m.LParam.ToInt32()));
                    break;
            }

            base.WndProc(ref m);
        }

        protected virtual void OnColumnHeaderMouseLeave()
        {
            this.ColumnHeaderMouseLeave?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnColumnHeaderMouseMove(Point point)
        {
            this.ColumnHeaderMouseMove?.Invoke(this, new ListViewColumnHeaderWindowMouseMoveEventArgs(point));
        }
    }
}
