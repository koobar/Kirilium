using Kirilium.Themes;
using System.Drawing;
using System.Runtime.Versioning;

namespace Kirilium.Controls
{
    [SupportedOSPlatform("windows")]
    internal static class KControl
    {
        /// <summary>
        /// 指定された状態に最適な文字色を取得する。
        /// </summary>
        /// <param name="enabled"></param>
        /// <param name="highlight"></param>
        /// <returns></returns>
        public static Color GetTextColor(bool enabled, bool highlight)
        {
            if (enabled)
            {
                if (highlight)
                {
                    return ThemeManager.CurrentTheme.GetColor(ColorKeys.ApplicationTextHighlight);
                }

                return ThemeManager.CurrentTheme.GetColor(ColorKeys.ApplicationTextNormal);
            }   
            else
            {
                return ThemeManager.CurrentTheme.GetColor(ColorKeys.ApplicationTextDisabled);
            }
        }

        /// <summary>
        /// 指定された状態に最適な境界線の色を取得する。
        /// </summary>
        /// <param name="focused"></param>
        /// <param name="isMouseOver"></param>
        /// <param name="isEnabled"></param>
        /// <returns></returns>
        public static Color GetBorderColor(bool focused, bool isMouseOver, bool isEnabled)
        {
            if (focused || isMouseOver)
            {
                return ThemeManager.CurrentTheme.GetColor(ColorKeys.ApplicationBorderHighlight);
            }

            if (!isEnabled)
            {
                return ThemeManager.CurrentTheme.GetColor(ColorKeys.ApplicationBorderDisabled);
            }

            return ThemeManager.CurrentTheme.GetColor(ColorKeys.ApplicationBorderNormal);
        }
    }
}
