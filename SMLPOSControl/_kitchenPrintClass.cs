using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using MyLib;

namespace SMLPOSControl
{
    public class _kitchenPrintClass
    {
        private Font __headerFont = new Font("Tahoma", 24f, FontStyle.Bold);
        private Font __subHeaderFont = new Font("Tahoma", 10f, FontStyle.Regular);
        private Font __detailFont = new Font("Tahoma", 12f, FontStyle.Bold);
        private Font __headerTableFont = new Font("Tahoma", 24f, FontStyle.Bold);

        int _languageIndexPrint = 0;

        public _kitchenPrintClass()
        {
        }

        string _getResource(string name)
        {
            if (this._languageIndexPrint != 0)
            {
                string __result = MyLib._myResource._findResource(name, name, true, this._languageIndexPrint)._str;

                if (__result.Equals(name))
                {
                    return MyLib._myGlobal._resource(name, _languageEnum.English);
                }

                return __result;

            }
            else
            {
                return MyLib._myGlobal._resource(name);
            }
        }

        float _printSlipLine(Graphics g, float paperWidth, float y)
        {
            g.DrawLine(new Pen(Brushes.Black), 0, y + 2, paperWidth, y + 2);
            return 4;
        }

        float _printSlipText(Graphics g, float paperWidth, float textWidth, float x, float y, string text, Font textFont, _printSlipTextStyle style, Boolean warpString)
        {
            float __x = x;
            float __sumHeight = 0;
            string[] __text = text.Trim().Split('\n');
            for (int __loop = 0; __loop < __text.Length; __loop++)
            {
                ArrayList __getText = new ArrayList();
                if (warpString)
                {
                    __getText = MyLib._myUtil._cutString(g, __text[__loop], textFont, textWidth);
                }
                else
                {
                    __getText.Add(__text[__loop]);
                }
                for (int __loop2 = 0; __loop2 < __getText.Count; __loop2++)
                {
                    SizeF __size = g.MeasureString(__getText[__loop2].ToString(), textFont);
                    switch (style)
                    {
                        case _printSlipTextStyle.Center:
                            __x = (paperWidth - __size.Width) / 2;
                            break;
                        case _printSlipTextStyle.Right:
                            __x = paperWidth - __size.Width;
                            break;
                    }
                    g.DrawString(__getText[__loop2].ToString(), textFont, Brushes.Black, __x, y + __sumHeight);
                    __sumHeight += (__size.Height * 0.9f);
                }
            }
            return __sumHeight;
        }

        public string _lastPrintKitchenCode = "";


        /// <summary>
        /// ส่งไปครัว
        /// </summary>
        public void _printOrder(string docNo, string docDate, string docTime, int printCount)
        {
            _printOrder(docNo, docDate, docTime, "", printCount);
        }

        /// <summary>
        /// ส่งไปครัว
        /// </summary>
        public void _printOrder(string docNo, string docDate, string docTime, string header, int printCount)
        {
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            StringBuilder _myQuery = new StringBuilder();
            _myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");
            _myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.kitchen_master._table + " where " + _g.d.kitchen_master._print_to_kitchen + "=1 order by " + _g.d.kitchen_master._code));

