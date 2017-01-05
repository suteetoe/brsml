namespace SMLPosClient
{
    partial class _posSaveSendMoney
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
            this._myManageData1 = new MyLib._myManageData();
            this._myPanel1 = new MyLib._myPanel();
            this._pos_save_send_money_screen1 = new SMLPosClient._pos_save_send_money_screen();
            this._myToolBar = new System.Windows.Forms.ToolStrip();
            this._save = new MyLib.ToolStripMyButton();
            this._close = new MyLib.ToolStripMyButton();
            this._myPanel1.SuspendLayout();
            this._myToolBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // _myManageData1
            // 
            this._myManageData1._mainMenuCode = "";
            this._myManageData1._mainMenuId = "";
            this._myManageData1.BackColor = System.Drawing.Color.WhiteSmoke;
            this._myManageData1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._myManageData1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myManageData1.Location = new System.Drawing.Point(0, 0);
            this._myManageData1.Name = "_myManageData1";
            this._myManageData1.Size = new System.Drawing.Size(816, 733);
            this._myManageData1.TabIndex = 0;
            this._myManageData1.TabStop = false;
            this._myManageData1._form2.Controls.Add(this._myPanel1);
            // 
            // _myPanel1
            // 
            this._myPanel1._switchTabAuto = false;
            this._myPanel1.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Controls.Add(this._pos_save_send_money_screen1);
            this._myPanel1.Controls.Add(this._myToolBar);
            this._myPanel1.CornerPicture = null;
            this._myPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myPanel1.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Location = new System.Drawing.Point(0, 0);
            this._myPanel1.Name = "_myPanel1";
            this._myPanel1.Size = new System.Drawing.Size(731, 712);
            this._myPanel1.TabIndex = 1;
            // 
            // _pos_save_send_money_screen1
            // 
            this._pos_save_send_money_screen1._isChange = false;
            this._pos_save_send_money_screen1.BackColor = System.Drawing.Color.Transparent;
            this._pos_save_send_money_screen1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._pos_save_send_money_screen1.Location = new System.Drawing.Point(0, 25);
            this._pos_save_send_money_screen1.Name = "_pos_save_send_money_screen1";
            this._pos_save_send_money_screen1.Size = new System.Drawing.Size(731, 687);
            this._pos_save_send_money_screen1.TabIndex = 1;
            // 
            // toolStrip1
            // 
            this._myToolBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._save,
            this._close});
            this._myToolBar.Location = new System.Drawing.Point(0, 0);
            this._myToolBar.Name = "toolStrip1";
            this._myToolBar.Size = new System.Drawing.Size(731, 25);
            this._myToolBar.TabIndex = 0;
            this._myToolBar.Text = "toolStrip1";
            // 
            // _save
            // 
            this._save.Image = global::SMLPosClient.Properties.Resources.disk_blue1;
            this._save.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._save.Name = "_save";
            this._save.Padding = new System.Windows.Forms.Padding(1);
            this._save.ResourceName = "";
            this._save.Size = new System.Drawing.Size(87, 22);
            this._save.Text = "บันทึก (F12)";
            // 
            // _close
            // 
            this._close.Image = global::SMLPosClient.Properties.Resources.error1;
            this._close.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._close.Name = "_close";
            this._close.Padding = new System.Windows.Forms.Padding(1);
            this._close.ResourceName = "";
            this._close.Size = new System.Drawing.Size(55, 22);
            this._close.Text = "ปิดจอ";
            // 
            // _posSaveSendMoney
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._myManageData1);
            this.Name = "_posSaveSendMoney";
            this.Size = new System.Drawing.Size(816, 733);
            this._myPanel1.ResumeLayout(false);
            this._myPanel1.PerformLayout();
            this._myToolBar.ResumeLayout(false);
            this._myToolBar.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private MyLib._myManageData _myManageData1;
        private MyLib._myPanel _myPanel1;
        private System.Windows.Forms.ToolStrip _myToolBar;
        private MyLib.ToolStripMyButton _save;
        private MyLib.ToolStripMyButton _close;
        private _pos_save_send_money_screen _pos_save_send_money_screen1;
    }
}
