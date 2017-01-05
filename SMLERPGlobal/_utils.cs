using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace _g
{
    public class _utils
    {
        string _itemCode;

        public void _updateInventoryMaster(string code)
        {
            this._itemCode = code;
        }

        public void _updateInventoryMasterFunction()
        {
            try
            {
                    string __code = (this._itemCode.Length == 0) ? "" : MyLib._myGlobal._addUpper(_g.d.ic_inventory._table + "." + _g.d.ic_inventory._code) + "=\'" + this._itemCode.ToUpper().Trim() + "\' and ";
                    StringBuilder __myQuery = new StringBuilder();
                    __myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                    // Update ชื่อหน่วยนับ
                    String __unitNameQuery = "coalesce((select " + _g.d.ic_unit._name_1 + " from " + _g.d.ic_unit._table + " where " + _g.d.ic_unit._table + "." + _g.d.ic_unit._code + "=" + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._unit_standard + "),'')";
                    __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.ic_inventory._table + " set " + _g.d.ic_inventory._unit_standard_name + "=" + __unitNameQuery + " where " + __code + "(" + _g.d.ic_inventory._unit_standard_name + " is null or " + _g.d.ic_inventory._unit_standard_name + "<>" + __unitNameQuery + ")"));
                    /*
                     * // update ตัวตั้ง
                    __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.ic_inventory._table + " set " + _g.d.ic_inventory._unit_standard_stand_value + "=coalesce((select " + _g.d.ic_unit_use._stand_value + " from " + _g.d.ic_unit_use._table + " where " + _g.d.ic_unit_use._table + "." + _g.d.ic_unit_use._ic_code + "=" + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + " and " + _g.d.ic_unit_use._table + "." + _g.d.ic_unit_use._code + "=" + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._unit_standard + "),1) where " + __code + "(" + _g.d.ic_inventory._unit_standard_stand_value + "<>coalesce((select " + _g.d.ic_unit_use._stand_value + " from " + _g.d.ic_unit_use._table + " where " + _g.d.ic_unit_use._table + "." + _g.d.ic_unit_use._ic_code + "=" + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + " and " + _g.d.ic_unit_use._table + "." + _g.d.ic_unit_use._code + "=" + _g.d.ic_inventory._unit_standard + "),1) or " + _g.d.ic_inventory._unit_standard_stand_value + " is null)"));
                    __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.ic_inventory._table + " set " + _g.d.ic_inventory._unit_standard_stand_value +"=1 where "+_g.d.ic_inventory._unit_standard_stand_value + " is null or "+_g.d.ic_inventory._unit_standard_stand_value + "=0"));
                    // update ตัวหาร
                    __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.ic_inventory._table + " set " + _g.d.ic_inventory._unit_standard_divide_value + "=(select " + _g.d.ic_unit_use._divide_value + " from " + _g.d.ic_unit_use._table + " where " + _g.d.ic_unit_use._table + "." + _g.d.ic_unit_use._ic_code + "=" + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + " and " + _g.d.ic_unit_use._table + "." + _g.d.ic_unit_use._code + "=" + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._unit_standard + ") where " + __code + "(" + _g.d.ic_inventory._unit_standard_divide_value + "<>(select " + _g.d.ic_unit_use._divide_value + " from " + _g.d.ic_unit_use._table + " where " + _g.d.ic_unit_use._table + "." + _g.d.ic_unit_use._ic_code + "=" + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + " and " + _g.d.ic_unit_use._table + "." + _g.d.ic_unit_use._code + "=" + _g.d.ic_inventory._unit_standard + ") or " + _g.d.ic_inventory._unit_standard_divide_value + " is null)"));
                    __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.ic_inventory._table + " set " + _g.d.ic_inventory._unit_standard_divide_value + "=1 where " + _g.d.ic_inventory._unit_standard_divide_value + " is null or " + _g.d.ic_inventory._unit_standard_divide_value + "=0"));
                     * */
                    __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("update ic_inventory set unit_standard_stand_value=1 where unit_standard_stand_value=0 or unit_standard_stand_value is null"));
                    __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("update ic_inventory set unit_standard_divide_value=1 where unit_standard_divide_value=0 or unit_standard_divide_value is null"));
                    __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("update ic_inventory set unit_standard_stand_value=1 where unit_type=0 and (unit_standard_stand_value<>1 or unit_standard_stand_value is null)"));
                    __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("update ic_inventory set unit_standard_divide_value=1 where unit_type=0 and (unit_standard_divide_value<>1 or unit_standard_divide_value is null)"));
                    __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("update ic_inventory set unit_standard_stand_value=coalesce((select stand_value from ic_unit_use where ic_unit_use.ic_code=ic_inventory.code and ic_unit_use.code=ic_inventory.unit_standard),1) where unit_type=1 and unit_standard_stand_value<>coalesce((select stand_value from ic_unit_use where ic_unit_use.ic_code=ic_inventory.code and ic_unit_use.code=ic_inventory.unit_standard),1)"));
                    __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("update ic_inventory set unit_standard_divide_value=coalesce((select divide_value from ic_unit_use where ic_unit_use.ic_code=ic_inventory.code and ic_unit_use.code=ic_inventory.unit_standard),1) where unit_type=1 and unit_standard_divide_value<>coalesce((select divide_value from ic_unit_use where ic_unit_use.ic_code=ic_inventory.code and ic_unit_use.code=ic_inventory.unit_standard),1)"));
                    // update ชื่อ barcode
                    string __ic_code = (this._itemCode.Length == 0) ? "" : MyLib._myGlobal._addUpper(_g.d.ic_inventory_barcode._table + "." + _g.d.ic_inventory_barcode._ic_code) + "=\'" + this._itemCode.ToUpper().Trim() + "\' and ";
                    __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.ic_inventory_barcode._table + " set " + _g.d.ic_inventory_barcode._description + "=" + "(select " + _g.d.ic_inventory._name_1 + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + "=" + _g.d.ic_inventory_barcode._table + "." + _g.d.ic_inventory_barcode._ic_code + ") where " + __ic_code + "(" + _g.d.ic_inventory_barcode._description + "<>" + "(select " + _g.d.ic_inventory._name_1 + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + "=" + _g.d.ic_inventory_barcode._table + "." + _g.d.ic_inventory_barcode._ic_code + ") or " + _g.d.ic_inventory_barcode._description + " is null)"));

                    // fix import data
                if (_g.g._companyProfile._activeSync == false)
                {

                    string __fix_ic_detail = "insert into ic_inventory_detail (ic_code) select UPPER(code) from ic_inventory  where not exists (select ic_code from ic_inventory_detail where ic_inventory_detail.ic_code = ic_inventory.code) ";
                    __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery(__fix_ic_detail));
                }

                    __myQuery.Append("</node>");
                    MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                    string __result = __myFrameWork._queryList(MyLib._myGlobal._databaseName, __myQuery.ToString());
                    if (__result.Length > 0)
                    {
                        MessageBox.Show(__result);
                    }
            }
            catch
            {
            }
        }

        public void _updateSupplierMasterFunction()
        {
            try
            {
                if (_g.g._companyProfile._activeSync == false)
                {
                    StringBuilder __myQuery = new StringBuilder();
                    __myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                    string __fix_ic_detail = "insert into " + _g.d.ap_supplier_detail._table + " (" + _g.d.ap_supplier_detail._ap_code + ") select UPPER(" + _g.d.ap_supplier._code + ") from " + _g.d.ap_supplier._table + "  where not exists (select  " + _g.d.ap_supplier_detail._ap_code + " from " + _g.d.ap_supplier_detail._table + " where " + _g.d.ap_supplier_detail._table + "." + _g.d.ap_supplier_detail._ap_code + " = " + _g.d.ap_supplier._table + "." + _g.d.ap_supplier._code + ") ";
                    __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery(__fix_ic_detail));

                    __myQuery.Append("</node>");
                    MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                    string __result = __myFrameWork._queryList(MyLib._myGlobal._databaseName, __myQuery.ToString());
                    if (__result.Length > 0)
                    {
                        MessageBox.Show(__result);
                    }
                }
            }
            catch
            {
            }

        }

        public void _updateCustomerMasterFunction()
        {
            try
            {
                if (_g.g._companyProfile._activeSync == false)
                {

                    StringBuilder __myQuery = new StringBuilder();
                    __myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                    string __fix_ic_detail = "insert into " + _g.d.ar_customer_detail._table + " (" + _g.d.ar_customer_detail._ar_code + ") select UPPER(" + _g.d.ar_customer._code + ") from " + _g.d.ar_customer._table + "  where not exists (select  " + _g.d.ar_customer_detail._ar_code + " from " + _g.d.ar_customer_detail._table + " where " + _g.d.ar_customer_detail._table + "." + _g.d.ar_customer_detail._ar_code + " = " + _g.d.ar_customer._table + "." + _g.d.ar_customer._code + ") ";
                    __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery(__fix_ic_detail));

                    __myQuery.Append("</node>");
                    MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                    string __result = __myFrameWork._queryList(MyLib._myGlobal._databaseName, __myQuery.ToString());
                    if (__result.Length > 0)
                    {
                        MessageBox.Show(__result);
                    }
                }
            }
            catch
            {
            }
        }
    }
}
