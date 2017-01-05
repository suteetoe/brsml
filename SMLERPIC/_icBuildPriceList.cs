using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace SMLERPIC
{
    public partial class _icBuildPriceList : UserControl
    {
        public _icBuildPriceList()
        {
            InitializeComponent();
            this._folder.Text = "C:\\";
        }

        private void _processButton_Click(object sender, EventArgs e)
        {
            try
            {
                ArrayList __icBuild = new ArrayList();

                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                // string __where = "AN-3451-10BB73/039-1L";

                DataTable __itemList = __myFrameWork._queryStream(MyLib._myGlobal._databaseName, "select " + _g.d.ic_inventory._code + "," + _g.d.ic_inventory._name_1 + " from " + _g.d.ic_inventory._table
                    + " where " + _g.d.ic_inventory._item_type + "<>5 order by " + _g.d.ic_inventory._code, "Load Product").Tables[0];

                DataTable __item = __myFrameWork._queryStream(MyLib._myGlobal._databaseName, "select " + _g.d.ic_inventory._code + "," + _g.d.ic_inventory._name_1 + " from " + _g.d.ic_inventory._table
                    + " where " + _g.d.ic_inventory._item_type + "=5 order by " + _g.d.ic_inventory._code, "Load Product").Tables[0];

                DataTable __itemPrice = __myFrameWork._queryStream(MyLib._myGlobal._databaseName, "select " + _g.d.ic_inventory_price._ic_code + "," + _g.d.ic_inventory_price._unit_code + "," + _g.d.ic_inventory_price._sale_price2 + " from " + _g.d.ic_inventory_price._table + " order by " + _g.d.ic_inventory_price._ic_code, "Load Price").Tables[0];

                DataTable __formula = __myFrameWork._queryStream(MyLib._myGlobal._databaseName, "select " + _g.d.ic_inventory_set_detail._ic_code + "," + _g.d.ic_inventory_set_detail._ic_set_code + ","
                    + _g.d.ic_inventory_set_detail._line_number + "," + _g.d.ic_inventory_set_detail._qty + "," + _g.d.ic_inventory_set_detail._price + "," + _g.d.ic_inventory_set_detail._unit_code
                    + " from " + _g.d.ic_inventory_set_detail._table + " order by " + _g.d.ic_inventory_set_detail._ic_set_code + "," + _g.d.ic_inventory_set_detail._line_number, "Load Formula").Tables[0];

                for (int __itemLoop = 0; __itemLoop < __item.Rows.Count; __itemLoop++)
                {
                    string __itemSetCode = __item.Rows[__itemLoop][_g.d.ic_inventory._code].ToString();
                    string __itemSetName = __item.Rows[__itemLoop][_g.d.ic_inventory._name_1].ToString();
                    if (__formula.Rows.Count != 0 && __itemList.Rows.Count != 0)
                    {
                        DataRow[] __findFormula = __formula.Select(_g.d.ic_inventory_set_detail._ic_set_code + "=\'" + __itemSetCode + "\'");
                        if (__findFormula.Length > 1)
                        {
                            decimal __itemBasePrice = 0M;
                            decimal __formulaQty = 0M;
                            decimal __formulaTotalPrice = 0M;
                            // ดึงสี base
                            string __itemBaseCode = __findFormula[0][_g.d.ic_inventory_set_detail._ic_code].ToString();
                            string __itemBaseName = "";
                            DataRow[] __itemBaseNameRow = __itemList.Select(_g.d.ic_inventory._code + "=\'" + __itemBaseCode + "\'");
                            if (__itemBaseNameRow.Length > 0)
                            {
                                __itemBaseName = __itemBaseNameRow[0][_g.d.ic_inventory._name_1].ToString();
                            }
                            // หาราคาสี base
                            DataRow[] __basePrice = __itemPrice.Select(_g.d.ic_inventory_price._ic_code + "=\'" + __itemBaseCode + "\'");
                            if (__basePrice.Length > 0)
                            {
                                __itemBasePrice = MyLib._myGlobal._decimalPhase(__basePrice[0][_g.d.ic_inventory_price._sale_price2].ToString());
                            }
                            // คำนวณหาจำนวนแม่สี+คำนวณหาราคา สีผสม
                            for (int __row = 2; __row < __findFormula.Length; __row++)
                            {
                                // ปัดขั้น
                                string __formulaItemCode = __findFormula[__row][_g.d.ic_inventory_set_detail._ic_code].ToString();
                                string __unitCode = __findFormula[__row][_g.d.ic_inventory_set_detail._unit_code].ToString();
                                Decimal __getQtyFormula = Math.Ceiling(MyLib._myGlobal._decimalPhase(__findFormula[__row][_g.d.ic_inventory_set_detail._qty].ToString()));
                                Decimal __getPrice = 0;
                                DataRow[] __getPriceRow = __itemPrice.Select(_g.d.ic_inventory_price._ic_code + "=\'" + __formulaItemCode + "\' and " + _g.d.ic_inventory_price._unit_code + "=\'" + __unitCode + "\'");
                                if (__getPriceRow.Length > 0)
                                {
                                    __getPrice = MyLib._myGlobal._decimalPhase(__getPriceRow[0][_g.d.ic_inventory_price._sale_price2].ToString());
                                }
                                __formulaQty += __getQtyFormula;
                                __formulaTotalPrice += __getQtyFormula * __getPrice;
                            }
                            Decimal __itemBasePricePerUnit = MyLib._myGlobal._ceiling((__formulaQty == 0) ? 0 : __formulaTotalPrice / __formulaQty, _g.g._companyProfile._item_price_decimal);
                            __formulaTotalPrice = __formulaQty * __itemBasePricePerUnit;
                            _icBuildPriceListStruct __data = new _icBuildPriceListStruct();
                            __data._formulaCode = __itemSetCode;
                            __data._formulaName = __itemSetName;
                            __data._baseCode = __itemBaseCode;
                            __data._baseName = __itemBaseName;
                            __data._basePrice = __itemBasePrice;
                            __data._formulaQty = __formulaQty;
                            __data._formulaTotalPrice = __formulaTotalPrice;
                            __icBuild.Add(__data);
                        }
                    }
                    else
                    {
                        // แสดง Error
                    }
                }
                // Save
                int __fileNumber = 0;
                int __count = 0;
                StringBuilder __fileNameList = new StringBuilder();
                TextWriter __file = null;
                for (int __row = 0; __row < __icBuild.Count; __row++)
                {
                    if (__file == null)
                    {
                        __fileNumber++;
                        String __fileName = this._folder.Text + "\\data" + __fileNumber.ToString() + ".csv";
                        if (__fileNameList.Length > 0)
                        {
                            __fileNameList.Append(",");
                        }
                        __fileNameList.Append(__fileName);
                        __file = new StreamWriter(__fileName, false, Encoding.GetEncoding("windows-874"));
                        __file.WriteLine("Color Code,Colode Name,Base Code,Base Name,Base Price,Color Qty,Color Price,Color Total Price,Total");
                    }
                    _icBuildPriceListStruct __data = (_icBuildPriceListStruct)__icBuild[__row];
                    StringBuilder __result = new StringBuilder();
                    __result.Append(__data._formulaCode).Append(',');
                    __result.Append(__data._formulaName).Append(',');
                    __result.Append(__data._baseCode).Append(',');
                    __result.Append(__data._baseName).Append(',');
                    __result.Append(__data._basePrice).Append(',');
                    __result.Append(__data._formulaQty).Append(',');
                    __result.Append((__data._formulaQty == 0) ? 0 : __data._formulaTotalPrice / __data._formulaQty).Append(',');
                    __result.Append(__data._formulaTotalPrice).Append(',');
                    __result.Append(__data._basePrice + __data._formulaTotalPrice);
                    __file.WriteLine(__result.ToString());
                    if (++__count > 40000)
                    {
                        __count = 0;
                        __file.Close();
                        __file = null;
                    }
                }
                if (__file != null)
                {
                    __file.Close();
                }
                MessageBox.Show("Success and open file : " + __fileNameList.ToString());
            }
            catch (Exception __ex)
            {
                MessageBox.Show(__ex.Message.ToString() + " : " + __ex.StackTrace.ToString());
            }
        }

        private void _closeButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }

    public struct _icBuildPriceListStruct
    {
        public String _formulaCode;
        public String _formulaName;
        public String _baseCode;
        public String _baseName;
        public Decimal _basePrice;
        public Decimal _formulaQty;
        public Decimal _formulaTotalPrice;
    }
}
