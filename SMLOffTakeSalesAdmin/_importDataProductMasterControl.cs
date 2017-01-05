using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using System.Collections;

namespace SMLOffTakeSalesAdmin
{
    public partial class _importDataProductMasterControl : UserControl
    {
        public IFormatProvider __cultureEN = new CultureInfo("en-US");
        DataTable __temp2 = null;
        public _importDataProductMasterControl()
        {
            InitializeComponent();
            this._maindetail1._selectFileButton.Click += new EventHandler(_selectFileButton_Click);
            this._maindetail1._bntPreview.Click += new EventHandler(_bntPreview_Click);
            this._maindetail1._bntClose.Click += new EventHandler(_bntClose_Click);
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
                __ds = __myFrameWork._query(MyLib._myGlobal._databaseName, "select product_line from product_master");
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
                        __listPk.Add("product_line");
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
                __temp2dupplicate = _myGlobal.__dupplicateKey(__temp2, 3);
                __temp2 = new DataTable();
                __temp2 = __temp2dupplicate;
                for (int __rowdata = 0; __rowdata < __temp2.Rows.Count; __rowdata++)
                {
                    __count++;
                    string __Product_line = __temp2.Rows[__rowdata][3].ToString().Trim();
                    if (__Product_line.Length > 0)
                    {
                        int __execMode = 1;
                        if (__dt.PrimaryKey.Length != 0)
                        {
                            DataRow __dataRowClient = __dt.Rows.Find(__Product_line);
                            if (__dataRowClient != null)
                            {
                                __execMode = 2;
                            }                            
                          //  __execMode = (__dataRowClient == null) ? 1 : 2; // 1=Insert,2=Update
                        }   
                      
                        if (__execMode == 1)
                        {
                            //Product	Type	Tier	Product line	Product name	Brand
                            string __Product = __temp2.Rows[__rowdata][0].ToString().Replace("\'", "\'\'");
                            string __Type = __temp2.Rows[__rowdata][1].ToString().Replace("\'", "\'\'");                            
                            string __Tier = __temp2.Rows[__rowdata][2].ToString().Replace("\'", "\'\'");
                             __Product_line = __temp2.Rows[__rowdata][3].ToString().Replace("\'", "\'\'");
                            string __Product_name = __temp2.Rows[__rowdata][4].ToString().Replace("\'", "\'\'");
                            string __Brand = __temp2.Rows[__rowdata][5].ToString().Replace("\'", "\'\'");                          

                            StringBuilder __query = new StringBuilder();

                            __query.Append("INSERT INTO [product_master]([product_line],[product],[product_name],[type],[tier],[brand]) ");
                            __query.Append(" VALUES ");
                            __query.Append(" ('" + __Product_line + "','" + __Product + "','" + __Product_name + "','" + __Type + "','" + __Tier + "','" + __Brand + "')");
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
                __temp2 = _myGlobal.getXmlSpeedsheet(__file.FileName.ToString(), 2);
                this._maindetail1._dataGridView.DataSource = __temp2;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error 1 " + ex.Message);
            }
        }
    }
}
