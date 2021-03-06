﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Json;
using System.Net;
using System.Threading;
using MyLib;
using System.Collections;

namespace SMLSINGHAControl
{
    public partial class _singhaMasterTransfer : UserControl
    {
        List<synctablelist> table = new List<synctablelist>();
        private _fromfilterdetail __fromfilterdetail;
        delegate void _writeProcessOutput(string s);
        event _writeProcessOutput log;
        public _singhaMasterTransfer()
        {
            InitializeComponent();
            this.textBox_log.ScrollBars = ScrollBars.Both;
            this.log += _setlog;
            this._transferControl1.button_process.Click += Button_process_Click;
            this._transferControl1.button_selectAll.Click += Button_selectAll_Click;
            this._transferControl1.button_selectNone.Click += Button_selectNone_Click;
            this._transferControl1._singhaGridGetdata1._mouseClickClip += new MyLib.ClipMouseClickHandler(_glDetailGrid__mouseClickClip);
            this._transferControl1._singhaGridGetdata1._afterAddRow += new MyLib.AfterAddRowEventHandler(_glDetailGrid__afterAddRow);


            this.table.Add(new synctablelist("erp_bank", "ธนาคาร", 1));
            this.table.Add(new synctablelist("erp_bank_branch", "สาขาธนาคาร", 2));
            this.table.Add(new synctablelist("erp_income_list", "รายได้อื่นๆ", 3));
            this.table.Add(new synctablelist("erp_expenses_list", "ค่าใช้จ่ายอื่นๆ", 4));
            this.table.Add(new synctablelist("gl_chart_of_account", "รหัสผังบัญชี", 5));
            this.table.Add(new synctablelist("ar_type", "ประเภทลูกหนี้", 6));
            this.table.Add(new synctablelist("ar_dimension", "มิติ ลูกหนี้ 1", 7));
            this.table.Add(new synctablelist("ar_project", "โครงการ", 8));
            this.table.Add(new synctablelist("ar_shoptype1", "ค้าส่ง", 9));
            this.table.Add(new synctablelist("ar_shoptype2", "ค้าปลีก", 10));
            this.table.Add(new synctablelist("ar_shoptype3", "ห้างท้องถิ่น", 11));
            this.table.Add(new synctablelist("ar_shoptype4", "On-Premise,HORECA", 12));
            this.table.Add(new synctablelist("ar_shoptype5", "ช่องทางพิเศษ", 13));
            this.table.Add(new synctablelist("sub_ar_shoptype5", "ช่องทางพิเศษย่อย", 14));
            this.table.Add(new synctablelist("ic_inventory", "สินค้า", 15));
            this.table.Add(new synctablelist("ic_unit", "หน่วยนับสินค้า", 16));



            foreach (synctablelist suit in table)
            {
                if (suit.table_enum != 0)
                {
                    int row1 = this._transferControl1._singhaGridGetdata1._addRow();
                    this._transferControl1._singhaGridGetdata1._cellUpdate(row1, 1, suit.table_name, true);
                    this._transferControl1._singhaGridGetdata1._cellUpdate(row1, 2, suit.name, true);
                }

            }
        }

        private void _buttonConfirm_Click(object sender, EventArgs e)
        {
            //_glDetailExtraObject __getObject = (_glDetailExtraObject)this._transferControl1._singhaGridGetdata1._cellGet(this._transferControl1._singhaGridGetdata1._selectRow,4);
            string __tablename = this._transferControl1._singhaGridGetdata1._cellGet(this._transferControl1._singhaGridGetdata1._selectRow, "Master Name").ToString();
            //__getObject._deteilList.Clear();
            //__getObject._deteilList.Add(__fromfilterdetail._selectdata(__fromfilterdetail._getddata(__tablename)));

            JsonArray __result = new JsonArray();
            __result.Clear();
            __result.Add(__fromfilterdetail._selectdata());
            this._transferControl1._singhaGridGetdata1._cellUpdate(this._transferControl1._singhaGridGetdata1._selectRow, "Filter", __result,  true);
            __fromfilterdetail.Close();
        }
 

