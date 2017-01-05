using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.IO;
using System.Xml.Serialization;
using System.Windows.Forms;
using System.Collections;

namespace SMLERPReportTool
{
    public static class _global
    {
        /// <summary>
        /// return true is print
        /// </summary>
        /// <param name="docTypeCode"></param>
        /// <param name="docNo"></param>
        /// <param name="transFlag"></param>
        /// <returns></returns>
        public static bool _printForm(string docTypeCode, string docNo, string transFlag)
        {
            return _printForm(docTypeCode, docNo, transFlag, false);
        }

        public static bool _printForm(string docTypeCode, string docNo, string transFlag, bool showPrintOption)
        {
            string[] __docNoSplite = docNo.Split('-');
            if (__docNoSplite.Length > 0 && docTypeCode.Trim().Length == 0)
            {
                docTypeCode = __docNoSplite[0].ToString();
            }
            if (docTypeCode.Trim().Length > 0)
            {
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                string __query = "select " + _g.d.erp_doc_format._form_code + " from " + _g.d.erp_doc_format._table + " where " + MyLib._myGlobal._addUpper(_g.d.erp_doc_format._code) + "=\'" + docTypeCode.ToUpper() + "\'";
                DataTable __dt = __myFrameWork._queryShort(__query).Tables[0];
                if (__dt.Rows.Count > 0)
                {
                    string __formCode = __dt.Rows[0][_g.d.erp_doc_format._form_code].ToString().Trim();
                    if (__formCode.Length > 0)
                    {
                        // toe fix per webservcie per database 
                        string __getTempFileByServer = MyLib._myGlobal._getFirstWebServiceServer.Replace(".", "_").Replace(":", "__") + "-" + MyLib._myGlobal._databaseName + "-";

                        // get firstline
                        // check xml 
                        string __currentConfigFileName = __getTempFileByServer + "configPrinterScreen" + transFlag + ".xml";
                        string __path = Path.GetTempPath() + "\\" + __currentConfigFileName.ToLower();
                        _PrintConfig __config = new _PrintConfig();
                        System.Windows.Forms.DialogResult __formResult = System.Windows.Forms.DialogResult.OK;
                        System.Drawing.Printing.PrintRange __printRangeOption = System.Drawing.Printing.PrintRange.AllPages;
                        int[] __printPageRange = null;
                        bool __includeDocSeries = false;
                        System.Drawing.Printing.PrintRange __seriesRangeOption = System.Drawing.Printing.PrintRange.AllPages;
                        int[] __seriesRange = null;

                        //toe ทดสอบดึงจาก temp ตัวใหม่
                        try
                        {
                            TextReader readFile = new StreamReader(__path);
                            XmlSerializer __xsLoad = new XmlSerializer(typeof(_PrintConfig));
                            __config = (_PrintConfig)__xsLoad.Deserialize(readFile);
                            readFile.Close();
                        }
                        catch
                        {
                            // ไม่ได้ไปดึงของเก่า
                            try
                            {
                                __currentConfigFileName = "configPrinterScreen" + transFlag + ".xml";
                                __path = Path.GetTempPath() + "\\" + __currentConfigFileName.ToLower();

                                TextReader readFile = new StreamReader(__path);
                                XmlSerializer __xsLoad = new XmlSerializer(typeof(_PrintConfig));
                                __config = (_PrintConfig)__xsLoad.Deserialize(readFile);
                                readFile.Close();
                            }
                            catch (Exception ex)
                            {
                            }

                        }


                        if ((__config.ScreenCode == null) || (__config.ShowAgain == false) || (showPrintOption == true))
                        {
                            _formPrintOption __printOption = new _formPrintOption(transFlag, __formCode);
                            if (__config.ScreenCode != null)
                            {
                                __printOption._showagainCheck.Checked = __config.ShowAgain;
                                __printOption._previewPrintCheck.Checked = __config.isPreview;
                                __printOption._printCheck.Checked = __config.isPrint;

                                for (int __i = 0; __i < __config.FormCode.Count; __i++)
                                {
                                    __printOption._myScreen1._setCheckBox((string)__config.FormCode[__i], "true");
                                }

                                // print multi form
                                //__printOption._printVatCheck.Checked = __config.printAttactForm;
                                //__printOption._formCombo.SelectedValue = __config.FormCode;

                                __printOption._setPrintNameSelectIndex(__config.PrinterName);
                            }

                            __formResult = __printOption.ShowDialog(MyLib._myGlobal._mainForm);
                            __config.isPreview = __printOption._previewPrintCheck.Checked;
                            __config.isPrint = __printOption._printCheck.Checked;
                            __printRangeOption = __printOption._printRange;
                            __printPageRange = __printOption._printPageRange;
                            __includeDocSeries = __printOption._includeDocSeriesCheckbox.Checked;
                            __seriesRangeOption = __printOption._printSeriesRangeOption;
                            __seriesRange = __printOption._printSeriesRange;

                            // clear old config
                            __config.FormCode.Clear();
                            for (int __i = 0; __i < __printOption.__formCodeList.Count; __i++)
                            {

                                if (__printOption._myScreen1._getDataStr(__printOption.__formCodeList[__i]).Equals("1"))
                                {
                                    __config.FormCode.Add(__printOption.__formCodeList[__i]);
                                }

                            }

                            //__config.printAttactForm = __printOption._printVatCheck.Checked;

                            __config.PrinterName = __printOption._printerCombo.Text.ToString();
                            // jead แก้ชั่วคราว
                            // toe remark print multi form
                            //__config.FormCode = (__printOption._formCombo.SelectedValue == null) ? "" : __printOption._formCombo.SelectedValue.ToString();

                            __printOption.Close();
                        }

                        if (__formResult == System.Windows.Forms.DialogResult.OK && (__config.isPrint == true || __config.isPreview == true || __config.printAttactForm))
                        {
                            // toe print multi field
                            /*
                            if (__config.FormCode.Length > 0)
                            {
                                SMLReport._formReport._formPrint __form = new SMLReport._formReport._formPrint(__config.FormCode);

                                __form.PreviewPrintDialog = __config.isPreview;
                                __form.PrinterName = __config.PrinterName;

                                __form._conditon.Add(new SMLReport._formReport._formPrint._conditionClass("doc_no", docNo));
                                __form._conditon.Add(new SMLReport._formReport._formPrint._conditionClass("trans_flag", transFlag));
                                __form._query();
                            }
                            if (__config.printAttactForm)
                            {
                                // หัก ณ. ที่จ่าย
                                SMLReport._formReport._formPrint __form = new SMLReport._formReport._formPrint("WHT");

                                __form.PreviewPrintDialog = __config.isPreview;
                                __form.PrinterName = __config.PrinterName;

                                __form._conditon.Add(new SMLReport._formReport._formPrint._conditionClass("doc_no", docNo));
                                __form._conditon.Add(new SMLReport._formReport._formPrint._conditionClass("trans_flag", transFlag));
                                __form._query();
                            }
                             * */
                            if (__config.FormCode.Count > 0)
                            {
                                for (int __i = 0; __i < __config.FormCode.Count; __i++)
                                {
                                    SMLReport._formReport._formPrint __form = new SMLReport._formReport._formPrint((string)__config.FormCode[__i]);

                                    __form._printRangeType = __printRangeOption;
                                    __form._includeDocSeries = __includeDocSeries;
                                    if (__printRangeOption == System.Drawing.Printing.PrintRange.SomePages)
                                    {
                                        __form._printRange = new List<int>();
                                        foreach (int _pageForPrint in __printPageRange)
                                        {
                                            __form._printRange.Add(_pageForPrint);
                                        }

                                        //__form._printRange = __printPageRange;
                                        if (__includeDocSeries == false || __seriesRangeOption == System.Drawing.Printing.PrintRange.SomePages)
                                        {
                                            __form._printSeriesOption = __seriesRangeOption;
                                            if (__seriesRangeOption == System.Drawing.Printing.PrintRange.SomePages)
                                            {
                                                foreach (int _seriesForPrint in __seriesRange)
                                                {
                                                    __form._printSeriesRange.Add(_seriesForPrint);
                                                }
                                            }
                                        }
                                    }

                                    __form.PreviewPrintDialog = __config.isPreview;
                                    __form.PrinterName = __config.PrinterName;

                                    __form._conditon.Add(new SMLReport._formReport._formPrint._conditionClass("doc_no", docNo));
                                    __form._conditon.Add(new SMLReport._formReport._formPrint._conditionClass("trans_flag", transFlag));
                                    __form._query();
                                    __form.Dispose();

                                }
                            }

                            __dt.Dispose();
                            return true;

                        }
                    }
                    else
                    {
                        //MessageBox.Show("กรุณากำหนด แบบฟอร์มที่ต้องการพิมพ์ก่อน");
                    }
                }
                else
                {
                    //MessageBox.Show("กรุณากำหนด แบบฟอร์มที่ต้องการพิมพ์ก่อน");
                }
                __dt.Dispose();
            }

            return false;
        }

