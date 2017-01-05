namespace SMLFastReport
{
    partial class _saveForm
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
            this._buttonSave = new MyLib.VistaButton();
            this._buttonCancel = new MyLib.VistaButton();
            this._menuNameTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this._menuIdTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this._myFlowLayoutPanel1 = new MyLib._myFlowLayoutPanel();
            this._screen = new SMLFastReport._screen_save_report();
            this._myPanel1.SuspendLayout();
            this._myFlowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _myPanel1
            // 
            this._myPanel1._switchTabAuto = false;
            this._myPanel1.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Controls.Add(this._myFlowLayoutPanel1);
            this._myPanel1.Controls.Add(this._screen);
            this._myPanel1.Controls.Add(this._menuNameTextBox);
            this._myPanel1.Controls.Add(this.label2);
            this._myPanel1.Controls.Add(this._menuIdTextBox);
            this._myPanel1.Controls.Add(this.label1);
            this._myPanel1.CornerPicture = null;
            this._myPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myPanel1.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Location = new System.Drawing.Point(0, 0);
            this._myPanel1.Name = "_myPanel1";
            this._myPanel1.Size = new System.Drawing.Size(583, 108);
            this._myPanel1.TabIndex = 6;
            // 
            // _buttonSave
            // 
            this._buttonSave._drawNewMethod = false;
            this._buttonSave.BackColor = System.Drawing.Color.Transparent;
            this._buttonSave.ButtonText = "Save";
            this._buttonSave.Location = new System.Drawing.Point(517, 3);
            this._buttonSave.myImage = global::SMLFastReport.Properties.Resources.disk_blue;
            this._buttonSave.Name = "_buttonSave";
            this._buttonSave.Size = new System.Drawing.Size(63, 26);
            this._buttonSave.TabIndex = 11;
            this._buttonSave.Text = "Save";
            this._buttonSave.UseVisualStyleBackColor = false;
            this._buttonSave.Click += new System.EventHandler(this._buttonSave_Click_1);
            // 
            // _buttonCancel
            // 
            this._buttonCancel._drawNewMethod = false;
            this._buttonCancel.BackColor = System.Drawing.Color.Transparent;
            this._buttonCancel.ButtonText = "Cancel";
            this._buttonCancel.Location = new System.Drawing.Point(444, 3);
            this._buttonCancel.myImage = global::SMLFastReport.Properties.Resources.error;
            this._buttonCancel.Name = "_buttonCancel";
            this._buttonCancel.Size = new System.Drawing.Size(67, 26);
            this._buttonCancel.TabIndex = 10;
            this._buttonCancel.Text = "Cancel";
            this._buttonCancel.UseVisualStyleBackColor = false;
            this._buttonCancel.Click += new System.EventHandler(this._buttonCancel_Click_1);
            // 
            // _menuNameTextBox
            // 
            this._menuNameTextBox.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._menuNameTextBox.Location = new System.Drawing.Point(101, 40);
            this._menuNameTextBox.Name = "_menuNameTextBox";
            this._menuNameTextBox.Size = new System.Drawing.Size(470, 22);
            this._menuNameTextBox.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(15, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 14);
            this.label2.TabIndex = 8;
            this.label2.Text = "Menu Name :";
            // 
            // _menuIdTextBox
            // 
            this._menuIdTextBox.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._menuIdTextBox.Location = new System.Drawing.Point(101, 12);
            this._menuIdTextBox.Name = "_menuIdTextBox";
            this._menuIdTextBox.Size = new System.Drawing.Size(470, 22);
            this._menuIdTextBox.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(34, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 14);
            this.label1.TabIndex = 6;
            this.label1.Text = "Menu ID :";
            // 
            // _myFlowLayoutPanel1
            // 
            this._myFlowLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this._myFlowLayoutPanel1.Controls.Add(this._buttonSave);
            this._myFlowLayoutPanel1.Controls.Add(this._buttonCancel);
            this._myFlowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._myFlowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this._myFlowLayoutPanel1.Location = new System.Drawing.Point(0, 74);
            this._myFlowLayoutPanel1.Name = "_myFlowLayoutPanel1";
            this._myFlowLayoutPanel1.Size = new System.Drawing.Size(583, 34);
            this._myFlowLayoutPanel1.TabIndex = 13;
            // 
            // _screen
            // 
            this._screen._isChange = false;
            this._screen.BackColor = System.Drawing.Color.Transparent;
            this._screen.Dock = System.Windows.Forms.DockStyle.Fill;
            this._screen.Location = new System.Drawing.Point(0, 0);
            this._screen.Name = "_screen";
            this._screen.Size = new System.Drawing.Size(583, 108);
            this._screen.TabIndex = 12;
            // 
            // _saveForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(583, 108);
            this.ControlBox = false;
            this.Controls.Add(this._myPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "_saveForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " Save ...";
            this._myPanel1.ResumeLayout(false);
            this._myPanel1.PerformLayout();
            this._myFlowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private MyLib._myPanel _myPanel1;
        private MyLib.VistaButton _buttonSave;
        private MyLib.VistaButton _buttonCancel;
        public System.Windows.Forms.TextBox _menuNameTextBox;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox _menuIdTextBox;
        private System.Windows.Forms.Label label1;
        private MyLib._myFlowLayoutPanel _myFlowLayoutPanel1;
        public _screen_save_report _screen;

    }
}