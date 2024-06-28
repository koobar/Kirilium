using System;
using System.Windows.Forms;

namespace Kirilium.Controls
{
    public class KWindow : Form
    {
        protected override void OnHandleCreated(EventArgs e)
        {
            ThemeManager.CurrentTheme.Apply(this);
            base.OnHandleCreated(e);
        }
    }
}
