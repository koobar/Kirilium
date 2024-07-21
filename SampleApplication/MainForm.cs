using Kirilium;
using Kirilium.Controls;
using Kirilium.Themes;
using System;
using System.Runtime.Versioning;
using System.Windows.Forms;

namespace SampleApplication
{
    [SupportedOSPlatform("windows")]
    public partial class MainForm : KWindow
    {
        public MainForm()
        {
            InitializeComponent();
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
            result.HeaderStyle = ColumnHeaderStyle.Nonclickable;
            result.MaximumColumnHeaderWidth = -1;
            result.FillLastColumnHeader = true;

            var contextMenu = new KContextMenuStrip();
            contextMenu.Items.Add(new KToolStripMenuItem() { Text = "Undo(&U)", ShortcutKeys = Keys.Control | Keys.Z });

            result.ContextMenuStrip = contextMenu;

            result.Columns.Add(new ColumnHeader() { Text = "Column 1", Width = -1 });
            result.Columns.Add(new ColumnHeader() { Text = "Column 2", Width = -2 });

            for (int i = 0; i < 10; ++i)
            {
                result.Items.Add(new ListViewItem() { Text = $"Item {i}", SubItems = { "SubItem 1", "SubItem 2" } });
            }

            if (view == View.Details)
            {
                result.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            }

            return result;
        }

        /// <summary>
        /// Create KDetailsList.
        /// </summary>
        /// <returns></returns>
        private KDetailsList CreateKDetailsList()
        {
            var result = new KDetailsList();
            result.Dock = DockStyle.Fill;
            result.Columns.Add(new KColumnHeader(result));
            result.Columns.Add(new KColumnHeader(result));
            result.Columns[0].Text = "Column 1";
            result.Columns[1].Text = "Column 2";

            var contextMenu = new KContextMenuStrip();
            contextMenu.Items.Add(new KToolStripMenuItem() { Text = "Undo(&U)", ShortcutKeys = Keys.Control | Keys.Z });
            result.ContextMenuStrip = contextMenu;

            for (int i = 0; i <= 100; ++i)
            {
                var item = new KDetailsListItem();
                item.SubItems.Add($"Item {i}");
                item.SubItems.Add("SubItem");

                result.Items.Add(item);
            }

            result.AutoResizeColumns(KColumnHeaderAutoResizeStyle.ColumnContent);

            return result;
        }

        protected override void OnLoad(EventArgs e)
        {
            this.kTextBox1.Text = "Hello Kirilium!";

            this.kTabControl1.IsClosable = true;
            this.kTabControl1.TabPages.Add(new KTabPage("KListView - LargeIcon", CreateListView(View.LargeIcon)));
            this.kTabControl1.TabPages.Add(new KTabPage("KListView - SmallIcon", CreateListView(View.SmallIcon)));
            this.kTabControl1.TabPages.Add(new KTabPage("KListView - List", CreateListView(View.List)));
            this.kTabControl1.TabPages.Add(new KTabPage("KDetailsList", CreateKDetailsList()));

            
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
