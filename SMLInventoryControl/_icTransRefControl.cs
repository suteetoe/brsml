using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLInventoryControl
{
    public partial class _icTransRefControl : UserControl
    {
        public delegate void ProcessButtonEventHandler(object sender);
        public delegate int VatTypeNumberHandler();
        //
        public event VatTypeNumberHandler _vatTypeNumber;
        public event ProcessButtonEventHandler _processButton;
        //
        private _g.g._transControlTypeEnum _icTransControlTypeTemp;
        private MyLib._searchDataFull _icTransSearch;
        public string _custCode = "";
        public string _docNo = "";
        public MyLib._myGrid _transGrid = new MyLib._myGrid();
        private Boolean _refCheckStatusTemp = false;

        public _icTransRefControl()
        {
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this._toolBarButtom.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }
        }

        public Boolean _refCheckStatus
        {
            set
            {
                this._refCheckStatusTemp = value;
                this._refCheck.Image = this._imageList.Images[(value == false) ? 0 : 1];
            }
            get
            {
                return this._refCheckStatusTemp;
            }
        }

        public void _cleardata()
        {
            this._transGrid._clear();
        }


        public _g.g._transControlTypeEnum _icTransControlType
        {
            set
            {
                this._icTransControlTypeTemp = value;
                this._build();
            }
            get
            {
                return this._icTransControlTypeTemp;
            }
        }

        private void _build()
        {
            if (this._icTransControlType == _g.g._transControlTypeEnum.ว่าง)
            {
                return;
            }
            this._transGrid.Dock = DockStyle.Fill;
            this._transGrid._table_name = _g.d.ap_ar_trans_detail._table;
            string __formatNumberAmount = _g.g._getFormatNumberStr(3);
            switch (this._icTransControlType)
            {
                case _g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ:
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._bill_type, 10, 25, 25, _g.g._companyProfile._use_reference_pr, false);
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._billing_no, 1, 0, 25, true, false, true, true, "", "", "", _g.d.ap_ar_trans_detail._doc_ref);
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._billing_date, 4, 0, 25, false, false, true, true, "", "", "", _g.d.ap_ar_trans_detail._doc_ref_date);
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._remark, 1, 0, 25, false, false);
                    this._transGrid._cellComboBoxGet += new MyLib.CellComboBoxItemGetDisplay(_transGrid__cellComboBoxGet);
                    this._transGrid._cellComboBoxItem += new MyLib.CellComboBoxItemEventHandler(_transGrid__cellComboBoxItem);
                    this.Controls.Add(this._transGrid);

                    /*
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._billing_no, 1, 0, 25, true, false, true, true, "", "", "", _g.d.ap_ar_trans_detail._doc_ref_po_requrest);
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._billing_date, 4, 0, 25, false, false, true, true, "", "", "", _g.d.ap_ar_trans_detail._doc_ref_date);
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._remark, 1, 0, 50, false, false);
                     * */
                    this.Controls.Add(this._transGrid);
                    break;
                case _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ:
                case _g.g._transControlTypeEnum.ขาย_ใบกำกับภาษีอย่างเต็มออกแทน:
                    //
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._bill_type, 10, 15, 15, true);
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._billing_no, 1, 0, 20, true, false, true, true);
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._billing_date, 4, 0, 20, false, false, true, true);
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._ref_doc_no, 1, 0, 15, false, false, true, true);
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._ref_doc_date, 4, 0, 15, false, false, true, true);


                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._remark, 1, 0, 15, false, false);
                    this._transGrid._cellComboBoxGet += new MyLib.CellComboBoxItemGetDisplay(_transGrid__cellComboBoxGet);
                    this._transGrid._cellComboBoxItem += new MyLib.CellComboBoxItemEventHandler(_transGrid__cellComboBoxItem);
                    this.Controls.Add(this._transGrid);
                    break;
                case _g.g._transControlTypeEnum.ขาย_สั่งขาย:
                    //
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._bill_type, 10, 25, 25, ((_g.g._companyProfile._ss_ref_po_only) ? false : true), false, true);
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._billing_no, 1, 0, 25, true, false, true, true, "", "", "", _g.d.ap_ar_trans_detail._doc_ref);
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._billing_date, 4, 0, 25, false, false, true, true, "", "", "", _g.d.ap_ar_trans_detail._doc_ref_date);
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._remark, 1, 0, 25, false, false);
                    this._transGrid._cellComboBoxGet += new MyLib.CellComboBoxItemGetDisplay(_transGrid__cellComboBoxGet);
                    this._transGrid._cellComboBoxItem += new MyLib.CellComboBoxItemEventHandler(_transGrid__cellComboBoxItem);
                    this.Controls.Add(this._transGrid);
                    break;
                case _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า:
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._billing_no, 1, 0, 25, true, false, true, true, "", "", "", _g.d.ap_ar_trans_detail._doc_ref_quotation);
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._billing_date, 4, 0, 25, false, false, true, true, "", "", "", _g.d.ap_ar_trans_detail._doc_ref_quotation_date);
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._remark, 1, 0, 50, false, false);
                    this.Controls.Add(this._transGrid);
                    break;
                case _g.g._transControlTypeEnum.ขาย_เพิ่มหนี้:
                    //
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._bill_type, 10, 25, 20, false, false, true);
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._billing_no, 1, 0, 20, true, false, true, true, "", "", "", _g.d.ap_ar_trans_detail._doc_ref);
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._billing_date, 4, 0, 12, false, false, true, true, "", "", "", _g.d.ap_ar_trans_detail._doc_ref_date);
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._sum_debt_value, 3, 0, 12, false, false, true, false, __formatNumberAmount);
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._sum_debt_balance, 3, 0, 12, false, false, true, false, __formatNumberAmount);
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._sum_debt_amount, 3, 0, 12, false, false, true, false, __formatNumberAmount, "", "", _g.d.ap_ar_trans_detail._ref_amount_debit);
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._final_amount, 3, 0, 12, false, false, true, false, __formatNumberAmount);

                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._sum_before_vat, 3, 0, 12, false, true, true, false, __formatNumberAmount);

                    this._transGrid._cellComboBoxGet += new MyLib.CellComboBoxItemGetDisplay(_transGrid__cellComboBoxGet);
                    this._transGrid._cellComboBoxItem += new MyLib.CellComboBoxItemEventHandler(_transGrid__cellComboBoxItem);
                    this.Controls.Add(this._transGrid);
                    break;
                case _g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้:
                    //
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._bill_type, 10, 25, 20, ((MyLib._myGlobal._OEMVersion.Equals("SINGHA")) ? true : false), false, true);
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._billing_no, 1, 0, 20, true, false, true, true, "", "", "", _g.d.ap_ar_trans_detail._doc_ref);
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._billing_date, 4, 0, 12, false, false, true, true, "", "", "", _g.d.ap_ar_trans_detail._doc_ref_date);
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._sum_debt_value, 3, 0, 12, false, false, true, false, __formatNumberAmount);
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._sum_debt_balance, 3, 0, 12, false, false, true, false, __formatNumberAmount);
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._sum_debt_amount, 3, 0, 12, false, false, true, false, __formatNumberAmount, "", "", _g.d.ap_ar_trans_detail._ref_amount_credit);
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._final_amount, 3, 0, 12, false, false, true, false, __formatNumberAmount);

                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._sum_before_vat, 3, 0, 12, false, true, true, false, __formatNumberAmount);

                    this._transGrid._cellComboBoxGet += new MyLib.CellComboBoxItemGetDisplay(_transGrid__cellComboBoxGet);
                    this._transGrid._cellComboBoxItem += new MyLib.CellComboBoxItemEventHandler(_transGrid__cellComboBoxItem);
                    this.Controls.Add(this._transGrid);
                    break;
                case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด:
                    //
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._bill_type, 10, 25, 20, false, false, true);
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._billing_no, 1, 0, 20, true, false, true, true, "", "", "", _g.d.ap_ar_trans_detail._doc_ref);
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._billing_date, 4, 0, 12, false, false, true, true, "", "", "", _g.d.ap_ar_trans_detail._doc_ref_date);
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._sum_debt_value, 3, 0, 12, false, false, true, false, __formatNumberAmount);
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._sum_debt_balance, 3, 0, 12, false, false, true, false, __formatNumberAmount);
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._sum_debt_amount, 3, 0, 12, false, false, true, false, __formatNumberAmount, "", "", _g.d.ap_ar_trans_detail._ref_amount_debit);
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._final_amount, 3, 0, 12, false, false, true, false, __formatNumberAmount);

                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._sum_before_vat, 3, 0, 12, false, true, true, false, __formatNumberAmount);

                    this._transGrid._cellComboBoxGet += new MyLib.CellComboBoxItemGetDisplay(_transGrid__cellComboBoxGet);
                    this._transGrid._cellComboBoxItem += new MyLib.CellComboBoxItemEventHandler(_transGrid__cellComboBoxItem);
                    this.Controls.Add(this._transGrid);
                    break;
                case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_เพิ่มหนี้:
                    //
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._bill_type, 10, 25, 20, false, false, true);
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._billing_no, 1, 0, 20, true, false, true, true, "", "", "", _g.d.ap_ar_trans_detail._doc_ref);
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._billing_date, 4, 0, 12, false, false, true, true, "", "", "", _g.d.ap_ar_trans_detail._doc_ref_date);
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._sum_debt_value, 3, 0, 12, false, false, true, false, __formatNumberAmount);
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._sum_debt_balance, 3, 0, 12, false, false, true, false, __formatNumberAmount);
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._sum_debt_amount, 3, 0, 12, false, false, true, false, __formatNumberAmount, "", "", _g.d.ap_ar_trans_detail._ref_amount_debit);
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._final_amount, 3, 0, 12, false, false, true, false, __formatNumberAmount);

                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._sum_before_vat, 3, 0, 12, false, true, true, false, __formatNumberAmount);

                    this._transGrid._cellComboBoxGet += new MyLib.CellComboBoxItemGetDisplay(_transGrid__cellComboBoxGet);
                    this._transGrid._cellComboBoxItem += new MyLib.CellComboBoxItemEventHandler(_transGrid__cellComboBoxItem);
                    this.Controls.Add(this._transGrid);
                    break;
                case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ส่งคืนสินค้าหรือราคาผิด:
                    //
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._bill_type, 10, 25, 20, false, false, true);
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._billing_no, 1, 0, 20, true, false, true, true, "", "", "", _g.d.ap_ar_trans_detail._doc_ref);
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._billing_date, 4, 0, 12, false, false, true, true, "", "", "", _g.d.ap_ar_trans_detail._doc_ref_date);
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._sum_debt_value, 3, 0, 12, false, false, true, false, __formatNumberAmount);
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._sum_debt_balance, 3, 0, 12, false, false, true, false, __formatNumberAmount);
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._sum_debt_amount, 3, 0, 12, false, false, true, false, __formatNumberAmount, "", "", _g.d.ap_ar_trans_detail._ref_amount_debit);
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._final_amount, 3, 0, 12, false, false, true, false, __formatNumberAmount);

                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._sum_before_vat, 3, 0, 12, false, true, true, false, __formatNumberAmount);

                    this._transGrid._cellComboBoxGet += new MyLib.CellComboBoxItemGetDisplay(_transGrid__cellComboBoxGet);
                    this._transGrid._cellComboBoxItem += new MyLib.CellComboBoxItemEventHandler(_transGrid__cellComboBoxItem);
                    this.Controls.Add(this._transGrid);
                    break;
                case _g.g._transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด:
                    //
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._bill_type, 10, 25, 20, false, false, true);
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._billing_no, 1, 0, 20, true, false, true, true, "", "", "", _g.d.ap_ar_trans_detail._doc_ref);
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._billing_date, 4, 0, 12, false, false, true, true, "", "", "", _g.d.ap_ar_trans_detail._doc_ref_date);
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._sum_debt_value, 3, 0, 12, false, false, true, false, __formatNumberAmount);
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._sum_debt_balance, 3, 0, 12, false, false, true, false, __formatNumberAmount);
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._sum_debt_amount, 3, 0, 12, false, false, true, false, __formatNumberAmount, "", "", _g.d.ap_ar_trans_detail._ref_amount_credit);
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._final_amount, 3, 0, 12, false, false, true, false, __formatNumberAmount);

                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._sum_before_vat, 3, 0, 12, false, true, true, false, __formatNumberAmount);

                    //
                    this._transGrid._cellComboBoxGet += new MyLib.CellComboBoxItemGetDisplay(_transGrid__cellComboBoxGet);
                    this._transGrid._cellComboBoxItem += new MyLib.CellComboBoxItemEventHandler(_transGrid__cellComboBoxItem);
                    this.Controls.Add(this._transGrid);
                    break;
                case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ลดหนี้:
                    //
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._bill_type, 10, 25, 20, false, false, true);
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._billing_no, 1, 0, 20, true, false, true, true, "", "", "", _g.d.ap_ar_trans_detail._doc_ref);
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._billing_date, 4, 0, 12, false, false, true, true, "", "", "", _g.d.ap_ar_trans_detail._doc_ref_date);
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._sum_debt_value, 3, 0, 12, false, false, true, false, __formatNumberAmount);
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._sum_debt_balance, 3, 0, 12, false, false, true, false, __formatNumberAmount);
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._sum_debt_amount, 3, 0, 12, false, false, true, false, __formatNumberAmount, "", "", _g.d.ap_ar_trans_detail._ref_amount_credit);
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._final_amount, 3, 0, 12, false, false, true, false, __formatNumberAmount);

                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._sum_before_vat, 3, 0, 12, false, true, true, false, __formatNumberAmount);

                    //
                    this._transGrid._cellComboBoxGet += new MyLib.CellComboBoxItemGetDisplay(_transGrid__cellComboBoxGet);
                    this._transGrid._cellComboBoxItem += new MyLib.CellComboBoxItemEventHandler(_transGrid__cellComboBoxItem);
                    this.Controls.Add(this._transGrid);
                    break;
                case _g.g._transControlTypeEnum.เจ้าหนี้_ตั้งหนี้อื่น:
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._billing_no, 1, 0, 25, true, false, true, true, "", "", "", _g.d.ap_ar_trans_detail._doc_ref_purchase_order);
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._billing_date, 4, 0, 25, false, false, true, true, "", "", "", _g.d.ap_ar_trans_detail._doc_ref_purchase_order_date);
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._remark, 1, 0, 50, false, false);
                    this.Controls.Add(this._transGrid);
                    break;
                case _g.g._transControlTypeEnum.เจ้าหนี้_ลดหนี้อื่น:
                    //
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._bill_type, 10, 25, 0, false, true, true);
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._billing_no, 1, 0, 40, true, false, true, true, "", "", "", _g.d.ap_ar_trans_detail._doc_ref);
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._billing_date, 4, 0, 12, false, false, true, true, "", "", "", _g.d.ap_ar_trans_detail._doc_ref_date);
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._sum_debt_value, 3, 0, 12, false, false, true, false, __formatNumberAmount);
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._sum_debt_balance, 3, 0, 12, false, false, true, false, __formatNumberAmount);
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._sum_debt_amount, 3, 0, 12, false, false, true, false, __formatNumberAmount, "", "", _g.d.ap_ar_trans_detail._ref_amount_credit);
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._final_amount, 3, 0, 12, false, false, true, false, __formatNumberAmount);

                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._sum_before_vat, 3, 0, 12, false, true, true, false, __formatNumberAmount);

                    //
                    this._transGrid._cellComboBoxGet += new MyLib.CellComboBoxItemGetDisplay(_transGrid__cellComboBoxGet);
                    this._transGrid._cellComboBoxItem += new MyLib.CellComboBoxItemEventHandler(_transGrid__cellComboBoxItem);
                    this.Controls.Add(this._transGrid);
                    break;
                case _g.g._transControlTypeEnum.เจ้าหนี้_เพิ่มหนี้อื่น:
                    //
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._bill_type, 10, 25, 0, false, true, true);
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._billing_no, 1, 0, 40, true, false, true, true, "", "", "", _g.d.ap_ar_trans_detail._doc_ref);
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._billing_date, 4, 0, 12, false, false, true, true, "", "", "", _g.d.ap_ar_trans_detail._doc_ref_date);
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._sum_debt_value, 3, 0, 12, false, false, true, false, __formatNumberAmount);
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._sum_debt_balance, 3, 0, 12, false, false, true, false, __formatNumberAmount);
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._sum_debt_amount, 3, 0, 12, false, false, true, false, __formatNumberAmount, "", "", _g.d.ap_ar_trans_detail._ref_amount_credit);
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._final_amount, 3, 0, 12, false, false, true, false, __formatNumberAmount);

                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._sum_before_vat, 3, 0, 12, false, true, true, false, __formatNumberAmount);

                    //
                    this._transGrid._cellComboBoxGet += new MyLib.CellComboBoxItemGetDisplay(_transGrid__cellComboBoxGet);
                    this._transGrid._cellComboBoxItem += new MyLib.CellComboBoxItemEventHandler(_transGrid__cellComboBoxItem);
                    this.Controls.Add(this._transGrid);
                    break;
                case _g.g._transControlTypeEnum.ลูกหนี้_ตั้งหนี้อื่น:
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._billing_no, 1, 0, 25, true, false, true, true, "", "", "", _g.d.ap_ar_trans_detail._doc_ref_purchase_order);
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._billing_date, 4, 0, 25, false, false, true, true, "", "", "", _g.d.ap_ar_trans_detail._doc_ref_purchase_order_date);
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._remark, 1, 0, 50, false, false);
                    this.Controls.Add(this._transGrid);
                    break;
                case _g.g._transControlTypeEnum.ลูกหนี้_ลดหนี้อื่น:
                    //
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._bill_type, 10, 25, 0, false, true, true);
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._billing_no, 1, 0, 40, true, false, true, true, "", "", "", _g.d.ap_ar_trans_detail._doc_ref);
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._billing_date, 4, 0, 12, false, false, true, true, "", "", "", _g.d.ap_ar_trans_detail._doc_ref_date);
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._sum_debt_value, 3, 0, 12, false, false, true, false, __formatNumberAmount);
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._sum_debt_balance, 3, 0, 12, false, false, true, false, __formatNumberAmount);
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._sum_debt_amount, 3, 0, 12, false, false, true, false, __formatNumberAmount, "", "", _g.d.ap_ar_trans_detail._ref_amount_credit);
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._final_amount, 3, 0, 12, false, false, true, false, __formatNumberAmount);

                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._sum_before_vat, 3, 0, 12, false, true, true, false, __formatNumberAmount);

                    //
                    this._transGrid._cellComboBoxGet += new MyLib.CellComboBoxItemGetDisplay(_transGrid__cellComboBoxGet);
                    this._transGrid._cellComboBoxItem += new MyLib.CellComboBoxItemEventHandler(_transGrid__cellComboBoxItem);
                    this.Controls.Add(this._transGrid);
                    break;
                case _g.g._transControlTypeEnum.ลูกหนี้_เพิ่มหนี้อื่น:
                    //
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._bill_type, 10, 25, 0, false, true, true);
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._billing_no, 1, 0, 40, true, false, true, true, "", "", "", _g.d.ap_ar_trans_detail._doc_ref);
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._billing_date, 4, 0, 12, false, false, true, true, "", "", "", _g.d.ap_ar_trans_detail._doc_ref_date);
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._sum_debt_value, 3, 0, 12, false, false, true, false, __formatNumberAmount);
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._sum_debt_balance, 3, 0, 12, false, false, true, false, __formatNumberAmount);
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._sum_debt_amount, 3, 0, 12, false, false, true, false, __formatNumberAmount, "", "", _g.d.ap_ar_trans_detail._ref_amount_debit);
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._final_amount, 3, 0, 12, false, false, true, false, __formatNumberAmount);

                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._sum_before_vat, 3, 0, 12, false, true, true, false, __formatNumberAmount);

                    //
                    this._transGrid._cellComboBoxGet += new MyLib.CellComboBoxItemGetDisplay(_transGrid__cellComboBoxGet);
                    this._transGrid._cellComboBoxItem += new MyLib.CellComboBoxItemEventHandler(_transGrid__cellComboBoxItem);
                    this.Controls.Add(this._transGrid);
                    break;
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น:
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._billing_no, 1, 0, 25, true, false, true, true, "", "", "", _g.d.ap_ar_trans_detail._doc_ref_purchase_order);
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._billing_date, 4, 0, 25, false, false, true, true, "", "", "", _g.d.ap_ar_trans_detail._doc_ref_purchase_order_date);
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._remark, 1, 0, 50, false, false);
                    this.Controls.Add(this._transGrid);
                    break;
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_ลดหนี้:
                    //
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._bill_type, 10, 25, 0, false, true, true);
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._billing_no, 1, 0, 40, true, false, true, true, "", "", "", _g.d.ap_ar_trans_detail._doc_ref);
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._billing_date, 4, 0, 12, false, false, true, true, "", "", "", _g.d.ap_ar_trans_detail._doc_ref_date);
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._sum_debt_value, 3, 0, 12, false, false, true, false, __formatNumberAmount);
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._sum_debt_balance, 3, 0, 12, false, false, true, false, __formatNumberAmount);
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._sum_debt_amount, 3, 0, 12, false, false, true, false, __formatNumberAmount, "", "", _g.d.ap_ar_trans_detail._ref_amount_credit);
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._final_amount, 3, 0, 12, false, false, true, false, __formatNumberAmount);

                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._sum_before_vat, 3, 0, 12, false, true, true, false, __formatNumberAmount);

                    //
                    this._transGrid._cellComboBoxGet += new MyLib.CellComboBoxItemGetDisplay(_transGrid__cellComboBoxGet);
                    this._transGrid._cellComboBoxItem += new MyLib.CellComboBoxItemEventHandler(_transGrid__cellComboBoxItem);
                    this.Controls.Add(this._transGrid);
                    break;
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_เพิ่มหนี้:
                    //
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._bill_type, 10, 25, 0, false, true, true);
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._billing_no, 1, 0, 40, true, false, true, true, "", "", "", _g.d.ap_ar_trans_detail._doc_ref);
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._billing_date, 4, 0, 12, false, false, true, true, "", "", "", _g.d.ap_ar_trans_detail._doc_ref_date);
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._sum_debt_value, 3, 0, 12, false, false, true, false, __formatNumberAmount);
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._sum_debt_balance, 3, 0, 12, false, false, true, false, __formatNumberAmount);
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._sum_debt_amount, 3, 0, 12, false, false, true, false, __formatNumberAmount, "", "", _g.d.ap_ar_trans_detail._ref_amount_credit);
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._final_amount, 3, 0, 12, false, false, true, false, __formatNumberAmount);

                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._sum_before_vat, 3, 0, 12, false, true, true, false, __formatNumberAmount);

                    //
                    this._transGrid._cellComboBoxGet += new MyLib.CellComboBoxItemGetDisplay(_transGrid__cellComboBoxGet);
                    this._transGrid._cellComboBoxItem += new MyLib.CellComboBoxItemEventHandler(_transGrid__cellComboBoxItem);
                    this.Controls.Add(this._transGrid);
                    break;
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น:
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._billing_no, 1, 0, 25, true, false, true, true, "", "", "", _g.d.ap_ar_trans_detail._doc_ref_purchase_order);
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._billing_date, 4, 0, 25, false, false, true, true, "", "", "", _g.d.ap_ar_trans_detail._doc_ref_purchase_order_date);
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._remark, 1, 0, 50, false, false);
                    this.Controls.Add(this._transGrid);
                    break;
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_ลดหนี้:
                    //
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._bill_type, 10, 25, 0, false, true, true);
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._billing_no, 1, 0, 40, true, false, true, true, "", "", "", _g.d.ap_ar_trans_detail._doc_ref);
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._billing_date, 4, 0, 12, false, false, true, true, "", "", "", _g.d.ap_ar_trans_detail._doc_ref_date);
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._sum_debt_value, 3, 0, 12, false, false, true, false, __formatNumberAmount);
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._sum_debt_balance, 3, 0, 12, false, false, true, false, __formatNumberAmount);
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._sum_debt_amount, 3, 0, 12, false, false, true, false, __formatNumberAmount, "", "", _g.d.ap_ar_trans_detail._ref_amount_credit);
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._final_amount, 3, 0, 12, false, false, true, false, __formatNumberAmount);

                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._sum_before_vat, 3, 0, 12, false, true, true, false, __formatNumberAmount);

                    //
                    this._transGrid._cellComboBoxGet += new MyLib.CellComboBoxItemGetDisplay(_transGrid__cellComboBoxGet);
                    this._transGrid._cellComboBoxItem += new MyLib.CellComboBoxItemEventHandler(_transGrid__cellComboBoxItem);
                    this.Controls.Add(this._transGrid);
                    break;
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_เพิ่มหนี้:
                    //
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._bill_type, 10, 25, 0, false, true, true);
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._billing_no, 1, 0, 40, true, false, true, true, "", "", "", _g.d.ap_ar_trans_detail._doc_ref);
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._billing_date, 4, 0, 12, false, false, true, true, "", "", "", _g.d.ap_ar_trans_detail._doc_ref_date);
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._sum_debt_value, 3, 0, 12, false, false, true, false, __formatNumberAmount);
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._sum_debt_balance, 3, 0, 12, false, false, true, false, __formatNumberAmount);
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._sum_debt_amount, 3, 0, 12, false, false, true, false, __formatNumberAmount, "", "", _g.d.ap_ar_trans_detail._ref_amount_credit);
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._final_amount, 3, 0, 12, false, false, true, false, __formatNumberAmount);

                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._sum_before_vat, 3, 0, 12, false, true, true, false, __formatNumberAmount);

                    //
                    this._transGrid._cellComboBoxGet += new MyLib.CellComboBoxItemGetDisplay(_transGrid__cellComboBoxGet);
                    this._transGrid._cellComboBoxItem += new MyLib.CellComboBoxItemEventHandler(_transGrid__cellComboBoxItem);
                    this.Controls.Add(this._transGrid);
                    break;
                case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ:
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._billing_no, 1, 0, 25, true, false, true, true, "", "", "", _g.d.ap_ar_trans_detail._doc_ref_purchase_order);
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._billing_date, 4, 0, 25, false, false, true, true, "", "", "", _g.d.ap_ar_trans_detail._doc_ref_purchase_order_date);
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._remark, 1, 0, 50, false, false);
                    this.Controls.Add(this._transGrid);
                    break;
                case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ตั้งหนี้:
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._billing_no, 1, 0, 25, true, false, true, true, "", "", "", _g.d.ap_ar_trans_detail._doc_ref_partial);
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._billing_date, 4, 0, 25, false, false, true, true, "", "", "", _g.d.ap_ar_trans_detail._doc_ref_partial_date);
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._remark, 1, 0, 50, false, false);
                    this.Controls.Add(this._transGrid);
                    break;
                case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_รับสินค้า:
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._billing_no, 1, 0, 25, true, false, true, true, "", "", "", _g.d.ap_ar_trans_detail._doc_ref_purchase_order);
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._billing_date, 4, 0, 25, false, false, true, true, "", "", "", _g.d.ap_ar_trans_detail._doc_ref_purchase_order_date);
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._remark, 1, 0, 50, false, false);
                    this.Controls.Add(this._transGrid);
                    this._mapRefLineNumberButton.Visible = true;
                    break;
                case _g.g._transControlTypeEnum.สินค้า_รับคืนสินค้าจากการเบิก:
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._billing_no, 1, 0, 25, true, false, true, true, "", "", "", _g.d.ap_ar_trans_detail._doc_ref_requisition);
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._billing_date, 4, 0, 25, false, false, true, true, "", "", "", _g.d.ap_ar_trans_detail._doc_ref_requisition_date);
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._remark, 1, 0, 50, false, false);
                    this.Controls.Add(this._transGrid);
                    break;
                case _g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ_ยกเลิก:
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._billing_no, 1, 0, 25, true, false, true, true, "", "", "", _g.d.ap_ar_trans_detail._doc_ref_requisition);
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._billing_date, 4, 0, 25, false, false, true, true, "", "", "", _g.d.ap_ar_trans_detail._doc_ref_requisition_date);
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._remark, 1, 0, 50, false, false);
                    this.Controls.Add(this._transGrid);
                    break;
                case _g.g._transControlTypeEnum.สินค้า_เบิกสินค้าวัตถุดิบ:
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._billing_no, 1, 0, 25, true, false, true, true, "", "", "", _g.d.ap_ar_trans_detail._doc_ref_pre_requisition);
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._billing_date, 4, 0, 25, false, false, true, true, "", "", "", _g.d.ap_ar_trans_detail._doc_date);
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._remark, 1, 0, 50, false, false);
                    this.Controls.Add(this._transGrid);
                    break;

                case _g.g._transControlTypeEnum.คลัง_รับฝาก_เบิก:
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._billing_no, 1, 0, 25, true, false, true, true, "", "", "", _g.d.ap_ar_trans_detail._doc_ref_product_deposit);
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._billing_date, 4, 0, 25, false, false, true, true, "", "", "", _g.d.ap_ar_trans_detail._doc_date);
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._remark, 1, 0, 50, false, false);
                    this.Controls.Add(this._transGrid);
                    break;
                case _g.g._transControlTypeEnum.คลัง_รับฝาก_รับคืนจากเบิก:
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._billing_no, 1, 0, 25, true, false, true, true, "", "", "", _g.d.ap_ar_trans_detail._doc_ref_requisition);
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._billing_date, 4, 0, 25, false, false, true, true, "", "", "", _g.d.ap_ar_trans_detail._doc_date);
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._remark, 1, 0, 50, false, false);
                    this.Controls.Add(this._transGrid);
                    break;
                case _g.g._transControlTypeEnum.สินค้า_โอนออก:
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._bill_type, 10, 15, 15, false, false);
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._billing_no, 1, 0, 25, true, false, true, true, "", "", "", _g.d.ap_ar_trans_detail._doc_ref);
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._billing_date, 4, 0, 25, false, false, true, true, "", "", "", _g.d.ap_ar_trans_detail._doc_date);
                    this._transGrid._addColumn(_g.d.ap_ar_trans_detail._remark, 1, 0, 50, false, false);

                    this._transGrid._cellComboBoxGet += new MyLib.CellComboBoxItemGetDisplay(_transGrid__cellComboBoxGet);
                    this._transGrid._cellComboBoxItem += new MyLib.CellComboBoxItemEventHandler(_transGrid__cellComboBoxItem);
                    this.Controls.Add(this._transGrid);
                    break;


            }
            this._transGrid._clickSearchButton += new MyLib.SearchEventHandler(_transGrid__clickSearchButton);
            this._transGrid._alterCellUpdate += new MyLib.AfterCellUpdateEventHandler(_transGrid__alterCellUpdate);
            this._transGrid._afterAddRow += new MyLib.AfterAddRowEventHandler(_transGrid__afterAddRow);
        }

        void _transGrid__afterAddRow(object sender, int row)
        {
            if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLColorStore)
            {
                switch (this._icTransControlType)
                {
                    case _g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้:
                    case _g.g._transControlTypeEnum.ขาย_เพิ่มหนี้:
                    case _g.g._transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด:
                    case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ลดหนี้:
                    case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด:
                    case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_เพิ่มหนี้:
                    case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ส่งคืนสินค้าหรือราคาผิด:
                        if (this._transGrid._rowData.Count > 1)
                        {
                            this._transGrid._rowData.RemoveAt(1);
                            this._transGrid._selectRow = 0;
                            return;
                        }
                        break;
                }
            }
            switch (this._icTransControlType)
            {
                case _g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้:
                case _g.g._transControlTypeEnum.ขาย_เพิ่มหนี้:
                case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด:
                case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_เพิ่มหนี้:
                case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ส่งคืนสินค้าหรือราคาผิด:
                case _g.g._transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด:
                case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ลดหนี้:
                case _g.g._transControlTypeEnum.เจ้าหนี้_ลดหนี้อื่น:
                case _g.g._transControlTypeEnum.เจ้าหนี้_เพิ่มหนี้อื่น:
                    this._transGrid._cellUpdate(row, _g.d.ap_ar_trans_detail._bill_type, 1, false);
                    break;                    
            }

            if (_g.g._companyProfile._ss_ref_po_only)
            {
                if (this._icTransControlType == _g.g._transControlTypeEnum.ขาย_สั่งขาย)
                {
                    this._transGrid._cellUpdate(row, _g.d.ap_ar_trans_detail._bill_type, 1, false);
                }

            }

        }

        object[] _transGrid__cellComboBoxItem(object sender, int row, int column)
        {
            switch (this._icTransControlType)
            {
                case _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ: return _g.g._so_sale_order_type_2;
                case _g.g._transControlTypeEnum.ขาย_สั่งขาย: return _g.g._so_sale_order_type_1;
                case _g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้: return _g.g._so_sale_order_type_3;
                case _g.g._transControlTypeEnum.ขาย_เพิ่มหนี้: return _g.g._so_sale_order_type_4;
                case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด: return _g.g._ap_bill_type_2;
                case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_เพิ่มหนี้: return _g.g._ap_bill_type_4;
                case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ส่งคืนสินค้าหรือราคาผิด: return _g.g._ap_bill_type_3;
                case _g.g._transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด: return _g.g._ap_bill_type_1;
                case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ลดหนี้: return _g.g._ap_bill_type_4;
                case _g.g._transControlTypeEnum.เจ้าหนี้_ลดหนี้อื่น: return _g.g._ap_bill_type_1;
                case _g.g._transControlTypeEnum.เจ้าหนี้_เพิ่มหนี้อื่น: return _g.g._ap_bill_type_1;
                case _g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ: return _g.g._po_bill_type_1;
                case _g.g._transControlTypeEnum.สินค้า_โอนออก: return _g.g._transfer_out_bill_type;
            }
            return null;
        }

        string _transGrid__cellComboBoxGet(object sender, int row, int column, string columnName, int select)
        {
            switch (this._icTransControlType)
            {
                case _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ: return _g.g._so_sale_order_type_2[(select == -1) ? 0 : select].ToString();
                case _g.g._transControlTypeEnum.ขาย_สั่งขาย: return _g.g._so_sale_order_type_1[(select == -1) ? 0 : select].ToString();
                case _g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้: return _g.g._so_sale_order_type_3[(select == -1) ? 0 : select].ToString();
                case _g.g._transControlTypeEnum.ขาย_เพิ่มหนี้: return _g.g._so_sale_order_type_4[(select == -1) ? 0 : select].ToString();
                case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด: return _g.g._ap_bill_type_2[(select == -1) ? 0 : select].ToString();
                case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_เพิ่มหนี้: return _g.g._ap_bill_type_4[(select == -1) ? 0 : select].ToString();
                case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ส่งคืนสินค้าหรือราคาผิด: return _g.g._ap_bill_type_3[(select == -1) ? 0 : select].ToString();
                case _g.g._transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด: return _g.g._ap_bill_type_1[(select == -1) ? 0 : select].ToString();
                case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ลดหนี้: return _g.g._ap_bill_type_4[(select == -1) ? 0 : select].ToString();
                case _g.g._transControlTypeEnum.เจ้าหนี้_ลดหนี้อื่น: return _g.g._ap_bill_type_1[(select == -1) ? 0 : select].ToString();
                case _g.g._transControlTypeEnum.เจ้าหนี้_เพิ่มหนี้อื่น: return _g.g._ap_bill_type_1[(select == -1) ? 0 : select].ToString();
                case _g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ:
                    {
                        if (select == 99)
                        {
                            return "ใบสั่งซื้อ/สั่งจอง";
                        }
                        return _g.g._po_bill_type_1[(select == -1) ? 0 : select].ToString();
                    }
                case _g.g._transControlTypeEnum.สินค้า_โอนออก: return _g.g._transfer_out_bill_type[(select == -1) ? 0 : select].ToString();

            }
            return null;
        }

        public DateTime _getDocDateRef()
        {
            DateTime __result = new DateTime();

            __result = (DateTime)this._transGrid._cellGet(0, _g.d.ap_ar_trans_detail._billing_date);
            return __result;
        }

        public String _getDocRef()
        {
            ArrayList __buildList = new ArrayList();
            StringBuilder __result = new StringBuilder();
            int __columnNumber = this._transGrid._findColumnByName(_g.d.ap_ar_trans_detail._billing_no);
            int __billTypeColumnNumber = this._transGrid._findColumnByName(_g.d.ap_ar_trans_detail._bill_type);
            for (int __loop1 = 0; __loop1 < this._transGrid._rowData.Count; __loop1++)
            {
                Boolean __found = false;
                int __getDocType = -1;
                if (__billTypeColumnNumber != -1)
                {
                    __getDocType = MyLib._myGlobal._intPhase(this._transGrid._cellGet(__loop1, __billTypeColumnNumber).ToString());
                }
                //if (__getDocType == -1 || docType == -1 || __getDocType == docType)
                //{
                string __docNo = this._transGrid._cellGet(__loop1, __columnNumber).ToString();
                for (int __loop2 = 0; __loop2 < __buildList.Count; __loop2++)
                {
                    if (__buildList[__loop2].ToString().Equals(__docNo))
                    {
                        __found = true;
                        break;
                    }
                }
                if (__found == false)
                {
                    if (__result.Length > 0)
                    {
                        __result.Append(",");
                    }
                    __result.Append(__docNo);
                    __buildList.Add(__docNo.ToString());
                }
                //}
            }
            return __result.ToString();
        }

        /// <summary>
        /// Pack เอกสารอ้างอิง
        /// </summary>
        /// <param name="docType">หมายเลขจาก Combobox</param>
        /// <returns></returns>
        public String _getDocRefPackForQuery(int docType)
        {
            ArrayList __buildList = new ArrayList();
            StringBuilder __result = new StringBuilder();
            int __columnNumber = this._transGrid._findColumnByName(_g.d.ap_ar_trans_detail._billing_no);
            int __billTypeColumnNumber = this._transGrid._findColumnByName(_g.d.ap_ar_trans_detail._bill_type);
            for (int __loop1 = 0; __loop1 < this._transGrid._rowData.Count; __loop1++)
            {
                Boolean __found = false;
                int __getDocType = -1;
                if (__billTypeColumnNumber != -1)
                {
                    __getDocType = MyLib._myGlobal._intPhase(this._transGrid._cellGet(__loop1, __billTypeColumnNumber).ToString());
                }
                if (__getDocType == -1 || docType == -1 || __getDocType == docType)
                {
                    string __docNo = this._transGrid._cellGet(__loop1, __columnNumber).ToString();
                    for (int __loop2 = 0; __loop2 < __buildList.Count; __loop2++)
                    {
                        if (__buildList[__loop2].ToString().Equals(__docNo))
                        {
                            __found = true;
                            break;
                        }
                    }
                    if (__found == false)
                    {
                        if (__result.Length > 0)
                        {
                            __result.Append(",");
                        }
                        __result.Append("\'" + __docNo + "\'");
                        __buildList.Add(__docNo.ToString());
                    }
                }
            }
            return __result.ToString();
        }

        void _transGrid__alterCellUpdate(object sender, int row, int column)
        {
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            string __docRemark = "";
            DateTime __docDate = new DateTime(1000, 1, 1);
            string __docRef = "";
            DateTime __docRefDate = new DateTime(1000, 1, 1);

            //
            MyLib._myGrid __sender = (MyLib._myGrid)sender;
            int __billNoColumnNumber = __sender._findColumnByName(_g.d.ap_ar_trans_detail._billing_no);
            int __billTypeColumnNumber = __sender._findColumnByName(_g.d.ap_ar_trans_detail._bill_type);
            if (__billNoColumnNumber != -1)
            {
                decimal __amount = 0.0M;
                decimal __balance = 0.0M;
                decimal __beforeVat = 0.0M;
                string __billNo = __sender._cellGet(row, __billNoColumnNumber).ToString();
                int __billType = -1;
                if (__billTypeColumnNumber != -1)
                {
                    __billType = MyLib._myGlobal._intPhase(__sender._cellGet(row, __billTypeColumnNumber).ToString());
                }
                if (__billNo.Length > 0)
                {
                    int __addr = -1;
                    for (int __row = 0; __row < __sender._rowData.Count; __row++)
                    {
                        int __billTypeCompare = -1;
                        if (__billTypeColumnNumber != -1)
                        {
                            __billTypeCompare = MyLib._myGlobal._intPhase(__sender._cellGet(__row, __billTypeColumnNumber).ToString());
                        }
                        string __billNoCompare = __sender._cellGet(__row, __billNoColumnNumber).ToString().ToUpper();
                        if (__billNo.Equals(__billNoCompare.ToUpper()) && (__billTypeColumnNumber == -1 || __billType == __billTypeCompare))
                        {
                            __addr = __row;
                            break;
                        }
                    }

                    if (__addr != row && __addr != -1)
                    {
                        MessageBox.Show(MyLib._myGlobal._resource("เอกสารซ้ำ"));
                        this._transGrid._cellUpdate(row, __billNoColumnNumber, "", true);
                    }
                    else
                    {
                        string __transTableName = _g.d.ic_trans._table;

                        // ดึงยอดคงเหลือ
                        StringBuilder __queryBalance = new StringBuilder();
                        switch (this._icTransControlType)
                        {
                            case _g.g._transControlTypeEnum.ขาย_เพิ่มหนี้:
                            case _g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้:
                                {
                                    StringBuilder __transFlag = new StringBuilder();
                                    __transFlag.Append(_g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้));
                                    __transFlag.Append(",");
                                    __transFlag.Append(_g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_เพิ่มหนี้));
                                    //
                                    __queryBalance.Append(",");

                                    if (MyLib._myGlobal._OEMVersion.Equals("SINGHA") && this._icTransControlType == _g.g._transControlTypeEnum.ขาย_เพิ่มหนี้)
                                    {
                                        __queryBalance.Append(_g.d.ic_trans._total_amount + " as balance");
                                    }
                                    else
                                        __queryBalance.Append(_g.d.ic_trans._total_amount + "+coalesce((select sum(sum_debt_amount*case when ap_ar_trans_detail.trans_flag <> " + _g.g._transFlagGlobal._transFlag(this._icTransControlType) + " then 1 else calc_flag end) from ap_ar_trans_detail where ap_ar_trans_detail.doc_no<>\'" + this._docNo + "\' and ap_ar_trans_detail.billing_no=ic_trans.doc_no and ap_ar_trans_detail.trans_flag in (" + __transFlag.ToString() + ") and ap_ar_trans_detail.last_status=0),0) as balance");
                                }
                                break;
                            case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด:
                            case _g.g._transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด:
                                {
                                    StringBuilder __transFlag = new StringBuilder();
                                    __transFlag.Append(_g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด));
                                    __transFlag.Append(",");
                                    __transFlag.Append(_g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด));
                                    //
                                    __queryBalance.Append(",");
                                    __queryBalance.Append(_g.d.ic_trans._total_amount + "+coalesce((select sum(sum_debt_amount*calc_flag) from ap_ar_trans_detail where ap_ar_trans_detail.billing_no<>\'" + this._docNo + "\' and ap_ar_trans_detail.billing_no=ic_trans.doc_no and ap_ar_trans_detail.trans_flag in (" + __transFlag.ToString() + ") and ap_ar_trans_detail.last_status=0),0) as balance");
                                }
                                break;
                            case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_เพิ่มหนี้:
                            case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ลดหนี้:
                                {
                                    StringBuilder __transFlag = new StringBuilder();
                                    __transFlag.Append(_g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_พาเชียล_ลดหนี้));
                                    __transFlag.Append(",");
                                    __transFlag.Append(_g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_พาเชียล_เพิ่มหนี้));
                                    //
                                    __queryBalance.Append(",");
                                    __queryBalance.Append(_g.d.ic_trans._total_amount + "+coalesce((select sum(sum_debt_amount*calc_flag) from ap_ar_trans_detail where ap_ar_trans_detail.billing_no<>\'" + this._docNo + "\' and ap_ar_trans_detail.billing_no=ic_trans.doc_no and ap_ar_trans_detail.trans_flag in (" + __transFlag.ToString() + ") and ap_ar_trans_detail.last_status=0),0) as balance");
                                }
                                break;
                            case _g.g._transControlTypeEnum.เจ้าหนี้_ลดหนี้อื่น:
                                {
                                    StringBuilder __transFlag = new StringBuilder();
                                    __transFlag.Append(_g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เจ้าหนี้_ตั้งหนี้อื่น));
                                    //
                                    __queryBalance.Append(",");
                                    __queryBalance.Append(_g.d.ic_trans._total_amount + "+coalesce((select sum(sum_debt_amount*calc_flag) from ap_ar_trans_detail where ap_ar_trans_detail.billing_no<>\'" + this._docNo + "\' and ap_ar_trans_detail.billing_no=ic_trans.doc_no and ap_ar_trans_detail.trans_flag in (" + __transFlag.ToString() + ") and ap_ar_trans_detail.last_status=0),0) as balance");
                                }
                                break;
                            case _g.g._transControlTypeEnum.เจ้าหนี้_เพิ่มหนี้อื่น:
                                {
                                    StringBuilder __transFlag = new StringBuilder();
                                    __transFlag.Append(_g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เจ้าหนี้_ตั้งหนี้อื่น));
                                    //
                                    __queryBalance.Append(",");
                                    __queryBalance.Append(_g.d.ic_trans._total_amount + "+coalesce((select sum(sum_debt_amount*calc_flag) from ap_ar_trans_detail where ap_ar_trans_detail.billing_no<>\'" + this._docNo + "\' and ap_ar_trans_detail.billing_no=ic_trans.doc_no and ap_ar_trans_detail.trans_flag in (" + __transFlag.ToString() + ") and ap_ar_trans_detail.last_status=0),0) as balance");
                                }
                                break;
                            case _g.g._transControlTypeEnum.ลูกหนี้_ลดหนี้อื่น:
                                {
                                    StringBuilder __transFlag = new StringBuilder();
                                    __transFlag.Append(_g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ลูกหนี้_ตั้งหนี้อื่น));
                                    //
                                    __queryBalance.Append(",");
                                    __queryBalance.Append(_g.d.ic_trans._total_amount + "+coalesce((select sum(sum_debt_amount*calc_flag) from ap_ar_trans_detail where ap_ar_trans_detail.billing_no<>\'" + this._docNo + "\' and ap_ar_trans_detail.billing_no=ic_trans.doc_no and ap_ar_trans_detail.trans_flag in (" + __transFlag.ToString() + ") and ap_ar_trans_detail.last_status=0),0) as balance");
                                }
                                break;
                            case _g.g._transControlTypeEnum.ลูกหนี้_เพิ่มหนี้อื่น:
                                {
                                    StringBuilder __transFlag = new StringBuilder();
                                    __transFlag.Append(_g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ลูกหนี้_ตั้งหนี้อื่น));
                                    //
                                    __queryBalance.Append(",");
                                    __queryBalance.Append(_g.d.ic_trans._total_amount + "+coalesce((select sum(sum_debt_amount*calc_flag) from ap_ar_trans_detail where ap_ar_trans_detail.billing_no<>\'" + this._docNo + "\' and ap_ar_trans_detail.billing_no=ic_trans.doc_no and ap_ar_trans_detail.trans_flag in (" + __transFlag.ToString() + ") and ap_ar_trans_detail.last_status=0),0) as balance");
                                }
                                break;
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_ลดหนี้:
                                {
                                    StringBuilder __transFlag = new StringBuilder();
                                    __transFlag.Append(_g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น));
                                    //
                                    __queryBalance.Append(",");
                                    __queryBalance.Append(_g.d.ic_trans._total_amount + "+coalesce((select sum(sum_debt_amount*calc_flag) from ap_ar_trans_detail where ap_ar_trans_detail.billing_no<>\'" + this._docNo + "\' and ap_ar_trans_detail.billing_no=ic_trans.doc_no and ap_ar_trans_detail.trans_flag in (" + __transFlag.ToString() + ") and ap_ar_trans_detail.last_status=0),0) as balance");
                                }
                                break;
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_เพิ่มหนี้:
                                {
                                    StringBuilder __transFlag = new StringBuilder();
                                    __transFlag.Append(_g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น));
                                    //
                                    __queryBalance.Append(",");
                                    __queryBalance.Append(_g.d.ic_trans._total_amount + "+coalesce((select sum(sum_debt_amount*calc_flag) from ap_ar_trans_detail where ap_ar_trans_detail.billing_no<>\'" + this._docNo + "\' and ap_ar_trans_detail.billing_no=ic_trans.doc_no and ap_ar_trans_detail.trans_flag in (" + __transFlag.ToString() + ") and ap_ar_trans_detail.last_status=0),0) as balance");
                                }
                                break;
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_ลดหนี้:
                                {
                                    StringBuilder __transFlag = new StringBuilder();
                                    __transFlag.Append(_g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น));
                                    //
                                    __queryBalance.Append(",");
                                    __queryBalance.Append(_g.d.ic_trans._total_amount + "+coalesce((select sum(sum_debt_amount*calc_flag) from ap_ar_trans_detail where ap_ar_trans_detail.billing_no<>\'" + this._docNo + "\' and ap_ar_trans_detail.billing_no=ic_trans.doc_no and ap_ar_trans_detail.trans_flag in (" + __transFlag.ToString() + ") and ap_ar_trans_detail.last_status=0),0) as balance");
                                }
                                break;
                            case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_เพิ่มหนี้:
                                {
                                    StringBuilder __transFlag = new StringBuilder();
                                    __transFlag.Append(_g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น));
                                    //
                                    __queryBalance.Append(",");
                                    __queryBalance.Append(_g.d.ic_trans._total_amount + "+coalesce((select sum(sum_debt_amount*calc_flag) from ap_ar_trans_detail where ap_ar_trans_detail.billing_no<>\'" + this._docNo + "\' and ap_ar_trans_detail.billing_no=ic_trans.doc_no and ap_ar_trans_detail.trans_flag in (" + __transFlag.ToString() + ") and ap_ar_trans_detail.last_status=0),0) as balance");
                                }
                                break;

                            case _g.g._transControlTypeEnum.คลัง_รับฝาก_รับคืนจากเบิก:
                            case _g.g._transControlTypeEnum.คลัง_รับฝาก_เบิก:
                                __transTableName = _g.d.ic_wms_trans._table;
                                __queryBalance.Append(",0 as balance");
                                break;
                            default:
                                {
                                    __queryBalance.Append(",0 as balance");
                                }
                                break;
                        }

                        string __query = "select " + _g.d.ic_trans._doc_date + "," + _g.d.ic_trans._total_amount + "," + _g.d.ic_trans._remark + __queryBalance.ToString() + "," + _g.d.ic_trans._total_before_vat + "," + _g.d.ic_trans._doc_ref +
                            "," + _g.d.ic_trans._doc_ref_date + "," + _g.d.ic_trans._approve_status +
                            ", " + _g.d.ic_trans._expire_date +
                            " from " + __transTableName + " where " + _g.d.ic_trans._doc_no + "=\'" + __billNo + "\'";
                        DataSet __result = __myFrameWork._query(MyLib._myGlobal._databaseName, __query);
                        if (__result.Tables[0].Rows.Count > 0)
                        {
                            __docRemark = __result.Tables[0].Rows[0][_g.d.ic_trans._remark].ToString();
                            __docDate = MyLib._myGlobal._convertDateFromQuery(__result.Tables[0].Rows[0][_g.d.ic_trans._doc_date].ToString());
                            __amount = MyLib._myGlobal._decimalPhase(__result.Tables[0].Rows[0][_g.d.ic_trans._total_amount].ToString());
                            __balance = MyLib._myGlobal._decimalPhase(__result.Tables[0].Rows[0]["balance"].ToString());
                            __beforeVat = MyLib._myGlobal._decimalPhase(__result.Tables[0].Rows[0][_g.d.ic_trans._total_before_vat].ToString());

                            __docRef = __result.Tables[0].Rows[0][_g.d.ic_trans._doc_ref].ToString();
                            __docRefDate = MyLib._myGlobal._convertDateFromQuery(__result.Tables[0].Rows[0][_g.d.ic_trans._doc_ref_date].ToString());

                            // เช็ค อ้างอิง PO ที่อนุมัติแล้ว
                            if (_g.g._companyProfile._ref_po_approve && (this._icTransControlType == _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ || this._icTransControlType == _g.g._transControlTypeEnum.ซื้อ_พาเชียล_รับสินค้า))
                            {
                                int approveStatus = MyLib._myGlobal._intPhase(__result.Tables[0].Rows[0][_g.d.ic_trans._approve_status].ToString());
                                if (approveStatus != 1)
                                {
                                    MessageBox.Show(MyLib._myGlobal._resource("เอกสารสั่งซื้อไม่อนุมัติ"));
                                    this._transGrid._cellUpdate(row, __billNoColumnNumber, "", true);
                                }
                            }
                            // เช็คใบเสนอราคาหมดอายุ
                            else if (_g.g._companyProfile._quotation_expire == true && (
                                ((this._icTransControlType == _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ || this._icTransControlType == _g.g._transControlTypeEnum.ขาย_สั่งขาย) && __billType == 1) ||
                                this._icTransControlType == _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า))
                            {
                                // เช็คใบเสนอราคาหมดอายุ
                                DateTime __getExpireDate = MyLib._myGlobal._convertDateFromQuery(__result.Tables[0].Rows[0][_g.d.ic_trans._expire_date].ToString());

                                if (__getExpireDate < DateTime.Today)
                                {
                                    MessageBox.Show(MyLib._myGlobal._resource("ใบเสนอราคาหมดอายุ"));
                                    this._transGrid._cellUpdate(row, __billNoColumnNumber, "", true);
                                    __docRemark = "";
                                    __docDate = new DateTime(1000, 1, 1);
                                    __amount = 0M;
                                    __balance = 0M;
                                    __docRef = "";
                                    __docRefDate = new DateTime(1000, 1, 1);
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show(MyLib._myGlobal._resource("ไม่พบเอกสาร"));
                            this._transGrid._cellUpdate(row, __billNoColumnNumber, "", true);
                        }
                    }
                    // ตรวจสอบเอกสารยกเลิก,เอกสารอ้างอิงครบแล้ว
                    switch (this._icTransControlType)
                    {
                        case _g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ:
                            {
                                if (_g.g._companyProfile._pr_approve_lock)
                                {
                                    string __query = "select " + _g.d.ic_trans._last_status + "," + _g.d.ic_trans._doc_success + " from " + _g.d.ic_trans._table + " where " + _g.d.ic_trans._doc_no + "=\'" + __billNo + "\'";
                                    DataTable __getData = __myFrameWork._queryShort(__query).Tables[0];
                                    if (__getData.Rows.Count > 0)
                                    {
                                        int __lastStatus = MyLib._myGlobal._intPhase(__getData.Rows[0][_g.d.ic_trans._last_status].ToString());
                                        if (__lastStatus == 1)
                                        {
                                            MessageBox.Show(MyLib._myGlobal._resource("เอกสารยกเลิกไม่สามารถอ้างอิงได้"));
                                            this._transGrid._cellUpdate(row, __billNoColumnNumber, "", true);
                                        }
                                        else
                                        {
                                            // เอกสารใช้ไปหมดแล้ว
                                            int __sucessStatus = MyLib._myGlobal._intPhase(__getData.Rows[0][_g.d.ic_trans._doc_success].ToString());
                                            if (__sucessStatus == 1)
                                            {
                                                //MessageBox.Show(MyLib._myGlobal._resource("เอกสารไม่สามารถอ้างอิงได้เพราะมีการนำไปอ้างอิงแล้ว"));
                                                MessageBox.Show(MyLib._myGlobal._resource("เอกสารไม่สามารถอ้างอิงได้"));
                                                this._transGrid._cellUpdate(row, __billNoColumnNumber, "", true);
                                            }
                                        }
                                    }
                                }
                            }
                            break;
                        case _g.g._transControlTypeEnum.เจ้าหนี้_ตั้งหนี้อื่น:
                        case _g.g._transControlTypeEnum.เจ้าหนี้_ลดหนี้อื่น:
                        case _g.g._transControlTypeEnum.เจ้าหนี้_เพิ่มหนี้อื่น:
                        case _g.g._transControlTypeEnum.ลูกหนี้_ตั้งหนี้อื่น:
                        case _g.g._transControlTypeEnum.ลูกหนี้_ลดหนี้อื่น:
                        case _g.g._transControlTypeEnum.ลูกหนี้_เพิ่มหนี้อื่น:
                        case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น:
                        case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_เพิ่มหนี้:
                        case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_ลดหนี้:
                        case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น:
                        case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_เพิ่มหนี้:
                        case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_ลดหนี้:
                        case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ:
                        case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ตั้งหนี้:
                        case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_รับสินค้า:
                        case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด:
                        case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_เพิ่มหนี้:
                        case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ส่งคืนสินค้าหรือราคาผิด:
                        case _g.g._transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด:
                        case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ลดหนี้:
                        case _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า:
                        case _g.g._transControlTypeEnum.ขาย_สั่งขาย:
                        case _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ:
                        case _g.g._transControlTypeEnum.ขาย_ใบกำกับภาษีอย่างเต็มออกแทน:
                        case _g.g._transControlTypeEnum.ขาย_เพิ่มหนี้:
                        case _g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้:
                            {
                                string __query = "select " + _g.d.ic_trans._last_status + "," + _g.d.ic_trans._doc_success + "," + _g.d.ic_trans._approve_status + " from " + _g.d.ic_trans._table + " where " + _g.d.ic_trans._doc_no + "=\'" + __billNo + "\' ";
                                DataTable __getData = __myFrameWork._queryShort(__query).Tables[0];
                                if (__getData.Rows.Count > 0)
                                {
                                    int __lastStatus = MyLib._myGlobal._intPhase(__getData.Rows[0][_g.d.ic_trans._last_status].ToString());
                                    if (__lastStatus == 1)
                                    {
                                        MessageBox.Show(MyLib._myGlobal._resource("เอกสารยกเลิกไม่สามารถอ้างอิงได้"));
                                        this._transGrid._cellUpdate(row, __billNoColumnNumber, "", true);
                                    }
                                    else
                                    {
                                        // เอกสารใช้ไปหมดแล้ว
                                        int __sucessStatus = MyLib._myGlobal._intPhase(__getData.Rows[0][_g.d.ic_trans._doc_success].ToString());
                                        if (__sucessStatus == 1)
                                        {
                                            MessageBox.Show(MyLib._myGlobal._resource("เอกสารไม่สามารถอ้างอิงได้"));
                                            this._transGrid._cellUpdate(row, __billNoColumnNumber, "", true);
                                        }
                                    }

                                    if (_g.g._companyProfile._sr_ss_credit_check && this._icTransControlType == _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ && (__billType == 2 || __billType == 3))
                                    {
                                        // __extraWhere += " and " + _g.d.ic_trans._approve_status + "=1 ";
                                        int __approvedStatus = MyLib._myGlobal._intPhase(__getData.Rows[0][_g.d.ic_trans._approve_status].ToString());
                                        if (__approvedStatus != 1)
                                        {
                                            MessageBox.Show(MyLib._myGlobal._resource("เอกสารยังไม่อนุมัติ"));
                                            this._transGrid._cellUpdate(row, __billNoColumnNumber, "", true);
                                        }

                                    }
                                }
                            }
                            break;
                        case _g.g._transControlTypeEnum.คลัง_รับฝาก_เบิก:
                        case _g.g._transControlTypeEnum.คลัง_รับฝาก_รับคืนจากเบิก:
                            {
                                string __query = "select " + _g.d.ic_trans._last_status + "," + _g.d.ic_trans._doc_success + " from " + _g.d.ic_wms_trans._table + " where " + _g.d.ic_trans._doc_no + "=\'" + __billNo + "\' ";
                                DataTable __getData = __myFrameWork._queryShort(__query).Tables[0];
                                if (__getData.Rows.Count > 0)
                                {
                                    int __lastStatus = MyLib._myGlobal._intPhase(__getData.Rows[0][_g.d.ic_trans._last_status].ToString());
                                    if (__lastStatus == 1)
                                    {
                                        MessageBox.Show(MyLib._myGlobal._resource("เอกสารยกเลิกไม่สามารถอ้างอิงได้"));
                                        this._transGrid._cellUpdate(row, __billNoColumnNumber, "", true);
                                    }
                                    else
                                    {
                                        // เอกสารใช้ไปหมดแล้ว
                                        int __sucessStatus = MyLib._myGlobal._intPhase(__getData.Rows[0][_g.d.ic_trans._doc_success].ToString());
                                        if (__sucessStatus == 1)
                                        {
                                            MessageBox.Show(MyLib._myGlobal._resource("เอกสารไม่สามารถอ้างอิงได้"));
                                            this._transGrid._cellUpdate(row, __billNoColumnNumber, "", true);
                                        }
                                    }
                                }
                            }
                            break;
                    }
                    //
                }
                __sender._cellUpdate(row, _g.d.ap_ar_trans_detail._billing_date, __docDate, false);
                __sender._cellUpdate(row, _g.d.ap_ar_trans_detail._remark, __docRemark, false);
                __sender._cellUpdate(row, _g.d.ap_ar_trans_detail._sum_debt_value, __amount, false);
                __sender._cellUpdate(row, _g.d.ap_ar_trans_detail._sum_debt_balance, __balance, false);
                __sender._cellUpdate(row, _g.d.ap_ar_trans_detail._sum_before_vat, __beforeVat, false);

                __sender._cellUpdate(row, _g.d.ap_ar_trans_detail._ref_doc_no, __docRef, false);
                __sender._cellUpdate(row, _g.d.ap_ar_trans_detail._ref_doc_date, __docRefDate, false);
            }
            __sender.Invalidate();
        }

        void _transGrid__clickSearchButton(object sender, MyLib.GridCellEventArgs e)
        {
            int __columnNumber = this._transGrid._findColumnByName(_g.d.ap_ar_trans_detail._billing_no);
            int __billTypeColumn = this._transGrid._findColumnByName(_g.d.ap_ar_trans_detail._bill_type);
            int __selectTransFlag = (__billTypeColumn == -1) ? 0 : MyLib._myGlobal._intPhase(this._transGrid._cellGet(this._transGrid._selectRow, __billTypeColumn).ToString());
            _displayIcTransSearch(((MyLib._myGrid._columnType)this._transGrid._columnList[__columnNumber])._name, this._custCode, sender, false, __selectTransFlag, this._vatTypeNumber());
        }

        public void _displayIcTransSearch(string name, string cust_code, object owner, Boolean whereDocNo, int selectTransFlag, int vatType)
        {
            // ค้นหาเอกสารอ้างอิง ตามประเภทที่สัมพันธ์กัน
            this._icTransSearch = new MyLib._searchDataFull();
            this._icTransSearch._dataList._gridData._beforeDisplayRow += new MyLib.BeforeDisplayRowEventHandler(_gridData__beforeDisplayRow);
            this._icTransSearch._owner = owner;
            this._icTransSearch.StartPosition = FormStartPosition.CenterScreen;
            this._icTransSearch.Text = name;
            this._icTransSearch._name = name;
            string __templateName = "";
            int __icTransFlag = 0;
            string __extraWhere = "";

            string __transTableName = _g.d.ic_trans._table;

            switch (this._icTransControlType)
            {
                case _g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ:
                    switch (selectTransFlag)
                    {
                        case 0:
                            __templateName = _g.g._transGlobalTemplate._transTemplate(_g.g._transControlTypeEnum.ซื้อ_เสนอซื้อ_อนุมัติ);
                            __icTransFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_เสนอซื้อ_อนุมัติ);
                            __extraWhere = " and " + _g.d.ic_trans._last_status + "=0 and " + _g.d.ic_trans._doc_success + "=0";
                            break;
                        case 1:
                            __templateName = _g.g._transGlobalTemplate._transTemplate(_g.g._transControlTypeEnum.ซื้อ_เสนอซื้อ);
                            __icTransFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_เสนอซื้อ);
                            __extraWhere = " and " + _g.d.ic_trans._last_status + "=0 "; // and " + _g.d.ic_trans._doc_success + "=0";
                            break;
                    }
                    break;
                case _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ:
                case _g.g._transControlTypeEnum.ขาย_ใบกำกับภาษีอย่างเต็มออกแทน:
                    switch (selectTransFlag)
                    {
                        case 1: // ใบเสนอราคา
                            __templateName = _g.g._transGlobalTemplate._transTemplate(_g.g._transControlTypeEnum.ขาย_เสนอราคา);
                            __icTransFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_เสนอราคา);
                            break;
                        case 2: // ใบสั่งซื้อ/ใบสั่งจอง
                            __templateName = _g.g._transGlobalTemplate._transTemplate(_g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า);
                            __icTransFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า);
                            break;
                        case 3: // ใบสั่งขาย
                            __templateName = _g.g._transGlobalTemplate._transTemplate(_g.g._transControlTypeEnum.ขาย_สั่งขาย);
                            __icTransFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_สั่งขาย);
                            break;
                    }
                    __extraWhere = " and " + _g.d.ic_trans._cust_code + "=\'" + cust_code + "\'" + " and " + _g.d.ic_trans._doc_success + "=0 ";

                    if (_g.g._companyProfile._sr_ss_credit_check && (selectTransFlag == 2 || selectTransFlag == 3))
                    {
                        __extraWhere += " and " + _g.d.ic_trans._approve_status + "=1 ";
                    }
                    break;
                case _g.g._transControlTypeEnum.ขาย_สั่งขาย:
                    switch (selectTransFlag)
                    {
                        case 1: // ใบเสนอราคา
                            __templateName = _g.g._transGlobalTemplate._transTemplate(_g.g._transControlTypeEnum.ขาย_เสนอราคา);
                            __icTransFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_เสนอราคา);
                            break;
                        case 2: // ใบสั่งซื้อ,ใบสั่งจอง
                            __templateName = _g.g._transGlobalTemplate._transTemplate(_g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า);
                            __icTransFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า);
                            break;
                    }
                    __extraWhere = " and " + _g.d.ic_trans._cust_code + "=\'" + cust_code + "\'";
                    break;
                case _g.g._transControlTypeEnum.ขาย_สั่งจองและสั่งซื้อสินค้า:
                    __templateName = _g.g._transGlobalTemplate._transTemplate(_g.g._transControlTypeEnum.ขาย_เสนอราคา);
                    __icTransFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_เสนอราคา);
                    __extraWhere = " and " + _g.d.ic_trans._cust_code + "=\'" + cust_code + "\'";
                    break;
                case _g.g._transControlTypeEnum.ขาย_เพิ่มหนี้:
                    __templateName = _g.g._transGlobalTemplate._transTemplate(_g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ);
                    __icTransFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ);
                    __extraWhere = " and " + _g.d.ic_trans._cust_code + "=\'" + cust_code + "\' and " + _g.d.ic_trans._vat_type + "=" + vatType.ToString();
                    break;
                case _g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้:

                    switch (selectTransFlag)
                    {
                        case 1: // ขาย
                            __templateName = _g.g._transGlobalTemplate._transTemplate(_g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ);
                            __icTransFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ);

                            __extraWhere = " and " + _g.d.ic_trans._cust_code + "=\'" + cust_code + "\' and " + _g.d.ic_trans._vat_type + "=" + vatType.ToString();
                            break;
                        case 2: // ตั้งหนี้ยกมา
                            __templateName = "screen_ar_credit_balance"; // _g.g._arapLoadViewGlobal._loadViewName(_g.g._transControlTypeEnum.ลูกหนี้_ตั้งหนี้ยกมา, 0);
                            __icTransFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ลูกหนี้_ตั้งหนี้ยกมา);
                            __extraWhere = " and " + _g.d.ic_trans._cust_code + "=\'" + cust_code + "\' "; // and " + _g.d.ic_trans._vat_type + "=" + vatType.ToString();
                            break;
                    }

                    break;
                case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าเพิ่มหนี้หรือราคาผิด:
                    __templateName = _g.g._transGlobalTemplate._transTemplate(_g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ);
                    __icTransFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ);
                    __extraWhere = " and " + _g.d.ic_trans._cust_code + "=\'" + cust_code + "\' and " + _g.d.ic_trans._vat_type + "=" + vatType.ToString();
                    break;
                case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ลดหนี้:
                    __templateName = _g.g._transGlobalTemplate._transTemplate(_g.g._transControlTypeEnum.ซื้อ_พาเชียล_ตั้งหนี้);
                    __icTransFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_พาเชียล_ตั้งหนี้);
                    __extraWhere = " and " + _g.d.ic_trans._cust_code + "=\'" + cust_code + "\' and " + _g.d.ic_trans._vat_type + "=" + vatType.ToString();
                    break;
                case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ส่งคืนสินค้าหรือราคาผิด:
                    __templateName = _g.g._transGlobalTemplate._transTemplate(_g.g._transControlTypeEnum.ซื้อ_พาเชียล_รับสินค้า);
                    __icTransFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_พาเชียล_รับสินค้า);
                    __extraWhere = " and " + _g.d.ic_trans._cust_code + "=\'" + cust_code + "\'";
                    break;
                case _g.g._transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด:
                    __templateName = _g.g._transGlobalTemplate._transTemplate(_g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ);
                    __icTransFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ);
                    __extraWhere = " and " + _g.d.ic_trans._cust_code + "=\'" + cust_code + "\' and " + _g.d.ic_trans._vat_type + "=" + vatType.ToString();
                    break;
                case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_เพิ่มหนี้:
                    __templateName = _g.g._transGlobalTemplate._transTemplate(_g.g._transControlTypeEnum.ซื้อ_พาเชียล_ตั้งหนี้);
                    __icTransFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_พาเชียล_ตั้งหนี้);
                    __extraWhere = " and " + _g.d.ic_trans._cust_code + "=\'" + cust_code + "\' and " + _g.d.ic_trans._vat_type + "=" + vatType.ToString();
                    break;
                case _g.g._transControlTypeEnum.เจ้าหนี้_ตั้งหนี้อื่น:
                    __templateName = _g.g._transGlobalTemplate._transTemplate(_g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ);
                    __icTransFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ);
                    __extraWhere = " and " + _g.d.ic_trans._cust_code + "=\'" + cust_code + "\' and " + _g.d.ic_trans._vat_type + "=" + vatType.ToString();
                    break;
                case _g.g._transControlTypeEnum.เจ้าหนี้_ลดหนี้อื่น:
                    __templateName = _g.g._transGlobalTemplate._transTemplate(_g.g._transControlTypeEnum.เจ้าหนี้_ตั้งหนี้อื่น);
                    __icTransFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เจ้าหนี้_ตั้งหนี้อื่น);
                    __extraWhere = " and " + _g.d.ic_trans._cust_code + "=\'" + cust_code + "\' and " + _g.d.ic_trans._vat_type + "=" + vatType.ToString();
                    break;
                case _g.g._transControlTypeEnum.เจ้าหนี้_เพิ่มหนี้อื่น:
                    __templateName = _g.g._transGlobalTemplate._transTemplate(_g.g._transControlTypeEnum.เจ้าหนี้_ตั้งหนี้อื่น);
                    __icTransFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เจ้าหนี้_ตั้งหนี้อื่น);
                    __extraWhere = " and " + _g.d.ic_trans._cust_code + "=\'" + cust_code + "\' and " + _g.d.ic_trans._vat_type + "=" + vatType.ToString();
                    break;
                case _g.g._transControlTypeEnum.ลูกหนี้_ตั้งหนี้อื่น:
                    __templateName = _g.g._transGlobalTemplate._transTemplate(_g.g._transControlTypeEnum.ขาย_เสนอราคา);
                    __icTransFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_เสนอราคา);
                    __extraWhere = " and " + _g.d.ic_trans._cust_code + "=\'" + cust_code + "\' and " + _g.d.ic_trans._vat_type + "=" + vatType.ToString();
                    break;
                case _g.g._transControlTypeEnum.ลูกหนี้_ลดหนี้อื่น:
                    __templateName = _g.g._transGlobalTemplate._transTemplate(_g.g._transControlTypeEnum.ลูกหนี้_ตั้งหนี้อื่น);
                    __icTransFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ลูกหนี้_ตั้งหนี้อื่น);
                    __extraWhere = " and " + _g.d.ic_trans._cust_code + "=\'" + cust_code + "\' and " + _g.d.ic_trans._vat_type + "=" + vatType.ToString();
                    break;
                case _g.g._transControlTypeEnum.ลูกหนี้_เพิ่มหนี้อื่น:
                    __templateName = _g.g._transGlobalTemplate._transTemplate(_g.g._transControlTypeEnum.ลูกหนี้_ตั้งหนี้อื่น);
                    __icTransFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ลูกหนี้_ตั้งหนี้อื่น);
                    __extraWhere = " and " + _g.d.ic_trans._cust_code + "=\'" + cust_code + "\' and " + _g.d.ic_trans._vat_type + "=" + vatType.ToString();
                    break;
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น:
                    __templateName = _g.g._transGlobalTemplate._transTemplate(_g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ);
                    __icTransFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ);
                    __extraWhere = " and " + _g.d.ic_trans._cust_code + "=\'" + cust_code + "\'";
                    break;
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_ลดหนี้:
                    __templateName = _g.g._transGlobalTemplate._transTemplate(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น);
                    __icTransFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น);
                    __extraWhere = " and " + _g.d.ic_trans._cust_code + "=\'" + cust_code + "\'";
                    break;
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_เพิ่มหนี้:
                    __templateName = _g.g._transGlobalTemplate._transTemplate(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น);
                    __icTransFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น);
                    __extraWhere = " and " + _g.d.ic_trans._cust_code + "=\'" + cust_code + "\'";
                    break;
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น:
                    __templateName = _g.g._transGlobalTemplate._transTemplate(_g.g._transControlTypeEnum.ขาย_เสนอราคา);
                    __icTransFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_เสนอราคา);
                    __extraWhere = " and " + _g.d.ic_trans._cust_code + "=\'" + cust_code + "\'";
                    break;
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_ลดหนี้:
                    __templateName = _g.g._transGlobalTemplate._transTemplate(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น);
                    __icTransFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น);
                    __extraWhere = " and " + _g.d.ic_trans._cust_code + "=\'" + cust_code + "\'";
                    break;
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_เพิ่มหนี้:
                    __templateName = _g.g._transGlobalTemplate._transTemplate(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น);
                    __icTransFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น);
                    __extraWhere = " and " + _g.d.ic_trans._cust_code + "=\'" + cust_code + "\'";
                    break;
                case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ:
                    {
                        string __extraWhereDocStatus = "";
                        if (_g.g._companyProfile._ref_po_approve == true)
                            __extraWhereDocStatus = " and " + _g.d.ic_trans._approve_status + "=1 ";

                        __templateName = _g.g._transGlobalTemplate._transTemplate(_g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ);
                        __icTransFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ);
                        __extraWhere = " and " + _g.d.ic_trans._cust_code + "=\'" + cust_code + "\' and " + _g.d.ic_trans._doc_success + "=0 " + __extraWhereDocStatus;
                    }
                    break;
                case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ตั้งหนี้:
                    __templateName = _g.g._transGlobalTemplate._transTemplate(_g.g._transControlTypeEnum.ซื้อ_พาเชียล_รับสินค้า);
                    __icTransFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_พาเชียล_รับสินค้า);
                    __extraWhere = " and " + _g.d.ic_trans._cust_code + "=\'" + cust_code + "\' and " + _g.d.ic_trans._doc_success + "=0 ";
                    break;
                case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_รับสินค้า:
                    {
                        string __extraWhereDocStatus = "";
                        if (_g.g._companyProfile._ref_po_approve == true)
                            __extraWhereDocStatus = " and " + _g.d.ic_trans._approve_status + "=1 ";

                        __templateName = _g.g._transGlobalTemplate._transTemplate(_g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ);
                        __icTransFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ใบสั่งซื้อ);
                        __extraWhere = " and " + _g.d.ic_trans._cust_code + "=\'" + cust_code + "\' and " + _g.d.ic_trans._doc_success + "=0 " + __extraWhereDocStatus;
                    }
                    break;
                case _g.g._transControlTypeEnum.สินค้า_รับคืนสินค้าจากการเบิก:
                    __templateName = _g.g._transGlobalTemplate._transTemplate(_g.g._transControlTypeEnum.สินค้า_เบิกสินค้าวัตถุดิบ);
                    __icTransFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินค้า_เบิกสินค้าวัตถุดิบ);
                    if (!cust_code.Equals(""))
                    {
                        __extraWhere = " and " + _g.d.ic_trans._cust_code + "=\'" + cust_code + "\' and last_status !=1";
                    }
                    else
                    {
                        __extraWhere = " and last_status !=1";
                    }
                    break;
                case _g.g._transControlTypeEnum.สินค้า_เบิกสินค้าวัตถุดิบ:
                    __templateName = _g.g._transGlobalTemplate._transTemplate(_g.g._transControlTypeEnum.สินค้า_ขอเบิกสินค้าวัตถุดิบ);
                    __icTransFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินค้า_ขอเบิกสินค้าวัตถุดิบ);
                    break;
                case _g.g._transControlTypeEnum.คลัง_รับฝาก_เบิก:
                    {
                        if (cust_code == "")
                        {
                            MessageBox.Show(MyLib._myGlobal._resource("ยังไม่ได้ใส่รหัสลูกค้า"));
                            return;
                        }

                        __templateName = "screen_wms_product_deposit";
                        __icTransFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.คลัง_รับฝาก_ฝาก);
                        __transTableName = _g.d.ic_wms_trans._table;
                        __extraWhere = " and " + _g.d.ic_wms_trans._cust_code + "=\'" + cust_code + "\' and last_status = 0 and coalesce(" + _g.d.ic_wms_trans._doc_success + ", 0) = 0";
                    }
                    break;
                case _g.g._transControlTypeEnum.คลัง_รับฝาก_รับคืนจากเบิก:
                    {
                        if (cust_code == "")
                        {
                            MessageBox.Show(MyLib._myGlobal._resource("ยังไม่ได้ใส่รหัสลูกค้า"));
                            return;
                        }
                        __templateName = "screen_wms_product_deposit";
                        __icTransFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.คลัง_รับฝาก_เบิก);
                        __transTableName = _g.d.ic_wms_trans._table;
                        __extraWhere = " and " + _g.d.ic_wms_trans._cust_code + "=\'" + cust_code + "\'";
                    }
                    break;
                case _g.g._transControlTypeEnum.สินค้า_โอนออก:
                    switch (selectTransFlag)
                    {
                        case 0: // โอน
                            __templateName = _g.g._transGlobalTemplate._transTemplate(_g.g._transControlTypeEnum.สินค้า_ขอโอน);
                            __icTransFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินค้า_ขอโอน);
                            __extraWhere = " and " + _g.d.ic_trans._doc_success + "=0 and " + _g.d.ic_trans._last_status + "=0 ";
                            break;
                        case 1: // ซื้อ
                            __templateName = _g.g._transGlobalTemplate._transTemplate(_g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ);
                            __icTransFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ);
                            string __wheredoc_date = "";
                            if (_g.g._companyProfile._begin_date_import_inv.ToString() !="") {
                                __wheredoc_date = " and ic_trans.doc_date >'" + _g.g._companyProfile._begin_date_import_inv.ToString() + "'";
                            }
                            __extraWhere = __wheredoc_date + " and not exists (select doc_no from ic_trans_detail where ic_trans_detail.trans_flag= 72 and ic_trans_detail.ref_doc_no = ic_trans.doc_no and ic_trans_detail.doc_ref_type = 1 )";
                            break;
                    }

                    break;

            }
            if (__templateName.Length == 0)
            {
                MessageBox.Show(MyLib._myGlobal._resource("กรุณเลือกประเภทเอกสารก่อน"));
            }
            else
            {
                if (whereDocNo)
                {
                    __extraWhere += " and " + _g.d.ic_trans._doc_no + " in (" + this._getDocRefPackForQuery(-1) + ")";
                }

                if (_g.g._companyProfile._branchStatus == 1 && _g.g._companyProfile._show_branch_doc_only && _g.g._companyProfile._change_branch_code == false)
                {
                    //if (__extraWhere.Length > 0)
                    //    __extraWhere += " and ";

                    __extraWhere += " and " + _g.d.ic_trans._branch_code + "=\'" + MyLib._myGlobal._branchCode + "\'";
                }
                //
                this._icTransSearch._dataList._loadViewFormat(__templateName, MyLib._myGlobal._userSearchScreenGroup, false);
                this._icTransSearch._dataList._extraWhere = __transTableName + "." + _g.d.ic_trans._trans_flag + "=" + __icTransFlag.ToString() + __extraWhere;
                this._icTransSearch._dataList._refreshData();
                //
                this._icTransSearch._dataList._gridData._mouseClick -= new MyLib.MouseClickHandler(_icTransSearch_gridData__mouseClick);
                this._icTransSearch._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_icTransSearch_gridData__mouseClick);
                //
                this._icTransSearch._searchEnterKeyPress -= new MyLib.SearchEnterKeyPressEventHandler(_icTransSearch_data_full__searchEnterKeyPress);
                this._icTransSearch._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_icTransSearch_data_full__searchEnterKeyPress);
                this._icTransSearch.ShowDialog();
            }
        }

        MyLib.BeforeDisplayRowReturn _gridData__beforeDisplayRow(MyLib._myGrid sender, int row, int columnNumber, string columnName, MyLib.BeforeDisplayRowReturn senderRow, MyLib._myGrid._columnType columnType, ArrayList rowData)
        {
            MyLib.BeforeDisplayRowReturn __result = senderRow;
            try
            {
                int __usedStatusColumn = sender._findColumnByName(_g.d.ic_trans._table + "." + _g.d.ic_trans._used_status);
                int __docSuccessColumn = sender._findColumnByName(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_success);
                int __lastStatusColumn = sender._findColumnByName(_g.d.ic_trans._table + "." + _g.d.ic_trans._last_status);
                if (__usedStatusColumn != -1)
                {
                    // มีการนำไปใช้แล้ว
                    int __status = MyLib._myGlobal._intPhase(sender._cellGet(row, __usedStatusColumn).ToString());
                    if (__status == 1)
                    {
                        __result.newColor = Color.LightSeaGreen;
                    }
                }
                if (__docSuccessColumn != -1)
                {
                    // มีการอ้างอิงครบแล้ว
                    int __status = MyLib._myGlobal._intPhase(sender._cellGet(row, __docSuccessColumn).ToString());
                    if (__status == 1)
                    {
                        __result.newColor = Color.SlateBlue;
                    }
                }
                if (__lastStatusColumn != -1)
                {
                    // เอกสารมีการยกเลิก
                    int __status = MyLib._myGlobal._intPhase(sender._cellGet(row, __lastStatusColumn).ToString());
                    if (__status == 1)
                    {
                        __result.newColor = Color.Red;
                    }
                }
            }
            catch
            {
            }
            return (__result);
        }

        void _icTransSearchSetData(MyLib._myGrid sender, int row)
        {
            this._icTransSearch.Close();
            string __data = "";


            switch (this._icTransControlType)
            {
                case _g.g._transControlTypeEnum.คลัง_รับฝาก_รับคืนจากเบิก:
                case _g.g._transControlTypeEnum.คลัง_รับฝาก_เบิก:
                    __data = sender._cellGet(row, _g.d.ic_wms_trans._table + "." + _g.d.ic_wms_trans._doc_no).ToString();

                    break;
                default:
                    __data = sender._cellGet(row, _g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no).ToString();
                    break;

            }
            //string __data = sender._cellGet(row, _g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no).ToString();

            MyLib._myGrid __ownerGrid = (MyLib._myGrid)this._icTransSearch._owner;
            int __columnNumber = __ownerGrid._findColumnByName(_g.d.ap_ar_trans_detail._billing_no);
            if (__columnNumber == -1)
            {
                // กรณีอ้างบิล
                __columnNumber = __ownerGrid._findColumnByName(_g.d.ic_trans_detail._ref_doc_no);
            }
            __ownerGrid._cellUpdate(__ownerGrid.SelectRow, __columnNumber, __data, true);
            __ownerGrid.Invalidate();
        }

        void _icTransSearch_data_full__searchEnterKeyPress(MyLib._myGrid sender, int row)
        {
            if (row != -1)
            {
                _icTransSearchSetData(sender, row);
            }
        }

        void _icTransSearch_gridData__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            if (e._row != -1)
            {
                _icTransSearchSetData((MyLib._myGrid)sender, e._row);
            }
        }

        private void _processToolStripButton_Click(object sender, EventArgs e)
        {
            if (_processButton != null)
            {
                this._processButton(this._transGrid);
            }
        }
    }
}
