using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Diagnostics;

namespace SMLERPAPARControl
{
    public class _ar_ap_trans_grid : MyLib._myGrid
    {
        private _g.g._transControlTypeEnum _controlTypeTemp;
        MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
        ArrayList _search_data_full_buffer = new ArrayList();
        ArrayList _search_data_full_buffer_name = new ArrayList();
        public _ar_ap_trans _screen;
        public _selectBillForm _selectBill;
        public string _lastCustCode = "";
        public DateTime _lastDateTime = new DateTime();
        public event _getCustCodeEventHandler _getCustCode;
        public event _getProcessDateEventHandler _getProcessDate;
        /*
                1=สั่งซื้อ (PO) , 2=สั่งขาย (SO) , 3=คลัง(IC), 4=เจ้าหนี้ (AP) , 5=ลูกหนี้ (AR)

                ซื้อสินค้า   	12/1
                ซื้อเพิ่ม/เพิ่มหนี้	14/1	
                ส่งคืนสินค้า/ลดหนี้	16/1
                ตั้งหนี้ยกมา	81/4
                เพิ่มหนี้ยกมา	83/4
                ลดหนี้ยกมา	85/4


                รับชำระหนี้ อ้างเอกสารได้คือ
                ขายสินค้า		44/2
                ขายเพิ่ม/เพิ่มหนี้	46/2	
                รับคืน/ลดหนี้	48/2
                ตั้งหนี้ยกมา	93/5
                เพิ่มหนี้ยกมา	95/5
                ลดหนี้ยกมา	97/5
        */

        public _g.g._transControlTypeEnum _transControlType
        {
            set
            {
                this._controlTypeTemp = value;
                this._build();
                this.Invalidate();
            }
            get
            {
                return this._controlTypeTemp;
            }
        }

        public _ar_ap_trans_grid()
        {
            // this._build();
        }

