using System;
using System.Runtime.Versioning;
using System.Windows.Forms;

namespace Kirilium.Controls
{
    [SupportedOSPlatform("windows")]
    public class KWindow : Form
    {
        protected override void OnHandleCreated(EventArgs e)
        {
            ThemeManager.CurrentTheme.Apply(this);
            base.OnHandleCreated(e);
        }
    }
}
