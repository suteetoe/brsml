using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Globalization;
using System.Collections;
using System.Json;

namespace SMLEDIControl
{
    public partial class _ediReceive : UserControl
    {
        string _agentCode = "";
        string __url = "http://bs.brteasy.com:8080/SinghaEDI_TEST";
        int _transFlag = 36;
        int _transType = 2;

        public _ediReceive()
        {
            InitializeComponent();

            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            DataSet __result = __myFrameWork._queryShort("select * from " + _g.d.erp_company_profile._table);

            if (__result.Tables.Count > 0 && __result.Tables[0].Rows.Count > 0)
            {
                this._agentCode = __result.Tables[0].Rows[0][_g.d.erp_company_profile._agent_code].ToString();
            }

            this._docGrid._table_name = _g.d.ic_trans._table;
            this._docGrid._addColumn("select", 11, 10, 10);
            this._docGrid._isEdit = false;
            this._docGrid._addColumn(_g.d.ic_trans._doc_no, 1, 90, 90);
            this._docGrid.WidthByPersent = true;
            this._docGrid._calcPersentWidthToScatter();
            this._docGrid._mouseClick += _docGrid__mouseClick;


            splitContainer1.Panel2Collapsed = true;
            splitContainer1.Panel2.Hide();

            this.Load += _singhaOnlineOrderImport_Load;
        }
        private void _singhaOnlineOrderImport_Load(object sender, EventArgs e)
        {
            this._getData();
        }

        private void _docGrid__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            // load data to detail
            if (e._row > -1)
            {
                string __docNo = this._docGrid._cellGet(e._row, _g.d.ic_trans._doc_no).ToString();
                //DataRow[] __getRow = this._dataDetail.Select(_g.d.ic_trans_detail._doc_no + "=\'" + __docNo + "\'");
                //this._detailGrid._loadFromDataTable(this._dataDetail, __getRow);

            }
        }


        void _getData()
        {
            try
            {
                this._docGrid._clear();
                // get data from restful server
                WebClient __n = new WebClient();

                //var __json = __n.DownloadString(__url + "/EDI/" + "?agentcode=" + this._agentCode);
                var __json = __n.DownloadString(__url + "/EDI/" + "?agentcode=1000806");
                JsonValue __jsonObject = JsonValue.Parse(__json);
                //JsonArray __jsonObject = new JsonArray(__json);
                // do other


                if (__jsonObject.Count > 0)
                {
                    for (int __row = 0; __row < __jsonObject.Count; __row++)
                    {
                        JsonValue __object = (JsonValue)__jsonObject[__row];
                        if (__object.ToString().Equals("\"success\"") == false && __object.ToString().Equals("\"reject\"") == false)
                        {
                            int __rowAdd = this._docGrid._addRow();
                            this._docGrid._cellUpdate(__rowAdd, _g.d.ic_trans._doc_no, __object.ToString().Replace("\"", string.Empty), true);
                            this._docGrid._cellUpdate(__rowAdd, 0, 1, true);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void _reloadButton_Click(object sender, EventArgs e)
        {
            this._getData();
        }

        private void _importButton_Click(object sender, EventArgs e)
        {
            this._process();
        }

        void _process()
        {
            try
            {
                if (MessageBox.Show("ต้องการนำเข้าข้อมูลที่ได้เลือกไว้หรือไม่", "ยืนยัน", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {

                    for (int __row = 0; __row < this._docGrid._rowData.Count; __row++)
                    {
                        if (this._docGrid._cellGet(__row, 0).ToString().Equals("1"))
                        {
                            StringBuilder __rejectMessage = new StringBuilder();
                            List<string> __itemList = new List<string>();
                            List<string> __productUnit = new List<string>();
                            Boolean __savePass = false;
                            WebClient __n = new WebClient();

                            StringBuilder __myQuery = new StringBuilder();
                            string __docNo = "";
                            string __fileName = "";
                            try
                            {
                                __fileName = this._docGrid._cellGet(__row, _g.d.ic_trans._doc_no).ToString().Replace("\"", string.Empty);
                                var __jsonStr = __n.DownloadString(__url + "/EDI/order/" + __fileName + "?agentcode=1000806");
                                //JsonValue __json = JsonValue.Parse(__jsonStr);
                                MessageBox.Show(__jsonStr.ToString(), "นำเข้าข้อมูลเรียบร้อยแล้ว พบข้อผิดพลาดที่รายการดังต่อไปนี้", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                // __myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");

                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.ToString(), "นำเข้าข้อมูลเรียบร้อยแล้ว พบข้อผิดพลาดที่รายการดังต่อไปนี้", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                __rejectMessage.AppendLine("Error : " + ex.ToString());
                            }


                        }
                    } // end loop
                    MessageBox.Show("Success");
                    this._getData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void _selectAllButton_Click(object sender, EventArgs e)
        {
            for (int __row = 0; __row < this._docGrid._rowData.Count; __row++)
            {
                this._docGrid._cellUpdate(__row, 0, 1, true);
            }
            this._docGrid.Invalidate();
        }

        private void _selectNoneButton_Click(object sender, EventArgs e)
        {
            for (int __row = 0; __row < this._docGrid._rowData.Count; __row++)
            {
                this._docGrid._cellUpdate(__row, 0, 0, true);
            }
            this._docGrid.Invalidate();
        }

        private void _closeButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}