namespace AqlaEvents
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.toolStripContainer = new System.Windows.Forms.ToolStripContainer();
            this.statusLabel = new System.Windows.Forms.Label();
            this.outputLabel = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.backButton = new System.Windows.Forms.ToolStripButton();
            this.forwardButton = new System.Windows.Forms.ToolStripButton();
            this.urlTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.goButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.QueryBox = new System.Windows.Forms.ToolStripComboBox();
            this.NextQueryButton = new System.Windows.Forms.ToolStripButton();
            this.DeleteQuery = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripSeparator();
            this.ThisWeekButton = new System.Windows.Forms.ToolStripButton();
            this.NextWeekButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.FeedButton = new System.Windows.Forms.ToolStripButton();
            this.PagesFeedButton = new System.Windows.Forms.ToolStripButton();
            this.ClipboardTimer = new System.Windows.Forms.Timer(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.toolStripContainer.ContentPanel.SuspendLayout();
            this.toolStripContainer.TopToolStripPanel.SuspendLayout();
            this.toolStripContainer.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripContainer
            // 
            // 
            // toolStripContainer.ContentPanel
            // 
            this.toolStripContainer.ContentPanel.Controls.Add(this.statusLabel);
            this.toolStripContainer.ContentPanel.Controls.Add(this.outputLabel);
            this.toolStripContainer.ContentPanel.Size = new System.Drawing.Size(1326, 725);
            this.toolStripContainer.ContentPanel.Load += new System.EventHandler(this.toolStripContainer_ContentPanel_Load);
            this.toolStripContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStripContainer.LeftToolStripPanelVisible = false;
            this.toolStripContainer.Location = new System.Drawing.Point(0, 0);
            this.toolStripContainer.Name = "toolStripContainer";
            this.toolStripContainer.RightToolStripPanelVisible = false;
            this.toolStripContainer.Size = new System.Drawing.Size(1326, 752);
            this.toolStripContainer.TabIndex = 0;
            this.toolStripContainer.Text = "toolStripContainer1";
            // 
            // toolStripContainer.TopToolStripPanel
            // 
            this.toolStripContainer.TopToolStripPanel.Controls.Add(this.toolStrip1);
            // 
            // statusLabel
            // 
            this.statusLabel.AutoSize = true;
            this.statusLabel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.statusLabel.Location = new System.Drawing.Point(0, 695);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(0, 15);
            this.statusLabel.TabIndex = 1;
            // 
            // outputLabel
            // 
            this.outputLabel.AutoSize = true;
            this.outputLabel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.outputLabel.Location = new System.Drawing.Point(0, 710);
            this.outputLabel.Name = "outputLabel";
            this.outputLabel.Size = new System.Drawing.Size(0, 15);
            this.outputLabel.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(18, 18);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.backButton,
            this.forwardButton,
            this.urlTextBox,
            this.goButton,
            this.toolStripSeparator1,
            this.QueryBox,
            this.NextQueryButton,
            this.DeleteQuery,
            this.toolStripButton1,
            this.ThisWeekButton,
            this.NextWeekButton,
            this.toolStripSeparator2,
            this.FeedButton,
            this.PagesFeedButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(0);
            this.toolStrip1.Size = new System.Drawing.Size(1326, 27);
            this.toolStrip1.Stretch = true;
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Layout += new System.Windows.Forms.LayoutEventHandler(this.HandleToolStripLayout);
            // 
            // backButton
            // 
            this.backButton.Enabled = false;
            this.backButton.Image = global::AqlaEvents.Properties.Resources.nav_left_green;
            this.backButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(59, 24);
            this.backButton.Text = "Back";
            this.backButton.Click += new System.EventHandler(this.BackButtonClick);
            // 
            // forwardButton
            // 
            this.forwardButton.Enabled = false;
            this.forwardButton.Image = global::AqlaEvents.Properties.Resources.nav_right_green;
            this.forwardButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.forwardButton.Name = "forwardButton";
            this.forwardButton.Size = new System.Drawing.Size(81, 24);
            this.forwardButton.Text = "Forward";
            this.forwardButton.Click += new System.EventHandler(this.ForwardButtonClick);
            // 
            // urlTextBox
            // 
            this.urlTextBox.AutoSize = false;
            this.urlTextBox.Name = "urlTextBox";
            this.urlTextBox.Size = new System.Drawing.Size(500, 25);
            this.urlTextBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.UrlTextBoxKeyUp);
            this.urlTextBox.Click += new System.EventHandler(this.urlTextBox_Click);
            // 
            // goButton
            // 
            this.goButton.Image = global::AqlaEvents.Properties.Resources.nav_plain_green;
            this.goButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.goButton.Name = "goButton";
            this.goButton.Size = new System.Drawing.Size(49, 24);
            this.goButton.Text = "Go";
            this.goButton.Click += new System.EventHandler(this.GoButtonClick);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 27);
            // 
            // QueryBox
            // 
            this.QueryBox.Name = "QueryBox";
            this.QueryBox.Size = new System.Drawing.Size(121, 27);
            this.QueryBox.SelectedIndexChanged += new System.EventHandler(this.QueryBox_SelectedIndexChanged);
            this.QueryBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.QueryBox_KeyUp);
            this.QueryBox.Click += new System.EventHandler(this.QueryBox_Click);
            // 
            // NextQueryButton
            // 
            this.NextQueryButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.NextQueryButton.Image = global::AqlaEvents.Properties.Resources.nav_right_green;
            this.NextQueryButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.NextQueryButton.Name = "NextQueryButton";
            this.NextQueryButton.Size = new System.Drawing.Size(23, 24);
            this.NextQueryButton.Text = "Next Or Add Query";
            this.NextQueryButton.Click += new System.EventHandler(this.NextQueryButton_Click);
            // 
            // DeleteQuery
            // 
            this.DeleteQuery.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.DeleteQuery.Image = ((System.Drawing.Image)(resources.GetObject("DeleteQuery.Image")));
            this.DeleteQuery.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.DeleteQuery.Name = "DeleteQuery";
            this.DeleteQuery.Size = new System.Drawing.Size(33, 24);
            this.DeleteQuery.Text = "Del";
            this.DeleteQuery.Click += new System.EventHandler(this.DeleteQuery_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(6, 27);
            // 
            // ThisWeekButton
            // 
            this.ThisWeekButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.ThisWeekButton.Image = ((System.Drawing.Image)(resources.GetObject("ThisWeekButton.Image")));
            this.ThisWeekButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ThisWeekButton.Name = "ThisWeekButton";
            this.ThisWeekButton.Size = new System.Drawing.Size(107, 24);
            this.ThisWeekButton.Text = "ALL THIS WEEK";
            this.ThisWeekButton.Click += new System.EventHandler(this.ThisWeekButton_Click);
            // 
            // NextWeekButton
            // 
            this.NextWeekButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.NextWeekButton.Image = ((System.Drawing.Image)(resources.GetObject("NextWeekButton.Image")));
            this.NextWeekButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.NextWeekButton.Name = "NextWeekButton";
            this.NextWeekButton.Size = new System.Drawing.Size(45, 24);
            this.NextWeekButton.Text = "NEXT";
            this.NextWeekButton.Click += new System.EventHandler(this.NextWeekButton_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 27);
            // 
            // FeedButton
            // 
            this.FeedButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.FeedButton.Image = ((System.Drawing.Image)(resources.GetObject("FeedButton.Image")));
            this.FeedButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.FeedButton.Name = "FeedButton";
            this.FeedButton.Size = new System.Drawing.Size(44, 24);
            this.FeedButton.Text = "FEED";
            this.FeedButton.Click += new System.EventHandler(this.FeedButton_Click);
            // 
            // PagesFeedButton
            // 
            this.PagesFeedButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.PagesFeedButton.Image = ((System.Drawing.Image)(resources.GetObject("PagesFeedButton.Image")));
            this.PagesFeedButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.PagesFeedButton.Name = "PagesFeedButton";
            this.PagesFeedButton.Size = new System.Drawing.Size(53, 24);
            this.PagesFeedButton.Text = "PAGES";
            this.PagesFeedButton.Click += new System.EventHandler(this.PagesFeedButton_Click);
            // 
            // ClipboardTimer
            // 
            this.ClipboardTimer.Enabled = true;
            this.ClipboardTimer.Tick += new System.EventHandler(this.ClipboardTimer_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1326, 752);
            this.Controls.Add(this.toolStripContainer);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "BrowserForm";
            this.toolStripContainer.ContentPanel.ResumeLayout(false);
            this.toolStripContainer.ContentPanel.PerformLayout();
            this.toolStripContainer.TopToolStripPanel.ResumeLayout(false);
            this.toolStripContainer.TopToolStripPanel.PerformLayout();
            this.toolStripContainer.ResumeLayout(false);
            this.toolStripContainer.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStripContainer toolStripContainer;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton backButton;
        private System.Windows.Forms.ToolStripButton forwardButton;
        private System.Windows.Forms.ToolStripTextBox urlTextBox;
        private System.Windows.Forms.ToolStripButton goButton;
        private System.Windows.Forms.Label outputLabel;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.Timer ClipboardTimer;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripComboBox QueryBox;
        private System.Windows.Forms.ToolStripButton NextQueryButton;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripButton DeleteQuery;
        private System.Windows.Forms.ToolStripSeparator toolStripButton1;
        private System.Windows.Forms.ToolStripButton ThisWeekButton;
        private System.Windows.Forms.ToolStripButton NextWeekButton;
        private System.Windows.Forms.ToolStripButton FeedButton;
        private System.Windows.Forms.ToolStripButton PagesFeedButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    }
}

