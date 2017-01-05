namespace SMLPOSChecker
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("ตรวจสอบข้อมูลภาษี POS");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("ตรวจสอบข้อมูล", new System.Windows.Forms.TreeNode[] {
            treeNode1});
            this._checkerMenu = new MyLib._myTreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this._menuPanel.SuspendLayout();
            this._tabControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // _menuPanel
            // 
            this._menuPanel.Controls.Add(this._checkerMenu);
            this._menuPanel.Size = new System.Drawing.Size(324, 458);
            // 
            // Home
            // 
            this.Home.Size = new System.Drawing.Size(241, 379);
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
            this._tabControl.Size = new System.Drawing.Size(249, 407);
            // 
            // _checkerMenu
            // 
            this._checkerMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this._checkerMenu.Location = new System.Drawing.Point(4, 4);
            this._checkerMenu.Name = "_checkerMenu";
            treeNode1.Name = "check_vat";
            treeNode1.Text = "ตรวจสอบข้อมูลภาษี POS";
            treeNode2.Name = "menu_check";
            treeNode2.Text = "ตรวจสอบข้อมูล";
            this._checkerMenu.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode2});
            this._checkerMenu.Size = new System.Drawing.Size(314, 448);
            this._checkerMenu.TabIndex = 0;
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // _mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(573, 458);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "_mainForm";
            this.Text = "_mainForm";
            this._menuPanel.ResumeLayout(false);
            this._tabControl.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MyLib._myTreeView _checkerMenu;
        private System.Windows.Forms.ImageList imageList1;
    }
}