
namespace SampleApplication
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            kMenuStrip1 = new Kirilium.Controls.KMenuStrip();
            fileFToolStripMenuItem = new Kirilium.Controls.KToolStripMenuItem();
            kToolStripMenuItem1 = new Kirilium.Controls.KToolStripMenuItem();
            kToolStrip1 = new Kirilium.Controls.KToolStrip();
            kToolStripButton1 = new Kirilium.Controls.KToolStripButton();
            kToolStripButton2 = new Kirilium.Controls.KToolStripButton();
            kToolStripButton3 = new Kirilium.Controls.KToolStripButton();
            kButton1 = new Kirilium.Controls.KButton();
            kComboBox1 = new Kirilium.Controls.KComboBox();
            kListBox1 = new Kirilium.Controls.KListBox();
            kProgressBar1 = new Kirilium.Controls.KProgressBar();
            khScrollBar1 = new Kirilium.Controls.KHScrollBar();
            kvScrollBar1 = new Kirilium.Controls.KVScrollBar();
            kTextBox1 = new Kirilium.Controls.KTextBox();
            kSeekBar1 = new Kirilium.Controls.KSeekBar();
            kLabel1 = new Kirilium.Controls.KLabel();
            kGroupBox1 = new Kirilium.Controls.KGroupBox();
            kRadioButton1 = new Kirilium.Controls.KRadioButton();
            kRadioButton2 = new Kirilium.Controls.KRadioButton();
            kRadioButton3 = new Kirilium.Controls.KRadioButton();
            kCheckBox1 = new Kirilium.Controls.KCheckBox();
            kCheckBox2 = new Kirilium.Controls.KCheckBox();
            kTabControl1 = new Kirilium.Controls.KTabControl();
            kMenuStrip1.SuspendLayout();
            kToolStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // kMenuStrip1
            // 
            kMenuStrip1.AutoSize = false;
            kMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { fileFToolStripMenuItem });
            kMenuStrip1.Location = new System.Drawing.Point(0, 0);
            kMenuStrip1.Name = "kMenuStrip1";
            kMenuStrip1.Size = new System.Drawing.Size(800, 24);
            kMenuStrip1.TabIndex = 0;
            kMenuStrip1.Text = "kMenuStrip1";
            // 
            // fileFToolStripMenuItem
            // 
            fileFToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { kToolStripMenuItem1 });
            fileFToolStripMenuItem.Name = "fileFToolStripMenuItem";
            fileFToolStripMenuItem.Size = new System.Drawing.Size(51, 18);
            fileFToolStripMenuItem.Text = "File(&F)";
            // 
            // kToolStripMenuItem1
            // 
            kToolStripMenuItem1.Name = "kToolStripMenuItem1";
            kToolStripMenuItem1.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N;
            kToolStripMenuItem1.Size = new System.Drawing.Size(193, 22);
            kToolStripMenuItem1.Text = "Create New(&N)";
            // 
            // kToolStrip1
            // 
            kToolStrip1.AutoSize = false;
            kToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            kToolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { kToolStripButton1, kToolStripButton2, kToolStripButton3 });
            kToolStrip1.Location = new System.Drawing.Point(0, 24);
            kToolStrip1.Name = "kToolStrip1";
            kToolStrip1.Padding = new System.Windows.Forms.Padding(3);
            kToolStrip1.Size = new System.Drawing.Size(800, 30);
            kToolStrip1.TabIndex = 1;
            kToolStrip1.Text = "kToolStrip1";
            // 
            // kToolStripButton1
            // 
            kToolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            kToolStripButton1.Image = (System.Drawing.Image)resources.GetObject("kToolStripButton1.Image");
            kToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            kToolStripButton1.Name = "kToolStripButton1";
            kToolStripButton1.Size = new System.Drawing.Size(23, 21);
            kToolStripButton1.Text = "Image";
            // 
            // kToolStripButton2
            // 
            kToolStripButton2.Image = (System.Drawing.Image)resources.GetObject("kToolStripButton2.Image");
            kToolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            kToolStripButton2.Name = "kToolStripButton2";
            kToolStripButton2.Size = new System.Drawing.Size(102, 21);
            kToolStripButton2.Text = "ImageAndText";
            // 
            // kToolStripButton3
            // 
            kToolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            kToolStripButton3.Image = (System.Drawing.Image)resources.GetObject("kToolStripButton3.Image");
            kToolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            kToolStripButton3.Name = "kToolStripButton3";
            kToolStripButton3.Size = new System.Drawing.Size(68, 21);
            kToolStripButton3.Text = "TextButton";
            // 
            // kButton1
            // 
            kButton1.Height = 25;
            kButton1.Image = null;
            kButton1.Left = 12;
            kButton1.Location = new System.Drawing.Point(12, 52);
            kButton1.Name = "kButton1";
            kButton1.Size = new System.Drawing.Size(182, 25);
            kButton1.TabIndex = 2;
            kButton1.Text = "CHANGE THEME";
            kButton1.Top = 52;
            kButton1.Width = 182;
            kButton1.Click += kButton1_Click;
            // 
            // kComboBox1
            // 
            kComboBox1.FormattingEnabled = true;
            kComboBox1.Items.AddRange(new object[] { "Item 1", "Item 2", "Item 3", "Item 4" });
            kComboBox1.Location = new System.Drawing.Point(12, 83);
            kComboBox1.Name = "kComboBox1";
            kComboBox1.Size = new System.Drawing.Size(182, 24);
            kComboBox1.TabIndex = 3;
            // 
            // kListBox1
            // 
            kListBox1.Height = 146;
            kListBox1.Left = 12;
            kListBox1.Location = new System.Drawing.Point(12, 113);
            kListBox1.Name = "kListBox1";
            kListBox1.Size = new System.Drawing.Size(182, 146);
            kListBox1.TabIndex = 4;
            kListBox1.Top = 113;
            kListBox1.Width = 182;
            // 
            // kProgressBar1
            // 
            kProgressBar1.Height = 23;
            kProgressBar1.Left = 13;
            kProgressBar1.Location = new System.Drawing.Point(13, 266);
            kProgressBar1.MaximumValue = 100;
            kProgressBar1.MinimumValue = 0;
            kProgressBar1.Name = "kProgressBar1";
            kProgressBar1.ShowPercentAsText = true;
            kProgressBar1.Size = new System.Drawing.Size(181, 23);
            kProgressBar1.TabIndex = 5;
            kProgressBar1.Top = 266;
            kProgressBar1.Value = 0;
            kProgressBar1.Width = 181;
            // 
            // khScrollBar1
            // 
            khScrollBar1.DecrementStep = 3;
            khScrollBar1.Height = 15;
            khScrollBar1.IncrementStep = 3;
            khScrollBar1.Left = 13;
            khScrollBar1.Location = new System.Drawing.Point(13, 295);
            khScrollBar1.Maximum = 100;
            khScrollBar1.Minimum = 0;
            khScrollBar1.Name = "khScrollBar1";
            khScrollBar1.Size = new System.Drawing.Size(181, 15);
            khScrollBar1.TabIndex = 6;
            khScrollBar1.Top = 295;
            khScrollBar1.Value = 0;
            khScrollBar1.Width = 181;
            khScrollBar1.ValueChanged += khScrollBar1_ValueChanged;
            // 
            // kvScrollBar1
            // 
            kvScrollBar1.DecrementStep = 3;
            kvScrollBar1.Height = 258;
            kvScrollBar1.IncrementStep = 3;
            kvScrollBar1.Left = 200;
            kvScrollBar1.Location = new System.Drawing.Point(200, 52);
            kvScrollBar1.Maximum = 100;
            kvScrollBar1.Minimum = 0;
            kvScrollBar1.Name = "kvScrollBar1";
            kvScrollBar1.Size = new System.Drawing.Size(15, 258);
            kvScrollBar1.TabIndex = 7;
            kvScrollBar1.Top = 52;
            kvScrollBar1.Value = 0;
            kvScrollBar1.Width = 15;
            // 
            // kTextBox1
            // 
            kTextBox1.Height = 129;
            kTextBox1.Left = 13;
            kTextBox1.Location = new System.Drawing.Point(13, 316);
            kTextBox1.Multiline = true;
            kTextBox1.Name = "kTextBox1";
            kTextBox1.ReadOnly = false;
            kTextBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            kTextBox1.Size = new System.Drawing.Size(202, 129);
            kTextBox1.TabIndex = 8;
            kTextBox1.Top = 316;
            kTextBox1.Width = 202;
            // 
            // kSeekBar1
            // 
            kSeekBar1.AllowMouseWheelValueChange = false;
            kSeekBar1.DecrementStep = 3;
            kSeekBar1.Height = 20;
            kSeekBar1.IncrementStep = 3;
            kSeekBar1.Left = 221;
            kSeekBar1.Location = new System.Drawing.Point(221, 52);
            kSeekBar1.Maximum = 1000;
            kSeekBar1.Minimum = 0;
            kSeekBar1.Name = "kSeekBar1";
            kSeekBar1.Size = new System.Drawing.Size(567, 20);
            kSeekBar1.TabIndex = 9;
            kSeekBar1.Top = 52;
            kSeekBar1.Value = 0;
            kSeekBar1.Width = 567;
            // 
            // kLabel1
            // 
            kLabel1.Height = 23;
            kLabel1.Left = 221;
            kLabel1.Location = new System.Drawing.Point(221, 78);
            kLabel1.Name = "kLabel1";
            kLabel1.Size = new System.Drawing.Size(130, 23);
            kLabel1.TabIndex = 10;
            kLabel1.TextLayout = Kirilium.Controls.KLabelTextLayout.LeftCenter;
            kLabel1.Top = 78;
            kLabel1.Width = 130;
            // 
            // kGroupBox1
            // 
            kGroupBox1.Height = 140;
            kGroupBox1.Left = 221;
            kGroupBox1.Location = new System.Drawing.Point(221, 107);
            kGroupBox1.Name = "kGroupBox1";
            kGroupBox1.Size = new System.Drawing.Size(130, 140);
            kGroupBox1.TabIndex = 11;
            kGroupBox1.Text = "KGroupBox1";
            kGroupBox1.Top = 107;
            kGroupBox1.Width = 130;
            // 
            // kRadioButton1
            // 
            kRadioButton1.Checked = false;
            kRadioButton1.Height = 19;
            kRadioButton1.Left = 229;
            kRadioButton1.Location = new System.Drawing.Point(229, 123);
            kRadioButton1.Name = "kRadioButton1";
            kRadioButton1.Size = new System.Drawing.Size(103, 19);
            kRadioButton1.TabIndex = 12;
            kRadioButton1.Text = "KRadioButton1";
            kRadioButton1.Top = 123;
            kRadioButton1.Width = 103;
            // 
            // kRadioButton2
            // 
            kRadioButton2.Checked = false;
            kRadioButton2.Height = 19;
            kRadioButton2.Left = 229;
            kRadioButton2.Location = new System.Drawing.Point(229, 148);
            kRadioButton2.Name = "kRadioButton2";
            kRadioButton2.Size = new System.Drawing.Size(103, 19);
            kRadioButton2.TabIndex = 13;
            kRadioButton2.Text = "KRadioButton2";
            kRadioButton2.Top = 148;
            kRadioButton2.Width = 103;
            // 
            // kRadioButton3
            // 
            kRadioButton3.Checked = false;
            kRadioButton3.Height = 19;
            kRadioButton3.Left = 229;
            kRadioButton3.Location = new System.Drawing.Point(229, 173);
            kRadioButton3.Name = "kRadioButton3";
            kRadioButton3.Size = new System.Drawing.Size(103, 19);
            kRadioButton3.TabIndex = 14;
            kRadioButton3.Text = "KRadioButton3";
            kRadioButton3.Top = 173;
            kRadioButton3.Width = 103;
            // 
            // kCheckBox1
            // 
            kCheckBox1.Checked = false;
            kCheckBox1.CheckState = System.Windows.Forms.CheckState.Unchecked;
            kCheckBox1.Height = 19;
            kCheckBox1.Left = 229;
            kCheckBox1.Location = new System.Drawing.Point(229, 198);
            kCheckBox1.Name = "kCheckBox1";
            kCheckBox1.Size = new System.Drawing.Size(90, 19);
            kCheckBox1.TabIndex = 15;
            kCheckBox1.Text = "KCheckBox1";
            kCheckBox1.ThreeState = false;
            kCheckBox1.Top = 198;
            kCheckBox1.Width = 90;
            // 
            // kCheckBox2
            // 
            kCheckBox2.Checked = false;
            kCheckBox2.CheckState = System.Windows.Forms.CheckState.Unchecked;
            kCheckBox2.Height = 19;
            kCheckBox2.Left = 229;
            kCheckBox2.Location = new System.Drawing.Point(229, 223);
            kCheckBox2.Name = "kCheckBox2";
            kCheckBox2.Size = new System.Drawing.Size(90, 19);
            kCheckBox2.TabIndex = 16;
            kCheckBox2.Text = "KCheckBox2";
            kCheckBox2.ThreeState = true;
            kCheckBox2.Top = 223;
            kCheckBox2.Width = 90;
            // 
            // kTabControl1
            // 
            kTabControl1.Height = 360;
            kTabControl1.IsClosable = false;
            kTabControl1.Left = 357;
            kTabControl1.Location = new System.Drawing.Point(357, 78);
            kTabControl1.Name = "kTabControl1";
            kTabControl1.SelectedIndex = 0;
            kTabControl1.Size = new System.Drawing.Size(431, 360);
            kTabControl1.TabHeaderHeight = 20;
            kTabControl1.TabIndex = 17;
            kTabControl1.Top = 78;
            kTabControl1.Width = 431;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            ClientSize = new System.Drawing.Size(800, 450);
            Controls.Add(kTabControl1);
            Controls.Add(kCheckBox2);
            Controls.Add(kCheckBox1);
            Controls.Add(kRadioButton3);
            Controls.Add(kRadioButton2);
            Controls.Add(kRadioButton1);
            Controls.Add(kGroupBox1);
            Controls.Add(kLabel1);
            Controls.Add(kSeekBar1);
            Controls.Add(kTextBox1);
            Controls.Add(kvScrollBar1);
            Controls.Add(khScrollBar1);
            Controls.Add(kProgressBar1);
            Controls.Add(kListBox1);
            Controls.Add(kComboBox1);
            Controls.Add(kButton1);
            Controls.Add(kToolStrip1);
            Controls.Add(kMenuStrip1);
            Font = new System.Drawing.Font("Yu Gothic UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            MainMenuStrip = kMenuStrip1;
            Name = "MainForm";
            Text = "MainForm";
            kMenuStrip1.ResumeLayout(false);
            kMenuStrip1.PerformLayout();
            kToolStrip1.ResumeLayout(false);
            kToolStrip1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Kirilium.Controls.KMenuStrip kMenuStrip1;
        private Kirilium.Controls.KToolStripMenuItem fileFToolStripMenuItem;
        private Kirilium.Controls.KToolStrip kToolStrip1;
        private Kirilium.Controls.KToolStripButton kToolStripButton1;
        private Kirilium.Controls.KToolStripButton kToolStripButton2;
        private Kirilium.Controls.KToolStripButton kToolStripButton3;
        private Kirilium.Controls.KButton kButton1;
        private Kirilium.Controls.KComboBox kComboBox1;
        private Kirilium.Controls.KListBox kListBox1;
        private Kirilium.Controls.KProgressBar kProgressBar1;
        private Kirilium.Controls.KHScrollBar khScrollBar1;
        private Kirilium.Controls.KVScrollBar kvScrollBar1;
        private Kirilium.Controls.KTextBox kTextBox1;
        private Kirilium.Controls.KSeekBar kSeekBar1;
        private Kirilium.Controls.KLabel kLabel1;
        private Kirilium.Controls.KGroupBox kGroupBox1;
        private Kirilium.Controls.KRadioButton kRadioButton1;
        private Kirilium.Controls.KRadioButton kRadioButton2;
        private Kirilium.Controls.KRadioButton kRadioButton3;
        private Kirilium.Controls.KCheckBox kCheckBox1;
        private Kirilium.Controls.KCheckBox kCheckBox2;
        private Kirilium.Controls.KTabControl kTabControl1;
        private Kirilium.Controls.KToolStripMenuItem kToolStripMenuItem1;
    }
}