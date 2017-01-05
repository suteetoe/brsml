namespace SMLInventoryControl._utils
{
    partial class _selectWareHouseForm
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
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this._saveButton = new System.Windows.Forms.Button();
            this._cancelButton = new System.Windows.Forms.Button();
            this._wareHouse = new SMLInventoryControl._utils._selectWareHouseUserControl();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.Controls.Add(this._saveButton);
            this.flowLayoutPanel1.Controls.Add(this._cancelButton);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 398);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(616, 31);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // _saveButton
            // 
            this._saveButton.AutoSize = true;
            this._saveButton.Image = global::SMLInventoryControl.Properties.Resources.flash;
            this._saveButton.Location = new System.Drawing.Point(554, 3);
            this._saveButton.Name = "_saveButton";
            this._saveButton.Size = new System.Drawing.Size(59, 25);
            this._saveButton.TabIndex = 0;
            this._saveButton.Text = "Save";
            this._saveButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._saveButton.UseVisualStyleBackColor = true;
            this._saveButton.Click += new System.EventHandler(this._saveButton1_Click);
            // 
            // _cancelButton
            // 
            this._cancelButton.AutoSize = true;
            this._cancelButton.Image = global::SMLInventoryControl.Properties.Resources.error1;
            this._cancelButton.Location = new System.Drawing.Point(480, 3);
            this._cancelButton.Name = "_cancelButton";
            this._cancelButton.Size = new System.Drawing.Size(68, 25);
            this._cancelButton.TabIndex = 1;
            this._cancelButton.Text = "Cancel";
            this._cancelButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._cancelButton.UseVisualStyleBackColor = true;
            this._cancelButton.Click += new System.EventHandler(this._cancelButton_Click);
            // 
            // _wareHouse
            // 
            this._wareHouse.Dock = System.Windows.Forms.DockStyle.Fill;
            this._wareHouse.Location = new System.Drawing.Point(0, 0);
            this._wareHouse.Name = "_wareHouse";
            this._wareHouse.Size = new System.Drawing.Size(616, 398);
            this._wareHouse.TabIndex = 0;
            // 
            // _selectWareHouseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(616, 429);
            this.ControlBox = false;
            this.Controls.Add(this._wareHouse);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "_selectWareHouseForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Select WareHouse";
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private _selectWareHouseUserControl _wareHouse;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button _cancelButton;
        public System.Windows.Forms.Button _saveButton;
    }
}