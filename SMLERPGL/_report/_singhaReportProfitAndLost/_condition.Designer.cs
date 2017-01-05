namespace SMLERPGL._report._singhaReportProfitAndLost
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
            this.components = new System.ComponentModel.Container();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this._buttonOk = new MyLib._myButton();
            this._headLabel = new MyLib._myShadowLabel(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this._myPanel1 = new MyLib._myPanel();
            this._screenJournalCondition1 = new SMLERPGL._report._screenReportCondition();
            this.flowLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this._myPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.Controls.Add(this._buttonOk);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 47);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.flowLayoutPanel1.Size = new System.Drawing.Size(286, 26);
            this.flowLayoutPanel1.TabIndex = 11;
            // 
            // _buttonOk
            // 
            this._buttonOk._drawNewMethod = false;
            this._buttonOk.AutoSize = true;
            this._buttonOk.BackColor = System.Drawing.Color.Transparent;
            this._buttonOk.ButtonText = "Process";
            this._buttonOk.Location = new System.Drawing.Point(208, 0);
            this._buttonOk.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this._buttonOk.myImage = global::SMLERPGL.Resource16x16.check;
            this._buttonOk.myTextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this._buttonOk.myUseVisualStyleBackColor = true;
            this._buttonOk.Name = "_buttonOk";
            this._buttonOk.Padding = new System.Windows.Forms.Padding(1);
            this._buttonOk.ResourceName = "";
            this._buttonOk.Size = new System.Drawing.Size(77, 24);
            this._buttonOk.TabIndex = 0;
            this._buttonOk.TabStop = false;
            this._buttonOk.Text = "Process";
            this._buttonOk.UseVisualStyleBackColor = false;
            this._buttonOk.Click += new System.EventHandler(this._buttonOk_Click);
            // 
            // _headLabel
            // 
            this._headLabel.Angle = 0F;
            this._headLabel.AutoSize = true;
            this._headLabel.BackColor = System.Drawing.Color.Transparent;
            this._headLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this._headLabel.DrawGradient = false;
            this._headLabel.EndColor = System.Drawing.Color.LightSkyBlue;
            this._headLabel.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._headLabel.ForeColor = System.Drawing.Color.LightSkyBlue;
            this._headLabel.Location = new System.Drawing.Point(4, 4);
            this._headLabel.Name = "_headLabel";
            this._headLabel.ResourceName = "";
            this._headLabel.ShadowColor = System.Drawing.Color.Black;
            this._headLabel.Size = new System.Drawing.Size(237, 29);
            this._headLabel.StartColor = System.Drawing.Color.White;
            this._headLabel.TabIndex = 2;
            this._headLabel.Text = "รายงานงบกำไรขาดทุน";
            this._headLabel.XOffset = 1F;
            this._headLabel.YOffset = 1F;
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.Controls.Add(this._screenJournalCondition1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 39);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(4);
            this.panel1.Size = new System.Drawing.Size(286, 8);
            this.panel1.TabIndex = 13;
            // 
            // _myPanel1
            // 
            this._myPanel1._switchTabAuto = false;
            this._myPanel1.AutoSize = true;
            this._myPanel1.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._myPanel1.Controls.Add(this._headLabel);
            this._myPanel1.CornerPicture = null;
            this._myPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this._myPanel1.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Location = new System.Drawing.Point(0, 0);
            this._myPanel1.Name = "_myPanel1";
            this._myPanel1.Padding = new System.Windows.Forms.Padding(4);
            this._myPanel1.Size = new System.Drawing.Size(286, 39);
            this._myPanel1.TabIndex = 12;
            // 
            // _screenJournalCondition1
            // 
            this._screenJournalCondition1._isChange = false;
            this._screenJournalCondition1._screenType = SMLERPGL._report._screenJournalConditionType.SinghaReportBalance;
            this._screenJournalCondition1.AutoSize = true;
            this._screenJournalCondition1.BackColor = System.Drawing.Color.Transparent;
            this._screenJournalCondition1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._screenJournalCondition1.Location = new System.Drawing.Point(4, 4);
            this._screenJournalCondition1.Name = "_screenJournalCondition1";
            this._screenJournalCondition1.Size = new System.Drawing.Size(278, 0);
            this._screenJournalCondition1.TabIndex = 0;
            // 
            // _condition
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(286, 195);
            this.ControlBox = false;
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this._myPanel1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "_condition";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "_condition";
            this.Load += new System.EventHandler(this._condition_Load);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this._myPanel1.ResumeLayout(false);
            this._myPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        public MyLib._myButton _buttonOk;
        private System.Windows.Forms.Panel panel1;
        public _screenReportCondition _screenJournalCondition1;
        private MyLib._myPanel _myPanel1;
        public MyLib._myShadowLabel _headLabel;
    }
}