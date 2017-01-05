using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLERPControl
{
    public partial class _datalistdeposit : UserControl
    {
        public bool _isChecked = false;
        public bool _ischk = true;
        public string __whereData = "";
        private int _oldrowdata = 0;
        private string oldText = "";
        private string transection_flag = "";
        public bool _loadViewDataSuccess = false;
        public string _orderBy = "";
        private bool _emtry = true;
        public string _extraWhere = "";
        public string _tableName = "";
        public bool _lockRecord = false;
        public _datalistdeposit()
        {
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this._toolBar.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }

            _dataList._isEdit = false;
            _dataList.AllowDrop = true;
            _dataList._processKeyEnable = false;
            _mytextboxsearch.TextChanged += new EventHandler(_mytextboxsearch_TextChanged);
            _mytextboxsearch.LostFocus += new EventHandler(_mytextboxsearch_LostFocus);
            _mytextboxsearch.GotFocus += new EventHandler(_mytextboxsearch_GotFocus);
            _dataList.Refresh();
            this._getDataEvent += new GetDataEventHandler(test_datalist__getDataEvent);
            _dataList._mouseClick += new MyLib.MouseClickHandler(_myGrid__mouseClick);
            this.timer1.Tick += new EventHandler(timer1_Tick);
            this._closeScreen += new CloseScreenEvent(test_datalist__closeScreen);
            this._retrivenow._iconNumber = 1;
            this._retrivenow.Image = imageList1.Images[this._retrivenow._iconNumber];
            this._mySelectAll._iconNumber = 1;
            this._mySelectAll.Image = imageList1.Images[this._mySelectAll._iconNumber];
        }

        void test_datalist__closeScreen()
        {

            if (_closeScreen != null)
            {
                _closeScreen();
                this.Visible = false;
                ParentForm.Close();
            }

        }

        void test_datalist__getDataEvent(int row)
        {

            if (row != -1 && row < _dataList._rowData.Count)
            {
                this._statusStrip.Items.Clear();
                string __showstatus = String.Empty;

                for (int _x = 1; _x < this._dataList._columnList.Count; _x++)
                {
                    MyLib._myGrid._columnType getColumn = (MyLib._myGrid._columnType)this._dataList._columnList[_x];
                    if (getColumn._type == 3)
                    {
                        string format = MyLib._myGlobal._getFormatNumber(MyLib._myGlobal._getFormatNumber("m02"));
                        string strDisplay = string.Format(format, (double)this._dataList._cellGet(row, getColumn._originalName));
                        __showstatus = getColumn._name + " : " + strDisplay;
                    }
                    else if (getColumn._type == 4)
                    {
                        DateTime _xDate = (DateTime)this._dataList._cellGet(row, getColumn._originalName);
                        __showstatus = getColumn._name + " : " + MyLib._myGlobal._convertDateToString(_xDate, true);
                    }
                    else
                    {
                        __showstatus = getColumn._name + " : " + this._dataList._cellGet(row, getColumn._originalName).ToString();
                    }
                    this._statusStrip.Items.Add(__showstatus);
                }

            }
        }
        //private DataTable setnewDatarow(DataTable dt)
        //{
        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //        string __status = dt.Rows[i]["status"].ToString();
        //        switch (__status)
        //        {
        //            case "0": dt.Rows[i]["status"] = "ยกเลิก";
        //                break;
        //            case "1": dt.Rows[i]["status"] = "ปกติ";
        //                break;

        //        }

        //    }
        //    return dt;
        //}
        public void _loaData()
        {
            SMLERPGlobal._mySMLFrameWork __frameWork = new SMLERPGlobal._mySMLFrameWork();

            DataSet __getData = __frameWork._dePositGetquery(MyLib._myGlobal._databaseName, this._extraWhere);
            DataTable __dt = __getData.Tables[0];
            _dataList._loadFromDataTable(setdeposit_typeDatarow(setnewDatarow(__dt)));
            _oldrowdata = _dataList._rowData.Count;
            _infoLabel.Text = string.Format("({0:#,0}/{1:#,0})", _dataList._rowData.Count, _oldrowdata);
            _dataList.Invalidate();
            // load Data

        }
        void _myGrid__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            if (e._row == -1 && e._column != -1 && e._column != 0)
            {
                MyLib._myGrid._columnType getColumnType = (MyLib._myGrid._columnType)_dataList._columnList[e._column];
                string newOrder = "\'" + getColumnType._originalName + "\'";
                if (newOrder.CompareTo(this._orderBy) == 0)
                {
                    this._orderBy += " desc";
                }
                else
                {
                    this._orderBy = "\'" + getColumnType._originalName + "\'";
                }
                _loadViewData();
            }

            if (e._row != -1)
            {
                this._statusStrip.Items.Clear();
                MyLib._myGrid __sender = (MyLib._myGrid)(sender);
                string __showstatus = String.Empty;

                for (int _x = 1; _x < __sender._columnList.Count; _x++)
                {
                    MyLib._myGrid._columnType getColumn = (MyLib._myGrid._columnType)__sender._columnList[_x];
                    if (getColumn._type == 3)
                    {
                        string format = MyLib._myGlobal._getFormatNumber(MyLib._myGlobal._getFormatNumber("m02"));
                        string strDisplay = string.Format(format, (double)__sender._cellGet(e._row, getColumn._originalName));
                        __showstatus = getColumn._name + " : " + strDisplay;
                    }
                    else if (getColumn._type == 4)
                    {
                        DateTime _xDate = (DateTime)__sender._cellGet(e._row, getColumn._originalName);
                        __showstatus = getColumn._name + " : " + MyLib._myGlobal._convertDateToString(_xDate, true);
                    }
                    else
                    {
                        __showstatus = getColumn._name + " : " + __sender._cellGet(e._row, getColumn._originalName).ToString();
                    }
                    this._statusStrip.Items.Add(__showstatus);
                }

            }
        }
        public void _loadViewData()
        {
            // this._extraWhere = extraWhere;
            this._loadViewDataSuccess = true;
            StringBuilder query = new StringBuilder();
            query.Append("select ");
            StringBuilder where = new StringBuilder();

            if (_mytextboxsearch.TextBox.Text.Length > 0)
            {
                while (_mytextboxsearch.TextBox.Text[_mytextboxsearch.TextBox.Text.Length - 1] == ' ')
                {
                    _mytextboxsearch.TextBox.Text = _mytextboxsearch.TextBox.Text.Remove(_mytextboxsearch.TextBox.Text.Length - 1, 1);
                    if (_mytextboxsearch.TextBox.Text.Length == 0)
                    {
                        break;
                    }
                }
            }
            string[] searchTextSplit = _mytextboxsearch.TextBox.Text.Split(' ');
            bool first = false;
            this.Cursor = Cursors.WaitCursor;
            _infoLabel.Text = "";
            for (int loop = 1; loop < _dataList._columnList.Count; loop++)
            {
                MyLib._myGrid._columnType getColumnType = (MyLib._myGrid._columnType)_dataList._columnList[loop];

                if (getColumnType._originalName.Length > 0)
                {
                    if (first)
                    {
                        query.Append(",");
                        if (getColumnType._isHide == false && _mytextboxsearch.TextBox.Text.Length > 0)
                        {
                            where.Append(" or ");
                        }
                    }
                    else
                    {
                        if (this._lockRecord)
                        {
                            query.Append("guid_code,");
                        }
                    }
                    if (_orderBy.Length == 0) _orderBy = "\'" + getColumnType._originalName + "\'";
                    first = true;
                    query.Append(getColumnType._query + " as \'" + getColumnType._originalName + "\'");
                    if (searchTextSplit.Length > 1 && getColumnType._isHide == false)
                    {
                        where.Append("(");
                        bool first2 = false;
                        for (int search = 0; search < searchTextSplit.Length; search++)
                        {
                            if (searchTextSplit[search].Length > 0)
                            {
                                if (first2) where.Append(" and ");
                                first2 = true;
                                where.Append(getColumnType._query + " like \'" + ((search != 0) ? "%" : "") + searchTextSplit[search] + "%\'");
                            }
                        }
                        where.Append(")");
                    }
                    else
                    {
                        if (getColumnType._isHide == false && _mytextboxsearch.TextBox.Text.Length > 0)
                        {
                            if (_mytextboxsearch.TextBox.Text[0] == '+')
                            {
                                where.Append(getColumnType._query + " like \'" + _mytextboxsearch.TextBox.Text.Remove(0, 1) + "%\'");
                            }
                            else
                            {
                                where.Append(getColumnType._query + " like \'%" + _mytextboxsearch.TextBox.Text + "%\'");
                            }
                        }
                    }
                }
            } // for

            if (first)
            {
                query.Append(" from " + _tableName);
                if (where.Length > 0)
                {
                    query.Append(" where (" + where + ")");
                    if (this._extraWhere.Length > 0)
                    {
                        query.Append(" and " + this._extraWhere);
                    }
                }
                else
                {
                    if (this._extraWhere.Length > 0)
                    {
                        query.Append(" where " + this._extraWhere);
                    }
                }

                query.Append(" order by " + _orderBy);
                SMLERPGlobal._mySMLFrameWork myFrameWork = new SMLERPGlobal._mySMLFrameWork();
                MyLib._queryReturn dataResult = new MyLib._queryReturn();
                dataResult.detail = myFrameWork._dePositGetquerySearch(MyLib._myGlobal._databaseName, query.ToString());
                _dataList._loadFromDataTable(setdeposit_typeDatarow(setnewDatarow(dataResult.detail.Tables[0])));
                _infoLabel.Text = string.Format("({0:#,0}/{1:#,0})", _dataList._rowData.Count, _oldrowdata);

            }
            this.Cursor = Cursors.Default;
            this._dataList.Invalidate();
        }
        private DataTable setnewDatarow(DataTable dt)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                /* ----------
                string __status = dt.Rows[i][_g.d.po_so_deposit_money._status].ToString();
                switch (__status)
                {
                    case "0": dt.Rows[i][_g.d.po_so_deposit_money._status] = "ยกเลิก";
                        break;
                    case "1": dt.Rows[i][_g.d.po_so_deposit_money._status] = "ปกติ";
                        break;
                    case "2": dt.Rows[i][_g.d.po_so_deposit_money._status] = "คืนบางส่วน";
                        break;
                    case "3": dt.Rows[i][_g.d.po_so_deposit_money._status] = "คืนทั้งหมด";
                        break;
                }
                */
            }
            return dt;
        }
        private DataTable setdeposit_typeDatarow(DataTable dt)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                /* ----------
                string __deposit_type = dt.Rows[i][_g.d.po_so_deposit_money._deposit_type].ToString();
                switch (__deposit_type)
                {
                    case "0": dt.Rows[i][_g.d.po_so_deposit_money._deposit_type] = "เงินล่วงหน้า";
                        break;
                    case "1": dt.Rows[i][_g.d.po_so_deposit_money._deposit_type] = "เงินมัดจำ";
                        break;

                }
                */
            }
            return dt;
        }



        void _mytextboxsearch_GotFocus(object sender, EventArgs e)
        {
            this._mytextboxsearch.BackColor = Color.LightGoldenrodYellow;
        }
        void textBox_GotFocus(object sender, EventArgs e)
        {
            this._mytextboxsearch.BackColor = Color.LightGoldenrodYellow;
        }
        void _mytextboxsearch_TextChanged(object sender, EventArgs e)
        {
            this._dataList._selectRow = -1;
        }

        public Color _defaultBackGround { get { return _defaultBackGroundResult; } set { _defaultBackGroundResult = value; } }
        private Color _defaultBackGroundResult = Color.White;
        void _mytextboxsearch_LostFocus(object sender, EventArgs e)
        {
            this._mytextboxsearch.BackColor = (_emtry) ? _defaultBackGround : Color.OldLace;
        }
        void timer1_Tick(object sender, EventArgs e)
        {
            if (this._retrivenow._iconNumber == 0)
            {
                if (oldText.CompareTo(this._mytextboxsearch.TextBox.Text) != 0)
                {
                    oldText = this._mytextboxsearch.TextBox.Text;
                    _loadViewData();
                }
            }
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            const int WM_KEYDOWN = 0x100;
            const int WM_SYSKEYDOWN = 0x104;

            if ((msg.Msg == WM_KEYDOWN) || (msg.Msg == WM_SYSKEYDOWN))
            {
                timer1.Stop();
                timer1.Start();
                switch (keyData)
                {
                    case Keys.Enter:
                        _loadViewData();
                        _dataList.Invalidate();
                        return true;
                    case Keys.Down:
                        _nextRecord();
                        _dataList.Invalidate();
                        return true;
                    case Keys.Up:
                        _prevRecord();
                        _dataList.Invalidate();
                        return true;
                    case Keys.Home:
                        _dataList._selectRow = 0;
                        if (_getDataEvent != null)
                        {
                            _getDataEvent(_dataList._selectRow);
                        }
                        _dataList.Invalidate();
                        return true;
                    case Keys.End:
                        if (_dataList._rowData.Count > 1)
                        {

                            _dataList._selectRow = _dataList._rowData.Count - 1;
                            if (_getDataEvent != null)
                            {
                                _getDataEvent(_dataList._selectRow);
                            }
                        }
                        _dataList.Invalidate();
                        return true;
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);

        }

        public event EventOkSearchHandler _okButton;
        public delegate void EventOkSearchHandler(object sender);
        protected virtual void __okButtonWork()
        {
            if (_okButton != null)
            {
                _okButton(this.__whereData);
                if (this.Visible)
                {
                }
                else
                {

                    this.Visible = true;
                }
                ParentForm.Close();
                // this.Visible = false;
            }
            _clearcheckbox();
        }
        void _clearcheckbox()
        {

            for (int __loop = 0; __loop < _dataList._rowData.Count; __loop++)
            {
                if ((int)_dataList._cellGet(__loop, 0) == 1)
                {

                    _dataList._cellUpdate(__loop, 0, 0, false);
                }
            }
        }
        public event GetDataEventHandler _getDataEvent;
        public delegate void GetDataEventHandler(int row);
        /// รายการต่อไป
        /// </summary>
        void _nextRecord()
        {
            if (_dataList._selectRow < _dataList._rowData.Count - 1)
            {
                _dataList._selectRow++;
                if (_getDataEvent != null)
                {
                    _getDataEvent(_dataList._selectRow);
                }
            }
            else
            {
                //  _nextPage();
                _dataList._selectRow = 0;
            }
        }

        /// <summary>
        /// ข้อมูลรายการที่แล้ว
        /// </summary>
        void _prevRecord()
        {
            if (_dataList._selectRow > 0)
            {
                _dataList._selectRow--;
                if (_getDataEvent != null)
                {
                    _getDataEvent(_dataList._selectRow);
                }
            }
            else
            {
                //_prevPage();
                _dataList._selectRow = _dataList._rowData.Count - 1;
            }
        }

        private void _mySelectAll_Click(object sender, EventArgs e)
        {
            this._mySelectAll._iconNumber = (this._mySelectAll._iconNumber == 0) ? 1 : 0;
            this._mySelectAll.Image = imageList1.Images[this._mySelectAll._iconNumber];
            if (this._mySelectAll._iconNumber == 1)//disselect all 
            {
                for (int __loop = 0; __loop < _dataList._rowData.Count; __loop++)
                {
                    _dataList._cellUpdate(__loop, 0, 0, true);
                }

                _mySelectAll.Text = "เลือกทั้งหมด";
            }
            else
            {//select all 
                for (int __loop = 0; __loop < _dataList._rowData.Count; __loop++)
                {
                    _dataList._cellUpdate(__loop, 0, 1, true);

                }
                _mySelectAll.Text = "ไม่เลือกทั้งหมด";
            }
            _dataList.Invalidate();
        }

        public string _myWhere()
        {
            __whereData = "";
            for (int __loop = 0; __loop < _dataList._rowData.Count; __loop++)
            {
                if ((int)_dataList._cellGet(__loop, 0) == 1)
                {
                    if (__whereData != "") __whereData += " or ";
                    __whereData += "  doc_no  =  \'" + _dataList._cellGet(__loop, 1).ToString() + "\'";
                }
            }
            return __whereData;
        }
        private void _retrivenow_Click(object sender, EventArgs e)
        {
            this._retrivenow._iconNumber = (this._retrivenow._iconNumber == 0) ? 1 : 0;
            this._retrivenow.Image = imageList1.Images[this._retrivenow._iconNumber];
        }

        private void _myButton_close_Click(object sender, EventArgs e)
        {

            // this.Visible = false;
            if (this.Visible) { }
            else
            {

                this.Visible = true;
            }
            ParentForm.Close();

        }

        private void _myButton_ok_Click(object sender, EventArgs e)
        {
            string xx = _myWhere();
            __okButtonWork();
        }
        public delegate void CloseScreenEvent();
        public event CloseScreenEvent _closeScreen;

    }
}