        int _buildCount = 0;
        void _build()
        {
            if (this._controlTypeTemp == _g.g._transControlTypeEnum.ว่าง)
            {
                return;
            }
            this._buildCount++;
            if (this._buildCount > 1)
            {
                MessageBox.Show("_ar_ap_trans_grid : มีการสร้างจอสองครั้ง");
            }
            this._columnList.Clear();
            this._table_name = _g.d.ap_ar_trans_detail._table;
            string __formatNumber = _g.g._getFormatNumberStr(3);
            this.SuspendLayout();
            switch (this._transControlType)
            {
                case _g.g._transControlTypeEnum.เจ้าหนี้_ใบรับวางบิล:
                    this._addColumn(_g.d.ap_ar_trans_detail._billing_no, 1, 1, 10, true, false, true, true);
                    this._addColumn(_g.d.ap_ar_trans_detail._billing_date, 4, 1, 15, false, false, true);
                    this._addColumn(_g.d.ap_ar_trans_detail._bill_type_name, 1, 1, 10, false, false, false, true);
                    this._addColumn(_g.d.ap_ar_trans_detail._ref_doc_no, 1, 1, 10, false, false, true, false);
                    this._addColumn(_g.d.ap_ar_trans_detail._ref_doc_date, 4, 1, 15, false, false, true);
                    if (MyLib._myGlobal._OEMVersion.Equals("SINGHA"))
                    {
                        this._addColumn(_g.d.ap_ar_trans_detail._bill_tax_no, 1, 1, 10, false, false, true, false);
                        this._addColumn(_g.d.ap_ar_trans_detail._bill_tax_date, 4, 1, 15, false, false, true);
                    }
                    this._addColumn(_g.d.ap_ar_trans_detail._due_date, 4, 1, 15, false, false, true);
                    this._addColumn(_g.d.ap_ar_trans_detail._sum_debt_amount, 3, 1, 10, false, false, true, false, __formatNumber);
                    this._addColumn(_g.d.ap_ar_trans_detail._balance_ref, 3, 1, 8, false, false, true, false, __formatNumber);
                    this._addColumn(_g.d.ap_ar_trans_detail._sum_pay_money, 3, 1, 10, true, false, true, false, __formatNumber, "", "", _g.d.ap_ar_trans_detail._sum_pay_money_1);
                    this._addColumn(_g.d.ap_ar_trans_detail._doc_ref, 1, 0, 8, false, true, true, true, "", "", "", _g.d.ap_ar_trans_detail._doc_ref_debt_billing_ap);
                    //this._addColumn(_g.d.ap_ar_trans_detail._line_number, 2, 1, 10, false, true, true, true, _g.g._getFormatNumberStr(0, 0));
                    this._addColumn(_rowNumberName, 2, 0, 15, false, true, true);
                    this._addColumn(_g.d.ap_ar_trans_detail._bill_type, 2, 1, 10, false, true, true, false);
                    this._columnTopActive = false;
                    break;
                case _g.g._transControlTypeEnum.เจ้าหนี้_ใบรับวางบิล_ยกเลิก:
                    this._addColumn(_g.d.ap_ar_trans_detail._doc_ref, 1, 0, 8, false, true, true, true, "", "", "", _g.d.ap_ar_trans_detail._doc_ref_debt_billing_ap);
                    this._addColumn(_g.d.ap_ar_trans_detail._bill_type_name, 1, 1, 10, false, false, false, true);
                    this._addColumn(_g.d.ap_ar_trans_detail._billing_no, 1, 1, 10, true, false, true, true);
                    this._addColumn(_g.d.ap_ar_trans_detail._billing_date, 4, 1, 15, false, false, true);
                    this._addColumn(_g.d.ap_ar_trans_detail._ref_doc_no, 1, 1, 10, false, false, true, false);
                    this._addColumn(_g.d.ap_ar_trans_detail._ref_doc_date, 4, 1, 15, false, false, true);
                    this._addColumn(_g.d.ap_ar_trans_detail._due_date, 4, 1, 15, false, false, true);
                    this._addColumn(_g.d.ap_ar_trans_detail._sum_value, 3, 1, 10, false, false, true, false, __formatNumber);
                    this._addColumn(_g.d.ap_ar_trans_detail._sum_before_vat, 3, 1, 10, false, false, true, false, __formatNumber);
                    this._addColumn(_g.d.ap_ar_trans_detail._sum_tax_value, 3, 1, 10, false, false, true, false, __formatNumber);
                    this._addColumn(_g.d.ap_ar_trans_detail._sum_discount, 3, 1, 10, false, false, true, false, __formatNumber);
                    this._addColumn(_g.d.ap_ar_trans_detail._sum_debt_value, 3, 1, 10, false, false, true, false, __formatNumber);
                    this._addColumn(_g.d.ap_ar_trans_detail._balance_ref, 3, 1, 8, false, false, true, false, __formatNumber);
                    //this._addColumn(_g.d.ap_ar_trans_detail._line_number, 2, 1, 10, false, true, true, true, _g.g._getFormatNumberStr(0, 0));
                    this._addColumn(_rowNumberName, 2, 0, 15, false, true, true);
                    this._addColumn(_g.d.ap_ar_trans_detail._bill_type, 2, 1, 10, false, true, true, false);
                    break;
                case _g.g._transControlTypeEnum.เจ้าหนี้_จ่ายชำระหนี้:
                    this._addColumn(_g.d.ap_ar_trans_detail._billing_no, 1, 1, 8, true, false, true, true);
                    this._addColumn(_g.d.ap_ar_trans_detail._billing_date, 4, 1, 9, false, false, true);
                    this._addColumn(_g.d.ap_ar_trans_detail._bill_type_name, 1, 1, 10, false, false, false, true);
                    this._addColumn(_g.d.ap_ar_trans_detail._ref_doc_no, 1, 1, 10, false, false, true, false);
                    this._addColumn(_g.d.ap_ar_trans_detail._ref_doc_date, 4, 1, 15, false, false, true);
                    if (MyLib._myGlobal._OEMVersion.Equals("SINGHA"))
                    {
                        this._addColumn(_g.d.ap_ar_trans_detail._bill_tax_no, 1, 1, 10, false, false, true, false);
                        this._addColumn(_g.d.ap_ar_trans_detail._bill_tax_date, 4, 1, 15, false, false, true);
                    }
                    this._addColumn(_g.d.ap_ar_trans_detail._due_date, 4, 1, 9, false, false, true);
                    this._addColumn(_g.d.ap_ar_trans_detail._doc_ref, 1, 0, 8, false, false, true, true, "", "", "", _g.d.ap_ar_trans_detail._doc_ref_debt_billing_ap);
                    this._addColumn(_g.d.ap_ar_trans_detail._sum_debt_amount, 3, 1, 10, false, false, true, false, __formatNumber);
                    this._addColumn(_g.d.ap_ar_trans_detail._balance_ref, 3, 1, 8, false, false, true, false, __formatNumber);
                    this._addColumn(_g.d.ap_ar_trans_detail._sum_pay_money, 3, 1, 10, true, false, true, false, __formatNumber);
                    //this._addColumn(_g.d.ap_ar_trans_detail._line_number, 2, 1, 8, false, true, true, true, _g.g._getFormatNumberStr(0, 0));
                    this._addColumn(_rowNumberName, 2, 0, 15, false, true, true);
                    this._addColumn(_g.d.ap_ar_trans_detail._bill_type, 2, 1, 10, false, true, true, false);
                    break;
                case _g.g._transControlTypeEnum.เจ้าหนี้_จ่ายชำระหนี้_ยกเลิก:
                    this._addColumn(_g.d.ap_ar_trans_detail._doc_ref, 1, 0, 8, false, false, true, true, "", "", "", _g.d.ap_ar_trans_detail._doc_ref_debt_billing_ap);
                    this._addColumn(_g.d.ap_ar_trans_detail._bill_type, 10, 1, 9, true, false, true, true);
                    this._addColumn(_g.d.ap_ar_trans_detail._billing_no, 1, 1, 8, true, false, true, true);
                    this._addColumn(_g.d.ap_ar_trans_detail._billing_date, 4, 1, 9, false, false, true);
                    this._addColumn(_g.d.ap_ar_trans_detail._ref_doc_no, 1, 1, 10, false, false, true, false);
                    this._addColumn(_g.d.ap_ar_trans_detail._ref_doc_date, 4, 1, 15, false, false, true);
                    this._addColumn(_g.d.ap_ar_trans_detail._due_date, 4, 1, 9, false, false, true);
                    this._addColumn(_g.d.ap_ar_trans_detail._sum_value, 3, 1, 8, false, false, true, false, __formatNumber);
                    this._addColumn(_g.d.ap_ar_trans_detail._sum_before_vat, 3, 1, 8, false, false, true, false, __formatNumber);
                    this._addColumn(_g.d.ap_ar_trans_detail._sum_tax_value, 3, 1, 8, false, false, true, false, __formatNumber);
                    this._addColumn(_g.d.ap_ar_trans_detail._sum_discount, 3, 1, 8, false, false, true, false, __formatNumber);
                    this._addColumn(_g.d.ap_ar_trans_detail._sum_debt_value, 3, 1, 8, false, false, true, false, __formatNumber);
                    this._addColumn(_g.d.ap_ar_trans_detail._balance_ref, 3, 1, 8, false, false, true, false, __formatNumber);
                    this._addColumn(_g.d.ap_ar_trans_detail._sum_pay_money, 3, 1, 8, true, false, true, false, __formatNumber);
                    this._addColumn(_g.d.ap_ar_trans_detail._sum_debt_balance, 3, 1, 8, false, false, true, false, __formatNumber);
                    //this._addColumn(_g.d.ap_ar_trans_detail._line_number, 2, 1, 8, false, true, true, true, _g.g._getFormatNumberStr(0, 0));
                    this._addColumn(_rowNumberName, 2, 0, 15, false, true, true);
                    this._columnTopActive = true;
                    this._addColumnTop("ประเภท", 0, 2);
                    this._addColumnTop("ข้อมูลรายวัน", 3, 9);
                    this._addColumnTop("ยอดตัดจ่าย", 10, 12);
                    break;
                case _g.g._transControlTypeEnum.ลูกหนี้_ใบวางบิล:
                    this._addColumn(_g.d.ap_ar_trans_detail._billing_no, 1, 1, 10, true, false, true, true);
                    this._addColumn(_g.d.ap_ar_trans_detail._billing_date, 4, 1, 15, false, false, true);
                    this._addColumn(_g.d.ap_ar_trans_detail._bill_type_name, 1, 1, 10, false, false, false, true);
                    this._addColumn(_g.d.ap_ar_trans_detail._ref_doc_no, 1, 1, 10, false, false, true, false);
                    this._addColumn(_g.d.ap_ar_trans_detail._ref_doc_date, 4, 1, 15, false, false, true);
                    if (MyLib._myGlobal._OEMVersion.Equals("SINGHA"))
                    {
                        this._addColumn(_g.d.ap_ar_trans_detail._bill_tax_no, 1, 1, 10, false, false, true, false);
                        this._addColumn(_g.d.ap_ar_trans_detail._bill_tax_date, 4, 1, 15, false, false, true);
                    }
                    this._addColumn(_g.d.ap_ar_trans_detail._due_date, 4, 1, 15, false, false, true);
                    this._addColumn(_g.d.ap_ar_trans_detail._sum_debt_amount, 3, 1, 10, false, false, true, false, __formatNumber);
                    this._addColumn(_g.d.ap_ar_trans_detail._balance_ref, 3, 1, 8, false, false, true, false, __formatNumber);
                    this._addColumn(_g.d.ap_ar_trans_detail._sum_pay_money, 3, 1, 10, true, false, true, false, __formatNumber, "", "", _g.d.ap_ar_trans_detail._sum_pay_money_1);
                    this._addColumn(_g.d.ap_ar_trans_detail._doc_ref, 1, 0, 8, false, true, true, true, "", "", "", _g.d.ap_ar_trans_detail._doc_ref_debt_billing_ar);
                    //this._addColumn(_g.d.ap_ar_trans_detail._line_number, 2, 1, 10, false, true, true, true, _g.g._getFormatNumberStr(0, 0));
                    this._addColumn(_rowNumberName, 2, 0, 15, false, true, true);
                    this._columnTopActive = false;
                    this._addColumn(_g.d.ap_ar_trans_detail._bill_type, 2, 1, 10, false, true, true, false);
                    break;
                case _g.g._transControlTypeEnum.ลูกหนี้_ใบวางบิล_ยกเลิก:
                    this._addColumn(_g.d.ap_ar_trans_detail._doc_ref, 1, 0, 8, false, true, true, true, "", "", "", _g.d.ap_ar_trans_detail._doc_ref_debt_billing_ap);
                    this._addColumn(_g.d.ap_ar_trans_detail._bill_type, 10, 1, 10, true, false, true, true);
                    this._addColumn(_g.d.ap_ar_trans_detail._billing_no, 1, 1, 10, true, false, true, true);
                    this._addColumn(_g.d.ap_ar_trans_detail._billing_date, 4, 1, 15, false, false, true);
                    this._addColumn(_g.d.ap_ar_trans_detail._ref_doc_no, 1, 1, 10, false, false, true, false);
                    this._addColumn(_g.d.ap_ar_trans_detail._ref_doc_date, 4, 1, 15, false, false, true);
                    this._addColumn(_g.d.ap_ar_trans_detail._due_date, 4, 1, 15, false, false, true);
                    this._addColumn(_g.d.ap_ar_trans_detail._sum_value, 3, 1, 10, false, false, true, false, __formatNumber);
                    this._addColumn(_g.d.ap_ar_trans_detail._sum_before_vat, 3, 1, 10, false, false, true, false, __formatNumber);
                    this._addColumn(_g.d.ap_ar_trans_detail._sum_tax_value, 3, 1, 10, false, false, true, false, __formatNumber);
                    this._addColumn(_g.d.ap_ar_trans_detail._sum_discount, 3, 1, 10, false, false, true, false, __formatNumber);
                    this._addColumn(_g.d.ap_ar_trans_detail._sum_debt_value, 3, 1, 10, false, false, true, false, __formatNumber);
                    this._addColumn(_g.d.ap_ar_trans_detail._balance_ref, 3, 1, 8, false, false, true, false, __formatNumber);
                    //this._addColumn(_g.d.ap_ar_trans_detail._line_number, 2, 1, 10, false, true, true, true, _g.g._getFormatNumberStr(0, 0));
                    this._addColumn(_rowNumberName, 2, 0, 15, false, true, true);
                    this._columnTopActive = true;
                    this._addColumnTop("ประเภท", 0, 1);
                    this._addColumnTop("ข้อมูลรายวัน", 2, 9);
                    break;

                /// ARDebtBilling : 10=รับชำระหนี้
                /// 
                case _g.g._transControlTypeEnum.ลูกหนี้_รับชำระหนี้:
                    this._addColumn(_g.d.ap_ar_trans_detail._billing_no, 1, 1, 8, true, false, true, true);
                    this._addColumn(_g.d.ap_ar_trans_detail._billing_date, 4, 1, 9, false, false, true);
                    this._addColumn(_g.d.ap_ar_trans_detail._bill_type_name, 1, 1, 10, false, false, false, true);
                    this._addColumn(_g.d.ap_ar_trans_detail._ref_doc_no, 1, 1, 10, false, false, true, true);
                    this._addColumn(_g.d.ap_ar_trans_detail._ref_doc_date, 4, 1, 15, false, false, true);
                    if (MyLib._myGlobal._OEMVersion.Equals("SINGHA"))
                    {
                        this._addColumn(_g.d.ap_ar_trans_detail._bill_tax_no, 1, 1, 10, false, false, true, false);
                        this._addColumn(_g.d.ap_ar_trans_detail._bill_tax_date, 4, 1, 15, false, false, true);
                    }
                    this._addColumn(_g.d.ap_ar_trans_detail._due_date, 4, 1, 9, false, false, true);
                    this._addColumn(_g.d.ap_ar_trans_detail._doc_ref, 1, 0, 8, false, false, true, true, "", "", "", _g.d.ap_ar_trans_detail._doc_ref_debt_billing_ar);
                    this._addColumn(_g.d.ap_ar_trans_detail._sum_debt_amount, 3, 1, 10, false, false, true, false, __formatNumber);
                    this._addColumn(_g.d.ap_ar_trans_detail._balance_ref, 3, 1, 8, false, false, true, false, __formatNumber);
                    this._addColumn(_g.d.ap_ar_trans_detail._sum_pay_money, 3, 1, 10, true, false, true, false, __formatNumber);
                    //this._addColumn(_g.d.ap_ar_trans_detail._line_number, 2, 1, 8, false, true, true, true, _g.g._getFormatNumberStr(0, 0));
                    this._addColumn(_rowNumberName, 2, 0, 15, false, true, true);
                    this._addColumn(_g.d.ap_ar_trans_detail._bill_type, 2, 1, 10, false, true, true, false);
                    break;
                case _g.g._transControlTypeEnum.ลูกหนี้_รับชำระหนี้_ยกเลิก:
                    this._addColumn(_g.d.ap_ar_trans_detail._doc_ref, 1, 0, 8, false, false, true, true, "", "", "", _g.d.ap_ar_trans_detail._doc_ref_debt_billing_ap);
                    this._addColumn(_g.d.ap_ar_trans_detail._bill_type, 10, 1, 9, true, false, true, true);
                    this._addColumn(_g.d.ap_ar_trans_detail._billing_no, 1, 1, 8, true, false, true, true);
                    this._addColumn(_g.d.ap_ar_trans_detail._billing_date, 4, 1, 9, false, false, true);
                    this._addColumn(_g.d.ap_ar_trans_detail._ref_doc_no, 1, 1, 10, false, false, true, true);
                    this._addColumn(_g.d.ap_ar_trans_detail._ref_doc_date, 4, 1, 15, false, false, true);
                    this._addColumn(_g.d.ap_ar_trans_detail._due_date, 4, 1, 9, false, false, true);
                    this._addColumn(_g.d.ap_ar_trans_detail._sum_value, 3, 1, 8, false, false, true, false, __formatNumber);
                    this._addColumn(_g.d.ap_ar_trans_detail._sum_before_vat, 3, 1, 8, false, false, true, false, __formatNumber);
                    this._addColumn(_g.d.ap_ar_trans_detail._sum_tax_value, 3, 1, 8, false, false, true, false, __formatNumber);
                    this._addColumn(_g.d.ap_ar_trans_detail._sum_discount, 3, 1, 8, false, false, true, false, __formatNumber);
                    this._addColumn(_g.d.ap_ar_trans_detail._sum_debt_value, 3, 1, 8, false, false, true, false, __formatNumber);
                    this._addColumn(_g.d.ap_ar_trans_detail._balance_ref, 3, 1, 8, false, false, true, false, __formatNumber);
                    this._addColumn(_g.d.ap_ar_trans_detail._sum_pay_money, 3, 1, 8, true, false, true, false, __formatNumber);
                    this._addColumn(_g.d.ap_ar_trans_detail._sum_debt_balance, 3, 1, 8, false, false, true, false, __formatNumber);
                    //this._addColumn(_g.d.ap_ar_trans_detail._line_number, 2, 1, 8, false, true, true, true, _g.g._getFormatNumberStr(0, 0));
                    this._addColumn(_rowNumberName, 2, 0, 15, false, true, true);
                    this._columnTopActive = true;
                    this._addColumnTop("ประเภท", 0, 2);
                    this._addColumnTop("ข้อมูลรายวัน", 3, 9);
                    this._addColumnTop("ยอดตัดจ่าย", 10, 12);
                    break;
                /// ARDebtBillingCut : ตัดหนี้สูญ
                case _g.g._transControlTypeEnum.ลูกหนี้_ตัดหนี้สูญ:
                    this._addColumn(_g.d.ap_ar_trans_detail._doc_ref, 1, 0, 8, false, false, true, true, "", "", "", _g.d.ap_ar_trans_detail._doc_ref_debt_billing_ap);
                    this._addColumn(_g.d.ap_ar_trans_detail._bill_type, 10, 1, 9, true, false, true, true);
                    this._addColumn(_g.d.ap_ar_trans_detail._billing_no, 1, 1, 8, true, false, true, true);
                    this._addColumn(_g.d.ap_ar_trans_detail._billing_date, 4, 1, 9, false, false, true);
                    this._addColumn(_g.d.ap_ar_trans_detail._ref_doc_no, 1, 1, 10, false, false, true, true);
                    this._addColumn(_g.d.ap_ar_trans_detail._ref_doc_date, 4, 1, 15, false, false, true);
                    this._addColumn(_g.d.ap_ar_trans_detail._due_date, 4, 1, 9, false, false, true);
                    this._addColumn(_g.d.ap_ar_trans_detail._sum_value, 3, 1, 8, false, false, true, false, __formatNumber);
                    this._addColumn(_g.d.ap_ar_trans_detail._sum_before_vat, 3, 1, 8, false, false, true, false, __formatNumber);
                    this._addColumn(_g.d.ap_ar_trans_detail._sum_tax_value, 3, 1, 8, false, false, true, false, __formatNumber);
                    this._addColumn(_g.d.ap_ar_trans_detail._sum_discount, 3, 1, 8, false, false, true, false, __formatNumber);
                    this._addColumn(_g.d.ap_ar_trans_detail._balance_ref, 3, 1, 8, false, false, true, false, __formatNumber);
                    this._addColumn(_g.d.ap_ar_trans_detail._sum_debt_value, 3, 1, 8, false, false, true, false, __formatNumber);
                    this._addColumn(_g.d.ap_ar_trans_detail._sum_pay_money, 3, 1, 8, true, false, true, false, __formatNumber);
                    this._addColumn(_g.d.ap_ar_trans_detail._sum_debt_balance, 3, 1, 8, false, false, true, false, __formatNumber);
                    //this._addColumn(_g.d.ap_ar_trans_detail._line_number, 2, 1, 8, false, true, true, true, _g.g._getFormatNumberStr(0, 0));
                    this._addColumn(_rowNumberName, 2, 0, 15, false, true, true);
                    this._columnTopActive = true;
                    this._addColumnTop("ประเภท", 0, 2);
                    this._addColumnTop("ข้อมูลรายวัน", 3, 9);
                    this._addColumnTop("ยอดตัดจ่าย", 10, 12);
                    break;
                case _g.g._transControlTypeEnum.IMEX_Bill_Collector:
                    this._addColumn(_g.d.ap_ar_trans_detail._cust_code, 1, 1, 9, false, false, true, false);
                    this._addColumn(_g.d.ap_ar_trans_detail._cust_name, 1, 1, 9, false, false, false, false);
                    this._addColumn(_g.d.ap_ar_trans_detail._billing_no, 1, 1, 8, false, false, true, true);
                    this._addColumn(_g.d.ap_ar_trans_detail._billing_date, 4, 1, 9, false, false, true);
                    this._addColumn(_g.d.ap_ar_trans_detail._due_date, 4, 1, 15, false, false, true);
                    this._addColumn(_g.d.ap_ar_trans_detail._bill_type_name, 1, 1, 10, false, false, false, true);

                    this._addColumn(_g.d.ap_ar_trans_detail._sum_debt_amount, 3, 1, 10, false, false, true, false, __formatNumber);
                    this._addColumn(_g.d.ap_ar_trans_detail._balance_ref, 3, 1, 8, false, false, true, false, __formatNumber);
                    this._addColumn(_g.d.ap_ar_trans_detail._sum_pay_money, 3, 1, 10, true, false, true, false, __formatNumber, "", "", _g.d.ap_ar_trans_detail._sum_pay_money_1);
                    this._addColumn(_g.d.ap_ar_trans_detail._bill_type, 2, 1, 10, false, true, true, false);

                    break;
            }

            //this._clickSearchButton += new MyLib.SearchEventHandler(_arapTransGridControl__clickSearchButton);
            this._alterCellUpdate += new MyLib.AfterCellUpdateEventHandler(_arapTransGridControl__alterCellUpdate);
            this._beforeDisplayRow += new MyLib.BeforeDisplayRowEventHandler(_ar_ap_trans_grid__beforeDisplayRow);
            // this._afterAddRow += new MyLib.AfterAddRowEventHandler(_arapTransGridControl__afterAddRow);

            this.ShowTotal = true;
            this.ColumnBackgroundAuto = false;
            this.ColumnBackgroundEnd = Color.FromArgb(198, 220, 249);
            this.RowOddBackground = Color.AliceBlue;
            this.AutoSize = true;
            this.Dock = DockStyle.Fill;
            this._calcPersentWidthToScatter();
            this.Invalidate();
            this.ResumeLayout();
        }

