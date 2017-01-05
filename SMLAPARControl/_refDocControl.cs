using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace SMLERPAPARControl
{
    public partial class _refDocControl : UserControl
    {
        private _g.g._transControlTypeEnum _controlTypeTemp;
        private MyLib._searchDataFull _aparTransSearch;
        public string _custCode = "";
        string _doc_date = "";
        public MyLib._myGrid _transGrid = new MyLib._myGrid();

        object[] _ap_bill_type = new object[] { _g.d.ap_ar_trans._po_pay_bill };
        object[] _ar_bill_type = new object[] { _g.d.ap_ar_trans._so_pay_bill };
        ArrayList _ap_ar_bill_type_arraylist = new ArrayList();
        string __formatNumber = MyLib._myGlobal._getFormatNumber("m02");
        public _refDocControl()
        {
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this._toolBar.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }
        }

        public _g.g._transControlTypeEnum _transControlType
        {
            set
            {
                this._controlTypeTemp = value;
                this._build();
                this.Invalidate();
            }
            get
            {
                return this._controlTypeTemp;
            }
        }

        private void _build()
        {
            if (this._controlTypeTemp == _g.g._transControlTypeEnum.ว่าง)
            {
                return;
            }
            this._transGrid.Dock = DockStyle.Fill;
            this._transGrid._table_name = _g.d.ap_ar_trans_detail._table;
            switch (this._controlTypeTemp)
            {
                case _g.g._transControlTypeEnum.เจ้าหนี้_จ่ายชำระหนี้:
                    //
                    //this._transGrid._addColumn(_g.d.ap_ar_trans._status, 10, 25, 20, false);
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._billing_no, 1, 0, 25, true, false, true, true);
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._billing_date, 4, 0, 20, false, false, true, true);
                    /*this._transGrid._addColumn(_g.d.ap_ar_trans._cust_code, 4, 0, 30, false, false, true, true);
                    this._transGrid._addColumn(_g.d.ap_ar_trans._total_net_value, 3, 1, 15, false, false, true, false, __formatNumber);*/
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._remark, 1, 0, 15, false, false);
                    this._transGrid._cellComboBoxGet += new MyLib.CellComboBoxItemGetDisplay(_transGrid__cellComboBoxGet);
                    this._transGrid._cellComboBoxItem += new MyLib.CellComboBoxItemEventHandler(_transGrid__cellComboBoxItem);
                    this._grouper1.Controls.Add(this._transGrid);
                    break;

                case _g.g._transControlTypeEnum.ลูกหนี้_รับชำระหนี้:
                    //
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._billing_no, 1, 0, 25, true, false, true, true);
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._billing_date, 4, 0, 20, false, false, true, true);
                    /*this._transGrid._addColumn(_g.d.ap_ar_trans._cust_code, 4, 0, 30, false, false, true, true);
                    this._transGrid._addColumn(_g.d.ap_ar_trans._total_net_value, 3, 1, 15, false, false, true, false, __formatNumber);*/
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._remark, 1, 0, 15, false, false);
                    this._transGrid._cellComboBoxGet += new MyLib.CellComboBoxItemGetDisplay(_transGrid__cellComboBoxGet);
                    this._transGrid._cellComboBoxItem += new MyLib.CellComboBoxItemEventHandler(_transGrid__cellComboBoxItem);
                    this._grouper1.Controls.Add(this._transGrid);
                    break;
            }

            this._transGrid.ShowTotal = true;
            this._transGrid.ColumnBackgroundAuto = false;
            this._transGrid.ColumnBackgroundEnd = Color.FromArgb(198, 220, 249);
            this._transGrid.RowOddBackground = Color.AliceBlue;
            this._transGrid.AutoSize = true;
            this._transGrid._calcPersentWidthToScatter();
            this.Invalidate();
            if (this._controlTypeTemp.Equals(_g.g._transControlTypeEnum.เจ้าหนี้_จ่ายชำระหนี้))
            {
                _ap_ar_bill_type_arraylist = new ArrayList();
                for (int __loop = 0; __loop < this._ap_bill_type.Length; __loop++)
                {
                    string __fieldName = _g.d.ap_ar_trans._table + "." + this._ap_bill_type[__loop];
                    this._ap_ar_bill_type_arraylist.Add(MyLib._myResource._findResource(__fieldName, __fieldName)._str);
                }
            }
            else if (this._controlTypeTemp.Equals(_g.g._transControlTypeEnum.ลูกหนี้_รับชำระหนี้))
            {
                _ap_ar_bill_type_arraylist = new ArrayList();
                for (int __loop = 0; __loop < this._ar_bill_type.Length; __loop++)
                {
                    string __fieldName = _g.d.ap_ar_trans._table + "." + this._ar_bill_type[__loop];
                    this._ap_ar_bill_type_arraylist.Add(MyLib._myResource._findResource(__fieldName, __fieldName)._str);
                }
            }

            this._transGrid._clickSearchButton -= new MyLib.SearchEventHandler(_transGrid__clickSearchButton);
            this._transGrid._alterCellUpdate -= new MyLib.AfterCellUpdateEventHandler(_transGrid__alterCellUpdate);

            this._transGrid._clickSearchButton += new MyLib.SearchEventHandler(_transGrid__clickSearchButton);
            this._transGrid._alterCellUpdate += new MyLib.AfterCellUpdateEventHandler(_transGrid__alterCellUpdate);

        }

        void _transGrid__alterCellUpdate(object sender, int row, int column)
        {
            string _cust_code = "";
            decimal _total_net_value = MyLib._myGlobal._decimalPhase("0.00");
            string __remark = "";
            DateTime __docDate = new DateTime(1000, 1, 1);
            //
            MyLib._myGrid __sender = (MyLib._myGrid)sender;
            int __billNoColumnNumber = __sender._findColumnByName(_g.d.ap_ar_trans_detail._billing_no);
            //int __billTypeColumnNumber = __sender._findColumnByName(_g.d.ap_ar_trans_detail._bill_type);
            if (__billNoColumnNumber != -1)
            {
                string __billNo = __sender._cellGet(row, __billNoColumnNumber).ToString();
                if (__billNo.Length > 0)
                {
                    int __addr = -1;
                    for (int __row = 0; __row < __sender._rowData.Count; __row++)
                    {
                        int __billTypeCompare = -1;

                        string __billNoCompare = __sender._cellGet(__row, __billNoColumnNumber).ToString().ToUpper();
                        if (__billNo.Equals(__billNoCompare.ToUpper()))
                        {
                            __addr = __row;
                            break;
                        }
                    }

                    if (__addr != row && __addr != -1)
                    {
                        MessageBox.Show(MyLib._myGlobal._resource("เอกสารซ้ำ"));
                        this._transGrid._cellUpdate(row, __billNoColumnNumber, "", true);
                    }
                    else
                    {
                        string __extraWhere = "";
                        switch (this._controlTypeTemp)
                        {
                            case _g.g._transControlTypeEnum.เจ้าหนี้_จ่ายชำระหนี้:
                                __extraWhere = "(" + _g.d.ap_ar_trans._trans_type + "= 1) and (" + _g.d.ap_ar_trans._trans_flag + " in (" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เจ้าหนี้_ใบรับวางบิล).ToString() + ")) and (" + _g.d.ap_ar_trans._doc_no + "=\'" + __billNo + "\')";
                                break;
                            case _g.g._transControlTypeEnum.ลูกหนี้_รับชำระหนี้:
                                __extraWhere = "(" + _g.d.ap_ar_trans._trans_type + "= 2) and (" + _g.d.ap_ar_trans._trans_flag + " in (" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ลูกหนี้_ใบวางบิล).ToString() + ")) and (" + _g.d.ap_ar_trans._doc_no + "=\'" + __billNo + "\')";
                                break;
                        }

                        if (_g.g._companyProfile._branchStatus == 1 && _g.g._companyProfile._show_branch_doc_only == true && (_g.g._companyProfile._change_branch_code == false))
                        {
                            __extraWhere += " and ( " + _g.d.ap_ar_trans._branch_code + "=\'" + MyLib._myGlobal._branchCode + "\' ) ";
                        }

                        MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                        string __query = "select " + _g.d.ap_ar_trans._doc_date + "," + _g.d.ap_ar_trans._remark + " from " + _g.d.ap_ar_trans._table + " where " + _g.d.ap_ar_trans._doc_no + "=\'" + __billNo + "\'" +  " and " +  __extraWhere;
                        DataSet __result = __myFrameWork._query(MyLib._myGlobal._databaseName, __query);
                        if (__result.Tables[0].Rows.Count > 0)
                        {
                            __remark = __result.Tables[0].Rows[0][_g.d.ap_ar_trans._remark].ToString();
                            __docDate = MyLib._myGlobal._convertDateFromQuery(__result.Tables[0].Rows[0][_g.d.ap_ar_trans._doc_date].ToString());
                            //__amount = MyLib._myGlobal._decimalPhase(__result.Tables[0].Rows[0][_g.d.ic_trans._total_amount].ToString());
                            //__balance = MyLib._myGlobal._decimalPhase(__result.Tables[0].Rows[0]["balance"].ToString());
                        }
                        else
                        {
                            MessageBox.Show(MyLib._myGlobal._resource("ไม่พบเอกสาร"));
                            this._transGrid._cellUpdate(row, __billNoColumnNumber, "", true);
                        }

                    }
                }

                //__sender._cellUpdate(row, _g.d.ap_ar_trans_detail._billing_date, MyLib._myGlobal._convertDate(_doc_date), false);
                __sender._cellUpdate(row, _g.d.ap_ar_trans_detail._billing_date, __docDate, false);
                /*__sender._cellUpdate(row, _g.d.ap_ar_trans_detail._cust_code, _cust_code, false);
                __sender._cellUpdate(row, _g.d.ap_ar_trans_detail._total_net_value, _total_net_value, false);*/
                __sender._cellUpdate(row, _g.d.ap_ar_trans_detail._remark, __remark, false);
            }
            __sender.Invalidate();
        }

        void _transGrid__clickSearchButton(object sender, MyLib.GridCellEventArgs e)
        {
            int __columnNumber = this._transGrid._findColumnByName(_g.d.ap_ar_trans_detail._billing_no);
            _displayAPARTransSearch(((MyLib._myGrid._columnType)this._transGrid._columnList[__columnNumber])._name, this._custCode, sender, false);
        }

        public void _displayAPARTransSearch(string name, string cust_code, object owner, Boolean whereDocNo)
        {
            // ค้นหาเอกสารอ้างอิง ตามประเภทที่สัมพันธ์กัน
            this._aparTransSearch = new MyLib._searchDataFull();
            this._aparTransSearch._owner = owner;
            this._aparTransSearch.StartPosition = FormStartPosition.CenterScreen;
            this._aparTransSearch.Text = name;
            this._aparTransSearch._name = name;
            string __templateName = "";
            string __extraWhere = "";
            switch (this._controlTypeTemp)
            {
                case _g.g._transControlTypeEnum.เจ้าหนี้_จ่ายชำระหนี้:
                    __templateName = _g.g._screen_ap_trans;
                    //__extraWhere = "(" + _g.d.ap_ar_trans._trans_flag + " in (" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เจ้าหนี้_ใบรับวางบิล).ToString() + ")) and  (" + _g.d.ap_ar_trans._cust_code + "=\'" + cust_code + "\') and  (doc_no not in  ((select distinct billing_no from ap_ar_trans_detail where (trans_flag  in (" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เจ้าหนี้_จ่ายชำระหนี้).ToString() + ")))))";
                    __extraWhere = "(" + _g.d.ap_ar_trans._trans_flag + " in (" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เจ้าหนี้_ใบรับวางบิล).ToString() + ")) and  " + _g.d.ap_ar_trans._cust_code + "=\'" + cust_code + "\' and coalesce(doc_success, 0)=0 "; 
                    break;
                case _g.g._transControlTypeEnum.ลูกหนี้_รับชำระหนี้:
                    __templateName = _g.g._screen_ar_trans;
                    //__extraWhere = "(" + _g.d.ap_ar_trans._trans_flag + " in (" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ลูกหนี้_ใบวางบิล).ToString() + ")) and (" + _g.d.ap_ar_trans._cust_code + "=\'" + cust_code + "\') and  (doc_no not in  ((select distinct billing_no from ap_ar_trans_detail where (trans_flag  in (" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ลูกหนี้_รับชำระหนี้).ToString() + ")))))";
                    __extraWhere = "(" + _g.d.ap_ar_trans._trans_flag + " in (" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ลูกหนี้_ใบวางบิล).ToString() + ")) and " + _g.d.ap_ar_trans._cust_code + "=\'" + cust_code + "\' and coalesce(doc_success, 0)=0 ";
                    break;
            }

            if (_g.g._companyProfile._branchStatus == 1 && _g.g._companyProfile._show_branch_doc_only == true && (_g.g._companyProfile._change_branch_code == false))
            {
                __extraWhere += " and ( " + _g.d.ap_ar_trans._branch_code + "=\'" + MyLib._myGlobal._branchCode + "\' ) ";
            }

            if (__templateName.Length == 0)
            {
                MessageBox.Show(MyLib._myGlobal._resource("กรุณเลือกประเภทเอกสารก่อน"));
            }
            else
            {
                this._aparTransSearch._dataList._loadViewFormat(__templateName, MyLib._myGlobal._userSearchScreenGroup, false);
                this._aparTransSearch._dataList._extraWhere = __extraWhere;
                this._aparTransSearch._dataList._refreshData();
                //
                this._aparTransSearch._dataList._gridData._mouseClick -= new MyLib.MouseClickHandler(_gridData__mouseClick);
                this._aparTransSearch._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
                //
                this._aparTransSearch._searchEnterKeyPress -= new MyLib.SearchEnterKeyPressEventHandler(_aparTransSearch__searchEnterKeyPress);
                this._aparTransSearch._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_aparTransSearch__searchEnterKeyPress);
                this._aparTransSearch.ShowDialog();
            }
        }

        void _aparTransSearch__searchEnterKeyPress(MyLib._myGrid sender, int row)
        {
            _aparTransSearchSetData(sender, row);
        }

        void _gridData__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            _aparTransSearchSetData((MyLib._myGrid)sender, e._row);
        }

        void _aparTransSearchSetData(MyLib._myGrid sender, int row)
        {
            this._aparTransSearch.Close();
            string __data = sender._cellGet(row, _g.d.ap_ar_trans._table + "." + _g.d.ap_ar_trans._doc_no).ToString();
            _doc_date = sender._cellGet(row, _g.d.ap_ar_trans._table + "." + _g.d.ap_ar_trans._doc_date).ToString();
            MyLib._myGrid __ownerGrid = (MyLib._myGrid)this._aparTransSearch._owner;
            int __columnNumber = __ownerGrid._findColumnByName(_g.d.ap_ar_trans_detail._billing_no);
            __ownerGrid._cellUpdate(__ownerGrid.SelectRow, __columnNumber, __data, true);
            __ownerGrid.Invalidate();
        }

        object[] _transGrid__cellComboBoxItem(object sender, int row, int column)
        {
            switch (this._controlTypeTemp)
            {
                case _g.g._transControlTypeEnum.เจ้าหนี้_จ่ายชำระหนี้: return _ap_ar_bill_type_arraylist.ToArray();
                case _g.g._transControlTypeEnum.ลูกหนี้_รับชำระหนี้: return _ap_ar_bill_type_arraylist.ToArray();
            }
            return null;
        }

        string _transGrid__cellComboBoxGet(object sender, int row, int column, string columnName, int select)
        {
            switch (this._controlTypeTemp)
            {
                case _g.g._transControlTypeEnum.เจ้าหนี้_จ่ายชำระหนี้: return _ap_ar_bill_type_arraylist[(select == -1) ? 0 : select].ToString();
                case _g.g._transControlTypeEnum.ลูกหนี้_รับชำระหนี้: return _ap_ar_bill_type_arraylist[(select == -1) ? 0 : select].ToString();
            }
            return null;
        }
    }
}
