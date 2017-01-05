namespace SMLDataQuery
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
            this._queryTextbox = new System.Windows.Forms.TextBox();
            this._processButton = new System.Windows.Forms.Button();
            this._resultGrid = new System.Windows.Forms.DataGridView();
            this._resultLabel = new System.Windows.Forms.Label();
            this._executeButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this._resultGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // _queryTextbox
            // 
            this._queryTextbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._queryTextbox.Location = new System.Drawing.Point(12, 12);
            this._queryTextbox.Multiline = true;
            this._queryTextbox.Name = "_queryTextbox";
            this._queryTextbox.Size = new System.Drawing.Size(1053, 84);
            this._queryTextbox.TabIndex = 0;
            // 
            // _processButton
            // 
            this._processButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._processButton.Location = new System.Drawing.Point(990, 102);
            this._processButton.Name = "_processButton";
            this._processButton.Size = new System.Drawing.Size(75, 23);
            this._processButton.TabIndex = 1;
            this._processButton.Text = "Process";
            this._processButton.UseVisualStyleBackColor = true;
            this._processButton.Click += new System.EventHandler(this._processButton_Click);
            // 
            // _resultGrid
            // 
            this._resultGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._resultGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._resultGrid.Location = new System.Drawing.Point(12, 131);
            this._resultGrid.Name = "_resultGrid";
            this._resultGrid.Size = new System.Drawing.Size(1053, 591);
            this._resultGrid.TabIndex = 2;
            // 
            // _resultLabel
            // 
            this._resultLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._resultLabel.Location = new System.Drawing.Point(13, 102);
            this._resultLabel.Name = "_resultLabel";
            this._resultLabel.Size = new System.Drawing.Size(890, 23);
            this._resultLabel.TabIndex = 3;
            this._resultLabel.Text = "label1";
            // 
            // _executeButton
            // 
            this._executeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._executeButton.Location = new System.Drawing.Point(909, 102);
            this._executeButton.Name = "_executeButton";
            this._executeButton.Size = new System.Drawing.Size(75, 23);
            this._executeButton.TabIndex = 4;
            this._executeButton.Text = "Execute";
            this._executeButton.UseVisualStyleBackColor = true;
            this._executeButton.Click += new System.EventHandler(this._executeButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1077, 734);
            this.Controls.Add(this._executeButton);
            this.Controls.Add(this._resultLabel);
            this.Controls.Add(this._resultGrid);
            this.Controls.Add(this._processButton);
            this.Controls.Add(this._queryTextbox);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this._resultGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox _queryTextbox;
        private System.Windows.Forms.Button _processButton;
        private System.Windows.Forms.DataGridView _resultGrid;
        private System.Windows.Forms.Label _resultLabel;
        private System.Windows.Forms.Button _executeButton;
    }
}

