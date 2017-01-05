using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace MyLib
{
    public partial class _computerStatus : Form
    {
        public _computerStatus()
        {
            InitializeComponent();
            this._listView.Clear();
            MyLib._getInfoStatus _getinfo = new _getInfoStatus();
            ArrayList __scandisk = new ArrayList(); ;
            __scandisk = _getinfo._scrandive();
            bool index = false;
            string[] _dataDive = Environment.GetLogicalDrives();

            for (int loop = 0; loop < _dataDive.Length; loop++)
            {
                ListViewItem newItem = new ListViewItem();
                newItem.Text = (_dataDive[loop].Replace("\\", ""));
                if (__scandisk.Count != 0)
                {
                    for (int _x = 0; _x < __scandisk.Count; _x++)
                    {
                        if ((__scandisk[_x]).ToString().Equals(newItem.Text.Replace(":", "")))
                        {
                            newItem.Text = newItem.Text + " (Flash Drive)";
                            index = true;
                        }
                        else
                        {

                        }
                    }
                }

                if (index)
                {
                    newItem.ImageIndex = 1;
                }
                else
                {
                    newItem.ImageIndex = 0;
                    newItem.Text = newItem.Text + " (Local Drive)";
                }

                newItem.Text = newItem.Text + "   [Serial] =  " + _getinfo.GetVolumeSerial((_dataDive[loop].Replace(":\\", "")));

                this._listView.Items.Add(newItem);
                index = false;
            }
            this.Height = (_dataDive.Length * this._listView.TileSize.Height) + 45;
        }
    }
}