using System;
using System.Collections.Generic;
using System.Text;

namespace SMLERPGL._chart
{
    public class _chartOfAccountScreen : MyLib._myScreen
    {
        public _chartOfAccountScreen()
        {
            this._maxColumn = 2;
            this.SuspendLayout();
            this._table_name = _g.d.gl_chart_of_account._table;
            this._addTextBox(0, 0, 1, 0, _g.d.gl_chart_of_account._code, 1, 25, 0, true, false, false);
            this._addComboBox(0, 1, _g.d.gl_chart_of_account._account_level, true, _g.g._accountLevel, false);
            this._addTextBox(1, 0, 1, 0, _g.d.gl_chart_of_account._name_1, 2, 100, 0, true, false, false);
            this._addTextBox(2, 0, 1, 0, _g.d.gl_chart_of_account._name_2, 2, 100, 0, true, false);
            this._addTextBox(3, 0, 1, 0, _g.d.gl_chart_of_account._account_close, 1, 25, 1, true, false);
            this._addTextBox(3, 1, 1, 0, _g.d.gl_chart_of_account._main_code, 1, 25, ((MyLib._myGlobal._OEMVersion.Equals("SINGHA")) ? 1 : 0), true, false, true, true, false);
            this._addTextBox(4, 0, 1, 0, _g.d.gl_chart_of_account._account_group, 1, 10, 1, true, false);
            this._addTextBox(4, 1, 1, 0, _g.d.gl_chart_of_account._cash_flow_group, 1, 25, 1, true, false);
            MyLib._myGroupBox __accountTypeGroupBox = this._addGroupBox(5, 0, 5, 1, 1, _g.d.gl_chart_of_account._account_type, true);
            this._addRadioButtonOnGroupBox(0, 0, __accountTypeGroupBox, _g.g._accountType[0], 0, true);
            this._addRadioButtonOnGroupBox(1, 0, __accountTypeGroupBox, _g.g._accountType[1], 1, false);
            this._addRadioButtonOnGroupBox(2, 0, __accountTypeGroupBox, _g.g._accountType[2], 2, false);
            this._addRadioButtonOnGroupBox(3, 0, __accountTypeGroupBox, _g.g._accountType[3], 3, false);
            this._addRadioButtonOnGroupBox(4, 0, __accountTypeGroupBox, _g.g._accountType[4], 4, false);
            MyLib._myGroupBox __taxGroupBox = this._addGroupBox(11, 0, 5, 1, 1, _g.d.gl_chart_of_account._tax_type, true);
            this._addRadioButtonOnGroupBox(0, 0, __taxGroupBox, _g.g._accountTaxType[0], 0, true);
            this._addRadioButtonOnGroupBox(1, 0, __taxGroupBox, _g.g._accountTaxType[1], 1, false);
            this._addRadioButtonOnGroupBox(2, 0, __taxGroupBox, _g.g._accountTaxType[2], 2, false);
            this._addRadioButtonOnGroupBox(3, 0, __taxGroupBox, _g.g._accountTaxType[3], 3, false);
            this._addRadioButtonOnGroupBox(4, 0, __taxGroupBox, _g.g._accountTaxType[4], 4, false);
            MyLib._myGroupBox __balanceSheetEffectGroupBox = this._addGroupBox(17, 0, 4, 1, 1, _g.d.gl_chart_of_account._balance_sheet_status, true);
            this._addRadioButtonOnGroupBox(0, 0, __balanceSheetEffectGroupBox, _g.g._accountBalanceSheetEffect[0], 0, true);
            this._addRadioButtonOnGroupBox(1, 0, __balanceSheetEffectGroupBox, _g.g._accountBalanceSheetEffect[1], 1, false);
            this._addRadioButtonOnGroupBox(2, 0, __balanceSheetEffectGroupBox, _g.g._accountBalanceSheetEffect[2], 2, false);
            this._addRadioButtonOnGroupBox(3, 0, __balanceSheetEffectGroupBox, _g.g._accountBalanceSheetEffect[3], 3, false);
            MyLib._myGroupBox __statusGroupBox = this._addGroupBox(5, 1, 2, 1, 1, _g.d.gl_chart_of_account._status, true);
            this._addRadioButtonOnGroupBox(0, 0, __statusGroupBox, "account_status_normal", 0, true);
            this._addRadioButtonOnGroupBox(1, 0, __statusGroupBox, "account_status_main", 1, false);
            MyLib._myGroupBox __balanceModeGroupBox = this._addGroupBox(8, 1, 2, 1, 1, _g.d.gl_chart_of_account._balance_mode, true);
            this._addRadioButtonOnGroupBox(0, 0, __balanceModeGroupBox, _g.g._accountBalanceMode[0].ToString(), 0, true);
            this._addRadioButtonOnGroupBox(1, 0, __balanceModeGroupBox, _g.g._accountBalanceMode[1].ToString(), 1, false);
            this._addCheckBox(11, 1, _g.d.gl_chart_of_account._side_status, false, true);
            this._addCheckBox(12, 1, _g.d.gl_chart_of_account._department_status, false, true);
            this._addCheckBox(13, 1, _g.d.gl_chart_of_account._allocate_status, false, true);
            this._addCheckBox(14, 1, _g.d.gl_chart_of_account._job_status, false, true);
            this._addCheckBox(15, 1, _g.d.gl_chart_of_account._project_status, false, true);
            this._addCheckBox(16, 1, _g.d.gl_chart_of_account._work_in_process_status, false, true);
            this._addCheckBox(17, 1, _g.d.gl_chart_of_account._active_status, false, true);
            //
            if ((MyLib._myGlobal._OEMVersion.Equals("SINGHA")) == false)
            {
                this._getControl(_g.d.gl_chart_of_account._main_code).Enabled = false;
            }
            this.ResumeLayout(false);
        }
    }
}
