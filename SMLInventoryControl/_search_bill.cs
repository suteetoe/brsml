using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMLInventoryControl
{
    public partial class _search_bill : Form
    {
        MyLib._myGrid _gridtem;
        MyLib._myFrameWork __frameWork = new MyLib._myFrameWork();
        public string _x_query = "";
        public string _x_screen = "";
        public string _x_table = "";
        public string _ictransControlTypeEnumTemp;
        public string _fieldNameSearch;
        public int _x_getdata = 0;
        private DataView dsv;

        public _search_bill()
        {
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this._toolBar.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this._myGrid1._gridType = 2;
                this._myGrid1._table_name = "";
                this._myGrid1._width_by_persent = true;
                this._myGrid1._total_show = true;
                this._myGrid1._isEdit = false;
                this._myGrid1.AllowDrop = true;
                this._myGrid1._mouseClick += new MyLib.MouseClickHandler(_myGrid1__mouseClick);
                _myGrid1.DragEnter += new DragEventHandler(_myGrid1_DragEnter);
                this._myGrid1.Refresh();
                this._retrivenow._iconNumber = 1;
                this._retrivenow.Image = imageList1.Images[this._retrivenow._iconNumber];
                this._mySelectAll._iconNumber = 0;
                this._mySelectAll.Image = imageList1.Images[this._mySelectAll._iconNumber];
            }
        }

        public void _firstFocus()
        {
            _mytextboxsearch.Focus();
        }

        //event Mouse Click เพื่อ แสดงข้อมูลที่ Statusbar
        void _myGrid1__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            if (e._row != -1)
            {
                this._statusStrip.Items.Clear();
                MyLib._myGrid __sender = (MyLib._myGrid)(sender);
                string __showstatus = String.Empty;
                //this._statusStrip.Items.Count
                //for (int __x = 1; __x < this._statusStrip.Items.Count; __x++)
                // {
                for (int _x = 1; _x < __sender._columnList.Count; _x++)
                {
                    MyLib._myGrid._columnType getColumn = (MyLib._myGrid._columnType)__sender._columnList[_x];
                    if (getColumn._type == 3)
                    {
                        string format = MyLib._myGlobal._getFormatNumber(MyLib._myGlobal._getFormatNumber("m02"));
                        string strDisplay = string.Format(format, (decimal)__sender._cellGet(e._row, getColumn._originalName));
                        //  this._statusStrip.Items[_x].Text = getColumn._name + " : " + strDisplay;
                        __showstatus = getColumn._name + " : " + strDisplay;
                    }
                    else if (getColumn._type == 4)
                    {
                        DateTime _xDate = (DateTime)__sender._cellGet(e._row, getColumn._originalName);
                        __showstatus = getColumn._name + " : " + MyLib._myGlobal._convertDateToString(_xDate, true);
                        // this._statusStrip.Items[__x].Text = getColumn._name + " : " + MyLib._myGlobal._convertDateToString(_xDate, true);
                    }
                    else
                    {
                        // this._statusStrip.Items[__x].Text = getColumn._name + " : " + __sender._cellGet(e._row, getColumn._originalName).ToString();
                        __showstatus = getColumn._name + " : " + __sender._cellGet(e._row, getColumn._originalName).ToString();
                    }
                    this._statusStrip.Items.Add(__showstatus);
                }

                //}

            }
        }
        //event Drag ข้อมูลจาก Grid
        void _myGrid1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }
        /// <summary>
        /// เป็น Event กดปุ่ม  Ko     
        /// </summary>        
        public event EventOkSearchHandler _okButton;
        public delegate void EventOkSearchHandler(object sender, string _screentype, string _tabletaget);
        protected virtual void __okButtonWork()
        {
            if (_okButton != null) _okButton(this._gridtem, this._x_screen, this._x_table);

        }
        /// <summary>
        /// เป็น Event Clear CheckBox ใน Grid    
        /// </summary>        
        void _clearcheckbox()
        {
            for (int __loop = 0; __loop < _myGrid1._rowData.Count; __loop++)
            {
                if ((int)_myGrid1._cellGet(__loop, 0) == 1)
                {

                    _myGrid1._cellUpdate(__loop, 0, 0, false);
                }
            }

        }
        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = true;
            this.Visible = false;
            //base.OnClosing(e);
            _clearcheckbox();
            this._statusStrip.Items.Clear();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            const int WM_KEYDOWN = 0x100;
            const int WM_SYSKEYDOWN = 0x104;

            if ((msg.Msg == WM_KEYDOWN) || (msg.Msg == WM_SYSKEYDOWN))
            {
                Keys keyCode = (Keys)(int)keyData & Keys.KeyCode;
                if ((keyData & Keys.Control) == Keys.Control && (keyCode == Keys.F))
                {
                    _mytextboxsearch.Focus();
                    return true;
                }
                else
                    if (keyData == Keys.Escape)
                    {
                        this.Close();
                        return true;
                    }
                    else
                        if (keyData == Keys.F2)
                        {
                            this.Close();
                            return true;
                        }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        private void _search_bill_Load(object sender, EventArgs e)
        {
            string __formatNumber = MyLib._myGlobal._getFormatNumber("m02");
            this._myGrid1._columnList.Clear();
            string __custName = "custname";
            string __departmentName = "departmentname";
            if (_ictransControlTypeEnumTemp.Equals(_g.g._transControlTypeEnum.ซื้อ_เสนอซื้อ.ToString()))
            {
                if (_fieldNameSearch.Equals(_g.d.ic_trans._approve_code))
                {
                    this._myGrid1._table_name = _g.d.erp_user._table;
                    this._myGrid1._addColumn("S", 11, 5, 5);
                    this._myGrid1._addColumn(_g.d.erp_user._code, 1, 50, 15, false, false, false, false);
                    this._myGrid1._addColumn(_g.d.erp_user._name_1, 1, 20, 30, false, false, false, false);
                    this._myGrid1._addColumn(_g.d.erp_user._department, 1, 20, 15, false, false, false, false);
                    this._myGrid1._addColumn(__departmentName, 1, 20, 35, false, false, false, false);
                    this._myGrid1.Invalidate();
                }
                else
                {
                    this._myGrid1._table_name = _g.d.ic_trans._table;
                    this._myGrid1._addColumn("S", 11, 5, 5);
                    this._myGrid1._addColumn(_g.d.ic_trans._doc_no, 1, 50, 25, false, false, false, false);
                    this._myGrid1._addColumn(_g.d.ic_trans._doc_date, 4, 20, 25, false, false, false, false);
                    this._myGrid1._addColumn(_g.d.ic_trans._remark, 1, 20, 45, false, false, false, false);
                    //this._myGrid1._addColumn(_g.d.ic_trans._approve_code, 1, 15, 20, false, false, false, false);
                    this._myGrid1.Invalidate();
                }
            }
            else if (_ictransControlTypeEnumTemp.Equals(_g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ.ToString()))
            {
                this._myGrid1._table_name = _g.d.ic_trans_detail._table;
                this._myGrid1._addColumn("S", 11, 5, 5);
                this._myGrid1._addColumn(_g.d.ic_trans_detail._item_code, 1, 50, 15, false, false, false, false);
                this._myGrid1._addColumn(_g.d.ic_trans_detail._item_name, 1, 20, 30, false, false, false, false);
                this._myGrid1._addColumn(_g.d.ic_trans_detail._doc_no, 1, 20, 15, false, false, false, false);
                this._myGrid1._addColumn(_g.d.ic_trans_detail._unit_code, 1, 15, 15, false, false, false, false);
                this._myGrid1._addColumn(_g.d.ic_trans_detail._approval_qty, 3, 15, 10, false, false, false, false, __formatNumber);
                this._myGrid1._addColumn(_g.d.ic_trans_detail._qty, 3, 10, 10, false, false, false, false, __formatNumber);
                this._myGrid1.Invalidate();
            }
            else if (_ictransControlTypeEnumTemp.Equals(_g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ.ToString()))
            {
                this.Text = "ค้นหารายการใบสั่งซื้อสินค้า";
                this._myGrid1._table_name = _g.d.ic_trans_detail._table;
                this._myGrid1._addColumn("S", 11, 5, 5);
                this._myGrid1._addColumn(_g.d.ic_trans._doc_no, 1, 50, 15, false, false, false, false);
                this._myGrid1._addColumn(_g.d.ic_trans._doc_date, 4, 20, 15, false, false, false, false);
                this._myGrid1._addColumn(_g.d.ic_trans._cust_code, 1, 50, 15, false, false, false, false);
                this._myGrid1._addColumn(__custName, 1, 20, 30, false, false, false, false);
                this._myGrid1._addColumn(_g.d.ic_trans._total_amount, 3, 10, 10, false, false, false, false, __formatNumber);
                this._myGrid1._addColumn(_g.d.ic_trans._status, 1, 50, 10, false, false, false, false);
            }
            else if (_ictransControlTypeEnumTemp.Equals(_g.g._transControlTypeEnum.ซื้อ_พาเชียล_รับสินค้า.ToString()))
            {
                this.Text = "ค้นหารายการใบสั่งซื้อสินค้า";
                this._myGrid1._table_name = _g.d.ic_trans_detail._table;
                this._myGrid1._addColumn("S", 11, 5, 5);
                this._myGrid1._addColumn(_g.d.ic_trans._doc_no, 1, 50, 15, false, false, false, false);
                this._myGrid1._addColumn(_g.d.ic_trans._doc_date, 4, 20, 15, false, false, false, false);
                this._myGrid1._addColumn(_g.d.ic_trans._cust_code, 1, 50, 15, false, false, false, false);
                this._myGrid1._addColumn(__custName, 1, 20, 30, false, false, false, false);
                this._myGrid1._addColumn(_g.d.ic_trans._total_amount, 3, 10, 10, false, false, false, false, __formatNumber);
                this._myGrid1._addColumn(_g.d.ic_trans._status, 1, 50, 10, false, false, false, false);
            }

            ////if (_x_screen == "Ar")
            ////{
            ////    //เอกสารขาย
            ////    /*   this._myGrid1._addColumn("S", 11, 5, 5);
            ////       this._myGrid1._addColumn("เอกสารขาย", 1, 100, 10, false, false, false, false);
            ////       this._myGrid1._addColumn("วันที่เอกสาร", 1, 100, 10, false, false, false, false);
            ////       this._myGrid1._addColumn("รหัสเครดิต", 1, 100, 10, false, false, false, false);
            ////       this._myGrid1._addColumn("วันครบกำหนด", 1, 100, 10, false, false, false, false);
            ////       this._myGrid1._addColumn("พนักงานขาย", 1, 100, 10, false, false, false, false);
            ////       this._myGrid1._addColumn("รายวัน", 1, 100, 5, false, false, false, false);
            ////       this._myGrid1._addColumn("แผนก", 1, 100, 5, false, false, false, false);
            ////       this._myGrid1._addColumn("รวมทั้งสิ้น", 1, 100, 10, false, false, false, false);
            ////       this._myGrid1._addColumn("ค้างชำระ", 1, 100, 5, false, false, false, false);
            ////       this._myGrid1._addColumn("หมายเหตุ", 1, 100, 10, false, false, false, false);
            ////       this._myGrid1._addColumn("สกุลเงิน", 1, 100, 10, false, false, false, false);
            ////     */
            ////  //  this._myGrid1._clear();
            ////    string formatNumber = MyLib._myGlobal._getFormatNumber("m02");
            ////    this._myGrid1._table_name = this._x_table;
            ////    this._myGrid1._addColumn("Check", 11, 10, 5);

            ////    this._myGrid1._addColumn(_g.d.po_so_inquiry._doc_no, 1, 50, 10, false, false, false, false);
            ////    this._myGrid1._addColumn(_g.d.po_so_inquiry._doc_date, 4, 20, 15, false, false, false, false);
            ////    this._myGrid1._addColumn(_g.d.po_so_inquiry._due_date, 4, 8, 10, false, false, false, false, "DD/MM/YYYY");
            ////    this._myGrid1._addColumn(_g.d.po_so_inquiry._credit_day, 2, 15, 10, false, false, false, false);
            ////    this._myGrid1._addColumn(_g.d.po_so_inquiry._inquiry_type, 1, 15, 10, false, false, false, false);
            ////    this._myGrid1._addColumn(_g.d.po_so_inquiry._sale_code, 1, 15, 15, false, false, false, false);
            ////    this._myGrid1._addColumn(_g.d.po_so_inquiry._total_net_value, 3, 15, 15, false, false, false, false, formatNumber);
            ////    this._myGrid1._addColumn(_g.d.po_so_inquiry._remark, 1, 10, 10, false, false, false, false);

            ////    //gridtem._table_name = _x_table;
            ////    // _x_query = "query_po_so_inquiry";
            ////}
            ////else if (_x_screen == "Ap")
            ////{
            ////    //เอกสารซื้อ
            ////    /*  this._myGrid1._addColumn("S", 11, 5, 5);
            ////      this._myGrid1._addColumn("เอกสารซื้อ", 1, 100, 10, false, false, false, false);
            ////      this._myGrid1._addColumn("ใบกำกับ", 1, 100, 10, false, false, false, false);
            ////      this._myGrid1._addColumn("วันที่เอกสาร", 1, 100, 10, false, false, false, false);
            ////      this._myGrid1._addColumn("รหัสเครดิต", 1, 100, 10, false, false, false, false);
            ////      this._myGrid1._addColumn("ครบกำหนด", 1, 100, 10, false, false, false, false);
            ////      this._myGrid1._addColumn("รายวัน", 1, 100, 5, false, false, false, false);
            ////      this._myGrid1._addColumn("แผนก", 1, 100, 5, false, false, false, false);
            ////      this._myGrid1._addColumn("รวมทั้งสิ้น", 1, 100, 10, false, false, false, false);
            ////      this._myGrid1._addColumn("ค้างชำระ", 1, 100, 5, false, false, false, false);
            ////      this._myGrid1._addColumn("หมายเหตุ", 1, 100, 10, false, false, false, false);
            ////      this._myGrid1._addColumn("สกุลเงิน", 1, 100, 10, false, false, false, false);
            ////     */
            ////    string formatNumber = MyLib._myGlobal._getFormatNumber("m02");
            ////    this._myGrid1._table_name = this._x_table;
            ////    this._myGrid1._addColumn("Check", 11, 10, 5);
            ////    this._myGrid1._addColumn(_g.d.po_so_inquiry._doc_no, 1, 50, 10, false, false, false, false);
            ////    this._myGrid1._addColumn(_g.d.po_so_inquiry._doc_date, 4, 20, 15, false, false, false, false);
            ////    this._myGrid1._addColumn(_g.d.po_so_inquiry._due_date, 4, 8, 10, false, false, false, false, "DD/MM/YYYY");
            ////    this._myGrid1._addColumn(_g.d.po_so_inquiry._credit_day, 2, 15, 10, false, false, false, false);
            ////    this._myGrid1._addColumn(_g.d.po_so_inquiry._inquiry_type, 1, 15, 10, false, false, false, false);
            ////    this._myGrid1._addColumn(_g.d.po_so_inquiry._sale_code, 1, 15, 15, false, false, false, false);
            ////    this._myGrid1._addColumn(_g.d.po_so_inquiry._total_net_value, 3, 15, 15, false, false, false, false, formatNumber);
            ////    this._myGrid1._addColumn(_g.d.po_so_inquiry._remark, 1, 10, 10, false, false, false, false);
            ////}
            ////else if (_x_screen == "CreditCard")
            ////{
            ////    string formatNumber = MyLib._myGlobal._getFormatNumber("m02");
            ////    //บัตรเครดิต
            ////    this._myGrid1._table_name = _g.d.cb_credit_card._table;
            ////    this._myGrid1._addColumn("Check", 11, 5, 10);
            ////    this._myGrid1._addColumn(_g.d.cb_credit_card._credit_card_no, 1, 20, 10, false, false, false, false);
            ////    this._myGrid1._addColumn(_g.d.cb_credit_card._bank_code, 1, 50, 10, false, false, false, false);
            ////    this._myGrid1._addColumn(_g.d.cb_credit_card._credit_due_date, 4, 8, 10, false, false, false, false, "DD/MM/YYYY");
            ////    this._myGrid1._addColumn(_g.d.cb_credit_card._doc_no, 1, 15, 15, false, false, false, false);
            ////    this._myGrid1._addColumn(_g.d.cb_credit_card._owner_name, 1, 15, 15, false, false, false, false);
            ////    this._myGrid1._addColumn(_g.d.cb_credit_card._amount, 3, 15, 15, false, false, false, false, formatNumber);

            //// //   _x_query = "query_credit_card";

            ////}
            ////else
            ////{
            ////    this._myGrid1._addColumn("Check", 11, 5, 10);
            ////    this._myGrid1._addColumn(_g.d.gl_chart_of_account._code, 1, 100, 20, false, false, false, false);
            ////    this._myGrid1._addColumn(_g.d.gl_chart_of_account._name_1, 1, 100, 35, false, false, false, false);
            ////    this._myGrid1._addColumn(_g.d.gl_chart_of_account._name_2, 1, 100, 35, false, false, false, false);
            ////}

            _refreash();
            // Event

        }
        /// <summary>
        /// เป็น Method refresh query ใน Grid    
        /// </summary> 
        public void _refreash()
        {
            string __query = "";
            if (_x_query == "")
            {
                __query = "select  " + _g.d.gl_chart_of_account._code + "," + _g.d.gl_chart_of_account._name_1 + "," + _g.d.gl_chart_of_account._name_2 + "," + _g.d.gl_chart_of_account._account_level + "," + _g.d.gl_chart_of_account._account_type + " from " + _g.d.gl_chart_of_account._table + " order by " + _g.d.gl_chart_of_account._code;
            }

            else
            {
                __query = this._x_query;
            }
            _myGrid1._clear();
            DataSet __getData = __frameWork._query(MyLib._myGlobal._databaseName, __query);
            if (__getData.Tables[0].Rows.Count > 0)
            {
                //this.Text = this.Text + __getData.Tables[0].Rows.Count.ToString();
                dsv = __getData.Tables[0].DefaultView;
                _myGrid1._loadFromDataTable(dsv.Table);
                _x_getdata = __getData.Tables[0].Rows.Count;
            }
            else
            {
                _x_getdata = 0;
            }
            _myGrid1.Invalidate();
            this._statusStrip.Items.Clear();
            this._mytextboxsearch.Text = "";
            this._mytextboxsearch.Focus();

        }
        /// <summary>
        /// เป็น Method ค้นหาข้อมูลใน Dataview    
        /// </summary> 

        private void ApplyFilter()
        {
            string filter = SMLERPControl._RowFilterBuilder.BuildMultiColumnFilter(this._mytextboxsearch.Text, dsv);
            dsv.RowFilter = filter;
            _myGrid1._loadFromDataTable(dsv.ToTable());
            _myGrid1.Invalidate();

        }
        private void _myButton_close_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        private void _myButton_ok_Click(object sender, EventArgs e)
        {
            _gridtem = new MyLib._myGrid();
            _gridtem = this._myGrid1;
            __okButtonWork();
            this.Visible = false;
        }

        private void toolStripTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ApplyFilter();
            }
        }

        private void _autoRunningNumberButton_Click(object sender, EventArgs e)
        {
            this._retrivenow._iconNumber = (this._retrivenow._iconNumber == 0) ? 1 : 0;
            this._retrivenow.Image = imageList1.Images[this._retrivenow._iconNumber];
        }

        private void toolStripTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (this._retrivenow._iconNumber == 0)
            {
                ApplyFilter();
            }
        }

        private void _mySelectAll_Click(object sender, EventArgs e)
        {
            this._mySelectAll._iconNumber = (this._mySelectAll._iconNumber == 0) ? 1 : 0;
            this._mySelectAll.Image = imageList1.Images[this._mySelectAll._iconNumber];
            if (this._mySelectAll._iconNumber == 0)//disselect all 
            {
                for (int __loop = 0; __loop < _myGrid1._rowData.Count; __loop++)
                {
                    _myGrid1._cellUpdate(__loop, 0, 0, true);
                }

                _mySelectAll.Text = "เลือกทั้งหมด";
            }
            else
            {//select all 
                for (int __loop = 0; __loop < _myGrid1._rowData.Count; __loop++)
                {
                    _myGrid1._cellUpdate(__loop, 0, 1, true);

                }
                _mySelectAll.Text = "ไม่เลือกทั้งหมด";
            }
            _myGrid1.Invalidate();
        }


    }
}