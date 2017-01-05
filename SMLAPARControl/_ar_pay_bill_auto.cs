using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Threading;

namespace SMLERPAPARControl
{
    public partial class _ar_pay_bill_auto : UserControl
    {
        MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
        _search_bill_auto _myCondition = new _search_bill_auto();
        public _ar_pay_bill_auto()
        {
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this._myToolbar.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }
            this._myButtonChecking.Image = this.imageList1.Images[0];
        }

        private void _save_data()
        {

        }

        private void _myButtonChecking_Click(object sender, EventArgs e)
        {
            if (this._myButtonChecking._iconNumber == 0)
            {
                this._myButtonChecking._name = "ไม่เลือกทั้งหมด (F10)";
                this._myButtonChecking.Image = this.imageList1.Images[1];
            }
            else
            {
                this._myButtonChecking._name = "เลือกทั้งหมด (F11)";
                this._myButtonChecking.Image = this.imageList1.Images[0];
            }
        }

        private void toolStripMyButton1_Click(object sender, EventArgs e)
        {
            this._myCondition.ShowDialog();
            if (!this._myCondition.Result.Equals(""))
            {
                ThreadStart theprogress = new ThreadStart(_process);
                Thread startprogress = new Thread(theprogress);
                startprogress.Priority = ThreadPriority.Highest;
                startprogress.IsBackground = true;
                startprogress.Start();
                startprogress.Abort();
            }
        }

