namespace SMLERPGL._tax
{
    partial class _whtConditionForm
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
            this._myFlowLayoutPanel1 = new MyLib._myFlowLayoutPanel();
            this._bt_exit = new MyLib.VistaButton();
            this._bt_process = new MyLib.VistaButton();
            this._conditionScreenTop = new MyLib._myScreen();
            this._myPanel1 = new MyLib._myPanel();
            this._grouper1 = new MyLib._grouper();
            this._whereControl = new SMLReport._whereUserControl();
            this._myPanel2 = new MyLib._myPanel();
            this._label = new MyLib._myShadowLabel(this.components);
            this._myFlowLayoutPanel1.SuspendLayout();
            this._myPanel1.SuspendLayout();
            this._grouper1.SuspendLayout();
            this._myPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // _myFlowLayoutPanel1
            // 
            this._myFlowLayoutPanel1.AutoSize = true;
            this._myFlowLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this._myFlowLayoutPanel1.Controls.Add(this._bt_exit);
            this._myFlowLayoutPanel1.Controls.Add(this._bt_process);
            this._myFlowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._myFlowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this._myFlowLayoutPanel1.Location = new System.Drawing.Point(0, 486);
            this._myFlowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this._myFlowLayoutPanel1.Name = "_myFlowLayoutPanel1";
            this._myFlowLayoutPanel1.Size = new System.Drawing.Size(687, 30);
            this._myFlowLayoutPanel1.TabIndex = 10;
            // 
            // _bt_exit
            // 
            this._bt_exit.AutoSize = true;
            this._bt_exit.BackColor = System.Drawing.Color.Transparent;
            this._bt_exit.ButtonText = "Close";
            this._bt_exit.Location = new System.Drawing.Point(598, 3);
            this._bt_exit.myImage = global::SMLERPGL.Properties.Resources.error;
            this._bt_exit.Name = "_bt_exit";
            this._bt_exit.Size = new System.Drawing.Size(64, 24);
            this._bt_exit.TabIndex = 8;
            this._bt_exit.Text = "vistaButton2";
            this._bt_exit.UseVisualStyleBackColor = true;
            // 
            // _bt_process
            // 
            this._bt_process.AutoSize = true;
            this._bt_process.BackColor = System.Drawing.Color.Transparent;
            this._bt_process.ButtonText = "Process";
            this._bt_process.Location = new System.Drawing.Point(515, 3);
            this._bt_process.myImage = global::SMLERPGL.Properties.Resources.flash;
            this._bt_process.Name = "_bt_process";
            this._bt_process.Size = new System.Drawing.Size(77, 24);
            this._bt_process.TabIndex = 7;
            this._bt_process.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._bt_process.UseVisualStyleBackColor = true;
            // 
            // _conditionScreenTop
            // 
            this._conditionScreenTop._isChange = false;
            this._conditionScreenTop.AutoSize = true;
            this._conditionScreenTop.BackColor = System.Drawing.Color.Transparent;
            this._conditionScreenTop.Dock = System.Windows.Forms.DockStyle.Top;
            this._conditionScreenTop.Location = new System.Drawing.Point(0, 41);
            this._conditionScreenTop.Name = "_conditionScreenTop";
            this._conditionScreenTop.Size = new System.Drawing.Size(687, 0);
            this._conditionScreenTop.TabIndex = 13;
            // 
            // _myPanel1
            // 
            this._myPanel1._switchTabAuto = false;
            this._myPanel1.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Controls.Add(this._grouper1);
            this._myPanel1.Controls.Add(this._myFlowLayoutPanel1);
            this._myPanel1.CornerPicture = null;
            this._myPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myPanel1.EndColor = System.Drawing.Color.SkyBlue;
            this._myPanel1.Location = new System.Drawing.Point(0, 41);
            this._myPanel1.Name = "_myPanel1";
            this._myPanel1.Size = new System.Drawing.Size(687, 516);
            this._myPanel1.TabIndex = 2;
            // 
            // _grouper1
            // 
            this._grouper1.BackgroundColor = System.Drawing.Color.Transparent;
            this._grouper1.BackgroundGradientColor = System.Drawing.SystemColors.ControlLight;
            this._grouper1.BackgroundGradientMode = MyLib._grouper.GroupBoxGradientMode.None;
            this._grouper1.BorderColor = System.Drawing.Color.White;
            this._grouper1.BorderThickness = 1F;
            this._grouper1.Controls.Add(this._whereControl);
            this._grouper1.CustomGroupBoxColor = System.Drawing.Color.White;
            this._grouper1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._grouper1.GroupImage = null;
            this._grouper1.GroupTitle = "";
            this._grouper1.Location = new System.Drawing.Point(0, 0);
            this._grouper1.Name = "_grouper1";
            this._grouper1.Padding = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this._grouper1.PaintGroupBox = false;
            this._grouper1.RoundCorners = 10;
            this._grouper1.ShadowColor = System.Drawing.Color.DarkGray;
            this._grouper1.ShadowControl = false;
            this._grouper1.ShadowThickness = 3;
            this._grouper1.Size = new System.Drawing.Size(687, 486);
            this._grouper1.TabIndex = 11;
            // 
            // _whereControl
            // 
            this._whereControl.AutoSize = true;
            this._whereControl.BackColor = System.Drawing.Color.Transparent;
            this._whereControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this._whereControl.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._whereControl.Location = new System.Drawing.Point(6, 5);
            this._whereControl.Name = "_whereControl";
            this._whereControl.Size = new System.Drawing.Size(675, 476);
            this._whereControl.TabIndex = 12;
            // 
            // _myPanel2
            // 
            this._myPanel2._switchTabAuto = false;
            this._myPanel2.AutoSize = true;
            this._myPanel2.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._myPanel2.Controls.Add(this._label);
            this._myPanel2.CornerPicture = null;
            this._myPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this._myPanel2.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel2.Location = new System.Drawing.Point(0, 0);
            this._myPanel2.Name = "_myPanel2";
            this._myPanel2.Padding = new System.Windows.Forms.Padding(5);
            this._myPanel2.Size = new System.Drawing.Size(687, 41);
            this._myPanel2.TabIndex = 14;
            // 
            // _label
            // 
            this._label.Angle = 0F;
            this._label.AutoSize = true;
            this._label.BackColor = System.Drawing.Color.Transparent;
            this._label.DrawGradient = false;
            this._label.EndColor = System.Drawing.Color.LightSkyBlue;
            this._label.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._label.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this._label.Location = new System.Drawing.Point(5, 5);
            this._label.Name = "_label";
            this._label.ShadowColor = System.Drawing.Color.Gray;
            this._label.Size = new System.Drawing.Size(77, 29);
            this._label.StartColor = System.Drawing.Color.White;
            this._label.TabIndex = 1;
            this._label.Text = "Label";
            this._label.XOffset = 1F;
            this._label.YOffset = 1F;
            // 
            // _vatConditionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Azure;
            this.ClientSize = new System.Drawing.Size(687, 557);
            this.Controls.Add(this._myPanel1);
            this.Controls.Add(this._conditionScreenTop);
            this.Controls.Add(this._myPanel2);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "_vatConditionForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "_vatConditionForm";
            this._myFlowLayoutPanel1.ResumeLayout(false);
            this._myFlowLayoutPanel1.PerformLayout();
            this._myPanel1.ResumeLayout(false);
            this._myPanel1.PerformLayout();
            this._grouper1.ResumeLayout(false);
            this._grouper1.PerformLayout();
            this._myPanel2.ResumeLayout(false);
            this._myPanel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MyLib.VistaButton _bt_exit;
        private MyLib._myFlowLayoutPanel _myFlowLayoutPanel1;
        private MyLib.VistaButton _bt_process;
        private MyLib._myPanel _myPanel1;
        public MyLib._myScreen _conditionScreenTop;
        private MyLib._myPanel _myPanel2;
        public MyLib._myShadowLabel _label;
        private MyLib._grouper _grouper1;
        public SMLReport._whereUserControl _whereControl;
    }
}