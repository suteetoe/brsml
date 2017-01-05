namespace SMLERPReport.Cheque_Card
{
    partial class condition_cheque_card
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
            this._grouper1 = new MyLib._grouper();
            this._condition_cheque_card1 = new SMLERPReport.Cheque_Card._condition_cheque_card_screen();
            this._panel_buttom = new System.Windows.Forms.Panel();
            this._vistaButton_exit = new MyLib.VistaButton();
            this._vistaButton_process = new MyLib.VistaButton();
            this._grouper1.SuspendLayout();
            this._panel_buttom.SuspendLayout();
            this.SuspendLayout();
            // 
            // _grouper1
            // 
            this._grouper1.BackgroundColor = System.Drawing.Color.LightCyan;
            this._grouper1.BackgroundGradientColor = System.Drawing.Color.Transparent;
            this._grouper1.BackgroundGradientMode = MyLib._grouper.GroupBoxGradientMode.None;
            this._grouper1.BorderColor = System.Drawing.Color.Black;
            this._grouper1.BorderThickness = 1F;
            this._grouper1.Controls.Add(this._condition_cheque_card1);
            this._grouper1.Controls.Add(this._panel_buttom);
            this._grouper1.CustomGroupBoxColor = System.Drawing.Color.White;
            this._grouper1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._grouper1.GroupImage = null;
            this._grouper1.GroupTitle = "เงื่อนไขการพิมพ์รายงานมาตรฐาน";
            this._grouper1.Location = new System.Drawing.Point(0, 0);
            this._grouper1.Name = "_grouper1";
            this._grouper1.Padding = new System.Windows.Forms.Padding(20, 30, 20, 30);
            this._grouper1.PaintGroupBox = false;
            this._grouper1.RoundCorners = 10;
            this._grouper1.ShadowColor = System.Drawing.Color.DarkGray;
            this._grouper1.ShadowControl = false;
            this._grouper1.ShadowThickness = 3;
            this._grouper1.Size = new System.Drawing.Size(634, 192);
            this._grouper1.TabIndex = 0;
            // 
            // _condition_cheque_card1
            // 
            this._condition_cheque_card1._isChange = false;
            this._condition_cheque_card1.BackColor = System.Drawing.Color.Transparent;
            this._condition_cheque_card1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._condition_cheque_card1.Location = new System.Drawing.Point(20, 30);
            this._condition_cheque_card1.Name = "_condition_cheque_card1";
            this._condition_cheque_card1.Size = new System.Drawing.Size(594, 89);
            this._condition_cheque_card1.TabIndex = 0;
            // 
            // _panel_buttom
            // 
            this._panel_buttom.Controls.Add(this._vistaButton_exit);
            this._panel_buttom.Controls.Add(this._vistaButton_process);
            this._panel_buttom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._panel_buttom.Location = new System.Drawing.Point(20, 119);
            this._panel_buttom.Name = "_panel_buttom";
            this._panel_buttom.Size = new System.Drawing.Size(594, 43);
            this._panel_buttom.TabIndex = 1;
            // 
            // _vistaButton_exit
            // 
            this._vistaButton_exit.BackColor = System.Drawing.Color.Transparent;
            this._vistaButton_exit.ButtonText = "ยกเลิก";
            this._vistaButton_exit.Location = new System.Drawing.Point(510, 6);
            this._vistaButton_exit.Name = "_vistaButton_exit";
            this._vistaButton_exit.Size = new System.Drawing.Size(70, 32);
            this._vistaButton_exit.TabIndex = 2;
            this._vistaButton_exit.Text = "vistaButton1";
            this._vistaButton_exit.UseVisualStyleBackColor = true;
            // 
            // _vistaButton_process
            // 
            this._vistaButton_process.BackColor = System.Drawing.Color.Transparent;
            this._vistaButton_process.ButtonText = "ตกลง";
            this._vistaButton_process.Location = new System.Drawing.Point(434, 6);
            this._vistaButton_process.Name = "_vistaButton_process";
            this._vistaButton_process.Size = new System.Drawing.Size(70, 32);
            this._vistaButton_process.TabIndex = 1;
            this._vistaButton_process.Text = "Process";
            this._vistaButton_process.UseVisualStyleBackColor = true;
            // 
            // condition_cheque_card
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightCyan;
            this.ClientSize = new System.Drawing.Size(634, 192);
            this.Controls.Add(this._grouper1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "condition_cheque_card";
            this.Text = "เงื่อนไขเช็ค / บัตร";
            this._grouper1.ResumeLayout(false);
            this._panel_buttom.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private MyLib._grouper _grouper1;
        private System.Windows.Forms.Panel _panel_buttom;
        private MyLib.VistaButton _vistaButton_exit;
        private MyLib.VistaButton _vistaButton_process;
        public _condition_cheque_card_screen _condition_cheque_card1;


    }
}