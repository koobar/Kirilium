namespace Kirilium.WinApi
{
    internal static class WindowMessages
    {
        // リストビュー
        public const int LVM_GETHEADER = 0x101F;
        public const int LVM_SETBKCOLOR = 0x1001;

        // WM
        public const int WM_PAINT = 0x000F;
        public const int WM_ERASEBKGND = 0x0014;
        public const int WM_PRINT = 0x0317;
        public const int WM_CONTEXTMENU = 0x7b;
        public const int WM_MOUSEMOVE = 0x0200;
        public const int WM_MOUSELEAVE = 0x02A3;
        public const int WM_MOUSEWHEEL = 0x020A;
        public const int WM_HSCROLL = 0x114;
        public const int WM_VSCROLL = 0x115;

        // WM_PRINTメッセージ
        public const int PRF_CHECKVISIBLE = 0x00000001;
        public const int PRF_NONCLIENT = 0x00000002;
        public const int PRF_CLIENT = 0x00000004;
        public const int PRF_ERASEBKGND = 0x00000008;
        public const int PRF_CHILDREN = 0x00000010;
    }
}
