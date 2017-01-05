namespace MyLib._databaseManage._linkDatabase
{
    partial class _linkDatabaseSearchDatabase
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(_linkDatabaseSearchDatabase));
			this._listViewDatabase = new MyLib._listViewXP();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.SuspendLayout();
			// 
			// _listViewDatabase
			// 
			this._listViewDatabase.LargeImageList = this.imageList1;
			this._listViewDatabase.Location = new System.Drawing.Point(2, 4);
			this._listViewDatabase.Margin = new System.Windows.Forms.Padding(0);
			this._listViewDatabase.MultiSelect = false;
			this._listViewDatabase.Name = "_listViewDatabase";
			this._listViewDatabase.Size = new System.Drawing.Size(426, 311);
			this._listViewDatabase.TabIndex = 0;
			this._listViewDatabase.View = System.Windows.Forms.View.Tile;
			this._listViewDatabase.SelectedIndexChanged += new System.EventHandler(this._listViewDatabase_SelectedIndexChanged);
			// 
			// imageList1
			// 
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.Images.SetKeyName(0, "data_add.png");
			// 
			// _linkDatabaseSearchDatabase
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Highlight;
			this.ClientSize = new System.Drawing.Size(428, 315);
			this.Controls.Add(this._listViewDatabase);
			this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
			this.MinimizeBox = false;
			this.Name = "_linkDatabaseSearchDatabase";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Please Select Database";
			this.Load += new System.EventHandler(this._linkDatabaseSearch_Load);
			this.ResumeLayout(false);

        }

        #endregion

        public MyLib._listViewXP _listViewDatabase;
        private System.Windows.Forms.ImageList imageList1;

    }
}