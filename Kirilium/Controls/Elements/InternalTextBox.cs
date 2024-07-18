using Kirilium.Themes;
using System;
using System.Runtime.Versioning;
using System.Windows.Forms;

namespace Kirilium.Controls.Elements
{
    [SupportedOSPlatform("windows")]
    internal class InternalTextBox : TextBox
    {
        // コンストラクタ
        public InternalTextBox()
        {
            base.BorderStyle = BorderStyle.None;
            base.BackColor = ThemeManager.CurrentTheme.GetColor(ColorKeys.TextBoxBackColorNormal);
            base.ForeColor = ThemeManager.CurrentTheme.GetColor(ColorKeys.ApplicationTextNormal);
        }

        protected override void OnGotFocus(EventArgs e)
        {
            if (this.Parent == null)
            {
                return;
            }

            this.Parent.Invalidate();
            base.OnGotFocus(e);
        }

        protected override void OnLostFocus(EventArgs e)
        {
            if (this.Parent == null)
            {
                return;
            }

            this.Parent.Invalidate();
            base.OnLostFocus(e);
        }

        protected override void OnReadOnlyChanged(EventArgs e)
        {
            if (base.ReadOnly)
            {
                base.BackColor = ThemeManager.CurrentTheme.GetColor(ColorKeys.TextBoxBackColorReadOnly);
                base.ForeColor = ThemeManager.CurrentTheme.GetColor(ColorKeys.TextBoxForeColorReadOnly);
            }
            else
            {
                base.BackColor = ThemeManager.CurrentTheme.GetColor(ColorKeys.TextBoxBackColorNormal);
                base.ForeColor = ThemeManager.CurrentTheme.GetColor(ColorKeys.ApplicationTextNormal);
            }

            base.OnReadOnlyChanged(e);
        }
    }
}
