using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLERPIC._icPromotion
{
    public partial class _formula : UserControl
    {
        private string _oldCode = "";
        private _icPromotionFormulaScreenControl _icPromotionFormulaScreen = new _icPromotionFormulaScreenControl();
        private _icPromotion._conditionControl _icPromotionCondition = new _icPromotion._conditionControl();
        private System.Windows.Forms.ToolStrip _myToolBar;
        private MyLib.ToolStripMyButton _saveButton;
        private int _conditionCaseNumber = 0;
        private _case1Class _case1 = new _case1Class();
        private _case2Class _case2 = new _case2Class();
        private _case3Class _case3 = new _case3Class();
        private _case4Class _case4 = new _case4Class();

        public _formula()
        {
            InitializeComponent();
            //
            this._myToolBar = new System.Windows.Forms.ToolStrip();
            //
            this._saveButton = new MyLib.ToolStripMyButton();
            // 
            // _saveButton
            // 
            this._saveButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.ImageAndText;
            this._saveButton.Image = global::SMLERPIC.Properties.Resources.disk_blue;
            this._saveButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this._saveButton.Name = "_saveButton";
            this._saveButton.Padding = new System.Windows.Forms.Padding(1);
            this._saveButton.ResourceName = "บันทึกข้อมูล (F12)";
            this._saveButton.Size = new System.Drawing.Size(23, 22);
            this._saveButton.Text = "บันทึก";
            //
            this._myToolBar.Items.Add(this._saveButton);
            //
            this._myManageMain._form2.Controls.Add(this._icPromotionFormulaScreen);
            this._icPromotionFormulaScreen.Dock = DockStyle.Top;
            this._myManageMain._form2.Controls.Add(this._myToolBar);
            this._myToolBar.Dock = DockStyle.Top;
            this._icPromotionCondition.Dock = DockStyle.Fill;
            this._myManageMain._form2.Controls.Add(this._icPromotionCondition);
            this._icPromotionCondition.BringToFront();
            //
            this._myManageMain._displayMode = 0;
            this._myManageMain._dataList._lockRecord = true;
            this._myManageMain._selectDisplayMode(this._myManageMain._displayMode);
            this._myManageMain._dataList._loadViewFormat(_g.g._search_ic_promotion_formula, MyLib._myGlobal._userSearchScreenGroup, true);
            this._myManageMain._dataList._referFieldAdd(_g.d.ic_promotion_formula._code, 1);
            this._myManageMain._loadDataToScreen += new MyLib.LoadDataToScreen(_myManageDetail__loadDataToScreen);
            this._myManageMain._manageButton = this._myToolBar;
            //this._myManageDetail._manageBackgroundPanel = this._myPanel1;
            this._myManageMain._newDataClick += new MyLib.NewDataEvent(_myManageDetail__newDataClick);
            this._myManageMain._newDataFromTempClick += _myManageMain__newDataFromTempClick;
            this._myManageMain._discardData += new MyLib.DiscardDataEvent(_myManageDetail__discardData);
            this._myManageMain._clearData += new MyLib.ClearDataEvent(_myManageDetail__clearData);
            this._myManageMain._closeScreen += new MyLib.CloseScreenEvent(_myManageDetail__closeScreen);
            this._myManageMain._dataList._deleteData += new MyLib.DeleteDataEventHandler(_dataList__deleteData);
            this._myManageMain._dataList._loadViewData(0);
            this._myManageMain._calcArea();
            this._myManageMain._dataListOpen = true;
            this._myManageMain._autoSize = true;
            this._myManageMain._autoSizeHeight = 450;
            this._saveButton.Click += new EventHandler(_saveButton_Click);
            this._myManageMain.Invalidate();
            //
            this._myToolBar.EnabledChanged += new EventHandler(_myToolBar_EnabledChanged);
            this._icPromotionFormulaScreen._comboBoxSelectIndexChanged += new MyLib.ComboBoxSelectIndexChangedHandler(_icPromotionFormulaScreen__comboBoxSelectIndexChanged);
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this._myToolBar.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }
        }

        void _myManageMain__newDataFromTempClick()
        {
            this._oldCode = "";

            //this._icPromotionFormulaScreen._setDataStr(_g.d.ic_promotion_formula._code, "");
        }

        void _icPromotionFormulaScreen__comboBoxSelectIndexChanged(object sender, string name)
        {
            MyLib._myComboBox __combo = (MyLib._myComboBox)sender;
            if (__combo._name.Equals(_g.d.ic_promotion_formula._case_number))
            {
                if (this._conditionCaseNumber != __combo.SelectedIndex)
                {
                    this._conditionCaseNumber = __combo.SelectedIndex;
                    switch (__combo.SelectedIndex)
                    {
                        case 1:  // ส่วนลดตามจำนวนเต็ม (ซื้ออะไรก็ได้ 3 กล่อง 100)
                            this._icPromotionCondition._build(_promotionCaseEnum.ส่วนลดตามจำนวนเต็ม);
                            this._case1.Dock = DockStyle.Fill;
                            this._icPromotionCondition._panelWork.Controls.Clear();
                            this._icPromotionCondition._panelWork.Controls.Add(this._case1);
                            this._icPromotionCondition._gridGroup.Enabled = false;
                            this._icPromotionCondition._splitContainer2.SplitterDistance = 0;
                            break;
                        case 2:  // ส่วนลดตามจำนวนเต็ม (ซื้อ A+B ลด %)
                            this._icPromotionCondition._build(_promotionCaseEnum.ส่วนลดตามจำนวนเต็มสินค้าจัดชุด);
                            this._case2.Dock = DockStyle.Fill;
                            this._icPromotionCondition._panelWork.Controls.Clear();
                            this._icPromotionCondition._panelWork.Controls.Add(this._case2);
                            this._icPromotionCondition._gridGroup.Enabled = false;
                            this._icPromotionCondition._splitContainer2.SplitterDistance = 0;
                            break;
                        case 3:  // ส่วนลดตามจำนวนเต็ม (ซื้อ A+B ลด %) ตามกลุ่ม
                            this._icPromotionCondition._build(_promotionCaseEnum.ส่วนลดตามจำนวนเต็มสินค้าจัดชุดตามกลุ่ม);
                            this._case3.Dock = DockStyle.Fill;
                            this._icPromotionCondition._panelWork.Controls.Clear();
                            this._icPromotionCondition._panelWork.Controls.Add(this._case3);
                            this._icPromotionCondition._gridGroup.Enabled = true;
                            this._icPromotionCondition._splitContainer2.SplitterDistance = 250;
                            break;
                        case 4:  // ของแถมตามจำนวนเต็ม (ซื้อ A+B แถม B)
                            this._icPromotionCondition._build(_promotionCaseEnum.ของแถมตามจำนวนเต็มสินค้าจัดชุด);
                            this._case4.Dock = DockStyle.Fill;
                            this._icPromotionCondition._panelWork.Controls.Clear();
                            this._icPromotionCondition._panelWork.Controls.Add(this._case4);
                            this._icPromotionCondition._gridGroup.Enabled = false;
                            this._icPromotionCondition._splitContainer2.SplitterDistance = 0;
                            break;
                        case 5: // ของแแถมสินค้าตามมูลค่า ซื้อ 1000 แถมอะไร
                            this._icPromotionCondition._build(_promotionCaseEnum.ของแถมตามมูลค่าสินค้า);

                            this._case4.Dock = DockStyle.Fill;
                            this._icPromotionCondition._panelWork.Controls.Clear();
                            this._icPromotionCondition._panelWork.Controls.Add(this._case4);
                            this._icPromotionCondition._gridGroup.Enabled = true;
                            this._icPromotionCondition._splitContainer2.SplitterDistance = 250;

                            break;
                    }
                }
            }
        }

        void _promotionProcess(string source)
        {
        }

        void _dataList__deleteData(ArrayList selectRowOrder)
        {
            if (MyLib._myGlobal._checkChangeMaster())
            {

                StringBuilder __myQuery = new StringBuilder();
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();

                __myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                for (int __loop = 0; __loop < selectRowOrder.Count; __loop++)
                {
                    MyLib._deleteDataType __getData = (MyLib._deleteDataType)selectRowOrder[__loop];
                    int _getColumnCode = _myManageMain._dataList._gridData._findColumnByName(_g.d.ic_promotion_formula._table + "." + _g.d.ic_promotion_formula._code);
                    string __code = _myManageMain._dataList._gridData._cellGet(__getData.row, _getColumnCode).ToString();
                    __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.ic_promotion_formula._table + " where " + _g.d.ic_promotion_formula._code + "=\'" + __code + "\'"));
                    __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.ic_promotion_formula_condition._table + " where " + _g.d.ic_promotion_formula_condition._code + "=\'" + __code + "\'"));
                    __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.ic_promotion_formula_price_discount._table + " where " + _g.d.ic_promotion_formula_price_discount._code + "=\'" + __code + "\'"));
                    __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.ic_promotion_formula_action._table + " where " + _g.d.ic_promotion_formula_action._code + "=\'" + __code + "\'"));
                    __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.ic_promotion_formula_group_qty._table + " where " + _g.d.ic_promotion_formula_group_qty._code + "=\'" + __code + "\'"));
                }
                __myQuery.Append("</node>");

                string __result = __myFrameWork._queryList(MyLib._myGlobal._databaseName, __myQuery.ToString());
                if (__result.Length == 0)
                {
                    MyLib._myGlobal._displayWarning(0, null);
                    this._myManageMain._dataList._refreshData();
                    this._icPromotionFormulaScreen._clear();
                    this._icPromotionFormulaScreen._focusFirst();
                }
                else
                {
                    MessageBox.Show(__result, "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        void _myToolBar_EnabledChanged(object sender, EventArgs e)
        {
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F12 && _myToolBar.Enabled)
            {
                _saveData(true);
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        void _myManageDetail__closeScreen()
        {
            this.Dispose();
        }

        bool _myManageDetail__loadDataToScreen(object rowData, string whereString, bool forEdit)
        {
            this._clearScreen();
            bool __result = false;
            try
            {
                int _getColumnCode = _myManageMain._dataList._gridData._findColumnByName(_g.d.ic_promotion_formula._table + "." + _g.d.ic_promotion_formula._code);
                string __code = _myManageMain._dataList._gridData._cellGet(_myManageMain._dataList._gridData._selectRow, _getColumnCode).ToString();
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                StringBuilder __query = new StringBuilder();
                __query.Append(MyLib._myGlobal._xmlHeader + "<node>");
                __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.ic_promotion_formula._table + " where " + _g.d.ic_promotion_formula._code + "=\'" + __code + "\'"));
                __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select *,(select " + _g.d.ic_inventory._name_1 + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + "=" + _g.d.ic_promotion_formula_condition._table + "." + _g.d.ic_promotion_formula_condition._condition_from + ") as " + _g.d.ic_promotion_formula_condition._item_name + " from " + _g.d.ic_promotion_formula_condition._table + " where " + _g.d.ic_promotion_formula_condition._code + "=\'" + __code + "\' order by " + _g.d.ic_promotion_formula_condition._line_number));
                __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.ic_promotion_formula_price_discount._table + " where " + _g.d.ic_promotion_formula_price_discount._code + "=\'" + __code + "\' order by " + _g.d.ic_promotion_formula_price_discount._line_number));
                __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.ic_promotion_formula_action._table + " where " + _g.d.ic_promotion_formula_action._code + "=\'" + __code + "\' and " + _g.d.ic_promotion_formula_action._action_command + "<>'' order by " + _g.d.ic_promotion_formula_action._qty_from));
                __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.ic_promotion_formula_group_qty._table + " where " + _g.d.ic_promotion_formula_group_qty._code + "=\'" + __code + "\' and " + _g.d.ic_promotion_formula_group_qty._group_number + ">0 order by " + _g.d.ic_promotion_formula_group_qty._group_number));
                __query.Append("</node>");
                String __queryStr = __query.ToString();
                ArrayList __dataResult = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __queryStr);
                if (__dataResult.Count > 0)
                {
                    this._icPromotionFormulaScreen._loadData(((DataSet)__dataResult[0]).Tables[0]);
                    this._icPromotionCondition._gridItem._loadFromDataTable(((DataSet)__dataResult[1]).Tables[0]);
                    this._icPromotionCondition._gridPriceAndDiscount._loadFromDataTable(((DataSet)__dataResult[2]).Tables[0]);
                    this._case1._loadFromDataTable(((DataSet)__dataResult[3]).Tables[0]);
                    this._case2._loadFromDataTable(((DataSet)__dataResult[3]).Tables[0]);
                    this._case3._loadFromDataTable(((DataSet)__dataResult[3]).Tables[0]);
                    this._case4._loadFromDataTable(((DataSet)__dataResult[3]).Tables[0]);
                    this._icPromotionCondition._gridGroup._loadFromDataTable(((DataSet)__dataResult[4]).Tables[0]);

                    if (((DataSet)__dataResult[0]).Tables[0].Rows.Count > 0)
                    {
                        string __lockDay = ((DataSet)__dataResult[0]).Tables[0].Rows[0][_g.d.ic_promotion_formula._lock_day].ToString();
                        if (__lockDay.Length > 0)
                        {
                            string[] __days = __lockDay.Split(',');
                            foreach (string day in __days)
                            {

                                string dayStr = "";
                                switch (day)
                                {
                                    case "0": dayStr = "อาทิตย์"; break;
                                    case "1": dayStr = "จันทร์"; break;
                                    case "2": dayStr = "อังคาร"; break;
                                    case "3": dayStr = "พุธ"; break;
                                    case "4": dayStr = "พฤหัสบดี"; break;
                                    case "5": dayStr = "ศุกร์"; break;
                                    case "6": dayStr = "เสาร์"; break;

                                }

                                __lockDay = __lockDay.Replace(day, dayStr);
                            }

                            _icPromotionFormulaScreen._setDataStr(_g.d.ic_promotion_formula._lock_day, __lockDay.ToString());
                        }
                    }
                }
                this._oldCode = this._icPromotionFormulaScreen._getDataStr(_g.d.ic_promotion_formula._code);
                __result = true;
            }
            catch (Exception __e)
            {
                MessageBox.Show(__e.Message);
            }
            return __result;
        }

        void _myManageDetail__newDataClick()
        {
            _clearScreen();
            this._myManageMain._dataList._refreshData();
        }

        bool _myManageDetail__discardData()
        {
            return true;
        }

        void _myManageDetail__clearData()
        {
            _clearScreen();
        }

        void _clearScreen()
        {
            this._icPromotionFormulaScreen._clear();
            this._icPromotionCondition._gridItem._clear();
            this._icPromotionCondition._gridPriceAndDiscount._clear();
            this._icPromotionCondition._gridGroup._clear();
            this._oldCode = "";
        }

        void _saveButton_Click(object sender, EventArgs e)
        {
            _saveData(true);
        }

        private void _saveData(Boolean clearData)
        {
            if (MyLib._myGlobal._checkChangeMaster())
            {
                try
                {
                    string __getEmtry = this._icPromotionFormulaScreen._checkEmtryField();
                    if (__getEmtry.Length > 0)
                    {
                        MyLib._myGlobal._displayWarning(2, __getEmtry);
                    }
                    else
                    {
                        StringBuilder __myQuery = new StringBuilder();
                        __myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                        __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.ic_promotion_formula._table + " where " + _g.d.ic_promotion_formula._code + "=\'" + this._oldCode + "\'"));
                        __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.ic_promotion_formula_condition._table + " where " + _g.d.ic_promotion_formula_condition._code + "=\'" + this._oldCode + "\'"));
                        __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.ic_promotion_formula_price_discount._table + " where " + _g.d.ic_promotion_formula_price_discount._code + "=\'" + this._oldCode + "\'"));
                        __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.ic_promotion_formula_action._table + " where " + _g.d.ic_promotion_formula_action._code + "=\'" + this._oldCode + "\'"));
                        __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.ic_promotion_formula_group_qty._table + " where " + _g.d.ic_promotion_formula_group_qty._code + "=\'" + this._oldCode + "\'"));
                        //
                        string __code = this._icPromotionFormulaScreen._getDataStr(_g.d.ic_promotion_formula._code).ToString();
                        ArrayList __getData = this._icPromotionFormulaScreen._createQueryForDatabase();

                        StringBuilder __daysLock = new StringBuilder();
                        string __days = _icPromotionFormulaScreen._getDataStr(_g.d.ic_promotion_formula._lock_day);
                        __days = __days.Replace("อาทิตย์", "0").Replace("จันทร์", "1").Replace("อังคาร", "2").Replace("พุธ", "3").Replace("พฤหัสบดี", "4").Replace("ศุกร์", "5").Replace("เสาร์", "6");


                        __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.ic_promotion_formula._table + " (" + __getData[0].ToString() + "," + _g.d.ic_promotion_formula._lock_day + ") values (" + __getData[1].ToString() + ",\'" + __days + "\')"));
                        this._icPromotionCondition._gridItem._updateRowIsChangeAll(true);
                        __myQuery.Append(this._icPromotionCondition._gridItem._createQueryForInsert(_g.d.ic_promotion_formula_condition._table, _g.d.ic_promotion_formula_condition._code + ",", "\'" + __code + "\',", false, true));
                        this._icPromotionCondition._gridPriceAndDiscount._updateRowIsChangeAll(true);
                        __myQuery.Append(this._icPromotionCondition._gridPriceAndDiscount._createQueryForInsert(_g.d.ic_promotion_formula_price_discount._table, _g.d.ic_promotion_formula_price_discount._code + ",", "\'" + __code + "\',", false, true));
                        switch (this._conditionCaseNumber)
                        {
                            case 1:
                                this._case1._updateRowIsChangeAll(true);
                                __myQuery.Append(this._case1._createQueryForInsert(_g.d.ic_promotion_formula_action._table, _g.d.ic_promotion_formula_action._code + ",", "\'" + __code + "\',", false));
                                break;
                            case 2:
                                this._case2._updateRowIsChangeAll(true);
                                __myQuery.Append(this._case2._createQueryForInsert(_g.d.ic_promotion_formula_action._table, _g.d.ic_promotion_formula_action._code + ",", "\'" + __code + "\',", false));
                                break;
                            case 3:
                                this._case3._updateRowIsChangeAll(true);
                                __myQuery.Append(this._case3._createQueryForInsert(_g.d.ic_promotion_formula_action._table, _g.d.ic_promotion_formula_action._code + ",", "\'" + __code + "\',", false));
                                break;
                            case 4:
                                this._case4._updateRowIsChangeAll(true);
                                __myQuery.Append(this._case4._createQueryForInsert(_g.d.ic_promotion_formula_action._table, _g.d.ic_promotion_formula_action._code + ",", "\'" + __code + "\',", false));
                                break;
                            case 5:
                                this._case4._updateRowIsChangeAll(true);
                                __myQuery.Append(this._case4._createQueryForInsert(_g.d.ic_promotion_formula_action._table, _g.d.ic_promotion_formula_action._code + ",", "\'" + __code + "\',", false));
                                break;
                        }
                        this._icPromotionCondition._gridGroup._updateRowIsChangeAll(true);
                        __myQuery.Append(this._icPromotionCondition._gridGroup._createQueryForInsert(_g.d.ic_promotion_formula_group_qty._table, _g.d.ic_promotion_formula_group_qty._code + ",", "\'" + __code + "\',", false));
                        __myQuery.Append("</node>");

                        MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                        string __result = __myFrameWork._queryList(MyLib._myGlobal._databaseName, __myQuery.ToString());
                        if (__result.Length == 0)
                        {
                            MyLib._myGlobal._displayWarning(1, null);
                            if (this._myManageMain._mode == 1)
                            {
                                this._myManageMain._afterInsertData();
                                _clearScreen();
                                this._icPromotionFormulaScreen._focusFirst();
                            }
                            else
                            {
                                this._myManageMain._afterUpdateData();
                            }
                            if (clearData)
                            {
                                this._myManageDetail__clearData();
                            }
                        }
                        else
                        {
                            MessageBox.Show(__result, "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception __e)
                {
                    MessageBox.Show(__e.Message);
                }
            }
        }
    }

    public enum _promotionCaseEnum
    {
        ส่วนลดตามจำนวนเต็ม,
        ส่วนลดตามจำนวนเต็มสินค้าจัดชุด,
        ส่วนลดตามจำนวนเต็มสินค้าจัดชุดตามกลุ่ม,
        ของแถมตามจำนวนเต็มสินค้าจัดชุด,
        ของแถมตามมูลค่าสินค้า
    }
}
