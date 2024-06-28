using System;
using System.Runtime.InteropServices;

namespace Kirilium.WinApi
{
    internal static class Dwmapi
    {
        public const uint DWMWA_WINDOW_CORNER_PREFERENCE = 33;
        public const uint DWMWA_DEFAULT = 0;
        public const uint DWMWCP_DONOTROUND = 1;
        public const uint DWMWCP_ROUND = 2;
        public const uint DWMWCP_ROUNDSMALL = 3;

        [DllImport("dwmapi.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern long DwmSetWindowAttribute(IntPtr hwnd, uint attribute, ref uint pvAttribute, uint cbAttribute);
    }
}
