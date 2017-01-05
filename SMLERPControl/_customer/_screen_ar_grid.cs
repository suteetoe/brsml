using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

/*
* @author MooAe 
* @copyright 2009
* @mail naiay@msn.com
*/

namespace SMLERPControl._customer
{

    public partial class _screen_ar_grid : MyLib._myGrid
    {
        MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
        MyLib._searchDataFull _search_data_full = new MyLib._searchDataFull();
        private _controlTypeEnumGrid _controlTypeTemp;

        public _controlTypeEnumGrid _controlName
        {
            set
            {
                this._controlTypeTemp = value;
                this._build();
                this.Invalidate();
            }
            get
            {
                return this._controlTypeTemp;
            }
        }

        public _screen_ar_grid()
        {
            this._build();
        }
        void _build(){

            this.SuspendLayout();
            this._columnList.Clear();
            switch (_controlName)
            {
                case _controlTypeEnumGrid.ArContactorGrid:
                    this._addRowEnabled = true;
                    this._table_name = _g.d.ar_contactor._table;
                    this._addColumn(_g.d.ar_contactor._name, 1, 0, 15, true, false, true);
                    this._addColumn(_g.d.ar_contactor._address, 1, 0, 35, true, false, true);
                    this._addColumn(_g.d.ar_contactor._telephone, 1, 0, 10, true, false, true);
                    this._addColumn(_g.d.ar_contactor._mobile, 1, 0, 10, true, false, true);
                    this._addColumn(_g.d.ar_contactor._email, 1, 0, 10, true, false, true);
                    this._addColumn(_g.d.ar_contactor._work_department, 1, 0, 10, true, false, true);
                    this._addColumn(_g.d.ar_contactor._work_title, 1, 0, 10, true, false, true);
                    //this._addColumn(_g.d.ar_contactor._group_contactor, 1, 0, 10, true, false, true, true);
                    //this._addColumn(_g.d.ar_contactor._row_number, 3, 1, 10, false, true, true);
                    this._addColumn(_g.d.ar_contactor._line_number, 1, 1, 10, false, true, true);
                    break;
                case _controlTypeEnumGrid.ArItemGrid:
                    this._addRowEnabled = true;
                    this._table_name = _g.d.ar_item_by_customer._table;
                    this._addColumn(_g.d.ar_item_by_customer._ic_code, 1, 0, 40, true, false, true, true);
                    /* ----------
                    this._addColumn(_g.d.ar_item_by_customer._ic_name, 1, 0, 35, false, false, true);
                    */
                    this._addColumn(_g.d.ar_item_by_customer._remark, 1, 0, 50, true, false, true);
                    this._addColumn(_g.d.ar_item_by_customer._status, 11, 0, 10, true, false, true);
                    //this._addColumn(_g.d.ar_item_by_customer._row_number, 3, 1, 10, false, true, true);
                    this._addColumn(_g.d.ar_item_by_customer._line_number, 1, 1, 10, false, true, true);
                    break;
                case _controlTypeEnumGrid.M_congenital_disease:
                    this._addRowEnabled = true;
                    this._table_name = _g.d.m_congenital_disease._table;
                    this._addColumn(_g.d.m_congenital_disease._disease, 1, 0, 20, true, false, true, true);
                    this._addColumn(_g.d.m_congenital_disease._disease_name, 1, 0, 30, false, false, false);
                    this._addColumn(_g.d.m_congenital_disease._symptom, 1, 0, 20, true, false, true);
                    this._addColumn(_g.d.m_congenital_disease._remark, 1, 0, 30, true, false, true);
                    break;
                case _controlTypeEnumGrid.M_allergic:
                    this._addRowEnabled = true;
                    this._table_name = _g.d.m_allergic._table;
                    this._addColumn(_g.d.m_allergic._ic_group_main, 1, 0, 20, true, false, true, true);
                    this._addColumn(_g.d.m_allergic._allergic_namegroup, 1, 0, 30, false, false, false, false);
                    this._addColumn(_g.d.m_allergic._symptom, 1, 0, 20, true, false, true);
                    this._addColumn(_g.d.m_allergic._remark, 1, 0, 30, true, false, true);
                    break;
            }
            this.ColumnBackground = Color.FromArgb(250, 251, 252);
            this.ColumnBackgroundEnd = Color.FromArgb(198, 220, 249);
            this.ColumnBackgroundAuto = false;
            this._rowNumberWork = true;
            this._moveNextColumn -= new MyLib.MoveNextColumnEventHandler(_arCustomerContactGrid__moveNextColumn);
            this._clickSearchButton -= new MyLib.SearchEventHandler(_arCustomerContactGrid__clickSearchButton);
            this._afterAddRow -= new MyLib.AfterAddRowEventHandler(_screen_ar_grid__afterAddRow);
            this._moveNextColumn += new MyLib.MoveNextColumnEventHandler(_arCustomerContactGrid__moveNextColumn);
            this._clickSearchButton += new MyLib.SearchEventHandler(_arCustomerContactGrid__clickSearchButton);
            this._afterAddRow += new MyLib.AfterAddRowEventHandler(_screen_ar_grid__afterAddRow);
            this._queryForInsertCheck += new MyLib.QueryForInsertCheckEventHandler(_screen_ar_grid__queryForInsertCheck);
            this._alterCellUpdate += new MyLib.AfterCellUpdateEventHandler(_screen_ar_grid__alterCellUpdate);
            this.ResumeLayout();
        }
        void _screen_ar_grid__alterCellUpdate(object sender, int row, int column)
        {
            switch (_controlName)
            {
                case _controlTypeEnumGrid.M_allergic:
                    if (column == 0)
                    {
                        this._searchAndWarning(row);
                    }
                    break;
                case _controlTypeEnumGrid.M_congenital_disease:
                    if (column == 0)
                    {
                        this._searchAndWarning(row);
                    }
                    break;
            }

        }
        bool _screen_ar_grid__queryForInsertCheck(MyLib._myGrid sender, int row)
        {
            bool __result = false;
            switch (_controlName)
            {
                case _controlTypeEnumGrid.M_allergic:
                    __result = ((((string)this._cellGet(row, _g.d.m_allergic._ic_group_main)).Length == 0) ? false : true);
                    break;
                case _controlTypeEnumGrid.M_congenital_disease:
                    __result = ((((string)this._cellGet(row, _g.d.m_congenital_disease._disease)).Length == 0) ? false : true);
                    break;
                case _controlTypeEnumGrid.ArContactorGrid :
                    __result = ((((string)this._cellGet(row, _g.d.ar_contactor._name)).Length == 0) ? false : true);
                    break;
                case _controlTypeEnumGrid.ArItemGrid:
                    __result = ((((string)this._cellGet(row, _g.d.ar_item_by_customer._ic_code)).Length == 0) ? false : true);
                    break;
            }
            return __result;
        }

