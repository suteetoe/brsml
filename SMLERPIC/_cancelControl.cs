using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLERPIC
{
    public partial class _cancelControl : UserControl
    {
        private SMLInventoryControl._icTransControl _icTrans;

        public _cancelControl(_g.g._transControlTypeEnum mode)
        {
            this._icTrans = new SMLInventoryControl._icTransControl();
            this.SuspendLayout();
            // 
            // _icTrans
            // 
            this._icTrans.Dock = System.Windows.Forms.DockStyle.Fill;
            this._icTrans.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
            this._icTrans._transControlType = mode;
            this._icTrans.Location = new System.Drawing.Point(0, 0);
            this._icTrans.Name = "_icTrans";
            this._icTrans.Size = new System.Drawing.Size(703, 569);
            this._icTrans.TabIndex = 0;
            // 
            // _soQuotationCancel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this._icTrans);
            this.Name = "_cancel";
            this.Size = new System.Drawing.Size(703, 569);
            this.ResumeLayout(false);
            //
            this._icTrans._myManageTrans._closeScreen += new MyLib.CloseScreenEvent(_myManageTrans__closeScreen);
        }

        void _myManageTrans__closeScreen()
        {
            this.Dispose();
        }
    }
}
