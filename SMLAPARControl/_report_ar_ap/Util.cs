using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace SMLERPAPARControl._report_ar_ap
{
    class Util
    {
        public static float _line;
        public static string _company = "บริษัท เอสเอ็มแอล ซอฟต์ จำกัด";
        public static string _address = "32 ถ. หัสดิเสวี ต.ช้างเผือก";
        public static string _address_x = "อ.เมือง จ.เชียงใหม่ 50000";
        public static string _tel = "08-3581-9195";
        public static string _reportname = "ใบเสร็จรับเงิน/ใบกำกับภาษี";
        public static string _tax_invoice = "08-3581-9195";
        //-------------------------------------------------------------
        public static string _ap_ar_code = "5211-0001";
        public static string _ap_ar_name = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
        public static string _ap_ar_address = "32 ถ. หัสดิเสวี ต.ช้างเผือก";
        public static string _ap_ar_address_x = "อ.เมือง จ.เชียงใหม่ 50000";
        public static string _ap_ar_address_t = "08-3581-9195";
        public static string _ap_ar_doc_no = "5211-50000";
        public static string _ap_ar_doc_date = DateTime.Now.ToShortDateString();
        public static string _ap_ar_sale_code = ">>>";
        //-------------------------------------------------------------

        public static int noOfItems;
        public static int currentItem;

        public static int translateInt(object column)
        {
            if (column == DBNull.Value)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(column);
            }
        }


        public static bool translateBool(object column)
        {
            if (column == DBNull.Value)
            {
                return false;
            }
            else
            {
                return Convert.ToBoolean(column);
            }
        }

        public static decimal translateDecimal(object column)
        {
            if (column == DBNull.Value)
            {
                return 0;
            }
            else
            {
                return Convert.ToDecimal(column);
            }
        }

        public static double translateDouble(object column)
        {
            if (column == DBNull.Value)
            {
                return 0;
            }
            else
            {
                return Convert.ToDouble(column);
            }
        }

        public static DateTime translateDate(object column)
        {
            if (column == DBNull.Value)
            {
                return DateTime.MinValue;
            }
            else
            {
                return Convert.ToDateTime(column);
            }
        }

        public static DateTime combineDateTime(object columnDate, object columnTime)
        {
            DateTime retVal = DateTime.MinValue;
            if (columnDate != DBNull.Value)
            {
                retVal = Convert.ToDateTime(columnDate).Date;
            }
            if (columnTime != DBNull.Value)
            {
                DateTime timeVal = Convert.ToDateTime(columnTime);
                retVal = retVal.Add(timeVal.TimeOfDay);
            }
            return retVal;
        }

        public static string translateString(object column)
        {
            if (column == DBNull.Value)
            {
                return "";
            }
            else
            {
                return column.ToString();
            }
        }

        public static bool confirm(string message)
        {
            return (MessageBox.Show(message, "Please Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes);
        }

        public static DialogResult confirmWithCancel(string message)
        {
            return MessageBox.Show(message, "Please Confirm", MessageBoxButtons.YesNoCancel);
        }

        public static float printLine(Graphics e
            , String line
            , Font theFont
            , float xPos
            , float yPos
            , float maxWidth
            , bool rightAlign
            , System.Drawing.Brush printColour
            )
        {
            float retVal = 0;
            if (line != "")
            {
                StringFormat theFormat = new StringFormat();
                float lineHeight = theFont.GetHeight(e);
                System.Drawing.RectangleF rect = new RectangleF(xPos, yPos, maxWidth, lineHeight);
                if (rightAlign)
                {
                    theFormat.FormatFlags = StringFormatFlags.DirectionRightToLeft;
                }
                rect = calcTextRectangle(
                    e
                    , theFont
                    , xPos
                    , yPos
                    , maxWidth
                    , line);
                e.DrawString(line, theFont, printColour, rect, theFormat);
                retVal = rect.Height;
            }
            return retVal;
        }

        public static System.Drawing.RectangleF calcTextRectangle(
            Graphics e
            , Font theFont
            , float xPos
            , float yPos
            , float maxWidth
            , string text)
        {
            int noOfLines = noOfPrintLines(
                e
                , theFont
                , maxWidth
                , text);
            float lineHeight = theFont.GetHeight(e);
            lineHeight = lineHeight * noOfLines;
            System.Drawing.RectangleF rect = new RectangleF(xPos, yPos, maxWidth, lineHeight);
            return rect;
        }

        public static int noOfPrintLines(
            Graphics e
            , Font theFont
            , float maxWidth
            , string text)
        {
            string[] textSplit = text.Split("\n\r".ToCharArray());
            System.Drawing.SizeF charSize = e.MeasureString("A", theFont);
            int totalNoOfLines = 0;
            for (int i = 0; i < textSplit.Length; i++)
            {
                if ((textSplit[i] != "\n")
                    && (textSplit[i] != "\r")
                    && (textSplit[i] != ""))
                {
                    System.Drawing.SizeF size = e.MeasureString(textSplit[i], theFont);
                    float noOfLines = size.Width / maxWidth;
                    noOfLines = (float)Math.Ceiling(noOfLines);
                    totalNoOfLines += Convert.ToInt32(noOfLines);
                }
            }
            return totalNoOfLines;
        }
    }
}
