using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Drawing;

namespace SMLERPReportTool
{
    public class _reportPosAnalyze : UserControl
    {
        private SMLReport._generate _report;
        private String _screenName = "";
        private DataTable _dataTable;
        private DataTable _dataTableDetail;
        private DataTable _dataAddtional;

        private SMLERPReportTool._conditionScreen _form_condition;
        string _levelNameRoot = "root";
        string _levelNameDetail = "detail";
        SMLReport._generateLevelClass _level2;
        Boolean _displayDetail = false;
        Boolean _showBarcode = false;
        _reportEnum _mode;
        _reportVatNumberType _vatNumberType = _reportVatNumberType.ปรกติ;
        _g.g._transControlTypeEnum _transFlag;
        string _extraWhere = "";

        public _reportPosAnalyze(_reportEnum mode, _g.g._transControlTypeEnum transFlag, string screenName)
        {
            Boolean __landscape = false;

            this._screenName = screenName;
            this._mode = mode;
            this._transFlag = transFlag;
            this._report = new SMLReport._generate(screenName, __landscape);
            this._report._query += new SMLReport._generate.QueryEventHandler(_report__query);
            this._report._init += new SMLReport._generate.InitEventHandler(_report__init);
            this._report._showCondition += new SMLReport._generate.ShowConditionEventHandler(_report__showCondition);
            this._report._dataRowSelect += new SMLReport._generate.DataRowSelectEventHandler(_report__dataRowSelect);
            this._report._renderValue += new SMLReport._generate.RenderValueEventHandler(_report__renderValue);
            this._report._renderFont += new SMLReport._generate.RenderFontEventHandler(_report__renderFont);
            this._report.Disposed += new EventHandler(_report_Disposed);
            //
            this._report.Dock = DockStyle.Fill;
            this.Controls.Add(this._report);
            this._report__showCondition(screenName);
        }

