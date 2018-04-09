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
        int _row ;
        JsonValue _resultObject = null;
        public _fromfilterdetail()
        {
            InitializeComponent();
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
                    __value_code = _resultObject[__row1]["code"].ToString().Replace("\"", string.Empty);
                    __value_name = _resultObject[__row1]["name_1"].ToString().Replace("\"", string.Empty);
                    int row1 = this._singhaGridGetdata1._addRow();
                    this._singhaGridGetdata1._cellUpdate(row1, 1, __value_code, true);
                    this._singhaGridGetdata1._cellUpdate(row1, 2, __value_name, true);
                }
            }

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


        public virtual JsonArray _selectdata(JsonValue _resultObject)
        {
            JsonArray __result = new JsonArray();

            for (int row = 0; row < this._singhaGridGetdata1._rowData.Count; row++)
            {
                if (this._singhaGridGetdata1._cellGet(row, 0).ToString().Equals("1"))
                {
                    string __code = this._singhaGridGetdata1._cellGet(row, 1).ToString();
                    for (int __row1 = 0; __row1 < _resultObject.Count; __row1++)
                    {
                        string __value_code = "";
                        __value_code = _resultObject[__row1]["code"].ToString().Replace("\"", string.Empty);
                        if (__value_code.Equals(__code))
                        {
                            __result.Add(_resultObject[__row1]);
                        }
                    }

                }
            }
            return __result;
        }

    }
}
