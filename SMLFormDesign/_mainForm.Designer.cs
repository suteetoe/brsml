namespace SMLFormDesign
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("สร้างและแก้ไขแบบฟอร์ม");
            this._formDesignMenu = new MyLib._myTreeView();
            this._menuPanel.SuspendLayout();
            this._tabControl.SuspendLayout();
            this.SuspendLayout();
            // 
            // _menuPanel
            // 
            this._menuPanel.Controls.Add(this._formDesignMenu);
            // 
            // _imageList16x16
            // 
            this._imageList16x16.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("_imageList16x16.ImageStream")));
            this._imageList16x16.Images.SetKeyName(0, "");
            this._imageList16x16.Images.SetKeyName(1, "");
            this._imageList16x16.Images.SetKeyName(2, "media_play_green.png");
            this._imageList16x16.Images.SetKeyName(3, "media_stop_red.png");
            // 
            // _formDesignMenu
            // 
            this._formDesignMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this._formDesignMenu.Location = new System.Drawing.Point(4, 4);
            this._formDesignMenu.Name = "_formDesignMenu";
            treeNode1.Name = "menu_form_design";
            treeNode1.Text = "สร้างและแก้ไขแบบฟอร์ม";
            this._formDesignMenu.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1});
            this._formDesignMenu.Size = new System.Drawing.Size(314, 558);
            this._formDesignMenu.TabIndex = 0;
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

        private MyLib._myTreeView _formDesignMenu;

    }
}

