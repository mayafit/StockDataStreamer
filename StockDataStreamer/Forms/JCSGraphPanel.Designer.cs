namespace StockDataStreamer.Forms
{
    partial class JCSGraphPanel
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
            this.graph = new ZedGraph.ZedGraphControl();
            this.SuspendLayout();
            // 
            // graph
            // 
            this.graph.Dock = System.Windows.Forms.DockStyle.Fill;
            this.graph.IsEnableSelection = true;
            this.graph.IsShowCursorValues = true;
            this.graph.IsShowHScrollBar = true;
            this.graph.IsShowPointValues = true;
            this.graph.IsShowVScrollBar = true;
            this.graph.Location = new System.Drawing.Point(0, 0);
            this.graph.Name = "graph";
            this.graph.ScrollGrace = 0;
            this.graph.ScrollMaxX = 0;
            this.graph.ScrollMaxY = 0;
            this.graph.ScrollMaxY2 = 0;
            this.graph.ScrollMinX = 0;
            this.graph.ScrollMinY = 0;
            this.graph.ScrollMinY2 = 0;
            this.graph.Size = new System.Drawing.Size(284, 264);
            this.graph.TabIndex = 0;
            // 
            // JCSGraphPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 264);
            this.Controls.Add(this.graph);
            this.Name = "JCSGraphPanel";
            this.Text = "JCSGraphPanel";
            this.ResumeLayout(false);

        }

        #endregion

        private ZedGraph.ZedGraphControl graph;
    }
}