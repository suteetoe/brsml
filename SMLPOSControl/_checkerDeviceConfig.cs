using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Drawing;
using System.Text;
using System.Xml.Serialization;
using System.Windows.Forms;

namespace SMLPOSControl
{
    public partial class _checkerDeviceConfig : Form
    {
        private string _fieldCheck = "checked";

        public _checkerDeviceConfig()
        {
            InitializeComponent();
            //
            this._grid._table_name = _g.d.kitchen_master._table;
            this._grid._addColumn(this._fieldCheck, 11, 10, 10);
            this._grid._addColumn(_g.d.kitchen_master._code, 1, 20, 30);
            this._grid._addColumn(_g.d.kitchen_master._name_1, 1, 20, 60);
            this._grid._isEdit = false;
            this._grid._calcPersentWidthToScatter();
            //
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            DataTable __data = __myFrameWork._queryShort("select * from " + _g.d.kitchen_master._table + " order by " + _g.d.kitchen_master._code).Tables[0];
            this._grid._loadFromDataTable(__data);
            this._saveButton.Click += _saveButton_Click;
            this._closeButton.Click += _closeButton_Click;
            //
            try
            {
                string __localpath = MyLib._myGlobal._smlConfigFile + _g.g._checkerXmlFileName;
                TextReader readFile = new StreamReader(__localpath);
                XmlSerializer __xsLoad = new XmlSerializer(typeof(_g._checkerDeviceXMLConfig));
                _g._checkerDeviceXMLConfig __config = (_g._checkerDeviceXMLConfig)__xsLoad.Deserialize(readFile);
                readFile.Close();
                //
                for (int __row = 0; __row < __config._kitchenCode.Count; __row++)
                {
                    for (int __find = 0; __find < this._grid._rowData.Count; __find++)
                    {
                        if (__config._kitchenCode[__row].Equals((string)this._grid._cellGet(__find, _g.d.kitchen_master._code)))
                        {
                            this._grid._cellUpdate(__find, _fieldCheck, 1, false);
                            break;
                        }
                    }
                }
                this._grid.Invalidate();
            }
            catch
            {

            }
        }

        void _closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void _saveButton_Click(object sender, EventArgs e)
        {
            _g._checkerDeviceXMLConfig __config = new _g._checkerDeviceXMLConfig();
            __config._kitchenCode = new List<string>();
            for (int __row = 0; __row < this._grid._rowData.Count; __row++)
            {
                int __check = (int)this._grid._cellGet(__row, this._fieldCheck);
                string __code = (string)this._grid._cellGet(__row, _g.d.kitchen_master._code);
                if (__check == 1)
                {
                    __config._kitchenCode.Add(__code);
                }
            }
            // write config to file 
            string __localpath = MyLib._myGlobal._smlConfigFile + _g.g._checkerXmlFileName;
            XmlSerializer __colXs = new XmlSerializer(typeof(_g._checkerDeviceXMLConfig));
            TextWriter __memoryStream = new StreamWriter(__localpath.ToLower(), false, Encoding.UTF8);
            __colXs.Serialize(__memoryStream, __config);
            __memoryStream.Close();
            this.Close();
        }
    }

}
