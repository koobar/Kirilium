using System.Runtime.InteropServices;

namespace Kirilium
{
    public static class OperatingSystem
    {
        [DllImport("winbrand.dll", CharSet = CharSet.Unicode)]
        private static extern string BrandingFormatString(string format);

        /// <summary>
        /// 実行中のOSがWindows 11であるかどうかを判定する。
        /// </summary>
        /// <returns></returns>
        public static bool IsWindows11()
        {
            const string osversion = @"%WINDOWS_LONG%";
            string osName = BrandingFormatString(osversion);
            osName = osName.ToLower();

            if (osName.StartsWith("windows 11"))
            {
                return true;
            }

            return false;

        }
    }
}
