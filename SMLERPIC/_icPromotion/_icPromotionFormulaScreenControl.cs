using System;
using System.Collections.Generic;
using System.Text;

namespace SMLERPIC._icPromotion
{
    public class _icPromotionFormulaScreenControl : MyLib._myScreen
    {
        private string[] _promotionCase = { _g.d.ic_promotion_formula._case_0, _g.d.ic_promotion_formula._case_1, _g.d.ic_promotion_formula._case_2, _g.d.ic_promotion_formula._case_3, _g.d.ic_promotion_formula._case_4, _g.d.ic_promotion_formula._case_5 };   // ประเภท Promotion

        public _icPromotionFormulaScreenControl()
        {
            string __formatNumber = MyLib._myGlobal._getFormatNumber("m00");
            this._table_name = _g.d.ic_promotion_formula._table;
            this._maxColumn = 2;
            this._addTextBox(0, 0, 1, 0, _g.d.ic_promotion_formula._code, 1, 1, 0, true, false, false);
            this._addTextBox(1, 0, 1, 0, _g.d.ic_promotion_formula._name_1, 2, 1, 0, true, false, false);
            this._addNumberBox(2, 0, 1, 0, _g.d.ic_promotion_formula._order_number, 1, 0, true);
            this._addCheckBox(2, 1, _g.d.ic_promotion_formula._status, false, true, true, true, _g.d.ic_promotion_formula._status, true);
            //this._addCheckBox(3, 0, _g.d.ic_promotion_formula._auto_change_unitcode, false, true, true, true, _g.d.ic_promotion_formula._auto_change_unitcode);
            this._addComboBox(3, 0, _g.d.ic_promotion_formula._case_number, true, this._promotionCase, true);
            this._addComboBox(3, 1, _g.d.ic_promotion_formula._member_condition, true, new string[] { _g.d.ic_promotion_formula._all_member_condition, _g.d.ic_promotion_formula._for_member_only, _g.d.ic_promotion_formula._for_non_member_only }, true);
            this._addComboBox(4, 0, _g.d.ic_promotion_formula._lock_promotion, true, new string[] { _g.d.ic_promotion_formula._lock_promotion_1, _g.d.ic_promotion_formula._lock_promotion_2 }, true);

            this._addCheckBox(5, 0, _g.d.ic_promotion_formula._use_date_range, false, true);
            this._addCheckBox(5, 1, _g.d.ic_promotion_formula._item_normal_price, false, true);

            this._addDateBox(6, 0, 1, 1, _g.d.ic_promotion_formula._from_date, 1, true);
            this._addDateBox(6, 1, 1, 1, _g.d.ic_promotion_formula._to_date, 1, true);
            this._addTextBox(7, 0, 1, 0, _g.d.ic_promotion_formula._lock_day, 1, 1, 1, true, false, true, false);

            this._addTextBox(8, 0, 3, 0, _g.d.ic_promotion_formula._lock_code_list, 2, 1, 0, true, false, true);

            this._addCheckBox(11, 0, _g.d.ic_promotion_formula._is_no_discount, false, true);

            this._textBoxSearch += _icPromotionFormulaScreenControl__textBoxSearch;

            MyLib._myTextBox textbox = (MyLib._myTextBox)this._getControl(_g.d.ic_promotion_formula._lock_day);
            textbox.textBox.Enabled = false;
        }

        void _icPromotionFormulaScreenControl__textBoxSearch(object sender)
        {
            MyLib._myTextBox _textbox = (MyLib._myTextBox)sender;
            if (_textbox._name.Equals(_g.d.ic_promotion_formula._lock_day))
            {

                _icBarcodeDiscountSelectDayForm __daySelectForm = new _icBarcodeDiscountSelectDayForm();

                string __getOldDahy = this._getDataStr(_g.d.ic_promotion_formula._lock_day);

                if (__getOldDahy.Length > 0)
                {
                    __daySelectForm._sunCheckbox.Checked =
                        __daySelectForm._monCheckbox.Checked =
                        __daySelectForm._tueCheckbox.Checked =
                        __daySelectForm._webCheckbox.Checked =
                        __daySelectForm._thuCheckbox.Checked =
                        __daySelectForm._friCheckbox.Checked =
                        __daySelectForm._satCheckbox.Checked = false;


                    string[] __daySplit = __getOldDahy.Split(',');
                    foreach (string day in __daySplit)
                    {
                        switch (day)
                        {
                            case "อาทิตย์":
                            case "0":
                                __daySelectForm._sunCheckbox.Checked = true; break;
                            case "จันทร์":
                            case "1":
                                __daySelectForm._monCheckbox.Checked = true; break;
                            case "อังคาร":
                            case "2":
                                __daySelectForm._tueCheckbox.Checked = true; break;
                            case "พุธ":
                            case "3":
                                __daySelectForm._webCheckbox.Checked = true; break;
                            case "พฤหัสบดี":
                            case "4":
                                __daySelectForm._thuCheckbox.Checked = true; break;
                            case "ศุกร์":
                            case "5":
                                __daySelectForm._friCheckbox.Checked = true; break;
                            case "เสาร์":
                            case "6":
                                __daySelectForm._satCheckbox.Checked = true; break;

                        }
                    }
                }
                __daySelectForm.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                __daySelectForm.ShowDialog();

                if (__daySelectForm.DialogResult == System.Windows.Forms.DialogResult.OK)
                {
                    if (__daySelectForm._allDayCheckbox.Checked == true)
                    {
                        //this._grid._cellUpdate(e._row, _g.d.ic_inventory_barcode_price._lock_day, "", true);
                        this._setDataStr(_g.d.ic_promotion_formula._lock_day, "");

                    }
                    else
                    {
                        // บางวัน
                        StringBuilder __selectDayData = new StringBuilder();

                        if (__daySelectForm._sunCheckbox.Checked == true)
                        {
                            if (__selectDayData.Length > 0)
                                __selectDayData.Append(",");
                            __selectDayData.Append("อาทิตย์");
                        }

                        if (__daySelectForm._monCheckbox.Checked == true)
                        {
                            if (__selectDayData.Length > 0)
                                __selectDayData.Append(",");
                            __selectDayData.Append("จันทร์");
                        }

                        if (__daySelectForm._tueCheckbox.Checked == true)
                        {
                            if (__selectDayData.Length > 0)
                                __selectDayData.Append(",");
                            __selectDayData.Append("อังคาร");
                        }

                        if (__daySelectForm._webCheckbox.Checked == true)
                        {
                            if (__selectDayData.Length > 0)
                                __selectDayData.Append(",");
                            __selectDayData.Append("พุธ");
                        }

                        if (__daySelectForm._thuCheckbox.Checked == true)
                        {
                            if (__selectDayData.Length > 0)
                                __selectDayData.Append(",");
                            __selectDayData.Append("พฤหัสบดี");
                        }

                        if (__daySelectForm._friCheckbox.Checked == true)
                        {
                            if (__selectDayData.Length > 0)
                                __selectDayData.Append(",");
                            __selectDayData.Append("ศุกร์");
                        }

                        if (__daySelectForm._satCheckbox.Checked == true)
                        {
                            if (__selectDayData.Length > 0)
                                __selectDayData.Append(",");
                            __selectDayData.Append("เสาร์");
                        }

                        //this._grid._cellUpdate(e._row, _g.d.ic_inventory_barcode_price._lock_day, __selectDayData.ToString(), true);
                        this._setDataStr(_g.d.ic_promotion_formula._lock_day, __selectDayData.ToString());

                    }
                }
            }
        }
    }
}