            _myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.kitchen_print_copy._table + " where " + _g.d.kitchen_print_copy._status + "=1 order by " + _g.d.kitchen_print_copy._kitchen_code));

            _myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from printer_order_check where code = (select zone_code from printer_order_check_table where table_number = (select table_number from table_order_doc where doc_no = \'" + docNo + "\' )) "));

            _myQuery.Append("</node>");
            ArrayList __result = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, _myQuery.ToString());
            DataTable __kitchen = ((DataSet)__result[0]).Tables[0];
            DataTable __kitchenCopyPrint = ((DataSet)__result[1]).Tables[0];

            //
            //Font __headerFont = new Font("Tahoma", 12f, FontStyle.Bold);
            //Font __subHeaderFont = new Font("Tahoma", 10f, FontStyle.Regular);
            //Font __detailFont = new Font("Tahoma", 12f, FontStyle.Bold);
            for (int __kitchenLoop = 0; __kitchenLoop < __kitchen.Rows.Count; __kitchenLoop++)
            {
                //List<MyLib._printRaw> __printRaw = new List<MyLib._printRaw>();
                string __kitchenCode = __kitchen.Rows[__kitchenLoop][_g.d.kitchen_master._code].ToString();
                string __kitchenName = __kitchen.Rows[__kitchenLoop][_g.d.kitchen_master._name_1].ToString();
                int __printerMode = (int)MyLib._myGlobal._decimalPhase(__kitchen.Rows[__kitchenLoop][_g.d.kitchen_master._printer_mode].ToString());
                bool __is_manual_feed = __kitchen.Rows[__kitchenLoop][_g.d.kitchen_master._printer_manual_feed].ToString().Equals("1") ? true : false;
                bool __isBarcodePrint = __kitchen.Rows[__kitchenLoop][_g.d.kitchen_master._barcode_print].ToString().Equals("1") ? true : false;
                bool __isPrintCheckerOrder = __kitchen.Rows[__kitchenLoop][_g.d.kitchen_master._print_order_for_check].ToString().Equals("1") ? true : false;
                bool __printNumberChecker = __kitchen.Rows[__kitchenLoop][_g.d.kitchen_master._print_number_order_check].ToString().Equals("1") ? true : false;
                float __paperwidth = (float)MyLib._myGlobal._decimalPhase(__kitchen.Rows[__kitchenLoop][_g.d.kitchen_master._printer_slip_width].ToString());
                bool __print_per_unit = __kitchen.Rows[__kitchenLoop][_g.d.kitchen_master._print_order_per_unit].ToString().Equals("1") ? true : false;
                bool __order_number_barcode_print = __kitchen.Rows[__kitchenLoop][_g.d.kitchen_master._print_order_number_barcode].ToString().Equals("1") ? true : false;
                bool __item_barcode_print = __kitchen.Rows[__kitchenLoop][_g.d.kitchen_master._print_barcode].ToString().Equals("1") ? true : false;

                bool __noPricePrint = __kitchen.Rows[__kitchenLoop][_g.d.kitchen_master._no_price_print].ToString().Equals("1") ? true : false;
                bool __lineCountPrint = __kitchen.Rows[__kitchenLoop][_g.d.kitchen_master._no_number_order_for_check].ToString().Equals("1") ? false : true;

                this._languageIndexPrint = MyLib._myGlobal._intPhase(__kitchen.Rows[__kitchenLoop][_g.d.kitchen_master._print_language_index].ToString());
                // set last kitchen
                this._lastPrintKitchenCode = __kitchenCode;

                string __printerName = __kitchen.Rows[__kitchenLoop][_g.d.kitchen_master._printer_name].ToString();
                int __printSum = (int)MyLib._myGlobal._decimalPhase(__kitchen.Rows[__kitchenLoop][_g.d.kitchen_master._print_summery].ToString());

                Boolean __copyPrint = __kitchen.Rows[__kitchenLoop][_g.d.kitchen_master._print_copy].ToString().Equals("1") ? true : false;
                int __printerCopyMode = (int)MyLib._myGlobal._decimalPhase(__kitchen.Rows[__kitchenLoop][_g.d.kitchen_master._print_copy_mode].ToString());
                string __printerCopyName = __kitchen.Rows[__kitchenLoop][_g.d.kitchen_master._print_copy_name].ToString();

                // set font ก่อน
                try
                {
                    if (__kitchen.Rows[__kitchenLoop][_g.d.kitchen_master._slip_header_font] != null)
                    {
                        string __headerFontStr = __kitchen.Rows[__kitchenLoop][_g.d.kitchen_master._slip_header_font].ToString().Trim();
                        if (__headerFontStr.Length > 0)
                        {
                            __headerFont = new Font(__headerFontStr.Split(',')[0].ToString(), (float)MyLib._myGlobal._decimalPhase(__headerFontStr.Split(',')[1].ToString()), FontStyle.Regular);
                            __headerTableFont = new Font(__headerFontStr.Split(',')[0].ToString(), (float)MyLib._myGlobal._decimalPhase(__headerFontStr.Split(',')[1].ToString()) * 2, FontStyle.Regular);
                        }
                    }

                    if (__kitchen.Rows[__kitchenLoop][_g.d.kitchen_master._slip_sub_header_font] != null)
                    {
                        string __subHeaderFontStr = __kitchen.Rows[__kitchenLoop][_g.d.kitchen_master._slip_sub_header_font].ToString().Trim();
                        if (__subHeaderFontStr.Length > 0)
                        {
                            __subHeaderFont = new Font(__subHeaderFontStr.Split(',')[0].ToString(), (float)MyLib._myGlobal._decimalPhase(__subHeaderFontStr.Split(',')[1].ToString()), FontStyle.Regular);
                        }
                    }

                    if (__kitchen.Rows[__kitchenLoop][_g.d.kitchen_master._slip_detail_font] != null)
                    {
                        string __detailFontStr = __kitchen.Rows[__kitchenLoop][_g.d.kitchen_master._slip_detail_font].ToString().Trim();
                        if (__detailFontStr.Length > 0)
                        {
                            __detailFont = new Font(__detailFontStr.Split(',')[0].ToString(), (float)MyLib._myGlobal._decimalPhase(__detailFontStr.Split(',')[1].ToString()), FontStyle.Regular);
                        }
                    }
                }
                catch
                {
                }

                this._printOrderKitchen(printCount, docNo, docDate, docTime, __kitchenCode, __kitchenName, __printerMode, __printerName, header, ((__printSum == 1) ? true : false), __is_manual_feed, __paperwidth, __isBarcodePrint, __print_per_unit, false, __item_barcode_print, true, __noPricePrint, true);

                if (__isPrintCheckerOrder == true)
                {
                    this._printOrderKitchen(printCount, docNo, docDate, docTime, __kitchenCode, __kitchenName, __printerMode, __printerName, "** สรุปรายการ **", true, __is_manual_feed, __paperwidth, __isBarcodePrint, false, __order_number_barcode_print, __item_barcode_print, __printNumberChecker, __noPricePrint, __lineCountPrint);
                }

                if (__copyPrint == true)
                {
                    this._printOrderKitchen(printCount, docNo, docDate, docTime, __kitchenCode, __kitchenName, __printerCopyMode, __printerCopyName, "สำเนา-ใบสั่งอาหาร", true, __is_manual_feed, __paperwidth, false, false, false, __item_barcode_print, true, __noPricePrint, true);
                }

                // other copy

                DataRow[] __printCopy2 = (__kitchenCopyPrint.Rows.Count == 0) ? null : __kitchenCopyPrint.Select(_g.d.kitchen_print_copy._kitchen_code + "=\'" + __kitchenCode + "\'");
                if (__printCopy2 != null && __printCopy2.Length > 0)
                {
                    for (int __copyLoop = 0; __copyLoop < __printCopy2.Length; __copyLoop++)
                    {

                        int __printCopyMode2 = MyLib._myGlobal._intPhase(__printCopy2[__copyLoop][_g.d.kitchen_print_copy._print_mode].ToString());
                        string __printCopyName2 = __printCopy2[__copyLoop][_g.d.kitchen_print_copy._print_name].ToString();

                        this._printOrderKitchen(printCount, docNo, docDate, docTime, __kitchenCode, __kitchenName, __printCopyMode2, __printCopyName2, "สำเนา-ใบสั่งอาหาร", true, __is_manual_feed, __paperwidth, false, false, false, __item_barcode_print, true, __noPricePrint, true);
                    }

                }




                #region โต๋ แก้ สั่งพิมพ์ทันที เก็บไว้ใน mem แล้ว mem เต็มเมื่อพิมพ์ 3 รายการขึ้นไป
                /*
                            for (int __row = 0; __row < this._itemGrid._rowData.Count; __row++)
                            {
                                string __itemCode = this._itemGrid._cellGet(__row, _g.d.table_order._item_code).ToString().Trim();
                                string __kitchenCode2 = this._itemGrid._cellGet(__row, _g.d.table_order._kitchen_code).ToString().Trim();
                                if (__itemCode.Length > 0 && __kitchenCode.Length > 0)
                                {
                                    if (__kitchenCode.Equals(__kitchenCode2))
                                    {
                                        // โต๋ แก้ สั่งพิมพ์ทันที เก็บไว้ใน mem แล้ว mem เต็มเมื่อพิมพ์ 3 รายการขึ้นไป
                                        if (__printHead == false)
                                        {
                                            __printHead = true;
                                            __printRaw.Add(new MyLib._printRaw(0));
                                            MyLib._printRaw __source = __printRaw[__printRaw.Count - 1];
                                            __source._height = 0;
                                            __source._height += this._printSlipText(__source._g, __source._width, __source._width, 0, __source._height, "ใบสั่ง" + " : " + __kitchenName + " (" + __kitchenCode + ")", __headerFont, _printSlipTextStyle.Center, false);
                                            __source._height += this._printSlipText(__source._g, __source._width, __source._width, 0, __source._height, "โต๊ะ" + " : " + this._tableNumber, __headerFont, _printSlipTextStyle.Center, false);
                                            __source._height += this._printSlipText(__source._g, __source._width, __source._width, 0, __source._height, this._saleCode + " : " + this._saleName, __subHeaderFont, _printSlipTextStyle.Center, false);
                                            __source._height += this._printSlipText(__source._g, __source._width, __source._width, 0, __source._height, __docNo + " : " + __docDate + " : " + __docTime, __subHeaderFont, _printSlipTextStyle.Center, false);
                                        }
                                        {
                                            // รายการอาหาร/เครื่องดื่ม
                                            MyLib._printRaw __source = __printRaw[__printRaw.Count - 1];
                                            string __itemName = this._itemGrid._cellGet(__row, _g.d.table_order._item_name).ToString();
                                            string __itemUnit = this._itemGrid._cellGet(__row, _g.d.table_order._unit_code).ToString();
                                            decimal __itemQty = MyLib._myGlobal._decimalPhase(this._itemGrid._cellGet(__row, _g.d.table_order._qty).ToString());
                                            string __itemRemark = this._itemGrid._cellGet(__row, _g.d.table_order._remark).ToString().Trim();
                                            StringBuilder __desc = new StringBuilder(__countLine.ToString() + "." + __itemName + " " + "@" + " " + __itemQty.ToString("N0") + " " + __itemUnit);
                                            if (__itemRemark.Length > 0)
                                            {
                                                __desc.Append(" ");
                                                __desc.Append(__itemRemark);
                                            }
                                            __source._height += this._printSlipText(__source._g, __source._width, __source._width, 0, __source._height, __desc.ToString(), __detailFont, _printSlipTextStyle.Left, true);
                                            __countLine++;
                                        }
                                        //
                                        if (__printSum == 0)
                                        {
                                            __printHead = false;
                                            __countLine = 1;
                                        }
                                    }
                                }
                            }
                            // Print
                            for (int __loop = 0; __loop < __printRaw.Count; __loop++)
                            {
                                switch (__printerMode)
                                {
                                    case 0:
                                        {
                                            try
                                            {
                                                PrintDocument __pd = new PrintDocument();
                                                __pd.PrinterSettings.PrinterName = __printerName;
                                                __pd.PrintPage += (s1, e1) =>
                                                {
                                                    e1.Graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                                                    e1.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                                                    e1.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                                                    e1.Graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                                                    e1.Graphics.PageUnit = GraphicsUnit.Pixel;
                                                    e1.Graphics.DrawImageUnscaled(__printRaw[__loop]._bitmap, 0, 0);
                                                };
                                                __pd.Print();
                                            }
                                            catch (Exception __ex)
                                            {
                                                MessageBox.Show(__ex.Message);
                                            }
                                        }
                                        break;
                                    case 1:
                                        {
                                            try
                                            {
                                                __printRaw[__loop]._print(__printerName);
                                                __printRaw[__loop]._print(__printerName, new byte[] { 29, 86, 66, 10 });
                                            }
                                            catch (Exception __ex)
                                            {
                                                MessageBox.Show(__ex.Message);
                                            }
                                        }
                                        break;
                                    case 2:
                                        {
                                            IPEndPoint __ip = new IPEndPoint(IPAddress.Parse(__printerName), 9100);
                                            Socket __socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                                            __socket.Connect(__ip);
                                            __socket.Send(new byte[] { 27, 64 }); // Initialize printer
                                            __socket.Send(__printRaw[__loop]._getDataBytes());

                                            __socket.Send(new byte[] { 10 });
                                            __socket.Send(new byte[] { 29, 86, 66, 10 }); // Cut
                                            __socket.Shutdown(SocketShutdown.Both);
                                            __socket.Close();
                                        }
                                        break;
                                }
                            }
                            */
                #endregion
                /*
                            MyLib._printRaw __source = null;
                            for (int __row = 0; __row < this._itemGrid._rowData.Count; __row++)
                            {
                                string __itemCode = this._itemGrid._cellGet(__row, _g.d.table_order._item_code).ToString().Trim();
                                string __kitchenCode2 = this._itemGrid._cellGet(__row, _g.d.table_order._kitchen_code).ToString().Trim();
                                if (__itemCode.Length > 0 && __kitchenCode.Length > 0)
                                {
                                    if (__kitchenCode.Equals(__kitchenCode2))
                                    {
                                        // create graphic
                                        if (__printHead == false)
                                        {
                                            __printHead = true;
                                            __source = new MyLib._printRaw(0);
                                            __source._height = 0;
                                            __source._height += this._printSlipText(__source._g, __source._width, __source._width, 0, __source._height, "ใบสั่ง" + " : " + __kitchenName + " (" + __kitchenCode + ")", __headerFont, _printSlipTextStyle.Center, false);
                                            __source._height += this._printSlipText(__source._g, __source._width, __source._width, 0, __source._height, "โต๊ะ" + " : " + this._tableNumber, __headerFont, _printSlipTextStyle.Center, false);
                                            __source._height += this._printSlipText(__source._g, __source._width, __source._width, 0, __source._height, this._saleCode + " : " + this._saleName, __subHeaderFont, _printSlipTextStyle.Center, false);
                                            __source._height += this._printSlipText(__source._g, __source._width, __source._width, 0, __source._height, __docNo + " : " + __docDate + " : " + __docTime, __subHeaderFont, _printSlipTextStyle.Center, false);

                                        }

                                        // order
                                        {
                                            // รายการอาหาร/เครื่องดื่ม
                                            //MyLib._printRaw __source = __printRaw[__printRaw.Count - 1];
                                            string __itemName = this._itemGrid._cellGet(__row, _g.d.table_order._item_name).ToString();
                                            string __itemUnit = this._itemGrid._cellGet(__row, _g.d.table_order._unit_code).ToString();
                                            decimal __itemQty = MyLib._myGlobal._decimalPhase(this._itemGrid._cellGet(__row, _g.d.table_order._qty).ToString());
                                            string __itemRemark = this._itemGrid._cellGet(__row, _g.d.table_order._remark).ToString().Trim();
                                            StringBuilder __desc = new StringBuilder(__countLine.ToString() + "." + __itemName + " " + "@" + " " + __itemQty.ToString("N0") + " " + __itemUnit);
                                            if (__itemRemark.Length > 0)
                                            {
                                                __desc.Append(" ");
                                                __desc.Append(__itemRemark);
                                            }
                                            __source._height += this._printSlipText(__source._g, __source._width, __source._width, 0, __source._height, __desc.ToString(), __detailFont, _printSlipTextStyle.Left, true);
                                            __countLine++;
                                        }

                                        // check last

                                        if (__printSum == 0 || __row == this._itemGrid._rowData.Count - 1)
                                        {
                                            __printHead = false;
                                            __countLine = 1;
                                            if (__source != null)
                                            {
                                                
                                            }
                                            __source = null;
                                        }


                                    }
                                }
                            }
                            */
            }

            DataTable __printCheckerOrder = ((DataSet)__result[2]).Tables[0];
            if (__printCheckerOrder.Rows.Count > 0)
            {
                int __printerCheckerMode = MyLib._myGlobal._intPhase(__printCheckerOrder.Rows[0][_g.d.printer_order_check._print_mode].ToString());
                string __printerCheckerName = __printCheckerOrder.Rows[0][_g.d.printer_order_check._printer_name].ToString();

                int __status = MyLib._myGlobal._intPhase(__printCheckerOrder.Rows[0][_g.d.printer_order_check._status].ToString());

                if (__status == 1)
                {
                    this._sendPrintCheckerOrder(docNo, docDate, docTime, __printerCheckerMode, __printerCheckerName);
                }

            }
        }

        private void _sendPrintCheckerOrder(string docNo, string docDate, string docTime, int printerMode, string printerName)
        {
            bool lineCountPrint = true;
            bool noPrintPrice = false;
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            StringBuilder _myQuery = new StringBuilder();
            _myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");

            // Array 0
            _myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("select *, (select name_1 from " + _g.d.erp_user._table + " where " + _g.d.erp_user._code + " = " + _g.d.table_order_doc._sale_code + ") as sale_name from " + _g.d.table_order_doc._table + " where " + _g.d.table_order_doc._doc_no + "=\'" + docNo + "\'"));
            _myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("select *" +
                ", (select " + _g.d.ic_inventory._print_order_per_unit + " from ic_inventory where ic_inventory.code = item_code) as " + _g.d.ic_inventory._print_order_per_unit +
                ", (select " + _g.d.ic_inventory._barcode_checker_print + " from ic_inventory where ic_inventory.code = item_code) as " + _g.d.ic_inventory._barcode_checker_print +
                ",(select name_1 from ic_inventory where code=item_code) as " + _g.d.table_order._item_name +
                ",(select name_1 from ic_unit where code=unit_code) as unit_name " +

                " from " + _g.d.table_order._table + " where " + _g.d.table_order._doc_no + "=\'" + docNo + "\' order by " + _g.d.table_order._item_code));

            _myQuery.Append("</node>");

            ArrayList __result = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, _myQuery.ToString());

            DataTable __head = ((DataSet)__result[0]).Tables[0];
            DataTable __details = ((DataSet)__result[1]).Tables[0];
            string __tableNumber = __head.Rows[0][_g.d.table_order_doc._table_number].ToString();
            string __saleCode = __head.Rows[0][_g.d.table_order_doc._sale_code].ToString();
            string __saleName = __head.Rows[0]["sale_name"].ToString();
            string __barcodeOrderNumber = __head.Rows[0][_g.d.table_order_doc._order_number_barcode].ToString();

            MyLib._printRaw __source = null;

            int __countLine = 1;

            __source = new MyLib._printRaw(595.0f, 20000);
            __source._height = 0;
            string header = "** สรุปรายการ **";

            __source._height += this._printSlipText(__source._g, __source._width, __source._width, 0, __source._height, header, __headerFont, _printSlipTextStyle.Center, false);
            __source._height += this._printSlipText(__source._g, __source._width, __source._width, 0, __source._height, _getResource("โต๊ะ") + ":" + __tableNumber, __headerTableFont, _printSlipTextStyle.Center, false);
            __source._height += this._printSlipText(__source._g, __source._width, __source._width, 0, __source._height, docDate + ":" + docTime, __subHeaderFont, _printSlipTextStyle.Center, false);
            __source._height += this._printSlipText(__source._g, __source._width, __source._width, 0, __source._height, docNo, __subHeaderFont, _printSlipTextStyle.Center, false);


            if (__saleCode.Length > 0)
            {
                __source._height += this._printSlipText(__source._g, __source._width, __source._width, 0, __source._height, __saleCode + ":" + __saleName, __subHeaderFont, _printSlipTextStyle.Center, false);
            }

            for (int __row = 0; __row < __details.Rows.Count; __row++)
            {
                string __itemName = __details.Rows[__row][_g.d.table_order._item_name].ToString(); // this._itemGrid._cellGet(__row, _g.d.table_order._item_name).ToString();
                string __asItemName = __details.Rows[__row][_g.d.table_order._as_item_name].ToString(); // this._itemGrid._cellGet(__row, _g.d.table_order._as_item_name).ToString();
                string __confirmGUID = __details.Rows[__row][_g.d.table_order._confirm_guid].ToString(); // this._itemGrid._cellGet(__row, _g.d.table_order._kitchen_code).ToString().Trim();

                if (__asItemName.Length > 0)
                {
                    __itemName = __asItemName;
                }

                string __itemUnit = __details.Rows[__row]["unit_name"].ToString();  // __details.Rows[__row][_g.d.table_order._unit_code].ToString();// this._itemGrid._cellGet(__row, _g.d.table_order._unit_code).ToString();
                decimal __itemQty = MyLib._myGlobal._decimalPhase(__details.Rows[__row][_g.d.table_order._qty].ToString()); // MyLib._myGlobal._decimalPhase(this._itemGrid._cellGet(__row, _g.d.table_order._qty).ToString());
                decimal __itemPrice = MyLib._myGlobal._decimalPhase(__details.Rows[__row][_g.d.table_order._price].ToString());
                string __itemRemark = __details.Rows[__row][_g.d.table_order._remark].ToString(); // this._itemGrid._cellGet(__row, _g.d.table_order._remark).ToString().Trim();
                StringBuilder __desc = new StringBuilder(((lineCountPrint) ? __countLine.ToString() + "." : "") + __itemName + " " + "@" + " " + __itemQty.ToString("#,###.##") + " " + __itemUnit);

                if (noPrintPrice == false && __itemPrice > 0)
                {
                    __desc.Append(" #" + __itemPrice.ToString("#,###.##"));
                }

                __source._height += this._printSlipText(__source._g, __source._width, __source._width, 0, __source._height, __desc.ToString(), __detailFont, _printSlipTextStyle.Left, true);

                if (__itemRemark.Length > 0)
                {
                    __source._height += this._printSlipText(__source._g, __source._width, __source._width - 20, 20, __source._height, "***" + __itemRemark, __detailFont, _printSlipTextStyle.Left, true);
                }
                __countLine++;
            }

            if (__source != null)
            {
                // send print
                _sendPrintKitchen(printerMode, printerName, __source);
                __source._dispose();
            }

        }

        private void _printOrderKitchen(int printCount, string docNo, string docDate, string docTime, string kitchenCode, string kitchenName, int printerMode, string printerName, string header, bool sumPrint, bool manualPaperFeed, float paperWidth, bool barcodePrint)
        {
            _printOrderKitchen(printCount, docNo, docDate, docTime, kitchenCode, kitchenName, printerMode, printerName, header, sumPrint, manualPaperFeed, paperWidth, barcodePrint, false);
        }

        private void _printOrderKitchen(int printCount, string docNo, string docDate, string docTime, string kitchenCode, string kitchenName, int printerMode, string printerName, string header, bool sumPrint, bool manualPaperFeed, float paperWidth, bool barcodePrint, bool printPerUnit)
        {
            _printOrderKitchen(printCount, docNo, docDate, docTime, kitchenCode, kitchenName, printerMode, printerName, header, sumPrint, manualPaperFeed, paperWidth, barcodePrint, false, false);
        }

        private void _printOrderKitchen(int printCount, string docNo, string docDate, string docTime, string kitchenCode, string kitchenName, int printerMode, string printerName, string header, bool sumPrint, bool manualPaperFeed, float paperWidth, bool barcodePrint, bool printPerUnit, bool _barcodeOrderNumberPrint)
        {
            _printOrderKitchen(printCount, docNo, docDate, docTime, kitchenCode, kitchenName, printerMode, printerName, header, sumPrint, manualPaperFeed, paperWidth, barcodePrint, printPerUnit, _barcodeOrderNumberPrint, false);

        }

        private void _printOrderKitchen(int printCount, string docNo, string docDate, string docTime, string kitchenCode, string kitchenName, int printerMode, string printerName, string header, bool sumPrint, bool manualPaperFeed, float paperWidth, bool barcodePrint, bool printPerUnit, bool _barcodeOrderNumberPrint, bool itemBarcodePrint)
        {
            _printOrderKitchen(printCount, docNo, docDate, docTime, kitchenCode, kitchenName, printerMode, printerName, header, sumPrint, manualPaperFeed, paperWidth, barcodePrint, printPerUnit, _barcodeOrderNumberPrint, false, false, true, true);
        }

        private void _printOrderKitchen(int printCount, string docNo, string docDate, string docTime, string kitchenCode, string kitchenName, int printerMode, string printerName, string header, bool sumPrint, bool manualPaperFeed, float paperWidth, bool barcodePrint, bool printPerUnit, bool _barcodeOrderNumberPrint, bool itemBarcodePrint, bool showCheckerNumber, bool noPrintPrice, bool lineCountPrint)
        {
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            bool __updatePrinterStatus = false;
            StringBuilder _myQuery = new StringBuilder();

            _myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");

            // Array 0
            _myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("select *, (select name_1 from " + _g.d.erp_user._table + " where " + _g.d.erp_user._code + " = " + _g.d.table_order_doc._sale_code + ") as sale_name from " + _g.d.table_order_doc._table + " where " + _g.d.table_order_doc._doc_no + "=\'" + docNo + "\'"));

            // Array 1
            _myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * " +
                ", (select " + _g.d.ic_inventory._print_order_per_unit + " from ic_inventory where ic_inventory.code = item_code) as " + _g.d.ic_inventory._print_order_per_unit +
                ", (select " + _g.d.ic_inventory._barcode_checker_print + " from ic_inventory where ic_inventory.code = item_code) as " + _g.d.ic_inventory._barcode_checker_print +
                ",(select name_1 from ic_inventory where code=item_code) as " + _g.d.table_order._item_name +
                ",(select " + _g.d.ic_inventory._name_eng_1 + " from ic_inventory where code=item_code) as " + _g.d.ic_inventory._name_eng_1 +
                ",(select " + _g.d.ic_unit._name_2 + " from ic_unit where code=unit_code) as " + _g.d.ic_unit._name_2 +
                ",(select " + _g.d.ic_unit._name_1 + " from ic_unit where code=unit_code) as unit_name " +

                " from " + _g.d.table_order._table + " where " + _g.d.table_order._doc_no + "=\'" + docNo + "\' order by " + _g.d.table_order._item_code));

            // Array 2
            // bill count
            _myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("select count(*) as bill_count " +
            ", (select count(doc_no) from table_order_doc where trans_guid in (select trans_guid from table_order_doc as x where x.doc_no = '" + docNo + "' )) as countorder " +
            " from (select " + _g.d.kitchen_print_count._doc_no + "," + _g.d.kitchen_print_count._doc_date + "," + _g.d.kitchen_print_count._kitchen_code + " from " + _g.d.kitchen_print_count._table + " where " + _g.d.kitchen_print_count._doc_date + " = '" + MyLib._myGlobal._workingDate.ToString("yyyy-MM-dd", new CultureInfo("en-US")) + "' and " + _g.d.kitchen_print_count._kitchen_code + " = '" + kitchenCode + "' ) as temp2 "));

            // toe แทรก option พิมพ์ของครัว
            _myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.kitchen_master._hide_table_footer_bill + "," + _g.d.kitchen_master._hide_repeat_order_bill + " from " + _g.d.kitchen_master._table + " where " + _g.d.kitchen_master._code + " =\'" + kitchenCode + "\'"));

            _myQuery.Append("</node>");

            ArrayList __result = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, _myQuery.ToString());

            // MessageBox.Show(__result.Count + " : " + _myQuery.ToString());

            DataTable __head = ((DataSet)__result[0]).Tables[0];
            DataTable __details = ((DataSet)__result[1]).Tables[0];
            string __tableNumber = __head.Rows[0][_g.d.table_order_doc._table_number].ToString();
            string __saleCode = __head.Rows[0][_g.d.table_order_doc._sale_code].ToString();
            string __saleName = __head.Rows[0]["sale_name"].ToString();
            string __barcodeOrderNumber = __head.Rows[0][_g.d.table_order_doc._order_number_barcode].ToString();
            //

            DataTable __kitchenOption = ((DataSet)__result[3]).Tables[0];
            bool __hide_table_footer_bill = __kitchenOption.Rows[0][_g.d.kitchen_master._hide_table_footer_bill].ToString().Equals("1") ? true : false;
            bool __hide_repeat_order_bill = __kitchenOption.Rows[0][_g.d.kitchen_master._hide_repeat_order_bill].ToString().Equals("1") ? true : false;

            int __orderCount = 0;
            try
            {
                string __queryOrderCount = "select count(*) as xcount from (select distinct doc_no from table_order where now() - to_timestamp(to_char(doc_date,'YYYY-MM-DD') || ' ' || doc_time || ':00','YYYY-MM-DD HH24:MI:SS') > '00:10:00' and last_status=0 and qty_balance > 0 and table_number='" + __tableNumber + "') as x1";
                DataTable __orderCountTable = __myFrameWork._queryShort(__queryOrderCount).Tables[0];
                if (__orderCountTable.Rows.Count > 0)
                {
                    __orderCount = MyLib._myGlobal._intPhase(__orderCountTable.Rows[0][0].ToString());
                }
            }
            catch
            {
            }
            //
            string __slipHeader = (header.Length == 0) ? "ใบสั่ง" : header; //  "** สรุปรายการ **";
            int __billCount = 0;
            // ทำ source และ ส่ง
            if (sumPrint)
            {
                Boolean _headerPrint = false;
                MyLib._printRaw __source = null;

                int __countLine = 1;
                for (int __row = 0; __row < __details.Rows.Count; __row++)
                {
                    string __itemCode = __details.Rows[__row][_g.d.table_order._item_code].ToString(); // this._itemGrid._cellGet(__row, _g.d.table_order._item_code).ToString().Trim();
                    string __kitchenCode2 = __details.Rows[__row][_g.d.table_order._kitchen_code].ToString(); // this._itemGrid._cellGet(__row, _g.d.table_order._kitchen_code).ToString().Trim();
                    if (__itemCode.Length > 0 && kitchenCode.Length > 0)
                    {
                        if (kitchenCode.Equals(__kitchenCode2))
                        {
                            if (_headerPrint == false)
                            {
                                __source = new MyLib._printRaw(paperWidth, 20000);
                                __source._height = 0;
                                if (header.Length == 0)
                                {
                                    __source._height += this._printSlipText(__source._g, __source._width, __source._width, 0, __source._height, _getResource(__slipHeader) + ":" + kitchenName, __headerFont, _printSlipTextStyle.Center, false);
                                }
                                else
                                {
                                    if (header == "** สรุปรายการ **")
                                    {
                                        if (showCheckerNumber)
                                        {
                                            DataTable __checkerBill = ((DataSet)__result[2]).Tables[0];
                                            __billCount = MyLib._myGlobal._intPhase(__checkerBill.Rows[0][0].ToString()) + 1;
                                            __source._height += this._printSlipText(__source._g, __source._width, __source._width, 0, __source._height, __billCount.ToString(), new Font("Tahoma", 22, FontStyle.Regular), _printSlipTextStyle.Center, false);
                                        }
                                    }
                                    __source._height += this._printSlipText(__source._g, __source._width, __source._width, 0, __source._height, __slipHeader, __headerFont, _printSlipTextStyle.Center, false);
                                    __source._height += this._printSlipText(__source._g, __source._width, __source._width, 0, __source._height, kitchenName, __headerFont, _printSlipTextStyle.Center, false);
                                }

                                if (header == "ใบสั่งจัดรายการ")
                                {
                                    __source._height += this._printSlipText(__source._g, __source._width, __source._width, 0, __source._height, _getResource("เครื่อง") + ":" + __tableNumber, __headerTableFont, _printSlipTextStyle.Center, false);
                                }
                                else
                                {
                                    __source._height += this._printSlipText(__source._g, __source._width, __source._width, 0, __source._height, _getResource("โต๊ะ") + ":" + __tableNumber, __headerTableFont, _printSlipTextStyle.Center, false);
                                }

                                if (__saleCode.Length > 0)
                                {
                                    __source._height += this._printSlipText(__source._g, __source._width, __source._width, 0, __source._height, __saleCode + ":" + __saleName, __subHeaderFont, _printSlipTextStyle.Center, false);
                                }

                                __source._height += this._printSlipText(__source._g, __source._width, __source._width, 0, __source._height, docDate + ":" + docTime, __subHeaderFont, _printSlipTextStyle.Center, false);
                                __source._height += this._printSlipText(__source._g, __source._width, __source._width, 0, __source._height, docNo, __subHeaderFont, _printSlipTextStyle.Center, false);

                                if (_barcodeOrderNumberPrint)
                                {
                                    // พิมพ์บาร์โค๊ด เลขโต๊ะ
                                    try
                                    {
                                        string __check_id = __barcodeOrderNumber;
                                        try
                                        {
                                            BarcoderLib.BarcodeEAN13 __ean13 = new BarcoderLib.BarcodeEAN13();

                                            Image __image = __ean13.Encode(__check_id, 135, 35);
                                            PointF __imgPoint = new PointF();
                                            __imgPoint.Y = __source._height; // * 72 / 96;
                                            float __imgWidth = (__image.Width / __image.HorizontalResolution) * __source._g.DpiX;
                                            __imgPoint.X = (__source._width - __imgWidth) / 2;

                                            float __imgHeight = (__image.Height / __image.VerticalResolution) * __source._g.DpiY;
                                            __source._g.DrawImage(__image, __imgPoint.X, __imgPoint.Y, __imgWidth, __imgHeight); // , 200 , 50
                                            __source._height += __imgHeight; // (__source._height * 72 / 96);
                                        }
                                        catch
                                        {
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(ex.ToString());
                                    }
                                }

                                _headerPrint = true;
                            }

                            // รายการอาหาร/เครื่องดื่ม
                            //MyLib._printRaw __source = __printRaw[__printRaw.Count - 1];
                            string __itemName = __details.Rows[__row][_g.d.table_order._item_name].ToString(); // this._itemGrid._cellGet(__row, _g.d.table_order._item_name).ToString();
                            string __itemNameEng = __details.Rows[__row][_g.d.ic_inventory._name_eng_1].ToString();

                            string __asItemName = __details.Rows[__row][_g.d.table_order._as_item_name].ToString(); // this._itemGrid._cellGet(__row, _g.d.table_order._as_item_name).ToString();
                            string __confirmGUID = __details.Rows[__row][_g.d.table_order._confirm_guid].ToString(); // this._itemGrid._cellGet(__row, _g.d.table_order._kitchen_code).ToString().Trim();

                            if (__asItemName.Length > 0)
                            {
                                __itemName = __asItemName;
                            }

                            if (this._languageIndexPrint > 0 && __itemNameEng.Length > 0)
                            {
                                __itemName = __itemNameEng;
                            }

                            if (itemBarcodePrint)
                            {
                                __itemName = __details.Rows[__row][_g.d.table_order._barcode].ToString() + "/" + __itemName;
                            }

                            string __itemUnit = __details.Rows[__row]["unit_name"].ToString(); // __details.Rows[__row][_g.d.table_order._unit_code].ToString();// this._itemGrid._cellGet(__row, _g.d.table_order._unit_code).ToString();

                            if (this._languageIndexPrint > 0 && __details.Rows[__row][_g.d.ic_unit._name_2].ToString().Length > 0)
                            {
                                __itemUnit = __details.Rows[__row][_g.d.ic_unit._name_2].ToString();
                            }

                            decimal __itemQty = MyLib._myGlobal._decimalPhase(__details.Rows[__row][_g.d.table_order._qty].ToString()); // MyLib._myGlobal._decimalPhase(this._itemGrid._cellGet(__row, _g.d.table_order._qty).ToString());
                            decimal __itemPrice = MyLib._myGlobal._decimalPhase(__details.Rows[__row][_g.d.table_order._price].ToString());
                            string __itemRemark = __details.Rows[__row][_g.d.table_order._remark].ToString(); // this._itemGrid._cellGet(__row, _g.d.table_order._remark).ToString().Trim();
                            StringBuilder __desc = new StringBuilder(((lineCountPrint) ? __countLine.ToString() + "." : "") + __itemName + " " + "@" + " " + __itemQty.ToString("#,###.##") + " " + __itemUnit);

                            if (noPrintPrice == false && __itemPrice > 0)
                            {
                                __desc.Append(" #" + __itemPrice.ToString("#,###.##"));
                            }
                            // toe set remark new line
                            //if (__itemRemark.Length > 0)
                            //{
                            //    __desc.Append(" ");
                            //    __desc.Append(__itemRemark);
                            //}
                            __source._height += this._printSlipText(__source._g, __source._width, __source._width, 0, __source._height, __desc.ToString(), __detailFont, _printSlipTextStyle.Left, true);

                            if (__itemRemark.Length > 0)
                            {
                                __source._height += this._printSlipText(__source._g, __source._width, __source._width - 20, 20, __source._height, "***" + __itemRemark, __detailFont, _printSlipTextStyle.Left, true);
                            }

                            // add cofirm qrcode
                            /*
                            if (__confirmGUID.Length > 0 && barcodePrint == true && MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLTomYumGoongPro)
                            {
                                // draw guid
                                byte[] __dateByte = new byte[1024];

                                try
                                {
                                    __dateByte = __myFrameWork._qrCodeByte(__confirmGUID);
                                    Image __image = Image.FromStream(new MemoryStream(__dateByte));

                                    // get location
                                    PointF __imgPoint = new PointF();


                                    __imgPoint.Y = __source._height; // * 72 / 96;
                                    float __imgWidth = (__image.Width / __image.HorizontalResolution) * __source._g.DpiX;
                                    __imgPoint.X = (__source._width - __imgWidth) / 2;
                                    //__imgPoint.Width = 100;
                                    //__imgPoint.Height = 100;



                                    __source._g.DrawImage(__image, __imgPoint);

                                    float __imgHeight = (__image.Height / __image.HorizontalResolution) * __source._g.DpiX;
                                    __source._height += __imgHeight; // (__source._height * 72 / 96);


                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.ToString());
                                }
                            }*/

                            __countLine++;
                        }
                    }
                }

                if (__source != null)
                {
                    if (manualPaperFeed == true)
                    {
                        __source._height += 150;
                    }

                    // send print
                    _sendPrintKitchen(printerMode, printerName, __source);
                    __source._dispose();
                    __updatePrinterStatus = true;
                }
            }
            else
            {
                // for confirm 
                for (int __row = 0; __row < __details.Rows.Count; __row++)
                {
                    string __itemCode = __details.Rows[__row][_g.d.table_order._item_code].ToString(); // this._itemGrid._cellGet(__row, _g.d.table_order._item_code).ToString().Trim();
                    string __kitchenCode2 = __details.Rows[__row][_g.d.table_order._kitchen_code].ToString(); // this._itemGrid._cellGet(__row, _g.d.table_order._kitchen_code).ToString().Trim();
                    string __confirmGUID = __details.Rows[__row][_g.d.table_order._confirm_guid].ToString(); // this._itemGrid._cellGet(__row, _g.d.table_order._kitchen_code).ToString().Trim();
                    bool __barcode_print = __details.Rows[__row][_g.d.ic_inventory._barcode_checker_print].ToString().Equals("1") ? true : false;
                    bool __print_per_unit = __details.Rows[__row][_g.d.ic_inventory._print_order_per_unit].ToString().Equals("1") ? true : false;

                    // guid line
                    string __line_guid = __details.Rows[__row][_g.d.table_order._guid_line].ToString();
                    if (__itemCode.Length > 0 && kitchenCode.Length > 0)
                    {
                        if (kitchenCode.Equals(__kitchenCode2))
                        {
                            Thread.Sleep(1000);
                            int __qtyLoop = 0;
                            decimal __itemQty = MyLib._myGlobal._decimalPhase(__details.Rows[__row][_g.d.table_order._qty].ToString()); // MyLib._myGlobal._decimalPhase(this._itemGrid._cellGet(__row, _g.d.table_order._qty).ToString());
                            string __queryChecker = "select * from " + _g.d.order_checker._table + " where " + _g.d.order_checker._doc_no + "=\'" + docNo + "\' and " + _g.d.order_checker._guid_line + "=\'" + __line_guid + "\'";
                            DataTable __queryCheckerTable = __myFrameWork._queryShort(__queryChecker).Tables[0];
                            // toe
                            while (__qtyLoop < __itemQty)
                            {
                                Thread.Sleep(1000);
                                MyLib._printRaw __source = new MyLib._printRaw(paperWidth, 1000);
                                __source._height = 0;
                                __source._height += this._printSlipText(__source._g, __source._width, __source._width, 0, __source._height, _getResource(__slipHeader) + ":" + kitchenName, __headerFont, _printSlipTextStyle.Center, false);
                                __source._height += this._printSlipText(__source._g, __source._width, __source._width, 0, __source._height, _getResource("โต๊ะ") + ":" + __tableNumber, __headerTableFont, _printSlipTextStyle.Center, false);
                                __source._height += this._printSlipText(__source._g, __source._width, __source._width, 0, __source._height, __saleCode + ":" + __saleName, __subHeaderFont, _printSlipTextStyle.Center, false);
                                __source._height += this._printSlipText(__source._g, __source._width, __source._width, 0, __source._height, docDate + ":" + docTime, __subHeaderFont, _printSlipTextStyle.Center, false);
                                __source._height += this._printSlipText(__source._g, __source._width, __source._width, 0, __source._height, docNo, __subHeaderFont, _printSlipTextStyle.Center, false);

                                string __itemName = __details.Rows[__row][_g.d.table_order._item_name].ToString(); // this._itemGrid._cellGet(__row, _g.d.table_order._item_name).ToString();
                                string __itemNameEng = __details.Rows[__row][_g.d.ic_inventory._name_eng_1].ToString();
                                string __asItemName = __details.Rows[__row][_g.d.table_order._as_item_name].ToString(); // this._itemGrid._cellGet(__row, _g.d.table_order._as_item_name).ToString();
                                if (__asItemName.Length > 0)
                                {
                                    __itemName = __asItemName;
                                }

                                if (this._languageIndexPrint > 0 && __itemNameEng.Length > 0)
                                {
                                    __itemName = __itemNameEng;
                                }


                                if (itemBarcodePrint)
                                {
                                    __itemName = __details.Rows[__row][_g.d.table_order._barcode].ToString() + "/" + __itemName;
                                }

                                string __itemUnit = __details.Rows[__row]["unit_name"].ToString(); //__details.Rows[__row][_g.d.table_order._unit_code].ToString();// this._itemGrid._cellGet(__row, _g.d.table_order._unit_code).ToString();
                                if (this._languageIndexPrint > 0 && __details.Rows[__row][_g.d.ic_unit._name_2].ToString().Length > 0)
                                {
                                    __itemUnit = __details.Rows[__row][_g.d.ic_unit._name_2].ToString();
                                }

                                decimal __itemPrice = MyLib._myGlobal._decimalPhase(__details.Rows[__row][_g.d.table_order._price].ToString());
                                string __itemRemark = __details.Rows[__row][_g.d.table_order._remark].ToString(); // this._itemGrid._cellGet(__row, _g.d.table_order._remark).ToString().Trim();


                                StringBuilder __desc = new StringBuilder(__itemName + " " + "@" + " ");
                                if (__print_per_unit == true)
                                {
                                    __desc.Append("1 " + __itemUnit + " (" + (__qtyLoop + 1).ToString("#,###.##") + "/" + __itemQty.ToString("#,###.##") + ")");
                                }
                                else
                                {
                                    __desc.Append(__itemQty.ToString("#,###.##") + " " + __itemUnit);
                                }
                                //if (__itemRemark.Length > 0)
                                //{
                                //    __desc.Append(" ");
                                //    __desc.Append(__itemRemark);
                                //}

                                // ราคา
                                if (__itemPrice > 0)
                                {
                                    __desc.Append(" #" + __itemPrice.ToString("#,###.##"));
                                }

                                __source._height += this._printSlipText(__source._g, __source._width, __source._width, 0, __source._height, __desc.ToString(), __detailFont, _printSlipTextStyle.Left, true);
                                //__countLine++;
                                if (__itemRemark.Length > 0)
                                {
                                    __source._height += this._printSlipText(__source._g, __source._width, __source._width - 20, 20, __source._height, "***" + __itemRemark, __detailFont, _printSlipTextStyle.Left, true);
                                }

                                // add confirm qrcode
                                if (_g.g._companyProfile._use_order_checker)
                                {

                                    if (_g.g._companyProfile._use_order_checker == true && __queryCheckerTable.Rows.Count > 0 && __barcode_print == true && (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLTomYumGoongPro || MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLTomYumGoong))
                                    {
                                        // draw guid
                                        //byte[] __dateByte = new byte[1024];

                                        try
                                        {
                                            /*
                                            __dateByte = __myFrameWork._qrCodeByte(__confirmGUID);
                                            Image __image = Image.FromStream(new MemoryStream(__dateByte));
                                            */
                                            string __check_id = __queryCheckerTable.Rows[__qtyLoop][_g.d.order_checker._guid_confirm].ToString();
                                            try
                                            {
                                                //SMLBarcodeManage.Ean13 __code39 = new SMLBarcodeManage._createCode39();
                                                for (int __loop = 0; __loop < 2; __loop++)
                                                {
                                                    try
                                                    {
                                                        BarcoderLib.BarcodeEAN13 __ean13 = new BarcoderLib.BarcodeEAN13();

                                                        Image __image = __ean13.Encode(__check_id, 135, 35);
                                                        //__image.Save(@"C:\\smlsoft\\bar1.png");
                                                        // get location
                                                        PointF __imgPoint = new PointF();

                                                        __imgPoint.Y = __source._height; // * 72 / 96;
                                                        float __imgWidth = (__image.Width / __image.HorizontalResolution) * __source._g.DpiX;
                                                        __imgPoint.X = (__source._width - __imgWidth) / 2;

                                                        float __imgHeight = (__image.Height / __image.VerticalResolution) * __source._g.DpiY;

                                                        //__imgPoint.Width = 100;
                                                        //__imgPoint.Height = 100;

                                                        __source._g.DrawImage(__image, __imgPoint.X, __imgPoint.Y, __imgWidth, __imgHeight); // , 200 , 50

                                                        //float __imgHeight = (__image.Height / __image.HorizontalResolution) * __source._g.DpiX;
                                                        __source._height += __imgHeight; // (__source._height * 72 / 96);
                                                    }
                                                    catch
                                                    {
                                                        __source._height += this._printSlipText(__source._g, __source._width, __source._width, 0, __source._height, "Barcode Error 1", __subHeaderFont, _printSlipTextStyle.Center, false);
                                                    }
                                                    if (__loop == 0)
                                                    {
                                                        __source._height += this._printSlipText(__source._g, __source._width, __source._width - 20, 20, __source._height, __check_id, __detailFont, _printSlipTextStyle.Center, true);
                                                    }
                                                }

                                            }
                                            catch
                                            {
                                                __source._height += this._printSlipText(__source._g, __source._width, __source._width, 0, __source._height, "Barcode Error 2", __subHeaderFont, _printSlipTextStyle.Center, false);
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            __source._height += this._printSlipText(__source._g, __source._width, __source._width, 0, __source._height, "Barcode Error 3", __subHeaderFont, _printSlipTextStyle.Center, false);
                                            Console.WriteLine(ex.ToString());
                                        }
                                    }
                                    else
                                    {
                                        string __error = __queryCheckerTable.Rows.Count.ToString() + "-" + ((__barcode_print == true) ? "TRUE" : "FALSE");
                                        // __source._height += this._printSlipText(__source._g, __source._width, __source._width, 0, __source._height, "Err:" + __error, __subHeaderFont, _printSlipTextStyle.Center, false);
                                    }
                                }

                                if (__hide_table_footer_bill == false)
                                    __source._height += this._printSlipText(__source._g, __source._width, __source._width, 0, __source._height, _getResource("โต๊ะ") + ":" + __tableNumber, __headerTableFont, _printSlipTextStyle.Center, false);

                                if (__hide_repeat_order_bill == false)
                                {
                                    if (__orderCount > 0)
                                    {
                                        __source._height += this._printSlipText(__source._g, __source._width, __source._width, 0, __source._height, _getResource("สั่งเพิ่มครั้งที่ " + __orderCount.ToString()), new Font("Tahoma", 22, FontStyle.Regular), _printSlipTextStyle.Center, false);
                                    }
                                }

                                if (printCount > 1)
                                {
                                    __source._height += this._printSlipText(__source._g, __source._width, __source._width, 0, __source._height, _getResource("พิมพ์ซ่อมครั้งที่ ") + ":" + printCount.ToString(), __headerTableFont, _printSlipTextStyle.Center, false);
                                }

                                if (manualPaperFeed == true)
                                {
                                    __source._height += 150;
                                }

                                _sendPrintKitchen(printerMode, printerName, __source);
                                __source._dispose();
                                __updatePrinterStatus = true;

                                __qtyLoop++;

                                if (__print_per_unit == false)
                                    break;
                            }
                        }
                    }
                }
            }

            if (__updatePrinterStatus)
            {
                Thread.Sleep(1000);
                __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, "update " + _g.d.kitchen_master._table + " set " + _g.d.kitchen_master._kitchen_ready + "=0 where  " + _g.d.kitchen_master._code + "=\'" + kitchenCode + "\' and coalesce(" + _g.d.kitchen_master._kitchen_ready + ", 0)<>0 ");
                if (header == "** สรุปรายการ **")
                {
                    __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, "insert into " + _g.d.kitchen_print_count._table + " (" + _g.d.kitchen_print_count._doc_no + "," + _g.d.kitchen_print_count._doc_date + "," + _g.d.kitchen_print_count._kitchen_code + "," + _g.d.kitchen_print_count._print_count + ") values (\'" + docNo + "\', \'" + docDate + "\', \'" + kitchenCode + "\', " + __billCount + ")");
                }
            }
        }

        private void _sendPrintKitchen(int printerMode, string printerName, MyLib._printRaw source)
        {
            switch (printerMode)
            {
                case 0:
                    {
                        try
                        {
                            PrintDocument __pd = new PrintDocument();
                            __pd.PrinterSettings.PrinterName = printerName;
                            __pd.PrintPage += (s1, e1) =>
                            {
                                e1.Graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                                e1.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                                e1.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                                e1.Graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                                e1.Graphics.PageUnit = GraphicsUnit.Pixel;
                                e1.Graphics.DrawImageUnscaled(source._bitmap, 0, 0);
                            };
                            __pd.Print();
                        }
                        catch (Exception __ex)
                        {
                            MessageBox.Show(__ex.Message);
                        }
                    }
                    break;
                case 1:
                    {
                        try
                        {
                            source._print(printerName);
                            source._print(printerName, new byte[] { 29, 86, 66, 10 });
                        }
                        catch (Exception __ex)
                        {
                            MessageBox.Show(__ex.Message);
                        }
                    }
                    break;
                case 2:
                    {
                        IPEndPoint __ip = new IPEndPoint(IPAddress.Parse(printerName), 9100);
                        Socket __socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                        __socket.NoDelay = false;
                        __socket.Connect(__ip);
                        __socket.Send(new byte[] { 27, 64 }); // Initialize printer
                        __socket.Send(source._getDataBytes());

                        __socket.Send(new byte[] { 10 });
                        __socket.Send(new byte[] { 29, 86, 66, 10 }); // Cut
                        __socket.Shutdown(SocketShutdown.Both);
                        __socket.Close();
                    }
                    break;
            }
        }

        public void _printCancelOrderKitchen(string cancelGuid, string docDate, string docTime, string kitchenCode, string kitchenName, DataTable itemCancel, string header, Font headerFont, Font subHeaderFont, Font detailFont, int __printSum, float __paperwidth, int printerMode, string printerName)
        {
            string __saleNameField = "sale_name";
            string __docDateField = "order_doc_date";
            string __docTimeField = "order_doc_time";

            MyLib._printRaw __source = null;

            Boolean __printHead = false;
            int __countLine = 1;
            Boolean __isPrint = false;
            if (__printSum == 1)
            {
                // พิมพ์แบบรวม

                for (int __row = 0; __row < itemCancel.Rows.Count; __row++)
                {
                    string __itemCode = itemCancel.Rows[__row][_g.d.table_order_cancel._item_code].ToString().Trim();
                    string __kitchenCode2 = itemCancel.Rows[__row][_g.d.table_order._kitchen_code].ToString().Trim();
                    string __saleCode = itemCancel.Rows[__row][_g.d.table_order_cancel._sale_code].ToString().Trim();
                    string __saleName = itemCancel.Rows[__row][_g.d.table_order_cancel._sale_name].ToString().Trim();
                    string __tableNumber = itemCancel.Rows[__row][_g.d.table_order_cancel._table_number].ToString().Trim();

                    string __docDate = MyLib._myGlobal._convertDateFromQuery(itemCancel.Rows[__row][__docDateField].ToString().Trim()).ToString("yyyy-MM-dd", new CultureInfo("en-US"));
                    string __docTime = itemCancel.Rows[__row][__docTimeField].ToString().Trim();

                    if (__itemCode.Length > 0 && kitchenCode.Length > 0)
                    {
                        if (kitchenCode.Equals(__kitchenCode2))
                        {
                            __isPrint = true;
                            if (__printHead == false)
                            {
                                __printHead = true;

                                __source = new MyLib._printRaw(__paperwidth, 20000); // __printRaw[__printRaw.Count - 1];

                                __source._height = 0;
                                __source._height += this._printSlipText(__source._g, __source._width, __source._width, 0, __source._height, (header), __headerFont, _printSlipTextStyle.Center, false);
                                __source._height += this._printSlipText(__source._g, __source._width, __source._width, 0, __source._height, _getResource("โต๊ะ") + ":" + __tableNumber, __headerTableFont, _printSlipTextStyle.Center, false);
                                __source._height += this._printSlipText(__source._g, __source._width, __source._width, 0, __source._height, _getResource("ยกเลิก"), __headerTableFont, _printSlipTextStyle.Center, false);
                                __source._height += this._printSlipText(__source._g, __source._width, __source._width, 0, __source._height, __saleCode + ":" + __saleName, __subHeaderFont, _printSlipTextStyle.Center, false);
                                __source._height += this._printSlipText(__source._g, __source._width, __source._width, 0, __source._height, __docDate + ":" + __docTime, __subHeaderFont, _printSlipTextStyle.Center, false);
                            }
                            {
                                // รายการอาหาร/เครื่องดื่ม
                                //MyLib._printRaw __source = __printRaw[__printRaw.Count - 1];
                                string __itemName = itemCancel.Rows[__row][_g.d.table_order_cancel._item_name].ToString();
                                string __itemUnit = itemCancel.Rows[__row]["unit_name"].ToString(); //itemCancel.Rows[__row][_g.d.table_order_cancel._unit_code].ToString();
                                decimal __itemQty = MyLib._myGlobal._decimalPhase(itemCancel.Rows[__row][_g.d.table_order_cancel._qty].ToString());
                                //string __itemRemark = this._itemCancelGrid._cellGet(__row, _g.d.table_order._remark).ToString().Trim();
                                StringBuilder __desc = new StringBuilder(__countLine.ToString() + "." + _getResource("ยกเลิก : ") + __itemName + " " + "@" + " " + __itemQty.ToString("N0") + " " + __itemUnit);
                                //if (__itemRemark.Length > 0)
                                //{
                                //    __desc.Append(" ");
                                //    __desc.Append(__itemRemark);
                                //}
                                __source._height += this._printSlipText(__source._g, __source._width, __source._width, 0, __source._height, __desc.ToString(), __detailFont, _printSlipTextStyle.Left, true);
                                __countLine++;
                            }
                            //
                            if (__printSum == 0)
                            {
                                __printHead = false;
                                __countLine = 1;
                            }
                        }
                    }


                }

                // send to printer
                if (__isPrint == true)
                {
                    _sendPrintKitchen(printerMode, printerName, __source);
                    __source._dispose();
                }

            }
            else
            {

                for (int __row = 0; __row < itemCancel.Rows.Count; __row++)
                {
                    string __itemCode = itemCancel.Rows[__row][_g.d.table_order_cancel._item_code].ToString().Trim();
                    string __kitchenCode2 = itemCancel.Rows[__row][_g.d.table_order._kitchen_code].ToString().Trim();
                    string __saleCode = itemCancel.Rows[__row][_g.d.table_order_cancel._sale_code].ToString().Trim();
                    string __saleName = itemCancel.Rows[__row][_g.d.table_order_cancel._sale_name].ToString().Trim();
                    string __tableNumber = itemCancel.Rows[__row][_g.d.table_order_cancel._table_number].ToString().Trim();

                    string __docDate = MyLib._myGlobal._convertDateFromQuery(itemCancel.Rows[__row][__docDateField].ToString().Trim()).ToString("yyyy-MM-dd", new CultureInfo("en-US"));
                    string __docTime = itemCancel.Rows[__row][__docTimeField].ToString().Trim();

                    if (__itemCode.Length > 0 && kitchenCode.Length > 0)
                    {
                        if (kitchenCode.Equals(__kitchenCode2))
                        {
                            //if (__printHead == false)
                            {
                                //__printHead = true;
                                //__printRaw.Add(new MyLib._printRaw(__paperwidth, 20000));
                                __source = new MyLib._printRaw(__paperwidth, 20000); // __printRaw[__printRaw.Count - 1];
                                __source._height = 0;
                                __source._height += this._printSlipText(__source._g, __source._width, __source._width, 0, __source._height, _getResource(header), __headerFont, _printSlipTextStyle.Center, false);
                                __source._height += this._printSlipText(__source._g, __source._width, __source._width, 0, __source._height, _getResource("โต๊ะ") + ":" + __tableNumber, __headerTableFont, _printSlipTextStyle.Center, false);
                                __source._height += this._printSlipText(__source._g, __source._width, __source._width, 0, __source._height, _getResource("ยกเลิก"), __headerTableFont, _printSlipTextStyle.Center, false);
                                __source._height += this._printSlipText(__source._g, __source._width, __source._width, 0, __source._height, __saleCode + ":" + __saleName, __subHeaderFont, _printSlipTextStyle.Center, false);
                                __source._height += this._printSlipText(__source._g, __source._width, __source._width, 0, __source._height, __docDate + ":" + __docTime, __subHeaderFont, _printSlipTextStyle.Center, false);
                            }
                            {
                                // รายการอาหาร/เครื่องดื่ม
                                //MyLib._printRaw __source = __printRaw[__printRaw.Count - 1];
                                string __itemName = itemCancel.Rows[__row][_g.d.table_order_cancel._item_name].ToString();
                                string __itemUnit = itemCancel.Rows[__row]["unit_name"].ToString();  //itemCancel.Rows[__row][_g.d.table_order_cancel._unit_code].ToString();
                                decimal __itemQty = MyLib._myGlobal._decimalPhase(itemCancel.Rows[__row][_g.d.table_order_cancel._qty].ToString());
                                //string __itemRemark = this._itemCancelGrid._cellGet(__row, _g.d.table_order._remark).ToString().Trim();
                                StringBuilder __desc = new StringBuilder(__countLine.ToString() + "." + _getResource("ยกเลิก : ") + __itemName + " " + "@" + " " + __itemQty.ToString("N0") + " " + __itemUnit);
                                //if (__itemRemark.Length > 0)
                                //{
                                //    __desc.Append(" ");
                                //    __desc.Append(__itemRemark);
                                //}
                                __source._height += this._printSlipText(__source._g, __source._width, __source._width, 0, __source._height, __desc.ToString(), __detailFont, _printSlipTextStyle.Left, true);
                                __countLine++;
                            }
                            //
                            //if (__printSum == 0)
                            {
                                __printHead = false;
                                __countLine = 1;
                            }

                            // send kitchen
                            _sendPrintKitchen(printerMode, printerName, __source);

                            __source._dispose();
                            Thread.Sleep(1000);


                        }
                    }
                }
            }
        }

        public void _printCancelOrder(string cancelGuid, string docDate, string docTime)
        {
            string __saleNameField = "sale_name";
            string __docDateField = "order_doc_date";
            string __docTimeField = "order_doc_time";
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();

            StringBuilder _myQuery = new StringBuilder();
            _myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");
            _myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.kitchen_master._table + " where " + _g.d.kitchen_master._print_to_kitchen + "=1 order by " + _g.d.kitchen_master._code));
            _myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("select *" + 
                ", (select " + _g.d.erp_user._name_1 + " from " + _g.d.erp_user._table + " where upper(" + _g.d.erp_user._table + "." + _g.d.erp_user._code + ") = upper(" + _g.d.table_order_cancel._table + "." + _g.d.table_order_cancel._sale_code + ")) as " + __saleNameField +
                ", (select " + _g.d.table_order._table + "." + _g.d.table_order._kitchen_code + " from " + _g.d.table_order._table + " where " + _g.d.table_order._table + "." + _g.d.table_order._guid_line + "=" + _g.d.table_order_cancel._table + "." + _g.d.table_order_cancel._guid_line + " limit 1 ) as " + _g.d.table_order._kitchen_code +
                ", (select name_1 from ic_inventory where code=table_order_cancel.item_code) as " + _g.d.table_order_cancel._item_name + " " +
                ", (select " + _g.d.table_order._doc_date + " from " + _g.d.table_order._table + " where " + _g.d.table_order_cancel._table + "." + _g.d.table_order_cancel._guid_line + "=table_order.guid_line ) as " + __docDateField + " " +
                ", (select " + _g.d.table_order._doc_time + " from " + _g.d.table_order._table + " where " + _g.d.table_order_cancel._table + "." + _g.d.table_order_cancel._guid_line + "=table_order.guid_line ) as " + __docTimeField + " " +
                ", (select name_1 from ic_unit where code=table_order_cancel.unit_code) as unit_name " +
                " from " + _g.d.table_order_cancel._table + " where " + _g.d.table_order_cancel._cancel_guid + "=\'" + cancelGuid + "\' order by " + _g.d.table_order_cancel._line_number));

            _myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.kitchen_print_cancel_copy._table + " where " + _g.d.kitchen_print_cancel_copy._status + "=1 order by " + _g.d.kitchen_print_cancel_copy._kitchen_code));


            _myQuery.Append("</node>");

            ArrayList __kitchenResult = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, _myQuery.ToString());
            DataTable __kitchen = ((DataSet)__kitchenResult[0]).Tables[0];

            DataTable __itemCancel = ((DataSet)__kitchenResult[1]).Tables[0];
            DataTable __kitchenCopyPrint = ((DataSet)__kitchenResult[2]).Tables[0];


            for (int __kitchenLoop = 0; __kitchenLoop < __kitchen.Rows.Count; __kitchenLoop++)
            {

                // set font ก่อน
                //Font __headerFont = new Font("Tahoma", 12f, FontStyle.Bold);
                //Font __subHeaderFont = new Font("Tahoma", 10f, FontStyle.Regular);
                //Font __detailFont = new Font("Tahoma", 12f, FontStyle.Bold);

                try
                {
                    if (__kitchen.Rows[__kitchenLoop][_g.d.kitchen_master._slip_header_font] != null)
                    {
                        string __headerFontStr = __kitchen.Rows[__kitchenLoop][_g.d.kitchen_master._slip_header_font].ToString().Trim();
                        if (__headerFontStr.Length > 0)
                        {
                            __headerFont = new Font(__headerFontStr.Split(',')[0].ToString(), (float)MyLib._myGlobal._decimalPhase(__headerFontStr.Split(',')[1].ToString()), FontStyle.Regular);
                            __headerTableFont = new Font(__headerFontStr.Split(',')[0].ToString(), (float)MyLib._myGlobal._decimalPhase(__headerFontStr.Split(',')[1].ToString()) * 2, FontStyle.Regular);
                        }
                    }

                    if (__kitchen.Rows[__kitchenLoop][_g.d.kitchen_master._slip_sub_header_font] != null)
                    {
                        string __subHeaderFontStr = __kitchen.Rows[__kitchenLoop][_g.d.kitchen_master._slip_sub_header_font].ToString().Trim();
                        if (__subHeaderFontStr.Length > 0)
                        {
                            __subHeaderFont = new Font(__subHeaderFontStr.Split(',')[0].ToString(), (float)MyLib._myGlobal._decimalPhase(__subHeaderFontStr.Split(',')[1].ToString()), FontStyle.Regular);
                        }
                    }

                    if (__kitchen.Rows[__kitchenLoop][_g.d.kitchen_master._slip_detail_font] != null)
                    {
                        string __detailFontStr = __kitchen.Rows[__kitchenLoop][_g.d.kitchen_master._slip_detail_font].ToString().Trim();
                        if (__detailFontStr.Length > 0)
                        {
                            __detailFont = new Font(__detailFontStr.Split(',')[0].ToString(), (float)MyLib._myGlobal._decimalPhase(__detailFontStr.Split(',')[1].ToString()), FontStyle.Regular);
                        }
                    }
                }
                catch
                {
                }

                //List<MyLib._printRaw> __printRaw = new List<MyLib._printRaw>();

                MyLib._printRaw __source = null;
                string __kitchenCode = __kitchen.Rows[__kitchenLoop][_g.d.kitchen_master._code].ToString();
                string __kitchenName = __kitchen.Rows[__kitchenLoop][_g.d.kitchen_master._name_1].ToString();
                int __printerMode = (int)MyLib._myGlobal._decimalPhase(__kitchen.Rows[__kitchenLoop][_g.d.kitchen_master._printer_mode].ToString());
                string __printerName = __kitchen.Rows[__kitchenLoop][_g.d.kitchen_master._printer_name].ToString();
                int __printSum = (int)MyLib._myGlobal._decimalPhase(__kitchen.Rows[__kitchenLoop][_g.d.kitchen_master._print_summery].ToString());
                float __paperwidth = (float)MyLib._myGlobal._decimalPhase(__kitchen.Rows[__kitchenLoop][_g.d.kitchen_master._printer_slip_width].ToString());

                this._languageIndexPrint = MyLib._myGlobal._intPhase(__kitchen.Rows[__kitchenLoop][_g.d.kitchen_master._print_language_index].ToString());


                Boolean __billCopyPrint = __kitchen.Rows[__kitchenLoop][_g.d.kitchen_master._print_copy].ToString().Equals("1") ? true : false;
                int __printerCopyMode = (int)MyLib._myGlobal._decimalPhase(__kitchen.Rows[__kitchenLoop][_g.d.kitchen_master._print_copy_mode].ToString());
                string __printerCopyName = __kitchen.Rows[__kitchenLoop][_g.d.kitchen_master._print_copy_name].ToString();

                this._printCancelOrderKitchen(cancelGuid, docDate, docTime, __kitchenCode, __kitchenName, __itemCancel, _getResource("ใบยกเลิก") + ":" + __kitchenName, __headerFont, __subHeaderFont, __detailFont, __printSum, __paperwidth, __printerMode, __printerName);

                if (__billCopyPrint)
                {
                    this._printCancelOrderKitchen(cancelGuid, docDate, docTime, __kitchenCode, __kitchenName, __itemCancel, _getResource("สำเนาใบยกเลิก") + ":" + __kitchenName, __headerFont, __subHeaderFont, __detailFont, 1, __paperwidth, __printerCopyMode, __printerCopyName);
                }

                DataRow[] __printCopy2 = (__kitchenCopyPrint.Rows.Count == 0) ? null : __kitchenCopyPrint.Select(_g.d.kitchen_print_cancel_copy._kitchen_code + "=\'" + __kitchenCode + "\'");
                if (__printCopy2 != null && __printCopy2.Length > 0)
                {
                    for (int __copyLoop = 0; __copyLoop < __printCopy2.Length; __copyLoop++)
                    {

                        int __printCopyMode2 = MyLib._myGlobal._intPhase(__printCopy2[__copyLoop][_g.d.kitchen_print_cancel_copy._print_mode].ToString());
                        string __printCopyName2 = __printCopy2[__copyLoop][_g.d.kitchen_print_cancel_copy._print_name].ToString();

                        //this._printOrderKitchen(printCount, docNo, docDate, docTime, __kitchenCode, __kitchenName, __printCopyMode2, __printCopyName2, "สำเนา-ใบสั่งอาหาร", true, __is_manual_feed, __paperwidth, false, false, false, __item_barcode_print, true, __noPricePrint, true);
                        this._printCancelOrderKitchen(cancelGuid, docDate, docTime, __kitchenCode, __kitchenName, __itemCancel, _getResource("สำเนาใบยกเลิก") + ":" + __kitchenName, __headerFont, __subHeaderFont, __detailFont, 1, __paperwidth, __printCopyMode2, __printCopyName2);

                    }

                }

                /* ย้ายไป  _printCancelOrderKitchen
                if (__printSum == 1)
                {
                    // พิมพ์แบบรวม

                    for (int __row = 0; __row < __itemCancel.Rows.Count; __row++)
                    {
                        string __itemCode = __itemCancel.Rows[__row][_g.d.table_order_cancel._item_code].ToString().Trim();
                        string __kitchenCode2 = __itemCancel.Rows[__row][_g.d.table_order._kitchen_code].ToString().Trim();
                        string __saleCode = __itemCancel.Rows[__row][_g.d.table_order_cancel._sale_code].ToString().Trim();
                        string __saleName = __itemCancel.Rows[__row][_g.d.table_order_cancel._sale_name].ToString().Trim();
                        string __tableNumber = __itemCancel.Rows[__row][_g.d.table_order_cancel._table_number].ToString().Trim();

                        string __docDate = MyLib._myGlobal._convertDateFromQuery(__itemCancel.Rows[__row][__docDateField].ToString().Trim()).ToString("yyyy-MM-dd", new CultureInfo("en-US"));
                        string __docTime = __itemCancel.Rows[__row][__docTimeField].ToString().Trim();

                        if (__itemCode.Length > 0 && __kitchenCode.Length > 0)
                        {
                            if (__kitchenCode.Equals(__kitchenCode2))
                            {
                                __isPrint = true;
                                if (__printHead == false)
                                {
                                    __printHead = true;

                                    __source = new MyLib._printRaw(__paperwidth, 20000); // __printRaw[__printRaw.Count - 1];

                                    __source._height = 0;
                                    __source._height += this._printSlipText(__source._g, __source._width, __source._width, 0, __source._height, "ใบยกเลิก" + " : " + __kitchenName + " (" + __kitchenCode + ")", __headerFont, _printSlipTextStyle.Center, false);
                                    __source._height += this._printSlipText(__source._g, __source._width, __source._width, 0, __source._height, "โต๊ะ" + " : " + __tableNumber, __headerTableFont, _printSlipTextStyle.Center, false);
                                    __source._height += this._printSlipText(__source._g, __source._width, __source._width, 0, __source._height, __saleCode + " : " + __saleName, __subHeaderFont, _printSlipTextStyle.Center, false);
                                    __source._height += this._printSlipText(__source._g, __source._width, __source._width, 0, __source._height, __docDate + " : " + __docTime, __subHeaderFont, _printSlipTextStyle.Center, false);
                                }
                                {
                                    // รายการอาหาร/เครื่องดื่ม
                                    //MyLib._printRaw __source = __printRaw[__printRaw.Count - 1];
                                    string __itemName = __itemCancel.Rows[__row][_g.d.table_order_cancel._item_name].ToString();
                                    string __itemUnit = __itemCancel.Rows[__row][_g.d.table_order_cancel._unit_code].ToString();
                                    decimal __itemQty = MyLib._myGlobal._decimalPhase(__itemCancel.Rows[__row][_g.d.table_order_cancel._qty].ToString());
                                    //string __itemRemark = this._itemCancelGrid._cellGet(__row, _g.d.table_order._remark).ToString().Trim();
                                    StringBuilder __desc = new StringBuilder(__countLine.ToString() + "." + __itemName + " " + "@" + " " + __itemQty.ToString("N0") + " " + __itemUnit);
                                    //if (__itemRemark.Length > 0)
                                    //{
                                    //    __desc.Append(" ");
                                    //    __desc.Append(__itemRemark);
                                    //}
                                    __source._height += this._printSlipText(__source._g, __source._width, __source._width, 0, __source._height, __desc.ToString(), __detailFont, _printSlipTextStyle.Left, true);
                                    __countLine++;
                                }
                                //
                                if (__printSum == 0)
                                {
                                    __printHead = false;
                                    __countLine = 1;
                                }
                            }
                        }


                    }

                    // send to printer
                    if (__isPrint == true)
                    {
                        _sendPrintKitchen(__printerMode, __printerName, __source);

                        if (__billCopyPrint)
                        {
                            _sendPrintKitchen(__printerCopyMode, __printerCopyName, __source);
                        }
                        __source._dispose();


                    }

                }
                else
                {

                    for (int __row = 0; __row < __itemCancel.Rows.Count; __row++)
                    {
                        string __itemCode = __itemCancel.Rows[__row][_g.d.table_order_cancel._item_code].ToString().Trim();
                        string __kitchenCode2 = __itemCancel.Rows[__row][_g.d.table_order._kitchen_code].ToString().Trim();
                        string __saleCode = __itemCancel.Rows[__row][_g.d.table_order_cancel._sale_code].ToString().Trim();
                        string __saleName = __itemCancel.Rows[__row][_g.d.table_order_cancel._sale_name].ToString().Trim();
                        string __tableNumber = __itemCancel.Rows[__row][_g.d.table_order_cancel._table_number].ToString().Trim();

                        string __docDate = MyLib._myGlobal._convertDateFromQuery(__itemCancel.Rows[__row][__docDateField].ToString().Trim()).ToString("yyyy-MM-dd", new CultureInfo("en-US"));
                        string __docTime = __itemCancel.Rows[__row][__docTimeField].ToString().Trim();

                        if (__itemCode.Length > 0 && __kitchenCode.Length > 0)
                        {
                            if (__kitchenCode.Equals(__kitchenCode2))
                            {
                                //if (__printHead == false)
                                {
                                    //__printHead = true;
                                    //__printRaw.Add(new MyLib._printRaw(__paperwidth, 20000));
                                    __source = new MyLib._printRaw(__paperwidth, 20000); // __printRaw[__printRaw.Count - 1];
                                    __source._height = 0;
                                    __source._height += this._printSlipText(__source._g, __source._width, __source._width, 0, __source._height, "ใบยกเลิก" + " : " + __kitchenName + " (" + __kitchenCode + ")", __headerFont, _printSlipTextStyle.Center, false);
                                    __source._height += this._printSlipText(__source._g, __source._width, __source._width, 0, __source._height, "โต๊ะ" + " : " + __tableNumber, __headerTableFont, _printSlipTextStyle.Center, false);
                                    __source._height += this._printSlipText(__source._g, __source._width, __source._width, 0, __source._height, __saleCode + " : " + __saleName, __subHeaderFont, _printSlipTextStyle.Center, false);
                                    __source._height += this._printSlipText(__source._g, __source._width, __source._width, 0, __source._height, __docDate + " : " + __docTime, __subHeaderFont, _printSlipTextStyle.Center, false);
                                }
                                {
                                    // รายการอาหาร/เครื่องดื่ม
                                    //MyLib._printRaw __source = __printRaw[__printRaw.Count - 1];
                                    string __itemName = __itemCancel.Rows[__row][_g.d.table_order_cancel._item_name].ToString();
                                    string __itemUnit = __itemCancel.Rows[__row][_g.d.table_order_cancel._unit_code].ToString();
                                    decimal __itemQty = MyLib._myGlobal._decimalPhase(__itemCancel.Rows[__row][_g.d.table_order_cancel._qty].ToString());
                                    //string __itemRemark = this._itemCancelGrid._cellGet(__row, _g.d.table_order._remark).ToString().Trim();
                                    StringBuilder __desc = new StringBuilder(__countLine.ToString() + "." + __itemName + " " + "@" + " " + __itemQty.ToString("N0") + " " + __itemUnit);
                                    //if (__itemRemark.Length > 0)
                                    //{
                                    //    __desc.Append(" ");
                                    //    __desc.Append(__itemRemark);
                                    //}
                                    __source._height += this._printSlipText(__source._g, __source._width, __source._width, 0, __source._height, __desc.ToString(), __detailFont, _printSlipTextStyle.Left, true);
                                    __countLine++;
                                }
                                //
                                //if (__printSum == 0)
                                {
                                    __printHead = false;
                                    __countLine = 1;
                                }

                                // send kitchen
                                _sendPrintKitchen(__printerMode, __printerName, __source);

                                __source._dispose();
                                Thread.Sleep(3000);


                            }
                        }
                    }
                }*/

                // Print'
                /*
                for (int __loop = 0; __loop < __printRaw.Count; __loop++)
                {
                    switch (__printerMode)
                    {
                        case 0:
                            {
                                try
                                {
                                    PrintDocument __pd = new PrintDocument();
                                    __pd.PrinterSettings.PrinterName = __printerName;
                                    __pd.PrintPage += (s1, e1) =>
                                    {
                                        e1.Graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                                        e1.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                                        e1.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                                        e1.Graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                                        e1.Graphics.PageUnit = GraphicsUnit.Pixel;
                                        e1.Graphics.DrawImageUnscaled(__printRaw[__loop]._bitmap, 0, 0);
                                    };
                                    __pd.Print();
                                }
                                catch (Exception __ex)
                                {
                                    MessageBox.Show(__ex.Message);
                                }
                            }
                            break;
                        case 1:
                            {
                                try
                                {
                                    __printRaw[__loop]._print(__printerName);
                                    __printRaw[__loop]._print(__printerName, new byte[] { 29, 86, 66, 10 });
                                }
                                catch (Exception __ex)
                                {
                                    MessageBox.Show(__ex.Message);
                                }
                            }
                            break;
                        case 2:
                            {
                                IPEndPoint __ip = new IPEndPoint(IPAddress.Parse(__printerName), 9100);
                                Socket __socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                                __socket.Connect(__ip);
                                __socket.Send(new byte[] { 27, 64 }); // Initialize printer
                                __socket.Send(__printRaw[__loop]._getDataBytes());
                                __socket.Send(new byte[] { 10 });
                                __socket.Send(new byte[] { 29, 86, 66, 10 }); // Cut
                                __socket.Shutdown(SocketShutdown.Both);
                                __socket.Close();
                            }
                            break;
                    }
                }
                */
            }
        }


        public void _printCustomerTable(string tableNumber)
        {
            // select trans_guid, customer_count
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();


            StringBuilder _myQuery = new StringBuilder();
            _myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");
            _myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.kitchen_master._table + " where " + _g.d.kitchen_master._print_tableopen + "=1 order by " + _g.d.kitchen_master._code));
            _myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("select *" +
                ", (select " + _g.d.table_trans._customer_count + " from  " + _g.d.table_trans._table + " where " + _g.d.table_trans._table + "." + _g.d.table_trans._table_number + " = " + _g.d.table_master._table + "." + _g.d.table_master._number + " and " + _g.d.table_master._table + "." + _g.d.table_master._trans_guid + " = " + _g.d.table_trans._table + "." + _g.d.table_trans._trans_guid + " ) as " + _g.d.table_trans._customer_count +
                ", (select " + _g.d.table_trans._open_sale_code + " from  " + _g.d.table_trans._table + " where " + _g.d.table_trans._table + "." + _g.d.table_trans._table_number + " = " + _g.d.table_master._table + "." + _g.d.table_master._number + " and " + _g.d.table_master._table + "." + _g.d.table_master._trans_guid + " = " + _g.d.table_trans._table + "." + _g.d.table_trans._trans_guid + " ) as " + _g.d.table_trans._open_sale_code +
                ",(select name_1 from " + _g.d.erp_user._table + " where " + _g.d.erp_user._code + " = (select " + _g.d.table_trans._open_sale_code + " from  " + _g.d.table_trans._table + " where " + _g.d.table_trans._table + "." + _g.d.table_trans._table_number + " = " + _g.d.table_master._table + "." + _g.d.table_master._number + " and " + _g.d.table_master._table + "." + _g.d.table_master._trans_guid + " = " + _g.d.table_trans._table + "." + _g.d.table_trans._trans_guid + " )) as sale_name " +
                " from " + _g.d.table_master._table + " where " + _g.d.table_master._number + "=\'" + tableNumber + "\'"));


            _myQuery.Append("</node>");

            ArrayList __result = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, _myQuery.ToString());
            DataTable __kitchen = ((DataSet)__result[0]).Tables[0];
            DataTable __tableOpenTable = ((DataSet)__result[1]).Tables[0];

            string __tableNumber = __tableOpenTable.Rows[0][_g.d.table_master._number].ToString();
            string __saleCode = __tableOpenTable.Rows[0][_g.d.table_trans._open_sale_code].ToString();
            string __saleName = __tableOpenTable.Rows[0]["sale_name"].ToString();
            string __openDate = MyLib._myGlobal._convertDateFromQuery(__tableOpenTable.Rows[0][_g.d.table_master._open_date].ToString()).ToString("dd/MM/yyyy", new CultureInfo("th-TH"));
            string __openTime = __tableOpenTable.Rows[0][_g.d.table_master._open_time].ToString();
            string __slipHeader = "ใบเปิดโต๊ะ";
            string __customerCount = __tableOpenTable.Rows[0][_g.d.table_trans._customer_count].ToString();

            // send print
            for (int __kitchenLoop = 0; __kitchenLoop < __kitchen.Rows.Count; __kitchenLoop++)
            {
                string __kitchenCode = __kitchen.Rows[__kitchenLoop][_g.d.kitchen_master._code].ToString();
                string __kitchenName = __kitchen.Rows[__kitchenLoop][_g.d.kitchen_master._name_1].ToString();
                int __printerMode = (int)MyLib._myGlobal._decimalPhase(__kitchen.Rows[__kitchenLoop][_g.d.kitchen_master._printer_mode].ToString());
                bool __is_manual_feed = __kitchen.Rows[__kitchenLoop][_g.d.kitchen_master._printer_manual_feed].ToString().Equals("1") ? true : false;
                float __paperwidth = (float)MyLib._myGlobal._decimalPhase(__kitchen.Rows[__kitchenLoop][_g.d.kitchen_master._printer_slip_width].ToString());
                string __printerName = __kitchen.Rows[__kitchenLoop][_g.d.kitchen_master._printer_name].ToString();

                this._languageIndexPrint = MyLib._myGlobal._intPhase(__kitchen.Rows[__kitchenLoop][_g.d.kitchen_master._print_language_index].ToString());

                // set font ก่อน
                try
                {
                    if (__kitchen.Rows[__kitchenLoop][_g.d.kitchen_master._slip_header_font] != null)
                    {
                        string __headerFontStr = __kitchen.Rows[__kitchenLoop][_g.d.kitchen_master._slip_header_font].ToString().Trim();
                        if (__headerFontStr.Length > 0)
                        {
                            __headerFont = new Font(__headerFontStr.Split(',')[0].ToString(), (float)MyLib._myGlobal._decimalPhase(__headerFontStr.Split(',')[1].ToString()), FontStyle.Regular);
                            __headerTableFont = new Font(__headerFontStr.Split(',')[0].ToString(), (float)MyLib._myGlobal._decimalPhase(__headerFontStr.Split(',')[1].ToString()) * 2, FontStyle.Regular);
                        }
                    }

                    if (__kitchen.Rows[__kitchenLoop][_g.d.kitchen_master._slip_sub_header_font] != null)
                    {
                        string __subHeaderFontStr = __kitchen.Rows[__kitchenLoop][_g.d.kitchen_master._slip_sub_header_font].ToString().Trim();
                        if (__subHeaderFontStr.Length > 0)
                        {
                            __subHeaderFont = new Font(__subHeaderFontStr.Split(',')[0].ToString(), (float)MyLib._myGlobal._decimalPhase(__subHeaderFontStr.Split(',')[1].ToString()), FontStyle.Regular);
                        }
                    }

                    if (__kitchen.Rows[__kitchenLoop][_g.d.kitchen_master._slip_detail_font] != null)
                    {
                        string __detailFontStr = __kitchen.Rows[__kitchenLoop][_g.d.kitchen_master._slip_detail_font].ToString().Trim();
                        if (__detailFontStr.Length > 0)
                        {
                            __detailFont = new Font(__detailFontStr.Split(',')[0].ToString(), (float)MyLib._myGlobal._decimalPhase(__detailFontStr.Split(',')[1].ToString()), FontStyle.Regular);
                        }
                    }
                }
                catch
                {
                }

                try
                {
                    // print
                    if (int.Parse(__customerCount) > 0)
                    {
                        MyLib._printRaw __source = __source = new MyLib._printRaw(__paperwidth, 5000);
                        __source._height += this._printSlipText(__source._g, __source._width, __source._width, 0, __source._height, _getResource(__slipHeader), __headerFont, _printSlipTextStyle.Center, false);
                        __source._height += this._printSlipText(__source._g, __source._width, __source._width, 0, __source._height, _getResource("โต๊ะ") + ":" + __tableNumber, __headerTableFont, _printSlipTextStyle.Center, false);
                        __source._height += this._printSlipText(__source._g, __source._width, __source._width, 0, __source._height, __saleCode + ":" + __saleName, __subHeaderFont, _printSlipTextStyle.Center, false);
                        __source._height += this._printSlipText(__source._g, __source._width, __source._width, 0, __source._height, __openDate + ":" + __openTime, __subHeaderFont, _printSlipTextStyle.Center, false);
                        __source._height += this._printSlipText(__source._g, __source._width, __source._width, 0, __source._height, _getResource("จำนวนลูกค้า") + ":" + __customerCount + " " + _getResource("คน"), __subHeaderFont, _printSlipTextStyle.Center, false);
                        _sendPrintKitchen(__printerMode, __printerName, __source);
                        __source._dispose();
                    }
                }
                catch
                {

                }

            }

        }
    }
}
