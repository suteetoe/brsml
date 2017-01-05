namespace SMLPOSCashier
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("กำหนดคุณสมบัติของเครื่อง POS");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("เริ่มต้น", new System.Windows.Forms.TreeNode[] {
            treeNode1});
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("หน้าจอขาย");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("บันทึกส่งเงิน");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("ระบบขาย", new System.Windows.Forms.TreeNode[] {
            treeNode3,
            treeNode4});
            this._mainMenuPosClient = new MyLib._myTreeView();
            this._menuImage = new System.Windows.Forms.ImageList(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this._menuPanel.SuspendLayout();
            this._tabControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // _menuPanel
            // 
            this._menuPanel.Controls.Add(this._mainMenuPosClient);
            this._menuPanel.Size = new System.Drawing.Size(324, 723);
            // 
            // Home
            // 
            this.Home.Size = new System.Drawing.Size(612, 644);
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
            this._tabControl.Size = new System.Drawing.Size(620, 672);
            // 
            // _mainMenuPosClient
            // 
            this._mainMenuPosClient.BackColor = System.Drawing.Color.White;
            this._mainMenuPosClient.Dock = System.Windows.Forms.DockStyle.Fill;
            this._mainMenuPosClient.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._mainMenuPosClient.Location = new System.Drawing.Point(4, 4);
            this._mainMenuPosClient.Margin = new System.Windows.Forms.Padding(0);
            this._mainMenuPosClient.Name = "_mainMenuPosClient";
            treeNode1.Name = "menu_config_pos_screen";
            treeNode1.Tag = "&config&";
            treeNode1.Text = "กำหนดคุณสมบัติของเครื่อง POS";
            treeNode2.ImageKey = "preferences.png";
            treeNode2.Name = "menu_setup";
            treeNode2.SelectedImageKey = "preferences.png";
            treeNode2.Tag = "&1&&2&&3&";
            treeNode2.Text = "เริ่มต้น";
            treeNode3.Name = "menu_display_pos_screen";
            treeNode3.Text = "หน้าจอขาย";
            treeNode4.Name = "menu_save_send_money";
            treeNode4.Text = "บันทึกส่งเงิน";
            treeNode5.Name = "menu_system_pos_screen";
            treeNode5.Text = "ระบบขาย";
            this._mainMenuPosClient.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode2,
            treeNode5});
            this._mainMenuPosClient.Size = new System.Drawing.Size(314, 713);
            this._mainMenuPosClient.TabIndex = 6;
            this._mainMenuPosClient.TabStop = false;
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
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(158, 134);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(158, 173);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(158, 219);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 2;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // _mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(944, 723);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(0, 0);
            this.MinimumSize = new System.Drawing.Size(697, 643);
            this.Name = "_mainForm";
            this.Text = "SML POS Client";
            this.Load += new System.EventHandler(this._mainForm_Load);
            this._menuPanel.ResumeLayout(false);
            this._tabControl.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MyLib._myTreeView _mainMenuPosClient;
        private System.Windows.Forms.ImageList _menuImage;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
    }
}

