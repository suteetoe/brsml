namespace SMLERPControl._ic
{
    partial class _icPurchasePointForm
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
            this._gridPurchasePoint = new MyLib._myGrid();
            this._myFlowLayoutPanel1 = new MyLib._myFlowLayoutPanel();
            this.vistaButton1 = new MyLib.VistaButton();
            this._myFlowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _gridPurchasePoint
            // 
            this._gridPurchasePoint._extraWordShow = true;
            this._gridPurchasePoint._selectRow = -1;
            this._gridPurchasePoint.BackColor = System.Drawing.SystemColors.Window;
            this._gridPurchasePoint.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._gridPurchasePoint.ColumnBackground = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(251)))), ((int)(((byte)(252)))));
            this._gridPurchasePoint.ColumnBackgroundEnd = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(217)))), ((int)(((byte)(227)))));
            this._gridPurchasePoint.Dock = System.Windows.Forms.DockStyle.Fill;
            this._gridPurchasePoint.Font = new System.Drawing.Font("Tahoma", 9F);
            this._gridPurchasePoint.Location = new System.Drawing.Point(5, 5);
            this._gridPurchasePoint.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._gridPurchasePoint.Name = "_gridPurchasePoint";
            this._gridPurchasePoint.Size = new System.Drawing.Size(755, 349);
            this._gridPurchasePoint.TabIndex = 0;
            this._gridPurchasePoint.TabStop = false;
            // 
            // _myFlowLayoutPanel1
            // 
            this._myFlowLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this._myFlowLayoutPanel1.Controls.Add(this.vistaButton1);
            this._myFlowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._myFlowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this._myFlowLayoutPanel1.Location = new System.Drawing.Point(5, 354);
            this._myFlowLayoutPanel1.Name = "_myFlowLayoutPanel1";
            this._myFlowLayoutPanel1.Padding = new System.Windows.Forms.Padding(1, 1, 2, 1);
            this._myFlowLayoutPanel1.Size = new System.Drawing.Size(755, 33);
            this._myFlowLayoutPanel1.TabIndex = 1;
            // 
            // vistaButton1
            // 
            this.vistaButton1._drawNewMethod = false;
            this.vistaButton1.BackColor = System.Drawing.Color.Transparent;
            this.vistaButton1.ButtonText = "Close";
            this.vistaButton1.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.vistaButton1.Location = new System.Drawing.Point(679, 4);
            this.vistaButton1.myImage = global::SMLERPControl.Properties.Resources.error;
            this.vistaButton1.Name = "vistaButton1";
            this.vistaButton1.Size = new System.Drawing.Size(70, 25);
            this.vistaButton1.TabIndex = 0;
            this.vistaButton1.Text = "vistaButton1";
            this.vistaButton1.UseVisualStyleBackColor = false;
            // 
            // _icPurchasePointForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(765, 392);
            this.Controls.Add(this._gridPurchasePoint);
            this.Controls.Add(this._myFlowLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "_icPurchasePointForm";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "_icPurchasePointForm";
            this._myFlowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public MyLib._myGrid _gridPurchasePoint;
        private MyLib._myFlowLayoutPanel _myFlowLayoutPanel1;
        private MyLib.VistaButton vistaButton1;

    }
}