        public static void _printForm(_reportEnum __reportEnum)
        {

        }

        /// <summary>
        /// พิมพ์ฟอร์มเป็นช่วง เอกสาร
        /// </summary>
        /// <param name="screen_code"></param>
        /// <param name="ConditionList"></param>
        /// <param name="showPrintOption"></param>
        /// <param name="defaultFormCode"></param>
        public static Boolean _printRangeForm(string screen_code, ArrayList ConditionList, bool showPrintOption, string defaultFormCode)
        {
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();

            StringBuilder __getDefaultFormCode = new StringBuilder();
            if (defaultFormCode.Length > 0)
            {
                string[] __formCodeSplit = defaultFormCode.Split(',');
                foreach (string __getCode in __formCodeSplit)
                {
                    if (__getDefaultFormCode.Length > 0)
                    {
                        __getDefaultFormCode.Append(",");
                    }
                    __getDefaultFormCode.Append("" + __getCode.Trim() + "");
                }
            }

            string __query = "select " + _g.d.erp_doc_format._form_code + " from " + _g.d.erp_doc_format._table + " where " + MyLib._myGlobal._addUpper(_g.d.erp_doc_format._code) + "=\'" + screen_code.ToUpper() + "\'";


            DataTable __dt = __myFrameWork._queryShort(__query).Tables[0];
            string __formCode = "";
            if (__dt.Rows.Count > 0)
            {
                __formCode = __dt.Rows[0][_g.d.erp_doc_format._form_code].ToString().Trim();
            }
            else
            {
                //MessageBox.Show("กรุณากำหนด แบบฟอร์มที่ต้องการพิมพ์ก่อน");
            }

            //__formCode = __dt.Rows[0][_g.d.erp_doc_format._form_code].ToString().Trim();

            if (__getDefaultFormCode.Length > 0)
            {
                __formCode += ((__formCode.Length > 0) ? "," : "") + __getDefaultFormCode.ToString();
            }

            if (__formCode.Length > 0)
            {
                // toe fix per webservcie per database 
                string __getTempFileByServer = MyLib._myGlobal._getFirstWebServiceServer.Replace(".", "_").Replace(":", "__") + "-" + MyLib._myGlobal._databaseName + "-";

                string __currentConfigFileName = __getTempFileByServer + "configPrinterScreen" + screen_code + ".xml";
                string __path = Path.GetTempPath() + "\\" + __currentConfigFileName.ToLower();
                _PrintConfig __config = new _PrintConfig();
                System.Windows.Forms.DialogResult __formResult = System.Windows.Forms.DialogResult.OK;
                System.Drawing.Printing.PrintRange __printRangeOption = System.Drawing.Printing.PrintRange.AllPages;
                int[] __printPageRange = null;
                bool __includeDocSeries = false;
                System.Drawing.Printing.PrintRange __seriesRangeOption = System.Drawing.Printing.PrintRange.AllPages;
                int[] __seriesRange = null;

                try
                {
                    TextReader readFile = new StreamReader(__path);
                    XmlSerializer __xsLoad = new XmlSerializer(typeof(_PrintConfig));
                    __config = (_PrintConfig)__xsLoad.Deserialize(readFile);
                    readFile.Close();
                }
                catch
                {
                    try
                    {
                        __currentConfigFileName = "configPrinterScreen" + screen_code + ".xml";
                        __path = Path.GetTempPath() + "\\" + __currentConfigFileName.ToLower();

                        TextReader readFile = new StreamReader(__path);
                        XmlSerializer __xsLoad = new XmlSerializer(typeof(_PrintConfig));
                        __config = (_PrintConfig)__xsLoad.Deserialize(readFile);
                        readFile.Close();
                    }
                    catch (Exception ex2)
                    {
                    }
                }

                if ((__config.ScreenCode == null) || (__config.ShowAgain == false) || (showPrintOption == true))
                {
                    _formPrintOption __printOption = new _formPrintOption(screen_code, __formCode);
                    __printOption._printRangeGroupbox.Enabled = false;

                    if (__config.ScreenCode != null)
                    {
                        __printOption._showagainCheck.Checked = __config.ShowAgain;
                        __printOption._previewPrintCheck.Checked = __config.isPreview;
                        __printOption._printCheck.Checked = __config.isPrint;
                        for (int __i = 0; __i < __config.FormCode.Count; __i++)
                        {
                            __printOption._setCheckedPrintFormName((string)__config.FormCode[__i]);
                        }

                        __printOption._setPrintNameSelectIndex(__config.PrinterName);
                    }

                    __formResult = __printOption.ShowDialog(MyLib._myGlobal._mainForm);

                    __config.isPreview = __printOption._previewPrintCheck.Checked;
                    __config.isPrint = __printOption._printCheck.Checked;
                    __printRangeOption = __printOption._printRange;
                    __printPageRange = __printOption._printPageRange;
                    __includeDocSeries = __printOption._includeDocSeriesCheckbox.Checked;
                    __seriesRangeOption = __printOption._printSeriesRangeOption;
                    __seriesRange = __printOption._printSeriesRange;
                    __config.PrinterName = __printOption._printerCombo.Text.ToString();


                    // get last formCode
                    __config.FormCode.Clear();

                    for (int __i = 0; __i < __printOption.__formCodeList.Count; __i++)
                    {

                        if (__printOption._myScreen1._getDataStr(__printOption.__formCodeList[__i]).Equals("1"))
                        {
                            __config.FormCode.Add(__printOption.__formCodeList[__i]);
                        }

                    }
                    __printOption.Close();

                }

                if (__formResult == System.Windows.Forms.DialogResult.OK && (__config.isPrint == true || __config.isPreview == true || __config.printAttactForm))
                {
                    if (__config.FormCode.Count > 0)
                    {
                        for (int __i = 0; __i < __config.FormCode.Count; __i++)
                        {
                            SMLReport._formReport._formPrint __form = new SMLReport._formReport._formPrint((string)__config.FormCode[__i]);

                            __form._printRangeType = __printRangeOption;
                            __form._includeDocSeries = __includeDocSeries;
                            if (__printRangeOption == System.Drawing.Printing.PrintRange.SomePages)
                            {
                                __form._printRange = new List<int>();
                                foreach (int _pageForPrint in __printPageRange)
                                {
                                    __form._printRange.Add(_pageForPrint);
                                }

                                //__form._printRange = __printPageRange;
                                if (__includeDocSeries == false || __seriesRangeOption == System.Drawing.Printing.PrintRange.SomePages)
                                {
                                    __form._printSeriesOption = __seriesRangeOption;
                                    if (__seriesRangeOption == System.Drawing.Printing.PrintRange.SomePages)
                                    {
                                        foreach (int _seriesForPrint in __seriesRange)
                                        {
                                            __form._printSeriesRange.Add(_seriesForPrint);
                                        }
                                    }
                                }

                            }

                            __form.PreviewPrintDialog = __config.isPreview;
                            __form.PrinterName = __config.PrinterName;

                            ArrayList __sendConditionPack = new ArrayList();

                            // set arrayList conditionclass
                            for (int __loop = 0; __loop < ConditionList.Count; __loop++) // (_ReportToolCondition __condition in ConditionList)
                            {
                                SMLERPReportTool._ReportToolCondition[] __getConditionArr = (SMLERPReportTool._ReportToolCondition[])ConditionList[__loop];
                                List<SMLReport._formReport._formPrint._conditionClass> __conditionDoc = new List<SMLReport._formReport._formPrint._conditionClass>();

                                foreach (SMLERPReportTool._ReportToolCondition __getCond in __getConditionArr)
                                {

                                    __conditionDoc.Add(new SMLReport._formReport._formPrint._conditionClass(__getCond._fieldName, __getCond._value));
                                }

                                __sendConditionPack.Add(__conditionDoc);
                            }

                            __form._query(__sendConditionPack);

                        }
                    }
                    return true;
                }

            }
            else
            {
                //MessageBox.Show("กรุณากำหนด แบบฟอร์มที่ต้องการพิมพ์ก่อน");
            }

            return false;
        }

