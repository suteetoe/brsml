namespace SMLPosClient
{
    partial class _posProductControl
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            this._dataGridView = new SMLPosClient._myDataGridView();
            this._lineNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._qty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._subTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._buttomPanel = new MyLib._myPanel();
            this._button_change_theme = new System.Windows.Forms.Button();
            this._comboBox_theme = new System.Windows.Forms.ComboBox();
            this._inputTextBoxPanel = new MyLib._myPanel();
            this._inputTextBox = new System.Windows.Forms.TextBox();
            this._totalLabel = new System.Windows.Forms.Label();
            this._topPanel = new MyLib._myPanel();
            this._customerPanel = new MyLib._myPanel();
            this._pointLabel = new System.Windows.Forms.Label();
            this._nameLabel = new System.Windows.Forms.Label();
            this._displayPricePanel = new MyLib._myPanel();
            this._priceLabel = new System.Windows.Forms.Label();
            this._packingLabel = new System.Windows.Forms.Label();
            this._displayNamePanel = new MyLib._myPanel();
            this._label = new MyLib._myShadowLabel(this.components);
            ((System.ComponentModel.ISupportInitialize)(this._dataGridView)).BeginInit();
            this._buttomPanel.SuspendLayout();
            this._inputTextBoxPanel.SuspendLayout();
            this._topPanel.SuspendLayout();
            this._customerPanel.SuspendLayout();
            this._displayPricePanel.SuspendLayout();
            this._displayNamePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // _dataGridView
            // 
            this._dataGridView.AllowUserToAddRows = false;
            this._dataGridView.AllowUserToDeleteRows = false;
            this._dataGridView.AllowUserToResizeColumns = false;
            this._dataGridView.AllowUserToResizeRows = false;
            this._dataGridView.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this._dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this._dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this._lineNumber,
            this._code,
            this._description,
            this._price,
            this._qty,
            this._subTotal});
            this._dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this._dataGridView.Location = new System.Drawing.Point(0, 144);
            this._dataGridView.Margin = new System.Windows.Forms.Padding(0);
            this._dataGridView.MultiSelect = false;
            this._dataGridView.Name = "_dataGridView";
            this._dataGridView.OwnerBackgroundImage = null;
            this._dataGridView.ReadOnly = true;
            this._dataGridView.RowHeadersVisible = false;
            this._dataGridView.Size = new System.Drawing.Size(599, 377);
            this._dataGridView.TabIndex = 0;
            // 
            // _lineNumber
            // 
            this._lineNumber.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this._lineNumber.DefaultCellStyle = dataGridViewCellStyle7;
            this._lineNumber.FillWeight = 50F;
            this._lineNumber.HeaderText = "No";
            this._lineNumber.Name = "_lineNumber";
            this._lineNumber.ReadOnly = true;
            // 
            // _code
            // 
            this._code.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this._code.HeaderText = "Code";
            this._code.Name = "_code";
            this._code.ReadOnly = true;
            // 
            // _description
            // 
            this._description.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this._description.FillWeight = 300F;
            this._description.HeaderText = "Description";
            this._description.Name = "_description";
            this._description.ReadOnly = true;
            // 
            // _price
            // 
            this._price.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle8.Format = "N2";
            dataGridViewCellStyle8.NullValue = null;
            this._price.DefaultCellStyle = dataGridViewCellStyle8;
            this._price.HeaderText = "Price";
            this._price.Name = "_price";
            this._price.ReadOnly = true;
            // 
            // _qty
            // 
            this._qty.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle9.Format = "N2";
            dataGridViewCellStyle9.NullValue = null;
            this._qty.DefaultCellStyle = dataGridViewCellStyle9;
            this._qty.HeaderText = "Qty";
            this._qty.Name = "_qty";
            this._qty.ReadOnly = true;
            // 
            // _subTotal
            // 
            this._subTotal.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle10.Format = "N2";
            dataGridViewCellStyle10.NullValue = null;
            this._subTotal.DefaultCellStyle = dataGridViewCellStyle10;
            this._subTotal.HeaderText = "Subtotal";
            this._subTotal.Name = "_subTotal";
            this._subTotal.ReadOnly = true;
            // 
            // _buttomPanel
            // 
            this._buttomPanel._switchTabAuto = false;
            this._buttomPanel.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._buttomPanel.Controls.Add(this._button_change_theme);
            this._buttomPanel.Controls.Add(this._comboBox_theme);
            this._buttomPanel.Controls.Add(this._inputTextBoxPanel);
            this._buttomPanel.Controls.Add(this._totalLabel);
            this._buttomPanel.CornerPicture = null;
            this._buttomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._buttomPanel.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this._buttomPanel.Location = new System.Drawing.Point(0, 521);
            this._buttomPanel.Name = "_buttomPanel";
            this._buttomPanel.ShowBackground = false;
            this._buttomPanel.Size = new System.Drawing.Size(599, 129);
            this._buttomPanel.TabIndex = 2;
            // 
            // _button_change_theme
            // 
            this._button_change_theme.Location = new System.Drawing.Point(493, 81);
            this._button_change_theme.Name = "_button_change_theme";
            this._button_change_theme.Size = new System.Drawing.Size(100, 23);
            this._button_change_theme.TabIndex = 5;
            this._button_change_theme.Text = "Change Theme";
            this._button_change_theme.UseVisualStyleBackColor = true;
            // 
            // _comboBox_theme
            // 
            this._comboBox_theme.FormattingEnabled = true;
            this._comboBox_theme.Location = new System.Drawing.Point(355, 81);
            this._comboBox_theme.Name = "_comboBox_theme";
            this._comboBox_theme.Size = new System.Drawing.Size(117, 21);
            this._comboBox_theme.TabIndex = 4;
            // 
            // _inputTextBoxPanel
            // 
            this._inputTextBoxPanel._switchTabAuto = false;
            this._inputTextBoxPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._inputTextBoxPanel.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._inputTextBoxPanel.Controls.Add(this._inputTextBox);
            this._inputTextBoxPanel.CornerPicture = null;
            this._inputTextBoxPanel.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this._inputTextBoxPanel.Location = new System.Drawing.Point(4, 81);
            this._inputTextBoxPanel.Name = "_inputTextBoxPanel";
            this._inputTextBoxPanel.Padding = new System.Windows.Forms.Padding(9);
            this._inputTextBoxPanel.ShowBackground = false;
            this._inputTextBoxPanel.Size = new System.Drawing.Size(345, 44);
            this._inputTextBoxPanel.TabIndex = 3;
            // 
            // _inputTextBox
            // 
            this._inputTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this._inputTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._inputTextBox.Location = new System.Drawing.Point(9, 9);
            this._inputTextBox.Name = "_inputTextBox";
            this._inputTextBox.Size = new System.Drawing.Size(327, 26);
            this._inputTextBox.TabIndex = 0;
            this._inputTextBox.TextChanged += new System.EventHandler(this._inputTextBox_TextChanged);
            // 
            // _totalLabel
            // 
            this._totalLabel.BackColor = System.Drawing.Color.Transparent;
            this._totalLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this._totalLabel.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._totalLabel.Location = new System.Drawing.Point(336, 9);
            this._totalLabel.Name = "_totalLabel";
            this._totalLabel.Size = new System.Drawing.Size(254, 37);
            this._totalLabel.TabIndex = 2;
            this._totalLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _topPanel
            // 
            this._topPanel._switchTabAuto = false;
            this._topPanel.BackColor = System.Drawing.Color.Transparent;
            this._topPanel.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._topPanel.Controls.Add(this._customerPanel);
            this._topPanel.Controls.Add(this._displayPricePanel);
            this._topPanel.Controls.Add(this._displayNamePanel);
            this._topPanel.CornerPicture = null;
            this._topPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this._topPanel.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._topPanel.Location = new System.Drawing.Point(0, 0);
            this._topPanel.Name = "_topPanel";
            this._topPanel.Size = new System.Drawing.Size(599, 144);
            this._topPanel.TabIndex = 1;
            // 
            // _customerPanel
            // 
            this._customerPanel._switchTabAuto = false;
            this._customerPanel.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._customerPanel.Controls.Add(this._pointLabel);
            this._customerPanel.Controls.Add(this._nameLabel);
            this._customerPanel.CornerPicture = null;
            this._customerPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this._customerPanel.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this._customerPanel.Location = new System.Drawing.Point(0, 100);
            this._customerPanel.Name = "_customerPanel";
            this._customerPanel.Padding = new System.Windows.Forms.Padding(9);
            this._customerPanel.ShowBackground = false;
            this._customerPanel.Size = new System.Drawing.Size(599, 35);
            this._customerPanel.TabIndex = 2;
            // 
            // _pointLabel
            // 
            this._pointLabel.BackColor = System.Drawing.Color.Transparent;
            this._pointLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._pointLabel.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._pointLabel.Location = new System.Drawing.Point(392, 9);
            this._pointLabel.Name = "_pointLabel";
            this._pointLabel.Size = new System.Drawing.Size(198, 17);
            this._pointLabel.TabIndex = 0;
            this._pointLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _nameLabel
            // 
            this._nameLabel.BackColor = System.Drawing.Color.Transparent;
            this._nameLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this._nameLabel.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._nameLabel.Location = new System.Drawing.Point(9, 9);
            this._nameLabel.Name = "_nameLabel";
            this._nameLabel.Size = new System.Drawing.Size(383, 17);
            this._nameLabel.TabIndex = 1;
            this._nameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _displayPricePanel
            // 
            this._displayPricePanel._switchTabAuto = false;
            this._displayPricePanel.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._displayPricePanel.Controls.Add(this._priceLabel);
            this._displayPricePanel.Controls.Add(this._packingLabel);
            this._displayPricePanel.CornerPicture = null;
            this._displayPricePanel.Dock = System.Windows.Forms.DockStyle.Top;
            this._displayPricePanel.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this._displayPricePanel.Location = new System.Drawing.Point(0, 51);
            this._displayPricePanel.Name = "_displayPricePanel";
            this._displayPricePanel.Padding = new System.Windows.Forms.Padding(9);
            this._displayPricePanel.ShowBackground = false;
            this._displayPricePanel.Size = new System.Drawing.Size(599, 49);
            this._displayPricePanel.TabIndex = 1;
            // 
            // _priceLabel
            // 
            this._priceLabel.BackColor = System.Drawing.Color.Transparent;
            this._priceLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._priceLabel.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._priceLabel.Location = new System.Drawing.Point(201, 9);
            this._priceLabel.Name = "_priceLabel";
            this._priceLabel.Size = new System.Drawing.Size(389, 31);
            this._priceLabel.TabIndex = 0;
            this._priceLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _packingLabel
            // 
            this._packingLabel.BackColor = System.Drawing.Color.Transparent;
            this._packingLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this._packingLabel.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._packingLabel.Location = new System.Drawing.Point(9, 9);
            this._packingLabel.Name = "_packingLabel";
            this._packingLabel.Size = new System.Drawing.Size(192, 31);
            this._packingLabel.TabIndex = 1;
            this._packingLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _displayNamePanel
            // 
            this._displayNamePanel._switchTabAuto = false;
            this._displayNamePanel.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._displayNamePanel.Controls.Add(this._label);
            this._displayNamePanel.CornerPicture = null;
            this._displayNamePanel.Dock = System.Windows.Forms.DockStyle.Top;
            this._displayNamePanel.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this._displayNamePanel.Location = new System.Drawing.Point(0, 0);
            this._displayNamePanel.Name = "_displayNamePanel";
            this._displayNamePanel.Padding = new System.Windows.Forms.Padding(9);
            this._displayNamePanel.ShowBackground = false;
            this._displayNamePanel.Size = new System.Drawing.Size(599, 51);
            this._displayNamePanel.TabIndex = 0;
            // 
            // _label
            // 
            this._label.Angle = 0F;
            this._label.BackColor = System.Drawing.Color.Transparent;
            this._label.Dock = System.Windows.Forms.DockStyle.Fill;
            this._label.DrawGradient = false;
            this._label.EndColor = System.Drawing.Color.LightSkyBlue;
            this._label.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._label.ForeColor = System.Drawing.Color.Red;
            this._label.Location = new System.Drawing.Point(9, 9);
            this._label.Name = "_label";
            this._label.ShadowColor = System.Drawing.Color.Black;
            this._label.Size = new System.Drawing.Size(581, 33);
            this._label.StartColor = System.Drawing.Color.White;
            this._label.TabIndex = 0;
            this._label.XOffset = 1F;
            this._label.YOffset = 1F;
            // 
            // _posProductControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._dataGridView);
            this.Controls.Add(this._buttomPanel);
            this.Controls.Add(this._topPanel);
            this.Name = "_posProductControl";
            this.Size = new System.Drawing.Size(599, 650);
            ((System.ComponentModel.ISupportInitialize)(this._dataGridView)).EndInit();
            this._buttomPanel.ResumeLayout(false);
            this._inputTextBoxPanel.ResumeLayout(false);
            this._inputTextBoxPanel.PerformLayout();
            this._topPanel.ResumeLayout(false);
            this._customerPanel.ResumeLayout(false);
            this._displayPricePanel.ResumeLayout(false);
            this._displayNamePanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private MyLib._myPanel _topPanel;
        private MyLib._myPanel _buttomPanel;
        private MyLib._myPanel _displayPricePanel;
        private MyLib._myPanel _displayNamePanel;
        private MyLib._myShadowLabel _label;
        private System.Windows.Forms.Label _priceLabel;
        private System.Windows.Forms.Label _packingLabel;
        public _myDataGridView _dataGridView;
        public System.Windows.Forms.TextBox _inputTextBox;
        private MyLib._myPanel _customerPanel;
        private System.Windows.Forms.Label _pointLabel;
        private System.Windows.Forms.Label _nameLabel;
        private System.Windows.Forms.DataGridViewTextBoxColumn _lineNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn _code;
        private System.Windows.Forms.DataGridViewTextBoxColumn _description;
        private System.Windows.Forms.DataGridViewTextBoxColumn _price;
        private System.Windows.Forms.DataGridViewTextBoxColumn _qty;
        private System.Windows.Forms.DataGridViewTextBoxColumn _subTotal;
        private System.Windows.Forms.Label _totalLabel;
        private MyLib._myPanel _inputTextBoxPanel;
        private System.Windows.Forms.ComboBox _comboBox_theme;
        private System.Windows.Forms.Button _button_change_theme;

    }
}
