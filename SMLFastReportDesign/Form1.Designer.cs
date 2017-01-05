namespace SMLFastReportDesign
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this._myDataList = new MyLib._myDataList();
            this._tabControl = new MyLib._myTabControl();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this._newButton = new System.Windows.Forms.ToolStripButton();
            this._generateXMLButton = new System.Windows.Forms.ToolStripButton();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 31);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.Color.Honeydew;
            this.splitContainer1.Panel1.Controls.Add(this._myDataList);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer1.Panel2.Controls.Add(this._tabControl);
            this.splitContainer1.Size = new System.Drawing.Size(859, 649);
            this.splitContainer1.SplitterDistance = 224;
            this.splitContainer1.TabIndex = 1;
            // 
            // _myDataList
            // 
            this._myDataList._extraWhere = "";
            this._myDataList._multiSelect = false;
            this._myDataList.BackColor = System.Drawing.Color.WhiteSmoke;
            this._myDataList.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myDataList.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._myDataList.Location = new System.Drawing.Point(0, 0);
            this._myDataList.Margin = new System.Windows.Forms.Padding(0);
            this._myDataList.Name = "_myDataList";
            this._myDataList.Size = new System.Drawing.Size(224, 649);
            this._myDataList.TabIndex = 0;
            this._myDataList.TabStop = false;
            // 
            // _tabControl
            // 
            this._tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this._tabControl.Location = new System.Drawing.Point(0, 0);
            this._tabControl.Multiline = true;
            this._tabControl.Name = "_tabControl";
            this._tabControl.SelectedIndex = 0;
            this._tabControl.Size = new System.Drawing.Size(631, 649);
            this._tabControl.TabIndex = 0;
            this._tabControl.TableName = "";
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackgroundImage = global::SMLFastReportDesign.Properties.Resources.bt03;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._newButton,
            this._generateXMLButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(859, 31);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // _newButton
            // 
            this._newButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._newButton.Image = global::SMLFastReportDesign.Properties.Resources.document_plain_new;
            this._newButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._newButton.Name = "_newButton";
            this._newButton.Size = new System.Drawing.Size(28, 28);
            this._newButton.Text = "toolStripButton1";
            this._newButton.Click += new System.EventHandler(this._newButton_Click);
            // 
            // _generateXMLButton
            // 
            this._generateXMLButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this._generateXMLButton.Image = global::SMLFastReportDesign.Properties.Resources.document_certificate;
            this._generateXMLButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._generateXMLButton.Name = "_generateXMLButton";
            this._generateXMLButton.Size = new System.Drawing.Size(28, 28);
            this._generateXMLButton.Text = "toolStripButton1";
            this._generateXMLButton.Click += new System.EventHandler(this._generateXMLButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(859, 680);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "SML Fast Report Designer";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton _newButton;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private MyLib._myDataList _myDataList;
        private MyLib._myTabControl _tabControl;
        private System.Windows.Forms.ToolStripButton _generateXMLButton;
    }
}

