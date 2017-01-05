namespace _g
{
    partial class _userGroupWarehouseShelf
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
            this._myToolbar = new System.Windows.Forms.ToolStrip();
            this._saveButton = new MyLib.ToolStripMyButton();
            this._closeButton = new MyLib.ToolStripMyButton();
            this._myPanel = new System.Windows.Forms.Panel();
            this._tabWarehouse = new MyLib._myTabControl();
            this.tab_purchase = new MyLib._myTabPage();
            this._puWarehouseLocation = new _g._warehouseLocationControl();
            this.tab_sale = new MyLib._myTabPage();
            this._siWarehouseLocation = new _g._warehouseLocationControl();
            this.tab_transfer = new MyLib._myTabPage();
            this._transferWarehouseLocation = new _g._warehouseLocationControl();
            this.tab_return = new MyLib._myTabPage();
            this._receiveWarehouseLocation = new _g._warehouseLocationControl();
            this._userGroupScreenTop1 = new _g._userGroupScreenTop();
            this._myManageData1 = new MyLib._myManageData();
            this._myToolbar.SuspendLayout();
            this._myPanel.SuspendLayout();
            this._tabWarehouse.SuspendLayout();
            this.tab_purchase.SuspendLayout();
            this.tab_sale.SuspendLayout();
            this.tab_transfer.SuspendLayout();
            this.tab_return.SuspendLayout();
            this.SuspendLayout();
            // 
            // _myToolbar
            // 
            this._myToolbar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._saveButton,
            this._closeButton});
            this._myToolbar.Location = new System.Drawing.Point(0, 0);
            this._myToolbar.Name = "_myToolbar";
            this._myToolbar.Size = new System.Drawing.Size(735, 25);
            this._myToolbar.TabIndex = 1;
            this._myToolbar.Text = "toolStrip1";
            // 
            // _saveButton
            // 
            this._saveButton.Image = global::_g.Properties.Resources.disk_blue;
            this._saveButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._saveButton.Name = "_saveButton";
            this._saveButton.Padding = new System.Windows.Forms.Padding(1);
            this._saveButton.ResourceName = "บันทึก";
            this._saveButton.Size = new System.Drawing.Size(57, 22);
            this._saveButton.Text = "บันทึก";
            this._saveButton.Click += new System.EventHandler(this._saveButton_Click);
            // 
            // _closeButton
            // 
            this._closeButton.Image = global::_g.Properties.Resources.error;
            this._closeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._closeButton.Name = "_closeButton";
            this._closeButton.Padding = new System.Windows.Forms.Padding(1);
            this._closeButton.ResourceName = "ปิดจอ";
            this._closeButton.Size = new System.Drawing.Size(55, 22);
            this._closeButton.Text = "ปิดจอ";
            this._closeButton.Click += new System.EventHandler(this._closeButton_Click);
            // 
            // _myPanel
            // 
            this._myPanel.Controls.Add(this._tabWarehouse);
            this._myPanel.Controls.Add(this._userGroupScreenTop1);
            this._myPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myPanel.Location = new System.Drawing.Point(0, 25);
            this._myPanel.Name = "_myPanel";
            this._myPanel.Size = new System.Drawing.Size(735, 670);
            this._myPanel.TabIndex = 2;
            // 
            // _tabWarehouse
            // 
            this._tabWarehouse.Controls.Add(this.tab_purchase);
            this._tabWarehouse.Controls.Add(this.tab_sale);
            this._tabWarehouse.Controls.Add(this.tab_transfer);
            this._tabWarehouse.Controls.Add(this.tab_return);
            this._tabWarehouse.Dock = System.Windows.Forms.DockStyle.Fill;
            this._tabWarehouse.Font = new System.Drawing.Font("Tahoma", 9F);
            this._tabWarehouse.Location = new System.Drawing.Point(0, 79);
            this._tabWarehouse.Multiline = true;
            this._tabWarehouse.Name = "_tabWarehouse";
            this._tabWarehouse.SelectedIndex = 0;
            this._tabWarehouse.Size = new System.Drawing.Size(735, 591);
            this._tabWarehouse.TabIndex = 3;
            this._tabWarehouse.TableName = "erp_user_group";
            // 
            // tab_purchase
            // 
            this.tab_purchase.Controls.Add(this._puWarehouseLocation);
            this.tab_purchase.Location = new System.Drawing.Point(4, 23);
            this.tab_purchase.Name = "tab_purchase";
            this.tab_purchase.Size = new System.Drawing.Size(727, 564);
            this.tab_purchase.TabIndex = 0;
            this.tab_purchase.Text = "tab_purchase";
            this.tab_purchase.UseVisualStyleBackColor = true;
            // 
            // _puWarehouseLocation
            // 
            this._puWarehouseLocation.Dock = System.Windows.Forms.DockStyle.Fill;
            this._puWarehouseLocation.Location = new System.Drawing.Point(0, 0);
            this._puWarehouseLocation.Name = "_puWarehouseLocation";
            this._puWarehouseLocation.Size = new System.Drawing.Size(727, 564);
            this._puWarehouseLocation.TabIndex = 0;
            // 
            // tab_sale
            // 
            this.tab_sale.Controls.Add(this._siWarehouseLocation);
            this.tab_sale.Location = new System.Drawing.Point(4, 23);
            this.tab_sale.Name = "tab_sale";
            this.tab_sale.Size = new System.Drawing.Size(727, 564);
            this.tab_sale.TabIndex = 1;
            this.tab_sale.Text = "tab_sale";
            this.tab_sale.UseVisualStyleBackColor = true;
            // 
            // _siWarehouseLocation
            // 
            this._siWarehouseLocation.Dock = System.Windows.Forms.DockStyle.Fill;
            this._siWarehouseLocation.Location = new System.Drawing.Point(0, 0);
            this._siWarehouseLocation.Name = "_siWarehouseLocation";
            this._siWarehouseLocation.Size = new System.Drawing.Size(727, 564);
            this._siWarehouseLocation.TabIndex = 0;
            // 
            // tab_transfer
            // 
            this.tab_transfer.Controls.Add(this._transferWarehouseLocation);
            this.tab_transfer.Location = new System.Drawing.Point(4, 23);
            this.tab_transfer.Name = "tab_transfer";
            this.tab_transfer.Size = new System.Drawing.Size(727, 564);
            this.tab_transfer.TabIndex = 2;
            this.tab_transfer.Text = "tab_transfer";
            this.tab_transfer.UseVisualStyleBackColor = true;
            // 
            // _transferWarehouseLocation
            // 
            this._transferWarehouseLocation.Dock = System.Windows.Forms.DockStyle.Fill;
            this._transferWarehouseLocation.Location = new System.Drawing.Point(0, 0);
            this._transferWarehouseLocation.Name = "_transferWarehouseLocation";
            this._transferWarehouseLocation.Size = new System.Drawing.Size(727, 564);
            this._transferWarehouseLocation.TabIndex = 0;
            // 
            // tab_return
            // 
            this.tab_return.Controls.Add(this._receiveWarehouseLocation);
            this.tab_return.Location = new System.Drawing.Point(4, 23);
            this.tab_return.Name = "tab_return";
            this.tab_return.Size = new System.Drawing.Size(727, 564);
            this.tab_return.TabIndex = 3;
            this.tab_return.Text = "tab_return";
            this.tab_return.UseVisualStyleBackColor = true;
            // 
            // _receiveWarehouseLocation
            // 
            this._receiveWarehouseLocation.Dock = System.Windows.Forms.DockStyle.Fill;
            this._receiveWarehouseLocation.Location = new System.Drawing.Point(0, 0);
            this._receiveWarehouseLocation.Name = "_receiveWarehouseLocation";
            this._receiveWarehouseLocation.Size = new System.Drawing.Size(727, 564);
            this._receiveWarehouseLocation.TabIndex = 0;
            // 
            // _userGroupScreenTop1
            // 
            this._userGroupScreenTop1._isChange = false;
            this._userGroupScreenTop1.BackColor = System.Drawing.Color.Transparent;
            this._userGroupScreenTop1.Dock = System.Windows.Forms.DockStyle.Top;
            this._userGroupScreenTop1.Location = new System.Drawing.Point(0, 0);
            this._userGroupScreenTop1.Name = "_userGroupScreenTop1";
            this._userGroupScreenTop1.Size = new System.Drawing.Size(735, 79);
            this._userGroupScreenTop1.TabIndex = 0;
            // 
            // _myManageData1
            // 
            this._myManageData1._mainMenuCode = "";
            this._myManageData1._mainMenuId = "";
            this._myManageData1.BackColor = System.Drawing.Color.WhiteSmoke;
            this._myManageData1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._myManageData1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myManageData1.Location = new System.Drawing.Point(0, 0);
            this._myManageData1.Name = "_myManageData1";
            this._myManageData1.Size = new System.Drawing.Size(820, 716);
            this._myManageData1.TabIndex = 2;
            this._myManageData1.TabStop = false;

            this._myManageData1._form2.Controls.Add(this._myPanel);
            this._myManageData1._form2.Controls.Add(this._myToolbar);

            // 
            // _userGroupWarehouseShelf
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._myManageData1);
            this.Name = "_userGroupWarehouseShelf";
            this.Size = new System.Drawing.Size(820, 716);
            this._myToolbar.ResumeLayout(false);
            this._myToolbar.PerformLayout();
            this._myPanel.ResumeLayout(false);
            this._tabWarehouse.ResumeLayout(false);
            this.tab_purchase.ResumeLayout(false);
            this.tab_sale.ResumeLayout(false);
            this.tab_transfer.ResumeLayout(false);
            this.tab_return.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStrip _myToolbar;
        private MyLib.ToolStripMyButton _saveButton;
        private MyLib.ToolStripMyButton _closeButton;
        private System.Windows.Forms.Panel _myPanel;
        //private System.Windows.Forms.SplitContainer splitContainer1;
        private MyLib._myManageData _myManageData1;
        private _userGroupScreenTop _userGroupScreenTop1;
        private MyLib._myTabControl _tabWarehouse;
        private MyLib._myTabPage tab_purchase;
        private _warehouseLocationControl _puWarehouseLocation;
        private MyLib._myTabPage tab_sale;
        private _warehouseLocationControl _siWarehouseLocation;
        private MyLib._myTabPage tab_transfer;
        private _warehouseLocationControl _transferWarehouseLocation;
        private MyLib._myTabPage tab_return;
        private _warehouseLocationControl _receiveWarehouseLocation;
    }
}
