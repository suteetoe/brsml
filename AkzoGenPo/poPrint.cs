using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using PdfSharp.Pdf;
using PdfSharp.Drawing;

namespace AkzoGenPo
{
    public class poPrint : SMLReport._formReport._formPrint
    {
        string __localPathName = MyLib._myGlobal._smlConfigFile + AkzoGlobal._global._poFormFileName;
        _podesign _form = null;

        public poPrint()
        {
            this._form = new _podesign();
            this._form._buildForm();
        }

        public Boolean _print(string __docNo)
        {
            // query ส่ง
            Boolean __result = false;

            _poFormPrint __print = new _poFormPrint();
            __print.formDesign = this._form;
            __print._printRangeType = System.Drawing.Printing.PrintRange.AllPages;
            __print._includeDocSeries = true;
            //__print._query();

            // ส่ง datatable แทน
            DataSet __ds = new DataSet();

            SqlConnection __conn = AkzoGlobal._global._sqlConnection;
            __conn.Open();
            string __myQueryTop = @"select doc_no, doc_date_time,amount_before_discount,discount , amount ,memo,agent_code, ap_code,vat_rate, vat_amount, total_amount,
                ap_name 
,address_1
,(( case when tambon = '' then '' else 'ตำบล ' + tambon + ' ' end) + ( case when amper = '' then '' else 'อำเภอ ' + amper + ' ' end) + ( case when province = '' then '' else 'จังหวัด ' + province + ' ' end)) as address_2
,(( case when tambon = '' then '' else 'ตำบล/แขวง ' + tambon + ' ' end) + ( case when amper = '' then '' else 'อำเภอ/เขต ' + amper + ' ' end) + ( case when province = '' then '' else 'จังหวัด ' + province + ' ' end)) as address_2_1
,(( case when tambon = '' then '' else 'แขวง ' + tambon + ' ' end) + ( case when amper = '' then '' else 'เขต ' + amper + ' ' end) + ( case when province = '' then '' else 'จังหวัด ' + province + ' ' end)) as address_2_2
,(( case when tambon = '' then '' else 'ต.' + tambon + ' ' end) + ( case when amper = '' then '' else 'อ.' + amper + ' ' end) + ( case when province = '' then '' else 'จ.' + province + ' ' end)) as address_short_2
,(( case when tambon = '' then '' else 'ตำบล ' + tambon + ' ' end) + ( case when amper = '' then '' else 'อำเภอ ' + amper + ' ' end) + ( case when province = '' then '' else 'จังหวัด ' + province + ' ' end) + zip_code) as address_zipcode_2
,(( case when tambon = '' then '' else 'ตำบล/แขวง ' + tambon + ' ' end) + ( case when amper = '' then '' else 'อำเภอ/เขต ' + amper + ' ' end) + ( case when province = '' then '' else 'จังหวัด ' + province + ' ' end) + zip_code) as address_zipcode_2_1
,(( case when tambon = '' then '' else 'แขวง ' + tambon + ' ' end) + ( case when amper = '' then '' else 'เขต ' + amper + ' ' end) + ( case when province = '' then '' else 'จังหวัด ' + province + ' ' end) + zip_code) as address_zipcode_2_2
,(( case when tambon = '' then '' else 'ต.' + tambon + ' ' end) + ( case when amper = '' then '' else 'อ.' + amper + ' ' end) + ( case when province = '' then '' else 'จ.' + province + ' ' end) + zip_code) as address_zipcode_short_2
, (( case when telephone = '' then '' else 'โทร.' + telephone + ' ' end) + ( case when fax = '' then '' else 'แฟกซ์.' + fax + ' ' end)) as tel_fax_short_1
, (( case when telephone = '' then '' else 'tel.' + telephone + ' ' end) + ( case when fax = '' then '' else 'fax.' + fax + ' ' end)) as tel_fax_short_2
,tambon
,amper
,province
,zip_code
,telephone
,fax
,email 
from ("
                + @"select doc_no ,doc_date_time, amount_before_discount,discount , amount ,memo,agent_code,
vat_rate, 
vat_amount, 
total_amount ,
(select ap_code from sml_agent where sml_agent.code=eorder_order.agent_code) as ap_code,
(select name_1 from ap_supplier where ap_supplier.code=(select ap_code from sml_agent where sml_agent.code=eorder_order.agent_code) and ap_supplier.agencode = agent_code) as ap_name,
(select address from ap_supplier where ap_supplier.code=(select ap_code from sml_agent where sml_agent.code=eorder_order.agent_code)  and ap_supplier.agencode = agent_code) as address_1
,(select name_1  from erp_tambon where erp_tambon.code=(select tambon from ap_supplier where ap_supplier.code=(select ap_code from sml_agent where sml_agent.code=eorder_order.agent_code)  and ap_supplier.agencode = agent_code) and erp_tambon.amper=(select amper from ap_supplier where ap_supplier.code=(select ap_code from sml_agent where sml_agent.code=eorder_order.agent_code)  and ap_supplier.agencode = agent_code) and erp_tambon.province=(select province from ap_supplier where ap_supplier.code=(select ap_code from sml_agent where sml_agent.code=eorder_order.agent_code)  and ap_supplier.agencode = agent_code)  )  as tambon
,(select name_1 from erp_amper where erp_amper.code = (select amper from ap_supplier where ap_supplier.code=(select ap_code from sml_agent where sml_agent.code=eorder_order.agent_code)  and ap_supplier.agencode = agent_code)  and erp_amper.province = (select province from ap_supplier where ap_supplier.code=(select ap_code from sml_agent where sml_agent.code=eorder_order.agent_code)  and ap_supplier.agencode = agent_code)) as amper
,(select name_1 from erp_province where erp_province.code = (select province from ap_supplier where ap_supplier.code=(select ap_code from sml_agent where sml_agent.code=eorder_order.agent_code)  and ap_supplier.agencode = agent_code)) as province
,(select zip_code from ap_supplier where ap_supplier.code=(select ap_code from sml_agent where sml_agent.code=eorder_order.agent_code)  and ap_supplier.agencode = agent_code) as zip_code
,(select telephone from ap_supplier where ap_supplier.code=(select ap_code from sml_agent where sml_agent.code=eorder_order.agent_code)  and ap_supplier.agencode = agent_code) as telephone
,(select fax from ap_supplier where ap_supplier.code=(select ap_code from sml_agent where sml_agent.code=eorder_order.agent_code)  and ap_supplier.agencode = agent_code) as fax
,(select email from ap_supplier where ap_supplier.code=(select ap_code from sml_agent where sml_agent.code=eorder_order.agent_code)  and ap_supplier.agencode = agent_code) as email 
" +

                 " from eorder_order  where doc_no =\'" + __docNo + "\' and  last_status = 2 ) as temp1 ";

            SqlDataAdapter __daWait = new SqlDataAdapter(__myQueryTop, __conn);
            DataTable __dtWait = new DataTable();
            __daWait.Fill(__dtWait);

            __ds.Tables.Add(__dtWait);

            string __myQueryDetail = "select ic_code,(select name_1 from ic_inventory where ic_inventory.code=eorder_order_detail.ic_code) as ic_name ,ic_unit, (select name_1 from ic_unit where ic_unit.code =  eorder_order_detail.ic_unit) as ic_unit_name ,qty,price , amount ,discount from eorder_order_detail where doc_no =\'" + __docNo + "\'";
            SqlDataAdapter __daWaitDetail = new SqlDataAdapter(__myQueryDetail, __conn);
            DataTable __dtWaitDetailII = new DataTable();
            __daWaitDetail.Fill(__dtWaitDetailII);

            __ds.Tables.Add(__dtWaitDetailII);

            // สั่งพิมพ์
            __print._query(__ds, false);


            ArrayList __arr = __print._pageTotalList;


            try
            {
                PdfDocument document = new PdfDocument();
                int __pageIndex = 0;

                for (int __i = 0; __i < __arr.Count; __i++)
                {
                    for (int __page = 1; __page <= (int)__arr[__i]; __page++)
                    {
                        PdfPage page = document.AddPage();
                        XGraphics gfx = XGraphics.FromPdfPage(page);

                        __print._addPage();
                        __print._drawPage(__pageIndex, gfx.Graphics, true);
                        __print._nextPage();
                        //gfx.DrawImage( new XImage( gfx.Graphics., new System.Drawing.Point(0, 0);

                    }
                }

                string filename = AkzoGlobal._global._pdfLocation + __docNo + ".pdf"; //@"c:\\smlpdftemp\\" + __docNo + ".pdf";
                __print._save(filename);

                Console.WriteLine("Gen  " + filename + " Success");
                __result = true;

            }
            catch (Exception ex)
            {
                Console.WriteLine("PDF Error : " + ex.Message.ToString() + " " + ex.StackTrace.ToString());
                return __result;
            }

            return true;
        }

    }

