using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace SMLERPIC
{
    public partial class _icSpecialPartRelateControl : UserControl
    {
        public _icSpecialPartRelateControl()
        {
            InitializeComponent();

            this._gridStockBalance._getResource = true;
            this._gridStockBalance._table_name = _g.d.ic_resource._table;
            this._gridStockBalance._addColumn(_g.d.ic_resource._warehouse, 1, 20, 20);
            this._gridStockBalance._addColumn(_g.d.ic_resource._qty, 1, 20, 60);
            this._gridStockBalance._calcPersentWidthToScatter();
            this._gridStockBalance._isEdit = false;

            this._partDetailNameLabel.Text = "";


        }

        public void _loadPartInfo(string itemCode)
        {
            StringBuilder __query = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");

            // inventory
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ic_inventory._name_1 + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._code + "=\'" + itemCode + "\'"));

            // stock balance
            SMLProcess._icProcess __process = new SMLProcess._icProcess();
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery(__process._queryItemBalance(itemCode, _g.d.ic_trans_detail._wh_code + " as " + _g.d.ic_resource._warehouse, _g.d.ic_resource._qty, _g.d.ic_trans_detail._wh_code, "")));

            // applicable with
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ic_specific_search._keyword + " from " + _g.d.ic_specific_search._table + " where " + _g.d.ic_specific_search._ic_code + "=\'" + itemCode + "\'"));

            // applicable path
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select ic_code, status ,(select " + _g.d.ic_inventory._name_1 + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + "=" + _g.d.ic_inventory_suggest._table + "." + _g.d.ic_inventory_suggest._ic_code + ") as " + _g.d.ic_inventory_bundle._ic_name + ",line_number from " + _g.d.ic_inventory_suggest._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_inventory_suggest._ic_suggest_code) + "=\'" + itemCode + "\' union all  " + " select ic_suggest_code as  ic_code, status ,(select " + _g.d.ic_inventory._name_1 + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + "=" + _g.d.ic_inventory_suggest._table + "." + _g.d.ic_inventory_suggest._ic_suggest_code + ") as " + _g.d.ic_inventory_bundle._ic_name + ",line_number from " + _g.d.ic_inventory_suggest._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_inventory_suggest._ic_code) + "=\'" + itemCode + "\' order by line_number "));

            __query.Append("</node>");

            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            ArrayList __result = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __query.ToString());
            if (__result.Count > 0)
            {
                DataTable __productDetail = ((DataSet)__result[0]).Tables[0];
                DataTable __tableBalance = ((DataSet)__result[1]).Tables[0];
                DataTable __tableAppliatde = ((DataSet)__result[2]).Tables[0];
                DataTable __tableApplicablePath = ((DataSet)__result[3]).Tables[0];

                this._partDetailNameLabel.Text = __productDetail.Rows[0][_g.d.ic_inventory._name_1].ToString();
                this._gridStockBalance._loadFromDataTable(__tableBalance);

                StringBuilder __ApplicableStr = new StringBuilder();
                for (int __row = 0; __row < __tableAppliatde.Rows.Count; __row++)
                {
                    if (__ApplicableStr.Length > 0)
                    {
                        __ApplicableStr.Append(",");
                    }
                    __ApplicableStr.Append(__tableAppliatde.Rows[__row][_g.d.ic_specific_search._keyword].ToString());
                }

                this._applicablePartListBox.Items.Clear();
                for (int __row = 0; __row < __tableApplicablePath.Rows.Count; __row++)
                {
                    //if (__ApplicableStr.Length > 0)
                    //{
                    //    __ApplicableStr.Append(",");
                    //}
                    string __getItemName = (__tableApplicablePath.Rows[__row][_g.d.ic_inventory_suggest._ic_name].ToString());

                    this._applicablePartListBox.Items.Add(__getItemName);
                }

                // picture
                this._getPicture._clearpic();
                string _codepic = itemCode;
                string _codepic_ = _codepic.Replace("/", "").Trim();

                this._getPicture._loadImage(_codepic_);
                this._getPicture._setEnable(true);

            }
        }

    }
}
