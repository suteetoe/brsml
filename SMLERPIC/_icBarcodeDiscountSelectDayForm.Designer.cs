namespace SMLERPIC
{
    partial class _icBarcodeDiscountSelectDayForm
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
            this._myFlowLayoutPanel1 = new MyLib._myFlowLayoutPanel();
            this._okButton = new MyLib.VistaButton();
            this._cancelButton = new MyLib.VistaButton();
            this._allDayCheckbox = new System.Windows.Forms.CheckBox();
            this._monCheckbox = new System.Windows.Forms.CheckBox();
            this._tueCheckbox = new System.Windows.Forms.CheckBox();
            this._sunCheckbox = new System.Windows.Forms.CheckBox();
            this._webCheckbox = new System.Windows.Forms.CheckBox();
            this._thuCheckbox = new System.Windows.Forms.CheckBox();
            this._friCheckbox = new System.Windows.Forms.CheckBox();
            this._satCheckbox = new System.Windows.Forms.CheckBox();
            this._myPanel1.SuspendLayout();
            this._myFlowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _myPanel1
            // 
            this._myPanel1._switchTabAuto = false;
            this._myPanel1.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Controls.Add(this._satCheckbox);
            this._myPanel1.Controls.Add(this._friCheckbox);
            this._myPanel1.Controls.Add(this._thuCheckbox);
            this._myPanel1.Controls.Add(this._webCheckbox);
            this._myPanel1.Controls.Add(this._sunCheckbox);
            this._myPanel1.Controls.Add(this._tueCheckbox);
            this._myPanel1.Controls.Add(this._monCheckbox);
            this._myPanel1.Controls.Add(this._allDayCheckbox);
            this._myPanel1.Controls.Add(this._myFlowLayoutPanel1);
            this._myPanel1.CornerPicture = null;
            this._myPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myPanel1.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Location = new System.Drawing.Point(0, 0);
            this._myPanel1.Name = "_myPanel1";
            this._myPanel1.Size = new System.Drawing.Size(487, 106);
            this._myPanel1.TabIndex = 0;
            // 
            // _myFlowLayoutPanel1
            // 
            this._myFlowLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this._myFlowLayoutPanel1.Controls.Add(this._okButton);
            this._myFlowLayoutPanel1.Controls.Add(this._cancelButton);
            this._myFlowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._myFlowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this._myFlowLayoutPanel1.Location = new System.Drawing.Point(0, 72);
            this._myFlowLayoutPanel1.Name = "_myFlowLayoutPanel1";
            this._myFlowLayoutPanel1.Size = new System.Drawing.Size(487, 34);
            this._myFlowLayoutPanel1.TabIndex = 0;
            // 
            // _okButton
            // 
            this._okButton._drawNewMethod = false;
            this._okButton.BackColor = System.Drawing.Color.Transparent;
            this._okButton.ButtonText = "บันทึก";
            this._okButton.Location = new System.Drawing.Point(384, 3);
            this._okButton.myImage = global::SMLERPIC.Properties.Resources.flash;
            this._okButton.Name = "_okButton";
            this._okButton.Size = new System.Drawing.Size(100, 26);
            this._okButton.TabIndex = 0;
            this._okButton.Text = "vistaButton1";
            this._okButton.UseVisualStyleBackColor = false;
            this._okButton.Click += new System.EventHandler(this._okButton_Click);
            // 
            // _cancelButton
            // 
            this._cancelButton._drawNewMethod = false;
            this._cancelButton.BackColor = System.Drawing.Color.Transparent;
            this._cancelButton.ButtonText = "ยกเลิก";
            this._cancelButton.Location = new System.Drawing.Point(278, 3);
            this._cancelButton.myImage = global::SMLERPIC.Properties.Resources.error1;
            this._cancelButton.Name = "_cancelButton";
            this._cancelButton.Size = new System.Drawing.Size(100, 26);
            this._cancelButton.TabIndex = 1;
            this._cancelButton.UseVisualStyleBackColor = false;
            this._cancelButton.Click += new System.EventHandler(this._cancelButton_Click);
            // 
            // _allDayCheckbox
            // 
            this._allDayCheckbox.AutoSize = true;
            this._allDayCheckbox.BackColor = System.Drawing.Color.Transparent;
            this._allDayCheckbox.Checked = true;
            this._allDayCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this._allDayCheckbox.Location = new System.Drawing.Point(12, 12);
            this._allDayCheckbox.Name = "_allDayCheckbox";
            this._allDayCheckbox.Size = new System.Drawing.Size(55, 18);
            this._allDayCheckbox.TabIndex = 1;
            this._allDayCheckbox.Text = "ทุกวัน";
            this._allDayCheckbox.UseVisualStyleBackColor = false;
            this._allDayCheckbox.CheckedChanged += new System.EventHandler(this.checkboxChanged);
            this._allDayCheckbox.CheckStateChanged += new System.EventHandler(this.checkboxStateChange);
            // 
            // _monCheckbox
            // 
            this._monCheckbox.AutoSize = true;
            this._monCheckbox.BackColor = System.Drawing.Color.Transparent;
            this._monCheckbox.Checked = true;
            this._monCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this._monCheckbox.Location = new System.Drawing.Point(80, 36);
            this._monCheckbox.Name = "_monCheckbox";
            this._monCheckbox.Size = new System.Drawing.Size(54, 18);
            this._monCheckbox.TabIndex = 2;
            this._monCheckbox.Text = "จันทร์";
            this._monCheckbox.UseVisualStyleBackColor = false;
            this._monCheckbox.CheckedChanged += new System.EventHandler(this.checkboxChanged);
            this._monCheckbox.CheckStateChanged += new System.EventHandler(this.checkboxStateChange);
            // 
            // _tueCheckbox
            // 
            this._tueCheckbox.AutoSize = true;
            this._tueCheckbox.BackColor = System.Drawing.Color.Transparent;
            this._tueCheckbox.Checked = true;
            this._tueCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this._tueCheckbox.Location = new System.Drawing.Point(140, 36);
            this._tueCheckbox.Name = "_tueCheckbox";
            this._tueCheckbox.Size = new System.Drawing.Size(57, 18);
            this._tueCheckbox.TabIndex = 3;
            this._tueCheckbox.Text = "อังคาร";
            this._tueCheckbox.UseVisualStyleBackColor = false;
            this._tueCheckbox.CheckedChanged += new System.EventHandler(this.checkboxChanged);
            this._tueCheckbox.CheckStateChanged += new System.EventHandler(this.checkboxStateChange);
            // 
            // _sunCheckbox
            // 
            this._sunCheckbox.AutoSize = true;
            this._sunCheckbox.BackColor = System.Drawing.Color.Transparent;
            this._sunCheckbox.Checked = true;
            this._sunCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this._sunCheckbox.Location = new System.Drawing.Point(12, 36);
            this._sunCheckbox.Name = "_sunCheckbox";
            this._sunCheckbox.Size = new System.Drawing.Size(62, 18);
            this._sunCheckbox.TabIndex = 4;
            this._sunCheckbox.Text = "อาทิตย์";
            this._sunCheckbox.UseVisualStyleBackColor = false;
            this._sunCheckbox.CheckedChanged += new System.EventHandler(this.checkboxChanged);
            this._sunCheckbox.CheckStateChanged += new System.EventHandler(this.checkboxStateChange);
            // 
            // _webCheckbox
            // 
            this._webCheckbox.AutoSize = true;
            this._webCheckbox.BackColor = System.Drawing.Color.Transparent;
            this._webCheckbox.Checked = true;
            this._webCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this._webCheckbox.Location = new System.Drawing.Point(203, 36);
            this._webCheckbox.Name = "_webCheckbox";
            this._webCheckbox.Size = new System.Drawing.Size(39, 18);
            this._webCheckbox.TabIndex = 5;
            this._webCheckbox.Text = "พุธ";
            this._webCheckbox.UseVisualStyleBackColor = false;
            this._webCheckbox.CheckedChanged += new System.EventHandler(this.checkboxChanged);
            this._webCheckbox.CheckStateChanged += new System.EventHandler(this.checkboxStateChange);
            // 
            // _thuCheckbox
            // 
            this._thuCheckbox.AutoSize = true;
            this._thuCheckbox.BackColor = System.Drawing.Color.Transparent;
            this._thuCheckbox.Checked = true;
            this._thuCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this._thuCheckbox.Location = new System.Drawing.Point(263, 36);
            this._thuCheckbox.Name = "_thuCheckbox";
            this._thuCheckbox.Size = new System.Drawing.Size(70, 18);
            this._thuCheckbox.TabIndex = 6;
            this._thuCheckbox.Text = "พฤหัสบดี";
            this._thuCheckbox.UseVisualStyleBackColor = false;
            this._thuCheckbox.CheckedChanged += new System.EventHandler(this.checkboxChanged);
            this._thuCheckbox.CheckStateChanged += new System.EventHandler(this.checkboxStateChange);
            // 
            // _friCheckbox
            // 
            this._friCheckbox.AutoSize = true;
            this._friCheckbox.BackColor = System.Drawing.Color.Transparent;
            this._friCheckbox.Checked = true;
            this._friCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this._friCheckbox.Location = new System.Drawing.Point(339, 36);
            this._friCheckbox.Name = "_friCheckbox";
            this._friCheckbox.Size = new System.Drawing.Size(45, 18);
            this._friCheckbox.TabIndex = 7;
            this._friCheckbox.Text = "ศุกร์";
            this._friCheckbox.UseVisualStyleBackColor = false;
            this._friCheckbox.CheckedChanged += new System.EventHandler(this.checkboxChanged);
            this._friCheckbox.CheckStateChanged += new System.EventHandler(this.checkboxStateChange);
            // 
            // _satCheckbox
            // 
            this._satCheckbox.AutoSize = true;
            this._satCheckbox.BackColor = System.Drawing.Color.Transparent;
            this._satCheckbox.Checked = true;
            this._satCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this._satCheckbox.Location = new System.Drawing.Point(390, 36);
            this._satCheckbox.Name = "_satCheckbox";
            this._satCheckbox.Size = new System.Drawing.Size(48, 18);
            this._satCheckbox.TabIndex = 8;
            this._satCheckbox.Text = "เสาร์";
            this._satCheckbox.UseVisualStyleBackColor = false;
            this._satCheckbox.CheckedChanged += new System.EventHandler(this.checkboxChanged);
            this._satCheckbox.CheckStateChanged += new System.EventHandler(this.checkboxStateChange);
            // 
            // _icBarcodeDiscountSelectDayForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(487, 106);
            this.Controls.Add(this._myPanel1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "_icBarcodeDiscountSelectDayForm";
            this.Text = "เลือกวัน";
            this._myPanel1.ResumeLayout(false);
            this._myPanel1.PerformLayout();
            this._myFlowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private MyLib._myPanel _myPanel1;
        private MyLib._myFlowLayoutPanel _myFlowLayoutPanel1;
        private MyLib.VistaButton _okButton;
        private MyLib.VistaButton _cancelButton;
        public System.Windows.Forms.CheckBox _satCheckbox;
        public System.Windows.Forms.CheckBox _friCheckbox;
        public System.Windows.Forms.CheckBox _thuCheckbox;
        public System.Windows.Forms.CheckBox _webCheckbox;
        public System.Windows.Forms.CheckBox _sunCheckbox;
        public System.Windows.Forms.CheckBox _tueCheckbox;
        public System.Windows.Forms.CheckBox _monCheckbox;
        public System.Windows.Forms.CheckBox _allDayCheckbox;
    }
}