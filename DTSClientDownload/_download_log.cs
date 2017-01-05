using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;

namespace DTSClientDownload
{
    public partial class _download_log : UserControl
    {

        string _itemDownloadStr = "ข้อมูลสินค้าจาก SCG";
        string _poDownloadStr = "ใบสั่งซื้อสินค้าจาก SCG";
        string _soDownloadStr = "ใบสั่งขาย/สั่งจองสินค้าจาก SCG";

        public _download_log()
        {
            InitializeComponent();
            _build();
            this.Load += new EventHandler(_download_log_Load);
        }

        void _download_log_Load(object sender, EventArgs e)
        {
            _loadData();
        }

        void _build()
        {
            // item
            this._gridDownloadItem._isEdit = false;
            this._gridDownloadItem._addColumn("code", 1, 25, 20, true, false, true, false, "", "", "", "รหัสสินค้า");
            this._gridDownloadItem._addColumn("barcode", 1, 25, 20, true, false, true, false, "", "", "", "บาร์โค๊ต");
            this._gridDownloadItem._addColumn("name1", 1, 25, 30, true, false, true, false, "", "", "", "ชื่อสินค้า");
            this._gridDownloadItem._addColumn("defstkunitcode", 1, 25, 10, true, false, true, false, "", "", "", "หน่วยนับ");
            this._gridDownloadItem._addColumn("unittype", 1, 25, 10, true, false, true, false, "", "", "", "-");
            this._gridDownloadItem._importFromTextFileFastToolStripMenuItem.Visible = false;
            this._gridDownloadItem._importFromTextFileToolStripMenuItem.Visible = false;
            this._gridDownloadItem._calcPersentWidthToScatter();

            // po
            this._gridDownloadPO._isEdit = false;
            this._gridDownloadPO._addColumn("docno", 1, 25, 25, true, false, true, false, "", "", "", "เลขที่เอกสาร");
            this._gridDownloadPO._addColumn("docdate", 4, 25, 25, true, false, true, false, "", "", "", "วันที่เอกสาร");
            this._gridDownloadPO._addColumn("mydescription", 1, 25, 40, true, false, true, false, "", "", "", "กลุ่มสินค้า");
            this._gridDownloadPO._importFromTextFileFastToolStripMenuItem.Visible = false;
            this._gridDownloadPO._importFromTextFileToolStripMenuItem.Visible = false;
            this._gridDownloadPO._calcPersentWidthToScatter();

            // so
            this._gridDownloadSO._isEdit = false;
            this._gridDownloadSO._addColumn("docno", 1, 25, 20, true, false, true, false, "", "", "", "เลขที่เอกสาร");
            this._gridDownloadSO._addColumn("docdate", 4, 25, 20, true, false, true, false, "", "", "", "วันที่เอกสาร");
            this._gridDownloadSO._addColumn("ownreceive", 1, 25, 30, true, false, true, false, "", "", "", "ผู้รับสินค้า");
            this._gridDownloadSO._addColumn("mydescription", 1, 25, 20, true, false, true, false, "", "", "", "หมายเหตุ");
            this._gridDownloadSO._importFromTextFileFastToolStripMenuItem.Visible = false;
            this._gridDownloadSO._importFromTextFileToolStripMenuItem.Visible = false;
            this._gridDownloadSO._calcPersentWidthToScatter();

        }

