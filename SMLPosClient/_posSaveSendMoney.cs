using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace SMLPosClient
{
    public partial class _posSaveSendMoney : UserControl
    {
        MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();

        public _posSaveSendMoney()
        {
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this._myToolBar.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }
            this.SuspendLayout();
            _myManageData1._dataList._buttonNew.Visible = false;
            _myManageData1._dataList._buttonNewFromTemp.Visible = false;

            _myManageData1._dataList._lockRecord = true; // ใช้แบบ Lock Record ต้องมี guid_code ด้วยนะ อย่าลืม
            _myManageData1._dataList._loadViewFormat(_g.g._search_screen_pos_save_send_money, MyLib._myGlobal._userSearchScreenGroup, true);
            _myManageData1._dataList._extraWhereEvent += new MyLib.ExtraWhereEventHandler(_dataList__extraWhereEvent);
            _myManageData1._dataList._referFieldAdd(_g.d.POSCashierSettle._DocNo, 1);
            _myManageData1._manageButton = this._myToolBar;
            _myManageData1._loadDataToScreen += new MyLib.LoadDataToScreen(_myManageData1__loadDataToScreen);
            _myManageData1._discardData += new MyLib.DiscardDataEvent(_myManageData1__discardData);
            _myManageData1._closeScreen += new MyLib.CloseScreenEvent(_myManageData1__closeScreen);
            _myManageData1._clearData += new MyLib.ClearDataEvent(_myManageData1__clearData);
            _myManageData1._dataList._deleteData += new MyLib.DeleteDataEventHandler(_dataList__deleteData);
            this._pos_save_send_money_screen1._saveKeyDown += new MyLib.SaveKeyDownHandler(_pos_save_send_money_screen1__saveKeyDown);       
            _myManageData1._dataListOpen = true;
            _myManageData1._calcArea();
            //_myManageData1._dataList._loadViewData(0);
            _myManageData1._autoSize = true;
            _myManageData1._autoSizeHeight = 350;
            this._save.Click += new EventHandler(_save_Click);
            this._close.Click += new EventHandler(_close_Click);
            this.ResumeLayout();

        }

        string _dataList__extraWhereEvent()
        {
            string __result = _g.d.POSCashierSettle._trans_type + "=2";
            return __result;
        }

        void _pos_save_send_money_screen1__saveKeyDown(object sender)
        {
            _save_data();
        }

        bool _myManageData1__discardData()
        {
            bool result = true;
            if (this._pos_save_send_money_screen1._isChange)
            {
                if (DialogResult.No == MyLib._myGlobal._displayWarning(3, null))
                {
                    result = false;
                }
                else
                {                    
                    this._pos_save_send_money_screen1._isChange = false;
                    this._pos_save_send_money_screen1._clear();
                }
            }
            return (result);

        }

        void _myManageData1__clearData()
        {
            this._pos_save_send_money_screen1._clear();
        }

        void _dataList__deleteData(System.Collections.ArrayList selectRowOrder)
        {
            StringBuilder __myQuery = new StringBuilder();
            __myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");
            for (int __loop = 0; __loop < selectRowOrder.Count; __loop++)
            {
                MyLib._deleteDataType getData = (MyLib._deleteDataType)selectRowOrder[__loop];
                __myQuery.Append(string.Format(MyLib._myUtil._convertTextToXmlForQuery("delete from {0} {1}"), _myManageData1._dataList._tableName, getData.whereString + " AND " + _g.d.POSCashierSettle._trans_type + "=2"));
            } // for
            __myQuery.Append("</node>");
            string result = _myFrameWork._queryList(MyLib._myGlobal._databaseName, __myQuery.ToString());
            if (result.Length == 0)
            {
                MyLib._myGlobal._displayWarning(1, MyLib._myGlobal._resource("สำเร็จ"));
                _myManageData1._dataList._refreshData();
            }
            else
            {
                MessageBox.Show(result, "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        bool _myManageData1__loadDataToScreen(object rowData, string whereString, bool forEdit)
        {
            try
            {
                DataSet getData = _myFrameWork._query(MyLib._myGlobal._databaseName, "select * from " + _myManageData1._dataList._tableName + whereString + " AND " + _g.d.POSCashierSettle._trans_type + "=2");
                this._pos_save_send_money_screen1._loadData(getData.Tables[0]);
                Control codeControl = this._pos_save_send_money_screen1._getControl(_g.d.m_information._ic_code);
                codeControl.Enabled = false;
                //this._pos_save_send_money_screen1._search(false);
                this._pos_save_send_money_screen1._isChange = false;
                if (forEdit)
                {
                    this._pos_save_send_money_screen1._focusFirst();
                }
                return (true);
            }
            catch (Exception)
            {
            }
            return (false);

        }

        void _save_Click(object sender, EventArgs e)
        {
            _save_data();
        }

        void _close_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        void _myManageData1__closeScreen()
        {
            this.Dispose();
        }

        void _save_data()
        {
            string getEmtry = this._pos_save_send_money_screen1._checkEmtryField();
            if (getEmtry.Length > 0)
            {
                MyLib._myGlobal._displayWarning(2, getEmtry);
            }
            else
            {

                ArrayList __getData = this._pos_save_send_money_screen1._createQueryForDatabase();
                StringBuilder __myQuery = new StringBuilder();
                __myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                if (_myManageData1._mode == 1)
                {
                    __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _myManageData1._dataList._tableName + " (" + __getData[0].ToString() + ", " + _g.d.POSCashierSettle._trans_type + ") values (" + __getData[1].ToString() + ", 2)"));
                }
                else
                {
                    __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _myManageData1._dataList._tableName + " set " + __getData[2].ToString() + _myManageData1._dataList._whereString + " AND " + _g.d.POSCashierSettle._trans_type + "=2"));
                }
                //
                __myQuery.Append("</node>");
                string result = _myFrameWork._queryList(MyLib._myGlobal._databaseName, __myQuery.ToString());
                if (result.Length == 0)
                {
                    MyLib._myGlobal._displayWarning(1, null);
                    this._pos_save_send_money_screen1._isChange = false;
                    if (_myManageData1._mode == 1)
                    {
                        _myManageData1._afterInsertData();
                    }
                    else
                    {
                        _myManageData1._afterUpdateData();
                    }
                    this._pos_save_send_money_screen1._clear();
                    this._pos_save_send_money_screen1._focusFirst();
                }
                else
                {
                    MessageBox.Show(result, "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F12)
            {
                _save_data();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

    }

    public class _pos_save_send_money_screen : MyLib._myScreen
    {
        public _pos_save_send_money_screen()
        {
            string __formatNumber = MyLib._myGlobal._getFormatNumber("m02");

            this._maxColumn = 2;
            this._table_name = _g.d.POSCashierSettle._table;
            this._maxColumn = 2;
            int __row = 0;
            this._addTextBox(__row++, 0, _g.d.POSCashierSettle._DocNo, 1);
            this._addDateBox(__row, 0, 1, 0, _g.d.POSCashierSettle._DocDate, 1, true);
            this._addTextBox(__row++, 1, _g.d.POSCashierSettle._doc_time, 1);
            this._addTextBox(__row++, 0, _g.d.POSCashierSettle._MACHINECODE, 1);
            this._addDateBox(__row, 0, 1, 0, _g.d.POSCashierSettle._begin_date, 1, true);
            this._addTextBox(__row++, 1, _g.d.POSCashierSettle._begin_time, 1);

            this._addTextBox(__row++, 0, _g.d.POSCashierSettle._begin_user_code, 1);
            __row++;
            this._addNumberBox(__row, 0, 1, 0, _g.d.POSCashierSettle._total_in, 1, 2, true, __formatNumber);
            this._addNumberBox(__row++, 1, 1, 0, _g.d.POSCashierSettle._total_out_credit, 1, 2, true, __formatNumber);

            this._addNumberBox(__row, 0, 1, 0, _g.d.POSCashierSettle._total_amount, 1, 2, true, __formatNumber);
            this._addNumberBox(__row++, 1, 1, 0, _g.d.POSCashierSettle._total_out, 1, 2, true, __formatNumber);

            this._addNumberBox(__row, 0, 1, 0, _g.d.POSCashierSettle._total_cancel, 1, 2, true, __formatNumber);
            this._addNumberBox(__row++, 1, 1, 0, _g.d.POSCashierSettle._total_out_coupon, 1, 2, true, __formatNumber);

            this._addNumberBox(__row++, 0, 1, 0, _g.d.POSCashierSettle._total_balance, 1, 2, true, __formatNumber);
            this._addNumberBox(__row++, 0, 1, 0, _g.d.POSCashierSettle._total_credit_charge, 1, 2, true, __formatNumber);

            this._addNumberBox(__row++, 0, 1, 0, _g.d.POSCashierSettle._sale_credit_amount, 1, 2, true, __formatNumber);

            this._addNumberBox(__row++, 0, 1, 0, _g.d.POSCashierSettle._total_diff, 1, 2, true, __formatNumber);

            this._addNumberBox(__row++, 0, 1, 0, _g.d.POSCashierSettle._total_advance, 1, 2, true, __formatNumber);
            this._addNumberBox(__row++, 0, 1, 0, _g.d.POSCashierSettle._total_after_advance, 1, 2, true, __formatNumber);

            this._addNumberBox(__row, 0, 1, 0, _g.d.POSCashierSettle._total_cash, 1, 2, true, __formatNumber);
            this._addNumberBox(__row++, 1, 1, 0, _g.d.POSCashierSettle._CashAmount, 1, 2, true, __formatNumber);

            this._addNumberBox(__row, 0, 1, 0, _g.d.POSCashierSettle._total_credit_card, 1, 2, true, __formatNumber);
            this._addNumberBox(__row++, 1, 1, 0, _g.d.POSCashierSettle._CreditCardAmount, 1, 2, true, __formatNumber);

            this._addNumberBox(__row, 0, 1, 0, _g.d.POSCashierSettle._total_coupon, 1, 2, true, __formatNumber);
            this._addNumberBox(__row++, 1, 1, 0, _g.d.POSCashierSettle._CoupongAmount, 1, 2, true, __formatNumber);

            // ยอดแต้ัม
            this._addNumberBox(__row++, 0, 1, 0, _g.d.POSCashierSettle._total_point, 1, 2, true, __formatNumber);

            this._addNumberBox(__row, 0, 1, 0, _g.d.POSCashierSettle._total_sum, 1, 2, true, __formatNumber);
            this._addNumberBox(__row++, 1, 1, 0, _g.d.POSCashierSettle._sum_amount, 1, 2, true, __formatNumber);

            this._addTextBox(__row++, 0, _g.d.POSCashierSettle._CashierCode, 0);
            this._addTextBox(__row++, 0, 1, 0, _g.d.POSCashierSettle._cashier_password, 1, 0, 0, true, true);

            this._addTextBox(__row, 0, 2, _g.d.POSCashierSettle._remark, 2, 1);
            //
            this._enabedControl(_g.d.POSCashierSettle._DocNo, false);
            this._enabedControl(_g.d.POSCashierSettle._DocDate, false);
            this._enabedControl(_g.d.POSCashierSettle._doc_time, false);
            this._enabedControl(_g.d.POSCashierSettle._MACHINECODE, false);
            this._enabedControl(_g.d.POSCashierSettle._begin_date, false);
            this._enabedControl(_g.d.POSCashierSettle._begin_time, false);
            this._enabedControl(_g.d.POSCashierSettle._begin_user_code, false);
            this._enabedControl(_g.d.POSCashierSettle._total_in, false);
            this._enabedControl(_g.d.POSCashierSettle._total_out, false);
            this._enabedControl(_g.d.POSCashierSettle._total_out_credit, false);
            this._enabedControl(_g.d.POSCashierSettle._total_out_coupon, false);
            this._enabedControl(_g.d.POSCashierSettle._total_amount, false);
            this._enabedControl(_g.d.POSCashierSettle._total_cancel, false);
            this._enabedControl(_g.d.POSCashierSettle._total_balance, false);
            this._enabedControl(_g.d.POSCashierSettle._total_cash, false);
            this._enabedControl(_g.d.POSCashierSettle._total_credit_card, false);
            this._enabedControl(_g.d.POSCashierSettle._total_diff, false);
            this._enabedControl(_g.d.POSCashierSettle._total_coupon, false);
            this._enabedControl(_g.d.POSCashierSettle._total_sum, false);
            this._enabedControl(_g.d.POSCashierSettle._sum_amount, false);
            this._enabedControl(_g.d.POSCashierSettle._total_point, false);
            this._enabedControl(_g.d.POSCashierSettle._total_advance, false);
            this._enabedControl(_g.d.POSCashierSettle._total_after_advance, false);
            this._enabedControl(_g.d.POSCashierSettle._sale_credit_amount, false);

            this._textBoxChanged += (s1, e1) =>
            {
                decimal __calc = this._getDataNumber(_g.d.POSCashierSettle._CashAmount) + this._getDataNumber(_g.d.POSCashierSettle._CreditCardAmount) + this._getDataNumber(_g.d.POSCashierSettle._CoupongAmount);
                this._setDataNumber(_g.d.POSCashierSettle._sum_amount, __calc);
            };

        }
    }
}