        void _screen_ar_grid__afterAddRow(object sender, int row)
        {
            switch (_controlName)
            {
                case _controlTypeEnumGrid.ArContactorGrid:
                    this._cellUpdate(row, _g.d.ar_item_by_customer._line_number, row, false);
                    break;
                case _controlTypeEnumGrid.ArItemGrid:
                    this._cellUpdate(row, _g.d.ar_contactor._line_number, row, false);
                    break;
            }
        }

        void _arCustomerContactGrid__clickSearchButton(object sender, MyLib.GridCellEventArgs e)
        {
            string _searchName = e._columnName;//this._cellGet(this._selectRow, this._selectColumn).ToString();
            //string _searchName = ((MyLib._myGrid)sender)...ToLower();
            string _search_text_new = _search_screen_neme(_searchName);
            if (!this._search_data_full._name.Equals(_search_text_new.ToLower()))
            {
                _search_data_full = new MyLib._searchDataFull();
                _search_data_full._name = _search_text_new;
                _search_data_full._dataList._loadViewFormat(_search_data_full._name, MyLib._myGlobal._userSearchScreenGroup, false);
                _search_data_full._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
                _search_data_full._dataList._refreshData();
                _search_data_full._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_search_data_full__searchEnterKeyPress);
                _search_data_full._dataList._loadViewData(0);
            }
            MyLib._myGlobal._startSearchBox(this._inputTextBox, e._columnName, _search_data_full);
        }

        void _search_data_full__searchEnterKeyPress(MyLib._myGrid sender, int row)
        {
            _searchGridByParent(sender, row);
        }

        void _searchGridByParent(object sender, int row)
        {
            MyLib._myDataList __getParent1 = (MyLib._myDataList)((MyLib._myGrid)sender).Parent;
            MyLib._searchDataFull __getParent2 = (MyLib._searchDataFull)__getParent1.Parent;
            _searchGridAll(__getParent2._name, row);
            SendKeys.Send("{ENTER}");
        }

