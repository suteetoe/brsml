namespace SMLERPReport.so
{
    partial class _condition
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
            this._condition_so1 = new SMLERPReport.so._condition_so();
            this._myFlowLayoutPanel1 = new MyLib._myFlowLayoutPanel();
            this._bt_exit = new MyLib.VistaButton();
            this._bt_process = new MyLib.VistaButton();
            this._whereControl = new SMLReport._whereUserControl();
            this._myPanel1.SuspendLayout();
            this._myFlowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _myPanel1
            // 
            this._myPanel1.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Controls.Add(this._whereControl);
            this._myPanel1.Controls.Add(this._myFlowLayoutPanel1);
            this._myPanel1.Controls.Add(this._condition_so1);
            this._myPanel1.CornerPicture = null;
            this._myPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myPanel1.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Location = new System.Drawing.Point(5, 5);
            this._myPanel1.Name = "_myPanel1";
            this._myPanel1.Size = new System.Drawing.Size(604, 509);
            this._myPanel1.TabIndex = 0;
            // 
            // _condition_so1
            // 
            this._condition_so1._isChange = false;
            this._condition_so1.BackColor = System.Drawing.Color.Transparent;
            this._condition_so1.Dock = System.Windows.Forms.DockStyle.Top;
            this._condition_so1.Location = new System.Drawing.Point(0, 0);
            this._condition_so1.Name = "_condition_so1";
            this._condition_so1.Size = new System.Drawing.Size(604, 109);
            this._condition_so1.TabIndex = 0;
            // 
            // _myFlowLayoutPanel1
            // 
            this._myFlowLayoutPanel1.AutoSize = true;
            this._myFlowLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this._myFlowLayoutPanel1.Controls.Add(this._bt_exit);
            this._myFlowLayoutPanel1.Controls.Add(this._bt_process);
            this._myFlowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._myFlowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this._myFlowLayoutPanel1.Location = new System.Drawing.Point(0, 479);
            this._myFlowLayoutPanel1.Name = "_myFlowLayoutPanel1";
            this._myFlowLayoutPanel1.Size = new System.Drawing.Size(604, 30);
            this._myFlowLayoutPanel1.TabIndex = 2;
            // 
            // _bt_exit
            // 
            this._bt_exit.BackColor = System.Drawing.Color.Transparent;
            this._bt_exit.ButtonText = "ESC = Exit";
            this._bt_exit.Location = new System.Drawing.Point(515, 3);
            this._bt_exit.Name = "_bt_exit";
            this._bt_exit.Size = new System.Drawing.Size(86, 24);
            this._bt_exit.TabIndex = 8;
            this._bt_exit.Text = "vistaButton2";
            this._bt_exit.UseVisualStyleBackColor = true;
            // 
            // _bt_process
            // 
            this._bt_process.BackColor = System.Drawing.Color.Transparent;
            this._bt_process.ButtonText = "F11 = Process";
            this._bt_process.Location = new System.Drawing.Point(412, 3);
            this._bt_process.Name = "_bt_process";
            this._bt_process.Size = new System.Drawing.Size(97, 24);
            this._bt_process.TabIndex = 7;
            this._bt_process.UseVisualStyleBackColor = true;
            // 
            // _whereControl
            // 
            this._whereControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this._whereControl.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._whereControl.Location = new System.Drawing.Point(0, 109);
            this._whereControl.Name = "_whereControl";
            this._whereControl.Size = new System.Drawing.Size(604, 370);
            this._whereControl.TabIndex = 12;
            // 
            // _condition
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(614, 519);
            this.Controls.Add(this._myPanel1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "_condition";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Text = "_condition";
            this._myPanel1.ResumeLayout(false);
            this._myPanel1.PerformLayout();
            this._myFlowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private MyLib._myPanel _myPanel1;
        private MyLib.VistaButton _bt_exit;
        private MyLib.VistaButton _bt_process;
        private MyLib._myFlowLayoutPanel _myFlowLayoutPanel1;
        public _condition_so _condition_so1;
        public SMLReport._whereUserControl _whereControl;
    }
}