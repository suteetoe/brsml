namespace SMLICIAdmin
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(_mainForm));
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("กำหนดรูปแบบการค้นหาข้อมูล");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("ตรวจสอบโครงสร้างฐานข้อมูล");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("ลดขนาดข้อมูล");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("สำรองข้อมูล");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("นำข้อมูลสำรองกลับมาใช้");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("เปลี่ยนรหัสผ่าน");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("กำหนดผู้มีสิทธิเข้าใช้ข้อมูล");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("กำหนดสิทธิกลุ่มการใช้งาน");
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("แสดงโครงสร้างฐานข้อมูลทั้งหมด");
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("ใช้คำสั่ง Query");
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("นำเข้าข้อมูลหลัก");
            System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("นำเข้าข้อมูลสูตรสี");
            System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("ตรวจสอบนำเข้าข้อมูลสูตรสี");
            System.Windows.Forms.TreeNode treeNode14 = new System.Windows.Forms.TreeNode("เครื่องมือ", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3,
            treeNode4,
            treeNode5,
            treeNode6,
            treeNode7,
            treeNode8,
            treeNode9,
            treeNode10,
            treeNode11,
            treeNode12,
            treeNode13});
            this._menuImage = new System.Windows.Forms.ImageList(this.components);
            this._mainMenuERP = new MyLib._myTreeView();
            this._menuPanel.SuspendLayout();
            this.Home.SuspendLayout();
            this.SuspendLayout();
            // 
            // _menuPanel
            // 
            this._menuPanel.Controls.Add(this._mainMenuERP);
            this._menuPanel.Size = new System.Drawing.Size(324, 502);
            // 
            // Home
            // 
            this.Home.Size = new System.Drawing.Size(833, 510);
            // 
            // _imageList16x16
            // 
            this._imageList16x16.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("_imageList16x16.ImageStream")));
            this._imageList16x16.Images.SetKeyName(0, "");
            this._imageList16x16.Images.SetKeyName(1, "");
            this._imageList16x16.Images.SetKeyName(2, "media_play_green.png");
            this._imageList16x16.Images.SetKeyName(3, "media_stop_red.png");
            // 
            // _menuImage
            // 
            this._menuImage.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("_menuImage.ImageStream")));
            this._menuImage.TransparentColor = System.Drawing.Color.Transparent;
            this._menuImage.Images.SetKeyName(0, "element.png");
            this._menuImage.Images.SetKeyName(1, "table_sql_view.png");
            this._menuImage.Images.SetKeyName(2, "box_out.png");
            this._menuImage.Images.SetKeyName(3, "box_add.png");
            this._menuImage.Images.SetKeyName(4, "user1.png");
            this._menuImage.Images.SetKeyName(5, "businessman.png");
            this._menuImage.Images.SetKeyName(6, "money.png");
            this._menuImage.Images.SetKeyName(7, "box_closed.png");
            this._menuImage.Images.SetKeyName(8, "shoppingcart_full.png");
            this._menuImage.Images.SetKeyName(9, "telephone.png");
            this._menuImage.Images.SetKeyName(10, "clients.png");
            this._menuImage.Images.SetKeyName(11, "book_blue.png");
            this._menuImage.Images.SetKeyName(12, "calendar_up.png");
            this._menuImage.Images.SetKeyName(13, "wrench.png");
            this._menuImage.Images.SetKeyName(14, "preferences.png");
            // 
            // _mainMenuERP
            // 
            this._mainMenuERP.BackColor = System.Drawing.Color.White;
            this._mainMenuERP.Dock = System.Windows.Forms.DockStyle.Fill;
            this._mainMenuERP.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._mainMenuERP.ImageIndex = 0;
            this._mainMenuERP.ImageList = this._menuImage;
            this._mainMenuERP.Location = new System.Drawing.Point(4, 4);
            this._mainMenuERP.Margin = new System.Windows.Forms.Padding(0);
            this._mainMenuERP.Name = "_mainMenuERP";
            treeNode1.Name = "menu_view_manager";
            treeNode1.Tag = "&1&&2&&3&";
            treeNode1.Text = "กำหนดรูปแบบการค้นหาข้อมูล";
            treeNode2.ImageKey = "data_ok.png";
            treeNode2.Name = "menu_verify_database";
            treeNode2.SelectedImageKey = "data_ok.png";
            treeNode2.Tag = "&1&&2&&3&";
            treeNode2.Text = "ตรวจสอบโครงสร้างฐานข้อมูล";
            treeNode3.ImageKey = "data_replace.png";
            treeNode3.Name = "menu_shink_database";
            treeNode3.SelectedImageKey = "data_replace.png";
            treeNode3.Tag = "&1&&2&&3&";
            treeNode3.Text = "ลดขนาดข้อมูล";
            treeNode4.ImageKey = "data_out.png";
            treeNode4.Name = "Node6";
            treeNode4.SelectedImageKey = "data_out.png";
            treeNode4.Tag = "&1&&2&&3&";
            treeNode4.Text = "สำรองข้อมูล";
            treeNode5.ImageKey = "data_into.png";
            treeNode5.Name = "Node7";
            treeNode5.SelectedImageKey = "data_into.png";
            treeNode5.Tag = "&1&&2&&3&";
            treeNode5.Text = "นำข้อมูลสำรองกลับมาใช้";
            treeNode6.ImageKey = "key1.png";
            treeNode6.Name = "menu_change_password";
            treeNode6.SelectedImageKey = "key1.png";
            treeNode6.Tag = "&1&&2&&3&";
            treeNode6.Text = "เปลี่ยนรหัสผ่าน";
            treeNode7.ImageKey = "user1.png";
            treeNode7.Name = "menu_permissions_user";
            treeNode7.SelectedImageKey = "user1.png";
            treeNode7.Tag = "&1&&2&&3&";
            treeNode7.Text = "กำหนดผู้มีสิทธิเข้าใช้ข้อมูล";
            treeNode8.ImageKey = "user1_preferences.png";
            treeNode8.Name = "menu_permissions_group";
            treeNode8.SelectedImageKey = "user1_preferences.png";
            treeNode8.Tag = "&1&&2&&3&";
            treeNode8.Text = "กำหนดสิทธิกลุ่มการใช้งาน";
            treeNode9.ImageKey = "data_table.png";
            treeNode9.Name = "menu_database_struct";
            treeNode9.SelectedImageKey = "data_table.png";
            treeNode9.Tag = "&1&&2&&3&";
            treeNode9.Text = "แสดงโครงสร้างฐานข้อมูลทั้งหมด";
            treeNode10.ImageKey = "data_view.png";
            treeNode10.Name = "menu_query";
            treeNode10.SelectedImageKey = "data_view.png";
            treeNode10.Tag = "&1&&2&&3&";
            treeNode10.Text = "ใช้คำสั่ง Query";
            treeNode11.Name = "menu_import_data_master";
            treeNode11.Text = "นำเข้าข้อมูลหลัก";
            treeNode12.Name = "menu_import_data_fomulcolor";
            treeNode12.Text = "นำเข้าข้อมูลสูตรสี";
            treeNode13.Name = "menu_import_data_fomulcolorscreenfile";
            treeNode13.Text = "ตรวจสอบนำเข้าข้อมูลสูตรสี";
            treeNode14.ImageKey = "wrench.png";
            treeNode14.Name = "menu_tools";
            treeNode14.SelectedImageKey = "wrench.png";
            treeNode14.Tag = "&1&&2&&3&";
            treeNode14.Text = "เครื่องมือ";
            this._mainMenuERP.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode14});
            this._mainMenuERP.SelectedImageIndex = 0;
            this._mainMenuERP.Size = new System.Drawing.Size(314, 492);
            this._mainMenuERP.TabIndex = 8;
            this._mainMenuERP.TabStop = false;
            // 
            // _mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(841, 607);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(0, 0);
            this.MinimumSize = new System.Drawing.Size(697, 643);
            this.Name = "_mainForm";
            this.Text = "SML ICI Admin";
            this._menuPanel.ResumeLayout(false);
            this.Home.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ImageList _menuImage;
        private MyLib._myTreeView _mainMenuERP;
    }
}