        public static bool _printForm(string screen_code, _ReportToolCondition[] __arrCondition, bool showPrintOption)
        {
            return _printForm(screen_code, __arrCondition, showPrintOption, "");
        }

        public static bool _printForm(string screen_code, _ReportToolCondition[] __arrCondition, bool showPrintOption, string defaultFormCode)
        {
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();

            //StringBuilder __getDefaultFormCode = new StringBuilder();

            List<string> __formCodeList = new List<string>();
            if (defaultFormCode.Length > 0)
            {
                string[] __formCodeSplit = defaultFormCode.Split(',');
                foreach (string __getCode in __formCodeSplit)
                {
                    //if (__getDefaultFormCode.Length > 0)
                    //{
                    //    __getDefaultFormCode.Append(",");
                    //}
                    //__getDefaultFormCode.Append("" + __getCode.Trim() + "");
                    if (__formCodeList.Contains(__getCode.Trim()) == false)
                        __formCodeList.Add(__getCode.Trim());
                }
            }

            string __query = "select " + _g.d.erp_doc_format._form_code + " from " + _g.d.erp_doc_format._table + " where " + MyLib._myGlobal._addUpper(_g.d.erp_doc_format._code) + "=\'" + screen_code.ToUpper() + "\'";


            DataTable __dt = __myFrameWork._queryShort(__query).Tables[0];
            string __formCodeTemp = "";
            if (__dt.Rows.Count > 0)
            {
                __formCodeTemp = __dt.Rows[0][_g.d.erp_doc_format._form_code].ToString().Trim();
                string[] __formCodeTempSplit = __formCodeTemp.Split(',');
                foreach (string formCodeStr in __formCodeTempSplit)
                {
                    if (__formCodeList.Contains(formCodeStr.Trim()) == false)
                    {
                        __formCodeList.Add(formCodeStr.Trim());

                    }
                }

            }
            else
            {
                //MessageBox.Show("กรุณากำหนด แบบฟอร์มที่ต้องการพิมพ์ก่อน");
            }

            //__formCode = __dt.Rows[0][_g.d.erp_doc_format._form_code].ToString().Trim();

            //if (__getDefaultFormCode.Length > 0 && Array.IndexOf(__formCode.Split(','), __getDefaultFormCode) == -1)
            //{
            //    __formCode += ((__formCode.Length > 0) ? "," : "") + __getDefaultFormCode.ToString();
            //}

            string __formCode = String.Join(",", __formCodeList.ToArray());

            if (__formCode.Length > 0)
            {
                // toe fix per webservcie per database 
                string __getTempFileByServer = MyLib._myGlobal._getFirstWebServiceServer.Replace(".", "_").Replace(":", "__") + "-" + MyLib._myGlobal._databaseName + "-";

                string __currentConfigFileName = __getTempFileByServer + "configPrinterScreen" + screen_code + ".xml";
                string __path = Path.GetTempPath() + "\\" + __currentConfigFileName.ToLower();
                _PrintConfig __config = new _PrintConfig();
                System.Windows.Forms.DialogResult __formResult = System.Windows.Forms.DialogResult.OK;
                System.Drawing.Printing.PrintRange __printRangeOption = System.Drawing.Printing.PrintRange.AllPages;
                int[] __printPageRange = null;
                bool __includeDocSeries = false;
                System.Drawing.Printing.PrintRange __seriesRangeOption = System.Drawing.Printing.PrintRange.AllPages;
                int[] __seriesRange = null;

                try
                {
                    TextReader readFile = new StreamReader(__path);
                    XmlSerializer __xsLoad = new XmlSerializer(typeof(_PrintConfig));
                    __config = (_PrintConfig)__xsLoad.Deserialize(readFile);
                    readFile.Close();
                }
                catch
                {
                    try
                    {
                        __currentConfigFileName = "configPrinterScreen" + screen_code + ".xml";
                        __path = Path.GetTempPath() + "\\" + __currentConfigFileName.ToLower();

                        TextReader readFile = new StreamReader(__path);
                        XmlSerializer __xsLoad = new XmlSerializer(typeof(_PrintConfig));
                        __config = (_PrintConfig)__xsLoad.Deserialize(readFile);
                        readFile.Close();
                    }
                    catch (Exception ex)
                    {
                    }
                }

                if ((__config.ScreenCode == null) || (__config.ShowAgain == false) || (showPrintOption == true))
                {
                    _formPrintOption __printOption = new _formPrintOption(screen_code, __formCode);

                    //__printOption._printVatCheck.Visible = false;

                    if (__config.ScreenCode != null)
                    {
                        __printOption._showagainCheck.Checked = __config.ShowAgain;
                        __printOption._previewPrintCheck.Checked = __config.isPreview;
                        __printOption._printCheck.Checked = __config.isPrint;

                        //__printOption._printVatCheck.Checked = __config.printAttactForm;
                        //__printOption._formCombo.SelectedValue = __config.FormCode;
                        for (int __i = 0; __i < __config.FormCode.Count; __i++)
                        {
                            __printOption._setCheckedPrintFormName((string)__config.FormCode[__i]);
                        }

                        __printOption._setPrintNameSelectIndex(__config.PrinterName);
                    }

                    __formResult = __printOption.ShowDialog(MyLib._myGlobal._mainForm);

                    /*
                    if (__printOption._formCombo.SelectedValue != null)
                    {
                        __config.isPreview = __printOption._previewPrintCheck.Checked;
                        __config.isPrint = __printOption._printCheck.Checked;
                        __config.printAttactForm = __printOption._printVatCheck.Checked;
                        __config.PrinterName = __printOption._printerCombo.Text.ToString();
                        __config.FormCode = __printOption._formCombo.SelectedValue.ToString();
                    }
                    */
                    __config.isPreview = __printOption._previewPrintCheck.Checked;
                    __config.isPrint = __printOption._printCheck.Checked;
                    __printRangeOption = __printOption._printRange;
                    __printPageRange = __printOption._printPageRange;
                    __includeDocSeries = __printOption._includeDocSeriesCheckbox.Checked;
                    __seriesRangeOption = __printOption._printSeriesRangeOption;
                    __seriesRange = __printOption._printSeriesRange;
                    __config.PrinterName = __printOption._printerCombo.Text.ToString();

                    //__config.printAttactForm = __printOption._printVatCheck.Checked;
                    //__config.FormCode = __printOption._formCombo.SelectedValue.ToString();

                    // get last formCode
                    __config.FormCode.Clear();

                    for (int __i = 0; __i < __printOption.__formCodeList.Count; __i++)
                    {

                        if (__printOption._myScreen1._getDataStr(__printOption.__formCodeList[__i]).Equals("1"))
                        {
                            __config.FormCode.Add(__printOption.__formCodeList[__i]);
                        }

                    }
                    __printOption.Close();
                }

                if (__formResult == System.Windows.Forms.DialogResult.OK && (__config.isPrint == true || __config.isPreview == true || __config.printAttactForm))
                {
                    /*
                    if (__config.FormCode.Length > 0)
                    {
                        SMLReport._formReport._formPrint __form = new SMLReport._formReport._formPrint(__config.FormCode);

                        __form.PreviewPrintDialog = __config.isPreview;
                        __form.PrinterName = __config.PrinterName;

                        foreach (_ReportToolCondition __condition in __arrCondition)
                        {
                            __form._conditon.Add(new SMLReport._formReport._formPrint._conditionClass(__condition._fieldName, __condition._value));
                        }
                        __form._query();
                    }
                     */
                    if (__config.FormCode.Count > 0)
                    {
                        for (int __i = 0; __i < __config.FormCode.Count; __i++)
                        {
                            SMLReport._formReport._formPrint __form = new SMLReport._formReport._formPrint((string)__config.FormCode[__i]);

                            __form._printRangeType = __printRangeOption;
                            __form._includeDocSeries = __includeDocSeries;
                            if (__printRangeOption == System.Drawing.Printing.PrintRange.SomePages)
                            {
                                __form._printRange = new List<int>();
                                foreach (int _pageForPrint in __printPageRange)
                                {
                                    __form._printRange.Add(_pageForPrint);
                                }

                                //__form._printRange = __printPageRange;
                                if (__includeDocSeries == false || __seriesRangeOption == System.Drawing.Printing.PrintRange.SomePages)
                                {
                                    __form._printSeriesOption = __seriesRangeOption;
                                    if (__seriesRangeOption == System.Drawing.Printing.PrintRange.SomePages)
                                    {
                                        foreach (int _seriesForPrint in __seriesRange)
                                        {
                                            __form._printSeriesRange.Add(_seriesForPrint);
                                        }
                                    }
                                }

                            }

                            __form.PreviewPrintDialog = __config.isPreview;
                            __form.PrinterName = __config.PrinterName;

                            foreach (_ReportToolCondition __condition in __arrCondition)
                            {
                                __form._conditon.Add(new SMLReport._formReport._formPrint._conditionClass(__condition._fieldName, __condition._value));
                            }
                            __form._query();

                        }
                    }
                    return true;
                }

            }
            else
            {
                //MessageBox.Show("กรุณากำหนด แบบฟอร์มที่ต้องการพิมพ์ก่อน");
            }

            return false;
        }