        void _loadData()
        {
            string __extraWhere = "";
            int __queryIndex = -1;

            int __itemQueryIndex = -1;
            int __SOQueryIndex = -1;
            int __POQueryIndex = -1;

            List<string> __query = new List<string>();
            if ((_emptySearchTextAndDate() == true && _unCheckAll() == true) ||
                        (_emptySearchTextAndDate() == false && _unCheckAll() == true) ||
                        (_emptySearchTextAndDate() == true && _searchItem.Checked == true) ||
                        (_emptySearchTextAndDate() == false && _searchItem.Checked == true)
                        )
            {

                if (this._downloadDate.TextBox.Text.Trim().Length > 0)
                {
                    __extraWhere = __extraWhere + " and REPLACE(CONVERT(VARCHAR(10), download_date, 111), '/', '-')  = " + this._downloadDate._textQuery("") + "";
                }


                StringBuilder __downloadICLog = new StringBuilder();
                __downloadICLog.Append("select dts_download_detail.agentcode, dts_download_detail.guid_download, dts_download_detail.download_flag, (CONVERT(varchar, dts_download_detail.download_date, 120)) as download_date, (CONVERT(varchar, dts_download_detail.download_start, 120)) as download_start, (CONVERT(varchar, dts_download_detail.download_success, 120)) as download_success, dts_download_detail.last_status, dts_download_detail.download_status, dts_download_detail.ref_no as code ");
                __downloadICLog.Append(", dts_bcitem.barcode as barcode, dts_bcitem.name1 as name1, dts_bcitem.defstkunitcode as defstkunitcode, dts_bcitem.unittype as unittype ");
                __downloadICLog.Append(" from dts_download_detail left join dts_bcitem on dts_bcitem.code = dts_download_detail.ref_no ");
                __downloadICLog.Append(" where download_flag = 0 ");

                if (_searchItem.Checked == true || _unCheckAll())
                {
                    __downloadICLog.Append(__extraWhere);

                    if (this._searchTextbox.Text.Trim().Length > 0)
                    {
                        __downloadICLog.Append(" and ( " + _global._whereLike("dts_bcitem.barcode,dts_bcitem.name1,dts_bcitem.defstkunitcode", this._searchTextbox.Text.Trim()) + " )");
                    }
                }

                __query.Add(__downloadICLog.ToString());
                __queryIndex++;
                __itemQueryIndex = __queryIndex;
            }

            if ((_emptySearchTextAndDate() == true && _unCheckAll() == true) ||
                        (_emptySearchTextAndDate() == false && _unCheckAll() == true) ||
                        (_emptySearchTextAndDate() == true && _searchPO.Checked == true) ||
                        (_emptySearchTextAndDate() == false && _searchPO.Checked == true)
                        )
            {

                StringBuilder __downloadPOLog = new StringBuilder();
                __downloadPOLog.Append("select dts_download_detail.agentcode, dts_download_detail.guid_download, dts_download_detail.download_flag, (CONVERT(varchar, dts_download_detail.download_date, 120)) as download_date, (CONVERT(varchar, dts_download_detail.download_start, 120)) as download_start, (CONVERT(varchar, dts_download_detail.download_success, 120)) as download_success, dts_download_detail.last_status, dts_download_detail.download_status, dts_download_detail.ref_no as docno ");
                __downloadPOLog.Append(", (CONVERT(varchar, dts_bcpurchaseorder.docdate, 120)) as docdate ");
                __downloadPOLog.Append(" from dts_download_detail left join dts_bcpurchaseorder on dts_bcpurchaseorder.docno = dts_download_detail.ref_no ");
                __downloadPOLog.Append(" where download_flag = 2");

                if (_searchPO.Checked == true || _unCheckAll())
                {
                    __downloadPOLog.Append(__extraWhere);
                    if (this._searchTextbox.Text.Trim().Length > 0)
                    {
                        __downloadPOLog.Append(" and " + _global._whereLike("dts_bcpurchaseorder.docno", this._searchTextbox.Text.Trim()));
                    }
                }

                __query.Add(__downloadPOLog.ToString());
                __queryIndex++;
                __POQueryIndex = __queryIndex;
            }

            if ((_emptySearchTextAndDate() == true && _unCheckAll() == true) ||
                        (_emptySearchTextAndDate() == false && _unCheckAll() == true) ||
                        (_emptySearchTextAndDate() == true && _searchSO.Checked == true) ||
                        (_emptySearchTextAndDate() == false && _searchSO.Checked == true)
                        )
            {
                StringBuilder __downloadSOLog = new StringBuilder();
                __downloadSOLog.Append("select dts_download_detail.agentcode, dts_download_detail.guid_download, dts_download_detail.download_flag, (CONVERT(varchar, dts_download_detail.download_date, 120)) as download_date, (CONVERT(varchar, dts_download_detail.download_start, 120)) as download_start, (CONVERT(varchar, dts_download_detail.download_success, 120)) as download_success, dts_download_detail.last_status, dts_download_detail.download_status, dts_download_detail.ref_no as docno ");
                __downloadSOLog.Append(", (CONVERT(varchar, dts_bcsaleorder.docdate, 120))  as docdate, dts_bcsaleorder.ownreceive as ownreceive, dts_bcsaleorder.mydescription as mydescription ");
                __downloadSOLog.Append(" from dts_download_detail left join dts_bcsaleorder on dts_bcsaleorder.docno = dts_download_detail.ref_no ");
                __downloadSOLog.Append(" where download_flag = 3");

                if (_searchSO.Checked == true || _unCheckAll())
                {
                    __downloadSOLog.Append(__extraWhere);
                    if (this._searchTextbox.Text.Trim().Length > 0)
                    {
                        __downloadSOLog.Append(" and " + _global._whereLike("dts_bcsaleorder.docno,dts_bcsaleorder.ownreceive,dts_bcsaleorder.mydescription", this._searchTextbox.Text.Trim()));
                    }
                }

                __query.Add(__downloadSOLog.ToString());
                __queryIndex++;
                __SOQueryIndex = __queryIndex;

            }

            _clientFrameWork __myFramework = new _clientFrameWork();
            //__myFramework._saveLogFile = true;
            //__myFramework._saveLogFileName = @"C:\\BCS\\qoledb.txt";
            //DataSet __res = __myFramework._query(__downloadICLog);
            //this._gridDownloadItem._loadFromDataTable(__res.Tables[0]);

            ArrayList __result = __myFramework._getDataList(__query);

            if (__result.Count > 0)
            {
                this._gridDownloadItem._clear();
                this._searchItem.Text = _itemDownloadStr;
                if (__itemQueryIndex != -1 && ((DataSet)__result[__itemQueryIndex]).Tables.Count > 0)
                {
                    if ((_emptySearchTextAndDate() == true && _unCheckAll() == true) ||
                        (_emptySearchTextAndDate() == false && _unCheckAll() == true) ||
                        (_emptySearchTextAndDate() == true && _searchItem.Checked == true) ||
                        (_emptySearchTextAndDate() == false && _searchItem.Checked == true)
                        )
                    {
                        this._gridDownloadItem._loadFromDataTable(((DataSet)__result[__itemQueryIndex]).Tables[0]);
                        this._searchItem.Text = _itemDownloadStr + " (" + ((DataSet)__result[__itemQueryIndex]).Tables[0].Rows.Count + ")";
                    }
                }
                else
                {
                    this._searchItem.Text = _itemDownloadStr + " (0)";
                }

                this._gridDownloadPO._clear();
                this._searchPO.Text = _poDownloadStr;
                if (__POQueryIndex != -1 && ((DataSet)__result[__POQueryIndex]).Tables.Count > 0)
                {
                    if ((_emptySearchTextAndDate() == true && _unCheckAll() == true) ||
                        (_emptySearchTextAndDate() == false && _unCheckAll() == true) ||
                        (_emptySearchTextAndDate() == true && _searchPO.Checked == true) ||
                        (_emptySearchTextAndDate() == false && _searchPO.Checked == true)
                        )
                    {

                        this._gridDownloadPO._loadFromDataTable(((DataSet)__result[__POQueryIndex]).Tables[0]);
                        this._searchPO.Text = _poDownloadStr + " (" + ((DataSet)__result[__POQueryIndex]).Tables[0].Rows.Count + ")";
                    }
                }
                else
                {
                    this._searchPO.Text = _poDownloadStr + " (0)";
                }

                this._gridDownloadSO._clear();
                this._searchSO.Text = _soDownloadStr;
                if (__SOQueryIndex != -1 && ((DataSet)__result[__SOQueryIndex]).Tables.Count > 0)
                {
                    if ((_emptySearchTextAndDate() == true && _unCheckAll() == true) ||
                        (_emptySearchTextAndDate() == false && _unCheckAll() == true) ||
                        (_emptySearchTextAndDate() == true && _searchSO.Checked == true) ||
                        (_emptySearchTextAndDate() == false && _searchSO.Checked == true)
                        )
                    {
                        this._gridDownloadSO._loadFromDataTable(((DataSet)__result[__SOQueryIndex]).Tables[0]);
                        this._searchSO.Text = _soDownloadStr + " (" + ((DataSet)__result[__SOQueryIndex]).Tables[0].Rows.Count + ")";
                    }
                }
                else
                {
                    this._searchSO.Text = _soDownloadStr + " (0)";
                }

            }
            else
            {
                this._searchItem.Text = _itemDownloadStr + " (0)";
                this._searchPO.Text = _poDownloadStr + " (0)";
                this._searchSO.Text = _soDownloadStr + " (0)";
            }

        }

