namespace SMLERPControl._master
{
    partial class _master_ic_shelf
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this._myManageData1 = new MyLib._myManageData();
            this._myPanel1 = new MyLib._myPanel();
            this._myToolbar = new System.Windows.Forms.ToolStrip();
            this.toolStripMyButton1 = new MyLib.ToolStripMyButton();
            this._ic_inventory_data_grid = new SMLERPControl._master._master_ic_shelf_grid();
            this._ic_inventory_screen_top = new SMLERPControl._master._master_ic_shelf_screen();
            this._myManageData1._form2.SuspendLayout();
            this._myManageData1.SuspendLayout();
            this._myPanel1.SuspendLayout();
            this._myToolbar.SuspendLayout();
            this.SuspendLayout();
            // 
            // _myManageData1
            // 
            this._myManageData1.BackColor = System.Drawing.Color.WhiteSmoke;
            this._myManageData1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._myManageData1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myManageData1.Location = new System.Drawing.Point(0, 0);
            this._myManageData1.Name = "_myManageData1";
            // 
            // _myManageData1.Panel1
            // 
            this._myManageData1._form1.BackColor = System.Drawing.Color.WhiteSmoke;
            // 
            // _myManageData1.Panel2
            // 
            this._myManageData1._form2.BackColor = System.Drawing.Color.WhiteSmoke;
            this._myManageData1._form2.Controls.Add(this._myPanel1);
            this._myManageData1._form2.Controls.Add(this._myToolbar);
            this._myManageData1.Size = new System.Drawing.Size(614, 496);
            this._myManageData1.TabIndex = 0;
            this._myManageData1.TabStop = false;
            // 
            // _myPanel1
            // 
            this._myPanel1.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Controls.Add(this._ic_inventory_data_grid);
            this._myPanel1.Controls.Add(this._ic_inventory_screen_top);
            this._myPanel1.CornerPicture = null;
            this._myPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myPanel1.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Location = new System.Drawing.Point(0, 25);
            this._myPanel1.Name = "_myPanel1";
            this._myPanel1.Padding = new System.Windows.Forms.Padding(5);
            this._myPanel1.Size = new System.Drawing.Size(471, 469);
            this._myPanel1.TabIndex = 3;
            // 
            // _myToolbar
            // 
            this._myToolbar.BackgroundImage = global::SMLERPControl.Properties.Resources.bt03;
            this._myToolbar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMyButton1});
            this._myToolbar.Location = new System.Drawing.Point(0, 0);
            this._myToolbar.Name = "_myToolbar";
            this._myToolbar.Size = new System.Drawing.Size(471, 25);
            this._myToolbar.TabIndex = 2;
            this._myToolbar.Text = "toolStrip1";
            // 
            // toolStripMyButton1
            // 
            this.toolStripMyButton1.Image = global::SMLERPControl.Properties.Resources.disk_blue;
            this.toolStripMyButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripMyButton1.Name = "toolStripMyButton1";
            this.toolStripMyButton1.ResourceName = "บันทึก";
            this.toolStripMyButton1.Padding = new System.Windows.Forms.Padding(1);
            this.toolStripMyButton1.Size = new System.Drawing.Size(58, 22);
            this.toolStripMyButton1.Text = "บันทึก";
            this.toolStripMyButton1.Click += new System.EventHandler(this._buttonSave_Click);
            // 
            // _ic_inventory_data_grid
            // 
            this._ic_inventory_data_grid.BackColor = System.Drawing.SystemColors.Window;
            this._ic_inventory_data_grid.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._ic_inventory_data_grid.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._ic_inventory_data_grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._ic_inventory_data_grid.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._ic_inventory_data_grid.Location = new System.Drawing.Point(5, 101);
            this._ic_inventory_data_grid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._ic_inventory_data_grid.Name = "_ic_inventory_data_grid";
            this._ic_inventory_data_grid.Size = new System.Drawing.Size(461, 363);
            this._ic_inventory_data_grid.TabIndex = 1;
            this._ic_inventory_data_grid.TabStop = false;
            // 
            // _ic_inventory_screen_top
            // 
            this._ic_inventory_screen_top._isChange = false;
            this._ic_inventory_screen_top.BackColor = System.Drawing.Color.Transparent;
            this._ic_inventory_screen_top.Dock = System.Windows.Forms.DockStyle.Top;
            this._ic_inventory_screen_top.Location = new System.Drawing.Point(5, 5);
            this._ic_inventory_screen_top.Name = "_ic_inventory_screen_top";
            this._ic_inventory_screen_top.Size = new System.Drawing.Size(461, 96);
            this._ic_inventory_screen_top.TabIndex = 0;
            // 
            // _master_ic_shelf
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._myManageData1);
            this.Name = "_master_ic_shelf";
            this.Size = new System.Drawing.Size(614, 496);
            this._myManageData1._form2.ResumeLayout(false);
            this._myManageData1._form2.PerformLayout();
            this._myManageData1.ResumeLayout(false);
            this._myPanel1.ResumeLayout(false);
            this._myToolbar.ResumeLayout(false);
            this._myToolbar.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private MyLib._myManageData _myManageData1;
        private System.Windows.Forms.ToolStrip _myToolbar;
        private MyLib.ToolStripMyButton toolStripMyButton1;
        private MyLib._myPanel _myPanel1;
        private _master_ic_shelf_screen _ic_inventory_screen_top;
        private _master_ic_shelf_grid _ic_inventory_data_grid;
    }
}