        MyLib.BeforeDisplayRowReturn _ar_ap_trans_grid__beforeDisplayRow(MyLib._myGrid sender, int row, int columnNumber, string columnName, MyLib.BeforeDisplayRowReturn senderRow, MyLib._myGrid._columnType columnType, ArrayList rowData)
        {
            int __billTypeNameColumnNumber = sender._findColumnByName(_g.d.ap_ar_trans_detail._bill_type_name);
            if (__billTypeNameColumnNumber == columnNumber)
            {
                int __billTypeColumnNumber = sender._findColumnByName(_g.d.ap_ar_trans_detail._bill_type);
                ((ArrayList)senderRow.newData)[columnNumber] = _g.g._transFlagGlobal._transName(MyLib._myGlobal._intPhase(((ArrayList)senderRow.newData)[__billTypeColumnNumber].ToString()));
            }
            return senderRow;
        }

        void _arapTransGridControl__alterCellUpdate(object sender, int row, int column)
        {
            switch (this._transControlType)
            {
                case _g.g._transControlTypeEnum.เจ้าหนี้_ใบรับวางบิล:
                case _g.g._transControlTypeEnum.ลูกหนี้_ใบวางบิล:
                case _g.g._transControlTypeEnum.เจ้าหนี้_ใบรับวางบิล_ยกเลิก:
                case _g.g._transControlTypeEnum.ลูกหนี้_ใบวางบิล_ยกเลิก:
                case _g.g._transControlTypeEnum.เจ้าหนี้_จ่ายชำระหนี้:
                case _g.g._transControlTypeEnum.ลูกหนี้_รับชำระหนี้:
                case _g.g._transControlTypeEnum.เจ้าหนี้_จ่ายชำระหนี้_ยกเลิก:
                case _g.g._transControlTypeEnum.ลูกหนี้_รับชำระหนี้_ยกเลิก:
                    {
                        if (this._cellGet(row, _g.d.ap_ar_trans_detail._billing_no) != null)
                        {
                            if (column == this._findColumnByName(_g.d.ap_ar_trans_detail._billing_no))
                            {
                                // ตรวจเอกสารซ้ำ
                                string __getBillNo = this._cellGet(row, _g.d.ap_ar_trans_detail._billing_no).ToString();
                                if (__getBillNo.Trim().Length == 0)
                                {
                                    this._clearRow(row);
                                    this._rowData.RemoveAt(row);
                                }
                                else
                                {
                                    Boolean __duplicate = false;
                                    for (int __find = 0; __find < this._rowData.Count; __find++)
                                    {
                                        if (__find != row && __getBillNo.ToLower().Equals(this._cellGet(__find, _g.d.ap_ar_trans_detail._billing_no).ToString().ToLower()))
                                        {
                                            __duplicate = true;
                                            break;
                                        }
                                    }
                                    if (__duplicate)
                                    {
                                        MessageBox.Show(MyLib._myGlobal._resource("มีรายการซ้ำ") + " : " + __getBillNo);
                                        this._clearRow(row);
                                    }
                                    else
                                    {
                                        if ((_g.g._companyProfile._ar_bill_inform && this._transControlType == _g.g._transControlTypeEnum.ลูกหนี้_ใบวางบิล) ||
                                            (_g.g._companyProfile._ap_bill_inform && this._transControlType == _g.g._transControlTypeEnum.เจ้าหนี้_ใบรับวางบิล))
                                        {
                                            // ใบวางบิลที่เคยวางไปแล้ว ห้ามนำมาวางใหม่
                                            string __query = "select " + _g.d.ap_ar_trans_detail._billing_no + " from " + _g.d.ap_ar_trans_detail._table + " where " + MyLib._myGlobal._addUpper(_g.d.ap_ar_trans_detail._billing_no) + "=\'" + __getBillNo.ToUpper() + "\' and " + _g.d.ap_ar_trans_detail._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(this._transControlType) + " and " + _g.d.ap_ar_trans_detail._last_status + "=0";
                                            DataTable __dt = _myFrameWork._queryShort(__query).Tables[0];
                                            if (__dt.Rows.Count > 0)
                                            {
                                                if (this._transControlType == _g.g._transControlTypeEnum.เจ้าหนี้_ใบรับวางบิล)
                                                    MessageBox.Show(MyLib._myGlobal._resource("ห้ามรับวางบิลซ้ำ") + " : " + __getBillNo);
                                                else
                                                    MessageBox.Show(MyLib._myGlobal._resource("ห้ามวางบิลซ้ำ") + " : " + __getBillNo);
                                                this._clearRow(row);
                                            }
                                        }
                                    }
                                    this._searchRow(row);
                                }
                            }
                        }
                    }
                    break;
            }
        }

