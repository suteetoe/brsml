using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLERPARAPReport.condition
{
    public partial class _condition_screen_detail : MyLib._myScreen
    {
        private SMLERPARAPInfo._apArConditionEnum _controlTypeTemp;


        public SMLERPARAPInfo._apArConditionEnum TransControlType
        {
            set
            {
                this._controlTypeTemp = value;
                this._build();
                switch (this._controlTypeTemp)
                {
                    case SMLERPARAPInfo._apArConditionEnum.ลูกหนี้_อายุลูกหนี้:
                        this._setDataStr(_g.d.resource_report_column._from_period_1, "1");
                        this._setDataStr(_g.d.resource_report_column._from_period_2, "8");
                        this._setDataStr(_g.d.resource_report_column._from_period_3, "15");
                        this._setDataStr(_g.d.resource_report_column._from_period_4, "22");
                        this._setDataStr(_g.d.resource_report_column._to_period_1, "7");
                        this._setDataStr(_g.d.resource_report_column._to_period_2, "14");
                        this._setDataStr(_g.d.resource_report_column._to_period_3, "21");
                        this._setDataStr(_g.d.resource_report_column._to_period_4, "28");
                        this._setDataStr(_g.d.resource_report_column._to_period_5, "29");
                        break;
                }
                this.Invalidate();
            }
            get
            {
                return this._controlTypeTemp;
            }
        }

        public _condition_screen_detail()
        {
            InitializeComponent();
        }

        void _build()
        {
            this.SuspendLayout();
            this._reset();
            this._clear();
            switch (this._controlTypeTemp)
            {
                case SMLERPARAPInfo._apArConditionEnum.ลูกหนี้_อายุลูกหนี้:
                    this._table_name = _g.d.resource_report_column._table;
                    this._maxColumn = 2;
                    this._addCheckBox(0, 0, _g.d.resource_report_column._show_debt_balance, false, true);
                    this._addCheckBox(0, 1, _g.d.resource_report_column._show_chq, false, true);
                    MyLib._myGroupBox _status_groupbox = this._addGroupBox(1, 0, 1, 2, 2, _g.d.resource_report_column._order_by, true);
                    this._addRadioButtonOnGroupBox(0, 0, _status_groupbox, _g.d.resource_report_column._order_by_doc_date, 0, true);
                    this._addRadioButtonOnGroupBox(0, 1, _status_groupbox, _g.d.resource_report_column._order_by_due_date, 1, false);
                    this._addTextBox(3, 0, 1, 0, _g.d.resource_report_column._from_period_1, 1, 3, 0, true, false);
                    this._addTextBox(3, 1, 1, 0, _g.d.resource_report_column._to_period_1, 1, 3, 0, true, false);
                    this._addTextBox(4, 0, 1, 0, _g.d.resource_report_column._from_period_2, 1, 3, 0, true, false);
                    this._addTextBox(4, 1, 1, 0, _g.d.resource_report_column._to_period_2, 1, 3, 0, true, false);
                    this._addTextBox(5, 0, 1, 0, _g.d.resource_report_column._from_period_3, 1, 3, 0, true, false);
                    this._addTextBox(5, 1, 1, 0, _g.d.resource_report_column._to_period_3, 1, 3, 0, true, false);
                    this._addTextBox(6, 0, 1, 0, _g.d.resource_report_column._from_period_4, 1, 3, 0, true, false);
                    this._addTextBox(6, 1, 1, 0, _g.d.resource_report_column._to_period_4, 1, 3, 0, true, false);
                    this._addTextBox(7, 1, 1, 0, _g.d.resource_report_column._to_period_5, 1, 3, 0, true, false);
                    break;
            }
        }
    }
}
