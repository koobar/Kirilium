using Kirilium.Themes;
using System.Drawing;
using System.Windows.Forms;

namespace Kirilium.Controls.Elements
{
    internal class KColorTable : ProfessionalColorTable
    {
        #region MenuStrip

        public override Color MenuBorder
        {
            get
            {
                return ThemeManager.CurrentTheme.GetColor(ColorKeys.ApplicationBorderNormal);
            }
        }

        public override Color ToolStripDropDownBackground
        {
            get
            {
                return ThemeManager.CurrentTheme.GetColor(ColorKeys.ToolStripMenuItemNormalBackColor);
            }
        }

        public override Color ImageMarginGradientBegin
        {
            get
            {
                return ThemeManager.CurrentTheme.GetColor(ColorKeys.ToolStripMenuItemNormalBackColor);
            }
        }

        public override Color ImageMarginGradientMiddle
        {
            get
            {
                return ThemeManager.CurrentTheme.GetColor(ColorKeys.ToolStripMenuItemNormalBackColor);
            }
        }

        public override Color ImageMarginGradientEnd
        {
            get
            {
                return ThemeManager.CurrentTheme.GetColor(ColorKeys.ToolStripMenuItemNormalBackColor);
            }
        }

        #endregion

        #region ToolStrip

        public override Color ToolStripGradientBegin
        {
            get
            {
                return ThemeManager.CurrentTheme.GetColor(ColorKeys.ToolStripBackColor);
            }
        }

        public override Color ToolStripGradientMiddle
        {
            get
            {
                return ThemeManager.CurrentTheme.GetColor(ColorKeys.ToolStripBackColor);
            }
        }

        public override Color ToolStripGradientEnd
        {
            get
            {
                return ThemeManager.CurrentTheme.GetColor(ColorKeys.ToolStripBackColor);
            }
        }

        public override Color ToolStripContentPanelGradientBegin
        {
            get
            {
                return ThemeManager.CurrentTheme.GetColor(ColorKeys.ToolStripBackColor);
            }
        }

        public override Color ToolStripContentPanelGradientEnd
        {
            get
            {
                return ThemeManager.CurrentTheme.GetColor(ColorKeys.ToolStripBackColor);
            }
        }

        public override Color ToolStripBorder
        {
            get
            {
                return ThemeManager.CurrentTheme.GetColor(ColorKeys.ToolStripBackColor);
            }
        }

        public override Color SeparatorDark
        {
            get
            {
                return ThemeManager.CurrentTheme.GetColor(ColorKeys.ApplicationBorderNormal);
            }
        }

        public override Color SeparatorLight
        {
            get
            {
                return ThemeManager.CurrentTheme.GetColor(ColorKeys.ApplicationBorderNormal);
            }
        }

        #endregion

        #region StatusStrip

        public override Color StatusStripGradientBegin
        {
            get
            {
                return ThemeManager.CurrentTheme.GetColor(ColorKeys.StatusStripBackColor);
            }
        }

        public override Color StatusStripGradientEnd
        {
            get
            {
                return ThemeManager.CurrentTheme.GetColor(ColorKeys.StatusStripBackColor);
            }
        }

        public override Color GripDark
        {
            get
            {
                return ThemeManager.CurrentTheme.GetColor(ColorKeys.ApplicationBorderNormal);
            }
        }

        public override Color GripLight
        {
            get
            {
                return ThemeManager.CurrentTheme.GetColor(ColorKeys.ApplicationBorderNormal);
            }
        }

        #endregion
    }
}
