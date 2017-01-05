using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace SMLERPReportTool
{
    public partial class _conditionScreen : MyLib._myForm
    {
        public _reportEnum _mode;
        public Boolean _processClick = false;
        public string _where = "";
        public DataTable _grid_where;
        private int _click_check = 0;
        public bool _display_serial = false;
        public ArrayList _conditionStr;
        public string _order_by = "";
        public string _whereSub = "";
        public string _whereSubTop = "";
        public SMLERPControl._selectWarehouseAndLocationForm _selectWarehouseAndLocation = null;
        public string _fieldCheck = "Check";

        public _conditionScreen(_reportEnum mode, string screenName)
        {
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this._toolStrip.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }
            this._toolStrip.Visible = false;
            this._label.Text = screenName;
            this.Text = screenName;
            this._processButton.Click += new EventHandler(_bt_process_Click);
            this._exitButton.Click += new EventHandler(_bt_exit_Click);
            this.Load += new EventHandler(_codition_ic_new_Load);
            this._mode = mode;
            this._screen._maxColumn = 1;
            this._screen._init(this._mode);
            switch (mode)
            {
                case _reportEnum.สินค้า_รายงานเคลื่อนไหว:
                    {
                        this._toolStrip.Visible = true;
                        ToolStripButton __wh = new ToolStripButton();
                        __wh.Text = "เลือกคลัง";
                        this._selectWarehouseAndLocation = new SMLERPControl._selectWarehouseAndLocationForm(true, 1);
                        __wh.Click += (s1, e1) =>
                        {
                            this._selectWarehouseAndLocation.ShowDialog();
                        };
                        this._selectWarehouseAndLocation._closeButton.Click += (s1, e1) =>
                        {
                            this._selectWarehouseAndLocation.Close();
                        };
                        this._toolStrip.Items.Add(__wh);
                    }
                    break;
                case _reportEnum.เช็ครับ_ยกมา:
                    this._grid._searchName = _g.g._search_screen_ar;
                    this._grid._setFromToColumn(_g.d.resource_report._from_ar, _g.d.resource_report._to_ar);
                    break;
                case _reportEnum.เช็ครับ_ฝาก:
                case _reportEnum.เช็ครับ_ผ่าน:
                case _reportEnum.เช็ครับ_รับคืน:
                case _reportEnum.เช็ครับ_ขาดสิทธิ์:
                case _reportEnum.เช็ครับ_เปลี่ยนเช็ค:
                    this._grouper1.Visible = false;
                    this._grouper2.Dock = DockStyle.Fill;
                    break;
                case _reportEnum.เช็คจ่าย_ยกมา:
                    this._grid._searchName = _g.g._search_screen_ap;
                    this._grid._setFromToColumn(_g.d.resource_report._from_ap, _g.d.resource_report._to_ap);
                    break;
                case _reportEnum.เช็คจ่าย_ผ่าน:
                case _reportEnum.เช็คจ่าย_คืน:
                case _reportEnum.เช็คจ่าย_ขาดสิทธิ์:
                case _reportEnum.เช็คจ่าย_เปลี่ยนเช็ค:
                    this._grouper1.Visible = false;
                    this._grouper2.Dock = DockStyle.Fill;
                    break;
                case _reportEnum.รายงานผลต่างจากการตรวจนับ:
                case _reportEnum.Stock_no_count_no_balance:
                case _reportEnum.สินค้า_รายงานการตรวจสอบสินค้า_serial:
                    {
                        this._grid._table_name = _g.d.ic_trans._table;
                        this._grid._addColumn(this._fieldCheck, 11, 5, 5);
                        this._grid._addColumn(_g.d.ic_trans._doc_date, 4, 10, 10);
                        this._grid._addColumn(_g.d.ic_trans._doc_time, 1, 10, 10);
                        this._grid._addColumn(_g.d.ic_trans._doc_no, 1, 20, 20);
                        this._grid._addColumn(_g.d.ic_trans._doc_ref_date, 4, 10, 10);
                        this._grid._addColumn(_g.d.ic_trans._doc_ref, 1, 20, 20);
                        this._grid._addColumn(_g.d.ic_trans._remark, 1, 30, 30);
                        this._grid._addColumn(_g.d.ic_trans._last_status, 1, 10, 10);
                        this._grid._calcPersentWidthToScatter();
                        this._grid._isEdit = false;

                        this._toolStrip.Visible = true;
                        ToolStripButton __load = new ToolStripButton();
                        __load.Text = "Load เอกสารตรวจนับ";
                        //this._selectWarehouseAndLocation = new SMLERPControl._selectWarehouseAndLocationForm(true);
                        __load.Click += (s1, e1) =>
                        {
                            //this._selectWarehouseAndLocation.ShowDialog();
                            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                            this._grid._clear();
                            String __query = "select " + MyLib._myGlobal._fieldAndComma("1 as " + this._fieldCheck, _g.d.ic_trans._doc_date, _g.d.ic_trans._doc_time, _g.d.ic_trans._doc_no, _g.d.ic_trans._doc_ref_date, _g.d.ic_trans._doc_ref, _g.d.ic_trans._remark) + " from " + _g.d.ic_trans._table + " where " + _g.d.ic_trans._doc_date + " between " + this._screen._getDataStrQuery(_g.d.resource_report._from_date) + " and " + this._screen._getDataStrQuery(_g.d.resource_report._to_date) + " and " + _g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินค้า_ตรวจนับสินค้า).ToString() + " and " + _g.d.ic_trans._last_status + "=0 and " + _g.d.ic_trans._doc_success + "=0 order by " + _g.d.ic_trans._doc_date + "," + _g.d.ic_trans._doc_time;
                            this._grid._loadFromDataTable(__myFrameWork._queryShort(__query).Tables[0]);

                        };
                        this._toolStrip.Items.Add(__load);
                        ToolStripButton __wh = new ToolStripButton();
                        __wh.Text = "เลือกคลัง/ที่เก็บสินค้า";
                        this._selectWarehouseAndLocation = new SMLERPControl._selectWarehouseAndLocationForm(true, 1);
                        __wh.Click += (s1, e1) =>
                        {
                            this._selectWarehouseAndLocation.ShowDialog();
                        };
                        this._selectWarehouseAndLocation._closeButton.Click += (s1, e1) =>
                        {
                            this._selectWarehouseAndLocation.Close();
                        };
                        this._toolStrip.Items.Add(__wh);

                    }
                    break;
                case _reportEnum.สินค้า_รายงานยอดคงเหลือสินค้า:
                case _reportEnum.Item_Balance_now_Only_Serial:
                    //case _reportEnum.Diff_from_count :                    
                    {
                        this._toolStrip.Visible = true;
                        ToolStripButton __wh = new ToolStripButton();
                        __wh.Text = "เลือกคลัง/ที่เก็บสินค้า";
                        this._selectWarehouseAndLocation = new SMLERPControl._selectWarehouseAndLocationForm(true);
                        __wh.Click += (s1, e1) =>
                        {
                            this._selectWarehouseAndLocation.ShowDialog();
                        };
                        this._selectWarehouseAndLocation._closeButton.Click += (s1, e1) =>
                        {
                            this._selectWarehouseAndLocation.Close();
                        };
                        this._toolStrip.Items.Add(__wh);
                    }
                    break;
                case _reportEnum.สินค้า_รายงานกำไรขั้นต้นตามเอกสารแบบมีส่วนลด:
                    this._grouper1.Visible = false;
                    this._grouper2.AutoSize = true;
                    this._grouper2.Dock = DockStyle.Fill;
                    break;
                case _reportEnum.เงินสดธนาคาร_รายงานการรับเงินประจำวัน:
                case _reportEnum.เงินสดธนาคาร_รายงานการจ่ายเงินประจำวัน:
                    this._grouper1.Visible = false;
                    this._grouper2.AutoSize = true;
                    this._grouper2.Dock = DockStyle.Fill;
                    break;
                case _reportEnum.กำไรขั้นต้น_กลุ่มลูกค้า:
                    this._grid._searchName = _g.g._search_screen_ic_inventory;
                    this._grid._setFromToColumn(_g.d.resource_report._from_item_code, _g.d.resource_report._to_item_code);
                    break;
                case _reportEnum.เงินสดย่อย_เคลื่อนไหว:
                case _reportEnum.เงินสดย่อย_สถานะ:
                case _reportEnum.เงินสดย่อย_เบิก:
                case _reportEnum.เงินสดย่อย_รับคืน:
                    this._grid._searchName = _g.g._search_screen_cb_petty_cash;
                    this._grid._setFromToColumn(_g.d.resource_report._from_petty_cash_code, _g.d.resource_report._to_petty_cash_code);
                    break;
                case _reportEnum.สินค้า_รายงานรับสินค้าสำเร็จรูป_ยกเลิก:
                case _reportEnum.สินค้า_รายงานเบิกสินค้าวัตถุดิบ_ยกเลิก:
                case _reportEnum.สินค้า_รายงานรับคืนเบิกสินค้าวัตถุดิบ_ยกเลิก:

                case _reportEnum.สินค้า_รายงานโอนสินค้าและวัตถุดิบ_ยกเลิก:
                case _reportEnum.สินค้า_รายงานปรับปรุงสินค้าและวัตถุดิบ_เกิน_ยกเลิก:
                case _reportEnum.สินค้า_รายงานปรับปรุงสินค้าและวัตถุดิบ_ขาด_ยกเลิก:

                case _reportEnum.เงินสดย่อย_เบิก_ยกเลิก:
                case _reportEnum.เงินสดย่อย_รับคืน_ยกเลิก:

                case _reportEnum.เช็ครับ_ฝาก_ยกเลิก:
                case _reportEnum.เช็ครับ_ผ่าน_ยกเลิก:
                case _reportEnum.เช็ครับ_รับคืน_ยกเลิก:
                case _reportEnum.เช็ครับ_เปลี่ยน_ยกเลิก:
                case _reportEnum.เช็ครับ_ขาดสิทธิ์_ยกเลิก:

                case _reportEnum.เช็คจ่าย_ผ่าน_ยกเลิก:
                case _reportEnum.เช็คจ่าย_คืน_ยกเลิก:
                case _reportEnum.เช็คจ่าย_เปลี่ยน_ยกเลิก:
                case _reportEnum.เช็คจ่าย_ขาดสิทธิ์_ยกเลิก:

                case _reportEnum.เคลื่อนไหวเงินสด:


                    this._grouper1.Visible = false;
                    this._grouper2.Dock = DockStyle.Fill;
                    break;
                case _reportEnum.Item_by_serial:
                case _reportEnum.Item_status:
                    this._grid._searchName = _g.g._search_screen_ic_inventory;
                    this._grid._setFromToColumn(_g.d.resource_report._from_item_code, _g.d.resource_report._to_item_code);
                    break;
                case _reportEnum.Item_Giveaway:
                    // ของแถมซื้อ
                    this._grouper1.Visible = false;
                    this._grouper2.Dock = DockStyle.Fill;
                    break;
            }
            switch (_global._reportType(mode))
            {
                case _reportTypeEnum.เช็ค:
                    this._grouper1.Visible = false;
                    this._grouper2.AutoSize = true;
                    this._grouper2.Dock = DockStyle.Fill;
                    break;
                case _reportTypeEnum.ลูกหนี้:
                case _reportTypeEnum.ขาย_ลูกหนี้:
                    this._grid._searchName = _g.g._search_screen_ar;
                    this._grid._setFromToColumn(_g.d.resource_report._from_ar, _g.d.resource_report._to_ar);
                    break;
                case _reportTypeEnum.เจ้าหนี้:
                case _reportTypeEnum.ซื้อ_เจ้าหนี้:
                    this._grid._searchName = _g.g._search_screen_ap;
                    this._grid._setFromToColumn(_g.d.resource_report._from_ap, _g.d.resource_report._to_ap);
                    break;
                case _reportTypeEnum.สินค้า:
                    this._grid._searchName = _g.g._search_screen_ic_inventory;
                    this._grid._setFromToColumn(_g.d.resource_report._from_item_code, _g.d.resource_report._to_item_code);
                    break;
                case _reportTypeEnum.สินค้า_Serial:
                    this._grid._searchName = _g.g._search_screen_ic_serial;
                    this._grid._setFromToColumn(_g.d.resource_report._from_serail_no, _g.d.resource_report._to_serail_no);
                    break;
                case _reportTypeEnum.ธนาคาร:
                    this._grid._searchName = _g.g._search_screen_สมุดเงินฝาก;
                    this._grid._setFromToColumn(_g.d.resource_report._from_book_code, _g.d.resource_report._to_book_code);

                    break;

            }

            // toe เพิ่มเติม grid ค้นหา
            /*switch (this._mode)
            {
                case _reportEnum.เงินสดธนาคาร_รายได้อื่น:
                case _reportEnum.เงินสดธนาคาร_รายได้อื่น_เพิ่มหนี้_ยกเลิก:
                case _reportEnum.เงินสดธนาคาร_รายได้อื่น_ลดหนี้:
                    this._grid._searchName = _g.g._search_screen_ar;
                    this._grid._setFromToColumn(_g.d.resource_report._from_ar, _g.d.resource_report._to_ar);

                    break;
                case _reportEnum.เงินสดธนาคาร_รายจ่ายอื่น:
                case _reportEnum.เงินสดธนาคาร_รายจ่ายอื่น_เพิ่มหนี้:
                case _reportEnum.เงินสดธนาคาร_รายจ่ายอื่น_ลดหนี้:
                    this._grid._searchName = _g.g._search_screen_ap;
                    this._grid._setFromToColumn(_g.d.resource_report._from_ap, _g.d.resource_report._to_ap);

                    break;
            }*/


        }

        void _codition_ic_new_Load(object sender, EventArgs e)
        {
            this._screen.AutoSize = true;
        }

        void _bt_exit_Click(object sender, EventArgs e)
        {
            this._processClick = false;
            this.Close();
        }

        void _bt_process_Click(object sender, EventArgs e)
        {
            if (_mode == _reportEnum.รายงานผลต่างจากการตรวจนับ || _mode == _reportEnum.สินค้า_รายงานการตรวจสอบสินค้า_serial)
            {
                // เช็คการเลือกเอกสาร
                if (this._grid._rowData.Count == 0)
                {
                    MessageBox.Show("กรุณาเลือกเอกสารตรวจนับ");
                    return;
                }
                else
                {
                    bool __found = false;
                    for (int __i = 0; __i < this._grid._rowData.Count; __i++)
                    {
                        if (this._grid._cellGet(__i, 0).ToString().Equals("1"))
                        {
                            __found = true;
                            break;
                        }
                    }

                    if (__found == false)
                    {
                        MessageBox.Show("กรุณาเลือกเอกสารตรวจนับ");
                        return;
                    }
                }

            }
            if (this._click_check == 0)
            {
                this._grid_where = null;
            }
            else
            {
                this._grid_where = _grid._getCondition();
            }

            this._where = this._screen._createQueryForDatabase()[1].ToString();
            this._processClick = true;
            this.Close();
        }

        private void _bt_process_Click_1(object sender, EventArgs e)
        {

        }
    }

    public partial class _condition_ic_screen : MyLib._myScreen
    {
        public _cashierSelectForm _cashierSelectDialog = null;
        //MyLib._myFrameWork _myFramework = new MyLib._myFrameWork();
        //MyLib._searchDataFull _searchFull = new MyLib._searchDataFull();
        //search 
        MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
        // <summary>
        // สินค้า
        // </summary>
        MyLib._searchDataFull _searchIc;
        // <summary>
        // กลุ่มสินค้า
        // </summary>
        MyLib._searchDataFull _searchIc_Group;
        // <summary>
        // ยี่ห้อสินค้า
        // </summary>
        MyLib._searchDataFull _searchIc_Brand;
        // <summary>
        // ประเภทสินค้า
        // </summary>
        MyLib._searchDataFull _searchIc_Type;
        // <summary>
        // หน่วยนับสินค้า  _searchIc_Unit
        // </summary>
        MyLib._searchDataFull _searchIc_Unit;
        // <summary>
        // คลังสินค้า  _searchIc_Warehouse
        // </summary>
        MyLib._searchDataFull _searchIc_Warehouse;
        // <summary>
        // ที่เก็บสินค้า  _searchIc_Shelf
        // </summary>
        MyLib._searchDataFull _searchIc_Shelf;
        // <summary>
        // เจ้าหนี้  _search_ap
        // </summary>
        MyLib._searchDataFull _search_ap;
        // <summary>
        // แผนก  _search_department
        // </summary>
        MyLib._searchDataFull _search_department;
        // <summary>
        // เลขที่เอกสาร  _search_docno
        // </summary>
        MyLib._searchDataFull _search_docno;
        // <summary>
        // หมวดสินค้า  _search_category
        // </summary>
        MyLib._searchDataFull _search_category;
        // <summary>
        // รูปแบบสินค้า _search_pattern
        // </summary>
        MyLib._searchDataFull _search_pattern;
        // <summary>
        // ผู้เบิกสินค้า _search_erp_user
        // </summary>
        MyLib._searchDataFull _search_erp_user;
        // <summary>
        // การจัดสรร _search_allocation_list
        // </summary>
        MyLib._searchDataFull _search_allocation_list;
        // <summary>
        // seial number _search_serial_number
        // </summary>
        MyLib._searchDataFull _search_serial_number;
        // <summary>
        // รายงานสถานะใบเสนอซื้อ _search_serial_number  
        // </summary>
        MyLib._searchDataFull _search_status_approve;  //MOO 18/07/2553
        // <summary>
        // ค้นหาประเภทลูกหนี้ _search_ar_type  
        // </summary>
        MyLib._searchDataFull _search_ar_type;  //TOE

        string _searchName = "";
        TextBox _searchTextBox;

        _reportEnum _mode;

        public _condition_ic_screen()
        {
            this._table_name = _g.d.resource_report._table;
            this._textBoxSearch += new MyLib.TextBoxSearchHandler(_condition_ic_screen__textBoxSearch);
            this._textBoxChanged += new MyLib.TextBoxChangedHandler(_condition_ic_screen__textBoxChanged);
        }

        void _condition_ic_screen__textBoxSearch(object sender)
        {
            string name = ((MyLib._myTextBox)sender)._name;
            string label_name = ((MyLib._myTextBox)sender)._labelName;
            MyLib._searchDataFull __searchControl = null;
            if (name.Equals(_g.d.resource_report._from_item_code) || name.Equals(_g.d.resource_report._to_item_code))
            {
                __searchControl = _searchIc;
            }
            else if (name.Equals(_g.d.resource_report._from_group) || name.Equals(_g.d.resource_report._to_group) || name.Equals(_g.d.resource_report._item_group_code))
            {
                __searchControl = _searchIc_Group;
            }
            else if (name.Equals(_g.d.resource_report._from_brand) || name.Equals(_g.d.resource_report._to_brand))
            {
                __searchControl = _searchIc_Brand;
            }
            else if (name.Equals(_g.d.resource_report._from_type) || name.Equals(_g.d.resource_report._to_type))
            {
                __searchControl = _searchIc_Type;
            }
            else if (name.Equals(_g.d.resource_report._from_unit) || name.Equals(_g.d.resource_report._to_unit))
            {
                __searchControl = _searchIc_Unit;
            }
            else if (name.Equals(_g.d.resource_report._from_warehouse) || name.Equals(_g.d.resource_report._to_warehouse))
            {
                __searchControl = _searchIc_Warehouse;
            }
            else if (name.Equals(_g.d.resource_report._from_shelf) || name.Equals(_g.d.resource_report._to_shelf))
            {
                __searchControl = _searchIc_Shelf;
            }
            else if (name.Equals(_g.d.resource_report._from_payable) || name.Equals(_g.d.resource_report._to_payable))  // เจ้าหนี้
            {
                __searchControl = _search_ap;
            }
            else if (name.Equals(_g.d.resource_report._from_department) || name.Equals(_g.d.resource_report._to_department))
            {
                __searchControl = _search_department;
            }
            else if (name.Equals(_g.d.resource_report._from_docno) || name.Equals(_g.d.resource_report._to_docno))
            {
                __searchControl = _search_docno;
            }
            else if (name.Equals(_g.d.resource_report._from_category) || name.Equals(_g.d.resource_report._to_category))
            {
                __searchControl = _search_category;
            }
            else if (name.Equals(_g.d.resource_report._from_format) || name.Equals(_g.d.resource_report._to_format))
            {
                __searchControl = _search_pattern;
            }
            else if (name.Equals(_g.d.resource_report._from_sale_person) || name.Equals(_g.d.resource_report._to_sale_person))
            {
                __searchControl = _search_erp_user;
            }
            else if (name.Equals(_g.d.resource_report._from_allocation) || name.Equals(_g.d.resource_report._to_allocation))
            {
                __searchControl = _search_allocation_list;
            }
            else if (name.Equals(_g.d.resource_report._from_serail_no) || name.Equals(_g.d.resource_report._to_serail_no))
            {
                __searchControl = _search_serial_number;
            }
            else if (name.Equals(_g.d.resource_report._from_approve) || name.Equals(_g.d.resource_report._to_approve))   //MOO 18/07/2553
            {
                __searchControl = _search_status_approve;
            }
            else if (name.Equals(_g.d.resource_report._from_ap) || name.Equals(_g.d.resource_report._to_ap))
            {
                __searchControl = _search_ap;
            }
            else if (name.Equals(_g.d.resource_report._ar_type))
            {
                __searchControl = _search_ar_type;
            }
            else if (name.Equals(_g.d.resource_report._branch))
            {
                MyLib._searchDataFull __searchBranchControl = new MyLib._searchDataFull();
                __searchBranchControl._name = _g.g._search_master_erp_branch_list;
                __searchBranchControl._dataList._multiSelect = true;
                __searchBranchControl._dataList._loadViewFormat(__searchBranchControl._name, MyLib._myGlobal._userSearchScreenGroup, false);
                __searchBranchControl._dataList._selectSuccessButton.Click += (s1, e1) =>
                {
                    string __selectData = __searchBranchControl._dataList._selectList();
                    this._setDataStr(_g.d.resource_report._branch, __selectData);
                    __searchBranchControl.Close();
                };
                __searchBranchControl.StartPosition = FormStartPosition.CenterScreen;
                __searchBranchControl.ShowDialog();

            }

            if (__searchControl != null)
            {
                MyLib._myTextBox __getControl = (MyLib._myTextBox)sender;
                _searchName = name;
                _searchTextBox = __getControl.textBox;

                if (this._mode == _reportEnum.สินค้า_รายงานสรุปเคลื่อนไหวตามปริมาณ && name.Equals(_g.d.resource_report._item_group_code))
                {
                    __searchControl.StartPosition = FormStartPosition.CenterScreen;
                    __searchControl.ShowDialog();
                }
                else
                    MyLib._myGlobal._startSearchBox(__getControl, label_name, __searchControl, false);
            }


        }

        void _condition_ic_screen__textBoxChanged(object sender, string name)
        {
            if (name.Equals(_g.d.ic_resource._ic_name))
            {
                _searchTextBox = (TextBox)sender;
                _searchName = name;
                //   _search(true);
            }
            if (name.Equals(_g.d.ic_resource._ic_normal))
            {
                _searchTextBox = (TextBox)sender;
                _searchName = name;
                //   _search(true);
            }
            if (name.Equals(_g.d.resource_report._from_ap))
            {
                _searchTextBox = (TextBox)sender;
                _searchName = name;
                if (this._getDataStr(_g.d.resource_report._from_ap).Length > 0)
                {
                    _search(true);
                }
            }
            if (name.Equals(_g.d.resource_report._to_ap))
            {
                _searchTextBox = (TextBox)sender;
                _searchName = name;
                if (this._getDataStr(_g.d.resource_report._to_ap).Length > 0)
                {
                    _search(true);
                }
            }
        }

        public void _init(_reportEnum mode)
        {
            Boolean __searchIcShow = false;
            Boolean __searchIc_Group_Show = false;
            Boolean __searchIc_Brand_show = false;
            Boolean __searchIc_Type_show = false;
            Boolean __searchIc_Unit_show = false;
            Boolean __searchIc_Warehouse_show = false;
            Boolean __searchIc_Shelf_show = false;
            Boolean __search_ap_show = false;
            Boolean __search_department_show = false;
            Boolean __search_docno_show = false;
            Boolean __search_category_show = false;
            Boolean __search_pattern_show = false;
            Boolean __search_erp_user_show = false;
            Boolean __search_allocation_list_show = false;
            Boolean __search_serial_number_show = false;
            Boolean __search_status_approve = false;
            Boolean __search_ar_type_show = false;

            this._table_name = _g.d.resource_report._table;
            DateTime __today = DateTime.Now;

            this._mode = mode;

            switch (mode)
            {
                #region สินค้า

                case _reportEnum.สินค้า_รายงานกำไรขั้นต้นตามเอกสารแบบมีส่วนลด:
                    this._maxColumn = 2;
                    this._addDateBox(0, 0, 1, 0, _g.d.resource_report._from_date, 1, true, true);
                    this._addDateBox(0, 1, 1, 0, _g.d.resource_report._to_date, 1, true, true);
                    break;
                case _reportEnum.สินค้า_รายงานรายละเอียดสินค้า:
                    // รายงานรายละเอียดสินค้า
                    this._maxColumn = 2;
                    //this._addTextBox(0, 0, 1, 0, _g.d.resource_report._from_item_code, 1, 1, 1, true, false, true);
                    //this._addTextBox(0, 1, 1, 0, _g.d.resource_report._to_item_code, 1, 1, 1, true, false, true);
                    //this._addTextBox(1, 0, 1, 0, _g.d.resource_report._from_group, 1, 1, 1, true, false, true);
                    //this._addTextBox(1, 1, 1, 0, _g.d.resource_report._to_group, 1, 1, 1, true, false, true);
                    //this._addTextBox(2, 0, 1, 0, _g.d.resource_report._from_brand, 1, 1, 1, true, false, true);
                    //this._addTextBox(2, 1, 1, 0, _g.d.resource_report._to_brand, 1, 1, 1, true, false, true);
                    //this._addTextBox(3, 0, 1, 0, _g.d.resource_report._from_type, 1, 1, 1, true, false, true);
                    //this._addTextBox(3, 1, 1, 0, _g.d.resource_report._to_type, 1, 1, 1, true, false, true);
                    //MyLib._myGroupBox __order_by_groupBox = this._addGroupBox(4, 0, 2, 2, 2, _g.d.resource_report._sort_order_by, true);
                    //this._addRadioButtonOnGroupBox(0, 0, __order_by_groupBox, _g.d.resource_report._item_code, 0, true);
                    //this._addRadioButtonOnGroupBox(0, 1, __order_by_groupBox, _g.d.resource_report._item_group_code, 1, false);
                    //this._addRadioButtonOnGroupBox(1, 0, __order_by_groupBox, _g.d.resource_report._item_brand_code, 2, false);
                    //this._addRadioButtonOnGroupBox(1, 1, __order_by_groupBox, _g.d.resource_report._item_type_code, 3, false);
                    break;
                case _reportEnum.Item_by_supplier:
                    // รายละเอียดสินค้าตามเจ้าหนี้
                    break;
                case _reportEnum.Cancel_Transfer_Item_and_material:
                    this._maxColumn = 2;
                    this._addDateBox(0, 0, 1, 0, _g.d.resource_report._from_date, 1, true, true);
                    this._addDateBox(0, 1, 1, 0, _g.d.resource_report._to_date, 1, true, true);
                    break;
                case _reportEnum.สินค้า_รายงานรายละเอียดบาร์โค๊ด:
                    //this._maxColumn = 2;
                    //this._addDateBox(0, 0, 1, 0, _g.d.resource_report._from_date, 1, true, true);
                    //this._addDateBox(0, 1, 1, 0, _g.d.resource_report._to_date, 1, true, true);
                    break;
                case _reportEnum.สินค้า_รายงานการใช้แม่สี:
                    this._maxColumn = 2;
                    this._addDateBox(0, 0, 1, 0, _g.d.resource_report._from_date, 1, true, true);
                    this._addDateBox(0, 1, 1, 0, _g.d.resource_report._to_date, 1, true, true);
                    break;
                case _reportEnum.สินค้า_รายงานสูตรสีผสม:
                    //this._maxColumn = 2;
                    //this._addDateBox(0, 0, 1, 0, _g.d.resource_report._from_date, 1, true, true);
                    //this._addDateBox(0, 1, 1, 0, _g.d.resource_report._to_date, 1, true, true);
                    break;
                case _reportEnum.สินค้า_รายงานราคาขายสินค้า:
                    //this._maxColumn = 2;
                    //this._addDateBox(0, 0, 1, 0, _g.d.resource_report._from_date, 1, true, true);
                    //this._addDateBox(0, 1, 1, 0, _g.d.resource_report._to_date, 1, true, true);
                    break;
                case _reportEnum.สินค้า_รายงานราคาซื้อสินค้า:
                    //this._maxColumn = 2;
                    //this._addDateBox(0, 0, 1, 0, _g.d.resource_report._from_date, 1, true, true);
                    //this._addDateBox(0, 1, 1, 0, _g.d.resource_report._to_date, 1, true, true);
                    break;
                case _reportEnum.Item_Giveaway:
                    this._maxColumn = 2;
                    this._addDateBox(0, 0, 1, 0, _g.d.resource_report._from_date, 1, true, true);
                    this._addDateBox(0, 1, 1, 0, _g.d.resource_report._to_date, 1, true, true);
                    break;
                case _reportEnum.Item_by_serial:
                    // รายละเอียดสินค้าแบบมี Serial
                    this._maxColumn = 2;
                    //this._addTextBox(0, 0, 1, 0, _g.d.resource_report._from_item_code, 1, 1, 1, true, false, true);
                    //this._addTextBox(0, 1, 1, 0, _g.d.resource_report._to_item_code, 1, 1, 1, true, false, true);

                    /* toe
                    this._addDateBox(1, 0, 1, 0, _g.d.resource_report._from_date_import, 1, true, true);
                    this._addDateBox(1, 1, 1, 0, _g.d.resource_report._to_date_import, 1, true, true);
                    this._addDateBox(2, 0, 1, 0, _g.d.resource_report._from_date_end_insulance, 1, true, true);
                    this._addDateBox(2, 1, 1, 0, _g.d.resource_report._to_date_end_insulance, 1, true, true);

                    string[] __data = { _g.d.resource_report._none_select, _g.d.resource_report._in_stock, _g.d.resource_report._widen_already, _g.d.resource_report._sale_already, _g.d.resource_report._cancel };
                    this._addComboBox(3, 0, _g.d.resource_report._status_item, true, __data, true);
                     * */
                    this._addTextBox(0, 0, 1, 0, _g.d.resource_report._from_group, 1, 2, 1, true, false, true);
                    this._addTextBox(0, 1, 1, 0, _g.d.resource_report._to_group, 1, 2, 1, true, false, true);

                    break;
                case _reportEnum.Serial_number:
                    // รายงานเคลื่อนไหว Serial Number
                    this._maxColumn = 2;
                    this._addDateBox(0, 0, 1, 0, _g.d.resource_report._from_date, 1, true, false, true);
                    this._addDateBox(0, 1, 1, 0, _g.d.resource_report._to_date, 1, true, false, true);

                    this._addTextBox(1, 0, 1, 0, _g.d.resource_report._from_item_code, 1, 2, 1, true, false, true);
                    this._addTextBox(1, 1, 1, 0, _g.d.resource_report._to_item_code, 1, 2, 1, true, false, true);

                    // โต๋ ย้ายเอาไปใส่ใน grid
                    //this._addTextBox(2, 0, 1, 0, _g.d.resource_report._from_serail_no, 1, 2, 1, true, false, true);
                    //this._addTextBox(2, 1, 1, 0, _g.d.resource_report._to_serail_no, 1, 2, 1, true, false, true);
                    //this._addCheckBox(3, 0, _g.d.resource_report._order_by_wh, true, true, false);
                    __searchIcShow = true;
                    break;
                case _reportEnum.Item_status:
                    // รายงานสถานะสินค้า
                    this._maxColumn = 2;
                    this._addDateBox(0, 0, 1, 0, _g.d.resource_report._from_date, 1, true, true);
                    this._addDateBox(0, 1, 1, 0, _g.d.resource_report._to_date, 1, true, true);
                    this._addTextBox(1, 0, 1, 0, _g.d.resource_report._from_group, 1, 2, 1, true, false, true);
                    this._addTextBox(1, 1, 1, 0, _g.d.resource_report._to_group, 1, 2, 1, true, false, true);

                    //this._addTextBox(2, 0, 1, 0, _g.d.resource_report._from_item_code, 1, 2, 1, true, false, true);
                    //this._addTextBox(2, 1, 1, 0, _g.d.resource_report._to_item_code, 1, 2, 1, true, false, true);
                    //MyLib._myGroupBox __status = this._addGroupBox(3, 0, 2, 2, 2, _g.d.resource_report._item_status, true);
                    //this._addRadioButtonOnGroupBox(0, 0, __status, _g.g._item_status[0], 0, true);
                    //this._addRadioButtonOnGroupBox(0, 1, __status, _g.g._item_status[1], 1, false);
                    break;
                case _reportEnum.Result_item_export:
                    // รายงานสรุปยอดค้างส่ง
                    this._maxColumn = 2;
                    this._addTextBox(0, 0, 1, 0, _g.d.resource_report._from_item_code, 1, 2, 1, true, false, true);
                    this._addTextBox(0, 1, 1, 0, _g.d.resource_report._to_item_code, 1, 2, 1, true, false, true);
                    this._addTextBox(1, 0, 1, 0, _g.d.resource_report._from_group, 1, 2, 1, true, false, true);
                    this._addTextBox(1, 1, 1, 0, _g.d.resource_report._to_group, 1, 2, 1, true, false, true);
                    this._addTextBox(2, 0, 1, 0, _g.d.resource_report._from_unit, 1, 2, 1, true, false, true);
                    this._addTextBox(2, 1, 1, 0, _g.d.resource_report._to_unit, 1, 2, 1, true, false, true);
                    break;
                case _reportEnum.Result_item_import:
                    // รายงานสรุปยอดค้างรับ
                    this._maxColumn = 2;
                    this._addTextBox(0, 0, 1, 0, _g.d.resource_report._from_item_code, 1, 2, 1, true, false, true);
                    this._addTextBox(0, 1, 1, 0, _g.d.resource_report._to_item_code, 1, 2, 1, true, false, true);
                    this._addTextBox(1, 0, 1, 0, _g.d.resource_report._from_group, 1, 2, 1, true, false, true);
                    this._addTextBox(1, 1, 1, 0, _g.d.resource_report._to_group, 1, 2, 1, true, false, true);
                    break;
                case _reportEnum.สินค้า_ถึงจุดสั่งซื้อ_ตามสินค้า:
                    this._maxColumn = 2;
                    this._addDateBox(0, 0, 1, 0, _g.d.resource_report._to_date, 1, true);
                    break;
                case _reportEnum.สินค้า_รายงานยอดคงเหลือสินค้า:
                    // รายงานยอดคงเหลือสินค้า
                    this._maxColumn = 2;
                    this._addDateBox(0, 0, 1, 0, _g.d.resource_report._to_date, 1, true);
                    this._addCheckBox(1, 0, _g.d.resource_report._warehouse, false, true, false);
                    this._addCheckBox(2, 0, _g.d.resource_report._display_shelf, false, true, false);
                    this._addCheckBox(3, 0, _g.d.resource_report._displpay_item_balance_equal_zero, false, true, false);
                    this._setDataDate(_g.d.resource_report._to_date, new DateTime(__today.Year, __today.Month, __today.Day));
                    // GgroupBox แรกเงื่อนไขการพิมพ์รายงานมาตรฐาน
                    //MyLib._myGroupBox __print_document_standard_groupbox = this._addGroupBox(0, 0, 4, 2, 2, "ตัวเลือก", false);
                    //this._addRadioButtonOnGroupBox(0, 0, __print_document_standard_groupbox, _g.d.resource_report._order_by_item, 0, true);
                    //this._addRadioButtonOnGroupBox(0, 1, __print_document_standard_groupbox, _g.d.resource_report._order_by_category, 0, false);
                    //this._addRadioButtonOnGroupBox(1, 0, __print_document_standard_groupbox, _g.d.resource_report._order_by_wh, 0, false);
                    //this._addRadioButtonOnGroupBox(1, 1, __print_document_standard_groupbox, _g.d.resource_report._order_by_type, 0, false);
                    //this._addRadioButtonOnGroupBox(2, 0, __print_document_standard_groupbox, _g.d.resource_report._order_by_group, 0, false);
                    //this._addRadioButtonOnGroupBox(2, 1, __print_document_standard_groupbox, _g.d.resource_report._order_by_format, 0, false);
                    //this._addRadioButtonOnGroupBox(3, 0, __print_document_standard_groupbox, _g.d.resource_report._order_by_brand, 0, false);

                    //this._addCheckBox(5, 0, _g.d.resource_report._print_report_at_date, false, false);
                    //this._addDateBox(5, 1, 1, 0, _g.d.resource_report._print_report_at_date, 1, true);
                    //this._addCheckBox(6, 0, _g.d.resource_report._displpay_item_balance_equal_zero, true, false);
                    //this._addCheckBox(6, 1, _g.d.resource_report._display_lot_item, true, false);
                    //this._addCheckBox(7, 0, _g.d.resource_report._display_item_in_warehouse, true, false);
                    //this._addCheckBox(7, 1, _g.d.resource_report._display_warehouse, true, false);
                    //this._addCheckBox(8, 0, _g.d.resource_report._display_amount_item, true, false);
                    //this._addCheckBox(8, 1, _g.d.resource_report._display_shelf, true, false);
                    //this._addCheckBox(9, 0, _g.d.resource_report._print_all, true, false);
                    //this._addCheckBox(9, 1, _g.d.resource_report._display_only_total_price, true, false);
                    //this._addCheckBox(10, 0, _g.d.resource_report._display_item_name2, true, false);

                    //ตามพี่อเนก
                    //this._addTextBox(0, 0, 1, 0, _g.d.resource_report._from_item_code, 1, 10);
                    //this._addTextBox(0, 1, 1, 0, _g.d.resource_report._to_item_code, 1, 10);
                    break;
                case _reportEnum.สินค้า_รายงานยอดคงเหลือสินค้า_Lot:
                    this._maxColumn = 2;
                    this._addDateBox(0, 0, 1, 0, _g.d.resource_report._to_date, 1, true);
                    this._setDataDate(_g.d.resource_report._to_date, new DateTime(__today.Year, __today.Month, __today.Day));


                    break;
                case _reportEnum.Item_balance_hightest:
                    //รายงานยอดคงเหลือสินค้าที่จุดสูงสุด
                    this._maxColumn = 2;
                    this._addTextBox(0, 0, 1, 0, _g.d.resource_report._from_group, 1, 2, 1, true, false);
                    this._addTextBox(0, 1, 1, 0, _g.d.resource_report._to_group, 1, 2, 1, true, false);
                    //this._addTextBox(1, 0, 1, 0, _g.d.resource_report._from_item_code, 1, 2, 1, true, false);
                    //this._addTextBox(1, 1, 1, 0, _g.d.resource_report._to_item_code, 1, 2, 1, true, false);
                    break;
                case _reportEnum.สินค้า_รายงานสินค้าที่ไม่มีการเคลื่อนไหว:
                    // รายงานสินค้าที่ไม่มีการเคลื่อนไหว
                    this._maxColumn = 2;
                    string __formatNumber = MyLib._myGlobal._getFormatNumber("m00");

                    this._addNumberBox(0, 0, 1, 0, _g.d.resource_report._count_day_from, 1, 2, true, __formatNumber);
                    this._addNumberBox(0, 1, 1, 0, _g.d.resource_report._count_day_to, 1, 2, true, __formatNumber);
                    this._addDateBox(1, 0, 1, 1, _g.d.resource_report._to_date, 1, true);
                    //
                    this._setDataNumber(_g.d.resource_report._count_day_from, 30);
                    this._setDataNumber(_g.d.resource_report._count_day_to, 5000);
                    this._setDataDate(_g.d.resource_report._to_date, new DateTime(__today.Year, __today.Month, __today.Day));
                    break;
                case _reportEnum.Result_transfer_item:
                    // รายงานสรุปเคลื่อนไหวปริมาณสินค้า
                    break;
                case _reportEnum.สินค้า_รายงานสรุปเคลื่อนไหวตามปริมาณ:
                    this._maxColumn = 2;
                    this._addDateBox(0, 0, 1, 0, _g.d.resource_report._from_date, 1, true);
                    this._addDateBox(0, 1, 1, 0, _g.d.resource_report._to_date, 1, true);
                    this._addTextBox(1, 0, 0, 0, _g.d.resource_report._item_group_code, 1, 25, 1, true, false, true);
                    this._addCheckBox(2, 0, _g.d.resource_report._movement_only, false, true, true);

                    //
                    this._setDataDate(_g.d.resource_report._from_date, new DateTime(__today.Year, __today.Month, 1));
                    this._setDataDate(_g.d.resource_report._to_date, new DateTime(__today.Year, __today.Month, __today.Day));

                    __searchIc_Group_Show = true;
                    break;
                case _reportEnum.สินค้า_รายงานเคลื่อนไหว:
                    {
                        // รายงานเคลื่อนไหวสินค้า
                        DateTime __dd = DateTime.Now;
                        DateTime __begin_day = new DateTime(__dd.Year, __dd.Month, 1);

                        DateTime __end_day = __begin_day.AddMonths(1).Subtract(new TimeSpan(1, 0, 0, 0));

                        this._maxColumn = 2;
                        this._addDateBox(0, 0, 1, 0, _g.d.resource_report._from_date, 1, true);
                        this._addDateBox(0, 1, 1, 0, _g.d.resource_report._to_date, 1, true);
                        //this._addCheckBox(1, 0, _g.d.resource_report._print_all, false, true, false);
                        this._setDataDate(_g.d.resource_report._from_date, __begin_day);
                        this._setDataDate(_g.d.resource_report._to_date, __end_day);

                        this._addCheckBox(1, 0, _g.d.resource_report._show_remark, false, true);
                    }
                    break;
                case _reportEnum.Cancel_Item_and_Staple:
                    // รายงานยกเลิกสินค้าและวัตถุดิบ  คงเหลือยกมา
                    this._maxColumn = 2;
                    this._addDateBox(0, 0, 1, 0, _g.d.resource_report._from_date, 1, true);
                    this._addDateBox(0, 1, 1, 0, _g.d.resource_report._to_date, 1, true);
                    break;
                case _reportEnum.Cancel_Import_Item_ready:
                    // รายงานการยกเลิกรับสินค้าสำเร็จรูป
                    this._maxColumn = 2;
                    this._addDateBox(0, 0, 1, 0, _g.d.resource_report._from_date, 1, true);
                    this._addDateBox(0, 1, 1, 0, _g.d.resource_report._to_date, 1, true);
                    break;
                case _reportEnum.Cancel_Implement_Item_and_Staple_Over:
                    // รายงานปรับปรุงสต๊อกสินค้าและวัตถุดิบ (เกิน)
                    this._maxColumn = 2;
                    this._addDateBox(0, 0, 1, 0, _g.d.resource_report._from_date, 1, true);
                    this._addDateBox(0, 1, 1, 0, _g.d.resource_report._to_date, 1, true);
                    break;
                case _reportEnum.Cancel_Implement_Item_and_Staple_Minus:
                    // รายงานยกเลิกปรับปรุงสต๊อกสินค้าและวัตถุดิบ (ขาด)
                    this._maxColumn = 2;
                    this._addDateBox(0, 0, 1, 0, _g.d.resource_report._from_date, 1, true);
                    this._addDateBox(0, 1, 1, 0, _g.d.resource_report._to_date, 1, true);
                    break;
                case _reportEnum.Item_transfer_standard:
                    // รายงานเคลื่อนไหวสินค้าต้นทุนมาตรฐาน
                    this._maxColumn = 2;
                    this._addNumberBox(0, 0, 1, 0, _g.d.resource_report._from_period, 1, 0, true);
                    this._addNumberBox(0, 1, 1, 0, _g.d.resource_report._to_period, 1, 0, true);
                    this._addDateBox(1, 0, 1, 0, _g.d.resource_report._from_date, 1, true);
                    this._addDateBox(1, 1, 1, 0, _g.d.resource_report._to_date, 1, true);
                    this._addTextBox(2, 0, 1, 0, _g.d.resource_report._from_warehouse, 1, 2, 1, true, false);
                    this._addTextBox(2, 1, 1, 0, _g.d.resource_report._to_warehouse, 1, 2, 1, true, false);
                    this._addTextBox(3, 0, 1, 0, _g.d.resource_report._from_shelf, 1, 2, 1, true, false);
                    this._addTextBox(3, 1, 1, 0, _g.d.resource_report._to_shelf, 1, 2, 1, true, false);
                    this._addTextBox(4, 0, 1, 0, _g.d.resource_report._from_item_code, 1, 2, 1, true, false);
                    this._addTextBox(4, 1, 1, 0, _g.d.resource_report._to_item_code, 1, 2, 1, true, false);
                    break;
                case _reportEnum.สินค้า_รายงานเคลื่อนไหวสีผสม:
                    this._maxColumn = 2;
                    this._addDateBox(0, 0, 1, 0, _g.d.resource_report._from_date, 1, true);
                    this._addDateBox(0, 1, 1, 0, _g.d.resource_report._to_date, 1, true);
                    break;
                case _reportEnum.สินค้า_รายงานบัญชีคุมพิเศษ:
                    this._maxColumn = 2;
                    this._addDateBox(0, 0, 1, 0, _g.d.resource_report._from_date, 1, true);
                    this._addDateBox(0, 1, 1, 0, _g.d.resource_report._to_date, 1, true);
                    //this._addCheckBox(1, 0, _g.d.resource_report._averrage, false, true, false);
                    //this._addCheckBox(1, 1, _g.d.resource_report._fifo, false, true, false);
                    //this._addCheckBox(2, 0, _g.d.resource_report._standard, false, true, false);
                    //this._addCheckBox(2, 1, _g.d.resource_report._panel_costs, false, true, false);
                    //this._addCheckBox(3, 0, _g.d.resource_report._print_by_format_revue, false, true, false);
                    //this._addCheckBox(3, 1, _g.d.resource_report._print_all, false, true, false);
                    //this._addCheckBox(4, 0, _g.d.resource_report._not_display_amount_balance_item, false, true, false);

                    //MyLib._myGroupBox __warehouse_groupbox = this._addGroupBox(5, 0, 3, 2, 2, _g.d.resource_report._warehouse, false);
                    //this._addRadioButtonOnGroupBox(0, 0, __warehouse_groupbox, _g.d.resource_report._print_total_warehouse, 0, false);
                    //this._addRadioButtonOnGroupBox(1, 0, __warehouse_groupbox, _g.d.resource_report._print_split_warehouse, 0, false);
                    //this._addRadioButtonOnGroupBox(2, 0, __warehouse_groupbox, _g.d.resource_report._print_all_wh_and_other_wh, 0, false);
                    break;
                case _reportEnum.รายงานผลต่างจากการตรวจนับ:
                case _reportEnum.Stock_no_count_no_balance:
                case _reportEnum.สินค้า_รายงานการตรวจสอบสินค้า_serial:
                    {
                        this._maxColumn = 2;
                        this._addDateBox(0, 0, 1, 0, _g.d.resource_report._from_date, 1, true);
                        this._addDateBox(0, 1, 1, 0, _g.d.resource_report._to_date, 1, true);
                    }
                    break;
                case _reportEnum.Print_document_for_count_by_item:
                case _reportEnum.Implement_Item:
                case _reportEnum.Print_document_for_count_by_warehouse:
                    // พิมพ์เอกสารเพื่อตรวจนับ-ตามสินค้า   รายงานผลต่างจากการตรวจนับ   รายงานการปรับปรุงยอดสินค้า   พิมพ์เอกสารเพื่อตรวจนับ-ตามคลัง
                    this._maxColumn = 2;
                    //this._addTextBox(0, 0, 1, 0, _g.d.resource_report._from_item_code, 1, 2, 1, true, false);
                    //this._addTextBox(0, 1, 1, 0, _g.d.resource_report._to_item_code, 1, 2, 1, true, false);
                    this._addTextBox(1, 0, 1, 0, _g.d.resource_report._from_warehouse, 1, 2, 1, true, false);
                    this._addTextBox(1, 1, 1, 0, _g.d.resource_report._to_warehouse, 1, 2, 1, true, false);
                    this._addDateBox(2, 0, 1, 0, _g.d.resource_report._from_date, 1, true, true);
                    this._addDateBox(2, 1, 1, 0, _g.d.resource_report._to_date, 1, true, true);
                    this._addTextBox(3, 0, 1, 0, _g.d.resource_report._from_docno, 1, 2, 1, true, false);
                    this._addTextBox(3, 1, 1, 0, _g.d.resource_report._to_docno, 1, 2, 1, true, false);
                    break;
                case _reportEnum.Span_import_item:
                    // รายงานการประเมินการรับสินค้า
                    this._maxColumn = 2;

                    this._addDateBox(0, 0, 1, 0, _g.d.resource_report._from_date, 1, true, true);
                    this._addDateBox(0, 1, 1, 0, _g.d.resource_report._to_date, 1, true, true);
                    this._addTextBox(1, 0, 1, 0, _g.d.resource_report._from_payable, 1, 2, 1, true, false);
                    this._addTextBox(1, 1, 1, 0, _g.d.resource_report._to_payable, 1, 2, 1, true, false);
                    this._addTextBox(2, 0, 1, 0, _g.d.resource_report._from_group, 1, 2, 1, true, false);
                    this._addTextBox(2, 1, 1, 0, _g.d.resource_report._to_group, 1, 2, 1, true, false);
                    //this._addTextBox(3, 0, 1, 0, _g.d.resource_report._from_item_code, 1, 2, 1, true, false);
                    //this._addTextBox(3, 1, 1, 0, _g.d.resource_report._to_item_code, 1, 2, 1, true, false);
                    break;
                case _reportEnum.Lot_item:
                    // รายงาน Lot สินค้า
                    this._maxColumn = 2;
                    string[] __data3 = { _g.d.resource_report._date_import, _g.d.resource_report._from_docdate, _g.d.resource_report._from_item_code };
                    this._addComboBox(0, 0, _g.d.resource_report._order_by, true, __data3, true);
                    this._addDateBox(1, 0, 1, 0, _g.d.resource_report._from_date, 1, true, true);
                    this._addDateBox(1, 1, 1, 0, _g.d.resource_report._to_date, 1, true, true);
                    this._addCheckBox(2, 0, _g.d.resource_report._print_all, false, true, false);
                    break;
                case _reportEnum.สินค้า_รายงานปรับปรุงสินค้าและวัตถุดิบ_ขาด:
                case _reportEnum.สินค้า_รายงานปรับปรุงสินค้าและวัตถุดิบ_เกิน:
                case _reportEnum.สินค้า_รายงานรับคืนเบิกสินค้าวัตถุดิบ:
                case _reportEnum.สินค้า_รายงานเบิกสินค้าวัตถุดิบ:
                case _reportEnum.สินค้า_รายงานโอนสินค้าและวัตถุดิบ:
                case _reportEnum.สินค้า_รายงานสินค้าและวัตถุดิบคงเหลือยกมา:
                case _reportEnum.สินค้า_รายงานสินค้าตรวจนับ:
                    this._maxColumn = 2;
                    this._addDateBox(0, 0, 1, 0, _g.d.resource_report._from_date, 1, true, true);
                    this._addDateBox(0, 1, 1, 0, _g.d.resource_report._to_date, 1, true, true);
                    this._addCheckBox(1, 0, _g.d.resource_report._display_detail, false, true, true);
                    this._addCheckBox(1, 1, _g.d.resource_report._show_cancel_document, false, true, false);
                    this._addCheckBox(2, 0, _g.d.resource_report._show_barcode, false, true, true);
                    break;
                case _reportEnum.สินค้า_รายงานรับสินค้าสำเร็จรูป:
                    this._maxColumn = 2;
                    this._addDateBox(0, 0, 1, 0, _g.d.resource_report._from_date, 1, true, true);
                    this._addDateBox(0, 1, 1, 0, _g.d.resource_report._to_date, 1, true, true);
                    this._addTextBox(1, 0, 1, 0, _g.d.resource_report._from_ap, 1, 0, 1, true, false);
                    this._addTextBox(1, 1, 1, 0, _g.d.resource_report._to_ap, 1, 0, 1, true, false);
                    this._addCheckBox(2, 0, _g.d.resource_report._display_detail, false, true, true);
                    this._addCheckBox(2, 1, _g.d.resource_report._show_cancel_document, false, true, false);
                    this._addCheckBox(3, 0, _g.d.resource_report._show_barcode, false, true, true);
                    __search_ap_show = true;

                    //this._addCheckBox(1, 0, _g.d.resource_report._print_all, false, true, false);
                    //this._addCheckBox(1, 1, _g.d.resource_report._print_by_format_revue, false, true, false);

                    //MyLib._myGroupBox __warehouse_groupbox = this._addGroupBox(2, 0, 3, 2, 2, "คลังสินค้า", false);
                    //this._addRadioButtonOnGroupBox(0, 0, __warehouse_groupbox, _g.d.resource_report._print_total_warehouse, 1, false);
                    //this._addRadioButtonOnGroupBox(1, 0, __warehouse_groupbox, _g.d.resource_report._print_split_warehouse, 1, false);
                    //this._addRadioButtonOnGroupBox(2, 0, __warehouse_groupbox, _g.d.resource_report._print_all_wh_and_other_wh, 0, false);
                    break;
                case _reportEnum.Receptance_Widen_by_Date:
                    // รายงานรับคืนเบิก-วันที่
                    this._maxColumn = 2;
                    this._addDateBox(0, 0, 1, 0, _g.d.resource_report._from_date, 1, true);
                    this._addDateBox(0, 1, 1, 0, _g.d.resource_report._to_date, 1, true);
                    this._addTextBox(1, 0, 1, 0, _g.d.resource_report._from_docno, 1, 2, 1, true, false);
                    this._addTextBox(1, 1, 1, 0, _g.d.resource_report._to_docno, 1, 2, 1, true, false);
                    this._addTextBox(2, 0, 1, 0, _g.d.resource_report._from_widen, 1, 2, 1, true, false);
                    this._addTextBox(2, 1, 1, 0, _g.d.resource_report._to_widen, 1, 2, 1, true, false);
                    this._addTextBox(3, 0, 1, 0, _g.d.resource_report._from_department, 1, 2, 1, true, false);
                    this._addTextBox(3, 1, 1, 0, _g.d.resource_report._to_department, 1, 2, 1, true, false);
                    this._addTextBox(4, 0, 1, 0, _g.d.resource_report._from_item_code, 1, 2, 1, true, false);
                    this._addTextBox(4, 1, 1, 0, _g.d.resource_report._to_item_code, 1, 2, 1, true, false);
                    this._addTextBox(5, 0, 1, 0, _g.d.resource_report._from_allocation, 1, 2, 1, true, false);
                    this._addTextBox(5, 1, 1, 0, _g.d.resource_report._to_allocation, 1, 2, 1, true, false);

                    string[] __data2 = { _g.d.resource_report._date, _g.d.resource_report._widen, _g.d.resource_report._department, _g.d.resource_report._item_code, "การจัดสรร" };
                    this._addComboBox(6, 0, _g.d.resource_report._order_by, true, __data2, true);
                    this._addCheckBox(7, 0, _g.d.resource_report._display_serial, false, true, false);

                    this._addTextBox(8, 0, 1, 0, _g.d.resource_report._from_group, 1, 2, 1, true, false);
                    this._addTextBox(8, 1, 1, 0, _g.d.resource_report._to_group, 1, 2, 1, true, false);
                    this._addTextBox(9, 0, 1, 0, _g.d.resource_report._from_type, 1, 2, 1, true, false);
                    this._addTextBox(9, 1, 1, 0, _g.d.resource_report._to_type, 1, 2, 1, true, false);
                    this._addTextBox(10, 0, 1, 0, _g.d.resource_report._from_category, 1, 2, 1, true, false);
                    this._addTextBox(10, 1, 1, 0, _g.d.resource_report._to_category, 1, 2, 1, true, false);
                    this._addTextBox(11, 0, 1, 0, _g.d.resource_report._from_format, 1, 2, 1, true, false);
                    this._addTextBox(11, 1, 1, 0, _g.d.resource_report._to_format, 1, 2, 1, true, false);
                    this._addTextBox(12, 0, 1, 0, _g.d.resource_report._from_brand, 1, 2, 1, true, false);
                    this._addTextBox(12, 1, 1, 0, _g.d.resource_report._to_brand, 1, 2, 1, true, false);
                    break;
                case _reportEnum.Cancel_Withdraw_Item_Staple:
                    // รายงานการเบิกสินค้า,วัตถุดิบ-วันที่เบิก
                    this._maxColumn = 2;
                    this._addDateBox(0, 0, 1, 0, _g.d.resource_report._from_date, 1, true);
                    this._addDateBox(0, 1, 1, 0, _g.d.resource_report._to_date, 1, true);

                    this._addTextBox(1, 0, 1, 0, _g.d.resource_report._from_docno, 1, 2, 1, true, false);
                    this._addTextBox(1, 1, 1, 0, _g.d.resource_report._to_docno, 1, 2, 1, true, false);
                    this._addCheckBox(2, 0, _g.d.resource_report._show_cancel_document, true, false, true);
                    //this._addTextBox(2, 0, 1, 0, _g.d.resource_report._from_item_code, 1, 2, 1, true, false);
                    //this._addTextBox(2, 1, 1, 0, _g.d.resource_report._to_item_code, 1, 2, 1, true, false);
                    //this._addTextBox(3, 0, 1, 0, _g.d.resource_report._from_widen, 1, 2, 1, true, false);
                    //this._addTextBox(3, 1, 1, 0, _g.d.resource_report._to_widen, 1, 2, 1, true, false);
                    //this._addTextBox(4, 0, 1, 0, _g.d.resource_report._from_department, 1, 2, 1, true, false);
                    //this._addTextBox(4, 1, 1, 0, _g.d.resource_report._to_department, 1, 2, 1, true, false);
                    //this._addCheckBox(5, 0, _g.d.resource_report._display_cost_unit, false, true, false);
                    //this._addCheckBox(5, 1, _g.d.resource_report._display_serial, false, true, false);


                    //this._addTextBox(7, 0, 1, 0, _g.d.resource_report._from_group, 1, 2, 1, true, false);
                    //this._addTextBox(7, 1, 1, 0, _g.d.resource_report._to_group, 1, 2, 1, true, false);
                    //this._addTextBox(8, 0, 1, 0, _g.d.resource_report._from_type, 1, 2, 1, true, false);
                    //this._addTextBox(8, 1, 1, 0, _g.d.resource_report._to_type, 1, 2, 1, true, false);
                    //this._addTextBox(9, 0, 1, 0, _g.d.resource_report._from_category, 1, 2, 1, true, false);
                    //this._addTextBox(9, 1, 1, 0, _g.d.resource_report._to_category, 1, 2, 1, true, false);
                    //this._addTextBox(10, 0, 1, 0, _g.d.resource_report._from_format, 1, 2, 1, true, false);
                    //this._addTextBox(10, 1, 1, 0, _g.d.resource_report._to_format, 1, 2, 1, true, false);
                    //this._addTextBox(11, 0, 1, 0, _g.d.resource_report._from_brand, 1, 2, 1, true, false);
                    //this._addTextBox(11, 1, 1, 0, _g.d.resource_report._to_brand, 1, 2, 1, true, false);
                    break;
                case _reportEnum.Cancel_Refunded_Withdraw_Item_Staple:
                    this._maxColumn = 2;
                    this._addDateBox(0, 0, 1, 0, _g.d.resource_report._from_date, 1, true);
                    this._addDateBox(0, 1, 1, 0, _g.d.resource_report._to_date, 1, true);
                    break;
                case _reportEnum.Expose_Item_price:
                    // รายงานกำหนดราคาสินค้า
                    this._maxColumn = 2;
                    this._addTextBox(0, 0, 1, 0, _g.d.resource_report._from_group, 1, 2, 1, true, false);
                    this._addTextBox(0, 1, 1, 0, _g.d.resource_report._to_group, 1, 2, 1, true, false);
                    //this._addTextBox(1, 0, 1, 0, _g.d.resource_report._from_item_code, 1, 2, 1, true, false);
                    //this._addTextBox(1, 1, 1, 0, _g.d.resource_report._to_item_code, 1, 2, 1, true, false);
                    this._addTextBox(2, 0, 1, 0, _g.d.resource_report._from_unit, 1, 2, 1, true, false);
                    this._addTextBox(2, 1, 1, 0, _g.d.resource_report._to_unit, 1, 2, 1, true, false);
                    this._addCheckBox(3, 0, _g.d.resource_report._order_by_group, false, true, false);
                    break;
                case _reportEnum.TransferItem_between_and_Detail:
                    // รายงานโอนสินค้าระหว่างคลังและรายการย่อย
                    this._maxColumn = 2;
                    this._addDateBox(0, 0, 1, 0, _g.d.resource_report._from_date, 1, true);
                    this._addDateBox(0, 1, 1, 0, _g.d.resource_report._to_date, 1, true);
                    this._addTextBox(1, 0, 1, 0, _g.d.resource_report._from_docno, 1, 2, 1, true, false);
                    this._addTextBox(1, 1, 1, 0, _g.d.resource_report._to_docno, 1, 2, 1, true, false);
                    this._addTextBox(2, 0, 1, 0, _g.d.resource_report._from_department, 1, 2, 1, true, false);
                    this._addTextBox(2, 1, 1, 0, _g.d.resource_report._to_department, 1, 2, 1, true, false);
                    this._addCheckBox(3, 0, _g.d.resource_report._display_serial, false, true, false);
                    break;
                case _reportEnum.TransferItem_between_Warehouse_by_output:
                    // รายงานโอนสินค้าระหว่างคลัง-ตามคลังโอนออก
                    this._maxColumn = 2;
                    this._addDateBox(0, 0, 1, 0, _g.d.resource_report._from_date, 1, true);
                    this._addDateBox(0, 1, 1, 0, _g.d.resource_report._to_date, 1, true);
                    this._addTextBox(1, 0, 1, 0, _g.d.resource_report._from_docno, 1, 2, 1, true, false);
                    this._addTextBox(1, 1, 1, 0, _g.d.resource_report._to_docno, 1, 2, 1, true, false);
                    this._addTextBox(2, 0, 1, 0, _g.d.resource_report._from_item_code, 1, 2, 1, true, false);
                    this._addTextBox(2, 1, 1, 0, _g.d.resource_report._to_item_code, 1, 2, 1, true, false);
                    this._addCheckBox(3, 0, _g.d.resource_report._display_serial, false, true, false);
                    break;
                case _reportEnum.Import_Stock_Item:
                    // รายงานการรับสต๊อกสินค้า
                    this._maxColumn = 2;
                    this._addDateBox(0, 0, 1, 0, _g.d.resource_report._from_date, 1, true);
                    this._addDateBox(0, 1, 1, 0, _g.d.resource_report._to_date, 1, true);
                    this._addTextBox(1, 0, 1, 0, _g.d.resource_report._from_item_code, 1, 2, 1, true, false);
                    this._addTextBox(1, 1, 1, 0, _g.d.resource_report._to_item_code, 1, 2, 1, true, false);
                    this._addTextBox(2, 0, 1, 0, _g.d.resource_report._from_department, 1, 2, 1, true, false);
                    this._addTextBox(2, 1, 1, 0, _g.d.resource_report._to_department, 1, 2, 1, true, false);
                    this._addTextBox(3, 0, 1, 0, _g.d.resource_report._from_warehouse, 1, 2, 1, true, false);
                    this._addTextBox(3, 1, 1, 0, _g.d.resource_report._to_warehouse, 1, 2, 1, true, false);
                    break;
                case _reportEnum.Record_Total_Item_First_Year:
                    // รายงานการบันทึกยอดสินค้ายกมาต้นปี 
                    this._maxColumn = 2;
                    this._addDateBox(0, 0, 1, 0, _g.d.resource_report._from_date, 1, true);
                    this._addDateBox(0, 1, 1, 0, _g.d.resource_report._to_date, 1, true);
                    this._addTextBox(1, 0, 1, 0, _g.d.resource_report._from_docno, 1, 2, 1, true, false);
                    this._addTextBox(1, 1, 1, 0, _g.d.resource_report._to_docno, 1, 2, 1, true, false);
                    this._addTextBox(2, 0, 1, 0, _g.d.resource_report._from_item_code, 1, 2, 1, true, false);
                    this._addTextBox(2, 1, 1, 0, _g.d.resource_report._to_item_code, 1, 2, 1, true, false);
                    this._addTextBox(3, 0, 1, 0, _g.d.resource_report._from_warehouse, 1, 2, 1, true, false);
                    this._addTextBox(3, 1, 1, 0, _g.d.resource_report._to_warehouse, 1, 2, 1, true, false);
                    this._addTextBox(4, 0, 1, 0, _g.d.resource_report._from_shelf, 1, 2, 1, true, false);
                    this._addTextBox(4, 1, 1, 0, _g.d.resource_report._to_shelf, 1, 2, 1, true, false);
                    break;
                case _reportEnum.Item_Material_Balance_Bring:
                    // รายงานรายการสินค้ายกมาต้นปี     
                    this._maxColumn = 2;
                    this._addDateBox(0, 0, 1, 0, _g.d.resource_report._from_date, 1, true);
                    this._addDateBox(0, 1, 1, 0, _g.d.resource_report._to_date, 1, true);
                    this._addTextBox(1, 0, 1, 0, _g.d.resource_report._from_item_code, 1, 2, 1, true, false);
                    this._addTextBox(1, 1, 1, 0, _g.d.resource_report._to_item_code, 1, 2, 1, true, false);
                    this._addTextBox(2, 0, 1, 0, _g.d.resource_report._from_warehouse, 1, 2, 1, true, false);
                    this._addTextBox(2, 1, 1, 0, _g.d.resource_report._to_warehouse, 1, 2, 1, true, false);
                    this._addTextBox(3, 0, 1, 0, _g.d.resource_report._from_shelf, 1, 2, 1, true, false);
                    this._addTextBox(3, 1, 1, 0, _g.d.resource_report._to_shelf, 1, 2, 1, true, false);
                    break;
                case _reportEnum.Item_Balance_now_Only_Serial:
                    // รายงานยอดคงเหลือสินค้า ณ ปัจจุบัน(เฉพาะสินค้ามี Serial) 
                    this._maxLabelWidth = new int[] { 40, 40, 40 };
                    this._maxColumn = 2;
                    //this._addDateBox(0, 0, 1, 0, _g.d.resource_report._to_date, 1, true);
                    //this._addCheckBox(0, 0, _g.d.resource_report._warehouse, false, true, false);
                    //this._addCheckBox(1, 0, _g.d.resource_report._display_shelf, false, true, false);
                    this._addCheckBox(0, 0, _g.d.resource_report._displpay_item_balance_equal_zero, false, true, false);
                    //this._setDataDate(_g.d.resource_report._to_date, new DateTime(__today.Year, __today.Month, __today.Day));

                    //this._addTextBox(0, 0, 1, 0, _g.d.resource_report._from_item_code, 1, 2, 1, true, false);
                    //this._addTextBox(0, 1, 1, 0, _g.d.resource_report._to_item_code, 1, 2, 1, true, false);
                    //this._addTextBox(1, 0, 1, 0, _g.d.resource_report._from_warehouse, 1, 2, 1, true, false);
                    //this._addTextBox(1, 1, 1, 0, _g.d.resource_report._to_warehouse, 1, 2, 1, true, false);
                    //this._addTextBox(2, 0, 1, 0, _g.d.resource_report._from_brand, 1, 2, 1, true, false);
                    //this._addTextBox(2, 1, 1, 0, _g.d.resource_report._to_brand, 1, 2, 1, true, false);
                    //this._addTextBox(3, 0, 1, 0, _g.d.resource_report._from_type, 1, 2, 1, true, false);
                    //this._addTextBox(3, 1, 1, 0, _g.d.resource_report._to_type, 1, 2, 1, true, false);
                    //this._addTextBox(4, 0, 1, 0, _g.d.resource_report._from_group, 1, 2, 1, true, false);
                    //this._addTextBox(4, 1, 1, 0, _g.d.resource_report._to_group, 1, 2, 1, true, false);
                    break;
                case _reportEnum.สินค้า_pos_sale_sugest_by_item_and_serial:
                    // รายงานวิเคราะห์ยอดขายสินค้าแบบแจกแจง-เรียงตามรหัส (แสดง Serial)
                    this._cashierSelectDialog = new _cashierSelectForm();
                    this._maxColumn = 2;
                    this._addDateBox(0, 0, 1, 0, _g.d.resource_report._from_date, 1, true, true);
                    this._addDateBox(0, 1, 1, 0, _g.d.resource_report._to_date, 1, true, true);
                    break;
                case _reportEnum.กำไรขั้นต้น_กลุ่มลูกค้า:
                    this._maxColumn = 2;
                    this._addDateBox(0, 0, 1, 0, _g.d.resource_report._from_date, 1, true, true);
                    this._addDateBox(0, 1, 1, 0, _g.d.resource_report._to_date, 1, true, true);

                    this._addTextBox(1, 0, 1, 0, _g.d.resource_report._ar_type, 1, 10, 1, true, false);
                    __search_ar_type_show = true;
                    break;
                case _reportEnum.สินค้า_รายงานรับสินค้าสำเร็จรูป_ยกเลิก:
                case _reportEnum.สินค้า_รายงานรับคืนเบิกสินค้าวัตถุดิบ_ยกเลิก:
                case _reportEnum.สินค้า_รายงานเบิกสินค้าวัตถุดิบ_ยกเลิก:

                case _reportEnum.สินค้า_รายงานโอนสินค้าและวัตถุดิบ_ยกเลิก:
                case _reportEnum.สินค้า_รายงานปรับปรุงสินค้าและวัตถุดิบ_เกิน_ยกเลิก:
                case _reportEnum.สินค้า_รายงานปรับปรุงสินค้าและวัตถุดิบ_ขาด_ยกเลิก:

                    this._maxColumn = 2;
                    this._addDateBox(0, 0, 1, 0, _g.d.resource_report._from_date, 1, true, true);
                    this._addDateBox(0, 1, 1, 0, _g.d.resource_report._to_date, 1, true, true);
                    break;

                #endregion

                #region ซื้อ

                case _reportEnum.ซื้อ_รายงานค้างรับตามใบสั่งซื้อ:
                    this._maxColumn = 2;
                    this._addDateBox(0, 0, 1, 0, _g.d.resource_report._from_date, 1, true, true);
                    this._addDateBox(0, 1, 1, 0, _g.d.resource_report._to_date, 1, true, true);
                    break;
                case _reportEnum.ซื้อ_จ่ายเงินล่วงหน้า:
                case _reportEnum.ซื้อ_จ่ายเงินล่วงหน้า_รับคืน:
                case _reportEnum.ซื้อ_จ่ายเงินมัดจำ:
                case _reportEnum.ซื้อ_จ่ายเงินมัดจำ_รับคืน:
                case _reportEnum.ซื้อ_รายงานยอดคงเหลือเงินมัดจำจ่าย:
                case _reportEnum.ซื้อ_รายงานยอดคงเหลือเงินจ่ายล่วงหน้า:
                    this._maxColumn = 2;
                    this._addDateBox(0, 0, 1, 0, _g.d.resource_report._from_date, 1, true, true);
                    this._addDateBox(0, 1, 1, 0, _g.d.resource_report._to_date, 1, true, true);
                    this._addCheckBox(1, 0, _g.d.resource_report._show_cancel_document, false, true, false);
                    break;
                case _reportEnum.ซื้อ_จ่ายเงินมัดจำ_ยกเลิก:
                case _reportEnum.ซื้อ_จ่ายเงินมัดจำ_รับคืน_ยกเลิก:
                case _reportEnum.ซื้อ_จ่ายเงินล่วงหน้า_รับคืน_ยกเลิก:
                case _reportEnum.ซื้อ_จ่ายเงินล่วงหน้า_ยกเลิก:
                case _reportEnum.ซื้อ_ซื้อสินค้าและบริการ_ยกเลิก:
                case _reportEnum.ซื้อ_เพิ่มหนี้ราคาผิด_ยกเลิก:
                case _reportEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด_ยกเลิก:
                    this._maxColumn = 2;
                    this._addDateBox(0, 0, 1, 0, _g.d.resource_report._from_date, 1, true, true);
                    this._addDateBox(0, 1, 1, 0, _g.d.resource_report._to_date, 1, true, true);
                    break;
                case _reportEnum.ซื้อ_รายงานตัดเงินมัดจำจ่าย:
                case _reportEnum.ซื้อ_รายงานตัดเงินจ่ายล่วงหน้า:
                    this._maxColumn = 2;
                    this._addDateBox(0, 0, 1, 0, _g.d.resource_report._from_date, 1, true, true);
                    this._addDateBox(0, 1, 1, 0, _g.d.resource_report._to_date, 1, true, true);
                    this._addCheckBox(1, 0, _g.d.resource_report._display_detail, false, true, true);
                    this._addCheckBox(1, 1, _g.d.resource_report._show_cancel_document, false, true, false);
                    break;
                case _reportEnum.ซื้อ_เพิ่มหนี้ราคาผิด:
                case _reportEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด:
                case _reportEnum.ซื้อ_ซื้อสินค้าและบริการ:
                case _reportEnum.ซื้อ_พาเชียล_ส่งคืนสินค้าหรือราคาผิด:
                case _reportEnum.ซื้อ_พาเชียล_รับสินค้า:
                case _reportEnum.ซื้อ_พาเชียล_ตั้งหนี้:
                case _reportEnum.ซื้อ_พาเชียล_เพิ่มหนี้:
                case _reportEnum.ซื้อ_พาเชียล_ลดหนี้:
                case _reportEnum.ซื้อ_รายงานใบสั่งซื้อ:
                case _reportEnum.ซื้อ_รายงานยกเลิกใบสั่งซื้อ:
                case _reportEnum.ซื้อ_รายงานอนุมัติใบสั่งซื้อ:
                case _reportEnum.ซื้อ_รายงานสถานะใบสั่งซื้อ:
                    this._maxColumn = 2;
                    this._addDateBox(0, 0, 1, 0, _g.d.resource_report._from_date, 1, true, true);
                    this._addDateBox(0, 1, 1, 0, _g.d.resource_report._to_date, 1, true, true);
                    this._addCheckBox(1, 0, _g.d.resource_report._display_detail, false, true, true);
                    this._addCheckBox(1, 1, _g.d.resource_report._show_cancel_document, false, true, false);
                    break;
                case _reportEnum.ซื้อ_ใบเสนอซื้อ:
                case _reportEnum.ซื้อ_ใบเสนอซื้อ_ยกเลิก:
                case _reportEnum.ซื้อ_ใบเสนอซื้อ_อนุมัติ:
                case _reportEnum.ซื้อ_ใบเสนอซื้อ_สถานะ:
                    this._maxColumn = 2;
                    this._addDateBox(0, 0, 1, 0, _g.d.resource_report._from_date, 1, true, true);
                    this._addDateBox(0, 1, 1, 0, _g.d.resource_report._to_date, 1, true, true);
                    this._addCheckBox(1, 0, _g.d.resource_report._display_detail, false, true, true);
                    if (mode == _reportEnum.ซื้อ_ใบเสนอซื้อ || mode == _reportEnum.ซื้อ_ใบเสนอซื้อ_สถานะ)
                    {
                        this._addCheckBox(1, 1, _g.d.resource_report._show_cancel_document, false, true, false);
                    }
                    break;

                #endregion

                #region ขาย

                case _reportEnum.ขาย_ขายสินค้าและบริการ_ยกเลิก:
                case _reportEnum.ขาย_เพิ่มหนี้สินค้าราคาผิด_ยกเลิก:
                case _reportEnum.ขาย_รับคืนสินค้า_ยกเลิก:
                    this._maxColumn = 2;
                    this._addDateBox(0, 0, 1, 0, _g.d.resource_report._from_date, 1, true, true);
                    this._addDateBox(0, 1, 1, 0, _g.d.resource_report._to_date, 1, true, true);
                    break;
                case _reportEnum.ขาย_เสนอราคา:
                case _reportEnum.ขาย_เสนอราคา_ยกเลิก:
                case _reportEnum.ขาย_เสนอราคา_อนุมัติ:
                case _reportEnum.ขาย_เสนอราคา_สถานะ:
                    this._maxColumn = 2;
                    this._addDateBox(0, 0, 1, 0, _g.d.resource_report._from_date, 1, true, true);
                    this._addDateBox(0, 1, 1, 0, _g.d.resource_report._to_date, 1, true, true);
                    this._addCheckBox(1, 0, _g.d.resource_report._display_detail, false, true, true);
                    if (mode == _reportEnum.ขาย_เสนอราคา || mode == _reportEnum.ขาย_เสนอราคา_สถานะ)
                    {
                        this._addCheckBox(1, 1, _g.d.resource_report._show_cancel_document, false, true, false);
                    }
                    break;
                case _reportEnum.ขาย_สั่งจองและสั่งซื้อสินค้า:
                case _reportEnum.ขาย_สั่งจองและสั่งซื้อสินค้า_ยกเลิก:
                case _reportEnum.ขาย_สั่งจองและสั่งซื้อสินค้า_สถานะ:
                case _reportEnum.ขาย_สั่งจองและสั่งซื้อสินค้า_อนุมัติ:
                    this._maxColumn = 2;
                    this._addDateBox(0, 0, 1, 0, _g.d.resource_report._from_date, 1, true, true);
                    this._addDateBox(0, 1, 1, 0, _g.d.resource_report._to_date, 1, true, true);
                    this._addCheckBox(1, 0, _g.d.resource_report._display_detail, false, true, true);
                    if (mode == _reportEnum.ขาย_สั่งจองและสั่งซื้อสินค้า || mode == _reportEnum.ขาย_สั่งจองและสั่งซื้อสินค้า_สถานะ)
                    {
                        this._addCheckBox(1, 1, _g.d.resource_report._show_cancel_document, false, true, false);
                    }
                    break;
                case _reportEnum.ขาย_สั่งขาย:
                case _reportEnum.ขาย_สั่งขาย_ยกเลิก:
                case _reportEnum.ขาย_สั่งขาย_สถานะ:
                case _reportEnum.ขาย_สั่งขาย_อนุมัติ:
                    this._maxColumn = 2;
                    this._addDateBox(0, 0, 1, 0, _g.d.resource_report._from_date, 1, true, true);
                    this._addDateBox(0, 1, 1, 0, _g.d.resource_report._to_date, 1, true, true);
                    this._addCheckBox(1, 0, _g.d.resource_report._display_detail, false, true, true);
                    if (mode == _reportEnum.ขาย_สั่งขาย || mode == _reportEnum.ขาย_สั่งขาย_สถานะ)
                    {
                        this._addCheckBox(1, 1, _g.d.resource_report._show_cancel_document, false, true, false);
                    }
                    break;
                case _reportEnum.ขาย_รับเงินล่วงหน้า:
                case _reportEnum.ขาย_รับเงินล่วงหน้า_คืน:
                case _reportEnum.ขาย_รับเงินล่วงหน้า_รายงานยอดคงเหลือ:
                case _reportEnum.ขาย_รับเงินล่วงหน้า_รายงานตัดเงิน:
                    {
                        this._maxColumn = 2;
                        int __row = 0;
                        this._addDateBox(__row, 0, 1, 0, _g.d.resource_report._from_date, 1, true, true);
                        this._addDateBox(__row++, 1, 1, 0, _g.d.resource_report._to_date, 1, true, true);
                        if (mode == _reportEnum.ขาย_รับเงินล่วงหน้า || mode == _reportEnum.ขาย_รับเงินล่วงหน้า_คืน)
                        {
                            this._addTextBox(__row, 0, 1, 0, _g.d.resource_report._from_time, 1, 5);
                            this._addTextBox(__row++, 1, 1, 0, _g.d.resource_report._to_time, 1, 5);
                        }
                        int __column = 0;
                        if (mode == _reportEnum.ขาย_รับเงินล่วงหน้า_รายงานตัดเงิน)
                        {
                            this._addCheckBox(__row, __column++, _g.d.resource_report._display_detail, false, true, true);
                        }
                        this._addCheckBox(__row, __column++, _g.d.resource_report._show_cancel_document, false, true, true);
                    }
                    break;
                case _reportEnum.ขาย_รับเงินล่วงหน้า_ยกเลิก:
                case _reportEnum.ขาย_รับเงินล่วงหน้า_คืน_ยกเลิก:
                    this._maxColumn = 2;
                    this._addDateBox(0, 0, 1, 0, _g.d.resource_report._from_date, 1, true, true);
                    this._addDateBox(0, 1, 1, 0, _g.d.resource_report._to_date, 1, true, true);
                    break;
                case _reportEnum.ขาย_รับเงินมัดจำ:
                case _reportEnum.ขาย_รับเงินมัดจำ_คืน:
                case _reportEnum.ขาย_รับเงินมัดจำ_รายงานยอดคงเหลือ:
                case _reportEnum.ขาย_รับเงินมัดจำ_รายงานตัดเงิน:
                    {
                        this._maxColumn = 2;
                        int __row = 0;
                        this._addDateBox(__row, 0, 1, 0, _g.d.resource_report._from_date, 1, true, true);
                        this._addDateBox(__row++, 1, 1, 0, _g.d.resource_report._to_date, 1, true, true);
                        if (mode == _reportEnum.ขาย_รับเงินมัดจำ || mode == _reportEnum.ขาย_รับเงินมัดจำ_คืน)
                        {
                            this._addTextBox(__row, 0, 1, 0, _g.d.resource_report._from_time, 1, 10);
                            this._addTextBox(__row++, 1, 1, 0, _g.d.resource_report._to_time, 1, 10);
                        }

                        int __column = 0;
                        if (mode == _reportEnum.ขาย_รับเงินมัดจำ_รายงานตัดเงิน)
                        {
                            this._addCheckBox(__row, __column++, _g.d.resource_report._display_detail, false, true, true);
                        }
                        this._addCheckBox(__row++, __column++, _g.d.resource_report._show_cancel_document, false, true, true);
                    }
                    break;
                case _reportEnum.ขาย_รับเงินมัดจำ_ยกเลิก:
                case _reportEnum.ขาย_รับเงินมัดจำ_คืน_ยกเลิก:
                    this._maxColumn = 2;
                    this._addDateBox(0, 0, 1, 0, _g.d.resource_report._from_date, 1, true, true);
                    this._addDateBox(0, 1, 1, 0, _g.d.resource_report._to_date, 1, true, true);
                    break;
                case _reportEnum.ขาย_ขายสินค้าและบริการ:
                case _reportEnum.ขาย_ขายสินค้าและบริการ_รายงานการออกใบกำกับภาษีอย่างเต็ม:
                case _reportEnum.ขาย_เพิ่มหนี้สินค้าราคาผิด:
                case _reportEnum.ขาย_รับคืนสินค้า:
                    this._cashierSelectDialog = new _cashierSelectForm();
                    this._maxColumn = 2;
                    this._addDateBox(0, 0, 1, 0, _g.d.resource_report._from_date, 1, true, true);
                    this._addDateBox(0, 1, 1, 0, _g.d.resource_report._to_date, 1, true, true);
                    this._addCheckBox(1, 0, _g.d.resource_report._display_detail, false, true, true);
                    this._addCheckBox(1, 1, _g.d.resource_report._show_cancel_document, false, true, false);
                    this._addComboBox(2, 0, _g.d.resource_report._vat_display_type, true, new string[] { _g.d.resource_report._vat_display_type_1, _g.d.resource_report._vat_display_type_2, _g.d.resource_report._vat_display_type_3 }, false);

                    // toe 
                    if (this._mode == _reportEnum.ขาย_ขายสินค้าและบริการ)
                    {
                        this._addComboBox(2, 1, _g.d.resource_report._category_sale, true, new string[] { _g.d.resource_report._all, _g.d.resource_report._credit_sale, _g.d.resource_report._cash_sale }, false);
                    }

                    this._addCheckBox(3, 0, _g.d.resource_report._cashier_check, false, true, false);
                    this._addButton(3, 1, 1, _g.d.resource_report._cashier_select);
                    this._buttonClick += (s1, e1) =>
                    {
                        this._cashierSelectDialog.ShowDialog();
                    };

                    if (this._mode == _reportEnum.ขาย_ขายสินค้าและบริการ)
                    {
                        if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLPOS ||
                            MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLPOSLite ||
                            MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLTomYumGoong ||
                            MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLPOSStarter ||
                            MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.IMSPOS ||
                            MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOS || 
                            MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOSProfessional)
                        {
                            if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLPOSLite ||
                            MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLTomYumGoong)
                            {
                                this._addCheckBox(4, 0, _g.d.resource_report._show_pos_only, false, true, false);
                            }
                            else
                            {
                                this._addComboBox(4, 0, _g.d.resource_report._show_pos_only, true, new string[] { _g.d.resource_report._all, _g.d.resource_report._show_pos_only, _g.d.resource_report._show_sale_only }, false, _g.d.resource_report._show_only);
                            }
                        }
                    }
                    break;
                case _reportEnum.ขาย_ขายสินค้าและบริการ_ลดหนี้_เพิ่มหนี้:
                    this._maxColumn = 2;
                    this._addDateBox(0, 0, 1, 0, _g.d.resource_report._from_date, 1, true, true);
                    this._addDateBox(0, 1, 1, 0, _g.d.resource_report._to_date, 1, true, true);

                    break;

                #endregion

                #region เจ้าหนี้

                case _reportEnum.เจ้าหนี้_ตั้งหนี้ยกมา:
                case _reportEnum.เจ้าหนี้_เพิ่มหนี้ยกมา:
                case _reportEnum.เจ้าหนี้_ลดหนี้ยกมา:
                    this._maxColumn = 2;
                    this._addDateBox(0, 0, 1, 0, _g.d.resource_report._from_date, 1, true, true);
                    this._addDateBox(0, 1, 1, 0, _g.d.resource_report._to_date, 1, true, true);
                    break;
                case _reportEnum.เจ้าหนี้_ตั้งหนี้อื่น:
                case _reportEnum.เจ้าหนี้_เพิ่มหนี้อื่น:
                case _reportEnum.เจ้าหนี้_ลดหนี้อื่น:
                    this._maxColumn = 2;
                    this._addDateBox(0, 0, 1, 0, _g.d.resource_report._from_date, 1, true, true);
                    this._addDateBox(0, 1, 1, 0, _g.d.resource_report._to_date, 1, true, true);
                    this._addCheckBox(1, 0, _g.d.resource_report._show_cancel_document, false, true, false);
                    break;
                case _reportEnum.เจ้าหนี้_ตั้งหนี้อื่น_ยกเลิก:
                case _reportEnum.เจ้าหนี้_เพิ่มหนี้อื่น_ยกเลิก:
                case _reportEnum.เจ้าหนี้_ลดหนี้อื่น_ยกเลิก:

                case _reportEnum.เจ้าหนี้_รับวางบิล_ยกเลิก:
                case _reportEnum.เจ้าหนี้_จ่ายชำระหนี้_ยกเลิก:
                    {
                        this._maxColumn = 2;
                        this._addDateBox(0, 0, 1, 0, _g.d.resource_report._from_date, 1, true, true);
                        this._addDateBox(0, 1, 1, 0, _g.d.resource_report._to_date, 1, true, true);
                    }
                    break;

                #endregion

                #region ลูกหนี้

                case _reportEnum.ลูกหนี้_ตั้งหนี้ยกมา:
                case _reportEnum.ลูกหนี้_เพิ่มหนี้ยกมา:
                case _reportEnum.ลูกหนี้_ลดหนี้ยกมา:
                    this._maxColumn = 2;
                    this._addDateBox(0, 0, 1, 0, _g.d.resource_report._from_date, 1, true, true);
                    this._addDateBox(0, 1, 1, 0, _g.d.resource_report._to_date, 1, true, true);
                    break;
                case _reportEnum.ลูกหนี้_รับชำระหนี้:
                case _reportEnum.ลูกหนี้_รับวางบิล:
                case _reportEnum.เจ้าหนี้_จ่ายชำระหนี้:
                case _reportEnum.เจ้าหนี้_รับวางบิล:
                    this._maxColumn = 2;
                    this._addDateBox(0, 0, 1, 0, _g.d.resource_report._from_date, 1, true, true);
                    this._addDateBox(0, 1, 1, 0, _g.d.resource_report._to_date, 1, true, true);
                    this._addCheckBox(1, 0, _g.d.resource_report._display_detail, false, true, false);
                    this._addCheckBox(1, 1, _g.d.resource_report._show_cancel_document, false, true, false);
                    break;
                case _reportEnum.ลูกหนี้_แต้มคงเหลือ:
                    {
                        this._maxColumn = 2;
                        this._addDateBox(0, 0, 1, 0, _g.d.resource_report._to_date, 1, true, true);
                        this._addCheckBox(1, 0, _g.d.resource_report._display_detail, false, true, true);
                        //this._addCheckBox(1, 0, _g.d.resource_report._displpay_item_balance_equal_zero, false, true, false);
                    }
                    break;
                case _reportEnum.ลูกหนี้_รายงานการรับชำระหนี้ประจำวัน:
                    this._maxColumn = 2;
                    this._addDateBox(0, 0, 1, 0, _g.d.resource_report._from_date, 1, true, false, false, _g.d.resource_report._from_receive_date);
                    this._addDateBox(0, 1, 1, 0, _g.d.resource_report._to_date, 1, true, false, false, _g.d.resource_report._to_receive_date);

                    this._addTextBox(1, 0, _g.d.resource_report._from_receive_no, 10);
                    this._addTextBox(1, 1, _g.d.resource_report._to_receive_no, 10);

                    this._addDateBox(2, 0, 1, 0, _g.d.resource_report._from_bill, 1, true, true);
                    this._addDateBox(2, 1, 1, 0, _g.d.resource_report._to_bill, 1, true, true);

                    this._addTextBox(3, 0, _g.d.resource_report._from_bill_no, 10);
                    this._addTextBox(3, 1, _g.d.resource_report._to_bill_no, 10);

                    if (_g.g._companyProfile._branchStatus == 1)
                    {
                        this._addTextBox(4, 0, 1, 0, _g.d.resource_report._branch, 1, 10, 1, true, false);
                    }

                    break;
                case _reportEnum.ลูกหนี้_รับวางบิล_ยกเลิก:
                case _reportEnum.ลูกหนี้_รับชำระหนี้_ยกเลิก:
                    {
                        this._maxColumn = 2;
                        this._addDateBox(0, 0, 1, 0, _g.d.resource_report._from_date, 1, true, true);
                        this._addDateBox(0, 1, 1, 0, _g.d.resource_report._to_date, 1, true, true);
                    }
                    break;

                #endregion

                #region เงินสด-ธนาคาร

                case _reportEnum.เงินสดธนาคาร_รายงานการจ่ายเงินประจำวัน:
                    this._maxColumn = 2;
                    this._addDateBox(0, 0, 1, 0, _g.d.resource_report._from_date, 1, true, true);
                    this._addDateBox(0, 1, 1, 0, _g.d.resource_report._to_date, 1, true, true);
                    break;
                case _reportEnum.เงินสดธนาคาร_รายงานการรับเงินประจำวัน:
                    this._maxColumn = 2;
                    this._addDateBox(0, 0, 1, 0, _g.d.resource_report._from_date, 1, true, true);
                    this._addDateBox(0, 1, 1, 0, _g.d.resource_report._to_date, 1, true, true);
                    break;
                case _reportEnum.เงินสดธนาคาร_รายได้อื่น_ยกเลิก:
                case _reportEnum.เงินสดธนาคาร_รายได้อื่น_ลดหนี้_ยกเลิก:
                case _reportEnum.เงินสดธนาคาร_รายได้อื่น_เพิ่มหนี้_ยกเลิก:
                    {
                        this._maxColumn = 2;
                        this._addDateBox(0, 0, 1, 0, _g.d.resource_report._from_date, 1, true, true);
                        this._addDateBox(0, 1, 1, 0, _g.d.resource_report._to_date, 1, true, true);
                    }
                    break;

                case _reportEnum.เงินสดธนาคาร_รายจ่ายอื่น_ยกเลิก:
                case _reportEnum.เงินสดธนาคาร_รายจ่ายอื่น_ลดหนี้_ยกเลิก:
                case _reportEnum.เงินสดธนาคาร_รายจ่ายอื่น_เพิ่มหนี้_ยกเลิก:
                    {
                        this._maxColumn = 2;
                        this._addDateBox(0, 0, 1, 0, _g.d.resource_report._from_date, 1, true, true);
                        this._addDateBox(0, 1, 1, 0, _g.d.resource_report._to_date, 1, true, true);
                    }
                    break;

                case _reportEnum.เงินสดธนาคาร_รายได้อื่น:
                    this._maxColumn = 2;
                    this._addDateBox(0, 0, 1, 0, _g.d.resource_report._from_date, 1, true, true);
                    this._addDateBox(0, 1, 1, 0, _g.d.resource_report._to_date, 1, true, true);

                    this._addCheckBox(1, 0, _g.d.resource_report._display_detail, false, true, true);
                    //this._addComboBox(2, 0, _g.d.resource_report._vat_display_type, true, new string[] { _g.d.resource_report._vat_display_type_1, _g.d.resource_report._vat_display_type_2, _g.d.resource_report._vat_display_type_3 }, false);

                    break;
                case _reportEnum.เงินสดธนาคาร_รายจ่ายอื่น:
                    this._maxColumn = 2;
                    this._addDateBox(0, 0, 1, 0, _g.d.resource_report._from_date, 1, true, true);
                    this._addDateBox(0, 1, 1, 0, _g.d.resource_report._to_date, 1, true, true);

                    this._addCheckBox(1, 0, _g.d.resource_report._display_detail, false, true, true);

                    break;
                case _reportEnum.เงินสดธนาคาร_รายจ่ายอื่น_เพิ่มหนี้:
                case _reportEnum.เงินสดธนาคาร_รายจ่ายอื่น_ลดหนี้:
                case _reportEnum.เงินสดธนาคาร_รายได้อื่น_เพิ่มหนี้:
                case _reportEnum.เงินสดธนาคาร_รายได้อื่น_ลดหนี้:
                    this._maxColumn = 2;
                    this._addDateBox(0, 0, 1, 0, _g.d.resource_report._from_date, 1, true, true);
                    this._addDateBox(0, 1, 1, 0, _g.d.resource_report._to_date, 1, true, true);

                    this._addCheckBox(1, 0, _g.d.resource_report._display_detail, false, true, true);

                    break;
                case _reportEnum.ธนาคาร_โอนเงิน_ยกเลิก:
                case _reportEnum.ธนาคาร_ฝากเงิน_ยกเลิก:
                case _reportEnum.ธนาคาร_ถอนเงิน_ยกเลิก:
                    {
                        this._maxColumn = 2;
                        this._addDateBox(0, 0, 1, 0, _g.d.resource_report._from_date, 1, true, true);
                        this._addDateBox(0, 1, 1, 0, _g.d.resource_report._to_date, 1, true, true);
                    }
                    break;
                case _reportEnum.ธนาคาร_โอนเงิน:
                    {
                        this._maxColumn = 2;
                        this._addDateBox(0, 0, 1, 0, _g.d.resource_report._from_date, 1, true, true);
                        this._addDateBox(0, 1, 1, 0, _g.d.resource_report._to_date, 1, true, true);
                        this._addCheckBox(1, 0, _g.d.resource_report._display_detail, false, true, true);

                    }
                    break;
                case _reportEnum.เคลื่อนไหวเงินสด:
                    this._maxColumn = 2;
                    this._addDateBox(0, 0, 1, 0, _g.d.resource_report._from_date, 1, true, true);
                    this._addDateBox(0, 1, 1, 0, _g.d.resource_report._to_date, 1, true, true);

                    break;

                #endregion

                #region เงินสดย่อย

                case _reportEnum.เงินสดย่อย_สถานะ:
                    this._maxColumn = 2;
                    this._addDateBox(0, 0, 1, 0, _g.d.resource_report._from_date, 1, true, true);
                    this._addDateBox(0, 1, 1, 0, _g.d.resource_report._to_date, 1, true, true);
                    break;
                case _reportEnum.เงินสดย่อย_เคลื่อนไหว:
                    this._maxColumn = 2;
                    this._addDateBox(0, 0, 1, 0, _g.d.resource_report._from_date, 1, true, true);
                    this._addDateBox(0, 1, 1, 0, _g.d.resource_report._to_date, 1, true, true);
                    break;
                case _reportEnum.เงินสดย่อย_เบิก:
                case _reportEnum.เงินสดย่อย_เบิก_ยกเลิก:
                case _reportEnum.เงินสดย่อย_รับคืน:
                case _reportEnum.เงินสดย่อย_รับคืน_ยกเลิก:
                    {
                        this._maxColumn = 2;
                        this._addDateBox(0, 0, 1, 0, _g.d.resource_report._from_date, 1, true, true);
                        this._addDateBox(0, 1, 1, 0, _g.d.resource_report._to_date, 1, true, true);

                        if (this._mode == _reportEnum.เงินสดย่อย_เบิก || this._mode == _reportEnum.เงินสดย่อย_รับคืน)
                        {
                            this._addCheckBox(1, 0, _g.d.resource_report._display_detail, false, true, true);
                        }
                    }
                    break;

                #endregion

                #region เช็ครับ

                case _reportEnum.เช็ครับ_ยกมา:
                case _reportEnum.เช็ครับ_ฝาก:
                case _reportEnum.เช็ครับ_ผ่าน:
                case _reportEnum.เช็ครับ_รับคืน:
                case _reportEnum.เช็ครับ_ขาดสิทธิ์:
                case _reportEnum.เช็ครับ_เปลี่ยนเช็ค:
                    this._maxColumn = 2;
                    this._addDateBox(0, 0, 1, 0, _g.d.resource_report._from_date, 1, true, true);
                    this._addDateBox(0, 1, 1, 0, _g.d.resource_report._to_date, 1, true, true);
                    this._addCheckBox(1, 0, _g.d.resource_report._display_detail, false, true, true);

                    break;
                case _reportEnum.เช็ค_รายงานเช็ครับ_ตามวันที่รับเช็ค:
                    this._maxColumn = 2;
                    this._addDateBox(0, 0, 1, 0, _g.d.resource_report._from_date, 1, true, true);
                    this._addDateBox(0, 1, 1, 0, _g.d.resource_report._to_date, 1, true, true);
                    break;
                case _reportEnum.เช็ครับ_ฝาก_ยกเลิก:
                case _reportEnum.เช็ครับ_ผ่าน_ยกเลิก:
                case _reportEnum.เช็ครับ_รับคืน_ยกเลิก:
                case _reportEnum.เช็ครับ_เปลี่ยน_ยกเลิก:
                case _reportEnum.เช็ครับ_ขาดสิทธิ์_ยกเลิก:
                    {
                        this._maxColumn = 2;
                        this._addDateBox(0, 0, 1, 0, _g.d.resource_report._from_date, 1, true, true);
                        this._addDateBox(0, 1, 1, 0, _g.d.resource_report._to_date, 1, true, true);
                    }
                    break;

                #endregion

                #region เช็คจ่าย

                case _reportEnum.เช็คจ่าย_ยกมา:
                case _reportEnum.เช็คจ่าย_ผ่าน:
                case _reportEnum.เช็คจ่าย_คืน:
                case _reportEnum.เช็คจ่าย_ขาดสิทธิ์:
                case _reportEnum.เช็คจ่าย_เปลี่ยนเช็ค:
                    this._maxColumn = 2;
                    this._addDateBox(0, 0, 1, 0, _g.d.resource_report._from_date, 1, true, true);
                    this._addDateBox(0, 1, 1, 0, _g.d.resource_report._to_date, 1, true, true);
                    this._addCheckBox(1, 0, _g.d.resource_report._display_detail, false, true, true);
                    break;

                case _reportEnum.เช็ค_รายงานเช็คจ่าย_ตามวันที่จ่ายเช็ค:
                    this._maxColumn = 2;
                    this._addDateBox(0, 0, 1, 0, _g.d.resource_report._from_date, 1, true, true);
                    this._addDateBox(0, 1, 1, 0, _g.d.resource_report._to_date, 1, true, true);
                    break;
                case _reportEnum.เช็คจ่าย_ผ่าน_ยกเลิก:
                case _reportEnum.เช็คจ่าย_คืน_ยกเลิก:
                case _reportEnum.เช็คจ่าย_เปลี่ยน_ยกเลิก:
                case _reportEnum.เช็คจ่าย_ขาดสิทธิ์_ยกเลิก:
                    {
                        this._maxColumn = 2;
                        this._addDateBox(0, 0, 1, 0, _g.d.resource_report._from_date, 1, true, true);
                        this._addDateBox(0, 1, 1, 0, _g.d.resource_report._to_date, 1, true, true);
                    }
                    break;

                #endregion

                #region บัตรเครดิต

                case _reportEnum.บัตรเครดิต_รายงานบัตรเครดิต_ตามวันที่รับ:
                    this._maxColumn = 2;
                    this._addDateBox(0, 0, 1, 0, _g.d.resource_report._from_date, 1, true, true);
                    this._addDateBox(0, 1, 1, 0, _g.d.resource_report._to_date, 1, true, true);
                    break;
                case _reportEnum.บัตรเครดิต_ขึ้นเงิน:
                case _reportEnum.บัตรเครดิต_ยกเลิก:
                    this._maxColumn = 2;
                    this._addDateBox(0, 0, 1, 0, _g.d.resource_report._from_date, 1, true, true);
                    this._addDateBox(0, 1, 1, 0, _g.d.resource_report._to_date, 1, true, true);
                    this._addCheckBox(1, 0, _g.d.resource_report._display_detail, false, true, true);
                    break;

                    #endregion


            }
            this.Invalidate();
            this.ResumeLayout();

            // _searchFundType._dataList._loadViewFormat(_searchFundType._name, MyLib._myGlobal._userSearchScreenGroup, false);


            //this._textBoxChanged += new MyLib.TextBoxChangedHandler(_conditionIcScreen__textBoxChanged);
            //this._textBoxSearch += new MyLib.TextBoxSearchHandler(_conditionIcScreen__textBoxSearch);

            ///case _reportEnum.ซื้อ_รายงานสถานะใบเสนอซื้อ:  By MOO  18/07/2553
            if (__search_status_approve)
            {
                this._search_status_approve = new MyLib._searchDataFull();
                this._search_status_approve.WindowState = FormWindowState.Maximized;
                this._search_status_approve._name = _g.g._search_screen_ซื้อ_เสนอซื้อ_อนุมัติ;
                this._search_status_approve._dataList._loadViewFormat(this._search_status_approve._name, MyLib._myGlobal._userSearchScreenGroup, false);
                this._search_status_approve._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
                this._search_status_approve._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_searchFundType__searchEnterKeyPress);
            }

            if (__searchIcShow)
            {
                //  ค้นหาสินค้า
                this._searchIc = new MyLib._searchDataFull();
                this._searchIc.WindowState = FormWindowState.Maximized;
                this._searchIc._name = _g.g._search_screen_ic_inventory;
                this._searchIc._dataList._loadViewFormat(this._searchIc._name, MyLib._myGlobal._userSearchScreenGroup, false);
                this._searchIc._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
                this._searchIc._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_searchFundType__searchEnterKeyPress);
            }
            if (__searchIc_Group_Show)
            {
                //ค้นหากลุ่มสินค้า
                this._searchIc_Group = new MyLib._searchDataFull();
                this._searchIc_Group._name = _g.g._search_master_ic_group;
                this._searchIc_Group._dataList._loadViewFormat(this._searchIc_Group._name, MyLib._myGlobal._userSearchScreenGroup, false);
                this._searchIc_Group._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
                this._searchIc_Group._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_searchFundType__searchEnterKeyPress);
                if (this._mode == _reportEnum.สินค้า_รายงานสรุปเคลื่อนไหวตามปริมาณ)
                {
                    this._searchIc_Group._dataList._multiSelect = true;
                    _searchIc_Group._dataList._selectSuccessButton.Click += (s1, e1) =>
                    {
                        this._searchTextBox.Text = _searchIc_Group._dataList._selectList();
                        _searchIc_Group.Close();
                    };
                }
            }
            if (__searchIc_Brand_show)
            {
                //  ยี่ห้อสินค้า
                this._searchIc_Brand = new MyLib._searchDataFull();
                this._searchIc_Brand._name = _g.g._search_master_ic_brand;
                this._searchIc_Brand._dataList._loadViewFormat(this._searchIc_Brand._name, MyLib._myGlobal._userSearchScreenGroup, false);
                this._searchIc_Brand._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
                this._searchIc_Brand._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_searchFundType__searchEnterKeyPress);
            }
            if (__searchIc_Type_show)
            {
                //  ประเภทสินค้า  
                this._searchIc_Type = new MyLib._searchDataFull();
                this._searchIc_Type._name = _g.g._search_master_ic_type;
                this._searchIc_Type._dataList._loadViewFormat(this._searchIc_Type._name, MyLib._myGlobal._userSearchScreenGroup, false);
                this._searchIc_Type._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
                this._searchIc_Type._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_searchFundType__searchEnterKeyPress);
            }
            if (__searchIc_Unit_show)
            {
                // หน่วยนับสินค้า 
                this._searchIc_Unit = new MyLib._searchDataFull();
                this._searchIc_Unit._name = _g.g._search_master_ic_unit;
                this._searchIc_Unit._dataList._loadViewFormat(this._searchIc_Unit._name, MyLib._myGlobal._userSearchScreenGroup, false);
                this._searchIc_Unit._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
                this._searchIc_Unit._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_searchFundType__searchEnterKeyPress);
            }
            if (__searchIc_Warehouse_show)
            {
                // คลังสินค้า 
                this._searchIc_Warehouse._name = _g.g._search_master_ic_warehouse;
                this._searchIc_Warehouse._dataList._loadViewFormat(this._searchIc_Warehouse._name, MyLib._myGlobal._userSearchScreenGroup, false);
                this._searchIc_Warehouse._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
                this._searchIc_Warehouse._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_searchFundType__searchEnterKeyPress);
            }
            if (__searchIc_Shelf_show)
            {
                // ที่เก็บสินค้า  
                this._searchIc_Shelf = new MyLib._searchDataFull();
                this._searchIc_Shelf._name = _g.g._search_master_ic_shelf;
                this._searchIc_Shelf._dataList._loadViewFormat(this._searchIc_Shelf._name, MyLib._myGlobal._userSearchScreenGroup, false);
                this._searchIc_Shelf._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
                this._searchIc_Shelf._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_searchFundType__searchEnterKeyPress);
            }
            if (__search_ap_show)
            {
                // เจ้าหนี้  _search_ap 
                this._search_ap = new MyLib._searchDataFull();
                this._search_ap._name = _g.g._search_screen_ap;
                this._search_ap._dataList._loadViewFormat(this._search_ap._name, MyLib._myGlobal._userSearchScreenGroup, false);
                this._search_ap._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
                this._search_ap._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_searchFundType__searchEnterKeyPress);
            }
            if (__search_department_show)
            {
                // แผนก
                this._search_department = new MyLib._searchDataFull();
                this._search_department._name = _g.g._search_screen_erp_department_list;
                this._search_department._dataList._loadViewFormat(this._search_department._name, MyLib._myGlobal._userSearchScreenGroup, false);
                this._search_department._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
                this._search_department._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_searchFundType__searchEnterKeyPress);
            }
            if (__search_docno_show)
            {
                // แผนก
                this._search_docno = new MyLib._searchDataFull();
                this._search_docno._name = _g.g._search_screen_ic_trans;
                this._search_docno._dataList._loadViewFormat(this._search_docno._name, MyLib._myGlobal._userSearchScreenGroup, false);
                this._search_docno._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
                this._search_docno._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_searchFundType__searchEnterKeyPress);
            }
            if (__search_category_show)
            {
                // หมวดสินค้า
                this._search_category = new MyLib._searchDataFull();
                this._search_category._name = _g.g._search_master_ic_category;
                this._search_category._dataList._loadViewFormat(this._search_category._name, MyLib._myGlobal._userSearchScreenGroup, false);
                this._search_category._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
                this._search_category._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_searchFundType__searchEnterKeyPress);
            }
            if (__search_pattern_show)
            {
                // รูปแบบ
                this._search_pattern = new MyLib._searchDataFull();
                this._search_pattern._name = _g.g._search_master_ic_pattern;
                this._search_pattern._dataList._loadViewFormat(this._search_pattern._name, MyLib._myGlobal._userSearchScreenGroup, false);
                this._search_pattern._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
                this._search_pattern._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_searchFundType__searchEnterKeyPress);
            }
            if (__search_erp_user_show)
            {
                // ผู้เบิก
                this._search_erp_user = new MyLib._searchDataFull();
                this._search_erp_user._name = _g.g._search_screen_erp_user;
                this._search_erp_user._dataList._loadViewFormat(this._search_erp_user._name, MyLib._myGlobal._userSearchScreenGroup, false);
                this._search_erp_user._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
                this._search_erp_user._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_searchFundType__searchEnterKeyPress);
            }
            if (__search_allocation_list_show)
            {
                // ผู้เบิก
                this._search_allocation_list = new MyLib._searchDataFull();
                this._search_allocation_list._name = _g.g._search_screen_erp_allocate;
                this._search_allocation_list._dataList._loadViewFormat(this._search_allocation_list._name, MyLib._myGlobal._userSearchScreenGroup, false);
                this._search_allocation_list._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
                this._search_allocation_list._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_searchFundType__searchEnterKeyPress);
            }
            if (__search_serial_number_show)
            {
                // serial number 
                this._search_serial_number = new MyLib._searchDataFull();
                this._search_serial_number._name = _g.g._search_screen_ic_serial;
                this._search_serial_number._dataList._loadViewFormat(this._search_serial_number._name, MyLib._myGlobal._userSearchScreenGroup, false);
                this._search_serial_number._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
                this._search_serial_number._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_searchFundType__searchEnterKeyPress);
            }
            if (__search_ar_type_show == true)
            {
                this._search_ar_type = new MyLib._searchDataFull();
                this._search_ar_type._name = _g.g._search_master_ar_type;
                this._search_ar_type._dataList._loadViewFormat(this._search_ar_type._name, MyLib._myGlobal._userSearchScreenGroup, false);
                this._search_ar_type._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
                this._search_ar_type._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_searchFundType__searchEnterKeyPress);

            }
            DateTime __getToday = DateTime.Now;
            this._setDataDate(_g.d.resource_report._from_date, new DateTime(__getToday.Year, __getToday.Month, 1));
            this._setDataDate(_g.d.resource_report._to_date, new DateTime(__getToday.Year, __getToday.Month, __getToday.Day));
        }

        void _searchFundType__searchEnterKeyPress(MyLib._myGrid sender, int row)
        {
            _searchByParent(sender, row);
        }

        private void _searchByParent(MyLib._myGrid sender, int row)
        {
            MyLib._myDataList __getParent1 = (MyLib._myDataList)((MyLib._myGrid)sender).Parent;
            MyLib._searchDataFull __getParent2 = (MyLib._searchDataFull)__getParent1.Parent;
            _searchAll(__getParent2._name, row);
            SendKeys.Send("{TAB}");
        }

        private void _searchAll(string name, int row)  //  หลังจากเลือกข้อมูลที่ค้นหาแล้ว
        {
            MyLib._searchDataFull __search = new MyLib._searchDataFull();
            string __result = "";
            if (name.Equals(_g.g._search_screen_ic_inventory))
            {
                __search = _searchIc;
                __result = (string)__search._dataList._gridData._cellGet(row, 0);
            }
            else if (name.Equals(_g.g._search_master_ic_group))
            {
                __search = _searchIc_Group;
                __result = (string)__search._dataList._gridData._cellGet(row, 0);
            }
            else if (name.Equals(_g.g._search_master_ic_brand))
            {
                __search = _searchIc_Brand;
                __result = (string)__search._dataList._gridData._cellGet(row, 0);
            }
            else if (name.Equals(_g.g._search_master_ic_type))
            {
                __search = _searchIc_Type;
                __result = (string)__search._dataList._gridData._cellGet(row, 0);
            }
            else if (name.Equals(_g.g._search_master_ic_unit))
            {
                __search = _searchIc_Unit;
                __result = (string)__search._dataList._gridData._cellGet(row, 0);
            }
            else if (name.Equals(_g.g._search_master_ic_warehouse))
            {
                __search = _searchIc_Warehouse;
                __result = (string)__search._dataList._gridData._cellGet(row, 0);
            }
            else if (name.Equals(_g.g._search_master_ic_shelf))
            {
                __search = _searchIc_Shelf;
                __result = (string)__search._dataList._gridData._cellGet(row, 0);
            }
            else if (name.Equals(_g.g._search_screen_ap))
            {
                __search = _search_ap;
                __result = (string)__search._dataList._gridData._cellGet(row, 0);
            }
            else if (name.Equals(_g.g._search_screen_erp_department_list))
            {
                __search = _search_department;
                __result = (string)__search._dataList._gridData._cellGet(row, 0);
            }
            else if (name.Equals(_g.g._search_screen_ic_trans))
            {
                __search = _search_docno;
                __result = (string)__search._dataList._gridData._cellGet(row, 1);
            }
            else if (name.Equals(_g.g._search_master_ic_category))
            {
                __search = _search_category;
                __result = (string)__search._dataList._gridData._cellGet(row, 0);
            }
            else if (name.Equals(_g.g._search_master_ic_pattern))
            {
                __search = _search_pattern;
                __result = (string)__search._dataList._gridData._cellGet(row, 0);
            }
            else if (name.Equals(_g.g._search_screen_erp_user))
            {
                __search = _search_erp_user;
                __result = (string)__search._dataList._gridData._cellGet(row, 0);
            }
            else if (name.Equals(_g.g._search_screen_erp_allocate))
            {
                __search = _search_allocation_list;
                __result = (string)__search._dataList._gridData._cellGet(row, 0);
            }
            else if (name.Equals(_g.g._search_screen_ic_serial))
            {
                __search = _search_serial_number;
                __result = (string)__search._dataList._gridData._cellGet(row, 0);
            }
            else if (name.Equals(_g.g._search_master_ar_type))
            {
                __search = _search_ar_type;
                __result = (string)__search._dataList._gridData._cellGet(row, 0);
            }

            if (__result.Length > 0)
            {
                __search.Visible = false;
                this._setDataStr(_searchName, __result, "", false);
                _search(true);
            }
        }

        void _gridData__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            MyLib._myDataList __getParent1 = (MyLib._myDataList)((MyLib._myGrid)sender).Parent;
            MyLib._searchDataFull __getParent2 = (MyLib._searchDataFull)__getParent1.Parent;
            _searchAll(__getParent2._name, e._row);
        }

        private void _search(bool warning)
        {
            try
            {
                StringBuilder __myquery = new StringBuilder();
                __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");

                switch (this._mode)
                {
                    case _reportEnum.สินค้า_รายงานรับสินค้าสำเร็จรูป:
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ap_supplier._name_1 + " from " + _g.d.ap_supplier._table + " where " + _g.d.ap_supplier._code + "=\'" + this._getDataStr(_g.d.resource_report._from_ap) + "\'"));
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ap_supplier._name_1 + " from " + _g.d.ap_supplier._table + " where " + _g.d.ap_supplier._code + "=\'" + this._getDataStr(_g.d.resource_report._to_ap) + "\'"));
                        break;
                    case _reportEnum.กำไรขั้นต้น_กลุ่มลูกค้า:
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ar_type._name_1 + " from " + _g.d.ar_type._table + " where " + _g.d.ar_type._code + "=\'" + this._getDataStr(_g.d.resource_report._ar_type) + "\'"));

                        break;
                    default:
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ic_inventory._name_1 + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._code + "=\'" + this._getDataStr(_g.d.ic_resource._ic_name) + "\'"));
                        __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ic_inventory._name_1 + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._code + "=\'" + this._getDataStr(_g.d.ic_resource._ic_normal) + "\'"));
                        break;

                }

                __myquery.Append("</node>");
                ArrayList _getData = _myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());

                switch (this._mode)
                {
                    case _reportEnum.สินค้า_รายงานรับสินค้าสำเร็จรูป:
                        //__myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ap_supplier._name_1 + " from " + _g.d.ap_supplier._table + " where " + _g.d.ap_supplier._code + "=\'" + this._getDataStr(_g.d.resource_report._to_ap) + "\'"));
                        _searchAndWarning(_g.d.resource_report._from_ap, (DataSet)_getData[0], warning);
                        _searchAndWarning(_g.d.resource_report._to_ap, (DataSet)_getData[1], warning);
                        break;
                    case _reportEnum.กำไรขั้นต้น_กลุ่มลูกค้า:
                        _searchAndWarning(_g.d.resource_report._ar_type, (DataSet)_getData[0], warning);
                        break;
                    default:
                        _searchAndWarning(_g.d.ic_resource._ic_name, (DataSet)_getData[0], warning);
                        _searchAndWarning(_g.d.ic_resource._ic_normal, (DataSet)_getData[1], warning);
                        break;
                }

            }
            catch
            {
            }
        }

        private bool _searchAndWarning(string fieldName, DataSet dataResult, Boolean warning)
        {
            bool __result = false;
            if (dataResult.Tables[0].Rows.Count > 0)
            {
                string getData = dataResult.Tables[0].Rows[0][0].ToString();
                string getDataStr = this._getDataStr(fieldName);
                if (fieldName.Equals(_g.d.ic_resource._ic_name) || fieldName.Equals(_g.d.ic_resource._ic_normal))
                {
                    this._setDataStr(fieldName, getDataStr);
                    //this._setDataStr(fieldName, getDataStr, getData, true);
                    //  this._setDataStr(_g.d.ic_inventory._name_1, getData);
                }
                else
                {
                    this._setDataStr(fieldName, getDataStr, getData, true);
                }
            }
            else
            {
                this._setDataStr(fieldName, "");
            }
            if (_searchTextBox != null)
            {
                if (_searchName.Equals(fieldName) && this._getDataStr(fieldName) != "")
                {
                    if (dataResult.Tables[0].Rows.Count == 0 && warning)
                    {
                        ((MyLib._myTextBox)_searchTextBox.Parent).textBox.Text = "";
                        ((MyLib._myTextBox)_searchTextBox.Parent)._textFirst = "";
                        ((MyLib._myTextBox)_searchTextBox.Parent)._textSecond = "";
                        ((MyLib._myTextBox)_searchTextBox.Parent)._textLast = "";
                        _searchTextBox.Focus();
                        //
                        MessageBox.Show(MyLib._myGlobal._resource("ไม่พบ") + " : " + this._getLabelName(fieldName), MyLib._myGlobal._resource("เตือน"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        __result = true;
                    }
                }
            }
            return __result;
        }

    }
    public partial class _condition_ic_grid : MyLib._myGrid
    {
        MyLib._searchDataFull _search_data_full = new MyLib._searchDataFull();
        public string _searchName = "";

        public _condition_ic_grid()
        {
            this._clickSearchButton += new MyLib.SearchEventHandler(_condition_ic_grid__clickSearchButton);
        }

        void _condition_ic_grid__clickSearchButton(object sender, MyLib.GridCellEventArgs e)
        {
            string _search_text_new = this._searchName;
            if (!this._search_data_full._name.Equals(_search_text_new.ToLower()))
            {
                this._search_data_full._name = _search_text_new;
                this._search_data_full._dataList._loadViewFormat(this._search_data_full._name, MyLib._myGlobal._userSearchScreenGroup, false);
                this._search_data_full._dataList._gridData._mouseClick -= new MyLib.MouseClickHandler(_gridData__mouseClick);
                this._search_data_full._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
                this._search_data_full.WindowState = FormWindowState.Maximized;
            }
            this._search_data_full.ShowDialog();
            //MyLib._myGlobal._startSearchBox(this._inputTextBox, "ค้นหารหัสสินค้า", this._search_data_full, true);
        }

        public void _setFromToColumn(string __from_column_name, string __to_column_name)
        {
            this._clear();
            this._addColumn(_g.d.resource_report._table + "." + __from_column_name, 1, 1, 50, true, false, false, true);
            this._addColumn(_g.d.resource_report._table + "." + __to_column_name, 1, 1, 50, true, false, false, true);
            this._width_by_persent = true;
            this.AllowDrop = true;
            this._isEdit = true;

            this.ColumnBackgroundAuto = false;
            this.ColumnBackgroundEnd = Color.FromArgb(198, 220, 249);
            this.RowOddBackground = Color.AliceBlue;
            this.AutoSize = true;
        }

        void _gridData__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            MyLib._myDataList getParent1 = (MyLib._myDataList)((MyLib._myGrid)sender).Parent;
            MyLib._searchDataFull getParent2 = (MyLib._searchDataFull)getParent1.Parent;
            string name = getParent2._name;
            if (name.CompareTo(this._searchName) == 0)
            {
                this._search_data_full.Close();
                this._cellUpdate(this._selectRow, this._selectColumn, e._text, false);
            }
        }

        public DataTable _getCondition()
        {
            if (this._rowCount(0) == 0) return null;
            DataTable __dataTable = new DataTable("FromTo");
            __dataTable.Columns.Add("from");
            __dataTable.Columns.Add("to");
            for (int __row = 0; __row < this._rowCount(0); __row++)
            {
                DataRow __dataRow = __dataTable.NewRow();
                __dataRow[0] = this._cellGet(__row, 0).ToString();
                __dataRow[1] = this._cellGet(__row, 1).ToString();
                __dataTable.Rows.Add(__dataRow);
            }
            return __dataTable;
        }
    }
}
