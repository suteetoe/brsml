using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace MyLib._databaseManage
{
    public class _permissionGrid : MyLib._myGrid
    {
        public override void _importFromTextFile(bool fastMode)
        {
            ArrayList __columnList = new ArrayList();
            for (int __column = 0; __column < this._columnList.Count; __column++)
            {
                MyLib._myGrid._columnType __myColumn = (MyLib._myGrid._columnType)_columnList[__column];
                if (__myColumn._isHide == true)
                {
                    __myColumn._isHide = false;
                }
                __columnList.Add(__myColumn);
            }

            _myGridImportFromTextFileForm __form = new _myGridImportFromTextFileForm(__columnList);
            __form._importButton.Click += (s1, e1) =>
            {
                this._importWorking = true;
                __form.Close();
                __form._mapFieldView.EndEdit();
                Application.DoEvents();


                // get map column index from MyLib._d.sml_permissions_group._menucode
                string __menuName = ((MyLib._myGrid._columnType)this._columnList[this._findColumnByName(MyLib._d.sml_permissions_group._menucode)])._name;

                int __menuNameColumnIndex = -1;
                for (int __rowMap = 0; __rowMap < __form._mapFieldView.Rows.Count; __rowMap++)
                {

                    // return c1, c2, .. etc
                    string __mapFieldName = __form._mapFieldView.Rows[__rowMap].Cells[0].Value.ToString();
                    if (__mapFieldName.ToUpper().Equals(__menuName))
                    {
                        if (__form._mapFieldView.Rows[__rowMap].Cells[1].Value != null)
                        {

                            string __getMapColumn = __form._mapFieldView.Rows[__rowMap].Cells[1].Value.ToString().ToUpper();
                            __menuNameColumnIndex = MyLib._myGlobal._intPhase(__getMapColumn.Replace("C", string.Empty));
                        }
                        break;
                    }


                }

                if (__menuNameColumnIndex != -1)
                {
                    for (int __row1 = 0; __row1 < __form._dataGridView.Rows.Count; __row1++)
                    {
                        try
                        {
                            int __addrRow = -1;
                            //string __getMenuName = __form._dataGridView.Rows[__row1].Cells[
                            for (int __row2 = 0; __row2 < __form._mapFieldView.Rows.Count; __row2++)
                            {
                                string __name = __form._mapFieldView.Rows[__row2].Cells[0].Value.ToString();
                                string __field = (__form._mapFieldView.Rows[__row2].Cells[1].Value == null) ? "" : __form._mapFieldView.Rows[__row2].Cells[1].Value.ToString();
                                if (__field.Trim().Length > 0)
                                {
                                    int __columnNumber = -1;
                                    for (int __loop = 0; __loop < __form._dataGridView.Columns.Count; __loop++)
                                    {
                                        if (__form._dataGridView.Columns[__loop].Name.Equals(__field))
                                        {
                                            __columnNumber = __loop;
                                            break;
                                        }
                                    }
                                    if (__columnNumber != -1)
                                    {
                                        string __value = __form._dataGridView.Rows[__row1].Cells[__columnNumber].Value.ToString();
                                        if (__addrRow == -1)
                                        {
                                            //__addrRow = this._addRow();
                                            string __getMenuId = __form._dataGridView.Rows[__row1].Cells[__menuNameColumnIndex].Value.ToString();
                                            __addrRow = this._findData(this._findColumnByName(MyLib._d.sml_permissions_group._menucode.ToUpper()), __getMenuId);
                                        }

                                        if (__addrRow != -1)
                                        {
                                            int __gridColumnNumber = -1;
                                            MyLib._myGrid._columnType __myColumn = null;
                                            for (int __column = 0; __column < this._columnList.Count; __column++)
                                            {
                                                __myColumn = (MyLib._myGrid._columnType)_columnList[__column];
                                                if (__myColumn._name.Equals(__name))
                                                {
                                                    __gridColumnNumber = __column;
                                                    break;
                                                }
                                            }
                                            if (__myColumn != null && __gridColumnNumber != -1)
                                            {
                                                switch (__myColumn._type)
                                                {
                                                    //case 1: this._cellUpdate(__addrRow, __gridColumnNumber, __value, (fastMode) ? false : true); break;
                                                    //case 2:
                                                    //case 3: this._cellUpdate(__addrRow, __gridColumnNumber, MyLib._myGlobal._decimalPhase(__value), (fastMode) ? false : true); break;
                                                    case 11: this._cellUpdate(__addrRow, __gridColumnNumber, MyLib._myGlobal._intPhase(__value), (fastMode) ? false : true); break;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        catch (Exception __ex)
                        {
                            MessageBox.Show(__ex.Message.ToString());
                        }
                    }
                }
                this.Refresh();
                this.Invalidate();
                base._goButtom();
                this._importWorking = false;
            };
            __form.ShowDialog();
        }
    }
}
