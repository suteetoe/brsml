using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SMLERPControl._bank
{
    public class _chqMovement : MyLib._myGrid
    {
        public _chqMovement()
        {
            this._beforeDisplayRow += _chqMovement__beforeDisplayRow;
        }

        MyLib.BeforeDisplayRowReturn _chqMovement__beforeDisplayRow(MyLib._myGrid sender, int row, int columnNumber, string columnName, MyLib.BeforeDisplayRowReturn senderRow, MyLib._myGrid._columnType columnType, System.Collections.ArrayList rowData)
        {
            int __columnDocType = this._findColumnByName(_g.d.cb_resource._doc_type);
            if (columnNumber == __columnDocType)
            {
                int __transNumber = MyLib._myGlobal._intPhase(rowData[__columnDocType].ToString());
                ((System.Collections.ArrayList)senderRow.newData)[__columnDocType] = _g.g._transFlagGlobal._transName(__transNumber);
            }
            return senderRow;
        }

        private void _build()
        {
            if (this._chqListControlType != _chqListControlTypeEnum.ว่าง)
            {
                switch (this._chqListControlType)
                {
                    case _chqListControlTypeEnum.ทะเบียนเช็ครับ:

                        this._isEdit = false;
                        this._table_name = _g.d.cb_resource._table;
                        this._addColumn(_g.d.cb_resource._doc_date, 4, 10, 25);
                        this._addColumn(_g.d.cb_resource._doc_time, 1, 5, 15);
                        this._addColumn(_g.d.cb_resource._doc_no, 1, 10, 30);
                        this._addColumn(_g.d.cb_resource._doc_type, 1, 10, 25);
                        //this._addColumn(_g.d.cb_resource._ap_name, 1, 5, 25);
                        //this._addColumn(_g.d.cb_resource._pass_book_code, 1, 20, 15);
                        //this._addColumn(_g.d.cb_resource._bank_code, 1, 5, 5);
                        //this._addColumn(_g.d.cb_resource._bank_branch, 1, 5, 5);
                        this._addColumn(_g.d.cb_resource._amount_balance, 3, 1, 10, true, false, true, false, _g.g._getFormatNumberStr(2), "", "", _g.d.cb_resource._amount_2);
                        this._addColumn(_g.d.cb_resource._remark, 1, 20, 20);
                        this._calcPersentWidthToScatter();
                        break;
                    case _chqListControlTypeEnum.ทะเบียนเช็คจ่าย:

                        this._isEdit = false;
                        this._table_name = _g.d.cb_resource._table;
                        this._addColumn(_g.d.cb_resource._doc_date, 4, 10, 25);
                        this._addColumn(_g.d.cb_resource._doc_time, 1, 5, 15);
                        this._addColumn(_g.d.cb_resource._doc_no, 1, 10, 30);
                        this._addColumn(_g.d.cb_resource._doc_type, 1, 10, 25);
                        //this._addColumn(_g.d.cb_resource._ar_name, 1, 5, 25);
                        //this._addColumn(_g.d.cb_resource._pass_book_code, 1, 20, 15);
                        //this._addColumn(_g.d.cb_resource._bank_code, 1, 5, 5);
                        //this._addColumn(_g.d.cb_resource._bank_branch, 1, 5, 5);
                        //this._addColumn(_g.d.cb_resource._sum_amount, 3, 1, 10, true, false, true, false, _g.g._getFormatNumberStr(2));
                        this._addColumn(_g.d.cb_resource._remark, 1, 20, 20);
                        this._calcPersentWidthToScatter();
                        break;
                    case _chqListControlTypeEnum.ทะเบียนบัตรเครดิต:
                        {
                            this._isEdit = false;
                            this._table_name = _g.d.cb_resource._table;
                            this._addColumn(_g.d.cb_resource._doc_date, 4, 10, 25);
                            this._addColumn(_g.d.cb_resource._doc_time, 1, 5, 15);
                            this._addColumn(_g.d.cb_resource._doc_no, 1, 10, 30);
                            this._addColumn(_g.d.cb_resource._doc_type, 1, 10, 25);
                            //this._addColumn(_g.d.cb_resource._ar_name, 1, 5, 25);
                            //this._addColumn(_g.d.cb_resource._pass_book_code, 1, 20, 15);
                            //this._addColumn(_g.d.cb_resource._bank_code, 1, 5, 5);
                            //this._addColumn(_g.d.cb_resource._bank_branch, 1, 5, 5);
                            //this._addColumn(_g.d.cb_resource._sum_amount, 3, 1, 10, true, false, true, false, _g.g._getFormatNumberStr(2));
                            this._addColumn(_g.d.cb_resource._remark, 1, 20, 20);
                            this._calcPersentWidthToScatter();
                        }
                        break;
                }
            }
        }

        private _chqListControlTypeEnum _chqListControlTypeTemp = _chqListControlTypeEnum.ว่าง;

        public _chqListControlTypeEnum _chqListControlType
        {
            set
            {
                this._chqListControlTypeTemp = value;
                this._build();
                this.Invalidate();
            }
            get
            {
                return this._chqListControlTypeTemp;
            }
        }

        public void _load(string chqNumber, string refdocNo, int docLineNumber)
        {
            MyLib._myFrameWork myFrameWork = new MyLib._myFrameWork();

            string __inFlag = "";
            string __docType = "";

            string __processFlag = "";

            // inflag 
            if (this._chqListControlType == _chqListControlTypeEnum.ทะเบียนเช็ครับ || this._chqListControlType == _chqListControlTypeEnum.ทะเบียนบัตรเครดิต)
            {
                __inFlag = MyLib._myGlobal._fieldAndComma(
                    // เช็คยกมา
                    _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ยกมา).ToString()
                    , _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_เปลี่ยนเช็ค).ToString()
                    // ขาย
                    , _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_รับเงินล่วงหน้า).ToString()
                    , _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_รับเงินมัดจำ).ToString()
                    , _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ).ToString()
                    // รับชำระหนี้
                    , _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ลูกหนี้_รับชำระหนี้).ToString()
                    , _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น).ToString()
                    , _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_เพิ่มหนี้).ToString()
                   );

            }
            else if (this._chqListControlType == _chqListControlTypeEnum.ทะเบียนเช็คจ่าย)
            {
                __inFlag = MyLib._myGlobal._fieldAndComma(
                    _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ยกมา).ToString()
                    , _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_เปลี่ยนเช็ค).ToString()

                    , _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_จ่ายเงินมัดจำ).ToString()
                    , _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า).ToString()
                    , _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ).ToString()

                    , _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เจ้าหนี้_จ่ายชำระหนี้).ToString()
                    , _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น).ToString()
                    , _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_เพิ่มหนี้).ToString()

                    , _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_ลดหนี้).ToString()
                    , _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดย่อย_เบิกเงินสดย่อย).ToString()
                    , _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_ฝากเงิน).ToString()
                    );

            }

            // process flag

            if (this._chqListControlType == _chqListControlTypeEnum.ทะเบียนเช็ครับ)
            {
                __docType = "2";
                __processFlag = MyLib._myGlobal._fieldAndComma(_g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_เข้าใหม่).ToString(),
                    _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_คืน).ToString(),
                    _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ผ่าน).ToString(),
                    _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ฝาก).ToString(),
                    _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ยกเลิก).ToString(),
                    _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_เปลี่ยนเช็ค).ToString());
                //,
                //_g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_ยกมา).ToString());
            }
            else if (this._chqListControlType == _chqListControlTypeEnum.ทะเบียนเช็คจ่าย)
            {
                __docType = "2";
                __processFlag = MyLib._myGlobal._fieldAndComma(_g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ผ่าน).ToString(),
                    _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_คืน).ToString(),
                    _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ยกเลิก).ToString(),
                    _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_เปลี่ยนเช็ค).ToString());
                //_g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_ยกมา).ToString(),
            }
            else if (this._chqListControlType == _chqListControlTypeEnum.ทะเบียนบัตรเครดิต)
            {
                __docType = "3";
                __processFlag = MyLib._myGlobal._fieldAndComma(_g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_บัตรเครดิต_ผ่าน).ToString(),
                    _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_บัตรเครดิต_ยกเลิก).ToString());
            }

            //__myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
            //__myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.cb_chq_list._table + whereString + " and " + _g.d.cb_chq_list._chq_type + " = " + _chqListGlobal._chqListType(chqListControlType).ToString()));

            // chq in              
            string __chqInField = MyLib._myGlobal._fieldAndComma(
                _g.d.cb_trans_detail._doc_date + " as " + _g.d.cb_resource._doc_date,
                _g.d.cb_trans_detail._doc_time + " as " + _g.d.cb_resource._doc_time,
                _g.d.cb_trans_detail._doc_no + " as " + _g.d.cb_resource._doc_no,
                _g.d.cb_trans_detail._trans_flag + " as " + _g.d.cb_resource._doc_type,
                _g.d.cb_trans_detail._remark + " as " + _g.d.cb_resource._remark,
                _g.d.cb_trans_detail._amount + " as " + _g.d.cb_resource._amount_balance);
            // + _g.d.ic_trans_detail._trans_flag + " in (" + __chqInFlag + ") "

            String __extraChqFilter = "";
            if (refdocNo != null && refdocNo.Length > 0)
            {
                __extraChqFilter = " and " + _g.d.cb_trans_detail._doc_no + "=\'" + refdocNo + "\' and coalesce(" + _g.d.cb_trans_detail._line_number + ", 0)=" + docLineNumber;
            }

            string __queryIn = "select " + __chqInField +
                " from " + _g.d.cb_trans_detail._table +
                " where " + _g.d.cb_trans_detail._trans_number + "=\'" + chqNumber + "\' and " + _g.d.cb_trans_detail._doc_type + "=" + __docType + __extraChqFilter + " and " + _g.d.cb_trans_detail._trans_flag + " in (" + __inFlag + ")";

            // chq transection
            string __chqProcessField = MyLib._myGlobal._fieldAndComma(
                _g.d.ic_trans_detail._doc_date + " as " + _g.d.cb_resource._doc_date,
                _g.d.ic_trans_detail._doc_time + " as " + _g.d.cb_resource._doc_time,
                _g.d.ic_trans_detail._doc_no + " as " + _g.d.cb_resource._doc_no,
                _g.d.ic_trans_detail._trans_flag + " as " + _g.d.cb_resource._doc_type,
                _g.d.ic_trans_detail._remark + " as " + _g.d.cb_resource._remark,
                _g.d.ic_trans_detail._sum_amount + " as " + _g.d.cb_resource._amount_balance);

            string __extraChqProcessFilter = "";
            if (refdocNo != null && refdocNo.Length > 0)
            {
                __extraChqProcessFilter = " and " + _g.d.ic_trans_detail._doc_ref + "=\'" + refdocNo + "\' and " + _g.d.ic_trans_detail._ref_row + "=" + docLineNumber;
            }


            string __queryProcess = "select " + __chqProcessField + " from " + _g.d.ic_trans_detail._table + " where " + _g.d.ic_trans_detail._trans_flag + " in (" + __processFlag + ") and " + _g.d.ic_trans_detail._chq_number + "=\'" + chqNumber + "\' " + __extraChqProcessFilter;

            StringBuilder __myquery = new StringBuilder();
            __myquery.Append("select " + MyLib._myGlobal._fieldAndComma(_g.d.cb_resource._doc_date, _g.d.cb_resource._doc_time, _g.d.cb_resource._doc_no, _g.d.cb_resource._doc_type, _g.d.cb_resource._remark, _g.d.cb_resource._amount_balance) + " from ( ");
            __myquery.Append(__queryIn);

            __myquery.Append(" union all ");

            // chq on hand
            __myquery.Append("select " + __chqInField +
                " from " + _g.d.cb_trans_detail._table +
                " where " + _g.d.cb_trans_detail._trans_number + "=\'" + chqNumber + "\' and " + _g.d.cb_trans_detail._doc_type + "=" + __docType + " and chq_on_hand =1  and " + _g.d.cb_trans_detail._trans_flag + " in (" + __inFlag + ")");


            __myquery.Append(" union all ");
            __myquery.Append(__queryProcess);
            __myquery.Append(" ) as temp1 ");
            __myquery.Append("  order by " + _g.d.cb_resource._doc_date + "," + _g.d.cb_resource._doc_time);

            DataSet __result = myFrameWork._query(MyLib._myGlobal._databaseName, __myquery.ToString());

            if (__result.Tables.Count > 0)
            {
                this._loadFromDataTable(__result.Tables[0]);
            }

            //__myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__queryChqIn));
        }
    }
}
