using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.IO;

namespace MyLib
{
    public partial class _preferenceForm : Form
    {
        public _preferenceForm()
        {
            InitializeComponent();
            this._loadFont();
        }

        void _loadFont()
        {
            if (File.Exists(MyLib._myGlobal._programClientConfigSFileName.ToLower()))
            {
                // load config file
                _programClientConfig __config = null;

                try
                {
                    // ลองอ่านจาก location ใหม่ (c:\smlsoft\)

                    TextReader readFile = new StreamReader(MyLib._myGlobal._programClientConfigSFileName.ToLower());
                    XmlSerializer __xsLoad = new XmlSerializer(typeof(_programClientConfig));
                    __config = (_programClientConfig)__xsLoad.Deserialize(readFile);
                    readFile.Close();
                }
                catch
                {
                }

                if (__config != null)
                {
                    this._fontTextbox.Text = string.Format("{0},{1}", __config._clientFontName, __config._clientFontSize.ToString());
                }
            }
        }


        private void _saveButton_Click(object sender, EventArgs e)
        {
            _save_data();
        }

        void _save_data()
        {
            string[] __splitText = this._fontTextbox.Text.Split(',');
            string __fontName = __splitText[0];
            float __fontSize = float.Parse(__splitText[1].ToString());

            _programClientConfig __config = new _programClientConfig();
            __config._clientFontName = __fontName;
            __config._clientFontSize = __fontSize;

            try
            {
                XmlSerializer __colXs = new XmlSerializer(typeof(_programClientConfig));
                TextWriter __memoryStream = new StreamWriter(MyLib._myGlobal._programClientConfigSFileName.ToLower(), false, Encoding.UTF8);
                __colXs.Serialize(__memoryStream, __config);
                __memoryStream.Close();

                MessageBox.Show(MyLib._myGlobal._resource("บันทึกการตั้งค่าสำเร็ต โปรแกรมจะปิดหน้าจอนี้ให้"), MyLib._myGlobal._resource("สำเร็จ"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "ผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FontDialog __font = new FontDialog();
            DialogResult __result = __font.ShowDialog();

            if (__result == System.Windows.Forms.DialogResult.OK)
            {
                this._fontTextbox.Text = string.Format("{0},{1}", __font.Font.Name, __font.Font.Size.ToString());
            }
        }
    }
}
