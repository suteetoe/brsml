using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Collections;

namespace SMLERPIC
{
    public partial class _icEditDetailFast : UserControl
    {
        public _icEditDetailFast()
        {
            InitializeComponent();
            this._resetButton.Click += _resetButton_Click;
            this._updateButton.Click += _updateButton_Click;
            _build();

            this._myManageData._dataList._lockRecord = false;
            this._myManageData._autoSize = true;
            this._myManageData._displayMode = 0;
            this._myManageData._selectDisplayMode(this._myManageData._displayMode);

            this._myManageData._dataList._loadViewFormat(_g.g._search_screen_ic_inventory, MyLib._myGlobal._userSearchScreenGroup, true);
            this._myManageData._dataList._gridData._mouseClick += _gridData__mouseClick;


            _g._utils __utils = new _g._utils();
            __utils._updateInventoryMaster("");
            Thread __thread = new Thread(new ThreadStart(__utils._updateInventoryMasterFunction));
            __thread.Start();

        }

        void _updateButton_Click(object sender, EventArgs e)
        {

            if (this._icGrid._rowData.Count > 0)
            {
                StringBuilder __getCodeStr = new StringBuilder();
                for (int __row = 0; __row < this._icGrid._rowData.Count; __row++)
                {
                    if (__getCodeStr.Length > 0)
                    {
                        __getCodeStr.Append(",");
                    }

                    string __icCode = this._icGrid._cellGet(__row, _g.d.ic_inventory._code).ToString();
                    __getCodeStr.Append("\'" + __icCode + "\'");
                }

                // pack คำสั่ง Update
                List<string> __updateList = new List<string>();

                string __getDescription = this._icmainScreenDescripControl._createQueryForUpdateValueOnly();
                string __getDimension = this._icmainScreenDimesionControl._createQueryForUpdateValueOnly();
                string __getICStatus = this._icmainScreenStatus._createQueryForUpdateValueOnly();
                string __getGroup = this._icmainScreenGroupControl._createQueryForUpdateValueOnly();
                string __getSaleWh = this._icmainScreenSaleWh._createQueryForUpdateValueOnly();
                string __getPurchaseWH = this._icmainScreenPurchaseWh._createQueryForUpdateValueOnly();
                string __getOutWH = this._icmainScreenOutWh._createQueryForUpdateValueOnly();

                //if (__getDescription.Length > 0)
                //    __updateList.Add(__getDescription);

                if (__getDimension.Length > 0)
                    __updateList.Add(__getDimension);

                if (__getICStatus.Length > 0)
                    __updateList.Add(__getICStatus);

                if (__getGroup.Length > 0)
                    __updateList.Add(__getGroup);

                if (__getSaleWh.Length > 0)
                    __updateList.Add(__getSaleWh);

                if (__getPurchaseWH.Length > 0)
                    __updateList.Add(__getPurchaseWH);

                if (__getOutWH.Length > 0)
                    __updateList.Add(__getOutWH);


                if (__updateList.Count > 0 || __getDescription.Length > 0)
                {
                    string __updateStr = "";

                    if (__updateList.Count > 0)
                        __updateStr = string.Join(",", __updateList.ToArray());

                    StringBuilder __query = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");
                    if (__getDescription.Length > 0)
                        __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update ic_inventory set " + __getDescription + " where code in (" + __getCodeStr + ")"));

                    if (__updateStr.Length > 0)

                        __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update ic_inventory_detail set " + __updateStr + " where ic_code in (" + __getCodeStr + ")"));

                    __query.Append("</node>");

                    MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                    string __result = __myFrameWork._queryList(MyLib._myGlobal._databaseName, __query.ToString());
                    if (__result.Length == 0)
                    {
                        MessageBox.Show("Update Success !!");
                        this._icGrid._rowData.Clear();

                    }
                    else
                    {
                        MessageBox.Show(__result.ToString());
                    }

                }

            }
        }

        void _clearScreen()
        {
            this._icmainScreenDescripControl._clear();
            this._icmainScreenDimesionControl._clear();
            this._icmainScreenStatus._clear();
            this._icmainScreenGroupControl._clear();
            this._icmainScreenSaleWh._clear();
            this._icmainScreenPurchaseWh._clear();
            this._icmainScreenOutWh._clear();

        }

        void _resetButton_Click(object sender, EventArgs e)
        {
            this._icGrid._rowData.Clear();
        }

        void _gridData__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            string __icCode = this._myManageData._dataList._gridData._cellGet(e._row, _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code).ToString();
            string __icName = this._myManageData._dataList._gridData._cellGet(e._row, _g.d.ic_inventory._table + "." + _g.d.ic_inventory._name_1).ToString();
            Boolean __found = false;

