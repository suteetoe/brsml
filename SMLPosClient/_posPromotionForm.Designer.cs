namespace SMLPosClient
{
    partial class _posPromotionForm
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
            this.button2 = new System.Windows.Forms.Button();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this._myDataGridView1 = new SMLPosClient._myDataGridView();
            this._barcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this._myDataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(275, 241);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(74, 189);
            this.button2.TabIndex = 4;
            this.button2.Text = "ตรวจสอบของแถม";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // textBox3
            // 
            this.textBox3.Enabled = false;
            this.textBox3.Location = new System.Drawing.Point(355, 241);
            this.textBox3.Multiline = true;
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(257, 189);
            this.textBox3.TabIndex = 5;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(64, 241);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 244);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "barcode";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 270);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(21, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "qty";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(64, 267);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 20);
            this.textBox2.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(195, 244);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(74, 189);
            this.button1.TabIndex = 3;
            this.button1.Text = "เพิ่มรายการ";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // _myDataGridView1
            // 
            this._myDataGridView1.AllowUserToAddRows = false;
            this._myDataGridView1.AllowUserToDeleteRows = false;
            this._myDataGridView1.AllowUserToResizeColumns = false;
            this._myDataGridView1.AllowUserToResizeRows = false;
            this._myDataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._myDataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this._barcode,
            this._qty});
            this._myDataGridView1.Dock = System.Windows.Forms.DockStyle.Top;
            this._myDataGridView1.Location = new System.Drawing.Point(0, 0);
            this._myDataGridView1.Name = "_myDataGridView1";
            this._myDataGridView1.OwnerBackgroundImage = null;
            this._myDataGridView1.Size = new System.Drawing.Size(624, 235);
            this._myDataGridView1.TabIndex = 0;
            // 
            // _barcode
            // 
            this._barcode.HeaderText = "Barcode";
            this._barcode.Name = "_barcode";
            // 
            // _qty
            // 
            this._qty.HeaderText = "Qty";
            this._qty.Name = "_qty";
            // 
            // _posPromotionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 442);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this._myDataGridView1);
            this.Name = "_posPromotionForm";
            this.Text = "_posPromotionForm";
            ((System.ComponentModel.ISupportInitialize)(this._myDataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private _myDataGridView _myDataGridView1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridViewTextBoxColumn _barcode;
        private System.Windows.Forms.DataGridViewTextBoxColumn _qty;
    }
}