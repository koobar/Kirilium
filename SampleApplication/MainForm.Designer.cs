
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
            this.kMenuStrip1 = new Kirilium.Controls.KMenuStrip();
            this.fileFToolStripMenuItem = new Kirilium.Controls.KToolStripMenuItem();
            this.kToolStrip1 = new Kirilium.Controls.KToolStrip();
            this.kToolStripButton1 = new Kirilium.Controls.KToolStripButton();
            this.kToolStripButton2 = new Kirilium.Controls.KToolStripButton();
            this.kToolStripButton3 = new Kirilium.Controls.KToolStripButton();
            this.kButton1 = new Kirilium.Controls.KButton();
            this.kComboBox1 = new Kirilium.Controls.KComboBox();
            this.kListBox1 = new Kirilium.Controls.KListBox();
            this.kProgressBar1 = new Kirilium.Controls.KProgressBar();
            this.khScrollBar1 = new Kirilium.Controls.KHScrollBar();
            this.kvScrollBar1 = new Kirilium.Controls.KVScrollBar();
            this.kTextBox1 = new Kirilium.Controls.KTextBox();
            this.kSeekBar1 = new Kirilium.Controls.KSeekBar();
            this.kLabel1 = new Kirilium.Controls.KLabel();
            this.kGroupBox1 = new Kirilium.Controls.KGroupBox();
            this.kRadioButton1 = new Kirilium.Controls.KRadioButton();
            this.kRadioButton2 = new Kirilium.Controls.KRadioButton();
            this.kRadioButton3 = new Kirilium.Controls.KRadioButton();
            this.kCheckBox1 = new Kirilium.Controls.KCheckBox();
            this.kCheckBox2 = new Kirilium.Controls.KCheckBox();
            this.kTabControl1 = new Kirilium.Controls.KTabControl();
            this.kMenuStrip1.SuspendLayout();
            this.kToolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // kMenuStrip1
            // 
            this.kMenuStrip1.AutoSize = false;
            this.kMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileFToolStripMenuItem});
            this.kMenuStrip1.Location = new System.Drawing.Point(0, 0);
            this.kMenuStrip1.Name = "kMenuStrip1";
            this.kMenuStrip1.Size = new System.Drawing.Size(800, 24);
            this.kMenuStrip1.TabIndex = 0;
            this.kMenuStrip1.Text = "kMenuStrip1";
            // 
            // fileFToolStripMenuItem
            // 
            this.fileFToolStripMenuItem.Name = "fileFToolStripMenuItem";
            this.fileFToolStripMenuItem.Size = new System.Drawing.Size(51, 18);
            this.fileFToolStripMenuItem.Text = "File(&F)";
            // 
            // kToolStrip1
            // 
            this.kToolStrip1.AutoSize = false;
            this.kToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.kToolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.kToolStripButton1,
            this.kToolStripButton2,
            this.kToolStripButton3});
            this.kToolStrip1.Location = new System.Drawing.Point(0, 24);
            this.kToolStrip1.Name = "kToolStrip1";
            this.kToolStrip1.Padding = new System.Windows.Forms.Padding(3);
            this.kToolStrip1.Size = new System.Drawing.Size(800, 25);
            this.kToolStrip1.TabIndex = 1;
            this.kToolStrip1.Text = "kToolStrip1";
            // 
            // kToolStripButton1
            // 
            this.kToolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.kToolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("kToolStripButton1.Image")));
            this.kToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.kToolStripButton1.Name = "kToolStripButton1";
            this.kToolStripButton1.Size = new System.Drawing.Size(23, 16);
            this.kToolStripButton1.Text = "Image";
            // 
            // kToolStripButton2
            // 
            this.kToolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("kToolStripButton2.Image")));
            this.kToolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.kToolStripButton2.Name = "kToolStripButton2";
            this.kToolStripButton2.Size = new System.Drawing.Size(102, 16);
            this.kToolStripButton2.Text = "ImageAndText";
            // 
            // kToolStripButton3
            // 
            this.kToolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.kToolStripButton3.Image = ((System.Drawing.Image)(resources.GetObject("kToolStripButton3.Image")));
            this.kToolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.kToolStripButton3.Name = "kToolStripButton3";
            this.kToolStripButton3.Size = new System.Drawing.Size(68, 16);
            this.kToolStripButton3.Text = "TextButton";
            // 
            // kButton1
            // 
            this.kButton1.Location = new System.Drawing.Point(12, 52);
            this.kButton1.Name = "kButton1";
            this.kButton1.Size = new System.Drawing.Size(182, 25);
            this.kButton1.TabIndex = 2;
            this.kButton1.Text = "PUSH ME";
            this.kButton1.UseVisualStyleBackColor = true;
            this.kButton1.Click += new System.EventHandler(this.kButton1_Click);
            // 
            // kComboBox1
            // 
            this.kComboBox1.FormattingEnabled = true;
            this.kComboBox1.Items.AddRange(new object[] {
            "Item 1",
            "Item 2",
            "Item 3",
            "Item 4"});
            this.kComboBox1.Location = new System.Drawing.Point(12, 83);
            this.kComboBox1.Name = "kComboBox1";
            this.kComboBox1.Size = new System.Drawing.Size(182, 24);
            this.kComboBox1.TabIndex = 3;
            // 
            // kListBox1
            // 
            this.kListBox1.Location = new System.Drawing.Point(12, 113);
            this.kListBox1.Name = "kListBox1";
            this.kListBox1.Padding = new System.Windows.Forms.Padding(1);
            this.kListBox1.Size = new System.Drawing.Size(182, 146);
            this.kListBox1.TabIndex = 4;
            // 
            // kProgressBar1
            // 
            this.kProgressBar1.Location = new System.Drawing.Point(13, 266);
            this.kProgressBar1.MaximumValue = 100;
            this.kProgressBar1.MinimumValue = 0;
            this.kProgressBar1.Name = "kProgressBar1";
            this.kProgressBar1.ShowPercentAsText = true;
            this.kProgressBar1.Size = new System.Drawing.Size(181, 23);
            this.kProgressBar1.TabIndex = 5;
            this.kProgressBar1.Text = "kProgressBar1";
            this.kProgressBar1.Value = 0;
            // 
            // khScrollBar1
            // 
            this.khScrollBar1.DecrementStep = 3;
            this.khScrollBar1.IncrementStep = 3;
            this.khScrollBar1.Location = new System.Drawing.Point(13, 295);
            this.khScrollBar1.MaximumValue = 100;
            this.khScrollBar1.MinimumValue = 0;
            this.khScrollBar1.Name = "khScrollBar1";
            this.khScrollBar1.Size = new System.Drawing.Size(181, 15);
            this.khScrollBar1.TabIndex = 6;
            this.khScrollBar1.Text = "khScrollBar1";
            this.khScrollBar1.ThumbSize = 45;
            this.khScrollBar1.Value = 0;
            this.khScrollBar1.ValueChanged += new System.EventHandler(this.khScrollBar1_ValueChanged);
            // 
            // kvScrollBar1
            // 
            this.kvScrollBar1.DecrementStep = 3;
            this.kvScrollBar1.IncrementStep = 3;
            this.kvScrollBar1.Location = new System.Drawing.Point(200, 52);
            this.kvScrollBar1.MaximumValue = 100;
            this.kvScrollBar1.MinimumValue = 0;
            this.kvScrollBar1.Name = "kvScrollBar1";
            this.kvScrollBar1.Size = new System.Drawing.Size(15, 258);
            this.kvScrollBar1.TabIndex = 7;
            this.kvScrollBar1.Text = "kvScrollBar1";
            this.kvScrollBar1.ThumbSize = 45;
            this.kvScrollBar1.Value = 0;
            // 
            // kTextBox1
            // 
            this.kTextBox1.Location = new System.Drawing.Point(13, 316);
            this.kTextBox1.Multiline = true;
            this.kTextBox1.Name = "kTextBox1";
            this.kTextBox1.ReadOnly = false;
            this.kTextBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.kTextBox1.Size = new System.Drawing.Size(202, 129);
            this.kTextBox1.TabIndex = 8;
            // 
            // kSeekBar1
            // 
            this.kSeekBar1.AllowMouseWheelValueChange = false;
            this.kSeekBar1.DecrementStep = 3;
            this.kSeekBar1.IncrementStep = 3;
            this.kSeekBar1.Location = new System.Drawing.Point(221, 52);
            this.kSeekBar1.MaximumValue = 100;
            this.kSeekBar1.MinimumValue = 0;
            this.kSeekBar1.Name = "kSeekBar1";
            this.kSeekBar1.Size = new System.Drawing.Size(567, 20);
            this.kSeekBar1.TabIndex = 9;
            this.kSeekBar1.Text = "kSeekBar1";
            this.kSeekBar1.Value = 0;
            // 
            // kLabel1
            // 
            this.kLabel1.Location = new System.Drawing.Point(221, 78);
            this.kLabel1.Name = "kLabel1";
            this.kLabel1.Size = new System.Drawing.Size(130, 23);
            this.kLabel1.TabIndex = 10;
            this.kLabel1.Text = "This is a Label.";
            this.kLabel1.TextLayout = Kirilium.Controls.KLabelTextLayout.LeftCenter;
            // 
            // kGroupBox1
            // 
            this.kGroupBox1.Location = new System.Drawing.Point(221, 107);
            this.kGroupBox1.Name = "kGroupBox1";
            this.kGroupBox1.Size = new System.Drawing.Size(130, 140);
            this.kGroupBox1.TabIndex = 11;
            this.kGroupBox1.Text = "kGroupBox1";
            // 
            // kRadioButton1
            // 
            this.kRadioButton1.AutoSize = true;
            this.kRadioButton1.Location = new System.Drawing.Point(229, 123);
            this.kRadioButton1.Name = "kRadioButton1";
            this.kRadioButton1.Size = new System.Drawing.Size(103, 19);
            this.kRadioButton1.TabIndex = 12;
            this.kRadioButton1.TabStop = true;
            this.kRadioButton1.Text = "kRadioButton1";
            this.kRadioButton1.UseVisualStyleBackColor = true;
            // 
            // kRadioButton2
            // 
            this.kRadioButton2.AutoSize = true;
            this.kRadioButton2.Location = new System.Drawing.Point(229, 148);
            this.kRadioButton2.Name = "kRadioButton2";
            this.kRadioButton2.Size = new System.Drawing.Size(103, 19);
            this.kRadioButton2.TabIndex = 13;
            this.kRadioButton2.TabStop = true;
            this.kRadioButton2.Text = "kRadioButton2";
            this.kRadioButton2.UseVisualStyleBackColor = true;
            // 
            // kRadioButton3
            // 
            this.kRadioButton3.AutoSize = true;
            this.kRadioButton3.Location = new System.Drawing.Point(229, 173);
            this.kRadioButton3.Name = "kRadioButton3";
            this.kRadioButton3.Size = new System.Drawing.Size(103, 19);
            this.kRadioButton3.TabIndex = 14;
            this.kRadioButton3.TabStop = true;
            this.kRadioButton3.Text = "kRadioButton3";
            this.kRadioButton3.UseVisualStyleBackColor = true;
            // 
            // kCheckBox1
            // 
            this.kCheckBox1.AutoSize = true;
            this.kCheckBox1.Location = new System.Drawing.Point(229, 198);
            this.kCheckBox1.Name = "kCheckBox1";
            this.kCheckBox1.Size = new System.Drawing.Size(90, 19);
            this.kCheckBox1.TabIndex = 15;
            this.kCheckBox1.Text = "kCheckBox1";
            this.kCheckBox1.UseVisualStyleBackColor = true;
            // 
            // kCheckBox2
            // 
            this.kCheckBox2.AutoSize = true;
            this.kCheckBox2.Location = new System.Drawing.Point(229, 223);
            this.kCheckBox2.Name = "kCheckBox2";
            this.kCheckBox2.Size = new System.Drawing.Size(90, 19);
            this.kCheckBox2.TabIndex = 16;
            this.kCheckBox2.Text = "kCheckBox2";
            this.kCheckBox2.ThreeState = true;
            this.kCheckBox2.UseVisualStyleBackColor = true;
            // 
            // kTabControl1
            // 
            this.kTabControl1.IsClosable = false;
            this.kTabControl1.Location = new System.Drawing.Point(357, 78);
            this.kTabControl1.Name = "kTabControl1";
            this.kTabControl1.SelectedIndex = 0;
            this.kTabControl1.Size = new System.Drawing.Size(431, 360);
            this.kTabControl1.TabHeaderHeight = 20;
            this.kTabControl1.TabIndex = 17;
            this.kTabControl1.Text = "kTabControl1";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.kTabControl1);
            this.Controls.Add(this.kCheckBox2);
            this.Controls.Add(this.kCheckBox1);
            this.Controls.Add(this.kRadioButton3);
            this.Controls.Add(this.kRadioButton2);
            this.Controls.Add(this.kRadioButton1);
            this.Controls.Add(this.kGroupBox1);
            this.Controls.Add(this.kLabel1);
            this.Controls.Add(this.kSeekBar1);
            this.Controls.Add(this.kTextBox1);
            this.Controls.Add(this.kvScrollBar1);
            this.Controls.Add(this.khScrollBar1);
            this.Controls.Add(this.kProgressBar1);
            this.Controls.Add(this.kListBox1);
            this.Controls.Add(this.kComboBox1);
            this.Controls.Add(this.kButton1);
            this.Controls.Add(this.kToolStrip1);
            this.Controls.Add(this.kMenuStrip1);
            this.Font = new System.Drawing.Font("Yu Gothic UI", 9F);
            this.MainMenuStrip = this.kMenuStrip1;
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.kMenuStrip1.ResumeLayout(false);
            this.kMenuStrip1.PerformLayout();
            this.kToolStrip1.ResumeLayout(false);
            this.kToolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

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
    }
}