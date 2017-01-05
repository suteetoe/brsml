using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Crom.Controls.Docking;
using System.Collections;

namespace SMLERPSOReport
{
    public partial class _report_sale_drug_13 : UserControl
    {
        private MyLib._myForm _conditionForm = new MyLib._myForm();
        private MyLib._myForm _resultForm = new MyLib._myForm();
        private MyLib._myForm _bookSelectForm = new MyLib._myForm();
        private MyLib._myScreen _condition = new MyLib._myScreen();
        private SMLReport._report._view _view = new SMLReport._report._view();
        private MyLib._myGrid _bookSelectGrid = new MyLib._myGrid();
        private MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
        private List<string> _bookCodeList;
        private DataTable _dataTableMovement;
        private DataTable _dataTableLot;
        private string _dateBegin;
        private string _dateEnd;
        private StringBuilder _lotNumberSelect;
        string _levelNameMain = "cust";
        string _levelNameDoc = "doc";
        string _itemCode = "";

        string _columnNo = "row_number";
        int _columnNumberCount = 0;

        public _report_sale_drug_13()
        {
            InitializeComponent();

            // เงื่อนไข
            this._condition._getResource = false;
            this._condition._maxColumn = 1;
            this._condition._addTextBox(0, 0, 1, 1, _g.d.ic_resource._item_code, 1, 0, 1, true, false, false, false, true, "รหัสสินค้า");
            this._condition._addTextBox(1, 0, 1, 1, _g.d.ic_resource._item_name, 1, 0, 0, true, false, false, true, true, "ชื่อยา");
            this._condition._addTextBox(1, 0, 1, 1, _g.d.ic_resource._mfn_name, 1, 0, 0, true, false, false, true, true, "ชื่อผู้ผลิต");
            this._condition._addTextBox(2, 0, 1, 1, _g.d.ic_resource._ic_size, 1, 0, 0, true, false, false, true, true, "ขนาดบรรจุ");
            this._condition._addTextBox(3, 0, 1, 1, _g.d.ic_resource._ic_special, 1, 0, 0, true, false, false, true, true, "เลขทะเบียนตำรับยา");
            this._condition._addTextBox(4, 0, 1, 1, _g.d.ic_resource._in, 1, 0, 0, true, false, false, true, true, "ได้มาจาก");

            this._condition._setDataStr(_g.d.ic_resource._mfn_name, MyLib._myGlobal._ltdName);
            this._condition._setDataStr(_g.d.ic_resource._in, "ฝ่ายผลิต");

            this._condition._textBoxSearch += _condition__textBoxSearch;
            this._condition._textBoxChanged += _condition__textBoxChanged;
            this._condition._enabedControl(_g.d.ic_resource._item_name, false);

            // รายละเอัยด LOT
            this._bookSelectGrid._table_name = _g.d.ic_resource._table;
            this._bookSelectGrid._isEdit = false;
            this._bookSelectGrid._addColumn("Select", 11, 1, 5);
            this._bookSelectGrid._addColumn(_g.d.ic_resource._lot_number, 1, 20, 10);
            this._bookSelectGrid._addColumn(_g.d.ic_resource._date_expire, 4, 10, 8, false, false);
            this._bookSelectGrid._addColumn(_g.d.ic_resource._many_unit, 2, 10, 8, false, true); // เก็บ max page

            this._bookSelectGrid._calcPersentWidthToScatter();
            //
            //this._bookSelectGrid._loadFromDataTable(this._myFrameWork._queryShort("select 1 as select,* from " + _g.d.erp_pass_book._table + " order by " + _g.d.erp_pass_book._code).Tables[0]);
            //

            this._view._buttonClose.Click += _buttonClose_Click;
            this._view._getObject += new SMLReport._report.GetObjectEventHandler(_view__getObject);
            this._view._getDataObject += new SMLReport._report.GetDataObjectEventHandler(_view__getDataObject);
            this._view._loadDataByThread += _view__loadDataByThread;
            this._view._buttonBuildReport.Click += _buttonBuildReport_Click;
            this._view._beforeReportDrawPaperArgs += _view__beforeReportDrawPaperArgs;
            this._view._buildSuccess += _view__buildSuccess;

            /*
                       this._report._viewControl._buttonCondition.Visible = false;
                       this._report._query += new SMLReport._generate.QueryEventHandler(_viewReport__query);
                       this._report._init += new SMLReport._generate.InitEventHandler(_viewReport__init);
                       this._report._dataRowSelect += new SMLReport._generate.DataRowSelectEventHandler(_viewReport__dataRowSelect);
                       this._report._renderValue += new SMLReport._generate.RenderValueEventHandler(_report__renderValue);
                       this._report._renderHeader += _report__renderHeader;
                       this._report.Disposed += (s1, e1) =>
                       {
                           this.Dispose();
                       };
                       */
            //
            this._conditionForm.Text = "เงื่อนไข";
            this._condition.Dock = DockStyle.Fill;
            this._conditionForm._colorBackground = false;
            this._conditionForm.Controls.Add(this._condition);
            //
            this._bookSelectForm.Text = "หมายเลข LOT";
            this._bookSelectGrid.Dock = DockStyle.Fill;
            this._bookSelectForm._colorBackground = false;
            this._bookSelectForm.Controls.Add(this._bookSelectGrid);
            //
            this._resultForm.Text = "รายงานการขายยาตามที่สำนักงานคณะกรรมการอาหารและยากำหนด";
            this._view.Dock = DockStyle.Fill;
            this._resultForm._colorBackground = false;
            this._resultForm.Controls.Add(this._view);
            //
            DockableFormInfo __formCondition = this._dock.Add(this._conditionForm, Crom.Controls.Docking.zAllowedDock.All, Guid.NewGuid());
            __formCondition.ShowCloseButton = false;
            __formCondition.ShowContextMenuButton = false;
            this._dock.DockForm(__formCondition, DockStyle.Left, zDockMode.Inner);
            //
            DockableFormInfo __formBookSelect = this._dock.Add(this._bookSelectForm, Crom.Controls.Docking.zAllowedDock.All, Guid.NewGuid());
            __formBookSelect.ShowCloseButton = false;
            __formBookSelect.ShowContextMenuButton = false;
            this._dock.DockForm(__formBookSelect, __formCondition, DockStyle.Bottom, zDockMode.Inner);
            //
            DockableFormInfo __formResultGridl = this._dock.Add(this._resultForm, Crom.Controls.Docking.zAllowedDock.All, Guid.NewGuid());
            __formResultGridl.ShowCloseButton = false;
            __formResultGridl.ShowContextMenuButton = false;
            this._dock.DockForm(__formResultGridl, DockStyle.Fill, zDockMode.Inner);
        }

