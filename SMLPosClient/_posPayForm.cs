using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMLPosClient
{
    public partial class _posPayForm : Form
    {
        public delegate void _saveDataEvent(_posPayForm sender);
        public delegate Boolean _checkDefaultCustEvent(string custCode);

        public event _saveDataEvent _saveData;
        public event _checkDefaultCustEvent _checkDefaultCust;

        private SMLPOSControl._posScreenConfig _posConfig;
        string _formatNumber = "#,0.00";
        /// <summary>
        /// ยอดรวมทั้งสิ้น
        /// </summary>
        public decimal _totalAmount = 0M;
        /// <summary>
        /// มูลค่าสินค้า
        /// </summary>
        public decimal _totalValue = 0M;
        /// <summary>
        /// ยอดรวมสินค้ายกเว้นภาษี
        /// </summary>
        public decimal _itemTotalExceptVatAmount = 0M;
        /// <summary>
        /// ยอดรวมสินค้ามีภาษี
        /// </summary>
        public decimal _itemTotalVatAmount = 0M;
        /// <summary>
        /// เงินทอน
        /// </summary>
        public decimal _moneyChange = 0M;
        /// <summary>
        /// มูลค่าส่วนลด
        /// </summary>
        public decimal _discountTotalAmount = 0M;
        /// <summary>
        /// ยอดภาษีมูลค่าเพิ่ม
        /// </summary>
        public decimal _vatAmount = 0M;
        /// <summary>
        /// ยอดก่อนภาษี
        /// </summary>
        public decimal _totalBeforeVat = 0M;
        /// <summary>
        /// คงเหลือหลังหักส่วนลด
        /// </summary>
        public decimal _totalAfterDiscount = 0M;
        /// <summary>
        /// ยอดปัดเศษ
        /// </summary>
        public decimal _diffAmount = 0M;
        /// <summary>
        /// ยอดคงเหลือทั้งสิ้น
        /// </summary>
        public decimal _finalBalance = 0M;
        /// <summary>
        /// จ่ายเป็นเงินสด
        /// </summary>
        public decimal _payCash = 0M;
        /// <summary>
        /// ยอดบัตรเครดิต
        /// </summary>
        public decimal _creditCardAmount = 0M;
        /// <summary>
        /// ยอด charge บัตรเครดิต
        /// </summary>
        public decimal _creditCardChargeAmount = 0M;
        /// <summary>
        /// ยอดคูปอง
        /// </summary>
        public decimal _couponAmount = 0M;
        /// <summary>
        /// จำนวนแต้ม
        /// </summary>
        public decimal _pointQty = 0M;
        /// <summary>
        /// อัตราส่วนแต้ม/บาท
        /// </summary>
        public decimal _pointRate = 0M;
        /// <summary>
        /// เงื่อนไขแต้ม/บาท
        /// </summary>
        public string _pointValueCondition = "";

        /// <summary>
        /// ยอดแต้ม (เงิน)
        /// </summary>
        public decimal _pointAmount = 0M;
        /// <summary>
        /// เสียง
        /// </summary>
        public MyLib._moneyToVoice _moneyToVoice;
        /// <summary>
        /// จำนวนที่ลูกค้าต้องชำระเงิน
        /// </summary>
        public decimal _totalCustPay = 0M;
        /// <summary>
        /// รวมจำนวนเงินรับชำระ
        /// </summary>
        public decimal _sumPayMoney = 0M;

        /// <summary>ยอดเงินรับล่วงหน้า</summary>
        public decimal _totalAdvanceAmount = 0M;

        /// <summary>ยอดเงินมัดจำ</summary>
        public decimal _totalDepositAmount = 0M;

        private decimal _pointBalance = 0M;

        public string _discountWord = "";

        /// <summary>service charge (Word)</summary>
        public string _serviceChargeWord = "";

        /// <summary>มูลค่า Service Charge</summary>
        public decimal _serviceChargeAmount = 0M;

        /// <summary>มูลค่าที่จ่ายด้วยสกุลเงินอื่น ๆ</summary>
        public decimal _payOtherCurrencyAmount = 0M;

        public string _custCode = "";
        public Boolean _use_credit_sale = false;

        public Boolean _useChangeOtherCurrency = false;
        public decimal _exChangeRate = 1;
        /// <summary>
        /// เงินทอน สกุลเงินอื่นๆ
        /// </summary>
        public decimal _changeMoneyOtherCurrency = 0M;

        /// <summary>
        /// ประเภทการขาย
        /// </summary>
        public int _inquery_type = 1;


        public SMLERPAPARControl._payCouponGridControl _payCouponGrid = new SMLERPAPARControl._payCouponGridControl();

        private System.Windows.Forms.TabPage tab_currency_other;
        public SMLERPAPARControl._payOtherCurrencyGridControl _payOtherCurrencyGrid = new SMLERPAPARControl._payOtherCurrencyGridControl();

        public _posPayForm(SMLPOSControl._posScreenConfig posConfig, string custCode, decimal discountAmount, decimal totalAmount, decimal totalVatAmount, decimal totalExceptVatAmount, decimal pointBalance, bool close_discount_bill, decimal foodDiscount, string discountWord, decimal serviceChargeAmount, string serviceChargeWord, Boolean useCreditSale, bool useChangeOtherCurrency, decimal exchangeRate)
        {
            InitializeComponent();

            this._useChangeOtherCurrency = useChangeOtherCurrency;
            this._exChangeRate = exchangeRate;
            this._use_credit_sale = useCreditSale;
            this._payAdvance._icTransControlType = _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ;
            this._payAdvance._dataGrid._build(_g.g._depositAdvanceEnum.รับเงินมัดจำ);
            this._payAdvance._dataGrid._getCustCode += new SMLERPAPARControl._payDepositAdvanceGridControl._getCustCodeEvent(_dataGrid__getCustCode);
            this._payAdvance._dataGrid._getProcessDate += new SMLERPAPARControl._payDepositAdvanceGridControl._getProcessDateEvent(_dataGrid__getProcessDate);
            this._payAdvance._dataGrid._alterCellUpdate += new MyLib.AfterCellUpdateEventHandler(_dataGrid__alterCellUpdate);

            this._payDepositGrid._build(_g.g._depositAdvanceEnum.รับเงินล่วงหน้า);
            this._payDepositGrid._getCustCode += new SMLERPAPARControl._payDepositAdvanceGridControl._getCustCodeEvent(_payDepositGrid__getCustCode);
            this._payDepositGrid._getProcessDate += new SMLERPAPARControl._payDepositAdvanceGridControl._getProcessDateEvent(_payDepositGrid__getProcessDate);
            this._payDepositGrid._alterCellUpdate += new MyLib.AfterCellUpdateEventHandler(_payDepositGrid__alterCellUpdate);

            if (MyLib._myGlobal._isVersionEnum != MyLib._myGlobal._versionType.SMLPOSLite && this._use_credit_sale == false)
            {
                this._tabPayDetail.Controls.RemoveAt(5);
                this._buttonCreditSave.Visible = false;
            }

            //toe
            switch (MyLib._myGlobal._isVersionEnum)
            {
                case MyLib._myGlobal._versionType.SMLTomYumGoong:
                case MyLib._myGlobal._versionType.SMLTomYumGoongPro:
                    {
                        if (MyLib._myGlobal._programName.Equals("Sea And Hill Restaurant"))
                        {
                            this.Height += 100;
                        }
                        else
                        {
                            this._advanceMoneyButton.Visible = false;
                            this._advanceLabel.Visible = false;
                            this._textboxAdvanceMoney.Visible = false;
                            this._tabPayDetail.Controls.RemoveAt(3);

                            this._depositMoneyButton.Visible = false;
                            this._depositLabel.Visible = false;
                            this._textboxDeposit.Visible = false;
                            this._tabPayDetail.Controls.RemoveAt(2);

                            this.panel2.Height -= 64;
                        }
                    }
                    break;
                case MyLib._myGlobal._versionType.SMLPOSLite:
                    {
                        // remove create sale
                        this._tabPayDetail.Controls.RemoveAt(5);
                        this._buttonCreditSave.Visible = false;

                        this.panel4.Visible = false;
                        this._serviceButton.Visible = false;

                        this._tabPayDetail.Controls.RemoveAt(3);

                        this._advanceLabel.Visible = false;
                        this._textboxDeposit.Visible = false;
                        this._advanceMoneyButton.Visible = false;

                        this.panel2.Height -= 96;

                    }
                    break;
                default:
                    {
                        this.panel4.Visible = false;
                        this.panel2.Height -= 64;
                        this._serviceButton.Visible = false;

                    }
                    break;
            }

            if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOSProfessional && _g.g._companyProfile._multi_currency)
            {
                this.tab_currency_other = new System.Windows.Forms.TabPage();

                this._tabPayDetail.SuspendLayout();
                this.SuspendLayout();
                // add tab
                this._tabPayDetail.Controls.Add(this.tab_currency_other);
                // 
                // tab_other_currency
                // 
                this.tab_currency_other.Location = new System.Drawing.Point(4, 23);
                this.tab_currency_other.Name = "tab_currency_other";
                this.tab_currency_other.Size = new System.Drawing.Size(788, 37);
                this.tab_currency_other.TabIndex = 6;
                this.tab_currency_other.Text = "tab_currency_other";
                this.tab_currency_other.Controls.Add(this._payOtherCurrencyGrid);
                this.tab_currency_other.UseVisualStyleBackColor = true;

                this._textBoxCoupon.Width = 116;

                this._textboxOtherCurrency.Visible = true;
                this._otherCurrencyLabel.Visible = true;

                if (this._useChangeOtherCurrency)
                {
                    this._textBoxChange.Width = 90;
                    this._labelChangeOtherCurrency.Visible = true;
                    this._textboxChangeOtherCurrency.Visible = true;
                }

                this._payOtherCurrencyGrid.Dock = DockStyle.Fill;

                this._tabPayDetail.ResumeLayout(false);
                this.ResumeLayout(false);
                this.PerformLayout();

                this._payOtherCurrencyGrid._afterCalcTotal += _payOtherCurrencyGrid__afterCalcTotal;
            }



            /*
            if (MyLib._myGlobal._isVersionEnum != MyLib._myGlobal._versionType.SMLTomYumGoong)
            {
                this.panel4.Visible = false;
                this.panel2.Height -= 64;

                this._serviceButton.Visible = false;
            }
            else
            {

                // visiable control
                this.label14.Visible = false;
                this._textboxAdvanceMoney.Visible = false;
                this.label15.Visible = false;
                this._textboxDeposit.Visible = false;

            }*/


            // ปรับตัวเลข
            totalAmount = (totalAmount + discountAmount + foodDiscount);
            //
            if (close_discount_bill)
            {
                this._textBoxDiscount.ReadOnly = close_discount_bill;
            }

            //MyLib._myGlobal._writeLogFile(@"C:\smlsoft\poslog.txt", DateTime.Now.ToString() + " : init pay form", true);

            this._custCode = custCode;
            this._pointBalance = pointBalance;
            this._posConfig = posConfig;
            this._totalAmount = totalAmount;
            this._itemTotalVatAmount = totalVatAmount;
            this._itemTotalExceptVatAmount = totalExceptVatAmount;
            this._totalValue = totalAmount;
            //
            this._buttonCancel.Click += new EventHandler(_buttonCancel_Click);
            this._buttonSave.Click += new EventHandler(_buttonSave_Click);
            this._textBoxCash.TextChanged += (s1, e1) => { this._calc(); };
            this._textBoxDiscount.TextChanged += (s1, e1) => { this._calc(); };
            this._textboxService.TextChanged += (s1, e1) => { this._calc(); };
            this._textBoxPoint.TextChanged += (s1, e1) => { this._calc(); };
            this._payCreditCardGrid._afterCalcTotal += new MyLib.AfterCalcTotalEventHandler(_payCreditCardGrid__afterCalcTotal);
            this._payCouponGrid._afterCalcTotal += new MyLib.AfterCalcTotalEventHandler(_payCouponGrid__afterCalcTotal);
            this._button1000.Click += (s1, e1) => { this._addMoney(1000M); };
            this._button500.Click += (s1, e1) => { this._addMoney(500M); };
            this._button100.Click += (s1, e1) => { this._addMoney(100M); };
            this._button50.Click += (s1, e1) => { this._addMoney(50M); };
            this._button20.Click += (s1, e1) => { this._addMoney(20M); };
            //
            this._textBoxPointBalance.Text = this._pointBalance.ToString();
            this._textBoxPointRate.Text = "1.0";
            try
            {
                // ค้นหา มูลค่าแต้ม
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                string __dateQuery = MyLib._myGlobal._convertDateToQuery(DateTime.Now);
                string __query = "select " + _g.d.pos_point_period._point_value + "," + _g.d.pos_point_period._point_value_condition + " from " + _g.d.pos_point_period._table + " where \'" + __dateQuery + "\' between " + _g.d.pos_point_period._from_date + " and " + _g.d.pos_point_period._to_date;
                DataTable __result = __myFrameWork._queryShort(__query).Tables[0];
                if (__result.Rows.Count > 0)
                {
                    this._textBoxPointRate.Text = __result.Rows[0][_g.d.pos_point_period._point_value].ToString();
                    this._pointValueCondition = __result.Rows[0][_g.d.pos_point_period._point_value_condition].ToString();
                }
            }
            catch
            {
            }
            //
            if (discountAmount != 0 || foodDiscount != 0)
            {
                this._textBoxDiscount.Text = (foodDiscount + discountAmount).ToString();
            }

            if (discountWord.Length > 0)
            {
                this._textBoxDiscount.Text = ((this._textBoxDiscount.Text.Trim().Length > 0) ? this._textBoxDiscount.Text + "," : "") + discountWord;
            }

            if (serviceChargeWord.Length > 0)
            {
                this._textboxService.Text = serviceChargeWord;
            }
            //
            this._calc();
            //
            if (this._posConfig._android_customer_display)
            {
                _androidCustomerScreen._setConfig(this._posConfig);
                _androidCustomerScreen._singleCommand(_g.g._androidDisplayEnum.Clear, "");
                StringBuilder __str = new StringBuilder(MyLib._myGlobal._xmlHeader);
                __str.Append("<node>");
                __str.Append(_androidCustomerScreen._add(_g.g._androidDisplayEnum.AmountWord, "รวมทั้งสิ้น"));
                __str.Append(_androidCustomerScreen._add(_g.g._androidDisplayEnum.Amount, this._finalBalance.ToString(this._formatNumber)));
                __str.Append("</node>");
                _androidCustomerScreen._send(__str.ToString());
            }
            //
            if (this._posConfig._use_sound_amount == 1)
            {
                this._moneyToVoice = new MyLib._moneyToVoice();
                this._moneyToVoice._toVoice("รวมเงิน", this._moneyToVoice._toText(this._finalBalance));
            }
            this._payCouponGrid._total_show = true;
            this._payCouponGrid.BorderStyle = BorderStyle.FixedSingle;
            this._payCouponGrid.Dock = DockStyle.Fill;
            this.tab_coupon.Controls.Add(this._payCouponGrid);
            this._payCouponGrid._getTotalAmount += _payCouponGrid__getTotalAmount;

            //this.splitContainer1.Panel2.Controls.Add(this._payCouponGrid);
            //

            this._textBoxCash.GotFocus += new EventHandler(_textBoxCash_GotFocus);
            this._textBoxPoint.GotFocus += new EventHandler(_textBoxPoint_GotFocus);
            this._textBoxDiscount.GotFocus += new EventHandler(_textBoxDiscount_GotFocus);
            this._textboxService.GotFocus += new EventHandler(_textboxService_GotFocus);

            //this._tabPayDetail.TabPages.Remove(this.tab_remark);
        }

        private void _payOtherCurrencyGrid__afterCalcTotal(object sender)
        {
            this._calc();
        }

        decimal _payCouponGrid__getTotalAmount()
        {
            return this._finalBalance;
        }

        void _payDepositGrid__alterCellUpdate(object sender, int row, int column)
        {
            this._calc();
        }

        void _dataGrid__alterCellUpdate(object sender, int row, int column)
        {
            this._calc();
        }

        DateTime _payDepositGrid__getProcessDate()
        {
            return DateTime.Now;
        }

        string _payDepositGrid__getCustCode()
        {
            return this._custCode;
        }

        DateTime _dataGrid__getProcessDate()
        {
            return DateTime.Now;
        }

        string _dataGrid__getCustCode()
        {
            return this._custCode;
        }

        void _textboxService_GotFocus(object sender, EventArgs e)
        {
            this._textNumber = this._textboxService;
        }

        void _textBoxDiscount_GotFocus(object sender, EventArgs e)
        {
            this._textNumber = this._textBoxDiscount;
        }

        void _textBoxPoint_GotFocus(object sender, EventArgs e)
        {
            this._textNumber = this._textBoxPoint;
        }

        void _textBoxCash_GotFocus(object sender, EventArgs e)
        {
            this._textNumber = this._textBoxCash;
        }

        void _payCouponGrid__afterCalcTotal(object sender)
        {
            this._calc();
        }

        void _addMoney(decimal value)
        {
            decimal __cash = MyLib._myGlobal._decimalPhase(this._textBoxCash.Text.Trim());
            __cash += value;
            this._textBoxCash.Text = __cash.ToString();
            this._calc();
        }

        void _save()
        {
            //MyLib._myGlobal._writeLogFile(@"C:\smlsoft\poslog.txt", DateTime.Now.ToString() + " : Start Save Process(After Click Save or F12)", false);
            if (MyLib._myGlobal._decimalPhase(this._textBoxPoint.Text) > this._pointBalance)
            {
                MessageBox.Show("จำนวนแต้มผิดพลาด");
            }
            else
                if (this._finalBalance > (this._payCash + this._payOtherCurrencyAmount + this._creditCardAmount + this._couponAmount + this._pointAmount + this._totalAdvanceAmount + this._totalDepositAmount))
            {
                MessageBox.Show("การชำระเงินไม่สัมพันธ์กับยอดขาย");
            }
            else
            {

                this.Close();
                if (_firstClick == true)
                {
                    _firstClick = false;
                    this._saveData(this);
                }
            }
        }

        bool _firstClick = true;
        void _buttonSave_Click(object sender, EventArgs e)
        {
            this._save();
        }

        void _payCreditCardGrid__afterCalcTotal(object sender)
        {
            this._calc();
        }

        void _calc()
        {
            this._payCash = MyLib._myGlobal._decimalPhase(this._textBoxCash.Text.Trim());

            // step 
            // เอายอดรวม มาคำณวน ส่วนลดอาหาร
            // เอายอดหลังลดอาหาร มาลด ท้ายบิล
            // เอายอด ท้ายบิล มาคิด service charge
            // เอายอดหลังคิด service charge ไปแยก vat


            // หา service charge ก่อน
            string textServiceStr = this._textboxService.Text;
            decimal __serviceAmountCheck = MyLib._myGlobal._decimalPhase(textServiceStr.Replace("%", string.Empty));
            if (__serviceAmountCheck < 0)
            {
                __serviceAmountCheck = -1 * __serviceAmountCheck;

                textServiceStr = __serviceAmountCheck.ToString() + ((this._textboxService.Text.IndexOf("%") != -1) ? "%" : "");
            }

            decimal __serviceAmount = this._totalAmount - MyLib._myGlobal._calcAfterDiscount(textServiceStr, MyLib._myGlobal._round(this._totalAmount, _g.g._companyProfile._item_price_decimal), _g.g._companyProfile._item_price_decimal);


            decimal __afterDiscount = MyLib._myGlobal._calcAfterDiscount(this._textBoxDiscount.Text, MyLib._myGlobal._round((this._totalAmount + __serviceAmount), _g.g._companyProfile._item_price_decimal), _g.g._companyProfile._item_price_decimal);
            decimal __discountAmount = (this._totalAmount + __serviceAmount) - __afterDiscount;

            // เงินมัดจำ
            this._payAdvance._dataGrid._calcTotal(false);
            decimal __advanceAmount = ((MyLib._myGrid._columnType)this._payAdvance._dataGrid._columnList[this._payAdvance._dataGrid._findColumnByName(_g.d.cb_trans_detail._amount)])._total;

            // จ่ายล่วงหน้า 
            this._payDepositGrid._calcTotal(false);
            decimal __depositAmount = ((MyLib._myGrid._columnType)this._payDepositGrid._columnList[this._payAdvance._dataGrid._findColumnByName(_g.d.cb_trans_detail._amount)])._total;

            //this._totalBeforeVat = Math.Round(this._itemTotalVatAmount * (100M / (100M + _g.g._companyProfile._vat_rate)), _g.g._companyProfile._item_amount_decimal);// ถอด Vat
            //this._vatAmount = Math.Round(this._totalBeforeVat * (_g.g._companyProfile._vat_rate / 100.0M), _g.g._companyProfile._item_amount_decimal);

            if (_posConfig._pos_vat_type == 1)
            {
                decimal __discountNoVatAmount = 0M;

                //this._totalBeforeVat = ((this._itemTotalVatAmount + __serviceAmount) - (__discountAmount + __advanceAmount));
                // this._totalBeforeVat = ((this._itemTotalVatAmount + __serviceAmount) < (__discountAmount + __advanceAmount)) ? 0 : ((this._itemTotalVatAmount + __serviceAmount) - (__discountAmount + __advanceAmount));
                if ((_itemTotalVatAmount + __serviceAmount) < (__discountAmount + __advanceAmount))
                {
                    // ส่วนลด ไม่พอลดยอดก่อนภาษี 
                    this._totalBeforeVat = 0;
                    __discountNoVatAmount = (__discountAmount - _itemTotalVatAmount);
                }
                else
                {
                    // ส่วนลด พอลดยอดก่อนภาษีทั้งหมด
                    this._totalBeforeVat = ((this._itemTotalVatAmount + __serviceAmount) - (__discountAmount + __advanceAmount));
                }

                this._vatAmount = MyLib._myGlobal._round(this._totalBeforeVat * (_g.g._companyProfile._vat_rate / 100.0M), _g.g._companyProfile._item_amount_decimal);
                // this._vatAmount = MyLib._myGlobal._round(this._itemTotalVatAmount * (_g.g._companyProfile._vat_rate / 100.0M), _g.g._companyProfile._item_amount_decimal);
                //__afterVat = __beforeVat + __vatValue;
                //__totalAmount = __totalValueNoVat + __afterVat;
                __afterDiscount += _vatAmount;
            }
            else
            {
                if (_g.g._companyProfile._discount_type == 1)
                {
                    // ลดกอน vat 
                    // after discount //__totalAmount = (this._totalAmount - __discountAmount);

                    // กรณี ยอดก่อน ภาษี น้อยกว่าส่วนลด 
                    //this._totalBeforeVat = MyLib._myGlobal._round((((this._itemTotalVatAmount + __serviceAmount) - (__discountAmount + __advanceAmount)) * 100.0M) / (100.0M + _g.g._companyProfile._vat_rate), _g.g._companyProfile._item_amount_decimal);
                    //this._vatAmount = MyLib._myGlobal._round(((this._itemTotalVatAmount + __serviceAmount) - (__discountAmount + __advanceAmount)) - this._totalBeforeVat, _g.g._companyProfile._item_amount_decimal);

                    this._totalBeforeVat = ((this._itemTotalVatAmount + __serviceAmount) < (__discountAmount + __advanceAmount)) ? 0 : MyLib._myGlobal._round((((this._itemTotalVatAmount + __serviceAmount) - (__discountAmount + __advanceAmount)) * 100.0M) / (100.0M + _g.g._companyProfile._vat_rate), _g.g._companyProfile._item_amount_decimal);
                    this._vatAmount = ((this._itemTotalVatAmount + __serviceAmount) < (__discountAmount + __advanceAmount)) ? 0 : MyLib._myGlobal._round(((this._itemTotalVatAmount + __serviceAmount) - (__discountAmount + __advanceAmount)) - this._totalBeforeVat, _g.g._companyProfile._item_amount_decimal);
                    //__afterVat = __beforeVat + __vatValue;
                }
                else
                {
                    /// แบบเดิม
                    //__totalAmount = (this._totalAmount - __discountAmount);
                    this._totalBeforeVat = MyLib._myGlobal._round((((this._itemTotalVatAmount + __serviceAmount) - __advanceAmount) * 100.0M) / (100.0M + _g.g._companyProfile._vat_rate), _g.g._companyProfile._item_amount_decimal);
                    this._vatAmount = MyLib._myGlobal._round(((this._itemTotalVatAmount + __serviceAmount) - __advanceAmount) - this._totalBeforeVat, _g.g._companyProfile._item_amount_decimal);
                    //__afterVat = __beforeVat + __vatValue;
                }

            }

            //
            this._discountWord = this._textBoxDiscount.Text;
            this._discountTotalAmount = __discountAmount;
            this._totalAfterDiscount = __afterDiscount;
            this._serviceChargeAmount = __serviceAmount;
            this._serviceChargeWord = this._textboxService.Text;

            // ดึงยอดการจ่าย
            this._payCreditCardGrid._calcTotal(false);
            this._creditCardAmount = ((MyLib._myGrid._columnType)this._payCreditCardGrid._columnList[this._payCreditCardGrid._findColumnByName(_g.d.cb_trans_detail._amount)])._total;

            this._creditCardChargeAmount = ((MyLib._myGrid._columnType)this._payCreditCardGrid._columnList[this._payCreditCardGrid._findColumnByName(_g.d.cb_trans_detail._charge)])._total;


            this._payCouponGrid._calcTotal(false);
            this._couponAmount = ((MyLib._myGrid._columnType)this._payCouponGrid._columnList[this._payCouponGrid._findColumnByName(_g.d.cb_trans_detail._amount)])._total;

            // สกุลเงินอื่น ๆ
            this._payOtherCurrencyGrid._calcTotal(false);
            this._payOtherCurrencyAmount = ((MyLib._myGrid._columnType)this._payOtherCurrencyGrid._columnList[this._payOtherCurrencyGrid._findColumnByName(_g.d.cb_trans_detail._sum_amount)])._total;


            this._pointQty = MyLib._myGlobal._decimalPhase(this._textBoxPoint.Text.Trim());
            this._pointRate = MyLib._myGlobal._decimalPhase(this._textBoxPointRate.Text.Trim());
            this._pointAmount = this._pointQty * this._pointRate;

            if (this._pointRate == 0 && this._pointValueCondition.Length > 0)
            {
                this._pointAmount = _posProcess._pointConditionProcess(this._pointValueCondition, _pointQty);
            }

            // เริ่มต้นการคำณวนรายละเอียดการจ่ายเงิน 

            if (this._inquery_type == 0)
            {
                // ขายเชื่อ คำนวณเฉพาะ เงินมัดจำ และส่วนลด

                this._finalBalance = this._totalAfterDiscount;
                this._diffAmount = 0M;
                // มัดจำ
                this._totalAdvanceAmount = __advanceAmount;

                // ล่วงหน้า
                this._totalDepositAmount = 0M;

                this._moneyChange = 0M; // this._payCash - (this._finalBalance - (this._creditCardAmount + this._couponAmount + this._pointAmount + this._totalAdvanceAmount + this._totalDepositAmount));
                this._creditCardAmount = 0M;// 
                this._couponAmount = 0M;
                this._pointAmount = 0M;
                this._payCash = 0M;

                this._payOtherCurrencyAmount = 0M;

                this._textboxAdvanceMoney.Text = (this._totalAdvanceAmount > 0) ? this._totalAdvanceAmount.ToString(this._formatNumber) : "";
                this._textboxDeposit.Text = (this._totalDepositAmount > 0) ? this._totalDepositAmount.ToString(this._formatNumber) : "";

                this._textBoxCreditCard.Text = (this._creditCardAmount > 0) ? this._creditCardAmount.ToString(this._formatNumber) : "";
                this._textBoxCoupon.Text = (this._couponAmount > 0) ? this._couponAmount.ToString(this._formatNumber) : "";
                this._textBoxDiscountAmount.Text = (this._discountTotalAmount > 0) ? this._discountTotalAmount.ToString(this._formatNumber) : "";
                this._textBoxChange.Text = (this._moneyChange > 0) ? this._moneyChange.ToString(this._formatNumber) : "";
                this._textBoxAfterDiscount.Text = (this._totalAfterDiscount > 0) ? this._totalAfterDiscount.ToString(this._formatNumber) : "";
                this._textBoxDiff.Text = this._diffAmount.ToString(this._formatNumber); //(this._diffAmount > 0) ? this._diffAmount.ToString(this._formatNumber) : "";
                this._textBoxBalance.Text = (this._finalBalance > 0) ? this._finalBalance.ToString(this._formatNumber) : "";
                this._textBoxPointAmount.Text = (this._pointAmount > 0) ? this._pointAmount.ToString(this._formatNumber) : "";

                this._textBoxTotal.Text = (this._totalAmount + this._serviceChargeAmount).ToString(this._formatNumber);
                this._textboxServiceAmount.Text = (this._serviceChargeAmount > 0) ? this._serviceChargeAmount.ToString(this._formatNumber) : "";

                this._textboxOtherCurrency.Text = (this._payOtherCurrencyAmount > 0) ? this._payOtherCurrencyAmount.ToString(this._formatNumber) : "";

                this._totalCustPay = this._finalBalance - (this._creditCardAmount + this._couponAmount + this._pointAmount + this._payOtherCurrencyAmount);
                this._buttonTotalPrice.ButtonText = (this._finalBalance > 0) ? this._totalCustPay.ToString(this._formatNumber) : "รับเงินพอดี";
                this._sumPayMoney = 0M; // this._payCash + this._creditCardAmount + this._couponAmount + this._pointAmount;
            }
            else
            {
                // ขายสด คำณวนยอดขายทุกอย่าง

                decimal __diff = _posProcess._diffProcess(this._totalAfterDiscount, _posConfig);

                /* โต๋ ย้ายไปทำใน _posProcess.cs
                 * 
                decimal __diff = this._totalAfterDiscount - Math.Floor(this._totalAfterDiscount);

                if (_posConfig._round_type == 1 && _posConfig._round_table.Length > 0 && _posConfig._round_table_list.Count > 0)
                {
                    foreach (SMLPOSControl._roundTable __round in _posConfig._round_table_list)
                    {
                        if (__diff > __round._fromValue && __diff < __round._toValue)
                        {
                            __diff = __round._roundValue;
                            break;
                        }
                    }
                }
                else
                {
                    // ปัดเหมือนเดิม
                    if (__diff >= 0.01M && __diff <= 0.24M) __diff = 0M;
                    else
                        if (__diff >= 0.25M && __diff <= 0.49M) __diff = 0.25M;
                        else
                            if (__diff >= 0.50M && __diff <= 0.74M) __diff = 0.5M;
                            else
                                if (__diff >= 0.75M && __diff <= 0.99M) __diff = 0.75M;
                }
                */

                this._finalBalance = Math.Floor(this._totalAfterDiscount) + __diff;
                this._diffAmount = (this._finalBalance - this._totalAfterDiscount); // (this._finalBalance - this._totalAfterDiscount) * -1;

                //
                // จ่ายล่วงหน้า
                this._totalAdvanceAmount = __advanceAmount;
                // มัดจำ
                this._totalDepositAmount = __depositAmount;

                this._moneyChange = (this._payCash + this._payOtherCurrencyAmount) - (this._finalBalance - (this._creditCardAmount + this._couponAmount + this._pointAmount + this._totalAdvanceAmount + this._totalDepositAmount));

                if (this._useChangeOtherCurrency)
                {
                    this._changeMoneyOtherCurrency = MyLib._myGlobal._round(this._moneyChange / this._exChangeRate, 2);
                }

                this._textboxAdvanceMoney.Text = (this._totalAdvanceAmount > 0) ? this._totalAdvanceAmount.ToString(this._formatNumber) : "";
                this._textboxDeposit.Text = (this._totalDepositAmount > 0) ? this._totalDepositAmount.ToString(this._formatNumber) : "";

                this._textBoxCreditCard.Text = (this._creditCardAmount > 0) ? this._creditCardAmount.ToString(this._formatNumber) : "";
                this._textBoxCoupon.Text = (this._couponAmount > 0) ? this._couponAmount.ToString(this._formatNumber) : "";
                this._textBoxDiscountAmount.Text = (this._discountTotalAmount > 0) ? this._discountTotalAmount.ToString(this._formatNumber) : "";
                this._textBoxChange.Text = (this._moneyChange > 0) ? this._moneyChange.ToString(this._formatNumber) : "";
                this._textBoxAfterDiscount.Text = (this._totalAfterDiscount > 0) ? this._totalAfterDiscount.ToString(this._formatNumber) : "";
                this._textBoxDiff.Text = this._diffAmount.ToString(this._formatNumber); //(this._diffAmount > 0) ? this._diffAmount.ToString(this._formatNumber) : "";
                this._textBoxBalance.Text = (this._finalBalance > 0) ? this._finalBalance.ToString(this._formatNumber) : "";
                this._textBoxPointAmount.Text = (this._pointAmount > 0) ? this._pointAmount.ToString(this._formatNumber) : "";

                this._textBoxTotal.Text = (this._totalAmount + this._serviceChargeAmount).ToString(this._formatNumber);
                this._textboxServiceAmount.Text = (this._serviceChargeAmount > 0) ? this._serviceChargeAmount.ToString(this._formatNumber) : "";

                // สกุลเงินอื่น ๆ
                this._textboxOtherCurrency.Text = (this._payOtherCurrencyAmount > 0) ? this._payOtherCurrencyAmount.ToString(this._formatNumber) : "";
                this._textboxChangeOtherCurrency.Text = (this._changeMoneyOtherCurrency > 0) ? this._changeMoneyOtherCurrency.ToString(this._formatNumber) : "";

                this._totalCustPay = this._finalBalance - (this._creditCardAmount + this._couponAmount + this._pointAmount + this._payOtherCurrencyAmount);
                this._buttonTotalPrice.ButtonText = (this._finalBalance > 0) ? this._totalCustPay.ToString(this._formatNumber) : "รับเงินพอดี";
                this._sumPayMoney = this._payCash + this._creditCardAmount + this._couponAmount + this._pointAmount + this._payOtherCurrencyAmount;
            }

        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            Keys __keyCode = (Keys)(int)keyData & Keys.KeyCode;

            switch (keyData)
            {
                case Keys.F1:
                    this._cashFocus();
                    return true;
                case Keys.F2:
                    this._creditCardFocus();
                    return true;
                case Keys.F5:
                    {
                        if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLTomYumGoong)
                        {
                            this._serviceFocus();
                        }
                    }
                    return true;
                case Keys.F6:
                    this._advanceAmountFocus();
                    return true;
                case Keys.F7:
                    this._depositAmountFocus();
                    return true;
                case Keys.F3:
                    this._couponFocus();
                    return true;
                case Keys.F12:
                    this._calc();
                    this._save();
                    return true;
                case Keys.Escape:
                    this._close();
                    return true;
                case Keys.F9:
                    this._creditSaleFocus();
                    return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            this._textBoxCash.Focus();
        }

        void _close()
        {
            DialogResult __ask = MessageBox.Show("Cancel ?", "Cancel", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2);
            if (__ask == System.Windows.Forms.DialogResult.Yes)
            {
                this.Dispose();
            }
        }

        void _buttonCancel_Click(object sender, EventArgs e)
        {
            this._close();
        }

        private void _posPayForm_Load(object sender, EventArgs e)
        {
            this._textBoxTotal.Text = this._totalAmount.ToString(_formatNumber);
            this._textBoxTotal_2.Text = this._totalAmount.ToString(_formatNumber);
        }

        void _cashFocus()
        {
            this._textBoxCash.Focus();
            this._textBoxCash.SelectAll();
        }

        void _serviceFocus()
        {
            this._textboxService.Focus();
            this._textboxService.SelectAll();
        }

        void _creditCardFocus()
        {
            this._tabPayDetail.SelectedTab = this.tab_credit;
            this._payCreditCardGrid.Select();
            this._payCreditCardGrid._gotoCell((this._payCreditCardGrid._selectRow < 0) ? 0 : this._payCreditCardGrid._selectRow, 0);
        }

        void _couponFocus()
        {
            this._tabPayDetail.SelectedTab = this.tab_coupon;
            this._payCouponGrid.Select();
            this._payCouponGrid._gotoCell((this._payCouponGrid._selectRow < 0) ? 0 : this._payCouponGrid._selectRow, 0);
        }

        void _advanceAmountFocus()
        {
            this._tabPayDetail.SelectedTab = this.tab_advance;
            this._payAdvance._dataGrid.Select();
            this._payAdvance._dataGrid._gotoCell((this._payAdvance._dataGrid._selectRow < 0) ? 0 : this._payAdvance._dataGrid.SelectRow, 0);

        }

        void _depositAmountFocus()
        {
            this._tabPayDetail.SelectedTab = this.tab_deposit;
            this._payDepositGrid.Select();
            this._payDepositGrid._gotoCell((this._payDepositGrid._selectRow < 0) ? 0 : this._payDepositGrid.SelectRow, 0);

        }

        private void _cashButton_Click(object sender, EventArgs e)
        {
            this._cashFocus();
        }

        private void _creditCardButton_Click(object sender, EventArgs e)
        {
            this._tabPayDetail.SelectedIndex = 0;
            this._creditCardFocus();
        }

        private void _couponButton_Click(object sender, EventArgs e)
        {
            this._tabPayDetail.SelectedIndex = 1;
            this._couponFocus();
        }

        public TextBox _textNumber = null;

        private void buttonDel_Click(object sender, EventArgs e)
        {
            if (_textNumber != null)
            {
                try
                {
                    if (_textNumber.Text.Length > 0)
                    {
                        _textNumber.Focus();
                        int __newSelectindex = (_textNumber.SelectionStart == _textNumber.Text.Length) ? _textNumber.Text.Length - 1 : _textNumber.SelectionStart - 1;
                        _textNumber.Text = _textNumber.Text.Remove(((_textNumber.SelectionStart == _textNumber.Text.Length) ? _textNumber.Text.Length - 1 : _textNumber.SelectionStart - 1), 1);
                        _textNumber.SelectionStart = __newSelectindex;
                    }
                }
                catch
                {
                }
            }
        }

        private void _buttonNumber_click(object sender, EventArgs e)
        {
            if (_textNumber != null)
            {
                Button _sender = (Button)sender;
                if (_textNumber.SelectionStart == _textNumber.Text.Length)
                {
                    _textNumber.Text += (_textNumber.Text.IndexOf("%") == -1) ? _sender.Text : "";
                    _textNumber.Focus();
                    _textNumber.SelectionStart = _textNumber.Text.Length;
                }
                else
                {
                    _textNumber.Focus();
                    int __index = _textNumber.SelectionStart;
                    if (_textNumber.Text.IndexOf("%") == -1)
                    {
                        _textNumber.Text = _textNumber.Text.Insert(__index, _sender.Text);
                        _textNumber.SelectionStart = __index + 1;

                    }
                    else
                        _textNumber.SelectionStart = __index;
                }
            }
        }

        private void _buttonDot_Click(object sender, EventArgs e)
        {
            if (_textNumber != null)
            {
                if (_textNumber.Text.IndexOf(".") == -1)
                {
                    if (_textNumber.SelectionStart == _textNumber.Text.Length)
                    {
                        _textNumber.Text += ".";
                        _textNumber.SelectionStart = _textNumber.Text.Length;
                    }
                    else
                    {
                        _textNumber.Focus();
                        int __index = _textNumber.SelectionStart;
                        _textNumber.Text = _textNumber.Text.Insert(__index, ".");
                        //_textNumber.Focus();
                        _textNumber.SelectionStart = __index + 1;
                    }
                }
                else
                {
                    _textNumber.Focus();
                    int __defaultIndex = _textNumber.SelectionStart;
                    _textNumber.SelectionStart = __defaultIndex;
                }
            }
        }

        private void _butonLeft_Click(object sender, EventArgs e)
        {
            if (_textNumber != null)
            {
                _textNumber.Focus();
                if (_textNumber.Text.Length > 0)
                    _textNumber.SelectionStart = _textNumber.SelectionStart - 1;
                else
                    _textNumber.SelectionStart = 0;
            }
        }

        private void _buttonRight_Click(object sender, EventArgs e)
        {
            if (_textNumber != null)
            {
                _textNumber.Focus();
                if (_textNumber.Text.Length <= _textNumber.Text.Length)
                    _textNumber.SelectionStart = _textNumber.SelectionStart + 1;
                else
                    _textNumber.SelectionStart = _textNumber.Text.Length;
            }
        }

        private void _buttonPercent_Click(object sender, EventArgs e)
        {
            if (_textNumber != null)
            {
                _textNumber.Focus();
                if (_textNumber.Text.IndexOf("%") == -1)
                    _textNumber.Text += "%";
                _textNumber.SelectionStart = _textNumber.Text.Length;
            }
        }

        private void _buttonTotalPrice_Click(object sender, EventArgs e)
        {
            this._textBoxCash.Text = (this._totalCustPay > 0) ? this._totalCustPay.ToString(this._formatNumber) : "";
        }

        private void _serviceButton_Click(object sender, EventArgs e)
        {
            this._serviceFocus();
        }

        private void _advanceMoneyButton_Click(object sender, EventArgs e)
        {
            _advanceAmountFocus();
        }

        private void _depositMoneyButton_Click(object sender, EventArgs e)
        {
            _depositAmountFocus();
        }

        private void _buttonCreditSave_Click(object sender, EventArgs e)
        {
            //this._creditSave();
            this._creditSaleFocus();

        }

        void _creditSaleFocus()
        {
            this._tabPayDetail.SelectedTab = this.tab_creditsale;
        }

        void _creditSave()
        {
            // เช็ค ลูกค้า
            if (_checkDefaultCust(this._custCodeTextbox.Text) == true)
            {
                MessageBox.Show("กรุณาเลือกลูกค้า");
                return;
            }

            // confirm 
            if (MessageBox.Show("คุณต้องการบันทีกรายการเป็นขายเงินเชื่อ", "ยืนยัน", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.No)
            {
                return;
            }

            if (this._custCode.Length > 0)
            {
                this._inquery_type = 0;
                // ปรับยอด
                _calc();

                this.Close();
                if (_firstClick == true)
                {
                    _firstClick = false;
                    this._saveData(this);
                }
            }
            else
            {
                MessageBox.Show("ไม่พบข้อมูลลูกค้า");
            }
        }

        void _searchCust()
        {
            MyLib._searchDataFull __search = new MyLib._searchDataFull();
            __search.Text = MyLib._myGlobal._resource("ค้นหาลูกค้า");
            __search._dataList._loadViewFormat(_g.g._search_screen_ar, MyLib._myGlobal._userSearchScreenGroup, false);
            __search.WindowState = FormWindowState.Maximized;
            __search._dataList._gridData._mouseClick += (s1, e1) =>
            {
                this._custCodeTextbox.Text = __search._dataList._gridData._cellGet(__search._dataList._gridData._selectRow, _g.d.ar_customer._table + "." + _g.d.ar_customer._code).ToString();
                this._custNameTextbox.Text = __search._dataList._gridData._cellGet(__search._dataList._gridData._selectRow, _g.d.ar_customer._table + "." + _g.d.ar_customer._name_1).ToString();
                this._custCode = this._custCodeTextbox.Text;
                __search.Close();
                __search.Dispose();
            };
            __search._searchEnterKeyPress += (s1, e1) =>
            {
                this._custCodeTextbox.Text = __search._dataList._gridData._cellGet(__search._dataList._gridData._selectRow, _g.d.ar_customer._table + "." + _g.d.ar_customer._code).ToString();
                this._custNameTextbox.Text = __search._dataList._gridData._cellGet(__search._dataList._gridData._selectRow, _g.d.ar_customer._table + "." + _g.d.ar_customer._name_1).ToString();
                this._custCode = this._custCodeTextbox.Text;
                __search.Close();
                __search.Dispose();
            };
            __search.ShowDialog();
        }

        private void _buttonFindCus_Click(object sender, EventArgs e)
        {
            this._searchCust();
        }

        private void _buttonSaleCreditConfirm_Click(object sender, EventArgs e)
        {
            this._creditSave();
        }

        private void _textbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                TextBox __getTextbox = (TextBox)sender;
                _moveCursor(__getTextbox.TabIndex + 1, this);
            }
            else if (e.KeyCode == Keys.Up)
            {
                TextBox __getTextbox = (TextBox)sender;
                _moveCursor(__getTextbox.TabIndex - 1, this);
            }
        }


        void _moveCursor(int nextControl, Control parent)
        {
            if (nextControl == 5)
            {
                nextControl = 1;
            }
            else if (nextControl == 0)
            {
                nextControl = 4;
            }

            foreach (Control __getControl in parent.Controls)
            {
                if (__getControl.GetType() == typeof(Panel))
                {
                    _moveCursor(nextControl, __getControl);
                }

                if (__getControl.GetType() == typeof(TextBox))
                {
                    // check tab index
                    TextBox __getTextbox = (TextBox)__getControl;

                    if (__getControl.Enabled == true && __getTextbox.ReadOnly == false)
                    {
                        if (__getControl.TabIndex == nextControl)
                        {
                            __getControl.Focus();
                            break;
                        }
                    }
                }
            }
        }


    }
}
