using MyLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Json;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace SMLSINGHAControl
{
    public partial class _fromfilterdetail : Form
    {
        string _tablename = "";
        int _row;
        JsonValue _resultObject = null;
        DataTable _tableFilter;

        public _fromfilterdetail()
        {
            InitializeComponent();
            this.label1.BackColor = Color.Transparent;
            this._TextBoxSearch.KeyDown += TextBox1_KeyDown;
        }

        private void TextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                _loadDataToGrid(this._TextBoxSearch.Text);
            }
        }



        public void _buil(string tablename)
        {
            this._tablename = tablename;
            _createGrid();
            _loaddata(_tablename);

        }

        public void _createGrid()
        {
            this._singhaGridGetdata1._clear();
            this._singhaGridGetdata1._clearGridColumn();
            this._singhaGridGetdata1._addColumn("check", 11, 0, 20, true, false, false, false);
            this._singhaGridGetdata1._addColumn("Code", 1, 255, 40, true, false, true, false);
            this._singhaGridGetdata1._addColumn("Name", 1, 255, 40, true, false, true, false);
            this._singhaGridGetdata1._alterCellUpdate += _singhaGridGetdata1__alterCellUpdate;

            _tableFilter = new DataTable();
            _tableFilter.Columns.Add(new DataColumn("check", typeof(string)));
            _tableFilter.Columns.Add(new DataColumn("code", typeof(string)));
            _tableFilter.Columns.Add(new DataColumn("name", typeof(string)));
            _tableFilter.Columns.Add(new DataColumn("json", typeof(JsonValue)));
        }

        private void _singhaGridGetdata1__alterCellUpdate(object sender, int row, int column)
        {
            if (row > -1 && row < this._singhaGridGetdata1._rowData.Count)
            {
                string __checked = this._singhaGridGetdata1._cellGet(row, 0).ToString();
                string __code = this._singhaGridGetdata1._cellGet(row, "code").ToString();

                DataRow[] __row = _tableFilter.Select("code=\'" + __code + "\'");
                if (__row.Length > 0)
                {
                    foreach (DataRow dataRow in __row)
                    {
                        dataRow["check"] = __checked;
                    }
                }
            }
        }

        void _loadDataToGrid(string searchString)
        {
            if (searchString.Length > 0)
            {
                String __searchKey = " code like \'%" + searchString + "%\' or name like \'%" + searchString + "%\' ";
                DataRow[] __rows = this._tableFilter.Select(__searchKey);
                this._singhaGridGetdata1._loadFromDataTable(_tableFilter, __rows);
            }
            else
            {
                this._singhaGridGetdata1._loadFromDataTable(_tableFilter);
            }

        }

        public void _loaddata(string _tablename)
        {
            WebClient __n = new WebClient();
            string __getCompanyRestUrl = MyLib._myGlobal._syncMasterUrl + _tablename; //http://localhost:9000/getdb/ MyLib._myGlobal._syncMasterUrl

            if (_g.g._companyProfile._sync_master_url != "")
            {
                __getCompanyRestUrl = _g.g._companyProfile._sync_master_url + _tablename;
            }
            try
            {
                _restClient __rest = new _restClient(__getCompanyRestUrl);
                string __response = __rest.MakeRequest();

                JsonValue __jsonObject = JsonValue.Parse(__response);
                if (__jsonObject.Count > 0)
                {
                    this._resultObject = __jsonObject;
                }
            }
            catch (Exception __ex)
            {
                Console.WriteLine("loaddata error :" + __ex);
                // this._setlog("loaddata error :" + __ex);
            }

            if (_resultObject != null)
            {
                string __value_code = "";
                string __value_name = "";
                for (int __row1 = 0; __row1 < _resultObject.Count; __row1++)
                {
                    // add row to datatable;

                    __value_code = _resultObject[__row1]["code"].ToString().Replace("\"", string.Empty);
                    __value_name = _resultObject[__row1]["name_1"].ToString().Replace("\"", string.Empty);
                    //int row1 = this._singhaGridGetdata1._addRow();
                    //this._singhaGridGetdata1._cellUpdate(row1, 1, __value_code, true);
                    //this._singhaGridGetdata1._cellUpdate(row1, 2, __value_name, true);
                    _tableFilter.Rows.Add(
                        "0",
                        __value_code,
                        __value_name,
                         _resultObject[__row1]
                        );
                }

                this._loadDataToGrid("");
            }


            // make primary key

        }

        public virtual JsonValue _getddata(string _tablename)
        {
            WebClient __n = new WebClient();
            string __getCompanyRestUrl = MyLib._myGlobal._syncMasterUrl + _tablename;

            if (_g.g._companyProfile._sync_master_url != "")
            {
                __getCompanyRestUrl = _g.g._companyProfile._sync_master_url + _tablename;
            }
            try
            {
                _restClient __rest = new _restClient(__getCompanyRestUrl);
                string __response = __rest.MakeRequest();

                JsonValue __jsonObject = JsonValue.Parse(__response);
                if (__jsonObject.Count > 0)
                {
                    this._resultObject = __jsonObject;
                }

            }
            catch (Exception __ex)
            {
                Console.WriteLine("loaddata error :" + __ex);
                // this._setlog("loaddata error :" + __ex);
            }

            return _resultObject;

        }

        private void _buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        public virtual JsonArray _selectdata()
        {
            JsonArray __result = new JsonArray();
            for (int row = 0; row < this._tableFilter.Rows.Count; row++)
            {
                Console.WriteLine("================================ :" + this._tableFilter.Rows[row][1].ToString()+"="+ this._tableFilter.Rows[row][0].ToString());
                if (this._tableFilter.Rows[row][0].ToString().Equals("1"))
                {
                    JsonValue _jsondata = (JsonValue)this._tableFilter.Rows[row][3];
                        __result.Add(_jsondata);
                }

            }
            return __result;
        }

        private void _button_SelectAll_Click(object sender, EventArgs e)
        {
            for (int row = 0; row < this._singhaGridGetdata1._rowData.Count; row++)
            {
                this._singhaGridGetdata1._cellUpdate(row, 0, 1, true);
            }
            this._singhaGridGetdata1.Invalidate();
        }

        private void _button_SelectNone_Click(object sender, EventArgs e)
        {
            for (int row = 0; row < this._singhaGridGetdata1._rowData.Count; row++)
            {
                this._singhaGridGetdata1._cellUpdate(row, 0, 0, true);
            }
            this._singhaGridGetdata1.Invalidate();
        }
    }
}
