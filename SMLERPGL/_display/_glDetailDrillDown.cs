using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMLERPGL._display
{
    public partial class _glDetailDrillDown : Form
    {
        public _glDetailDrillDown()
        {
            InitializeComponent();
            this._glDetail1._glDetailGrid._isEdit = false;
            this._glDetail1.Invalidate();
        }

        public void _process(string docNo)
        {
            this._glDetail1._loadData(this._screenTop,false,  docNo);
            try
            {
                /*MyLib._myFrameWork myFrameWork = new MyLib._myFrameWork();
                StringBuilder __myquery = new StringBuilder();
                string __whereString = " where " + _g.d.gl_journal._doc_date + "=\'" + MyLib._myGlobal._convertDateToQuery(docDate) + "\' and " + _g.d.gl_journal._doc_no + "=\'" + docNo + "\' and " + _g.d.gl_journal._book_code + "=\'" + bookCode + "\'";
                __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.gl_journal._table + __whereString));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.gl_journal_detail._table + __whereString));
                __myquery.Append("</node>");
                ArrayList _getData = myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
                this._screenTop._loadData(((DataSet)_getData[0]).Tables[0]);
                this._glDetail1._glDetailGrid._loadFromDataTable(((DataSet)_getData[1]).Tables[0]);
                this._screenTop._search(false);
                this._screenTop.Invalidate();
                this._glDetail1._glDetailGrid.Invalidate();*/
            }
            catch
            {
            }
        }
    }
}