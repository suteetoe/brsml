using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLERPGL._report._reportDesign
{
    public partial class _saveAsControl : Form
    {
        public string _codeReturn = "";
        public string _nameReturn = "";
        private string _data = null;
        private string _oldCode = "";
        private MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
        public _saveAsControl(string code, string data)
        {
            InitializeComponent();
            this._data = data;
            this._oldCode = code;
        }

        private void _buttonSave_Click(object sender, EventArgs e)
        {
            if (this._oldCode.Length == 0)
            {
                // Insert
                string __code = _codeTextBox.textBox.Text.ToUpper().Trim();
                string __queryCount = "select count(*) as xcount from " + _g.d.gl_design_report._table + " where " + _g.d.gl_design_report._code + "='" + __code + "'";
                DataTable __countDataTable = _myFrameWork._queryShort(__queryCount).Tables[0];
                int __totalRecord = (int)MyLib._myGlobal._decimalPhase(__countDataTable.Rows[0][0].ToString());
                if (__totalRecord == 0)
                {
                    string __query = "insert into " + _g.d.gl_design_report._table + " (" + _g.d.gl_design_report._code + "," + _g.d.gl_design_report._name1 + "," + _g.d.gl_design_report._data + ") values ('" + __code + "','" + _name1TextBox.textBox.Text.Trim() + "','" + Convert.ToBase64String(Encoding.Unicode.GetBytes(this._data)) + "')";
                    string __result = _myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __query);
                    if (__result.Length == 0)
                    {
                        this._codeReturn = _codeTextBox.textBox.Text.ToUpper().Trim();
                        this._nameReturn = _name1TextBox.textBox.Text.ToUpper().Trim();
                        this.Dispose();
                    }
                    else
                    {
                        MessageBox.Show(__result);
                    }
                }
                else
                {
                    MessageBox.Show("รหัสซ้ำ");
                }
            }
            else
            {
                // Update
                StringBuilder __myQuery = new StringBuilder();
                __myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete " + _g.d.gl_design_report._table + " where " + _g.d.gl_design_report._code + "='" + this._oldCode + "'"));
                __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.gl_design_report._table + " (" + _g.d.gl_design_report._code + "," + _g.d.gl_design_report._name1 + "," + _g.d.gl_design_report._data + ") values ('" + this._oldCode + "','" + _name1TextBox.textBox.Text.Trim() + "','" + Convert.ToBase64String(Encoding.Unicode.GetBytes(this._data)) + "')"));
                __myQuery.Append("</node>");
                string __result = _myFrameWork._queryList(MyLib._myGlobal._databaseName, __myQuery.ToString());
                if (__result.Length != 0)
                {
                    MessageBox.Show(__result);
                }
            }
        }
    }
}
