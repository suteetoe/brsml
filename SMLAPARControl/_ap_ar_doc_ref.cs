using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMLERPAPARControl
{
    public partial class _ap_ar_doc_ref : Form
    {
        MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();

        private string __result;

        public string _result
        {
            get { return __result; }
            set { __result = value; }
        }

        private string __gerwhere;

        public string _gerwhere
        {
            get { return __gerwhere; }
            set { __gerwhere = value; }
        }

        private int __is_ap_ar;

        public int _is_ap_ar
        {
            get { return __is_ap_ar; }
            set { __is_ap_ar = value; }
        }

        public _ap_ar_doc_ref()
        {
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this._myToolbar.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }
            this._ap_ar_doc_ref_grid1._clear();
            this.Load += new EventHandler(_ap_ar_doc_ref_Load);
            // this._ap_ar_doc_ref_grid1._mouseClick += new MyLib.MouseClickHandler(_ap_ar_doc_ref_grid1__mouseClick);
            //this._ap_ar_doc_ref_grid1.Click += new EventHandler(_ap_ar_doc_ref_grid1_Click);
            this._mySelectAll._iconNumber = 0;
            this._mySelectAll.Image = imageList1.Images[this._mySelectAll._iconNumber];
            this.Invalidate();
        }

        void _ap_ar_doc_ref_grid1__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            int xcxx = this._ap_ar_doc_ref_grid1._selectRow;
            if (e._row != -1)
            {
                bool ischk = true;
                bool ccc = (this._ap_ar_doc_ref_grid1._cellGet(e._row, 0).Equals("0")) ? true : false;
                //if (this._cellGet(e._row, 0).ToString() == true) ischk = false;
                this._ap_ar_doc_ref_grid1._cellUpdate(e._column, 0, ccc, true);
            }
        }

        void _ap_ar_doc_ref_Load(object sender, EventArgs e)
        {
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            this._mytextboxsearch.Focus();
            string _db_type = "||";
            string _ar_ap_name = "(select name_1 from ap_supplier where code = ap_ar_trans.cust_code)";
            if (__myFrameWork._databaseSelectType.Equals(MyLib._myGlobal._databaseType.MicrosoftSQL2005) ||
                        __myFrameWork._databaseSelectType.Equals(MyLib._myGlobal._databaseType.MicrosoftSQL2000))
            {
                _db_type = "+";
            }
            if (this.__is_ap_ar == 2)
            {
                _ar_ap_name = "(select name_1 from ar_customer where code = ap_ar_trans.cust_code)";
            }
            string __query = "select  " + _g.d.ap_ar_trans._doc_no + ","
                + _g.d.ap_ar_trans._doc_date + ","
                + (_g.d.ap_ar_trans._cust_code + _db_type + "'/'" + _db_type + _ar_ap_name) + "as cust_code ,"
                + _g.d.ap_ar_trans._total_debt_balance
                + " from " + _g.d.ap_ar_trans._table
                + " where " + this.__gerwhere
                + " order by " + _g.d.ap_ar_trans._doc_no;
            DataSet __getData = _myFrameWork._query(MyLib._myGlobal._databaseName, __query);
            if (__getData.Tables[0].Rows.Count > 0)
            {
                this._ap_ar_doc_ref_grid1._loadFromDataTable(__getData.Tables[0]);
                this._ap_ar_doc_ref_grid1.Invalidate();
            }
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

        private void _mytextboxsearch_TextChanged(object sender, EventArgs e)
        {
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            string _db_type = "||";
            string _ar_ap_name = "(select name_1 from ap_supplier where code = ap_ar_trans.cust_code)";
            if (__myFrameWork._databaseSelectType.Equals(MyLib._myGlobal._databaseType.MicrosoftSQL2005) ||
                        __myFrameWork._databaseSelectType.Equals(MyLib._myGlobal._databaseType.MicrosoftSQL2000))
            {
                _db_type = "+";
            }
            if (this.__is_ap_ar == 2)
            {
            }
            string __query = "select  " + _g.d.ap_ar_trans._doc_no + ","
                + _g.d.ap_ar_trans._doc_date + ","
                + (_g.d.ap_ar_trans._cust_code + _db_type + "'/'" + _db_type + _ar_ap_name) + "as cust_code ,"
                + _g.d.ap_ar_trans._total_debt_balance
                + " from " + _g.d.ap_ar_trans._table
                + " where " + this.__gerwhere + " and "
                + "(" + _g.d.ap_ar_trans._doc_no + _db_type + _g.d.ap_ar_trans._doc_date + _db_type + _g.d.ap_ar_trans._cust_code + _db_type + _g.d.ap_ar_trans._total_debt_balance + " like '%" + _mytextboxsearch.Text.Trim() + "%') "
                + " order by " + _g.d.ap_ar_trans._doc_no;
            DataSet __getData = _myFrameWork._query(MyLib._myGlobal._databaseName, __query);
            if (__getData.Tables[0].Rows.Count > 0)
            {
                this._ap_ar_doc_ref_grid1._loadFromDataTable(__getData.Tables[0]);
            }
        }

        private void _myButton_ok_Click(object sender, EventArgs e)
        {
            string _comar = "";
            if (this._ap_ar_doc_ref_grid1._rowData.Count > 0)
            {
                this.__result = "";
                for (int i = 0; i < this._ap_ar_doc_ref_grid1._rowData.Count; i++)
                {
                    string cc = this._ap_ar_doc_ref_grid1._cellGet(i, 0).ToString();
                    if (this._ap_ar_doc_ref_grid1._cellGet(i, 0).Equals(1))
                    {
                        this.__result += _comar + this._ap_ar_doc_ref_grid1._cellGet(i, _g.d.ap_ar_trans._doc_no);
                        _comar = ",";
                    }
                }
            }
            this.Visible = false;
        }

        private void _mySelectAll_Click(object sender, EventArgs e)
        {
            this._mySelectAll._iconNumber = (this._mySelectAll._iconNumber == 0) ? 1 : 0;
            this._mySelectAll.Image = imageList1.Images[this._mySelectAll._iconNumber];
            if (this._mySelectAll._iconNumber == 0)
            {
                for (int __loop = 0; __loop < this._ap_ar_doc_ref_grid1._rowData.Count; __loop++)
                {
                    this._ap_ar_doc_ref_grid1._cellUpdate(__loop, 0, 0, true);
                }

                _mySelectAll.Text = "เลือกทั้งหมด";
            }
            else
            {
                for (int __loop = 0; __loop < this._ap_ar_doc_ref_grid1._rowData.Count; __loop++)
                {
                    this._ap_ar_doc_ref_grid1._cellUpdate(__loop, 0, 1, true);

                }
                _mySelectAll.Text = "ไม่เลือกทั้งหมด";
            }
            this._ap_ar_doc_ref_grid1.Invalidate();
        }

        private void _myButton_close_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }

    }

    public class _ap_ar_doc_ref_grid : MyLib._myGrid
    {
        public _ap_ar_doc_ref_grid()
        {
            this.SuspendLayout();
            this._columnList.Clear();
            this._table_name = _g.d.ap_ar_trans_detail._table;
            string __formatNumber = MyLib._myGlobal._getFormatNumber("m02");
            this._addColumn("S", 11, 5, 5);
            this._addColumn(_g.d.ap_ar_trans._doc_date, 4, 1, 25, true, false, true);
            this._addColumn(_g.d.ap_ar_trans._doc_no, 1, 1, 20, true, false, true, true);
            this._addColumn(_g.d.ap_ar_trans._cust_code, 1, 1, 30, true, false, true);
            this._addColumn(_g.d.ap_ar_trans._total_debt_balance, 3, 1, 20, true, false, true, false, __formatNumber);
            this._mouseClick += new MyLib.MouseClickHandler(_ap_ar_doc_ref_grid__mouseClick);


            // _myGrid1.DragEnter += new DragEventHandler(_myGrid1_DragEnter);

            this._width_by_persent = true;
            this.AllowDrop = true;
            this._isEdit = false;

            this.ColumnBackgroundAuto = false;
            this.ColumnBackgroundEnd = Color.FromArgb(198, 220, 249);
            this.RowOddBackground = Color.AliceBlue;
            this.AutoSize = true;
            this.Dock = DockStyle.Fill;
            this.ResumeLayout();
        }

        void _ap_ar_doc_ref_grid_MouseClick(object sender, MouseEventArgs e)
        {
            throw new NotImplementedException();
        }

        void _ap_ar_doc_ref_grid__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            int zzz = this._selectRow;
            if (e._row != -1)
            {
                bool ischk = true;
                string xs = this._cellGet(e._row, 0).ToString();
                int ccc = (this._cellGet(e._row, 0).Equals(0)) ? 1 : 0;
                //if (this._cellGet(e._row, 0).ToString() == true) ischk = false;
                this._cellUpdate(e._row, 0, ccc, true);
            }
        }
    }
}