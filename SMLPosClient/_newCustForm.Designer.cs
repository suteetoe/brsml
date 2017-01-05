namespace SMLPosClient
{
    partial class _newCustForm
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
            this._panel = new System.Windows.Forms.Panel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this._buttonSave = new MyLib.VistaButton();
            this._buttonCancel = new MyLib.VistaButton();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _panel
            // 
            this._panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._panel.Location = new System.Drawing.Point(0, 0);
            this._panel.Name = "_panel";
            this._panel.Padding = new System.Windows.Forms.Padding(5);
            this._panel.Size = new System.Drawing.Size(817, 448);
            this._panel.TabIndex = 0;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.Controls.Add(this._buttonSave);
            this.flowLayoutPanel1.Controls.Add(this._buttonCancel);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 448);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(817, 38);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // _buttonSave
            // 
            this._buttonSave._drawNewMethod = false;
            this._buttonSave.BackColor = System.Drawing.Color.Transparent;
            this._buttonSave.ButtonText = "บันทึก";
            this._buttonSave.Location = new System.Drawing.Point(714, 3);
            this._buttonSave.Name = "_buttonSave";
            this._buttonSave.Size = new System.Drawing.Size(100, 32);
            this._buttonSave.TabIndex = 0;
            this._buttonSave.Text = "vistaButton1";
            this._buttonSave.UseVisualStyleBackColor = false;
            this._buttonSave.Click += new System.EventHandler(this._buttonSave_Click);
            // 
            // _buttonCancel
            // 
            this._buttonCancel._drawNewMethod = false;
            this._buttonCancel.BackColor = System.Drawing.Color.Transparent;
            this._buttonCancel.ButtonText = "ยกเลิก";
            this._buttonCancel.Location = new System.Drawing.Point(608, 3);
            this._buttonCancel.Name = "_buttonCancel";
            this._buttonCancel.Size = new System.Drawing.Size(100, 32);
            this._buttonCancel.TabIndex = 1;
            this._buttonCancel.Text = "vistaButton2";
            this._buttonCancel.UseVisualStyleBackColor = false;
            this._buttonCancel.Click += new System.EventHandler(this._buttonCancel_Click);
            // 
            // _newCustForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(817, 486);
            this.Controls.Add(this._panel);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "_newCustForm";
            this.Text = "เพิ่มลูกค้า";
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel _panel;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private MyLib.VistaButton _buttonSave;
        private MyLib.VistaButton _buttonCancel;
    }
}