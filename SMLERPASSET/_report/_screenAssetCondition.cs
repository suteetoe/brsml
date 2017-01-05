using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace SMLERPASSET._report
{
    public class _screenAssetCondition : MyLib._myScreen
    {
        MyLib._searchDataFull _searchAsset = new MyLib._searchDataFull();
        string _labelName = "";
        TextBox _searchTextBox;
        MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
        // Type
        _selectReportType _screenType = _selectReportType._reportAsset;

        public _screenAssetCondition()
        {
            this.AutoSize = true;
            _createScreen();
            // Event
            this._textBoxChanged += new MyLib.TextBoxChangedHandler(_screenAssetCondition__textBoxChanged);
            this._textBoxSearch += new MyLib.TextBoxSearchHandler(_screenAssetCondition__textBoxSearch);
            // Start Asset
            _searchAsset._name = _g.g._search_screen_as;
            _searchAsset._dataList._loadViewFormat(_searchAsset._name, MyLib._myGlobal._userSearchScreenGroup, false);
            _searchAsset.WindowState = FormWindowState.Maximized;

            _searchAsset._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
            _searchAsset._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_searchAsset__searchEnterKeyPress);
        }

        void _screenAssetCondition__textBoxChanged(object sender, string name)
        {
            if (name.CompareTo("as_resource.from_asset") == 0 || name.CompareTo("as_resource.to_asset") == 0)
            {
                _searchTextBox = (TextBox)sender;
                string _code = _searchTextBox.Text;
                _searchAssetCode(_code, name);
            }
        }

        void _createScreen()
        {
            if (_screenType == _selectReportType._reportAsset)
            {
                this._maxColumn = 2;
                this._addTextBox(0, 0, 1, 0, "as_resource.from_asset", 1, 25, 1, true, false, true, false);
                this._addTextBox(0, 1, 1, 1, "from", 2, 100, 0, false, false, false, false);
                this._addTextBox(1, 0, 1, 0, "as_resource.to_asset", 1, 25, 1, true, false, true, false);
                this._addTextBox(1, 1, 1, 0, "to", 2, 100, 0, false, false, false, false);
                //***************
                MyLib._myGroupBox __conGroupBox = this._addGroupBox(2, 0, 5, 1, 2, "as_resource.order_by", true);
                this._addRadioButtonOnGroupBox(0, 0, __conGroupBox, "as_resource.by_section", 0, true);
                this._addRadioButtonOnGroupBox(1, 0, __conGroupBox, "as_resource.by_type", 1, false);
                this._addRadioButtonOnGroupBox(2, 0, __conGroupBox, "as_resource.by_location", 2, false);
                this._addRadioButtonOnGroupBox(3, 0, __conGroupBox, "as_resource.by_buy_date", 3, false);
                this._addRadioButtonOnGroupBox(4, 0, __conGroupBox, "as_resource.by_start_calc_date", 4, false);
            }
            else if (_screenType == _selectReportType._reportDepreciate)
            {
                this._maxColumn = 2;
                this._addTextBox(0, 0, 1, 0, "as_resource.from_asset", 1, 25, 1, true, false, true, false);
                this._addTextBox(0, 1, 1, 1, "from", 2, 100, 0, false, false, false, false);
                this._addTextBox(1, 0, 1, 0, "as_resource.to_asset", 1, 25, 1, true, false, true, false);
                this._addTextBox(1, 1, 1, 0, "to", 2, 100, 0, false, false, false, false);
                this._addDateBox(2, 0, 1, 0, "as_resource.date_begin", 1, true, true);
                this._addDateBox(2, 1, 1, 0, "as_resource.date_end", 1, true, true);
                //***************
                MyLib._myGroupBox __condGroupBox = this._addGroupBox(3, 0, 3, 1, 2, "as_resource.order_by", true);
                this._addRadioButtonOnGroupBox(0, 0, __condGroupBox, "as_resource.by_section", 0, true);
                this._addRadioButtonOnGroupBox(1, 0, __condGroupBox, "as_resource.by_type", 1, false);
                this._addRadioButtonOnGroupBox(2, 0, __condGroupBox, "as_resource.by_location", 2, false);
                // ภ.ง.ด.50
                this._addCheckBox(7, 0, "as_resource.income_tax_50", false, true);
                this._setCheckBox("as_resource.income_tax_50", "0");
                // วัน เดือน ปี
                this._addComboBox(8, 0, "as_resource.view_by", true, new string[] { "as_resource.by_day", "as_resource.by_month", "as_resource.by_year" }, false);
                // วิธีการคำนวณ
                this._addComboBox(8, 1, "as_resource.process_mode", true, new string[] { "as_resource.by_revenue", "as_resource.by_account", "as_resource.by_total_date" }, false);
            }
            else if (_screenType == _selectReportType._reportMaintain)
            {
                this._maxColumn = 2;
                this._addTextBox(0, 0, 1, 0, "as_resource.from_asset", 1, 25, 1, true, false, true, false);
                this._addTextBox(0, 1, 1, 1, "from", 2, 100, 0, false, false, false, false);
                this._addTextBox(1, 0, 1, 0, "as_resource.to_asset", 1, 25, 1, true, false, true, false);
                this._addTextBox(1, 1, 1, 0, "to", 2, 100, 0, false, false, false, false);
                this._addDateBox(2, 0, 1, 0, "as_resource.date_begin", 1, true, true);
                this._addDateBox(2, 1, 1, 0, "as_resource.date_end", 1, true, true);
            }
            this.Invalidate();
        }

        void _searchAsset__searchEnterKeyPress(MyLib._myGrid sender, int row)
        {
            _searchAsset.Close();
            SendKeys.Send("{TAB}");
            this._setDataStr(_labelName, sender._cellGet(sender._selectRow, _g.d.as_asset._table + "." + _g.d.as_asset._code).ToString());
            _searchAssetCode(sender._cellGet(sender._selectRow, _g.d.as_asset._table + "." + _g.d.as_asset._code).ToString(), _labelName);
            // _searchAssetCode(sender._cellGet(sender._selectRow, _g.d.as_asset._table + "." + _g.d.as_asset._code).ToString(), _labelName);
        }

        void _gridData__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            MyLib._myDataList getParent1 = (MyLib._myDataList)((MyLib._myGrid)sender).Parent;
            MyLib._searchDataFull getParent2 = (MyLib._searchDataFull)getParent1.Parent;
            string name = getParent2._name;
            if (name.CompareTo(_g.g._search_screen_as) == 0)
            {
                _searchAsset.Close();
                SendKeys.Send("{TAB}");
                //  _searchAssetCode(e._text, _labelName);
                this._setDataStr(_labelName, e._text);
                _searchAssetCode(e._text, _labelName);
            }
        }

        void _screenAssetCondition__textBoxSearch(object sender)
        {
            string label_name = ((MyLib._myTextBox)sender)._labelName;
            MyLib._myTextBox getControl = (MyLib._myTextBox)sender;
            _searchTextBox = getControl.textBox;
            _labelName = getControl._name;
            _searchAsset.Text = label_name;
            _searchAsset.ShowDialog();
        }

        void _searchAssetCode(string code, string fieldName)
        {
            MyLib._myFrameWork myFrameWork = new MyLib._myFrameWork();
            string query = "select " + _g.d.as_asset._name_1 + " from " + _g.d.as_asset._table + " where " + _g.d.as_asset._code + "=\'" + code + "\'";
            try
            {
                DataSet dataResult = myFrameWork._query(MyLib._myGlobal._databaseName, query);

                if (dataResult.Tables[0].Rows.Count == 0)
                {
                    ((MyLib._myTextBox)_searchTextBox.Parent).textBox.Text = "";
                    ((MyLib._myTextBox)_searchTextBox.Parent)._textFirst = "";
                    ((MyLib._myTextBox)_searchTextBox.Parent)._textSecond = "";
                    ((MyLib._myTextBox)_searchTextBox.Parent)._textLast = "";
                    _searchTextBox.Focus();
                    if (fieldName.CompareTo("as_resource.from_asset") == 0)
                    {
                        //this._setDataStr("as_resource.from_asset", code);
                        this._setDataStr("from", "");
                    }
                    else
                        if (fieldName.CompareTo("as_resource.to_asset") == 0)
                        {
                            // this._setDataStr("as_resource.to_asset", code);
                            this._setDataStr("to", "");
                        }

                    //
                    // MessageBox.Show(message + _screenGeneral._getLabelName(fieldName), MyLib._myGlobal._warningMessage, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    MessageBox.Show(MyLib._myGlobal._resource("ไม่พบ"), MyLib._myGlobal._resource("เตือน"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                else
                {
                    string getData = dataResult.Tables[0].Rows[0][0].ToString();
                    if (fieldName.CompareTo("as_resource.from_asset") == 0)
                    {
                        //this._setDataStr("as_resource.from_asset", code);
                        this._setDataStr("from", getData);
                    }
                    else
                        if (fieldName.CompareTo("as_resource.to_asset") == 0)
                        {
                            // this._setDataStr("as_resource.to_asset", code);
                            this._setDataStr("to", getData);
                        }
                }
            }
            catch
            {
            }
        }

        public _selectReportType ScreenType
        {
            get
            {
                return _screenType;
            }
            set
            {
                _screenType = value;
                this.SuspendLayout();
                this.Controls.Clear();
                _createScreen();
                this.ResumeLayout(false);
                this.PerformLayout();
            }
        }
    }

    public enum _selectReportType
    {
        _reportAsset,
        _reportDepreciate,
        _reportMaintain
    }
}
