namespace SMLERPGL
{
    partial class GenDataForm
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
            this._buttonStart = new System.Windows.Forms.Button();
            this._textBoxDate = new System.Windows.Forms.TextBox();
            this._textBoxCount = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this._textBoxDays = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this._dateTimePicker = new System.Windows.Forms.DateTimePicker();
            this._textBoxPerDay = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // _buttonStart
            // 
            this._buttonStart.Location = new System.Drawing.Point(83, 11);
            this._buttonStart.Name = "_buttonStart";
            this._buttonStart.Size = new System.Drawing.Size(138, 23);
            this._buttonStart.TabIndex = 0;
            this._buttonStart.Text = "Start";
            this._buttonStart.UseVisualStyleBackColor = true;
            this._buttonStart.Click += new System.EventHandler(this._buttonStart_Click);
            // 
            // _textBoxDate
            // 
            this._textBoxDate.Location = new System.Drawing.Point(12, 131);
            this._textBoxDate.Name = "_textBoxDate";
            this._textBoxDate.Size = new System.Drawing.Size(284, 20);
            this._textBoxDate.TabIndex = 1;
            // 
            // _textBoxCount
            // 
            this._textBoxCount.Location = new System.Drawing.Point(12, 157);
            this._textBoxCount.Name = "_textBoxCount";
            this._textBoxCount.Size = new System.Drawing.Size(284, 20);
            this._textBoxCount.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Build Days :";
            // 
            // _textBoxDays
            // 
            this._textBoxDays.Location = new System.Drawing.Point(96, 40);
            this._textBoxDays.Name = "_textBoxDays";
            this._textBoxDays.Size = new System.Drawing.Size(200, 20);
            this._textBoxDays.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 97);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Start Date :";
            // 
            // _dateTimePicker
            // 
            this._dateTimePicker.Location = new System.Drawing.Point(96, 91);
            this._dateTimePicker.Name = "_dateTimePicker";
            this._dateTimePicker.Size = new System.Drawing.Size(200, 20);
            this._dateTimePicker.TabIndex = 6;
            // 
            // _textBoxPerDay
            // 
            this._textBoxPerDay.Location = new System.Drawing.Point(96, 66);
            this._textBoxPerDay.Name = "_textBoxPerDay";
            this._textBoxPerDay.Size = new System.Drawing.Size(200, 20);
            this._textBoxPerDay.TabIndex = 7;
            this._textBoxPerDay.Text = "5000";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Records/Day :";
            // 
            // GenDataForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(306, 195);
            this.Controls.Add(this.label3);
            this.Controls.Add(this._textBoxPerDay);
            this.Controls.Add(this._dateTimePicker);
            this.Controls.Add(this.label2);
            this.Controls.Add(this._textBoxDays);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._textBoxCount);
            this.Controls.Add(this._textBoxDate);
            this.Controls.Add(this._buttonStart);
            this.Name = "GenDataForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GenDataForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button _buttonStart;
        private System.Windows.Forms.TextBox _textBoxDate;
        private System.Windows.Forms.TextBox _textBoxCount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox _textBoxDays;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker _dateTimePicker;
        private System.Windows.Forms.TextBox _textBoxPerDay;
        private System.Windows.Forms.Label label3;
    }
}