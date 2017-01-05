namespace MyLib
{
    partial class _myDialogForm
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
            this._myPanel1 = new MyLib._myPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this._dialogScreen = new MyLib._myScreen();
            this.panel2 = new System.Windows.Forms.Panel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this._buttonCancel = new MyLib._myButton();
            this._buttonOk = new MyLib._myButton();
            this._myPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _myPanel1
            // 
            this._myPanel1._switchTabAuto = false;
            this._myPanel1.AutoSize = true;
            this._myPanel1.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Controls.Add(this.panel1);
            this._myPanel1.Controls.Add(this.panel2);
            this._myPanel1.CornerPicture = null;
            this._myPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myPanel1.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Location = new System.Drawing.Point(0, 0);
            this._myPanel1.Name = "_myPanel1";
            this._myPanel1.Size = new System.Drawing.Size(309, 39);
            this._myPanel1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this._dialogScreen);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(309, 4);
            this.panel1.TabIndex = 0;
            // 
            // _dialogScreen
            // 
            this._dialogScreen._isChange = false;
            this._dialogScreen.BackColor = System.Drawing.Color.Transparent;
            this._dialogScreen.Location = new System.Drawing.Point(0, 0);
            this._dialogScreen.Name = "_dialogScreen";
            this._dialogScreen.Size = new System.Drawing.Size(309, 39);
            this._dialogScreen.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.Controls.Add(this.flowLayoutPanel1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 4);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(309, 35);
            this.panel2.TabIndex = 0;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this._buttonCancel);
            this.flowLayoutPanel1.Controls.Add(this._buttonOk);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.flowLayoutPanel1.Size = new System.Drawing.Size(309, 35);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // _buttonCancel
            // 
            this._buttonCancel.AutoSize = true;
            this._buttonCancel.BackColor = System.Drawing.Color.Transparent;
            this._buttonCancel.ButtonText = "Cancel";
            this._buttonCancel.Location = new System.Drawing.Point(229, 5);
            this._buttonCancel.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this._buttonCancel.myImage = global::MyLib.Properties.Resources.error1;
            this._buttonCancel.myTextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._buttonCancel.myUseVisualStyleBackColor = false;
            this._buttonCancel.Name = "_buttonCancel";
            this._buttonCancel.ResourceName = "";
            this._buttonCancel.Size = new System.Drawing.Size(70, 24);
            this._buttonCancel.TabIndex = 1;
            this._buttonCancel.Text = "_myButton2";
            this._buttonCancel.UseVisualStyleBackColor = false;
            this._buttonCancel.Click += new System.EventHandler(this._buttonCancel_Click);
            // 
            // _buttonOk
            // 
            this._buttonOk.AutoSize = true;
            this._buttonOk.BackColor = System.Drawing.Color.Transparent;
            this._buttonOk.ButtonText = "Save";
            this._buttonOk.Location = new System.Drawing.Point(162, 5);
            this._buttonOk.Margin = new System.Windows.Forms.Padding(1, 0, 5, 0);
            this._buttonOk.myImage = global::MyLib.Properties.Resources.flash1;
            this._buttonOk.myTextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._buttonOk.myUseVisualStyleBackColor = false;
            this._buttonOk.Name = "_buttonOk";
            this._buttonOk.ResourceName = "";
            this._buttonOk.Size = new System.Drawing.Size(61, 24);
            this._buttonOk.TabIndex = 0;
            this._buttonOk.UseVisualStyleBackColor = false;
            this._buttonOk.Click += new System.EventHandler(this._buttonOk_Click);
            // 
            // _myDialogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(309, 39);
            this.Controls.Add(this._myPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "_myDialogForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "_myDialogForm";
            this._myPanel1.ResumeLayout(false);
            this._myPanel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private _myPanel _myPanel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        public _myButton _buttonCancel;
        public _myButton _buttonOk;
        public _myScreen _dialogScreen;

    }
}