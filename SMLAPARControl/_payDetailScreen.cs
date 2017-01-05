using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace SMLERPAPARControl
{
    public class _payDetailScreen : MyLib._myScreen
    {
        public delegate void _refreshDataEvent(_payDetailScreen sender);
        public event _refreshDataEvent _refreshData;
        //
        //string __formatNumber = MyLib._myGlobal._getFormatNumber("m02");
        string __formatNumber = MyLib._myGlobal._getFormatNumber("m0" + _g.g._companyProfile._item_amount_decimal.ToString());
        int _buildCount = 0;
        private _g.g._transControlTypeEnum _ictransControlTypeTemp;

        public _g.g._transControlTypeEnum _icTransControlType
        {
            set
            {
                this._ictransControlTypeTemp = value;
                this._build();
                this.Invalidate();
            }
            get
            {
                return this._ictransControlTypeTemp;
            }
        }

        void _build()
        {
            if (this._icTransControlType == _g.g._transControlTypeEnum.ว่าง)
            {
                return;
            }
            if (this._buildCount++ > 0)
            {
                MessageBox.Show("_payDetailScreen สร้างมากกว่า 1 ครั้ง");
            }
            this._maxColumn = 2;
            this._table_name = _g.d.cb_trans._table;
            int __row = 0;

            switch (this._icTransControlType)
            {
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_ถอนเงิน:
                    this._addNumberBox(__row, 0, 1, 0, _g.d.cb_trans._total_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._cash_amount, 1, 2, true, __formatNumber);

                    if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccount || MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOS || MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountProfessional || MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOSProfessional)
                    {
                        this._addNumberBox(__row, 0, 1, 0, _g.d.cb_trans._total_expense_other, 1, 2, true, __formatNumber, true, _g.d.cb_trans._total_receive_other);
                    }
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._total_income_amount, 1, 2, true, __formatNumber);

                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._petty_cash_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._tranfer_amount, 1, 2, true, __formatNumber);
                    if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccount || MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOS || MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountProfessional || MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOSProfessional)
                    {
                        this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._total_income_other, 1, 2, true, __formatNumber, true, _g.d.cb_trans._total_extract);
                    }

                    __row++;
                    this._addNumberBox(__row, 0, 1, 0, _g.d.cb_trans._total_net_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._total_amount_pay, 1, 2, true, __formatNumber);
                    break;
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_ฝากเงิน:
                    // ด้านซ้าย
                    this._addNumberBox(__row, 0, 1, 0, _g.d.cb_trans._total_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._cash_amount, 1, 2, true, __formatNumber);

                    if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccount || MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOS || MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountProfessional || MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOSProfessional)
                    {
                        this._addNumberBox(__row, 0, 1, 0, _g.d.cb_trans._total_expense_other, 1, 2, true, __formatNumber, true, _g.d.cb_trans._total_pay_other);
                    }

                    // ด้านขวา
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._total_income_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._petty_cash_amount, 1, 2, true, __formatNumber);
                    //this._addNumberBox(3, 1, 1, 0, _g.d.cb_trans._chq_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._tranfer_amount, 1, 2, true, __formatNumber);

                    if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccount || MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOS || MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountProfessional || MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOSProfessional)
                    {
                        this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._total_income_other, 1, 2, true, __formatNumber, true, _g.d.cb_trans._total_extract);
                    }

                    __row++;
                    this._addNumberBox(__row, 0, 1, 0, _g.d.cb_trans._total_net_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._total_amount_pay, 1, 2, true, __formatNumber);
                    break;
                case _g.g._transControlTypeEnum.เงินสดย่อย_เบิกเงินสดย่อย:
                    this._addNumberBox(__row, 0, 1, 0, _g.d.cb_trans._total_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._cash_amount, 1, 2, true, __formatNumber);

                    if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccount || MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOS || MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountProfessional || MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOSProfessional)
                    {
                        this._addNumberBox(__row, 0, 1, 0, _g.d.cb_trans._total_expense_other, 1, 2, true, __formatNumber, true, _g.d.cb_trans._total_pay_other);
                    }

                    //
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._total_income_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._petty_cash_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._chq_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._tranfer_amount, 1, 2, true, __formatNumber);
                    if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccount || MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOS || MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountProfessional || MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOSProfessional)
                    {
                        this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._total_income_other, 1, 2, true, __formatNumber, true, _g.d.cb_trans._total_extract);
                    }

                    __row++;
                    this._addNumberBox(__row, 0, 1, 0, _g.d.cb_trans._total_net_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row, 1, 1, 0, _g.d.cb_trans._total_amount_pay, 1, 2, true, __formatNumber);
                    break;
                case _g.g._transControlTypeEnum.เงินสดย่อย_รับคืนเงินสดย่อย:
                    this._addNumberBox(__row, 0, 1, 0, _g.d.cb_trans._total_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._cash_amount, 1, 2, true, __formatNumber);
                    if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccount || MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOS || MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountProfessional || MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOSProfessional)
                    {
                        this._addNumberBox(__row, 0, 1, 0, _g.d.cb_trans._total_expense_other, 1, 2, true, __formatNumber, true, _g.d.cb_trans._total_receive_other);
                    }
                    //
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._total_income_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._petty_cash_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._chq_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._tranfer_amount, 1, 2, true, __formatNumber);
                    if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccount || MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOS || MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountProfessional || MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOSProfessional)
                    {
                        this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._total_income_other, 1, 2, true, __formatNumber, true, _g.d.cb_trans._total_extract);
                    }

                    __row++;
                    this._addNumberBox(__row, 0, 1, 0, _g.d.cb_trans._total_net_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row, 1, 1, 0, _g.d.cb_trans._total_amount_pay, 1, 2, true, __formatNumber);
                    break;
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น:
                    this._addNumberBox(__row, 0, 1, 0, _g.d.cb_trans._total_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._cash_amount, 1, 2, true, __formatNumber);

                    if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccount || MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOS || MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountProfessional || MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOSProfessional)
                    {
                        this._addNumberBox(__row, 0, 1, 0, _g.d.cb_trans._total_expense_other, 1, 2, true, __formatNumber, true, _g.d.cb_trans._total_pay_other);
                    }

                    //
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._total_income_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._petty_cash_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._deposit_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._total_tax_at_pay, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._chq_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._tranfer_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._discount_amount, 1, 2, true, __formatNumber);
                    if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccount || MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOS || MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountProfessional || MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOSProfessional)
                    {
                        this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._total_income_other, 1, 2, true, __formatNumber, true, _g.d.cb_trans._total_extract);
                    }

                    __row++;
                    this._addNumberBox(__row, 0, 1, 0, _g.d.cb_trans._total_net_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._total_amount_pay, 1, 2, true, __formatNumber);
                    break;
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_ลดหนี้:
                    this._addNumberBox(__row, 0, 1, 0, _g.d.cb_trans._total_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._cash_amount, 1, 2, true, __formatNumber);

                    if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccount || MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOS || MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountProfessional || MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOSProfessional)
                    {
                        this._addNumberBox(__row++, 0, 1, 0, _g.d.cb_trans._total_expense_other, 1, 2, true, __formatNumber, true, _g.d.cb_trans._total_receive_other);
                    }

                    //
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._total_income_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._petty_cash_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._deposit_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._total_tax_at_pay, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._chq_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._tranfer_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._discount_amount, 1, 2, true, __formatNumber);
                    if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccount || MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOS || MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountProfessional || MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOSProfessional)
                    {
                        this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._total_income_other, 1, 2, true, __formatNumber, true, _g.d.cb_trans._total_extract);
                    }

                    __row++;
                    this._addNumberBox(__row, 0, 1, 0, _g.d.cb_trans._total_net_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._total_amount_pay, 1, 2, true, __formatNumber);
                    break;
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายจ่ายอื่น_เพิ่มหนี้:
                    this._addNumberBox(__row, 0, 1, 0, _g.d.cb_trans._total_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._cash_amount, 1, 2, true, __formatNumber);
                    if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccount || MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOS || MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountProfessional || MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOSProfessional)
                    {
                        this._addNumberBox(__row, 0, 1, 0, _g.d.cb_trans._total_expense_other, 1, 2, true, __formatNumber, true, _g.d.cb_trans._total_pay_other);
                    }

                    //
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._total_income_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._petty_cash_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._deposit_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._total_tax_at_pay, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._chq_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._tranfer_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._discount_amount, 1, 2, true, __formatNumber);
                    if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccount || MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOS || MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountProfessional || MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOSProfessional)
                    {
                        this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._total_income_other, 1, 2, true, __formatNumber, true, _g.d.cb_trans._total_extract);
                    }
                    __row++;
                    this._addNumberBox(__row, 0, 1, 0, _g.d.cb_trans._total_net_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._total_amount_pay, 1, 2, true, __formatNumber);
                    break;
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_ลดหนี้:
                    this._addNumberBox(__row, 0, 1, 0, _g.d.cb_trans._total_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._cash_amount, 1, 2, true, __formatNumber);
                    if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccount || MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOS || MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountProfessional || MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOSProfessional)
                    {
                        this._addNumberBox(__row, 0, 1, 0, _g.d.cb_trans._total_expense_other, 1, 2, true, __formatNumber, true, _g.d.cb_trans._total_pay_other);
                    }
                    //
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._total_income_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._petty_cash_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._deposit_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._total_tax_at_pay, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._chq_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._tranfer_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._discount_amount, 1, 2, true, __formatNumber);
                    if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccount || MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOS || MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountProfessional || MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOSProfessional)
                    {
                        this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._total_income_other, 1, 2, true, __formatNumber, true, _g.d.cb_trans._total_extract);
                    }
                    __row++;

                    this._addNumberBox(__row, 1, 1, 0, _g.d.cb_trans._total_amount_pay, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 0, 1, 0, _g.d.cb_trans._total_net_amount, 1, 2, true, __formatNumber);
                    break;
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น:
                    this._addNumberBox(__row, 0, 1, 0, _g.d.cb_trans._total_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._cash_amount, 1, 2, true, __formatNumber);
                    if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccount || MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOS || MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountProfessional || MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOSProfessional)
                    {
                        this._addNumberBox(__row, 0, 1, 0, _g.d.cb_trans._total_expense_other, 1, 2, true, __formatNumber, true, _g.d.cb_trans._total_receive_other);
                    }

                    //
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._total_income_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._petty_cash_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._deposit_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._total_tax_at_pay, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._chq_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._tranfer_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._card_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._discount_amount, 1, 2, true, __formatNumber);
                    if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccount || MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOS || MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountProfessional || MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOSProfessional)
                    {
                        this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._total_income_other, 1, 2, true, __formatNumber, true, _g.d.cb_trans._total_extract);
                    }
                    __row++;
                    this._addNumberBox(__row, 0, 1, 0, _g.d.cb_trans._total_net_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._total_amount_pay, 1, 2, true, __formatNumber);
                    break;
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_รายได้อื่น_เพิ่มหนี้:
                    this._addNumberBox(__row, 0, 1, 0, _g.d.cb_trans._total_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._cash_amount, 1, 2, true, __formatNumber);
                    if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccount || MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOS || MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountProfessional || MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOSProfessional)
                    {
                        this._addNumberBox(__row, 0, 1, 0, _g.d.cb_trans._total_expense_other, 1, 2, true, __formatNumber, true, _g.d.cb_trans._total_receive_other);
                    }
                    //
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._total_income_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._petty_cash_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._deposit_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._total_tax_at_pay, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._chq_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._tranfer_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._card_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._discount_amount, 1, 2, true, __formatNumber);
                    if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccount || MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOS || MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountProfessional || MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOSProfessional)
                    {
                        this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._total_income_other, 1, 2, true, __formatNumber, true, _g.d.cb_trans._total_extract);
                    }
                    __row++;
                    this._addNumberBox(__row, 0, 1, 0, _g.d.cb_trans._total_net_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._total_amount_pay, 1, 2, true, __formatNumber);
                    break;
                case _g.g._transControlTypeEnum.ลูกหนี้_รับชำระหนี้:
                    this._addNumberBox(__row, 0, 1, 0, _g.d.cb_trans._total_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._cash_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row, 0, 1, 0, _g.d.cb_trans._total_credit_charge, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._total_income_amount, 1, 2, true, __formatNumber);

                    if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccount || MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOS || MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountProfessional || MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOSProfessional)
                    {
                        this._addNumberBox(__row, 0, 1, 0, _g.d.cb_trans._total_expense_other, 1, 2, true, __formatNumber, true, _g.d.cb_trans._total_receive_other);
                    }
                    //
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._petty_cash_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._deposit_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._total_tax_at_pay, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._chq_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._tranfer_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._card_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._discount_amount, 1, 2, true, __formatNumber);
                    if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccount || MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOS || MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountProfessional || MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOSProfessional)
                    {
                        this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._total_income_other, 1, 2, true, __formatNumber, true, _g.d.cb_trans._total_extract);
                    }
                    __row++;
                    this._addNumberBox(__row, 0, 1, 0, _g.d.cb_trans._total_net_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._total_amount_pay, 1, 2, true, __formatNumber);
                    break;
                case _g.g._transControlTypeEnum.เจ้าหนี้_จ่ายชำระหนี้:
                    this._addNumberBox(__row, 0, 1, 0, _g.d.cb_trans._total_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._cash_amount, 1, 2, true, __formatNumber);
                    if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccount || MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOS || MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountProfessional || MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOSProfessional)
                    {
                        this._addNumberBox(__row, 0, 1, 0, _g.d.cb_trans._total_expense_other, 1, 2, true, __formatNumber, true, _g.d.cb_trans._total_pay_other);
                    }
                    //
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._total_income_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._petty_cash_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._deposit_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._total_tax_at_pay, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._chq_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._tranfer_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._discount_amount, 1, 2, true, __formatNumber);
                    if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccount || MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOS || MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountProfessional || MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOSProfessional)
                    {
                        this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._total_income_other, 1, 2, true, __formatNumber, true, _g.d.cb_trans._total_extract);
                    }
                    __row++;
                    this._addNumberBox(__row, 0, 1, 0, _g.d.cb_trans._total_net_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._total_amount_pay, 1, 2, true, __formatNumber);
                    break;
                case _g.g._transControlTypeEnum.ขาย_รับคืนสินค้าจากการขายและลดหนี้:
                    this._addNumberBox(__row, 0, 1, 0, _g.d.cb_trans._total_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._cash_amount, 1, 2, true, __formatNumber);
                    if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccount || MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOS || MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountProfessional || MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOSProfessional)
                    {
                        this._addNumberBox(__row, 0, 1, 0, _g.d.cb_trans._total_expense_other, 1, 2, true, __formatNumber, true, _g.d.cb_trans._total_pay_other);
                    }
                    //
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._total_income_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._petty_cash_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._total_tax_at_pay, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._chq_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._tranfer_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._discount_amount, 1, 2, true, __formatNumber);
                    if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccount || MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOS || MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountProfessional || MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOSProfessional)
                    {
                        this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._total_income_other, 1, 2, true, __formatNumber, true, _g.d.cb_trans._total_extract);
                    }

                    __row++;
                    this._addNumberBox(__row, 0, 1, 0, _g.d.cb_trans._total_net_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._total_amount_pay, 1, 2, true, __formatNumber);
                    break;
                case _g.g._transControlTypeEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด:
                    this._addNumberBox(__row, 0, 1, 0, _g.d.cb_trans._total_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._cash_amount, 1, 2, true, __formatNumber);

                    if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccount || MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOS || MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountProfessional || MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOSProfessional)
                    {
                        this._addNumberBox(__row, 0, 1, 0, _g.d.cb_trans._total_expense_other, 1, 2, true, __formatNumber, true, _g.d.cb_trans._total_receive_other);
                    }

                    //
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._total_income_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._petty_cash_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._total_tax_at_pay, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._chq_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._card_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._tranfer_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._discount_amount, 1, 2, true, __formatNumber);
                    if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccount || MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOS || MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountProfessional || MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOSProfessional)
                    {
                        this._addNumberBox(8, 1, 1, 0, _g.d.cb_trans._total_income_other, 1, 2, true, __formatNumber, true, _g.d.cb_trans._total_extract);
                    }
                    __row++;
                    this._addNumberBox(10, 0, 1, 0, _g.d.cb_trans._total_net_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(10, 1, 1, 0, _g.d.cb_trans._total_amount_pay, 1, 2, true, __formatNumber);
                    break;
                case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ลดหนี้:
                    this._addNumberBox(__row, 0, 1, 0, _g.d.cb_trans._total_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._cash_amount, 1, 2, true, __formatNumber);
                    if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccount || MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOS || MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountProfessional || MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOSProfessional)
                    {
                        this._addNumberBox(__row, 0, 1, 0, _g.d.cb_trans._total_expense_other, 1, 2, true, __formatNumber, true, _g.d.cb_trans._total_receive_other);
                    }
                    //
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._total_income_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._petty_cash_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._deposit_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._total_tax_at_pay, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._chq_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._tranfer_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._discount_amount, 1, 2, true, __formatNumber);

                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._total_income_other, 1, 2, true, __formatNumber, true, _g.d.cb_trans._total_extract);

                    __row++;
                    this._addNumberBox(__row, 0, 1, 0, _g.d.cb_trans._total_net_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._total_amount_pay, 1, 2, true, __formatNumber);
                    break;
                case _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ:
                case _g.g._transControlTypeEnum.ขาย_ใบกำกับภาษีอย่างเต็มออกแทน:

                    if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLBIllFree)
                    {
                        this._addNumberBox(0, 0, 1, 0, _g.d.cb_trans._total_amount, 1, 2, true, __formatNumber);
                        this._addNumberBox(1, 0, 1, 0, _g.d.cb_trans._total_credit_charge, 1, 2, true, __formatNumber);

                        this._addNumberBox(9, 0, 1, 0, _g.d.cb_trans._total_net_amount, 1, 2, true, __formatNumber);
                        //

                        this._addNumberBox(0, 1, 1, 0, _g.d.cb_trans._cash_amount, 1, 2, true, __formatNumber);
                        this._addNumberBox(1, 1, 1, 0, _g.d.cb_trans._total_income_amount, 1, 2, true, __formatNumber);
                        this._addNumberBox(2, 1, 1, 0, _g.d.cb_trans._total_tax_at_pay, 1, 2, true, __formatNumber);
                        this._addNumberBox(3, 1, 1, 0, _g.d.cb_trans._chq_amount, 1, 2, true, __formatNumber);
                        this._addNumberBox(4, 1, 1, 0, _g.d.cb_trans._tranfer_amount, 1, 2, true, __formatNumber);
                        this._addNumberBox(5, 1, 1, 0, _g.d.cb_trans._card_amount, 1, 2, true, __formatNumber);
                        this._addNumberBox(6, 1, 1, 0, _g.d.cb_trans._discount_amount, 1, 2, true, __formatNumber);

                        this._addNumberBox(9, 1, 1, 0, _g.d.cb_trans._total_amount_pay, 1, 2, true, __formatNumber);

                    }
                    else
                    {

                        this._addNumberBox(__row, 0, 1, 0, _g.d.cb_trans._total_amount, 1, 2, true, __formatNumber);
                        this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._cash_amount, 1, 2, true, __formatNumber);

                        this._addNumberBox(__row, 0, 1, 0, _g.d.cb_trans._total_credit_charge, 1, 2, true, __formatNumber);
                        this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._total_income_amount, 1, 2, true, __formatNumber);
                        if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccount || MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOS || MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountProfessional || MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOSProfessional)
                        {
                            this._addNumberBox(__row, 0, 1, 0, _g.d.cb_trans._total_expense_other, 1, 2, true, __formatNumber, true, _g.d.cb_trans._total_receive_other);
                        }

                        //
                        this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._petty_cash_amount, 1, 2, true, __formatNumber);


                        this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._deposit_amount, 1, 2, true, __formatNumber);
                        if (MyLib._myGlobal._programName.Equals("SML CM"))
                        {
                            this._addNumberBox(__row, 0, 1, 0, _g.d.cb_trans._pay_cash_amount, 1, 2, true, __formatNumber, true, _g.d.cb_trans._receive_money);
                        }

                        this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._total_tax_at_pay, 1, 2, true, __formatNumber);
                        if (MyLib._myGlobal._programName.Equals("SML CM"))
                        {
                            this._addNumberBox(__row, 0, 1, 0, _g.d.cb_trans._money_change, 1, 2, true, __formatNumber);
                        }
                        this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._chq_amount, 1, 2, true, __formatNumber);
                        this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._tranfer_amount, 1, 2, true, __formatNumber);
                        this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._card_amount, 1, 2, true, __formatNumber);
                        this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._coupon_amount, 1, 2, true, __formatNumber);
                        this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._point_qty, 1, 2, true, __formatNumber);
                        this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._point_rate, 1, 2, true, __formatNumber);
                        this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._point_amount, 1, 2, true, __formatNumber);
                        this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._discount_amount, 1, 2, true, __formatNumber);
                        if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccount || MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOS || MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountProfessional || MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOSProfessional)
                        {
                            this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._total_income_other, 1, 2, true, __formatNumber, true, _g.d.cb_trans._total_extract);
                        }
                        if (_g.g._companyProfile._multi_currency)
                        {
                            this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._total_other_currency, 1, 2, true, __formatNumber, true, _g.d.cb_trans._total_other_currency);
                        }
                        __row++;
                        this._addNumberBox(__row, 0, 1, 0, _g.d.cb_trans._total_net_amount, 1, 2, true, __formatNumber);
                        this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._total_amount_pay, 1, 2, true, __formatNumber);

                        if (MyLib._myGlobal._programName.Equals("SML CM"))
                        {
                            this._enabedControl(_g.d.cb_trans._cash_amount, false);

                            // change color
                            MyLib._myTextBox __getPayCashTextbox = (MyLib._myTextBox)this._getControl(_g.d.cb_trans._pay_cash_amount);
                            __getPayCashTextbox._label.ForeColor = Color.Red;

                            MyLib._myTextBox __getMonerChangeTextbox = (MyLib._myTextBox)this._getControl(_g.d.cb_trans._money_change);
                            __getMonerChangeTextbox._label.ForeColor = Color.Blue;
                        }
                    }
                    break;
                case _g.g._transControlTypeEnum.ขาย_รับเงินล่วงหน้า:
                    this._addNumberBox(__row, 0, 1, 0, _g.d.cb_trans._total_amount, 1, 2, true, __formatNumber, true, _g.d.cb_trans._amount_deposit_in);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._cash_amount, 1, 2, true, __formatNumber);
                    if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccount ||
                        MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOS ||
                        MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountProfessional ||
                        MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOSProfessional)
                    {
                        this._addNumberBox(__row, 0, 1, 0, _g.d.cb_trans._total_expense_other, 1, 2, true, __formatNumber, true, _g.d.cb_trans._total_receive_other);
                    }
                    //
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._total_income_amount, 1, 2, true, __formatNumber);
                    //this._addNumberBox(2, 1, 1, 0, _g.d.cb_trans._total_tax_at_pay, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._petty_cash_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._chq_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._tranfer_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._card_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._discount_amount, 1, 2, true, __formatNumber);
                    if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccount ||
                        MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOS ||
                        MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountProfessional ||
                        MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOSProfessional)
                    {
                        this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._total_income_other, 1, 2, true, __formatNumber, true, _g.d.cb_trans._total_extract);
                    }
                    __row++;
                    this._addNumberBox(__row, 0, 1, 0, _g.d.cb_trans._total_net_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._total_amount_pay, 1, 2, true, __formatNumber);
                    break;
                case _g.g._transControlTypeEnum.ขาย_เงินล่วงหน้า_คืน:
                    this._addNumberBox(__row, 0, 1, 0, _g.d.cb_trans._total_amount, 1, 2, true, __formatNumber, true, _g.d.cb_trans._amount_deposit_return_1);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._cash_amount, 1, 2, true, __formatNumber);

                    if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccount ||
                        MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOS ||
                        MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountProfessional ||
                        MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOSProfessional)
                    {
                        this._addNumberBox(__row, 0, 1, 0, _g.d.cb_trans._total_expense_other, 1, 2, true, __formatNumber, true, _g.d.cb_trans._total_pay_other);
                    }

                    //
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._total_income_amount, 1, 2, true, __formatNumber);
                    //this._addNumberBox(2, 1, 1, 0, _g.d.cb_trans._total_tax_at_pay, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._petty_cash_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._chq_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._tranfer_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._discount_amount, 1, 2, true, __formatNumber);
                    if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccount ||
                        MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOS ||
                        MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountProfessional ||
                        MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOSProfessional)
                    {
                        this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._total_income_other, 1, 2, true, __formatNumber, true, _g.d.cb_trans._total_extract);
                    }

                    __row++;
                    this._addNumberBox(__row, 0, 1, 0, _g.d.cb_trans._total_net_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._total_amount_pay, 1, 2, true, __formatNumber);
                    break;
                case _g.g._transControlTypeEnum.ขาย_รับเงินมัดจำ:
                    //
                    this._addNumberBox(__row, 0, 1, 0, _g.d.cb_trans._total_amount, 1, 2, true, __formatNumber, true, _g.d.cb_trans._amount_deposit_in);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._cash_amount, 1, 2, true, __formatNumber);
                    if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccount ||
                        MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOS ||
                        MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountProfessional ||
                        MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOSProfessional)
                    {
                        this._addNumberBox(__row, 0, 1, 0, _g.d.cb_trans._total_expense_other, 1, 2, true, __formatNumber, true, _g.d.cb_trans._total_receive_other);
                    }


                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._total_income_amount, 1, 2, true, __formatNumber);
                    if (MyLib._myGlobal._isVersionEnum != MyLib._myGlobal._versionType.SMLPOSLite)
                    {
                        this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._total_tax_at_pay, 1, 2, true, __formatNumber);
                    }
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._petty_cash_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._chq_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._tranfer_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._card_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._discount_amount, 1, 2, true, __formatNumber);
                    if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccount ||
                        MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOS ||
                        MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountProfessional ||
                        MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOSProfessional)
                    {
                        this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._total_income_other, 1, 2, true, __formatNumber, true, _g.d.cb_trans._total_extract);
                    }
                    __row++;
                    this._addNumberBox(__row, 1, 1, 0, _g.d.cb_trans._total_amount_pay, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 0, 1, 0, _g.d.cb_trans._total_net_amount, 1, 2, true, __formatNumber);
                    break;
                case _g.g._transControlTypeEnum.ขาย_เงินมัดจำ_คืน:

                    this._addNumberBox(__row, 0, 1, 0, _g.d.cb_trans._total_amount, 1, 2, true, __formatNumber, true, _g.d.cb_trans._amount_deposit_return_1);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._cash_amount, 1, 2, true, __formatNumber);

                    if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccount ||
                        MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOS ||
                        MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountProfessional ||
                        MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOSProfessional)
                    {
                        this._addNumberBox(__row, 0, 1, 0, _g.d.cb_trans._total_expense_other, 1, 2, true, __formatNumber, true, _g.d.cb_trans._total_pay_other);
                    }

                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._total_income_amount, 1, 2, true, __formatNumber);
                    if (MyLib._myGlobal._isVersionEnum != MyLib._myGlobal._versionType.SMLPOSLite)
                    {
                        this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._total_tax_at_pay, 1, 2, true, __formatNumber);
                    }
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._petty_cash_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._chq_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._tranfer_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._discount_amount, 1, 2, true, __formatNumber);
                    if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccount ||
                        MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOS ||
                        MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountProfessional ||
                        MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOSProfessional)
                    {
                        this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._total_income_other, 1, 2, true, __formatNumber, true, _g.d.cb_trans._total_extract);
                    }

                    __row++;
                    this._addNumberBox(__row, 1, 1, 0, _g.d.cb_trans._total_amount_pay, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 0, 1, 0, _g.d.cb_trans._total_net_amount, 1, 2, true, __formatNumber);
                    //
                    break;
                case _g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ:
                    this._addNumberBox(__row, 0, 1, 0, _g.d.cb_trans._total_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._cash_amount, 1, 2, true, __formatNumber);
                    if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccount ||
                        MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOS ||
                        MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountProfessional ||
                        MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOSProfessional)
                    {
                        this._addNumberBox(__row, 0, 1, 0, _g.d.cb_trans._total_expense_other, 1, 2, true, __formatNumber, true, _g.d.cb_trans._total_pay_other);
                    }

                    //
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._total_income_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._petty_cash_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._deposit_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._total_tax_at_pay, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._chq_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._tranfer_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._discount_amount, 1, 2, true, __formatNumber);
                    if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccount ||
                        MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOS ||
                        MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountProfessional ||
                        MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOSProfessional)
                    {
                        this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._total_income_other, 1, 2, true, __formatNumber, true, _g.d.cb_trans._total_extract);
                    }
                    __row++;
                    this._addNumberBox(__row, 0, 1, 0, _g.d.cb_trans._total_net_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._total_amount_pay, 1, 2, true, __formatNumber);
                    break;
                case _g.g._transControlTypeEnum.ซื้อ_พาเชียล_ตั้งหนี้:
                    this._addNumberBox(__row, 0, 1, 0, _g.d.cb_trans._total_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._cash_amount, 1, 2, true, __formatNumber);
                    if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccount ||
                        MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOS ||
                        MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountProfessional ||
                        MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOSProfessional)
                    {
                        this._addNumberBox(__row, 0, 1, 0, _g.d.cb_trans._total_expense_other, 1, 2, true, __formatNumber, true, _g.d.cb_trans._total_pay_other);
                    }

                    //
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._total_income_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._petty_cash_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._deposit_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._total_tax_at_pay, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._chq_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._tranfer_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._discount_amount, 1, 2, true, __formatNumber);
                    if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccount ||
                        MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOS ||
                        MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountProfessional ||
                        MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOSProfessional)
                    {
                        this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._total_income_other, 1, 2, true, __formatNumber, true, _g.d.cb_trans._total_extract);
                    }
                    __row++;
                    this._addNumberBox(__row, 0, 1, 0, _g.d.cb_trans._total_net_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._total_amount_pay, 1, 2, true, __formatNumber);
                    break;
                case _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า:
                    this._addNumberBox(__row, 0, 1, 0, _g.d.cb_trans._total_amount, 1, 2, true, __formatNumber, true, _g.d.cb_trans._amount_deposit_out);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._cash_amount, 1, 2, true, __formatNumber);
                    if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccount ||
                        MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOS ||
                        MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountProfessional ||
                        MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOSProfessional)
                    {
                        this._addNumberBox(__row, 0, 1, 0, _g.d.cb_trans._total_expense_other, 1, 2, true, __formatNumber, true, _g.d.cb_trans._total_pay_other);
                    }

                    //
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._total_income_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._petty_cash_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._chq_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._tranfer_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._discount_amount, 1, 2, true, __formatNumber);
                    if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccount ||
                        MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOS ||
                        MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountProfessional ||
                        MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOSProfessional)
                    {
                        this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._total_income_other, 1, 2, true, __formatNumber, true, _g.d.cb_trans._total_extract);
                    }
                    __row++;
                    this._addNumberBox(__row, 0, 1, 0, _g.d.cb_trans._total_net_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._total_amount_pay, 1, 2, true, __formatNumber);
                    break;
                case _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า_รับคืน:
                    this._addNumberBox(__row, 0, 1, 0, _g.d.cb_trans._total_amount, 1, 2, true, __formatNumber, true, _g.d.cb_trans._amount_deposit_return);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._cash_amount, 1, 2, true, __formatNumber);
                    if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccount ||
                        MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOS ||
                        MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountProfessional ||
                        MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOSProfessional)
                    {
                        this._addNumberBox(__row, 0, 1, 0, _g.d.cb_trans._total_expense_other, 1, 2, true, __formatNumber, true, _g.d.cb_trans._total_receive_other);
                    }
                    //
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._total_income_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._petty_cash_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._chq_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._tranfer_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._card_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._discount_amount, 1, 2, true, __formatNumber);
                    if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccount ||
                        MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOS ||
                        MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountProfessional ||
                        MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOSProfessional)
                    {
                        this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._total_income_other, 1, 2, true, __formatNumber, true, _g.d.cb_trans._total_extract);
                    }
                    __row++;
                    this._addNumberBox(__row, 0, 1, 0, _g.d.cb_trans._total_net_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._total_amount_pay, 1, 2, true, __formatNumber);
                    break;
                case _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินมัดจำ:
                    this._addNumberBox(__row, 0, 1, 0, _g.d.cb_trans._total_amount, 1, 2, true, __formatNumber, true, _g.d.cb_trans._amount_deposit_out);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._cash_amount, 1, 2, true, __formatNumber);
                    if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccount ||
                        MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOS ||
                        MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountProfessional ||
                        MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOSProfessional)
                    {
                        this._addNumberBox(__row, 0, 1, 0, _g.d.cb_trans._total_expense_other, 1, 2, true, __formatNumber, true, _g.d.cb_trans._total_pay_other);
                    }
                    //
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._total_income_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._petty_cash_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._chq_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._total_tax_at_pay, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._tranfer_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._discount_amount, 1, 2, true, __formatNumber);
                    if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccount ||
                        MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOS ||
                        MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountProfessional ||
                        MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOSProfessional)
                    {
                        this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._total_income_other, 1, 2, true, __formatNumber, true, _g.d.cb_trans._total_extract);
                    }
                    __row++;
                    this._addNumberBox(__row, 0, 1, 0, _g.d.cb_trans._total_net_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._total_amount_pay, 1, 2, true, __formatNumber);
                    break;
                case _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินมัดจำ_รับคืน:
                    this._addNumberBox(__row, 0, 1, 0, _g.d.cb_trans._total_amount, 1, 2, true, __formatNumber, true, _g.d.cb_trans._amount_deposit_return);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._cash_amount, 1, 2, true, __formatNumber);
                    if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccount ||
                        MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOS ||
                        MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountProfessional ||
                        MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOSProfessional)
                    {
                        this._addNumberBox(__row, 0, 1, 0, _g.d.cb_trans._total_expense_other, 1, 2, true, __formatNumber, true, _g.d.cb_trans._total_receive_other);
                    }
                    //
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._total_income_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._petty_cash_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._chq_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._tranfer_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._card_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._discount_amount, 1, 2, true, __formatNumber);
                    if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccount ||
                        MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOS ||
                        MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountProfessional ||
                        MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOSProfessional)
                    {
                        this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._total_income_other, 1, 2, true, __formatNumber, true, _g.d.cb_trans._total_extract);
                    }
                    __row++;
                    this._addNumberBox(__row, 0, 1, 0, _g.d.cb_trans._total_net_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._total_amount_pay, 1, 2, true, __formatNumber);
                    break;
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็ครับ_เปลี่ยนเช็ค:
                    this._addNumberBox(__row, 0, 1, 0, _g.d.cb_trans._total_amount, 1, 2, true, __formatNumber, true, _g.d.cb_trans._amount_deposit_in);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._cash_amount, 1, 2, true, __formatNumber);
                    if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccount ||
                        MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOS ||
                        MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountProfessional ||
                        MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOSProfessional)
                    {
                        this._addNumberBox(__row, 0, 1, 0, _g.d.cb_trans._total_expense_other, 1, 2, true, __formatNumber, true, _g.d.cb_trans._total_receive_other);
                    }
                    //
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._total_income_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._petty_cash_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._chq_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._tranfer_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._deposit_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._card_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._discount_amount, 1, 2, true, __formatNumber);
                    if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccount ||
                        MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOS ||
                        MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountProfessional ||
                        MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOSProfessional)
                    {
                        this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._total_income_other, 1, 2, true, __formatNumber, true, _g.d.cb_trans._total_extract);
                    }
                    __row++;
                    this._addNumberBox(__row, 0, 1, 0, _g.d.cb_trans._total_net_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._total_amount_pay, 1, 2, true, __formatNumber);

                    break;
                case _g.g._transControlTypeEnum.เงินสดธนาคาร_เช็คจ่าย_เปลี่ยนเช็ค:
                    this._addNumberBox(__row, 0, 1, 0, _g.d.cb_trans._total_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._cash_amount, 1, 2, true, __formatNumber);
                    if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccount ||
                        MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOS ||
                        MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountProfessional ||
                        MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOSProfessional)
                    {
                        this._addNumberBox(__row, 0, 1, 0, _g.d.cb_trans._total_expense_other, 1, 2, true, __formatNumber, true, _g.d.cb_trans._total_pay_other);
                    }
                    //
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._total_income_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._petty_cash_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._deposit_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._chq_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._tranfer_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._discount_amount, 1, 2, true, __formatNumber);
                    if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccount ||
                        MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOS ||
                        MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountProfessional ||
                        MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOSProfessional)
                    {
                        this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._total_income_other, 1, 2, true, __formatNumber, true, _g.d.cb_trans._total_extract);
                    }
                    __row++;
                    this._addNumberBox(__row, 0, 1, 0, _g.d.cb_trans._total_net_amount, 1, 2, true, __formatNumber);
                    this._addNumberBox(__row++, 1, 1, 0, _g.d.cb_trans._total_amount_pay, 1, 2, true, __formatNumber);

                    break;
                default:
                    if (SystemInformation.ComputerName.ToLower().IndexOf("jead8") != -1 || SystemInformation.ComputerName.ToLower().IndexOf("toe-pc") != -1)
                    {
                        MessageBox.Show("pay detail Case not found : " + this._icTransControlType.ToString());
                    }
                    break;
            }
            //
            if (MyLib._myGlobal._isVersionEnum != MyLib._myGlobal._versionType.SMLBIllFree)
            {
                this._enabedControl(_g.d.cb_trans._petty_cash_amount, false);
                this._enabedControl(_g.d.cb_trans._total_amount, false);
                this._enabedControl(_g.d.cb_trans._total_credit_charge, false);
                this._enabedControl(_g.d.cb_trans._total_net_amount, false);
                this._enabedControl(_g.d.cb_trans._tranfer_amount, false);
                this._enabedControl(_g.d.cb_trans._chq_amount, false);
                this._enabedControl(_g.d.cb_trans._card_amount, false);
                this._enabedControl(_g.d.cb_trans._coupon_amount, false);
                this._enabedControl(_g.d.cb_trans._point_amount, false);
                this._enabedControl(_g.d.cb_trans._deposit_amount, false);
                this._enabedControl(_g.d.cb_trans._total_tax_at_pay, false);
                this._enabedControl(_g.d.cb_trans._total_amount_pay, false);
                this._enabedControl(_g.d.cb_trans._total_expense_other, false);
                this._enabedControl(_g.d.cb_trans._total_income_other, false);

            }
            this._textBoxChanged += new MyLib.TextBoxChangedHandler(_payDetailScreen__textBoxChanged);
            //
            this.Invalidate();
        }

        public void _recalc()
        {
            decimal __totalAmont = 0M;
            decimal __totalAmountPay = 0M;
            //
            __totalAmont = this._getDataNumber(_g.d.cb_trans._total_amount) + this._getDataNumber(_g.d.cb_trans._total_credit_charge) + this._getDataNumber(_g.d.cb_trans._total_expense_other);
            __totalAmountPay =
                this._getDataNumber(_g.d.cb_trans._cash_amount) +
                this._getDataNumber(_g.d.cb_trans._petty_cash_amount) +
                this._getDataNumber(_g.d.cb_trans._total_income_amount) +
                this._getDataNumber(_g.d.cb_trans._deposit_amount) +
                this._getDataNumber(_g.d.cb_trans._total_tax_at_pay) +
                this._getDataNumber(_g.d.cb_trans._chq_amount) +
                this._getDataNumber(_g.d.cb_trans._card_amount) +
                this._getDataNumber(_g.d.cb_trans._tranfer_amount) +
                this._getDataNumber(_g.d.cb_trans._point_amount) +
                this._getDataNumber(_g.d.cb_trans._discount_amount) +
                this._getDataNumber(_g.d.cb_trans._coupon_amount) +
                this._getDataNumber(_g.d.cb_trans._total_income_other) +
                this._getDataNumber(_g.d.cb_trans._total_other_currency);
            /*switch (this._icTransControlType)
            {
                case _g.g._icTransControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ:
                    __totalAmont =
                        this._getDataNumber(_g.d.cb_trans._total_amount) +
                        this._getDataNumber(_g.d.cb_trans._total_other_amount);

                    __totalAmountPay =
                        this._getDataNumber(_g.d.cb_trans._cash_amount) +
                        this._getDataNumber(_g.d.cb_trans._petty_cash_amount) +
                        this._getDataNumber(_g.d.cb_trans._total_income_amount) +
                        this._getDataNumber(_g.d.cb_trans._deposit_amount) +
                        this._getDataNumber(_g.d.cb_trans._advance_amount) +
                        this._getDataNumber(_g.d.cb_trans._total_tax_at_pay) +
                        this._getDataNumber(_g.d.cb_trans._chq_amount) +
                        this._getDataNumber(_g.d.cb_trans._tranfer_amount);
                    break;
                case _g.g._icTransControlTypeEnum.ซื้อ_จ่ายเงินล่วงหน้า:
                    __totalAmont =
                        this._getDataNumber(_g.d.cb_trans._total_amount) +
                        this._getDataNumber(_g.d.cb_trans._total_other_amount);

                    __totalAmountPay =
                        this._getDataNumber(_g.d.cb_trans._cash_amount) +
                        this._getDataNumber(_g.d.cb_trans._petty_cash_amount) +
                        this._getDataNumber(_g.d.cb_trans._total_income_amount) +
                        this._getDataNumber(_g.d.cb_trans._chq_amount) +
                        this._getDataNumber(_g.d.cb_trans._tranfer_amount);
                    break;
                case _g.g._icTransControlTypeEnum.ซื้อ_รับคืนเงินล่วงหน้า:
                    __totalAmont =
                        this._getDataNumber(_g.d.cb_trans._total_amount) +
                        this._getDataNumber(_g.d.cb_trans._total_other_amount);

                    __totalAmountPay =
                        this._getDataNumber(_g.d.cb_trans._cash_amount) +
                        this._getDataNumber(_g.d.cb_trans._petty_cash_amount) +
                        this._getDataNumber(_g.d.cb_trans._total_income_amount) +
                        this._getDataNumber(_g.d.cb_trans._chq_amount) +
                        this._getDataNumber(_g.d.cb_trans._tranfer_amount);
                    break;
            }*/
            this._setDataNumber(_g.d.cb_trans._total_net_amount, __totalAmont);
            this._setDataNumber(_g.d.cb_trans._total_amount_pay, __totalAmountPay);

            if (MyLib._myGlobal._programName.Equals("SML CM") && this._icTransControlType == _g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ)
            {
                // หาเงินทอน
                decimal __payCash = this._getDataNumber(_g.d.cb_trans._pay_cash_amount);
                if (__payCash > 0)
                {
                    decimal __cashAmount = this._getDataNumber(_g.d.cb_trans._cash_amount);

                    decimal __moneyChange = __payCash - __cashAmount;
                    this._setDataNumber(_g.d.cb_trans._money_change, __moneyChange);
                }
            }
        }

        void _payDetailScreen__textBoxChanged(object sender, string name)
        {

            if (name.Equals(_g.d.cb_trans._pay_cash_amount))
            {
                decimal __totalAmont = 0M;
                decimal __totalAmountPay = 0M;
                decimal __payCash = this._getDataNumber(_g.d.cb_trans._pay_cash_amount);
                //
                __totalAmont = this._getDataNumber(_g.d.cb_trans._total_amount) + this._getDataNumber(_g.d.cb_trans._total_credit_charge) + this._getDataNumber(_g.d.cb_trans._total_expense_other);
                __totalAmountPay = this._getDataNumber(_g.d.cb_trans._petty_cash_amount) +
                    this._getDataNumber(_g.d.cb_trans._total_income_amount) +
                    this._getDataNumber(_g.d.cb_trans._deposit_amount) +
                    this._getDataNumber(_g.d.cb_trans._total_tax_at_pay) +
                    this._getDataNumber(_g.d.cb_trans._chq_amount) +
                    this._getDataNumber(_g.d.cb_trans._card_amount) +
                    this._getDataNumber(_g.d.cb_trans._tranfer_amount) +
                    this._getDataNumber(_g.d.cb_trans._point_amount) +
                    this._getDataNumber(_g.d.cb_trans._discount_amount) +
                    this._getDataNumber(_g.d.cb_trans._coupon_amount) +
                    this._getDataNumber(_g.d.cb_trans._total_income_other) +
                    this._getDataNumber(_g.d.cb_trans._total_other_currency);

                decimal __payCashAmount = __totalAmont - __totalAmountPay;
                this._setDataNumber(_g.d.cb_trans._cash_amount, __payCashAmount);
            }
            this._recalc();
            if (this._refreshData != null)
            {
                this._refreshData(this);
            }
        }
    }
}
