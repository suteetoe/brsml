namespace SMLHealthy
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("กำหนดค่ามาตรฐานสุขภาพ");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("รายละเอียดข้อมูลทั่วไป", new System.Windows.Forms.TreeNode[] {
            treeNode1});
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("กลุ่มยาตาม MIM");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("ช่วงเวลาการใช้ยา");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("ข้อกำหนดเวลาใช้ยา");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("ขนาดและวิธีใช้ยา");
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("คำเตือนการใช้ยา");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("สรรพคุณยา");
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("ชนิดโรค");
            System.Windows.Forms.TreeNode treeNode10 = new System.Windows.Forms.TreeNode("ตารางความเสี่ยงกระดูกพรุน");
            System.Windows.Forms.TreeNode treeNode11 = new System.Windows.Forms.TreeNode("ดัชนีมวลกาย");
            System.Windows.Forms.TreeNode treeNode12 = new System.Windows.Forms.TreeNode("ระบบสินค้าคงคลัง(ยา/อาหารสุขภาพ)", new System.Windows.Forms.TreeNode[] {
            treeNode3,
            treeNode4,
            treeNode5,
            treeNode6,
            treeNode7,
            treeNode8,
            treeNode9,
            treeNode10,
            treeNode11});
            System.Windows.Forms.TreeNode treeNode13 = new System.Windows.Forms.TreeNode("สัญชาติ");
            System.Windows.Forms.TreeNode treeNode14 = new System.Windows.Forms.TreeNode("ลูกหนี้/ลูกค้า/สมาชิก ", new System.Windows.Forms.TreeNode[] {
            treeNode13});
            System.Windows.Forms.TreeNode treeNode15 = new System.Windows.Forms.TreeNode("ค่าเริ่มต้น", new System.Windows.Forms.TreeNode[] {
            treeNode2,
            treeNode12,
            treeNode14});
            System.Windows.Forms.TreeNode treeNode16 = new System.Windows.Forms.TreeNode("รายละเอียดยาและอาหารสุขภาพ");
            System.Windows.Forms.TreeNode treeNode17 = new System.Windows.Forms.TreeNode("สินค้า(ยา/อาหารสุขภาพ)", new System.Windows.Forms.TreeNode[] {
            treeNode16});
            System.Windows.Forms.TreeNode treeNode18 = new System.Windows.Forms.TreeNode("รายละเอียดสมาชิก");
            System.Windows.Forms.TreeNode treeNode19 = new System.Windows.Forms.TreeNode("ข้อมูลสุขภาพ");
            System.Windows.Forms.TreeNode treeNode20 = new System.Windows.Forms.TreeNode("ประวัติคำปรึกษาสุขภาพ");
            System.Windows.Forms.TreeNode treeNode21 = new System.Windows.Forms.TreeNode("ประวัติการให้คำปรึกษาการใช้ยา");
            System.Windows.Forms.TreeNode treeNode22 = new System.Windows.Forms.TreeNode("ลูกหนี้/ลูกค้า/สมาชิก", new System.Windows.Forms.TreeNode[] {
            treeNode18,
            treeNode19,
            treeNode20,
            treeNode21});
            this._mainMenuSMLHealthy = new MyLib._myTreeView();
            this._menuPanel.SuspendLayout();
            this._tabControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // _menuPanel
            // 
            this._menuPanel.Controls.Add(this._mainMenuSMLHealthy);
            this._menuPanel.Size = new System.Drawing.Size(324, 562);
            // 
            // Home
            // 
            this.Home.Size = new System.Drawing.Size(432, 465);
            // 
            // _imageList16x16
            // 
            this._imageList16x16.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("_imageList16x16.ImageStream")));
            this._imageList16x16.Images.SetKeyName(0, "");
            this._imageList16x16.Images.SetKeyName(1, "");
            this._imageList16x16.Images.SetKeyName(2, "media_play_green.png");
            this._imageList16x16.Images.SetKeyName(3, "media_stop_red.png");
            // 
            // _outLook
            // 
            this._outLook.Location = new System.Drawing.Point(150, 150);
            this._outLook.Load += new System.EventHandler(this._outLook_Load);
            // 
            // _mainMenuSMLHealthy
            // 
            this._mainMenuSMLHealthy.BackColor = System.Drawing.Color.White;
            this._mainMenuSMLHealthy.Dock = System.Windows.Forms.DockStyle.Fill;
            this._mainMenuSMLHealthy.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._mainMenuSMLHealthy.Location = new System.Drawing.Point(4, 4);
            this._mainMenuSMLHealthy.Margin = new System.Windows.Forms.Padding(0);
            this._mainMenuSMLHealthy.Name = "_mainMenuSMLHealthy";
            treeNode1.ImageKey = "book_blue.png";
            treeNode1.Name = "menu_healthy_m_healty_standard";
            treeNode1.SelectedImageKey = "book_blue.png";
            treeNode1.Tag = "&healthyconfig&";
            treeNode1.Text = "กำหนดค่ามาตรฐานสุขภาพ";
            treeNode2.ImageKey = "table_sql_view.png";
            treeNode2.Name = "menu_setup_healthy_general";
            treeNode2.SelectedImageKey = "table_sql_view.png";
            treeNode2.Tag = "&line&&1&&2&&3&";
            treeNode2.Text = "รายละเอียดข้อมูลทั่วไป";
            treeNode3.Name = "menu_setup_ic_healthy_m_mim_group";
            treeNode3.Tag = "&healthyconfig&";
            treeNode3.Text = "กลุ่มยาตาม MIM";
            treeNode4.Name = "menu_setup_ic_healthy_m_times";
            treeNode4.Tag = "&healthyconfig&";
            treeNode4.Text = "ช่วงเวลาการใช้ยา";
            treeNode5.Name = "menu_setup_ic_healthy_m_frequency";
            treeNode5.Tag = "&healthyconfig&";
            treeNode5.Text = "ข้อกำหนดเวลาใช้ยา";
            treeNode6.Name = "menu_setup_ic_healthy_m_dozen";
            treeNode6.Tag = "&healthyconfig&";
            treeNode6.Text = "ขนาดและวิธีใช้ยา";
            treeNode7.Name = "menu_setup_ic_healthy_m_warning";
            treeNode7.Tag = "&healthyconfig&";
            treeNode7.Text = "คำเตือนการใช้ยา";
            treeNode8.Name = "menu_setup_ic_healthy_m_properties";
            treeNode8.Tag = "&healthyconfig&";
            treeNode8.Text = "สรรพคุณยา";
            treeNode9.Name = "menu_setup_ic_healthy_m_disease";
            treeNode9.Tag = "&healthyconfig&";
            treeNode9.Text = "ชนิดโรค";
            treeNode10.Name = "menu_setup_ic_healthy_m_kkos";
            treeNode10.Tag = "&healthyconfig&";
            treeNode10.Text = "ตารางความเสี่ยงกระดูกพรุน";
            treeNode11.Name = "menu_setup_ic_healthy_m_bodymassindex";
            treeNode11.Tag = "&healthyconfig&";
            treeNode11.Text = "ดัชนีมวลกาย";
            treeNode12.ImageKey = "box_closed.png";
            treeNode12.Name = "menu_setup_ic_healthy";
            treeNode12.SelectedImageKey = "box_closed.png";
            treeNode12.Text = "ระบบสินค้าคงคลัง(ยา/อาหารสุขภาพ)";
            treeNode13.Name = "menu_setup_healthy_m_nationality";
            treeNode13.Tag = "&healthyconfig&";
            treeNode13.Text = "สัญชาติ";
            treeNode14.Name = "Node1111";
            treeNode14.Text = "ลูกหนี้/ลูกค้า/สมาชิก ";
            treeNode15.ImageKey = "preferences.png";
            treeNode15.Name = "menu_setup_healthy";
            treeNode15.SelectedImageKey = "preferences.png";
            treeNode15.Tag = "&1&&2&&3&";
            treeNode15.Text = "ค่าเริ่มต้น";
            treeNode16.Name = "menu_healthy_m_information";
            treeNode16.Tag = "&ar_healthy&";
            treeNode16.Text = "รายละเอียดยาและอาหารสุขภาพ";
            treeNode17.ImageKey = "box_closed.png";
            treeNode17.Name = "menu_ic_Healthy";
            treeNode17.SelectedImageKey = "box_closed.png";
            treeNode17.Tag = "&1&&2&&3&";
            treeNode17.Text = "สินค้า(ยา/อาหารสุขภาพ)";
            treeNode18.Name = "menu_ar_detail_healthy";
            treeNode18.Tag = "&ar_healthy&";
            treeNode18.Text = "รายละเอียดสมาชิก";
            treeNode19.Name = "menu_cust_healthy_m_yourhealthy";
            treeNode19.Tag = "&ar_healthy&";
            treeNode19.Text = "ข้อมูลสุขภาพ";
            treeNode20.Name = "menu_cust_healthy_m_consultation";
            treeNode20.Tag = "&ar_healthy&";
            treeNode20.Text = "ประวัติคำปรึกษาสุขภาพ";
            treeNode21.Name = "menu_cust_healthy_m_drugsConsultants";
            treeNode21.Tag = "&ar_healthy&";
            treeNode21.Text = "ประวัติการให้คำปรึกษาการใช้ยา";
            treeNode22.ImageKey = "box_add.png";
            treeNode22.Name = "menu_healthy_customer";
            treeNode22.SelectedImageKey = "box_add.png";
            treeNode22.Tag = "&1&&2&&3&";
            treeNode22.Text = "ลูกหนี้/ลูกค้า/สมาชิก";
            this._mainMenuSMLHealthy.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode15,
            treeNode17,
            treeNode22});
            this._mainMenuSMLHealthy.Size = new System.Drawing.Size(314, 552);
            this._mainMenuSMLHealthy.TabIndex = 5;
            this._mainMenuSMLHealthy.TabStop = false;
            // 
            // _mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(764, 562);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "_mainForm";
            this.Text = "Form1";
            this._menuPanel.ResumeLayout(false);
            this._tabControl.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MyLib._myTreeView _mainMenuSMLHealthy;
    }
}

