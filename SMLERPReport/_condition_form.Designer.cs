namespace SMLERPReport
{
    partial class _condition_form
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
            this._myFlowLayoutPanel1 = new MyLib._myFlowLayoutPanel();
            this._button_exit = new MyLib.VistaButton();
            this._button_process = new MyLib.VistaButton();
            this._grouper1 = new MyLib._grouper();
            this._myFlowLayoutPanel1.SuspendLayout();
            this._grouper1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _myFlowLayoutPanel1
            // 
            this._myFlowLayoutPanel1.AutoSize = true;
            this._myFlowLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this._myFlowLayoutPanel1.Controls.Add(this._button_exit);
            this._myFlowLayoutPanel1.Controls.Add(this._button_process);
            this._myFlowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._myFlowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this._myFlowLayoutPanel1.Location = new System.Drawing.Point(10, 446);
            this._myFlowLayoutPanel1.Name = "_myFlowLayoutPanel1";
            this._myFlowLayoutPanel1.Size = new System.Drawing.Size(667, 38);
            this._myFlowLayoutPanel1.TabIndex = 8;
            // 
            // _button_exit
            // 
            this._button_exit._drawNewMethod = false;
            this._button_exit.AutoSize = true;
            this._button_exit.BackColor = System.Drawing.Color.Transparent;
            this._button_exit.ButtonText = "Exit (ESC)";
            this._button_exit.Location = new System.Drawing.Point(564, 3);
            this._button_exit.Name = "_button_exit";
            this._button_exit.Size = new System.Drawing.Size(100, 32);
            this._button_exit.TabIndex = 6;
            this._button_exit.Text = "vistaButton2";
            this._button_exit.UseVisualStyleBackColor = true;
            // 
            // _button_process
            // 
            this._button_process._drawNewMethod = false;
            this._button_process.AutoSize = true;
            this._button_process.BackColor = System.Drawing.Color.Transparent;
            this._button_process.ButtonText = "Process (F11)";
            this._button_process.Location = new System.Drawing.Point(458, 3);
            this._button_process.Name = "_button_process";
            this._button_process.Size = new System.Drawing.Size(100, 32);
            this._button_process.TabIndex = 5;
            this._button_process.UseVisualStyleBackColor = true;
            // 
            // _grouper1
            // 
            this._grouper1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._grouper1.BackgroundGradientColor = System.Drawing.Color.White;
            this._grouper1.BackgroundGradientMode = MyLib._grouper.GroupBoxGradientMode.None;
            this._grouper1.BorderColor = System.Drawing.Color.Black;
            this._grouper1.BorderThickness = 1F;
            this._grouper1.Controls.Add(this._myFlowLayoutPanel1);
            this._grouper1.CustomGroupBoxColor = System.Drawing.Color.White;
            this._grouper1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._grouper1.GroupImage = null;
            this._grouper1.GroupTitle = "เงื่อนไขการพิมพ์รายงานมาตรฐาน";
            this._grouper1.Location = new System.Drawing.Point(0, 0);
            this._grouper1.Name = "_grouper1";
            this._grouper1.Padding = new System.Windows.Forms.Padding(10, 25, 10, 10);
            this._grouper1.PaintGroupBox = true;
            this._grouper1.RoundCorners = 10;
            this._grouper1.ShadowColor = System.Drawing.Color.DarkGray;
            this._grouper1.ShadowControl = false;
            this._grouper1.ShadowThickness = 3;
            this._grouper1.Size = new System.Drawing.Size(687, 494);
            this._grouper1.TabIndex = 4;
            // 
            // _condition_form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(687, 494);
            this.Controls.Add(this._grouper1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.KeyPreview = true;
            this.Name = "_condition_form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "_condition_form";
            this._myFlowLayoutPanel1.ResumeLayout(false);
            this._myFlowLayoutPanel1.PerformLayout();
            this._grouper1.ResumeLayout(false);
            this._grouper1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private MyLib._myFlowLayoutPanel _myFlowLayoutPanel1;
        private MyLib.VistaButton _button_exit;
        public MyLib._grouper _grouper1;
        public MyLib.VistaButton _button_process;

    }
}