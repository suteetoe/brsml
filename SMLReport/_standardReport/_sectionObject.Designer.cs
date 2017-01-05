namespace SMLReport._standardReport
{
    partial class _sectionObject
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(_sectionObject));
            this._toolBar = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this._label = new System.Windows.Forms.ToolStripLabel();
            this._rulerControl = new SMLReport._design._rulerControl();
            this._drawPanel = new System.Windows.Forms.Panel();
            this._draw = new SMLReport._design._drawPanel();
            this._toolBar.SuspendLayout();
            this._drawPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // _toolBar
            // 
            this._toolBar.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this._toolBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this._label});
            this._toolBar.Location = new System.Drawing.Point(0, 0);
            this._toolBar.Name = "_toolBar";
            this._toolBar.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this._toolBar.Size = new System.Drawing.Size(424, 25);
            this._toolBar.TabIndex = 0;
            this._toolBar.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "toolStripButton1";
            // 
            // _label
            // 
            this._label.BackColor = System.Drawing.SystemColors.Control;
            this._label.Name = "_label";
            this._label.Size = new System.Drawing.Size(78, 22);
            this._label.Text = "toolStripLabel1";
            // 
            // _rulerControl
            // 
            this._rulerControl.BackColor = System.Drawing.Color.Transparent;
            this._rulerControl._beginValue = 0F;
            this._rulerControl.Dock = System.Windows.Forms.DockStyle.Left;
            this._rulerControl.Location = new System.Drawing.Point(0, 25);
            this._rulerControl.Name = "_rulerControl";
            this._rulerControl._ruleScale = 1F;
            this._rulerControl.Size = new System.Drawing.Size(18, 74);
            this._rulerControl.TabIndex = 1;
            this._rulerControl._vertical = true;
            // 
            // _drawPanel
            // 
            this._drawPanel.Controls.Add(this._draw);
            this._drawPanel.Location = new System.Drawing.Point(24, 28);
            this._drawPanel.Name = "_drawPanel";
            this._drawPanel.Size = new System.Drawing.Size(384, 71);
            this._drawPanel.TabIndex = 3;
            // 
            // _draw
            // 
            this._draw._drawNetRectangle = false;
            this._draw._netRectangle = new System.Drawing.Rectangle(0, 0, 0, 0);
            this._draw._activeTool = SMLReport._design._drawToolType.Pointer;
            this._draw.BackColor = System.Drawing.Color.White;
            this._draw._drawScale = 1F;
            this._draw.Location = new System.Drawing.Point(3, 3);
            this._draw.Name = "_draw";
            this._draw.Size = new System.Drawing.Size(360, 65);
            this._draw.TabIndex = 0;
            // 
            // _sectionObject
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Controls.Add(this._drawPanel);
            this.Controls.Add(this._rulerControl);
            this.Controls.Add(this._toolBar);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "_sectionObject";
            this.Size = new System.Drawing.Size(424, 99);
            this._toolBar.ResumeLayout(false);
            this._toolBar.PerformLayout();
            this._drawPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip _toolBar;
        private System.Windows.Forms.ToolStripLabel _label;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private SMLReport._design._rulerControl _rulerControl;
        private System.Windows.Forms.Panel _drawPanel;
        public SMLReport._design._drawPanel _draw;
    }
}
