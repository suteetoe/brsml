namespace SMLPOSControl._food
{
    partial class _orderControl
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
            this._topFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this._openTableButton = new MyLib._fancyButton();
            this._orderButton = new MyLib._fancyButton();
            this._cancelOrderButton = new MyLib._fancyButton();
            this._editOrderButton = new MyLib._fancyButton();
            this._moveTableButton = new MyLib._fancyButton();
            this._tableCloseButton = new MyLib._fancyButton();
            this._tableReOpenButton = new MyLib._fancyButton();
            this._tableButton = new MyLib._fancyButton();
            this._confirmButton = new MyLib._fancyButton();
            this._reservedTableButton = new MyLib._fancyButton();
            this._reservedTableCancelButton = new MyLib._fancyButton();
            this._orderPackButton = new MyLib._fancyButton();
            this._telephoneOrderButton = new MyLib._fancyButton();
            this._closeButton = new MyLib._fancyButton();
            this._workPanel = new System.Windows.Forms.Panel();
            this._orderSpeechTimer = new System.Windows.Forms.Timer(this.components);
            this._topFlowLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // _topFlowLayoutPanel
            // 
            this._topFlowLayoutPanel.AutoSize = true;
            this._topFlowLayoutPanel.Controls.Add(this._openTableButton);
            this._topFlowLayoutPanel.Controls.Add(this._orderButton);
            this._topFlowLayoutPanel.Controls.Add(this._cancelOrderButton);
            this._topFlowLayoutPanel.Controls.Add(this._editOrderButton);
            this._topFlowLayoutPanel.Controls.Add(this._moveTableButton);
            this._topFlowLayoutPanel.Controls.Add(this._tableCloseButton);
            this._topFlowLayoutPanel.Controls.Add(this._tableReOpenButton);
            this._topFlowLayoutPanel.Controls.Add(this._tableButton);
            this._topFlowLayoutPanel.Controls.Add(this._confirmButton);
            this._topFlowLayoutPanel.Controls.Add(this._reservedTableButton);
            this._topFlowLayoutPanel.Controls.Add(this._reservedTableCancelButton);
            this._topFlowLayoutPanel.Controls.Add(this._orderPackButton);
            this._topFlowLayoutPanel.Controls.Add(this._telephoneOrderButton);
            this._topFlowLayoutPanel.Controls.Add(this._closeButton);
            this._topFlowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this._topFlowLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this._topFlowLayoutPanel.Name = "_topFlowLayoutPanel";
            this._topFlowLayoutPanel.Padding = new System.Windows.Forms.Padding(3);
            this._topFlowLayoutPanel.Size = new System.Drawing.Size(1200, 77);
            this._topFlowLayoutPanel.TabIndex = 0;
            // 
            // _openTableButton
            // 
            this._openTableButton._blueAmountText = 0;
            this._openTableButton._drawNewMethod = false;
            this._openTableButton._id = null;
            this._openTableButton.BackColor = System.Drawing.Color.Transparent;
            this._openTableButton.ButtonColor = System.Drawing.Color.Blue;
            this._openTableButton.ButtonText = "เปิดโต๊ะใหม่";
            this._openTableButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._openTableButton.ImageSize = new System.Drawing.Size(32, 32);
            this._openTableButton.Location = new System.Drawing.Point(6, 6);
            this._openTableButton.myImage = global::SMLPOSControl.Properties.Resources.note_add;
            this._openTableButton.myImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this._openTableButton.myTextAlign = System.Drawing.ContentAlignment.TopCenter;
            this._openTableButton.Name = "_openTableButton";
            this._openTableButton.ResourceName = "เปิดโต๊ะใหม่";
            this._openTableButton.Size = new System.Drawing.Size(70, 65);
            this._openTableButton.TabIndex = 0;
            this._openTableButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this._openTableButton.Click += new System.EventHandler(this._openTableButton_Click);
            // 
            // _orderButton
            // 
            this._orderButton._blueAmountText = 0;
            this._orderButton._drawNewMethod = false;
            this._orderButton._id = null;
            this._orderButton.BackColor = System.Drawing.Color.Transparent;
            this._orderButton.ButtonColor = System.Drawing.Color.DarkMagenta;
            this._orderButton.ButtonText = "สั่งอาหาร";
            this._orderButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._orderButton.ImageSize = new System.Drawing.Size(32, 32);
            this._orderButton.Location = new System.Drawing.Point(82, 6);
            this._orderButton.myImage = global::SMLPOSControl.Properties.Resources.contract;
            this._orderButton.myImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this._orderButton.myTextAlign = System.Drawing.ContentAlignment.TopCenter;
            this._orderButton.Name = "_orderButton";
            this._orderButton.ResourceName = "สั่งอาหาร";
            this._orderButton.Size = new System.Drawing.Size(70, 65);
            this._orderButton.TabIndex = 2;
            this._orderButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this._orderButton.Click += new System.EventHandler(this._orderButton_Click);
            // 
            // _cancelOrderButton
            // 
            this._cancelOrderButton._blueAmountText = 0;
            this._cancelOrderButton._drawNewMethod = false;
            this._cancelOrderButton._id = null;
            this._cancelOrderButton.BackColor = System.Drawing.Color.Transparent;
            this._cancelOrderButton.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this._cancelOrderButton.ButtonText = "คืน /      ยกเลิก";
            this._cancelOrderButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._cancelOrderButton.ImageSize = new System.Drawing.Size(32, 32);
            this._cancelOrderButton.Location = new System.Drawing.Point(158, 6);
            this._cancelOrderButton.myImage = global::SMLPOSControl.Properties.Resources.delete21;
            this._cancelOrderButton.myImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this._cancelOrderButton.myTextAlign = System.Drawing.ContentAlignment.TopCenter;
            this._cancelOrderButton.Name = "_cancelOrderButton";
            this._cancelOrderButton.ResourceName = "คืน /      ยกเลิก";
            this._cancelOrderButton.Size = new System.Drawing.Size(70, 65);
            this._cancelOrderButton.TabIndex = 11;
            this._cancelOrderButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this._cancelOrderButton.Click += new System.EventHandler(this._cancelOrderButton_Click);
            // 
            // _editOrderButton
            // 
            this._editOrderButton._blueAmountText = 0;
            this._editOrderButton._drawNewMethod = false;
            this._editOrderButton._id = null;
            this._editOrderButton.BackColor = System.Drawing.Color.Transparent;
            this._editOrderButton.ButtonColor = System.Drawing.Color.Orange;
            this._editOrderButton.ButtonText = "แก้ไขรายการ";
            this._editOrderButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._editOrderButton.ImageSize = new System.Drawing.Size(32, 32);
            this._editOrderButton.Location = new System.Drawing.Point(234, 6);
            this._editOrderButton.myImage = global::SMLPOSControl.Properties.Resources.weight2;
            this._editOrderButton.myImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this._editOrderButton.myTextAlign = System.Drawing.ContentAlignment.TopCenter;
            this._editOrderButton.Name = "_editOrderButton";
            this._editOrderButton.ResourceName = "แก้ไขรายการ";
            this._editOrderButton.Size = new System.Drawing.Size(70, 65);
            this._editOrderButton.TabIndex = 13;
            this._editOrderButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this._editOrderButton.Click += new System.EventHandler(this._editOrderButton_Click);
            // 
            // _moveTableButton
            // 
            this._moveTableButton._blueAmountText = 0;
            this._moveTableButton._drawNewMethod = false;
            this._moveTableButton._id = null;
            this._moveTableButton.BackColor = System.Drawing.Color.Transparent;
            this._moveTableButton.ButtonText = "ย้ายโต๊ะ";
            this._moveTableButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._moveTableButton.ImageSize = new System.Drawing.Size(32, 32);
            this._moveTableButton.Location = new System.Drawing.Point(310, 6);
            this._moveTableButton.myImage = global::SMLPOSControl.Properties.Resources.clipboard_next;
            this._moveTableButton.myImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this._moveTableButton.myTextAlign = System.Drawing.ContentAlignment.TopCenter;
            this._moveTableButton.Name = "_moveTableButton";
            this._moveTableButton.ResourceName = "ย้ายโต๊ะ";
            this._moveTableButton.Size = new System.Drawing.Size(70, 65);
            this._moveTableButton.TabIndex = 3;
            this._moveTableButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this._moveTableButton.Click += new System.EventHandler(this._moveTableButton_Click);
            // 
            // _tableCloseButton
            // 
            this._tableCloseButton._blueAmountText = 0;
            this._tableCloseButton._drawNewMethod = false;
            this._tableCloseButton._id = null;
            this._tableCloseButton.BackColor = System.Drawing.Color.Transparent;
            this._tableCloseButton.ButtonColor = System.Drawing.Color.Crimson;
            this._tableCloseButton.ButtonText = "ปิดโต๊ะ";
            this._tableCloseButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._tableCloseButton.ImageSize = new System.Drawing.Size(32, 32);
            this._tableCloseButton.Location = new System.Drawing.Point(386, 6);
            this._tableCloseButton.myImage = global::SMLPOSControl.Properties.Resources.form_yellow;
            this._tableCloseButton.myImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this._tableCloseButton.myTextAlign = System.Drawing.ContentAlignment.TopCenter;
            this._tableCloseButton.Name = "_tableCloseButton";
            this._tableCloseButton.ResourceName = "ปิดโต๊ะ";
            this._tableCloseButton.Size = new System.Drawing.Size(70, 65);
            this._tableCloseButton.TabIndex = 5;
            this._tableCloseButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this._tableCloseButton.Click += new System.EventHandler(this._tableCloseButton_Click);
            // 
            // _tableReOpenButton
            // 
            this._tableReOpenButton._blueAmountText = 0;
            this._tableReOpenButton._drawNewMethod = false;
            this._tableReOpenButton._id = null;
            this._tableReOpenButton.BackColor = System.Drawing.Color.Transparent;
            this._tableReOpenButton.ButtonColor = System.Drawing.Color.MediumPurple;
            this._tableReOpenButton.ButtonText = "ยกเลิก    ปิดโต๊ะ";
            this._tableReOpenButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._tableReOpenButton.ImageSize = new System.Drawing.Size(32, 32);
            this._tableReOpenButton.Location = new System.Drawing.Point(462, 6);
            this._tableReOpenButton.myImage = global::SMLPOSControl.Properties.Resources.index_replace;
            this._tableReOpenButton.myImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this._tableReOpenButton.myTextAlign = System.Drawing.ContentAlignment.TopCenter;
            this._tableReOpenButton.Name = "_tableReOpenButton";
            this._tableReOpenButton.ResourceName = "ยกเลิก    ปิดโต๊ะ";
            this._tableReOpenButton.Size = new System.Drawing.Size(70, 65);
            this._tableReOpenButton.TabIndex = 12;
            this._tableReOpenButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this._tableReOpenButton.Click += new System.EventHandler(this._tableReOpenButton_Click);
            // 
            // _tableButton
            // 
            this._tableButton._blueAmountText = 0;
            this._tableButton._drawNewMethod = false;
            this._tableButton._id = null;
            this._tableButton.BackColor = System.Drawing.Color.Transparent;
            this._tableButton.ButtonText = "สถานะโต๊ะ";
            this._tableButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._tableButton.ImageSize = new System.Drawing.Size(32, 32);
            this._tableButton.Location = new System.Drawing.Point(538, 6);
            this._tableButton.myImage = global::SMLPOSControl.Properties.Resources.scroll_information;
            this._tableButton.myImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this._tableButton.myTextAlign = System.Drawing.ContentAlignment.TopCenter;
            this._tableButton.Name = "_tableButton";
            this._tableButton.ResourceName = "สถานะโต๊ะ";
            this._tableButton.Size = new System.Drawing.Size(70, 65);
            this._tableButton.TabIndex = 2;
            this._tableButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this._tableButton.Click += new System.EventHandler(this._tableButton_Click);
            // 
            // _confirmButton
            // 
            this._confirmButton._blueAmountText = 0;
            this._confirmButton._drawNewMethod = false;
            this._confirmButton._id = null;
            this._confirmButton.BackColor = System.Drawing.Color.Transparent;
            this._confirmButton.ButtonText = "Confirm";
            this._confirmButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._confirmButton.ImageSize = new System.Drawing.Size(32, 32);
            this._confirmButton.Location = new System.Drawing.Point(614, 6);
            this._confirmButton.myImage = global::SMLPOSControl.Properties.Resources.notebook_preferences;
            this._confirmButton.myImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this._confirmButton.myTextAlign = System.Drawing.ContentAlignment.TopCenter;
            this._confirmButton.Name = "_confirmButton";
            this._confirmButton.ResourceName = "Confirm";
            this._confirmButton.Size = new System.Drawing.Size(70, 65);
            this._confirmButton.TabIndex = 14;
            this._confirmButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this._confirmButton.Visible = false;
            this._confirmButton.Click += new System.EventHandler(this._confirmButton_Click);
            // 
            // _reservedTableButton
            // 
            this._reservedTableButton._blueAmountText = 0;
            this._reservedTableButton._drawNewMethod = false;
            this._reservedTableButton._id = null;
            this._reservedTableButton.BackColor = System.Drawing.Color.Transparent;
            this._reservedTableButton.ButtonText = "จองโต๊ะ";
            this._reservedTableButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._reservedTableButton.ImageSize = new System.Drawing.Size(32, 32);
            this._reservedTableButton.Location = new System.Drawing.Point(690, 6);
            this._reservedTableButton.myImage = global::SMLPOSControl.Properties.Resources.book_blue_add;
            this._reservedTableButton.myImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this._reservedTableButton.myTextAlign = System.Drawing.ContentAlignment.TopCenter;
            this._reservedTableButton.Name = "_reservedTableButton";
            this._reservedTableButton.ResourceName = "จองโต๊ะ";
            this._reservedTableButton.Size = new System.Drawing.Size(70, 65);
            this._reservedTableButton.TabIndex = 8;
            this._reservedTableButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this._reservedTableButton.Visible = false;
            this._reservedTableButton.Click += new System.EventHandler(this._reservedTableButton_Click);
            // 
            // _reservedTableCancelButton
            // 
            this._reservedTableCancelButton._blueAmountText = 0;
            this._reservedTableCancelButton._drawNewMethod = false;
            this._reservedTableCancelButton._id = null;
            this._reservedTableCancelButton.BackColor = System.Drawing.Color.Transparent;
            this._reservedTableCancelButton.ButtonText = "ยกเลิกจองโต๊ะ";
            this._reservedTableCancelButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._reservedTableCancelButton.ImageSize = new System.Drawing.Size(32, 32);
            this._reservedTableCancelButton.Location = new System.Drawing.Point(766, 6);
            this._reservedTableCancelButton.myImage = global::SMLPOSControl.Properties.Resources.book_blue_delete;
            this._reservedTableCancelButton.myImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this._reservedTableCancelButton.myTextAlign = System.Drawing.ContentAlignment.TopCenter;
            this._reservedTableCancelButton.Name = "_reservedTableCancelButton";
            this._reservedTableCancelButton.ResourceName = "ยกเลิกจองโต๊ะ";
            this._reservedTableCancelButton.Size = new System.Drawing.Size(70, 65);
            this._reservedTableCancelButton.TabIndex = 9;
            this._reservedTableCancelButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this._reservedTableCancelButton.Visible = false;
            this._reservedTableCancelButton.Click += new System.EventHandler(this._reservedTableCancelButton_Click);
            // 
            // _orderPackButton
            // 
            this._orderPackButton._blueAmountText = 0;
            this._orderPackButton._drawNewMethod = false;
            this._orderPackButton._id = null;
            this._orderPackButton.BackColor = System.Drawing.Color.Transparent;
            this._orderPackButton.ButtonColor = System.Drawing.Color.DeepPink;
            this._orderPackButton.ButtonText = "สั่งกลับบ้าน";
            this._orderPackButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._orderPackButton.ImageSize = new System.Drawing.Size(32, 32);
            this._orderPackButton.Location = new System.Drawing.Point(842, 6);
            this._orderPackButton.myImage = global::SMLPOSControl.Properties.Resources.box_into;
            this._orderPackButton.myImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this._orderPackButton.myTextAlign = System.Drawing.ContentAlignment.TopCenter;
            this._orderPackButton.Name = "_orderPackButton";
            this._orderPackButton.ResourceName = "สั่งกลับบ้าน";
            this._orderPackButton.Size = new System.Drawing.Size(70, 65);
            this._orderPackButton.TabIndex = 6;
            this._orderPackButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this._orderPackButton.Visible = false;
            // 
            // _telephoneOrderButton
            // 
            this._telephoneOrderButton._blueAmountText = 0;
            this._telephoneOrderButton._drawNewMethod = false;
            this._telephoneOrderButton._id = null;
            this._telephoneOrderButton.BackColor = System.Drawing.Color.Transparent;
            this._telephoneOrderButton.ButtonColor = System.Drawing.Color.DarkBlue;
            this._telephoneOrderButton.ButtonText = "บริการส่ง Delivery";
            this._telephoneOrderButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._telephoneOrderButton.ImageSize = new System.Drawing.Size(32, 32);
            this._telephoneOrderButton.Location = new System.Drawing.Point(918, 6);
            this._telephoneOrderButton.myImage = global::SMLPOSControl.Properties.Resources.telephone;
            this._telephoneOrderButton.myImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this._telephoneOrderButton.myTextAlign = System.Drawing.ContentAlignment.TopCenter;
            this._telephoneOrderButton.Name = "_telephoneOrderButton";
            this._telephoneOrderButton.ResourceName = "บริการส่ง Delivery";
            this._telephoneOrderButton.Size = new System.Drawing.Size(70, 65);
            this._telephoneOrderButton.TabIndex = 7;
            this._telephoneOrderButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this._telephoneOrderButton.Visible = false;
            // 
            // _closeButton
            // 
            this._closeButton._blueAmountText = 0;
            this._closeButton._drawNewMethod = false;
            this._closeButton._id = null;
            this._closeButton.BackColor = System.Drawing.Color.Transparent;
            this._closeButton.ButtonColor = System.Drawing.Color.Red;
            this._closeButton.ButtonText = "ปิดจอ";
            this._closeButton.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._closeButton.ImageSize = new System.Drawing.Size(32, 32);
            this._closeButton.Location = new System.Drawing.Point(994, 6);
            this._closeButton.myImage = global::SMLPOSControl.Properties.Resources.power_off_2;
            this._closeButton.myImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this._closeButton.myTextAlign = System.Drawing.ContentAlignment.TopCenter;
            this._closeButton.Name = "_closeButton";
            this._closeButton.ResourceName = "ปิดจอ";
            this._closeButton.Size = new System.Drawing.Size(70, 65);
            this._closeButton.TabIndex = 10;
            this._closeButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this._closeButton.Click += new System.EventHandler(this._closeButton_Click);
            // 
            // _workPanel
            // 
            this._workPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this._workPanel.Location = new System.Drawing.Point(0, 77);
            this._workPanel.Name = "_workPanel";
            this._workPanel.Size = new System.Drawing.Size(1200, 661);
            this._workPanel.TabIndex = 1;
            // 
            // _orderSpeechTimer
            // 
            this._orderSpeechTimer.Interval = 1000;
            //this._orderSpeechTimer.Tick += new System.EventHandler(this._orderSpeechTimer_Tick);
            // 
            // _orderControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.Controls.Add(this._workPanel);
            this.Controls.Add(this._topFlowLayoutPanel);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "_orderControl";
            this.Size = new System.Drawing.Size(1200, 738);
            this._topFlowLayoutPanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel _topFlowLayoutPanel;
        private MyLib._fancyButton _openTableButton;
        private MyLib._fancyButton _moveTableButton;
        private MyLib._fancyButton _tableCloseButton;
        private MyLib._fancyButton _tableButton;
        private MyLib._fancyButton _orderPackButton;
        private MyLib._fancyButton _telephoneOrderButton;
        private MyLib._fancyButton _reservedTableButton;
        private MyLib._fancyButton _reservedTableCancelButton;
        private MyLib._fancyButton _orderButton;
        private System.Windows.Forms.Panel _workPanel;
        private MyLib._fancyButton _closeButton;
        private MyLib._fancyButton _cancelOrderButton;
        private MyLib._fancyButton _tableReOpenButton;
        private MyLib._fancyButton _editOrderButton;
        private MyLib._fancyButton _confirmButton;
        private System.Windows.Forms.Timer _orderSpeechTimer;
    }
}