        Boolean _emptySearchTextAndDate()
        {
            Boolean __result = false;
            if (_searchTextbox.Text.Trim().Length == 0 && this._downloadDate.TextBox.Text.Trim().Length == 0)
            {
                return true;
            }

            return __result;
        }

        Boolean _unCheckAll()
        {
            Boolean __result = false;

            if (_searchItem.Checked == false && _searchPO.Checked == false && _searchSO.Checked == false)
                __result = true;
            else
                __result = false;

            return __result;

        }

        private void _searchButton_Click(object sender, EventArgs e)
        {
            _loadData();
        }

        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Exception Occured while releasing object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }

        private void _exportButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog __objFile = new SaveFileDialog() { DefaultExt = "xls", Filter = "Excel Files (*.xls)|*.xls", FilterIndex = 0 };
            if (__objFile.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string __xmlFileName = __objFile.FileName;

                    Excel.Application xlApp;
                    Excel.Workbook xlWorkBook;
                    Excel.Worksheet xlWorkSheet;
                    object misValue = System.Reflection.Missing.Value;

                    xlApp = new Excel.ApplicationClass();
                    xlWorkBook = xlApp.Workbooks.Add(misValue);

                    #region สินค้า

                    xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
                    xlWorkSheet.Name = _itemDownloadStr;
                    xlWorkSheet.Cells[1, 1] = "รหัสสินค้า";
                    xlWorkSheet.Cells[1, 1] = __excelFormatAlignCenter((Excel.Range)xlWorkSheet.Cells[1, 1]);
                    ((Excel.Range)xlWorkSheet.Cells[1, 1]).ColumnWidth = 15;

                    xlWorkSheet.Cells[1, 2] = "บาร์โค๊ด";
                    xlWorkSheet.Cells[1, 2] = __excelFormatAlignCenter((Excel.Range)xlWorkSheet.Cells[1, 2]);
                    ((Excel.Range)xlWorkSheet.Cells[1, 2]).ColumnWidth = 22;

                    xlWorkSheet.Cells[1, 3] = "ชื่อสินค้า";
                    xlWorkSheet.Cells[1, 3] = __excelFormatAlignCenter((Excel.Range)xlWorkSheet.Cells[1, 3]);
                    ((Excel.Range)xlWorkSheet.Cells[1, 3]).ColumnWidth = 40;

                    xlWorkSheet.Cells[1, 4] = "หน่วยนับ";
                    xlWorkSheet.Cells[1, 4] = __excelFormatAlignCenter((Excel.Range)xlWorkSheet.Cells[1, 4]);
                    ((Excel.Range)xlWorkSheet.Cells[1, 4]).ColumnWidth = 10;

                    for (int __i = 0; __i < this._gridDownloadItem._rowData.Count; __i++)
                    {
                        //__xmlString.Append("<Row>\n");

                        xlWorkSheet.Cells[__i + 2, 1] = MyLib._myUtil._convertTextToXml(this._gridDownloadItem._cellGet(__i, "code").ToString());
                        xlWorkSheet.Cells[__i + 2, 1] = __excelFormat((Excel.Range)xlWorkSheet.Cells[__i + 2, 1]);

                        //((Excel.Range)(xlWorkSheet.Cells[__i + 2, 1])).
                        xlWorkSheet.Cells[__i + 2, 2] = MyLib._myUtil._convertTextToXml(this._gridDownloadItem._cellGet(__i, "barcode").ToString());
                        xlWorkSheet.Cells[__i + 2, 2] = __excelFormatNumber((Excel.Range)xlWorkSheet.Cells[__i + 2, 2]);

                        xlWorkSheet.Cells[__i + 2, 3] = MyLib._myUtil._convertTextToXml(this._gridDownloadItem._cellGet(__i, "name1").ToString());
                        xlWorkSheet.Cells[__i + 2, 3] = __excelFormat((Excel.Range)xlWorkSheet.Cells[__i + 2, 3]);

                        xlWorkSheet.Cells[__i + 2, 4] = MyLib._myUtil._convertTextToXml(this._gridDownloadItem._cellGet(__i, "defstkunitcode").ToString());
                        xlWorkSheet.Cells[__i + 2, 4] = __excelFormat((Excel.Range)xlWorkSheet.Cells[__i + 2, 4]);


                        //__xmlString.Append("<Cell ss:Index=\"2\"  ss:StyleID=\"s83\" ><Data ss:Type=\"String\">" +  + "</Data></Cell>\n");
                        //__xmlString.Append("<Cell ss:Index=\"3\"  ss:StyleID=\"s83\" ><Data ss:Type=\"String\">" + + "</Data></Cell>\n");
                        //__xmlString.Append("<Cell ss:Index=\"4\"  ss:StyleID=\"s83\" ><Data ss:Type=\"String\">" +  + "</Data></Cell>\n");
                        //__xmlString.Append("</Row>\n");
                    }

                    #endregion

                    #region ใบสั่งซื้อ

                    xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(2);
                    xlWorkSheet.Name = _poDownloadStr;

                    xlWorkSheet.Cells[1, 1] = "เลขที่เอกสาร";
                    xlWorkSheet.Cells[1, 1] = __excelFormatAlignCenter((Excel.Range)xlWorkSheet.Cells[1, 1]);
                    ((Excel.Range)xlWorkSheet.Cells[1, 1]).ColumnWidth = 15;

                    xlWorkSheet.Cells[1, 2] = "วันที่เอกสาร";
                    xlWorkSheet.Cells[1, 2] = __excelFormatAlignCenter((Excel.Range)xlWorkSheet.Cells[1, 2]);
                    ((Excel.Range)xlWorkSheet.Cells[1, 2]).ColumnWidth = 13;

                    xlWorkSheet.Cells[1, 3] = "กลุ่มสินค้า";
                    xlWorkSheet.Cells[1, 3] = __excelFormatAlignCenter((Excel.Range)xlWorkSheet.Cells[1, 3]);
                    ((Excel.Range)xlWorkSheet.Cells[1, 3]).ColumnWidth = 40;

                    //xlWorkSheet.Cells[1, 4] = "หน่วยนับ";
                    //xlWorkSheet.Cells[1, 4] = __excelFormat((Excel.Range)xlWorkSheet.Cells[1, 4]);
                    //((Excel.Range)xlWorkSheet.Cells[1, 4]).ColumnWidth = 10;

                    for (int __i = 0; __i < this._gridDownloadPO._rowData.Count; __i++)
                    {
                        //__xmlString.Append("<Row>\n");

                        xlWorkSheet.Cells[__i + 2, 1] = MyLib._myUtil._convertTextToXml(this._gridDownloadPO._cellGet(__i, "docno").ToString());
                        xlWorkSheet.Cells[__i + 2, 1] = __excelFormat((Excel.Range)xlWorkSheet.Cells[__i + 2, 1]);

                        //((Excel.Range)(xlWorkSheet.Cells[__i + 2, 1])).
                        xlWorkSheet.Cells[__i + 2, 2] = MyLib._myUtil._convertTextToXml(MyLib._myGlobal._convertDateToString((DateTime)this._gridDownloadPO._cellGet(__i, "docdate"), false));
                        xlWorkSheet.Cells[__i + 2, 2] = __excelFormat((Excel.Range)xlWorkSheet.Cells[__i + 2, 2]);

                        xlWorkSheet.Cells[__i + 2, 3] = MyLib._myUtil._convertTextToXml(this._gridDownloadPO._cellGet(__i, "mydescription").ToString());
                        xlWorkSheet.Cells[__i + 2, 3] = __excelFormat((Excel.Range)xlWorkSheet.Cells[__i + 2, 3]);

                        //xlWorkSheet.Cells[__i + 2, 4] = MyLib._myUtil._convertTextToXml(this._gridDownloadItem._cellGet(__i, "defstkunitcode").ToString());
                        //xlWorkSheet.Cells[__i + 2, 4] = __excelFormat((Excel.Range)xlWorkSheet.Cells[__i + 2, 4]);


                        //__xmlString.Append("<Cell ss:Index=\"2\"  ss:StyleID=\"s83\" ><Data ss:Type=\"String\">" +  + "</Data></Cell>\n");
                        //__xmlString.Append("<Cell ss:Index=\"3\"  ss:StyleID=\"s83\" ><Data ss:Type=\"String\">" + + "</Data></Cell>\n");
                        //__xmlString.Append("<Cell ss:Index=\"4\"  ss:StyleID=\"s83\" ><Data ss:Type=\"String\">" +  + "</Data></Cell>\n");
                        //__xmlString.Append("</Row>\n");
                    }

                    #endregion

                    #region สั่งขาย สั่งจอง

                    xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(3);
                    xlWorkSheet.Name = "ใบสั่งขาย สั่งจองสินค้าจาก SCG";

                    xlWorkSheet.Cells[1, 1] = "เลขที่เอกสาร";
                    xlWorkSheet.Cells[1, 1] = __excelFormatAlignCenter((Excel.Range)xlWorkSheet.Cells[1, 1]);
                    ((Excel.Range)xlWorkSheet.Cells[1, 1]).ColumnWidth = 15;

                    xlWorkSheet.Cells[1, 2] = "วันที่เอกสาร";
                    xlWorkSheet.Cells[1, 2] = __excelFormatAlignCenter((Excel.Range)xlWorkSheet.Cells[1, 2]);
                    ((Excel.Range)xlWorkSheet.Cells[1, 2]).ColumnWidth = 13;

                    xlWorkSheet.Cells[1, 3] = "ผู้รับสินค้า";
                    xlWorkSheet.Cells[1, 3] = __excelFormatAlignCenter((Excel.Range)xlWorkSheet.Cells[1, 3]);
                    ((Excel.Range)xlWorkSheet.Cells[1, 3]).ColumnWidth = 35;

                    xlWorkSheet.Cells[1, 4] = "หมายเหตุ";
                    xlWorkSheet.Cells[1, 4] = __excelFormatAlignCenter((Excel.Range)xlWorkSheet.Cells[1, 4]);
                    ((Excel.Range)xlWorkSheet.Cells[1, 4]).ColumnWidth = 30;

                    for (int __i = 0; __i < this._gridDownloadSO._rowData.Count; __i++)
                    {
                        //__xmlString.Append("<Row>\n");

                        xlWorkSheet.Cells[__i + 2, 1] = MyLib._myUtil._convertTextToXml(this._gridDownloadSO._cellGet(__i, "docno").ToString());
                        xlWorkSheet.Cells[__i + 2, 1] = __excelFormat((Excel.Range)xlWorkSheet.Cells[__i + 2, 1]);

                        //((Excel.Range)(xlWorkSheet.Cells[__i + 2, 1])).
                        xlWorkSheet.Cells[__i + 2, 2] = MyLib._myUtil._convertTextToXml(MyLib._myGlobal._convertDateToString((DateTime)this._gridDownloadSO._cellGet(__i, "docdate"), false));
                        xlWorkSheet.Cells[__i + 2, 2] = __excelFormat((Excel.Range)xlWorkSheet.Cells[__i + 2, 2]);

                        xlWorkSheet.Cells[__i + 2, 3] = MyLib._myUtil._convertTextToXml(this._gridDownloadSO._cellGet(__i, "ownreceive").ToString());
                        xlWorkSheet.Cells[__i + 2, 3] = __excelFormat((Excel.Range)xlWorkSheet.Cells[__i + 2, 3]);

                        xlWorkSheet.Cells[__i + 2, 4] = MyLib._myUtil._convertTextToXml(this._gridDownloadSO._cellGet(__i, "mydescription").ToString());
                        xlWorkSheet.Cells[__i + 2, 4] = __excelFormat((Excel.Range)xlWorkSheet.Cells[__i + 2, 4]);


                        //__xmlString.Append("<Cell ss:Index=\"2\"  ss:StyleID=\"s83\" ><Data ss:Type=\"String\">" +  + "</Data></Cell>\n");
                        //__xmlString.Append("<Cell ss:Index=\"3\"  ss:StyleID=\"s83\" ><Data ss:Type=\"String\">" + + "</Data></Cell>\n");
                        //__xmlString.Append("<Cell ss:Index=\"4\"  ss:StyleID=\"s83\" ><Data ss:Type=\"String\">" +  + "</Data></Cell>\n");
                        //__xmlString.Append("</Row>\n");
                    }

                    #endregion

                    xlWorkBook.SaveAs(__xmlFileName, Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                    xlWorkBook.Close(true, misValue, misValue);
                    xlApp.Quit();

                    releaseObject(xlWorkSheet);
                    releaseObject(xlWorkBook);
                    releaseObject(xlApp);

                    // openIT
                    try
                    {
                        System.Diagnostics.Process proc = new System.Diagnostics.Process();
                        proc.EnableRaisingEvents = false;
                        proc.StartInfo.FileName = "excel";
                        proc.StartInfo.Arguments = __xmlFileName;
                        proc.Start();
                        //proc.WaitForExit();
                    }
                    catch (Exception __ex)
                    {
                        MessageBox.Show("Fail Open Excel : " + __ex.Message);
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            //MessageBox.Show("Excel file created , you can find the file c:\\csharp-Excel.xls");


            //Microsoft.Office.Interop.Excel.Application myExcelApp;
            //Microsoft.Office.Interop.Excel.Workbooks myExcelWorkbooks;
            //Microsoft.Office.Interop.Excel.Workbook myExcelWorkbook;


            //object misValue = System.Reflection.Missing.Value;

            //myExcelApp = new Microsoft.Office.Interop.Excel.ApplicationClass();
            //myExcelApp.Visible = true;
            //myExcelWorkbooks = myExcelApp.Workbooks;
            //String fileName = "C:\\book1.xls"; // set this to your file you want
            //myExcelWorkbook = myExcelWorkbooks.Open(fileName, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue, misValue);

            //Microsoft.Office.Interop.Excel.Worksheet myExcelWorksheet = (Microsoft.Office.Interop.Excel.Worksheet)myExcelWorkbook.ActiveSheet;

            //String cellFormulaAsString = myExcelWorksheet.get_Range("A2", misValue).Formula.ToString(); // this puts the formula in Cell A2 or text depending whats in it in the string.

            //myExcelWorksheetToChange.get_Range("C22", misValue).Formula = "New Value" ; // this changes the cell value in C2 to "New Value"

        }

        Excel.Range __excelFormat(Excel.Range __range)
        {
            __range.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexNone, Color.Gray.ToArgb());
            //__range.NumberFormat = "0000";
            return __range;
        }

        Excel.Range __excelFormatAlignCenter(Excel.Range __range)
        {
            __range.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexNone, Color.Gray.ToArgb());
            __range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //__range.NumberFormat = "0000";
            return __range;
        }


