using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLActivesync
{
    public partial class _changeMasterCode : UserControl
    {
        private _changeMasterType _type = _changeMasterType.ว่าง;
        public MyLib._searchDataFull _searchItem = new MyLib._searchDataFull();
        MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();

        public _changeMasterCode(_changeMasterType type)
        {
            InitializeComponent();

            this._type = type;

            this._grid._table_name = _g.d.ar_customer._table;
            this._grid._addColumn(_g.d.ar_customer._code_old, 1, 30, 30, true, false, true, true);
            this._grid._addColumn(_g.d.ar_customer._code, 1, 30, 30, true, false);
            this._grid._addColumn(_g.d.ar_customer._name_1, 1, 40, 40, false, false);
            this._grid._calcPersentWidthToScatter();
            this._grid._clickSearchButton += new MyLib.SearchEventHandler(_grid__clickSearchButton);
            this._grid._alterCellUpdate += new MyLib.AfterCellUpdateEventHandler(_grid__alterCellUpdate);

        }

        void _grid__clickSearchButton(object sender, MyLib.GridCellEventArgs e)
        {
            switch (this._type)
            {
                case _changeMasterType.ลูกหนี้:

                    break;
            }
        }

        void _grid__alterCellUpdate(object sender, int row, int column)
        {
            string __fieldCode = "";
            string __fieldName = "";
            string __tableName = "";

            string __fieldOldCode = "";

            switch (this._type)
            {
                case _changeMasterType.ลูกหนี้ :
                    __fieldCode = _g.d.ar_customer._code;
                    __fieldName = _g.d.ar_customer._name_1;
                    __tableName = _g.d.ar_customer._table;
                    __fieldOldCode = _g.d.ar_customer._code_old;
                    break;
                case _changeMasterType.สินค้า :
                        __fieldCode = _g.d.ic_inventory._code;
                        __fieldName = _g.d.ic_inventory._name_1;
                        __tableName = _g.d.ic_inventory._table;
                        __fieldOldCode = _g.d.ic_inventory._code_old;
                    break;
                case _changeMasterType.เจ้าหนี้ :
                    __fieldCode = _g.d.ap_supplier._code;
                    __fieldName = _g.d.ap_supplier._name_1;
                    __tableName = _g.d.ap_supplier._table;
                    __fieldOldCode = _g.d.ap_supplier._code_old;
                    break;

            }

            int __getItemCodeOldColumn = this._grid._findColumnByName(__fieldOldCode);
            int __getItemCodeColumn = this._grid._findColumnByName(__fieldCode);
            if (column == __getItemCodeOldColumn)
            {
                string __itemCode = this._grid._cellGet(row, __fieldOldCode).ToString();
                string __query = "select " + __fieldName + " from " + __tableName + " where " + __fieldCode + "=\'" + MyLib._myUtil._convertTextToXml(__itemCode) + "\'";
                string __itemName = "";
                DataTable __getData = this._myFrameWork._queryShort(__query).Tables[0];
                if (__getData.Rows.Count > 0)
                {
                    __itemName = __getData.Rows[0][__fieldName].ToString();
                }
                this._grid._cellUpdate(row, __fieldName, __itemName, false);
            }
            if (column == __getItemCodeColumn)
            {
                string __itemCode = this._grid._cellGet(row, __fieldCode).ToString();
                string __query = "select " + __fieldName + " from " + __tableName + " where " + __fieldCode + "=\'" + MyLib._myUtil._convertTextToXml(__itemCode) + "\'";
                DataTable __getData = this._myFrameWork._queryShort(__query).Tables[0];
                if (__getData.Rows.Count > 0)
                {
                    MessageBox.Show(MyLib._myGlobal._resource("รหัสใหม่ซ้ำ"));
                    this._grid._cellUpdate(row, __fieldCode, "", false);
                }
            }
        }

        private void _clipBoardButton_Click(object sender, EventArgs e)
        {
            string __fieldCode = _g.d.ar_customer._code;
            string __fieldOldCode = _g.d.ar_customer._code_old;

            try
            {
                string __str = Clipboard.GetText();
                string[] __rowStr = __str.Replace("\r", "").Split('\n');
                for (int __row = 0; __row < __rowStr.Length; __row++)
                {
                    string[] __field = __rowStr[__row].ToString().Split('\t');
                    if (__field.Length == 2)
                    {
                        string __itemOldCode = __field[0].ToString();
                        string __itemNewCode = __field[1].ToString();
                        int __addr = this._grid._addRow();
                        this._grid._cellUpdate(__addr, __fieldOldCode, __itemOldCode, true);
                        this._grid._cellUpdate(__addr, __fieldCode, __itemNewCode, true);
                    }
                }
            }
            catch (Exception __ex)
            {
                MessageBox.Show(__ex.Message.ToString());
            }
        }

        private void _clearButton_Click(object sender, EventArgs e)
        {
            this._grid._clear();
        }

        private void _closeButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        string _command(string tableName, string fieldName, int row)
        {
            return _command(tableName, fieldName, row, "");
        }

        string _command(string tableName, string fieldName, int row, string extraWhere)
        {
            string __itemCodeOld = this._grid._cellGet(row, _g.d.ic_inventory._code_old).ToString().ToUpper();
            string __itemCodeNew = this._grid._cellGet(row, _g.d.ic_inventory._code).ToString().Trim().ToUpper();

            StringBuilder __result = new StringBuilder();
            if (__itemCodeOld.Equals(__itemCodeNew) == false)
            {
                //string __deleteFormat = "delete from {0} where {1}=\'" + __itemCodeNew + "\'";
                string __updateFormat = "update {0} set {1}=\'" + __itemCodeNew + "\' where {1}=\'" + __itemCodeOld + "\'" + ((extraWhere.Length > 0) ? " and " + extraWhere : "");
                //__result.Append(string.Format(MyLib._myUtil._convertTextToXmlForQuery(__deleteFormat), tableName, fieldName));
                __result.Append(string.Format(MyLib._myUtil._convertTextToXmlForQuery(__updateFormat), tableName, fieldName));
            }
            return __result.ToString();
        }

        private void _processButton_Click(object sender, EventArgs e)
        {
            string __fieldCode = _g.d.ar_customer._code;
            string __fieldName = _g.d.ar_customer._name_1;

            try
            {
                StringBuilder __query = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");
                for (int __row = 0; __row < this._grid._rowData.Count; __row++)
                {
                    string __itemName = this._grid._cellGet(__row, __fieldName).ToString().Trim().ToUpper();
                    string __itemCode = this._grid._cellGet(__row, __fieldCode).ToString().Trim().ToUpper();
                    string __remark = "Skip";
                    if (__itemName.Trim().Length > 0 && __itemCode.Length > 0)
                    {
                        switch (this._type)
                        {
                            case _changeMasterType.ลูกหนี้:
                                {
                                    // master 

                                    __query.Append(this._command(_g.d.ar_customer._table, _g.d.ar_customer._code, __row));
                                    __query.Append(this._command(_g.d.ar_customer_detail._table, _g.d.ar_customer_detail._ar_code, __row));
                                    __query.Append(this._command(_g.d.ar_dealer._table, _g.d.ar_dealer._ar_code, __row));
                                    __query.Append(this._command(_g.d.ar_contactor._table, _g.d.ar_contactor._ar_code, __row));
                                    __query.Append(this._command(_g.d.ar_item_by_customer._table, _g.d.ar_item_by_customer._ar_code, __row));

                                    // for branch
                                    string __itemCodeOld = this._grid._cellGet(__row, _g.d.ic_inventory._code_old).ToString().Trim().ToUpper();
                                    string __itemCodeNew = this._grid._cellGet(__row, _g.d.ic_inventory._code).ToString().Trim().ToUpper();
                                    
                                    __query.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from sync_change_code where change_mode = \'AR\' and old_code = \'" + __itemCodeOld + "\' "));
                                    __query.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into sync_change_code (change_mode, old_code, new_code, branch_code) select \'AR\', \'" + __itemCodeOld + "\', \'" + __itemCodeNew + "\', branch_code from sync_branch_list "));

                                }
                                break;
                            case _changeMasterType.เจ้าหนี้ :
                                {

                                    __query.Append(this._command(_g.d.ap_supplier._table, _g.d.ap_supplier._code, __row));
                                    __query.Append(this._command(_g.d.ap_supplier_detail._table, _g.d.ap_supplier_detail._ap_code, __row));
                                    __query.Append(this._command(_g.d.ap_contactor._table, _g.d.ap_contactor._ap_code, __row));
                                    __query.Append(this._command(_g.d.ap_item_by_supplier._table, _g.d.ap_item_by_supplier._ap_code, __row));


                                    // for branch
                                    string __itemCodeOld = this._grid._cellGet(__row, _g.d.ic_inventory._code_old).ToString().Trim().ToUpper();
                                    string __itemCodeNew = this._grid._cellGet(__row, _g.d.ic_inventory._code).ToString().Trim().ToUpper();

                                    __query.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from sync_change_code where change_mode = \'AP\' and old_code = \'" + __itemCodeOld + "\' "));
                                    __query.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into sync_change_code (change_mode, old_code, new_code, branch_code) select \'AP\', \'" + __itemCodeOld + "\', \'" + __itemCodeNew + "\', branch_code from sync_branch_list "));

                                }
                                break;
                            case _changeMasterType.สินค้า :
                                {

                                    __query.Append(this._command(_g.d.ic_inventory._table, _g.d.ic_inventory._code, __row));
                                    __query.Append(this._command(_g.d.ic_extra_detail._table, _g.d.ic_extra_detail._ic_code, __row));
                                    __query.Append(this._command(_g.d.ic_color_use._table, _g.d.ic_color_use._ic_code, __row));
                                    __query.Append(this._command(_g.d.ic_inventory_append._table, _g.d.ic_inventory_append._ic_append_code, __row));
                                    __query.Append(this._command(_g.d.ic_inventory_append_detail._table, _g.d.ic_inventory_append_detail._ic_code, __row));
                                    __query.Append(this._command(_g.d.ic_inventory_barcode._table, _g.d.ic_inventory_barcode._ic_code, __row));
                                    __query.Append(this._command(_g.d.ic_inventory_detail._table, _g.d.ic_inventory_detail._ic_code, __row));
                                    __query.Append(this._command(_g.d.images._table, _g.d.images._image_id, __row));
                                    __query.Append(this._command(_g.d.ic_inventory_price._table, _g.d.ic_inventory_price._ic_code, __row));
                                    __query.Append(this._command(_g.d.ic_inventory_purchase_price._table, _g.d.ic_inventory_purchase_price._ic_code, __row));
                                    __query.Append(this._command(_g.d.ic_inventory_replace._table, _g.d.ic_inventory_replace._ic_replace_code, __row));
                                    __query.Append(this._command(_g.d.ic_inventory_replace_detail._table, _g.d.ic_inventory_replace_detail._ic_code, __row));
                                    __query.Append(this._command(_g.d.ic_inventory_set_detail._table, _g.d.ic_inventory_set_detail._ic_code, __row));
                                    __query.Append(this._command(_g.d.ic_name_billing._table, _g.d.ic_name_billing._ic_code, __row));
                                    __query.Append(this._command(_g.d.ic_name_merket._table, _g.d.ic_name_merket._ic_code, __row));
                                    __query.Append(this._command(_g.d.ic_name_pos._table, _g.d.ic_name_pos._ic_code, __row));
                                    __query.Append(this._command(_g.d.ic_name_short._table, _g.d.ic_name_short._ic_code, __row));
                                    __query.Append(this._command(_g.d.ic_opposite_unit._table, _g.d.ic_opposite_unit._ic_code, __row));
                                    __query.Append(this._command(_g.d.ic_pattern_use._table, _g.d.ic_pattern_use._ic_code, __row));
                                    __query.Append(this._command(_g.d.ic_promotion_detail._table, _g.d.ic_promotion_detail._ic_code, __row));
                                    __query.Append(this._command(_g.d.ic_purchase_permium_condition._table, _g.d.ic_purchase_permium_condition._ic_code, __row));
                                    __query.Append(this._command(_g.d.ic_purchase_permium_list._table, _g.d.ic_purchase_permium_list._ic_code, __row));
                                    __query.Append(this._command(_g.d.ic_purchase_price_detail._table, _g.d.ic_purchase_price_detail._ic_code, __row));
                                    __query.Append(this._command(_g.d.ic_relation_detail._table, _g.d.ic_relation_detail._ic_code_1, __row));
                                    __query.Append(this._command(_g.d.ic_serial._table, _g.d.ic_serial._ic_code, __row));
                                    __query.Append(this._command(_g.d.ic_size_use._table, _g.d.ic_size_use._ic_code, __row));
                                    __query.Append(this._command(_g.d.ic_stk_build._table, _g.d.ic_stk_build._ic_code, __row));
                                    __query.Append(this._command(_g.d.ic_stk_build_detail._table, _g.d.ic_stk_build_detail._ic_code, __row));

                                    __query.Append(this._command(_g.d.ic_unit_use._table, _g.d.ic_unit_use._ic_code, __row));
                                    __query.Append(this._command(_g.d.ic_wh_shelf._table, _g.d.ic_wh_shelf._ic_code, __row));
                                    __query.Append(this._command(_g.d.ic_inventory_level._table, _g.d.ic_inventory_level._ic_code, __row));
                                    __query.Append(this._command(_g.d.ic_inventory_price_formula._table, _g.d.ic_inventory_price_formula._ic_code, __row));

                                    // for branch
                                    string __itemCodeOld = this._grid._cellGet(__row, _g.d.ic_inventory._code_old).ToString().Trim().ToUpper();
                                    string __itemCodeNew = this._grid._cellGet(__row, _g.d.ic_inventory._code).ToString().Trim().ToUpper();

                                    __query.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from sync_change_code where change_mode = \'IC\' and old_code = \'" + __itemCodeOld + "\' "));
                                    __query.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into sync_change_code (change_mode, old_code, new_code, branch_code) select \'IC\', \'" + __itemCodeOld + "\', \'" + __itemCodeNew + "\', branch_code from sync_branch_list "));

                                }
                                break;

                            /*case g._transTypeEnum.เจ้าหนี้:

                                // master 

                                __query.Append(this._command(_g.d.ap_supplier._table, _g.d.ap_supplier._code, __row));
                                __query.Append(this._command(_g.d.ap_supplier_detail._table, _g.d.ap_supplier_detail._ap_code, __row));
                                __query.Append(this._command(_g.d.ap_contactor._table, _g.d.ap_contactor._ap_code, __row));
                                __query.Append(this._command(_g.d.ap_item_by_supplier._table, _g.d.ap_item_by_supplier._ap_code, __row));

                                break;*/
                        }

                        /*
                        __query.Append(this._command(_g.d.ic_inventory._table, _g.d.ic_inventory._code, __row));
                        __query.Append(this._command(_g.d.ic_extra_detail._table, _g.d.ic_extra_detail._ic_code, __row));
                        __query.Append(this._command(_g.d.ic_color_use._table, _g.d.ic_color_use._ic_code, __row));
                        __query.Append(this._command(_g.d.ic_inventory_append._table, _g.d.ic_inventory_append._ic_append_code, __row));
                        __query.Append(this._command(_g.d.ic_inventory_append_detail._table, _g.d.ic_inventory_append_detail._ic_code, __row));
                        __query.Append(this._command(_g.d.ic_inventory_barcode._table, _g.d.ic_inventory_barcode._ic_code, __row));
                        __query.Append(this._command(_g.d.ic_inventory_detail._table, _g.d.ic_inventory_detail._ic_code, __row));
                        __query.Append(this._command(_g.d.images._table, _g.d.images._image_id, __row));
                        __query.Append(this._command(_g.d.ic_inventory_price._table, _g.d.ic_inventory_price._ic_code, __row));
                        __query.Append(this._command(_g.d.ic_inventory_purchase_price._table, _g.d.ic_inventory_purchase_price._ic_code, __row));
                        __query.Append(this._command(_g.d.ic_inventory_replace._table, _g.d.ic_inventory_replace._ic_replace_code, __row));
                        __query.Append(this._command(_g.d.ic_inventory_replace_detail._table, _g.d.ic_inventory_replace_detail._ic_code, __row));
                        __query.Append(this._command(_g.d.ic_inventory_set_detail._table, _g.d.ic_inventory_set_detail._ic_code, __row));
                        __query.Append(this._command(_g.d.ic_name_billing._table, _g.d.ic_name_billing._ic_code, __row));
                        __query.Append(this._command(_g.d.ic_name_merket._table, _g.d.ic_name_merket._ic_code, __row));
                        __query.Append(this._command(_g.d.ic_name_pos._table, _g.d.ic_name_pos._ic_code, __row));
                        __query.Append(this._command(_g.d.ic_name_short._table, _g.d.ic_name_short._ic_code, __row));
                        __query.Append(this._command(_g.d.ic_opposite_unit._table, _g.d.ic_opposite_unit._ic_code, __row));
                        __query.Append(this._command(_g.d.ic_pattern_use._table, _g.d.ic_pattern_use._ic_code, __row));
                        __query.Append(this._command(_g.d.ic_promotion_detail._table, _g.d.ic_promotion_detail._ic_code, __row));
                        __query.Append(this._command(_g.d.ic_purchase_permium_condition._table, _g.d.ic_purchase_permium_condition._ic_code, __row));
                        __query.Append(this._command(_g.d.ic_purchase_permium_list._table, _g.d.ic_purchase_permium_list._ic_code, __row));
                        __query.Append(this._command(_g.d.ic_purchase_price_detail._table, _g.d.ic_purchase_price_detail._ic_code, __row));
                        __query.Append(this._command(_g.d.ic_relation_detail._table, _g.d.ic_relation_detail._ic_code_1, __row));
                        __query.Append(this._command(_g.d.ic_serial._table, _g.d.ic_serial._ic_code, __row));
                        __query.Append(this._command(_g.d.ic_size_use._table, _g.d.ic_size_use._ic_code, __row));
                        __query.Append(this._command(_g.d.ic_stk_build._table, _g.d.ic_stk_build._ic_code, __row));
                        __query.Append(this._command(_g.d.ic_stk_build_detail._table, _g.d.ic_stk_build_detail._ic_code, __row));
                        __query.Append(this._command(_g.d.ic_trans_detail._table, _g.d.ic_trans_detail._item_code, __row));
                        __query.Append(this._command(_g.d.ic_trans_detail_lot._table, _g.d.ic_trans_detail_lot._item_code, __row));
                        __query.Append(this._command(_g.d.ic_trans_serial_number._table, _g.d.ic_trans_serial_number._ic_code, __row));
                        __query.Append(this._command(_g.d.ic_unit_use._table, _g.d.ic_unit_use._ic_code, __row));
                        __query.Append(this._command(_g.d.ic_wh_shelf._table, _g.d.ic_wh_shelf._ic_code, __row));
                        __query.Append(this._command(_g.d.ic_inventory_level._table, _g.d.ic_inventory_level._ic_code, __row));
                        __query.Append(this._command(_g.d.ic_inventory_price_formula._table, _g.d.ic_inventory_price_formula._ic_code, __row));*/

                        __remark = "Process";
                    }
                    this._grid._cellUpdate(__row, _g.d.ic_inventory._remark, __remark, false);
                }
                __query.Append("</node>");
                string __result = this._myFrameWork._queryList(MyLib._myGlobal._databaseName, __query.ToString());
                if (__result.Length == 0)
                {
                    MessageBox.Show("Success");
                    this.Dispose();
                }
                else
                {
                    MessageBox.Show("Fail : " + __result);
                }
            }
            catch (Exception __ex)
            {
                MessageBox.Show(__ex.Message.ToString());
            }
        }
    }

    public enum _changeMasterType
    {
        ว่าง,
        สินค้า,
        ลูกหนี้,
        เจ้าหนี้
    }
}