        Font _report__renderFont(DataRow data, SMLReport._generateColumnListClass source, SMLReport._generateLevelClass sender)
        {
            if (sender._levelName.Equals(_levelNameDetail))
            {
                int __columnNumber = this._dataTableDetail.Columns.IndexOf(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._ref_row);
                if (__columnNumber != -1)
                {
                    if ((int)MyLib._myGlobal._decimalPhase(data[__columnNumber].ToString()) == -1)
                    {
                        return new Font(source._font.FontFamily, source._font.Size, FontStyle.Italic);
                    }
                }
            }
            return source._font;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="columnName"></param>
        /// <param name="mode">0=ปรับให้เป็นแยกนอก,1=ปรับให้เป็นรวมใน</param>
        void _changeValueVatType(SMLReport._generateLevelClass sender, string columnName, int columnNumber, int __vatTypeColumnNumber)
        {
            int __columnNumber = sender._findColumnName(columnName);
            if (__columnNumber != -1 && __columnNumber == columnNumber && __vatTypeColumnNumber != -1)
            {
                // ดูประเภทภาษี
                int __vatType = (int)MyLib._myGlobal._decimalPhase(sender._columnList[__vatTypeColumnNumber]._dataStr);
                //
                switch (this._vatNumberType)
                {
                    case _reportVatNumberType.แสดงตัวเลขแบบแยกนอกทั้งหมด:
                        switch (__vatType)
                        {
                            case 1:
                                {
                                    // กรณีข้อมูลเป็นแบบรวมใน ให้เอามาแยกนอก
                                    decimal __value = sender._columnList[__columnNumber]._dataNumber * 100M / 107M;
                                    sender._columnList[__columnNumber]._dataNumber = __value;
                                }
                                break;
                        }
                        break;
                    case _reportVatNumberType.แสดงตัวเลขแบบรวมในทั้งหมด:
                        switch (__vatType)
                        {
                            case 0:
                                {
                                    // กรณีข้อมูลเป็นแบบแยกนอก ให้เอามารวมใน
                                    decimal __value = sender._columnList[__columnNumber]._dataNumber + (sender._columnList[__columnNumber]._dataNumber * (7M / 100M));
                                    sender._columnList[__columnNumber]._dataNumber = __value;
                                }
                                break;
                        }
                        break;
                }
            }
        }

        void _report__renderValue(SMLReport._generateLevelClass sender, int columnNumber, SMLReport._generateColumnStyle isTotal)
        {
            if (isTotal == SMLReport._generateColumnStyle.Data)
            {
                switch (this._mode)
                {
                    case _reportEnum.ซื้อ_รายงานตัดเงินจ่ายล่วงหน้า:
                    case _reportEnum.ซื้อ_รายงานตัดเงินมัดจำจ่าย:
                    case _reportEnum.ขาย_รับเงินมัดจำ_รายงานตัดเงิน:
                    case _reportEnum.ขาย_รับเงินล่วงหน้า_รายงานตัดเงิน:
                        {
                            int __transFlagColumnNumber = sender._findColumnName(_g.d.cb_trans_detail._table + "." + _g.d.cb_trans_detail._trans_flag);
                            if (__transFlagColumnNumber != -1 && __transFlagColumnNumber == columnNumber)
                            {
                                int __value = (int)MyLib._myGlobal._decimalPhase(sender._columnList[columnNumber]._dataStr);
                                sender._columnList[columnNumber]._dataStr = _g.g._transFlagGlobal._transName(__value);
                            }
                        }
                        break;
                }
                int __vatTypeColumnNumber = sender._findColumnName(_g.d.ic_trans._table + "." + _g.d.ic_trans._vat_type);
                if (__vatTypeColumnNumber != -1 && __vatTypeColumnNumber == columnNumber)
                {
                    // 0=แยกนอก
                    // 1=รวมใน
                    // 2=ยกเว้น
                    switch ((int)MyLib._myGlobal._decimalPhase(sender._columnList[columnNumber]._dataStr))
                    {
                        case 0: sender._columnList[columnNumber]._dataStr = "E"; break;
                        case 1: sender._columnList[columnNumber]._dataStr = "I"; break;
                        case 2: sender._columnList[columnNumber]._dataStr = "C"; break;
                    }
                }
                // ปรับภาษี ตามเงื่อนไข (ส่วนหัว)
                if (__vatTypeColumnNumber != -1)
                {
                    this._changeValueVatType(sender, _g.d.ic_trans._table + "." + _g.d.ic_trans._total_value, columnNumber, __vatTypeColumnNumber);
                    this._changeValueVatType(sender, _g.d.ic_trans._table + "." + _g.d.ic_trans._total_discount, columnNumber, __vatTypeColumnNumber);
                    this._changeValueVatType(sender, _g.d.ic_trans._table + "." + _g.d.ic_trans._total_after_discount, columnNumber, __vatTypeColumnNumber);
                }
                int __vatTypeDetailColumnNumber = sender._findColumnName(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._tax_type);
                if (__vatTypeDetailColumnNumber != -1 && __vatTypeDetailColumnNumber == columnNumber)
                {
                    // 0=แยกนอก
                    // 1=รวมใน
                    // 2=ยกเว้น
                    switch ((int)MyLib._myGlobal._decimalPhase(sender._columnList[columnNumber]._dataStr))
                    {
                        case 0: sender._columnList[columnNumber]._dataStr = "V"; break;
                        case 1: sender._columnList[columnNumber]._dataStr = ""; break;
                    }
                }
                // ปรับภาษี ตามเงื่อนไข (ส่วนรายละเอียด)
                if (__vatTypeDetailColumnNumber != -1)
                {
                    this._changeValueVatType(sender, _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._price, columnNumber, __vatTypeDetailColumnNumber);
                    this._changeValueVatType(sender, _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._discount_amount, columnNumber, __vatTypeDetailColumnNumber);
                    this._changeValueVatType(sender, _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._sum_amount, columnNumber, __vatTypeDetailColumnNumber);
                }
                //
                int __lastStatusColumnNumber = sender._findColumnName(_g.d.ic_trans._table + "." + _g.d.ic_trans._last_status);
                if (__lastStatusColumnNumber != -1 && __lastStatusColumnNumber == columnNumber)
                {
                    // last_status,used_status,doc_success,not_approve,on_hold,approve_status
                    string[] __str = sender._columnList[columnNumber]._dataStr.Split(',');
                    string __message = "";
                    if (__str.Length > 1)
                    {
                        int __lastStatus = Int32.Parse(__str[0]);
                        int __usedStatus = Int32.Parse(__str[1]);
                        int __docSuccess = Int32.Parse(__str[2]);
                        int __notApprove = Int32.Parse(__str[3]);
                        int __onHold = Int32.Parse(__str[4]);
                        int __approveStatus = Int32.Parse(__str[5]);
                        int __expireDateStatus = Int32.Parse(__str[6]);
                        //
                        switch (this._mode)
                        {
                            case _reportEnum.ซื้อ_รายงานค้างรับตามใบสั่งซื้อ:
                            case _reportEnum.ซื้อ_รายงานใบสั่งซื้อ:
                            case _reportEnum.ซื้อ_รายงานสถานะใบสั่งซื้อ:
                                switch (__approveStatus)
                                {
                                    case 0: __message = MyLib._myGlobal._resource("รออนุมัติ"); break;
                                    case 1: __message = MyLib._myGlobal._resource("อนุมัติเรียบร้อย"); break;
                                    case 2: __message = MyLib._myGlobal._resource("ไม่อนุมัติ"); break;
                                }
                                if (__onHold == 1) __message = MyLib._myGlobal._resource("ระงับชั่วคราว");
                                if (__lastStatus == 1) __message = MyLib._myGlobal._resource("ยกเลิก");
                                if (__expireDateStatus == 1) __message = MyLib._myGlobal._resource("เอกสารหมดอายุ");
                                if (__docSuccess == 1) __message = MyLib._myGlobal._resource("มีการรับสินค้า");
                                break;
                            case _reportEnum.ซื้อ_ใบเสนอซื้อ:
                            case _reportEnum.ซื้อ_ใบเสนอซื้อ_สถานะ:
                                switch (__approveStatus)
                                {
                                    case 0: __message = MyLib._myGlobal._resource("รออนุมัติ"); break;
                                    case 1: __message = MyLib._myGlobal._resource("อนุมัติเรียบร้อย"); break;
                                    case 2: __message = MyLib._myGlobal._resource("ไม่อนุมัติ"); break;
                                }
                                if (__onHold == 1) __message = MyLib._myGlobal._resource("ระงับชั่วคราว");
                                if (__lastStatus == 1) __message = MyLib._myGlobal._resource("ยกเลิก");
                                if (__expireDateStatus == 1) __message = MyLib._myGlobal._resource("เอกสารหมดอายุ");
                                break;
                            case _reportEnum.ซื้อ_ใบเสนอซื้อ_อนุมัติ:
                                if (__docSuccess == 0) __message = MyLib._myGlobal._resource("รอออกใบสั่งซื้อ");
                                if (__docSuccess == 1) __message = MyLib._myGlobal._resource("ออกใบสั่งซื้อเรียบร้อย");
                                break;
                            case _reportEnum.ขาย_เสนอราคา:
                            case _reportEnum.ขาย_เสนอราคา_สถานะ:
                                switch (__approveStatus)
                                {
                                    case 0: __message = MyLib._myGlobal._resource("รออนุมัติ"); break;
                                    case 1: __message = MyLib._myGlobal._resource("อนุมัติเรียบร้อย"); break;
                                    case 2: __message = MyLib._myGlobal._resource("ไม่อนุมัติ"); break;
                                }
                                if (__onHold == 1) __message = MyLib._myGlobal._resource("ระงับชั่วคราว");
                                if (__lastStatus == 1) __message = MyLib._myGlobal._resource("ยกเลิก");
                                if (__expireDateStatus == 1) __message = MyLib._myGlobal._resource("เอกสารหมดอายุ");
                                break;
                            case _reportEnum.ขาย_เสนอราคา_อนุมัติ:
                                if (__docSuccess == 0) __message = MyLib._myGlobal._resource("รอออกใบสั่งซื้อ");
                                if (__docSuccess == 1) __message = MyLib._myGlobal._resource("ออกใบสั่งซื้อเรียบร้อย");
                                break;
                            case _reportEnum.ขาย_สั่งจองและสั่งซื้อสินค้า:
                            case _reportEnum.ขาย_สั่งจองและสั่งซื้อสินค้า_สถานะ:
                                switch (__approveStatus)
                                {
                                    case 0: __message = MyLib._myGlobal._resource("รออนุมัติ"); break;
                                    case 1: __message = MyLib._myGlobal._resource("อนุมัติเรียบร้อย"); break;
                                    case 2: __message = MyLib._myGlobal._resource("ไม่อนุมัติ"); break;
                                }
                                if (__onHold == 1) __message = MyLib._myGlobal._resource("ระงับชั่วคราว");
                                if (__lastStatus == 1) __message = MyLib._myGlobal._resource("ยกเลิก");
                                if (__expireDateStatus == 1) __message = MyLib._myGlobal._resource("เอกสารหมดอายุ");
                                break;
                            case _reportEnum.ขาย_สั่งจองและสั่งซื้อสินค้า_อนุมัติ:
                                if (__docSuccess == 0) __message = MyLib._myGlobal._resource("รอออกใบสั่งซื้อ");
                                if (__docSuccess == 1) __message = MyLib._myGlobal._resource("ออกใบสั่งซื้อเรียบร้อย");
                                break;
                            case _reportEnum.ขาย_สั่งขาย:
                            case _reportEnum.ขาย_สั่งขาย_สถานะ:
                                switch (__approveStatus)
                                {
                                    case 0: __message = MyLib._myGlobal._resource("รออนุมัติ"); break;
                                    case 1: __message = MyLib._myGlobal._resource("อนุมัติเรียบร้อย"); break;
                                    case 2: __message = MyLib._myGlobal._resource("ไม่อนุมัติ"); break;
                                }
                                if (__onHold == 1) __message = MyLib._myGlobal._resource("ระงับชั่วคราว");
                                if (__lastStatus == 1) __message = MyLib._myGlobal._resource("ยกเลิก");
                                if (__expireDateStatus == 1) __message = MyLib._myGlobal._resource("เอกสารหมดอายุ");
                                break;
                            case _reportEnum.ขาย_สั่งขาย_อนุมัติ:
                                if (__docSuccess == 0) __message = MyLib._myGlobal._resource("รอออกใบสั่งซื้อ");
                                if (__docSuccess == 1) __message = MyLib._myGlobal._resource("ออกใบสั่งซื้อเรียบร้อย");
                                break;
                            default:
                                if (__onHold == 1) __message = MyLib._myGlobal._resource("ระงับชั่วคราว");
                                if (__usedStatus == 1) __message = MyLib._myGlobal._resource("อ้างอิงบางส่วน");
                                if (__docSuccess == 1) __message = MyLib._myGlobal._resource("อ้างอิงครบ");
                                if (__notApprove == 1) __message = MyLib._myGlobal._resource("ไม่อนุมัติ");
                                if (__lastStatus == 1) __message = MyLib._myGlobal._resource("ยกเลิก");
                                break;
                        }
                    }
                    sender._columnList[columnNumber]._dataStr = __message;
                }
            }
            //
            switch (this._mode)
            {
                case _reportEnum.ซื้อ_ใบเสนอซื้อ:
                case _reportEnum.ซื้อ_ใบเสนอซื้อ_อนุมัติ:
                case _reportEnum.ซื้อ_ใบเสนอซื้อ_ยกเลิก:
                case _reportEnum.ซื้อ_ใบเสนอซื้อ_สถานะ:
                    {
                        if (isTotal == SMLReport._generateColumnStyle.Data)
                        {
                            int __inquiryTypeColumnNumber = sender._findColumnName(_g.d.ic_trans._table + "." + _g.d.ic_trans._inquiry_type);
                            if (columnNumber == __inquiryTypeColumnNumber)
                            {
                                int __type = (int)MyLib._myGlobal._decimalPhase(sender._columnList[columnNumber]._dataStr);
                                string __fieldName = _g.d.ic_trans._table + "." + _g.g._purchaseType[__type];
                                sender._columnList[columnNumber]._dataStr = MyLib._myResource._findResource(__fieldName, __fieldName)._str;
                            }
                        }
                    }
                    break;
                case _reportEnum.ขาย_เสนอราคา:
                case _reportEnum.ขาย_เสนอราคา_ยกเลิก:
                case _reportEnum.ขาย_เสนอราคา_สถานะ:
                case _reportEnum.ขาย_เสนอราคา_อนุมัติ:
                    {
                        if (isTotal == SMLReport._generateColumnStyle.Data)
                        {
                            int __inquiryTypeColumnNumber = sender._findColumnName(_g.d.ic_trans._table + "." + _g.d.ic_trans._inquiry_type);
                            if (columnNumber == __inquiryTypeColumnNumber)
                            {
                                int __type = (int)MyLib._myGlobal._decimalPhase(sender._columnList[columnNumber]._dataStr);
                                string __fieldName = _g.d.ic_trans._table + "." + _g.g._saleType[__type];
                                sender._columnList[columnNumber]._dataStr = MyLib._myResource._findResource(__fieldName, __fieldName)._str;
                            }
                        }
                    }
                    break;
                case _reportEnum.ขาย_สั่งจองและสั่งซื้อสินค้า:
                case _reportEnum.ขาย_สั่งจองและสั่งซื้อสินค้า_ยกเลิก:
                case _reportEnum.ขาย_สั่งจองและสั่งซื้อสินค้า_สถานะ:
                case _reportEnum.ขาย_สั่งจองและสั่งซื้อสินค้า_อนุมัติ:
                    {
                        if (isTotal == SMLReport._generateColumnStyle.Data)
                        {
                            int __inquiryTypeColumnNumber = sender._findColumnName(_g.d.ic_trans._table + "." + _g.d.ic_trans._inquiry_type);
                            if (columnNumber == __inquiryTypeColumnNumber)
                            {
                                int __type = (int)MyLib._myGlobal._decimalPhase(sender._columnList[columnNumber]._dataStr);
                                string __fieldName = _g.d.ic_trans._table + "." + _g.g._saleType[__type];
                                sender._columnList[columnNumber]._dataStr = MyLib._myResource._findResource(__fieldName, __fieldName)._str;
                            }
                        }
                    }
                    break;
                case _reportEnum.ขาย_สั่งขาย:
                case _reportEnum.ขาย_สั่งขาย_ยกเลิก:
                case _reportEnum.ขาย_สั่งขาย_สถานะ:
                case _reportEnum.ขาย_สั่งขาย_อนุมัติ:
                    {
                        if (isTotal == SMLReport._generateColumnStyle.Data)
                        {
                            int __inquiryTypeColumnNumber = sender._findColumnName(_g.d.ic_trans._table + "." + _g.d.ic_trans._inquiry_type);
                            if (columnNumber == __inquiryTypeColumnNumber)
                            {
                                int __type = (int)MyLib._myGlobal._decimalPhase(sender._columnList[columnNumber]._dataStr);
                                string __fieldName = _g.d.ic_trans._table + "." + _g.g._saleType[__type];
                                sender._columnList[columnNumber]._dataStr = MyLib._myResource._findResource(__fieldName, __fieldName)._str;
                            }
                        }
                    }
                    break;
                case _reportEnum.สินค้า_pos_sale_sugest_by_item_and_serial:
                    {
                        if (isTotal == SMLReport._generateColumnStyle.Data)
                        {
                            int __inquiryTypeColumnNumber = sender._findColumnName(_g.d.ic_trans_serial_number._table + "." + _g.d.ic_trans_serial_number._serial_number);
                            if (columnNumber == __inquiryTypeColumnNumber)
                            {
                                //int __type = (int)MyLib._myGlobal._decimalPhase(sender._columnList[columnNumber]._dataStr);
                                //string __fieldName = _g.d.ic_trans._table + "." + _g.g._saleType[__type];
                                int __item_code_columnnumber = sender._levelParent._findColumnName(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code);
                                int __doc_no_columnnumber = sender._findColumnName(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._doc_no);

                                string __item_code = sender._levelParent._columnList[__item_code_columnnumber]._dataStr;
                                string __doc_no = sender._columnList[__doc_no_columnnumber]._dataStr;

                                DataRow[] __serial_list = this._dataAddtional.Select("ic_trans_detail.item_code=\'" + __item_code + "\' and ic_trans_detail.doc_no =\'" + __doc_no + "\'");

                                List<string> __serial_list_str = new List<string>();
                                for (int __row = 0; __row < __serial_list.Length; __row++)
                                {
                                    __serial_list_str.Add(__serial_list[__row][_g.d.ic_trans_serial_number._table + "." + _g.d.ic_trans_serial_number._serial_number].ToString());
                                }

                                sender._columnList[columnNumber]._dataStr = string.Join(", ", __serial_list_str.ToArray()); // "serial number"; //MyLib._myResource._findResource(__fieldName, __fieldName)._str;
                            }
                        }

                    }
                    break;
            }
        }

        void _report_Disposed(object sender, EventArgs e)
        {
            this.Dispose();
        }

        DataRow[] _report__dataRowSelect(SMLReport._generateLevelClass levelParent, SMLReport._generateLevelClass level, DataRow source)
        {
            if (this._dataTable == null)
            {
                return null;
            }
            if (level._levelName.Equals(this._levelNameRoot))
            {
                return this._dataTable.Select();
            }
            else
            {
                try
                {
                    if (level._levelName.Equals(this._levelNameDetail))
                    {
                        StringBuilder __where = new StringBuilder();
                        switch (this._mode)
                        {
                            case _reportEnum.ขาย_รับเงินมัดจำ_รายงานตัดเงิน:
                            case _reportEnum.ขาย_รับเงินล่วงหน้า_รายงานตัดเงิน:
                            case _reportEnum.ซื้อ_รายงานตัดเงินจ่ายล่วงหน้า:
                            case _reportEnum.ซื้อ_รายงานตัดเงินมัดจำจ่าย:
                                {
                                    int __refColumn = levelParent._findColumnName(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no);
                                    __where.Append(_g.d.cb_trans_detail._table + "." + _g.d.cb_trans_detail._trans_number + "=\'" + source[levelParent._columnList[__refColumn]._fieldName].ToString() + "\'");
                                }
                                break;
                            case _reportEnum.สินค้า_pos_sale_sugest_by_item_and_serial:
                                {
                                    int __refColumn = levelParent._findColumnName(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code);
                                    __where.Append(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code + "=\'" + source[levelParent._columnList[__refColumn]._fieldName].ToString() + "\'");

                                }
                                break;
                            default:
                                {
                                    Boolean __foundIsWhere = false;
                                    for (int __loop = 0; __loop < levelParent._columnList.Count; __loop++)
                                    {
                                        if (levelParent._columnList[__loop]._isWhere)
                                        {
                                            __foundIsWhere = true;
                                            break;
                                        }
                                    }
                                    if (__foundIsWhere == false)
                                    {
                                        // Report ที่ไม่ได้กำหนดการค้นหา Detail ให้เอา 2 Field แรก
                                        for (int __loop = 0; __loop < 2; __loop++)
                                        {
                                            if (levelParent._columnList[__loop]._fieldName.Length > 0)
                                            {
                                                if (__where.Length > 0)
                                                {
                                                    __where.Append(" and ");
                                                }
                                                __where.Append(levelParent._columnList[__loop]._fieldName + "=\'" + source[levelParent._columnList[__loop]._fieldName].ToString().Replace("\'", "\'\'") + "\'");
                                            }
                                        }
                                    }
                                    else
                                    {
                                        for (int __loop = 0; __loop < levelParent._columnList.Count; __loop++)
                                        {
                                            if (levelParent._columnList[__loop]._isWhere)
                                            {
                                                if (levelParent._columnList[__loop]._fieldName.Length > 0)
                                                {
                                                    if (__where.Length > 0)
                                                    {
                                                        __where.Append(" and ");
                                                    }
                                                    __where.Append(levelParent._columnList[__loop]._fieldName + "=\'" + source[levelParent._columnList[__loop]._fieldName].ToString().Replace("\'", "\'\'") + "\'");
                                                }
                                            }
                                        }
                                    }
                                }
                                break;
                        }
                        return (this._dataTableDetail == null || this._dataTableDetail.Rows.Count == 0) ? null : this._dataTableDetail.Select(__where.ToString());
                    }
                }
                catch (Exception __ex)
                {
                    MessageBox.Show(__ex.Message.ToString());
                }
            }
            return null;
        }

        void _report__showCondition(string screenName)
        {
            this._showCondition();
        }

        private void _reportInitRootColumn(List<SMLReport._generateColumnListClass> columnList)
        {
            FontStyle __fontStyle = (this._displayDetail) ? FontStyle.Bold : FontStyle.Regular;
            switch (this._mode)
            {
                case _reportEnum.ขาย_รับเงินล่วงหน้า_ยกเลิก:
                case _reportEnum.ขาย_รับเงินล่วงหน้า_คืน_ยกเลิก:
                case _reportEnum.ขาย_รับเงินมัดจำ_ยกเลิก:
                case _reportEnum.ขาย_รับเงินมัดจำ_คืน_ยกเลิก:
                case _reportEnum.ขาย_รับเงินมัดจำ_รายงานยอดคงเหลือ:
                case _reportEnum.ขาย_รับเงินมัดจำ_รายงานตัดเงิน:
                case _reportEnum.ขาย_รับเงินล่วงหน้า_รายงานตัดเงิน:
                case _reportEnum.ขาย_รับเงินล่วงหน้า_รายงานยอดคงเหลือ:
                    {
                        string __custCodeField = "";
                        string __custNameField = "";
                        switch (SMLERPReportTool._global._reportType(this._mode))
                        {
                            case _reportTypeEnum.ซื้อ_เจ้าหนี้:
                            case _reportTypeEnum.เจ้าหนี้:
                                __custCodeField = _g.d.ic_trans._table + "." + _g.d.ic_trans._ap_code;
                                __custNameField = _g.d.ic_trans._table + "." + _g.d.ic_trans._ap_name;
                                break;
                            case _reportTypeEnum.ลูกหนี้:
                            case _reportTypeEnum.ขาย_ลูกหนี้:
                                __custCodeField = _g.d.ic_trans._table + "." + _g.d.ic_trans._ar_code;
                                __custNameField = _g.d.ic_trans._table + "." + _g.d.ic_trans._ar_name;
                                break;
                        }
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_date, null, 10, SMLReport._report._cellType.DateTime, 0, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no, null, 20, SMLReport._report._cellType.String, 0, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_ref_date, null, 10, SMLReport._report._cellType.DateTime, 0, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_ref, null, 20, SMLReport._report._cellType.String, 0, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._cust_code, __custCodeField, 10, SMLReport._report._cellType.String, 0, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._cust_name, __custNameField, 25, SMLReport._report._cellType.String, 0, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._total_amount, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, __fontStyle));
                        switch (this._mode)
                        {
                            case _reportEnum.ขาย_รับเงินล่วงหน้า_รายงานยอดคงเหลือ:
                            case _reportEnum.ขาย_รับเงินมัดจำ_รายงานยอดคงเหลือ:
                            case _reportEnum.ขาย_รับเงินมัดจำ_รายงานตัดเงิน:
                            case _reportEnum.ขาย_รับเงินล่วงหน้า_รายงานตัดเงิน:
                                string __resourceName = "";
                                switch (this._mode)
                                {
                                    case _reportEnum.ขาย_รับเงินล่วงหน้า_รายงานยอดคงเหลือ:
                                    case _reportEnum.ขาย_รับเงินล่วงหน้า_รายงานตัดเงิน:
                                        __resourceName = _g.d.ic_trans._table + "." + _g.d.ic_trans._deposit_buy;
                                        break;
                                    case _reportEnum.ขาย_รับเงินมัดจำ_รายงานยอดคงเหลือ:
                                    case _reportEnum.ขาย_รับเงินมัดจำ_รายงานตัดเงิน:
                                        __resourceName = _g.d.ic_trans._table + "." + _g.d.ic_trans._deposit_buy2;
                                        break;
                                }
                                columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._deposit_buy2, __resourceName, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, __fontStyle));
                                columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._sum_used, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, __fontStyle));
                                columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._balance_amount, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, __fontStyle));
                                break;
                        }
                    }
                    break;
                case _reportEnum.ขาย_เสนอราคา:
                case _reportEnum.ขาย_เสนอราคา_ยกเลิก:
                case _reportEnum.ขาย_เสนอราคา_อนุมัติ:
                case _reportEnum.ขาย_เสนอราคา_สถานะ:
                    {
                        string __userApproveResourceName = "";
                        string __userApproveFieldName = "";
                        switch (this._mode)
                        {
                            case _reportEnum.ขาย_เสนอราคา:
                                __userApproveResourceName = _g.d.ic_trans._approve_code;
                                __userApproveFieldName = _g.d.ic_trans._approve_code;
                                break;
                            case _reportEnum.ขาย_เสนอราคา_อนุมัติ:
                                __userApproveResourceName = _g.d.ic_trans._user_approve;
                                __userApproveFieldName = _g.d.ic_trans._user_approve;
                                break;
                            case _reportEnum.ขาย_เสนอราคา_ยกเลิก:
                                __userApproveResourceName = _g.d.ic_trans._user_cancel;
                                __userApproveFieldName = _g.d.ic_trans._user_approve;
                                break;
                            case _reportEnum.ขาย_เสนอราคา_สถานะ:
                                __userApproveResourceName = _g.d.ic_trans._approve_code;
                                __userApproveFieldName = _g.d.ic_trans._approve_code;
                                break;
                        }
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_date, null, 10, SMLReport._report._cellType.DateTime, 0, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no, null, 15, SMLReport._report._cellType.String, 0, __fontStyle, true));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_time, null, 8, SMLReport._report._cellType.String, 0, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._ar_name, null, 25, SMLReport._report._cellType.String, 0, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._inquiry_type, null, 15, SMLReport._report._cellType.String, 0, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._department_code, null, 12, SMLReport._report._cellType.String, 0, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_group, null, 12, SMLReport._report._cellType.String, 0, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._user_request, null, 12, SMLReport._report._cellType.String, 0, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + __userApproveFieldName, _g.d.ic_trans._table + "." + __userApproveResourceName, 12, SMLReport._report._cellType.String, 0, __fontStyle));
                        if (this._mode != _reportEnum.ขาย_เสนอราคา_ยกเลิก)
                        {
                            columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._last_status, null, 12, SMLReport._report._cellType.String, 0, __fontStyle));
                        }
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._total_before_vat, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, __fontStyle));
                    }
                    break;
                case _reportEnum.ขาย_สั่งจองและสั่งซื้อสินค้า:
                case _reportEnum.ขาย_สั่งจองและสั่งซื้อสินค้า_ยกเลิก:
                case _reportEnum.ขาย_สั่งจองและสั่งซื้อสินค้า_สถานะ:
                case _reportEnum.ขาย_สั่งจองและสั่งซื้อสินค้า_อนุมัติ:
                    {
                        string __userApproveResourceName = "";
                        string __userApproveFieldName = "";
                        switch (this._mode)
                        {
                            case _reportEnum.ขาย_สั่งจองและสั่งซื้อสินค้า:
                                __userApproveResourceName = _g.d.ic_trans._approve_code;
                                __userApproveFieldName = _g.d.ic_trans._approve_code;
                                break;
                            case _reportEnum.ขาย_สั่งจองและสั่งซื้อสินค้า_อนุมัติ:
                                __userApproveResourceName = _g.d.ic_trans._user_approve;
                                __userApproveFieldName = _g.d.ic_trans._user_approve;
                                break;
                            case _reportEnum.ขาย_สั่งจองและสั่งซื้อสินค้า_ยกเลิก:
                                __userApproveResourceName = _g.d.ic_trans._user_cancel;
                                __userApproveFieldName = _g.d.ic_trans._user_approve;
                                break;
                            case _reportEnum.ขาย_สั่งจองและสั่งซื้อสินค้า_สถานะ:
                                __userApproveResourceName = _g.d.ic_trans._approve_code;
                                __userApproveFieldName = _g.d.ic_trans._approve_code;
                                break;
                        }
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_date, null, 10, SMLReport._report._cellType.DateTime, 0, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no, null, 15, SMLReport._report._cellType.String, 0, __fontStyle, true));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_time, null, 8, SMLReport._report._cellType.String, 0, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._ar_name, null, 25, SMLReport._report._cellType.String, 0, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._inquiry_type, null, 15, SMLReport._report._cellType.String, 0, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._department_code, null, 12, SMLReport._report._cellType.String, 0, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_group, null, 12, SMLReport._report._cellType.String, 0, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._user_request, null, 12, SMLReport._report._cellType.String, 0, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + __userApproveFieldName, _g.d.ic_trans._table + "." + __userApproveResourceName, 12, SMLReport._report._cellType.String, 0, __fontStyle));
                        if (this._mode != _reportEnum.ขาย_สั่งจองและสั่งซื้อสินค้า_ยกเลิก)
                        {
                            columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._last_status, null, 12, SMLReport._report._cellType.String, 0, __fontStyle));
                        }
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._total_before_vat, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, __fontStyle));
                    }
                    break;
                case _reportEnum.ขาย_สั่งขาย:
                case _reportEnum.ขาย_สั่งขาย_ยกเลิก:
                case _reportEnum.ขาย_สั่งขาย_สถานะ:
                case _reportEnum.ขาย_สั่งขาย_อนุมัติ:
                    {
                        string __userApproveResourceName = "";
                        string __userApproveFieldName = "";
                        switch (this._mode)
                        {
                            case _reportEnum.ขาย_สั่งขาย:
                                __userApproveResourceName = _g.d.ic_trans._approve_code;
                                __userApproveFieldName = _g.d.ic_trans._approve_code;
                                break;
                            case _reportEnum.ขาย_สั่งขาย_อนุมัติ:
                                __userApproveResourceName = _g.d.ic_trans._user_approve;
                                __userApproveFieldName = _g.d.ic_trans._user_approve;
                                break;
                            case _reportEnum.ขาย_สั่งขาย_ยกเลิก:
                                __userApproveResourceName = _g.d.ic_trans._user_cancel;
                                __userApproveFieldName = _g.d.ic_trans._user_approve;
                                break;
                            case _reportEnum.ขาย_สั่งขาย_สถานะ:
                                __userApproveResourceName = _g.d.ic_trans._approve_code;
                                __userApproveFieldName = _g.d.ic_trans._approve_code;
                                break;
                        }
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_date, null, 10, SMLReport._report._cellType.DateTime, 0, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no, null, 15, SMLReport._report._cellType.String, 0, __fontStyle, true));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_time, null, 8, SMLReport._report._cellType.String, 0, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._ar_name, null, 25, SMLReport._report._cellType.String, 0, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._inquiry_type, null, 15, SMLReport._report._cellType.String, 0, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._department_code, null, 12, SMLReport._report._cellType.String, 0, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_group, null, 12, SMLReport._report._cellType.String, 0, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._user_request, null, 12, SMLReport._report._cellType.String, 0, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + __userApproveFieldName, _g.d.ic_trans._table + "." + __userApproveResourceName, 12, SMLReport._report._cellType.String, 0, __fontStyle));
                        if (this._mode != _reportEnum.ขาย_สั่งขาย_ยกเลิก)
                        {
                            columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._last_status, null, 12, SMLReport._report._cellType.String, 0, __fontStyle));
                        }
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._total_before_vat, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, __fontStyle));
                    }
                    break;
                case _reportEnum.ซื้อ_จ่ายเงินล่วงหน้า_ยกเลิก:
                case _reportEnum.ซื้อ_จ่ายเงินล่วงหน้า_รับคืน_ยกเลิก:
                case _reportEnum.ซื้อ_จ่ายเงินมัดจำ_ยกเลิก:
                case _reportEnum.ซื้อ_จ่ายเงินมัดจำ_รับคืน_ยกเลิก:
                case _reportEnum.ซื้อ_รายงานยอดคงเหลือเงินมัดจำจ่าย:
                case _reportEnum.ซื้อ_รายงานตัดเงินมัดจำจ่าย:
                case _reportEnum.ซื้อ_รายงานยอดคงเหลือเงินจ่ายล่วงหน้า:
                case _reportEnum.ซื้อ_รายงานตัดเงินจ่ายล่วงหน้า:
                    {
                        string __custCodeField = "";
                        string __custNameField = "";
                        switch (SMLERPReportTool._global._reportType(this._mode))
                        {
                            case _reportTypeEnum.ซื้อ_เจ้าหนี้:
                            case _reportTypeEnum.เจ้าหนี้:
                                __custCodeField = _g.d.ic_trans._table + "." + _g.d.ic_trans._ap_code;
                                __custNameField = _g.d.ic_trans._table + "." + _g.d.ic_trans._ap_name;
                                break;
                            case _reportTypeEnum.ลูกหนี้:
                            case _reportTypeEnum.ขาย_ลูกหนี้:
                                __custCodeField = _g.d.ic_trans._table + "." + _g.d.ic_trans._ar_code;
                                __custNameField = _g.d.ic_trans._table + "." + _g.d.ic_trans._ar_name;
                                break;
                        }
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_date, null, 10, SMLReport._report._cellType.DateTime, 0, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no, null, 20, SMLReport._report._cellType.String, 0, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_ref_date, null, 10, SMLReport._report._cellType.DateTime, 0, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_ref, null, 20, SMLReport._report._cellType.String, 0, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._cust_code, __custCodeField, 10, SMLReport._report._cellType.String, 0, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._cust_name, __custNameField, 25, SMLReport._report._cellType.String, 0, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._total_amount, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, __fontStyle));
                        switch (this._mode)
                        {
                            case _reportEnum.ซื้อ_รายงานยอดคงเหลือเงินจ่ายล่วงหน้า:
                            case _reportEnum.ซื้อ_รายงานยอดคงเหลือเงินมัดจำจ่าย:
                            case _reportEnum.ซื้อ_รายงานตัดเงินมัดจำจ่าย:
                            case _reportEnum.ซื้อ_รายงานตัดเงินจ่ายล่วงหน้า:
                                string __resourceName = "";
                                switch (this._mode)
                                {
                                    case _reportEnum.ซื้อ_รายงานตัดเงินจ่ายล่วงหน้า:
                                    case _reportEnum.ซื้อ_รายงานยอดคงเหลือเงินจ่ายล่วงหน้า:
                                        __resourceName = _g.d.ic_trans._table + "." + _g.d.ic_trans._deposit_sell;
                                        break;
                                    case _reportEnum.ซื้อ_รายงานยอดคงเหลือเงินมัดจำจ่าย:
                                    case _reportEnum.ซื้อ_รายงานตัดเงินมัดจำจ่าย:
                                        __resourceName = _g.d.ic_trans._table + "." + _g.d.ic_trans._deposit_sell2;
                                        break;
                                }
                                columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._deposit_sell2, __resourceName, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, __fontStyle));
                                columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._sum_used, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, __fontStyle));
                                columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._balance_amount, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, __fontStyle));
                                break;
                        }
                    }
                    break;
                case _reportEnum.ลูกหนี้_ตั้งหนี้ยกมา:
                case _reportEnum.ลูกหนี้_เพิ่มหนี้ยกมา:
                case _reportEnum.ลูกหนี้_ลดหนี้ยกมา:
                case _reportEnum.เจ้าหนี้_ตั้งหนี้ยกมา:
                case _reportEnum.เจ้าหนี้_เพิ่มหนี้ยกมา:
                case _reportEnum.เจ้าหนี้_ลดหนี้ยกมา:
                case _reportEnum.เจ้าหนี้_ตั้งหนี้อื่น:
                case _reportEnum.เจ้าหนี้_เพิ่มหนี้อื่น:
                case _reportEnum.เจ้าหนี้_ลดหนี้อื่น:
                    {
                        string __custCodeField = "";
                        string __custNameField = "";
                        switch (SMLERPReportTool._global._reportType(this._mode))
                        {
                            case SMLERPReportTool._reportTypeEnum.เจ้าหนี้:
                                __custCodeField = _g.d.ic_trans._table + "." + _g.d.ic_trans._ap_code;
                                __custNameField = _g.d.ic_trans._table + "." + _g.d.ic_trans._ap_name;
                                break;
                            case SMLERPReportTool._reportTypeEnum.ลูกหนี้:
                                __custCodeField = _g.d.ic_trans._table + "." + _g.d.ic_trans._ar_code;
                                __custNameField = _g.d.ic_trans._table + "." + _g.d.ic_trans._ar_name;
                                break;
                        }
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_date, null, 10, SMLReport._report._cellType.DateTime, 0, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no, null, 20, SMLReport._report._cellType.String, 0, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_ref_date, null, 10, SMLReport._report._cellType.DateTime, 0, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_ref, null, 20, SMLReport._report._cellType.String, 0, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._cust_code, __custCodeField, 10, SMLReport._report._cellType.String, 0, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._cust_name, __custNameField, 25, SMLReport._report._cellType.String, 0, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._total_amount, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, __fontStyle));
                    }
                    break;
                case _reportEnum.สินค้า_รายงานปรับปรุงสินค้าและวัตถุดิบ_ขาด:
                case _reportEnum.สินค้า_รายงานปรับปรุงสินค้าและวัตถุดิบ_เกิน:
                case _reportEnum.สินค้า_รายงานโอนสินค้าและวัตถุดิบ:
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_date, null, 10, SMLReport._report._cellType.DateTime, 0, __fontStyle));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no, null, 20, SMLReport._report._cellType.String, 0, __fontStyle));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_ref_date, null, 10, SMLReport._report._cellType.DateTime, 0, __fontStyle));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_ref, null, 20, SMLReport._report._cellType.String, 0, __fontStyle));
                    break;
                case _reportEnum.สินค้า_รายงานรับคืนเบิกสินค้าวัตถุดิบ:
                case _reportEnum.สินค้า_รายงานเบิกสินค้าวัตถุดิบ:
                case _reportEnum.สินค้า_รายงานรับสินค้าสำเร็จรูป:
                case _reportEnum.สินค้า_รายงานสินค้าและวัตถุดิบคงเหลือยกมา:
                case _reportEnum.สินค้า_รายงานสินค้าตรวจนับ:
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_date, null, 10, SMLReport._report._cellType.DateTime, 0, __fontStyle));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no, null, 20, SMLReport._report._cellType.String, 0, __fontStyle));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_ref_date, null, 10, SMLReport._report._cellType.DateTime, 0, __fontStyle));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_ref, null, 20, SMLReport._report._cellType.String, 0, __fontStyle));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._total_amount, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, __fontStyle));
                    break;
                case _reportEnum.ขาย_ขายสินค้าและบริการ:
                case _reportEnum.ขาย_ขายสินค้าและบริการ_รายงานการออกใบกำกับภาษีอย่างเต็ม:
                case _reportEnum.ขาย_เพิ่มหนี้สินค้าราคาผิด:
                case _reportEnum.ขาย_รับคืนสินค้า:
                    {
                        string __custCodeField = "";
                        string __custNameField = "";
                        switch (SMLERPReportTool._global._reportType(this._mode))
                        {
                            case SMLERPReportTool._reportTypeEnum.ซื้อ_เจ้าหนี้:
                                __custCodeField = _g.d.ic_trans._table + "." + _g.d.ic_trans._ap_code;
                                __custNameField = _g.d.ic_trans._table + "." + _g.d.ic_trans._ap_name;
                                break;
                            case SMLERPReportTool._reportTypeEnum.ขาย_ลูกหนี้:
                                __custCodeField = _g.d.ic_trans._table + "." + _g.d.ic_trans._ar_code;
                                __custNameField = _g.d.ic_trans._table + "." + _g.d.ic_trans._ar_name;
                                break;
                        }
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_date, null, 8, SMLReport._report._cellType.DateTime, 0, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no, null, 10, SMLReport._report._cellType.String, 0, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_time, null, 5, SMLReport._report._cellType.String, 0, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_ref_date, null, 8, SMLReport._report._cellType.DateTime, 0, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_ref, null, 8, SMLReport._report._cellType.String, 0, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._cust_code, __custCodeField, 8, SMLReport._report._cellType.String, 0, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._cust_name, __custNameField, 15, SMLReport._report._cellType.String, 0, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._total_value, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._total_discount, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._total_after_discount, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._total_except_vat, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._vat_rate, null, 5, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._total_vat_value, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._vat_type, null, 5, SMLReport._report._cellType.String, 0, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._total_amount, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._cashier_code, null, 5, SMLReport._report._cellType.String, 0, __fontStyle));
                    }
                    break;
                case _reportEnum.ซื้อ_เพิ่มหนี้ราคาผิด:
                case _reportEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด:
                case _reportEnum.ซื้อ_ซื้อสินค้าและบริการ:
                    {
                        string __custCodeField = "";
                        string __custNameField = "";
                        switch (SMLERPReportTool._global._reportType(this._mode))
                        {
                            case SMLERPReportTool._reportTypeEnum.ซื้อ_เจ้าหนี้:
                                __custCodeField = _g.d.ic_trans._table + "." + _g.d.ic_trans._ap_code;
                                __custNameField = _g.d.ic_trans._table + "." + _g.d.ic_trans._ap_name;
                                break;
                            case SMLERPReportTool._reportTypeEnum.ขาย_ลูกหนี้:
                                __custCodeField = _g.d.ic_trans._table + "." + _g.d.ic_trans._ar_code;
                                __custNameField = _g.d.ic_trans._table + "." + _g.d.ic_trans._ar_name;
                                break;
                        }
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_date, null, 8, SMLReport._report._cellType.DateTime, 0, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no, null, 10, SMLReport._report._cellType.String, 0, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_ref_date, null, 8, SMLReport._report._cellType.DateTime, 0, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_ref, null, 10, SMLReport._report._cellType.String, 0, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._cust_code, __custCodeField, 10, SMLReport._report._cellType.String, 0, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._cust_name, __custNameField, 25, SMLReport._report._cellType.String, 0, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._total_value, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._total_discount, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._total_after_discount, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._total_except_vat, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._total_vat_value, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._total_amount, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, __fontStyle));
                        if (this._mode == _reportEnum.ซื้อ_รายงานสถานะใบสั่งซื้อ)
                        {
                            columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._status, null, 10, SMLReport._report._cellType.String, 0, __fontStyle));
                        }
                    }
                    break;
                case _reportEnum.ขาย_ขายสินค้าและบริการ_ยกเลิก:
                case _reportEnum.ขาย_เพิ่มหนี้สินค้าราคาผิด_ยกเลิก:
                case _reportEnum.ขาย_รับคืนสินค้า_ยกเลิก:
                case _reportEnum.ซื้อ_ซื้อสินค้าและบริการ_ยกเลิก:
                case _reportEnum.ซื้อ_เพิ่มหนี้ราคาผิด_ยกเลิก:
                case _reportEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด_ยกเลิก:
                    {
                        string __custCodeField = "";
                        string __custNameField = "";
                        switch (SMLERPReportTool._global._reportType(this._mode))
                        {
                            case SMLERPReportTool._reportTypeEnum.ซื้อ_เจ้าหนี้:
                                __custCodeField = _g.d.ic_trans._table + "." + _g.d.ic_trans._ap_code;
                                __custNameField = _g.d.ic_trans._table + "." + _g.d.ic_trans._ap_name;
                                break;
                            case SMLERPReportTool._reportTypeEnum.ขาย_ลูกหนี้:
                                __custCodeField = _g.d.ic_trans._table + "." + _g.d.ic_trans._ar_code;
                                __custNameField = _g.d.ic_trans._table + "." + _g.d.ic_trans._ar_name;
                                break;
                        }
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_date, null, 8, SMLReport._report._cellType.DateTime, 0, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no, null, 10, SMLReport._report._cellType.String, 0, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_ref_date, null, 8, SMLReport._report._cellType.DateTime, 0, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_ref, null, 10, SMLReport._report._cellType.String, 0, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._cust_code, __custCodeField, 10, SMLReport._report._cellType.String, 0, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._cust_name, __custNameField, 25, SMLReport._report._cellType.String, 0, __fontStyle));
                    }
                    break;
                case _reportEnum.ซื้อ_รายงานค้างรับตามใบสั่งซื้อ:
                case _reportEnum.ซื้อ_รายงานใบสั่งซื้อ:
                case _reportEnum.ซื้อ_รายงานยกเลิกใบสั่งซื้อ:
                case _reportEnum.ซื้อ_รายงานอนุมัติใบสั่งซื้อ:
                case _reportEnum.ซื้อ_รายงานสถานะใบสั่งซื้อ:
                    {
                        string __custCodeField = _g.d.ic_trans._table + "." + _g.d.ic_trans._ap_code;
                        string __custNameField = _g.d.ic_trans._table + "." + _g.d.ic_trans._ap_name;
                        string __userApproveResourceName = "";
                        string __userApproveFieldName = "";
                        switch (this._mode)
                        {
                            case _reportEnum.ซื้อ_รายงานใบสั่งซื้อ:
                                __userApproveResourceName = _g.d.ic_trans._approve_code;
                                __userApproveFieldName = _g.d.ic_trans._approve_code;
                                break;
                            case _reportEnum.ซื้อ_รายงานอนุมัติใบสั่งซื้อ:
                                __userApproveResourceName = _g.d.ic_trans._user_approve;
                                __userApproveFieldName = _g.d.ic_trans._user_approve;
                                break;
                            case _reportEnum.ซื้อ_รายงานยกเลิกใบสั่งซื้อ:
                                __userApproveResourceName = _g.d.ic_trans._user_cancel;
                                __userApproveFieldName = _g.d.ic_trans._user_approve;
                                break;
                            case _reportEnum.ซื้อ_รายงานสถานะใบสั่งซื้อ:
                                __userApproveResourceName = _g.d.ic_trans._approve_code;
                                __userApproveFieldName = _g.d.ic_trans._approve_code;
                                break;
                        }
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_date, null, 8, SMLReport._report._cellType.DateTime, 0, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no, null, 10, SMLReport._report._cellType.String, 0, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_ref_date, null, 8, SMLReport._report._cellType.DateTime, 0, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_ref, null, 10, SMLReport._report._cellType.String, 0, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._cust_code, __custCodeField, 10, SMLReport._report._cellType.String, 0, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._cust_name, __custNameField, 20, SMLReport._report._cellType.String, 0, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._total_value, null, 8, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._total_discount, null, 8, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._total_after_discount, null, 8, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._total_except_vat, null, 8, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._total_vat_value, null, 8, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._total_amount, null, 8, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._user_request, null, 10, SMLReport._report._cellType.String, 0, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + __userApproveFieldName, _g.d.ic_trans._table + "." + __userApproveResourceName, 10, SMLReport._report._cellType.String, 0, __fontStyle));
                        if (this._mode != _reportEnum.ซื้อ_รายงานยกเลิกใบสั่งซื้อ && this._mode != _reportEnum.ซื้อ_รายงานอนุมัติใบสั่งซื้อ)
                        {
                            columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._last_status, null, 12, SMLReport._report._cellType.String, 0, __fontStyle));
                        }
                    }
                    break;
                case _reportEnum.ซื้อ_ใบเสนอซื้อ:
                case _reportEnum.ซื้อ_ใบเสนอซื้อ_ยกเลิก:
                case _reportEnum.ซื้อ_ใบเสนอซื้อ_อนุมัติ:
                case _reportEnum.ซื้อ_ใบเสนอซื้อ_สถานะ:
                    {
                        string __userApproveResourceName = "";
                        string __userApproveFieldName = "";
                        switch (this._mode)
                        {
                            case _reportEnum.ซื้อ_ใบเสนอซื้อ:
                                __userApproveResourceName = _g.d.ic_trans._approve_code;
                                __userApproveFieldName = _g.d.ic_trans._approve_code;
                                break;
                            case _reportEnum.ซื้อ_ใบเสนอซื้อ_อนุมัติ:
                                __userApproveResourceName = _g.d.ic_trans._user_approve;
                                __userApproveFieldName = _g.d.ic_trans._user_approve;
                                break;
                            case _reportEnum.ซื้อ_ใบเสนอซื้อ_ยกเลิก:
                                __userApproveResourceName = _g.d.ic_trans._user_cancel;
                                __userApproveFieldName = _g.d.ic_trans._user_approve;
                                break;
                            case _reportEnum.ซื้อ_ใบเสนอซื้อ_สถานะ:
                                __userApproveResourceName = _g.d.ic_trans._approve_code;
                                __userApproveFieldName = _g.d.ic_trans._approve_code;
                                break;
                        }
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_date, null, 10, SMLReport._report._cellType.DateTime, 0, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no, null, 15, SMLReport._report._cellType.String, 0, __fontStyle, true));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_time, null, 8, SMLReport._report._cellType.String, 0, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._ap_name, null, 25, SMLReport._report._cellType.String, 0, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._inquiry_type, null, 15, SMLReport._report._cellType.String, 0, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._department_code, null, 12, SMLReport._report._cellType.String, 0, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_group, null, 12, SMLReport._report._cellType.String, 0, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._user_request, null, 12, SMLReport._report._cellType.String, 0, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + __userApproveFieldName, _g.d.ic_trans._table + "." + __userApproveResourceName, 12, SMLReport._report._cellType.String, 0, __fontStyle));
                        if (this._mode != _reportEnum.ซื้อ_ใบเสนอซื้อ_ยกเลิก)
                        {
                            columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._last_status, null, 12, SMLReport._report._cellType.String, 0, __fontStyle));
                        }
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._total_before_vat, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, __fontStyle));
                    }
                    break;
                case _reportEnum.ซื้อ_จ่ายเงินล่วงหน้า:
                case _reportEnum.ซื้อ_จ่ายเงินล่วงหน้า_รับคืน:
                    {
                        string __custCodeField = _g.d.ic_trans._table + "." + _g.d.ic_trans._ap_code;
                        string __custNameField = _g.d.ic_trans._table + "." + _g.d.ic_trans._ap_name;
                        string __payCashField = _g.d.ic_trans._table + "." + ((this._mode == _reportEnum.ซื้อ_จ่ายเงินล่วงหน้า) ? _g.d.ic_trans._sum_pay_money_cash_out : _g.d.ic_trans._sum_pay_money_cash_in);
                        string __payChqField = _g.d.ic_trans._table + "." + ((this._mode == _reportEnum.ซื้อ_จ่ายเงินล่วงหน้า) ? _g.d.ic_trans._sum_pay_money_chq_out : _g.d.ic_trans._sum_pay_money_chq_in);
                        string __payCardField = _g.d.ic_trans._table + "." + ((this._mode == _reportEnum.ซื้อ_จ่ายเงินล่วงหน้า) ? _g.d.ic_trans._sum_pay_money_credit_out : _g.d.ic_trans._sum_pay_money_credit_in);
                        string __payTransferField = _g.d.ic_trans._table + "." + ((this._mode == _reportEnum.ซื้อ_จ่ายเงินล่วงหน้า) ? _g.d.ic_trans._sum_pay_money_transfer_out : _g.d.ic_trans._sum_pay_money_transfer_in);
                        string __payRoundingField = _g.d.ic_trans._table + "." + _g.d.ic_trans._sum_pay_rounding;
                        string __payPettyCashField = _g.d.ic_trans._table + "." + _g.d.ic_trans._sum_petty_cash;
                        //
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_date, null, 8, SMLReport._report._cellType.DateTime, 0, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no, null, 10, SMLReport._report._cellType.String, 0, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_ref_date, null, 8, SMLReport._report._cellType.DateTime, 0, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_ref, null, 10, SMLReport._report._cellType.String, 0, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._cust_code, __custCodeField, 10, SMLReport._report._cellType.String, 0, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._cust_name, __custNameField, 25, SMLReport._report._cellType.String, 0, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._sum_pay_money_cash, __payCashField, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._sum_pay_rounding, __payRoundingField, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._sum_petty_cash, __payPettyCashField, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._sum_pay_money_chq, __payChqField, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._sum_pay_money_transfer, __payTransferField, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, __fontStyle));
                        if (this._mode == _reportEnum.ซื้อ_จ่ายเงินล่วงหน้า_รับคืน)
                        {
                            columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._sum_pay_money_credit, __payCardField, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, __fontStyle));
                        }
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._sum_total, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, __fontStyle));
                    }
                    break;
                case _reportEnum.ซื้อ_จ่ายเงินมัดจำ:
                case _reportEnum.ซื้อ_จ่ายเงินมัดจำ_รับคืน:
                    {
                        string __custCodeField = _g.d.ic_trans._table + "." + _g.d.ic_trans._ap_code;
                        string __custNameField = _g.d.ic_trans._table + "." + _g.d.ic_trans._ap_name;
                        string __payCashField = _g.d.ic_trans._table + "." + ((this._mode == _reportEnum.ซื้อ_จ่ายเงินมัดจำ) ? _g.d.ic_trans._sum_pay_money_cash_out : _g.d.ic_trans._sum_pay_money_cash_in);
                        string __payChqField = _g.d.ic_trans._table + "." + ((this._mode == _reportEnum.ซื้อ_จ่ายเงินมัดจำ) ? _g.d.ic_trans._sum_pay_money_chq_out : _g.d.ic_trans._sum_pay_money_chq_in);
                        string __payCardField = _g.d.ic_trans._table + "." + ((this._mode == _reportEnum.ซื้อ_จ่ายเงินมัดจำ) ? _g.d.ic_trans._sum_pay_money_credit_out : _g.d.ic_trans._sum_pay_money_credit_in);
                        string __payTransferField = _g.d.ic_trans._table + "." + ((this._mode == _reportEnum.ซื้อ_จ่ายเงินมัดจำ) ? _g.d.ic_trans._sum_pay_money_transfer_out : _g.d.ic_trans._sum_pay_money_transfer_in);
                        string __payRoundingField = _g.d.ic_trans._table + "." + _g.d.ic_trans._sum_pay_rounding;
                        string __payPettyCashField = _g.d.ic_trans._table + "." + _g.d.ic_trans._sum_petty_cash;
                        //
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_date, null, 8, SMLReport._report._cellType.DateTime, 0, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no, null, 10, SMLReport._report._cellType.String, 0, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_ref_date, null, 8, SMLReport._report._cellType.DateTime, 0, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_ref, null, 10, SMLReport._report._cellType.String, 0, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._cust_code, __custCodeField, 10, SMLReport._report._cellType.String, 0, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._cust_name, __custNameField, 25, SMLReport._report._cellType.String, 0, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._sum_pay_money_cash, __payCashField, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._sum_pay_rounding, __payRoundingField, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._sum_petty_cash, __payPettyCashField, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._sum_pay_money_chq, __payChqField, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._sum_pay_money_transfer, __payTransferField, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, __fontStyle));
                        if (this._mode == _reportEnum.ซื้อ_จ่ายเงินมัดจำ_รับคืน)
                        {
                            columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._sum_pay_money_credit, __payCardField, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, __fontStyle));
                        }
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._sum_total, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, __fontStyle));
                    }
                    break;
                case _reportEnum.ขาย_รับเงินล่วงหน้า:
                case _reportEnum.ขาย_รับเงินล่วงหน้า_คืน:
                    {
                        string __custCodeField = "";
                        string __custNameField = "";
                        string __payCashField = "";
                        string __payChqField = "";
                        string __payCreditField = "";
                        string __payTransferField = "";
                        switch (this._mode)
                        {
                            case _reportEnum.ขาย_รับเงินล่วงหน้า:
                            case _reportEnum.ขาย_รับเงินล่วงหน้า_คืน:
                                __custCodeField = _g.d.ic_trans._table + "." + _g.d.ic_trans._ap_code;
                                __custNameField = _g.d.ic_trans._table + "." + _g.d.ic_trans._ap_name;
                                __payCashField = _g.d.ic_trans._table + "." + _g.d.ic_trans._sum_pay_money_cash_out;
                                __payChqField = _g.d.ic_trans._table + "." + _g.d.ic_trans._sum_pay_money_chq_out;
                                __payCreditField = _g.d.ic_trans._table + "." + _g.d.ic_trans._sum_pay_money_credit_out;
                                __payTransferField = _g.d.ic_trans._table + "." + _g.d.ic_trans._sum_pay_money_transfer_out;
                                break;
                        }
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_date, null, 8, SMLReport._report._cellType.DateTime, 0, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no, null, 10, SMLReport._report._cellType.String, 0, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_ref_date, null, 8, SMLReport._report._cellType.DateTime, 0, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_ref, null, 10, SMLReport._report._cellType.String, 0, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._cust_code, __custCodeField, 10, SMLReport._report._cellType.String, 0, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._cust_name, __custNameField, 25, SMLReport._report._cellType.String, 0, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._sum_pay_money_cash, __payCashField, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._sum_pay_money_chq, __payChqField, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._sum_pay_money_credit, __payCreditField, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._sum_pay_money_transfer, __payTransferField, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._sum_total, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, __fontStyle));
                    }
                    break;
                case _reportEnum.ขาย_รับเงินมัดจำ:
                case _reportEnum.ขาย_รับเงินมัดจำ_คืน:
                    {
                        string __custCodeField = "";
                        string __custNameField = "";
                        string __payCashField = "";
                        string __payChqField = "";
                        string __payCreditField = "";
                        string __payTransferField = "";
                        switch (this._mode)
                        {
                            case _reportEnum.ขาย_รับเงินมัดจำ:
                            case _reportEnum.ขาย_รับเงินมัดจำ_คืน:
                                __custCodeField = _g.d.ic_trans._table + "." + _g.d.ic_trans._ap_code;
                                __custNameField = _g.d.ic_trans._table + "." + _g.d.ic_trans._ap_name;
                                __payCashField = _g.d.ic_trans._table + "." + _g.d.ic_trans._sum_pay_money_cash_out;
                                __payChqField = _g.d.ic_trans._table + "." + _g.d.ic_trans._sum_pay_money_chq_out;
                                __payCreditField = _g.d.ic_trans._table + "." + _g.d.ic_trans._sum_pay_money_credit_out;
                                __payTransferField = _g.d.ic_trans._table + "." + _g.d.ic_trans._sum_pay_money_transfer_out;
                                break;
                        }
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_date, null, 8, SMLReport._report._cellType.DateTime, 0, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no, null, 10, SMLReport._report._cellType.String, 0, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_ref_date, null, 8, SMLReport._report._cellType.DateTime, 0, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_ref, null, 10, SMLReport._report._cellType.String, 0, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._cust_code, __custCodeField, 10, SMLReport._report._cellType.String, 0, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._cust_name, __custNameField, 25, SMLReport._report._cellType.String, 0, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._sum_pay_money_cash, __payCashField, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._sum_pay_money_chq, __payChqField, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._sum_pay_money_credit, __payCreditField, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._sum_pay_money_transfer, __payTransferField, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, __fontStyle));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans._table + "." + _g.d.ic_trans._sum_total, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, __fontStyle));
                    }
                    break;
                case _reportEnum.สินค้า_pos_sale_sugest_by_item_and_serial:
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code, null, 20, SMLReport._report._cellType.String, 0, __fontStyle));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_name, null, 80, SMLReport._report._cellType.String, 0, __fontStyle));

                    break;
            }
        }

        SMLReport._generateLevelClass _reportInitRoot(SMLReport._generateLevelClass levelParent, Boolean sumTotal, Boolean autoWidth)
        {
            List<SMLReport._generateColumnListClass> __columnList = new List<SMLReport._generateColumnListClass>();
            this._reportInitRootColumn(__columnList);
            return this._report._addLevel(this._levelNameRoot, levelParent, __columnList, sumTotal, autoWidth);
        }

        private void _reportInitDetailColumn(List<SMLReport._generateColumnListClass> columnList, float spaceFirstColumnWidth)
        {
            columnList.Add(new SMLReport._generateColumnListClass("", "", spaceFirstColumnWidth, SMLReport._report._cellType.String, 0));
        }

        /// <summary>
        /// รายละเอียด (บรรทัดที่สอง)
        /// </summary>
        /// <param name="columnList"></param>
        private void _reportInitColumnValue(List<SMLReport._generateColumnListClass> columnList)
        {
            switch (this._mode)
            {
                case _reportEnum.ขาย_รับเงินล่วงหน้า_รายงานตัดเงิน:
                case _reportEnum.ขาย_รับเงินมัดจำ_รายงานตัดเงิน:
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.cb_trans_detail._table + "." + _g.d.cb_trans_detail._trans_flag, _g.d.cb_trans_detail._table + "." + _g.d.cb_trans_detail._trans_type, 20, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.cb_trans_detail._table + "." + _g.d.cb_trans_detail._doc_date, null, 20, SMLReport._report._cellType.DateTime, 0, FontStyle.Regular));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.cb_trans_detail._table + "." + _g.d.cb_trans_detail._doc_no, null, 20, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.cb_trans_detail._table + "." + _g.d.cb_trans_detail._amount, null, 8, SMLReport._report._cellType.Number, _g.g._companyProfile._item_qty_decimal, FontStyle.Regular));
                    break;
                case _reportEnum.ขาย_สั่งจองและสั่งซื้อสินค้า:
                case _reportEnum.ขาย_สั่งจองและสั่งซื้อสินค้า_ยกเลิก:
                    {
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code, null, 15, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_name, null, 25, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._unit_code, null, 10, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._due_date, null, 10, SMLReport._report._cellType.DateTime, 0, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._qty, null, 5, SMLReport._report._cellType.Number, _g.g._companyProfile._item_qty_decimal, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._price, null, 5, SMLReport._report._cellType.Number, _g.g._companyProfile._item_price_decimal, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._discount, null, 10, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._sum_amount, null, 5, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
                    }
                    break;
                case _reportEnum.ขาย_สั่งจองและสั่งซื้อสินค้า_สถานะ:
                    {
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code, null, 15, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_name, null, 25, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._unit_code, null, 10, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._due_date, null, 10, SMLReport._report._cellType.DateTime, 0, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._qty, null, 5, SMLReport._report._cellType.Number, _g.g._companyProfile._item_qty_decimal, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._price, null, 5, SMLReport._report._cellType.Number, _g.g._companyProfile._item_price_decimal, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._discount, null, 5, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._sum_amount, null, 5, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._approval_qty, null, 5, SMLReport._report._cellType.Number, _g.g._companyProfile._item_qty_decimal, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._approval_price, null, 5, SMLReport._report._cellType.Number, _g.g._companyProfile._item_price_decimal, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._approval_discount, null, 5, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._approval_sum_amount, null, 5, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
                    }
                    break;
                case _reportEnum.ขาย_สั่งจองและสั่งซื้อสินค้า_อนุมัติ:
                    {
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code, null, 15, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_name, null, 25, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._unit_code, null, 10, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._due_date, null, 10, SMLReport._report._cellType.DateTime, 0, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._temp_float_1, _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._qty, 5, SMLReport._report._cellType.Number, _g.g._companyProfile._item_qty_decimal, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._temp_float_2, _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._price, 5, SMLReport._report._cellType.Number, _g.g._companyProfile._item_price_decimal, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._discount, null, 10, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._sum_amount, null, 5, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
                    }
                    break;
                case _reportEnum.ขาย_สั่งขาย:
                case _reportEnum.ขาย_สั่งขาย_ยกเลิก:
                    {
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code, null, 15, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_name, null, 25, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._unit_code, null, 10, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._due_date, null, 10, SMLReport._report._cellType.DateTime, 0, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._qty, null, 5, SMLReport._report._cellType.Number, _g.g._companyProfile._item_qty_decimal, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._price, null, 5, SMLReport._report._cellType.Number, _g.g._companyProfile._item_price_decimal, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._discount, null, 10, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._sum_amount, null, 5, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
                    }
                    break;
                case _reportEnum.ขาย_สั่งขาย_สถานะ:
                    {
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code, null, 15, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_name, null, 25, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._unit_code, null, 10, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._due_date, null, 10, SMLReport._report._cellType.DateTime, 0, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._qty, null, 5, SMLReport._report._cellType.Number, _g.g._companyProfile._item_qty_decimal, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._price, null, 5, SMLReport._report._cellType.Number, _g.g._companyProfile._item_price_decimal, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._discount, null, 5, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._sum_amount, null, 5, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._approval_qty, null, 5, SMLReport._report._cellType.Number, _g.g._companyProfile._item_qty_decimal, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._approval_price, null, 5, SMLReport._report._cellType.Number, _g.g._companyProfile._item_price_decimal, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._approval_discount, null, 5, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._approval_sum_amount, null, 5, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
                    }
                    break;
                case _reportEnum.ขาย_สั่งขาย_อนุมัติ:
                    {
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code, null, 15, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_name, null, 25, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._unit_code, null, 10, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._due_date, null, 10, SMLReport._report._cellType.DateTime, 0, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._temp_float_1, _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._qty, 5, SMLReport._report._cellType.Number, _g.g._companyProfile._item_qty_decimal, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._temp_float_2, _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._price, 5, SMLReport._report._cellType.Number, _g.g._companyProfile._item_price_decimal, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._discount, null, 10, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._sum_amount, null, 5, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
                    }
                    break;
                case _reportEnum.ซื้อ_รายงานตัดเงินจ่ายล่วงหน้า:
                case _reportEnum.ซื้อ_รายงานตัดเงินมัดจำจ่าย:
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.cb_trans_detail._table + "." + _g.d.cb_trans_detail._trans_flag, _g.d.cb_trans_detail._table + "." + _g.d.cb_trans_detail._trans_type, 20, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.cb_trans_detail._table + "." + _g.d.cb_trans_detail._doc_date, null, 20, SMLReport._report._cellType.DateTime, 0, FontStyle.Regular));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.cb_trans_detail._table + "." + _g.d.cb_trans_detail._doc_no, null, 20, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.cb_trans_detail._table + "." + _g.d.cb_trans_detail._amount, null, 8, SMLReport._report._cellType.Number, _g.g._companyProfile._item_qty_decimal, FontStyle.Regular));
                    break;
                case _reportEnum.สินค้า_รายงานโอนสินค้าและวัตถุดิบ:
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code, null, 20, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_name, null, 40, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._wh_name_out, null, 8, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._shelf_name_out, null, 8, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._wh_name_in, null, 8, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._shelf_name_in, null, 8, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._unit_code, null, 10, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._qty, null, 8, SMLReport._report._cellType.Number, _g.g._companyProfile._item_qty_decimal, FontStyle.Regular));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._price, _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._cost, 8, SMLReport._report._cellType.Number, _g.g._companyProfile._item_price_decimal, FontStyle.Regular));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._sum_amount, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
                    break;
                case _reportEnum.สินค้า_รายงานปรับปรุงสินค้าและวัตถุดิบ_ขาด:
                case _reportEnum.สินค้า_รายงานปรับปรุงสินค้าและวัตถุดิบ_เกิน:
                case _reportEnum.สินค้า_รายงานรับคืนเบิกสินค้าวัตถุดิบ:
                case _reportEnum.สินค้า_รายงานเบิกสินค้าวัตถุดิบ:
                case _reportEnum.สินค้า_รายงานรับสินค้าสำเร็จรูป:
                case _reportEnum.สินค้า_รายงานสินค้าและวัตถุดิบคงเหลือยกมา:
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code, null, 20, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_name, null, 40, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._wh_code, null, 8, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._shelf_code, null, 8, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._unit_code, null, 10, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._qty, null, 8, SMLReport._report._cellType.Number, _g.g._companyProfile._item_qty_decimal, FontStyle.Regular));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._price, _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._cost, 8, SMLReport._report._cellType.Number, _g.g._companyProfile._item_price_decimal, FontStyle.Regular));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._sum_amount, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
                    break;
                case _reportEnum.สินค้า_รายงานสินค้าตรวจนับ:
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code, null, 20, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                    if (this._showBarcode)
                    {
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._barcode, null, 20, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                    }
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_name, null, 35, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._wh_code, null, 8, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._shelf_code, null, 8, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._unit_code, null, 10, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._qty, null, 8, SMLReport._report._cellType.Number, _g.g._companyProfile._item_qty_decimal, FontStyle.Regular));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._price, null, 8, SMLReport._report._cellType.Number, _g.g._companyProfile._item_price_decimal, FontStyle.Regular));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._sum_amount, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
                    break;
                case _reportEnum.ขาย_ขายสินค้าและบริการ:
                case _reportEnum.ขาย_ขายสินค้าและบริการ_รายงานการออกใบกำกับภาษีอย่างเต็ม:
                case _reportEnum.ขาย_เพิ่มหนี้สินค้าราคาผิด:
                case _reportEnum.ขาย_รับคืนสินค้า:
                case _reportEnum.ซื้อ_เพิ่มหนี้ราคาผิด:
                case _reportEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด:
                case _reportEnum.ซื้อ_ซื้อสินค้าและบริการ:
                    Boolean __warehouseEnable = true;
                    switch (this._mode)
                    {
                        case _reportEnum.ซื้อ_รายงานใบสั่งซื้อ:
                        case _reportEnum.ซื้อ_รายงานยกเลิกใบสั่งซื้อ:
                        case _reportEnum.ซื้อ_รายงานอนุมัติใบสั่งซื้อ:
                        case _reportEnum.ซื้อ_รายงานสถานะใบสั่งซื้อ:
                            __warehouseEnable = false;
                            break;
                    }
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code, null, 20, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_name, null, 40, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                    if (__warehouseEnable)
                    {
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._wh_code, null, 8, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._shelf_code, null, 8, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                    }
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._unit_code, null, 10, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._qty, null, 8, SMLReport._report._cellType.Number, _g.g._companyProfile._item_qty_decimal, FontStyle.Regular));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._price, null, 8, SMLReport._report._cellType.Number, _g.g._companyProfile._item_price_decimal, FontStyle.Regular));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._discount, null, 8, SMLReport._report._cellType.String, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._sum_amount, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._tax_type, _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._vat_type_word, 8, SMLReport._report._cellType.String, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
                    break;
                case _reportEnum.ซื้อ_รายงานค้างรับตามใบสั่งซื้อ:
                case _reportEnum.ซื้อ_รายงานใบสั่งซื้อ:
                case _reportEnum.ซื้อ_รายงานยกเลิกใบสั่งซื้อ:
                case _reportEnum.ซื้อ_รายงานอนุมัติใบสั่งซื้อ:
                case _reportEnum.ซื้อ_รายงานสถานะใบสั่งซื้อ:
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code, null, 15, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_name, null, 25, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._unit_code, null, 10, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._qty, null, 8, SMLReport._report._cellType.Number, _g.g._companyProfile._item_qty_decimal, FontStyle.Regular));
                    if (this._mode != _reportEnum.ซื้อ_รายงานยกเลิกใบสั่งซื้อ)
                    {
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._price, null, 8, SMLReport._report._cellType.Number, _g.g._companyProfile._item_price_decimal, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._discount, null, 8, SMLReport._report._cellType.String, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._sum_amount, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._tax_type, _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._vat_type_word, 8, SMLReport._report._cellType.String, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
                    }
                    break;
                case _reportEnum.ซื้อ_ใบเสนอซื้อ:
                case _reportEnum.ซื้อ_ใบเสนอซื้อ_ยกเลิก:
                    {
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code, null, 15, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_name, null, 25, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._unit_code, null, 10, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._due_date, null, 10, SMLReport._report._cellType.DateTime, 0, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._qty, null, 5, SMLReport._report._cellType.Number, _g.g._companyProfile._item_qty_decimal, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._price, null, 5, SMLReport._report._cellType.Number, _g.g._companyProfile._item_price_decimal, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._discount, null, 10, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._sum_amount, null, 5, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
                    }
                    break;
                case _reportEnum.ซื้อ_ใบเสนอซื้อ_สถานะ:
                    {
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code, null, 15, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_name, null, 25, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._unit_code, null, 10, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._due_date, null, 10, SMLReport._report._cellType.DateTime, 0, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._qty, null, 5, SMLReport._report._cellType.Number, _g.g._companyProfile._item_qty_decimal, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._price, null, 5, SMLReport._report._cellType.Number, _g.g._companyProfile._item_price_decimal, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._discount, null, 5, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._sum_amount, null, 5, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._approval_qty, null, 5, SMLReport._report._cellType.Number, _g.g._companyProfile._item_qty_decimal, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._approval_price, null, 5, SMLReport._report._cellType.Number, _g.g._companyProfile._item_price_decimal, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._approval_discount, null, 5, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._approval_sum_amount, null, 5, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
                    }
                    break;
                case _reportEnum.ซื้อ_ใบเสนอซื้อ_อนุมัติ:
                    {
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code, null, 15, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_name, null, 25, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._unit_code, null, 10, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._due_date, null, 10, SMLReport._report._cellType.DateTime, 0, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._temp_float_1, _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._qty, 5, SMLReport._report._cellType.Number, _g.g._companyProfile._item_qty_decimal, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._temp_float_2, _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._price, 5, SMLReport._report._cellType.Number, _g.g._companyProfile._item_price_decimal, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._discount, null, 10, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._sum_amount, null, 5, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
                    }
                    break;
                case _reportEnum.ขาย_เสนอราคา:
                case _reportEnum.ขาย_เสนอราคา_ยกเลิก:
                    {
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code, null, 15, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_name, null, 25, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._unit_code, null, 10, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._due_date, null, 10, SMLReport._report._cellType.DateTime, 0, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._qty, null, 5, SMLReport._report._cellType.Number, _g.g._companyProfile._item_qty_decimal, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._price, null, 5, SMLReport._report._cellType.Number, _g.g._companyProfile._item_price_decimal, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._discount, null, 10, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._sum_amount, null, 5, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
                    }
                    break;
                case _reportEnum.ขาย_เสนอราคา_สถานะ:
                    {
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code, null, 15, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_name, null, 25, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._unit_code, null, 10, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._due_date, null, 10, SMLReport._report._cellType.DateTime, 0, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._qty, null, 5, SMLReport._report._cellType.Number, _g.g._companyProfile._item_qty_decimal, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._price, null, 5, SMLReport._report._cellType.Number, _g.g._companyProfile._item_price_decimal, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._discount, null, 5, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._sum_amount, null, 5, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._approval_qty, null, 5, SMLReport._report._cellType.Number, _g.g._companyProfile._item_qty_decimal, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._approval_price, null, 5, SMLReport._report._cellType.Number, _g.g._companyProfile._item_price_decimal, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._approval_discount, null, 5, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._approval_sum_amount, null, 5, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
                    }
                    break;
                case _reportEnum.ขาย_เสนอราคา_อนุมัติ:
                    {
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code, null, 15, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_name, null, 25, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._unit_code, null, 10, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._due_date, null, 10, SMLReport._report._cellType.DateTime, 0, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._temp_float_1, _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._qty, 5, SMLReport._report._cellType.Number, _g.g._companyProfile._item_qty_decimal, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._temp_float_2, _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._price, 5, SMLReport._report._cellType.Number, _g.g._companyProfile._item_price_decimal, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._discount, null, 10, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                        columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._sum_amount, null, 5, SMLReport._report._cellType.Number, _g.g._companyProfile._item_amount_decimal, FontStyle.Regular));
                    }
                    break;
                case _reportEnum.สินค้า_pos_sale_sugest_by_item_and_serial:
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._doc_date, null, 10, SMLReport._report._cellType.DateTime, 0, FontStyle.Regular));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._doc_no, null, 15, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._unit_code, null, 10, SMLReport._report._cellType.String, 0, FontStyle.Regular));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_serial_number._table + "." + _g.d.ic_trans_serial_number._serial_number, null, 20, SMLReport._report._cellType.String, 0, FontStyle.Regular));

                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._qty, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_qty_decimal, FontStyle.Regular));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._price, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_qty_decimal, FontStyle.Regular));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._discount_amount, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_qty_decimal, FontStyle.Regular));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._total_vat_value, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_qty_decimal, FontStyle.Regular));
                    columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._sum_amount, null, 10, SMLReport._report._cellType.Number, _g.g._companyProfile._item_qty_decimal, FontStyle.Regular));
                    break;
            }
        }

        SMLReport._generateLevelClass _reportInitDetail(SMLReport._generateLevelClass levelParent, Boolean sumTotal, float spaceFirstColumnWidth, Boolean autoWidth)
        {
            List<SMLReport._generateColumnListClass> __columnList = new List<SMLReport._generateColumnListClass>();
            this._reportInitDetailColumn(__columnList, spaceFirstColumnWidth);
            this._reportInitColumnValue(__columnList);
            return this._report._addLevel(this._levelNameDetail, levelParent, __columnList, sumTotal, autoWidth);
        }

        void _report__init()
        {
            int __cashierCheck = (int)MyLib._myGlobal._decimalPhase(this._form_condition._screen._getDataStr(_g.d.resource_report._cashier_check));
            if (__cashierCheck == 1)
            {
                // พิมพ์แยกตามพนักงานเก็บเงิน
                StringBuilder __where = new StringBuilder();
                for (int __row = 0; __row < this._form_condition._screen._cashierSelectDialog._grid._rowData.Count; __row++)
                {
                    int __check = (int)MyLib._myGlobal._decimalPhase(this._form_condition._screen._cashierSelectDialog._grid._cellGet(__row, 0).ToString());
                    if (__check == 1)
                    {
                        if (__where.Length != 0)
                        {
                            __where.Append(" or ");
                        }
                        __where.Append(_g.d.ic_trans._cashier_code + "=\'" + this._form_condition._screen._cashierSelectDialog._grid._cellGet(__row, _g.d.erp_user._code).ToString() + "\'");
                    }
                }
                string __whereStr = (__where.Length == 0) ? "" : " and (" + __where.ToString() + ")";
                this._report__init_start(__whereStr);
            }
            else
            {
                this._report__init_start("");
            }
        }

        void _report__init_start(string extraWhere)
        {
            this._extraWhere = extraWhere;
            this._displayDetail = this._form_condition._screen._getDataStr(_g.d.resource_report._display_detail).ToString().Equals("1") ? true : false;
            this._showBarcode = this._form_condition._screen._getDataStr(_g.d.resource_report._show_barcode).ToString().Equals("1") ? true : false;
            this._report._level = this._reportInitRoot(null, true, true);

            this._level2 = this._reportInitDetail(this._report._level, false, 2, true);
            //if (this._displayDetail == true)
            //{
            //}
            this._report._resourceTable = ""; // แบบกำหนดเอง
            // ยอดรวมแบบ Grand Total
            this._report._level.__grandTotal = new List<SMLReport._generateColumnListClass>();
            this._reportInitRootColumn(this._report._level.__grandTotal);
            this._report._level._levelGrandTotal = this._report._level;
            //
        }

        void _report__query()
        {
            if (this._dataTable == null)
            {
                try
                {
                    //
                    string __beginDate = this._form_condition._screen._getDataStrQuery(_g.d.resource_report._from_date);
                    string __endDate = this._form_condition._screen._getDataStrQuery(_g.d.resource_report._to_date);
                    Boolean __useLastStatus = this._form_condition._screen._getDataStr(_g.d.resource_report._show_cancel_document).ToString().Equals("1") ? true : false;
                    string __getWhereMain = "";
                    string __getWhere = "";
                    string __getAddtionalWhere = "";

                    string __where = this._form_condition._extra._getWhere("").Replace(" where ", " and ");
                    //
                    MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                    string __lastStatusWhere = (__useLastStatus) ? "" : " and last_status=0";
                    //
                    StringBuilder __extraField = new StringBuilder();
                    StringBuilder __extraAs = new StringBuilder();
                    StringBuilder __where_doc_sucess = new StringBuilder();
                    switch (this._mode)
                    {
                        case _reportEnum.ขาย_เสนอราคา:
                        case _reportEnum.ขาย_เสนอราคา_ยกเลิก:
                        case _reportEnum.ขาย_เสนอราคา_อนุมัติ:
                        case _reportEnum.ขาย_เสนอราคา_สถานะ:
                            __extraAs.Append("doc_time as \"ic_trans.doc_time\",");
                            __extraAs.Append("(select name_1 from ap_supplier where ap_supplier.code=cust_code) ||\'(\'||cust_code||\')\' as \"ic_trans.ap_name\",");
                            __extraAs.Append("department_code as \"ic_trans.department_code\",");
                            __extraAs.Append("doc_group as \"ic_trans.doc_group\",");
                            __extraAs.Append("user_request as \"ic_trans.user_request\",");
                            __extraAs.Append("approve_code as \"ic_trans.approve_code\",");
                            __extraAs.Append("inquiry_type as \"ic_trans.inquiry_type\",");
                            __extraAs.Append("user_approve as \"ic_trans.user_approve\",");
                            __extraAs.Append("total_before_vat as \"ic_trans.total_before_vat\",");
                            //
                            __extraField.Append("doc_time,cust_code,department_code,doc_group,user_request,approve_code,inquiry_type,user_approve,");
                            break;
                        case _reportEnum.ขาย_สั่งจองและสั่งซื้อสินค้า:
                        case _reportEnum.ขาย_สั่งจองและสั่งซื้อสินค้า_ยกเลิก:
                        case _reportEnum.ขาย_สั่งจองและสั่งซื้อสินค้า_สถานะ:
                        case _reportEnum.ขาย_สั่งจองและสั่งซื้อสินค้า_อนุมัติ:
                            __extraAs.Append("doc_time as \"ic_trans.doc_time\",");
                            __extraAs.Append("(select name_1 from ap_supplier where ap_supplier.code=cust_code) ||\'(\'||cust_code||\')\' as \"ic_trans.ap_name\",");
                            __extraAs.Append("department_code as \"ic_trans.department_code\",");
                            __extraAs.Append("doc_group as \"ic_trans.doc_group\",");
                            __extraAs.Append("user_request as \"ic_trans.user_request\",");
                            __extraAs.Append("approve_code as \"ic_trans.approve_code\",");
                            __extraAs.Append("inquiry_type as \"ic_trans.inquiry_type\",");
                            __extraAs.Append("user_approve as \"ic_trans.user_approve\",");
                            __extraAs.Append("total_before_vat as \"ic_trans.total_before_vat\",");
                            //
                            __extraField.Append("doc_time,cust_code,department_code,doc_group,user_request,approve_code,inquiry_type,user_approve,");
                            break;
                        case _reportEnum.ขาย_สั่งขาย:
                        case _reportEnum.ขาย_สั่งขาย_ยกเลิก:
                        case _reportEnum.ขาย_สั่งขาย_สถานะ:
                        case _reportEnum.ขาย_สั่งขาย_อนุมัติ:
                            __extraAs.Append("doc_time as \"ic_trans.doc_time\",");
                            __extraAs.Append("(select name_1 from ap_supplier where ap_supplier.code=cust_code) ||\'(\'||cust_code||\')\' as \"ic_trans.ap_name\",");
                            __extraAs.Append("department_code as \"ic_trans.department_code\",");
                            __extraAs.Append("doc_group as \"ic_trans.doc_group\",");
                            __extraAs.Append("user_request as \"ic_trans.user_request\",");
                            __extraAs.Append("approve_code as \"ic_trans.approve_code\",");
                            __extraAs.Append("inquiry_type as \"ic_trans.inquiry_type\",");
                            __extraAs.Append("user_approve as \"ic_trans.user_approve\",");
                            __extraAs.Append("total_before_vat as \"ic_trans.total_before_vat\",");
                            //
                            __extraField.Append("doc_time,cust_code,department_code,doc_group,user_request,approve_code,inquiry_type,user_approve,");
                            break;
                        case _reportEnum.ลูกหนี้_ตั้งหนี้ยกมา:
                        case _reportEnum.ลูกหนี้_เพิ่มหนี้ยกมา:
                        case _reportEnum.ลูกหนี้_ลดหนี้ยกมา:
                        case _reportEnum.เจ้าหนี้_ตั้งหนี้ยกมา:
                        case _reportEnum.เจ้าหนี้_เพิ่มหนี้ยกมา:
                        case _reportEnum.เจ้าหนี้_ลดหนี้ยกมา:
                        case _reportEnum.เจ้าหนี้_ตั้งหนี้อื่น:
                        case _reportEnum.เจ้าหนี้_เพิ่มหนี้อื่น:
                        case _reportEnum.เจ้าหนี้_ลดหนี้อื่น:
                        case _reportEnum.ซื้อ_จ่ายเงินล่วงหน้า_ยกเลิก:
                        case _reportEnum.ซื้อ_จ่ายเงินล่วงหน้า_รับคืน_ยกเลิก:
                        case _reportEnum.ซื้อ_จ่ายเงินมัดจำ_ยกเลิก:
                        case _reportEnum.ซื้อ_จ่ายเงินมัดจำ_รับคืน_ยกเลิก:
                        case _reportEnum.ขาย_รับเงินล่วงหน้า_ยกเลิก:
                        case _reportEnum.ขาย_รับเงินล่วงหน้า_คืน_ยกเลิก:
                            __extraAs.Append("cust_code as \"ic_trans.cust_code\",");
                            __extraAs.Append("cust_name as \"ic_trans.cust_name\",");
                            if (SMLERPReportTool._global._reportType(this._mode) == SMLERPReportTool._reportTypeEnum.เจ้าหนี้ || SMLERPReportTool._global._reportType(this._mode) == SMLERPReportTool._reportTypeEnum.ซื้อ_เจ้าหนี้)
                            {
                                __extraField.Append("cust_code,(select name_1 from ap_supplier where ic_trans.cust_code=ap_supplier.code) as cust_name,");
                            }
                            else
                            {
                                __extraField.Append("cust_code,(select name_1 from ar_customer where ic_trans.cust_code=ar_customer.code) as cust_name,");
                            }
                            __getWhereMain = this._form_condition._grid._createWhere(_g.d.ic_trans._table + "." + _g.d.ic_trans._cust_code);
                            break;
                        case _reportEnum.ขาย_รับเงินล่วงหน้า_รายงานตัดเงิน:
                        case _reportEnum.ขาย_รับเงินล่วงหน้า_รายงานยอดคงเหลือ:
                        case _reportEnum.ขาย_รับเงินมัดจำ_รายงานตัดเงิน:
                        case _reportEnum.ขาย_รับเงินมัดจำ_รายงานยอดคงเหลือ:
                            {
                                __extraAs.Append("cust_code as \"ic_trans.cust_code\",");
                                __extraAs.Append("cust_name as \"ic_trans.cust_name\",");
                                __extraAs.Append("deposit_buy2 as \"ic_trans.deposit_buy2\",");
                                __extraAs.Append("sum_used as \"ic_trans.sum_used\",");
                                __extraAs.Append("total_amount-(deposit_buy2+sum_used) as \"ic_trans.balance_amount\",");
                                if (SMLERPReportTool._global._reportType(this._mode) == SMLERPReportTool._reportTypeEnum.ซื้อ_เจ้าหนี้)
                                {
                                    __extraField.Append("cust_code,(select name_1 from ap_supplier where ic_trans.cust_code=ap_supplier.code) as cust_name,");
                                }
                                else
                                {
                                    __extraField.Append("cust_code,(select name_1 from ar_customer where ic_trans.cust_code=ar_customer.code) as cust_name,");
                                }
                                StringBuilder __queryPay = new StringBuilder();
                                __queryPay.Append("coalesce((select sum(total_amount) from ic_trans as x1 where x1.last_status=0 and x1.doc_ref=ic_trans.doc_no),0) as " + _g.d.ic_trans._deposit_buy2 + ",");
                                __queryPay.Append("coalesce((select sum(amount) from cb_trans_detail as x2 where x2.last_status=0 and x2.trans_number=ic_trans.doc_no),0) as " + _g.d.ic_trans._sum_used + ",");
                                //
                                __extraField.Append(__queryPay.ToString());
                                __getWhereMain = this._form_condition._grid._createWhere(_g.d.ic_trans._table + "." + _g.d.ic_trans._cust_code);
                                __getWhere = this._form_condition._grid._createWhere(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._cust_code);
                            }
                            break;
                        case _reportEnum.ซื้อ_รายงานยอดคงเหลือเงินจ่ายล่วงหน้า:
                        case _reportEnum.ซื้อ_รายงานยอดคงเหลือเงินมัดจำจ่าย:
                        case _reportEnum.ซื้อ_รายงานตัดเงินมัดจำจ่าย:
                        case _reportEnum.ซื้อ_รายงานตัดเงินจ่ายล่วงหน้า:
                            {
                                __extraAs.Append("cust_code as \"ic_trans.cust_code\",");
                                __extraAs.Append("cust_name as \"ic_trans.cust_name\",");
                                __extraAs.Append("deposit_sell2 as \"ic_trans.deposit_sell2\",");
                                __extraAs.Append("sum_used as \"ic_trans.sum_used\",");
                                __extraAs.Append("total_amount-(deposit_sell2+sum_used) as \"ic_trans.balance_amount\",");
                                if (SMLERPReportTool._global._reportType(this._mode) == SMLERPReportTool._reportTypeEnum.ซื้อ_เจ้าหนี้)
                                {
                                    __extraField.Append("cust_code,(select name_1 from ap_supplier where ic_trans.cust_code=ap_supplier.code) as cust_name,");
                                }
                                else
                                {
                                    __extraField.Append("cust_code,(select name_1 from ar_customer where ic_trans.cust_code=ar_customer.code) as cust_name,");
                                }
                                StringBuilder __queryPay = new StringBuilder();
                                __queryPay.Append("coalesce((select sum(total_amount) from ic_trans as x1 where x1.last_status=0 and x1.doc_ref=ic_trans.doc_no),0) as " + _g.d.ic_trans._deposit_sell2 + ",");
                                __queryPay.Append("coalesce((select sum(amount) from cb_trans_detail as x2 where x2.last_status=0 and x2.trans_number=ic_trans.doc_no),0) as " + _g.d.ic_trans._sum_used + ",");
                                //
                                __extraField.Append(__queryPay.ToString());
                                __getWhereMain = this._form_condition._grid._createWhere(_g.d.ic_trans._table + "." + _g.d.ic_trans._cust_code);
                                __getWhere = this._form_condition._grid._createWhere(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._cust_code);
                            }
                            break;
                        case _reportEnum.ขาย_รับเงินล่วงหน้า:
                        case _reportEnum.ขาย_รับเงินล่วงหน้า_คืน:
                        case _reportEnum.ขาย_รับเงินมัดจำ:
                        case _reportEnum.ขาย_รับเงินมัดจำ_คืน:
                        case _reportEnum.ซื้อ_จ่ายเงินล่วงหน้า_รับคืน:
                        case _reportEnum.ซื้อ_จ่ายเงินล่วงหน้า:
                        case _reportEnum.ซื้อ_จ่ายเงินมัดจำ:
                        case _reportEnum.ซื้อ_จ่ายเงินมัดจำ_รับคืน:
                            {
                                __extraAs.Append("cust_code as \"ic_trans.cust_code\",");
                                __extraAs.Append("cust_name as \"ic_trans.cust_name\",");
                                __extraAs.Append("sum_pay_money_cash as \"ic_trans.sum_pay_money_cash\",");
                                __extraAs.Append("sum_pay_money_chq as \"ic_trans.sum_pay_money_chq\",");
                                __extraAs.Append("sum_pay_money_credit as \"ic_trans.sum_pay_money_credit\",");
                                __extraAs.Append("sum_pay_money_transfer as \"ic_trans.sum_pay_money_transfer\",");
                                __extraAs.Append("sum_petty_cash as \"ic_trans.sum_petty_cash\",");
                                __extraAs.Append("sum_pay_rounding as \"ic_trans.sum_pay_rounding\",");
                                __extraAs.Append("sum_advance as \"ic_trans.sum_advance\",");
                                __extraAs.Append("sum_deposit as \"ic_trans.sum_deposit\",");
                                __extraAs.Append("sum_pay_money_cash+sum_petty_cash+sum_pay_money_chq+sum_pay_rounding+sum_pay_money_credit+sum_pay_money_transfer+sum_advance+sum_deposit as \"ic_trans.sum_total\",");
                                if (SMLERPReportTool._global._reportType(this._mode) == SMLERPReportTool._reportTypeEnum.ซื้อ_เจ้าหนี้)
                                {
                                    __extraField.Append("cust_code,(select name_1 from ap_supplier where ic_trans.cust_code=ap_supplier.code) as cust_name,");
                                }
                                else
                                {
                                    __extraField.Append("cust_code,(select name_1 from ar_customer where ic_trans.cust_code=ar_customer.code) as cust_name,");
                                }
                                StringBuilder __queryPay = new StringBuilder();
                                string __queryFromWhere = " from cb_trans where cb_trans.doc_no=ic_trans.doc_no ";
                                __queryPay.Append("coalesce((select sum(cash_amount)" + __queryFromWhere + "),0) as " + _g.d.ic_trans._sum_pay_money_cash + ",");
                                __queryPay.Append("coalesce((select sum(chq_amount)" + __queryFromWhere + "),0) as " + _g.d.ic_trans._sum_pay_money_chq + ",");
                                __queryPay.Append("coalesce((select sum(total_income_amount)" + __queryFromWhere + "),0) as " + _g.d.ic_trans._sum_pay_rounding + ",");
                                __queryPay.Append("coalesce((select sum(card_amount)" + __queryFromWhere + "),0) as " + _g.d.ic_trans._sum_pay_money_credit + ",");
                                __queryPay.Append("coalesce((select sum(tranfer_amount)" + __queryFromWhere + "),0) as " + _g.d.ic_trans._sum_pay_money_transfer + ",");
                                __queryPay.Append("coalesce((select sum(petty_cash_amount)" + __queryFromWhere + "),0) as " + _g.d.ic_trans._sum_petty_cash + ",");
                                __queryPay.Append("coalesce((select sum(advance_amount)" + __queryFromWhere + "),0) as " + _g.d.ic_trans._sum_advance + ",");
                                __queryPay.Append("coalesce((select sum(deposit_amount)" + __queryFromWhere + "),0) as " + _g.d.ic_trans._sum_deposit + ",");
                                //__extraField.Append("sum_pay_money_cash,sum_pay_money_chq,sum_pay_money_credit,sum_pay_money_transfer,");
                                __extraField.Append(__queryPay.ToString());
                                __getWhereMain = this._form_condition._grid._createWhere(_g.d.ic_trans._table + "." + _g.d.ic_trans._cust_code);
                                __getWhere = this._form_condition._grid._createWhere(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._cust_code);
                            }
                            break;
                        case _reportEnum.ขาย_ขายสินค้าและบริการ:
                        case _reportEnum.ขาย_ขายสินค้าและบริการ_รายงานการออกใบกำกับภาษีอย่างเต็ม:
                        case _reportEnum.ขาย_เพิ่มหนี้สินค้าราคาผิด:
                        case _reportEnum.ขาย_รับคืนสินค้า:
                        case _reportEnum.ขาย_ขายสินค้าและบริการ_ยกเลิก:
                        case _reportEnum.ขาย_เพิ่มหนี้สินค้าราคาผิด_ยกเลิก:
                        case _reportEnum.ขาย_รับคืนสินค้า_ยกเลิก:
                        case _reportEnum.ซื้อ_เพิ่มหนี้ราคาผิด:
                        case _reportEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด:
                        case _reportEnum.ซื้อ_ซื้อสินค้าและบริการ:
                        case _reportEnum.ซื้อ_ซื้อสินค้าและบริการ_ยกเลิก:
                        case _reportEnum.ซื้อ_เพิ่มหนี้ราคาผิด_ยกเลิก:
                        case _reportEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด_ยกเลิก:
                            __extraAs.Append("doc_time as \"ic_trans.doc_time\",");
                            __extraAs.Append("cashier_code as \"ic_trans.cashier_code\",");
                            __extraAs.Append("cust_code as \"ic_trans.cust_code\",");
                            __extraAs.Append("cust_name as \"ic_trans.cust_name\",");

                            // toe check ถ้าเป็นเอกสารยกเลิกไม่แสดงยอด
                            if (this._mode == _reportEnum.ขาย_ขายสินค้าและบริการ && __useLastStatus)
                            {
                                __extraAs.Append("case when _def_last_status = 0 then total_value else 0 end as \"ic_trans.total_value\",");
                                __extraAs.Append("case when _def_last_status = 0 then total_discount else 0 end as \"ic_trans.total_discount\",");
                                __extraAs.Append("case when _def_last_status = 0 then total_after_discount else 0 end as \"ic_trans.total_after_discount\",");
                                __extraAs.Append("case when _def_last_status = 0 then total_vat_value else 0 end as \"ic_trans.total_vat_value\",");
                                __extraAs.Append("case when _def_last_status = 0 then total_except_vat else 0 end as \"ic_trans.total_except_vat\",");
                            }
                            else
                            {
                                __extraAs.Append("total_value as \"ic_trans.total_value\",");
                                __extraAs.Append("total_discount as \"ic_trans.total_discount\",");
                                __extraAs.Append("total_after_discount as \"ic_trans.total_after_discount\",");
                                __extraAs.Append("total_vat_value as \"ic_trans.total_vat_value\",");
                                __extraAs.Append("total_except_vat as \"ic_trans.total_except_vat\",");
                            }
                            if (SMLERPReportTool._global._reportType(this._mode) == SMLERPReportTool._reportTypeEnum.ซื้อ_เจ้าหนี้)
                            {
                                __extraField.Append("cust_code,(select name_1 from ap_supplier where ic_trans.cust_code=ap_supplier.code) as cust_name,");
                            }
                            else
                            {
                                __extraField.Append("cust_code,(select name_1 from ar_customer where ic_trans.cust_code=ar_customer.code) as cust_name,");
                            }
                            __extraField.Append("total_value,total_discount,total_value - total_discount as total_after_discount,total_vat_value,total_except_vat,");
                            __getWhereMain = this._form_condition._grid._createWhere(_g.d.ic_trans._table + "." + _g.d.ic_trans._cust_code);
                            __getWhere = this._form_condition._grid._createWhere(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._cust_code);
                            break;
                        case _reportEnum.ซื้อ_รายงานค้างรับตามใบสั่งซื้อ:
                        case _reportEnum.ซื้อ_รายงานใบสั่งซื้อ:
                        case _reportEnum.ซื้อ_รายงานยกเลิกใบสั่งซื้อ:
                        case _reportEnum.ซื้อ_รายงานอนุมัติใบสั่งซื้อ:
                        case _reportEnum.ซื้อ_รายงานสถานะใบสั่งซื้อ:
                            __extraAs.Append("cust_code as \"ic_trans.cust_code\",");
                            __extraAs.Append("cust_name as \"ic_trans.cust_name\",");
                            __extraAs.Append("total_value as \"ic_trans.total_value\",");
                            __extraAs.Append("total_discount as \"ic_trans.total_discount\",");
                            __extraAs.Append("total_after_discount as \"ic_trans.total_after_discount\",");
                            __extraAs.Append("total_vat_value as \"ic_trans.total_vat_value\",");
                            __extraAs.Append("user_request as \"ic_trans.user_request\",");
                            __extraAs.Append("approve_code as \"ic_trans.approve_code\",");
                            __extraAs.Append("inquiry_type as \"ic_trans.inquiry_type\",");
                            __extraAs.Append("user_approve as \"ic_trans.user_approve\",");
                            __extraAs.Append("total_except_vat as \"ic_trans.total_except_vat\",");
                            //
                            __extraField.Append("cust_code,(select name_1 from ap_supplier where ic_trans.cust_code=ap_supplier.code) as cust_name,");
                            __extraField.Append("total_value,total_discount,total_value - total_discount as total_after_discount,total_vat_value,total_except_vat,user_request,approve_code,inquiry_type,user_approve,");
                            __getWhereMain = this._form_condition._grid._createWhere(_g.d.ic_trans._table + "." + _g.d.ic_trans._cust_code);
                            __getWhere = this._form_condition._grid._createWhere(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._cust_code);
                            break;
                        case _reportEnum.ซื้อ_ใบเสนอซื้อ:
                        case _reportEnum.ซื้อ_ใบเสนอซื้อ_ยกเลิก:
                        case _reportEnum.ซื้อ_ใบเสนอซื้อ_อนุมัติ:
                        case _reportEnum.ซื้อ_ใบเสนอซื้อ_สถานะ:
                            __extraAs.Append("doc_time as \"ic_trans.doc_time\",");
                            __extraAs.Append("(select name_1 from ap_supplier where ap_supplier.code=cust_code) ||\'(\'||cust_code||\')\' as \"ic_trans.ap_name\",");
                            __extraAs.Append("department_code as \"ic_trans.department_code\",");
                            __extraAs.Append("doc_group as \"ic_trans.doc_group\",");
                            __extraAs.Append("user_request as \"ic_trans.user_request\",");
                            __extraAs.Append("approve_code as \"ic_trans.approve_code\",");
                            __extraAs.Append("inquiry_type as \"ic_trans.inquiry_type\",");
                            __extraAs.Append("user_approve as \"ic_trans.user_approve\",");
                            __extraAs.Append("total_before_vat as \"ic_trans.total_before_vat\",");
                            //
                            __extraField.Append("doc_time,cust_code,department_code,doc_group,user_request,approve_code,inquiry_type,user_approve,");
                            break;
                        default:
                            __getWhere = this._form_condition._grid._createWhere(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code);
                            __getAddtionalWhere = this._form_condition._grid._createWhere(_g.d.ic_trans_serial_number._table + "." + _g.d.ic_trans_serial_number._ic_code);
                            break;
                    }
                    if (__getWhereMain.Length > 0) __getWhereMain = " and (" + __getWhereMain + ")";
                    if (__getWhere.Length > 0) __getWhere = " and (" + __getWhere + ")";

                    if (__getAddtionalWhere.Length > 0) __getAddtionalWhere = " and (" + __getAddtionalWhere + ")";

                    // toe เพิ่ม where ตัวบิลขายที่มีการเอาไปอ้างอิง ทำใบกำกับภาษีอย่างเต็มออก
                    if (this._mode == _reportEnum.ขาย_ขายสินค้าและบริการ)
                    {
                        __where = " and (doc_ref is null or doc_ref = \'\') ";
                    }
                    else if (this._mode == _reportEnum.ขาย_ขายสินค้าและบริการ_รายงานการออกใบกำกับภาษีอย่างเต็ม)
                    {
                        __where = " and doc_ref is not null and doc_ref != \'\' and is_pos = 1";
                    }

                    //
                    StringBuilder __query = new StringBuilder();
                    __query.Append("select " + __extraAs.ToString());
                    __query.Append("doc_date as \"ic_trans.doc_date\",");
                    __query.Append("doc_no as \"ic_trans.doc_no\",");
                    __query.Append("doc_ref_date as \"ic_trans.doc_ref_date\",");

                    if (this._mode == _reportEnum.ขาย_ขายสินค้าและบริการ && __useLastStatus)
                    {
                        __query.Append("case when _def_last_status = 0 then doc_ref else 'ยกเลิก' end as\"ic_trans.doc_ref\",");
                        __query.Append("case when _def_last_status = 0 then total_amount else 0 end as \"ic_trans.total_amount\",");
                    }
                    else
                    {
                        __query.Append("doc_ref as \"ic_trans.doc_ref\",");
                        __query.Append("total_amount as \"ic_trans.total_amount\",");
                    }
                    __query.Append("vat_type as \"ic_trans.vat_type\",");

                    if (this._mode == _reportEnum.ขาย_ขายสินค้าและบริการ && __useLastStatus)
                    {
                        __query.Append("case when _def_last_status = 0 then vat_rate else 0 end as \"ic_trans.vat_rate\",");
                    }
                    else
                    {
                        __query.Append("vat_rate as \"ic_trans.vat_rate\",");
                    }

                    __query.Append("last_status as \"ic_trans.last_status\"");
                    __query.Append(" from (select ");
                    __query.Append(__extraField.ToString() + "doc_date,doc_no,doc_time,cashier_code,doc_ref_date,doc_ref,total_amount,total_before_vat,vat_type,vat_rate,last_status as _def_last_status,");
                    __query.Append("cast(last_status as varchar)||','||cast(used_status as varchar)||','||cast(doc_success as varchar)||','||cast(not_approve_1 as varchar)||','||cast(on_hold as varchar)||','||cast(approve_status as varchar)||','||cast(expire_status  as varchar) as last_status");
                    __query.Append(" from ic_trans where trans_flag in(" + _g.g._transFlagGlobal._transFlag(this._transFlag).ToString() + ") " + this._extraWhere + " and ic_trans.doc_date between " + __beginDate + " and " + __endDate + __lastStatusWhere + __where_doc_sucess + __getWhereMain + __where + ") as temp1");
                    __query.Append(this._form_condition._extra._getOrderBy() + ",doc_no");
                    //
                    StringBuilder __extraDetailField = new StringBuilder();
                    StringBuilder __extraDetailAs = new StringBuilder();
                    StringBuilder __whereHouse = new StringBuilder();
                    string __priceField = "";
                    string __sumAmountField = "";
                    string __sumAmountTransField = "";
                    switch (this._mode)
                    {
                        case _reportEnum.ขาย_สั่งจองและสั่งซื้อสินค้า:
                        case _reportEnum.ขาย_สั่งจองและสั่งซื้อสินค้า_ยกเลิก:
                        case _reportEnum.ขาย_สั่งจองและสั่งซื้อสินค้า_สถานะ:
                        case _reportEnum.ขาย_สั่งจองและสั่งซื้อสินค้า_อนุมัติ:
                            __extraDetailAs.Append("discount as \"ic_trans_detail.discount\",");
                            __extraDetailAs.Append("due_date as \"ic_trans_detail.due_date\",");
                            __extraDetailField.Append("discount,due_date,");
                            __priceField = "price";
                            __sumAmountField = "sum_amount";
                            __sumAmountTransField = "sum_amount";
                            if (this._mode == _reportEnum.ขาย_สั่งจองและสั่งซื้อสินค้า_สถานะ)
                            {
                                __extraDetailAs.Append("approval_sum_amount as \"ic_trans_detail.approval_sum_amount\",");
                                __extraDetailAs.Append("approval_price as \"ic_trans_detail.approval_price\",");
                                __extraDetailAs.Append("approval_qty as \"ic_trans_detail.approval_qty\",");
                                __extraDetailAs.Append("approval_discount as \"ic_trans_detail.approval_discount\",");
                                //
                                string __getRefQuery = "coalesce((select {0} from ic_trans_detail as x1 where x1.ref_doc_no=ic_trans_detail.doc_no and x1.ref_line_number=ic_trans_detail.line_number and x1.last_status=0 and x1.trans_flag=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า_อนุมัติ) + "),{1}) as {2},";
                                __extraDetailField.Append(String.Format(__getRefQuery, "qty", "0", "approval_qty"));
                                __extraDetailField.Append(String.Format(__getRefQuery, "price", "0", "approval_price"));
                                __extraDetailField.Append(String.Format(__getRefQuery, "discount", "\'\'", "approval_discount"));
                                __extraDetailField.Append(String.Format(__getRefQuery, "sum_amount", "0", "approval_sum_amount"));
                            }
                            break;
                        case _reportEnum.ขาย_สั่งขาย:
                        case _reportEnum.ขาย_สั่งขาย_ยกเลิก:
                        case _reportEnum.ขาย_สั่งขาย_สถานะ:
                        case _reportEnum.ขาย_สั่งขาย_อนุมัติ:
                            __extraDetailAs.Append("discount as \"ic_trans_detail.discount\",");
                            __extraDetailAs.Append("due_date as \"ic_trans_detail.due_date\",");
                            __extraDetailField.Append("discount,due_date,");
                            __priceField = "price";
                            __sumAmountField = "sum_amount";
                            __sumAmountTransField = "sum_amount";
                            if (this._mode == _reportEnum.ขาย_สั่งขาย_สถานะ)
                            {
                                __extraDetailAs.Append("approval_sum_amount as \"ic_trans_detail.approval_sum_amount\",");
                                __extraDetailAs.Append("approval_price as \"ic_trans_detail.approval_price\",");
                                __extraDetailAs.Append("approval_qty as \"ic_trans_detail.approval_qty\",");
                                __extraDetailAs.Append("approval_discount as \"ic_trans_detail.approval_discount\",");
                                //
                                string __getRefQuery = "coalesce((select {0} from ic_trans_detail as x1 where x1.ref_doc_no=ic_trans_detail.doc_no and x1.ref_line_number=ic_trans_detail.line_number and x1.last_status=0 and x1.trans_flag=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_สั่งขาย_อนุมัติ) + "),{1}) as {2},";
                                __extraDetailField.Append(String.Format(__getRefQuery, "qty", "0", "approval_qty"));
                                __extraDetailField.Append(String.Format(__getRefQuery, "price", "0", "approval_price"));
                                __extraDetailField.Append(String.Format(__getRefQuery, "discount", "\'\'", "approval_discount"));
                                __extraDetailField.Append(String.Format(__getRefQuery, "sum_amount", "0", "approval_sum_amount"));
                            }
                            break;
                        case _reportEnum.สินค้า_รายงานโอนสินค้าและวัตถุดิบ:
                            __whereHouse.Append("wh_code as \"ic_trans_detail.wh_name_out\",");
                            __whereHouse.Append("shelf_code as \"ic_trans_detail.shelf_name_out\",");
                            __whereHouse.Append("wh_code_2 as \"ic_trans_detail.wh_name_in\",");
                            __whereHouse.Append("shelf_code_2 as \"ic_trans_detail.shelf_name_in\",");
                            __priceField = "average_cost";
                            __sumAmountField = "sum_of_cost";
                            __sumAmountTransField = "sum_of_cost";
                            break;
                        case _reportEnum.สินค้า_รายงานปรับปรุงสินค้าและวัตถุดิบ_ขาด:
                        case _reportEnum.สินค้า_รายงานปรับปรุงสินค้าและวัตถุดิบ_เกิน:
                        case _reportEnum.สินค้า_รายงานเบิกสินค้าวัตถุดิบ:
                            __whereHouse.Append("wh_code as \"ic_trans_detail.wh_code\",");
                            __whereHouse.Append("shelf_code as \"ic_trans_detail.shelf_code\",");
                            __priceField = "average_cost";
                            __sumAmountField = "sum_of_cost";
                            __sumAmountTransField = "sum_of_cost";
                            break;
                        case _reportEnum.ขาย_ขายสินค้าและบริการ:
                        case _reportEnum.ขาย_ขายสินค้าและบริการ_รายงานการออกใบกำกับภาษีอย่างเต็ม:
                        case _reportEnum.ขาย_เพิ่มหนี้สินค้าราคาผิด:
                        case _reportEnum.ขาย_รับคืนสินค้า:
                        case _reportEnum.ขาย_ขายสินค้าและบริการ_ยกเลิก:
                        case _reportEnum.ขาย_เพิ่มหนี้สินค้าราคาผิด_ยกเลิก:
                        case _reportEnum.ขาย_รับคืนสินค้า_ยกเลิก:
                        case _reportEnum.ซื้อ_เพิ่มหนี้ราคาผิด:
                        case _reportEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด:
                        case _reportEnum.ซื้อ_ซื้อสินค้าและบริการ:
                        case _reportEnum.ซื้อ_รายงานค้างรับตามใบสั่งซื้อ:
                        case _reportEnum.ซื้อ_รายงานใบสั่งซื้อ:
                        case _reportEnum.ซื้อ_รายงานยกเลิกใบสั่งซื้อ:
                        case _reportEnum.ซื้อ_รายงานอนุมัติใบสั่งซื้อ:
                        case _reportEnum.ซื้อ_รายงานสถานะใบสั่งซื้อ:
                        case _reportEnum.ซื้อ_ซื้อสินค้าและบริการ_ยกเลิก:
                        case _reportEnum.ซื้อ_เพิ่มหนี้ราคาผิด_ยกเลิก:
                        case _reportEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด_ยกเลิก:
                            /*columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._price_exclude_vat, _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._price, 8, SMLReport._report._cellType.Number, _g.g._companyProfile._item_price_decimal, FontStyle.Regular));
                            columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._sum_amount_exclude_vat, null, 8, SMLReport._report._cellType.Number, _g.g._companyProfile._item_price_decimal, FontStyle.Regular));
                            columnList.Add(new SMLReport._generateColumnListClass(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._discount_amount, _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._price, 8, SMLReport._report._cellType.Number, _g.g._companyProfile._item_price_decimal, FontStyle.Regular));*/
                            __whereHouse.Append("wh_code as \"ic_trans_detail.wh_code\",");
                            __whereHouse.Append("shelf_code as \"ic_trans_detail.shelf_code\",");
                            __priceField = "price";
                            __sumAmountField = "sum_amount";
                            __sumAmountTransField = "sum_amount";
                            __extraDetailAs.Append("discount as \"ic_trans_detail.discount\",");
                            __extraDetailField.Append("discount,");
                            break;
                        case _reportEnum.ซื้อ_ใบเสนอซื้อ_สถานะ:
                        case _reportEnum.ซื้อ_ใบเสนอซื้อ:
                        case _reportEnum.ซื้อ_ใบเสนอซื้อ_ยกเลิก:
                        case _reportEnum.ซื้อ_ใบเสนอซื้อ_อนุมัติ:
                            __extraDetailAs.Append("discount as \"ic_trans_detail.discount\",");
                            __extraDetailAs.Append("due_date as \"ic_trans_detail.due_date\",");
                            __extraDetailField.Append("discount,due_date,");
                            __priceField = "price";
                            __sumAmountField = "sum_amount";
                            __sumAmountTransField = "sum_amount";
                            if (this._mode == _reportEnum.ซื้อ_ใบเสนอซื้อ_สถานะ)
                            {
                                __extraDetailAs.Append("approval_sum_amount as \"ic_trans_detail.approval_sum_amount\",");
                                __extraDetailAs.Append("approval_price as \"ic_trans_detail.approval_price\",");
                                __extraDetailAs.Append("approval_qty as \"ic_trans_detail.approval_qty\",");
                                __extraDetailAs.Append("approval_discount as \"ic_trans_detail.approval_discount\",");
                                //
                                string __getRefQuery = "coalesce((select {0} from ic_trans_detail as x1 where x1.ref_doc_no=ic_trans_detail.doc_no and x1.ref_line_number=ic_trans_detail.line_number and x1.last_status=0 and x1.trans_flag=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_เสนอซื้อ_อนุมัติ) + "),{1}) as {2},";
                                __extraDetailField.Append(String.Format(__getRefQuery, "qty", "0", "approval_qty"));
                                __extraDetailField.Append(String.Format(__getRefQuery, "price", "0", "approval_price"));
                                __extraDetailField.Append(String.Format(__getRefQuery, "discount", "\'\'", "approval_discount"));
                                __extraDetailField.Append(String.Format(__getRefQuery, "sum_amount", "0", "approval_sum_amount"));
                            }
                            break;
                        case _reportEnum.ขาย_เสนอราคา:
                        case _reportEnum.ขาย_เสนอราคา_ยกเลิก:
                        case _reportEnum.ขาย_เสนอราคา_สถานะ:
                        case _reportEnum.ขาย_เสนอราคา_อนุมัติ:
                            __extraDetailAs.Append("discount as \"ic_trans_detail.discount\",");
                            __extraDetailAs.Append("due_date as \"ic_trans_detail.due_date\",");
                            __extraDetailField.Append("discount,due_date,");
                            __priceField = "price";
                            __sumAmountField = "sum_amount";
                            __sumAmountTransField = "sum_amount";
                            if (this._mode == _reportEnum.ขาย_เสนอราคา_สถานะ)
                            {
                                __extraDetailAs.Append("approval_sum_amount as \"ic_trans_detail.approval_sum_amount\",");
                                __extraDetailAs.Append("approval_price as \"ic_trans_detail.approval_price\",");
                                __extraDetailAs.Append("approval_qty as \"ic_trans_detail.approval_qty\",");
                                __extraDetailAs.Append("approval_discount as \"ic_trans_detail.approval_discount\",");
                                //
                                string __getRefQuery = "coalesce((select {0} from ic_trans_detail as x1 where x1.ref_doc_no=ic_trans_detail.doc_no and x1.ref_line_number=ic_trans_detail.line_number and x1.last_status=0 and x1.trans_flag=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_เสนอราคา_อนุมัติ) + "),{1}) as {2},";
                                __extraDetailField.Append(String.Format(__getRefQuery, "qty", "0", "approval_qty"));
                                __extraDetailField.Append(String.Format(__getRefQuery, "price", "0", "approval_price"));
                                __extraDetailField.Append(String.Format(__getRefQuery, "discount", "\'\'", "approval_discount"));
                                __extraDetailField.Append(String.Format(__getRefQuery, "sum_amount", "0", "approval_sum_amount"));
                            }
                            break;
                        default:
                            __whereHouse.Append("wh_code as \"ic_trans_detail.wh_code\",");
                            __whereHouse.Append("shelf_code as \"ic_trans_detail.shelf_code\",");
                            __priceField = "price";
                            __sumAmountField = "sum_amount";
                            __sumAmountTransField = "sum_amount";
                            break;
                    }

                    // toe 
                    //string __detailWhere = "";
                    //if (this._mode == _reportEnum.ขาย_ขายสินค้าและบริการ)
                    //{
                    //    __detailWhere = " where doc_ref_trans is null";
                    //}

                    StringBuilder __queryDetail = new StringBuilder();
                    switch (this._mode)
                    {
                        case _reportEnum.ซื้อ_รายงานตัดเงินจ่ายล่วงหน้า:
                        case _reportEnum.ซื้อ_รายงานตัดเงินมัดจำจ่าย:
                            {
                                string __transFlag = "";
                                switch (this._mode)
                                {
                                    case _reportEnum.ซื้อ_รายงานตัดเงินจ่ายล่วงหน้า:
                                        __transFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า_รับคืน).ToString();
                                        break;
                                    case _reportEnum.ซื้อ_รายงานตัดเงินมัดจำจ่าย:
                                        __transFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_จ่ายเงินมัดจำ_รับคืน).ToString();
                                        break;
                                }
                                __queryDetail.Append("select ");
                                __queryDetail.Append("trans_flag as \"cb_trans_detail.trans_flag\",");
                                __queryDetail.Append("doc_date as \"cb_trans_detail.doc_date\",");
                                __queryDetail.Append("doc_no as \"cb_trans_detail.doc_no\",");
                                __queryDetail.Append("trans_number as \"cb_trans_detail.trans_number\",");
                                __queryDetail.Append("amount as \"cb_trans_detail.amount\"");
                                __queryDetail.Append(" from ");
                                __queryDetail.Append("(select trans_flag,doc_date,doc_no,trans_number,amount from cb_trans_detail where cb_trans_detail.last_status=0");
                                __queryDetail.Append(" union all ");
                                __queryDetail.Append("select trans_flag,doc_date,doc_no,doc_ref as trans_number,total_amount as amount from ic_trans where ic_trans.trans_flag=" + __transFlag + " and ic_trans.last_status=0) as temp2");
                                __queryDetail.Append(" order by doc_date,doc_no");
                            }
                            break;
                        case _reportEnum.ขาย_รับเงินมัดจำ_รายงานตัดเงิน:
                        case _reportEnum.ขาย_รับเงินล่วงหน้า_รายงานตัดเงิน:
                            {
                                string __transFlag = "";
                                switch (this._mode)
                                {
                                    case _reportEnum.ขาย_รับเงินล่วงหน้า_รายงานตัดเงิน:
                                        __transFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_เงินล่วงหน้า_คืน).ToString();
                                        break;
                                    case _reportEnum.ขาย_รับเงินมัดจำ_รายงานตัดเงิน:
                                        __transFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_เงินมัดจำ_คืน).ToString();
                                        break;
                                }
                                __queryDetail.Append("select ");
                                __queryDetail.Append("trans_flag as \"cb_trans_detail.trans_flag\",");
                                __queryDetail.Append("doc_date as \"cb_trans_detail.doc_date\",");
                                __queryDetail.Append("doc_no as \"cb_trans_detail.doc_no\",");
                                __queryDetail.Append("trans_number as \"cb_trans_detail.trans_number\",");
                                __queryDetail.Append("amount as \"cb_trans_detail.amount\"");
                                __queryDetail.Append(" from ");
                                __queryDetail.Append("(select trans_flag,doc_date,doc_no,trans_number,amount from cb_trans_detail where cb_trans_detail.last_status=0");
                                __queryDetail.Append(" union all ");
                                __queryDetail.Append("select trans_flag,doc_date,doc_no,doc_ref as trans_number,total_amount as amount from ic_trans where ic_trans.trans_flag=" + __transFlag + " and ic_trans.last_status=0) as temp2");
                                __queryDetail.Append(" order by doc_date,doc_no");
                            }
                            break;
                        default:
                            {
                                __queryDetail.Append("select " + __extraDetailAs.ToString());
                                __queryDetail.Append("doc_date as \"ic_trans.doc_date\","); // กำหนดเป็น ic_trans เพื่อใช้สำหรับ datatable select
                                __queryDetail.Append("doc_no as \"ic_trans.doc_no\","); // กำหนดเป็น ic_trans เพื่อใช้สำหรับ datatable select
                                __queryDetail.Append("item_code as \"ic_trans_detail.item_code\",");
                                __queryDetail.Append("barcode as \"ic_trans_detail.barcode\",");
                                __queryDetail.Append("(select name_1 from ic_inventory where ic_inventory.code=item_code) as \"ic_trans_detail.item_name\",");
                                __queryDetail.Append(__whereHouse.ToString());
                                __queryDetail.Append("unit_code||'~'||coalesce((select name_1 from ic_unit where ic_unit.code=unit_code),'') as \"ic_trans_detail.unit_code\",");
                                __queryDetail.Append("qty as \"ic_trans_detail.qty\",");
                                __queryDetail.Append("temp_float_1 as \"ic_trans_detail.temp_float_1\",");
                                __queryDetail.Append("temp_float_2 as \"ic_trans_detail.temp_float_2\",");
                                __queryDetail.Append("vat_type as \"ic_trans_detail.vat_type\",");
                                __queryDetail.Append("ref_row as \"ic_trans_detail.ref_row\",");
                                __queryDetail.Append(__priceField + " as \"ic_trans_detail.price\",");
                                __queryDetail.Append(__sumAmountField + " as \"ic_trans_detail.sum_amount\"");
                                __queryDetail.Append(" from (select " + __extraDetailField.ToString());
                                __queryDetail.Append("doc_date,doc_no,item_code,barcode,wh_code,shelf_code,wh_code_2,shelf_code_2,vat_type,ref_row,unit_code,temp_float_1,temp_float_2,qty,(select ic_trans.doc_ref from ic_trans where ic_trans.doc_no = ic_trans_detail.doc_no) as doc_ref_trans," + __priceField + "," + __sumAmountTransField);
                                __queryDetail.Append(" from ic_trans_detail where trans_flag in(" + _g.g._transFlagGlobal._transFlag(this._transFlag).ToString() + ") and ic_trans_detail.doc_date between " + __beginDate + " and " + __endDate + __lastStatusWhere + __getWhere + ") as temp1 order by doc_date,doc_no");
                            }
                            break;
                    }
                    //

                    string __newQHeader = "select distinct item_code as \"ic_trans_detail.item_code\", coalesce((select name_1 from ic_inventory where code=item_code),'') as \"ic_trans_detail.item_name\" from ic_trans_detail where doc_date between " + __beginDate + " and " + __endDate + __getWhere + " and last_status=0 and trans_flag = 44 order by item_code";
                    //string __newQDetail = "select doc_no as \"ic_trans_detail.doc_no\",doc_date as \"ic_trans_detail.doc_date\",unit_code as \"ic_trans_detail.unit_code\", \'\' as \"ic_trans_serial_number.serial_number\" ,qty as \"ic_trans_detail.qty\",price as \"ic_trans_detail.price\",discount_amount as \"ic_trans_detail.discount_amount\",total_vat_value as \"ic_trans_detail.total_vat_value\",sum_amount as \"ic_trans_detail.sum_amount\",ic_trans_detail.item_code as \"ic_trans_detail.item_code\" from ic_trans_detail where doc_date between " + __beginDate + " and " + __endDate + __getWhere + " and last_status=0 and trans_flag = 44 order by item_code,doc_date,doc_no"; non group query
                    string __newQDetail = "select doc_no as \"ic_trans_detail.doc_no\",doc_date as \"ic_trans_detail.doc_date\",unit_code as \"ic_trans_detail.unit_code\", \'\' as \"ic_trans_serial_number.serial_number\" ,qty as \"ic_trans_detail.qty\",price as \"ic_trans_detail.price\",discount_amount as \"ic_trans_detail.discount_amount\",total_vat_value as \"ic_trans_detail.total_vat_value\",sum_amount as \"ic_trans_detail.sum_amount\", item_code as \"ic_trans_detail.item_code\" from ( select doc_no,doc_date, item_code, unit_code, sum(qty) as qty, sum(price) as price, sum(discount_amount) as discount_amount, sum(total_vat_value) as total_vat_value, sum(sum_amount) as sum_amount from ic_trans_detail where doc_date between " + __beginDate + " and " + __endDate + __getWhere + " and last_status=0 and trans_flag = 44 group by doc_no,doc_date, item_code, unit_code ) as temp1 order by item_code,doc_date,doc_no";
                    string __addtionalQuery = "select serial_number as \"ic_trans_serial_number.serial_number\", ic_code as \"ic_trans_detail.item_code\", doc_no as \"ic_trans_detail.doc_no\" from ic_trans_serial_number where trans_flag = 44 and last_status = 0  and doc_date between " + __beginDate + " and " + __endDate + __getAddtionalWhere + "";

                    string __queryHeadStr = __newQHeader;//__query.ToString();
                    string __queryDetailStr = __newQDetail;//__queryDetail.ToString();



                    this._dataTable = __myFrameWork._queryStream(MyLib._myGlobal._databaseName, __queryHeadStr).Tables[0];
                    this._dataTableDetail = __myFrameWork._queryStream(MyLib._myGlobal._databaseName, __queryDetailStr).Tables[0];// (this._displayDetail) ? __myFrameWork._queryStream(MyLib._myGlobal._databaseName, __queryDetailStr).Tables[0] : null;
                    this._dataAddtional = __myFrameWork._queryStream(MyLib._myGlobal._databaseName, __addtionalQuery).Tables[0];
                }
                catch (Exception __ex)
                {
                    MessageBox.Show(__ex.Message.ToString());
                }
            }
        }

        private void _printNow()
        {
            this._dataTable = null; // จะได้ load data ใหม่
            // แสดงเงื่อนไข
            this._report._conditionTextDetail = "";
            StringBuilder __conditionDetail = new StringBuilder();
            string __beginDate = this._form_condition._screen._getDataStr(_g.d.resource_report._from_date);
            string __endDate = this._form_condition._screen._getDataStr(_g.d.resource_report._to_date);
            int __getVatType = (int)MyLib._myGlobal._decimalPhase(this._form_condition._screen._getDataStr(_g.d.resource_report._vat_display_type));
            switch (__getVatType)
            {
                case 0: this._vatNumberType = _reportVatNumberType.ปรกติ; break;
                case 1: this._vatNumberType = _reportVatNumberType.แสดงตัวเลขแบบแยกนอกทั้งหมด; break;
                case 2: this._vatNumberType = _reportVatNumberType.แสดงตัวเลขแบบรวมในทั้งหมด; break;
            }
            //
            this._report._conditionText = MyLib._myGlobal._resource("จากวันที่") + " : " + __beginDate + "  " + MyLib._myGlobal._resource("ถึงวันที่") + " : " + __endDate + " ";
            // ลูกหนี้
            this._report._conditionText = _g.g._conditionGrid(this._form_condition._grid, this._report._conditionText);
            //
            if (this._vatNumberType != _reportVatNumberType.ปรกติ)
            {
                this._report._conditionTextDetail = this._vatNumberType.ToString() + " ";
            }
            switch (this._mode)
            {
                case _reportEnum.สินค้า_รายงานโอนสินค้าและวัตถุดิบ:
                case _reportEnum.สินค้า_รายงานปรับปรุงสินค้าและวัตถุดิบ_ขาด:
                case _reportEnum.สินค้า_รายงานปรับปรุงสินค้าและวัตถุดิบ_เกิน:
                case _reportEnum.สินค้า_รายงานรับคืนเบิกสินค้าวัตถุดิบ:
                case _reportEnum.สินค้า_รายงานเบิกสินค้าวัตถุดิบ:
                case _reportEnum.สินค้า_รายงานรับสินค้าสำเร็จรูป:
                case _reportEnum.สินค้า_รายงานสินค้าและวัตถุดิบคงเหลือยกมา:
                case _reportEnum.สินค้า_รายงานสินค้าตรวจนับ:
                case _reportEnum.ขาย_ขายสินค้าและบริการ:
                case _reportEnum.ขาย_ขายสินค้าและบริการ_รายงานการออกใบกำกับภาษีอย่างเต็ม:
                case _reportEnum.ขาย_เพิ่มหนี้สินค้าราคาผิด:
                case _reportEnum.ขาย_รับคืนสินค้า:
                case _reportEnum.ขาย_ขายสินค้าและบริการ_ยกเลิก:
                case _reportEnum.ขาย_เพิ่มหนี้สินค้าราคาผิด_ยกเลิก:
                case _reportEnum.ขาย_รับคืนสินค้า_ยกเลิก:
                case _reportEnum.ขาย_สั่งจองและสั่งซื้อสินค้า:
                case _reportEnum.ขาย_สั่งจองและสั่งซื้อสินค้า_สถานะ:
                case _reportEnum.ขาย_สั่งขาย:
                case _reportEnum.ขาย_สั่งขาย_สถานะ:
                case _reportEnum.ขาย_เสนอราคา:
                case _reportEnum.ขาย_เสนอราคา_สถานะ:
                case _reportEnum.ซื้อ_เพิ่มหนี้ราคาผิด:
                case _reportEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด:
                case _reportEnum.ซื้อ_ซื้อสินค้าและบริการ:
                case _reportEnum.ซื้อ_ซื้อสินค้าและบริการ_ยกเลิก:
                case _reportEnum.ซื้อ_เพิ่มหนี้ราคาผิด_ยกเลิก:
                case _reportEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด_ยกเลิก:
                case _reportEnum.ซื้อ_รายงานใบสั่งซื้อ:
                case _reportEnum.ซื้อ_รายงานค้างรับตามใบสั่งซื้อ:
                case _reportEnum.ซื้อ_รายงานยกเลิกใบสั่งซื้อ:
                case _reportEnum.ซื้อ_รายงานอนุมัติใบสั่งซื้อ:
                case _reportEnum.ซื้อ_รายงานสถานะใบสั่งซื้อ:
                case _reportEnum.ซื้อ_ใบเสนอซื้อ_สถานะ:
                case _reportEnum.ซื้อ_ใบเสนอซื้อ:
                    this._report._conditionTextDetail = this._report._conditionTextDetail + MyLib._myGlobal._resource(__conditionDetail.Append((this._form_condition._screen._getDataStr(_g.d.resource_report._show_cancel_document).ToString().Equals("1")) ? MyLib._myGlobal._resource("แสดงรายการที่ถูกยกเลิก") : " ").ToString());
                    break;
            }
            //
            this._report._build();
        }

        private void _showCondition()
        {
            if (this._form_condition == null)
            {
                this._form_condition = new SMLERPReportTool._conditionScreen(this._mode, this._screenName);
                this._report__init();
                this._form_condition._extra._tableName = _g.d.ic_trans._table;
                string[] __fieldList = this._report._level.__fieldList(false).Split(',');
                this._form_condition._extra._addFieldComboBox(__fieldList);
                //this._form_condition._title = this._screenName;
            }
            this._form_condition.ShowDialog();
            if (this._form_condition._processClick)
            {
                this._printNow();
            }
        }

    }
}
