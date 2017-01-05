﻿using System;
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
    public partial class _importDataRDM_Master : UserControl
    {
        public IFormatProvider __cultureEN = new CultureInfo("en-US");
        DataTable __temp2 = null;
        public _importDataRDM_Master()
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
                __ds = __myFrameWork._query(MyLib._myGlobal._databaseName, "select colornumber from rdmcolor");
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
                        __listPk.Add("colornumber");
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
                __temp2dupplicate = _myGlobal.__dupplicateKey(__temp2, 0);
                __temp2 = new DataTable();
                __temp2 = __temp2dupplicate;
                for (int __rowdata = 0; __rowdata < __temp2.Rows.Count; __rowdata++)
                {
                    __count++;
                    string __colornumber = __temp2.Rows[__rowdata][0].ToString();
                    if (__colornumber.Length > 0)
                    {
                        int __execMode = 1;
                        if (__dt.PrimaryKey.Length != 0)
                        {
                            DataRow __dataRowClient = __dt.Rows.Find(__colornumber);
                            __execMode = (__dataRowClient == null) ? 1 : 2; // 1=Insert,2=Update
                        }
                        if (__execMode == 1)
                        {
                            __colornumber = __temp2.Rows[__rowdata][0].ToString().Replace("\'", "\'\'");
                            string __hue = __temp2.Rows[__rowdata][1].ToString().Replace("\'", "\'\'");
                            string __depth = __temp2.Rows[__rowdata][2].ToString().Replace("\'", "\'\'");
                            StringBuilder __query = new StringBuilder();
                            __query.Append("INSERT INTO rdmcolor (colornumber,hue,depth)");
                            __query.Append("VALUES");
                            __query.Append("('" + __colornumber + "','" + __hue + "','" + __depth + "')");

                            __queryserver.Append(MyLib._myUtil._convertTextToXmlForQuery(__query.ToString()));
                            if (__count > 100)
                            {
                                __queryserver.Append("</node>");
                                _saveToDatabase(__queryserver.ToString(), 0, 0);
                                __count = 0;
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
                this._maindetail1._dataGridView.DataSource = new DataTable();

            }
            try
            {
                __temp2 = _myGlobal.getXmlSpeedsheet(__file.FileName.ToString(), 1);
                this._maindetail1._dataGridView.DataSource = __temp2;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error 1 " + ex.Message);
            }
        }
        
    }
}
