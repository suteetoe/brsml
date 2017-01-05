using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace SMLERPControl.erp_user
{
    public partial class _screenerp_user :MyLib._myScreen
    {
        MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
        string _searchName = "";
        TextBox _searchTextBox;
        public _screenerp_user()
        {
            this.SuspendLayout();
            this._maxColumn = 1;
            this.AutoSize = true;
            this._table_name = _g.d.erp_user._table;
            this._addTextBox(0, 0, 1, 0, _g.d.erp_user._code, 1, 0, 0, true, false, false);
            this._addTextBox(1, 0, 1, 0, _g.d.erp_user._name_1, 1, 0, 0, true, false, false);
            this._addTextBox(2, 0, 1, 0, _g.d.erp_user._name_2, 1, 0, 0, true, false, true);
            this._addTextBox(3, 0, 1, 0, _g.d.erp_user._id_card, 1, 0, 0, true, false, true);
            this._addTextBox(4, 0, 3, 0, _g.d.erp_user._address, 1, 0, 0, true, false, true);
            this._addCheckBox(7, 0, _g.d.erp_user._status, true, false);
            this._addCheckBox(8, 0, _g.d.erp_user._price_level_2, true, false);
            this._addCheckBox(9, 0, _g.d.erp_user._status, true, false);
            this._addTextBox(10, 0, 1, 0, _g.d.erp_user._password, 1, 0, 0, true, true, true);
            this._refresh();
            this.ResumeLayout();
           // this._textBoxChanged += new MyLib.TextBoxChangedHandler(_screenerp_user__textBoxChanged);
            this.Invalidate();
        }
        public void _search(Boolean warning)
        {
            try
            {
                // ค้นหาชื่อกลุ่มลูกค้า
                StringBuilder __myquery = new StringBuilder();
                __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");

                //_controlTypeEnum.ArDetailGroup
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.erp_user._name_1 + " from " + _g.d.erp_user._table + " where " + _g.d.erp_user._code + "=\'" + this._getDataStr(_g.d.erp_user._code) + "\'"));


                __myquery.Append("</node>");
                ArrayList _getData = _myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
                _searchAndWarning(_g.d.erp_user._code, (DataSet)_getData[0], warning);
            }
            catch { }
        }
        bool _searchAndWarning(string fieldName, DataSet _dataResult, Boolean warning)
        {
            bool __result = false;
            if (_dataResult.Tables[0].Rows.Count > 0)
            {
                string __getData = _dataResult.Tables[0].Rows[0][0].ToString();
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
                    if (_dataResult.Tables[0].Rows.Count == 0 && warning)
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

        void _screenerp_user__textBoxChanged(object sender, string name)
        {
            if (name.Equals(_g.d.erp_user._code)){
             this._searchTextBox = (TextBox)sender;
                    this._searchName = name;
                    this._search(true);
            }
        }
    }
}
