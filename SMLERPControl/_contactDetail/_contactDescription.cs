using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace SMLERPControl._contactDetail
{
    public partial class _contactDescription : UserControl
    {
        ///public string _screenName = "";
        public string _screenName ="";
        public string _screenSearch;
        _setColumnName _setColumnField = new _setColumnName();
        MyLib._searchDataFull searchContact;
        public _contactDescription()
        {
            InitializeComponent();
        }
       
        public class _setColumnName
        {
            public string _tableName;
            public string _Code;
            public string _Name;
            public string _Address;
            public string _Worktitle;
            public string _Workdepartment;
            public string _Telephone;
            public string _Mobile;
            public string _Email;        
        }
        void _clearDataTemp()
        {
            _setColumnField._Code = "";
            _setColumnField._Name = "";
            _setColumnField._Address = "";
            _setColumnField._Worktitle = "";
            _setColumnField._Workdepartment = "";
            _setColumnField._Telephone = "";
            _setColumnField._Mobile = "";
            _setColumnField._Email = "";
        }
        // รับข้อมูลจากส่วนของ Screen
        public void _createColumnGrid(string __tableName, bool __widthpersent, bool __totalshow, int __amountColumn)
        {
            if (__tableName == "ap_contactor")
            {
                _screenName = "ap_contactor";
                _setFieldColumnAp(__tableName, __widthpersent, __totalshow, __amountColumn);
            }
            if (__tableName == "ar_contactor")
            {
                _screenName = "ar_contactor";
                _setFieldColumnAr(__tableName, __widthpersent, __totalshow, __amountColumn);
            }
        }
        // กำหนดขนาด Column AP
        void _setFieldColumnAp(string __tableName, bool __widthpersent, bool __totalshow, int __amountColumn)
        {
            _clearDataTemp();
            _setColumnField._tableName = __tableName;
            _setColumnField._Code = _g.d.ap_contactor._ap_code;
            _setColumnField._Name = _g.d.ap_contactor._name;
            _setColumnField._Address = _g.d.ap_contactor._address;
            _setColumnField._Worktitle = _g.d.ap_contactor._work_title;
            _setColumnField._Workdepartment = _g.d.ap_contactor._work_department;
            _setColumnField._Telephone = _g.d.ap_contactor._telephone;
            _setColumnField._Mobile = _g.d.ap_contactor._mobile;
            _setColumnField._Email = _g.d.ap_contactor._email;
            _createGridColumnField(_setColumnField._tableName, __widthpersent, __totalshow, 8);
        }
        // กำหนดขนาด Column AR
        void _setFieldColumnAr(string __tableName, bool __widthpersent, bool __totalshow, int __amountColumn)
        {
            _clearDataTemp();
            _setColumnField._tableName = __tableName;
            _setColumnField._Code = _g.d.ar_contactor._ar_code;
            _setColumnField._Name = _g.d.ar_contactor._name;
            _setColumnField._Address = _g.d.ar_contactor._address;
            _setColumnField._Worktitle = _g.d.ar_contactor._work_title;
            _setColumnField._Workdepartment = _g.d.ar_contactor._work_department;
            _setColumnField._Telephone = _g.d.ar_contactor._telephone;
            _setColumnField._Mobile = _g.d.ar_contactor._mobile;
            _setColumnField._Email = _g.d.ar_contactor._email;
            _createGridColumnField(_setColumnField._tableName, __widthpersent, __totalshow, 8);
        }       
        // กำหนดความกว้างของ Column
        public ArrayList _setColumnFieldWidth(int _columnWidth)
        {
            ArrayList __columnField = new ArrayList();
            switch (_columnWidth)
            {
                case 4:
                    __columnField.Add(15);
                    __columnField.Add(45);
                    __columnField.Add(20);
                    __columnField.Add(20); break;
                case 5:
                    __columnField.Add(15);
                    __columnField.Add(40);
                    __columnField.Add(15);
                    __columnField.Add(15);
                    __columnField.Add(15); break;
                case 6:
                    __columnField.Add(10);
                    __columnField.Add(30);
                    __columnField.Add(15);
                    __columnField.Add(15);
                    __columnField.Add(15);
                    __columnField.Add(15); break;
                case 7:
                    __columnField.Add(10);
                    __columnField.Add(30);
                    __columnField.Add(12);
                    __columnField.Add(12);
                    __columnField.Add(12);
                    __columnField.Add(12);
                    __columnField.Add(12); break;
                case 8:
                    __columnField.Add(10);
                    __columnField.Add(20);
                    __columnField.Add(20);
                    __columnField.Add(10);
                    __columnField.Add(10);
                    __columnField.Add(10);
                    __columnField.Add(10);
                    __columnField.Add(10); break;
            }
            return __columnField;
        }
        void _createGridColumnField(string __tableName, bool __widthPersent, bool __totalRow, int __amountColumn)
        {
            ArrayList __getColumn = _setColumnFieldWidth(__amountColumn);
            int __iWidth = 0;
            this._myGridContact._table_name = __tableName.ToString();
            this._myGridContact._width_by_persent = __widthPersent;
            this._myGridContact._total_show = __totalRow;
            if (_setColumnField._Code != "")
            {
                this._myGridContact._addColumn(_setColumnField._Code, 1, 25, (int)__getColumn[__iWidth], true, false, true, true);
                __iWidth++;
            }
            if (_setColumnField._Name != "")
            {
                this._myGridContact._addColumn(_setColumnField._Name, 1, 25, (int)__getColumn[__iWidth], true, false);
                __iWidth++;
            }
            if (_setColumnField._Address != "")
            {
                this._myGridContact._addColumn(_setColumnField._Address, 1, 25, (int)__getColumn[__iWidth], true, false);
                __iWidth++;
            }
            if (_setColumnField._Worktitle != "")
            {
                this._myGridContact._addColumn(_setColumnField._Worktitle, 1, 25, (int)__getColumn[__iWidth], true, false);
                __iWidth++;
            }
            if (_setColumnField._Workdepartment != "")
            {
                this._myGridContact._addColumn(_setColumnField._Workdepartment, 1, 25, (int)__getColumn[__iWidth], true, false);
                __iWidth++;
            }
            if (_setColumnField._Telephone != "")
            {
                this._myGridContact._addColumn(_setColumnField._Telephone, 1, 25, (int)__getColumn[__iWidth], true, false);
                __iWidth++;
            }
            if (_setColumnField._Mobile != "")
            {
                this._myGridContact._addColumn(_setColumnField._Mobile, 1, 25, (int)__getColumn[__iWidth], true, false);
                __iWidth++;
            }
            if (_setColumnField._Email != "")
            {
                this._myGridContact._addColumn(_setColumnField._Email, 1, 25, (int)__getColumn[__iWidth], true, false);
                __iWidth++;
            }           
            this._myGridContact._clickSearchButton += new MyLib.SearchEventHandler(_myGridContact__clickSearchButton);
            //this._myGridContact._alterCellUpdate += new MyLib.AfterCellUpdateEventHandler(_myGridContact__alterCellUpdate);
            this._myGridContact.Invalidate();
            this._myGridContact.Refresh();
        }
        public void _clearDataGrid()
        {
            this._myGridContact._clear();
        }

        void _myGridContact__clickSearchButton(object sender, MyLib.GridCellEventArgs e)
        {
            if (e._columnName.CompareTo(_setColumnField._Code) == 0)
            {
                if (searchContact == null)
                {
                    _setscreenSearch(_screenName);
                   
                   searchContact = new MyLib._searchDataFull();
                   searchContact._name = _screenSearch;
                   searchContact._dataList._loadViewFormat(searchContact._name, MyLib._myGlobal._userSearchScreenGroup, false);
                   searchContact._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
                   searchContact._dataList._refreshData();
                }
                searchContact.Text = e._columnName;
                MyLib._myGlobal._startSearchBox(this._myGridContact._inputTextBox, e._columnName, searchContact);
            }          
        }
        void _gridData__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            searchContact.Close();
            _myGridContact._cellUpdate(this._myGridContact._selectRow, _setColumnField._Code, e._text, true);
        }
        void _setscreenSearch(string _getSearch)
        {
            if (_getSearch == "ar_contactor")
            {
                _screenSearch = _g.g._search_screen_ar;
            }
            if (_getSearch == "ap_contactor")
            {
                _screenSearch = _g.g._search_screen_ap;
            }
        }
    }
}
