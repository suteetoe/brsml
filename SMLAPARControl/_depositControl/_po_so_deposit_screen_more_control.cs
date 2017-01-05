using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Globalization;

namespace SMLERPAPARControl._depositControl
{
    public class _po_so_deposit_screen_more_control : MyLib._myScreen
    {
        private MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
        public string _screen_code = "";
        private _g.g._transControlTypeEnum _icTransControlTypeTemp;
        private int _buildCount = 0;

        private TextBox _searchTextBox;
        private string _searchName = "";
        private ArrayList _search_data_full_buffer = new ArrayList();
        private ArrayList _search_data_full_buffer_name = new ArrayList();
        private int _search_data_full_buffer_addr = -1;
        private MyLib._searchDataFull _search_data_full_pointer;
        private string _old_filed_name = "";
        private ArrayList __searchScreenMasterList = new ArrayList();
        private SMLERPGlobal._searchProperties __searchScreenProperties = new SMLERPGlobal._searchProperties();


        public _g.g._transControlTypeEnum _icTransControlType
        {
            set
            {
                if (MyLib._myGlobal._isDesignMode == false)
                {
                    this._icTransControlTypeTemp = value;
                    this._build();
                    this.Invalidate();
                }
            }
            get
            {
                return this._icTransControlTypeTemp;
            }
        }

        public void _newData()
        {
            this._setDataStr(_g.d.ic_trans._branch_code, MyLib._myGlobal._branchCode);
            this._setDataStr(_g.d.ic_trans._cashier_code, MyLib._myGlobal._userCode);
        }

        void _build()
        {
            if (this._icTransControlType == _g.g._transControlTypeEnum.ว่าง)
            {
                return;
            }
            this._buildCount++;
            if (this._buildCount > 1)
            {
                MessageBox.Show("_po_so_deposit_screen_more_control : มีการสร้างจอสองครั้ง");
            }
            int __row = 0;
            this._reset();
            string __formatNumber = MyLib._myGlobal._getFormatNumber("m02");
            string __formatNumberNone = MyLib._myGlobal._getFormatNumber("m00");
            this._table_name = _g.d.ic_trans._table;
            this._maxColumn = 2;
            ///
            if (_g.g._companyProfile._branchStatus == 1)
            {
                this._addTextBox(__row++, 0, 1, 0, _g.d.ic_trans._branch_code, 1, 1, 1, true, false, false);
                if (_g.g._companyProfile._change_branch_code == false)
                {
                    this._enabedControl(_g.d.ic_trans._branch_code, false);
                }
                this._setDataStr(_g.d.ic_trans._branch_code, MyLib._myGlobal._branchCode);
            }
            //
            this._addTextBox(__row++, 0, 1, 0, _g.d.ic_trans._cashier_code, 1, 1, 0, true, false, false);
            this._enabedControl(_g.d.ic_trans._cashier_code, false);
            this._setDataStr(_g.d.ic_trans._cashier_code, MyLib._myGlobal._userCode);

            this._textBoxSearch += _po_so_deposit_screen_more_control__textBoxSearch;
        }

