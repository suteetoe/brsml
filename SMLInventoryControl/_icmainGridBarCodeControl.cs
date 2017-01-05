using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLInventoryControl
{
    public class _icmainGridBarCodeControl : MyLib._myGrid
    {
        private MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
        public SMLERPControl._ic._icTransItemGridSelectUnitForm _icTransItemGridSelectUnit = new SMLERPControl._ic._icTransItemGridSelectUnitForm();
        public event GetUnitCodeEventHandler GetUnitCode;
        public event GetUnitTypeEventHandler GetUnitType;
        public event GetItemCodeEventHandler GetItemCode;
        public event GetItemDescEventHandler GetItemDesc;

        public _icmainGridBarCodeControl()
        {
            this._build();
        }

        void _build()
        {
            if (MyLib._myGlobal._isDesignMode) return;
            string __formatNumberPrice = _g.g._getFormatNumberStr(2);

            this._clear();
            this._columnListTop.Clear();

            if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLBIllFree)
            {
                this.AllowDrop = true;
                this._rowNumberWork = true;
                this._getResource = true;
                this._table_name = _g.d.ic_inventory_barcode._table;
                this._addColumn(_g.d.ic_inventory_barcode._barcode, 1, 20, 25, true, false, true, false);
                this._addColumn(_g.d.ic_inventory_barcode._unit_code, 1, 5, 15, false, false, true, true);
                this._columnExtraWord(_g.d.ic_inventory_barcode._unit_code, "(F4)");
                this._message = "F6=Create Barcode,F5 Create New Code(4 digit)";

            }
            else
            {
                this._columnTopActive = true;
                this.AllowDrop = true;
                this._rowNumberWork = true;
                this._getResource = true;
                this._table_name = _g.d.ic_inventory_barcode._table;
                this._addColumn(_g.d.ic_inventory_barcode._barcode, 1, 20, 25, true, false, true, false);
                this._addColumn(_g.d.ic_inventory_barcode._unit_code, 1, 5, 15, false, false, true, true);

                this._addColumn(_g.d.ic_inventory_barcode._price, 3, 0, 15, true, false, true, false, __formatNumberPrice);
                this._addColumn(_g.d.ic_inventory_barcode._price_member, 3, 0, 15, true, false, true, false, __formatNumberPrice);
                this._addColumn(_g.d.ic_inventory_barcode._price_2, 3, 0, 15, true, false, true, false, __formatNumberPrice);
                this._addColumn(_g.d.ic_inventory_barcode._price_member_2, 3, 0, 15, true, false, true, false, __formatNumberPrice);
                this._addColumn(_g.d.ic_inventory_barcode._price_3, 3, 0, 15, true, false, true, false, __formatNumberPrice);
                this._addColumn(_g.d.ic_inventory_barcode._price_member_3, 3, 0, 15, true, false, true, false, __formatNumberPrice);
                this._addColumn(_g.d.ic_inventory_barcode._price_4, 3, 0, 15, true, false, true, false, __formatNumberPrice);
                this._addColumn(_g.d.ic_inventory_barcode._price_member_4, 3, 0, 15, true, false, true, false, __formatNumberPrice);
                this._addColumn(_g.d.ic_inventory_barcode._hidden_text, 11, 0, 5, true, false, true, false);
                this._addColumn(_g.d.ic_inventory_barcode._no_point, 11, 0, 5, true, false, true, false);

                //
                this._addColumnTop(_g.d.ic_inventory_barcode._price_group_1, this._findColumnByName(_g.d.ic_inventory_barcode._price), this._findColumnByName(_g.d.ic_inventory_barcode._price_member));
                this._addColumnTop(_g.d.ic_inventory_barcode._price_group_2, this._findColumnByName(_g.d.ic_inventory_barcode._price_2), this._findColumnByName(_g.d.ic_inventory_barcode._price_member_2));
                this._addColumnTop(_g.d.ic_inventory_barcode._price_group_3, this._findColumnByName(_g.d.ic_inventory_barcode._price_3), this._findColumnByName(_g.d.ic_inventory_barcode._price_member_3));
                this._addColumnTop(_g.d.ic_inventory_barcode._price_group_4, this._findColumnByName(_g.d.ic_inventory_barcode._price_4), this._findColumnByName(_g.d.ic_inventory_barcode._price_member_4));
                //
                this._setColumnBackground(_g.d.ic_inventory_barcode._price, Color.Honeydew);
                this._setColumnBackground(_g.d.ic_inventory_barcode._price_member, Color.LightYellow);
                this._setColumnBackground(_g.d.ic_inventory_barcode._price_2, Color.Honeydew);
                this._setColumnBackground(_g.d.ic_inventory_barcode._price_member_2, Color.LightYellow);
                this._setColumnBackground(_g.d.ic_inventory_barcode._price_3, Color.Honeydew);
                this._setColumnBackground(_g.d.ic_inventory_barcode._price_member_3, Color.LightYellow);
                this._setColumnBackground(_g.d.ic_inventory_barcode._price_4, Color.Honeydew);
                this._setColumnBackground(_g.d.ic_inventory_barcode._price_member_4, Color.LightYellow);
                //
                this._columnExtraWord(_g.d.ic_inventory_barcode._unit_code, "(F4)");
                this._message = "F6=Create Barcode,F5 Create New Code(4 digit)";
            }
            this._calcPersentWidthToScatter();
            this._icTransItemGridSelectUnit._selectUnitCode += new SMLERPControl._ic._icTransItemGridSelectUnitForm._selectUnitCodeEventHandler(_icTransItemGridSelectUnit__selectUnitCode);
            this._afterAddRow += new MyLib.AfterAddRowEventHandler(_icmainGridBarCodeControl__afterAddRow);
            this._alterCellUpdate += new MyLib.AfterCellUpdateEventHandler(_icmainGridBarCodeControl__alterCellUpdate);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F4)
            {
                this._selectUnitCode();
                return true;
            }
            if (keyData == Keys.F6)
            {
                if (this._selectRow != -1 && this._selectRow < this._rowData.Count)
                {
                    _createBarcodeForm __form = new _createBarcodeForm();
                    __form._saveButton.Click += (s1, e1) =>
                    {
                        try
                        {
                            if (__form._barCodelabel.Text.Trim().Length > 0)
                            {
                                this._cellUpdate(this._selectRow, _g.d.ic_inventory_barcode._barcode, __form._barCodelabel.Text.Trim(), false);
                                SendKeys.Send("{TAB}");
                                __form.Close();
                            }
                        }
                        catch (ExecutionEngineException __ex)
                        {
                            MessageBox.Show(__ex.Message.ToString());
                        }
                    };
                    __form.ShowDialog();
                    return true;
                }
            }
            if (keyData == Keys.F5)
            {
                if (this._selectRow != -1 && this._selectRow < this._rowData.Count)
                {
                    try
                    {
                        double __runningNumber = 1;

                        DataTable __dt = _myFrameWork._queryShort("select " + _g.d.ic_inventory_barcode._barcode + " from " + _g.d.ic_inventory_barcode._table + " where " + _g.d.ic_inventory_barcode._barcode + "<\'9z\' and length(" + _g.d.ic_inventory_barcode._barcode + ")=4 order by " + _g.d.ic_inventory_barcode._barcode + " desc limit 1").Tables[0]; // \'" + __icCode + "z\' 
                        if (__dt.Rows.Count > 0)
                        {
                            string __getItemCode = __dt.Rows[0][_g.d.ic_inventory_barcode._barcode].ToString();
                            if (__getItemCode.Length > 0)
                            {
                                //string __s1 = __getItemCode.Substring(0, __icCode.Length);
                                //if (__s1.Equals(__icCode))
                                //{
                                //    string __s2 = __getItemCode.Remove(0, __icCode.Length);
                                //    int __runningNumber = (int)MyLib._myGlobal._decimalPhase(__s2);
                                //    if (__runningNumber > 0)
                                //    {
                                //        StringBuilder __format = new StringBuilder();
                                //        for (int __loop = 0; __loop < __s2.Length; __loop++)
                                //        {
                                //            __format.Append("0");
                                //        }
                                //        __newICCode = __s1 + String.Format("{0:" + __format.ToString() + "}", ((decimal)(__runningNumber + 1)));
                                //        this._setDataStr(_g.d.ic_inventory._code, __newICCode, "", true);
                                //    }
                                //}
                                __runningNumber = double.Parse(__getItemCode) + 1;

                            }
                        }

                        //string __result = String.Format("{0:" + __newFormat.Remove(0, 1) + "#}", __runningNumber);
                        string __result = String.Format("{0:0000}", __runningNumber);
                        this._cellUpdate(this._selectRow, _g.d.ic_inventory_barcode._barcode, __result.Trim(), false);

                    }
                    catch
                    {
                    }
                    return true;
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        void _icmainGridBarCodeControl__alterCellUpdate(object sender, int row, int column)
        {
            if (column == 0)
            {
                Boolean __duplicate = false;
                string __barcode1 = this._cellGet(row, 0).ToString();
                if (__barcode1.Length > 0)
                {
                    for (int __loop = 0; __loop < this._rowData.Count; __loop++)
                    {
                        if (__loop != row)
                        {
                            string __barcode2 = this._cellGet(__loop, 0).ToString();
                            if (__barcode2.Length > 0 && __barcode2.Equals(__barcode1))
                            {
                                __duplicate = true;
                            }
                        }
                    }
                }
                if (__duplicate)
                {
                    MessageBox.Show("บาร์โค๊ดซ้ำ");
                    this._cellUpdate(row, 0, "", true);
                }
            }
        }

        void _icmainGridBarCodeControl__afterAddRow(object sender, int row)
        {
            if (this.GetUnitCode != null)
            {
                this._cellUpdate(row, _g.d.ic_inventory_barcode._unit_code, this.GetUnitCode(this), true);
            }
        }

        void _icTransItemGridSelectUnit__selectUnitCode(int mode, string unitCode)
        {
            if (this._rowData.Count > 0 && this.SelectRow < this._rowData.Count)
            {
                this._cellUpdate(this._selectRow, _g.d.ic_inventory_barcode._unit_code, unitCode, true);
            }
        }

        /// <summary>
        /// เลือกหน่วยนับ
        /// </summary>
        protected void _selectUnitCode()
        {
            int __unitType = (this.GetUnitType == null) ? 0 : this.GetUnitType(this);
            string __itemCode = (this.GetItemCode == null) ? "" : this.GetItemCode(this);
            string __itemDesc = (this.GetItemDesc == null) ? "" : this.GetItemDesc(this);
            if (__unitType == 0)
            {
                MessageBox.Show(__itemDesc + " : สินค้านี้มีหน่วยนับเดียว");
            }
            else
            {
                string __unitCode = this._cellGet(this._selectRow, _g.d.ic_trans_detail._unit_code).ToString();
                this._icTransItemGridSelectUnit._itemCode = __itemCode;
                this._icTransItemGridSelectUnit._lastCode = __unitCode;
                this._icTransItemGridSelectUnit.Text = __itemDesc;
                this._icTransItemGridSelectUnit.ShowDialog();
            }
        }
    }


    public delegate int GetUnitTypeEventHandler(object sender);
    public delegate string GetUnitCodeEventHandler(object sender);
    public delegate string GetItemCodeEventHandler(object sender);
    public delegate string GetItemDescEventHandler(object sender);
}