    public class _poFormPrint : SMLReport._formReport._formPrint
    {
        PdfDocument _document = new PdfDocument();
        PdfPage _page = null;
        XGraphics _gfx = null;

        public _poFormPrint()
        {

        }

        public void _addPage()
        {
            this._page = this._document.AddPage();
            this._page.Size = PdfSharp.PageSize.A4;
            this._page.Width = this._page.Width - 20;
            this._gfx = XGraphics.FromPdfPage(this._page, XGraphicsUnit.Presentation);
        }

        protected override void onDrawString(System.Drawing.Graphics g, string s, System.Drawing.Font font, System.Drawing.Brush brush, float x, float y, System.Drawing.StringFormat format)
        {
            //base.onDrawString(g, s, font, brush, x, y, format);
            if (this._gfx != null)
            {
                //DraString(g, s, font, brush, x, y, format);
                this._gfx.DrawString(s, new XFont(font.Name, font.Size, _getXFontStyle(font.Style), new XPdfFontOptions(PdfFontEncoding.Unicode)), brush, _pointToPicas(x), _pointToPicas(y), _getStringFormat(format));
            }
        }

        protected override void onDrawLine(System.Drawing.Graphics g, System.Drawing.Pen pen, float x1, float y1, float x2, float y2)
        {
            //base.onDrawLine(g, pen, x1, y1, x2, y2);
            if (this._gfx != null)
            {
                //DraString(g, s, font, brush, x, y, format);
                this._gfx.DrawLine(pen, _pointToPicas(x1), _pointToPicas(y1), _pointToPicas(x2), _pointToPicas(y2));
            }
        }

