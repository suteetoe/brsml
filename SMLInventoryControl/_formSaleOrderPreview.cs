using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Text;
using System.Windows.Forms;

namespace SMLInventoryControl
{
    public partial class _formSaleOrderPreview : Form
    {
        public string __pageEnum;
        public string __docnoResult = "";
        public string __docdateResult = "";
        public string __arcodeResult = "";
        public string __paperSource = "";
        public int __transFlag;
        public int __transType;
        public ArrayList __pointArray = new ArrayList();
        private PaperKind kind;
        PrinterResolution pkResolution;
        public ArrayList pkResolutionList = new ArrayList();
        string[] __kind = { "High", "Medium", "Low", "Draft", "360 x 180", "180 x 180", "120 x 180" };

        public _formSaleOrderPreview(string __page, string __docno, string __docdate, string __arcode)
        {
            InitializeComponent();  
            this.StartPosition = FormStartPosition.CenterScreen;
            _myButtonPreview.AutoSize = false;
            _myButtonPreview.Dock = DockStyle.Fill;
            _myButtonPrint.AutoSize = false;
            _myButtonPrint.Dock = DockStyle.Fill;
            _myButtonClose.AutoSize = false;
            _myButtonClose.Dock = DockStyle.Fill;
            comboBoxPaper.SelectedIndex = 0;        
            radioButtonFull.Checked = true;
            groupBoxBorder.Enabled = false;
            __pageEnum = __page;
            __docnoResult = __docno;
            __docdateResult = __docdate;
            __arcodeResult = __arcode;
            checkBox1.Checked = true;
            checkBox2.Checked = true;
            checkBox3.Checked = true;
            checkBox4.Checked = true;
            checkBox5.Checked = true;
            if (__page.ToLower().Equals(SMLInventoryControl._gForm._formEnum.ขาย_ขายสินค้าและบริการ.ToString()))
            {
                checkBox5.Enabled = false;
                checkBox5.Checked = false;
            }
            else if (__page.ToLower().Equals(SMLInventoryControl._gForm._formEnum.ขาย_ขายสินค้าและบริการ_ici.ToString()))
            {
                radioButtonFull.Text = "ฟอร์ม ICI";
                radioButtonDraft.Text = "กำหหนดเอง";
                groupBoxBorder.Enabled = false;
                comboBoxPaper.Enabled = true;
                comboBoxPaper.SelectedIndex = 1;
            }
            else if (__page.ToLower().Equals(SMLInventoryControl._gForm._formEnum.ขาย_เพิ่มหนี้.ToString()))
            {
                checkBox1.Text = "กรอบด้านนอก (หัว+รายการ+ล่าง)";
                checkBox5.Enabled = false;
                checkBox5.Checked = false;
            }
            else if (__page.ToLower().Equals(SMLInventoryControl._gForm._formEnum.ซื้อ_จ่ายเงินมัดจำหรือเงินล่วงหน้า.ToString()) ||
                __page.ToString().Equals(SMLInventoryControl._gForm._formEnum.ซื้อ_คืนเงินมัดจำหรือเงินล่วงหน้า.ToString()) ||
                __page.ToString().Equals(SMLInventoryControl._gForm._formEnum.ขาย_รับเงินล่วงหน้า.ToString()) ||
                __page.ToString().Equals(SMLInventoryControl._gForm._formEnum.ขาย_คืนเงินล่วงหน้า.ToString()))
            {
                //ซื้อ_คืนเงินมัดจำหรือเงินล่วงหน้า
                checkBox3.Checked = false;
                checkBox4.Checked = false;
                checkBox3.Enabled = false;
                checkBox4.Enabled = false;
            }
            else if (__page.ToLower().Equals(SMLInventoryControl._gForm._formEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด.ToString()))
            {
                checkBox1.Checked = false;
                checkBox2.Checked = false;
                checkBox4.Checked = false;
                checkBox1.Enabled = false;
                checkBox2.Enabled = false;
                checkBox4.Enabled = false;

            }
            else if (__page.ToLower().Equals(SMLInventoryControl._gForm._formEnum.ขาย_ลดหนี้.ToString()))
            {
                checkBox1.Checked = false;
                checkBox2.Checked = false;
               // checkBox4.Checked = false;
                checkBox1.Enabled = false;
                checkBox2.Enabled = false;
               // checkBox4.Enabled = false;

            }
            else if (__page.ToLower().Equals(SMLInventoryControl._gForm._formEnum.สินค้า_รับสินค้าสำเร็จรูป.ToString()))
            {
                checkBox1.Checked = false;
                checkBox5.Checked = false;
                checkBox1.Enabled = false;
                checkBox5.Enabled = false;
            }
            else if (__page.ToLower().Equals(SMLInventoryControl._gForm._formEnum.สินค้า_เบิกสินค้าวัตถุดิบ.ToString()) ||
                __page.ToLower().Equals(SMLInventoryControl._gForm._formEnum.สินค้า_รับคืนสินค้าจากการเบิก.ToString()) ||
                __page.ToLower().Equals(SMLInventoryControl._gForm._formEnum.สินค้า_โอนออก.ToString()))
            {
                checkBox5.Checked = false;
                checkBox5.Enabled = false;
            }
            else
            {
                checkBox5.Enabled = true;
                checkBox5.Checked = true;
            }
            comboBoxResolution.Enabled = false;
            
            for (int i = 0; i < _printDocument.PrinterSettings.PrinterResolutions.Count; i++)
            {
                pkResolution = _printDocument.PrinterSettings.PrinterResolutions[i];
                if (i < __kind.Length)
                {
                    comboBoxResolution.Items.Add(__kind[i].ToString());
                    pkResolutionList.Add(pkResolution);
                }
            }                        
        }