        List<int> _pageList = new List<int>();

        private void _view__buildSuccess()
        {
            // build success
            _pageList.Clear();

            string __lotNumberInPage = "";
            int __pageIndex = 0;

            for (int page = 0; page < this._view._paper._pageList.Count; page++)
            {
                SMLReport._report._pageListType reportPage = (SMLReport._report._pageListType)this._view._paper._pageList[page];

                string __getLotNumber = ((SMLReport._report._columnDataListType)((SMLReport._report._objectListType)reportPage._objectList[4])._columnList[8])._text;

                if (__getLotNumber != __lotNumberInPage)
                    __pageIndex = 0;


                __pageIndex++;
                _pageList.Add(__pageIndex);

                __lotNumberInPage = __getLotNumber;

            }
            // 

            // หา max page per lot 
            for (int __row = 0; __row < this._dataTableLot.Rows.Count; __row++)
            {
                string __lotNumber = this._dataTableLot.Rows[__row]["lot_number"].ToString();
                int __lotInPage = 0;

                for (int page = 0; page < this._view._paper._pageList.Count; page++)
                {
                    SMLReport._report._pageListType reportPage = (SMLReport._report._pageListType)this._view._paper._pageList[page];
                    string __getLotNumber = ((SMLReport._report._columnDataListType)((SMLReport._report._objectListType)reportPage._objectList[4])._columnList[8])._text;

                    if (__lotNumber == __getLotNumber)
                    {
                        __lotInPage++;
                    }
                }

                // uppdate max page in lot
                int __lotGridRow = this._bookSelectGrid._findData(this._bookSelectGrid._findColumnByName(_g.d.ic_resource._lot_number), __lotNumber);
                if (__lotGridRow != -1)
                {
                    this._bookSelectGrid._cellUpdate(__lotGridRow, _g.d.ic_resource._many_unit, __lotInPage, true); // เก็บ max page
                }

            }
        }