        void _searchGridAll(string name, int row)
        {
            if (name.CompareTo(_g.g._search_master_ar_customer_group) == 0)
            {
                string result = (string)_search_data_full._dataList._gridData._cellGet(row, 0);
                if (result.Length > 0)
                {
                    _search_data_full.Close();
                    this._cellUpdate(this._selectRow, _g.d.ar_contactor._group_contactor, result, true);
                    _searchAndWarning(this._selectRow);
                }
            }
            if (name.CompareTo(_g.g._search_screen_ic_inventory) == 0)
            {
                string result = (string)_search_data_full._dataList._gridData._cellGet(row, 0);
                if (result.Length > 0)
                {
                    _search_data_full.Close();
                    this._cellUpdate(this._selectRow, _g.d.ar_item_by_customer._ic_code, result, true);
                    _searchAndWarning(this._selectRow);
                }
            }
            if (name.CompareTo(_g.g._search_screen_healthy_disease) == 0)
            {
                string result = (string)_search_data_full._dataList._gridData._cellGet(row, 0);
                if (result.Length > 0)
                {
                    _search_data_full.Close();
                    this._cellUpdate(this._selectRow, _g.d.m_congenital_disease._disease, result, true);
                    _searchAndWarning(this._selectRow);
                }
            }
            if (name.CompareTo(_g.g._search_master_ic_group) == 0)
            {
                string result = (string)_search_data_full._dataList._gridData._cellGet(row, 0);
                if (result.Length > 0)
                {
                    _search_data_full.Close();
                    this._cellUpdate(this._selectRow, _g.d.m_allergic._ic_group_main, result, true);
                    _searchAndWarning(this._selectRow);
                }
            }
        }

