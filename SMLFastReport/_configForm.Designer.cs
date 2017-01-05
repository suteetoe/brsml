namespace SMLFastReport
{
    partial class _configForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(_configForm));
            this.label1 = new System.Windows.Forms.Label();
            this._reportNameTextBox = new System.Windows.Forms.TextBox();
            this._tabs = new System.Windows.Forms.TabControl();
            this._condition = new System.Windows.Forms.TabPage();
            this._conditionPanel = new MyLib._myPanel();
            this._conditionScreen = new SMLFastReport._conditionControl();
            this._panelCondition = new System.Windows.Forms.Panel();
            this._conditionGrid = new System.Windows.Forms.DataGridView();
            this._name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._row = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._column = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._span = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._resource_code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._text = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._type = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this._command = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._default = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this._column_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this._upButton = new System.Windows.Forms.ToolStripButton();
            this._downButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this._previewButton = new System.Windows.Forms.ToolStripButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this._reportRefTextbox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this._query1 = new System.Windows.Forms.TabPage();
            this._queryDesign1 = new SMLFastReport._queryControl();
            this._query2 = new System.Windows.Forms.TabPage();
            this._queryDesign2 = new SMLFastReport._queryControl();
            this._query3 = new System.Windows.Forms.TabPage();
            this._queryDesign3 = new SMLFastReport._queryControl();
            this._query4 = new System.Windows.Forms.TabPage();
            this._queryDesign4 = new SMLFastReport._queryControl();
            this._query5 = new System.Windows.Forms.TabPage();
            this._queryDesign5 = new SMLFastReport._queryControl();
            this._query6 = new System.Windows.Forms.TabPage();
            this._queryDesign6 = new SMLFastReport._queryControl();
            this._query7 = new System.Windows.Forms.TabPage();
            this._queryDesign7 = new SMLFastReport._queryControl();
            this._tabAccount = new System.Windows.Forms.TabPage();
            this._queryAccount = new SMLFastReport._queryAccountControl();
            this._iconList = new System.Windows.Forms.ImageList(this.components);
            this._tabReportOption = new System.Windows.Forms.TabPage();
            this._splitRowCheckbox = new System.Windows.Forms.CheckBox();
            this._lineFooterLastPageCheckbox = new System.Windows.Forms.CheckBox();
            this._tabs.SuspendLayout();
            this._condition.SuspendLayout();
            this._conditionPanel.SuspendLayout();
            this._panelCondition.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._conditionGrid)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this._query1.SuspendLayout();
            this._query2.SuspendLayout();
            this._query3.SuspendLayout();
            this._query4.SuspendLayout();
            this._query5.SuspendLayout();
            this._query6.SuspendLayout();
            this._query7.SuspendLayout();
            this._tabAccount.SuspendLayout();
            this._tabReportOption.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Report Name";
            // 
            // _reportNameTextBox
            // 
            this._reportNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this._reportNameTextBox.Location = new System.Drawing.Point(79, 3);
            this._reportNameTextBox.Name = "_reportNameTextBox";
            this._reportNameTextBox.Size = new System.Drawing.Size(678, 21);
            this._reportNameTextBox.TabIndex = 1;
            // 
            // _tabs
            // 
            this._tabs.Controls.Add(this._condition);
            this._tabs.Controls.Add(this._query1);
            this._tabs.Controls.Add(this._query2);
            this._tabs.Controls.Add(this._query3);
            this._tabs.Controls.Add(this._query4);
            this._tabs.Controls.Add(this._query5);
            this._tabs.Controls.Add(this._query6);
            this._tabs.Controls.Add(this._query7);
            this._tabs.Controls.Add(this._tabAccount);
            this._tabs.Controls.Add(this._tabReportOption);
            this._tabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this._tabs.ImageList = this._iconList;
            this._tabs.Location = new System.Drawing.Point(0, 0);
            this._tabs.Name = "_tabs";
            this._tabs.SelectedIndex = 0;
            this._tabs.Size = new System.Drawing.Size(778, 583);
            this._tabs.TabIndex = 2;
            // 
            // _condition
            // 
            this._condition.Controls.Add(this._conditionPanel);
            this._condition.Controls.Add(this.panel1);
            this._condition.ImageKey = "document_gear.png";
            this._condition.Location = new System.Drawing.Point(4, 23);
            this._condition.Name = "_condition";
            this._condition.Padding = new System.Windows.Forms.Padding(3);
            this._condition.Size = new System.Drawing.Size(770, 556);
            this._condition.TabIndex = 0;
            this._condition.Text = "Condition";
            this._condition.UseVisualStyleBackColor = true;
            // 
            // _conditionPanel
            // 
            this._conditionPanel._switchTabAuto = false;
            this._conditionPanel.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._conditionPanel.Controls.Add(this._conditionScreen);
            this._conditionPanel.Controls.Add(this._panelCondition);
            this._conditionPanel.CornerPicture = null;
            this._conditionPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._conditionPanel.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this._conditionPanel.Location = new System.Drawing.Point(3, 58);
            this._conditionPanel.Name = "_conditionPanel";
            this._conditionPanel.Size = new System.Drawing.Size(764, 495);
            this._conditionPanel.TabIndex = 4;
            // 
            // _conditionScreen
            // 
            this._conditionScreen._isChange = false;
            this._conditionScreen.BackColor = System.Drawing.Color.Transparent;
            this._conditionScreen.Dock = System.Windows.Forms.DockStyle.Fill;
            this._conditionScreen.Location = new System.Drawing.Point(0, 0);
            this._conditionScreen.Name = "_conditionScreen";
            this._conditionScreen.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this._conditionScreen.Size = new System.Drawing.Size(764, 274);
            this._conditionScreen.TabIndex = 0;
            // 
            // _panelCondition
            // 
            this._panelCondition.Controls.Add(this._conditionGrid);
            this._panelCondition.Controls.Add(this.toolStrip1);
            this._panelCondition.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._panelCondition.Location = new System.Drawing.Point(0, 274);
            this._panelCondition.Name = "_panelCondition";
            this._panelCondition.Size = new System.Drawing.Size(764, 221);
            this._panelCondition.TabIndex = 2;
            // 
            // _conditionGrid
            // 
            this._conditionGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this._conditionGrid.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this._conditionGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this._conditionGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this._name,
            this._row,
            this._column,
            this._span,
            this._resource_code,
            this._text,
            this._type,
            this._command,
            this._default,
            this._column_name});
            this._conditionGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this._conditionGrid.Location = new System.Drawing.Point(0, 25);
            this._conditionGrid.Name = "_conditionGrid";
            this._conditionGrid.Size = new System.Drawing.Size(764, 196);
            this._conditionGrid.TabIndex = 1;
            // 
            // _name
            // 
            this._name.HeaderText = "Name";
            this._name.Name = "_name";
            // 
            // _row
            // 
            this._row.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this._row.FillWeight = 50F;
            this._row.HeaderText = "Row";
            this._row.Name = "_row";
            this._row.Width = 54;
            // 
            // _column
            // 
            this._column.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this._column.FillWeight = 50F;
            this._column.HeaderText = "Column";
            this._column.Name = "_column";
            this._column.Width = 55;
            // 
            // _span
            // 
            this._span.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this._span.HeaderText = "Span";
            this._span.Name = "_span";
            this._span.Width = 55;
            // 
            // _resource_code
            // 
            this._resource_code.FillWeight = 150F;
            this._resource_code.HeaderText = "Code";
            this._resource_code.Name = "_resource_code";
            // 
            // _text
            // 
            this._text.HeaderText = "Text";
            this._text.Name = "_text";
            this._text.ReadOnly = true;
            // 
            // _type
            // 
            this._type.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this._type.HeaderText = "Type";
            this._type.Items.AddRange(new object[] {
            "Text",
            "Number",
            "Date",
            "DropDown"});
            this._type.Name = "_type";
            this._type.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this._type.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this._type.Width = 110;
            // 
            // _command
            // 
            this._command.HeaderText = "Command";
            this._command.Name = "_command";
            // 
            // _default
            // 
            this._default.HeaderText = "Default";
            this._default.Name = "_default";
            // 
            // _column_name
            // 
            this._column_name.HeaderText = "Column Name";
            this._column_name.Name = "_column_name";
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._upButton,
            this._downButton,
            this.toolStripSeparator1,
            this._previewButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(764, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // _upButton
            // 
            this._upButton.Image = global::SMLFastReport.Properties.Resources.arrow_up_green;
            this._upButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._upButton.Name = "_upButton";
            this._upButton.Size = new System.Drawing.Size(75, 22);
            this._upButton.Text = "Move Up";
            this._upButton.Click += new System.EventHandler(this._upButton_Click);
            // 
            // _downButton
            // 
            this._downButton.Image = global::SMLFastReport.Properties.Resources.arrow_down_green;
            this._downButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._downButton.Name = "_downButton";
            this._downButton.Size = new System.Drawing.Size(91, 22);
            this._downButton.Text = "Move Down";
            this._downButton.Click += new System.EventHandler(this._downButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // _previewButton
            // 
            this._previewButton.Image = global::SMLFastReport.Properties.Resources.gear_run;
            this._previewButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._previewButton.Name = "_previewButton";
            this._previewButton.Size = new System.Drawing.Size(121, 22);
            this._previewButton.Text = "Condtion Preview";
            this._previewButton.Click += new System.EventHandler(this._previewButton_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this._reportRefTextbox);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this._reportNameTextBox);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel1.Size = new System.Drawing.Size(764, 55);
            this.panel1.TabIndex = 2;
            // 
            // _reportRefTextbox
            // 
            this._reportRefTextbox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this._reportRefTextbox.Location = new System.Drawing.Point(79, 28);
            this._reportRefTextbox.Name = "_reportRefTextbox";
            this._reportRefTextbox.Size = new System.Drawing.Size(678, 21);
            this._reportRefTextbox.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Report Ref";
            // 
            // _query1
            // 
            this._query1.Controls.Add(this._queryDesign1);
            this._query1.ImageKey = "server_document.png";
            this._query1.Location = new System.Drawing.Point(4, 23);
            this._query1.Name = "_query1";
            this._query1.Size = new System.Drawing.Size(770, 556);
            this._query1.TabIndex = 1;
            this._query1.Text = "Query 1";
            this._query1.UseVisualStyleBackColor = true;
            // 
            // _queryDesign1
            // 
            this._queryDesign1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._queryDesign1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._queryDesign1.Location = new System.Drawing.Point(0, 0);
            this._queryDesign1.Name = "_queryDesign1";
            this._queryDesign1.Size = new System.Drawing.Size(770, 556);
            this._queryDesign1.TabIndex = 0;
            // 
            // _query2
            // 
            this._query2.Controls.Add(this._queryDesign2);
            this._query2.ImageKey = "server_document.png";
            this._query2.Location = new System.Drawing.Point(4, 23);
            this._query2.Name = "_query2";
            this._query2.Size = new System.Drawing.Size(770, 556);
            this._query2.TabIndex = 2;
            this._query2.Text = "Query 2";
            this._query2.UseVisualStyleBackColor = true;
            // 
            // _queryDesign2
            // 
            this._queryDesign2.Dock = System.Windows.Forms.DockStyle.Fill;
            this._queryDesign2.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._queryDesign2.Location = new System.Drawing.Point(0, 0);
            this._queryDesign2.Name = "_queryDesign2";
            this._queryDesign2.Size = new System.Drawing.Size(770, 556);
            this._queryDesign2.TabIndex = 0;
            // 
            // _query3
            // 
            this._query3.Controls.Add(this._queryDesign3);
            this._query3.ImageKey = "server_document.png";
            this._query3.Location = new System.Drawing.Point(4, 23);
            this._query3.Name = "_query3";
            this._query3.Size = new System.Drawing.Size(770, 556);
            this._query3.TabIndex = 3;
            this._query3.Text = "Query 3";
            this._query3.UseVisualStyleBackColor = true;
            // 
            // _queryDesign3
            // 
            this._queryDesign3.Dock = System.Windows.Forms.DockStyle.Fill;
            this._queryDesign3.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._queryDesign3.Location = new System.Drawing.Point(0, 0);
            this._queryDesign3.Name = "_queryDesign3";
            this._queryDesign3.Size = new System.Drawing.Size(770, 556);
            this._queryDesign3.TabIndex = 0;
            // 
            // _query4
            // 
            this._query4.Controls.Add(this._queryDesign4);
            this._query4.ImageKey = "server_document.png";
            this._query4.Location = new System.Drawing.Point(4, 23);
            this._query4.Name = "_query4";
            this._query4.Size = new System.Drawing.Size(770, 556);
            this._query4.TabIndex = 4;
            this._query4.Text = "Query 4";
            this._query4.UseVisualStyleBackColor = true;
            // 
            // _queryDesign4
            // 
            this._queryDesign4.Dock = System.Windows.Forms.DockStyle.Fill;
            this._queryDesign4.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._queryDesign4.Location = new System.Drawing.Point(0, 0);
            this._queryDesign4.Name = "_queryDesign4";
            this._queryDesign4.Size = new System.Drawing.Size(770, 556);
            this._queryDesign4.TabIndex = 1;
            // 
            // _query5
            // 
            this._query5.Controls.Add(this._queryDesign5);
            this._query5.ImageKey = "server_document.png";
            this._query5.Location = new System.Drawing.Point(4, 23);
            this._query5.Name = "_query5";
            this._query5.Size = new System.Drawing.Size(770, 556);
            this._query5.TabIndex = 5;
            this._query5.Text = "Query 5";
            this._query5.UseVisualStyleBackColor = true;
            // 
            // _queryDesign5
            // 
            this._queryDesign5.Dock = System.Windows.Forms.DockStyle.Fill;
            this._queryDesign5.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._queryDesign5.Location = new System.Drawing.Point(0, 0);
            this._queryDesign5.Name = "_queryDesign5";
            this._queryDesign5.Size = new System.Drawing.Size(770, 556);
            this._queryDesign5.TabIndex = 2;
            // 
            // _query6
            // 
            this._query6.Controls.Add(this._queryDesign6);
            this._query6.ImageKey = "server_document.png";
            this._query6.Location = new System.Drawing.Point(4, 23);
            this._query6.Name = "_query6";
            this._query6.Size = new System.Drawing.Size(770, 556);
            this._query6.TabIndex = 6;
            this._query6.Text = "Query 6";
            this._query6.UseVisualStyleBackColor = true;
            // 
            // _queryDesign6
            // 
            this._queryDesign6.Dock = System.Windows.Forms.DockStyle.Fill;
            this._queryDesign6.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._queryDesign6.Location = new System.Drawing.Point(0, 0);
            this._queryDesign6.Name = "_queryDesign6";
            this._queryDesign6.Size = new System.Drawing.Size(770, 556);
            this._queryDesign6.TabIndex = 2;
            // 
            // _query7
            // 
            this._query7.Controls.Add(this._queryDesign7);
            this._query7.ImageKey = "server_document.png";
            this._query7.Location = new System.Drawing.Point(4, 23);
            this._query7.Name = "_query7";
            this._query7.Size = new System.Drawing.Size(770, 556);
            this._query7.TabIndex = 7;
            this._query7.Text = "Query 7";
            this._query7.UseVisualStyleBackColor = true;
            // 
            // _queryDesign7
            // 
            this._queryDesign7.Dock = System.Windows.Forms.DockStyle.Fill;
            this._queryDesign7.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._queryDesign7.Location = new System.Drawing.Point(0, 0);
            this._queryDesign7.Name = "_queryDesign7";
            this._queryDesign7.Size = new System.Drawing.Size(770, 556);
            this._queryDesign7.TabIndex = 2;
            // 
            // _tabAccount
            // 
            this._tabAccount.Controls.Add(this._queryAccount);
            this._tabAccount.Location = new System.Drawing.Point(4, 23);
            this._tabAccount.Name = "_tabAccount";
            this._tabAccount.Size = new System.Drawing.Size(770, 556);
            this._tabAccount.TabIndex = 8;
            this._tabAccount.Text = "Account";
            this._tabAccount.UseVisualStyleBackColor = true;
            // 
            // _queryAccount
            // 
            this._queryAccount.Dock = System.Windows.Forms.DockStyle.Fill;
            this._queryAccount.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._queryAccount.Location = new System.Drawing.Point(0, 0);
            this._queryAccount.Name = "_queryAccount";
            this._queryAccount.Size = new System.Drawing.Size(770, 556);
            this._queryAccount.TabIndex = 0;
            // 
            // _iconList
            // 
            this._iconList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("_iconList.ImageStream")));
            this._iconList.TransparentColor = System.Drawing.Color.Transparent;
            this._iconList.Images.SetKeyName(0, "document_gear.png");
            this._iconList.Images.SetKeyName(1, "server_document.png");
            // 
            // _tabReportOption
            // 
            this._tabReportOption.Controls.Add(this._lineFooterLastPageCheckbox);
            this._tabReportOption.Controls.Add(this._splitRowCheckbox);
            this._tabReportOption.Location = new System.Drawing.Point(4, 23);
            this._tabReportOption.Name = "_tabReportOption";
            this._tabReportOption.Size = new System.Drawing.Size(770, 556);
            this._tabReportOption.TabIndex = 9;
            this._tabReportOption.Text = "Report Options";
            this._tabReportOption.UseVisualStyleBackColor = true;
            // 
            // _splitRowCheckbox
            // 
            this._splitRowCheckbox.AutoSize = true;
            this._splitRowCheckbox.Location = new System.Drawing.Point(21, 20);
            this._splitRowCheckbox.Name = "_splitRowCheckbox";
            this._splitRowCheckbox.Size = new System.Drawing.Size(142, 17);
            this._splitRowCheckbox.TabIndex = 0;
            this._splitRowCheckbox.Text = "ตัดบรรทัดเมื่อขึ้นหน้าใหม่";
            this._splitRowCheckbox.UseVisualStyleBackColor = true;
            // 
            // _lineFooterLastPageCheckbox
            // 
            this._lineFooterLastPageCheckbox.AutoSize = true;
            this._lineFooterLastPageCheckbox.Location = new System.Drawing.Point(21, 43);
            this._lineFooterLastPageCheckbox.Name = "_lineFooterLastPageCheckbox";
            this._lineFooterLastPageCheckbox.Size = new System.Drawing.Size(169, 17);
            this._lineFooterLastPageCheckbox.TabIndex = 1;
            this._lineFooterLastPageCheckbox.Text = "แสดงเส้นท้ายเฉพาะหน้าสุดท้าย";
            this._lineFooterLastPageCheckbox.UseVisualStyleBackColor = true;
            // 
            // _configForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(778, 583);
            this.Controls.Add(this._tabs);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "_configForm";
            this.Text = "Config";
            this._tabs.ResumeLayout(false);
            this._condition.ResumeLayout(false);
            this._conditionPanel.ResumeLayout(false);
            this._panelCondition.ResumeLayout(false);
            this._panelCondition.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._conditionGrid)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this._query1.ResumeLayout(false);
            this._query2.ResumeLayout(false);
            this._query3.ResumeLayout(false);
            this._query4.ResumeLayout(false);
            this._query5.ResumeLayout(false);
            this._query6.ResumeLayout(false);
            this._query7.ResumeLayout(false);
            this._tabAccount.ResumeLayout(false);
            this._tabReportOption.ResumeLayout(false);
            this._tabReportOption.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox _reportNameTextBox;
        private System.Windows.Forms.TabControl _tabs;
        private System.Windows.Forms.TabPage _condition;
        private System.Windows.Forms.TabPage _query1;
        private System.Windows.Forms.TabPage _query2;
        private System.Windows.Forms.TabPage _query3;
        private _queryControl _queryDesign1;
        private _queryControl _queryDesign2;
        private _queryControl _queryDesign3;
        public MyLib._myPanel _conditionPanel;
        private System.Windows.Forms.Panel panel1;
        public _conditionControl _conditionScreen;
        private System.Windows.Forms.TabPage _query4;
        private _queryControl _queryDesign4;
        private System.Windows.Forms.TabPage _query5;
        private System.Windows.Forms.TabPage _query6;
        private System.Windows.Forms.TabPage _query7;
        private _queryControl _queryDesign5;
        private _queryControl _queryDesign6;
        private _queryControl _queryDesign7;
        private System.Windows.Forms.Panel _panelCondition;
        private System.Windows.Forms.DataGridView _conditionGrid;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton _upButton;
        private System.Windows.Forms.ToolStripButton _downButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn _name;
        private System.Windows.Forms.DataGridViewTextBoxColumn _row;
        private System.Windows.Forms.DataGridViewTextBoxColumn _column;
        private System.Windows.Forms.DataGridViewTextBoxColumn _span;
        private System.Windows.Forms.DataGridViewTextBoxColumn _resource_code;
        private System.Windows.Forms.DataGridViewTextBoxColumn _text;
        private System.Windows.Forms.DataGridViewComboBoxColumn _type;
        private System.Windows.Forms.DataGridViewTextBoxColumn _command;
        private System.Windows.Forms.DataGridViewTextBoxColumn _default;
        private System.Windows.Forms.DataGridViewTextBoxColumn _column_name;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ImageList _iconList;
        public System.Windows.Forms.ToolStripButton _previewButton;
        private System.Windows.Forms.TabPage _tabAccount;
        private _queryAccountControl _queryAccount;
        private System.Windows.Forms.TextBox _reportRefTextbox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TabPage _tabReportOption;
        private System.Windows.Forms.CheckBox _lineFooterLastPageCheckbox;
        private System.Windows.Forms.CheckBox _splitRowCheckbox;
    }
}