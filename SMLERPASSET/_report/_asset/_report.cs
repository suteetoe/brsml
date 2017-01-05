using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLERPASSET._report._asset
{
    public partial class _report : UserControl
    {
        DataSet _getAsset;
        DataSet _getAssetDetail;
        MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
        SMLReport._report._objectListType __assetObject;
        SMLReport._report._objectListType __assetDetailObject;
        // Condition
        _condition _conditionScreen = new _condition();
        string _conditionCodeBegin = "";
        string _conditionCodeEnd = "";
        int _conditionSortBy = 0;
        // Total
        float _totalBuyPrice = 0;
        float _totalDeadValue = 0;
        //
        public _report()
        {
            InitializeComponent();
            // Event
            _view1._buttonExample.Enabled = false;
            _view1._buttonCondition.Click += new EventHandler(_buttonCondition_Click);
            _view1._buttonBuildReport.Click += new EventHandler(_buttonBuildReport_Click);
            _view1._loadData += new SMLReport._report.LoadDataEventHandler(_view1__loadData);
            _view1._getObject += new SMLReport._report.GetObjectEventHandler(_view1__getObject);
            _view1._getDataObject += new SMLReport._report.GetDataObjectEventHandler(_view1__getDataObject);
            _view1._buttonClose.Click += new EventHandler(_buttonClose_Click);
            //*******************
            _showCondition();
        }

        void _showCondition()
        {
            _conditionScreen.ShowDialog();
            _conditionCodeBegin = this._conditionScreen._screenAssetCondition1._getDataStr("as_resource.from_asset");
            _conditionCodeEnd = this._conditionScreen._screenAssetCondition1._getDataStr("as_resource.to_asset");
            _conditionSortBy = MyLib._myGlobal._intPhase(this._conditionScreen._screenAssetCondition1._getDataStr("as_resource.order_by"));
            _getAsset = null;
            _getAssetDetail = null;
        }

        void _buttonCondition_Click(object sender, EventArgs e)
        {
            _showCondition();
        }

        void _view1__getDataObject()
        {
            int __sectionCode = 0;
            int __sectionName = 0;
            int __typeCode = 0;
            int __typeName = 0;
            int __locationCode = 0;
            int __locationName = 0;
            int __buyDate = 0;
            int __startcalcDate = 0;
            // Index Top
            if (_conditionSortBy == 0)
            {
                __sectionCode = _view1._findColumn(__assetObject, _g.d.as_asset._department_code);
                __sectionName = _view1._findColumn(__assetObject, "section_name");
            }
            else if (_conditionSortBy == 1)
            {
                __typeCode = _view1._findColumn(__assetObject, _g.d.as_asset._as_type);
                __typeName = _view1._findColumn(__assetObject, "type_name");
            }
            else if (_conditionSortBy == 2)
            {
                __locationCode = _view1._findColumn(__assetObject, _g.d.as_asset._as_location);
                __locationName = _view1._findColumn(__assetObject, "location_name");
            }
            else if (_conditionSortBy == 3)
            {
                __buyDate = _view1._findColumn(__assetObject, _g.d.as_asset_detail._as_buy_date);
            }
            else if (_conditionSortBy == 4)
            {
                __startcalcDate = _view1._findColumn(__assetObject, _g.d.as_asset_detail._start_calc_date);
            }            
            // Index Bottom
            int __detailCode = _view1._findColumn(__assetDetailObject, _g.d.as_asset._code);
            int __detailName1 = _view1._findColumn(__assetDetailObject, _g.d.as_asset._name_1);
            int __detailUnit = _view1._findColumn(__assetDetailObject, "unit_name");
            int __detailStartCalc = _view1._findColumn(__assetDetailObject, "start_calc");
            int __detailBuyPrice = _view1._findColumn(__assetDetailObject, "buy_price");
            int __detailRate = _view1._findColumn(__assetDetailObject, "rate");
            int __detailDeadValue = _view1._findColumn(__assetDetailObject, "dead_value");
            //*********************
            SMLReport._report._columnListType __getColumn = (SMLReport._report._columnListType)__assetObject._columnList[0];
            for (int __row = 0; __row < _getAsset.Tables[0].Rows.Count; __row++)
            {
                SMLReport._report._objectListType __dataObject = _view1._addObject(_view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                _view1._createEmtryColumn(__assetObject, __dataObject);
                Font __newFont = new Font(__getColumn._fontData, FontStyle.Bold);
                string __formatNumber = MyLib._myGlobal._getFormatNumber("m02");
                //*************
                DataRow[] __getAssetDetail = null;
                if (_conditionSortBy == 0)
                {
                    string __getSectionCode = _getAsset.Tables[0].Rows[__row].ItemArray[0].ToString();
                    string __getSectionName = _getAsset.Tables[0].Rows[__row].ItemArray[1].ToString();
                    _view1._addDataColumn(__assetObject, __dataObject, __sectionCode, __getSectionCode, __newFont, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                    _view1._addDataColumn(__assetObject, __dataObject, __sectionName, __getSectionName, __newFont, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                    __getAssetDetail = _getAssetDetail.Tables[0].Select(_g.d.as_asset._department_code + "=\'" + __getSectionCode + "\'");
                }
                else if (_conditionSortBy == 1)
                {
                    string __getTypeCode = _getAsset.Tables[0].Rows[__row].ItemArray[0].ToString();
                    string __getTypeName = _getAsset.Tables[0].Rows[__row].ItemArray[1].ToString();
                    _view1._addDataColumn(__assetObject, __dataObject, __typeCode, __getTypeCode, __newFont, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                    _view1._addDataColumn(__assetObject, __dataObject, __typeName, __getTypeName, __newFont, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                    __getAssetDetail = _getAssetDetail.Tables[0].Select(_g.d.as_asset._as_type + "=\'" + __getTypeCode + "\'");
                }
                else if (_conditionSortBy == 2)
                {
                    string __getLocationCode = _getAsset.Tables[0].Rows[__row].ItemArray[0].ToString();
                    string __getLocationName = _getAsset.Tables[0].Rows[__row].ItemArray[1].ToString();
                    _view1._addDataColumn(__assetObject, __dataObject, __locationCode, __getLocationCode, __newFont, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                    _view1._addDataColumn(__assetObject, __dataObject, __locationName, __getLocationName, __newFont, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                    __getAssetDetail = _getAssetDetail.Tables[0].Select(_g.d.as_asset._as_location + "=\'" + __getLocationCode + "\'");
                }
                else if (_conditionSortBy == 3)
                {
                    string __getBuyDateStr = _getAsset.Tables[0].Rows[__row].ItemArray[0].ToString();
                    DateTime __getBuyDate = MyLib._myGlobal._convertDateFromQuery(__getBuyDateStr);
                    _view1._addDataColumn(__assetObject, __dataObject, __buyDate, MyLib._myGlobal._convertDateToString(__getBuyDate, false, true), null, SMLReport._report._cellAlign.Center, 0,SMLReport._report._cellType.String);
                    __getAssetDetail = _getAssetDetail.Tables[0].Select("buy_date=\'" + __getBuyDateStr + "\'");
                }
                else if (_conditionSortBy == 4)
                {
                    string __getStartcalcDateStr = _getAsset.Tables[0].Rows[__row].ItemArray[0].ToString();
                    DateTime __getStartcalcDate = MyLib._myGlobal._convertDateFromQuery(__getStartcalcDateStr);
                    _view1._addDataColumn(__assetObject, __dataObject, __buyDate, MyLib._myGlobal._convertDateToString(__getStartcalcDate, false, true), null, SMLReport._report._cellAlign.Center, 0,SMLReport._report._cellType.String);
                    __getAssetDetail = _getAssetDetail.Tables[0].Select("startcalc_date=\'" + __getStartcalcDateStr + "\'");
                }
                //*****************
                for (int __rowDetail = 0; __rowDetail < __getAssetDetail.Length; __rowDetail++)
                {
                    SMLReport._report._objectListType __dataDetailObject = _view1._addObject(_view1._objectDataList, SMLReport._report._objectType.Detail, true, 8, true, SMLReport._report._columnBorder.None);
                    _view1._createEmtryColumn(__assetDetailObject, __dataDetailObject);
                    string __getAssetCode = __getAssetDetail[__rowDetail].ItemArray[0].ToString();
                    string __getAssetName1 = __getAssetDetail[__rowDetail].ItemArray[1].ToString();
                    string __getUnit = __getAssetDetail[__rowDetail].ItemArray[3].ToString();
                    string __getStartCalcDateStr = __getAssetDetail[__rowDetail].ItemArray[4].ToString();
                    DateTime __getStartCalcDate = MyLib._myGlobal._convertDateFromQuery(__getStartCalcDateStr);
                    float __getBuyPrice = (float)Double.Parse(__getAssetDetail[__rowDetail].ItemArray[5].ToString());
                    float __getRate = (float)Double.Parse(__getAssetDetail[__rowDetail].ItemArray[6].ToString());
                    float __getDeadValue = (float)Double.Parse(__getAssetDetail[__rowDetail].ItemArray[7].ToString());
                    //*****************
                    _view1._addDataColumn(__assetDetailObject, __dataDetailObject, __detailCode, __getAssetCode, null, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                    _view1._addDataColumn(__assetDetailObject, __dataDetailObject, __detailName1, __getAssetName1, null, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                    _view1._addDataColumn(__assetDetailObject, __dataDetailObject, __detailUnit, __getUnit, null, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                    _view1._addDataColumn(__assetDetailObject, __dataDetailObject, __detailStartCalc, MyLib._myGlobal._convertDateToString(__getStartCalcDate, false, true), null, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                    _view1._addDataColumn(__assetDetailObject, __dataDetailObject, __detailBuyPrice, (__getBuyPrice == 0) ? "" : string.Format(__formatNumber, __getBuyPrice), null, SMLReport._report._cellAlign.Right, 0,SMLReport._report._cellType.Number);
                    _view1._addDataColumn(__assetDetailObject, __dataDetailObject, __detailRate, (__getRate == 0) ? "" : string.Format(__formatNumber, __getRate), null, SMLReport._report._cellAlign.Right, 0,SMLReport._report._cellType.Number);
                    _view1._addDataColumn(__assetDetailObject, __dataDetailObject, __detailDeadValue, (__getDeadValue == 0) ? "" : string.Format(__formatNumber, __getDeadValue), null, SMLReport._report._cellAlign.Right, 0,SMLReport._report._cellType.Number);
                    // Total
                    _totalBuyPrice += __getBuyPrice;
                    _totalDeadValue += __getDeadValue;                 
                    if (__rowDetail == (__getAssetDetail.Length-1))
                    {
                        __rowDetail++;
                        SMLReport._report._objectListType __dataTotalObject = _view1._addObject(_view1._objectDataList, SMLReport._report._objectType.Detail, true, 8, true, SMLReport._report._columnBorder.None);
                        _view1._createEmtryColumn(__assetDetailObject, __dataTotalObject);
                        _view1._addDataColumn(__assetDetailObject, __dataTotalObject, __detailCode, "", null, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                        _view1._addDataColumn(__assetDetailObject, __dataTotalObject, __detailName1, "รวม " + __rowDetail + " รายการ", __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._columnBorder.Bottom, SMLReport._report._cellType.String);
                        _view1._addDataColumn(__assetDetailObject, __dataTotalObject, __detailUnit, "", null, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                        _view1._addDataColumn(__assetDetailObject, __dataTotalObject, __detailStartCalc, "", null, SMLReport._report._cellAlign.Center, 0,SMLReport._report._cellType.String);
                        _view1._addDataColumn(__assetDetailObject, __dataTotalObject, __detailBuyPrice, (_totalBuyPrice == 0) ? "" : string.Format(__formatNumber, _totalBuyPrice), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.Bottom, SMLReport._report._cellType.Number);
                        _view1._addDataColumn(__assetDetailObject, __dataTotalObject, __detailRate, "", null, SMLReport._report._cellAlign.Right, 0,SMLReport._report._cellType.Number);
                        _view1._addDataColumn(__assetDetailObject, __dataTotalObject, __detailDeadValue, (_totalDeadValue == 0) ? "" : string.Format(__formatNumber, _totalDeadValue), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.Bottom, SMLReport._report._cellType.Number);
                    }             
                }
                // Clear Total
                _totalBuyPrice = 0;
                _totalDeadValue = 0;
            }
        }

        bool _view1__getObject(System.Collections.ArrayList objectList, SMLReport._report._objectType type)
        {
            if (type == SMLReport._report._objectType.Header)
            {
                SMLReport._report._objectListType __headerObject = _view1._addObject(_view1._objectList, SMLReport._report._objectType.Header, true, 0, true, SMLReport._report._columnBorder.None);
                int __newColumn = _view1._addColumn(__headerObject, 100);
                _view1._addCell(__headerObject, __newColumn, true, 0, -1, SMLReport._report._cellType.String, SMLReport._report._reportValueDefault._ltdName, SMLReport._report._cellAlign.Center, _view1._fontHeader1);
                _view1._addCell(__headerObject, __newColumn, true, 1, -1, SMLReport._report._cellType.String, "รายงานรายละเอียดสินทรัพย์", SMLReport._report._cellAlign.Center, _view1._fontHeader2);
                _view1._addCell(__headerObject, __newColumn, true, 2, -1, SMLReport._report._cellType.String, "&LTD_ADDRESS3&", SMLReport._report._cellAlign.Center, _view1._fontStandard);
                _view1._addCell(__headerObject, __newColumn, true, 2, -1, SMLReport._report._cellType.String, "วันที่พิมพ์ : " + SMLReport._report._reportValueDefault._currentDateTime, SMLReport._report._cellAlign.Left, _view1._fontStandard);
                _view1._addCell(__headerObject, __newColumn, true, 2, -1, SMLReport._report._cellType.String, "หน้า : " + SMLReport._report._reportValueDefault._pageNumber + "/" + SMLReport._report._reportValueDefault._pageTotal, SMLReport._report._cellAlign.Right, _view1._fontStandard);
                return true;
            }
            else
                if (type == SMLReport._report._objectType.Detail)
                {
                    __assetObject = _view1._addObject(_view1._objectList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.Top);
                    if (_conditionSortBy == 0)
                    {
                        _view1._addColumn(__assetObject, 25, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.as_asset._department_code, _g.d.as_asset._table + "." + _g.d.as_asset._department_code, _g.d.as_asset._department_code, SMLReport._report._cellAlign.Left);
                        _view1._addColumn(__assetObject, 75, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "section_name", "as_resource.section_name", "section_name", SMLReport._report._cellAlign.Left);
                    }
                    else if (_conditionSortBy == 1)
                    {
                        _view1._addColumn(__assetObject, 25, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.as_asset._as_type, _g.d.as_asset._table + "." + _g.d.as_asset._as_type, _g.d.as_asset._as_type, SMLReport._report._cellAlign.Left);
                        _view1._addColumn(__assetObject, 75, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "type_name", "as_resource.type_name", "type_name", SMLReport._report._cellAlign.Left);
                    }
                    else if (_conditionSortBy == 2)
                    {
                        _view1._addColumn(__assetObject, 25, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.as_asset._as_location, _g.d.as_asset._table + "." + _g.d.as_asset._as_location, _g.d.as_asset._as_location, SMLReport._report._cellAlign.Left);
                        _view1._addColumn(__assetObject, 75, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "location_name", "as_resource.location_name", "location_name", SMLReport._report._cellAlign.Left);
                    }
                    else if (_conditionSortBy == 3)
                    {
                        _view1._addColumn(__assetObject, 100, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.as_asset_detail._as_buy_date, _g.d.as_asset_detail._table + "." + _g.d.as_asset_detail._as_buy_date, _g.d.as_asset_detail._as_buy_date, SMLReport._report._cellAlign.Left);
                    }
                    else if (_conditionSortBy == 4)
                    {
                        _view1._addColumn(__assetObject, 100, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.as_asset_detail._start_calc_date, _g.d.as_asset_detail._table + "." + _g.d.as_asset_detail._start_calc_date, _g.d.as_asset_detail._start_calc_date, SMLReport._report._cellAlign.Left);
                    }
                    //*******************
                    __assetDetailObject = _view1._addObject(_view1._objectList, SMLReport._report._objectType.Detail, true, 8, true, SMLReport._report._columnBorder.Bottom);
                    _view1._addColumn(__assetDetailObject, 10, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.as_asset._code, "as_resource.as_code", _g.d.as_asset._code, SMLReport._report._cellAlign.Left);
                    _view1._addColumn(__assetDetailObject, 30, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.as_asset._name_1, "as_resource.as_name", _g.d.as_asset._name_1, SMLReport._report._cellAlign.Left);
                    _view1._addColumn(__assetDetailObject, 10, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "unit_name", "as_resource.unit_name", "unit_name", SMLReport._report._cellAlign.Left);
                    _view1._addColumn(__assetDetailObject, 10, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "start_calc", "as_resource.by_start_calc_date", "start_calc", SMLReport._report._cellAlign.Left);
                    _view1._addColumn(__assetDetailObject, 10, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "buy_price", "as_resource.buy_price", "buy_price", SMLReport._report._cellAlign.Right);
                    _view1._addColumn(__assetDetailObject, 12, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "rate", "as_resource.rate", "rate", SMLReport._report._cellAlign.Right);
                    _view1._addColumn(__assetDetailObject, 10, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "dead_value", "as_resource.dead_value", "dead_value", SMLReport._report._cellAlign.Right);
                    return true;
                }
            return false;
        }

        bool _view1__loadData()
        {
            if (_getAsset == null)
            {
                _view1._reportProgressBar.Style = ProgressBarStyle.Marquee;
                this.Cursor = Cursors.WaitCursor;
                try
                {
                    string __whereStr = " where ";
                    string __andStr = " and ";
                    StringBuilder __whereAssetDetail = new StringBuilder();
                    StringBuilder __whereAsset = new StringBuilder();
                    if (this._conditionCodeBegin.Length != 0)
                    {
                        if (__whereAssetDetail.Length == 0)
                        {
                            __whereAssetDetail.Append(__whereStr);
                        }
                        __whereAssetDetail.Append(_g.d.as_asset._code + ">=\'" + this._conditionCodeBegin + "\'");
                        if (__whereAsset.Length == 0)
                        {
                            __whereAsset.Append(__whereStr);
                        }
                        __whereAsset.Append(_g.d.as_asset_detail._as_code + ">=\'" + this._conditionCodeBegin + "\'");
                    }
                    if (this._conditionCodeEnd.Length != 0)
                    {
                        if (__whereAssetDetail.Length == 0)
                        {
                            __whereAssetDetail.Append(__whereStr);
                        }
                        else
                        {
                            __whereAssetDetail.Append(__andStr);
                        }
                        __whereAssetDetail.Append(_g.d.as_asset._code + "<=\'" + this._conditionCodeEnd + "\'");
                        if (__whereAsset.Length == 0)
                        {
                            __whereAsset.Append(__whereStr);
                        }
                        else
                        {
                            __whereAsset.Append(__andStr);
                        }
                        __whereAsset.Append(_g.d.as_asset_detail._as_code + "<=\'" + this._conditionCodeEnd + "\'");
                    }
                    //*******************************
                    string __orderByAsset = "";
                    string __orderByAssetDetail = "";
                    if (_conditionSortBy == 0)
                    {
                        __orderByAsset = "select " + _g.d.as_asset._department_code + "," + "(select " + _g.d.erp_department_list._name_1 + " from " + _g.d.erp_department_list._table + " where " + _g.d.erp_department_list._code + "=" + _g.d.as_asset._department_code + ") as section_name" + " from " + _g.d.as_asset._table + __whereAssetDetail.ToString() + " group by " + _g.d.as_asset._department_code;
                        __orderByAssetDetail = "," + _g.d.as_asset._department_code;
                    }
                    else if (_conditionSortBy == 1)
                    {
                        __orderByAsset = "select " + _g.d.as_asset._as_type + "," + "(select " + _g.d.as_asset_type._name_1 + " from " + _g.d.as_asset_type._table + " where " + _g.d.as_asset_type._code + "=" + _g.d.as_asset._as_type + ") as type_name" + " from " + _g.d.as_asset._table + __whereAssetDetail.ToString() + " group by " + _g.d.as_asset._as_type;
                        __orderByAssetDetail = "," + _g.d.as_asset._as_type;
                    }
                    else if (_conditionSortBy == 2)
                    {
                        __orderByAsset = "select " + _g.d.as_asset._as_location + "," + "(select " + _g.d.as_asset_location._name_1 + " from " + _g.d.as_asset_location._table + " where " + _g.d.as_asset_location._code + "=" + _g.d.as_asset._as_location + ") as location_name" + " from " + _g.d.as_asset._table + __whereAssetDetail.ToString() + " group by " + _g.d.as_asset._as_location;
                        __orderByAssetDetail = "," + _g.d.as_asset._as_location;
                    }
                    else if (_conditionSortBy == 3)
                    {
                        __orderByAsset = "select " + _g.d.as_asset_detail._as_buy_date + " from " + _g.d.as_asset_detail._table + __whereAsset.ToString() + " group by " + _g.d.as_asset_detail._as_buy_date;
                        __orderByAssetDetail = ", (select " + _g.d.as_asset_detail._as_buy_date + " from " + _g.d.as_asset_detail._table + " where " + _g.d.as_asset_detail._as_code + "=" + _g.d.as_asset._code + ") as buy_date";
                    }
                    else if (_conditionSortBy == 4)
                    {
                        __orderByAsset = "select " + _g.d.as_asset_detail._start_calc_date + " from " + _g.d.as_asset_detail._table + __whereAsset.ToString() + " group by " + _g.d.as_asset_detail._start_calc_date;
                        __orderByAssetDetail = ", (select " + _g.d.as_asset_detail._start_calc_date + " from " + _g.d.as_asset_detail._table + " where " + _g.d.as_asset_detail._as_code + "=" + _g.d.as_asset._code + ") as startcalc_date";
                    }
                    string __query = __orderByAsset.ToString();
                    _getAsset = _myFrameWork._query(MyLib._myGlobal._databaseName, __query);
                    __query = "select " + _g.d.as_asset._code + "," + _g.d.as_asset._name_1 + __orderByAssetDetail + "," + "(select " + _g.d.ic_unit._name_1 + " from " + _g.d.ic_unit._table + " where " + _g.d.ic_unit._code + "=" + _g.d.as_asset._unit_code + ") as unit_name," +
                        "(select " + _g.d.as_asset_detail._start_calc_date + " from " + _g.d.as_asset_detail._table + " where " + _g.d.as_asset_detail._as_code + "=" + _g.d.as_asset._code + ") as start_calc," +
                        "(select " + _g.d.as_asset_detail._as_buy_price + " from " + _g.d.as_asset_detail._table + " where " + _g.d.as_asset_detail._as_code + "=" + _g.d.as_asset._code + ") as buy_price," +
                        "(select " + _g.d.as_asset_detail._as_rate + " from " + _g.d.as_asset_detail._table + " where " + _g.d.as_asset_detail._as_code + "=" + _g.d.as_asset._code + ") as rate," +
                        "(select " + _g.d.as_asset_detail._as_dead_value + " from " + _g.d.as_asset_detail._table + " where " + _g.d.as_asset_detail._as_code + "=" + _g.d.as_asset._code + ") as dead_value" +
                        " from " + _g.d.as_asset._table + __whereAssetDetail.ToString();
                    _getAssetDetail = _myFrameWork._query(MyLib._myGlobal._databaseName, __query);
                }
                catch
                {
                    this.Cursor = Cursors.Default;
                    _view1._reportProgressBar.Style = ProgressBarStyle.Blocks;
                    return false;
                }
                _view1._reportProgressBar.Style = ProgressBarStyle.Blocks;
            }
            this.Cursor = Cursors.Default;
            return true;
        }

        void _buttonClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        void _buttonBuildReport_Click(object sender, EventArgs e)
        {
            _view1._buildReport(SMLReport._report._reportType.Standard);
        }


        private void _view1_Load(object sender, EventArgs e)
        {

        }

    }
}
