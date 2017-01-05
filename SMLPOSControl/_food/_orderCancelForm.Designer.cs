namespace SMLPOSControl._food
{
    partial class _orderCancelForm
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
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this._okButton = new MyLib.VistaButton();
            this._myPanel1 = new MyLib._myPanel();
            this._backButton = new MyLib.VistaButton();
            this._screen = new MyLib._myScreen();
            this.flowLayoutPanel1.SuspendLayout();
            this._myPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.flowLayoutPanel1.Controls.Add(this._okButton);
            this.flowLayoutPanel1.Controls.Add(this._backButton);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 152);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(362, 46);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // _okButton
            // 
            this._okButton.AutoSize = true;
            this._okButton.BackColor = System.Drawing.Color.Transparent;
            this._okButton.ButtonText = "ตกลง";
            this._okButton.ImageSize = new System.Drawing.Size(32, 32);
            this._okButton.Location = new System.Drawing.Point(279, 3);
            this._okButton.myImage = global::SMLPOSControl.Properties.Resources.disk_blue1;
            this._okButton.Name = "_okButton";
            this._okButton.Size = new System.Drawing.Size(80, 40);
            this._okButton.TabIndex = 0;
            this._okButton.Text = "ตกลง";
            this._okButton.UseVisualStyleBackColor = false;
            // 
            // _myPanel1
            // 
            this._myPanel1._switchTabAuto = false;
            this._myPanel1.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Controls.Add(this._screen);
            this._myPanel1.Controls.Add(this.flowLayoutPanel1);
            this._myPanel1.CornerPicture = null;
            this._myPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myPanel1.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Location = new System.Drawing.Point(0, 0);
            this._myPanel1.Name = "_myPanel1";
            this._myPanel1.ShowLineBackground = true;
            this._myPanel1.Size = new System.Drawing.Size(362, 198);
            this._myPanel1.TabIndex = 1;
            // 
            // _backButton
            // 
            this._backButton.AutoSize = true;
            this._backButton.BackColor = System.Drawing.Color.Transparent;
            this._backButton.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this._backButton.ButtonText = "กลับ";
            this._backButton.ImageSize = new System.Drawing.Size(32, 32);
            this._backButton.Location = new System.Drawing.Point(199, 3);
            this._backButton.myImage = global::SMLPOSControl.Properties.Resources.delete21;
            this._backButton.Name = "_backButton";
            this._backButton.Size = new System.Drawing.Size(74, 40);
            this._backButton.TabIndex = 1;
            this._backButton.Text = "กลับ";
            this._backButton.UseVisualStyleBackColor = false;
            this._backButton.Click += new System.EventHandler(this._backButton_Click);
            // 
            // _screen
            // 
            this._screen._isChange = false;
            this._screen.BackColor = System.Drawing.Color.Transparent;
            this._screen.Dock = System.Windows.Forms.DockStyle.Fill;
            this._screen.Location = new System.Drawing.Point(0, 0);
            this._screen.Name = "_screen";
            this._screen.Padding = new System.Windows.Forms.Padding(5);
            this._screen.Size = new System.Drawing.Size(362, 152);
            this._screen.TabIndex = 1;
            // 
            // _orderCancelForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(362, 198);
            this.Controls.Add(this._myPanel1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "_orderCancelForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ยกเลิก";
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this._myPanel1.ResumeLayout(false);
            this._myPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private MyLib._myPanel _myPanel1;
        private MyLib.VistaButton _backButton;
        public MyLib.VistaButton _okButton;
        public MyLib._myScreen _screen;
    }
}