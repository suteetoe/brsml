using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace SMLERPControl
{
    public partial class _chqReceiveScreen : Form
    {
        string searchName = "";
        TextBox searchTextBox;
        MyLib._searchDataFull searchBank = new MyLib._searchDataFull();

        public _chqReceiveScreen()
        {
            InitializeComponent();
/*            _myScreen1._maxColumn = 1;
            _myScreen1._table_name = _g.d.gl_chq_receive._table;
            _myScreen1._addTextBox(0, 0, 0, 1, _g.d.gl_chq_receive._chq_number, 1, 0, 0, true, false, false);
            _myScreen1._addTextBox(1, 0, 0, 1, _g.d.gl_chq_receive._bank_code, 1, 0, 1, true, false, false);
            _myScreen1._addTextBox(2, 0, _g.d.gl_chq_receive._bank_branch, 0);
            _myScreen1._addTextBox(3, 0, _g.d.gl_chq_receive._owner_name, 0);
            _myScreen1._addNumberBox(4, 0, 0, 0, _g.d.gl_chq_receive._amount, 1, 2, true);
            _myScreen1._addDateBox(5, 0, 1, 0, _g.d.gl_chq_receive._due_date, 1, true,false);
            _myScreen1._addDateBox(6, 0, 1, 0, _g.d.gl_chq_receive._expire_date, 1, true);
            _myScreen1._addTextBox(7, 0, _g.d.gl_chq_receive._remark, 0);
            _myScreen1._refresh();
            _myScreen1._textBoxSearch += new MyLib.TextBoxSearchHandler(_myScreen1__textBoxSearch);
            _myScreen1._textBoxChanged += new MyLib.TextBoxChangedHandler(_myScreen1__textBoxChanged);
            this.Shown += new EventHandler(_chqReceiveScreen_Shown);
            //
            searchBank._name = _g.g._search_screen_bank;
            searchBank._dataList._loadViewFormat(searchBank._name, MyLib._myGlobal._userSearchScreenGroup, false);
            searchBank._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
            searchBank._dataList._refreshData();*/
        }

        /*void _myScreen1__textBoxChanged(object sender, string name)
        {
            if (name.CompareTo(_g.d.gl_chq_receive._bank_code) == 0)
            {
                searchTextBox = (TextBox)sender;
                searchName = name;
                search(true);
            }
        }

        void search(Boolean warning)
        {
            MyLib._myFrameWork myFrameWork = new MyLib._myFrameWork();
            string query ="select " + _g.d.erp_bank._name_1 + " from " + _g.d.erp_bank._table + " where " + _g.d.erp_bank._code + "=\'" + _myScreen1._getDataStr(_g.d.gl_chq_receive._bank_code) + "\'";
            try
            {
                DataSet dataResult = myFrameWork._query(MyLib._myGlobal._databaseName,query);
                ArrayList compareList = new ArrayList();
                compareList.Add(_g.d.gl_chq_receive._bank_code);
                for (int loop = 0; loop < compareList.Count; loop++)
                {
                    string fieldName = (string)compareList[loop];
                    string loopStr = loop.ToString();
                    if (dataResult.Tables[loopStr].Rows.Count > 0)
                    {
                        string getData = dataResult.Tables[loopStr].Rows[0][0].ToString();
                        string getDataStr = _myScreen1._getDataStr(fieldName);
                        _myScreen1._setDataStr(fieldName, getDataStr, getData, true);
                    }
                    if (searchTextBox != null)
                    {
                        if (searchName.CompareTo(fieldName) == 0 && _myScreen1._getDataStr(fieldName) != "")
                        {
                            if (dataResult.Tables[loopStr].Rows.Count == 0 && warning)
                            {
                                MessageBox.Show("ไม่พบ " + _myScreen1._getLabelName(fieldName), MyLib._myGlobal._warningMessage, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                                ((MyLib._myTextBox)searchTextBox.Parent)._textFirst = "";
                                ((MyLib._myTextBox)searchTextBox.Parent)._textSecond = "";
                                ((MyLib._myTextBox)searchTextBox.Parent)._textLast = "";
                                searchTextBox.Focus();
                                break;
                            }
                        }
                    }
                }
            }
            catch
            {
            }
        }

        void _gridData__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            MyLib._myDataList getParent1 = (MyLib._myDataList)((MyLib._myGrid)sender).Parent;
            MyLib._searchDataFull getParent2 = (MyLib._searchDataFull)getParent1.Parent;
            string name = getParent2._name;
            if (name.CompareTo(_g.g._search_screen_bank) == 0)
            {
                string result = (string)searchBank._dataList._gridData._cellGet(e._row, _g.d.erp_bank ._table+ "." + _g.d.erp_bank._code);
                if (result.Length > 0)
                {
                    searchBank.Close();
                    _myScreen1._setDataStr(searchName, result);
                    search(true);
                }
            }
        }

        void _myScreen1__textBoxSearch(object sender)
        {
            string name = ((MyLib._myTextBox)sender)._name;
            string label_name = ((MyLib._myTextBox)sender)._labelName;
            if (name.CompareTo(_g.d.gl_chq_receive._bank_code) == 0)
            {
                searchTextBox = ((MyLib._myTextBox)sender).textBox; 
                searchName = name;
                MyLib._myTextBox getControl = (MyLib._myTextBox)sender;
                MyLib._myGlobal._startSearchBox(getControl, label_name, searchBank);
            }
        }

        void _chqReceiveScreen_Shown(object sender, EventArgs e)
        {
            _myScreen1._focusFirst();
        }

        private void _saveButton_Click(object sender, EventArgs e)
        {

        }*/

        private void _buttonSave_Click(object sender, EventArgs e)
        {
            /*string getEmtry = _myScreen1._checkEmtryField();
            if (getEmtry.Length > 0)
            {
                MyLib._myGlobal._displayWarning(2, getEmtry);
            }
            else
            {
                string query = "";
                ArrayList getData = _myScreen1._createQueryForDatabase();
                query = "insert into " + _g.d.gl_chq_receive._table + " (" + getData[0].ToString() + ") values (" + getData[1].ToString() + ")";
                //
                string myQuery = MyLib._myGlobal._xmlHeader + "<node>";
                myQuery += "<query>" + query + "</query>";
                myQuery += "</node>";
                MyLib._myFrameWork myFrameWork = new MyLib._myFrameWork();
                string result = myFrameWork._queryList(MyLib._myGlobal._databaseName, myQuery);
                if (result.Length == 0)
                {
                    MessageBox.Show("บันทึกข้อมูลเสร็จเรียบร้อย โปรแกรมจะปิดหน้าจอนี้ให้", MyLib._myGlobal._successMessage, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _myScreen1._isChange = false;
                    Close();
                }
                else
                {
                    MessageBox.Show(result, "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }*/
        }
    }
}