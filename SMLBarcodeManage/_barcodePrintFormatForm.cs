using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMLBarcodeManage
{
    public partial class _barcodePrintFormatForm : Form
    {
        public string _formatCodeSelect = "";
        public _barcodePrintFormatForm()
        {
            InitializeComponent();

            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            // disable tools button
            this._myDataList._buttonDelete.Enabled = false;
            this._myDataList._buttonSelectAll.Enabled = false;
            this._myDataList._buttonNewFromTemp.Enabled = false;
            this._myDataList._buttonNew.Enabled = false;
            this._myDataList._buttonClose.Click += new EventHandler(_buttonClose_Click);

            // _dataList Use
            this._myDataList._gridData._isEdit = false;
            this._myDataList._lockRecord = true;
            this._myDataList._loadViewFormat("screen_barcode_print_format", MyLib._myGlobal._userSearchScreenGroup, false);
            this._myDataList._referFieldAdd(_g.d.sml_barcode_print_format._code, 1);
            //this._myDataList1._loadViewData(0);

            this._myDataList._gridData._mouseDoubleClick += new MyLib.MouseDoubleClickHandler(_gridData__mouseDoubleClick);


        }

        void _gridData__mouseDoubleClick(object sender, MyLib.GridCellEventArgs e)
        {
            // after select format
            if (e._row != -1)
            {
                _formatCodeSelect = this._myDataList._gridData._cellGet(e._row, 0).ToString();
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
        }

        void _buttonClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
