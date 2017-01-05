using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLERPASSET._report._maintain
{
    public partial class _report : UserControl
    {
        DataSet _getMaintain;
        DataSet _getMaintainDetail;
        MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
        SMLReport._report._objectListType __maintainObject;
        SMLReport._report._objectListType __maintainDetailObject;
        // Condition
        _condition _conditionScreen = new _condition();
        string _conditionCodeBegin = "";
        string _conditionCodeEnd = "";
        DateTime _conditionDateBegin;
        DateTime _conditionDateEnd;
        // Total
        float _totalMaintainPrice = 0;
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

        void _view1__getDataObject()
        {
            // Index Asset
            int __assetCode = _view1._findColumn(__maintainObject, _g.d.as_asset_maintenance_detail._as_code);
            int __assetName = _view1._findColumn(__maintainObject, "as_name");
            // Index Maintain
            int __maintainDate = _view1._findColumn(__maintainDetailObject, _g.d.as_asset_maintenance_detail._maintain_date);
            int __maintainDocNo = _view1._findColumn(__maintainDetailObject, _g.d.as_asset_maintenance_detail._doc_no);
            int __maintainRemark = _view1._findColumn(__maintainDetailObject, _g.d.as_asset_maintenance_detail._remark);
            int __maintainPrice = _view1._findColumn(__maintainDetailObject, _g.d.as_asset_maintenance_detail._maintain_price);
            //*********************
            SMLReport._report._columnListType __getColumn = (SMLReport._report._columnListType)__maintainObject._columnList[0];
            for (int __row = 0; __row < _getMaintain.Tables[0].Rows.Count; __row++)
            {
                SMLReport._report._objectListType __dataObject = _view1._addObject(_view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                _view1._createEmtryColumn(__maintainObject, __dataObject);
                Font __newFont = new Font(__getColumn._fontData, FontStyle.Bold);
                string __formatNumber = MyLib._myGlobal._getFormatNumber("m02");
                //*************
                string __getAssetCode = _getMaintain.Tables[0].Rows[__row].ItemArray[0].ToString();
                string __getAssetName = _getMaintain.Tables[0].Rows[__row].ItemArray[1].ToString();
                _view1._addDataColumn(__maintainObject, __dataObject, __assetCode, __getAssetCode, __newFont, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                _view1._addDataColumn(__maintainObject, __dataObject, __assetName, __getAssetName, __newFont, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                DataRow[] __getMaintainDetail = _getMaintainDetail.Tables[0].Select(_g.d.as_asset_maintenance_detail._as_code + "=\'" + __getAssetCode + "\'");
                //*****************
                for (int __rowDetail = 0; __rowDetail < __getMaintainDetail.Length; __rowDetail++)
                {
                    SMLReport._report._objectListType __dataDetailObject = _view1._addObject(_view1._objectDataList, SMLReport._report._objectType.Detail, true, 8, true, SMLReport._report._columnBorder.None);
                    _view1._createEmtryColumn(__maintainDetailObject, __dataDetailObject);
                    string __getMaintainDateStr = __getMaintainDetail[__rowDetail].ItemArray[1].ToString();
                    DateTime __getMaintainDate = MyLib._myGlobal._convertDateFromQuery(__getMaintainDateStr);
                    string __getMaintainDocNo = __getMaintainDetail[__rowDetail].ItemArray[2].ToString();
                    string __getMaintainRemark = __getMaintainDetail[__rowDetail].ItemArray[3].ToString();
                    float __getMaintainPrice = (float)Double.Parse(__getMaintainDetail[__rowDetail].ItemArray[4].ToString());
                    //*****************
                    _view1._addDataColumn(__maintainDetailObject, __dataDetailObject, __maintainDate, MyLib._myGlobal._convertDateToString(__getMaintainDate, false, true), null, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                    _view1._addDataColumn(__maintainDetailObject, __dataDetailObject, __maintainDocNo, __getMaintainDocNo, null, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                    _view1._addDataColumn(__maintainDetailObject, __dataDetailObject, __maintainRemark, __getMaintainRemark, null, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                    _view1._addDataColumn(__maintainDetailObject, __dataDetailObject, __maintainPrice, (__getMaintainPrice == 0) ? "" : string.Format(__formatNumber, __getMaintainPrice), null, SMLReport._report._cellAlign.Right, 0,SMLReport._report._cellType.Number);
                    // Total
                    _totalMaintainPrice += __getMaintainPrice;
                    if (__rowDetail == (__getMaintainDetail.Length - 1))
                    {
                        __rowDetail++;
                        SMLReport._report._objectListType __dataTotalObject = _view1._addObject(_view1._objectDataList, SMLReport._report._objectType.Detail, true, 8, true, SMLReport._report._columnBorder.None);
                        _view1._createEmtryColumn(__maintainDetailObject, __dataTotalObject);
                        _view1._addDataColumn(__maintainDetailObject, __dataTotalObject, __maintainDate, "", null, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                        _view1._addDataColumn(__maintainDetailObject, __dataTotalObject, __maintainDocNo, "", null, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                        _view1._addDataColumn(__maintainDetailObject, __dataTotalObject, __maintainRemark, "รวม " + __rowDetail + " รายการ", __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._columnBorder.Bottom, SMLReport._report._cellType.String);
                        _view1._addDataColumn(__maintainDetailObject, __dataTotalObject, __maintainPrice, (_totalMaintainPrice == 0) ? "" : string.Format(__formatNumber, _totalMaintainPrice), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.Bottom, SMLReport._report._cellType.Number);
                    }
                }
                // Clear Total
                _totalMaintainPrice = 0;
            }
        }

        bool _view1__getObject(System.Collections.ArrayList objectList, SMLReport._report._objectType type)
        {
            if (type == SMLReport._report._objectType.Header)
            {
                SMLReport._report._objectListType __headerObject = _view1._addObject(_view1._objectList, SMLReport._report._objectType.Header, true, 0, true, SMLReport._report._columnBorder.None);
                int __newColumn = _view1._addColumn(__headerObject, 100);
                _view1._addCell(__headerObject, __newColumn, true, 0, -1, SMLReport._report._cellType.String, SMLReport._report._reportValueDefault._ltdName, SMLReport._report._cellAlign.Center, _view1._fontHeader1);
                _view1._addCell(__headerObject, __newColumn, true, 1, -1, SMLReport._report._cellType.String, "รายงานการซ่อมบำรุงสินทรัพย์", SMLReport._report._cellAlign.Center, _view1._fontHeader2);
                _view1._addCell(__headerObject, __newColumn, true, 2, -1, SMLReport._report._cellType.String, "&LTD_ADDRESS3&", SMLReport._report._cellAlign.Center, _view1._fontStandard);
                _view1._addCell(__headerObject, __newColumn, true, 2, -1, SMLReport._report._cellType.String, "วันที่พิมพ์ : " + SMLReport._report._reportValueDefault._currentDateTime, SMLReport._report._cellAlign.Left, _view1._fontStandard);
                _view1._addCell(__headerObject, __newColumn, true, 2, -1, SMLReport._report._cellType.String, "หน้า : " + SMLReport._report._reportValueDefault._pageNumber + "/" + SMLReport._report._reportValueDefault._pageTotal, SMLReport._report._cellAlign.Right, _view1._fontStandard);
                return true;
            }
            else
                if (type == SMLReport._report._objectType.Detail)
                {
                    __maintainObject = _view1._addObject(_view1._objectList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.Top);
                    _view1._addColumn(__maintainObject, 25, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.as_asset_maintenance_detail._as_code, _g.d.as_asset_maintenance_detail._table + "." + _g.d.as_asset_maintenance_detail._as_code, _g.d.as_asset_maintenance_detail._as_code, SMLReport._report._cellAlign.Left);
                    _view1._addColumn(__maintainObject, 75, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "as_name", "as_resource.as_name", "as_name", SMLReport._report._cellAlign.Left);
                    //*******************
                    __maintainDetailObject = _view1._addObject(_view1._objectList, SMLReport._report._objectType.Detail, true, 8, true, SMLReport._report._columnBorder.Bottom);
                    _view1._addColumn(__maintainDetailObject, 15, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.as_asset_maintenance_detail._maintain_date, _g.d.as_asset_maintenance_detail._table + "." + _g.d.as_asset_maintenance_detail._maintain_date, _g.d.as_asset_maintenance_detail._maintain_date, SMLReport._report._cellAlign.Left);
                    _view1._addColumn(__maintainDetailObject, 15, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.as_asset_maintenance_detail._doc_no, _g.d.as_asset_maintenance_detail._table + "." + _g.d.as_asset_maintenance_detail._doc_no, _g.d.as_asset_maintenance_detail._doc_no, SMLReport._report._cellAlign.Left);
                    _view1._addColumn(__maintainDetailObject, 50, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.as_asset_maintenance_detail._remark, _g.d.as_asset_maintenance_detail._table + "." + _g.d.as_asset_maintenance_detail._remark, _g.d.as_asset_maintenance_detail._remark, SMLReport._report._cellAlign.Left);
                    _view1._addColumn(__maintainDetailObject, 12, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.as_asset_maintenance_detail._maintain_price, _g.d.as_asset_maintenance_detail._table + "." + _g.d.as_asset_maintenance_detail._maintain_price, _g.d.as_asset_maintenance_detail._maintain_price, SMLReport._report._cellAlign.Right);
                    return true;
                }
            return false;
        }

        void _buttonClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        bool _view1__loadData()
        {
            if (_getMaintain == null)
            {
                _view1._reportProgressBar.Style = ProgressBarStyle.Marquee;
                this.Cursor = Cursors.WaitCursor;
                try
                {
                    string __whereStr = " where ";
                    string __andStr = " and ";
                    StringBuilder __whereMaintain = new StringBuilder();
                    StringBuilder __whereCode = new StringBuilder();
                    StringBuilder __whereDate = new StringBuilder();
                    if (this._conditionCodeBegin.Length != 0)
                    {
                        __whereCode.Append(_g.d.as_asset_maintenance_detail._as_code + ">=\'" + this._conditionCodeBegin + "\'");
                    }
                    if (this._conditionCodeEnd.Length != 0)
                    {
                        if (__whereCode.Length != 0)
                        {
                            __whereCode.Append(__andStr);
                        }
                        __whereCode.Append(_g.d.as_asset_maintenance_detail._as_code + "<=\'" + this._conditionCodeEnd + "\'");
                    }
                    if (this._conditionDateBegin.Year != 1000)
                    {
                        __whereDate.Append(_g.d.as_asset_maintenance_detail._maintain_date + ">=\'" + MyLib._myGlobal._convertDateToQuery(this._conditionDateBegin) + "\'");
                    }
                    if (this._conditionDateEnd.Year != 1000)
                    {
                        if (__whereDate.Length != 0)
                        {
                            __whereDate.Append(__andStr);
                        }
                        __whereDate.Append(_g.d.as_asset_maintenance_detail._maintain_date + "<=\'" + MyLib._myGlobal._convertDateToQuery(this._conditionDateEnd) + "\'");
                    }
                    // ประกอบคิวรี่
                    if (__whereCode.Length != 0)
                    {
                        __whereMaintain.Append(__whereStr);
                        __whereMaintain.Append(__whereCode);
                    }
                    if (__whereDate.Length != 0)
                    {
                        if (__whereMaintain.Length != 0)
                        {
                            __whereMaintain.Append(__andStr);
                            __whereMaintain.Append(__whereDate);
                        }
                        else
                        {
                            __whereMaintain.Append(__whereStr);
                            __whereMaintain.Append(__whereDate);
                        }
                    }
                    //*******************************
                    string __query = "select DISTINCT " + _g.d.as_asset_maintenance_detail._as_code + "," + "(select " + _g.d.as_asset._name_1 + " from " + _g.d.as_asset._table + " where " + _g.d.as_asset._code + "=" + _g.d.as_asset_maintenance_detail._as_code + ") as as_name" + " from " + _g.d.as_asset_maintenance_detail._table + __whereMaintain.ToString() + " order by " + _g.d.as_asset_maintenance_detail._as_code;
                    _getMaintain = _myFrameWork._query(MyLib._myGlobal._databaseName, __query);
                    __query = "select " + _g.d.as_asset_maintenance_detail._as_code + "," + _g.d.as_asset_maintenance_detail._maintain_date + "," + _g.d.as_asset_maintenance_detail._doc_no + ","+
                        _g.d.as_asset_maintenance_detail._remark+","+_g.d.as_asset_maintenance_detail._maintain_price+
                        " from " + _g.d.as_asset_maintenance_detail._table + __whereMaintain.ToString();
                    _getMaintainDetail = _myFrameWork._query(MyLib._myGlobal._databaseName, __query);
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

        void _buttonBuildReport_Click(object sender, EventArgs e)
        {
            _view1._buildReport(SMLReport._report._reportType.Standard);
        }

        void _showCondition()
        {
            _conditionScreen.ShowDialog();
            _conditionCodeBegin = this._conditionScreen._screenAssetCondition1._getDataStr("as_resource.from_asset");
            _conditionCodeEnd = this._conditionScreen._screenAssetCondition1._getDataStr("as_resource.to_asset");
            _conditionDateBegin = MyLib._myGlobal._convertDate(this._conditionScreen._screenAssetCondition1._getDataStr("as_resource.date_begin"));
            _conditionDateEnd = MyLib._myGlobal._convertDate(this._conditionScreen._screenAssetCondition1._getDataStr("as_resource.date_end"));
            _getMaintain = null;
            _getMaintainDetail = null;
        }

        void _buttonCondition_Click(object sender, EventArgs e)
        {
            _showCondition();
        }
    }
}