        private ArrayList __getprintOption()
        {
            ArrayList __result = new ArrayList();
            _gForm._printoption __option = new _gForm._printoption();
            try
            {
                __option._paperSource = (comboBoxPaper.SelectedIndex == 0) ? "A4" : "Letter";
                __option._printQuality = (radioButtonFull.Checked == true) ? 1 : 0;
                __option._box1 = (checkBox1.Checked == true) ? 1 : 0;
                __option._box2 = (checkBox2.Checked == true) ? 1 : 0;
                __option._box3 = (checkBox3.Checked == true) ? 1 : 0;
                __option._box4 = (checkBox4.Checked == true) ? 1 : 0;
                __option._box5 = (checkBox5.Checked == true) ? 1 : 0;
                __option._printerSelect = (checkBox6.Checked == true) ? true : false;
                __result.Add(__option);
            }
            catch (Exception ex)
            {
            }
            return __result;
        }
        private void _myButtonPreview_Click(object sender, EventArgs e)
        {
            try
            {
                this._printDocument.BeginPrint += new PrintEventHandler(_printDocument_BeginPrint);
                this._printDocument.PrintPage += new PrintPageEventHandler(_printDocument_PrintPage);
                this._printDocument.EndPrint += new PrintEventHandler(_printDocument_EndPrint);
                ArrayList __getOption = new ArrayList();
                __getOption = __getprintOption();
                VR.PrintPreview.EnhancedPrintPreviewDialog __showDialog = new VR.PrintPreview.EnhancedPrintPreviewDialog(__pageEnum, __docnoResult, __docdateResult, __arcodeResult, __getOption);               
                __showDialog.Document = this._printDocument;                
                __showDialog.ShowDialog();
            }
            catch (Exception ex)
            {
            }
        }

        void _printDocument_EndPrint(object sender, PrintEventArgs e)
        {
            try
            {
                
            }
            catch (Exception ex)
            {
            }
        }