        private void _view__beforeReportDrawPaperArgs(SMLReport._report._pageListType pageListType)
        {
            for (int loop = 0; loop < pageListType._objectList.Count; loop++)
            {
                SMLReport._report._objectListType __getObject = (SMLReport._report._objectListType)pageListType._objectList[loop];

                if (__getObject._type == SMLReport._report._objectType.Header)
                {
                    if (__getObject._columnList.Count == 1)
                    {
                        string __lotNumber = ((SMLReport._report._columnDataListType)((SMLReport._report._objectListType)pageListType._objectList[4])._columnList[8])._text;
                        int __lotGridRow = this._bookSelectGrid._findData(this._bookSelectGrid._findColumnByName(_g.d.ic_resource._lot_number), __lotNumber);

                        int __maxPage = MyLib._myGlobal._intPhase(this._bookSelectGrid._cellGet(__lotGridRow, _g.d.ic_resource._many_unit).ToString());
                        // header title
                        ((SMLReport._report._cellListType)((SMLReport._report._columnListType)__getObject._columnList[0])._cellList[3])._text = "หน้า : " + _pageList[this._view._paper._pageCurrent] + "/" + __maxPage;
                    }
                    if (__getObject._columnList.Count == 3)
                    {
                        string __lotNumber = ((SMLReport._report._columnDataListType)((SMLReport._report._objectListType)pageListType._objectList[4])._columnList[8])._text;

                        DataRow[] __getLotData = this._dataTableLot.Select("lot_number=\'" + __lotNumber + "\'");

                        if (__getLotData.Length > 0)
                        {
                            decimal __getqtyIn = MyLib._myGlobal._decimalPhase(__getLotData[0]["qty_in"].ToString());
                            DateTime __dateIn = MyLib._myGlobal._convertDateFromQuery(__getLotData[0]["mfd_date"].ToString());

                            string __formatNumber = MyLib._myGlobal._getFormatNumber("m02");

                            ((SMLReport._report._cellListType)((SMLReport._report._columnListType)__getObject._columnList[0])._cellList[3])._text = "จำนวนรับ : " + string.Format(__formatNumber, __getqtyIn) + " " + __getLotData[0]["ic_unit_code"].ToString();

                            ((SMLReport._report._cellListType)((SMLReport._report._columnListType)__getObject._columnList[2])._cellList[1])._text = "เลขที่ LOT ที่ผลิต : " + __lotNumber;
                            ((SMLReport._report._cellListType)((SMLReport._report._columnListType)__getObject._columnList[2])._cellList[3])._text = "วันที่รับ : " + __dateIn.ToShortDateString();
                        }
                    }
                }
            }
        }

        private void _buttonBuildReport_Click(object sender, EventArgs e)
        {
            string __getItemCode = this._condition._getDataStr(_g.d.ic_resource._item_code);
            if (__getItemCode.Length > 0)
                _view._buildReport(SMLReport._report._reportType.Standard);
        }