            for (int __row = 0; __row < this._icGrid._rowData.Count; __row++)
            {
                if (__icCode.Equals(this._icGrid._cellGet(__row, _g.d.ic_inventory._code).ToString()))
                {
                    __found = true;
                    break;
                }
            }
            if (__found == false)
            {

                int __addr = this._icGrid._addRow();
                this._icGrid._cellUpdate(__addr, _g.d.ic_inventory._code, __icCode, false);
                this._icGrid._cellUpdate(__addr, _g.d.ic_inventory._name_1, __icName, false);

                /*
                this._icGrid._cellUpdate(__addr, _g.d.ic_inventory_barcode._barcode, __data.Rows[0][_g.d.ic_inventory_barcode._barcode].ToString(), false);
                this._icGrid._cellUpdate(__addr, _g.d.ic_inventory_barcode._ic_code, __data.Rows[0][_g.d.ic_inventory_barcode._ic_code].ToString(), false);
                this._selectedGid._cellUpdate(__addr, _g.d.ic_inventory_barcode._description, __data.Rows[0][_g.d.ic_inventory_barcode._description].ToString(), false);
                this._selectedGid._cellUpdate(__addr, _g.d.ic_inventory_barcode._unit_code, __data.Rows[0][_g.d.ic_inventory_barcode._unit_code].ToString(), false);
                this._selectedGid._cellUpdate(__addr, _g.d.ic_inventory_barcode._price, MyLib._myGlobal._decimalPhase(__data.Rows[0][_g.d.ic_inventory_barcode._price].ToString()), false);
                this._selectedGid._cellUpdate(__addr, _g.d.ic_inventory_barcode._price_2, MyLib._myGlobal._decimalPhase(__data.Rows[0][_g.d.ic_inventory_barcode._price_2].ToString()), false);
                this._selectedGid._cellUpdate(__addr, _g.d.ic_inventory_barcode._price_3, MyLib._myGlobal._decimalPhase(__data.Rows[0][_g.d.ic_inventory_barcode._price_3].ToString()), false);
                this._selectedGid._cellUpdate(__addr, _g.d.ic_inventory_barcode._price_4, MyLib._myGlobal._decimalPhase(__data.Rows[0][_g.d.ic_inventory_barcode._price_4].ToString()), false);
                this._selectedGid._cellUpdate(__addr, _g.d.ic_inventory_barcode._price_member, MyLib._myGlobal._decimalPhase(__data.Rows[0][_g.d.ic_inventory_barcode._price_member].ToString()), false);
                this._selectedGid._cellUpdate(__addr, _g.d.ic_inventory_barcode._price_member_2, MyLib._myGlobal._decimalPhase(__data.Rows[0][_g.d.ic_inventory_barcode._price_member_2].ToString()), false);
                this._selectedGid._cellUpdate(__addr, _g.d.ic_inventory_barcode._price_member_3, MyLib._myGlobal._decimalPhase(__data.Rows[0][_g.d.ic_inventory_barcode._price_member_3].ToString()), false);
                this._selectedGid._cellUpdate(__addr, _g.d.ic_inventory_barcode._price_member_4, MyLib._myGlobal._decimalPhase(__data.Rows[0][_g.d.ic_inventory_barcode._price_member_4].ToString()), false);
                this._selectedGid._cellUpdate(__addr, _g.d.ic_inventory_barcode._unit_name, __data.Rows[0][_g.d.ic_inventory_barcode._unit_name].ToString(), false);

                this._selectedGid._cellUpdate(__addr, _g.d.ic_inventory_barcode._price_formula_price_0, MyLib._myGlobal._decimalPhase(__data.Rows[0][_g.d.ic_inventory_barcode._price_formula_price_0].ToString()), false);

                this._selectedGid._cellUpdate(__addr, _g.d.ic_inventory_barcode._stamp_count, 0M, false);
                */
            }
        }

        public void _build()
        {

            this._myTabControl1.TableName = _g.d.ic_resource._table;
            this._myTabControl1._getResource();

            this._icGrid._width_by_persent = true;
            this._icGrid._table_name = _g.d.ic_inventory._table;
            this._icGrid._addColumn(_g.d.ic_inventory._code, 1, 25, 25);
            this._icGrid._addColumn(_g.d.ic_inventory._name_1, 1, 75, 75);
            this._icGrid._calcPersentWidthToScatter();

        }

        private void _closeButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