     public void _searchAndWarning(int row)
        {
            // ค้นหารายการหน่วยนับ
            string __unitCode = "";
            string __query = "";
            DataSet __getData = null;
            string __unitName = "";
            switch (_controlName)
            {
                case _controlTypeEnumGrid.M_congenital_disease:
                    __unitCode = this._cellGet(row, _g.d.m_congenital_disease._disease).ToString();
                    __query = "select " + _g.d.m_disease._name_1 + "," + _g.d.m_disease._symptom + " from " + _g.d.m_disease._table + " where " + MyLib._myGlobal._addUpper(_g.d.m_disease._code) + "=\'" + __unitCode.ToUpper() + "\'";
                    __getData = _myFrameWork._query(MyLib._myGlobal._databaseName, __query);
                    __unitName = "";
                    string __symptom = "";
                    if (__getData.Tables.Count > 0)
                    {
                        int __indexName1 = __getData.Tables[0].Columns.IndexOf(_g.d.m_disease._name_1);
                        if (__indexName1 != -1)
                        {
                            __unitName = __getData.Tables[0].Rows[0].ItemArray.GetValue(__indexName1).ToString();
                            __symptom = __getData.Tables[0].Rows[0].ItemArray.GetValue(__getData.Tables[0].Columns.IndexOf(_g.d.m_disease._symptom)).ToString();
                        }
                        else
                        {
                            this._cellUpdate(row, _g.d.m_congenital_disease._disease, __unitName, false);
                        }
                    }
                    this._cellUpdate(row, _g.d.m_congenital_disease._disease_name, __unitName, false);
                  //  this._cellUpdate(row, _g.d.m_congenital_disease._symptom, __symptom, false);
                    break;
                case _controlTypeEnumGrid.M_allergic:
                    __unitCode = this._cellGet(row, _g.d.m_allergic._ic_group_main).ToString();
                    __query = "select " + _g.d.m_disease._name_1 + " from " + _g.d.ic_group._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_group._code) + "=\'" + __unitCode.ToUpper() + "\'";
                    __getData = _myFrameWork._query(MyLib._myGlobal._databaseName, __query);
                    __unitName = "";

                    if (__getData.Tables.Count > 0)
                    {
                        int __indexName1 = __getData.Tables[0].Columns.IndexOf(_g.d.m_disease._name_1);
                        if (__indexName1 != -1)
                        {
                            __unitName = __getData.Tables[0].Rows[0].ItemArray.GetValue(__indexName1).ToString();
                        }
                        else
                        {
                            this._cellUpdate(row, _g.d.m_allergic._ic_group_main, "", false);
                        }
                    }
                    this._cellUpdate(row, _g.d.m_allergic._allergic_namegroup, __unitName, false);
                    break;
                case _controlTypeEnumGrid.ArItemGrid:
                    __unitCode = this._cellGet(row, _g.d.ic_unit_use._code).ToString();
                    __query = "select " + _g.d.ic_unit._name_1 + " from " + _g.d.ic_unit._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_unit._code) + "=\'" + __unitCode.ToUpper() + "\'";
                    __getData = _myFrameWork._query(MyLib._myGlobal._databaseName, __query);
                    __unitName = "";
                    if (__getData.Tables.Count > 0)
                    {
                        int __indexName1 = __getData.Tables[0].Columns.IndexOf(_g.d.ic_unit._name_1);
                        __unitName = __getData.Tables[0].Rows[0].ItemArray.GetValue(__indexName1).ToString();
                    }
                    this._cellUpdate(row, _g.d.ic_unit_use._name_1, __unitName, false);
                    break;
            }
            this.Invalidate();
        }


        void _gridData__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            MyLib._myDataList getParent1 = (MyLib._myDataList)((MyLib._myGrid)sender).Parent;
            MyLib._searchDataFull getParent2 = (MyLib._searchDataFull)getParent1.Parent;
            string name = getParent2._name;
            if (name.CompareTo(_g.g._search_master_ar_customer_group) == 0)
            {
                _search_data_full.Close();
                this._cellUpdate(this._selectRow, _g.d.ar_contactor._group_contactor, e._text, true);
                //_searchAndWarning(this._selectRow);
            }
            if (name.CompareTo(_g.g._search_screen_ic_inventory) == 0)
            {
                _search_data_full.Close();
                this._cellUpdate(this._selectRow, _g.d.ar_item_by_customer._ic_code, e._text, true);
                //_searchAndWarning(this._selectRow);
            }
            if (name.CompareTo(_g.g._search_screen_healthy_disease) == 0)
            {
                _search_data_full.Close();
                this._cellUpdate(this._selectRow, _g.d.m_congenital_disease._disease, e._text, true);
                _searchAndWarning(this._selectRow);
            }
            if (name.CompareTo(_g.g._search_master_ic_group) == 0)
            {
                _search_data_full.Close();
                this._cellUpdate(this._selectRow, _g.d.m_allergic._ic_group_main, e._text, true);
                _searchAndWarning(this._selectRow);
            }
        }

        MyLib._myGridMoveColumnType _arCustomerContactGrid__moveNextColumn(MyLib._myGrid sender, int newRow, int newColumn)
        {
            MyLib._myGridMoveColumnType __result = new MyLib._myGridMoveColumnType();
            switch (_controlName)
            {
                case _controlTypeEnumGrid.ArContactorGrid:

                    if (newColumn == 0)
                    {
                        newColumn = 2;
                    }
                    break;
                case _controlTypeEnumGrid.ArItemGrid:
                    if (newColumn == 0)
                    {
                        newColumn = 0;
                    }

                    break;
                case _controlTypeEnumGrid.M_congenital_disease:
                    if (newColumn == 0)
                    {
                        newColumn = 0;
                    }

                    break;
                case _controlTypeEnumGrid.M_allergic:
                    if (newColumn == 0)
                    {
                        newColumn = 0;
                    }

                    break;
            }

            __result._newColumn = newColumn;
            __result._newRow = newRow;
            return __result;
        }

       
        string _search_screen_neme(string _name)
        {
            switch (_name)
            {
                case "group_contactor": return _g.g._search_master_ar_customer_group;
                case "ic_code": return _g.g._search_screen_ic_inventory;
                case "disease": return _g.g._search_screen_healthy_disease;
                case "ic_group_main": return _g.g._search_master_ic_group;
            }
            return "";
        }


    }

    public enum _controlTypeEnumGrid
    {
        /// <summary>
        /// ArContactorGrid : ผู้ติดต่อ
        /// </summary>
        ArContactorGrid,
        /// <summary>
        /// ArItemGrid : สินค้าลูกค้าต่อลูกค้า
        /// </summary>
        ArItemGrid,
        M_congenital_disease,
        /// <summary>
        /// ArItemGrid : ประวัติแพ้ยา
        /// </summary>
        M_allergic,
    }

    public class _grid_ar_point_movement : MyLib._myGrid
    {
        public _grid_ar_point_movement()
        {
            string __formatNumberAmount = _g.g._getFormatNumberStr(3);

            this._table_name = _g.d.pos_resource._table;
            this._width_by_persent = true;
            this._addColumn(_g.d.pos_resource._doc_date, 4, 20, 20);
            this._addColumn(_g.d.pos_resource._doc_time, 1, 10, 10);
            this._addColumn(_g.d.pos_resource._doc_no, 1, 20, 20);
            //this._addColumn(_g.d.pos_resource._sale_code, 1, 10, 10);
            //this._addColumn(_g.d.pos_resource._pos_id, 1, 20, 20);
            this._addColumn(_g.d.pos_resource._doc_amount, 3, 10, 8, false, false, false, false, __formatNumberAmount);
            this._addColumn(_g.d.pos_resource._point_add, 3, 10, 8, false, false, false, false, __formatNumberAmount);
            this._addColumn(_g.d.pos_resource._point_use, 3, 10, 8, false, false, false, false, __formatNumberAmount);
            this._addColumn(_g.d.pos_resource._point_balance, 3, 10, 8, false, false, false, false, __formatNumberAmount);

            this._calcPersentWidthToScatter();
        }
    }

}
