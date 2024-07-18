using Kirilium.Themes;
using System;
using System.Runtime.Versioning;
using System.Windows.Forms;

namespace Kirilium.Controls.Elements
{
    [SupportedOSPlatform("windows")]
    internal class InternalListBox : ListBox
    {
        // コンストラクタ
        public InternalListBox()
        {
            this.BackColor = ThemeManager.CurrentTheme.GetColor(ColorKeys.ListBoxBackColorNormal);
            this.ForeColor = ThemeManager.CurrentTheme.GetColor(ColorKeys.ApplicationTextNormal);
            this.BorderStyle = BorderStyle.None;

            ThemeManager.ThemeChanged += OnThemeChanged;
        }

        // デストラクタ
        ~InternalListBox()
        {
            ThemeManager.ThemeChanged -= OnThemeChanged;
        }

        private void OnThemeChanged(object sender, EventArgs e)
        {
            this.BackColor = ThemeManager.CurrentTheme.GetColor(ColorKeys.ListBoxBackColorNormal);
            this.ForeColor = ThemeManager.CurrentTheme.GetColor(ColorKeys.ApplicationTextNormal);
            Invalidate();
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            if (!this.Enabled)
            {
                this.BackColor = ThemeManager.CurrentTheme.GetColor(ColorKeys.ListBoxBackColorDisabled);
            }
            else
            {
                this.BackColor = ThemeManager.CurrentTheme.GetColor(ColorKeys.ListBoxBackColorNormal);
            }

            base.OnEnabledChanged(e);
        }
    }
}