        Excel.Range __excelFormatNumber(Excel.Range __range)
        {
            __range.BorderAround(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexNone, Color.Gray.ToArgb());
            __range.NumberFormat = "0000";
            return __range;
        }

        private void _searchItem_CheckedChanged(object sender, EventArgs e)
        {
            _loadData();
        }

        /*

        private void _exportButton_Click(object sender, EventArgs e)
        {
            // auto export
            SaveFileDialog __objFile = new SaveFileDialog() { DefaultExt = "xml", Filter = "Excel XML Files (*.xml)|*.xml", FilterIndex = 0 };
            if (__objFile.ShowDialog() == DialogResult.OK)
            {
                string __xmlFileName = __objFile.FileName;

                // excel header 
                StringBuilder __xmlString = new StringBuilder();
                __xmlString.Append("<?xml version=\"1.0\"?>\n");
                __xmlString.Append("<?mso-application progid=\"Excel.Sheet\"?>\n");
                __xmlString.Append("<Workbook xmlns=\"urn:schemas-microsoft-com:office:spreadsheet\"\n");
                __xmlString.Append(" xmlns:o=\"urn:schemas-microsoft-com:office:office\"\n");
                __xmlString.Append(" xmlns:x=\"urn:schemas-microsoft-com:office:excel\"\n");
                __xmlString.Append(" xmlns:ss=\"urn:schemas-microsoft-com:office:spreadsheet\">\n");

                __xmlString.Append("<Styles>\n");


                __xmlString.Append("<Style ss:ID=\"s83\">\n");
                __xmlString.Append("<Alignment ss:Horizontal=\"Left\" ss:Vertical=\"Center\" ss:ShrinkToFit=\"1\"/>\n");
                __xmlString.Append("<Borders>\n");
                __xmlString.Append("<Border ss:Position=\"Bottom\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\" ss:Color=\"#C0C0C0\"/>\n");
                __xmlString.Append("<Border ss:Position=\"Left\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\" ss:Color=\"#C0C0C0\"/>\n");
                __xmlString.Append("<Border ss:Position=\"Right\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\" ss:Color=\"#C0C0C0\"/>\n");
                __xmlString.Append("<Border ss:Position=\"Top\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\" ss:Color=\"#C0C0C0\"/>\n");
                __xmlString.Append("</Borders>\n");
                __xmlString.Append("<Font ss:FontName=\"Angsana New\" ss:Size=\"12\"/>\n");
                __xmlString.Append("</Style>\n");


                __xmlString.Append("<Style ss:ID=\"s84\">");
                __xmlString.Append("<Alignment ss:Horizontal=\"Right\" ss:Vertical=\"Center\" ss:ShrinkToFit=\"1\"/>");
                __xmlString.Append("<Borders>");
                __xmlString.Append("<Border ss:Position=\"Bottom\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\" ss:Color=\"#C0C0C0\"/>");
                __xmlString.Append("<Border ss:Position=\"Left\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\" ss:Color=\"#C0C0C0\"/>");
                __xmlString.Append("<Border ss:Position=\"Right\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"  ss:Color=\"#C0C0C0\"/>");
                __xmlString.Append("<Border ss:Position=\"Top\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\" ss:Color=\"#C0C0C0\"/>");
                __xmlString.Append("</Borders>");
                __xmlString.Append("<Font ss:FontName=\"Angsana New\" ss:Size=\"12\"/>");
                __xmlString.Append(" </Style>");

                __xmlString.Append("<Style ss:ID=\"s85\">\n");
                __xmlString.Append("<Alignment ss:Horizontal=\"Center\" ss:Vertical=\"Center\" ss:ShrinkToFit=\"1\"/>\n");
                __xmlString.Append("<Borders>\n");
                __xmlString.Append("<Border ss:Position=\"Bottom\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\" ss:Color=\"#C0C0C0\"/>\n");
                __xmlString.Append("<Border ss:Position=\"Left\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\" ss:Color=\"#C0C0C0\"/>\n");
                __xmlString.Append("<Border ss:Position=\"Right\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\" ss:Color=\"#C0C0C0\"/>\n");
                __xmlString.Append("<Border ss:Position=\"Top\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\" ss:Color=\"#C0C0C0\"/>\n");
                __xmlString.Append("</Borders>\n");
                __xmlString.Append("<Font ss:FontName=\"Angsana New\" ss:Size=\"12\"/>\n");
                __xmlString.Append("</Style>\n");

                __xmlString.Append("</Styles>\n");

                // sheet 1

                #region สินค้า

                __xmlString.Append("<Worksheet ss:Name=\"" + _itemDownloadStr + "\">\n");
                __xmlString.Append("<Table>\n");

                // column length define
                __xmlString.Append("<Column ss:AutoFitWidth=\"0\" ss:Width=\"120\"/>\n");
                __xmlString.Append("<Column ss:AutoFitWidth=\"0\" ss:Width=\"120\"/>\n");
                __xmlString.Append("<Column ss:AutoFitWidth=\"0\" ss:Width=\"160\"/>\n");
                __xmlString.Append("<Column ss:AutoFitWidth=\"0\" ss:Width=\"100\"/>\n");

                // column header define
                __xmlString.Append("<Row>\n");
                __xmlString.Append("<Cell ss:Index=\"1\"  ss:StyleID=\"s85\" ><Data ss:Type=\"String\">รหัสสินค้า</Data></Cell>\n");
                __xmlString.Append("<Cell ss:Index=\"2\"  ss:StyleID=\"s85\" ><Data ss:Type=\"String\">บาร์โค้ด</Data></Cell>\n");
                __xmlString.Append("<Cell ss:Index=\"3\"  ss:StyleID=\"s85\" ><Data ss:Type=\"String\">ชื่อสินค้า</Data></Cell>\n");
                __xmlString.Append("<Cell ss:Index=\"4\"  ss:StyleID=\"s85\" ><Data ss:Type=\"String\">หน่วยนับ</Data></Cell>\n");
                //__xmlString.Append("<Cell ss:Index=\"5\"  ss:StyleID=\"s85\" ><Data ss:Type=\"String\">ราคาทั่วไป</Data></Cell>\n");
                //__xmlString.Append("<Cell ss:Index=\"6\"  ss:StyleID=\"s85\" ><Data ss:Type=\"String\">ราคาสมาชิก</Data></Cell>\n");
                __xmlString.Append("</Row>\n");

                for (int __i = 0; __i < this._gridDownloadItem._rowData.Count; __i++)
                {
                    __xmlString.Append("<Row>\n");
                    __xmlString.Append("<Cell ss:Index=\"1\"  ss:StyleID=\"s83\" ><Data ss:Type=\"String\">" + MyLib._myUtil._convertTextToXml(this._gridDownloadItem._cellGet(__i, "code").ToString()) + "</Data></Cell>\n");
                    __xmlString.Append("<Cell ss:Index=\"2\"  ss:StyleID=\"s83\" ><Data ss:Type=\"String\">" + MyLib._myUtil._convertTextToXml(this._gridDownloadItem._cellGet(__i, "barcode").ToString()) + "</Data></Cell>\n");
                    __xmlString.Append("<Cell ss:Index=\"3\"  ss:StyleID=\"s83\" ><Data ss:Type=\"String\">" + MyLib._myUtil._convertTextToXml(this._gridDownloadItem._cellGet(__i, "name1").ToString()) + "</Data></Cell>\n");
                    __xmlString.Append("<Cell ss:Index=\"4\"  ss:StyleID=\"s83\" ><Data ss:Type=\"String\">" + MyLib._myUtil._convertTextToXml(this._gridDownloadItem._cellGet(__i, "defstkunitcode").ToString()) + "</Data></Cell>\n");
                    __xmlString.Append("</Row>\n");
                }

                __xmlString.Append("</Table>\n");
                __xmlString.Append("<WorksheetOptions xmlns=\"urn:schemas-microsoft-com:office:excel\">\n");
                __xmlString.Append("<DoNotDisplayGridlines/>\n");
                __xmlString.Append("<PageSetup>\n");
                __xmlString.Append("<PageMargins x:Bottom=\"0.35\" x:Left=\"0.35\" x:Right=\"0.35\" x:Top=\"0.35\"/>\n");
                __xmlString.Append("</PageSetup>\n");
                __xmlString.Append("<Print>\n");
                __xmlString.Append("<ValidPrinterInfo/>\n");
                __xmlString.Append("<PaperSizeIndex>9</PaperSizeIndex>\n");
                __xmlString.Append("<VerticalResolution>0</VerticalResolution>\n");
                __xmlString.Append("</Print>\n");
                __xmlString.Append("</WorksheetOptions>\n");
                __xmlString.Append("</Worksheet>\n");

                #endregion

                // sheet 2
                #region PO

                __xmlString.Append("<Worksheet ss:Name=\"" + _poDownloadStr + "\">\n");
                __xmlString.Append("<Table>\n");

                // column length define
                __xmlString.Append("<Column ss:AutoFitWidth=\"0\" ss:Width=\"140\"/>\n");
                __xmlString.Append("<Column ss:AutoFitWidth=\"0\" ss:Width=\"140\"/>\n");
                __xmlString.Append("<Column ss:AutoFitWidth=\"0\" ss:Width=\"160\"/>\n");

                // column header define
                __xmlString.Append("<Row>\n");
                __xmlString.Append("<Cell ss:Index=\"1\"  ss:StyleID=\"s85\" ><Data ss:Type=\"String\">เลขที่เอกสาร</Data></Cell>\n");
                __xmlString.Append("<Cell ss:Index=\"2\"  ss:StyleID=\"s85\" ><Data ss:Type=\"String\">วันที่เอกสาร</Data></Cell>\n");
                __xmlString.Append("<Cell ss:Index=\"3\"  ss:StyleID=\"s85\" ><Data ss:Type=\"String\">กลุ่มสินค้า</Data></Cell>\n");
                //__xmlString.Append("<Cell ss:Index=\"4\"  ss:StyleID=\"s85\" ><Data ss:Type=\"String\">หน่วยนับ</Data></Cell>\n");
                //__xmlString.Append("<Cell ss:Index=\"5\"  ss:StyleID=\"s85\" ><Data ss:Type=\"String\">ราคาทั่วไป</Data></Cell>\n");
                //__xmlString.Append("<Cell ss:Index=\"6\"  ss:StyleID=\"s85\" ><Data ss:Type=\"String\">ราคาสมาชิก</Data></Cell>\n");
                __xmlString.Append("</Row>\n");

                for (int __i = 0; __i < this._gridDownloadPO._rowData.Count; __i++)
                {
                    __xmlString.Append("<Row>\n");
                    __xmlString.Append("<Cell ss:Index=\"1\"  ss:StyleID=\"s83\" ><Data ss:Type=\"String\">" + MyLib._myUtil._convertTextToXml(this._gridDownloadPO._cellGet(__i, "docno").ToString()) + "</Data></Cell>\n");
                    __xmlString.Append("<Cell ss:Index=\"2\"  ss:StyleID=\"s83\" ><Data ss:Type=\"String\">" + MyLib._myUtil._convertTextToXml(MyLib._myGlobal._convertDateToString((DateTime) this._gridDownloadPO._cellGet(__i, "docdate"),false)) + "</Data></Cell>\n");
                    __xmlString.Append("<Cell ss:Index=\"3\"  ss:StyleID=\"s83\" ><Data ss:Type=\"String\">" + MyLib._myUtil._convertTextToXml(this._gridDownloadPO._cellGet(__i, "mydescription").ToString()) + "</Data></Cell>\n");
                    __xmlString.Append("</Row>\n");
                }

                __xmlString.Append("</Table>\n");
                __xmlString.Append("<WorksheetOptions xmlns=\"urn:schemas-microsoft-com:office:excel\">\n");
                __xmlString.Append("<DoNotDisplayGridlines/>\n");
                __xmlString.Append("<PageSetup>\n");
                __xmlString.Append("<PageMargins x:Bottom=\"0.35\" x:Left=\"0.35\" x:Right=\"0.35\" x:Top=\"0.35\"/>\n");
                __xmlString.Append("</PageSetup>\n");
                __xmlString.Append("<Print>\n");
                __xmlString.Append("<ValidPrinterInfo/>\n");
                __xmlString.Append("<PaperSizeIndex>9</PaperSizeIndex>\n");
                __xmlString.Append("<VerticalResolution>0</VerticalResolution>\n");
                __xmlString.Append("</Print>\n");
                __xmlString.Append("</WorksheetOptions>\n");
                __xmlString.Append("</Worksheet>\n");

                #endregion

                // sheet 3
                #region SO

                __xmlString.Append("<Worksheet ss:Name=\"ใบสั่งขาย สั่งจองสินค้าจาก SCG\">\n");
                __xmlString.Append("<Table>\n");

                // column length define
                __xmlString.Append("<Column ss:AutoFitWidth=\"0\" ss:Width=\"140\"/>\n");
                __xmlString.Append("<Column ss:AutoFitWidth=\"0\" ss:Width=\"140\"/>\n");
                __xmlString.Append("<Column ss:AutoFitWidth=\"0\" ss:Width=\"150\"/>\n");
                __xmlString.Append("<Column ss:AutoFitWidth=\"0\" ss:Width=\"150\"/>\n");

                // column header define
                __xmlString.Append("<Row>\n");
                __xmlString.Append("<Cell ss:Index=\"1\"  ss:StyleID=\"s85\" ><Data ss:Type=\"String\">เลขที่เอกสาร</Data></Cell>\n");
                __xmlString.Append("<Cell ss:Index=\"2\"  ss:StyleID=\"s85\" ><Data ss:Type=\"String\">วันที่เอกสาร</Data></Cell>\n");
                __xmlString.Append("<Cell ss:Index=\"3\"  ss:StyleID=\"s85\" ><Data ss:Type=\"String\">ผู้รับสินค้า</Data></Cell>\n");
                __xmlString.Append("<Cell ss:Index=\"4\"  ss:StyleID=\"s85\" ><Data ss:Type=\"String\">หมายเหตุ</Data></Cell>\n");
                //__xmlString.Append("<Cell ss:Index=\"5\"  ss:StyleID=\"s85\" ><Data ss:Type=\"String\">ราคาทั่วไป</Data></Cell>\n");
                //__xmlString.Append("<Cell ss:Index=\"6\"  ss:StyleID=\"s85\" ><Data ss:Type=\"String\">ราคาสมาชิก</Data></Cell>\n");
                __xmlString.Append("</Row>\n");

                for (int __i = 0; __i < this._gridDownloadSO._rowData.Count; __i++)
                {
                    __xmlString.Append("<Row>\n");
                    __xmlString.Append("<Cell ss:Index=\"1\"  ss:StyleID=\"s83\" ><Data ss:Type=\"String\">" + MyLib._myUtil._convertTextToXml(this._gridDownloadSO._cellGet(__i, "docno").ToString()) + "</Data></Cell>\n");
                    __xmlString.Append("<Cell ss:Index=\"2\"  ss:StyleID=\"s83\" ><Data ss:Type=\"String\">" + MyLib._myUtil._convertTextToXml(MyLib._myGlobal._convertDateToString((DateTime)this._gridDownloadSO._cellGet(__i, "docdate"), false)) + "</Data></Cell>\n");
                    __xmlString.Append("<Cell ss:Index=\"3\"  ss:StyleID=\"s83\" ><Data ss:Type=\"String\">" + MyLib._myUtil._convertTextToXml(this._gridDownloadSO._cellGet(__i, "ownreceive").ToString()) + "</Data></Cell>\n");
                    __xmlString.Append("<Cell ss:Index=\"4\"  ss:StyleID=\"s83\" ><Data ss:Type=\"String\">" + MyLib._myUtil._convertTextToXml(this._gridDownloadSO._cellGet(__i, "mydescription").ToString()) + "</Data></Cell>\n");
                    __xmlString.Append("</Row>\n");
                }

                __xmlString.Append("</Table>\n");
                __xmlString.Append("<WorksheetOptions xmlns=\"urn:schemas-microsoft-com:office:excel\">\n");
                __xmlString.Append("<DoNotDisplayGridlines/>\n");
                __xmlString.Append("<PageSetup>\n");
                __xmlString.Append("<PageMargins x:Bottom=\"0.35\" x:Left=\"0.35\" x:Right=\"0.35\" x:Top=\"0.35\"/>\n");
                __xmlString.Append("</PageSetup>\n");
                __xmlString.Append("<Print>\n");
                __xmlString.Append("<ValidPrinterInfo/>\n");
                __xmlString.Append("<PaperSizeIndex>9</PaperSizeIndex>\n");
                __xmlString.Append("<VerticalResolution>0</VerticalResolution>\n");
                __xmlString.Append("</Print>\n");
                __xmlString.Append("</WorksheetOptions>\n");
                __xmlString.Append("</Worksheet>\n");

                #endregion


                __xmlString.Append("</Workbook>\n");


                File.WriteAllText(__xmlFileName, __xmlString.ToString());
                FileStream fs = File.OpenRead(__xmlFileName);
                byte[] buffer = new byte[fs.Length];
                fs.Read(buffer, 0, buffer.Length);

                fs.Close();


                // openIT
                try
                {
                    System.Diagnostics.Process proc = new System.Diagnostics.Process();
                    proc.EnableRaisingEvents = false;
                    proc.StartInfo.FileName = "excel";
                    proc.StartInfo.Arguments = __xmlFileName;
                    proc.Start();
                    //proc.WaitForExit();
                }
                catch (Exception __ex)
                {
                    MessageBox.Show("Fail Open Excel : " + __ex.Message);
                }
            }            
        }

        */
    }
}
