namespace DTSClientDownload
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("ข้อมูลสินค้า SCG");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("นำเข้าใบสั่งซื้อสินค้า eOrdering");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("นำเข้าใบสั่งขาย/สั่งจอง");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("ประวัติการรับข้อมูล");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("Menu", new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode2,
            treeNode3,
            treeNode4});
            this._mainDTSMenu = new MyLib._myTreeView();
            this._statusTimerProcess = new System.Windows.Forms.Timer(this.components);
            this._menuPanel.SuspendLayout();
            this._tabControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // _menuPanel
            // 
            this._menuPanel.Controls.Add(this._mainDTSMenu);
            // 
            // _imageList16x16
            // 
            this._imageList16x16.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("_imageList16x16.ImageStream")));
            this._imageList16x16.Images.SetKeyName(0, "");
            this._imageList16x16.Images.SetKeyName(1, "");
            this._imageList16x16.Images.SetKeyName(2, "media_play_green.png");
            this._imageList16x16.Images.SetKeyName(3, "media_stop_red.png");
            // 
            // _mainDTSMenu
            // 
            this._mainDTSMenu.BackColor = System.Drawing.Color.White;
            this._mainDTSMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this._mainDTSMenu.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._mainDTSMenu.Location = new System.Drawing.Point(4, 4);
            this._mainDTSMenu.Margin = new System.Windows.Forms.Padding(0);
            this._mainDTSMenu.Name = "_mainDTSMenu";
            treeNode1.ImageKey = "table_sql_view.png";
            treeNode1.Name = "menu_item_download";
            treeNode1.SelectedImageKey = "table_sql_view.png";
            treeNode1.Tag = "";
            treeNode1.Text = "ข้อมูลสินค้า SCG";
            treeNode2.ImageKey = "box_closed.png";
            treeNode2.Name = "menu_po_download";
            treeNode2.SelectedImageKey = "box_closed.png";
            treeNode2.Text = "นำเข้าใบสั่งซื้อสินค้า eOrdering";
            treeNode3.Name = "menu_so_download";
            treeNode3.Text = "นำเข้าใบสั่งขาย/สั่งจอง";
            treeNode4.Name = "dts_download_log";
            treeNode4.Text = "ประวัติการรับข้อมูล";
            treeNode5.ImageKey = "preferences.png";
            treeNode5.Name = "menu_main";
            treeNode5.SelectedImageKey = "preferences.png";
            treeNode5.Tag = "&1&&2&&3&&6&&9&&7&&8&&10&";
            treeNode5.Text = "Menu";
            this._mainDTSMenu.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode5});
            this._mainDTSMenu.Size = new System.Drawing.Size(314, 558);
            this._mainDTSMenu.TabIndex = 6;
            this._mainDTSMenu.TabStop = false;
            // 
            // _statusTimerProcess
            // 
            this._statusTimerProcess.Interval = 1000;
            this._statusTimerProcess.Tick += new System.EventHandler(this._statusTimerProcess_Tick);
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

        private MyLib._myTreeView _mainDTSMenu;
        private System.Windows.Forms.Timer _statusTimerProcess;
    }
}

