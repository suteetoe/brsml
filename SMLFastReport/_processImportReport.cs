using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Data;
using System.Diagnostics;

namespace SMLFastReport
{
    public static class _processImportReport
    {
        public static void _doImport()
        {
            Boolean __firstTime = false;
            while (true)
            {
                Thread.Sleep((1000 * 60) * ((__firstTime == false) ? 1 : 10));

                __firstTime = true;
                MyLib._myFrameWork __fwMaster = new MyLib._myFrameWork(MyLib._myGlobal._masterWebservice, MyLib._myGlobal._masterConfigXmlName, MyLib._myGlobal._databaseType.PostgreSql);

                // new process 
                // compare in memory

                if (__fwMaster._testConnect())
                {
                    string __getWhere = " where coalesce(" + _g.d.sml_fastreport._owner_code + ", '' ) =\'\' or ( " + _g.d.sml_fastreport._owner_code + "=\'" + MyLib._myGlobal._productCode + "\') ";

                    string __reportServerQuery = "select upper(" + _g.d.sml_fastreport._menuid + ") as " + _g.d.sml_fastreport._menuid + ", " + _g.d.sml_fastreport._menuname + ", " + _g.d.sml_fastreport._report_type + "," + _g.d.sml_fastreport._timeupdate + " from " + _g.d.sml_fastreport._table + __getWhere + " order by " + _g.d.sml_fastreport._menuid;
                    DataSet __result = __fwMaster._query(MyLib._myGlobal._masterDatabaseName, __reportServerQuery);


                    if (__result.Tables.Count > 0)
                    {

                        DataTable __tableReportCenter = __result.Tables[0];

                        MyLib._myFrameWork __ws = new MyLib._myFrameWork();
                        string __checkVersionQuery = "select " + _g.d.sml_fastreport._timeupdate + ", " + _g.d.sml_fastreport._menuid + ", " + _g.d.sml_fastreport._menuname + ", " + _g.d.sml_fastreport._report_type + "," + _g.d.sml_fastreport._is_system_report + " from " + _g.d.sml_fastreport._table + " order by " + _g.d.sml_fastreport._menuid; // " where UPPER(" + _g.d.sml_fastreport._menuid + ") = '" + __masterMenuId.ToUpper() + "'";
                        DataSet __myResult = __ws._query(MyLib._myGlobal._databaseName, __checkVersionQuery);
                        DataTable __myReport = __myResult.Tables[0];

                        for (int __i = 0; __i < __tableReportCenter.Rows.Count; __i++)
                        {
                            //int __row = this._dataGrid._addRow();
                            //this._dataGrid._cellUpdate(__i, 0, __table.Rows[__i][_g.d.formdesign._formcode], false);
                            //this._dataGrid._cellUpdate(__row, 0, __table.Rows[__i][_g.d.sml_fastreport._menuid], false);
                            //this._dataGrid._cellUpdate(__row, 1, __table.Rows[__i][_g.d.sml_fastreport._menuname], false);
                            //this._dataGrid._cellUpdate(__row, 2, __table.Rows[__i][_g.d.sml_fastreport._report_type], false);

                            string __masterTimeUpdate = "";
                            string __masterMenuId = "";
                            string __masterMenuName = "";
                            int __masterMenuType = 0;
                            DateTime __reportSaveTime = new DateTime();

                            try
                            {
                                __masterTimeUpdate = __tableReportCenter.Rows[__i][_g.d.sml_fastreport._timeupdate].ToString();
                                __masterMenuId = __tableReportCenter.Rows[__i][_g.d.sml_fastreport._menuid].ToString();
                                __masterMenuName = __tableReportCenter.Rows[__i][_g.d.sml_fastreport._menuname].ToString();
                                __reportSaveTime = MyLib._myGlobal._checkDate(MyLib._myGlobal._convertDateFromQuery(__masterTimeUpdate), 0);
                                //  DateTime.Parse(__masterTimeUpdate, MyLib._myGlobal._cultureInfo());
                                __masterMenuType = (int)MyLib._myGlobal._decimalPhase(__tableReportCenter.Rows[__i][_g.d.sml_fastreport._report_type].ToString());
                            }
                            catch (Exception ex)
                            {
                            }


                            //string __checkVersionQuery = "select " + _g.d.sml_fastreport._timeupdate + ", " + _g.d.sml_fastreport._menuid + ", " + _g.d.sml_fastreport._menuname + ", " + _g.d.sml_fastreport._report_type + "," + _g.d.sml_fastreport._is_system_report + " from " + _g.d.sml_fastreport._table + " where UPPER(" + _g.d.sml_fastreport._menuid + ") = '" + __masterMenuId.ToUpper() + "'";

                            string __localTimeUpdate = "";
                            DateTime __localSaveTime = new DateTime();
                            int __localType = -1;
                            int __is_systemReport = 0;

                            if (!__masterTimeUpdate.Equals(""))
                            {

                                try
                                {
                                    DataRow[] __reportSelect = (__myReport.Rows.Count == 0) ? null : __myReport.Select(_g.d.sml_fastreport._menuid + "=\'" + __masterMenuId.ToUpper() + "\'") ;

                                    if (__reportSelect != null && __reportSelect.Length > 0) 
                                    {
                                        __localTimeUpdate = __reportSelect[0][_g.d.sml_fastreport._timeupdate].ToString();
                                        //if (DateTime.TryParse(__localTimeUpdate, out __localSaveTime) == false)
                                        //{
                                        //    __localSaveTime = new DateTime(2000, 1, 1);
                                        //}
                                        __localSaveTime = MyLib._myGlobal._checkDate(MyLib._myGlobal._convertDateFromQuery(__localTimeUpdate), 0);
                                        __localType = (int)MyLib._myGlobal._decimalPhase(__reportSelect[0][_g.d.sml_fastreport._report_type].ToString());
                                        __is_systemReport = (int)MyLib._myGlobal._decimalPhase(__reportSelect[0][_g.d.sml_fastreport._is_system_report].ToString());
                                    }
                                }
                                catch (Exception ex)
                                {
                                    //Debugger.Break();
                                    Thread.CurrentThread.Abort();
                                }
                            }

                            bool __import = false;



                            if (__reportSaveTime > __localSaveTime || __localType != __masterMenuType || __is_systemReport == 0)
                            {
                                __import = true;
                            }

                            if (MyLib._myGlobal._allowProcessReportServer == false)
                            {
                                __import = false;
                            }

                            if (__import)
                            {
                                // start sycn

                                // select from master
                                string __query = string.Format("select " + _g.d.sml_fastreport._reportdata + " from " + _g.d.sml_fastreport._table + " where UPPER(" + _g.d.sml_fastreport._menuid + ") = '{0}'", __masterMenuId.ToUpper());
                                byte[] __getByte = __fwMaster._queryByte(MyLib._myGlobal._masterDatabaseName, __query);


                                try
                                {
                                    // insert to local
                                    string _delQuery = string.Format("delete from " + _g.d.sml_fastreport._table + " where UPPER(" + _g.d.sml_fastreport._menuid + ") = '{0}'", __masterMenuId.ToUpper());
                                    MyLib._myFrameWork __ws2 = new MyLib._myFrameWork();
                                    __ws2._query(MyLib._myGlobal._databaseName, _delQuery);

                                    string _query = string.Format("insert into " + _g.d.sml_fastreport._table + "(" + _g.d.sml_fastreport._menuid + "," + _g.d.sml_fastreport._menuname + ", " + _g.d.sml_fastreport._timeupdate + "," + _g.d.sml_fastreport._guid_code + ", " + _g.d.sml_fastreport._report_type + "," + _g.d.sml_fastreport._is_system_report + ", " + _g.d.sml_fastreport._reportdata + ") VALUES('{0}','{1}', '{2}', '', {3}, 1,?)", __masterMenuId, __masterMenuName, __masterTimeUpdate, __masterMenuType);
                                    string __resultStr = __ws2._queryByteData(MyLib._myGlobal._databaseName, _query, new object[] { __getByte });

                                    if (__resultStr.Length == 0)
                                    {
                                        Console.WriteLine("Import Report : " + __masterMenuId + " success \\n");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Error Import Report : " + __masterMenuId + " \\n" + __resultStr.ToString() + "\\n");
                                    }

                                }
                                catch (Exception ex)
                                {
                                    //deb.Show(ex.ToString());
                                    Console.WriteLine("Error Auto Import Report : \\n" + ex.ToString());
                                }
                            }
                        }
                        //this._dataGrid.Invalidate();



                        // compare
                    }

                    /* old
                    if (__result.Tables.Count > 0)
                    {
                        DataTable __table = __result.Tables[0];

                        for (int __i = 0; __i < __table.Rows.Count; __i++)
                        {
                            //int __row = this._dataGrid._addRow();
                            //this._dataGrid._cellUpdate(__i, 0, __table.Rows[__i][_g.d.formdesign._formcode], false);
                            //this._dataGrid._cellUpdate(__row, 0, __table.Rows[__i][_g.d.sml_fastreport._menuid], false);
                            //this._dataGrid._cellUpdate(__row, 1, __table.Rows[__i][_g.d.sml_fastreport._menuname], false);
                            //this._dataGrid._cellUpdate(__row, 2, __table.Rows[__i][_g.d.sml_fastreport._report_type], false);

                            string __masterTimeUpdate = "";
                            string __masterMenuId = "";
                            string __masterMenuName = "";
                            int __masterMenuType = 0;
                            DateTime __reportSaveTime = new DateTime();

                            try
                            {
                                __masterTimeUpdate = __table.Rows[__i][_g.d.sml_fastreport._timeupdate].ToString();
                                __masterMenuId = __table.Rows[__i][_g.d.sml_fastreport._menuid].ToString();
                                __masterMenuName = __table.Rows[__i][_g.d.sml_fastreport._menuname].ToString();
                                __reportSaveTime = MyLib._myGlobal._checkDate(MyLib._myGlobal._convertDateFromQuery(__masterTimeUpdate), 0);
                                //  DateTime.Parse(__masterTimeUpdate, MyLib._myGlobal._cultureInfo());
                                __masterMenuType = (int)MyLib._myGlobal._decimalPhase(__table.Rows[__i][_g.d.sml_fastreport._report_type].ToString());
                            }
                            catch (Exception ex)
                            {
                            }

                            
                            string __checkVersionQuery = "select " + _g.d.sml_fastreport._timeupdate + ", " + _g.d.sml_fastreport._menuid + ", " + _g.d.sml_fastreport._menuname + ", " + _g.d.sml_fastreport._report_type + "," + _g.d.sml_fastreport._is_system_report + " from " + _g.d.sml_fastreport._table + " where UPPER(" + _g.d.sml_fastreport._menuid + ") = '" + __masterMenuId.ToUpper() + "'";

                            string __localTimeUpdate = "";
                            DateTime __localSaveTime = new DateTime();
                            int __localType = -1;
                            int __is_systemReport = 0;

                            if (!__masterTimeUpdate.Equals(""))
                            {
                                MyLib._myFrameWork __ws = new MyLib._myFrameWork();

                                try
                                {
                                    DataSet __ds = __ws._query(MyLib._myGlobal._databaseName, __checkVersionQuery);

                                    if (__ds.Tables.Count > 0 && __ds.Tables[0].Rows.Count > 0)
                                    {
                                        __localTimeUpdate = __ds.Tables[0].Rows[0][_g.d.sml_fastreport._timeupdate].ToString();
                                        //if (DateTime.TryParse(__localTimeUpdate, out __localSaveTime) == false)
                                        //{
                                        //    __localSaveTime = new DateTime(2000, 1, 1);
                                        //}
                                        __localSaveTime = MyLib._myGlobal._checkDate(MyLib._myGlobal._convertDateFromQuery(__localTimeUpdate), 0);
                                        __localType = (int)MyLib._myGlobal._decimalPhase(__ds.Tables[0].Rows[0][_g.d.sml_fastreport._report_type].ToString());
                                        __is_systemReport = (int)MyLib._myGlobal._decimalPhase(__ds.Tables[0].Rows[0][_g.d.sml_fastreport._is_system_report].ToString());
                                    }
                                }
                                catch (Exception ex)
                                {
                                    //Debugger.Break();
                                    Thread.CurrentThread.Abort();
                                }
                            }

                            bool __import = false;



                            if (__reportSaveTime > __localSaveTime || __localType != __masterMenuType || __is_systemReport == 0)
                            {
                                __import = true;
                            }

                            if (MyLib._myGlobal._allowProcessReportServer == false)
                            {
                                __import = false;
                            }

                            if (__import)
                            {
                                // start sycn

                                // select from master
                                string __query = string.Format("select " + _g.d.sml_fastreport._reportdata + " from " + _g.d.sml_fastreport._table + " where UPPER(" + _g.d.sml_fastreport._menuid + ") = '{0}'", __masterMenuId.ToUpper());
                                byte[] __getByte = __fwMaster._queryByte(MyLib._myGlobal._masterDatabaseName, __query);


                                try
                                {
                                    // insert to local
                                    string _delQuery = string.Format("delete from " + _g.d.sml_fastreport._table + " where UPPER(" + _g.d.sml_fastreport._menuid + ") = '{0}'", __masterMenuId.ToUpper());
                                    MyLib._myFrameWork __ws = new MyLib._myFrameWork();
                                    __ws._query(MyLib._myGlobal._databaseName, _delQuery);

                                    string _query = string.Format("insert into " + _g.d.sml_fastreport._table + "(" + _g.d.sml_fastreport._menuid + "," + _g.d.sml_fastreport._menuname + ", " + _g.d.sml_fastreport._timeupdate + "," + _g.d.sml_fastreport._guid_code + ", " + _g.d.sml_fastreport._report_type + "," + _g.d.sml_fastreport._is_system_report + ", " + _g.d.sml_fastreport._reportdata + ") VALUES('{0}','{1}', '{2}', '', {3}, 1,?)", __masterMenuId, __masterMenuName, __masterTimeUpdate, __masterMenuType);
                                    string __resultStr = __ws._queryByteData(MyLib._myGlobal._databaseName, _query, new object[] { __getByte });

                                    if (__resultStr.Length == 0)
                                    {
                                        Console.WriteLine("Import Report : " + __masterMenuId + " success \\n");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Error Import Report : " + __masterMenuId + " \\n" + __resultStr.ToString() + "\\n");
                                    }

                                }
                                catch (Exception ex)
                                {
                                    //deb.Show(ex.ToString());
                                    Console.WriteLine("Error Auto Import Report : \\n" + ex.ToString());
                                }
                            }
                        }
                        //this._dataGrid.Invalidate();



                        // compare
                    }*/
                }


                // if singha exit

                if (MyLib._myGlobal._OEMVersion.Equals("SINGHA"))
                {
                    break;
                }

            }
        }
    }
}