        void _po_so_deposit_screen_more_control__textBoxSearch(object sender)
        {
            MyLib._myTextBox __getControl = (MyLib._myTextBox)sender;
            if (!this._old_filed_name.Equals(__getControl._name.ToLower()))
            {
                Boolean __found = false;
                this._old_filed_name = __getControl._name.ToLower();
                // jead
                int __addr = 0;
                for (int __loop = 0; __loop < this._search_data_full_buffer_name.Count; __loop++)
                {
                    if (__getControl._name.Equals(((string)this._search_data_full_buffer_name[__loop]).ToString()))
                    {
                        __addr = __loop;
                        __found = true;
                        break;
                    }
                }
                if (__found == false)
                {
                    __addr = this._search_data_full_buffer_name.Add(__getControl._name);
                    MyLib._searchDataFull __searchObject = new MyLib._searchDataFull();
                    if (__getControl._iconNumber == 4)
                    {
                        __searchObject.WindowState = FormWindowState.Maximized;
                    }
                    this._search_data_full_buffer.Add(__searchObject);
                }

                this._search_data_full_pointer = (MyLib._searchDataFull)this._search_data_full_buffer[__addr];
                this._search_data_full_buffer_addr = __addr;
                //
            }
            //   MyLib._myTextBox __getControl = (MyLib._myTextBox)sender;
            this._searchTextBox = __getControl.textBox;
            this._searchName = ((MyLib._myTextBox)sender)._label._field_name.ToLower();
            string label_name = ((MyLib._myTextBox)sender)._labelName;
            __searchScreenMasterList.Clear();
            try
            {
                {
                    string __extraWhere = "";
                    __searchScreenMasterList = __searchScreenProperties._setColumnSearch(this._searchName, 0, "", "");
                    if (__searchScreenMasterList.Count > 0)
                    {
                        if (!this._search_data_full_pointer._name.Equals(__searchScreenMasterList[0].ToString().ToLower()))
                        {
                            if (this._search_data_full_pointer._name.Length == 0)
                            {
                                this._search_data_full_pointer._name = __searchScreenMasterList[0].ToString();
                                this._search_data_full_pointer._dataList._loadViewFormat(this._search_data_full_pointer._name, MyLib._myGlobal._userSearchScreenGroup, false);
                                // ถ้าจะเอา event มาแทรกใน function ให้ลบออกก่อนไม่งั้นมันจะวน
                                this._search_data_full_pointer._dataList._gridData._mouseClick -= new MyLib.MouseClickHandler(_gridData__mouseClick);
                                this._search_data_full_pointer._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
                                //
                                this._search_data_full_pointer._searchEnterKeyPress -= new MyLib.SearchEnterKeyPressEventHandler(_search_data_full_pointer__searchEnterKeyPress);
                                this._search_data_full_pointer._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_search_data_full_pointer__searchEnterKeyPress);
                            }
                        }
                        __extraWhere = (__searchScreenMasterList.Count == 3) ? __searchScreenMasterList[2].ToString() : "";
                        MyLib._myGlobal._startSearchBox(__getControl, label_name, this._search_data_full_pointer, false, true, __extraWhere);
                        if (__getControl._iconNumber == 1)
                        {
                            __getControl.Focus();
                            __getControl.textBox.Focus();
                        }
                        else
                        {
                            this._search_data_full_pointer.Focus();
                            this._search_data_full_pointer._firstFocus();
                        }
                    }
                }
            }
            catch (Exception __ex)
            {
                MessageBox.Show(__ex.Message.ToString() + " : " + __ex.StackTrace.ToString());
            }
        }

        void _search_data_full_pointer__searchEnterKeyPress(MyLib._myGrid sender, int row)
        {
            this._searchByParent(sender, row);
        }

        void _searchByParent(object sender, int row)
        {
            MyLib._myDataList __getParent1 = (MyLib._myDataList)((MyLib._myGrid)sender).Parent;
            MyLib._searchDataFull __getParent2 = (MyLib._searchDataFull)__getParent1.Parent;
            this._searchAll(__getParent2._name, row);
            SendKeys.Send("{TAB}");
        }


        void _gridData__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            MyLib._myDataList __getParent1 = (MyLib._myDataList)((MyLib._myGrid)sender).Parent;
            MyLib._searchDataFull __getParent2 = (MyLib._searchDataFull)__getParent1.Parent;
            this._searchAll(__getParent2._name, e._row);
            SendKeys.Send("{ENTER}");
        }

        void _searchAll(string screenName, int row)
        {
            int __columnNumber = 0;
            string __result = (string)this._search_data_full_pointer._dataList._gridData._cellGet(row, __columnNumber);
            if (__result.Length > 0)
            {
                this._search_data_full_pointer.Visible = false;
                this._setDataStr(this._searchName, __result, "", false);
                //this._search();
            }
        }


    }
}
