namespace SMLOffTakeSalesAdmin
{
    partial class _mainScreen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(_mainScreen));
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("นำเข้าข้อมูล  RDM Master");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("นำเข้าข้อมูล Product Master");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("นำข้อมูลเข้า Channel Master to SML Agent");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("เริ่มต้น", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3});
            this._menuImage = new System.Windows.Forms.ImageList(this.components);
            this._mainMenuERP = new MyLib._myTreeView();
            this._menuPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // _menuPanel
            // 
            this._menuPanel.Controls.Add(this._mainMenuERP);
            this._menuPanel.Size = new System.Drawing.Size(324, 607);
            // 
            // Home
            // 
            this.Home.Size = new System.Drawing.Size(509, 510);
            // 
            // _imageList16x16
            // 
          //  this._imageList16x16.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("_imageList16x16.ImageStream")));
            this._imageList16x16.Images.SetKeyName(0, "");
            this._imageList16x16.Images.SetKeyName(1, "");
            this._imageList16x16.Images.SetKeyName(2, "media_play_green.png");
            this._imageList16x16.Images.SetKeyName(3, "media_stop_red.png");
            // 
            // _menuImage
            // 
            this._menuImage.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this._menuImage.ImageSize = new System.Drawing.Size(16, 16);
            this._menuImage.TransparentColor = System.Drawing.Color.Transparent;
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
            treeNode1.Name = "menu_import_rdm";
            treeNode1.Tag = "&take&";
            treeNode1.Text = "นำเข้าข้อมูล  RDM Master";
            treeNode2.Name = "menu_import_productmaster";
            treeNode2.Tag = "&take&";
            treeNode2.Text = "นำเข้าข้อมูล Product Master";
            treeNode3.Name = "menu_setup_chanel_master";
            treeNode3.Tag = "&take&";
            treeNode3.Text = "นำข้อมูลเข้า Channel Master to SML Agent";
            treeNode4.ImageKey = "preferences.png";
            treeNode4.Name = "menu_setup";
            treeNode4.SelectedImageKey = "preferences.png";
            treeNode4.Tag = "&1&&2&&3&";
            treeNode4.Text = "เริ่มต้น";
            this._mainMenuERP.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode4});
            this._mainMenuERP.SelectedImageIndex = 0;
            this._mainMenuERP.Size = new System.Drawing.Size(314, 597);
            this._mainMenuERP.TabIndex = 6;
            this._mainMenuERP.TabStop = false;
            // 
            // _mainScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(841, 607);
           // this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(0, 0);
            this.MinimumSize = new System.Drawing.Size(697, 643);
            this.Name = "_mainScreen";
            this.Text = "ANBridgeCenterAdmin";
            this._menuPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ImageList _menuImage;
        private MyLib._myTreeView _mainMenuERP;
    }
}