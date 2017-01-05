namespace SMLInventoryControl._icPurchasePrice
{
    partial class _icPriceList
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
            this._myManageDetail = new MyLib._myManageData();
            this._myPanel1 = new MyLib._myPanel();
            this._myTabControlDetail = new MyLib._myTabControl();
            this.tab_normal_price = new System.Windows.Forms.TabPage();
            this._normalPrice = new SMLInventoryControl._icPurchasePrice._icPriceDetail();
            this.tab_ap_price = new System.Windows.Forms.TabPage();
            this._apPrice = new SMLInventoryControl._icPurchasePrice._icPriceDetail();
            this._icmainScreenTop = new SMLERPControl._icmainScreenTopControl();
            this._myToolBar = new System.Windows.Forms.ToolStrip();
            this._saveButton = new MyLib.ToolStripMyButton();
            this._myManageDetail._form2.SuspendLayout();
            this._myManageDetail.SuspendLayout();
            this._myPanel1.SuspendLayout();
            this._myTabControlDetail.SuspendLayout();
            this.tab_normal_price.SuspendLayout();
            this.tab_ap_price.SuspendLayout();
            this._myToolBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // _myManageDetail
            // 
            this._myManageDetail._mainMenuCode = "";
            this._myManageDetail._mainMenuId = "";
            this._myManageDetail.BackColor = System.Drawing.Color.WhiteSmoke;
            this._myManageDetail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._myManageDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myManageDetail.Location = new System.Drawing.Point(0, 0);
            this._myManageDetail.Margin = new System.Windows.Forms.Padding(0);
            this._myManageDetail.Name = "_myManageDetail";
            // 
            // _myManageDetail.Panel1
            // 
            this._myManageDetail._form1.BackColor = System.Drawing.Color.WhiteSmoke;
            // 
            // _myManageDetail.Panel2
            // 
            this._myManageDetail._form2.BackColor = System.Drawing.Color.WhiteSmoke;
            this._myManageDetail._form2.Controls.Add(this._myPanel1);
            this._myManageDetail._form2.Controls.Add(this._myToolBar);
            this._myManageDetail.Size = new System.Drawing.Size(1114, 742);
            this._myManageDetail.TabIndex = 1;
            this._myManageDetail.TabStop = false;
            // 
            // _myPanel1
            // 
            this._myPanel1._switchTabAuto = false;
            this._myPanel1.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Controls.Add(this._myTabControlDetail);
            this._myPanel1.Controls.Add(this._icmainScreenTop);
            this._myPanel1.CornerPicture = null;
            this._myPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myPanel1.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Location = new System.Drawing.Point(0, 25);
            this._myPanel1.Margin = new System.Windows.Forms.Padding(0);
            this._myPanel1.Name = "_myPanel1";
            this._myPanel1.Size = new System.Drawing.Size(856, 715);
            this._myPanel1.TabIndex = 1;
            // 
            // _myTabControlDetail
            // 
            this._myTabControlDetail.Controls.Add(this.tab_normal_price);
            this._myTabControlDetail.Controls.Add(this.tab_ap_price);
            this._myTabControlDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myTabControlDetail.Location = new System.Drawing.Point(0, 233);
            this._myTabControlDetail.Multiline = true;
            this._myTabControlDetail.Name = "_myTabControlDetail";
            this._myTabControlDetail.SelectedIndex = 0;
            this._myTabControlDetail.ShowTabNumber = true;
            this._myTabControlDetail.Size = new System.Drawing.Size(856, 482);
            this._myTabControlDetail.TabIndex = 1;
            this._myTabControlDetail.TableName = "";
            // 
            // tab_normal_price
            // 
            this.tab_normal_price.Controls.Add(this._normalPrice);
            this.tab_normal_price.Location = new System.Drawing.Point(4, 23);
            this.tab_normal_price.Name = "tab_normal_price";
            this.tab_normal_price.Size = new System.Drawing.Size(848, 455);
            this.tab_normal_price.TabIndex = 0;
            this.tab_normal_price.Text = "1.tab_normal_price";
            this.tab_normal_price.UseVisualStyleBackColor = true;
            // 
            // _normalPrice
            // 
            this._normalPrice._priceListType = _g.g._priceListType.ซื้อ_ราคาซื้อทั่วไป;
            this._normalPrice._priceType = _g.g._priceGridType.ราคาปรกติ;
            this._normalPrice.Dock = System.Windows.Forms.DockStyle.Fill;
            this._normalPrice.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._normalPrice.Location = new System.Drawing.Point(0, 0);
            this._normalPrice.Name = "_normalPrice";
            this._normalPrice.Size = new System.Drawing.Size(848, 455);
            this._normalPrice.TabIndex = 0;
            // 
            // tab_ap_price
            // 
            this.tab_ap_price.Controls.Add(this._apPrice);
            this.tab_ap_price.Location = new System.Drawing.Point(4, 23);
            this.tab_ap_price.Name = "tab_ap_price";
            this.tab_ap_price.Size = new System.Drawing.Size(848, 455);
            this.tab_ap_price.TabIndex = 2;
            this.tab_ap_price.Text = "2.tab_ap_price";
            this.tab_ap_price.UseVisualStyleBackColor = true;
            // 
            // _apPrice
            // 
            this._apPrice._priceListType = _g.g._priceListType.ซื้อ_ราคาซื้อตามเจ้าหนี้;
            this._apPrice._priceType = _g.g._priceGridType.ราคาตามเจ้าหนี้;
            this._apPrice.Dock = System.Windows.Forms.DockStyle.Fill;
            this._apPrice.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._apPrice.Location = new System.Drawing.Point(0, 0);
            this._apPrice.Name = "_apPrice";
            this._apPrice.Size = new System.Drawing.Size(848, 455);
            this._apPrice.TabIndex = 1;
            // 
            // _icmainScreenTop
            // 
            this._icmainScreenTop._isChange = false;
            this._icmainScreenTop.AutoSize = true;
            this._icmainScreenTop.BackColor = System.Drawing.Color.Transparent;
            this._icmainScreenTop.DisplayScreen = false;
            this._icmainScreenTop.Dock = System.Windows.Forms.DockStyle.Top;
            this._icmainScreenTop.Location = new System.Drawing.Point(0, 0);
            this._icmainScreenTop.Name = "_icmainScreenTop";
            this._icmainScreenTop.Size = new System.Drawing.Size(856, 233);
            this._icmainScreenTop.TabIndex = 0;
            // 
            // _myToolBar
            // 
            this._myToolBar.BackgroundImage = global::SMLInventoryControl.Properties.Resources.bt03;
            this._myToolBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._saveButton});
            this._myToolBar.Location = new System.Drawing.Point(0, 0);
            this._myToolBar.Name = "_myToolBar";
            this._myToolBar.Size = new System.Drawing.Size(856, 25);
            this._myToolBar.TabIndex = 0;
            this._myToolBar.Text = "toolStrip1";
            // 
            // _saveButton
            // 
            this._saveButton.Image = global::SMLInventoryControl.Properties.Resources.disk_blue;
            this._saveButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._saveButton.Name = "_saveButton";
            this._saveButton.Padding = new System.Windows.Forms.Padding(1);
            this._saveButton.ResourceName = "บันทึกข้อมูล (F12)";
            this._saveButton.Size = new System.Drawing.Size(113, 22);
            this._saveButton.Text = "บันทึกข้อมูล (F12)";
            // 
            // _icPriceList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._myManageDetail);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "_icPriceList";
            this.Size = new System.Drawing.Size(1114, 742);
            this._myManageDetail._form2.ResumeLayout(false);
            this._myManageDetail._form2.PerformLayout();
            this._myManageDetail.ResumeLayout(false);
            this._myPanel1.ResumeLayout(false);
            this._myPanel1.PerformLayout();
            this._myTabControlDetail.ResumeLayout(false);
            this.tab_normal_price.ResumeLayout(false);
            this.tab_ap_price.ResumeLayout(false);
            this._myToolBar.ResumeLayout(false);
            this._myToolBar.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage tab_normal_price;
        private SMLERPControl._icmainScreenTopControl _icmainScreenTop;
        private System.Windows.Forms.ToolStrip _myToolBar;
        private MyLib.ToolStripMyButton _saveButton;
        private MyLib._myTabControl _myTabControlDetail;
        private System.Windows.Forms.TabPage tab_ap_price;
        private MyLib._myManageData _myManageDetail;
        private MyLib._myPanel _myPanel1;
        private _icPriceDetail _normalPrice;
        private _icPriceDetail _apPrice;
    }
}
