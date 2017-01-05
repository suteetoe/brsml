using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Diagnostics;
using System.Globalization;

namespace SMLERPAPARControl._depositControl
{
    public class _po_so_deposit_screen_bottom_control : MyLib._myScreen
    {
        public delegate _g.g._vatTypeEnum VatTypeEventHandler(object sender);
        public event VatTypeEventHandler _vatType;
        //
        public delegate void _afterCalcEvent();
        public event _afterCalcEvent _afterCalc;
        //
        private _g.g._transControlTypeEnum _ictransControlTypeTemp;
        MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
        int _buildCount = 0;

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
                MessageBox.Show("_PoSoDepositScreenBottomControl สร้างมากกว่า 1 ครั้ง");
            }
            this.SuspendLayout();
            this._reset();
            this._table_name = _g.d.ic_trans._table;
            string __formatNumberNone = MyLib._myGlobal._getFormatNumber("m00");
            string __formatNumber = MyLib._myGlobal._getFormatNumber(_g.g._getFormatNumberStr(3));
            switch (this._icTransControlType)
            {
                case _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินมัดจำ:
                    this._maxColumn = 3;
                    this._table_name = _g.d.ic_trans._table;
                    this._addNumberBox(0, 1, 1, 0, _g.d.ic_trans._vat_rate, 1, 2, true, __formatNumber);
                    this._addNumberBox(0, 2, 1, 0, _g.d.ic_trans._total_value, 1, 2, true, __formatNumber);
                    this._addNumberBox(1, 1, 1, 0, _g.d.ic_trans._total_before_vat, 1, 2, true, __formatNumber);
                    this._addNumberBox(1, 2, 1, 0, _g.d.ic_trans._total_vat_value, 1, 2, true, __formatNumber);
                    this._addNumberBox(2, 2, 1, 0, _g.d.ic_trans._total_amount, 1, 2, true, __formatNumber);
                    //
                    this._enabedControl(_g.d.ic_trans._vat_rate, (MyLib._myGlobal._useNoVat) ? true : false);
                    this._enabedControl(_g.d.ic_trans._total_before_vat, false);
                    this._enabedControl(_g.d.ic_trans._total_vat_value, false);
                    this._enabedControl(_g.d.ic_trans._total_amount, false);
                    break;
                case _g.g._transControlTypeEnum.ซื้อ_จ่ายเงินมัดจำ_รับคืน:
                    this._maxColumn = 3;
                    this._table_name = _g.d.ic_trans._table;
                    this._addNumberBox(0, 1, 1, 0, _g.d.ic_trans._vat_rate, 1, 2, true, __formatNumber);
                    this._addNumberBox(0, 2, 1, 0, _g.d.ic_trans._total_value, 1, 2, true, __formatNumber);
                    this._addNumberBox(1, 1, 1, 0, _g.d.ic_trans._total_before_vat, 1, 2, true, __formatNumber);
                    this._addNumberBox(1, 2, 1, 0, _g.d.ic_trans._total_vat_value, 1, 2, true, __formatNumber);
                    this._addNumberBox(2, 2, 1, 0, _g.d.ic_trans._total_amount, 1, 2, true, __formatNumber);
                    //
                    this._enabedControl(_g.d.ic_trans._vat_rate, (MyLib._myGlobal._useNoVat) ? true : false);
                    this._enabedControl(_g.d.ic_trans._total_before_vat, false);
                    this._enabedControl(_g.d.ic_trans._total_vat_value, false);
                    this._enabedControl(_g.d.ic_trans._total_amount, false);
                    break;
                case _g.g._transControlTypeEnum.ขาย_รับเงินมัดจำ:
                    this._maxColumn = 3;
                    this._table_name = _g.d.ic_trans._table;
                    this._addNumberBox(0, 1, 1, 0, _g.d.ic_trans._vat_rate, 1, 2, true, __formatNumber);
                    this._addNumberBox(0, 2, 1, 0, _g.d.ic_trans._total_value, 1, 2, true, __formatNumber);
                    this._addNumberBox(1, 1, 1, 0, _g.d.ic_trans._total_before_vat, 1, 2, true, __formatNumber);
                    this._addNumberBox(1, 2, 1, 0, _g.d.ic_trans._total_vat_value, 1, 2, true, __formatNumber);
                    this._addNumberBox(2, 2, 1, 0, _g.d.ic_trans._total_amount, 1, 2, true, __formatNumber);
                    //
                    this._enabedControl(_g.d.ic_trans._vat_rate, (MyLib._myGlobal._useNoVat) ? true : false);
                    this._enabedControl(_g.d.ic_trans._total_before_vat, false);
                    this._enabedControl(_g.d.ic_trans._total_vat_value, false);
                    this._enabedControl(_g.d.ic_trans._total_amount, false);
                    break;
                case _g.g._transControlTypeEnum.ขาย_เงินมัดจำ_คืน:
                    this._maxColumn = 3;
                    this._table_name = _g.d.ic_trans._table;
                    this._addNumberBox(0, 1, 1, 0, _g.d.ic_trans._vat_rate, 1, 2, true, __formatNumber);
                    this._addNumberBox(0, 2, 1, 0, _g.d.ic_trans._total_value, 1, 2, true, __formatNumber);
                    this._addNumberBox(1, 1, 1, 0, _g.d.ic_trans._total_before_vat, 1, 2, true, __formatNumber);
                    this._addNumberBox(1, 2, 1, 0, _g.d.ic_trans._total_vat_value, 1, 2, true, __formatNumber);
                    this._addNumberBox(2, 2, 1, 0, _g.d.ic_trans._total_amount, 1, 2, true, __formatNumber);
                    //
                    this._enabedControl(_g.d.ic_trans._vat_rate, (MyLib._myGlobal._useNoVat) ? true : false);
                    this._enabedControl(_g.d.ic_trans._total_before_vat, false);
                    this._enabedControl(_g.d.ic_trans._total_vat_value, false);
                    this._enabedControl(_g.d.ic_trans._total_amount, false);
                    break;
                default:
                    this._maxColumn = 3;
                    this._addNumberBox(0, 1, 1, 0, _g.d.ic_trans._total_value, 1, 2, true, __formatNumber);
                    this._addNumberBox(0, 2, 1, 0, _g.d.ic_trans._total_amount, 1, 2, true, __formatNumber);
                    //
                    this._enabedControl(_g.d.ic_trans._total_value, false);
                    this._enabedControl(_g.d.ic_trans._total_amount, false);
                    break;
            }
            this.Dock = DockStyle.Bottom;
            this.AutoSize = true;
            //

