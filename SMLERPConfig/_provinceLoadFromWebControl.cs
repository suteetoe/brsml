using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace SMLERPConfig
{
    public partial class _provinceLoadFromWebControl : UserControl
    {
        private string _thailandDatabaseName = "thailand";

        public _provinceLoadFromWebControl()
        {
            InitializeComponent();
            this.Load += new EventHandler(_provinceLoadFromWebrControl_Load);
        }

        void _provinceLoadFromWebrControl_Load(object sender, EventArgs e)
        {
            this._progressBar.Visible = false;
            this._myGrid1._addColumn("check", 11, 0, 10, false, false, false, false);
            this._myGrid1._addColumn("Code", 1, 500, 25);
            this._myGrid1._addColumn("Name", 1, 500, 50);
            this._myGrid1._addColumn("ID", 1, 500, 5);
            this._myGrid1._addColumn("Latitude", 1, 500, 5);
            this._myGrid1._addColumn("Longtitude", 1, 500, 5);
            MyLib._myFrameWork __smlWebService = new MyLib._myFrameWork(MyLib._myGlobal._masterWebservice, MyLib._myGlobal._masterConfigXmlName, MyLib._myGlobal._databaseType.PostgreSql);
            DataSet __province = __smlWebService._query(this._thailandDatabaseName, "select province_code,province_name,province_id,lat,lng from province order by province_code");
            if (__province.Tables.Count > 0 && __province.Tables[0].Rows.Count > 0)
            {
                for (int __row = 0; __row < __province.Tables[0].Rows.Count; __row++)
                {
                    int __addr = this._myGrid1._addRow();
                    this._myGrid1._cellUpdate(__addr, 1, __province.Tables[0].Rows[__row][0].ToString(), false);
                    this._myGrid1._cellUpdate(__addr, 2, __province.Tables[0].Rows[__row][1].ToString(), false);
                    this._myGrid1._cellUpdate(__addr, 3, __province.Tables[0].Rows[__row][2].ToString(), false);
                    this._myGrid1._cellUpdate(__addr, 4, __province.Tables[0].Rows[__row][3].ToString(), false);
                    this._myGrid1._cellUpdate(__addr, 5, __province.Tables[0].Rows[__row][4].ToString(), false);
                }
            }
            this._myGrid1.Invalidate();
        }

        private void vistaButton2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void _selectAllButton_Click(object sender, EventArgs e)
        {
            for (int __row = 0; __row < this._myGrid1._rowData.Count; __row++)
            {
                this._myGrid1._cellUpdate(__row, 0, 1, false);
            }
            this._myGrid1.Invalidate();
        }

        void __process()
        {
            try
            {
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                MyLib._myFrameWork __smlWebService = new MyLib._myFrameWork(MyLib._myGlobal._masterWebservice, MyLib._myGlobal._masterConfigXmlName, MyLib._myGlobal._databaseType.PostgreSql);
                for (int __row = 0; __row < this._myGrid1._rowData.Count; __row++)
                {
                    int __checked = (int)this._myGrid1._cellGet(__row, 0);
                    if (__checked == 1)
                    {
                        string __provinceCode = (String)this._myGrid1._cellGet(__row, 1);
                        string __provinceName = (String)this._myGrid1._cellGet(__row, 2);
                        int __province_id = MyLib._myGlobal._intPhase((String)this._myGrid1._cellGet(__row, 3));
                        string __province_lat = (String)this._myGrid1._cellGet(__row, 4);
                        string __province_lng = (String)this._myGrid1._cellGet(__row, 5);
                        _progressMessage = __provinceCode + "," + __provinceName;
                        /*this._statusLabel.Text = __provinceCode + "," + __provinceName;
                        this._statusLabel.Refresh();*/
                        StringBuilder __myQuery = new StringBuilder();
                        __myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                        __myQuery.Append("<query>");
                        __myQuery.Append("delete from " + _g.d.erp_province._table + " where " + _g.d.erp_province._code + "=\'" + __provinceCode + "\'");
                        __myQuery.Append("</query>");
                        __myQuery.Append("<query>");
                        __myQuery.Append(
                            "insert into " + _g.d.erp_province._table + " (" + _g.d.erp_province._code + "," + _g.d.erp_province._name_1 + "," + _g.d.erp_province._lat + "," + _g.d.erp_province._lng + ") " +
                            "values (\'" + __provinceCode + "\',\'" + __provinceName + "\'," + __province_lat + "," + __province_lng + ")");
                        __myQuery.Append("</query>");
                        // ดึงอำเภอ
                        DataSet __ampher = __smlWebService._query(this._thailandDatabaseName, "select amphur_code,amphur_name,amphur_id,lat,lng from amphur where province_id=\'" + __province_id.ToString() + "\'");
                        for (int __rowAmpher = 0; __rowAmpher < __ampher.Tables[0].Rows.Count; __rowAmpher++)
                        {
                            string __ampherCode = __ampher.Tables[0].Rows[__rowAmpher][0].ToString();
                            string __ampherName = __ampher.Tables[0].Rows[__rowAmpher][1].ToString().Replace("*", "");
                            int __ampherID = MyLib._myGlobal._intPhase(__ampher.Tables[0].Rows[__rowAmpher][2].ToString());
                            string __ampher_lat = __ampher.Tables[0].Rows[__rowAmpher][3].ToString();
                            string __ampher_lng = __ampher.Tables[0].Rows[__rowAmpher][4].ToString();
                            _progressMessage = __provinceCode + "," + __provinceName + " : " + __ampherCode + "," + __ampherName;
                            __myQuery.Append("<query>");
                            __myQuery.Append("delete from " + _g.d.erp_amper._table + " where " + _g.d.erp_amper._province + "=\'" + __provinceCode + "\' and " + _g.d.erp_amper._code + "=\'" + __ampherCode + "\'");
                            __myQuery.Append("</query>");
                            __myQuery.Append("<query>");
                            __myQuery.Append(
                                "insert into " + _g.d.erp_amper._table + " (" + _g.d.erp_amper._province + "," + _g.d.erp_amper._code + "," + _g.d.erp_amper._name_1 + "," + _g.d.erp_amper._lat + "," + _g.d.erp_amper._lng + ") " +
                                "values (\'" + __provinceCode + "\',\'" + __ampherCode + "\',\'" + __ampherName + "\'," + __ampher_lat + "," + __ampher_lng + ")");
                            __myQuery.Append("</query>");
                            // ดึงตำบล
                            DataSet __tambon = __smlWebService._query(this._thailandDatabaseName, "select district_code,district_name,lat,lng from district where province_id=\'" + __province_id.ToString() + "\' and amphur_id=\'" + __ampherID.ToString() + "\'");
                            for (int __rowTambon = 0; __rowTambon < __tambon.Tables[0].Rows.Count; __rowTambon++)
                            {
                                string __tambonCode = __tambon.Tables[0].Rows[__rowTambon][0].ToString();
                                string __tambonName = __tambon.Tables[0].Rows[__rowTambon][1].ToString().Replace("*", "");
                                string __tambon_lat = __tambon.Tables[0].Rows[__rowTambon][2].ToString();
                                string __tambon_lng = __tambon.Tables[0].Rows[__rowTambon][3].ToString();
                                __myQuery.Append("<query>");
                                __myQuery.Append("delete from " + _g.d.erp_tambon._table + " where " + _g.d.erp_tambon._province + "=\'" + __provinceCode + "\' and " + _g.d.erp_tambon._amper + "=\'" + __ampherCode + "\' and " + _g.d.erp_tambon._code + "=\'" + __tambonCode + "\'");
                                __myQuery.Append("</query>");
                                __myQuery.Append("<query>");
                                __myQuery.Append(
                                    "insert into " + _g.d.erp_tambon._table + " (" + _g.d.erp_tambon._province + "," + _g.d.erp_tambon._amper + "," + _g.d.erp_tambon._code + "," + _g.d.erp_tambon._name_1 + "," + _g.d.erp_tambon._lat + "," + _g.d.erp_tambon._lng + ") " +
                                    "values (\'" + __provinceCode + "\',\'" + __ampherCode + "\',\'" + __tambonCode + "\',\'" + __tambonName + "\'," + __tambon_lat + "," + __tambon_lng + ")");
                                __myQuery.Append("</query>");
                            }
                        }

                        __myQuery.Append("</node>");
                        string __result = __myFrameWork._queryList(MyLib._myGlobal._databaseName, __myQuery.ToString());
                        if (__result.Length > 0)
                        {
                            this._progressMessage = __result;
                        }
                        this._progressCount++;
                    }
                }
                this._progressMessage = "Success";
            }
            catch (Exception __ex)
            {
                this._progressMessage = __ex.Message.ToString() + " : " + __ex.StackTrace.ToString();
            }
        }

        string _progressMessage = "";
        int _progressMax = 0;
        int _progressCount = 0;

        private void _processButton_Click(object sender, EventArgs e)
        {
            this._progressCount = 0;
            this._progressMax = 0;
            for (int __row = 0; __row < this._myGrid1._rowData.Count; __row++)
            {
                int __checked = (int)this._myGrid1._cellGet(__row, 0);
                if (__checked == 1)
                {
                    this._progressMax++;
                }
            }
            this._progressBar.Visible = true;
            this._progressBar.Maximum = this._progressMax;
            this.Enabled = false;
            Thread __new = new Thread(__process);
            __new.Start();
            this._timer1.Start();
        }

        private void _timer1_Tick(object sender, EventArgs e)
        {
            this._progressBar.Value = this._progressCount;
            this._progressBar.Invalidate();
            this._statusLabel.Text = this._progressMessage;
            this._statusLabel.Invalidate();
            if (this._progressMessage.Equals("Success"))
            {
                this._timer1.Stop();
                MessageBox.Show(this._progressMessage);
                this._progressMessage = "";
                this._progressBar.Visible = false;
                this.Enabled = true;
            }
        }
    }
}
