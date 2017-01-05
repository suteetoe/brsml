using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace DTSClientDownload
{
    public partial class _transDownloadControl : UserControl
    {
        DataTable _wareHouseDataTable;
        DataTable _shelfDataTable;

        private _screenDownloadEnum _type = _screenDownloadEnum.None;
        public _screenDownloadEnum _screen_type
        {
            get
            {
                return this._type;
            }
            set
            {
                this._type = value;
                this._serverDownloadGrid._screen_type = value;
                this._clientDownloadGrid._screen_type = value;
                this._transDetailGrid._screen_type = value;

                if (this._type == _screenDownloadEnum.PO)
                {
                    this._serverDownloadLabel.Text = "ใบสั่งซื้อ SCG";
                    this._clientDownloadLabel.Text = "ใบสั่งซื้อ CHAMP";
                }
                else
                {
                    this._serverDownloadLabel.Text = "ใบสั่งขาย/สั่งจอง SCG";
                    this._clientDownloadLabel.Text = "ใบสั่งขาย/สั่งจอง CHAMP";
                }
            }
        }

        void _checkEnableSaveButton()
        {
            if (this._clientDownloadGrid._rowData.Count > 0)
            {
                // เอา item มาเช็คด้วย
                //string __docWhere = this._clientDownloadGrid._createWhere("docno");
                StringBuilder __docnoList = new StringBuilder();
                for (int __i = 0; __i < this._clientDownloadGrid._rowData.Count; __i++)
                {
                    //if (this._clientDownloadGrid._cellGet(__i, 0).ToString().Equals("1"))
                    //{
                    if (__docnoList.Length > 0)
                    {
                        __docnoList.Append(",");
                    }

                    __docnoList.Append("\'" + this._clientDownloadGrid._cellGet(__i, "docno").ToString() + "\'");
                    //}
                }

                string __queryCheck = "";
                string __queryCheckDupDocNo = "";
                if (this._type == _screenDownloadEnum.PO)
                {
                    __queryCheck = " select COUNT(*) as xcount from " + _g.DataClient.dts_bcpurchaseordersub._table + " where not exists (select * from bcitem where bcitem.Code = " + _g.DataClient.dts_bcpurchaseordersub._table + "." + _g.DataClient.dts_bcpurchaseordersub._itemcode + " ) and docno in (" + __docnoList.ToString() + ") ";
                    __queryCheckDupDocNo = "select count(*) as xcount from bcpurchaseordersub where docno in (" + __docnoList.ToString() +")";
                }
                else if (this._type == _screenDownloadEnum.SO)
                {
                    __queryCheck = " select COUNT(*) as xcount from " + _g.DataClient.dts_bcsaleordersub._table + " where not exists (select * from bcitem where bcitem.Code = " + _g.DataClient.dts_bcsaleordersub._table + "." + _g.DataClient.dts_bcsaleordersub._itemcode + " ) and docno in (" + __docnoList.ToString() + ") ";
                    __queryCheckDupDocNo = "select count(*) as xcount from bcsaleordersub where docno in (" + __docnoList.ToString() + ")";
                }

                List<string> __queryCheckList = new List<string>();
                __queryCheckList.Add(__queryCheck);
                __queryCheckList.Add(__queryCheckDupDocNo);

                _clientFrameWork __frameWork = new _clientFrameWork();

                ArrayList __result = __frameWork._getDataList(__queryCheckList);

                //DataSet __result = __frameWork._query(__queryCheck);
                if (__result.Count == 2)
                {
                    if (((DataSet)__result[0]).Tables.Count > 0 && ((DataSet)__result[0]).Tables[0].Rows.Count > 0)
                    {
                        if (MyLib._myGlobal._decimalPhase(((DataSet)__result[0]).Tables[0].Rows[0][0].ToString()) == 0)
                        {
                            if (MyLib._myGlobal._decimalPhase(((DataSet)__result[1]).Tables[0].Rows[0][0].ToString()) == 0)
                            {
                                this._saveButton.Enabled = true;
                            }
                            else
                            {
                                this._saveButton.Enabled = false;
                            }
                        }
                        else
                        {
                            this._saveButton.Enabled = false;
                        }
                    }
                }
                //this._saveButton.Enabled = true;
            }
            else
            {
                this._saveButton.Enabled = false;
            }
        }

        public _transDownloadControl()
        {
            InitializeComponent();

            _loadWhLocation();

            this.Load += new EventHandler(_transDownloadControl_Load);
            this._showAllRecord.CheckStateChanged += new EventHandler(_showAllRecord_CheckStateChanged);
            this._serverDownloadGrid._alterCellUpdate += new MyLib.AfterCellUpdateEventHandler(_serverDownloadGrid__alterCellUpdate);
            this._clientDownloadGrid._afterSelectRow += new MyLib.AfterSelectRowEventHandler(_clientDownloadGrid__afterSelectRow);
            this._clientDownloadGrid._mouseClick += new MyLib.MouseClickHandler(_clientDownloadGrid__mouseClick);
            this._clientDownloadGrid._alterCellUpdate += new MyLib.AfterCellUpdateEventHandler(_clientDownloadGrid__alterCellUpdate);
            this._transDetailGrid._cellComboBoxItem += new MyLib.CellComboBoxItemEventHandler(_transDetailGrid__cellComboBoxItem);
            this._transDetailGrid._cellComboBoxGet += new MyLib.CellComboBoxItemGetDisplay(_transDetailGrid__cellComboBoxGet);
            this._transDetailGrid._alterCellUpdate += new MyLib.AfterCellUpdateEventHandler(_transDetailGrid__alterCellUpdate);
            this._checkEnableSaveButton();

            this._importButton.Enabled = false;
            this._exportButton.Enabled = false;

        }

        void _clientDownloadGrid__alterCellUpdate(object sender, int row, int column)
        {
            _checkEnableExportButton();
        }

        void _clientDownloadGrid__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            if (e._row < this._clientDownloadGrid._rowData.Count)
            {
                _loadTransDetail(e._row);
            }
        }

        void _showAllRecord_CheckStateChanged(object sender, EventArgs e)
        {
            this._loadData();
        }

        string _transDetailGrid__cellComboBoxGet(object sender, int row, int column, string columnName, int select)
        {
            if (select == -1)
                return "";

            if (columnName.Equals("wh_code"))
            {
                if (_wareHouseDataTable == null || _wareHouseDataTable.Rows.Count == 0)
                    return "";

                if (select < _wareHouseDataTable.Rows.Count)
                {
                    string __wh_select = _wareHouseDataTable.Rows[select]["code"].ToString() + "~" + _wareHouseDataTable.Rows[select]["name"].ToString();
                    this._transDetailGrid._cellUpdate(row, "wh_name", __wh_select, true);
                    return __wh_select;
                }

            }
            else if (columnName.Equals("lc_code"))
            {
                if (_shelfDataTable == null || _shelfDataTable.Rows.Count == 0)
                    return "";

                DataRow[] __datarow = _shelfDataTable.Select("whcode=\'" + _global._getValue(this._transDetailGrid._cellGet(row, "wh_name").ToString()) + "\'");

                if (__datarow != null && select < __datarow.Length)
                {
                    string __lc_select = __datarow[select]["code"].ToString() + "~" + __datarow[select]["name"].ToString();
                    this._transDetailGrid._cellUpdate(row, "location_name", __lc_select, true);
                    return __lc_select;
                }

            }
            return "";
        }

        object[] _transDetailGrid__cellComboBoxItem(object sender, int row, int column)
        {
            object[] __result = null;
            int __columnWhCode = this._transDetailGrid._findColumnByName("wh_code");
            int __columnLocationCode = this._transDetailGrid._findColumnByName("lc_code");

            _clientFrameWork __clientFramework = new _clientFrameWork();
            string __itemCode = this._transDetailGrid._cellGet(row, "item_code").ToString();
            
            if (column.Equals(__columnWhCode))
            {
                DataSet __whList = __clientFramework._query("select distinct WHCode, (select top 1 Name from BCWarehouse where BCWarehouse.Code = BCItemWarehouse.WHCode) as WHName from BCItemWarehouse WHERE itemcode = '" + __itemCode + "'");

                List<string> __dataList = new List<string>();
                for (int __i = 0; __i < __whList.Tables[0].Rows.Count; __i++)
                {
                    __dataList.Add(__whList.Tables[0].Rows[__i]["WHCode"].ToString() + "~" + __whList.Tables[0].Rows[__i]["WHName"].ToString());
                }
                return __dataList.ToArray();
                /*
                List<string> __dataList = new List<string>();
                for (int __i = 0; __i < _wareHouseDataTable.Rows.Count; __i++)
                {
                    __dataList.Add(_wareHouseDataTable.Rows[__i]["code"].ToString() + "~" + _wareHouseDataTable.Rows[__i]["name"].ToString());
                }

                return __dataList.ToArray();
                 * */
            }
            else if (column.Equals(__columnLocationCode))
            {
                string __whCode = _global._getValue(this._transDetailGrid._cellGet(row, "wh_name").ToString());

                DataSet __shelfList = __clientFramework._query("select distinct ShelfCode, (select top 1 BCShelf.Name from BCShelf where BCShelf.WHCode = BCItemWarehouse.WHCode and BCShelf.Code = BCItemWarehouse.ShelfCode) as ShelfName from BCItemWarehouse WHERE itemcode = '" + __itemCode + "' and WHCode = '" + __whCode + "'");
                
                List<string> __dataList = new List<string>();
                for (int __i = 0; __i < __shelfList.Tables[0].Rows.Count; __i++)
                {
                    __dataList.Add(__shelfList.Tables[0].Rows[__i]["ShelfCode"].ToString() + "~" + __shelfList.Tables[0].Rows[__i]["ShelfName"].ToString());
                }
                return __dataList.ToArray();

                /*
                DataRow[] __row = _shelfDataTable.Select("whcode=\'" + __whCode + "\'");

                List<string> __dataList = new List<string>();
                for (int __i = 0; __i < __row.Length; __i++)
                {
                    __dataList.Add(__row[__i]["code"].ToString() + "~" + __row[__i]["name"].ToString());
                }

                return __dataList.ToArray();
                 * */
            }
            
            return __result;
        }

        void _transDetailGrid__alterCellUpdate(object sender, int row, int column)
        {
            // on row update
            int __columnWarehouse = this._transDetailGrid._findColumnByName("wh_name");
            int __columnLocation = this._transDetailGrid._findColumnByName("location_name");

            if (column == __columnWarehouse || column == __columnLocation)
            {
                // update ค่ากลับไปที่ตัวหลัก
                //
                _trans_detail_list __list = new _trans_detail_list();
                for (int __i = 0; __i < this._transDetailGrid._rowData.Count; __i++)
                {
                    _trans_detail __detail = new _trans_detail();
                    __detail._itemCode = this._transDetailGrid._cellGet(__i, "item_code").ToString();
                    __detail._itemName = this._transDetailGrid._cellGet(__i, "item_name").ToString();
                    __detail._unitCode = this._transDetailGrid._cellGet(__i, "unit_code").ToString();
                    __detail._qty = (decimal)this._transDetailGrid._cellGet(__i, "qty");
                    __detail._price = (decimal)this._transDetailGrid._cellGet(__i, "price");
                    __detail._amount = (decimal)this._transDetailGrid._cellGet(__i, "amount");
                    __detail._netamount = (decimal)this._transDetailGrid._cellGet(__i, "netamount");
                    __detail._taxrate = (decimal)this._transDetailGrid._cellGet(__i, "taxrate");
                    __detail._rate1 = (decimal)this._transDetailGrid._cellGet(__i, "rate1");
                    __detail._rate2 = (decimal)this._transDetailGrid._cellGet(__i, "rate2");

                    __detail._wh_code = _global._getValue(this._transDetailGrid._cellGet(__i, "wh_name").ToString());
                    __detail._shelf_code = _global._getValue(this._transDetailGrid._cellGet(__i, "location_name").ToString());


                    _trans_detail __relateItem = (_trans_detail)this._transDetailGrid._cellGet(__i, "detailclass");

                    __detail.groupcode = __relateItem.groupcode;
                    __detail.typecode = __relateItem.typecode;
                    __detail.categorycode = __relateItem.categorycode;
                    __detail.formatcode = __relateItem.formatcode;

                    if (this._type == _screenDownloadEnum.SO)
                    {
                        __detail._OriginalDate = __relateItem._OriginalDate;
                    }

                    __list._detail.Add(__detail);
                }

                //Console.WriteLine(__list._detail.Count);
                this._clientDownloadGrid._cellUpdate(this._clientDownloadGrid._selectRow, "trans_detail", __list, false);
            }
        }

        void _clientDownloadGrid__afterSelectRow(object sender, int row)
        {
            _loadTransDetail(row);
        }

        void _loadTransDetail(int row)
        {
            _trans_detail_list __list = (_trans_detail_list)this._clientDownloadGrid._cellGet(row, "trans_detail");
            this._transDetailGrid._clear();

            if (__list == null)
                return;

            _clientFrameWork __clientFrameWork = new _clientFrameWork();
            for (int __i = 0; __i < __list._detail.Count; __i++)
            {
                //_gridTransDetailImport _transDetailGrid  = new _gridTransDetailImport();

                int __addr = this._transDetailGrid._addRow();
                this._transDetailGrid._cellUpdate(__addr, "item_code", __list._detail[__i]._itemCode, false);
                this._transDetailGrid._cellUpdate(__addr, "item_name", __list._detail[__i]._itemName, false);
                this._transDetailGrid._cellUpdate(__addr, "unit_code", __list._detail[__i]._unitCode, false);
                this._transDetailGrid._cellUpdate(__addr, "qty", __list._detail[__i]._qty, false);
                this._transDetailGrid._cellUpdate(__addr, "price", __list._detail[__i]._price, false);
                this._transDetailGrid._cellUpdate(__addr, "amount", __list._detail[__i]._amount, false);
                this._transDetailGrid._cellUpdate(__addr, "netamount", __list._detail[__i]._netamount, false);
                this._transDetailGrid._cellUpdate(__addr, "taxrate", __list._detail[__i]._taxrate, false);
                this._transDetailGrid._cellUpdate(__addr, "rate1", __list._detail[__i]._rate1, false);
                this._transDetailGrid._cellUpdate(__addr, "rate2", __list._detail[__i]._rate2, false);

                //// get BCItemWarehouse where itemcode = ''
                //DataSet __warehouse = __clientFrameWork._query(" select WHCode, ShelfCode, (select top 1 Name from BCWarehouse where BCWarehouse.Code = BCItemWarehouse.WHCode) as WHName, (select top 1 BCShelf.Name from BCShelf where BCShelf.WHCode = BCItemWarehouse.WHCode and BCShelf.Code = BCItemWarehouse.ShelfCode) as ShelfName from BCItemWarehouse WHERE itemcode = '" + __list._detail[__i]._itemCode + "'");
                //if (__warehouse.Tables.Count > 0 && __warehouse.Tables[0].Rows.Count > 0)
                //{
                //    // set combobox for selected warehouse and shelf
                //}

                if (__list._detail[__i]._wh_code != "")
                    this._transDetailGrid._cellUpdate(__addr, "wh_code", _getWarehouseSelectIndex(__list._detail[__i]._wh_code), false);

                if (__list._detail[__i]._shelf_code != "")
                    this._transDetailGrid._cellUpdate(__addr, "lc_code", _getShelfSelectIndex(__list._detail[__i]._shelf_code, __list._detail[__i]._wh_code), false);

                // update detail class
                this._transDetailGrid._cellUpdate(__addr, "detailclass", __list._detail[__i], false);

            }
        }

        int _getWarehouseSelectIndex(string whCode)
        {
            for (int __i = 0; __i < _wareHouseDataTable.Rows.Count; __i++)
            {
                if (_wareHouseDataTable.Rows[__i]["code"].ToString().Equals(whCode))
                {
                    return __i;
                }
            }
            return -1;
        }

        int _getShelfSelectIndex(string shelfCode, string whCode)
        {
            DataRow[] __dataRow = _shelfDataTable.Select("whcode = \'" + whCode + "\'");

            for (int __i = 0; __i < __dataRow.Length; __i++)
            {
                if (__dataRow[__i]["code"].ToString().Equals(shelfCode))
                {
                    return __i;
                }
            }
            return -1;
        }

        void _loadWhLocation()
        {
            _clientFrameWork __myFrameWork = new _clientFrameWork();

            List<string> __query = new List<string>();
            __query.Add("select code,name from bcwarehouse");
            __query.Add("select whcode, (select bcwarehouse.name from bcwarehouse where bcwarehouse.code = bcshelf.whcode) as whname, code, name from bcshelf");

            ArrayList __result = __myFrameWork._getDataList(__query);
            if (__result.Count > 0)
            {
                _wareHouseDataTable = ((DataSet)__result[0]).Tables[0];
                _shelfDataTable = ((DataSet)__result[1]).Tables[0];
            }
        }

        void _checkEnableImportButton()
        {
            bool __canAdd = false;
            for (int __i = 0; __i < this._serverDownloadGrid._rowData.Count; __i++)
            {
                if (this._serverDownloadGrid._cellGet(__i, 0).ToString().Equals("1"))
                {
                    __canAdd = true;
                    break;
                }
            }

            this._importButton.Enabled = __canAdd;

        }

        void _checkEnableExportButton()
        {
            Boolean __pass = false;
            for (int __i = 0; __i < this._clientDownloadGrid._rowData.Count; __i++)
            {
                if (this._clientDownloadGrid._cellGet(__i, 0).ToString().Equals("1"))
                {
                    __pass = true;
                    break;
                }
            }

            this._exportButton.Enabled = __pass;
        }

        void _serverDownloadGrid__alterCellUpdate(object sender, int row, int column)
        {
            _checkEnableImportButton();
            //List<int> __selectIndex = new List<int>();

            //for (int __i = 0; __i < this._serverDownloadGrid._rowData.Count; __i++)
            //{
            //    if (this._serverDownloadGrid._cellGet(__i, 0).ToString().Equals("1"))
            //    {
            //        _addSelectItem(__i);
            //    }
            //    else
            //    {
            //        _removeSelectItem(__i);
            //    }
            //}

            // for (int __i = 0; __i < this._gridItemClient1._rowData.Count; __i++)

            /*
            this._clientDownloadGrid._clear();
            switch (this._screen_type)
            {
                case _screenDownloadEnum.PO:

                    for (int __rowIndex = 0; __rowIndex < __selectIndex.Count; __rowIndex++)
                    {
                        int __i = __selectIndex[__rowIndex];
                        string __podocno = this._serverDownloadGrid._cellGet(__i, "docno").ToString();
                        string __podocdate = this._serverDownloadGrid._cellGet(__i, "docdate").ToString();
                        string __poMyDescription = "";
                        int __itemUnitType = 0;

                        int addr = this._clientDownloadGrid._addRow();
                        this._clientDownloadGrid._cellUpdate(addr, 0, 1, false);
                        this._clientDownloadGrid._cellUpdate(addr, "docno", __podocno, false);
                        this._clientDownloadGrid._cellUpdate(addr, "docdate", MyLib._myGlobal._convertDateFromQuery(__podocdate), false);
                        this._clientDownloadGrid._cellUpdate(addr, "mydescription", __poMyDescription, false);

                    }

                    break;
                case _screenDownloadEnum.SO:
                    break;
            }
            */
        }

        void _addSelectItem(int row)
        {
            _clientFrameWork __myFrameWork = new _clientFrameWork();
            Boolean __found = false;

            string __podocno = this._serverDownloadGrid._cellGet(row, "docno").ToString();
            string __podocdate = this._serverDownloadGrid._cellGet(row, "docdate").ToString();

            string __ownerReceive = "";
            if (this._screen_type == _screenDownloadEnum.SO)
            {
                __ownerReceive = this._serverDownloadGrid._cellGet(row, "ownreceive").ToString();
            }

            for (int __i = 0; __i < this._clientDownloadGrid._rowData.Count; __i++)
            {
                string __tmpDocNo = this._clientDownloadGrid._cellGet(__i, "docno").ToString();
                if (__tmpDocNo.Equals(__podocno))
                {
                    __found = true;
                    break;
                }
            }

            if (__found == false)
            {
                List<string> __query = new List<string>();

                switch (this._screen_type)
                {
                    case _screenDownloadEnum.PO:
                        __query.Add("select *, "
                            + " ( select creditday from bcap where bcap.code = " + _g.DataClient.dts_bcpurchaseorder._table + "." + _g.DataClient.dts_bcpurchaseorder._apcode + " ) as " + _g.DataClient.dts_bcpurchaseorder._creditday
                            + " from " + _g.DataClient.dts_bcpurchaseorder._table + " where " + _g.DataClient.dts_bcpurchaseorder._docno + " = \'" + __podocno + "\'");
                        __query.Add("select * , "
                            + " COALESCE((select bcitem.name1 from bcitem where bcitem.code = " + _g.DataClient.dts_bcpurchaseordersub._table + "." + _g.DataClient.dts_bcpurchaseordersub._itemcode + ") , '') as " + _g.DataClient.dts_bcpurchaseordersub._itemname + ","
                            + " (select " + _g.DataClient.dts_bcpurchaseorder._taxrate + " from " + _g.DataClient.dts_bcpurchaseorder._table + " where " + _g.DataClient.dts_bcpurchaseorder._table + "." + _g.DataClient.dts_bcpurchaseorder._docno + " = " + _g.DataClient.dts_bcpurchaseordersub._table + "." + _g.DataClient.dts_bcpurchaseordersub._docno + ") as " + _g.DataClient.dts_bcpurchaseordersub._taxrate + ","
                            + " COALESCE((select Rate1 from BCStkPacking where ItemCode = " + _g.DataClient.dts_bcpurchaseordersub._table + "." + _g.DataClient.dts_bcpurchaseordersub._itemcode + " and UnitCode = " + _g.DataClient.dts_bcpurchaseordersub._table + "." + _g.DataClient.dts_bcpurchaseordersub._unitcode + " ), 1) as " + _g.DataClient.dts_bcpurchaseordersub._rate1 + ", "
                            + " COALESCE((select Rate2 from BCStkPacking where ItemCode = " + _g.DataClient.dts_bcpurchaseordersub._table + "." + _g.DataClient.dts_bcpurchaseordersub._itemcode + " and UnitCode = " + _g.DataClient.dts_bcpurchaseordersub._table + "." + _g.DataClient.dts_bcpurchaseordersub._unitcode + " ), 1) as " + _g.DataClient.dts_bcpurchaseordersub._rate2 + ", "
                            + " (select bcitem.groupcode from bcitem  where bcitem.Code = " + _g.DataClient.dts_bcpurchaseordersub._table + "." + _g.DataClient.dts_bcpurchaseordersub._itemcode + ") as groupcode, "
                            + " (select bcitem.typecode from bcitem  where bcitem.Code = " + _g.DataClient.dts_bcpurchaseordersub._table + "." + _g.DataClient.dts_bcpurchaseordersub._itemcode + ") as typecode, "
                            + " (select bcitem.categorycode from bcitem  where bcitem.Code = " + _g.DataClient.dts_bcpurchaseordersub._table + "." + _g.DataClient.dts_bcpurchaseordersub._itemcode + ") as categorycode, "
                            + " (select bcitem.formatcode from bcitem  where bcitem.Code = " + _g.DataClient.dts_bcpurchaseordersub._table + "." + _g.DataClient.dts_bcpurchaseordersub._itemcode + ") as formatcode ,"
                            + " (select bcitem.DefSaleWHCode from bcitem  where bcitem.Code = " + _g.DataClient.dts_bcpurchaseordersub._table + "." + _g.DataClient.dts_bcpurchaseordersub._itemcode + ") as DefSaleWHCode ,"
                            + " (select bcitem.DefSaleShelf from bcitem  where bcitem.Code = " + _g.DataClient.dts_bcpurchaseordersub._table + "." + _g.DataClient.dts_bcpurchaseordersub._itemcode + ") as DefSaleShelf ,"
                            + " (select bcitem.DefBuyWHCode from bcitem  where bcitem.Code = " + _g.DataClient.dts_bcpurchaseordersub._table + "." + _g.DataClient.dts_bcpurchaseordersub._itemcode + ") as DefBuyWHCode ,"
                            + " (select bcitem.DefBuyShelf from bcitem  where bcitem.Code = " + _g.DataClient.dts_bcpurchaseordersub._table + "." + _g.DataClient.dts_bcpurchaseordersub._itemcode + ") as DefBuyShelf "
                            + " from " + _g.DataClient.dts_bcpurchaseordersub._table + " where " + _g.DataClient.dts_bcpurchaseordersub._docno + " = \'" + __podocno + "\'");

                        break;
                    case _screenDownloadEnum.SO:
                        __query.Add("select * from " + _g.DataClient.dts_bcsaleorder._table + " where " + _g.DataClient.dts_bcsaleorder._docno + " = \'" + __podocno + "\'");
                        __query.Add("select * ,"
                            + " COALESCE((select bcitem.name1 from bcitem where bcitem.code = " + _g.DataClient.dts_bcsaleordersub._table + "." + _g.DataClient.dts_bcsaleordersub._itemcode + ") , '') as " + _g.DataClient.dts_bcsaleordersub._itemname + ","
                            + " (select " + _g.DataClient.dts_bcsaleorder._taxrate + " from " + _g.DataClient.dts_bcsaleorder._table + " where " + _g.DataClient.dts_bcsaleorder._table + "." + _g.DataClient.dts_bcsaleorder._docno + " = " + _g.DataClient.dts_bcsaleordersub._table + "." + _g.DataClient.dts_bcsaleordersub._docno + ") as " + _g.DataClient.dts_bcsaleordersub._taxrate + ","
                            + " COALESCE((select Rate1 from BCStkPacking where ItemCode = " + _g.DataClient.dts_bcsaleordersub._table + "." + _g.DataClient.dts_bcsaleordersub._itemcode + " and UnitCode = " + _g.DataClient.dts_bcsaleordersub._table + "." + _g.DataClient.dts_bcsaleordersub._unitcode + " ), 1) as rate1, "
                            + " COALESCE((select Rate2 from BCStkPacking where ItemCode = " + _g.DataClient.dts_bcsaleordersub._table + "." + _g.DataClient.dts_bcsaleordersub._itemcode + " and UnitCode = " + _g.DataClient.dts_bcsaleordersub._table + "." + _g.DataClient.dts_bcsaleordersub._unitcode + " ), 1) as rate2,  "
                            + " (select bcitem.groupcode from bcitem  where bcitem.Code = " + _g.DataClient.dts_bcsaleordersub._table + "." + _g.DataClient.dts_bcsaleordersub._itemcode + ") as groupcode, "
                            + " (select bcitem.typecode from bcitem  where bcitem.Code = " + _g.DataClient.dts_bcsaleordersub._table + "." + _g.DataClient.dts_bcsaleordersub._itemcode + ") as typecode, "
                            + " (select bcitem.categorycode from bcitem  where bcitem.Code = " + _g.DataClient.dts_bcsaleordersub._table + "." + _g.DataClient.dts_bcsaleordersub._itemcode + ") as categorycode, "
                            + " (select bcitem.formatcode from bcitem  where bcitem.Code = " + _g.DataClient.dts_bcsaleordersub._table + "." + _g.DataClient.dts_bcsaleordersub._itemcode + ") as formatcode ,"
                            + " (select bcitem.DefSaleWHCode from bcitem  where bcitem.Code = " + _g.DataClient.dts_bcsaleordersub._table + "." + _g.DataClient.dts_bcsaleordersub._itemcode + ") as DefSaleWHCode ,"
                            + " (select bcitem.DefSaleShelf from bcitem  where bcitem.Code = " + _g.DataClient.dts_bcsaleordersub._table + "." + _g.DataClient.dts_bcsaleordersub._itemcode + ") as DefSaleShelf ,"
                            + " (select bcitem.DefBuyWHCode from bcitem  where bcitem.Code = " + _g.DataClient.dts_bcsaleordersub._table + "." + _g.DataClient.dts_bcsaleordersub._itemcode + ") as DefBuyWHCode ,"
                            + " (select bcitem.DefBuyShelf from bcitem  where bcitem.Code = " + _g.DataClient.dts_bcsaleordersub._table + "." + _g.DataClient.dts_bcsaleordersub._itemcode + ") as DefBuyShelf "
                            + " from " + _g.DataClient.dts_bcsaleordersub._table + " where " + _g.DataClient.dts_bcsaleordersub._docno + " = \'" + __podocno + "\'");

                        break;
                }

                ArrayList __result = __myFrameWork._getDataList(__query);
                if (__result.Count > 0)
                {
                    // อย่าลืม เอา คลังและที่เก็บเริ่มต้นมาด้วย
                    DataTable __ds1 = ((DataSet)__result[1]).Tables[0];
                    DataTable __ds2 = ((DataSet)__result[0]).Tables[0];

                    int addr = this._clientDownloadGrid._addRow();
                    string __poMyDescription = "";

                    if (this._screen_type == _screenDownloadEnum.SO)
                    {
                        __poMyDescription = __ds2.Rows[0]["mydescription"].ToString();
                    }


                    this._clientDownloadGrid._cellUpdate(addr, 0, 0, false);
                    this._clientDownloadGrid._cellUpdate(addr, "docno", __podocno, false);
                    this._clientDownloadGrid._cellUpdate(addr, "docdate", MyLib._myGlobal._convertDateFromQuery(__podocdate), false);
                    this._clientDownloadGrid._cellUpdate(addr, "ownreceive", __ownerReceive, false);
                    this._clientDownloadGrid._cellUpdate(addr, "mydescription", __poMyDescription, false);



                    _trans __transItem = new _trans();
                    __transItem._docno = __ds2.Rows[0]["docno"].ToString();
                    __transItem._docdate = MyLib._myGlobal._convertDateFromQuery(__ds2.Rows[0]["docdate"].ToString());
                    __transItem._taxtype = (int)MyLib._myGlobal._decimalPhase(__ds2.Rows[0]["taxtype"].ToString());
                    __transItem._taxRate = (int)MyLib._myGlobal._decimalPhase(__ds2.Rows[0]["taxrate"].ToString());

                    if (this._screen_type == _screenDownloadEnum.PO)
                    {
                        __transItem._aparcode = __ds2.Rows[0]["apcode"].ToString();
                        __transItem._leadtime = (int)MyLib._myGlobal._decimalPhase(__ds2.Rows[0][_g.DataClient.dts_bcpurchaseorder._leadtime].ToString());
                        __transItem._creditday = (int)MyLib._myGlobal._decimalPhase(__ds2.Rows[0][_g.DataClient.dts_bcpurchaseorder._creditday].ToString());
                    }
                    else
                    {
                        __transItem._aparcode = __ds2.Rows[0][_g.DataClient.dts_bcsaleorder._arcode].ToString();
                        __transItem._deliveryDay = (int)MyLib._myGlobal._decimalPhase(__ds2.Rows[0][_g.DataClient.dts_bcsaleorder._deliveryday].ToString());
                        __transItem._isConditionSend = (int)MyLib._myGlobal._decimalPhase(__ds2.Rows[0][_g.DataClient.dts_bcsaleorder._isconditionsend].ToString());
                        __transItem._billType = (int)MyLib._myGlobal._decimalPhase(__ds2.Rows[0][_g.DataClient.dts_bcsaleorder._billtype].ToString());
                        __transItem._ownreceive = __ownerReceive;
                        __transItem._myDescription = __poMyDescription;
                    }

                    __transItem._sumofitemamount = MyLib._myGlobal._decimalPhase(__ds2.Rows[0]["sumofitemamount"].ToString());
                    //__transItem._afterdiscount = MyLib._myGlobal._decimalPhase(__ds2.Rows[0]["afterdiscount"].ToString());
                    //__transItem._beforetaxamount = MyLib._myGlobal._decimalPhase(__ds2.Rows[0]["beforetaxamount"].ToString());
                    __transItem._taxamount = MyLib._myGlobal._decimalPhase(__ds2.Rows[0]["taxamount"].ToString());
                    __transItem._totalamount = MyLib._myGlobal._decimalPhase(__ds2.Rows[0]["totalamount"].ToString());
                    //__transItem._excepttaxamount = MyLib._myGlobal._decimalPhase(__ds2.Rows[0]["excepttaxamount"].ToString());
                    //__transItem._zerotaxamount = MyLib._myGlobal._decimalPhase(__ds2.Rows[0]["zerotaxamount"].ToString());
                    __transItem._netamount = MyLib._myGlobal._decimalPhase(__ds2.Rows[0]["netamount"].ToString());

                    this._clientDownloadGrid._cellUpdate(addr, "transitem", __transItem, false);

                    _trans_detail_list __detailList = new _trans_detail_list();
                    for (int __row = 0; __row < __ds1.Rows.Count; __row++)
                    {
                        _trans_detail __detail = new _trans_detail();
                        __detail._itemCode = __ds1.Rows[__row]["itemcode"].ToString();
                        __detail._itemName = __ds1.Rows[__row]["itemname"].ToString();
                        __detail._unitCode = __ds1.Rows[__row]["unitcode"].ToString();
                        __detail._qty = MyLib._myGlobal._decimalPhase(__ds1.Rows[__row]["qty"].ToString());
                        __detail._price = MyLib._myGlobal._decimalPhase(__ds1.Rows[__row]["price"].ToString());
                        __detail._amount = MyLib._myGlobal._decimalPhase(__ds1.Rows[__row]["amount"].ToString());
                        __detail._netamount = MyLib._myGlobal._decimalPhase(__ds1.Rows[__row]["netamount"].ToString());
                        __detail._taxrate = MyLib._myGlobal._decimalPhase(__ds1.Rows[__row]["taxrate"].ToString());
                        __detail._rate1 = MyLib._myGlobal._decimalPhase(__ds1.Rows[__row]["rate1"].ToString());
                        __detail._rate2 = MyLib._myGlobal._decimalPhase(__ds1.Rows[__row]["rate2"].ToString());

                        __detail.groupcode = __ds1.Rows[__row]["groupcode"].ToString();
                        __detail.typecode = __ds1.Rows[__row]["typecode"].ToString();
                        __detail.categorycode = __ds1.Rows[__row]["categorycode"].ToString();
                        __detail.formatcode = __ds1.Rows[__row]["formatcode"].ToString();

                        if (this._type == _screenDownloadEnum.SO)
                        {
                            __detail._OriginalDate = MyLib._myGlobal._convertDateFromQuery(__ds1.Rows[0][_g.DataClient.dts_bcsaleordersub._originaldate].ToString());
                        }

                        __detail._DefBuyWHCode = __ds1.Rows[__row]["DefBuyWHCode"].ToString();
                        __detail._DefBuyShelf = __ds1.Rows[__row]["DefBuyShelf"].ToString();
                        __detail._DefSaleWHCode = __ds1.Rows[__row]["DefSaleWHCode"].ToString();
                        __detail._DefSaleShelf = __ds1.Rows[__row]["DefSaleShelf"].ToString();

                        if (this._type == _screenDownloadEnum.SO)
                        {
                            __detail._wh_code = __detail._DefSaleWHCode;
                            __detail._shelf_code = __detail._DefSaleShelf ;
                        }
                        else if (this._type == _screenDownloadEnum.PO)
                        {
                            __detail._wh_code = __detail._DefBuyWHCode;
                            __detail._shelf_code = __detail._DefBuyShelf ;
                        }

                        __detailList._detail.Add(__detail);

                    }
                    this._clientDownloadGrid._cellUpdate(addr, "trans_detail", __detailList, false);
                }
            }

        }

        void _removeSelectItem(int row)
        {
            int __line = -1;

            string __podocno = this._serverDownloadGrid._cellGet(row, "docno").ToString();

            for (int __i = 0; __i < this._serverDownloadGrid._rowData.Count; __i++)
            {
                string __docNo = this._clientDownloadGrid._cellGet(__i, "docno").ToString();

                if (__podocno.Equals(__docNo))
                {
                    __line = __i;
                    break;
                }
            }

            // remove at line 
            if (__line != -1)
            {
                this._clientDownloadGrid._rowData.RemoveAt(__line);
                this._clientDownloadGrid.Refresh();
            }
        }

        void _transDownloadControl_Load(object sender, EventArgs e)
        {
            _loadData();
        }

        void _loadData()
        {
            _clientFrameWork __FrameWork = new _clientFrameWork();
            string __query = "";
            string __extraWhere = "";

            switch (this._screen_type)
            {
                case _screenDownloadEnum.PO:

                    if (this._showAllRecord.Checked == true)
                    {
                        __extraWhere = "";
                    }
                    else
                    {
                        __extraWhere = " where docno not in (select docno from bcpurchaseorder) ";
                    }
                    //__query = "select docno, (convert(varchar, docdate, 120)) as docdate from dts_bcpurchaseorder  order by docno,docdate ";
                    __query = "select docno, (convert(varchar, docdate, 120)) as docdate from dts_bcpurchaseorder " + __extraWhere + " order by docno,docdate ";
                    break;
                case _screenDownloadEnum.SO:

                    if (this._showAllRecord.Checked == true)
                    {
                        __extraWhere = "";
                    }
                    else
                    {
                        __extraWhere = " where docno not in (select docno from bcsaleorder) ";
                    }
                    //__query = "select docno, (convert(varchar, docdate, 120)) as docdate, ownreceive from dts_bcsaleorder order by docno,docdate ";
                    __query = "select docno, (convert(varchar, docdate, 120)) as docdate, ownreceive, mydescription from dts_bcsaleorder " + __extraWhere + " order by docno,docdate ";
                    break;
            }

            DataSet __result = __FrameWork._query(__query);

            if (__result.Tables.Count > 0)
            {
                this._serverDownloadGrid._loadFromDataTable(__result.Tables[0]);
            }
        }

        private void _saveButton_Click(object sender, EventArgs e)
        {
            _saveData();
        }

        void _saveData()
        {
            // check mode po   
            if (MessageBox.Show("ต้องการบันทึกข้อมูลหรือไม่", DTSClientDownload._global._champMessageCaption, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
            {
                return;
            }

            // เช็ค tax และ ยอด vat

            _clientFrameWork __myFrameWork = new _clientFrameWork();
            List<string> __query;
            string __downloadGuid = Guid.NewGuid().ToString();
            int __downloadFlag = (this._screen_type == _screenDownloadEnum.PO) ? 2 : 3;
            string __transTableName = (this._screen_type == _screenDownloadEnum.PO) ? "bcpurchaseorder" : "bcsaleorder";
            string __transDetailTableName = (this._screen_type == _screenDownloadEnum.PO) ? "bcpurchaseordersub" : "bcsaleordersub";
            string __docType = (this._screen_type == _screenDownloadEnum.PO) ? "รายการสั่งซื้อ" : "รายการสั่งขายสั่งจอง";

            string __soBillType = "0";

            //check
            StringBuilder __itemDuplicate = new StringBuilder();
            for (int __i = 0; __i < this._clientDownloadGrid._rowData.Count; __i++)
            {
                if (this._clientDownloadGrid._cellGet(__i, 0).ToString().Equals("1"))
                {

                    DataSet __itemResult = __myFrameWork._query("select docno, docdate from " + __transTableName + " where docno = \'" + this._clientDownloadGrid._cellGet(__i, "docno").ToString() + "\'");
                    if (__itemResult.Tables.Count > 0 && __itemResult.Tables[0].Rows.Count > 0)
                    {
                        __itemDuplicate.AppendLine(__itemResult.Tables[0].Rows[0]["docno"].ToString());
                    }
                }
            }


            if (__itemDuplicate.Length > 0)
            {
                MessageBox.Show(__docType + "มีอยู่แล้วในระบบ \n" + __itemDuplicate.ToString(), DTSClientDownload._global._champMessageCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // after check duplicate

            // insert log start download
            __myFrameWork._excute("insert into dts_download(guid_download, agentcode, download_flag,download_date, download_start) values(\'" + __downloadGuid + "\',\'" + MyLib._myGlobal._userCode + "\'," + __downloadFlag + ", \'" + MyLib._myGlobal._convertDateTimeToQuery(DateTime.Now) + "\', \'" + MyLib._myGlobal._convertDateTimeToQuery(DateTime.Now) + "\')");

            for (int __row = 0; __row < this._clientDownloadGrid._rowData.Count; __row++)
            {
                if (this._clientDownloadGrid._cellGet(__row, 0).ToString().Equals("1"))
                {
                    string __donNo = this._clientDownloadGrid._cellGet(__row, "docno").ToString();

                    __myFrameWork._excute("insert into dts_download_detail(guid_download, agentcode, download_flag,download_date, download_start,ref_no) values(\'" + __downloadGuid + "\',\'" + MyLib._myGlobal._userCode + "\'," + __downloadFlag + ", \'" + MyLib._myGlobal._convertDateTimeToQuery(DateTime.Now) + "\', \'" + MyLib._myGlobal._convertDateTimeToQuery(DateTime.Now) + "\', \'" + __donNo + "\')");

                    _trans __transItem = (_trans)this._clientDownloadGrid._cellGet(__row, "transitem");
                    _trans_detail_list __trans_detail_list = (_trans_detail_list)this._clientDownloadGrid._cellGet(__row, "trans_detail");

                    if (this._screen_type == _screenDownloadEnum.SO)
                    {
                        // เช็ครายการว่าจะให้ไป ใบสั่งจองหรือ ใบสั่งขาย 0=สั่งขาย,1=สั่งจอง
                        for (int __i = 0; __i < __trans_detail_list._detail.Count; __i++)
                        {
                            //DataSet __itemBalanceResult = __myFrameWork._query("select qty from bcstkwarehouse where itemcode = \'" + __trans_detail_list._detail[__i]._itemCode + "\'");
                            DataSet __itemBalanceResult = __myFrameWork._query("select a.itemcode,a.whcode,a.shelfcode,a.unitcode,a.qty,a.docdate,b.amount  from bcstklocation as a right outer join bcstkwarehouse as b on a.whcode=b.whcode and a.itemcode=b.itemcode where a.itemcode = \'" + __trans_detail_list._detail[__i]._itemCode + "\' and a.WHCode = \'" + __trans_detail_list._detail[__i]._wh_code + "\' and a.ShelfCode =\'" + __trans_detail_list._detail[__i]._shelf_code + "\' ");
                            if (__itemBalanceResult.Tables.Count > 0 && __itemBalanceResult.Tables[0].Rows.Count > 0)
                            {
                                decimal __balanceQty = MyLib._myGlobal._decimalPhase(__itemBalanceResult.Tables[0].Rows[0]["qty"].ToString());
                                if (__trans_detail_list._detail[__i]._qty > __balanceQty)
                                {
                                    __soBillType = "1";
                                    break;
                                }
                            }
                            else
                            {
                                __soBillType = "1";
                                break;
                            }
                        }
                    }

                    __query = new List<string>();

                    // insert bcpurchaseorder

                    string __fieldList = MyLib._myGlobal._fieldAndComma("docno", "docdate", "taxtype", "taxrate", "sumofitemamount", "afterdiscount", "beforetaxamount", "taxamount", "totalamount", "excepttaxamount", "zerotaxamount", "iscompletesave", "netamount", "creatorcode", "createdatetime");
                    string __valueList = "";

                    if (_screen_type == _screenDownloadEnum.PO)
                    {
                        __valueList = MyLib._myGlobal._fieldAndComma("\'" + __donNo + "\'", "\'" + MyLib._myGlobal._convertDateToQuery(__transItem._docdate) + "\'", "\'" + __transItem._taxtype + "\'", "\'" + __transItem._taxRate + "\'", __transItem._sumofitemamount.ToString(), __transItem._afterdiscount.ToString(), __transItem._beforetaxamount.ToString(), __transItem._taxamount.ToString(), __transItem._totalamount.ToString(), __transItem._excepttaxamount.ToString(), __transItem._zerotaxamount.ToString(), "1", __transItem._netamount.ToString(), "\'dts\'", "GETDATE()");
                    }
                    else if (_screen_type == _screenDownloadEnum.SO)
                    {
                        __valueList = MyLib._myGlobal._fieldAndComma("\'" + __donNo + "\'", "\'" + MyLib._myGlobal._convertDateToQuery(__transItem._docdate) + "\'", "\'" + __transItem._taxtype + "\'", "\'" + __transItem._taxRate + "\'", __transItem._sumofitemamount.ToString(), __transItem._afterdiscount.ToString(), __transItem._beforetaxamountGet.ToString(), __transItem._taxAmountGet.ToString(), __transItem._totalAmountGet.ToString(), __transItem._excepttaxamount.ToString(), __transItem._zerotaxamount.ToString(), "1", __transItem._netamountGet.ToString(), "\'dts\'", "GETDATE()");
                    }


                    if (_screen_type == _screenDownloadEnum.PO)
                    {
                        __fieldList = __fieldList + ",apcode,leaddate," + _g.DataClient.dts_bcpurchaseorder._creditday + "," + _g.DataClient.dts_bcpurchaseorder._leadtime + "," + _g.DataClient.dts_bcpurchaseorder._duedate;
                        __valueList = __valueList + ",\'" + __transItem._aparcode + "\',\'" + MyLib._myGlobal._convertDateToQuery(__transItem._leaddate) + "\'," + __transItem._creditday + "," + __transItem._leadtime + ",\'" + MyLib._myGlobal._convertDateToQuery(__transItem._duedate) + "\'";
                    }
                    else if (_screen_type == _screenDownloadEnum.SO)
                    {
                        __fieldList = __fieldList + ",arcode,billtype,sostatus," + _g.DataClient.dts_bcsaleorder._ownreceive + "," + _g.DataClient.dts_bcsaleorder._mydescription + "," + _g.DataClient.dts_bcsaleorder._deliveryday + "," + _g.DataClient.dts_bcsaleorder._isconditionsend + "," + _g.DataClient.dts_bcsaleorder._deliverydate;

                        // " + __transItem._aparcode + " bill type = 1 พี่บอย confirm
                        __valueList = __valueList + ",\'XP\',1," + __soBillType + ",\'" + __transItem._ownreceive + "\', \'" + __transItem._myDescription + "\'," + __transItem._deliveryDay + "," + __transItem._isConditionSend + ", \'" + MyLib._myGlobal._convertDateToQuery(__transItem._deliveryDate) + "\'";
                    }

                    __query.Add("insert into " + __transTableName + "(" + __fieldList + ") values (" + __valueList + ")");

                    for (int __detailIndex = 0; __detailIndex < __trans_detail_list._detail.Count; __detailIndex++)
                    {
                        string __detailField = MyLib._myGlobal._fieldAndComma("taxtype", "docno", "docdate", "itemcode", "unitcode", "itemname", "whcode", "shelfcode", "qty", "price", "amount", "remainqty", "netamount", "taxrate", "linenumber", "homeamount", "packingrate1", "packingrate2", _g.DataClient.dts_bcpurchaseordersub._groupcode, _g.DataClient.dts_bcpurchaseordersub._typecode, _g.DataClient.dts_bcpurchaseordersub._categorycode, _g.DataClient.dts_bcpurchaseordersub._formatcode, "creatorcode", "createdatetime");
                        string __detailValue = MyLib._myGlobal._fieldAndComma("\'" + __transItem._taxtype + "\'", "\'" + __donNo + "\'", "\'" + MyLib._myGlobal._convertDateToQuery(__transItem._docdate) + "\'", "\'" + __trans_detail_list._detail[__detailIndex]._itemCode + "\'", "\'" + __trans_detail_list._detail[__detailIndex]._unitCode + "\'", "\'" + __trans_detail_list._detail[__detailIndex]._itemName + "\'", "\'" + __trans_detail_list._detail[__detailIndex]._wh_code + "\'", "\'" + __trans_detail_list._detail[__detailIndex]._shelf_code + "\'", __trans_detail_list._detail[__detailIndex]._qty.ToString(), __trans_detail_list._detail[__detailIndex]._price.ToString(), __trans_detail_list._detail[__detailIndex]._amount.ToString(), __trans_detail_list._detail[__detailIndex]._qty.ToString(), __trans_detail_list._detail[__detailIndex]._netamount.ToString(), __trans_detail_list._detail[__detailIndex]._taxrate.ToString(), __detailIndex.ToString(), __trans_detail_list._detail[__detailIndex]._netamount.ToString(), __trans_detail_list._detail[__detailIndex]._rate1.ToString(), __trans_detail_list._detail[__detailIndex]._rate2.ToString(), "\'" + __trans_detail_list._detail[__detailIndex].groupcode.ToString() + "\'", "\'" + __trans_detail_list._detail[__detailIndex].typecode.ToString() + "\'", "\'" + __trans_detail_list._detail[__detailIndex].categorycode.ToString() + "\'", "\'" + __trans_detail_list._detail[__detailIndex].formatcode.ToString() + "\'", "\'dts\'", "GETDATE()");

                        if (_screen_type == _screenDownloadEnum.PO)
                        {
                            __detailField = __detailField + ",apcode";
                            __detailValue = __detailValue + ",\'" + __transItem._aparcode + "\'";
                        }
                        else if (_screen_type == _screenDownloadEnum.SO)
                        {
                            __detailField = __detailField + ",POQty,arcode,sostatus,reftype," + _g.DataClient.dts_bcsaleordersub._originaldate;
                            __detailValue = __detailValue + "," + __trans_detail_list._detail[__detailIndex]._qty.ToString() + ",\'XP\'," + __soBillType + "," + ((__soBillType.Equals("0")) ? "1" : "0") + ",\'" + MyLib._myGlobal._convertDateToQuery(__trans_detail_list._detail[__detailIndex]._OriginalDate) + "\'";
                        }

                        string __detail_query = "insert into " + __transDetailTableName + "(" + __detailField + ") values(" + __detailValue + ")";
                        __query.Add(__detail_query);

                        // process stock
                        __query.Add("INSERT INTO ProcessStock (ItemCode,DocDate,ProcessFlag,FlowStatus) VALUES(\'" + __trans_detail_list._detail[__detailIndex]._itemCode + "\', \'" + MyLib._myGlobal._convertDateToQuery(__transItem._docdate) + "\', 1, 1) ");

                        __query.Add("insert into processstatus (itemcode,docdate,processflag,FlowStatus,roworderref) select top 100 itemcode,docdate,processflag,FlowStatus,roworder from processstock where processflag=1 order by itemcode");
                    }

                    // save เข้า champ
                    string __result = __myFrameWork._queryList(__query);
                    if (__result.Length == 0)
                    {
                        __myFrameWork._excute("update dts_download_detail set download_success = \'" + MyLib._myGlobal._convertDateTimeToQuery(DateTime.Now) + "\', download_status = 1 where guid_download = \'" + __downloadGuid + "\' and agentcode=\'" + MyLib._myGlobal._userCode + "\' and download_flag = " + __downloadFlag + " and ref_no = \'" + __donNo + "\' ");
                    }
                    else
                    {
                        Console.WriteLine(__result);
                        __myFrameWork._excute("update dts_download_detail set download_success = \'" + MyLib._myGlobal._convertDateTimeToQuery(DateTime.Now) + "\', download_status = 2 where guid_download = \'" + __downloadGuid + "\' and agentcode=\'" + MyLib._myGlobal._userCode + "\' and download_flag = " + __downloadFlag + " and ref_no = \'" + __donNo + "\' ");

                        MessageBox.Show(__result, DTSClientDownload._global._champMessageCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // select guid from doc master and insert into sync 
                    {
                        string __selectGUIDforDeleteQuery = "select guid from " + ((this._screen_type == _screenDownloadEnum.PO) ? _g.DataClient.dts_bcpurchaseorder._table + " where " + _g.DataClient.dts_bcpurchaseorder._docno : _g.DataClient.dts_bcsaleorder._table + " where " + _g.DataClient.dts_bcsaleorder._docno) + " = \'" + __donNo + "\'";
                        string __selectSubGUIDforDeleteQuery = "select guid from " + ((this._screen_type == _screenDownloadEnum.PO) ? _g.DataClient.dts_bcpurchaseordersub._table + " where " + _g.DataClient.dts_bcpurchaseordersub._docno : _g.DataClient.dts_bcsaleordersub._table + " where " + _g.DataClient.dts_bcsaleordersub._docno) + " = \'" + __donNo + "\'";

                        List<string> __queryGet = new List<string>();
                        __queryGet.Add(__selectGUIDforDeleteQuery);
                        __queryGet.Add(__selectSubGUIDforDeleteQuery);

                        ArrayList __getDataResult = __myFrameWork._getDataList(__queryGet);
                        if (__getDataResult.Count > 0)
                        {

                            List<string> __queryDelGuid = new List<string>();
                            DataSet __ds1 = (DataSet)__getDataResult[0];
                            DataSet __ds2 = (DataSet)__getDataResult[1];

                            if (__ds1.Tables.Count > 0 && __ds1.Tables[0].Rows.Count > 0)
                            {
                                for (int __i = 0; __i < __ds1.Tables[0].Rows.Count; __i++)
                                {
                                    __queryDelGuid.Add("insert into client_sync_data(table_name, sync_mode, guid) values (\'" + ((this._screen_type == _screenDownloadEnum.PO) ? _g.DataServer.bcpurchaseorder._table : _g.DataServer.bcsaleorder._table) + "\', \'0\', \'" + __ds1.Tables[0].Rows[__i]["guid"].ToString() + "\')");
                                }
                            }

                            if (__ds2.Tables.Count > 0 && __ds2.Tables[0].Rows.Count > 0)
                            {
                                for (int __i = 0; __i < __ds2.Tables[0].Rows.Count; __i++)
                                {
                                    __queryDelGuid.Add("insert into client_sync_data(table_name, sync_mode, guid) values (\'" + ((this._screen_type == _screenDownloadEnum.PO) ? _g.DataServer.bcpurchaseordersub._table : _g.DataServer.bcsaleordersub._table) + "\', \'0\', \'" + __ds2.Tables[0].Rows[__i]["guid"].ToString() + "\')");
                                }
                            }

                            if (__queryDelGuid.Count > 0)
                            {
                                string __insResult = __myFrameWork._queryList(__queryDelGuid);
                            }
                        }
                    }
                }
            }

            __myFrameWork._excute("update dts_download set download_success = \'" + MyLib._myGlobal._convertDateTimeToQuery(DateTime.Now) + "\', download_status = 1 where guid_download = \'" + __downloadGuid + "\' and agentcode=\'" + MyLib._myGlobal._userCode + "\' and download_flag = " + __downloadFlag + " ");
            MessageBox.Show("ดาวน์โหลดข้อมูลสมบูรณ์ !!", DTSClientDownload._global._champMessageCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);

            

            this._clientDownloadGrid._clear();
            this._loadData();
            _checkEnableSaveButton();
        }

        private void _importButton_Click(object sender, EventArgs e)
        {
            for (int __i = 0; __i < this._serverDownloadGrid._rowData.Count; __i++)
            {
                if (this._serverDownloadGrid._cellGet(__i, 0).ToString().Equals("1"))
                {
                    _addSelectItem(__i);
                }
                //else
                //{
                //    _removeSelectItem(__i);
                //}
            }

            //for (int __i = 0; __i < this._serverDownloadGrid._rowData.Count; __i++)
            //{
            //    this._serverDownloadGrid._cellUpdate(__i, 0, 1, true);
            //}
            //this._serverDownloadGrid.Refresh();
            this._serverDownloadGrid.Refresh();
            this._checkEnableSaveButton();
        }

        private void _exportButton_Click(object sender, EventArgs e)
        {
            for (int __i = this._clientDownloadGrid._rowData.Count - 1; __i >= 0; __i--)
            {
                if (this._clientDownloadGrid._cellGet(__i, 0).ToString().Equals("1"))
                {
                    //_addSelectItem(__i);
                    this._clientDownloadGrid._rowData.RemoveAt(__i);
                }
                //else
                //{
                //    _removeSelectItem(__i);
                //}
            }
            this._clientDownloadGrid.Refresh();
            this._transDetailGrid._clear();
            this._checkEnableSaveButton();
            //for (int __i = 0; __i < this._serverDownloadGrid._rowData.Count; __i++)
            //{
            //    this._serverDownloadGrid._cellUpdate(__i, 0, 0, true);
            //}
            //this._serverDownloadGrid.Refresh();

        }

        private void _unSelectAllClientToolStripButton_Click(object sender, EventArgs e)
        {
            for (int __i = 0; __i < this._clientDownloadGrid._rowData.Count; __i++)
            {
                this._clientDownloadGrid._cellUpdate(__i, 0, 0, true);
            }
            this._clientDownloadGrid.Refresh();
        }

        private void _selectallToolStript_Click(object sender, EventArgs e)
        {
            for (int __i = 0; __i < this._serverDownloadGrid._rowData.Count; __i++)
            {
                this._serverDownloadGrid._cellUpdate(__i, 0, 1, true);
            }
            this._serverDownloadGrid.Refresh();
        }

        private void _disSelectedServerToolStripButton_Click(object sender, EventArgs e)
        {
            for (int __i = 0; __i < this._serverDownloadGrid._rowData.Count; __i++)
            {
                this._serverDownloadGrid._cellUpdate(__i, 0, 0, true);
            }
            this._serverDownloadGrid.Refresh();
        }

        private void _selectAllClientToolStripButton_Click(object sender, EventArgs e)
        {
            for (int __i = 0; __i < this._clientDownloadGrid._rowData.Count; __i++)
            {
                this._clientDownloadGrid._cellUpdate(__i, 0, 1, true);
            }
            this._clientDownloadGrid.Refresh();
        }

        private void _refreshToolStrip_Click(object sender, EventArgs e)
        {
            this._loadData();
        }


    }

}
