using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace SMLERPAudit
{
    public partial class _masterLog : UserControl
    {
        public _masterLog()
        {
            InitializeComponent();

            if (MyLib._myGlobal._isDesignMode == false)
            {
                this.toolStrip1.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }
            //
            string __formatNumberAmount = _g.g._getFormatNumberStr(3);
            //
            //this._dataGrid._columnTopActive = true;
            this._dataGrid._table_name = _g.d.master_logs._table;
            this._dataGrid._addColumn(_g.d.master_logs._user_code, 1, 1, 10);
            this._dataGrid._addColumn(_g.d.master_logs._function_code, 1, 1, 10);
            this._dataGrid._addColumn(_g.d.master_logs._computer_name, 1, 1, 10);
            this._dataGrid._addColumn(_g.d.master_logs._date_time, 1, 1, 10);
            this._dataGrid._addColumn(_g.d.master_logs._menu_name, 1, 1, 20);
            this._dataGrid._addColumn(_g.d.master_logs._code, 1, 1, 10);
            this._dataGrid._addColumn(_g.d.master_logs._name_1, 1, 1, 10);

            this._dataGrid._addColumn(_g.d.master_logs._data1, 12, 1, 10, true, false, false);
            this._dataGrid._addColumn(_g.d.master_logs._data2, 12, 1, 10, true, false, false);
            this._dataGrid._addColumn("roworder", 1, 1, 0, false, true);

            //this._dataGrid._addColumn(_g.d.logs._doc_amount, 3, 1, 10, true, false, true, false, __formatNumberAmount);
            //this._dataGrid._addColumn(_g.d.logs._doc_date_old, 4, 1, 10);
            //this._dataGrid._addColumn(_g.d.logs._doc_no_old, 1, 1, 10);
            //this._dataGrid._addColumn(_g.d.logs._doc_amount_old, 3, 1, 10, true, false, true, false, __formatNumberAmount);
            //
            //this._dataGrid._addColumnTop(_g.d.logs._data_new, this._dataGrid._findColumnByName(_g.d.logs._doc_date), this._dataGrid._findColumnByName(_g.d.logs._doc_amount));
            // this._dataGrid._addColumnTop(_g.d.logs._data_old, this._dataGrid._findColumnByName(_g.d.logs._doc_date_old), this._dataGrid._findColumnByName(_g.d.logs._doc_amount_old));
            //
            this._dataGrid._calcPersentWidthToScatter();
            //this._dataGrid._beforeDisplayRendering += new MyLib.BeforeDisplayRenderRowEventHandler(_dataGrid__beforeDisplayRendering);
            this._dataGrid._beforeDisplayRow += _dataGrid__beforeDisplayRow;
            this._dataGrid._mouseClickClip += _dataGrid__mouseClickClip;
            this._dataGrid._beforeLoadDataObjectColumn += _dataGrid__beforeLoadDataObjectColumn;
        }

        private MyLib.BeforeDisplayRowReturn _dataGrid__beforeDisplayRow(MyLib._myGrid sender, int row, int columnNumber, string columnName, MyLib.BeforeDisplayRowReturn senderRow, MyLib._myGrid._columnType columnType, ArrayList rowData)
        {
            if (columnName.Equals(_g.d.master_logs._table + "." + _g.d.master_logs._menu_name))
            {
                int __menuNameColumn = sender._findColumnByName(_g.d.master_logs._menu_name);
                string __menuName = sender._cellGet(row, _g.d.master_logs._menu_name).ToString().ToUpper();
                ((ArrayList)senderRow.newData)[__menuNameColumn] = __menuName + " (" + MyLib._myResource._findResource(__menuName, __menuName)._str + ")";
            }
            else
                       if (columnName.Equals(_g.d.master_logs._table + "." + _g.d.master_logs._function_code))
            {
                int __columnNumber = sender._findColumnByName(_g.d.logs._function_code);
                int __functionCode = MyLib._myGlobal._intPhase(sender._cellGet(row, __columnNumber).ToString());
                string __detail = "";
                switch (__functionCode)
                {
                    case 1: __detail = "Append"; break;
                    case 2: __detail = "Update"; break;
                    case 3: __detail = "Delete"; break;
                }
                           ((ArrayList)senderRow.newData)[__columnNumber] = __detail;
            }
            return senderRow;
        }

        private object _dataGrid__beforeLoadDataObjectColumn(object sender, int row, int column)
        {
            return sender.ToString();
        }

        private void _dataGrid__mouseClickClip(object sender, MyLib.GridCellEventArgs e)
        {

            if (e._columnName.Equals(_g.d.master_logs._data1) || (e._columnName.Equals(_g.d.master_logs._data2)))
            {
                if (e._row != -1)
                {
                    string __roworder = this._dataGrid._cellGet(e._row, "roworder").ToString();
                    string __query = "select " + e._columnName + " from " + _g.d.master_logs._table + " where roworder = " + __roworder;

                    MyLib._myFrameWork __fw = new MyLib._myFrameWork();
                    byte[] __getByte = __fw._queryByte(MyLib._myGlobal._databaseName, __query);

                    string __dataStr = System.Text.Encoding.UTF8.GetString(__getByte, 0, __getByte.Length);
                    MessageBox.Show(__dataStr);
                }
            }

        }

        private void _processButton_Click(object sender, EventArgs e)
        {
            this._dataGrid._clear();
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            string __query = "select roworder," + _g.d.master_logs._user_code + "," + _g.d.master_logs._computer_name + "," + _g.d.master_logs._menu_name + "," +
                _g.d.master_logs._function_code + "," + _g.d.master_logs._date_time + "," +
                _g.d.master_logs._code + "," + _g.d.master_logs._name_1 +
                " from " + _g.d.master_logs._table +
                " where (" + _g.d.master_logs._date_time + " between " + this._conditionScreen._getDataStrQuery(_g.d.logs._date_begin, " 00:00") + " and " + this._conditionScreen._getDataStrQuery(_g.d.logs._date_end, " 23:59") + ")";
            string __search = this._conditionScreen._getDataStr(_g.d.logs._search).Trim();
            if (__search.Length > 0)
            {
                __query = __query + " and (" + _g.d.logs._user_code + " like \'%" + __search + "%\' or " + _g.d.logs._computer_name + " like \'%" + __search + "%\')";
            }
            DataTable __getData = __myFrameWork._queryShort(__query.ToLower()).Tables[0];
            this._dataGrid._loadFromDataTable(__getData);

        }
    }
}
