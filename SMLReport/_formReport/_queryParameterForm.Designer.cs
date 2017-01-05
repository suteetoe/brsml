namespace SMLReport._formReport
{
    partial class _queryParameterForm
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
            this._myPanel1 = new MyLib._myPanel();
            this._myFlowLayoutPanel1 = new MyLib._myFlowLayoutPanel();
            this._myShadowLabel1 = new MyLib._myShadowLabel(this.components);
            this._myFlowLayoutPanel2 = new MyLib._myFlowLayoutPanel();
            this._bt_process = new MyLib.VistaButton();
            this._conditionScreen = new MyLib._myScreen();
            this._myPanel1.SuspendLayout();
            this._myFlowLayoutPanel1.SuspendLayout();
            this._myFlowLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // _myPanel1
            // 
            this._myPanel1._switchTabAuto = false;
            this._myPanel1.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Controls.Add(this._conditionScreen);
            this._myPanel1.Controls.Add(this._myFlowLayoutPanel2);
            this._myPanel1.Controls.Add(this._myFlowLayoutPanel1);
            this._myPanel1.CornerPicture = null;
            this._myPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myPanel1.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Location = new System.Drawing.Point(2, 2);
            this._myPanel1.Name = "_myPanel1";
            this._myPanel1.Size = new System.Drawing.Size(436, 395);
            this._myPanel1.TabIndex = 0;
            // 
            // _myFlowLayoutPanel1
            // 
            this._myFlowLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this._myFlowLayoutPanel1.Controls.Add(this._myShadowLabel1);
            this._myFlowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this._myFlowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this._myFlowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this._myFlowLayoutPanel1.Name = "_myFlowLayoutPanel1";
            this._myFlowLayoutPanel1.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this._myFlowLayoutPanel1.Size = new System.Drawing.Size(436, 43);
            this._myFlowLayoutPanel1.TabIndex = 0;
            // 
            // _myShadowLabel1
            // 
            this._myShadowLabel1.Angle = 0F;
            this._myShadowLabel1.AutoSize = true;
            this._myShadowLabel1.EndColor = System.Drawing.Color.Transparent;
            this._myShadowLabel1.Font = new System.Drawing.Font("Tahoma", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._myShadowLabel1.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this._myShadowLabel1.Location = new System.Drawing.Point(176, 5);
            this._myShadowLabel1.Name = "_myShadowLabel1";
            this._myShadowLabel1.ShadowColor = System.Drawing.Color.DarkGray;
            this._myShadowLabel1.Size = new System.Drawing.Size(257, 33);
            this._myShadowLabel1.StartColor = System.Drawing.Color.Transparent;
            this._myShadowLabel1.TabIndex = 0;
            this._myShadowLabel1.Text = "Query Conditions.";
            this._myShadowLabel1.XOffset = 1F;
            this._myShadowLabel1.YOffset = 1F;
            // 
            // _myFlowLayoutPanel2
            // 
            this._myFlowLayoutPanel2.BackColor = System.Drawing.Color.Transparent;
            this._myFlowLayoutPanel2.Controls.Add(this._bt_process);
            this._myFlowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._myFlowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this._myFlowLayoutPanel2.Location = new System.Drawing.Point(0, 364);
            this._myFlowLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this._myFlowLayoutPanel2.Name = "_myFlowLayoutPanel2";
            this._myFlowLayoutPanel2.Size = new System.Drawing.Size(436, 31);
            this._myFlowLayoutPanel2.TabIndex = 1;
            // 
            // _bt_process
            // 
            this._bt_process.BackColor = System.Drawing.Color.Transparent;
            this._bt_process.ButtonText = "Process";
            this._bt_process.Image = global::SMLReport.Properties.Resources.flash;
            this._bt_process.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this._bt_process.Location = new System.Drawing.Point(358, 3);
            this._bt_process.myImage = global::SMLReport.Properties.Resources.flash;
            this._bt_process.Name = "_bt_process";
            this._bt_process.Size = new System.Drawing.Size(75, 25);
            this._bt_process.TabIndex = 9;
            this._bt_process.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._bt_process.UseVisualStyleBackColor = true;
            this._bt_process.Click += new System.EventHandler(this._bt_process_Click);
            // 
            // _conditionScreen
            // 
            this._conditionScreen._isChange = false;
            this._conditionScreen.BackColor = System.Drawing.Color.Transparent;
            this._conditionScreen.Dock = System.Windows.Forms.DockStyle.Fill;
            this._conditionScreen.Location = new System.Drawing.Point(0, 43);
            this._conditionScreen.Name = "_conditionScreen";
            this._conditionScreen.Size = new System.Drawing.Size(436, 321);
            this._conditionScreen.TabIndex = 2;
            // 
            // _queryParameterForm
            // 
            this._colorBegin = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(440, 399);
            this.ControlBox = false;
            this.Controls.Add(this._myPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "_queryParameterForm";
            this.Padding = new System.Windows.Forms.Padding(2);
            this.Text = "Query Condition";
            this._myPanel1.ResumeLayout(false);
            this._myFlowLayoutPanel1.ResumeLayout(false);
            this._myFlowLayoutPanel1.PerformLayout();
            this._myFlowLayoutPanel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private MyLib._myPanel _myPanel1;
        private MyLib._myFlowLayoutPanel _myFlowLayoutPanel1;
        private MyLib._myShadowLabel _myShadowLabel1;
        private MyLib._myFlowLayoutPanel _myFlowLayoutPanel2;
        private MyLib.VistaButton _bt_process;
        public MyLib._myScreen _conditionScreen;

    }
}