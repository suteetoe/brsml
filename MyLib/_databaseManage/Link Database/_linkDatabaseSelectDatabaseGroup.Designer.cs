namespace MyLib._databaseManage._linkDatabase
{
    partial class _linkDatabaseSelectDatabaseGroup
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(_linkDatabaseSelectDatabaseGroup));
			this._listViewDatabaseGroup = new MyLib._listViewXP();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.SuspendLayout();
			// 
			// _listViewDatabaseGroup
			// 
			this._listViewDatabaseGroup.LargeImageList = this.imageList1;
			this._listViewDatabaseGroup.Location = new System.Drawing.Point(10, 11);
			this._listViewDatabaseGroup.Margin = new System.Windows.Forms.Padding(0);
			this._listViewDatabaseGroup.MultiSelect = false;
			this._listViewDatabaseGroup.Name = "_listViewDatabaseGroup";
			this._listViewDatabaseGroup.Size = new System.Drawing.Size(416, 359);
			this._listViewDatabaseGroup.TabIndex = 1;
			this._listViewDatabaseGroup.View = System.Windows.Forms.View.Tile;
			// 
			// imageList1
			// 
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.Images.SetKeyName(0, "data_copy.png");
			// 
			// _linkDatabaseSelectDatabaseGroup
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Highlight;
			this.ClientSize = new System.Drawing.Size(569, 397);
			this.Controls.Add(this._listViewDatabaseGroup);
			this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.MinimizeBox = false;
			this.Name = "_linkDatabaseSelectDatabaseGroup";
			this.ShowInTaskbar = true;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Please Select Database Group";
			this.Load += new System.EventHandler(this._linkDatabaseSelectDatabaseGroup_Load);
			this.ResumeLayout(false);

        }

        #endregion

        public MyLib._listViewXP _listViewDatabaseGroup;
        private System.Windows.Forms.ImageList imageList1;

    }
}