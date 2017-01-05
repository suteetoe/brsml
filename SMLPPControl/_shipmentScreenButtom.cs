using System;
using System.Collections.Generic;
using System.Text;

namespace SMLPPControl
{
    public class _shipmentScreenButtom : MyLib._myScreen
    {
        private SMLPPGlobal.g._ppControlTypeEnum _transControlType = SMLPPGlobal.g._ppControlTypeEnum.ว่าง;

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

        void build()
        {
            this.SuspendLayout();
            this._reset();
            string __formatNumber = MyLib._myGlobal._getFormatNumber(_g.g._getFormatNumberStr(3));
            string __formatNumberNone = MyLib._myGlobal._getFormatNumber("m00");

            if (this.transControlType != SMLPPGlobal.g._ppControlTypeEnum.ว่าง)
            {
                switch (this.transControlType)
                {
                    case SMLPPGlobal.g._ppControlTypeEnum.SaleShipment:
                        {
                            this._maxColumn = 4;
                            this._table_name = _g.d.ic_trans._table;
                            this._addNumberBox(0, 0, 1, 0, _g.d.ic_trans._credit_day, 1, 2, true, __formatNumberNone);
                            this._addDateBox(0, 1, 1, 0, _g.d.ic_trans._credit_date, 1, true, true);
                            this._addNumberBox(1, 0, 1, 0, _g.d.ic_trans._send_day, 1, 2, true, __formatNumberNone);
                            this._addDateBox(1, 1, 1, 0, _g.d.ic_trans._send_date, 1, true, true);
                            this._addTextBox(2, 0, 2, 0, _g.d.ic_trans._remark, 2, 1, 0, true, false, true);
                            this._addNumberBox(0, 2, 1, 0, _g.d.ic_trans._vat_rate, 1, 2, true, __formatNumber);
                            this._addNumberBox(0, 3, 1, 0, _g.d.ic_trans._total_value, 1, 2, true, __formatNumber);
                            this._addTextBox(1, 2, 1, 0, _g.d.ic_trans._discount_word, 1, 1, 0, true, false, true);
                            this._addNumberBox(1, 3, 1, 0, _g.d.ic_trans._total_discount, 1, 2, true, __formatNumber);

                            if (MyLib._myGlobal._isVersionEnum != MyLib._myGlobal._versionType.SMLBIllFree)
                            {
                                this._addNumberBox(2, 2, 1, 0, _g.d.ic_trans._advance_amount, 1, 2, true, __formatNumber);
                            }

                            this._addNumberBox(2, 3, 1, 0, _g.d.ic_trans._total_before_vat, 1, 2, true, __formatNumber);
                            this._addNumberBox(3, 2, 1, 0, _g.d.ic_trans._total_vat_value, 1, 2, true, __formatNumber);
                            this._addNumberBox(3, 3, 1, 0, _g.d.ic_trans._total_after_vat, 1, 2, true, __formatNumber);
                            this._addNumberBox(4, 2, 1, 0, _g.d.ic_trans._total_except_vat, 1, 2, true, __formatNumber);
                            this._addNumberBox(4, 3, 1, 0, _g.d.ic_trans._total_amount, 1, 2, true, __formatNumber);
                            if (_g.g._companyProfile._manual_total_enable)
                            {
                                this._addCheckBox(4, 1, _g.d.ic_trans._total_manual, true, false);
                            }
                            //

                            this._enabedControl(_g.d.ic_trans._credit_day, false);
                            this._enabedControl(_g.d.ic_trans._credit_date, false);
                            this._enabedControl(_g.d.ic_trans._send_day, false);
                            this._enabedControl(_g.d.ic_trans._send_date, false);
                            this._enabedControl(_g.d.ic_trans._vat_rate, false);
                            this._enabedControl(_g.d.ic_trans._total_value, false);
                            this._enabedControl(_g.d.ic_trans._discount_word, false);
                            this._enabedControl(_g.d.ic_trans._total_discount, false);
                            this._enabedControl(_g.d.ic_trans._advance_amount, false);
                            this._enabedControl(_g.d.ic_trans._total_before_vat, false);
                            this._enabedControl(_g.d.ic_trans._total_vat_value, false);
                            this._enabedControl(_g.d.ic_trans._total_after_vat, false);
                            this._enabedControl(_g.d.ic_trans._total_except_vat, false);
                            this._enabedControl(_g.d.ic_trans._total_amount, false);
                            this._enabedControl(_g.d.ic_trans._total_manual, false);
                        }
                        break;

                }
            }

            this.Invalidate();
            this.ResumeLayout();

        }

    }
}