        void _printDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
            }
        }

      

        void _printDocument_BeginPrint(object sender, PrintEventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;         
            PageSettings Psettings =_printDocument.DefaultPageSettings;
            Psettings.Margins = new Margins(30, 30, 30, 30);
            Psettings.Landscape = false;
            try
            {
                if (comboBoxPaper.SelectedIndex == 0)
                {
                    Psettings.PaperSize = new System.Drawing.Printing.PaperSize("A4", 827, 1169);
                }
                else
                {
                    Psettings.PaperSize = new System.Drawing.Printing.PaperSize("Letter", 850, 1100);
                }
                int __getComBoIndex = comboBoxResolution.SelectedIndex;
                string __getComboResolution = pkResolutionList[__getComBoIndex].ToString();
                Psettings.PrinterResolution = (PrinterResolution)pkResolutionList[__getComBoIndex];
                //foreach (PrinterResolution pr in _printDocument.PrinterSettings.PrinterResolutions)
                //{
                //    if (pr.Kind.ToString() == __getComboResolution)
                //    {
                //        Psettings.PrinterResolution = pr;
                //        break;
                //    }
                    
                //}
            }
            catch (Exception ex)
            {
            }
            _printDocument.DefaultPageSettings = Psettings;
            this.Cursor = Cursors.Default;
        }

        private void _myButtonPrint_Click(object sender, EventArgs e)
        {
            try
            {               
                this._printDocument.BeginPrint += new PrintEventHandler(_printDocument_BeginPrint);
                this._printDocument.PrintPage += new PrintPageEventHandler(_printDocument_PrintPage);
                this._printDocument.EndPrint += new PrintEventHandler(_printDocument_EndPrint);
                ArrayList __getOption = new ArrayList();
                __getOption = __getprintOption();
                VR.PrintPreview.EnhancedPrintPreviewDialog __showDialog = new VR.PrintPreview.EnhancedPrintPreviewDialog(__pageEnum, __docnoResult, __docdateResult, __arcodeResult, __getOption);             
                __showDialog.Document = this._printDocument;                
                __showDialog.__printData();
                this.Close();
            }
            catch (Exception ex)
            {
            }
        }

        private void _myButtonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void radioButtonFull_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                checkBox1.Checked = true;
                checkBox2.Checked = true;               
                checkBox3.Checked = true;
                checkBox4.Checked = true; 
                checkBox5.Checked = true;
                if (__pageEnum != null)
                {
                    if (__pageEnum.ToLower().Equals(SMLInventoryControl._gForm._formEnum.ขาย_ขายสินค้าและบริการ.ToString()))
                    {
                        checkBox5.Checked = false;
                    }
                    else if (__pageEnum.ToLower().Equals(SMLInventoryControl._gForm._formEnum.ขาย_เพิ่มหนี้.ToString()))
                    {
                        checkBox5.Checked = false;
                    }
                    else if (__pageEnum.ToLower().Equals(SMLInventoryControl._gForm._formEnum.ซื้อ_จ่ายเงินมัดจำหรือเงินล่วงหน้า.ToString()) ||
                       __pageEnum.ToLower().Equals(SMLInventoryControl._gForm._formEnum.ซื้อ_คืนเงินมัดจำหรือเงินล่วงหน้า.ToString()) ||
                        __pageEnum.ToLower().Equals(SMLInventoryControl._gForm._formEnum.ขาย_รับเงินล่วงหน้า.ToString()) ||
                        __pageEnum.ToLower().Equals(SMLInventoryControl._gForm._formEnum.ขาย_คืนเงินล่วงหน้า.ToString()))
                    {
                        checkBox4.Checked = false;
                        checkBox3.Checked = false;
                    }
                    else if (__pageEnum.ToLower().Equals(SMLInventoryControl._gForm._formEnum.ซื้อ_ส่งคืนสินค้าลดหนี้ราคาผิด.ToString()))
                    {
                        checkBox1.Checked = false;
                        checkBox2.Checked = false;
                        checkBox4.Checked = false;
                    }
                    else if (__pageEnum.ToLower().Equals(SMLInventoryControl._gForm._formEnum.สินค้า_รับสินค้าสำเร็จรูป.ToString()))
                    {
                        checkBox1.Checked = false;
                        checkBox5.Checked = false;
                    }
                    else if (__pageEnum.ToLower().Equals(SMLInventoryControl._gForm._formEnum.สินค้า_เบิกสินค้าวัตถุดิบ.ToString()) ||
                        __pageEnum.ToLower().Equals(SMLInventoryControl._gForm._formEnum.สินค้า_รับคืนสินค้าจากการเบิก.ToString()) ||
                        __pageEnum.ToLower().Equals(SMLInventoryControl._gForm._formEnum.สินค้า_โอนออก.ToString()))
                    {
                        checkBox5.Checked = false;
                    }
                    else
                    {
                        checkBox5.Checked = true;
                    }
                }
                groupBoxBorder.Enabled = false;
                comboBoxResolution.Enabled = false;
            }
            catch (Exception ex)
            {
            }
        }

        private void radioButtonDraft_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (__pageEnum.ToLower().Equals(SMLInventoryControl._gForm._formEnum.ขาย_ขายสินค้าและบริการ_ici.ToString()))
                {
                    groupBoxBorder.Enabled = false;
                    comboBoxResolution.Enabled = true;
                }
                else
                {
                    groupBoxBorder.Enabled = true;
                    comboBoxResolution.Enabled = true;
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void comboBoxPaper_SelectedIndexChanged(object sender, EventArgs e)
        {
            try 
            {
                ComboBox __cb = (ComboBox)sender;
                if (__cb.SelectedIndex == 0)
                {
                    __paperSource = "A4";
                }
                else if (__cb.SelectedIndex == 1)
                {
                    __paperSource = "Letter";
                }
            }
            catch (Exception ex)
            {
            }
        }


    }
}
