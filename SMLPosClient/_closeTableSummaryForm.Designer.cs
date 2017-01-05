namespace SMLPosClient
{
    partial class _closeTableSummaryForm
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
            this._myPanel1 = new MyLib._myPanel();
            this._foodDiscountButton = new MyLib.VistaButton();
            this._processButton = new MyLib.VistaButton();
            this._findCusButton = new MyLib.VistaButton();
            this._cusidTextbox = new System.Windows.Forms.TextBox();
            this._myLabel1 = new MyLib._myLabel();
            this._mainPosPanel = new System.Windows.Forms.Panel();
            this._serviceChargeButton = new MyLib.VistaButton();
            this._myPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _myPanel1
            // 
            this._myPanel1._switchTabAuto = false;
            this._myPanel1.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Controls.Add(this._serviceChargeButton);
            this._myPanel1.Controls.Add(this._foodDiscountButton);
            this._myPanel1.Controls.Add(this._processButton);
            this._myPanel1.Controls.Add(this._findCusButton);
            this._myPanel1.Controls.Add(this._cusidTextbox);
            this._myPanel1.Controls.Add(this._myLabel1);
            this._myPanel1.CornerPicture = null;
            this._myPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this._myPanel1.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._myPanel1.Location = new System.Drawing.Point(0, 0);
            this._myPanel1.Name = "_myPanel1";
            this._myPanel1.Size = new System.Drawing.Size(764, 54);
            this._myPanel1.TabIndex = 0;
            // 
            // _foodDiscountButton
            // 
            this._foodDiscountButton._drawNewMethod = false;
            this._foodDiscountButton.BackColor = System.Drawing.Color.Transparent;
            this._foodDiscountButton.ButtonText = "ลดอาหาร";
            this._foodDiscountButton.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._foodDiscountButton.ImageSize = new System.Drawing.Size(24, 24);
            this._foodDiscountButton.Location = new System.Drawing.Point(554, 5);
            this._foodDiscountButton.myImage = global::SMLPosClient.Properties.Resources.percent;
            this._foodDiscountButton.Name = "_foodDiscountButton";
            this._foodDiscountButton.Size = new System.Drawing.Size(100, 40);
            this._foodDiscountButton.TabIndex = 4;
            this._foodDiscountButton.Text = "vistaButton1";
            this._foodDiscountButton.UseVisualStyleBackColor = false;
            this._foodDiscountButton.Click += new System.EventHandler(this._foodDiscountButton_Click);
            // 
            // _processButton
            // 
            this._processButton._drawNewMethod = false;
            this._processButton.BackColor = System.Drawing.Color.Transparent;
            this._processButton.ButtonText = "Process";
            this._processButton.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._processButton.ImageSize = new System.Drawing.Size(20, 20);
            this._processButton.Location = new System.Drawing.Point(658, 5);
            this._processButton.myImage = global::SMLPosClient.Properties.Resources.flash;
            this._processButton.Name = "_processButton";
            this._processButton.Size = new System.Drawing.Size(100, 40);
            this._processButton.TabIndex = 3;
            this._processButton.Text = "vistaButton2";
            this._processButton.UseVisualStyleBackColor = false;
            this._processButton.Click += new System.EventHandler(this._processButton_Click);
            // 
            // _findCusButton
            // 
            this._findCusButton._drawNewMethod = false;
            this._findCusButton.BackColor = System.Drawing.Color.Transparent;
            this._findCusButton.ButtonText = "ค้นหา";
            this._findCusButton.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._findCusButton.ImageSize = new System.Drawing.Size(24, 24);
            this._findCusButton.Location = new System.Drawing.Point(359, 5);
            this._findCusButton.myImage = global::SMLPosClient.Properties.Resources.users4;
            this._findCusButton.Name = "_findCusButton";
            this._findCusButton.Size = new System.Drawing.Size(86, 40);
            this._findCusButton.TabIndex = 2;
            this._findCusButton.Text = "vistaButton1";
            this._findCusButton.UseVisualStyleBackColor = false;
            this._findCusButton.Click += new System.EventHandler(this._findCusButton_Click);
            // 
            // _cusidTextbox
            // 
            this._cusidTextbox.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._cusidTextbox.Location = new System.Drawing.Point(97, 12);
            this._cusidTextbox.Name = "_cusidTextbox";
            this._cusidTextbox.Size = new System.Drawing.Size(257, 30);
            this._cusidTextbox.TabIndex = 1;
            // 
            // _myLabel1
            // 
            this._myLabel1.AutoSize = true;
            this._myLabel1.BackColor = System.Drawing.Color.Transparent;
            this._myLabel1.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._myLabel1.Location = new System.Drawing.Point(12, 15);
            this._myLabel1.Name = "_myLabel1";
            this._myLabel1.ResourceName = "";
            this._myLabel1.Size = new System.Drawing.Size(91, 23);
            this._myLabel1.TabIndex = 0;
            this._myLabel1.Text = "สมาชิก : ";
            // 
            // _mainPosPanel
            // 
            this._mainPosPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._mainPosPanel.Location = new System.Drawing.Point(0, 54);
            this._mainPosPanel.Name = "_mainPosPanel";
            this._mainPosPanel.Size = new System.Drawing.Size(764, 558);
            this._mainPosPanel.TabIndex = 1;
            // 
            // _serviceChargeButton
            // 
            this._serviceChargeButton._drawNewMethod = false;
            this._serviceChargeButton.BackColor = System.Drawing.Color.Transparent;
            this._serviceChargeButton.ButtonText = "Service Charge";
            this._serviceChargeButton.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._serviceChargeButton.ImageSize = new System.Drawing.Size(24, 24);
            this._serviceChargeButton.Location = new System.Drawing.Point(449, 5);
            this._serviceChargeButton.myImage = global::SMLPosClient.Properties.Resources.percent;
            this._serviceChargeButton.Name = "_serviceChargeButton";
            this._serviceChargeButton.Size = new System.Drawing.Size(100, 40);
            this._serviceChargeButton.TabIndex = 5;
            this._serviceChargeButton.Text = "vistaButton1";
            this._serviceChargeButton.UseVisualStyleBackColor = false;
            this._serviceChargeButton.Click += new System.EventHandler(this._serviceChargeButton_Click);
            // 
            // _closeTableSummaryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(764, 612);
            this.Controls.Add(this._mainPosPanel);
            this.Controls.Add(this._myPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "_closeTableSummaryForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ยอดชำระเงิน";
            this._myPanel1.ResumeLayout(false);
            this._myPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private MyLib._myPanel _myPanel1;
        private System.Windows.Forms.Panel _mainPosPanel;
        private System.Windows.Forms.TextBox _cusidTextbox;
        private MyLib._myLabel _myLabel1;
        private MyLib.VistaButton _processButton;
        private MyLib.VistaButton _findCusButton;
        private MyLib.VistaButton _foodDiscountButton;
        private MyLib.VistaButton _serviceChargeButton;
    }
}