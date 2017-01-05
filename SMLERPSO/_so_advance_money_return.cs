using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLERPSO
{
    public partial class _so_advance_money_return : UserControl
    {
        public _so_advance_money_return()
        {
            InitializeComponent();
            this._po_so_deposit_control1._screen_code = "SDR";
            this._po_so_deposit_control1._closeButton.Click += new EventHandler(_closeButton_Click);
            this._po_so_deposit_control1._myManageData1._closeScreen += new MyLib.CloseScreenEvent(_myManageData1__closeScreen);
            this._po_so_deposit_control1._buttonPrint.Click += new EventHandler(_buttonPrint_Click);
        }

        void _buttonPrint_Click(object sender, EventArgs e)
        {
            try
            {
                string __getDocNo = "";
                string __getDocDate = "";
                string __getArCode = "";
                DateTime __dateDocDate;
                __getDocNo = this._po_so_deposit_control1._screenTop._getDataStr(_g.d.ic_trans._doc_no);
                __dateDocDate = MyLib._myGlobal._convertDate(this._po_so_deposit_control1._screenTop._getDataStr(_g.d.ic_trans._doc_date));
                __getDocDate = MyLib._myGlobal._convertDateToQuery(__dateDocDate);
                __getArCode = this._po_so_deposit_control1._screenTop._getDataStr(_g.d.ic_trans._cust_code);
                if (__getDocNo.Length > 0 && __getArCode.Length > 0)
                {
                    SMLInventoryControl._formSaleOrderPreview __showPrintDialog = new SMLInventoryControl._formSaleOrderPreview(SMLInventoryControl._gForm._formEnum.ขาย_คืนเงินล่วงหน้า.ToString(), __getDocNo, __getDocDate, __getArCode);
                    __showPrintDialog.ShowDialog();
                }
                else
                {
                    MessageBox.Show(MyLib._myGlobal._resource("กรุณาป้อนเลขที่เอกสาร และลูกหนี้"));
                }

            }
            catch (Exception ex)
            {
            }
        }

        void _myManageData1__closeScreen()
        {
            this.Dispose();
        }

        void _closeButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
