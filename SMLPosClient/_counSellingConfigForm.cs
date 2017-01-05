using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.IO;

namespace SMLPosClient
{
    public partial class _counSellingConfigForm : MyLib._myForm
    {
        public string _configFileName
        {
            get
            {
                return string.Format(MyLib._myGlobal._smlConfigFile + "{2}-{0}-{1}-LabelConfig.xml", MyLib._myGlobal._webServiceServer.Replace(".", "_").Replace(":", "__"), MyLib._myGlobal._providerCode, MyLib._myGlobal._databaseName).ToLower();
            }
        }

        public _counSellingConfigForm()
        {
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this._toolStrip1.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }
            _getDefaultPrinter();

            this.Load += new EventHandler(_counSellingConfigForm_Load);
        }

        void _getDefaultPrinter()
        {
            int __default = 0;
            int __count = 0;

            _printerNameCombobox.Items.Clear();

            //foreach (ManagementObject __getPrinter in __printerList.Get())
            foreach (MyLib._printerListClass __getPrinter in MyLib._myGlobal._printerList)
            {
                string __printerName = __getPrinter._printerName;
                if (__getPrinter._isDefault)
                {
                    __default = __count;
                }
                _printerNameCombobox.Items.Add(__printerName);
                __count++;
            }
            _printerNameCombobox.SelectedIndex = __default;

        }


        void _counSellingConfigForm_Load(object sender, EventArgs e)
        {
            // load printer and set default;


            // load last config
            _labelConfigClass __config = null;

            try
            {
                // ลองอ่านจาก location ใหม่ (c:\smlsoft\)

                TextReader readFile = new StreamReader(_configFileName);
                XmlSerializer __xsLoad = new XmlSerializer(typeof(_labelConfigClass));
                __config = (_labelConfigClass)__xsLoad.Deserialize(readFile);
                readFile.Close();
            }
            catch
            {
            }

            if (__config != null)
            {
                _labelWidthTextbox.Text = __config._labelWidth.ToString();
                _labelHeightTextbox.Text = __config._lableHeight.ToString();

                _barcodeFontTextbox.Text = string.Format("{0},{1}", __config._barcodeFontName, __config._barcodeFontSize.ToString());
                _headerFontTextbox.Text = string.Format("{0},{1}", __config._headerFontName, __config._headerFontSize.ToString());
                _detailFontTextbox.Text = string.Format("{0},{1}", __config._detailFontName, __config._detailFontSize.ToString());

                _useBarcodeFont.Checked = __config._useBarcodeFont;
                _printerNameCombobox.Text = __config._printerName;
            }
            else
            {
                _labelWidthTextbox.Text = "500";
                _labelHeightTextbox.Text = "250";
            }

        }

        private void _buttonClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void _buttonSave_Click(object sender, EventArgs e)
        {
            // save config
            _labelConfigClass __config = new _labelConfigClass();
            __config._labelWidth = (float)MyLib._myGlobal._decimalPhase(_labelWidthTextbox.Text);
            __config._lableHeight = (float)MyLib._myGlobal._decimalPhase(_labelHeightTextbox.Text);

            if (_barcodeFontTextbox.Text.IndexOf(",") != -1)
            {
                __config._barcodeFontName = _barcodeFontTextbox.Text.Split(',')[0].ToString();
                __config._barcodeFontSize = (float)MyLib._myGlobal._decimalPhase(_barcodeFontTextbox.Text.Split(',')[1].ToString());
            }
            if (_headerFontTextbox.Text.IndexOf(",") != -1)
            {
                __config._headerFontName = _headerFontTextbox.Text.Split(',')[0].ToString();
                __config._headerFontSize = (float)MyLib._myGlobal._decimalPhase(_headerFontTextbox.Text.Split(',')[1].ToString());
            }
            if (_detailFontTextbox.Text.IndexOf(",") != -1)
            {

                __config._detailFontName = _detailFontTextbox.Text.Split(',')[0].ToString();
                __config._detailFontSize = (float)MyLib._myGlobal._decimalPhase(_detailFontTextbox.Text.Split(',')[1].ToString());
            }

            __config._printerName = _printerNameCombobox.Text;
            __config._useBarcodeFont = _useBarcodeFont.Checked;

            XmlSerializer __colXs = new XmlSerializer(typeof(_labelConfigClass));
            //string __configFileName = _configFileName;
            TextWriter __memoryStream = new StreamWriter(_configFileName, false, Encoding.UTF8);
            __colXs.Serialize(__memoryStream, __config);
            __memoryStream.Close();

            MessageBox.Show("บันทึกการตั้งค่า เรียบร้อยแล้ว ระบบจะทำการปิดจอให้", "Success");
            this.Dispose();
        }

        private void _buttonSelectBarcodeFont_Click(object sender, EventArgs e)
        {
            _selectFontConfig(_barcodeFontTextbox);
        }

        private void _selectFontConfig(TextBox __targetTextBox)
        {
            FontDialog __dialog = new FontDialog();
            try
            {
                string __fontinitStr = __targetTextBox.Text;
                if (__fontinitStr.Length > 0)
                {
                    __dialog.Font = new Font(__fontinitStr.Split(',')[0].ToString(), (float)MyLib._myGlobal._decimalPhase(__fontinitStr.Split(',')[1].ToString()));
                }
            }
            catch
            {
            }
            DialogResult __result = __dialog.ShowDialog(MyLib._myGlobal._mainForm);

            if (__result == DialogResult.OK)
            {
                string __fontStr = string.Format("{0},{1}", __dialog.Font.Name, __dialog.Font.Size.ToString());
                __targetTextBox.Text = __fontStr;
            }
        }

        private void _buttonSelectHeaderFont_Click(object sender, EventArgs e)
        {
            _selectFontConfig(_headerFontTextbox);
        }

        private void _buttonSelectDetailFont_Click(object sender, EventArgs e)
        {
            _selectFontConfig(_detailFontTextbox);
        }

        private void _useBarcodeFont_CheckedChanged(object sender, EventArgs e)
        {
            if (_useBarcodeFont.Checked == true)
            {
                _barcodeFontTextbox.Enabled = true;
                _buttonSelectBarcodeFont.Enabled = true;
            }
            else
            {
                _barcodeFontTextbox.Enabled = false;
                _buttonSelectBarcodeFont.Enabled = false;
            }
        }
    }

    public class _labelConfigClass
    {
        public float _labelWidth = 0f;
        public float _lableHeight = 0f;
        public string _barcodeFontName = "";
        public float _barcodeFontSize = 0f;
        public string _headerFontName = "";
        public float _headerFontSize = 0f;
        public string _detailFontName = "";
        public float _detailFontSize = 0f;

        public string _printerName = "";
        public Boolean _useBarcodeFont = false;
    }
}
