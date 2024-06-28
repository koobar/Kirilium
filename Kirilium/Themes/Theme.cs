using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using static Kirilium.WinApi.Dwmapi;
using static Kirilium.WinApi.Uxtheme;

namespace Kirilium.Themes
{
    public class Theme
    {
        // 非公開フィールド
        protected readonly Dictionary<ColorKeys, Color> colorTable;

        // コンストラクタ
        public Theme()
        {
            this.colorTable = new Dictionary<ColorKeys, Color>();
            this.UseDarkWindowTitleBar = false;
        }

        /// <summary>
        /// ウィンドウのタイトルバーにダークテーマを適用するかどうかを示す。
        /// </summary>
        public bool UseDarkWindowTitleBar { protected set; get; }

        /// <summary>
        /// 指定されたカラーキーの色を設定する。
        /// </summary>
        /// <param name="colorKey"></param>
        /// <param name="color"></param>
        public void SetColor(ColorKeys colorKey, Color color)
        {
            if (this.colorTable.ContainsKey(colorKey))
            {
                this.colorTable[colorKey] = color;
            }
            else
            {
                this.colorTable.Add(colorKey, color);
            }
        }

        /// <summary>
        /// 指定されたカラーキーの色を取得する。
        /// </summary>
        /// <param name="colorKey"></param>
        /// <returns></returns>
        public Color GetColor(ColorKeys colorKey)
        {
            if (this.colorTable.ContainsKey(colorKey))
            {
                return this.colorTable[colorKey];
            }

            return Color.Black;
        }

        /// <summary>
        /// 指定されたフォームのタイトルバー周りの配色設定を更新する。
        /// </summary>
        /// <param name="form"></param>
        private void UpdateWindowTitleBarTheme(Form form)
        {
            if (this.UseDarkWindowTitleBar)
            {
                if (OperatingSystem.IsWindows11())
                {
                    DwmSetWindowAttribute(form.Handle, DWMWA_USE_IMMERSIVE_DARK_MODE, new[] { 1 }, 4);
                    SetPreferredAppMode(UXTHEME_APPMODE_FORCEDARK);
                }
                else
                {
                    // Windows 10 20H1以前に使用されていた属性でエラーが返された場合、20H1以降の属性で試す。
                    if (DwmSetWindowAttribute(form.Handle, DWMWA_USE_IMMERSIVE_DARK_MODE_BEFORE_20H1, new[] { 1 }, 4) != 0)
                    {
                        DwmSetWindowAttribute(form.Handle, DWMWA_USE_IMMERSIVE_DARK_MODE, new[] { 1 }, 4);
                    }

                    AllowDarkModeForApp(true);
                }
            }
            else
            {
                if (OperatingSystem.IsWindows11())
                {
                    SetPreferredAppMode(UXTHEME_APPMODE_FORCELIGHT);
                }
                else
                {
                    AllowDarkModeForApp(false);
                }
            }
        }

        /// <summary>
        /// 指定されたフォームにテーマを反映する。
        /// </summary>
        /// <param name="form"></param>
        public virtual void Apply(Form form)
        {
            UpdateWindowTitleBarTheme(form);

            form.BackColor = GetColor(ColorKeys.WindowBackColor);
            form.Font = SystemFonts.CaptionFont;
            form.AutoScaleMode = AutoScaleMode.Dpi;

            ThemeManager.ThemeChanged += delegate
            {
                form.BackColor = ThemeManager.CurrentTheme.GetColor(ColorKeys.WindowBackColor);
            };
        }
    }
}
