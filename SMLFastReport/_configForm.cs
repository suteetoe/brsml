using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMLFastReport
{
    public partial class _configForm : Form
    {
        public _configForm()
        {
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this.toolStrip1.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }
            // test
            /*
            this._queryDesign1._tableNameTextBox.Text = "ic_inventory";
            this._queryDesign1._queryTextBox.Text = "select code,name_1,balance_qty,unit_standard||'/'||(select name_1 from ic_unit where ic_unit.code=unit_standard) as  unit_standard from ic_inventory where (select count(*) from ic_trans_detail where ic_trans_detail.item_code=code and ic_trans_detail.trans_flag=44) > 0";
            this._queryDesign2._tableNameTextBox.Text = "ic_trans_detail";
            this._queryDesign2._queryTextBox.Text = "select item_code,doc_date,doc_no,qty,price,discount,sum_amount_exclude_vat from ic_trans_detail where trans_flag=44 order by item_code,doc_date,doc_no";
            this._queryDesign2._relationTextBox.Text = "item_code='#code#'";
            */
            this._conditionGrid.CellValueChanged += new DataGridViewCellEventHandler(_conditionGrid_CellValueChanged);
            this._conditionScreen._beforeBuild += _conditionScreen__beforeBuild;
        }

        private void _conditionScreen__beforeBuild()
        {
            this._reportNameTextBox.Focus();
        }

        void _conditionGrid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 4)
            {
                this._conditionGrid.EndEdit();
                string __text = MyLib._myResource._findResource(this._conditionGrid.Rows[e.RowIndex].Cells[4].Value.ToString(), false)._str;
                this._conditionGrid.Rows[e.RowIndex].Cells[5].Value = __text;
                this._conditionGrid.EndEdit();
            }
        }

        public _xmlClass _xml()
        {
            _xmlClass __xml = new _xmlClass();
            __xml._header = this._reportNameTextBox.Text;
            __xml._refNo = this._reportRefTextbox.Text;
            __xml._splitRow = this._splitRowCheckbox.Checked;
            __xml._showLineLastPage = this._lineFooterLastPageCheckbox.Checked;

            __xml._query.Add(this._queryDesign1._xml());
            __xml._query.Add(this._queryDesign2._xml());
            __xml._query.Add(this._queryDesign3._xml());
            __xml._query.Add(this._queryDesign4._xml());
            __xml._query.Add(this._queryDesign5._xml());
            __xml._query.Add(this._queryDesign6._xml());
            __xml._query.Add(this._queryDesign7._xml());
            this._conditionGrid.EndEdit();
            for (int __row = 0; __row < this._conditionGrid.Rows.Count; __row++)
            {
                _conditionDetailClass __data = new _conditionDetailClass();
                __data._row = (int)MyLib._myGlobal._decimalPhase(this._gridValue(this._conditionGrid.Rows[__row].Cells["_row"].Value));
                __data._column = (int)MyLib._myGlobal._decimalPhase(this._gridValue(this._conditionGrid.Rows[__row].Cells["_column"].Value));
                __data._span = (int)MyLib._myGlobal._decimalPhase(this._gridValue(this._conditionGrid.Rows[__row].Cells["_span"].Value));
                __data._code = this._gridValue(this._conditionGrid.Rows[__row].Cells["_resource_code"].Value);
                __data._text = this._gridValue(this._conditionGrid.Rows[__row].Cells["_text"].Value);
                __data._type = this._gridValue(this._conditionGrid.Rows[__row].Cells["_type"].Value);
                __data._command = this._gridValue(this._conditionGrid.Rows[__row].Cells["_command"].Value);
                __data._name = this._gridValue(this._conditionGrid.Rows[__row].Cells["_name"].Value);
                __data._defaultValue = this._gridValue(this._conditionGrid.Rows[__row].Cells["_default"].Value);
                __data._columnName = this._gridValue(this._conditionGrid.Rows[__row].Cells["_column_name"].Value);

                if (__data._name.Trim().Length > 0)
                {
                    __xml._conditionList.Add(__data);
                }
            }
            return __xml;
        }

        private string _gridValue(object value)
        {
            string __result = "";
            if (value != null)
            {
                __result = value.ToString().Trim();
            }
            return __result;
        }

        public void _loadFromXML(_xmlClass __xml)
        {
            this._reportNameTextBox.Text = __xml._header;
            this._reportRefTextbox.Text = __xml._refNo;

            this._splitRowCheckbox.Checked = __xml._splitRow;
            this._lineFooterLastPageCheckbox.Checked = __xml._showLineLastPage;
            if (__xml._query.Count > 0) this._queryDesign1._setXML(__xml._query[0]);
            if (__xml._query.Count > 1) this._queryDesign2._setXML(__xml._query[1]);
            if (__xml._query.Count > 2) this._queryDesign3._setXML(__xml._query[2]);
            if (__xml._query.Count > 3) this._queryDesign4._setXML(__xml._query[3]);
            if (__xml._query.Count > 4) this._queryDesign5._setXML(__xml._query[4]);
            if (__xml._query.Count > 5) this._queryDesign6._setXML(__xml._query[5]);
            if (__xml._query.Count > 6) this._queryDesign7._setXML(__xml._query[6]);
            this._conditionGrid.Rows.Clear();
            foreach (_conditionDetailClass __data in __xml._conditionList)
            {
                this._conditionGrid.Rows.Add(__data._name.ToString(), __data._row.ToString(), __data._column.ToString(), __data._span, __data._code.ToString(), __data._text.ToString(), __data._type, __data._command.ToString(), __data._defaultValue, __data._columnName);
            }
        }

        private void _previewButton_Click(object sender, EventArgs e)
        {
            try
            {
                this._conditionScreen._build(this._xml());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void _upButton_Click(object sender, EventArgs e)
        {
            if (this._conditionGrid.SelectedCells.Count > 0)
            {
                int __rowSelected = this._conditionGrid.SelectedCells[0].RowIndex;
                int __cellSelected = this._conditionGrid.SelectedCells[0].ColumnIndex;
                if (__rowSelected > 0)
                {
                    DataGridViewRow __dr1 = (DataGridViewRow)this._conditionGrid.Rows[__rowSelected].Clone();
                    int __i = 0;
                    foreach (DataGridViewCell __cell in this._conditionGrid.Rows[__rowSelected].Cells)
                    {
                        __dr1.Cells[__i].Value = __cell.Value;
                        __i++;
                    }
                    this._conditionGrid.Rows.RemoveAt(__rowSelected);
                    this._conditionGrid.Rows.Insert(__rowSelected - 1, __dr1);

                    this._conditionGrid.ClearSelection();

                    this._conditionGrid.Rows[__rowSelected - 1].Selected = true;
                }
            }
        }

        private void _downButton_Click(object sender, EventArgs e)
        {
            if (this._conditionGrid.SelectedCells.Count > 0)
            {
                int __rowSelected = this._conditionGrid.SelectedCells[0].RowIndex;
                int __cellSelected = this._conditionGrid.SelectedCells[0].ColumnIndex;
                if (__rowSelected < this._conditionGrid.Rows.Count - 1)
                {
                    DataGridViewRow __dr1 = (DataGridViewRow)this._conditionGrid.Rows[__rowSelected].Clone();
                    int __i = 0;
                    foreach (DataGridViewCell __cell in this._conditionGrid.Rows[__rowSelected].Cells)
                    {
                        __dr1.Cells[__i].Value = __cell.Value;
                        __i++;
                    }
                    this._conditionGrid.Rows.RemoveAt(__rowSelected);
                    this._conditionGrid.Rows.Insert(__rowSelected + 1, __dr1);
                    this._conditionGrid.ClearSelection();
                    this._conditionGrid.Rows[__rowSelected + 1].Selected = true;
                }
            }
        }

    }
}
