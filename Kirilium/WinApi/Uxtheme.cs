using System.Runtime.InteropServices;

namespace Kirilium.WinApi
{
    internal static class Uxtheme
    {
        public const uint UXTHEME_APPMODE_DEFAULT = 0;
        public const uint UXTHEME_APPMODE_ALLOWDARK = 1;
        public const uint UXTHEME_APPMODE_FORCEDARK = 2;
        public const uint UXTHEME_APPMODE_FORCELIGHT = 3;
        public const uint UXTHEME_APPMODE_MAX = 4;

        [DllImport("uxtheme.dll", EntryPoint = "#135")]
        public static extern bool AllowDarkModeForApp(bool bAllowDarkMode);

        [DllImport("uxtheme.dll", EntryPoint = "#104")]
        public static extern void RefreshImmersiveColorPolicyState();

        [DllImport("uxtheme.dll", EntryPoint = "#135")]
        public static extern uint SetPreferredAppMode(uint appMode);
    }
}