        public void __clickSearchButton(object sender, MyLib.GridCellEventArgs e, string _cust_code, string _strQueryWhere)
        {
            Boolean __reload = false;
            if (this._selectBill == null)
            {
                this._selectBill = new _selectBillForm(this._transControlType);
                this._selectBill._processButton.Click += new EventHandler(_processButton_Click);
                __reload = true;
            }
            if (__reload || this._lastCustCode.Equals(this._getCustCode()) == false || this._lastDateTime.Equals(this._getProcessDate()) == false)
            {
                this._lastCustCode = this._getCustCode();
                this._lastDateTime = this._getProcessDate();
                this._selectBill._process(this._lastCustCode, this._lastDateTime);
            }
            this._selectBill.Text = this._lastCustCode + " : " + MyLib._myGlobal._convertDateToString(this._lastDateTime, true);
            // ลบรายการที่เลือกไปแล้ว
            for (int __row = 0; __row < this._rowData.Count; __row++)
            {
                string __docNo = this._cellGet(__row, _g.d.ap_ar_trans_detail._billing_no).ToString();
                int __addr = this._selectBill._resultGrid._findData(this._selectBill._resultGrid._findColumnByName(_g.d.ap_ar_resource._doc_no), __docNo);
                if (__addr != -1)
                {
                    this._selectBill._resultGrid._rowData.RemoveAt(__addr);
                }
            }
            //
            this._selectBill.ShowDialog();
        }

