using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMLFastReportDesign
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this.toolStrip1.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }

            this._myDataList._gridData._isEdit = false;
            this._myDataList._lockRecord = true;
            this._myDataList._loadViewFormat("screen_fastreport_loadreport", MyLib._myGlobal._userSearchScreenGroup, true);
            this._myDataList._referFieldAdd(_g.d.sml_fastreport._menuid, 1);

            this._myDataList._gridData._mouseDoubleClick += new MyLib.MouseDoubleClickHandler(_gridData__mouseDoubleClick);
            this._myDataList._deleteData += new MyLib.DeleteDataEventHandler(_myDataList__deleteData);

        }

        void _myDataList__deleteData(System.Collections.ArrayList selectRowOrder)
        {
            MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();

            StringBuilder __myQuery = new StringBuilder();
            __myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");
            for (int __loop = 0; __loop < selectRowOrder.Count; __loop++)
            {
                MyLib._deleteDataType getData = (MyLib._deleteDataType)selectRowOrder[__loop];
                __myQuery.Append(string.Format(MyLib._myUtil._convertTextToXmlForQuery("delete from {0} {1}"), _myDataList._tableName, getData.whereString));
            } // for
            __myQuery.Append("</node>");
            string result = _myFrameWork._queryList(MyLib._myGlobal._databaseName, __myQuery.ToString());
            if (result.Length == 0)
            {
                MyLib._myGlobal._displayWarning(1, MyLib._myGlobal._resource("สำเร็จ"));
                _myDataList._refreshData();
            }
            else
            {
                MessageBox.Show(result, "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        void _gridData__mouseDoubleClick(object sender, MyLib.GridCellEventArgs e)
        {
            // select Row
            if (e._row != -1)
            {
                // create tab
                string __reportId = ((MyLib._myGrid)sender)._cellGet(e._row, 1).ToString();
                string __reportName = ((MyLib._myGrid)sender)._cellGet(e._row, 2).ToString();
                //_ReportSelected(this, new SelectReportEvent(e._row, ((MyLib._myGrid)sender)._cellGet(e._row, 0).ToString(), ((MyLib._myGrid)sender)._cellGet(e._row, 1).ToString()));
                SMLFastReport._designer __fastReport = new SMLFastReport._designer();
                __fastReport._load(__reportId, true);
                _createAndSelectTab(__reportId, __reportId, __reportName, __fastReport);

            }

        }

        private void _newButton_Click(object sender, EventArgs e)
        {
            // create tab
            int __tabCount = this._tabControl.TabPages.Count + 1;
            _createAndSelectTab("สร้างรายงาน " + __tabCount, "สร้างรายงาน " + __tabCount, "สร้างรายงาน " + __tabCount, new SMLFastReport._designer());
        }

        string _createTab(string originalID, string tabID, string tabName)
        {
            string __guid = _g.g._logMenu(1, "", originalID);
            MyLib._myTabPage myTabPage = new MyLib._myTabPage();
            myTabPage.Name = tabID + Guid.NewGuid().ToString();
            myTabPage.__screenGuid = __guid;
            myTabPage.__menuCode = originalID;
            myTabPage.Text = tabName;
            myTabPage.Tag = originalID;
            myTabPage.UseVisualStyleBackColor = false;
            this._tabControl.TabPages.Add(myTabPage);
            return myTabPage.Name;
        }

        private void _displayTab(Control screen)
        {
            this._tabControl.SelectedTab.Controls.Add(screen);
            screen.Dock = DockStyle.Fill;
            screen.Disposed += new EventHandler(_screen_Disposed);
            screen.Focus();
        }

        void _screen_Disposed(object sender, EventArgs e)
        {
            try
            {
                int thisTab = this._tabControl.SelectedIndex;
                this._tabControl.SelectedTab.Dispose();
                if (thisTab > 0)
                {
                    this._tabControl.SelectTab(--thisTab);
                }
            }
            catch
            {
            }
        }

        public void _createAndSelectTab(string originalID, string tabID, string tabName, Control screen)
        {
            string __tabID = _createTab(originalID, tabID, tabName);
            this._tabControl.SelectTab(__tabID);

            _displayTab(screen);
            screen.Focus();
        }

        private void _generateXMLButton_Click(object sender, EventArgs e)
        {
            _formCreateScrip __genScript = new _formCreateScrip();
            __genScript.Show();
        }

    }
}