        protected void _process()
        {
            try
            {
               
                DataSet _ds = new DataSet();

                ArrayList _getData = _myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, this._myCondition.Result.ToString());
                for (int i = 0; i < _getData.Count; i++)
                {
                    _ds = ((DataSet)_getData[i]);
                    DataRow[] _dr = _ds.Tables[0].Select(string.Empty);
                    if (_dr.Length > 0)
                    {
                        foreach (DataRow _row in _dr)
                        {
                            string x_1 = _row[_g.d.ap_ar_trans._cust_code].ToString();
                            string x_2 = _row[_g.d.ap_ar_resource._ap_ar_name].ToString();
                            string x_3 = _row[_g.d.ap_ar_trans._doc_no].ToString();
                            string x_4 = _row[_g.d.ap_ar_trans._doc_date].ToString();
                            string x_5 = _row[_g.d.ap_ar_trans._due_date].ToString();

                            int __resultAddr = this._arGridPayBillAuto._addRow();
                            this._arGridPayBillAuto._cellUpdate(__resultAddr, _g.d.ap_ar_trans._cust_code, _row[_g.d.ap_ar_trans._cust_code].ToString(), false);
                            this._arGridPayBillAuto._cellUpdate(__resultAddr, _g.d.ap_ar_resource._ap_ar_name, _row[_g.d.ap_ar_resource._ap_ar_name].ToString(), false);
                            this._arGridPayBillAuto._cellUpdate(__resultAddr, _g.d.ap_ar_trans._doc_no, _row[_g.d.ap_ar_trans._doc_no].ToString(), true);
                            this._arGridPayBillAuto._cellUpdate(__resultAddr, _g.d.ap_ar_trans._doc_date, _row[_g.d.ap_ar_trans._doc_date].ToString(), true);
                            this._arGridPayBillAuto._cellUpdate(__resultAddr, _g.d.ap_ar_trans._due_date, _row[_g.d.ap_ar_trans._due_date].ToString(), true);
                            this._arGridPayBillAuto._cellUpdate(__resultAddr, _g.d.ap_ar_trans._total_debt_balance, _row[_g.d.ap_ar_trans._total_debt_balance].ToString(), false);
                            this._arGridPayBillAuto._cellUpdate(__resultAddr, _g.d.ap_ar_trans._total_pay_money, _row[_g.d.ap_ar_trans._total_pay_money].ToString(), false);
                            //this._arGridPayBillAuto._cellUpdate(__resultAddr, _g.d.ap_ar_trans._remark, _row[_g.d.ap_ar_trans._remark].ToString(), false);
                            this._arGridPayBillAuto._cellUpdate(__resultAddr, "line_number", 1, false);

                        }
                    }
                }
            }
            catch
            {
            }
        }



    }

    public class _arScreenToPPayBillAuto : MyLib._myScreen
    {
        string _searchName = "";
        TextBox _searchTextBox;
        string __formatNumber = MyLib._myGlobal._getFormatNumber("m02");
        public _arScreenToPPayBillAuto()
        {
            this.SuspendLayout();
            this._maxColumn = 2;
            this._addDateBox(0, 0, 1, 0, _g.d.ap_ar_trans._doc_date, 1, true, false);
            this._addNumberBox(1, 0, 1, 0, _g.d.ap_ar_trans._credit_day, 1, 1, true, __formatNumber);
            this._addDateBox(1, 1, 1, 0, _g.d.ap_ar_trans._due_date, 1, true, true);
            this.Padding = new Padding(3);
            //this._textBoxSearch += new MyLib.TextBoxSearchHandler(_screenApControl__textBoxSearch);
            //this._textBoxChanged += new MyLib.TextBoxChangedHandler(_screenApControl__textBoxChanged);
            this.Dock = DockStyle.Top;
            this.AutoSize = true;
            this.ResumeLayout();
        }
    }

    public class _arScreenSearchPayBillAuto : MyLib._myScreen
    {
        string _searchName = "";
        TextBox _searchTextBox;
        MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
        MyLib._searchDataFull _search_data_full = new MyLib._searchDataFull();
        public _arScreenSearchPayBillAuto()
        {
            this.SuspendLayout();
            this._maxColumn = 2;
            this._addDateBox(0, 0, 1, 0, _g.d.ap_ar_resource._from_doc_date, 1, true, true);
            this._addDateBox(0, 1, 1, 0, _g.d.ap_ar_resource._to_doc_date, 1, true, true);
            this._addTextBox(1, 0, 1, 0, _g.d.ap_ar_resource._from_doc_no, 1, 1, 3, true, false, true);
            this._addTextBox(1, 1, 1, 0, _g.d.ap_ar_resource._to_doc_no, 1, 1, 3, true, false, true);
            this._addTextBox(2, 0, 1, 0, _g.d.ap_ar_resource._from_customer, 1, 1, 1, true, false, true);
            this._addTextBox(2, 1, 1, 0, _g.d.ap_ar_resource._to_customer, 1, 1, 1, true, false, true);
            this._addCheckBox(3, 0, _g.d.ap_ar_resource._so_debt_balance, true, false);
            this._addCheckBox(3, 1, _g.d.ap_ar_resource._so_cn_balance, true, false);
            this._addCheckBox(4, 0, _g.d.ap_ar_resource._so_billing, true, false);
            this._addCheckBox(4, 1, _g.d.ap_ar_resource._so_addition_debt, true, false);
            this.Padding = new Padding(3);
            

            MyLib._myTextBox __getControl_1 = (MyLib._myTextBox)this._getControl(_g.d.ap_ar_resource._from_customer);
            if (__getControl_1 != null)
            {
                __getControl_1.textBox.Enter += new EventHandler(textBox_Enter);
                __getControl_1.textBox.Leave += new EventHandler(textBox_Leave);
            }
            MyLib._myTextBox __getControl_2 = (MyLib._myTextBox)this._getControl(_g.d.ap_ar_resource._to_customer);
            if (__getControl_2 != null)
            {
                __getControl_2.textBox.Enter += new EventHandler(textBox_Enter);
                __getControl_2.textBox.Leave += new EventHandler(textBox_Leave);
            }

            this._textBoxSearch += new MyLib.TextBoxSearchHandler(_arScreenSearchPayBillAuto__textBoxSearch);
            this._textBoxChanged += new MyLib.TextBoxChangedHandler(_arScreenSearchPayBillAuto__textBoxChanged);

            this.Dock = DockStyle.Top;
            this.AutoSize = true;
            this.ResumeLayout();
        }

        void textBox_Leave(object sender, EventArgs e)
        {
            this._search_data_full.Visible = false;
        }

        void textBox_Enter(object sender, EventArgs e)
        {
            MyLib._myTextBox _getControl = (MyLib._myTextBox)((Control)sender).Parent;
            _arScreenSearchPayBillAuto__textBoxSearch(_getControl);
            _getControl.textBox.Focus();
        }

        void _arScreenSearchPayBillAuto__textBoxChanged(object sender, string name)
        {

            if (name.Equals(_g.d.ap_ar_trans._cust_code))
            {
                this._searchTextBox = (TextBox)sender;
                this._searchName = name;
                this._search(true);
            }
        }

        void _arScreenSearchPayBillAuto__textBoxSearch(object sender)
        {
            //ค้นหารหัสลูกหนี้
            // ค้นหาหน้าจอ Top
            MyLib._myTextBox __getControl = (MyLib._myTextBox)sender;
            this._searchTextBox = __getControl.textBox;
            this._searchName = ((MyLib._myTextBox)sender)._name.ToLower();
            string label_name = ((MyLib._myTextBox)sender)._labelName;
            string _search_text_new = _g.g._search_screen_ar;
            if (!this._search_data_full._name.Equals(_search_text_new.ToLower()))
            {
                this._search_data_full = new MyLib._searchDataFull();
                this._search_data_full._name = _search_text_new;
                this._search_data_full._dataList._loadViewFormat(this._search_data_full._name, MyLib._myGlobal._userSearchScreenGroup, false);
                this._search_data_full._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
                this._search_data_full._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_search_data_full__searchEnterKeyPress);
            }
            MyLib._myGlobal._startSearchBox(__getControl, label_name, this._search_data_full, false);
        }


        void _search_data_full__searchEnterKeyPress(MyLib._myGrid sender, int row)
        {
            this._searchByParent(sender, row);
        }

        void _searchByParent(object sender, int row)
        {
            MyLib._myDataList __getParent1 = (MyLib._myDataList)((MyLib._myGrid)sender).Parent;
            MyLib._searchDataFull __getParent2 = (MyLib._searchDataFull)__getParent1.Parent;
            _searchAll(__getParent2._name, row);
            SendKeys.Send("{ENTER}");
        }

        void _gridData__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            MyLib._myDataList __getParent1 = (MyLib._myDataList)((MyLib._myGrid)sender).Parent;
            MyLib._searchDataFull __getParent2 = (MyLib._searchDataFull)__getParent1.Parent;
            this._searchAll(__getParent2._name, e._row);
            SendKeys.Send("{ENTER}");
        }

        void _searchAll(string name, int row)
        {
            string _search_text_new = name;
            if (name.Length > 0)
            {
                string result = (string)this._search_data_full._dataList._gridData._cellGet(row, 0);
                if (result.Length != 0)
                {
                    this._search_data_full.Visible = false;
                    this._setDataStr(_searchName, result, "", true);
                    this._search(true);
                }
            }
        }
        public void _search(Boolean warning)
        {
            try
            {
                // ค้นหาชื่อกลุ่มลูกค้า
                StringBuilder __myquery = new StringBuilder();
                __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ar_customer._name_1 + " from " + _g.d.ar_customer._table + " where code=\'" + this._getDataStr(_g.d.ap_ar_resource._from_customer) + "\'"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ar_customer._name_1 + " from " + _g.d.ar_customer._table + " where code=\'" + this._getDataStr(_g.d.ap_ar_resource._to_customer) + "\'"));
                __myquery.Append("</node>");
                ArrayList _getData = _myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
                _searchAndWarning(_g.d.ap_ar_resource._from_customer, (DataSet)_getData[0], warning);
                _searchAndWarning(_g.d.ap_ar_resource._to_customer, (DataSet)_getData[1], warning);
            }
            catch
            {
            }
        }

        bool _searchAndWarning(string fieldName, DataSet _datApesult, Boolean wApning)
        {
            bool __result = false;
            if (_datApesult.Tables[0].Rows.Count > 0)
            {

                string __getData = _datApesult.Tables[0].Rows[0][0].ToString();
                string __getDataStr = this._getDataStr(fieldName);
                this._setDataStr(fieldName, __getDataStr, __getData, true);
            }
            else
            {
                this._setDataStr(fieldName, "", "", true);
            }
            if (this._searchTextBox != null)
            {
                if (this._searchName.CompareTo(fieldName) == 0 && this._getDataStr(fieldName) != "")
                {
                    if (_datApesult.Tables[0].Rows.Count == 0 && wApning)
                    {
                        MessageBox.Show(MyLib._myGlobal._resource("ไม่พบ") + " : " + this._getLabelName(fieldName), MyLib._myGlobal._resource("เตือน"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        ((MyLib._myTextBox)this._searchTextBox.Parent)._textFirst = "";
                        ((MyLib._myTextBox)this._searchTextBox.Parent)._textSecond = "";
                        ((MyLib._myTextBox)this._searchTextBox.Parent)._textLast = "";
                        this._setDataStr(fieldName, "", "", true);
                        this._searchTextBox.Focus();
                        __result = true;
                    }
                }
            }
            return __result;
        }

        void textBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                if (this._search_data_full.Visible)
                {
                    this._search_data_full.Focus();
                    this._search_data_full._firstFocus();
                }
            }
        }

    }

    public class _arGridPayBillAuto : MyLib._myGrid
    {
        MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
        MyLib._searchDataFull _search_data_full = new MyLib._searchDataFull();
        string _searchName = "";

        public _arGridPayBillAuto()
        {
            this._columnList.Clear();
            this._table_name = _g.d.ap_ar_trans._table;
            string __formatNumber = MyLib._myGlobal._getFormatNumber("m02");
            this.SuspendLayout();
            this._addColumn("check", 11, 1, 4, true, false, true, true);
            this._addColumn(_g.d.ap_ar_trans._cust_code, 0, 1, 10, false, false, true, false);
            this._addColumn(_g.d.ap_ar_resource._ap_ar_name, 0, 1, 20, false, false, false, false);
            this._addColumn(_g.d.ap_ar_trans._doc_no, 0, 1, 12, false, false, false);
            this._addColumn(_g.d.ap_ar_trans._doc_date, 4, 1, 15, false, false, false);
            this._addColumn(_g.d.ap_ar_trans._due_date, 4, 1, 15, false, false, false);
            this._addColumn(_g.d.ap_ar_trans._total_debt_balance, 3, 1, 12, false, false, true, false, __formatNumber);
            this._addColumn(_g.d.ap_ar_trans._total_pay_money, 3, 1, 12, false, false, true, false, __formatNumber);
            this._addColumn("row_number", 3, 1, 10, false, true, true);
            this._addColumn("line_number", 1, 1, 10, false, true, true);
            this.ShowTotal = true;
            this.ColumnBackgroundAuto = false;
            this.ColumnBackgroundEnd = Color.FromArgb(198, 220, 249);
            this.RowOddBackground = Color.AliceBlue;
            this.AutoSize = true;
            this.Dock = DockStyle.Fill;
            this.ResumeLayout();
        }
    }
}