        private void _glDetailGrid__afterAddRow(object sender, int row)
        {
            // เพิ่ม Object เพื่อรองรับการทำงานต่อไป
            //MyLib._myGrid __grid = (MyLib._myGrid)sender;
            //__grid._cellUpdate(row, _g.d.gl_journal_detail._dimension, new _glDetailExtraObject(), false);

        }
    

        private void _glDetailGrid__mouseClickClip(object sender, GridCellEventArgs e)
        {
            MyLib._myGrid __grid = (MyLib._myGrid)sender;
            string __tablename = __grid._cellGet(e._row, "Master Name").ToString();
            __fromfilterdetail = new _fromfilterdetail();
            __fromfilterdetail._buttonConfirm.Click += new EventHandler(_buttonConfirm_Click);
            __fromfilterdetail._buil(__tablename);
            __fromfilterdetail.ShowDialog();
        }



        private void Button_selectNone_Click(object sender, EventArgs e)
        {
            for (int row = 0; row < this._transferControl1._singhaGridGetdata1._rowData.Count; row++)
            {
                this._transferControl1._singhaGridGetdata1._cellUpdate(row, 0, 0, true);
            }
            this._transferControl1._singhaGridGetdata1.Invalidate();
        }

        private void Button_selectAll_Click(object sender, EventArgs e)
        {
            for (int row = 0; row < this._transferControl1._singhaGridGetdata1._rowData.Count; row++)
            {
                this._transferControl1._singhaGridGetdata1._cellUpdate(row, 0, 1, true);
            }
            this._transferControl1._singhaGridGetdata1.Invalidate();
        }

        internal void _setlog(string newLogdata)
        {
            this.textBox_log.AppendText(newLogdata + Environment.NewLine);
        }

        private void Button_process_Click(object sender, EventArgs e)
        {
            Thread __threadProcess = new Thread(new ThreadStart(_on_process));
            __threadProcess.Name = "SML Thread";
            __threadProcess.IsBackground = true;
            __threadProcess.Start();
            //this._on_process();
        }


        private void _on_process()
        {
            for (int row = 0; row < this._transferControl1._singhaGridGetdata1._rowData.Count; row++)
            {
                if (this._transferControl1._singhaGridGetdata1._cellGet(row, 0).ToString().Equals("1"))
                {
                    this._transferControl1._singhaGridGetdata1._cellUpdate(row, 3, "wait", true);
                    JsonArray _getfilter = (JsonArray)this._transferControl1._singhaGridGetdata1._cellGet(row, 4);
                    string __tableName = this._transferControl1._singhaGridGetdata1._cellGet(row, 1).ToString();
               
                    if (__tableName.Equals("ic_inventory"))
                    {
                        _processImportInventory __processInventory = new _processImportInventory(__tableName);
                        __processInventory._output += (str) => {
                            this.Invoke(log, new object[] { str });
                        };
                        if (_getfilter == null)
                        {
                            __processInventory._processInventory(); 
                        }
                        else
                        {
                            __processInventory._processfilterInventory(_getfilter);
                        }
                      
                    }
                    else {
                        _processImport __process = new _processImport(__tableName);
                        __process._output += (str) => {
                            this.Invoke(log, new object[] { str });
                        };
                        if (_getfilter == null)
                        {
                            __process._process(); 
                        }
                        else
                        {
                            __process._processfilter(_getfilter);
                        }
                    }
                     //   this.Invoke(log, new object[] { __process._setlog("------------------------------------------------------------------------------------------------------------------------------------------------------------------") });
                  
                    this._transferControl1._singhaGridGetdata1._cellUpdate(row, 3, "success", true);
                    this._transferControl1._singhaGridGetdata1._cellUpdate(row, 0, 0, true);
                }
            }
        }



        private void Close_Button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

    }

    public class synctablelist
    {
        public string table_name;
        public string name;
        public int table_enum;
        public synctablelist(string table_name, string name, int table_enum)
    //  public synctablelist(string table_name, string name)
        {
            this.table_name = table_name;
            this.name = name;
            this.table_enum = table_enum;
        }
    }

    public class _glDetailExtraObject
    {
        public ArrayList _deteilList = new ArrayList();

    }

    public enum synctablename
    {
        erp_expenses_list = 1,
        erp_bank = 2,
        Null = 0


    }

}
