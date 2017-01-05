using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Xml;
using System.IO;
using System.Globalization;

namespace SMLERPASSET._report._depreciate
{
    public partial class _report : UserControl
    {
        ArrayList __getFromProcess;
        DataSet _getAsset;
        DataSet _getDepreciate;
        MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
        SMLReport._report._objectListType __depreciateObject;
        SMLReport._report._objectListType __sortByObject;
        // Condition
        _condition _conditionScreen = new _condition();
        DateTime _conditionDateBegin;
        DateTime _conditionDateEnd;
        string _conditionCodeBegin = "";
        string _conditionCodeEnd = "";
        int _conditionSortBy = 0;
        int _conditionProcessBy = 0;
        int _conditionProcessMode = 0;
        bool _pngd50 = false;
        // Total Column
        int _totalColumnCreate = 0;
        float _totalCome = 0;
        float _totalResult = 0;
        float _totalValueGo = 0;
        // Format
        string _formatNumber = MyLib._myGlobal._getFormatNumber("m02");
        //**************
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
            // หน้ากระดาษ
            _view1._pageSetupDialog.PageSettings.Landscape = true;
            //****************
            _showCondition();
        }

        void _buttonClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        void _view1__getDataObject()
        {
            int __sectionCode = 0;
            int __sectionName = 0;
            int __typeCode = 0;
            int __typeName = 0;
            int __locationCode = 0;
            int __locationName = 0;
            // Index Top
            if (_conditionSortBy == 0)
            {
                __sectionCode = _view1._findColumn(__sortByObject, _g.d.as_asset._department_code);
                __sectionName = _view1._findColumn(__sortByObject, "section_name");
            }
            else if (_conditionSortBy == 1)
            {
                __typeCode = _view1._findColumn(__sortByObject, _g.d.as_asset._as_type);
                __typeName = _view1._findColumn(__sortByObject, "type_name");
            }
            else if (_conditionSortBy == 2)
            {
                __locationCode = _view1._findColumn(__sortByObject, _g.d.as_asset._as_location);
                __locationName = _view1._findColumn(__sortByObject, "location_name");
            }
            // Index Data
            int __columnAssetCodeData = _getDepreciate.Tables[0].Columns.IndexOf("as_code");
            int __columnAssetNameData = _getDepreciate.Tables[0].Columns.IndexOf("as_name");
            int __columnAssetComeData = _getDepreciate.Tables[0].Columns.IndexOf("as_come");
            int __columnAssetResultData = _getDepreciate.Tables[0].Columns.IndexOf("as_result");
            int __columnAssetValueGoData = _getDepreciate.Tables[0].Columns.IndexOf("as_valuego");
            // Index Column
            int __columnAssetCode = _view1._findColumn(__depreciateObject, "as_code");
            int __columnAssetName = _view1._findColumn(__depreciateObject, "as_name");
            int __columnAssetCome = _view1._findColumn(__depreciateObject, "as_come");
            int __columnAssetResult = _view1._findColumn(__depreciateObject, "as_result");
            int __columnAssetValueGo = _view1._findColumn(__depreciateObject, "as_valuego");
            //*********************
            SMLReport._report._columnListType __getColumn = (SMLReport._report._columnListType)__sortByObject._columnList[0];
            for (int __row = 0; __row < _getAsset.Tables[0].Rows.Count; __row++)
            {
                SMLReport._report._objectListType __dataObject = _view1._addObject(_view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                _view1._createEmtryColumn(__sortByObject, __dataObject);
                Font __newFont = new Font(__getColumn._fontData, FontStyle.Bold);
                //*************
                DataRow[] __getDepreciateDetail = null;
                if (_conditionSortBy == 0)
                {
                    string __getSectionCode = _getAsset.Tables[0].Rows[__row].ItemArray[0].ToString();
                    string __getSectionName = _getAsset.Tables[0].Rows[__row].ItemArray[1].ToString();
                    _view1._addDataColumn(__sortByObject, __dataObject, __sectionCode, __getSectionCode, __newFont, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                    _view1._addDataColumn(__sortByObject, __dataObject, __sectionName, __getSectionName, __newFont, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                    __getDepreciateDetail = _getDepreciate.Tables[0].Select("as_report=\'" + __getSectionCode + "\'");
                }
                else if (_conditionSortBy == 1)
                {
                    string __getTypeCode = _getAsset.Tables[0].Rows[__row].ItemArray[0].ToString();
                    string __getTypeName = _getAsset.Tables[0].Rows[__row].ItemArray[1].ToString();
                    _view1._addDataColumn(__sortByObject, __dataObject, __typeCode, __getTypeCode, __newFont, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                    _view1._addDataColumn(__sortByObject, __dataObject, __typeName, __getTypeName, __newFont, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                    __getDepreciateDetail = _getDepreciate.Tables[0].Select("as_report=\'" + __getTypeCode + "\'");
                }
                else if (_conditionSortBy == 2)
                {
                    string __getLocationCode = _getAsset.Tables[0].Rows[__row].ItemArray[0].ToString();
                    string __getLocationName = _getAsset.Tables[0].Rows[__row].ItemArray[1].ToString();
                    _view1._addDataColumn(__sortByObject, __dataObject, __locationCode, __getLocationCode, __newFont, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                    _view1._addDataColumn(__sortByObject, __dataObject, __locationName, __getLocationName, __newFont, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                    __getDepreciateDetail = _getDepreciate.Tables[0].Select("as_report=\'" + __getLocationCode + "\'");
                }
                //*********************
                float[] _totalColumnValue = new float[_totalColumnCreate+1];
                for (int __rowDetail = 0; __rowDetail < __getDepreciateDetail.Length; __rowDetail++)
                {
                    SMLReport._report._objectListType __dataDetailObject = _view1._addObject(_view1._objectDataList, SMLReport._report._objectType.Detail, true, 3, true, SMLReport._report._columnBorder.None);
                    _view1._createEmtryColumn(__depreciateObject, __dataDetailObject);
                    //*************
                    string __getAssetCode = __getDepreciateDetail[__rowDetail].ItemArray[__columnAssetCodeData].ToString();
                    string __getAssetName = __getDepreciateDetail[__rowDetail].ItemArray[__columnAssetNameData].ToString();
                    float _dCome = (float)Double.Parse(__getDepreciateDetail[__rowDetail].ItemArray[__columnAssetComeData].ToString());
                    float _dResult = (float)Double.Parse(__getDepreciateDetail[__rowDetail].ItemArray[__columnAssetResultData].ToString());
                    float _dValueGo = (float)Double.Parse(__getDepreciateDetail[__rowDetail].ItemArray[__columnAssetValueGoData].ToString());
                    _view1._addDataColumn(__depreciateObject, __dataDetailObject, __columnAssetCode, __getAssetCode, null, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                    _view1._addDataColumn(__depreciateObject, __dataDetailObject, __columnAssetName, __getAssetName, null, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                    _view1._addDataColumn(__depreciateObject, __dataDetailObject, __columnAssetCome, (_dCome == 0) ? "" : string.Format(_formatNumber, _dCome), null, SMLReport._report._cellAlign.Right, 0,SMLReport._report._cellType.Number);
                    for (int __coladd = 0; __coladd <= _totalColumnCreate; __coladd++)
                    {
                        float _dDate = (float)Double.Parse(__getDepreciateDetail[__rowDetail].ItemArray[__coladd + 4].ToString());
                        _view1._addDataColumn(__depreciateObject, __dataDetailObject, __coladd + 3, (_dDate == 0) ? "" : string.Format(_formatNumber, _dDate), null, SMLReport._report._cellAlign.Right, 0,SMLReport._report._cellType.Number);
                        _totalColumnValue[__coladd] += _dDate;
                    }
                    _view1._addDataColumn(__depreciateObject, __dataDetailObject, __columnAssetResult, (_dResult == 0) ? "" : string.Format(_formatNumber, _dResult), null, SMLReport._report._cellAlign.Right, 0,SMLReport._report._cellType.Number);
                    _view1._addDataColumn(__depreciateObject, __dataDetailObject, __columnAssetValueGo, (_dValueGo == 0) ? "" : string.Format(_formatNumber, _dValueGo), null, SMLReport._report._cellAlign.Right, 0,SMLReport._report._cellType.Number);
                    // Total
                    _totalCome += _dCome;
                    _totalResult += _dResult;
                    _totalValueGo += _dValueGo;
                    if (__rowDetail == (__getDepreciateDetail.Length - 1))
                    {
                        __rowDetail++;
                        SMLReport._report._objectListType __dataTotalObject = _view1._addObject(_view1._objectDataList, SMLReport._report._objectType.Detail, true, 3, true, SMLReport._report._columnBorder.None);
                        _view1._createEmtryColumn(__depreciateObject, __dataTotalObject);
                        _view1._addDataColumn(__depreciateObject, __dataTotalObject, __columnAssetCode, "", null, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                        _view1._addDataColumn(__depreciateObject, __dataTotalObject, __columnAssetName, "รวม " + __rowDetail + " รายการ", __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._columnBorder.Bottom, SMLReport._report._cellType.String);
                        _view1._addDataColumn(__depreciateObject, __dataTotalObject, __columnAssetCome, (_totalCome == 0) ? "" : string.Format(_formatNumber, _totalCome), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.Bottom, SMLReport._report._cellType.Number);
                        for (int __coltotal = 0; __coltotal <= _totalColumnCreate; __coltotal++)
                        {
                            _view1._addDataColumn(__depreciateObject, __dataTotalObject, __coltotal + 3, (_totalColumnValue[__coltotal] == 0) ? "" : string.Format(_formatNumber, _totalColumnValue[__coltotal]), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.Bottom, SMLReport._report._cellType.Number);
                        }
                        _view1._addDataColumn(__depreciateObject, __dataTotalObject, __columnAssetResult, (_totalResult == 0) ? "" : string.Format(_formatNumber, _totalResult), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.Bottom, SMLReport._report._cellType.Number);
                        _view1._addDataColumn(__depreciateObject, __dataTotalObject, __columnAssetValueGo, (_totalValueGo == 0) ? "" : string.Format(_formatNumber, _totalValueGo), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.Bottom, SMLReport._report._cellType.Number);
                    }             
                }
                // Clear Total
                _totalCome = 0;
                _totalResult = 0;
                _totalValueGo = 0;
                for (int __colclear = 0; __colclear <= _totalColumnCreate; __colclear++)
                {
                    _totalColumnValue[__colclear] = 0;
                }
            }
        }

        bool _view1__getObject(System.Collections.ArrayList objectList, SMLReport._report._objectType type)
        {
            if (type == SMLReport._report._objectType.Header)
            {
                SMLReport._report._objectListType __headerObject = _view1._addObject(_view1._objectList, SMLReport._report._objectType.Header, true, 0, true, SMLReport._report._columnBorder.None);
                int __newColumn = _view1._addColumn(__headerObject, 100);
                _view1._addCell(__headerObject, __newColumn, true, 0, -1, SMLReport._report._cellType.String, SMLReport._report._reportValueDefault._ltdName, SMLReport._report._cellAlign.Center, _view1._fontHeader1);
                if (_pngd50)
                {
                    _view1._addCell(__headerObject, __newColumn, true, 1, -1, SMLReport._report._cellType.String, "รายงานค่าเสื่อมราคาสินทรัพย์", SMLReport._report._cellAlign.Center, _view1._fontHeader2);
                }
                else
                {
                    _view1._addCell(__headerObject, __newColumn, true, 1, -1, SMLReport._report._cellType.String, "รายงานค่าเสื่อมราคาตาม ภ.ง.ด.50", SMLReport._report._cellAlign.Center, _view1._fontHeader2);
                }
                _view1._addCell(__headerObject, __newColumn, true, 2, -1, SMLReport._report._cellType.String, "&LTD_ADDRESS3&", SMLReport._report._cellAlign.Center, _view1._fontStandard);
                _view1._addCell(__headerObject, __newColumn, true, 2, -1, SMLReport._report._cellType.String, "วันที่พิมพ์ : " + SMLReport._report._reportValueDefault._currentDateTime, SMLReport._report._cellAlign.Left, _view1._fontStandard);
                _view1._addCell(__headerObject, __newColumn, true, 2, -1, SMLReport._report._cellType.String, "หน้า : " + SMLReport._report._reportValueDefault._pageNumber + "/" + SMLReport._report._reportValueDefault._pageTotal, SMLReport._report._cellAlign.Right, _view1._fontStandard);
                return true;
            }
            else
                if (type == SMLReport._report._objectType.Detail)
                {
                    __sortByObject = _view1._addObject(_view1._objectList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.Top);
                    if (_conditionSortBy == 0)
                    {
                        _view1._addColumn(__sortByObject, 15, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.as_asset._department_code, _g.d.as_asset._table + "." + _g.d.as_asset._department_code, _g.d.as_asset._department_code, SMLReport._report._cellAlign.Left);
                        _view1._addColumn(__sortByObject, 85, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "section_name", "as_resource.section_name", "section_name", SMLReport._report._cellAlign.Left);
                    }
                    else if (_conditionSortBy == 1)
                    {
                        _view1._addColumn(__sortByObject, 15, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.as_asset._as_type, _g.d.as_asset._table + "." + _g.d.as_asset._as_type, _g.d.as_asset._as_type, SMLReport._report._cellAlign.Left);
                        _view1._addColumn(__sortByObject, 85, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "type_name", "as_resource.type_name", "type_name", SMLReport._report._cellAlign.Left);
                    }
                    else if (_conditionSortBy == 2)
                    {
                        _view1._addColumn(__sortByObject, 15, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.as_asset._as_location, _g.d.as_asset._table + "." + _g.d.as_asset._as_location, _g.d.as_asset._as_location, SMLReport._report._cellAlign.Left);
                        _view1._addColumn(__sortByObject, 85, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "location_name", "as_resource.location_name", "location_name", SMLReport._report._cellAlign.Left);
                    }
                    //*******************
                    __depreciateObject = _view1._addObject(_view1._objectList, SMLReport._report._objectType.Detail, true, 3, true, SMLReport._report._columnBorder.Bottom);
                    _view1._addColumn(__depreciateObject, 8, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "as_code", "as_resource.as_code", "as_code", SMLReport._report._cellAlign.Left);
                    _view1._addColumn(__depreciateObject, 12, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "as_name", "as_resource.as_name", "as_name", SMLReport._report._cellAlign.Left);
                    float __colwidth = (float)77 / (_totalColumnCreate + 4);
                    _view1._addColumn(__depreciateObject, __colwidth, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "as_come", "as_resource.as_come", "as_come", SMLReport._report._cellAlign.Right);
                    DateTime __dateColumnBeginForAdd = _conditionDateBegin;
                    Calendar cal = CultureInfo.CurrentCulture.DateTimeFormat.Calendar;
                    string __columnName = "";
                    for (int __col = 0; __col <= _totalColumnCreate; __col++)
                    {
                        if (_conditionProcessBy == 1)
                        {
                            __columnName = MyLib._myGlobal._convertDateToString(Convert.ToDateTime(__dateColumnBeginForAdd), false);
                            __columnName = __columnName.Substring(0, 2);
                            TimeSpan __timeSpanAdd = new TimeSpan(1, 0, 0, 0);
                            _view1._addColumn(__depreciateObject, __colwidth, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "c" + __col, __columnName, "c" + __col, SMLReport._report._cellAlign.Right);
                            __dateColumnBeginForAdd = __dateColumnBeginForAdd.Add(__timeSpanAdd);
                        }
                        else if (_conditionProcessBy == 2)
                        {
                            __columnName = MyLib._myGlobal._convertDateToString(Convert.ToDateTime(__dateColumnBeginForAdd), false);
                            __columnName = __columnName.Substring(2, __columnName.Length - 7);
                            _view1._addColumn(__depreciateObject, __colwidth, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "c" + __col, __columnName, "c" + __col, SMLReport._report._cellAlign.Right);
                            __dateColumnBeginForAdd = cal.AddMonths(__dateColumnBeginForAdd, 1);
                        }
                        else if (_conditionProcessBy == 3)
                        {
                            __columnName = MyLib._myGlobal._convertDateToString(Convert.ToDateTime(__dateColumnBeginForAdd), false);
                            __columnName = __columnName.Substring(__columnName.Length - 4, 4);
                            _view1._addColumn(__depreciateObject, __colwidth, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "c" + __col, __columnName, "c" + __col, SMLReport._report._cellAlign.Right);
                            __dateColumnBeginForAdd = cal.AddYears(__dateColumnBeginForAdd, 1);
                        }
                    }
                    _view1._addColumn(__depreciateObject, __colwidth, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "as_result", "as_resource.as_result", "as_result", SMLReport._report._cellAlign.Right);
                    _view1._addColumn(__depreciateObject, __colwidth, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "as_valuego", "as_resource.value_go", "as_valuego", SMLReport._report._cellAlign.Right);
                    return true;
                }
            return false;
        }

        bool _view1__loadData()
        {
            if (__getFromProcess == null)
            {
                this.Cursor = Cursors.WaitCursor;
                // ดึงข้อมูลจากเงื่อนไขหน้าจอ
                string __whereStr = " where ";
                string __andStr = " and ";
                StringBuilder __whereAssetDetail = new StringBuilder();
                StringBuilder __whereDepreciate = new StringBuilder();
                if (this._conditionCodeBegin.Length != 0)
                {
                    if (__whereAssetDetail.Length == 0)
                    {
                        __whereAssetDetail.Append(__whereStr);
                    }
                    __whereAssetDetail.Append(_g.d.as_asset._code + ">=\'" + this._conditionCodeBegin + "\'");
                    if (__whereDepreciate.Length == 0)
                    {
                        __whereDepreciate.Append(__whereStr);
                    }
                    __whereDepreciate.Append("ad." + _g.d.as_asset_detail._as_code + ">=\'" + this._conditionCodeBegin + "\'");
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
                    if (__whereDepreciate.Length == 0)
                    {
                        __whereDepreciate.Append(__whereStr);
                    }
                    else
                    {
                        __whereDepreciate.Append(__andStr);
                    }
                    __whereDepreciate.Append("ad." + _g.d.as_asset_detail._as_code + "<=\'" + this._conditionCodeEnd + "\'");
                }
                //*******************************
                string __orderByAsset = "";
                string __orderByDepreciate = "";
                if (_conditionSortBy == 0)
                {
                    __orderByAsset = "select " + _g.d.as_asset._department_code + "," + "(select " + _g.d.erp_department_list._name_1 + " from " + _g.d.erp_department_list._table + " where " + _g.d.erp_department_list._code + "=" + _g.d.as_asset._department_code + ") as section_name" + " from " + _g.d.as_asset._table + __whereAssetDetail.ToString() + " group by " + _g.d.as_asset._department_code;
                    __orderByDepreciate = "(select a." + _g.d.as_asset._department_code + " from " + _g.d.as_asset._table + " a where ad." + _g.d.as_asset_detail._as_code + "=a." + _g.d.as_asset._code + ") as orderby_code,";
                }
                else if (_conditionSortBy == 1)
                {
                    __orderByAsset = "select " + _g.d.as_asset._as_type + "," + "(select " + _g.d.as_asset_type._name_1 + " from " + _g.d.as_asset_type._table + " where " + _g.d.as_asset_type._code + "=" + _g.d.as_asset._as_type + ") as type_name" + " from " + _g.d.as_asset._table + __whereAssetDetail.ToString() + " group by " + _g.d.as_asset._as_type;
                    __orderByDepreciate = "(select a." + _g.d.as_asset._as_type + " from " + _g.d.as_asset._table + " a where ad." + _g.d.as_asset_detail._as_code + "=a." + _g.d.as_asset._code + ") as orderby_code,";
                }
                else if (_conditionSortBy == 2)
                {
                    __orderByAsset = "select " + _g.d.as_asset._as_location + "," + "(select " + _g.d.as_asset_location._name_1 + " from " + _g.d.as_asset_location._table + " where " + _g.d.as_asset_location._code + "=" + _g.d.as_asset._as_location + ") as location_name" + " from " + _g.d.as_asset._table + __whereAssetDetail.ToString() + " group by " + _g.d.as_asset._as_location;
                    __orderByDepreciate = "(select a." + _g.d.as_asset._as_location + " from " + _g.d.as_asset._table + " a where ad." + _g.d.as_asset_detail._as_code + "=a." + _g.d.as_asset._code + ") as orderby_code,";
                }
                string __query = __orderByAsset.ToString();
                _getAsset = _myFrameWork._query(MyLib._myGlobal._databaseName, __query);
                // ข้อมูลจากการคำนวณ
                SMLProcess._asProcess __process = new SMLProcess._asProcess();
                __getFromProcess = _getDepreciateForProcess(_conditionDateBegin, _conditionDateEnd, _conditionProcessBy, _conditionProcessMode, __orderByDepreciate, __whereDepreciate.ToString());
                if (__getFromProcess.Count > 0)
                {
                    _getDepreciate = (DataSet)__getFromProcess[0];
                }
            }
            this.Cursor = Cursors.Default;
            return true;
        }

        public ArrayList _getDepreciateForProcess(DateTime dateBegin, DateTime dateEnd, int processBy, int processMode, string orderbyDepreciate, string whereReport)
        {
            ArrayList __result = new ArrayList();
            DataSet __ds = new DataSet();
            string getString = "";
            try
            {
                __result.Clear();
                SMLProcess._asProcess __process = new SMLProcess._asProcess();
                getString = __process._asViewDepreciateBalance(dateBegin, dateEnd, processBy, processMode, orderbyDepreciate, whereReport);
                XmlDocument __readXmlDoc = new XmlDocument();
                __readXmlDoc.LoadXml(getString);
                XmlNodeList __tableNodes = __readXmlDoc.SelectNodes("//ResultSet");
                foreach (XmlNode __getTableNode in __tableNodes)
                {
                    DataSet __getData = new DataSet();
                    XmlTextReader __readTableXml = new XmlTextReader(new StringReader("<ResultSet>" + __getTableNode.InnerXml + "</ResultSet>"));
                    try
                    {
                        __getData.ReadXml(__readTableXml, XmlReadMode.InferSchema);
                        if (__getData.Tables.Count == 0)
                        {
                            __getData.Tables.Add(new DataTable());
                        }
                    }
                    catch
                    {
                        __getData.Tables.Add(new DataTable());
                    }
                    __result.Add(__getData);
                }
            }
            catch
            {
            }
            return (__result);
        }

        void _buttonBuildReport_Click(object sender, EventArgs e)
        {
            if (_conditionDateBegin.Year == 1000 || _conditionDateEnd.Year == 1000)
            {
                MessageBox.Show((MyLib._myGlobal._language == 0) ? "กรุณาบันทึก วันที่เริ่มต้น และ วันที่สิ้นสุด ก่อน" : "Please enter startdate and enddate");
            }
            else
            {
                int __dateCompare = DateTime.Compare(_conditionDateBegin, _conditionDateEnd);
                if (__dateCompare > 0)
                {
                    MessageBox.Show((MyLib._myGlobal._language == 0) ? "วันที่สิ้นสุดต้องมากกว่าหรือเท่ากับวันที่เริ่มต้นเท่านั้น" : "Enddate much more than or equal to Begindate");
                }
                else
                {
                    _view1._buildReport(SMLReport._report._reportType.Standard);
                }
            }
        }

        void _buttonCondition_Click(object sender, EventArgs e)
        {
            _showCondition();
        }

        void _showCondition()
        {
            _conditionScreen.ShowDialog();
            // Set Value
            _conditionCodeBegin = this._conditionScreen._screenAssetCondition1._getDataStr("as_resource.from_asset");
            _conditionCodeEnd = this._conditionScreen._screenAssetCondition1._getDataStr("as_resource.to_asset");
            _conditionDateBegin = MyLib._myGlobal._convertDate(this._conditionScreen._screenAssetCondition1._getDataStr("as_resource.date_begin"));
            _conditionDateEnd = MyLib._myGlobal._convertDate(this._conditionScreen._screenAssetCondition1._getDataStr("as_resource.date_end"));
            _conditionSortBy = MyLib._myGlobal._intPhase(this._conditionScreen._screenAssetCondition1._getDataStr("as_resource.order_by"));
            _pngd50 = this._conditionScreen._screenAssetCondition1._getDataStr("as_resource.income_tax_50").Equals("1") ? false : true;
            Control __getControls = this._conditionScreen._screenAssetCondition1._getControl("as_resource.view_by");
            MyLib._myComboBox __getDatas = (MyLib._myComboBox)__getControls;
            _conditionProcessBy = __getDatas.SelectedIndex+1;
            Control __getControl = this._conditionScreen._screenAssetCondition1._getControl("as_resource.process_mode");
            MyLib._myComboBox __getData = (MyLib._myComboBox)__getControl;
            _conditionProcessMode = __getData.SelectedIndex;
            __getFromProcess = null;
            _getAsset = null;
            // คำนวณคอลัมน์ตามช่วงที่เลือก
            DateTime __dateColumnBegin = _conditionDateBegin;
            DateTime __dateColumnEnd = _conditionDateEnd;
            if (_conditionProcessBy == 1)
            {
                TimeSpan __timeSpan = __dateColumnEnd.Subtract(__dateColumnBegin);
                _totalColumnCreate = (int)__timeSpan.TotalDays;
            }
            else if (_conditionProcessBy == 2)
            {
                _totalColumnCreate = 12 * (__dateColumnEnd.Year - __dateColumnBegin.Year) + __dateColumnEnd.Month - __dateColumnBegin.Month;
            }
            else if (_conditionProcessBy == 3)
            {
                _totalColumnCreate = __dateColumnEnd.Year - __dateColumnBegin.Year;
            }
        }
    }
}
