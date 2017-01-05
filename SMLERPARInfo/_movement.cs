using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLERPARAPInfo
{
    public partial class _movement : UserControl
    {
        private _apArConditionEnum _mode;
        private _conditionScreen _conditionForm;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mode">0=ลูกหนี้,1=เจ้าหนี้</param>
        public _movement(_apArConditionEnum mode)
        {
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this.toolStrip1.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }
            //
            this._mode = mode;
            this._conditionForm = new _conditionScreen(mode);
            this.Controls.Add(this._conditionForm);
            //
            string __formatNumberCreditDay = _g.g._getFormatNumberStr(0,0);
            string __formatNumberAmount = _g.g._getFormatNumberStr(3);
            this._resultGrid._table_name = _g.d.ap_ar_resource._table;
            this._resultGrid._addColumn(_g.d.ap_ar_resource._doc_date, 1, 8, 10);
            this._resultGrid._addColumn(_g.d.ap_ar_resource._doc_no, 1, 8, 15);
            this._resultGrid._addColumn(_g.d.ap_ar_resource._vat_no, 1, 8, 15);
            this._resultGrid._addColumn(_g.d.ap_ar_resource._ref_no, 1, 8, 15);
            this._resultGrid._addColumn(_g.d.ap_ar_resource._credit_day, 3, 10, 8, false, false, false, false, __formatNumberCreditDay);
            this._resultGrid._addColumn(_g.d.ap_ar_resource._doc_type, 1, 10, 8);
            //
            this._resultGrid._addColumn(_g.d.ap_ar_resource._debit_amount, 3, 10, 8, false, false, false, false, __formatNumberAmount);
            this._resultGrid._addColumn(_g.d.ap_ar_resource._credit_amount, 3, 10, 8, false, false, false, false, __formatNumberAmount);
            this._resultGrid._addColumn(_g.d.ap_ar_resource._ar_balance, 3, 10, 8, false, false, false, false, __formatNumberAmount);
            //
            this._resultGrid._setColumnBackground(_g.d.ap_ar_resource._debit_amount, Color.AliceBlue);
            this._resultGrid._setColumnBackground(_g.d.ap_ar_resource._credit_amount, Color.AliceBlue);
            //
            this._resultGrid._total_show = true;
            this._resultGrid._calcPersentWidthToScatter();
            //
            this._processButton.Click += new EventHandler(_processButton_Click);
            this._closeButton.Click += new EventHandler(_closeButton_Click);
            //
        }

        void _closeButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        void _processButton_Click(object sender, EventArgs e)
        {
            try
            {
                _process __process = new _process();
                string __custCode = this._conditionForm._getDataStr((this._mode == _apArConditionEnum.ลูกหนี้_เคลื่อนไหว) ? _g.d.ap_ar_resource._ar_code_begin : _g.d.ap_ar_resource._ap_code_begin);
                DateTime __dateBegin = MyLib._myGlobal._convertDate(this._conditionForm._getDataStr(_g.d.ap_ar_resource._date_begin));
                DateTime __dateEnd = MyLib._myGlobal._convertDate(this._conditionForm._getDataStr(_g.d.ap_ar_resource._date_end));
                this._resultGrid._loadFromDataTable(__process._movement(this._mode,__custCode, null, __dateBegin, __dateEnd, false));
            }
            catch (Exception __ex)
            {
                MessageBox.Show(__ex.Message.ToString());
            }
        }
    }
}
