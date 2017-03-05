using Crom.Controls;
using StockDataStreamer.Forms;

namespace StockDataStreamer
{
    partial class Main
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.downloadDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.dockContainer1 = new Crom.Controls.Docking.DockContainer();
            this.mainPanel = new StockDataStreamer.Forms.MainPanel();
            this.algsPanel = new StockDataStreamer.Forms.AlgorithmsPanel();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(796, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.downloadDataToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // downloadDataToolStripMenuItem
            // 
            this.downloadDataToolStripMenuItem.Name = "downloadDataToolStripMenuItem";
            this.downloadDataToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
            this.downloadDataToolStripMenuItem.Text = "Download Data";
            this.downloadDataToolStripMenuItem.Click += new System.EventHandler(this.downloadDataToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 598);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(796, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // dockContainer1
            // 
            this.dockContainer1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.dockContainer1.CanMoveByMouseFilledForms = true;
            this.dockContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dockContainer1.Location = new System.Drawing.Point(0, 24);
            this.dockContainer1.MinimumSize = new System.Drawing.Size(504, 528);
            this.dockContainer1.Name = "dockContainer1";
            this.dockContainer1.Size = new System.Drawing.Size(796, 574);
            this.dockContainer1.TabIndex = 2;
            this.dockContainer1.TitleBarGradientColor1 = System.Drawing.SystemColors.ControlDarkDark;
            this.dockContainer1.TitleBarGradientColor2 = System.Drawing.Color.White;
            this.dockContainer1.TitleBarGradientSelectedColor1 = System.Drawing.Color.DarkGray;
            this.dockContainer1.TitleBarGradientSelectedColor2 = System.Drawing.Color.White;
            this.dockContainer1.TitleBarTextColor = System.Drawing.Color.Black;
            // 
            // mainPanel
            // 
            this.mainPanel.ClientSize = new System.Drawing.Size(294, 517);
            this.mainPanel.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.mainPanel.FormInfo = null;
            this.mainPanel.Location = new System.Drawing.Point(0, 27);
            this.mainPanel.MaximizeBox = false;
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.ShowInTaskbar = false;
            this.mainPanel.Visible = false;
            // 
            // algsPanel
            // 
            this.algsPanel.ClientSize = new System.Drawing.Size(294, 517);
            this.algsPanel.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.algsPanel.FormInfo = null;
            this.algsPanel.Location = new System.Drawing.Point(0, 27);
            this.algsPanel.MaximizeBox = false;
            this.algsPanel.Name = "algsPanel";
            this.algsPanel.ShowInTaskbar = false;
            this.algsPanel.Text = "AlgorithmsPanel";
            this.algsPanel.Visible = false;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(796, 620);
            this.Controls.Add(this.dockContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Main";
            this.Text = "Main";
            this.Load += new System.EventHandler(this.Main_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripMenuItem downloadDataToolStripMenuItem;
        private Crom.Controls.Docking.DockContainer dockContainer1;
        private StockDataStreamer.Forms.MainPanel mainPanel;
        private AlgorithmsPanel algsPanel;
    }
}