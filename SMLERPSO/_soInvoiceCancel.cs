using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLERPSO
{
    public partial class _soInvoiceCancel : UserControl
    {
        public _soInvoiceCancel(string menuName, string extraWhere, Boolean readOnly)
        {
            InitializeComponent();
            if (readOnly)
            {
                this._icTrans._myManageTrans._readOnly = true;
                this._icTrans._myManageTrans._isAdd = false;
                this._icTrans._myManageTrans._isEdit = false;
                this._icTrans._myManageTrans._isDelete = false;
                //this._icTrans._myManageTrans._dataList._fullMode = false;
            }
            this._icTrans._menuName = menuName;
            this._icTrans._dataListExtraWhere = extraWhere;
            this._icTrans.Disposed += new EventHandler(_icTrans_Disposed);
        }

        void _icTrans_Disposed(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