        public static _reportTypeEnum _reportType(_reportEnum mode)
        {
            string __modeCompare = "**" + mode.ToString();
            if (__modeCompare.IndexOf("**ขาย_") != -1) return _reportTypeEnum.ขาย_ลูกหนี้;
            else
                if (__modeCompare.IndexOf("**ซื้อ_") != -1) return _reportTypeEnum.ซื้อ_เจ้าหนี้;
            else
                    if (__modeCompare.IndexOf("**สินค้า_") != -1) return _reportTypeEnum.สินค้า;
            else
                        if (__modeCompare.IndexOf("**เจ้าหนี้_") != -1) return _reportTypeEnum.เจ้าหนี้;
            else
                            if (__modeCompare.IndexOf("**ลูกหนี้_") != -1) return _reportTypeEnum.ลูกหนี้;
            else
                                if (__modeCompare.IndexOf("**เช็ค_") != -1) return _reportTypeEnum.เช็ค;
            else
                                    if (__modeCompare.IndexOf("**บัตรเครดิต_") != -1) return _reportTypeEnum.บัตรเครดิต;
            else
                                        if (__modeCompare.IndexOf("**เงินสดธนาคาร_รายได้") != -1) return _reportTypeEnum.ลูกหนี้;
            else
                                            if (__modeCompare.IndexOf("**เงินสดธนาคาร_รายจ่าย") != -1) return _reportTypeEnum.เจ้าหนี้;
            else
                                                if (__modeCompare.IndexOf("**เงินสดธนาคาร_") != -1) return _reportTypeEnum.เงินสดธนาคาร;
            else
                                                    if (__modeCompare.IndexOf("**ธนาคาร_") != -1) return _reportTypeEnum.ธนาคาร;
            else
                                                        if (mode == _reportEnum.Serial_number) return _reportTypeEnum.สินค้า_Serial;
            else
                                                            if (mode == _reportEnum.Item_Balance_now_Only_Serial) return _reportTypeEnum.สินค้า;
            return _reportTypeEnum.ว่าง;
        }
    }

