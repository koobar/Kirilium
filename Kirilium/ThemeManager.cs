using Kirilium.Themes;
using Microsoft.Win32;
using System;
using System.Runtime.Versioning;

namespace Kirilium
{
    [SupportedOSPlatform("windows")]
    public static class ThemeManager
    {
        // 非公開定数
        private const string REGISTRY_KEY_PATH = @"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize";
        private const string REGISTRY_VALUE_NAME = "AppsUseLightTheme";

        // 非公開フィールド
        private static Theme currentTheme;

        // イベント
        internal static event EventHandler ThemeChanged;

        /// <summary>
        /// 現在のテーマ
        /// </summary>
        public static Theme CurrentTheme
        {
            set
            {
                if (currentTheme != value)
                {
                    currentTheme = value;
                    ThemeChanged?.Invoke(null, EventArgs.Empty);
                }
            }
            get
            {
                if (currentTheme == null)
                {
                    Init();
                }

                return currentTheme;
            }
        }

        /// <summary>
        /// Windowsのアプリのテーマがダークモードに設定されているか否かを設定する。
        /// </summary>
        /// <returns></returns>
        private static bool IsWindowsDarkModeEnabled()
        {
            using (var key = Registry.CurrentUser.OpenSubKey(REGISTRY_KEY_PATH))
            {
                var valObj = key?.GetValue(REGISTRY_VALUE_NAME);

                if (valObj == null)
                {
                    return false;
                }

                if ((int)valObj > 0)
                {
                    return false;
                }

                return true;
            }
        }

        /// <summary>
        /// 初期化
        /// </summary>
        /// <param name="theme"></param>
        public static void Init(Theme theme = null)
        {
            if (theme == null)
            {
                if (IsWindowsDarkModeEnabled())
                {
                    theme = new DarkTheme();
                }
                else
                {
                    theme = new LightTheme();
                }
            }

            CurrentTheme = theme;
        }
    }
}
