using Kirilium;
using Kirilium.Controls;
using Kirilium.Themes;
using System;
using System.Windows.Forms;

namespace SampleApplication
{
    public partial class MainForm : KWindow
    {
        public MainForm()
        {
            InitializeComponent();
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            //ThemeManager.CurrentTheme.Apply(this);
            
            base.OnHandleCreated(e);
        }

        /// <summary>
        /// Create KListView.
        /// </summary>
        /// <param name="view"></param>
        /// <returns></returns>
        private KListView CreateListView(View view)
        {
            var result = new KListView();
            result.View = view;
            result.Dock = DockStyle.Fill;
            result.FullRowSelect = true;

            result.Columns.Add(new ColumnHeader() { Text = "Column 1", Width = -2 });
            result.Columns.Add(new ColumnHeader() { Text = "Column 2", Width = -2 });

            for (int i = 0; i < 10; ++i)
            {
                result.Items.Add(new ListViewItem() { Text = $"Item {i}", SubItems = { "SubItem 1", "SubItem 2" } });
            }

            return result;
        }

        protected override void OnLoad(EventArgs e)
        {
            this.kTextBox1.Text = "Hello Kirilium!";

            this.kTabControl1.TabPages.Add(new KTabPage("KListView - LargeIcon", CreateListView(View.LargeIcon)));
            this.kTabControl1.TabPages.Add(new KTabPage("KListView - SmallIcon", CreateListView(View.SmallIcon)));
            this.kTabControl1.TabPages.Add(new KTabPage("KListView - List", CreateListView(View.List)));
            this.kTabControl1.TabPages.Add(new KTabPage("KListView - Details", CreateListView(View.Details)));

            base.OnLoad(e);
        }

        private void khScrollBar1_ValueChanged(object sender, EventArgs e)
        {
            this.kProgressBar1.Value = this.khScrollBar1.Value;
        }

        private void kButton1_Click(object sender, EventArgs e)
        {
            if (ThemeManager.CurrentTheme is LightTheme)
            {
                ThemeManager.CurrentTheme = new DarkTheme();
            }
            else
            {
                ThemeManager.CurrentTheme = new LightTheme();
            }
        }
    }
}