    public enum _reportTypeEnum
    {
        ว่าง,
        สินค้า,
        ขาย_ลูกหนี้,
        ซื้อ_เจ้าหนี้,
        เจ้าหนี้,
        ลูกหนี้,
        เช็ค,
        บัตรเครดิต,
        เงินสดธนาคาร,
        สินค้า_Serial,
        ธนาคาร
    }

    public enum _reportVatNumberType
    {
        ปรกติ,
        แสดงตัวเลขแบบรวมในทั้งหมด,
        แสดงตัวเลขแบบแยกนอกทั้งหมด
    }

    public enum _reportEnum
    {
        ขาย_,
        ขาย_รับเงินล่วงหน้า,
        ขาย_รับเงินล่วงหน้า_ยกเลิก,
        ขาย_รับเงินล่วงหน้า_คืน,
        ขาย_รับเงินล่วงหน้า_คืน_ยกเลิก,
        ขาย_รับเงินล่วงหน้า_รายงานยอดคงเหลือ,
        ขาย_รับเงินล่วงหน้า_รายงานตัดเงิน,
        ขาย_รับเงินมัดจำ,
        ขาย_รับเงินมัดจำ_ยกเลิก,
        ขาย_รับเงินมัดจำ_คืน,
        ขาย_รับเงินมัดจำ_คืน_ยกเลิก,
        ขาย_รับเงินมัดจำ_รายงานยอดคงเหลือ,
        ขาย_รับเงินมัดจำ_รายงานตัดเงิน,
        ขาย_สั่งขาย,
        ขาย_สั่งขาย_ยกเลิก,
        ขาย_สั่งขาย_อนุมัติ,
        ขาย_สั่งขาย_สถานะ,
        ขาย_ขายสินค้าและบริการ,
        ขาย_ขายสินค้าและบริการ_ลดหนี้_เพิ่มหนี้,
        ขาย_ขายสินค้าและบริการ_รายงานการออกใบกำกับภาษีอย่างเต็ม,
        ขาย_ขายสินค้าและบริการ_ยกเลิก,
        ขาย_เพิ่มหนี้สินค้าราคาผิด,
        ขาย_เพิ่มหนี้สินค้าราคาผิด_ยกเลิก,
        ขาย_รับคืนสินค้า,
        ขาย_รับคืนสินค้า_ยกเลิก,
        ขาย_สั่งจองและสั่งซื้อสินค้า,
        ขาย_สั่งจองและสั่งซื้อสินค้า_ยกเลิก,
        ขาย_สั่งจองและสั่งซื้อสินค้า_อนุมัติ,
        ขาย_สั่งจองและสั่งซื้อสินค้า_สถานะ,
        ขาย_รายงานใบเสนอซื้อสินค้า,
        ขาย_รายงานลดหนี้สินค้าราคาผิด,
        ขาย_เสนอราคา,
        ขาย_เสนอราคา_ยกเลิก,
        ขาย_เสนอราคา_อนุมัติ,
        ขาย_เสนอราคา_สถานะ,
        ขาย_รายงานขายสินค้าแยกตามสินค้า,
        ซื้อ_ซื้อสินค้าและบริการ,
        ซื้อ_ซื้อสินค้าและบริการ_ยกเลิก,
        ซื้อ_จ่ายเงินล่วงหน้า,
        ซื้อ_จ่ายเงินล่วงหน้า_ยกเลิก,
        ซื้อ_จ่ายเงินล่วงหน้า_รับคืน,
        ซื้อ_จ่ายเงินล่วงหน้า_รับคืน_ยกเลิก,
        ซื้อ_จ่ายเงินมัดจำ,
        ซื้อ_จ่ายเงินมัดจำ_ยกเลิก,
        ซื้อ_จ่ายเงินมัดจำ_รับคืน,
        ซื้อ_จ่ายเงินมัดจำ_รับคืน_ยกเลิก,
        ซื้อ_รายงานยอดคงเหลือเงินจ่ายล่วงหน้า,
        ซื้อ_รายงานตัดเงินจ่ายล่วงหน้า,
        ซื้อ_รายงานยอดคงเหลือเงินมัดจำจ่าย,
        ซื้อ_รายงานตัดเงินมัดจำจ่าย,
        ซื้อ_รายงานซื้อสินค้าแยกตามสินค้า,
        ซื้อ_รายงานใบสั่งซื้อ,
        ซื้อ_รายงานอนุมัติใบสั่งซื้อ,
        ซื้อ_รายงานยกเลิกใบสั่งซื้อ,
        ซื้อ_รายงานสถานะใบสั่งซื้อ,
        ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด,
        ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด_ยกเลิก,
        ซื้อ_เพิ่มหนี้ราคาผิด,
        ซื้อ_เพิ่มหนี้ราคาผิด_ยกเลิก,
        ซื้อ_ใบเสนอซื้อ,
        ซื้อ_ใบเสนอซื้อ_อนุมัติ,
        ซื้อ_ใบเสนอซื้อ_ยกเลิก,
        ซื้อ_ใบเสนอซื้อ_สถานะ,
        ซื้อ_พาเชียล_รับสินค้า,
        ซื้อ_พาเชียล_ส่งคืนสินค้าหรือราคาผิด,
        ซื้อ_พาเชียล_ตั้งหนี้,
        ซื้อ_พาเชียล_เพิ่มหนี้,
        ซื้อ_พาเชียล_ลดหนี้,
        ซื้อ_รายงานค้างรับตามใบสั่งซื้อ,
        สินค้า_รายงานรายละเอียดสินค้า,
        สินค้า_รายงานรายละเอียดบาร์โค๊ด,
        สินค้า_รายงานสูตรสีผสม,
        สินค้า_รายงานราคาขายสินค้า,
        สินค้า_รายงานราคาซื้อสินค้า,
        สินค้า_รายงานสินค้าที่ไม่มีการเคลื่อนไหว,
        สินค้า_รายงานสรุปเคลื่อนไหวตามปริมาณ,
        สินค้า_รายงานเคลื่อนไหว,
        สินค้า_รายงานโอนสินค้าและวัตถุดิบ,
        สินค้า_รายงานโอนสินค้าและวัตถุดิบ_ยกเลิก,
        สินค้า_รายงานบัญชีคุมพิเศษ,
        สินค้า_รายงานปรับปรุงสินค้าและวัตถุดิบ_เกิน,
        สินค้า_รายงานปรับปรุงสินค้าและวัตถุดิบ_ขาด,
        สินค้า_รายงานปรับปรุงสินค้าและวัตถุดิบ_เกิน_ยกเลิก,
        สินค้า_รายงานปรับปรุงสินค้าและวัตถุดิบ_ขาด_ยกเลิก,
        สินค้า_รายงานสินค้าและวัตถุดิบคงเหลือยกมา,
        สินค้า_รายงานสินค้าตรวจนับ,
        สินค้า_รายงานรับสินค้าสำเร็จรูป,
        สินค้า_รายงานรับสินค้าสำเร็จรูป_ยกเลิก,
        สินค้า_รายงานเบิกสินค้าวัตถุดิบ,
        สินค้า_รายงานเบิกสินค้าวัตถุดิบ_ยกเลิก,
        สินค้า_รายงานรับคืนเบิกสินค้าวัตถุดิบ,
        สินค้า_รายงานรับคืนเบิกสินค้าวัตถุดิบ_ยกเลิก,
        สินค้า_รายงานสูตรสินค้าชุด,
        สินค้า_ถึงจุดสั่งซื้อ_ตามสินค้า,
        สินค้า_รายงานเคลื่อนไหวสีผสม,
        สินค้า_รายงานการใช้แม่สี,
        สินค้า_รายงานกำไรขั้นต้นตามเอกสารแบบมีส่วนลด,
        เจ้าหนี้_รายละเอียด,
        เจ้าหนี้_ตั้งหนี้ยกมา,
        เจ้าหนี้_เพิ่มหนี้ยกมา,
        เจ้าหนี้_ลดหนี้ยกมา,
        เจ้าหนี้_ตั้งหนี้อื่น,
        เจ้าหนี้_ตั้งหนี้อื่น_ยกเลิก,
        เจ้าหนี้_เพิ่มหนี้อื่น,
        เจ้าหนี้_เพิ่มหนี้อื่น_ยกเลิก,
        เจ้าหนี้_ลดหนี้อื่น,
        เจ้าหนี้_ลดหนี้อื่น_ยกเลิก,
        เจ้าหนี้_รับวางบิล,
        เจ้าหนี้_รับวางบิล_ยกเลิก,
        เจ้าหนี้_จ่ายชำระหนี้,
        เจ้าหนี้_จ่ายชำระหนี้_ยกเลิก,
        ลูกหนี้_รายละเอียด,
        ลูกหนี้_ตั้งหนี้ยกมา,
        ลูกหนี้_เพิ่มหนี้ยกมา,
        ลูกหนี้_ลดหนี้ยกมา,
        ลูกหนี้_รับวางบิล,
        ลูกหนี้_รับวางบิล_ยกเลิก,
        ลูกหนี้_รับชำระหนี้,
        ลูกหนี้_รับชำระหนี้_ยกเลิก,
        ลูกหนี้_แต้มคงเหลือ,
        ลูกหนี้_รายงานการรับชำระหนี้ประจำวัน,
        บัตรเครดิต_รายงานบัตรเครดิต_ตามวันที่รับ,
        บัตรเครดิต_ขึ้นเงิน,
        บัตรเครดิต_ยกเลิก,
        เงินสดธนาคาร_รายงานการจ่ายเงินประจำวัน,
        เงินสดธนาคาร_รายงานการรับเงินประจำวัน,
        เงินสดธนาคาร_รายได้อื่น,
        เงินสดธนาคาร_รายได้อื่น_ลดหนี้,
        เงินสดธนาคาร_รายได้อื่น_เพิ่มหนี้,
        เงินสดธนาคาร_รายจ่ายอื่น,
        เงินสดธนาคาร_รายจ่ายอื่น_ลดหนี้,
        เงินสดธนาคาร_รายจ่ายอื่น_เพิ่มหนี้,
        เงินสดธนาคาร_รายได้อื่น_ยกเลิก,
        เงินสดธนาคาร_รายได้อื่น_ลดหนี้_ยกเลิก,
        เงินสดธนาคาร_รายได้อื่น_เพิ่มหนี้_ยกเลิก,
        เงินสดธนาคาร_รายจ่ายอื่น_ยกเลิก,
        เงินสดธนาคาร_รายจ่ายอื่น_ลดหนี้_ยกเลิก,
        เงินสดธนาคาร_รายจ่ายอื่น_เพิ่มหนี้_ยกเลิก,

