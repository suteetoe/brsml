namespace SMLTransferDataPOS
{
    partial class _mainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(_mainForm));
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("กำหนดค่าเครื่องแม่ข่าย");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("กำหนดค่าเริ่มต้น", new System.Windows.Forms.TreeNode[] {
            treeNode1});
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("รายการขายประจำวัน");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("รายการเบิกสินค้า");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("รายการปรับปรุงสินค้า");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("รายการโอนสินค้าระหว่างสาขา");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("ข้อมูลประจำวัน", new System.Windows.Forms.TreeNode[] {
            treeNode3,
            treeNode4,
            treeNode5,
            treeNode6});
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("ปรับปรุงข้อมูลหลัก");
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("ข้อมูลหลัก", new System.Windows.Forms.TreeNode[] {
            treeNode8});
            this._menuTransfer = new MyLib._myTreeView();
            this._menuPanel.SuspendLayout();
            this._tabControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // _menuPanel
            // 
            this._menuPanel.Controls.Add(this._menuTransfer);
            // 
            // Home
            // 
            this.Home.Size = new System.Drawing.Size(595, 489);
            // 
            // _imageList16x16
            // 
            this._imageList16x16.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("_imageList16x16.ImageStream")));
            this._imageList16x16.Images.SetKeyName(0, "");
            this._imageList16x16.Images.SetKeyName(1, "");
            this._imageList16x16.Images.SetKeyName(2, "media_play_green.png");
            this._imageList16x16.Images.SetKeyName(3, "media_stop_red.png");
            // 
            // _menuTransfer
            // 
            this._menuTransfer.Dock = System.Windows.Forms.DockStyle.Fill;
            this._menuTransfer.Location = new System.Drawing.Point(4, 4);
            this._menuTransfer.Name = "_menuTransfer";
            treeNode1.Name = "menu_server_config";
            treeNode1.Text = "กำหนดค่าเครื่องแม่ข่าย";
            treeNode2.Name = "menu_config";
            treeNode2.Text = "กำหนดค่าเริ่มต้น";
            treeNode3.Name = "menu_sale_transection";
            treeNode3.Text = "รายการขายประจำวัน";
            treeNode4.Name = "menu_ic_wd_transfer";
            treeNode4.Tag = "&earth&";
            treeNode4.Text = "รายการเบิกสินค้า";
            treeNode5.Name = "menu_ic_adjust_transfer";
            treeNode5.Tag = "&earth&";
            treeNode5.Text = "รายการปรับปรุงสินค้า";
            treeNode6.Name = "menu_ic_transfer_branch";
            treeNode6.Tag = "&earth&";
            treeNode6.Text = "รายการโอนสินค้าระหว่างสาขา";
            treeNode7.Name = "menu_transection";
            treeNode7.Text = "ข้อมูลประจำวัน";
            treeNode8.Name = "menu_ic_transfer";
            treeNode8.Text = "ปรับปรุงข้อมูลหลัก";
            treeNode9.Name = "menu_ic_inventory_control";
            treeNode9.Text = "ข้อมูลหลัก";
            this._menuTransfer.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode2,
            treeNode7,
            treeNode9});
            this._menuTransfer.Size = new System.Drawing.Size(314, 558);
            this._menuTransfer.TabIndex = 0;
            // 
            // _mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(927, 568);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "_mainForm";
            this.Text = "_mainForm";
            this._menuPanel.ResumeLayout(false);
            this._tabControl.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MyLib._myTreeView _menuTransfer;
    }
}