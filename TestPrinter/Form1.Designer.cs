namespace TestPrinter
{
    partial class Form1
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
            this._printButton = new System.Windows.Forms.Button();
            this._textBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // _printButton
            // 
            this._printButton.Location = new System.Drawing.Point(456, 453);
            this._printButton.Name = "_printButton";
            this._printButton.Size = new System.Drawing.Size(75, 23);
            this._printButton.TabIndex = 0;
            this._printButton.Text = "Print";
            this._printButton.UseVisualStyleBackColor = true;
            this._printButton.Click += new System.EventHandler(this._printButton_Click);
            // 
            // _textBox
            // 
            this._textBox.Location = new System.Drawing.Point(13, 13);
            this._textBox.Multiline = true;
            this._textBox.Name = "_textBox";
            this._textBox.Size = new System.Drawing.Size(518, 434);
            this._textBox.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(543, 488);
            this.Controls.Add(this._textBox);
            this.Controls.Add(this._printButton);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button _printButton;
        private System.Windows.Forms.TextBox _textBox;
    }
}

