using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLERPASSET._report._sale
{
    public partial class _report : UserControl
    {
        DataSet _getData;
        MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
        SMLReport._report._objectListType __detailObject;
        SMLReport._report._objectListType __bottomObject;
        // Total
        float _totalAsValue = 0;
        float _totalAsCome = 0;
        float _totalDepreciateValue = 0;
        float _totalAfterDepreciate = 0;
        float _totalNetValue = 0;
        float _totalAsTotal = 0;
        //****************
        public _report()
        {
            InitializeComponent();
            // กำหนดว่าในรายงานมี Object กี่แบบ (ต้องเรียงให้เรียบร้อยด้วย จะได้ทำงานตามขั้นตอนไม่กระโดด)
            this._view1._buttonCondition.Enabled = false;
            this._view1._buttonExample.Enabled = false;
            _view1._buttonBuildReport.Click += new EventHandler(_buttonBuildReport_Click);
            _view1._loadData += new SMLReport._report.LoadDataEventHandler(_view1__loadData);
            _view1._getObject += new SMLReport._report.GetObjectEventHandler(_view1__getObject);
            _view1._getDataObject += new SMLReport._report.GetDataObjectEventHandler(_view1__getDataObject);
            _view1._buttonClose.Click += new EventHandler(_buttonClose_Click);
            // หน้ากระดาษ
            _view1._pageSetupDialog.PageSettings.Landscape = true;
            // แสดงผลทันที
            _view1._buildReport(SMLReport._report._reportType.Standard);
        }

        void _buttonClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        void _view1__getDataObject()
        {
            // Index Column
            int __saleDateColumn = _view1._findColumn(__detailObject, _g.d.as_asset_sale._sale_date);
            int __docNoColumn = _view1._findColumn(__detailObject, _g.d.as_asset_sale._doc_no);
            int __asCodeColumn = _view1._findColumn(__detailObject, _g.d.as_asset_sale_detail._as_code);
            int __asNameColumn = _view1._findColumn(__detailObject, "as_name");
            int __buyDateColumn = _view1._findColumn(__detailObject, "buy_date");
            int __asValue = _view1._findColumn(__detailObject, _g.d.as_asset_sale_detail._as_value);
            int __asCome = _view1._findColumn(__detailObject, "as_come");
            int __depreciateValue = _view1._findColumn(__detailObject, _g.d.as_asset_sale_detail._depreciate_value);
            int __afterDepreciate = _view1._findColumn(__detailObject, _g.d.as_asset_sale_detail._after_depreciate);
            int __netValue = _view1._findColumn(__detailObject, _g.d.as_asset_sale_detail._net_value);
            int __asTotal = _view1._findColumn(__detailObject, "as_total");
            //*******************
            SMLReport._report._columnListType __getColumn = (SMLReport._report._columnListType)__detailObject._columnList[0];
            for (int __row = 0; __row < _getData.Tables[0].Rows.Count; __row++)
            {
                SMLReport._report._objectListType __dataObject = _view1._addObject(_view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                _view1._createEmtryColumn(__detailObject, __dataObject);
                // Style
                Font __newFont = new Font(__getColumn._fontData, FontStyle.Bold);
                string __formatNumber = MyLib._myGlobal._getFormatNumber("m02");
                // Get Value
                string __getSaleDateStr = _getData.Tables[0].Rows[__row].ItemArray[0].ToString();
                DateTime __getSaleDate = MyLib._myGlobal._convertDateFromQuery(__getSaleDateStr);
                string __getDocNo = _getData.Tables[0].Rows[__row].ItemArray[1].ToString();
                string __getAsCode = _getData.Tables[0].Rows[__row].ItemArray[2].ToString();
                string __getAsName = _getData.Tables[0].Rows[__row].ItemArray[3].ToString();
                string __getBuyDateStr = _getData.Tables[0].Rows[__row].ItemArray[4].ToString();
                DateTime __getBuyDate = MyLib._myGlobal._convertDateFromQuery(__getBuyDateStr);
                float __getAsValue = (float)Double.Parse(_getData.Tables[0].Rows[__row].ItemArray[5].ToString());
                float __getAsCome = (float)Double.Parse(_getData.Tables[0].Rows[__row].ItemArray[6].ToString());
                float __getDepreciateValue = (float)Double.Parse(_getData.Tables[0].Rows[__row].ItemArray[7].ToString());
                float __getAfterDepreciate = (float)Double.Parse(_getData.Tables[0].Rows[__row].ItemArray[8].ToString());
                float __getNetValue = (float)Double.Parse(_getData.Tables[0].Rows[__row].ItemArray[9].ToString());
                float __getAsTotal = (float)Double.Parse(_getData.Tables[0].Rows[__row].ItemArray[10].ToString());
                // Create Column
                _view1._addDataColumn(__detailObject, __dataObject, __saleDateColumn, MyLib._myGlobal._convertDateToString(__getSaleDate, false, true), null, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                _view1._addDataColumn(__detailObject, __dataObject, __docNoColumn, __getDocNo, null, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                _view1._addDataColumn(__detailObject, __dataObject, __asCodeColumn, __getAsCode, null, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                _view1._addDataColumn(__detailObject, __dataObject, __asNameColumn, __getAsName, null, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                _view1._addDataColumn(__detailObject, __dataObject, __buyDateColumn, MyLib._myGlobal._convertDateToString(__getBuyDate, false, true), null, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                _view1._addDataColumn(__detailObject, __dataObject, __asValue, (__getAsValue == 0) ? "" : string.Format(__formatNumber, __getAsValue), null, SMLReport._report._cellAlign.Right, 0,SMLReport._report._cellType.Number);
                _view1._addDataColumn(__detailObject, __dataObject, __asCome, (__getAsCome == 0) ? "" : string.Format(__formatNumber, __getAsCome), null, SMLReport._report._cellAlign.Right, 0,SMLReport._report._cellType.Number);
                _view1._addDataColumn(__detailObject, __dataObject, __depreciateValue, (__getDepreciateValue == 0) ? "" : string.Format(__formatNumber, __getDepreciateValue), null, SMLReport._report._cellAlign.Right, 0,SMLReport._report._cellType.Number);
                _view1._addDataColumn(__detailObject, __dataObject, __afterDepreciate, (__getAfterDepreciate == 0) ? "" : string.Format(__formatNumber, __getAfterDepreciate), null, SMLReport._report._cellAlign.Right, 0,SMLReport._report._cellType.Number);
                _view1._addDataColumn(__detailObject, __dataObject, __netValue, (__getNetValue == 0) ? "" : string.Format(__formatNumber, __getNetValue), null, SMLReport._report._cellAlign.Right, 0,SMLReport._report._cellType.Number);
                _view1._addDataColumn(__detailObject, __dataObject, __asTotal, (__getAsTotal == 0) ? "" : string.Format(__formatNumber, __getAsTotal), null, SMLReport._report._cellAlign.Right, 0,SMLReport._report._cellType.Number);
                // Total
                _totalAsValue += __getAsValue;
                _totalAsCome += __getAsCome;
                _totalDepreciateValue += __getDepreciateValue;
                _totalAfterDepreciate += __getAfterDepreciate;
                _totalNetValue += __getNetValue;
                _totalAsTotal += __getAsTotal;
                if (__row == (_getData.Tables[0].Rows.Count - 1))
                {
                    __row++;
                    SMLReport._report._objectListType __dataTotalObject = _view1._addObject(_view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                    _view1._createEmtryColumn(__detailObject, __dataTotalObject);
                    _view1._addDataColumn(__detailObject, __dataTotalObject, __saleDateColumn, "", null, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                    _view1._addDataColumn(__detailObject, __dataTotalObject, __docNoColumn, "", null, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                    _view1._addDataColumn(__detailObject, __dataTotalObject, __asCodeColumn, "", null, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                    _view1._addDataColumn(__detailObject, __dataTotalObject, __asNameColumn, "รวม " + __row + " รายการ", __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._columnBorder.Bottom, SMLReport._report._cellType.String);
                    _view1._addDataColumn(__detailObject, __dataTotalObject, __buyDateColumn, "", null, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                    _view1._addDataColumn(__detailObject, __dataTotalObject, __asValue, (_totalAsValue == 0) ? "" : string.Format(__formatNumber, _totalAsValue), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.Bottom, SMLReport._report._cellType.String);
                    _view1._addDataColumn(__detailObject, __dataTotalObject, __asCome, (_totalAsCome == 0) ? "" : string.Format(__formatNumber, _totalAsCome), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.Bottom, SMLReport._report._cellType.String);
                    _view1._addDataColumn(__detailObject, __dataTotalObject, __depreciateValue, (_totalDepreciateValue == 0) ? "" : string.Format(__formatNumber, _totalDepreciateValue), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.Bottom, SMLReport._report._cellType.String);
                    _view1._addDataColumn(__detailObject, __dataTotalObject, __afterDepreciate, (_totalAfterDepreciate == 0) ? "" : string.Format(__formatNumber, _totalAfterDepreciate), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.Bottom, SMLReport._report._cellType.String);
                    _view1._addDataColumn(__detailObject, __dataTotalObject, __netValue, (_totalNetValue == 0) ? "" : string.Format(__formatNumber, _totalNetValue), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.Bottom, SMLReport._report._cellType.String);
                    _view1._addDataColumn(__detailObject, __dataTotalObject, __asTotal, (_totalAsTotal == 0) ? "" : string.Format(__formatNumber, _totalAsTotal), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._columnBorder.Bottom, SMLReport._report._cellType.String);
                }
            }
            // Clear Total
            _totalAsValue = 0;
            _totalAsCome = 0;
            _totalDepreciateValue = 0;
            _totalAfterDepreciate = 0;
            _totalNetValue = 0;
            _totalAsTotal = 0;
        }

        bool _view1__getObject(System.Collections.ArrayList objectList, SMLReport._report._objectType type)
        {
            if (type == SMLReport._report._objectType.Header)
            {
                SMLReport._report._objectListType __headerObject = _view1._addObject(_view1._objectList, SMLReport._report._objectType.Header, true, 0, true, SMLReport._report._columnBorder.None);
                int __newColumn = _view1._addColumn(__headerObject, 100);
                _view1._addCell(__headerObject, __newColumn, true, 0, -1, SMLReport._report._cellType.String, SMLReport._report._reportValueDefault._ltdName, SMLReport._report._cellAlign.Center, _view1._fontHeader1);
                _view1._addCell(__headerObject, __newColumn, true, 1, -1, SMLReport._report._cellType.String, "รายงานสินทรัพย์ที่ถูกขายพร้อมกำไรขั้นต้น", SMLReport._report._cellAlign.Center, _view1._fontHeader2);
                _view1._addCell(__headerObject, __newColumn, true, 1, -1, SMLReport._report._cellType.String, "วันที่พิมพ์ : " + SMLReport._report._reportValueDefault._currentDateTime, SMLReport._report._cellAlign.Left, _view1._fontStandard);
                _view1._addCell(__headerObject, __newColumn, true, 1, -1, SMLReport._report._cellType.String, "หน้า : " + SMLReport._report._reportValueDefault._pageNumber + "/" + SMLReport._report._reportValueDefault._pageTotal, SMLReport._report._cellAlign.Right, _view1._fontStandard);
                return true;
            }
            else
                if (type == SMLReport._report._objectType.Detail)
                {
                    __detailObject = _view1._addObject(_view1._objectList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.Top);
                    _view1._addColumn(__detailObject, 8, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.as_asset_sale._sale_date, _g.d.as_asset_sale._table + "." + _g.d.as_asset_sale._sale_date, _g.d.as_asset_sale._sale_date, SMLReport._report._cellAlign.Left);
                    _view1._addColumn(__detailObject, 8, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.as_asset_sale._doc_no, _g.d.as_asset_sale._table + "." + _g.d.as_asset_sale._doc_no, _g.d.as_asset_sale._doc_no, SMLReport._report._cellAlign.Left);
                    _view1._addColumn(__detailObject, 10, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.as_asset_sale_detail._as_code, _g.d.as_asset_sale_detail._table + "." + _g.d.as_asset_sale_detail._as_code, _g.d.as_asset_sale_detail._as_code, SMLReport._report._cellAlign.Left);
                    _view1._addColumn(__detailObject, 18, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "as_name", "as_resource.as_name", "as_name", SMLReport._report._cellAlign.Left);
                    _view1._addColumn(__detailObject, 8, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "buy_date", "as_resource.by_buy_date", "buy_date", SMLReport._report._cellAlign.Left);
                    _view1._addColumn(__detailObject, 8, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.as_asset_sale_detail._as_value, _g.d.as_asset_sale_detail._table + "." + _g.d.as_asset_sale_detail._as_value, _g.d.as_asset_sale_detail._as_value, SMLReport._report._cellAlign.Right);
                    _view1._addColumn(__detailObject, 8, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "as_come", "as_resource.as_come", "as_come", SMLReport._report._cellAlign.Right);
                    _view1._addColumn(__detailObject, 8, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.as_asset_sale_detail._depreciate_value, _g.d.as_asset_sale_detail._table + "." + _g.d.as_asset_sale_detail._depreciate_value, _g.d.as_asset_sale_detail._depreciate_value, SMLReport._report._cellAlign.Right);
                    _view1._addColumn(__detailObject, 8, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.as_asset_sale_detail._after_depreciate, _g.d.as_asset_sale_detail._table + "." + _g.d.as_asset_sale_detail._after_depreciate, _g.d.as_asset_sale_detail._after_depreciate, SMLReport._report._cellAlign.Right);
                    _view1._addColumn(__detailObject, 8, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, _g.d.as_asset_sale_detail._net_value, "as_resource.sale_price", _g.d.as_asset_sale_detail._net_value, SMLReport._report._cellAlign.Right);
                    _view1._addColumn(__detailObject, 8, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "as_total", "as_resource.profit_loss", "as_total", SMLReport._report._cellAlign.Right);
                    __bottomObject = _view1._addObject(_view1._objectList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.Bottom);
                    return true;
                }
            return false;
        }

        bool _view1__loadData()
        {
            if (_getData == null)
            {
                try
                {
                    _getData = _myFrameWork._query(MyLib._myGlobal._databaseName, "select s." + _g.d.as_asset_sale._sale_date + ",s." + _g.d.as_asset_sale._doc_no + ",sd." + _g.d.as_asset_sale_detail._as_code + ",(select " + _g.d.as_asset._name_1 + " from " + _g.d.as_asset._table + " where " + _g.d.as_asset._code + "=sd." + _g.d.as_asset_sale_detail._as_code + ") as as_name," + 
                        "(select " + _g.d.as_asset_detail._as_buy_date + " from " + _g.d.as_asset_detail._table + " where " + _g.d.as_asset_detail._as_code + "=sd." + _g.d.as_asset_sale_detail._as_code + ") as buy_date," +
                        "sd." + _g.d.as_asset_sale_detail._as_value + ",sd." + _g.d.as_asset_sale_detail._as_value + " as as_come,sd." + _g.d.as_asset_sale_detail._depreciate_value + ",sd." + _g.d.as_asset_sale_detail._after_depreciate + "," +
                        "sd." + _g.d.as_asset_sale_detail._net_value + ",sd." + _g.d.as_asset_sale_detail._net_value + "-sd." + _g.d.as_asset_sale_detail._after_depreciate + " as as_total" + " from " + _g.d.as_asset_sale_detail._table + " as sd INNER JOIN " + _g.d.as_asset_sale._table + " as s ON sd." + _g.d.as_asset_sale_detail._doc_no + "=s." +_g.d.as_asset_sale._doc_no);
                }
                catch
                {
                    return false;
                }
            }
            return true;
        }

        void _buttonBuildReport_Click(object sender, EventArgs e)
        {
            _view1._reportProgressBar.Style = ProgressBarStyle.Marquee;
            _view1._buildReport(SMLReport._report._reportType.Standard);
            _view1._reportProgressBar.Style = ProgressBarStyle.Blocks;
        }
    }
}