        void _processButton_Click(object sender, EventArgs e)
        {
            this._selectBill.Close();
            // เพิ่มบรรทัดใหม่
            for (int __row = 0; __row < this._selectBill._resultGrid._rowData.Count; __row++)
            {
                // ลบบรรทัดที่ว่าง
                int __rowDelete = 0;
                while (__rowDelete < this._rowData.Count)
                {
                    if (this._cellGet(__rowDelete, _g.d.ap_ar_trans_detail._billing_no).ToString().Trim().Length == 0)
                    {
                        this._rowData.RemoveAt(__rowDelete);
                    }
                    else
                    {
                        __rowDelete++;
                    }
                }
                if ((int)this._selectBill._resultGrid._cellGet(__row, _g.d.ap_ar_resource._select) == 1)
                {
                    int __rowAddr = this._addRow();
                    this._cellUpdate(__rowAddr, _g.d.ap_ar_trans_detail._billing_no, this._selectBill._resultGrid._cellGet(__row, _g.d.ap_ar_resource._doc_no).ToString(), true);
                }
            }
            this._gotoCell(this._rowData.Count, this._findColumnByName(_g.d.ap_ar_trans_detail._billing_no));
        }

        private void _searchAll(string name, int row)
        {
            /*string result = (string)this._search_data_full_pointer._dataList._gridData._cellGet(row, 0);
            if (result.Length > 0)
            {
                this._search_data_full_pointer.Close();
                this._cellUpdate(this._selectRow, _g.d.ap_ar_trans_detail._billing_no, result, true);
                _searchItemRow(row);
                SendKeys.Send("{ENTER}");
                _g.g._is_change = true;
            }*/
        }

