using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLERPSO
{
    public partial class _soInvoice : UserControl
    {
        public _soInvoice(string menuName, string extraWhere, Boolean readOnly)
        {
            InitializeComponent();
            if (readOnly)
            {
                this._ictrans._myManageTrans._readOnly = true;
                this._ictrans._myManageTrans._isAdd = false;
                this._ictrans._myManageTrans._isEdit = false;
                this._ictrans._myManageTrans._isDelete = true;
                //this._ictrans._myManageTrans._dataList._fullMode = false;

                this._ictrans._myManageTrans._dataList._printRangeButton.Visible = false;
            }
            this._ictrans._menuName = menuName;
            this._ictrans._dataListExtraWhere = extraWhere;
            this._ictrans._myManageTrans.Disposed += new EventHandler(_myManageTrans_Disposed);
        }

        void _myManageTrans_Disposed(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