            this.Invalidate();
            this.ResumeLayout();
        }

        public void _calcVat()
        {
            decimal __totalValue = this._getDataNumber(_g.d.ic_trans._total_value);
            decimal __vatRate = this._getDataNumber(_g.d.ic_trans._vat_rate);
            switch (this._vatType(this))
            {
                case _g.g._vatTypeEnum.ภาษีแยกนอก:
                    {
                        this._setDataNumber(_g.d.ic_trans._total_before_vat, __totalValue);
                        decimal __beforeVatAmount = this._getDataNumber(_g.d.ic_trans._total_before_vat);
                        decimal __vatAmount = __beforeVatAmount * (__vatRate / 100.0M);
                        this._setDataNumber(_g.d.ic_trans._total_vat_value, __vatAmount);
                        this._setDataNumber(_g.d.ic_trans._total_amount, __beforeVatAmount + __vatAmount);
                    }
                    break;
                case _g.g._vatTypeEnum.ภาษีรวมใน:
                    {
                        decimal __beforeVatAmount = (__totalValue * 100.0M) / (100.0M + __vatRate);
                        this._setDataNumber(_g.d.ic_trans._total_before_vat, __beforeVatAmount);
                        decimal __vatAmount = __totalValue - __beforeVatAmount;
                        this._setDataNumber(_g.d.ic_trans._total_vat_value, __vatAmount);
                        this._setDataNumber(_g.d.ic_trans._total_amount, __totalValue);
                    }
                    break;
                case _g.g._vatTypeEnum.ยกเว้นภาษี:
                    {
                        decimal __beforeVatAmount = __totalValue;
                        this._setDataNumber(_g.d.ic_trans._total_before_vat, __beforeVatAmount);
                        decimal __vatAmount = __totalValue - __beforeVatAmount;
                        this._setDataNumber(_g.d.ic_trans._total_vat_value, __vatAmount);
                        this._setDataNumber(_g.d.ic_trans._total_amount, __totalValue);
                    }
                    break;
            }
            if (this._afterCalc != null)
            {
                this._afterCalc();
            }
        }
    }
}