        private void _view__loadDataByThread()
        {
            try
            {

                StringBuilder __myquery = new StringBuilder();
                __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");


                this._lotNumberSelect = new StringBuilder();
                for (int __row = 0; __row < this._bookSelectGrid._rowData.Count; __row++)
                {
                    if ((int)MyLib._myGlobal._decimalPhase(this._bookSelectGrid._cellGet(__row, 0).ToString()) == 1)
                    {
                        string __value = this._bookSelectGrid._cellGet(__row, _g.d.ic_resource._lot_number).ToString().Trim().ToUpper();
                        if (__value.Length > 0)
                        {
                            if (this._lotNumberSelect.Length > 0)
                            {
                                this._lotNumberSelect.Append(",");
                            }
                            this._lotNumberSelect.Append("\'" + __value + "\'");
                        }
                    }
                }

                //  head
                StringBuilder __queryLot1 = new StringBuilder();
                __queryLot1.Append("select 0 as " + _columnNo + " ,ic_code,ic_name,ic_unit_code as ic_unit_code,lot_number,qty_in*(select stand_value/divide_value from ic_unit_use where ic_unit_use.ic_code=temp2.ic_code and ic_unit_use.code=temp2.ic_unit_code) as qty_in, (select  mfd_date from ic_trans_detail_lot where ic_trans_detail_lot.lot_number = temp2.lot_number and ic_trans_detail_lot.item_code=temp2.ic_code and calc_flag = 1 order by doc_date, doc_time  offset 0 limit 1  ) as mfd_date, null as doc_date, 0 as qty_out, '' as ar_name, '' as address, '' as remark  ");
                __queryLot1.Append(" from ( ");
                __queryLot1.Append(" select ic_code,ic_name,ic_unit_code,lot_number,sum(qty_in) as qty_in ");
                __queryLot1.Append(" from ( ");
                __queryLot1.Append(" select item_code as ic_code,coalesce((select name_1 from ic_inventory where ic_inventory.code=ic_trans_detail_lot.item_code),'') as ic_name,coalesce((select unit_cost from ic_inventory where ic_inventory.code=ic_trans_detail_lot.item_code),'') as ic_unit_code ,lot_number as lot_number ,case when calc_flag=1 and trans_flag <> 70 then qty else 0 end as qty_in ");
                __queryLot1.Append(" from ic_trans_detail_lot ");
                __queryLot1.Append(" where item_code = \'" + this._itemCode + "\' ");
                if (this._lotNumberSelect.Length > 0)
                {
                    __queryLot1.Append(" and " + _g.d.ic_trans_detail_lot._lot_number + " in (" + this._lotNumberSelect.ToString() + ") ");
                }
                __queryLot1.Append(" ) as temp1  ");
                __queryLot1.Append(" group by ic_code,ic_name,ic_unit_code,lot_number ");
                __queryLot1.Append(" ) as temp2 order by ic_code,lot_number");

                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__queryLot1.ToString()));


                // detail
                StringBuilder __queryDetail = new StringBuilder();
                __queryDetail.Append(" select 0 as " + _columnNo + ", item_code, (select name_1 from ic_inventory where ic_inventory.code = temp1.item_code)  as item_name, lot_number, 0 as qty_in, null as mfd_date, doc_date, qty as qty_out ");
                __queryDetail.Append(" , (select name_1 from ar_customer where ar_customer.code = temp1.cust_code) as cust_name ");
                __queryDetail.Append(" , (select address from ar_customer where ar_customer.code = temp1.cust_code) as address ");
                __queryDetail.Append(" , (select name_1 from ar_dimension where ar_dimension.code =(select dimension_5 from ar_customer_detail where ar_customer_detail.ar_code = temp1.cust_code)) as ar_type_5 ");
                __queryDetail.Append(" , doc_no, (sale_code  || '~' || (select name_1 from erp_user where erp_user.code = sale_code )) as sale_code ");
                __queryDetail.Append(" from (  ");
                __queryDetail.Append(" select (select cust_code from ic_trans where ic_trans.doc_no = ic_trans_detail_lot.doc_no and ic_trans.trans_flag = ic_trans.trans_flag) as cust_code, item_code, lot_number, doc_date, doc_no, trans_flag , qty ");
                __queryDetail.Append(" , (select sale_code from ic_trans where ic_trans.doc_no = ic_trans_detail_lot.doc_no and ic_trans.trans_flag = ic_trans.trans_flag) as sale_code ");
                __queryDetail.Append(" from ic_trans_detail_lot  ");
                __queryDetail.Append(" where trans_flag = 44  and item_code  = \'" + this._itemCode + "\' ");
                if (this._lotNumberSelect.Length > 0)
                {
                    __queryDetail.Append(" and " + _g.d.ic_trans_detail_lot._lot_number + " in (" + this._lotNumberSelect.ToString() + ") ");
                }

