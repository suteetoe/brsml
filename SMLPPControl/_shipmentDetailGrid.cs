using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMLPPControl
{
    public class _shipmentDetailGrid : MyLib._myGrid
    {
        object[] _shipment_detail_doc_ref_type = new object[] { "ใบจัดสินค้ัา", "ขาย" };
        object[] _shipment_detail_doc_ref_type_singha = new object[] { "เลือก", "ขาย", "เบิกสินค้า", "โอนสินค้า", "เบิกฝาก" };

        private MyLib._searchDataFull _searchMaster;

        private SMLPPGlobal.g._ppControlTypeEnum _transControlType = SMLPPGlobal.g._ppControlTypeEnum.ว่าง;

        public delegate string getCustCodeEventHandler();
        public event getCustCodeEventHandler _getCustCode;


        public SMLPPGlobal.g._ppControlTypeEnum transControlType
        {
            get
            {
                return this._transControlType;
            }
            set
            {
                this._transControlType = value;
                this.build();
            }
        }

        public _shipmentDetailGrid()
        {

        }

        public void build()
        {
            string __formatNumberQty = _g.g._getFormatNumberStr(1);
            string __formatNumberPrice = _g.g._getFormatNumberStr(2);
            string __formatNumberAmount = _g.g._getFormatNumberStr(3);
            this._width_by_persent = true;

            if (this.DesignMode == false)
            {
                if (this.transControlType != SMLPPGlobal.g._ppControlTypeEnum.ว่าง)
                {

                    switch (this.transControlType)
                    {
                        case SMLPPGlobal.g._ppControlTypeEnum.Shipment:
                            {
                                this._table_name = _g.d.pp_shipment_detail._table;

                                this._addColumn(_g.d.pp_shipment_detail._doc_ref_type, 10, 20, 20, true, false, true);
                                this._addColumn(_g.d.pp_shipment_detail._ref_doc_no, 1, 20, 20, true, false, true, true);
                                this._addColumn(_g.d.pp_shipment_detail._ref_doc_date, 4, 20, 20);
                                this._addColumn(_g.d.pp_shipment_detail._remark, 1, 40, 40);

                                this._calcPersentWidthToScatter();
                                this._cellComboBoxGet += _shipmentDetailGrid__cellComboBoxGet;
                                this._cellComboBoxItem += _shipmentDetailGrid__cellComboBoxItem;

                                this._afterAddRow += _shipmentDetailGrid__afterAddRow;
                                this._clickSearchButton += _shipmentDetailGrid__clickSearchButton;
                                this._alterCellUpdate += _shipmentDetailGrid__alterCellUpdate;

                            }
                            break;

                        case SMLPPGlobal.g._ppControlTypeEnum.SaleShipment:
                            {

                                this._table_name = _g.d.ic_trans_detail._table;

                                this._addColumn(_g.d.ic_trans_detail._doc_ref_type, 10, 25, 25, true, true, true);
                                this._addColumn(_g.d.ic_trans_detail._ref_doc_no, 1, 1, 10, true, true, true, true, "", "", "", _g.d.ic_trans_detail._doc_ref);
                                this._addColumn(_g.d.ic_trans_detail._item_code, 1, 1, 15, true, false, true, true);
                                this._addColumn(_g.d.ic_trans_detail._item_name, 1, 1, 20, false, false, true, false);
                                this._addColumn(_g.d.ic_trans_detail._wh_code, 1, 1, 8, false, false, true, false);
                                this._addColumn(_g.d.ic_trans_detail._shelf_code, 1, 1, 8, false, false, true, false);

                                this._addColumn(_g.d.ic_trans_detail._unit_code, 1, 1, 8, false, false, true, false);
                                this._addColumn(_g.d.ic_trans_detail._qty, 3, 1, 8, true, false, true, false, __formatNumberQty);
                                this._addColumn(_g.d.ic_trans_detail._price, 3, 1, 8, (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLBIllFree) ? true : _g.g._companyProfile._column_price_enable, false, true, false, __formatNumberPrice);
                                this._addColumn(_g.d.ic_trans_detail._discount, 1, 1, 8, false, false, true);
                                this._addColumn(_g.d.ic_trans_detail._sum_amount, 3, 1, 10, false, false, true, false, __formatNumberAmount);
                                this._addColumn(_g.d.ic_trans_detail._hidden_cost_1, 3, 1, 8, (_g.g._companyProfile._hidden_income_1) ? true : false, (_g.g._companyProfile._hidden_income_1) ? false : true, true, false, __formatNumberAmount, "", "", _g.d.ic_trans_detail._hidden_cost_1_name_2);
                                this._addColumn(_g.d.ic_trans_detail._set_ref_line, 1, 1, 10, false, true, true);
                                this._addColumn(_g.d.ic_trans_detail._set_ref_qty, 3, 1, 10, false, true, true, false, __formatNumberQty);
                                this._addColumn(_g.d.ic_trans_detail._set_ref_price, 3, 1, 10, false, true, true, false, __formatNumberPrice);
                                this._addColumn(_g.d.ic_inventory._have_replacement, 2, 0, 0, false);
                                if (_g.g._companyProfile._ic_price_formula_control)
                                {
                                    this._addColumn(_g.d.ic_inventory_price_formula._price_0, 3, 1, 0, false);
                                }
                                if (_g.g._companyProfile._use_expire)
                                {
                                    this._addColumn(_g.d.ic_trans_detail._date_expire, 4, 1, 8, false, true, true, false);
                                }

                                this._isEdit = false;
                            }
                            break;
                    }

                }


            }


        }

        void _shipmentDetailGrid__alterCellUpdate(object sender, int row, int column)
        {
            switch (this.transControlType)
            {
                case SMLPPGlobal.g._ppControlTypeEnum.Shipment:
                    {

                    }
                    break;
            }
        }



        void _shipmentDetailGrid__clickSearchButton(object sender, MyLib.GridCellEventArgs e)
        {
            switch (this.transControlType)
            {
                case SMLPPGlobal.g._ppControlTypeEnum.Shipment:
                    {
                        if (e._columnName == _g.d.pp_shipment_detail._ref_doc_no)
                        {
                            // search doc no
                            int _searchType = (int)this._cellGet(e._row, _g.d.pp_shipment_detail._doc_ref_type);
                            string __getcustCode = "";
                            if (this._getCustCode != null)
                            {
                                __getcustCode = this._getCustCode();
                            }

                            if (_searchType > 0)
                            {
                                this._searchMaster = new MyLib._searchDataFull();
                                string __screenName = "";
                                string __fieldDocNo = "";
                                string __fieldDocDate = "";

                                StringBuilder __extraWhere = new StringBuilder();

                                switch (_searchType)
                                {
                                    case 2:
                                        __screenName = _g.g._search_screen_ic_stk_request;
                                        __extraWhere.Append(_g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินค้า_เบิกสินค้าวัตถุดิบ));
                                        __extraWhere.Append(" and " + _g.d.ic_trans._last_status + "= 0 ");
                                        // เอาเฉพาะเอกสาร ที่ไม่เคยส่ง
                                        __extraWhere.Append(" and not exists (select doc_no from pp_shipment_detail where pp_shipment_detail.ref_doc_no = ic_trans.doc_no) ");
                                        if (__getcustCode != "")
                                        {
                                            __extraWhere.Append(" and " + _g.d.ic_trans._cust_code + "= \'" + __getcustCode + "\' ");
                                        }
                                        __fieldDocNo = _g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no;
                                        __fieldDocDate = _g.d.ic_trans._table + "." + _g.d.ic_trans._doc_date;

                                        break;
                                    case 3:
                                        __screenName = _g.g._search_screen_ic_stk_transfer;
                                        __extraWhere.Append(_g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินค้า_โอนออก));
                                        __extraWhere.Append(" and " + _g.d.ic_trans._last_status + "= 0 ");
                                        // เอาเฉพาะเอกสาร ที่ไม่เคยส่ง
                                        __extraWhere.Append(" and not exists (select doc_no from pp_shipment_detail where pp_shipment_detail.ref_doc_no = ic_trans.doc_no) ");
                                        __fieldDocNo = _g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no;
                                        __fieldDocDate = _g.d.ic_trans._table + "." + _g.d.ic_trans._doc_date;

                                        break;
                                    case 4:
                                        __screenName = "screen_wms_product_deposit";
                                        __extraWhere.Append(_g.d.ic_wms_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.คลัง_รับฝาก_เบิก));
                                        // เอาเฉพาะเอกสาร ที่ไม่เคยส่ง
                                        __extraWhere.Append(" and not exists (select doc_no from pp_shipment_detail where pp_shipment_detail.ref_doc_no = ic_wms_trans.doc_no) ");
                                        if (__getcustCode != "")
                                        {
                                            __extraWhere.Append(" and " + _g.d.ic_trans._cust_code + "= \'" + __getcustCode + "\' ");
                                        }
                                        __fieldDocNo = _g.d.ic_wms_trans._table + "." + _g.d.ic_wms_trans._doc_no;
                                        __fieldDocDate = _g.d.ic_wms_trans._table + "." + _g.d.ic_wms_trans._doc_date;
                                        break;
                                    default:
                                        // ขาย
                                        __screenName = _g.g._search_screen_sale;
                                        __extraWhere.Append(_g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ));
                                        __extraWhere.Append(" and " + _g.d.ic_trans._last_status + "= 0 ");

                                        if (MyLib._myGlobal._OEMVersion.Equals("SINGHA"))
                                        {
                                            // เอาเฉพาะเอกสาร ที่ไม่เคยส่ง
                                            __extraWhere.Append(" and not exists (select doc_no from pp_shipment_detail where pp_shipment_detail.ref_doc_no = ic_trans.doc_no) ");

                                        }

                                        //if (MyLib._myGlobal._OEMVersion.Equals("SINGHA") == false)
                                        {
                                            if (__getcustCode != "")
                                            {
                                                __extraWhere.Append(" and " + _g.d.ic_trans._cust_code + "= \'" + __getcustCode + "\' ");
                                            }
                                        }
                                        __fieldDocNo = _g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no;
                                        __fieldDocDate = _g.d.ic_trans._table + "." + _g.d.ic_trans._doc_date;
                                        break;
                                }

                                if (_g.g._companyProfile._show_branch_doc_only && _g.g._companyProfile._change_branch_code == false)
                                {
                                    __extraWhere.Append(" and " + _g.d.ic_trans._branch_code + "=\'" + _g.g._companyProfile._branch_code + "\'");
                                }

                                //this._searchMaster._dataList.first
                                this._searchMaster._name = __screenName;
                                //
                                this._searchMaster._dataList._loadViewFormat(_searchMaster._name, MyLib._myGlobal._userSearchScreenGroup, true);
                                this._searchMaster._searchEnterKeyPress += _searchMaster__searchEnterKeyPress; // new MyLib.SearchEnterKeyPressEventHandler(_searchItem__searchEnterKeyPress);
                                this._searchMaster._dataList._gridData._mouseClick += _gridData__mouseClick;   // new MyLib.MouseClickHandler(_gridData__mouseClick);
                                if (__extraWhere.Length > 0)
                                {
                                    this._searchMaster._dataList._extraWhere = __extraWhere.ToString();
                                }
                                this._searchMaster._dataList._refreshData();
                                this._searchMaster.StartPosition = FormStartPosition.CenterScreen;

                                MyLib.ToolStripMyButton __buttonProcessSelected = new MyLib.ToolStripMyButton();
                                __buttonProcessSelected.DisplayStyle = ToolStripItemDisplayStyle.Image;
                                //SMLInventoryControl.Properties.Resources.flash
                                __buttonProcessSelected.Image = (Image)global::SMLPPControl.Properties.Resources.flash;
                                __buttonProcessSelected.ImageTransparentColor = System.Drawing.Color.Magenta;
                                __buttonProcessSelected.Name = "__buttonProcessSelected";
                                __buttonProcessSelected.Padding = new System.Windows.Forms.Padding(1);
                                __buttonProcessSelected.ResourceName = "";
                                __buttonProcessSelected.Size = new System.Drawing.Size(30, 22);
                                __buttonProcessSelected.Text = "Process Selected";
                                __buttonProcessSelected.ToolTipText = "Process Selected";
                                __buttonProcessSelected.Click += (s1, e1) =>
                                {
                                    bool __first = true;
                                    for (int __row = 0; __row < this._searchMaster._dataList._gridData._rowData.Count; __row++)
                                    {
                                        if (this._searchMaster._dataList._gridData._cellGet(__row, "check").ToString().Equals("1"))
                                        {
                                            MyLib._myGrid __grid = this._searchMaster._dataList._gridData;

                                            string __docNo = __grid._cellGet(__row, __fieldDocNo).ToString();
                                            DateTime __docDate = MyLib._myGlobal._convertDateFromQuery(__grid._cellGet(__row, __fieldDocDate).ToString());

                                            int __searchResult = this._findData(this._findColumnByName(_g.d.pp_shipment_detail._ref_doc_no), __docNo);

                                            if (__searchResult == -1)
                                            {
                                                if (__first == false)
                                                {
                                                    this._selectRow = this._addRow();
                                                }

                                                this._cellUpdate(this._selectRow, _g.d.pp_shipment_detail._doc_ref_type, _searchType, true);
                                                this._cellUpdate(this._selectRow, _g.d.pp_shipment_detail._ref_doc_no, __docNo, true);
                                                this._cellUpdate(this._selectRow, _g.d.pp_shipment_detail._ref_doc_date, __docDate, true);

                                                __first = false;
                                            }

                                        }
                                    }
                                    this._searchMaster.Close();
                                    SendKeys.Send("{TAB}");

                                };
                                this._searchMaster._dataList._button.Items.Add(__buttonProcessSelected);

                                this._searchMaster.ShowDialog();

                            }
                            else
                            {

                            }
                        }
                    }
                    break;
            }
        }

        void _gridData__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {

        }

        void _searchMaster__searchEnterKeyPress(MyLib._myGrid sender, int row)
        {

        }

        void _shipmentDetailGrid__afterAddRow(object sender, int row)
        {
            switch (this.transControlType)
            {
                case SMLPPGlobal.g._ppControlTypeEnum.Shipment:
                    {
                        this._cellUpdate(row, _g.d.pp_shipment_detail._doc_ref_type, 1, true);
                    }
                    break;
            }
        }

        object[] _shipmentDetailGrid__cellComboBoxItem(object sender, int row, int column)
        {
            switch (this.transControlType)
            {
                case SMLPPGlobal.g._ppControlTypeEnum.Shipment:
                    {
                        if (column == this._findColumnByName(_g.d.pp_shipment_detail._doc_ref_type))
                        {
                            if (MyLib._myGlobal._OEMVersion.Equals("SINGHA"))
                            {
                                return _shipment_detail_doc_ref_type_singha;
                            }
                            else
                                return _shipment_detail_doc_ref_type;

                        }
                    }
                    break;
            }

            return null;
        }

        string _shipmentDetailGrid__cellComboBoxGet(object sender, int row, int column, string columnName, int select)
        {
            switch (this.transControlType)
            {
                case SMLPPGlobal.g._ppControlTypeEnum.Shipment:
                    {
                        if (column == this._findColumnByName(_g.d.pp_shipment_detail._doc_ref_type))
                        {
                            if (MyLib._myGlobal._OEMVersion.Equals("SINGHA"))
                            {
                                return _shipment_detail_doc_ref_type_singha[(select == -1) ? 0 : select].ToString(); ;
                            }
                            return _shipment_detail_doc_ref_type[(select == -1) ? 0 : select].ToString();
                        }
                    }
                    break;
            }
            return "";
        }

    }
}
