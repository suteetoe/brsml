namespace SMLERPIC
{
    partial class _icMultiBarcodeDiscountControl
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
            this.panel1 = new System.Windows.Forms.Panel();
            this._selectedGid = new MyLib._myGrid();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this._tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.panel1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this._selectedGid);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(467, 263);
            this.panel1.TabIndex = 0;
            // 
            // _selectedGid
            // 
            this._selectedGid._extraWordShow = true;
            this._selectedGid._selectRow = -1;
            this._selectedGid.BackColor = System.Drawing.SystemColors.Window;
            this._selectedGid.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._selectedGid.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._selectedGid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._selectedGid.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._selectedGid.Location = new System.Drawing.Point(0, 0);
            this._selectedGid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._selectedGid.Name = "_selectedGid";
            this._selectedGid.Size = new System.Drawing.Size(467, 263);
            this._selectedGid.TabIndex = 0;
            this._selectedGid.TabStop = false;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this._tableLayoutPanel);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Size = new System.Drawing.Size(467, 491);
            this.splitContainer1.SplitterDistance = 224;
            this.splitContainer1.TabIndex = 0;
            // 
            // _tableLayoutPanel
            // 
            this._tableLayoutPanel.ColumnCount = 2;
            this._tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this._tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this._tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this._tableLayoutPanel.Name = "_tableLayoutPanel";
            this._tableLayoutPanel.RowCount = 3;
            this._tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this._tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this._tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this._tableLayoutPanel.Size = new System.Drawing.Size(467, 224);
            this._tableLayoutPanel.TabIndex = 5;
            // 
            // _icMultiBarcodeDiscountControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "_icMultiBarcodeDiscountControl";
            this.Size = new System.Drawing.Size(467, 491);
            this.panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        public System.Windows.Forms.TableLayoutPanel _tableLayoutPanel;
        public MyLib._myGrid _selectedGid;
    }
}
