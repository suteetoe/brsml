using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace _g._changeCode
{
    public partial class _changeApArCodeUserControl : UserControl
    {
        _g.g._transTypeEnum _type = g._transTypeEnum.ว่าง;
        public MyLib._searchDataFull _searchItem = new MyLib._searchDataFull();
        MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();

        string _flag_ap_update = "";
        string _flag_ar_update = "";

        public _changeApArCodeUserControl(_g.g._transTypeEnum type)
        {
            InitializeComponent();
            this._type = type;

            if (MyLib._myGlobal._isDesignMode == false)
            {
                this.toolStrip1.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }
            //

            if (this._type != g._transTypeEnum.ว่าง)
            {
                switch (this._type)
                {
                    case g._transTypeEnum.ลูกหนี้:
                        this._grid._table_name = _g.d.ar_customer._table;
                        this._grid._addColumn(_g.d.ar_customer._code_old, 1, 20, 20, true, false, true, true);
                        this._grid._addColumn(_g.d.ar_customer._code, 1, 20, 20, true, false);
                        this._grid._addColumn(_g.d.ar_customer._name_1, 1, 20, 40, false, false);
                        this._grid._addColumn(_g.d.ar_customer._remark, 1, 20, 20, false, false);
                        //
                        this._searchItem._showMode = 0;
                        this._searchItem.WindowState = FormWindowState.Maximized;
                        this._searchItem._name = _g.g._search_screen_ar;
                        this._searchItem._dataList._tableName = _g.d.ar_customer._table;
                        this._searchItem._searchEnterKeyPress += _searchItem__searchEnterKeyPress;
                        this._searchItem._dataList._gridData._mouseClick += _gridData__mouseClick;
                        this._searchItem._dataList._loadViewFormat(this._searchItem._name, MyLib._myGlobal._userSearchScreenGroup, false);
                        //
                        this._processButton.Click += _processButton_Click;


                        _flag_ar_update = MyLib._myGlobal._fieldAndComma(
                            // เสนอราคา
                           _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_เสนอราคา).ToString(),
                           _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_เสนอราคา_ยกเลิก).ToString(),
                           _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_เสนอราคา_อนุมัติ).ToString(),

                           // สั่งซื้อ
                           _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า).ToString(),
                           _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า_ยกเลิก).ToString(),
                           _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า_อนุมัติ).ToString(),

                           // สั่งขาย
                           _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_สั่งขาย).ToString(),
                           _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_สั่งขาย_ยกเลิก).ToString(),
                           _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_สั่งขาย_อนุมัติ).ToString(),

                           // เงินล่วงหน้า
                           _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_รับเงินล่วงหน้า).ToString(),
                           _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_รับเงินล่วงหน้า_ยกเลิก).ToString(),
                           _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_เงินล่วงหน้า_คืน).ToString(),
                           _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_เงินล่วงหน้า_คืน_ยกเลิก).ToString(),

                           // เงินมัดจำ
                           _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_รับเงินมัดจำ).ToString(),
                           _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_รับเงินมัดจำ_ยกเลิก).ToString(),
                           _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_เงินมัดจำ_คืน).ToString(),
                           _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_เงินมัดจำ_คืน_ยกเลิก).ToString(),

                           // ขาย
                           _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ).ToString(),
                           _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ_ยกเลิก).ToString(),
                           _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_เพิ่มหนี้).ToString(),
                           _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_เพิ่มหนี้_ยกเลิก).ToString(),
                           _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้).ToString(),
                           _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้_ยกเลิก).ToString(),


                           // หนี้ยกมา
                           _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ลูกหนี้_ตั้งหนี้ยกมา).ToString(),
                           _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ลูกหนี้_เพิ่มหนี้ยกมา).ToString(),
                           _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ลูกหนี้_ลดหนี้ยกมา).ToString(),

                           // หนี้อื่น ๆ 
                           _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ลูกหนี้_ตั้งหนี้อื่น).ToString(),
                           _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ลูกหนี้_ตั้งหนี้อื่น_ยกเลิก).ToString(),
                           _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ลูกหนี้_เพิ่มหนี้อื่น).ToString(),
                           _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ลูกหนี้_เพิ่มหนี้อื่น_ยกเลิก).ToString(),
                           _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ลูกหนี้_ลดหนี้อื่น).ToString(),
                           _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ลูกหนี้_ลดหนี้อื่น_ยกเลิก).ToString(),

                           // วางบิล
                           _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ลูกหนี้_ใบวางบิล).ToString(),
                           _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ลูกหนี้_ใบวางบิล_ยกเลิก).ToString(),

                          // จ่ายชำระ
                           _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ลูกหนี้_รับชำระหนี้).ToString(),
                           _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ลูกหนี้_รับชำระหนี้_ยกเลิก).ToString(),

                           // ค่าใช้จ่าย
                           _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น).ToString(),
                           _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_ยกเลิก).ToString(),
                           _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_เพิ่มหนี้).ToString(),
                           _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_เพิ่มหนี้_ยกเลิก).ToString(),
                           _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_ลดหนี้).ToString(),
                           _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_ลดหนี้_ยกเลิก).ToString(),

                           _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ลูกหนี้_แต้มยกมา).ToString(),
                           _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ลูกหนี้_ลดแต้ม).ToString()

                           );

                        break;
                    case g._transTypeEnum.เจ้าหนี้:
                        this._grid._table_name = _g.d.ap_supplier._table;
                        this._grid._addColumn(_g.d.ap_supplier._code_old, 1, 20, 20, true, false, true, true);
                        this._grid._addColumn(_g.d.ap_supplier._code, 1, 20, 20, true, false);
                        this._grid._addColumn(_g.d.ap_supplier._name_1, 1, 20, 40, false, false);
                        this._grid._addColumn(_g.d.ap_supplier._remark, 1, 20, 20, false, false);

                        this._searchItem._showMode = 0;
                        this._searchItem.WindowState = FormWindowState.Maximized;
                        this._searchItem._name = _g.g._search_screen_ap;
                        this._searchItem._dataList._tableName = _g.d.ap_supplier._table;
                        this._searchItem._searchEnterKeyPress += _searchItem__searchEnterKeyPress;
                        this._searchItem._dataList._gridData._mouseClick += _gridData__mouseClick;
                        this._searchItem._dataList._loadViewFormat(this._searchItem._name, MyLib._myGlobal._userSearchScreenGroup, false);
                        //
                        this._processButton.Click += _processButton_Click;

                        _flag_ap_update = MyLib._myGlobal._fieldAndComma(
                            // เสนอซื้อ 
                            _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_เสนอซื้อ).ToString(),
                            _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_เสนอซื้อ_ยกเลิก).ToString(),
                            _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_เสนอซื้อ_อนุมัติ).ToString(),

                            // สั่งซื้อ
                            _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ).ToString(),
                            _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ_ยกเลิก).ToString(),
                            _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ_อนุมัติ).ToString(),

                            // เงินล่วงหน้า
                            _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า).ToString(),
                            _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า_ยกเลิก).ToString(),
                            _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า_รับคืน).ToString(),
                            _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า_รับคืน_ยกเลิก).ToString(),

                            // เงินมัดจำ
                            _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_จ่ายเงินมัดจำ).ToString(),
                            _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_จ่ายเงินมัดจำ_ยกเลิก).ToString(),
                            _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_จ่ายเงินมัดจำ_รับคืน).ToString(),
                            _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_จ่ายเงินมัดจำ_รับคืน_ยกเลิก).ToString(),

                            // ซื้อ
                            _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ).ToString(),
                            _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและบริการ_ยกเลิก).ToString(),
                            _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด).ToString(),
                            _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด_ยกเลิก).ToString(),
                            _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด).ToString(),
                            _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด_ยกเลิก).ToString(),

                            // ซื้อพาเชียล
                            _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_พาเชียล_รับสินค้า).ToString(),
                            _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_พาเชียล_ส่งคืนสินค้าหรือราคาผิด).ToString(),
                            _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_พาเชียล_ตั้งหนี้).ToString(),
                            _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_พาเชียล_เพิ่มหนี้).ToString(),
                            _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_พาเชียล_ลดหนี้).ToString(),

                            // หนี้ยกมา
                            _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เจ้าหนี้_ตั้งหนี้ยกมา).ToString(),
                            _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เจ้าหนี้_เพิ่มหนี้ยกมา).ToString(),
                            _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เจ้าหนี้_ลดหนี้ยกมา).ToString(),

                            // หนี้อื่น ๆ 
                            _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เจ้าหนี้_ตั้งหนี้อื่น).ToString(),
                            _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เจ้าหนี้_ตั้งหนี้อื่น_ยกเลิก).ToString(),
                            _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เจ้าหนี้_เพิ่มหนี้อื่น).ToString(),
                            _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เจ้าหนี้_เพิ่มหนี้อื่น_ยกเลิก).ToString(),
                            _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เจ้าหนี้_ลดหนี้อื่น).ToString(),
                            _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เจ้าหนี้_ลดหนี้อื่น_ยกเลิก).ToString(),

                            // วางบิล
                            _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เจ้าหนี้_ใบรับวางบิล).ToString(),
                            _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เจ้าหนี้_ใบรับวางบิล_ยกเลิก).ToString(),

                           // จ่ายชำระ
                            _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เจ้าหนี้_จ่ายชำระหนี้).ToString(),
                            _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เจ้าหนี้_จ่ายชำระหนี้_ยกเลิก).ToString(),

                            // ค่าใช้จ่าย
                            _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น).ToString(),
                            _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_ยกเลิก).ToString(),
                            _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_เพิ่มหนี้).ToString(),
                            _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_เพิ่มหนี้_ยกเลิก).ToString(),
                            _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_ลดหนี้).ToString(),
                            _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_ลดหนี้_ยกเลิก).ToString()
                            );
                        break;
                }

                this._grid._clickSearchButton += _grid__clickSearchButton;
                this._grid._alterCellUpdate += _grid__alterCellUpdate;
            }
        }


        string _command(string tableName, string fieldName, int row)
        {
            return _command(tableName, fieldName, row, "");
        }

        string _command(string tableName, string fieldName, int row, string extraWhere)
        {
            string __itemCodeOld = this._grid._cellGet(row, _g.d.ic_inventory._code_old).ToString().ToUpper();
            string __itemCodeNew = this._grid._cellGet(row, _g.d.ic_inventory._code).ToString().Trim().ToUpper();

            StringBuilder __result = new StringBuilder();
            if (__itemCodeOld.Equals(__itemCodeNew) == false)
            {
                //string __deleteFormat = "delete from {0} where {1}=\'" + __itemCodeNew + "\'";
                string __updateFormat = "update {0} set {1}=\'" + __itemCodeNew + "\' where {1}=\'" + __itemCodeOld + "\'" + ((extraWhere.Length > 0) ? " and " + extraWhere : "");
                //__result.Append(string.Format(MyLib._myUtil._convertTextToXmlForQuery(__deleteFormat), tableName, fieldName));
                __result.Append(string.Format(MyLib._myUtil._convertTextToXmlForQuery(__updateFormat), tableName, fieldName));
            }
            return __result.ToString();
        }

        void _processButton_Click(object sender, EventArgs e)
        {
            string __fieldCode = (this._type == g._transTypeEnum.ลูกหนี้) ? _g.d.ar_customer._code : _g.d.ap_supplier._code;
            string __fieldName = (this._type == g._transTypeEnum.ลูกหนี้) ? _g.d.ar_customer._name_1 : _g.d.ap_supplier._name_1;

            try
            {
                StringBuilder __query = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");
                for (int __row = 0; __row < this._grid._rowData.Count; __row++)
                {
                    string __itemName = this._grid._cellGet(__row, __fieldName).ToString().Trim().ToUpper();
                    string __itemCode = this._grid._cellGet(__row, __fieldCode).ToString().Trim().ToUpper();
                    string __remark = "Skip";
                    if (__itemName.Trim().Length > 0 && __itemCode.Length > 0)
                    {
                        switch (this._type)
                        {
                            case g._transTypeEnum.ลูกหนี้:

                                // master 

                                __query.Append(this._command(_g.d.ar_customer._table, _g.d.ar_customer._code, __row));
                                __query.Append(this._command(_g.d.ar_customer_detail._table, _g.d.ar_customer_detail._ar_code, __row));
                                __query.Append(this._command(_g.d.ar_dealer._table, _g.d.ar_dealer._ar_code, __row));
                                __query.Append(this._command(_g.d.ar_contactor._table, _g.d.ar_contactor._ar_code, __row));
                                __query.Append(this._command(_g.d.ar_item_by_customer._table, _g.d.ar_item_by_customer._ar_code, __row));
                                __query.Append(this._command(_g.d.ic_inventory_price._table, _g.d.ic_inventory_price._cust_code, __row));

                                // trans 
                                __query.Append(this._command(_g.d.ic_trans._table, _g.d.ic_trans._cust_code, __row, _g.d.ic_trans._trans_flag + " in (" + _flag_ar_update + ")"));
                                __query.Append(this._command(_g.d.ic_trans_detail._table, _g.d.ic_trans_detail._cust_code, __row, _g.d.ic_trans_detail._trans_flag + " in (" + _flag_ar_update + ")"));

                                __query.Append(this._command(_g.d.ap_ar_trans._table, _g.d.ap_ar_trans._cust_code, __row, _g.d.ap_ar_trans._trans_flag + " in (" + _flag_ar_update + ")"));
                                __query.Append(this._command(_g.d.ap_ar_trans_detail._table, _g.d.ap_ar_trans_detail._cust_code, __row, _g.d.ap_ar_trans_detail._trans_flag + " in (" + _flag_ar_update + ")"));

                                __query.Append(this._command(_g.d.cb_trans._table, _g.d.cb_trans._ap_ar_code, __row, _g.d.cb_trans._trans_flag + " in (" + _flag_ar_update + ")"));
                                __query.Append(this._command(_g.d.cb_trans_detail._table, _g.d.cb_trans_detail._ap_ar_code, __row, _g.d.cb_trans_detail._trans_flag + " in (" + _flag_ar_update + ")"));

                                __query.Append(this._command(_g.d.ic_trans_serial_number._table, _g.d.ic_trans_serial_number._cust_code, __row, _g.d.ic_trans_serial_number._trans_flag + " in (" + _flag_ar_update + ")"));
                                __query.Append(this._command(_g.d.ic_trans_shipment._table, _g.d.ic_trans_shipment._cust_code, __row, _g.d.ic_trans_shipment._trans_flag + " in (" + _flag_ar_update + ")"));

                                __query.Append(this._command(_g.d.ap_ar_transport_label._table, _g.d.ap_ar_transport_label._cust_code, __row, _g.d.ap_ar_transport_label._ar_ap_type + "=1"));


                                break;
                            case g._transTypeEnum.เจ้าหนี้:

                                // master 

                                __query.Append(this._command(_g.d.ap_supplier._table, _g.d.ap_supplier._code, __row));
                                __query.Append(this._command(_g.d.ap_supplier_detail._table, _g.d.ap_supplier_detail._ap_code, __row));
                                __query.Append(this._command(_g.d.ap_contactor._table, _g.d.ap_contactor._ap_code, __row));
                                __query.Append(this._command(_g.d.ap_item_by_supplier._table, _g.d.ap_item_by_supplier._ap_code, __row));

                                // trans 
                                __query.Append(this._command(_g.d.ic_trans._table, _g.d.ic_trans._cust_code, __row, _g.d.ic_trans._trans_flag + " in (" + _flag_ap_update + ")"));
                                __query.Append(this._command(_g.d.ic_trans_detail._table, _g.d.ic_trans_detail._cust_code, __row, _g.d.ic_trans_detail._trans_flag + " in (" + _flag_ap_update + ")"));

                                __query.Append(this._command(_g.d.ap_ar_trans._table, _g.d.ap_ar_trans._cust_code, __row, _g.d.ap_ar_trans._trans_flag + " in (" + _flag_ap_update + ")"));
                                __query.Append(this._command(_g.d.ap_ar_trans_detail._table, _g.d.ap_ar_trans_detail._cust_code, __row, _g.d.ap_ar_trans_detail._trans_flag + " in (" + _flag_ap_update + ")"));

                                __query.Append(this._command(_g.d.cb_trans._table, _g.d.cb_trans._ap_ar_code, __row, _g.d.cb_trans._trans_flag + " in (" + _flag_ap_update + ")"));
                                __query.Append(this._command(_g.d.cb_trans_detail._table, _g.d.cb_trans_detail._ap_ar_code, __row, _g.d.cb_trans_detail._trans_flag + " in (" + _flag_ap_update + ")"));

                                __query.Append(this._command(_g.d.ic_trans_serial_number._table, _g.d.ic_trans_serial_number._cust_code, __row, _g.d.ic_trans_serial_number._trans_flag + " in (" + _flag_ap_update + ")"));
                                __query.Append(this._command(_g.d.ic_trans_shipment._table, _g.d.ic_trans_shipment._cust_code, __row, _g.d.ic_trans_shipment._trans_flag + " in (" + _flag_ap_update + ")"));

                                __query.Append(this._command(_g.d.ap_ar_transport_label._table, _g.d.ap_ar_transport_label._cust_code, __row, _g.d.ap_ar_transport_label._ar_ap_type + "=0"));

                                break;
                        }

                        /*
                        __query.Append(this._command(_g.d.ic_inventory._table, _g.d.ic_inventory._code, __row));
                        __query.Append(this._command(_g.d.ic_extra_detail._table, _g.d.ic_extra_detail._ic_code, __row));
                        __query.Append(this._command(_g.d.ic_color_use._table, _g.d.ic_color_use._ic_code, __row));
                        __query.Append(this._command(_g.d.ic_inventory_append._table, _g.d.ic_inventory_append._ic_append_code, __row));
                        __query.Append(this._command(_g.d.ic_inventory_append_detail._table, _g.d.ic_inventory_append_detail._ic_code, __row));
                        __query.Append(this._command(_g.d.ic_inventory_barcode._table, _g.d.ic_inventory_barcode._ic_code, __row));
                        __query.Append(this._command(_g.d.ic_inventory_detail._table, _g.d.ic_inventory_detail._ic_code, __row));
                        __query.Append(this._command(_g.d.images._table, _g.d.images._image_id, __row));
                        __query.Append(this._command(_g.d.ic_inventory_price._table, _g.d.ic_inventory_price._ic_code, __row));
                        __query.Append(this._command(_g.d.ic_inventory_purchase_price._table, _g.d.ic_inventory_purchase_price._ic_code, __row));
                        __query.Append(this._command(_g.d.ic_inventory_replace._table, _g.d.ic_inventory_replace._ic_replace_code, __row));
                        __query.Append(this._command(_g.d.ic_inventory_replace_detail._table, _g.d.ic_inventory_replace_detail._ic_code, __row));
                        __query.Append(this._command(_g.d.ic_inventory_set_detail._table, _g.d.ic_inventory_set_detail._ic_code, __row));
                        __query.Append(this._command(_g.d.ic_name_billing._table, _g.d.ic_name_billing._ic_code, __row));
                        __query.Append(this._command(_g.d.ic_name_merket._table, _g.d.ic_name_merket._ic_code, __row));
                        __query.Append(this._command(_g.d.ic_name_pos._table, _g.d.ic_name_pos._ic_code, __row));
                        __query.Append(this._command(_g.d.ic_name_short._table, _g.d.ic_name_short._ic_code, __row));
                        __query.Append(this._command(_g.d.ic_opposite_unit._table, _g.d.ic_opposite_unit._ic_code, __row));
                        __query.Append(this._command(_g.d.ic_pattern_use._table, _g.d.ic_pattern_use._ic_code, __row));
                        __query.Append(this._command(_g.d.ic_promotion_detail._table, _g.d.ic_promotion_detail._ic_code, __row));
                        __query.Append(this._command(_g.d.ic_purchase_permium_condition._table, _g.d.ic_purchase_permium_condition._ic_code, __row));
                        __query.Append(this._command(_g.d.ic_purchase_permium_list._table, _g.d.ic_purchase_permium_list._ic_code, __row));
                        __query.Append(this._command(_g.d.ic_purchase_price_detail._table, _g.d.ic_purchase_price_detail._ic_code, __row));
                        __query.Append(this._command(_g.d.ic_relation_detail._table, _g.d.ic_relation_detail._ic_code_1, __row));
                        __query.Append(this._command(_g.d.ic_serial._table, _g.d.ic_serial._ic_code, __row));
                        __query.Append(this._command(_g.d.ic_size_use._table, _g.d.ic_size_use._ic_code, __row));
                        __query.Append(this._command(_g.d.ic_stk_build._table, _g.d.ic_stk_build._ic_code, __row));
                        __query.Append(this._command(_g.d.ic_stk_build_detail._table, _g.d.ic_stk_build_detail._ic_code, __row));
                        __query.Append(this._command(_g.d.ic_trans_detail._table, _g.d.ic_trans_detail._item_code, __row));
                        __query.Append(this._command(_g.d.ic_trans_detail_lot._table, _g.d.ic_trans_detail_lot._item_code, __row));
                        __query.Append(this._command(_g.d.ic_trans_serial_number._table, _g.d.ic_trans_serial_number._ic_code, __row));
                        __query.Append(this._command(_g.d.ic_unit_use._table, _g.d.ic_unit_use._ic_code, __row));
                        __query.Append(this._command(_g.d.ic_wh_shelf._table, _g.d.ic_wh_shelf._ic_code, __row));
                        __query.Append(this._command(_g.d.ic_inventory_level._table, _g.d.ic_inventory_level._ic_code, __row));
                        __query.Append(this._command(_g.d.ic_inventory_price_formula._table, _g.d.ic_inventory_price_formula._ic_code, __row));*/

                        __remark = "Process";
                    }
                    this._grid._cellUpdate(__row, _g.d.ic_inventory._remark, __remark, false);
                }
                __query.Append("</node>");
                string __result = this._myFrameWork._queryList(MyLib._myGlobal._databaseName, __query.ToString());
                if (__result.Length == 0)
                {
                    MessageBox.Show("Success");
                    this.Dispose();
                }
                else
                {
                    MessageBox.Show("Fail : " + __result);
                }
            }
            catch (Exception __ex)
            {
                MessageBox.Show(__ex.Message.ToString());
            }
        }

        void _gridData__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            string __codeColumn = (this._type == g._transTypeEnum.ลูกหนี้) ? _g.d.ar_customer._table + "." + _g.d.ar_customer._code : _g.d.ap_supplier._table + "." + _g.d.ap_supplier._code;
            string __oldCodeColumn = (this._type == g._transTypeEnum.ลูกหนี้) ? _g.d.ar_customer._code_old : _g.d.ap_supplier._code_old;


            MyLib._myDataList __getParent1 = (MyLib._myDataList)((MyLib._myGrid)sender).Parent;
            MyLib._searchDataFull __getParent2 = (MyLib._searchDataFull)__getParent1.Parent;
            string __name = __getParent2._name;
            string __itemCode = (string)this._searchItem._dataList._gridData._cellGet(e._row, __codeColumn);
            this._grid._cellUpdate(this._grid._selectRow, __oldCodeColumn, __itemCode, true);
            this._searchItem.Close();
            SendKeys.Send("{TAB}");
        }

        void _searchItem__searchEnterKeyPress(MyLib._myGrid sender, int row)
        {
            string __codeColumn = (this._type == g._transTypeEnum.ลูกหนี้) ? _g.d.ar_customer._table + "." + _g.d.ar_customer._code : _g.d.ap_supplier._table + "." + _g.d.ap_supplier._code;
            string __oldCodeColumn = (this._type == g._transTypeEnum.ลูกหนี้) ? _g.d.ar_customer._code_old : _g.d.ap_supplier._code_old;

            string __itemCode = (string)this._searchItem._dataList._gridData._cellGet(row, __codeColumn);
            this._grid._cellUpdate(this._grid._selectRow, __oldCodeColumn, __itemCode, true);
            this._searchItem.Close();
            SendKeys.Send("{TAB}");
        }

        void _grid__alterCellUpdate(object sender, int row, int column)
        {
            string __fieldCode = (this._type == g._transTypeEnum.ลูกหนี้) ? _g.d.ar_customer._code : _g.d.ap_supplier._code;
            string __fieldName = (this._type == g._transTypeEnum.ลูกหนี้) ? _g.d.ar_customer._name_1 : _g.d.ap_supplier._name_1;
            string __fieldOldCode = (this._type == g._transTypeEnum.ลูกหนี้) ? _g.d.ar_customer._code_old : _g.d.ap_supplier._code_old;

            int __getItemCodeOldColumn = this._grid._findColumnByName(__fieldOldCode);
            int __getItemCodeColumn = this._grid._findColumnByName(__fieldCode);
            if (column == __getItemCodeOldColumn)
            {
                string __itemCode = this._grid._cellGet(row, __fieldOldCode).ToString();
                string __query = "select " + __fieldName + " from " + this._grid._table_name + " where " + __fieldCode + "=\'" + MyLib._myUtil._convertTextToXml(__itemCode) + "\'";
                string __itemName = "";
                DataTable __getData = this._myFrameWork._queryShort(__query).Tables[0];
                if (__getData.Rows.Count > 0)
                {
                    __itemName = __getData.Rows[0][__fieldName].ToString();
                }
                this._grid._cellUpdate(row, __fieldName, __itemName, false);
            }
            if (column == __getItemCodeColumn)
            {
                string __itemCode = this._grid._cellGet(row, __fieldCode).ToString();
                string __query = "select " + __fieldName + " from " + this._grid._table_name + " where " + __fieldCode + "=\'" + MyLib._myUtil._convertTextToXml(__itemCode) + "\'";
                DataTable __getData = this._myFrameWork._queryShort(__query).Tables[0];
                if (__getData.Rows.Count > 0)
                {
                    MessageBox.Show(MyLib._myGlobal._resource("รหัสใหม่ซ้ำ"));
                    this._grid._cellUpdate(row, __fieldCode, "", false);
                }
            }

        }

        void _grid__clickSearchButton(object sender, MyLib.GridCellEventArgs e)
        {
            this._searchItem.ShowDialog();
        }

        private void _clipBoardButton_Click(object sender, EventArgs e)
        {
            string __fieldCode = (this._type == g._transTypeEnum.ลูกหนี้) ? _g.d.ar_customer._code : _g.d.ap_supplier._code;
            string __fieldOldCode = (this._type == g._transTypeEnum.ลูกหนี้) ? _g.d.ar_customer._code_old : _g.d.ap_supplier._code_old;

            try
            {
                string __str = Clipboard.GetText();
                string[] __rowStr = __str.Replace("\r", "").Split('\n');
                for (int __row = 0; __row < __rowStr.Length; __row++)
                {
                    string[] __field = __rowStr[__row].ToString().Split('\t');
                    if (__field.Length == 2)
                    {
                        string __itemOldCode = __field[0].ToString();
                        string __itemNewCode = __field[1].ToString();
                        int __addr = this._grid._addRow();
                        this._grid._cellUpdate(__addr, __fieldOldCode, __itemOldCode, true);
                        this._grid._cellUpdate(__addr, __fieldCode, __itemNewCode, true);
                    }
                }
            }
            catch (Exception __ex)
            {
                MessageBox.Show(__ex.Message.ToString());
            }
        }

        private void _clearButton_Click(object sender, EventArgs e)
        {
            this._grid._clear();
        }

        private void _closeButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
