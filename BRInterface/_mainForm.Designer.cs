namespace BRInterface
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("ใบขอโอนสินค้า");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("โอนรายการขายสินค้า/รับเงิน");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("โอนใบสั่งขาย");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("โอนรายการรับเงินมัดจำขวด");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("โอนใบยืม/คืนขวดลัง");
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("SateTools", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3,
            treeNode4,
            treeNode5});
            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("โอนรายขาย SHINGHA Online");
            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("โอนรายการขาย SHINGA Family");
            System.Windows.Forms.TreeNode treeNode9 = new System.Windows.Forms.TreeNode("SINGHA Online/Family", new System.Windows.Forms.TreeNode[] {
            treeNode7,
            treeNode8});
            this._mainMenuInterface = new MyLib._myTreeView();
            this._menuPanel.SuspendLayout();
            this._tabControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // _menuPanel
            // 
            this._menuPanel.Controls.Add(this._mainMenuInterface);
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
            // _mainMenuInterface
            // 
            this._mainMenuInterface.Dock = System.Windows.Forms.DockStyle.Fill;
            this._mainMenuInterface.Location = new System.Drawing.Point(4, 4);
            this._mainMenuInterface.Name = "_mainMenuInterface";
            treeNode1.Name = "menu_import_request_transfer_import";
            treeNode1.Text = "ใบขอโอนสินค้า";
            treeNode2.Name = "menu_improt_sale";
            treeNode2.Text = "โอนรายการขายสินค้า/รับเงิน";
            treeNode3.Name = "menu_import_sale_order";
            treeNode3.Text = "โอนใบสั่งขาย";
            treeNode4.Name = "menu_import_case_button_deposit";
            treeNode4.Text = "โอนรายการรับเงินมัดจำขวด";
            treeNode5.Name = "menu_import_case_button_issue";
            treeNode5.Text = "โอนใบยืม/คืนขวดลัง";
            treeNode6.Name = "menu_saletools";
            treeNode6.Text = "SateTools";
            treeNode7.Name = "menu_transfer_singha_online_transection";
            treeNode7.Text = "โอนรายขาย SHINGHA Online";
            treeNode8.Name = "menu_transfer_singha_family_transection";
            treeNode8.Text = "โอนรายการขาย SHINGA Family";
            treeNode9.Name = "menu_singha_online";
            treeNode9.Text = "SINGHA Online/Family";
            this._mainMenuInterface.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode6,
            treeNode9});
            this._mainMenuInterface.Size = new System.Drawing.Size(314, 558);
            this._mainMenuInterface.TabIndex = 0;
            // 
            // _mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(927, 568);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "_mainForm";
            this.Text = "Form1";
            this._menuPanel.ResumeLayout(false);
            this._tabControl.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MyLib._myTreeView _mainMenuInterface;
    }
}

