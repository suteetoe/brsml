using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLERPAP
{
    public partial class _ap_debt_billing : UserControl
    {
        public _ap_debt_billing(string menuName)
        {
            InitializeComponent();
            this._ap_screen._screenTop._screen_code = "DE";
            this._ap_screen._myManageData1._closeScreen += new MyLib.CloseScreenEvent(_myManageData1__closeScreen);
            this._ap_screen._closeButton.Click += new EventHandler(_closeButton_Click);
            this._ap_screen._print.Click += new EventHandler(_print_Click);
            this._ap_screen._afterSave += new SMLERPAPARControl._ar_ap_trans._afterSaveEventHandler(_ap_screen__afterSave);
            this._ap_screen._menuName = menuName;
        }

        void _ap_screen__afterSave()
        {
            try
            {
                _printData();
                /*
                try
                {
                    if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLColorStore)
                    {
                        _printData();
                    }
                    else
                    {
                        string __docNo = this._ap_screen._screenTop._getDataStr(_g.d.ap_ar_trans._doc_no);
                        SMLERPReportTool._global._printForm(this._ap_screen._screenTop._docFormatCode, __docNo, _g.g._transFlagGlobal._transFlag(this._ap_screen._transControlType).ToString());
                    }
                }
                catch (Exception ex)
                {
                }
                */
            }
            catch (Exception ex)
            {
            }
        }


        private void _printData()
        {
            // toe
            if (this._ap_screen._printFormAfterSave == false)
            {

                try
                {
                    if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLColorStore)
                    {
                        string __getDocNo = "";
                        string __getDocDate = "";
                        string __getArCode = "";
                        DateTime __dateDocDate;
                        //ap_ar_trans
                        // jead : error
                        __getDocNo = this._ap_screen._screenTop._getDataStr(_g.d.ap_ar_trans._doc_no);
                        __dateDocDate = MyLib._myGlobal._convertDate(this._ap_screen._screenTop._getDataStr(_g.d.ap_ar_trans._doc_date));
                        __getDocDate = MyLib._myGlobal._convertDateToQuery(__dateDocDate);
                        __getArCode = this._ap_screen._screenTop._getDataStr(_g.d.ap_ar_trans._cust_code);
                        if (__getDocNo.Length > 0 && __getArCode.Length > 0)
                        {
                            SMLInventoryControl._formSaleOrderPreview __showPrintDialog = new SMLInventoryControl._formSaleOrderPreview(SMLInventoryControl._gForm._formEnum.เจ้าหนี้_ใบจ่ายชำระหนี้.ToString(), __getDocNo, __getDocDate, __getArCode);
                            __showPrintDialog.ShowDialog();
                        }
                        else
                        {
                            MessageBox.Show(MyLib._myGlobal._resource("กรุณาป้อนเลขที่เอกสาร และเจ้าหนี้"));
                        }
                    }
                    //else
                    //{
                    //    string __docNo = this._ap_screen._screenTop._getDataStr(_g.d.ap_ar_trans._doc_no);
                    //    SMLERPReportTool._global._printForm(this._ap_screen._screenTop._docFormatCode, __docNo, _g.g._transFlagGlobal._transFlag(this._ap_screen._transControlType).ToString());
                    //}

                }
                catch (Exception ex)
                {
                }
            }
        }

        void _print_Click(object sender, EventArgs e)
        {
            try
            {
                _printData();
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
