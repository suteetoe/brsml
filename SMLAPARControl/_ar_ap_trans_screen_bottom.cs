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
    public class _ar_ap_trans_screen_bottom : MyLib._myScreen
    {
        private _g.g._transControlTypeEnum _controlTypeTemp;
        MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
        MyLib._searchDataFull _search_data_full = new MyLib._searchDataFull();

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

        public _ar_ap_trans_screen_bottom()
        {
            //this._build();
        }

        int _buildCount = 0;
        void _build()
        {
            if (this._controlTypeTemp == _g.g._transControlTypeEnum.ว่าง)
            {
                return;
            }
            if (this._buildCount++ > 0)
            {
                MessageBox.Show("Screen Bottom Duplicate");
            }
            int __row = 0;
            this.SuspendLayout();
            this._reset();
            string __formatNumber = MyLib._myGlobal._getFormatNumber(_g.g._getFormatNumberStr(3));
            this._table_name = _g.d.ap_ar_trans._table;
            switch (this._controlTypeTemp)
            {
                case _g.g._transControlTypeEnum.เจ้าหนี้_ใบรับวางบิล:
                    this._maxColumn = 3;
                    if (MyLib._myGlobal._OEMVersion.Equals("ims") || MyLib._myGlobal._OEMVersion.Equals("imex"))
                    {
                        this._addNumberBox(0, 0, 1, 0, _g.d.ap_ar_trans._total_pay_tax, 1, 2, true, __formatNumber);
                    }

                    this._addNumberBox(0, 2, 1, 0, _g.d.ap_ar_trans._total_net_value, 1, 2, true, __formatNumber);
                    break;
                case _g.g._transControlTypeEnum.เจ้าหนี้_ใบรับวางบิล_ยกเลิก:
                    break;
                case _g.g._transControlTypeEnum.เจ้าหนี้_จ่ายชำระหนี้:
                    this._maxColumn = 3;
                    this._addNumberBox(__row++, 2, 1, 0, _g.d.ap_ar_trans._total_net_value, 1, 2, true, __formatNumber);
                    break;
                case _g.g._transControlTypeEnum.เจ้าหนี้_จ่ายชำระหนี้_ยกเลิก:
                    break;
                case _g.g._transControlTypeEnum.ลูกหนี้_ใบวางบิล:
                    this._maxColumn = 3;
                    this._addNumberBox(0, 2, 1, 0, _g.d.ap_ar_trans._total_net_value, 1, 2, true, __formatNumber);
                    break;
                case _g.g._transControlTypeEnum.ลูกหนี้_ใบวางบิล_ยกเลิก:
                    break;
                case _g.g._transControlTypeEnum.ลูกหนี้_รับชำระหนี้:
                    this._maxColumn = 3;
                    this._addNumberBox(0, 2, 1, 0, _g.d.ap_ar_trans._total_net_value, 1, 2, true, __formatNumber);
                    break;
                case _g.g._transControlTypeEnum.ลูกหนี้_รับชำระหนี้_ยกเลิก:
                    break;
                case _g.g._transControlTypeEnum.IMEX_Bill_Collector:
                    this._maxColumn = 3;
                    this._addNumberBox(0, 2, 1, 0, _g.d.ap_ar_trans._total_net_value, 1, 2, true, __formatNumber);
                    break;
            }
            this._enabedControl(_g.d.ap_ar_trans._total_net_value, false);
            //
            this.Invalidate();
            this.ResumeLayout();
        }
    }
}
