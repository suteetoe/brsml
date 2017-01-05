namespace IMSSalesCommission
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("บันทึกค่าส่งเสริมการขาย");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("ขาย", new System.Windows.Forms.TreeNode[] {
            treeNode1});
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("รายงาน");
            this._myMainMenu = new MyLib._myTreeView();
            this._menuPanel.SuspendLayout();
            this._tabControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // _menuPanel
            // 
            this._menuPanel.Controls.Add(this._myMainMenu);
            this._menuPanel.Size = new System.Drawing.Size(324, 784);
            // 
            // Home
            // 
            this.Home.Size = new System.Drawing.Size(649, 705);
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
            this._tabControl.Size = new System.Drawing.Size(657, 733);
            // 
            // _myMainMenu
            // 
            this._myMainMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myMainMenu.Location = new System.Drawing.Point(4, 4);
            this._myMainMenu.Name = "_myMainMenu";
            treeNode1.Name = "menu_sales_commission";
            treeNode1.Text = "บันทึกค่าส่งเสริมการขาย";
            treeNode2.Name = "menu_so";
            treeNode2.Text = "ขาย";
            treeNode3.Name = "menu_report";
            treeNode3.Text = "รายงาน";
            this._myMainMenu.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode2,
            treeNode3});
            this._myMainMenu.Size = new System.Drawing.Size(314, 774);
            this._myMainMenu.TabIndex = 0;
            // 
            // _mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(981, 784);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "_mainForm";
            this.Text = "Form1";
            this._menuPanel.ResumeLayout(false);
            this._tabControl.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MyLib._myTreeView _myMainMenu;
    }
}

