using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Globalization;

namespace SMLOffTakeSalesAdmin
{
    public partial class _importDataChannelMasterControl : UserControl
    {
        public IFormatProvider __cultureEN = new CultureInfo("en-US");
        DataTable __temp2 = null;
        public _importDataChannelMasterControl()
        {
            InitializeComponent();
            this._maindetail1._selectFileButton.Click += new EventHandler(_selectFileButton_Click);
            this._maindetail1._bntClose.Click += new EventHandler(_bntClose_Click);
            this._maindetail1._bntPreview.Click+=new EventHandler(_bntPreview_Click);
        }

        void _bntClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        private void _saveToDatabase(string query, int countRecord, int maxRecord)
        {

            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();

            string __queryStr = query.ToString();
            string __result = __myFrameWork._queryList(MyLib._myGlobal._databaseName, __queryStr);
            if (__result.Length > 0)
            {

            }
        }
        void _bntPreview_Click(object sender, EventArgs e)
        {
            string __result = "";
            if (__temp2 != null)
            {
                DataSet __ds = new DataSet();
                DataTable __dt = new DataTable();
                DataTable __dttemp = new DataTable();
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                __ds = __myFrameWork._query(MyLib._myGlobal._databaseName, "select code from sml_agent");
                DataColumn[] __dtColumns = new DataColumn[0];
                if (__ds.Tables.Count > 0)
                {
                    __dt = __ds.Tables[0];
                    if (__dt.Rows.Count > 0)
                    {
                        DataTable __dtdupplicate = new DataTable();
                        __dtdupplicate = _myGlobal.__dupplicateKey(__dt, 0);
                        __dt = new DataTable();
                        __dt = __dtdupplicate;
                        ArrayList __listPk = new ArrayList();
                        __listPk.Add("code");
                        __dtColumns = new DataColumn[__listPk.Count];
                        for (int d = 0; d < __listPk.Count; d++)
                        {
                            __dtColumns[d] = __dt.Columns[__listPk[d].ToString()];
                        }
                        __dt.PrimaryKey = __dtColumns;
                    }
                }

                object[] __getClientGuidData = new object[__dtColumns.Length];
                StringBuilder __queryserver = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");
                int __count = 0;
                DataTable __temp2dupplicate = new DataTable();
                __temp2dupplicate = _myGlobal.__dupplicateKey(__temp2, 2);
                __temp2 = new DataTable();
                __temp2 = __temp2dupplicate;
                for (int __rowdata = 0; __rowdata < __temp2.Rows.Count; __rowdata++)
                {
                    __count++;
                    string __custcode = __temp2.Rows[__rowdata][2].ToString().Trim();
                    if (__custcode.Length > 0)
                    {
                        int __execMode = 1;
                        if (__dt.PrimaryKey.Length != 0)
                        {
                            DataRow __dataRowClient = __dt.Rows.Find(__custcode);
                            if (__dataRowClient != null)
                            {
                                __execMode = 2;
                            }                           
                          //  __execMode = (__dataRowClient == null) ? 1 : 2; // 1=Insert,2=Update
                        }   
                      
                        if (__execMode == 1)
                        {
                            string __Slsmn = __temp2.Rows[__rowdata][0].ToString().Replace("\'", "\'\'");
                            string __cust_group = __temp2.Rows[__rowdata][1].ToString().Replace("\'", "\'\'");
                            __custcode = __temp2.Rows[__rowdata][2].ToString().Replace("\'", "\'\'");
                            string __cust_name = __temp2.Rows[__rowdata][3].ToString().Replace("\'", "\'\'");
                            string __cust_address = __temp2.Rows[__rowdata][4].ToString().Replace("\'", "\'\'");
                            string __cust_address1 = __temp2.Rows[__rowdata][5].ToString().Replace("\'", "\'\'");
                            string __cust_address2 = __temp2.Rows[__rowdata][6].ToString().Replace("\'", "\'\'");
                            string __cust_phone = __temp2.Rows[__rowdata][7].ToString().Replace("\'", "\'\'");
                            string __post_code = __temp2.Rows[__rowdata][8].ToString().Replace("\'", "\'\'");
                            string __cust_type = __temp2.Rows[__rowdata][9].ToString().Replace("\'", "\'\'");
                            string __mkt_lvl = __temp2.Rows[__rowdata][10].ToString().Replace("\'", "\'\'");
                            string __mkt_chan = __temp2.Rows[__rowdata][11].ToString().Replace("\'", "\'\'");
                            string __chan_type = __temp2.Rows[__rowdata][12].ToString().Replace("\'", "\'\'");
                            string __dateAcc = __temp2.Rows[__rowdata][13].ToString().Replace("\'", "\'\'");
                            string __province = __temp2.Rows[__rowdata][14].ToString().Replace("\'", "\'\'");
                            string __rsm = __temp2.Rows[__rowdata][15].ToString().Replace("\'", "\'\'");
                            string __region = __temp2.Rows[__rowdata][16].ToString().Replace("\'", "\'\'");
                            string __salemanname = __temp2.Rows[__rowdata][17].ToString().Replace("\'", "\'\'");
                            string __installedDate = __temp2.Rows[__rowdata][18].ToString().Replace("\'", "\'\'");
                            if (__installedDate.Length == 0)
                            {
                                __installedDate = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", __cultureEN);
                            }
                            if (__dateAcc.Length == 0)
                            {
                                // Convert.ToDateTime(__dateAcc);
                                __dateAcc = "19000101";
                            }
                            //CultureInfo _ci_en = new CultureInfo("en-US");
                            //DateTime __date = DateTime.ParseExact(__dateAcc.ToString(), "yyyyMMdd", _ci_en.DateTimeFormat);
                            //__dateAcc = __date.ToString("yyyyMMdd", __cultureEN);
                            StringBuilder __query = new StringBuilder();

                            __query.Append("INSERT INTO [sml_agent]([code],[name],[slsmn],[cust_group],[address],[address1],[address2],[phone],[postcode],[cust_type],[mkt_lev],[mkt_chan],[chan_type],[dateacc],[province],[rsm],[region],[salesmanname],[installeddate]) ");
                            __query.Append(" VALUES ");
                            __query.Append(" ('" + __custcode + "','" + __cust_name + "','" + __Slsmn + "','" + __cust_group + "','" + __cust_address + "','" + __cust_address1 + "','" + __cust_address2 + "','" + __cust_phone + "','" + __post_code + "','" + __cust_type + "','" + __mkt_lvl + "','" + __mkt_chan + "','" + __chan_type + "','" + __dateAcc + "','" + __province + "','" + __rsm + "','" + __region + "','" + __salemanname + "','" + __installedDate + "')");
                            __queryserver.Append(MyLib._myUtil._convertTextToXmlForQuery(__query.ToString()));
                            if (__count > 100)
                            {
                                __queryserver.Append("</node>");
                                _saveToDatabase(__queryserver.ToString(), 0, 0);
                                __count = 1;
                                __queryserver = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");
                            }
                        }
                    }
                }
                __queryserver.Append("</node>");
                if (__count > 0)
                {
                    _saveToDatabase(__queryserver.ToString(), 0, 0);
                }
                if (__result.Length == 0)
                {
                    MessageBox.Show("Success");
                    this._maindetail1._dataGridView.DataSource = null;
                    this.__temp2 = null;
                }
                else
                {
                    MessageBox.Show("Error >> " + __result);
                }
            }
            else
            {
                MessageBox.Show("เลือก File ก่อน ");
            }
        }
        void _selectFileButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog __file = new OpenFileDialog();
            __file.Title = "Select Text File";
            __file.Multiselect = false;
            __file.Filter = "xml files (*.xml)|*.xml";
            if (__file.ShowDialog() == DialogResult.OK)
            {
                this._maindetail1._textFileTextBox.Text = __file.FileName.ToString();
                //this._processMessage = "Build Data to Grid";                   
                this._maindetail1._dataGridView.DataSource = new DataTable();

            }
            try
            {
                __temp2 = _myGlobal.getXmlSpeedsheet(__file.FileName.ToString(), 5);
                this._maindetail1._dataGridView.DataSource = __temp2;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error 1 " + ex.Message);
            }
        }
    }
}