                __queryDetail.Append(" ) as temp1  ");
                __queryDetail.Append(" order by item_code, lot_number, doc_date, doc_no ");

                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__queryDetail.ToString()));


                __myquery.Append("</node>");

                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                //this._dataTableData = __myFrameWork._query(MyLib._myGlobal._databaseName, "").Tables[0];
                ArrayList _getData = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());

                if (_getData.Count > 0)
                {
                    this._dataTableLot = ((DataSet)_getData[0]).Tables[0];
                    this._dataTableMovement = ((DataSet)_getData[1]).Tables[0];

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                this._view._loadDataByThreadSuccess = false;
                return;

            }
            this._view._loadDataByThreadSuccess = true;

        }

        SMLReport._report._objectListType __ojtReport;

        bool _view__getObject(System.Collections.ArrayList objectList, SMLReport._report._objectType type)
        {
            if (type == SMLReport._report._objectType.Header)
            {
                SMLReport._report._objectListType __headerObject = this._view._addObject(this._view._objectList, SMLReport._report._objectType.Header, true, 0, true, SMLReport._report._columnBorder.None);
                int __newColumn = this._view._addColumn(__headerObject, 100);

                this._view._addCell(__headerObject, __newColumn, true, 0, -1, SMLReport._report._cellType.String, "รายงานการขายยาตามที่สำนักงานคณะกรรมการอาหารและยากำหนด", SMLReport._report._cellAlign.Center, this._view._fontHeader1);
                this._view._addCell(__headerObject, __newColumn, true, 1, -1, SMLReport._report._cellType.String, SMLReport._report._reportValueDefault._ltdName, SMLReport._report._cellAlign.Center, this._view._fontHeader2);
                this._view._addCell(__headerObject, __newColumn, true, 2, -1, SMLReport._report._cellType.String, MyLib._myGlobal._ltdAddress.Replace("\n", string.Empty), SMLReport._report._cellAlign.Center, this._view._fontHeader2);

                this._view._addCell(__headerObject, __newColumn, true, 3, -1, SMLReport._report._cellType.String, "หน้า : ", SMLReport._report._cellAlign.Right, this._view._fontHeader2);

                {
                    SMLReport._report._objectListType __headerObject2 = this._view._addObject(this._view._objectList, SMLReport._report._objectType.Header, true, 0, true, SMLReport._report._columnBorder.None);
                    int __column1 = this._view._addColumn(__headerObject2, 35);
                    int __column2 = this._view._addColumn(__headerObject2, 30);
                    int __column3 = this._view._addColumn(__headerObject2, 35);

                    string __getSpecial = this._condition._getDataStr(_g.d.ic_resource._ic_special);
                    string __getSize = this._condition._getDataStr(_g.d.ic_resource._ic_special);

                    this._view._addCell(__headerObject2, __column1, true, 0, -1, SMLReport._report._cellType.String, "ชื่อยา : " + this._condition._getDataStr(_g.d.ic_resource._item_name), SMLReport._report._cellAlign.Left, this._view._fontHeader2);
                    this._view._addCell(__headerObject2, __column3, true, 0, -1, SMLReport._report._cellType.String, "เลขทะเบียนตำรับยา : " + __getSpecial, SMLReport._report._cellAlign.Left, this._view._fontHeader2);

                    this._view._addCell(__headerObject2, __column1, true, 1, -1, SMLReport._report._cellType.String, "ชื่อผู้ผลิต : " + this._condition._getDataStr(_g.d.ic_resource._mfn_name), SMLReport._report._cellAlign.Left, this._view._fontHeader2);
                    this._view._addCell(__headerObject2, __column3, true, 1, -1, SMLReport._report._cellType.String, "เลขที่ LOT ที่ผลิต : ", SMLReport._report._cellAlign.Left, this._view._fontHeader2);

                    this._view._addCell(__headerObject2, __column1, true, 2, -1, SMLReport._report._cellType.String, "ขนาดบรรจุ : " + __getSize, SMLReport._report._cellAlign.Left, this._view._fontHeader2);
                    this._view._addCell(__headerObject2, __column3, true, 2, -1, SMLReport._report._cellType.String, "ได้มาจาก : " + this._condition._getDataStr(_g.d.ic_resource._in), SMLReport._report._cellAlign.Left, this._view._fontHeader2);

                    this._view._addCell(__headerObject2, __column1, true, 3, -1, SMLReport._report._cellType.String, "จำนวนรับ : ", SMLReport._report._cellAlign.Left, this._view._fontHeader2);
                    this._view._addCell(__headerObject2, __column3, true, 3, -1, SMLReport._report._cellType.String, "วันที่รับ : ", SMLReport._report._cellAlign.Left, this._view._fontHeader2);

                    this._view._addCell(__headerObject2, __column3, true, 4, -1, SMLReport._report._cellType.String, "แบบ ข.ย.13", SMLReport._report._cellAlign.Right, this._view._fontStandard);

                }
            }
            else if (type == SMLReport._report._objectType.Detail)
            {
                // แถวบน
                __ojtReport = this._view._addObject(this._view._objectList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.Top);
                // ลำดับ
                this._view._addColumn(__ojtReport, 5, true, SMLReport._report._columnBorder.LeftRight, SMLReport._report._columnBorder.Left, "", "ลำดับที่", "", SMLReport._report._cellAlign.Center);
                // วันเดือนปี
                this._view._addColumn(__ojtReport, 10, true, SMLReport._report._columnBorder.Right, SMLReport._report._columnBorder.Left, "", "วัน เดือน ปี", "", SMLReport._report._cellAlign.Center);
                // จ่ายไปให้
                this._view._addColumn(__ojtReport, 55, true, SMLReport._report._columnBorder.RightBottom, SMLReport._report._columnBorder.LeftBottom, "", "จ่ายไปให้", "", SMLReport._report._cellAlign.Center);
                // จำนวน/ปริมาณ
                this._view._addColumn(__ojtReport, 20, true, SMLReport._report._columnBorder.RightBottom, SMLReport._report._columnBorder.LeftBottom, "", "จำนวน/ปริมาณ", "", SMLReport._report._cellAlign.Center);
                // หมายเหตุ
                this._view._addColumn(__ojtReport, 10, true, SMLReport._report._columnBorder.Right, SMLReport._report._columnBorder.LeftRight, "", "หมายเหตุ", "", SMLReport._report._cellAlign.Center);



                // แถวล่าง
                __ojtReport = this._view._addObject(this._view._objectList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.Bottom);

                this._view._addColumn(__ojtReport, 5, true, SMLReport._report._columnBorder.LeftRight, SMLReport._report._columnBorder.LeftRightBottom, "", "", "", SMLReport._report._cellAlign.Center);
                // ที่ขาย
                this._view._addColumn(__ojtReport, 10, true, SMLReport._report._columnBorder.Right, SMLReport._report._columnBorder.LeftRightBottom, "", "ที่ขาย", "", SMLReport._report._cellAlign.Center);
                // ชื่อที่อยู่
                this._view._addColumn(__ojtReport, 15, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.LeftBottom, "", "", "", SMLReport._report._cellAlign.Center);
                this._view._addColumn(__ojtReport, 30, true, SMLReport._report._columnBorder.Right, SMLReport._report._columnBorder.RightBottom, "", "ชื่อและที่อยู่", "", SMLReport._report._cellAlign.Center);

                // ประเภท
                this._view._addColumn(__ojtReport, 10, true, SMLReport._report._columnBorder.Right, SMLReport._report._columnBorder.LeftBottom, "", "ประเภท", "", SMLReport._report._cellAlign.Center);
                // ขาย
                this._view._addColumn(__ojtReport, 10, true, SMLReport._report._columnBorder.Right, SMLReport._report._columnBorder.LeftBottom, "", "ขาย", "", SMLReport._report._cellAlign.Center);
                // คงเหลือ
                this._view._addColumn(__ojtReport, 10, true, SMLReport._report._columnBorder.Right, SMLReport._report._columnBorder.LeftRightBottom, "", "คงเหลือ", "", SMLReport._report._cellAlign.Center);
                // หมายเหตุ
                this._view._addColumn(__ojtReport, 10, true, SMLReport._report._columnBorder.Right, SMLReport._report._columnBorder.RightBottom, "", "", "", SMLReport._report._cellAlign.Center);

                this._view._addColumn(__ojtReport, 0, true, SMLReport._report._columnBorder.Right, SMLReport._report._columnBorder.None, "", "", "", SMLReport._report._cellAlign.Center);
                // lot hide
                // this._view._addColumn(__ojtReport, 10, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "", "", SMLReport._report._cellAlign.Default);

                /*
                this._view._addColumn(__ojtReport, 6, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "", "", SMLReport._report._cellAlign.Default);
                this._view._addColumn(__ojtReport, 8, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", _g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._pay, "", SMLReport._report._cellAlign.Default);
                this._view._addColumn(__ojtReport, 10, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", _g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._status_pay, "", SMLReport._report._cellAlign.Default);
                this._view._addColumn(__ojtReport, 8, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", _g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._tax_rate, "", SMLReport._report._cellAlign.Right);
                this._view._addColumn(__ojtReport, 9, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", _g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._now_pay, "", SMLReport._report._cellAlign.Right);
                this._view._addColumn(__ojtReport, 9, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", _g.d.resource_report_vat._table + "." + _g.d.resource_report_vat._now_sent, "", SMLReport._report._cellAlign.Right);
                this._view._addColumn(__ojtReport, 5, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "", "", SMLReport._report._cellAlign.Default);
                this._view._addColumn(__ojtReport, 0, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "", "", SMLReport._report._cellAlign.Right);
                */
            }
            else if (type == SMLReport._report._objectType.PageFooter)
            {
                SMLReport._report._objectListType __footerObject = _view._addObject(_view._objectList, SMLReport._report._objectType.PageFooter, true, 0, true, SMLReport._report._columnBorder.None);
                int __col1 = _view._addColumn(__footerObject, 70);
                int __columnDetail = _view._addColumn(__footerObject, 30);

                this._view._addCell(__footerObject, __columnDetail, true, 0, -1, SMLReport._report._cellType.String, "(ลายมือชื่อ)..........................................ผู้รับอนุญาต", SMLReport._report._cellAlign.Left, this._view._fontStandard);
                this._view._addCell(__footerObject, __columnDetail, true, 1, -1, SMLReport._report._cellType.String, "(ลายมือชื่อ)..........................................ผู้มีหน้าที่ปฏิบัติการ", SMLReport._report._cellAlign.Left, this._view._fontStandard);

                return true;

            }

            return false;
        }

        void _view__getDataObject()
        {
            DataRow[] __lot = _dataTableLot.Select();
            SMLReport._report._objectListType __dataObject = null;
            SMLReport._report._columnListType __getColumn = (SMLReport._report._columnListType)__ojtReport._columnList[0];
            string __formatNumber = MyLib._myGlobal._getFormatNumber("m0" + _g.g._companyProfile._item_amount_decimal.ToString());
            Font __newFont = new Font(__getColumn._fontData, FontStyle.Bold);
            Font __totalFont = new Font(__getColumn._fontData, FontStyle.Bold);


            for (int __lotRow = 0; __lotRow < __lot.Length; __lotRow++)
            {
                int __row = 1;

                string __getLotNumber = __lot[__lotRow]["lot_number"].ToString();
                string __getLotMovement = "lot_number = \'" + __getLotNumber + "\'";
                DataRow[] _dr = this._dataTableMovement.Select(__getLotMovement);

                decimal __balanceAmount = MyLib._myGlobal._decimalPhase(__lot[__lotRow]["qty_in"].ToString());

                for (int _row = 0; _row < _dr.Length; _row++)
                {

                    decimal __qty_out = MyLib._myGlobal._decimalPhase(_dr[_row]["qty_out"].ToString());

                    decimal __balance_amount = __balanceAmount -= __qty_out;

                    __dataObject = this._view._addObject(this._view._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);

                    if (__row == _dr.Length)
                    {
                        __dataObject._pageBreak = true;
                    }

                    this._view._createEmtryColumn(__ojtReport, __dataObject);
                    this._view._addDataColumn(__ojtReport, __dataObject, 0, (__row).ToString(), null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);

                    this._view._addDataColumn(__ojtReport, __dataObject, 1, MyLib._myGlobal._convertDateFromQuery(_dr[_row][_g.d.ic_trans_detail_lot._doc_date].ToString()).ToString("dd/MM/yyyy"), null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                    this._view._addDataColumn(__ojtReport, __dataObject, 2, _dr[_row]["cust_name"].ToString(), null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String, Color.Black, false, true);
                    this._view._addDataColumn(__ojtReport, __dataObject, 3, _dr[_row]["address"].ToString(), null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String, Color.Black, false, true);
                    this._view._addDataColumn(__ojtReport, __dataObject, 4, _dr[_row]["ar_type_5"].ToString(), null, SMLReport._report._cellAlign.Center, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String, Color.Black, false, true);
                    this._view._addDataColumn(__ojtReport, __dataObject, 5, string.Format(__formatNumber, __qty_out), null, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                    this._view._addDataColumn(__ojtReport, __dataObject, 6, string.Format(__formatNumber, __balance_amount), null, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                    this._view._addDataColumn(__ojtReport, __dataObject, 7, "", null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String, Color.Black, false, true);
                    this._view._addDataColumn(__ojtReport, __dataObject, 8, __getLotNumber, null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String, Color.Black, false, true, true);

                    /*
                    this._view._addDataColumn(__ojtReport, __dataObject, 1, _dr[_row][_g.d.gl_wht_list_detail._cust_code].ToString() + "\n" + _dr[_row][_g.d.resource_report_vat._cust_address].ToString().Replace("\n", "") + "\n", null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._columnBorder.None, SMLReport._report._cellType.String, Color.Black, false, true);
                    this._view._addDataColumn(__ojtReport, __dataObject, 2, _dr[_row][_g.d.resource_report_vat._cust_taxno].ToString(), null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                    this._view._addDataColumn(__ojtReport, __dataObject, 3, _dr[_row][_g.d.ap_supplier_detail._branch_code].ToString(), null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                    this._view._addDataColumn(__ojtReport, __dataObject, 4, MyLib._myGlobal._convertDateFromQuery(_dr[_row][_g.d.gl_wht_list_detail._due_date].ToString()).ToShortDateString(), null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                    this._view._addDataColumn(__ojtReport, __dataObject, 5, _dr[_row][_g.d.gl_wht_list_detail._income_type].ToString(), null, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                    this._view._addDataColumn(__ojtReport, __dataObject, 6, string.Format(__formatNumber, __taxrate), null, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                    this._view._addDataColumn(__ojtReport, __dataObject, 7, string.Format(__formatNumber, __amount), null, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                    this._view._addDataColumn(__ojtReport, __dataObject, 8, string.Format(__formatNumber, __tax), null, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                    this._view._addDataColumn(__ojtReport, __dataObject, 9, "1", null, SMLReport._report._cellAlign.Center, 0, SMLReport._report._cellType.String);
                    this._view._addDataColumn(__ojtReport, __dataObject, 10, "", null, SMLReport._report._cellAlign.Center, 0, SMLReport._report._cellType.String);*/

                    __row++;
                }

            }


        }

        private void _buttonClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        void _condition__textBoxSearch(object sender)
        {
            // open popup search
            string name = ((MyLib._myTextBox)sender)._name;
            string label_name = ((MyLib._myTextBox)sender)._labelName;
            MyLib._searchDataFull __searchControl = null;

            if (name == _g.d.ic_resource._item_code)
            {

                MyLib._myTextBox __getControl = (MyLib._myTextBox)sender;
                __searchControl = new MyLib._searchDataFull();
                __searchControl._name = _g.g._search_screen_ic_inventory;
                __searchControl._dataList._loadViewFormat(__searchControl._name, MyLib._myGlobal._userSearchScreenGroup, false);
                __searchControl._dataList._gridData._mouseClick += (o1, s1) =>
                {
                    string __arCode = __searchControl._dataList._gridData._cellGet(__searchControl._dataList._gridData._selectRow, _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code).ToString();
                    this._condition._setDataStr(_g.d.ic_resource._item_code, __arCode);
                    string __itemName = __searchControl._dataList._gridData._cellGet(__searchControl._dataList._gridData._selectRow, _g.d.ic_inventory._table + "." + _g.d.ic_inventory._name_1).ToString();
                    this._condition._setDataStr(_g.d.ic_resource._item_name, __itemName);
                    __searchControl.Close();

                };

                __searchControl._searchEnterKeyPress += (s1, e1) =>
                {
                    string __arCode = __searchControl._dataList._gridData._cellGet(__searchControl._dataList._gridData._selectRow, _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code).ToString();
                    this._condition._setDataStr(_g.d.ic_resource._item_code, __arCode);
                    string __itemName = __searchControl._dataList._gridData._cellGet(__searchControl._dataList._gridData._selectRow, _g.d.ic_inventory._table + "." + _g.d.ic_inventory._name_1).ToString();
                    this._condition._setDataStr(_g.d.ic_resource._item_name, __itemName);
                    __searchControl.Close();
                };


                __searchControl.StartPosition = FormStartPosition.CenterScreen;
                __searchControl.ShowDialog();
            }
        }

        void _condition__textBoxChanged(object sender, string name)
        {
            this._bookSelectGrid._clear();

            // update lot grid
            this._itemCode = this._condition._getDataStr(_g.d.ic_resource._item_code);


            if (this._itemCode.Length > 0)
            {


                SMLERPControl._icInfoProcess __process = new SMLERPControl._icInfoProcess();
                this._bookSelectGrid._loadFromDataTable(__process._stkStockInfoAndBalanceByLot(_g.g._productCostType.ปรกติ, null, this._itemCode, this._itemCode, "", false, SMLERPControl._icInfoProcess._stockBalanceType.ยอดคงเหลือตามLOT));


            }
        }
    }
}
