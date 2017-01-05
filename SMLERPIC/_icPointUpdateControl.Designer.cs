namespace SMLERPIC
{
    partial class _icPointUpdateControl
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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this._checkAllButton = new System.Windows.Forms.ToolStripButton();
            this._removeAllButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this._applyButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this._closeButton = new System.Windows.Forms.ToolStripButton();
            this._dataList = new MyLib._myDataList();
            this.toolStripComboBox1 = new System.Windows.Forms.ToolStripComboBox();
            this._pointComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._pointComboBox,
            this.toolStripSeparator1,
            this._checkAllButton,
            this._removeAllButton,
            this.toolStripSeparator3,
            this._applyButton,
            this.toolStripSeparator2,
            this._closeButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(804, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // _checkAllButton
            // 
            this._checkAllButton.Image = global::SMLERPIC.Properties.Resources.check;
            this._checkAllButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._checkAllButton.Name = "_checkAllButton";
            this._checkAllButton.Size = new System.Drawing.Size(77, 22);
            this._checkAllButton.Text = "Check All";
            this._checkAllButton.Click += new System.EventHandler(this._checkAllButton_Click);
            // 
            // _removeAllButton
            // 
            this._removeAllButton.Image = global::SMLERPIC.Properties.Resources.delete;
            this._removeAllButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._removeAllButton.Name = "_removeAllButton";
            this._removeAllButton.Size = new System.Drawing.Size(87, 22);
            this._removeAllButton.Text = "Remove All";
            this._removeAllButton.Click += new System.EventHandler(this._removeAllButton_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // _applyButton
            // 
            this._applyButton.Image = global::SMLERPIC.Properties.Resources.disk_blue;
            this._applyButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._applyButton.Name = "_applyButton";
            this._applyButton.Size = new System.Drawing.Size(58, 22);
            this._applyButton.Text = "Apply";
            this._applyButton.Click += new System.EventHandler(this.ToolStripButton_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // _closeButton
            // 
            this._closeButton.Image = global::SMLERPIC.Properties.Resources.error1;
            this._closeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._closeButton.Name = "_closeButton";
            this._closeButton.Size = new System.Drawing.Size(56, 22);
            this._closeButton.Text = "Close";
            this._closeButton.Click += new System.EventHandler(this._closeButton_Click);
            // 
            // _dataList
            // 
            this._dataList._extraWhere = "";
            this._dataList._fullMode = false;
            this._dataList._multiSelect = false;
            this._dataList.BackColor = System.Drawing.Color.WhiteSmoke;
            this._dataList.Dock = System.Windows.Forms.DockStyle.Fill;
            this._dataList.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._dataList.Location = new System.Drawing.Point(0, 25);
            this._dataList.Margin = new System.Windows.Forms.Padding(0);
            this._dataList.Name = "_dataList";
            this._dataList.Size = new System.Drawing.Size(804, 588);
            this._dataList.TabIndex = 1;
            this._dataList.TabStop = false;
            // 
            // toolStripComboBox1
            // 
            this.toolStripComboBox1.Name = "toolStripComboBox1";
            this.toolStripComboBox1.Size = new System.Drawing.Size(121, 23);
            // 
            // _pointComboBox
            // 
            this._pointComboBox.Items.AddRange(new object[] {
            "All",
            "Point only",
            "No Point only"});
            this._pointComboBox.Name = "_pointComboBox";
            this._pointComboBox.Size = new System.Drawing.Size(121, 25);
            // 
            // _icPointUpdateControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._dataList);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "_icPointUpdateControl";
            this.Size = new System.Drawing.Size(804, 613);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton _closeButton;
        private MyLib._myDataList _dataList;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton _applyButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton _checkAllButton;
        private System.Windows.Forms.ToolStripButton _removeAllButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripComboBox _pointComboBox;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBox1;
    }
}
