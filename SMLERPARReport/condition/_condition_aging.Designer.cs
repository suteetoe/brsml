namespace SMLERPARAPReport.condition
{
    partial class _condition_aging
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
            this._grouperCondition = new MyLib._grouper();
            this._myFlowLayoutPanel1 = new MyLib._myFlowLayoutPanel();
            this._bt_exit = new MyLib.VistaButton();
            this._bt_process = new MyLib.VistaButton();
            this._myPanel1.SuspendLayout();
            this._myFlowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _myPanel1
            // 
            this._myPanel1._switchTabAuto = false;
            this._myPanel1.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Controls.Add(this._grouperCondition);
            this._myPanel1.Controls.Add(this._myFlowLayoutPanel1);
            this._myPanel1.CornerPicture = null;
            this._myPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myPanel1.EndColor = System.Drawing.Color.SkyBlue;
            this._myPanel1.Location = new System.Drawing.Point(0, 0);
            this._myPanel1.Name = "_myPanel1";
            this._myPanel1.Padding = new System.Windows.Forms.Padding(3);
            this._myPanel1.Size = new System.Drawing.Size(749, 234);
            this._myPanel1.TabIndex = 2;
            // 
            // _grouperCondition
            // 
            this._grouperCondition.AutoSize = true;
            this._grouperCondition.BackgroundColor = System.Drawing.Color.Transparent;
            this._grouperCondition.BackgroundGradientColor = System.Drawing.SystemColors.ControlLight;
            this._grouperCondition.BackgroundGradientMode = MyLib._grouper.GroupBoxGradientMode.None;
            this._grouperCondition.BorderColor = System.Drawing.Color.White;
            this._grouperCondition.BorderThickness = 1F;
            this._grouperCondition.CustomGroupBoxColor = System.Drawing.Color.White;
            this._grouperCondition.Dock = System.Windows.Forms.DockStyle.Fill;
            this._grouperCondition.GroupImage = null;
            this._grouperCondition.GroupTitle = "";
            this._grouperCondition.Location = new System.Drawing.Point(3, 3);
            this._grouperCondition.Name = "_grouperCondition";
            this._grouperCondition.Padding = new System.Windows.Forms.Padding(5);
            this._grouperCondition.PaintGroupBox = false;
            this._grouperCondition.RoundCorners = 10;
            this._grouperCondition.ShadowColor = System.Drawing.Color.DarkGray;
            this._grouperCondition.ShadowControl = false;
            this._grouperCondition.ShadowThickness = 3;
            this._grouperCondition.Size = new System.Drawing.Size(743, 198);
            this._grouperCondition.TabIndex = 15;
            // 
            // _myFlowLayoutPanel1
            // 
            this._myFlowLayoutPanel1.AutoSize = true;
            this._myFlowLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this._myFlowLayoutPanel1.Controls.Add(this._bt_exit);
            this._myFlowLayoutPanel1.Controls.Add(this._bt_process);
            this._myFlowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._myFlowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this._myFlowLayoutPanel1.Location = new System.Drawing.Point(3, 201);
            this._myFlowLayoutPanel1.Name = "_myFlowLayoutPanel1";
            this._myFlowLayoutPanel1.Size = new System.Drawing.Size(743, 30);
            this._myFlowLayoutPanel1.TabIndex = 13;
            // 
            // _bt_exit
            // 
            this._bt_exit.AutoSize = true;
            this._bt_exit.BackColor = System.Drawing.Color.Transparent;
            this._bt_exit.ButtonText = "ESC = Exit";
            this._bt_exit.Location = new System.Drawing.Point(664, 3);
            this._bt_exit.Name = "_bt_exit";
            this._bt_exit.Size = new System.Drawing.Size(74, 24);
            this._bt_exit.TabIndex = 10;
            this._bt_exit.Text = "vistaButton2";
            this._bt_exit.UseVisualStyleBackColor = true;
            this._bt_exit.Click += new System.EventHandler(this._bt_exit_Click);
            // 
            // _bt_process
            // 
            this._bt_process.AutoSize = true;
            this._bt_process.BackColor = System.Drawing.Color.Transparent;
            this._bt_process.ButtonText = "F11 = Process";
            this._bt_process.Location = new System.Drawing.Point(566, 3);
            this._bt_process.Name = "_bt_process";
            this._bt_process.Size = new System.Drawing.Size(92, 24);
            this._bt_process.TabIndex = 9;
            this._bt_process.UseVisualStyleBackColor = true;
            this._bt_process.Click += new System.EventHandler(this._bt_process_Click);
            // 
            // _condition_aging
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(749, 234);
            this.Controls.Add(this._myPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.ImeMode = System.Windows.Forms.ImeMode.AlphaFull;
            this.Name = "_condition_aging";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this._myPanel1.ResumeLayout(false);
            this._myPanel1.PerformLayout();
            this._myFlowLayoutPanel1.ResumeLayout(false);
            this._myFlowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private MyLib._myPanel _myPanel1;
        private MyLib._grouper _grouperCondition;
        private MyLib._myFlowLayoutPanel _myFlowLayoutPanel1;
        private MyLib.VistaButton _bt_exit;
        private MyLib.VistaButton _bt_process;
    }
}