        /// <summary>
        /// รายละเอียดสินค้าตามเจ้าหนี้
        /// </summary>
        Item_by_supplier,
        /// <summary>
        /// รายงานของแถม
        /// </summary>
        Item_Giveaway,
        /// <summary>
        /// รายละเอียดสินค้าแบบมี Serial
        /// </summary>
        Item_by_serial,
        /// <summary>
        /// รายงานเคลื่อนไหว Serial Number
        /// </summary>
        Serial_number,
        /// <summary>
        /// รายงานสถานะสินค้า
        /// </summary>
        Item_status,
        /// <summary>
        /// รายงานสรุปยอดค้างส่ง
        /// </summary>
        Result_item_export,
        /// <summary>
        /// รายงานสรุปยอดค้างรับ
        /// </summary>
        Result_item_import,
        สินค้า_รายงานยอดคงเหลือสินค้า,
        สินค้า_รายงานยอดคงเหลือสินค้า_Lot,
        /// <summary>
        /// รายงานยอดคงเหลือสินค้าที่จุดสูงสุด
        /// </summary>
        Item_balance_hightest,
        /// <summary>
        /// รายงานสินค้าที่ไม่มีการเคลื่อนไหว
        /// </summary>
        /// <summary>
        /// รายงานสรุปไม่เคลื่อนไหวปริมาณสินค้า
        /// </summary>
        Result_transfer_item,
        /// <summary>
        /// รายงานเคลื่อนไหวสินค้า
        /// </summary>
        /// <summary>
        /// รายงงานโอนสินค้าและวัตถุดิบ
        /// </summary>
        /// <summary>
        /// รายงงานยกเลิกโอนสินค้าและวัตถุดิบ
        /// </summary>
        Cancel_Transfer_Item_and_material,
        /// <summary>
        /// รายงานเคลื่อนไหวสินค้าต้นทุนมาตรฐาน
        /// </summary>
        Item_transfer_standard,
        /// <summary>
        /// รายงานบัญชีคุมพิเศษสินค้า
        /// </summary>
        /// <summary>
        /// พิมพ์เอกสารเพื่อตรวจนับ-ตามสินค้า
        /// </summary>
        Print_document_for_count_by_item,
        /// <summary>
        /// รายงานผลต่างจากการตรวจนับ
        /// </summary>
        รายงานผลต่างจากการตรวจนับ,
        /// <summary>
        /// รายงานสินค้าที่ไม่มีการตรวจนับแต่มียอดคงเหลือ
        /// </summary>
        Stock_no_count_no_balance,
        /// <summary>
        /// รายงานการปรับปรุงยอดสินค้า
        /// </summary>
        Implement_Item,
        /// <summary>
        /// รายงานยกเลิกปรับปรุงสนิค้าและวัตถุดิบ (เกิน)
        /// </summary>
        Cancel_Implement_Item_and_Staple_Over,
        /// <summary>
        /// รายงานปรับปรุงสนิค้าและวัตถุดิบ (ขาด)
        /// </summary>
        /// <summary>
        /// รายงานยกเลิกปรับปรุงสนิค้าและวัตถุดิบ (ขาด)
        /// </summary>
        Cancel_Implement_Item_and_Staple_Minus,
        /// <summary>
        /// รายงานการประเมินการรับสินค้า
        /// </summary>
        Span_import_item,
        /// <summary>
        /// รายงาน Lot สินค้า
        /// </summary>
        Lot_item,
        /// <summary>
        /// รายงานยกเลิกสินค้าและวัตถุดิบ คงเหลือยกมา
        /// </summary>
        Cancel_Item_and_Staple,
        /// <summary>
        /// พิมพ์เอกสารเพื่อตรวจนับ-ตามคลัง
        /// </summary>
        Print_document_for_count_by_warehouse,
        /// <summary>
        /// รายงานการยกเลิกรับสินค้าสำเร็จรูป
        /// </summary>
        Cancel_Import_Item_ready,
        /// <summary>
        /// รายงานรับคืนเบิก-วันที่
        /// </summary>
        Receptance_Widen_by_Date,
        /// <summary>
        /// รายงานการยกเลิกเบิกสินค้า,วัตถุดิบ
        /// </summary>
        Cancel_Withdraw_Item_Staple,
        /// <summary>
        /// รายงานการยกเลิกรับคืนเบิกสินค้า,วัตถุดิบ
        /// </summary>
        Cancel_Refunded_Withdraw_Item_Staple,
        /// <summary>
        /// รายงานกำหนดราคาขายสินค้า
        /// </summary>
        Expose_Item_price,
        /// <summary>
        /// รายงานโอนสินค้าระหว่างคลัง-ตามคลังโอนออก
        /// </summary>
        TransferItem_between_Warehouse_by_output,
        /// <summary>
        /// รายงานโอนสินค้าระหว่างคลังและรายการย่อย
        /// </summary>
        TransferItem_between_and_Detail,
        /// <summary>
        /// รายงานการรับสต๊อกสินค้า
        /// </summary>
        Import_Stock_Item,
        /// <summary>
        /// รายงานการบันทึกยอดสินค้ายกมาต้นปี                   
        /// </summary>
        Record_Total_Item_First_Year,
        /// <summary>
        /// รายงานรายการสินค้าและวัตถุดิบ  คงเหลือยกมา                          
        /// </summary>
        Item_Material_Balance_Bring,
        /// <summary>
        /// รายงานยอดคงเหลือสินค้า ณ ปัจจุบัน(เฉพาะสินค้ามี Serial) 
        /// </summary>
        Item_Balance_now_Only_Serial,
        /// <summary>
        /// รายงานรายละเอียดสินค้า
        /// </summary>
        Item_master,
        /// <summary>
        /// รายงานการรับสินค้าสำเร็จรูป-วันที่
        /// </summary>
        Import_Item_ready_by_Date,
        /// <summary>
        /// รายงานการเบิกสินค้า,วัตถุดิบ-วันที่เบิก
        /// </summary>
        Widen_Item_Staple_Date,
        /// <summary>
        /// รายงานรายการสินค้ายกมาต้นปี                            
        /// </summary>
        Item_First_Year,
        /// <summary>
        /// รายงานภาษีหัก ณ ที่ จ่าย ( ภงด. 53)
        /// </summary>
        Report_wht_in_53,
        /// <summary>
        /// รายงานวิเคราะห์ยอดขายสินค้าแบบแจกแจง-เรียงตามรหัส (แสดง Serial)
        /// </summary>
        สินค้า_pos_sale_sugest_by_item_and_serial,
        /// <summary>
        /// รายงานสรุปการขายสินค้าตามพนักงาน
        /// </summary>
        สินค้า_sell_by_sale,
        /// <summary>
        /// รายงานการตรวจสอบสินค้า Serial
        /// </summary>
        สินค้า_รายงานการตรวจสอบสินค้า_serial,
        /// <summary>
        /// akzo รายงานกำไรขั้นต้นกลุ่มลูกค้า
        /// </summary>
        กำไรขั้นต้น_กลุ่มลูกค้า,
        เงินสดย่อย_สถานะ,
        เงินสดย่อย_เคลื่อนไหว,
        เงินสดย่อย_เบิก,
        เงินสดย่อย_รับคืน,
        เงินสดย่อย_เบิก_ยกเลิก,
        เงินสดย่อย_รับคืน_ยกเลิก,
        ธนาคาร_ฝากเงิน,
        ธนาคาร_ถอนเงิน,
        ธนาคาร_โอนเงิน,
        ธนาคาร_ฝากเงิน_ยกเลิก,
        ธนาคาร_ถอนเงิน_ยกเลิก,
        ธนาคาร_โอนเงิน_ยกเลิก,
        เช็ค_รายงานเช็คจ่าย_ตามวันที่จ่ายเช็ค,
        เช็ค_รายงานเช็ครับ_ตามวันที่รับเช็ค,
        เช็ครับ_ยกมา,
        เช็ครับ_ฝาก,
        เช็ครับ_ผ่าน,
        เช็ครับ_รับคืน,
        เช็ครับ_ขาดสิทธิ์,
        เช็ครับ_เปลี่ยนเช็ค,
        เช็คจ่าย_ยกมา,
        เช็คจ่าย_ผ่าน,
        เช็คจ่าย_ขาดสิทธิ์,
        เช็คจ่าย_คืน,
        เช็คจ่าย_เปลี่ยนเช็ค,
        เช็ครับ_ฝาก_ยกเลิก,
        เช็ครับ_ผ่าน_ยกเลิก,
        เช็ครับ_รับคืน_ยกเลิก,
        เช็ครับ_ขาดสิทธิ์_ยกเลิก,
        เช็ครับ_เปลี่ยน_ยกเลิก,
        เช็ครับ_ทะเบียนเช็ครับ,
        เช็คจ่าย_ผ่าน_ยกเลิก,
        เช็คจ่าย_ขาดสิทธิ์_ยกเลิก,
        เช็คจ่าย_คืน_ยกเลิก,
        เช็คจ่าย_เปลี่ยน_ยกเลิก,
        เช็คจ่าย_ทะเบียนเช็คจ่าย,
        เคลื่อนไหวเงินสด
    }

    public class _ReportToolCondition
    {
        public string _fieldName;
        public string _value;

        public _ReportToolCondition(string fieldName, string value)
        {
            this._fieldName = fieldName;
            this._value = value;
        }
    }
}