        protected override void onDrawRectangle(System.Drawing.Graphics g, System.Drawing.Pen pen, float x, float y, float width, float height)
        {
            //base.onDrawRectangle(g, pen, x, y, width, height);
            if (this._gfx != null)
            {
                //DraString(g, s, font, brush, x, y, format);
                this._gfx.DrawRectangle(pen, _pointToPicas(x), _pointToPicas(y), _pointToPicas(width), _pointToPicas(height));
            }

        }

        protected override void onDrawRoundRectangle(System.Drawing.Graphics g, System.Drawing.Pen pen, System.Drawing.Brush brush, SMLReport._design._drawRoundedRectangle roundrectangle)
        {
            //base.onDrawRoundRectangle(g, pen, brush, roundrectangle);
            if (this._gfx != null)
            {
                //DraString(g, s, font, brush, x, y, format);
                DrawRoundRect(this._gfx, pen, _pointToPicas(roundrectangle._actualSize.X), _pointToPicas(roundrectangle._actualSize.Y), _pointToPicas(roundrectangle._actualSize.Width), _pointToPicas(roundrectangle._actualSize.Height), _pointToPicas(roundrectangle.RoundedRadius.Bottomleft));
            }
        }

        static void DrawRoundRect(XGraphics g, System.Drawing.Pen p, double x, double y, double width, double height, float radius)
        {
            XGraphicsPath gp = new XGraphicsPath();
            gp.AddLine(x + radius, y, x + width - (radius * 2), y); // Line   
            gp.AddArc(x + width - (radius * 2), y, radius * 2, radius * 2, 270, 90); // Corner   
            gp.AddLine(x + width, y + radius, x + width, y + height - (radius * 2)); // Line   
            gp.AddArc(x + width - (radius * 2), y + height - (radius * 2), radius * 2, radius * 2, 0, 90); // Corner   
            gp.AddLine(x + width - (radius * 2), y + height, x + radius, y + height); // Line  
            gp.AddArc(x, y + height - (radius * 2), radius * 2, radius * 2, 90, 90); // Corner  
            gp.AddLine(x, y + height - (radius * 2), x, y + radius); // Line  
            gp.AddArc(x, y, radius * 2, radius * 2, 180, 90); // Corner  
            gp.CloseFigure();
            g.DrawPath(p, gp);
            gp.CloseFigure();
        }

        public void _save(string fileName)
        {
            this._document.Save(fileName);
        }

        XFontStyle _getXFontStyle(System.Drawing.FontStyle style)
        {
            XFontStyle xStyle = XFontStyle.Regular;
            switch (style)
            {
                case System.Drawing.FontStyle.Bold:
                    xStyle = XFontStyle.Bold;
                    break;
                case System.Drawing.FontStyle.Italic:
                    xStyle = XFontStyle.Italic;
                    break;
                case System.Drawing.FontStyle.Strikeout:
                    xStyle = XFontStyle.Strikeout;
                    break;
                case System.Drawing.FontStyle.Underline:
                    xStyle = XFontStyle.Underline;
                    break;
            }

            return xStyle;
        }

        XStringFormat _getStringFormat(System.Drawing.StringFormat format)
        {
            XStringFormat xFormat = new XStringFormat();

            switch (format.Alignment)
            {
                case System.Drawing.StringAlignment.Far:
                    xFormat.Alignment = XStringAlignment.Far;
                    break;
                case System.Drawing.StringAlignment.Center:
                    xFormat.Alignment = XStringAlignment.Center;
                    break;
                default:
                    xFormat.Alignment = XStringAlignment.Near;
                    break;
            }

            switch (format.LineAlignment)
            {
                case System.Drawing.StringAlignment.Near:
                    xFormat.LineAlignment = XLineAlignment.Near;
                    break;
                case System.Drawing.StringAlignment.Far:
                    xFormat.LineAlignment = XLineAlignment.Far;
                    break;
                default:
                    xFormat.LineAlignment = XLineAlignment.Center;
                    break;
            }


            return xFormat;
        }

        float _pointToPicas(float point)
        {
            float __ratio = (float)this._page.Width.Point / ((SMLReport._formReport._drawPaper)this._form._paperList[this._currentPageIndex])._myPageSetup.PagePixel.Width;
            float _result = point * __ratio;
            return _result;
        }

    }

}