        void _clearRow(int row)
        {
            this._cellUpdate(row, _g.d.ap_ar_trans_detail._billing_no, "", false);
            this._cellUpdate(row, _g.d.ap_ar_trans_detail._billing_date, "", false);
            this._cellUpdate(row, _g.d.ap_ar_trans_detail._due_date, "", false);
            this._cellUpdate(row, _g.d.ap_ar_trans_detail._sum_value, 0.00M, false);
            this._cellUpdate(row, _g.d.ap_ar_trans_detail._sum_before_vat, 0.00M, false);
            this._cellUpdate(row, _g.d.ap_ar_trans_detail._sum_tax_value, 0.00M, false);
            this._cellUpdate(row, _g.d.ap_ar_trans_detail._sum_discount, 0.00M, true);
            this._cellUpdate(row, _g.d.ap_ar_trans_detail._sum_debt_value, 0.00M, false);
            this._cellUpdate(row, _g.d.ap_ar_trans_detail._balance_ref, 0.00M, false);


        }

        void _searchRow(int row)
        {
            string __docNo = this._cellGet(row, _g.d.ap_ar_trans_detail._billing_no).ToString();
            if (__docNo.Length > 0)
            {
                SMLERPARAPInfo._process __process = new SMLERPARAPInfo._process();

                string __where = "";
                if (this._selectBill != null && this._selectBill._showZeroDocCheckbox.Checked == true)
                {
                    __where = " (true) or ( amount=0 and (select count(doc_no) from ap_ar_trans_detail where ap_ar_trans_detail.billing_no = temp3.doc_no ) = 0 ) ";
                }

                DataTable __getData = __process._arBalanceDoc(this._transControlType, 0, this._getCustCode(), this._getCustCode(), __docNo, __docNo, this._getProcessDate(), "", __where, false);
                if (__getData != null && __getData.Rows.Count > 0)
                {
                    DataRow __dataRow = __getData.Rows[0];
                    int __docType = MyLib._myGlobal._intPhase(__dataRow[_g.d.ap_ar_resource._doc_type_number].ToString());
                    this._cellUpdate(row, _g.d.ap_ar_trans_detail._billing_date, MyLib._myGlobal._convertDateFromQuery(__dataRow[_g.d.ap_ar_resource._doc_date].ToString()), false);
                    this._cellUpdate(row, _g.d.ap_ar_trans_detail._bill_type, __docType, false);
                    this._cellUpdate(row, _g.d.ap_ar_trans_detail._due_date, MyLib._myGlobal._convertDateFromQuery(__dataRow[_g.d.ap_ar_resource._due_date].ToString()), false);
                    this._cellUpdate(row, _g.d.ap_ar_trans_detail._sum_debt_amount, __dataRow[_g.d.ap_ar_resource._amount], false);
                    this._cellUpdate(row, _g.d.ap_ar_trans_detail._balance_ref, __dataRow[_g.d.ap_ar_resource._ar_balance], false);
                    this._cellUpdate(row, _g.d.ap_ar_trans_detail._sum_pay_money, __dataRow[_g.d.ap_ar_resource._ar_balance], false);
                    this._cellUpdate(row, _g.d.ap_ar_resource._ref_doc_no, __dataRow[_g.d.ap_ar_resource._ref_doc_no], false);
                    this._cellUpdate(row, _g.d.ap_ar_resource._ref_doc_date, MyLib._myGlobal._convertDateFromQuery(__dataRow[_g.d.ap_ar_resource._ref_doc_date].ToString()), false);
                    this._cellUpdate(row, _g.d.ap_ar_trans_detail._bill_tax_no,__dataRow[_g.d.ap_ar_resource._tax_doc_no].ToString(), false);
                    this._cellUpdate(row, _g.d.ap_ar_trans_detail._bill_tax_date, MyLib._myGlobal._convertDateFromQuery(__dataRow[_g.d.ap_ar_resource._tax_doc_date].ToString()), false);
                }
                else
                {
                    MessageBox.Show(MyLib._myGlobal._resource("ไม่พบรายการ กรุณาตรวจสอบใหม่") + " : " + __docNo);
                    this._clearRow(row);
                }
            }
            else
            {
                this._clearRow(row);
            }
            // คำนวณยอดใหม่
            this._screen._reCalc();
        }
        //
        public delegate string _getCustCodeEventHandler();
        public delegate DateTime _getProcessDateEventHandler();
    }
}
