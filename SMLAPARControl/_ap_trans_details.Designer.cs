namespace SMLERPAPARControl
{
    partial class _ap_trans_details
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
            this._myTabControl1 = new MyLib._myTabControl();
            this._payable = new System.Windows.Forms.TabPage();
            this._ap_pay1 = new SMLERPAPARControl._payControl();
            this._withHoldingTax = new System.Windows.Forms.TabPage();
            this._withHoldingTaxGive1 = new SMLERPGLControl._withHoldingTaxGive();
            this._vat_buy = new System.Windows.Forms.TabPage();
            this._vatList = new SMLERPGLControl._vatBuy();
            this._gl = new System.Windows.Forms.TabPage();
            this._glDetail = new SMLERPGLControl._glDetail();
            this._myTabControl1.SuspendLayout();
            this._payable.SuspendLayout();
            this._withHoldingTax.SuspendLayout();
            this._vat_buy.SuspendLayout();
            this._gl.SuspendLayout();
            this.SuspendLayout();
            // 
            // _myTabControl1
            // 
            this._myTabControl1.Controls.Add(this._payable);
            this._myTabControl1.Controls.Add(this._withHoldingTax);
            this._myTabControl1.Controls.Add(this._vat_buy);
            this._myTabControl1.Controls.Add(this._gl);
            this._myTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._myTabControl1.Location = new System.Drawing.Point(0, 0);
            this._myTabControl1.Multiline = true;
            this._myTabControl1.Name = "_myTabControl1";
            this._myTabControl1.SelectedIndex = 0;
            this._myTabControl1.Size = new System.Drawing.Size(1162, 728);
            this._myTabControl1.TabIndex = 0;
            this._myTabControl1.TableName = "";
            // 
            // _payable
            // 
            this._payable.Controls.Add(this._ap_pay1);
            this._payable.Location = new System.Drawing.Point(4, 23);
            this._payable.Name = "_payable";
            this._payable.Padding = new System.Windows.Forms.Padding(3);
            this._payable.Size = new System.Drawing.Size(1154, 701);
            this._payable.TabIndex = 0;
            this._payable.Text = "_payable";
            this._payable.UseVisualStyleBackColor = true;
            // 
            // _ap_pay1
            // 
            this._ap_pay1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._ap_pay1._from_screen = null;
            this._ap_pay1.Location = new System.Drawing.Point(0, 0);
            this._ap_pay1.Name = "_ap_pay1";
            this._ap_pay1._result = null;
            this._ap_pay1.Size = new System.Drawing.Size(762, 384);
           // this._ap_pay1._sum_amount = 0D;
            this._ap_pay1.TabIndex = 0;
            // 
            // _withHoldingTax
            // 
            this._withHoldingTax.Controls.Add(this._withHoldingTaxGive1);
            this._withHoldingTax.Location = new System.Drawing.Point(4, 23);
            this._withHoldingTax.Name = "_withHoldingTax";
            this._withHoldingTax.Padding = new System.Windows.Forms.Padding(3);
            this._withHoldingTax.Size = new System.Drawing.Size(1154, 701);
            this._withHoldingTax.TabIndex = 1;
            this._withHoldingTax.Text = "_withHoldingTax";
            this._withHoldingTax.UseVisualStyleBackColor = true;
            // 
            // _withHoldingTaxGive1
            // 
            this._withHoldingTaxGive1.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._withHoldingTaxGive1.Location = new System.Drawing.Point(77, 45);
            this._withHoldingTaxGive1.Name = "_withHoldingTaxGive1";
            this._withHoldingTaxGive1.Size = new System.Drawing.Size(663, 438);
            this._withHoldingTaxGive1.TabIndex = 0;
            // 
            // _vat_buy
            // 
            this._vat_buy.Controls.Add(this._vatList);
            this._vat_buy.Location = new System.Drawing.Point(4, 23);
            this._vat_buy.Name = "_vat_buy";
            this._vat_buy.Padding = new System.Windows.Forms.Padding(3);
            this._vat_buy.Size = new System.Drawing.Size(1154, 701);
            this._vat_buy.TabIndex = 2;
            this._vat_buy.Text = "_vat_buy";
            this._vat_buy.UseVisualStyleBackColor = true;
            // 
            // _vatList
            // 
            this._vatList.BackColor = System.Drawing.Color.Transparent;
            this._vatList.Dock = System.Windows.Forms.DockStyle.Fill;
            this._vatList.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._vatList.Location = new System.Drawing.Point(3, 3);
            this._vatList.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._vatList.Name = "_vatList";
            this._vatList.Size = new System.Drawing.Size(1148, 695);
            this._vatList.TabIndex = 1;
            // 
            // _gl
            // 
            this._gl.Controls.Add(this._glDetail);
            this._gl.Location = new System.Drawing.Point(4, 23);
            this._gl.Name = "_gl";
            this._gl.Padding = new System.Windows.Forms.Padding(3);
            this._gl.Size = new System.Drawing.Size(1154, 701);
            this._gl.TabIndex = 3;
            this._gl.Text = "_gl";
            this._gl.UseVisualStyleBackColor = true;
            // 
            // _glDetail
            // 
            this._glDetail.BackColor = System.Drawing.Color.Transparent;
            this._glDetail.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._glDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this._glDetail.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._glDetail.Location = new System.Drawing.Point(3, 3);
            this._glDetail.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this._glDetail.Name = "_glDetail";
            this._glDetail.Size = new System.Drawing.Size(1148, 695);
            this._glDetail.TabIndex = 0;
            // 
            // _ap_trans_details
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._myTabControl1);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this.Name = "_ap_trans_details";
            this.Size = new System.Drawing.Size(1162, 728);
            this._myTabControl1.ResumeLayout(false);
            this._payable.ResumeLayout(false);
            this._withHoldingTax.ResumeLayout(false);
            this._vat_buy.ResumeLayout(false);
            this._gl.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private MyLib._myTabControl _myTabControl1;
        private System.Windows.Forms.TabPage _payable;
        private System.Windows.Forms.TabPage _withHoldingTax;
        private System.Windows.Forms.TabPage _vat_buy;
        private System.Windows.Forms.TabPage _gl;
        private SMLERPGLControl._withHoldingTaxGive _withHoldingTaxGive1;
        private SMLERPGLControl._vatBuy _vatList;
        private SMLERPGLControl._glDetail _glDetail;
        private _payControl _ap_pay1;
    }
}
