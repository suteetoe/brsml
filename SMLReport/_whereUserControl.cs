using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLReport
{
    public partial class _whereUserControl : UserControl
    {
        public string _tableName = "";
        MyLib._databaseManage._databaseStructForm _databaseStruct = null;

        public _whereUserControl()
        {
            InitializeComponent();
            this._reportFieldComboBox.SelectedIndexChanged += new EventHandler(_reportFieldComboBox_SelectedIndexChanged);
            this._reportFieldComboBox.Items.Add("Select Report Field");
        }

        private void _reportFieldComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox __sender = (ComboBox)sender;
            if (__sender.SelectedIndex > 0)
            {
                this._where2TextBox.Text = this._where2TextBox.Text + __sender.SelectedItem.ToString().Split('/')[0].ToString() + " ";
            }
        }

        public void _addFieldComboBox(string[] source)
        {
            this._addFieldComboBox(source, 0);
        }

        public void _addFieldComboBox(string[] source,int selectIndexNumber)
        {
            for (int __loop = 0; __loop < source.Length; __loop++)
            {
                string __fieldName = source[__loop].ToString().Replace("*", "");
                string __resourceName = ((MyLib._myResourceType)MyLib._myResource._findResource(__fieldName, __fieldName))._str;
                this._reportFieldComboBox.Items.Add(__fieldName + "/" + __resourceName);
                this._orderByComboBox.Items.Add("\"" + __fieldName + "\"/" + __resourceName);
            }
            this._orderByComboBox.SelectedIndex = selectIndexNumber;
            this._reportFieldComboBox.SelectedIndex = 0;
        }

        private void vistaButton1_Click(object sender, EventArgs e)
        {
            if (this._databaseStruct == null)
            {
                this._databaseStruct = new MyLib._databaseManage._databaseStructForm(this._tableName);
                this._databaseStruct._database._fieldDoubleClick -= new MyLib._databaseManage.FieldDoubleClickHandler(_database__fieldDoubleClick);
                this._databaseStruct._database._fieldDoubleClick += new MyLib._databaseManage.FieldDoubleClickHandler(_database__fieldDoubleClick);
            }
            this._databaseStruct.ShowDialog();
        }

        void _database__fieldDoubleClick(object sender, string selectFieldName)
        {
            this._databaseStruct.Close();
            this._whereTextBox.Text = this._whereTextBox.Text + " " + selectFieldName;
        }

        public int _getOrderSelectIndexNumber()
        {
            return this._orderByComboBox.SelectedIndex;
        }

        public string _getOrderBy()
        {
            return " order by " + this._orderByComboBox.SelectedItem.ToString().Split('/')[0].ToString();
        }

        public string _getWhere(string source)
        {
            StringBuilder __result = new StringBuilder();
            __result.Append(this._getWhere1(source));
            string __where2 = this._getWhere2();
            if (__result.Length > 0 && __where2.Length > 0)
            {
                __result.Append(" and (" + this._where2TextBox.Text.Trim() + ")");
            }
            else
            {
                if (__result.Length == 0 && __where2.Length > 0)
                {
                    __result.Append(__where2);
                }
            }
            return __result.ToString();
        }

        public string _getWhere1(string source)
        {
            string __where = "";
            if (source.Length > 0)
            {
                __where = " where (" + source + ")";
                if (this._whereTextBox.Text.Trim().Length > 0)
                {
                    __where = __where + " and (" + this._whereTextBox.Text.Trim() + ")";
                }
            }
            else
            {
                if (this._whereTextBox.Text.Trim().Length > 0)
                {
                    __where = " where (" + this._whereTextBox.Text.Trim() + ")";
                }
            }
            return __where;
        }

        public string _getWhere2()
        {
            string __where = "";
            if (this._where2TextBox.Text.Trim().Length > 0)
            {
                __where = " where (" + this._where2TextBox.Text.Trim() + ")";
            }
            return __where;
        }
    }
}
