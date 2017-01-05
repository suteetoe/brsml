namespace SMLERPGL
{
    partial class _journalPassUndo
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this._panelMain = new System.Windows.Forms.Panel();
            this._panelDetail = new System.Windows.Forms.Panel();
            this._panelTop = new System.Windows.Forms.Panel();
            this._panelCondition = new System.Windows.Forms.Panel();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this._processButton = new System.Windows.Forms.Button();
            this._selectAllButton = new System.Windows.Forms.Button();
            this._removeAllButton = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this._passButton = new System.Windows.Forms.Button();
            this._closeButton = new System.Windows.Forms.Button();
            this._panelMain.SuspendLayout();
            this._panelTop.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // _panelMain
            // 
            this._panelMain.Controls.Add(this._panelDetail);
            this._panelMain.Controls.Add(this._panelTop);
            this._panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this._panelMain.Location = new System.Drawing.Point(0, 0);
            this._panelMain.Name = "_panelMain";
            this._panelMain.Size = new System.Drawing.Size(522, 404);
            this._panelMain.TabIndex = 1;
            // 
            // _panelDetail
            // 
            this._panelDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this._panelDetail.Location = new System.Drawing.Point(0, 30);
            this._panelDetail.Name = "_panelDetail";
            this._panelDetail.Size = new System.Drawing.Size(522, 374);
            this._panelDetail.TabIndex = 1;
            // 
            // _panelTop
            // 
            this._panelTop.AutoSize = true;
            this._panelTop.BackColor = System.Drawing.Color.Azure;
            this._panelTop.Controls.Add(this._panelCondition);
            this._panelTop.Controls.Add(this.flowLayoutPanel2);
            this._panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this._panelTop.Location = new System.Drawing.Point(0, 0);
            this._panelTop.Name = "_panelTop";
            this._panelTop.Size = new System.Drawing.Size(522, 30);
            this._panelTop.TabIndex = 0;
            // 
            // _panelCondition
            // 
            this._panelCondition.AutoSize = true;
            this._panelCondition.Dock = System.Windows.Forms.DockStyle.Fill;
            this._panelCondition.Location = new System.Drawing.Point(0, 0);
            this._panelCondition.Name = "_panelCondition";
            this._panelCondition.Size = new System.Drawing.Size(522, 0);
            this._panelCondition.TabIndex = 2;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.AutoSize = true;
            this.flowLayoutPanel2.BackColor = System.Drawing.Color.Azure;
            this.flowLayoutPanel2.Controls.Add(this._processButton);
            this.flowLayoutPanel2.Controls.Add(this._selectAllButton);
            this.flowLayoutPanel2.Controls.Add(this._removeAllButton);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(522, 30);
            this.flowLayoutPanel2.TabIndex = 1;
            // 
            // _processButton
            // 
            this._processButton.AutoSize = true;
            this._processButton.Image = global::SMLERPGL.Properties.Resources.flash;
            this._processButton.Location = new System.Drawing.Point(3, 3);
            this._processButton.Name = "_processButton";
            this._processButton.Size = new System.Drawing.Size(75, 24);
            this._processButton.TabIndex = 0;
            this._processButton.Text = "Process";
            this._processButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._processButton.UseVisualStyleBackColor = true;
            this._processButton.Click += new System.EventHandler(this._processButton_Click);
            // 
            // _selectAllButton
            // 
            this._selectAllButton.AutoSize = true;
            this._selectAllButton.Image = global::SMLERPGL.Properties.Resources.checks;
            this._selectAllButton.Location = new System.Drawing.Point(84, 3);
            this._selectAllButton.Name = "_selectAllButton";
            this._selectAllButton.Size = new System.Drawing.Size(83, 24);
            this._selectAllButton.TabIndex = 1;
            this._selectAllButton.Text = "Select All";
            this._selectAllButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._selectAllButton.UseVisualStyleBackColor = true;
            this._selectAllButton.Click += new System.EventHandler(this._selectAllButton_Click);
            // 
            // _removeAllButton
            // 
            this._removeAllButton.AutoSize = true;
            this._removeAllButton.Image = global::SMLERPGL.Properties.Resources.selection;
            this._removeAllButton.Location = new System.Drawing.Point(173, 3);
            this._removeAllButton.Name = "_removeAllButton";
            this._removeAllButton.Size = new System.Drawing.Size(93, 24);
            this._removeAllButton.TabIndex = 2;
            this._removeAllButton.Text = "Remove All";
            this._removeAllButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._removeAllButton.UseVisualStyleBackColor = true;
            this._removeAllButton.Click += new System.EventHandler(this._removeAllButton_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.LightCyan;
            this.flowLayoutPanel1.Controls.Add(this._passButton);
            this.flowLayoutPanel1.Controls.Add(this._closeButton);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 404);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(522, 30);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // _passButton
            // 
            this._passButton.AutoSize = true;
            this._passButton.Image = global::SMLERPGL.Properties.Resources.flash;
            this._passButton.Location = new System.Drawing.Point(444, 3);
            this._passButton.Name = "_passButton";
            this._passButton.Size = new System.Drawing.Size(75, 24);
            this._passButton.TabIndex = 0;
            this._passButton.Text = "Pass";
            this._passButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._passButton.UseVisualStyleBackColor = true;
            this._passButton.Click += new System.EventHandler(this._passButton_Click);
            // 
            // _closeButton
            // 
            this._closeButton.AutoSize = true;
            this._closeButton.Image = global::SMLERPGL.Properties.Resources.error;
            this._closeButton.Location = new System.Drawing.Point(363, 3);
            this._closeButton.Name = "_closeButton";
            this._closeButton.Size = new System.Drawing.Size(75, 24);
            this._closeButton.TabIndex = 1;
            this._closeButton.Text = "Close";
            this._closeButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this._closeButton.UseVisualStyleBackColor = true;
            this._closeButton.Click += new System.EventHandler(this._closeButton_Click);
            // 
            // _journalPass
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._panelMain);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "_journalPass";
            this.Size = new System.Drawing.Size(522, 434);
            this._panelMain.ResumeLayout(false);
            this._panelMain.PerformLayout();
            this._panelTop.ResumeLayout(false);
            this._panelTop.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel _panelMain;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button _passButton;
        private System.Windows.Forms.Button _closeButton;
        private System.Windows.Forms.Panel _panelDetail;
        private System.Windows.Forms.Panel _panelTop;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Button _processButton;
        private System.Windows.Forms.Panel _panelCondition;
        private System.Windows.Forms.Button _selectAllButton;
        private System.Windows.Forms.Button _removeAllButton;
    }
}
