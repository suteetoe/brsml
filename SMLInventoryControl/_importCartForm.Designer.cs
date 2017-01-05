namespace SMLInventoryControl
{
    partial class _importCartForm
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
            this._saleCode = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this._cartNumber = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this._processButton = new MyLib.VistaButton();
            this._itemGrid = new MyLib._myGrid();
            this.panel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _saleCode
            // 
            this._saleCode.Location = new System.Drawing.Point(78, 5);
            this._saleCode.Name = "_saleCode";
            this._saleCode.Size = new System.Drawing.Size(118, 22);
            this._saleCode.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 14);
            this.label1.TabIndex = 1;
            this.label1.Text = "Sale Code :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(204, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 14);
            this.label2.TabIndex = 3;
            this.label2.Text = "Cart Number :";
            // 
            // _cartNumber
            // 
            this._cartNumber.FormattingEnabled = true;
            this._cartNumber.Location = new System.Drawing.Point(294, 6);
            this._cartNumber.Name = "_cartNumber";
            this._cartNumber.Size = new System.Drawing.Size(542, 22);
            this._cartNumber.TabIndex = 4;
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this._cartNumber);
            this.panel1.Controls.Add(this._saleCode);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(848, 31);
            this.panel1.TabIndex = 5;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.Controls.Add(this._processButton);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 434);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(848, 30);
            this.flowLayoutPanel1.TabIndex = 6;
            // 
            // _processButton
            // 
            this._processButton._drawNewMethod = false;
            this._processButton.AutoSize = true;
            this._processButton.BackColor = System.Drawing.Color.Transparent;
            this._processButton.ButtonText = "Process";
            this._processButton.Location = new System.Drawing.Point(768, 3);
            this._processButton.myImage = global::SMLInventoryControl.Properties.Resources.flash;
            this._processButton.Name = "_processButton";
            this._processButton.Size = new System.Drawing.Size(77, 24);
            this._processButton.TabIndex = 0;
            this._processButton.Text = "Process";
            this._processButton.UseVisualStyleBackColor = false;
            // 
            // _itemGrid
            // 
            this._itemGrid._extraWordShow = true;
            this._itemGrid._selectRow = -1;
            this._itemGrid.BackColor = System.Drawing.SystemColors.Window;
            this._itemGrid.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._itemGrid.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._itemGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._itemGrid.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._itemGrid.Location = new System.Drawing.Point(0, 31);
            this._itemGrid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._itemGrid.Name = "_itemGrid";
            this._itemGrid.Size = new System.Drawing.Size(848, 403);
            this._itemGrid.TabIndex = 7;
            this._itemGrid.TabStop = false;
            // 
            // _importCartForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(848, 464);
            this.Controls.Add(this._itemGrid);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "_importCartForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Import from Cart";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox _cartNumber;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        public MyLib.VistaButton _processButton;
        public MyLib._myGrid _itemGrid;
        public System.Windows.Forms.TextBox _saleCode;
    }
}