using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace _g
{
    public partial class _userGroupWarehouseShelf : UserControl
    {
        public _userGroupWarehouseShelf()
        {
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this._myToolbar.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
                this._tabWarehouse._getResource();
            }

            _myManageData1._dataList._lockRecord = true;
            _myManageData1._dataList._loadViewFormat(_g.g._search_screen_erp_user_group, MyLib._myGlobal._userSearchScreenGroup, true);
            _myManageData1._dataList._referFieldAdd(_g.d.erp_user_group._code, 1);
            _myManageData1._manageButton = this._myToolbar;

            _myManageData1._newDataClick += new MyLib.NewDataEvent(_myManageData1__newDataClick);
            _myManageData1._loadDataToScreen += new MyLib.LoadDataToScreen(_myManageData1__loadDataToScreen);
            _myManageData1._closeScreen += new MyLib.CloseScreenEvent(_myManageData1__closeScreen);

            //this._gridWarehouseShelfList1._alterCellUpdate += new MyLib.AfterCellUpdateEventHandler(_gridWarehouseShelfList1__alterCellUpdate);
            _userGroupNewData();
        }

        //void _gridWarehouseShelfList1__alterCellUpdate(object sender, int row, int column)
        //{
        //    _gridWarehouseShelfSelected1._clear();

        //    for (int __row = 0; __row < _gridWarehouseShelfList1._rowData.Count; __row++)
        //    {
        //        string __select = _gridWarehouseShelfList1._cellGet(__row, 0).ToString();
        //        if (__select.Equals("1"))
        //        {
        //            //string __code = _gridWarehouseShelfList1._cellGet(__row, 1).ToString();
        //            //if (__code.Trim().Length > 0)
        //            //{
        //            //    string __name = _userGroupDetail._userListGrid._cellGet(__row, 2).ToString();
        //            //    __selectUserCode.Add(__code);
        //            //    __selectUserName.Add(__name);
        //            //}

        //            int __addr = _gridWarehouseShelfSelected1._addRow();
        //            _gridWarehouseShelfSelected1._cellUpdate(__addr, _g.d.erp_user_group_wh_shelf._wh_code, _gridWarehouseShelfList1._cellGet(__row, _g.d.erp_user_group_wh_shelf._wh_code), false);
        //            _gridWarehouseShelfSelected1._cellUpdate(__addr, _g.d.erp_user_group_wh_shelf._wh_name, _gridWarehouseShelfList1._cellGet(__row, _g.d.erp_user_group_wh_shelf._wh_name), false);
        //            _gridWarehouseShelfSelected1._cellUpdate(__addr, _g.d.erp_user_group_wh_shelf._shelf_code, _gridWarehouseShelfList1._cellGet(__row, _g.d.erp_user_group_wh_shelf._shelf_code), false);
        //            _gridWarehouseShelfSelected1._cellUpdate(__addr, _g.d.erp_user_group_wh_shelf._shelf_name, _gridWarehouseShelfList1._cellGet(__row, _g.d.erp_user_group_wh_shelf._shelf_name), false);
        //        }
        //    }

        //}

        public void _userGroupNewData()
        {
            _puWarehouseLocation._gridWarehouseShelfList._clear();
            _siWarehouseLocation._gridWarehouseShelfList._clear();
            _transferWarehouseLocation._gridWarehouseShelfList._clear();
            _receiveWarehouseLocation._gridWarehouseShelfList._clear();

            _puWarehouseLocation._gridWarehouseShelfSelected._clear();
            _siWarehouseLocation._gridWarehouseShelfSelected._clear();
            _transferWarehouseLocation._gridWarehouseShelfSelected._clear();
            _receiveWarehouseLocation._gridWarehouseShelfSelected._clear();

            //_gridWarehouseShelfList1._clear();
            //_gridWarehouseShelfSelected1._clear();

            // ดึงคลังและที่เก็บทั้งหมด
            string __query = "select " + MyLib._myGlobal._fieldAndComma(
                _g.d.ic_shelf._whcode + " as " + _g.d.erp_user_group_wh_shelf._wh_code,
                "( select " + _g.d.ic_warehouse._table + "." + _g.d.ic_warehouse._name_1 + " from " + _g.d.ic_warehouse._table + " where " + _g.d.ic_warehouse._table + "." + _g.d.ic_warehouse._code + "=" + _g.d.ic_shelf._table + "." + _g.d.ic_shelf._whcode + ") as " + _g.d.erp_user_group_wh_shelf._wh_name,
                _g.d.ic_shelf._code + " as " + _g.d.erp_user_group_wh_shelf._shelf_code,
                _g.d.ic_shelf._name_1 + " as " + _g.d.erp_user_group_wh_shelf._shelf_name
                ) +

                " from " + _g.d.ic_shelf._table + " order by " + _g.d.ic_shelf._whcode + "," + _g.d.ic_shelf._code;
            MyLib._myFrameWork __frameWork = new MyLib._myFrameWork();
            DataTable __getData = __frameWork._query(MyLib._myGlobal._databaseName, __query).Tables[0];
            //_gridWarehouseShelfList1._loadFromDataTable(__getData);
            _puWarehouseLocation._gridWarehouseShelfList._loadFromDataTable(__getData);
            _siWarehouseLocation._gridWarehouseShelfList._loadFromDataTable(__getData);
            _transferWarehouseLocation._gridWarehouseShelfList._loadFromDataTable(__getData);
            _receiveWarehouseLocation._gridWarehouseShelfList._loadFromDataTable(__getData);

        }

        bool _myManageData1__loadDataToScreen(object rowData, string whereString, bool forEdit)
        {
            _userGroupNewData();

            int __columnNumber = this._myManageData1._dataList._gridData._findColumnByName(_g.d.erp_user_group._table + "." + _g.d.erp_user_group._code);
            ArrayList __rowDataArray = (ArrayList)rowData;
            string screenCode = __rowDataArray[__columnNumber].ToString();

            StringBuilder __query = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.erp_user_group._table + " where " + _g.d.erp_user_group._code + "=\'" + screenCode + "\'"));
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select *, " + "( select " + _g.d.ic_warehouse._table + "." + _g.d.ic_warehouse._name_1 + " from " + _g.d.ic_warehouse._table + " where " + _g.d.ic_warehouse._table + "." + _g.d.ic_warehouse._code + "=" + _g.d.erp_user_group_wh_shelf._table + "." + _g.d.erp_user_group_wh_shelf._wh_code + ") as " + _g.d.erp_user_group_wh_shelf._wh_name + ", " + "( select " + _g.d.ic_shelf._table + "." + _g.d.ic_shelf._name_1 + " from " + _g.d.ic_shelf._table + " where " + _g.d.ic_shelf._table + "." + _g.d.ic_shelf._code + "=" + _g.d.erp_user_group_wh_shelf._table + "." + _g.d.erp_user_group_wh_shelf._shelf_code + " and " + _g.d.ic_shelf._table + "." + _g.d.ic_shelf._whcode + "=" + _g.d.erp_user_group_wh_shelf._table + "." + _g.d.erp_user_group_wh_shelf._wh_code + ") as " + _g.d.erp_user_group_wh_shelf._shelf_name + " from " + _g.d.erp_user_group_wh_shelf._table + " where " + _g.d.erp_user_group_wh_shelf._group_code + "=\'" + screenCode + "\' and " + _g.d.erp_user_group_wh_shelf._screen_code + "=\'PU\'"));
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select *, " + "( select " + _g.d.ic_warehouse._table + "." + _g.d.ic_warehouse._name_1 + " from " + _g.d.ic_warehouse._table + " where " + _g.d.ic_warehouse._table + "." + _g.d.ic_warehouse._code + "=" + _g.d.erp_user_group_wh_shelf._table + "." + _g.d.erp_user_group_wh_shelf._wh_code + ") as " + _g.d.erp_user_group_wh_shelf._wh_name + ", " + "( select " + _g.d.ic_shelf._table + "." + _g.d.ic_shelf._name_1 + " from " + _g.d.ic_shelf._table + " where " + _g.d.ic_shelf._table + "." + _g.d.ic_shelf._code + "=" + _g.d.erp_user_group_wh_shelf._table + "." + _g.d.erp_user_group_wh_shelf._shelf_code + " and " + _g.d.ic_shelf._table + "." + _g.d.ic_shelf._whcode + "=" + _g.d.erp_user_group_wh_shelf._table + "." + _g.d.erp_user_group_wh_shelf._wh_code + ") as " + _g.d.erp_user_group_wh_shelf._shelf_name + " from " + _g.d.erp_user_group_wh_shelf._table + " where " + _g.d.erp_user_group_wh_shelf._group_code + "=\'" + screenCode + "\' and " + _g.d.erp_user_group_wh_shelf._screen_code + "=\'SI\'"));
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select *, " + "( select " + _g.d.ic_warehouse._table + "." + _g.d.ic_warehouse._name_1 + " from " + _g.d.ic_warehouse._table + " where " + _g.d.ic_warehouse._table + "." + _g.d.ic_warehouse._code + "=" + _g.d.erp_user_group_wh_shelf._table + "." + _g.d.erp_user_group_wh_shelf._wh_code + ") as " + _g.d.erp_user_group_wh_shelf._wh_name + ", " + "( select " + _g.d.ic_shelf._table + "." + _g.d.ic_shelf._name_1 + " from " + _g.d.ic_shelf._table + " where " + _g.d.ic_shelf._table + "." + _g.d.ic_shelf._code + "=" + _g.d.erp_user_group_wh_shelf._table + "." + _g.d.erp_user_group_wh_shelf._shelf_code + " and " + _g.d.ic_shelf._table + "." + _g.d.ic_shelf._whcode + "=" + _g.d.erp_user_group_wh_shelf._table + "." + _g.d.erp_user_group_wh_shelf._wh_code + ") as " + _g.d.erp_user_group_wh_shelf._shelf_name + " from " + _g.d.erp_user_group_wh_shelf._table + " where " + _g.d.erp_user_group_wh_shelf._group_code + "=\'" + screenCode + "\' and " + _g.d.erp_user_group_wh_shelf._screen_code + "=\'IM\'"));
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select *, " + "( select " + _g.d.ic_warehouse._table + "." + _g.d.ic_warehouse._name_1 + " from " + _g.d.ic_warehouse._table + " where " + _g.d.ic_warehouse._table + "." + _g.d.ic_warehouse._code + "=" + _g.d.erp_user_group_wh_shelf._table + "." + _g.d.erp_user_group_wh_shelf._wh_code + ") as " + _g.d.erp_user_group_wh_shelf._wh_name + ", " + "( select " + _g.d.ic_shelf._table + "." + _g.d.ic_shelf._name_1 + " from " + _g.d.ic_shelf._table + " where " + _g.d.ic_shelf._table + "." + _g.d.ic_shelf._code + "=" + _g.d.erp_user_group_wh_shelf._table + "." + _g.d.erp_user_group_wh_shelf._shelf_code + " and " + _g.d.ic_shelf._table + "." + _g.d.ic_shelf._whcode + "=" + _g.d.erp_user_group_wh_shelf._table + "." + _g.d.erp_user_group_wh_shelf._wh_code + ") as " + _g.d.erp_user_group_wh_shelf._shelf_name + " from " + _g.d.erp_user_group_wh_shelf._table + " where " + _g.d.erp_user_group_wh_shelf._group_code + "=\'" + screenCode + "\' and " + _g.d.erp_user_group_wh_shelf._screen_code + "=\'ST\'"));
            __query.Append("</node>");

            MyLib._myFrameWork __myFramework = new MyLib._myFrameWork();
            ArrayList __result = __myFramework._queryListGetData(MyLib._myGlobal._databaseName, __query.ToString());

            DataTable __userGroup = ((DataSet)__result[0]).Tables[0];
            DataTable __puWarehouseGroup = ((DataSet)__result[1]).Tables[0];
            DataTable __siWarehouseGroup = ((DataSet)__result[2]).Tables[0];
            DataTable __imWarehouseGroup = ((DataSet)__result[3]).Tables[0];
            DataTable __stWarehouseGroup = ((DataSet)__result[4]).Tables[0];

            // pu grid
            for (int __row = 0; __row < __puWarehouseGroup.Rows.Count; __row++)
            {
                int __addr = -1;

                for (int __rowList = 0; __rowList < this._puWarehouseLocation._gridWarehouseShelfList._rowData.Count; __rowList++)
                {
                    //string __whCode = _gridWarehouseShelfList1._cellGet(__rowList, _g.d.erp_user_group_wh_shelf._wh_code).ToString();
                    //string __shelfCode = _gridWarehouseShelfList1._cellGet(__rowList, _g.d.erp_user_group_wh_shelf._shelf_code).ToString();

                    if (__puWarehouseGroup.Rows[__row][_g.d.erp_user_group_wh_shelf._wh_code].ToString().Equals(this._puWarehouseLocation._gridWarehouseShelfList._cellGet(__rowList, _g.d.erp_user_group_wh_shelf._wh_code).ToString()) && __puWarehouseGroup.Rows[__row][_g.d.erp_user_group_wh_shelf._shelf_code].ToString().Equals(this._puWarehouseLocation._gridWarehouseShelfList._cellGet(__rowList, _g.d.erp_user_group_wh_shelf._shelf_code).ToString()))
                    {
                        __addr = __rowList;
                        break;
                    }
                }
                //string __userCode = __warehouseGroup.Rows[__row][0].ToString();
                //int __addr = _gridWarehouseShelfList1._findData(1, __userCode);
                if (__addr != -1)
                {
                    this._puWarehouseLocation._gridWarehouseShelfList._cellUpdate(__addr, 0, 1, false);
                }
            }

            // si grid
            for (int __row = 0; __row < __siWarehouseGroup.Rows.Count; __row++)
            {
                int __addr = -1;

                for (int __rowList = 0; __rowList < this._siWarehouseLocation._gridWarehouseShelfList._rowData.Count; __rowList++)
                {
                    if (__siWarehouseGroup.Rows[__row][_g.d.erp_user_group_wh_shelf._wh_code].ToString().Equals(this._siWarehouseLocation._gridWarehouseShelfList._cellGet(__rowList, _g.d.erp_user_group_wh_shelf._wh_code).ToString()) && __siWarehouseGroup.Rows[__row][_g.d.erp_user_group_wh_shelf._shelf_code].ToString().Equals(this._siWarehouseLocation._gridWarehouseShelfList._cellGet(__rowList, _g.d.erp_user_group_wh_shelf._shelf_code).ToString()))
                    {
                        __addr = __rowList;
                        break;
                    }
                }
                if (__addr != -1)
                {
                    this._siWarehouseLocation._gridWarehouseShelfList._cellUpdate(__addr, 0, 1, false);
                }
            }

            // im grid
            for (int __row = 0; __row < __imWarehouseGroup.Rows.Count; __row++)
            {
                int __addr = -1;

                for (int __rowList = 0; __rowList < this._transferWarehouseLocation._gridWarehouseShelfList._rowData.Count; __rowList++)
                {
                    if (__imWarehouseGroup.Rows[__row][_g.d.erp_user_group_wh_shelf._wh_code].ToString().Equals(this._transferWarehouseLocation._gridWarehouseShelfList._cellGet(__rowList, _g.d.erp_user_group_wh_shelf._wh_code).ToString()) && __imWarehouseGroup.Rows[__row][_g.d.erp_user_group_wh_shelf._shelf_code].ToString().Equals(this._transferWarehouseLocation._gridWarehouseShelfList._cellGet(__rowList, _g.d.erp_user_group_wh_shelf._shelf_code).ToString()))
                    {
                        __addr = __rowList;
                        break;
                    }
                }
                if (__addr != -1)
                {
                    this._transferWarehouseLocation._gridWarehouseShelfList._cellUpdate(__addr, 0, 1, false);
                }
            }

            // st grid
            for (int __row = 0; __row < __stWarehouseGroup.Rows.Count; __row++)
            {
                int __addr = -1;

                for (int __rowList = 0; __rowList < this._receiveWarehouseLocation._gridWarehouseShelfList._rowData.Count; __rowList++)
                {
                    if (__stWarehouseGroup.Rows[__row][_g.d.erp_user_group_wh_shelf._wh_code].ToString().Equals(this._receiveWarehouseLocation._gridWarehouseShelfList._cellGet(__rowList, _g.d.erp_user_group_wh_shelf._wh_code).ToString()) && __stWarehouseGroup.Rows[__row][_g.d.erp_user_group_wh_shelf._shelf_code].ToString().Equals(this._receiveWarehouseLocation._gridWarehouseShelfList._cellGet(__rowList, _g.d.erp_user_group_wh_shelf._shelf_code).ToString()))
                    {
                        __addr = __rowList;
                        break;
                    }
                }
                if (__addr != -1)
                {
                    this._receiveWarehouseLocation._gridWarehouseShelfList._cellUpdate(__addr, 0, 1, false);
                }
            }

            this._userGroupScreenTop1._loadData(__userGroup);
            this._puWarehouseLocation._gridWarehouseShelfSelected._loadFromDataTable(__puWarehouseGroup);
            this._siWarehouseLocation._gridWarehouseShelfSelected._loadFromDataTable(__siWarehouseGroup);
            this._transferWarehouseLocation._gridWarehouseShelfSelected._loadFromDataTable(__imWarehouseGroup);
            this._receiveWarehouseLocation._gridWarehouseShelfSelected._loadFromDataTable(__stWarehouseGroup);

            return true;
        }

        void _myManageData1__closeScreen()
        {
            this.Dispose();
        }

        void _myManageData1__newDataClick()
        {
            _userGroupNewData();
        }

        void _save_data()
        {
            //this._gridWarehouseShelfSelected1._updateRowIsChangeAll(true);
            this._puWarehouseLocation._gridWarehouseShelfSelected._updateRowIsChangeAll(true);
            this._siWarehouseLocation._gridWarehouseShelfSelected._updateRowIsChangeAll(true);
            this._transferWarehouseLocation._gridWarehouseShelfSelected._updateRowIsChangeAll(true);
            this._receiveWarehouseLocation._gridWarehouseShelfSelected._updateRowIsChangeAll(true);

            string __groupCode = this._userGroupScreenTop1._getDataStr(_g.d.erp_user_group._code);

            if (__groupCode.Length == 0)
            {
                MessageBox.Show("กรุณาเลือกกลุ่ม", "warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // pack query insert
            StringBuilder __query = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.erp_user_group_wh_shelf._table + " where " + _g.d.erp_user_group_wh_shelf._group_code + "=\'" + __groupCode + "\'"));

            // pu warehouse
            __query.Append(this._puWarehouseLocation._gridWarehouseShelfSelected._createQueryForInsert(_g.d.erp_user_group_wh_shelf._table, _g.d.erp_user_group_wh_shelf._group_code + "," + _g.d.erp_user_group_wh_shelf._screen_code + ",", "\'" + __groupCode + "\',\'PU\',"));

            // si warehouse
            __query.Append(this._siWarehouseLocation._gridWarehouseShelfSelected._createQueryForInsert(_g.d.erp_user_group_wh_shelf._table, _g.d.erp_user_group_wh_shelf._group_code + "," + _g.d.erp_user_group_wh_shelf._screen_code + ",", "\'" + __groupCode + "\',\'SI\',"));

            // โอน
            __query.Append(this._transferWarehouseLocation._gridWarehouseShelfSelected._createQueryForInsert(_g.d.erp_user_group_wh_shelf._table, _g.d.erp_user_group_wh_shelf._group_code + "," + _g.d.erp_user_group_wh_shelf._screen_code + ",", "\'" + __groupCode + "\',\'IM\',"));

            // คืน
            __query.Append(this._receiveWarehouseLocation._gridWarehouseShelfSelected._createQueryForInsert(_g.d.erp_user_group_wh_shelf._table, _g.d.erp_user_group_wh_shelf._group_code + "," + _g.d.erp_user_group_wh_shelf._screen_code + ",", "\'" + __groupCode + "\',\'ST\',"));

            __query.Append("</node>");

            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            string __result = __myFrameWork._queryList(MyLib._myGlobal._databaseName, __query.ToString());

            if (__result.Length > 0)
            {
                MessageBox.Show(__result.ToString(), "Warnning", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("บันทึกข้อมูลเรียบร้อย", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _myManageData1._unlockRecord();
            }

        }

        private void _saveButton_Click(object sender, EventArgs e)
        {
            _save_data();
        }

        private void _closeButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }

    public class _userGroupScreenTop : MyLib._myScreen
    {
        public _userGroupScreenTop()
        {
            this._table_name = _g.d.erp_user_group._table;
            this._maxColumn = 1;

            this._addTextBox(0, 0, _g.d.erp_user_group._code, 10);
            this._addTextBox(1, 0, _g.d.erp_user_group._name_1, 100);
            this._addTextBox(2, 0, _g.d.erp_user_group._name_2, 100);
        }
    }

    public class _gridWarehouseShelfList : MyLib._myGrid
    {
        public _gridWarehouseShelfList()
        {
            this._table_name = _g.d.erp_user_group_wh_shelf._table;
            this._addColumn("Select", 11, 1, 10);
            this._addColumn(_g.d.erp_user_group_wh_shelf._wh_code, 1, 10, 20, true, false, true, true);
            this._addColumn(_g.d.erp_user_group_wh_shelf._wh_name, 1, 10, 20, false, false, false, false);
            this._addColumn(_g.d.erp_user_group_wh_shelf._shelf_code, 1, 10, 20, true, false, true, true);
            this._addColumn(_g.d.erp_user_group_wh_shelf._shelf_name, 1, 10, 30, false, false, false, false);
        }
    }

    public class _gridWarehouseShelfSelected : MyLib._myGrid
    {
        public _gridWarehouseShelfSelected()
        {
            this._table_name = _g.d.erp_user_group_wh_shelf._table;

            this._addColumn(_g.d.erp_user_group_wh_shelf._wh_code, 1, 10, 20, true, false, true, true);
            this._addColumn(_g.d.erp_user_group_wh_shelf._wh_name, 1, 10, 30, false, false, false, false);
            this._addColumn(_g.d.erp_user_group_wh_shelf._shelf_code, 1, 10, 20, true, false, true, true);
            this._addColumn(_g.d.erp_user_group_wh_shelf._shelf_name, 1, 10, 30, false, false, false, false);
        }
    }
}
