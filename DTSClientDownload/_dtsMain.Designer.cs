namespace DTSClientDownload
{
    partial class _dtsMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(_dtsMain));
            this._mainTab = new System.Windows.Forms.TabControl();
            this._itemTab = new System.Windows.Forms.TabPage();
            this._poTab = new System.Windows.Forms.TabPage();
            this._soTab = new System.Windows.Forms.TabPage();
            this._logTab = new System.Windows.Forms.TabPage();
            this._mainTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // _mainTab
            // 
            this._mainTab.Controls.Add(this._itemTab);
            this._mainTab.Controls.Add(this._poTab);
            this._mainTab.Controls.Add(this._soTab);
            this._mainTab.Controls.Add(this._logTab);
            this._mainTab.Dock = System.Windows.Forms.DockStyle.Fill;
            this._mainTab.Location = new System.Drawing.Point(0, 0);
            this._mainTab.Name = "_mainTab";
            this._mainTab.SelectedIndex = 0;
            this._mainTab.Size = new System.Drawing.Size(1580, 837);
            this._mainTab.TabIndex = 0;
            // 
            // _itemTab
            // 
            this._itemTab.BackColor = System.Drawing.SystemColors.Control;
            this._itemTab.Location = new System.Drawing.Point(4, 23);
            this._itemTab.Name = "_itemTab";
            this._itemTab.Padding = new System.Windows.Forms.Padding(3);
            this._itemTab.Size = new System.Drawing.Size(1572, 810);
            this._itemTab.TabIndex = 0;
            this._itemTab.Text = "ข้อมูลสินค้า SCG";
            // 
            // _poTab
            // 
            this._poTab.BackColor = System.Drawing.SystemColors.Control;
            this._poTab.Location = new System.Drawing.Point(4, 23);
            this._poTab.Name = "_poTab";
            this._poTab.Padding = new System.Windows.Forms.Padding(3);
            this._poTab.Size = new System.Drawing.Size(1572, 810);
            this._poTab.TabIndex = 1;
            this._poTab.Text = "นำเข้าใบสั่งซื้อสินค้า eOrdering";
            // 
            // _soTab
            // 
            this._soTab.BackColor = System.Drawing.SystemColors.Control;
            this._soTab.Location = new System.Drawing.Point(4, 23);
            this._soTab.Name = "_soTab";
            this._soTab.Padding = new System.Windows.Forms.Padding(3);
            this._soTab.Size = new System.Drawing.Size(1572, 810);
            this._soTab.TabIndex = 2;
            this._soTab.Text = "นำเข้าใบสั่งขาย/สั่งจอง";
            // 
            // _logTab
            // 
            this._logTab.BackColor = System.Drawing.SystemColors.Control;
            this._logTab.Location = new System.Drawing.Point(4, 23);
            this._logTab.Name = "_logTab";
            this._logTab.Padding = new System.Windows.Forms.Padding(3);
            this._logTab.Size = new System.Drawing.Size(1572, 810);
            this._logTab.TabIndex = 3;
            this._logTab.Text = "ประวัติการรับข้อมูล";
            // 
            // _dtsMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1580, 837);
            this.Controls.Add(this._mainTab);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "_dtsMain";
            this.Text = "SCG Download";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this._mainTab.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl _mainTab;
        private System.Windows.Forms.TabPage _itemTab;
        private System.Windows.Forms.TabPage _poTab;
        private System.Windows.Forms.TabPage _soTab;
        private System.Windows.Forms.TabPage _logTab;
    }
}