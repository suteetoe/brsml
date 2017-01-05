using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLERPGL
{
    public partial class _checkData : UserControl
    {
        public _checkData()
        {
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this.toolStrip1.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }
            //
            this._resultGrid._isEdit = false;
            this._resultGrid._addColumn(_g.d.gl_resource._check_name, 1, 100, 40);
            this._resultGrid._addColumn(_g.d.gl_resource._check_desc, 1, 100, 60);
            this._all.MyCheckBox.CheckedChanged += (s1, e1) =>
            {
                this._endDateTextBox.Enabled = (this._all.MyCheckBox.Checked == false) ? true : false;
            };
            this._all.MyCheckBox.Checked = true;
        }


        private void _buttonClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void _buttonStart_Click(object sender, EventArgs e)
        {
            string __endDate = "";
            if (this._all.MyCheckBox.Checked == false)
            {
                __endDate = this._endDateTextBox.Text.ToString();
            }
            this._resultGrid._clear();
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            string __errorMessage = "พบข้อผิดพลาด";
            {
                // เอกสารข้อมูลรายวันที่ไม่กำหนดประเภทเอกสาร
                int __row = this._resultGrid._addRow();
                this._resultGrid._cellUpdate(__row, _g.d.gl_resource._check_name, "เอกสารข้อมูลรายวันที่ไม่กำหนดประเภทเอกสาร", false);
                this._resultGrid.Invalidate();
                string __query = "select " + MyLib._myGlobal._fieldAndComma(_g.d.ic_trans._trans_flag, _g.d.ic_trans._doc_date, _g.d.ic_trans._doc_no, _g.d.ic_trans._total_amount) + " from " + _g.d.ic_trans._table + " where (" + _g.d.ic_trans._doc_format_code + " is null or " + _g.d.ic_trans._doc_format_code + "=\'\')";
                if (this._all.MyCheckBox.Checked == false)
                {
                    __query = __query + " and " + _g.d.ic_trans._doc_date + "<=\'" + __endDate + "\'";
                }
                __query = __query + " order by " + _g.d.ic_trans._doc_type + "," + _g.d.ic_trans._doc_date + "," + _g.d.ic_trans._doc_no;
                DataTable __getData = __myFrameWork._query(MyLib._myGlobal._databaseName, __query).Tables[0];
                for (int __loop = 0; __loop < __getData.Rows.Count; __loop++)
                {
                    __row = this._resultGrid._addRow();
                    this._resultGrid._cellUpdate(__row, _g.d.gl_resource._check_name, __errorMessage, false);
                    int __transType = (int)MyLib._myGlobal._decimalPhase(__getData.Rows[__loop].ItemArray[0].ToString());
                    this._resultGrid._cellUpdate(__row, _g.d.gl_resource._check_desc, string.Format("ประเภท:{0}{1} วันที่: {2} เลขที่เอกสาร: {3}", _g.g._transFlagGlobal._transName(__transType), __transType, __getData.Rows[__loop].ItemArray[1], __getData.Rows[__loop].ItemArray[2]), false);
                }
                this._resultGrid.Invalidate();
            }
            {
                // ตรวจสอบ Journal Type ใน sub ถ้าไม่ตรงก็ update ให้ตรงกับหัว
                StringBuilder __myQuery = new StringBuilder();
                __myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.gl_journal_detail._table + " set " + _g.d.gl_journal_detail._journal_type + "=(select " + _g.d.gl_journal._journal_type + " from " + _g.d.gl_journal._table + " where " + _g.d.gl_journal._table + "." + _g.d.gl_journal._doc_date + "=" + _g.d.gl_journal_detail._table + "." + _g.d.gl_journal_detail._doc_date + " and " + _g.d.gl_journal._table + "." + _g.d.gl_journal._doc_no + "=" + _g.d.gl_journal_detail._table + "." + _g.d.gl_journal_detail._doc_no + " and " + _g.d.gl_journal._table + "." + _g.d.gl_journal._book_code + "=" + _g.d.gl_journal_detail._table + "." + _g.d.gl_journal_detail._book_code + ") where " + _g.d.gl_journal_detail._journal_type + "<>(select " + _g.d.gl_journal._journal_type + " from " + _g.d.gl_journal._table + " where " + _g.d.gl_journal._table + "." + _g.d.gl_journal._doc_date + "=" + _g.d.gl_journal_detail._table + "." + _g.d.gl_journal_detail._doc_date + " and " + _g.d.gl_journal._table + "." + _g.d.gl_journal._doc_no + "=" + _g.d.gl_journal_detail._table + "." + _g.d.gl_journal_detail._doc_no + " and " + _g.d.gl_journal._table + "." + _g.d.gl_journal._book_code + "=" + _g.d.gl_journal_detail._table + "." + _g.d.gl_journal_detail._book_code + ")"));
                __myQuery.Append("</node>");
                string __result = __myFrameWork._queryList(MyLib._myGlobal._databaseName, __myQuery.ToString());
                // 
                // ตรวจสอบรายวันย่อยที่ไม่มีผังบัญชี
                int __row = this._resultGrid._addRow();
                this._resultGrid._cellUpdate(__row, _g.d.gl_resource._check_name, "ตรวจสอบรายวันย่อยที่ไม่มีผังบัญชี", false);
                this._resultGrid.Invalidate();
                string __query = "select " + _g.d.gl_journal_detail._doc_date + "," + _g.d.gl_journal_detail._doc_no + "," + _g.d.gl_journal_detail._book_code + "," + _g.d.gl_journal_detail._account_code + " from " + _g.d.gl_journal_detail._table + " where " + _g.d.gl_journal_detail._account_code + " not in (select distinct " + _g.d.gl_chart_of_account._code + " from " + _g.d.gl_chart_of_account._table + ")";
                if (this._all.MyCheckBox.Checked == false)
                {
                    __query = __query + " and " + _g.d.gl_journal_detail._doc_date + "<=\'" + __endDate + "\'";
                }
                __query = __query + " order by " + _g.d.gl_journal_detail._doc_date + "," + _g.d.gl_journal_detail._doc_no;
                DataTable __getData = __myFrameWork._query(MyLib._myGlobal._databaseName, __query).Tables[0];
                for (int __loop = 0; __loop < __getData.Rows.Count; __loop++)
                {
                    __row = this._resultGrid._addRow();
                    this._resultGrid._cellUpdate(__row, _g.d.gl_resource._check_name, __errorMessage, false);
                    this._resultGrid._cellUpdate(__row, _g.d.gl_resource._check_desc, string.Format("วันที่: {0} เลขที่เอกสาร: {1} สมุดรายวัน: {2} รหัสผังบัญชีที่ไม่พบ: {3}", __getData.Rows[__loop].ItemArray[0], __getData.Rows[__loop].ItemArray[1], __getData.Rows[__loop].ItemArray[2], __getData.Rows[__loop].ItemArray[3]), false);
                }
                this._resultGrid.Invalidate();
                // ตรวจสอบรายวันย่อยที่มีผังบัญชีเป็นบัญชีหลัก
                __row = this._resultGrid._addRow();
                this._resultGrid._cellUpdate(__row, _g.d.gl_resource._check_name, "ตรวจสอบรายวันย่อยที่ใช้ผังบัญชีหลัก", false);
                this._resultGrid.Invalidate();
                __query = "select " + _g.d.gl_journal_detail._doc_date + "," + _g.d.gl_journal_detail._doc_no + "," + _g.d.gl_journal_detail._book_code + "," + _g.d.gl_journal_detail._account_code + " from " + _g.d.gl_journal_detail._table + " where " + _g.d.gl_journal_detail._account_code + " in (select " + _g.d.gl_chart_of_account._code + " from " + _g.d.gl_chart_of_account._table + " where " + _g.d.gl_chart_of_account._status + "=1)";
                if (this._all.MyCheckBox.Checked == false)
                {
                    __query = __query + " and " + _g.d.gl_journal_detail._doc_date + "<=\'" + __endDate + "\'";
                }
                __query = __query + " order by " + _g.d.gl_journal_detail._doc_date + "," + _g.d.gl_journal_detail._doc_no;
                __getData = __myFrameWork._query(MyLib._myGlobal._databaseName, __query).Tables[0];
                for (int __loop = 0; __loop < __getData.Rows.Count; __loop++)
                {
                    __row = this._resultGrid._addRow();
                    this._resultGrid._cellUpdate(__row, _g.d.gl_resource._check_name, __errorMessage, false);
                    this._resultGrid._cellUpdate(__row, _g.d.gl_resource._check_desc, string.Format("วันที่: {0} เลขที่เอกสาร: {1} สมุดรายวัน: {2} รหัสผังบัญชี: {3}", __getData.Rows[__loop].ItemArray[0], __getData.Rows[__loop].ItemArray[1], __getData.Rows[__loop].ItemArray[2], __getData.Rows[__loop].ItemArray[3]), false);
                }
                this._resultGrid.Invalidate();
                // ตรวจสอบรายวันที่ยอดเดบิตกับเครดิตไม่เท่ากัน
                __row = this._resultGrid._addRow();
                this._resultGrid._cellUpdate(__row, _g.d.gl_resource._check_name, "ตรวจสอบรายวันที่ยอดเดบิตกับเครดิตไม่เท่ากัน", false);
                this._resultGrid.Invalidate();
                __query = "select " + _g.d.gl_journal_detail._doc_date + "," + _g.d.gl_journal_detail._doc_no + "," + _g.d.gl_journal_detail._book_code + " from " + _g.d.gl_journal_detail._table;
                if (this._all.MyCheckBox.Checked == false)
                {
                    __query = __query + " where " + _g.d.gl_journal_detail._doc_date + "<=\'" + __endDate + "\'";
                }
                __query = __query + " group by " + _g.d.gl_journal_detail._doc_date + "," + _g.d.gl_journal_detail._doc_no + "," + _g.d.gl_journal_detail._book_code + " having sum(" + _g.d.gl_journal_detail._debit + ")<>sum(" + _g.d.gl_journal_detail._credit + ")";
                __query = __query + " order by " + _g.d.gl_journal_detail._doc_date + "," + _g.d.gl_journal_detail._doc_no;
                __getData = __myFrameWork._query(MyLib._myGlobal._databaseName, __query).Tables[0];
                for (int __loop = 0; __loop < __getData.Rows.Count; __loop++)
                {
                    __row = this._resultGrid._addRow();
                    this._resultGrid._cellUpdate(__row, _g.d.gl_resource._check_name, __errorMessage, false);
                    this._resultGrid._cellUpdate(__row, _g.d.gl_resource._check_desc, string.Format("วันที่: {0} เลขที่เอกสาร: {1} สมุดรายวัน: {2}", __getData.Rows[__loop].ItemArray[0], __getData.Rows[__loop].ItemArray[1], __getData.Rows[__loop].ItemArray[2]), false);
                }
                this._resultGrid.Invalidate();
                // ตรวจสอบรายวันไม่มีหัวเอกสาร
                __row = this._resultGrid._addRow();
                this._resultGrid._cellUpdate(__row, _g.d.gl_resource._check_name, "ตรวจสอบรายวันที่ไม่พบหัวเอกสาร", false);
                this._resultGrid.Invalidate();
                __query = "select " + _g.d.gl_journal_detail._doc_date + "," + _g.d.gl_journal_detail._doc_no + "," + _g.d.gl_journal_detail._book_code + " from " + _g.d.gl_journal_detail._table + " where ";
                if (this._all.MyCheckBox.Checked == false)
                {
                    __query = __query + _g.d.gl_journal_detail._doc_date + "<=\'" + __endDate + "\' and ";
                }
                __query = __query + "(" + _g.d.gl_journal_detail._doc_no + " not in (select distinct " + _g.d.gl_journal._doc_no + " from " + _g.d.gl_journal._table + " where " + _g.d.gl_journal._table + "." + _g.d.gl_journal._doc_date + "=" + _g.d.gl_journal_detail._table + "." + _g.d.gl_journal_detail._doc_date + " and " + _g.d.gl_journal._table + "." + _g.d.gl_journal._doc_no + "=" + _g.d.gl_journal_detail._table + "." + _g.d.gl_journal_detail._doc_no + " and " + _g.d.gl_journal._table + "." + _g.d.gl_journal._book_code + "=" + _g.d.gl_journal_detail._table + "." + _g.d.gl_journal_detail._book_code + ") or " + _g.d.gl_journal_detail._book_code + " not in (select distinct " + _g.d.gl_journal_book._code + " from " + _g.d.gl_journal_book._table + ")) group by " + _g.d.gl_journal_detail._doc_date + "," + _g.d.gl_journal_detail._doc_no + "," + _g.d.gl_journal_detail._book_code + " order by " + _g.d.gl_journal_detail._doc_date + "," + _g.d.gl_journal_detail._doc_no;
                __getData = __myFrameWork._query(MyLib._myGlobal._databaseName, __query).Tables[0];
                for (int __loop = 0; __loop < __getData.Rows.Count; __loop++)
                {
                    __row = this._resultGrid._addRow();
                    this._resultGrid._cellUpdate(__row, _g.d.gl_resource._check_name, __errorMessage, false);
                    this._resultGrid._cellUpdate(__row, _g.d.gl_resource._check_desc, string.Format("วันที่: {0} เลขที่เอกสาร: {1} สมุดรายวัน: {2}", __getData.Rows[__loop].ItemArray[0], __getData.Rows[__loop].ItemArray[1], __getData.Rows[__loop].ItemArray[2]), false);
                }
                this._resultGrid.Invalidate();
                // ตรวจสอบรายวันไม่มีสมุดรายวัน
                __row = this._resultGrid._addRow();
                this._resultGrid._cellUpdate(__row, _g.d.gl_resource._check_name, "ตรวจสอบรายวันที่ไม่พบสมุดรายวัน", false);
                this._resultGrid.Invalidate();
                __query = "select " + _g.d.gl_journal._doc_date + "," + _g.d.gl_journal._doc_no + "," + _g.d.gl_journal._book_code + " from " + _g.d.gl_journal._table + " where " + _g.d.gl_journal._book_code + " not in (select distinct " + _g.d.gl_journal_book._code + " from " + _g.d.gl_journal_book._table + ")";
                if (this._all.MyCheckBox.Checked == false)
                {
                    __query = __query + " and " + _g.d.gl_journal_detail._doc_date + "<=\'" + __endDate + "\'";
                }
                __query = __query + " order by " + _g.d.gl_journal_detail._doc_date + "," + _g.d.gl_journal_detail._doc_no;
                __getData = __myFrameWork._query(MyLib._myGlobal._databaseName, __query).Tables[0];
                for (int __loop = 0; __loop < __getData.Rows.Count; __loop++)
                {
                    __row = this._resultGrid._addRow();
                    this._resultGrid._cellUpdate(__row, _g.d.gl_resource._check_name, __errorMessage, false);
                    this._resultGrid._cellUpdate(__row, _g.d.gl_resource._check_desc, string.Format("วันที่: {0} เลขที่เอกสาร: {1} สมุดรายวัน: {2}", __getData.Rows[__loop].ItemArray[0], __getData.Rows[__loop].ItemArray[1], __getData.Rows[__loop].ItemArray[2]), false);
                }
                this._resultGrid.Invalidate();
                // ตรวจสอบเอกสารที่ยังไม่โอนไปยังแยกประเภท
                __row = this._resultGrid._addRow();
                this._resultGrid._cellUpdate(__row, _g.d.gl_resource._check_name, "ตรวจสอบเอกสารที่ยังไม่โอนไปยังแยกประเภท (ic_trans)", false);
                this._resultGrid.Invalidate();
                _g.g._transControlTypeEnum[] __transFlagList1 = { 
                        _g.g._transControlTypeEnum.เงินสดธนาคาร_บัตรเครดิต_ผ่าน,
                        _g.g._transControlTypeEnum.เงินสดธนาคาร_บัตรเครดิต_ยกเลิก,
                        _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ผ่าน,
                        _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ยกเลิก,
                        _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ผ่าน,
                        _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_คืน,
                        _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_เข้าใหม่,
                        _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ยกเลิก,
                        _g.g._transControlTypeEnum.เงินสดธนาคาร_ฝากเงิน,
                        _g.g._transControlTypeEnum.เงินสดธนาคาร_ถอนเงิน,
                        _g.g._transControlTypeEnum.เงินสดย่อย_เบิกเงินสดย่อย,
                        _g.g._transControlTypeEnum.เงินสดย่อย_รับคืนเงินสดย่อย,
                        _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น,
                        _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_ยกเลิก,
                        _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_ลดหนี้,
                        _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_ลดหนี้_ยกเลิก,
                        _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_เพิ่มหนี้,
                        _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_เพิ่มหนี้_ยกเลิก,
                        _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น,
                        _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_ยกเลิก,
                        _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_ลดหนี้,
                        _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_ลดหนี้_ยกเลิก,
                        _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_เพิ่มหนี้,
                        _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_เพิ่มหนี้_ยกเลิก,
                        _g.g._transControlTypeEnum.เจ้าหนี้_ตั้งหนี้อื่น,
                        _g.g._transControlTypeEnum.เจ้าหนี้_ตั้งหนี้อื่น_ยกเลิก,
                        _g.g._transControlTypeEnum.เจ้าหนี้_เพิ่มหนี้อื่น,
                        _g.g._transControlTypeEnum.เจ้าหนี้_เพิ่มหนี้อื่น_ยกเลิก,
                        _g.g._transControlTypeEnum.เจ้าหนี้_ลดหนี้อื่น,
                        _g.g._transControlTypeEnum.เจ้าหนี้_ลดหนี้อื่น_ยกเลิก,
                        _g.g._transControlTypeEnum.เจ้าหนี้_จ่ายชำระหนี้,
                        _g.g._transControlTypeEnum.เจ้าหนี้_จ่ายชำระหนี้_ยกเลิก,
                        _g.g._transControlTypeEnum.เจ้าหนี้_ตัดหนี้สูญ,
                        _g.g._transControlTypeEnum.เจ้าหนี้_ตัดหนี้สูญ_ยกเลิก,
                        _g.g._transControlTypeEnum.ลูกหนี้_ตั้งหนี้อื่น,
                        _g.g._transControlTypeEnum.ลูกหนี้_ตั้งหนี้อื่น_ยกเลิก,
                        _g.g._transControlTypeEnum.ลูกหนี้_เพิ่มหนี้อื่น,
                        _g.g._transControlTypeEnum.ลูกหนี้_เพิ่มหนี้อื่น_ยกเลิก,
                        _g.g._transControlTypeEnum.ลูกหนี้_ลดหนี้อื่น,
                        _g.g._transControlTypeEnum.ลูกหนี้_ลดหนี้อื่น_ยกเลิก,
                        _g.g._transControlTypeEnum.ลูกหนี้_รับชำระหนี้,
                        _g.g._transControlTypeEnum.ลูกหนี้_รับชำระหนี้_ยกเลิก,
                        _g.g._transControlTypeEnum.ลูกหนี้_ตัดหนี้สูญ,
                        _g.g._transControlTypeEnum.ลูกหนี้_ตัดหนี้สูญ_ยกเลิก,
                        _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า,
                        _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า_ยกเลิก,
                        _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า_รับคืน,
                        _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า_รับคืน_ยกเลิก,
                        _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินมัดจำ,
                        _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินมัดจำ_รับคืน,
                        _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินมัดจำ_ยกเลิก,
                        _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินมัดจำ_รับคืน_ยกเลิก,
                        _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ,
                        _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและบริการ_ยกเลิก,
                        _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด,
                        _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด_ยกเลิก,
                        _g.g._transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด,
                        _g.g._transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด_ยกเลิก,
                        _g.g._transControlTypeEnum.ซื้อ_พาเชียล_รับสินค้า,
                        _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ส่งคืนสินค้าหรือราคาผิด,
                        _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ตั้งหนี้,
                        _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ลดหนี้,
                        _g.g._transControlTypeEnum.ซื้อ_พาเชียล_เพิ่มหนี้,
                        _g.g._transControlTypeEnum.ขาย_รับเงินล่วงหน้า,
                        _g.g._transControlTypeEnum.ขาย_รับเงินล่วงหน้า_ยกเลิก,
                        _g.g._transControlTypeEnum.ขาย_เงินล่วงหน้า_คืน,
                        _g.g._transControlTypeEnum.ขาย_เงินล่วงหน้า_คืน_ยกเลิก,
                        _g.g._transControlTypeEnum.ขาย_รับเงินมัดจำ,
                        _g.g._transControlTypeEnum.ขาย_รับเงินมัดจำ_ยกเลิก,
                        _g.g._transControlTypeEnum.ขาย_เงินมัดจำ_คืน,
                        _g.g._transControlTypeEnum.ขาย_เงินมัดจำ_คืน_ยกเลิก,
                        _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ,
                        _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ_ยกเลิก,
                        _g.g._transControlTypeEnum.ขาย_เพิ่มหนี้,
                        _g.g._transControlTypeEnum.ขาย_เพิ่มหนี้_ยกเลิก,
                        _g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้,
                        _g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้_ยกเลิก,
                        _g.g._transControlTypeEnum.สินค้า_เบิกสินค้าวัตถุดิบ,
                        _g.g._transControlTypeEnum.สินค้า_เบิกสินค้าวัตถุดิบ_ยกเลิก,
                        _g.g._transControlTypeEnum.สินค้า_รับคืนสินค้าจากการเบิก,
                        _g.g._transControlTypeEnum.สินค้า_รับคืนสินค้าจากการเบิก_ยกเลิก,
                        _g.g._transControlTypeEnum.สินค้า_รับสินค้าสำเร็จรูป,
                        _g.g._transControlTypeEnum.สินค้า_รับสินค้าสำเร็จรูป_ยกเลิก,
                        _g.g._transControlTypeEnum.สินค้า_ปรับปรุงสต็อก,
                        _g.g._transControlTypeEnum.สินค้า_ปรับปรุงสต็อก_ยกเลิก                                                               
                                                                };
                StringBuilder __transFlagStr1 = new StringBuilder();
                for (int __type = 0; __type < __transFlagList1.Length; __type++)
                {
                    if (__transFlagStr1.Length > 0)
                    {
                        __transFlagStr1.Append(",");
                    }
                    __transFlagStr1.Append(_g.g._transFlagGlobal._transFlag(__transFlagList1[__type]).ToString());
                }
                __query = "select " + _g.d.ic_trans._trans_flag + "," + _g.d.ic_trans._doc_date + "," + _g.d.ic_trans._doc_no + "," + _g.d.gl_journal._doc_format_code + 
                    " from " + _g.d.ic_trans._table + 
                    " where " + _g.d.ic_trans._trans_flag + " in (" + __transFlagStr1.ToString() + ") and coalesce(is_cancel, 0) = 0 " +
                    //" and " + _g.d.gl_journal._doc_no + " not in (select distinct " + _g.d.gl_journal._doc_no + " from " + _g.d.gl_journal._table + ")" ; // toe add cancel doc
                    " and not exists (select distinct doc_no from gl_journal where gl_journal.doc_no = ic_trans.doc_no) " + 
                    " and not exists (select distinct doc_no from gl_journal where (gl_journal.doc_no) = ic_trans.doc_no || '*' ) ";
                if (this._all.MyCheckBox.Checked == false)
                {
                    __query = __query + " and " + _g.d.ic_trans._doc_date + "<=\'" + __endDate + "\'";
                }

                if (this._have_book_only.MyCheckBox.Checked == true)
                {
                    // 
                    __query += " and (select gl_book from erp_doc_format  where erp_doc_format.code = ic_trans.doc_format_code ) != '' ";
                }
                __query = __query + " order by " + _g.d.ic_trans._doc_date + "," + _g.d.ic_trans._trans_flag + "," + _g.d.ic_trans._doc_no;
                __getData = __myFrameWork._query(MyLib._myGlobal._databaseName, __query).Tables[0];
                for (int __loop = 0; __loop < __getData.Rows.Count; __loop++)
                {
                    __row = this._resultGrid._addRow();
                    this._resultGrid._cellUpdate(__row, _g.d.gl_resource._check_name, __errorMessage, false);
                    this._resultGrid._cellUpdate(__row, _g.d.gl_resource._check_desc, string.Format("เลขที่เอกสาร: {2} ประเภท: {0} วันที่: {1} รหัสเอกสาร: {3}", _g.g._transFlagGlobal._transName((int)MyLib._myGlobal._decimalPhase(__getData.Rows[__loop].ItemArray[0].ToString())), __getData.Rows[__loop].ItemArray[1], __getData.Rows[__loop].ItemArray[2], __getData.Rows[__loop].ItemArray[3]), false);
                }
                this._resultGrid.Invalidate();
                /*__row = this._resultGrid._addRow();
                this._resultGrid._cellUpdate(__row, _g.d.gl_resource._check_name, "ตรวจสอบเอกสารที่ยังไม่โอนไปยังแยกประเภท (cb_trans)", false);
                this._resultGrid.Invalidate();
                _g.g._transControlTypeEnum[] __transFlagList2 = { 
                                                                   _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ ,
                                                                   _g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้ ,
                                                                   _g.g._transControlTypeEnum.ขาย_รับเงินล่วงหน้า ,
                                                                   _g.g._transControlTypeEnum.ขาย_รับเงินมัดจำ ,
                                                                   _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ ,
                                                                   _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด ,
                                                                   _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ ,
                                                                   _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ตั้งหนี้
                                                               };
                StringBuilder __transFlagStr2 = new StringBuilder();
                for (int __type = 0; __type < __transFlagList1.Length; __type++)
                {
                    if (__transFlagStr2.Length > 0)
                    {
                        __transFlagStr2.Append(",");
                    }
                    __transFlagStr2.Append(_g.g._transFlagGlobal._transFlag(__transFlagList1[__type]).ToString());
                }
                __query = "select " + _g.d.ic_trans._trans_flag + "," + _g.d.ic_trans._doc_date + "," + _g.d.ic_trans._doc_no + "," + _g.d.gl_journal._doc_format_code + " from " + _g.d.ic_trans._table +
                        " where " + _g.d.ic_trans._trans_flag + " in (" + __transFlagStr2.ToString() + ") and " + _g.d.gl_journal._doc_no + " not in (select distinct " + _g.d.gl_journal._doc_no + " from " + _g.d.gl_journal._table + ")" +
                        " order by " + _g.d.ic_trans._doc_date + "," + _g.d.ic_trans._trans_flag + "," + _g.d.ic_trans._doc_no;
                __getData = __myFrameWork._query(MyLib._myGlobal._databaseName, __query).Tables[0];
                for (int __loop = 0; __loop < __getData.Rows.Count; __loop++)
                {
                    __row = this._resultGrid._addRow();
                    this._resultGrid._cellUpdate(__row, _g.d.gl_resource._check_name, __errorMessage, false);
                    this._resultGrid._cellUpdate(__row, _g.d.gl_resource._check_desc, string.Format("ประเภท: {0} วันที่: {1} เลขที่เอกสาร: {2} รหัสเอกสาร: {3}", _g.g._transFlagGlobal._transName((int)MyLib._myGlobal._decimalPhase(__getData.Rows[__loop].ItemArray[0].ToString())), __getData.Rows[__loop].ItemArray[1], __getData.Rows[__loop].ItemArray[2], __getData.Rows[__loop].ItemArray[3]), false);
                }
                this._resultGrid.Invalidate();*/
            }
        }
    }
}
