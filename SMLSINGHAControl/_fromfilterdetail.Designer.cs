namespace SMLSINGHAControl
{
    partial class _fromfilterdetail
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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this._buttonConfirm = new MyLib.ToolStripMyButton();
            this._buttonClose = new MyLib.ToolStripMyButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this._singhaGridGetdata1 = new SMLSINGHAControl._singhaGridGetdata();
            this.toolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._buttonConfirm,
            this._buttonClose});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(789, 25);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // _buttonConfirm
            // 
            this._buttonConfirm.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._buttonConfirm.Image = global::SMLSINGHAControl.Properties.Resources.flash;
            this._buttonConfirm.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._buttonConfirm.Name = "_buttonConfirm";
            this._buttonConfirm.Padding = new System.Windows.Forms.Padding(1);
            this._buttonConfirm.ResourceName = "";
            this._buttonConfirm.Size = new System.Drawing.Size(126, 22);
            this._buttonConfirm.Text = "ยืนยันและปิดหน้าจอ";
            
            // 
            // _buttonClose
            // 
            this._buttonClose.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._buttonClose.Image = global::SMLSINGHAControl.Properties.Resources.error1;
            this._buttonClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._buttonClose.Name = "_buttonClose";
            this._buttonClose.Padding = new System.Windows.Forms.Padding(1);
            this._buttonClose.ResourceName = "";
            this._buttonClose.Size = new System.Drawing.Size(130, 22);
            this._buttonClose.Text = "ยกเลิกและปิดหน้าจอ";
            this._buttonClose.Click += new System.EventHandler(this._buttonClose_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this._singhaGridGetdata1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(789, 698);
            this.panel1.TabIndex = 4;
            // 
            // _singhaGridGetdata1
            // 
            this._singhaGridGetdata1._extraWordShow = true;
            this._singhaGridGetdata1._selectRow = -1;
            this._singhaGridGetdata1.BackColor = System.Drawing.SystemColors.Window;
            this._singhaGridGetdata1.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._singhaGridGetdata1.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._singhaGridGetdata1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._singhaGridGetdata1.Font = new System.Drawing.Font("Tahoma", 9F);
            this._singhaGridGetdata1.Location = new System.Drawing.Point(0, 0);
            this._singhaGridGetdata1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._singhaGridGetdata1.Name = "_singhaGridGetdata1";
            this._singhaGridGetdata1.Size = new System.Drawing.Size(789, 698);
            this._singhaGridGetdata1.TabIndex = 0;
            this._singhaGridGetdata1.TabStop = false;
            // 
            // _fromfilterdetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(789, 723);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "_fromfilterdetail";
            this.Text = "เลือกเฉพาะรายการ";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        public MyLib.ToolStripMyButton _buttonConfirm;
        private MyLib.ToolStripMyButton _buttonClose;
        private System.Windows.Forms.Panel panel1;
        private _singhaGridGetdata _singhaGridGetdata1;
    }
}