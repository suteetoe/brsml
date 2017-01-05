namespace SMLActivesync
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("กำหนดค่า Data Center");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("กำหนดรายละเอียดสาขา");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("กำหนดตารางข้อมูล");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("กำหนดค่าเริ่มต้น", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3});
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("ควบคุมการส่งข้อมูล");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("ควบคุมการรับข้อมูล");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("ตรวจสอบสถานะ");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("งานประจำวัน", new System.Windows.Forms.TreeNode[] {
            treeNode5,
            treeNode6,
            treeNode7});
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("เปลี่ยนรหัสสินค้า");
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("เปลี่ยนรหัสลูกหนี้");
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("เปลี่ยนรหัสเจ้าหนี้");
            System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("เปลี่ยนรหัส", new System.Windows.Forms.TreeNode[] {
            treeNode9,
            treeNode10,
            treeNode11});
            this._mainMenuERP = new MyLib._myTreeView();
            this._menuPanel.SuspendLayout();
            this._tabControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // _menuPanel
            // 
            this._menuPanel.Controls.Add(this._mainMenuERP);
            this._menuPanel.Size = new System.Drawing.Size(324, 695);
            // 
            // Home
            // 
            this.Home.Size = new System.Drawing.Size(631, 616);
            // 
            // _imageList16x16
            // 
            this._imageList16x16.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("_imageList16x16.ImageStream")));
            this._imageList16x16.Images.SetKeyName(0, "");
            this._imageList16x16.Images.SetKeyName(1, "");
            this._imageList16x16.Images.SetKeyName(2, "media_play_green.png");
            this._imageList16x16.Images.SetKeyName(3, "media_stop_red.png");
            // 
            // _tabControl
            // 
            this._tabControl.Size = new System.Drawing.Size(639, 644);
            // 
            // _mainMenuERP
            // 
            this._mainMenuERP.BackColor = System.Drawing.Color.White;
            this._mainMenuERP.Dock = System.Windows.Forms.DockStyle.Fill;
            this._mainMenuERP.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._mainMenuERP.Location = new System.Drawing.Point(4, 4);
            this._mainMenuERP.Margin = new System.Windows.Forms.Padding(0);
            this._mainMenuERP.Name = "_mainMenuERP";
            treeNode1.Name = "menu_datacenter_config";
            treeNode1.Text = "กำหนดค่า Data Center";
            treeNode2.Name = "menu_datacenter_branch";
            treeNode2.Text = "กำหนดรายละเอียดสาขา";
            treeNode3.Name = "menu_datacenter_table";
            treeNode3.Text = "กำหนดตารางข้อมูล";
            treeNode4.ImageKey = "preferences.png";
            treeNode4.Name = "menu_setup_config";
            treeNode4.SelectedImageKey = "preferences.png";
            treeNode4.Tag = "&1&&2&&3&";
            treeNode4.Text = "กำหนดค่าเริ่มต้น";
            treeNode5.Name = "menu_datacenter_send_control";
            treeNode5.Text = "ควบคุมการส่งข้อมูล";
            treeNode6.Name = "menu_datacenter_receive_control";
            treeNode6.Text = "ควบคุมการรับข้อมูล";
            treeNode7.Name = "menu_datacenter_monitor";
            treeNode7.Text = "ตรวจสอบสถานะ";
            treeNode8.Name = "menu_datacenter_daily";
            treeNode8.Text = "งานประจำวัน";
            treeNode9.Name = "menu_change_item_code_master";
            treeNode9.Text = "เปลี่ยนรหัสสินค้า";
            treeNode10.Name = "menu_change_customer_code_master";
            treeNode10.Text = "เปลี่ยนรหัสลูกหนี้";
            treeNode11.Name = "menu_change_supplier_code_master";
            treeNode11.Text = "เปลี่ยนรหัสเจ้าหนี้";
            treeNode12.Name = "menu_change_mastercode";
            treeNode12.Text = "เปลี่ยนรหัส";
            this._mainMenuERP.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode4,
            treeNode8,
            treeNode12});
            this._mainMenuERP.Size = new System.Drawing.Size(314, 685);
            this._mainMenuERP.TabIndex = 4;
            this._mainMenuERP.TabStop = false;
            this._mainMenuERP.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this._mainMenuERP_AfterSelect);
            // 
            // _mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(963, 695);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(0, 0);
            this.MinimumSize = new System.Drawing.Size(697, 643);
            this.Name = "_mainForm";
            this.Text = "Form1";
            this._menuPanel.ResumeLayout(false);
            this._tabControl.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ImageList _menuImage;
        private MyLib._myTreeView _mainMenuERP;
    }
}

