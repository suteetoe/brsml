namespace SMLPOSControl._food
{
    partial class _orderSuggestRemarkForm
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
            this._remarkTextBox = new MyLib._myTextBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this._buttonSave = new MyLib.VistaButton();
            this._buttonClear = new MyLib.VistaButton();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _myFlowLayoutPanel1
            // 
            this._myFlowLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this._myFlowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myFlowLayoutPanel1.Location = new System.Drawing.Point(0, 27);
            this._myFlowLayoutPanel1.Name = "_myFlowLayoutPanel1";
            this._myFlowLayoutPanel1.Size = new System.Drawing.Size(661, 446);
            this._myFlowLayoutPanel1.TabIndex = 0;
            // 
            // _remarkTextBox
            // 
            this._remarkTextBox._column = 0;
            this._remarkTextBox._defaultBackGround = System.Drawing.Color.White;
            this._remarkTextBox._emtry = true;
            this._remarkTextBox._enterToTab = false;
            this._remarkTextBox._icon = false;
            this._remarkTextBox._iconNumber = 1;
            this._remarkTextBox._isChange = false;
            this._remarkTextBox._isQuery = true;
            this._remarkTextBox._isSearch = false;
            this._remarkTextBox._isTime = false;
            this._remarkTextBox._labelName = "";
            this._remarkTextBox._maxColumn = 0;
            this._remarkTextBox._name = null;
            this._remarkTextBox._row = 0;
            this._remarkTextBox._rowCount = 0;
            this._remarkTextBox._textFirst = "";
            this._remarkTextBox._textLast = "";
            this._remarkTextBox._textSecond = "";
            this._remarkTextBox._upperCase = false;
            this._remarkTextBox.BackColor = System.Drawing.SystemColors.Window;
            this._remarkTextBox.Dock = System.Windows.Forms.DockStyle.Top;
            this._remarkTextBox.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._remarkTextBox.ForeColor = System.Drawing.Color.Black;
            this._remarkTextBox.IsUpperCase = false;
            this._remarkTextBox.Location = new System.Drawing.Point(0, 0);
            this._remarkTextBox.Margin = new System.Windows.Forms.Padding(0);
            this._remarkTextBox.MaxLength = 0;
            this._remarkTextBox.Name = "_remarkTextBox";
            this._remarkTextBox.ShowIcon = false;
            this._remarkTextBox.Size = new System.Drawing.Size(661, 27);
            this._remarkTextBox.TabIndex = 0;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.Controls.Add(this._buttonSave);
            this.flowLayoutPanel1.Controls.Add(this._buttonClear);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 435);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(661, 38);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // _buttonSave
            // 
            this._buttonSave._drawNewMethod = false;
            this._buttonSave.BackColor = System.Drawing.Color.Transparent;
            this._buttonSave.ButtonText = "Save";
            this._buttonSave.Location = new System.Drawing.Point(558, 3);
            this._buttonSave.Name = "_buttonSave";
            this._buttonSave.Size = new System.Drawing.Size(100, 32);
            this._buttonSave.TabIndex = 0;
            this._buttonSave.Text = "vistaButton1";
            this._buttonSave.UseVisualStyleBackColor = false;
            this._buttonSave.Click += new System.EventHandler(this._buttonSave_Click);
            // 
            // _buttonClear
            // 
            this._buttonClear._drawNewMethod = false;
            this._buttonClear.BackColor = System.Drawing.Color.Transparent;
            this._buttonClear.ButtonText = "Clear";
            this._buttonClear.Location = new System.Drawing.Point(452, 3);
            this._buttonClear.Name = "_buttonClear";
            this._buttonClear.Size = new System.Drawing.Size(100, 32);
            this._buttonClear.TabIndex = 1;
            this._buttonClear.Text = "vistaButton2";
            this._buttonClear.UseVisualStyleBackColor = false;
            this._buttonClear.Click += new System.EventHandler(this._buttonClear_Click);
            // 
            // _orderSuggestRemarkForm
            // 
            this._colorBegin = System.Drawing.Color.White;
            this._colorEnd = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(661, 473);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this._myFlowLayoutPanel1);
            this.Controls.Add(this._remarkTextBox);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "_orderSuggestRemarkForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "หมายเหตุ";
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MyLib._myFlowLayoutPanel _myFlowLayoutPanel1;
        private MyLib._myTextBox _remarkTextBox;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private MyLib.VistaButton _buttonSave;
        private MyLib.VistaButton _buttonClear;
    }
}