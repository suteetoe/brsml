namespace SMLInventoryControl
{
    partial class _stockAutoAdjustProductNotFoundForm
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this._whGrid = new MyLib._myGrid();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this._processButton = new MyLib.VistaButton();
            this._itemListGrid = new MyLib._myGrid();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this._createButton = new MyLib.VistaButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this._screen = new MyLib._myScreen();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 31);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this._whGrid);
            this.splitContainer1.Panel1.Controls.Add(this.flowLayoutPanel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this._itemListGrid);
            this.splitContainer1.Panel2.Controls.Add(this.flowLayoutPanel2);
            this.splitContainer1.Size = new System.Drawing.Size(882, 605);
            this.splitContainer1.SplitterDistance = 326;
            this.splitContainer1.TabIndex = 5;
            // 
            // _whGrid
            // 
            this._whGrid._extraWordShow = true;
            this._whGrid._selectRow = -1;
            this._whGrid.BackColor = System.Drawing.SystemColors.Window;
            this._whGrid.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._whGrid.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._whGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._whGrid.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._whGrid.IsEdit = false;
            this._whGrid.Location = new System.Drawing.Point(0, 0);
            this._whGrid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._whGrid.Name = "_whGrid";
            this._whGrid.Size = new System.Drawing.Size(324, 573);
            this._whGrid.TabIndex = 5;
            this._whGrid.TabStop = false;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.Controls.Add(this._processButton);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 573);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(324, 30);
            this.flowLayoutPanel1.TabIndex = 6;
            // 
            // _processButton
            // 
            this._processButton._drawNewMethod = false;
            this._processButton.AutoSize = true;
            this._processButton.BackColor = System.Drawing.Color.Transparent;
            this._processButton.ButtonText = "ประมวลผล";
            this._processButton.Location = new System.Drawing.Point(3, 3);
            this._processButton.Name = "_processButton";
            this._processButton.Size = new System.Drawing.Size(75, 24);
            this._processButton.TabIndex = 0;
            this._processButton.Text = "ประมวลผล";
            this._processButton.UseVisualStyleBackColor = false;
            this._processButton.Click += new System.EventHandler(this._processButton_Click);
            // 
            // _itemListGrid
            // 
            this._itemListGrid._extraWordShow = true;
            this._itemListGrid._selectRow = -1;
            this._itemListGrid.BackColor = System.Drawing.SystemColors.Window;
            this._itemListGrid.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._itemListGrid.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._itemListGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._itemListGrid.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._itemListGrid.IsEdit = false;
            this._itemListGrid.Location = new System.Drawing.Point(0, 0);
            this._itemListGrid.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._itemListGrid.Name = "_itemListGrid";
            this._itemListGrid.Size = new System.Drawing.Size(550, 573);
            this._itemListGrid.TabIndex = 4;
            this._itemListGrid.TabStop = false;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.AutoSize = true;
            this.flowLayoutPanel2.Controls.Add(this._createButton);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(0, 573);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(550, 30);
            this.flowLayoutPanel2.TabIndex = 7;
            // 
            // _createButton
            // 
            this._createButton._drawNewMethod = false;
            this._createButton.AutoSize = true;
            this._createButton.BackColor = System.Drawing.Color.Transparent;
            this._createButton.ButtonText = "สร้างเอกสารปรับปรุงเพื่อให้ยอดคงเหลือเป็นศูนย์";
            this._createButton.Location = new System.Drawing.Point(3, 3);
            this._createButton.Name = "_createButton";
            this._createButton.Size = new System.Drawing.Size(261, 24);
            this._createButton.TabIndex = 0;
            this._createButton.Text = "สร้างเอกสารปรับปรุงเพื่อให้ยอดคงเหลือเป็นศูนย์";
            this._createButton.UseVisualStyleBackColor = false;
            this._createButton.Click += new System.EventHandler(this._createButton_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this._screen);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.panel1.Size = new System.Drawing.Size(882, 31);
            this.panel1.TabIndex = 7;
            // 
            // _screen
            // 
            this._screen._isChange = false;
            this._screen.BackColor = System.Drawing.Color.Transparent;
            this._screen.Dock = System.Windows.Forms.DockStyle.Top;
            this._screen.Location = new System.Drawing.Point(0, 4);
            this._screen.Name = "_screen";
            this._screen.Size = new System.Drawing.Size(882, 10);
            this._screen.TabIndex = 0;
            // 
            // _stockAutoAdjustProductNotFoundForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(882, 636);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "_stockAutoAdjustProductNotFoundForm";
            this.Text = "สินค้าที่ไม่มีการตรวจนับแต่มียอดคงเหลือ";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private MyLib._myGrid _itemListGrid;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private MyLib._myGrid _whGrid;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private MyLib.VistaButton _processButton;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private MyLib.VistaButton _createButton;
        private System.Windows.Forms.Panel panel1;
        private MyLib._myScreen _screen;
    }
}