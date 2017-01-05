using System;
using System.Data;
using System.ComponentModel;
using System.Text;
using System.Xml;
using System.IO;
using System.Net;
using System.Windows.Forms;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Globalization;
using System.Runtime.InteropServices;

namespace MyLib
{
    public class _myResource
    {
        public static DataTable _resource;
        /*  public static _myResourceType _insertResource(string code, string defaultName)
          {
              _myResourceType __result = new _myResourceType();
              try
              {
                  if (MyLib._myGlobal._webServiceServer == null)
                  {
                      return (__result);
                  }
                  if (_myGlobal._statusLabel != null)
                  {
                      _myGlobal._statusLabel.Text = string.Concat("Insert Resource ", code);
                      _myGlobal._statusLabel.Invalidate();
                      _myGlobal._statusStrip.Refresh();
                  }
                  // ถ้าไม่เจอ ให้เพิ่ม Resource Auto
                  try
                  {
                      if (MyLib._myGlobal._guid.Length != 0)
                      {
                          _myFrameWork __myFrameWork = new _myFrameWork();
                          MyLib.SMLJAVAWS.resourceType getResource = __myFrameWork._resourceInsert(((_myGlobal._dataGroup.Length == 0) ? "SML" : _myGlobal._dataGroup.ToUpper()), code, defaultName, MyLib._myGlobal._databaseStructFileName);
                          __result._str = getResource._name_1;
                          __result._length = getResource._length;
                          _resource.Rows.Add(code, getResource._name_1, getResource._name_2, getResource._length);
                      }
                  }
                  catch (Exception ex)
                  {
                  }
              }
              catch (Exception __e)
              {
              }
              return (__result);
          }

          public static _myResourceType _findResource(string codeFirst, string defaultName)
          {
              try
              {
                  if (_myGlobal._statusLabel != null && _myGlobal._statusLabel.Text.Length > 0)
                  {
                      _myGlobal._statusLabel.Text = "";
                      _myGlobal._statusLabel.Invalidate();
                      _myGlobal._statusStrip.Refresh();
                  }
              }
              catch (Exception __e)
              {
              }
              StringBuilder __code = new StringBuilder();
              __code.Append(codeFirst.ToUpper());
              _myResourceType __result = new _myResourceType();
              __result._str = __code.ToString();
              string[] __split = codeFirst.Split('.');
              string __wordBegin = "";
              if (__split.Length > 1)
              {
                  if (__split[__split.Length - 1].Substring(0, 1).Equals("*"))
                  {
                      __code = new StringBuilder();
                      for (int __loop = 0; __loop < __split.Length - 1; __loop++)
                      {
                          if (__loop != 0)
                          {
                              __code.Append(".");
                          }
                          __code.Append(__split[__loop]);
                      }
                      if (__split[__split.Length - 1].Equals("*begin"))
                      {
                          __wordBegin = "จาก";
                      }
                      else
                          if (__split[__split.Length - 1].Equals("*end"))
                          {
                              __wordBegin = "ถึง";
                          }
                  }
              }

              if (__code.Length > 0)
              {
                  if (_resource != null)
                  {
                      if (_resource.Rows.Count > 0)
                      {
                          //DataRow[] getRows = _resource.Select(string.Concat("code=\'", __code, "\'"));
                          DataRow __getRows = _resource.Rows.Find(__code);
                          if (__getRows != null)
                          {
                              __result._str = __getRows[MyLib._myGlobal._language + 1].ToString();
                              try
                              {
                                  string __getData = __getRows[3].ToString();
                                  __result._length = (__getData.Length == 0) ? 0 : MyLib._myGlobal._intPhase(__getRows[3].ToString());
                              }
                              catch
                              {
                                  // Debugger.Break();
                                  __result._length = 0;
                              }
                          }
                          else
                          {
                              __result = _insertResource(__code.ToString(), defaultName);
                          }
                      }
                      else
                      {
                          __result = _insertResource(__code.ToString(), defaultName);
                      }
                  }
                  else
                  {
                      __result = _insertResource(__code.ToString(), defaultName);
                  }
              }
              __result._str = string.Concat(__wordBegin, __result._str);
              return (__result);
          }*/
        public delegate void LabelDelegate(string value);
        public static void _SetBoxLabel(string value)
        {
            if (_myGlobal._statusLabeltemp.InvokeRequired)
            {
                _myGlobal._statusLabeltemp.Invoke(new LabelDelegate(_SetBoxLabel), new object[] { value });
            }
            else
            {
                Application.DoEvents();
                _myGlobal._statusLabeltemp.Text = value;
                // _myGlobal._statusLabel.Invalidate();
                //  _myGlobal._statusStrip.Refresh();
            }
            _myGlobal._statusLabel.Text = (_myGlobal._statusLabeltemp == null) ? "" : _myGlobal._statusLabeltemp.Text;
        }

        public static _myResourceType _insertResource(string code, string defaultName)
        {
            _myResourceType __result = new _myResourceType();
            __result._str = code;
            if (MyLib._myGlobal._webServiceServer == null || _resource == null)
            {
                return (__result);
            }
            if (code.ToLower().Equals("guid_code") || code.ToLower().Equals("is_lock_record") || (code.Length > 0 && code[0] == '.'))
            {
                return (__result);
            }
            code = code.ToLower();
            /*if (_myGlobal._statusLabel != null)
            {
                _myGlobal._statusLabeltemp = new Label();
                //_myGlobal._statusLabeltemp.Text = "";
                _SetBoxLabel(string.Concat("Insert Resource ", code));
                //   _myGlobal._statusLabel.Text = string.Concat("Insert Resource ", code);
                //  _myGlobal._statusLabel.Invalidate();
                // _myGlobal._statusStrip.Refresh();
            }*/
            // ถ้าไม่เจอ ให้เพิ่ม Resource Auto
            try
            {
                if (MyLib._myGlobal._guid.Length != 0)
                {
                    _myFrameWork __myFrameWork = new _myFrameWork();
                    MyLib.SMLJAVAWS.resourceType __getResource = __myFrameWork._resourceInsert(((_myGlobal._dataGroup.Length == 0) ? "SML" : _myGlobal._dataGroup.ToUpper()), code, defaultName, MyLib._myGlobal._databaseStructFileName);
                    switch (MyLib._myGlobal._language)
                    {
                        case _languageEnum.Thai: __result._str = __getResource._name_1; break;
                        case _languageEnum.English: __result._str = __getResource._name_2; break;
                        case _languageEnum.Lao: __result._str = __getResource._name_6; break;
                    }
                    __result._length = __getResource._length;
                    if (_resource.Rows.Find(code) == null)
                    {
                        _resource.Rows.Add(code, __getResource._name_1, __getResource._name_2, __getResource._name_3, __getResource._name_4, __getResource._name_5, __getResource._name_6, __getResource._length);
                    }
                }
            }
            catch
            {
            }
            return (__result);
        }

        public static _myResourceType _findResource(string codeFirst, int languageNumber)
        {
            return _findResource(codeFirst, codeFirst, true, languageNumber);
        }


        public static _myResourceType _findResource(string codeFirst)
        {
            return _findResource(codeFirst, codeFirst);
        }

        public static _myResourceType _findResource(string codeFirst, Boolean insert)
        {
            return _findResource(codeFirst, codeFirst, false);
        }

        //aohs
        public static _myResourceType _findResource(string codeFirst, string defaultName)
        {
            return _findResource(codeFirst, defaultName, true);
        }
        //jead
        //public static _myResourceType _findResource(string codeFirst, string defaultName)
        //{
        //    return _findResource(codeFirst, codeFirst, true);
        //}

        public static _myResourceType _findResource(string codeFirst, string defaultName, Boolean insert)
        {
            return _findResource(codeFirst, defaultName, insert, MyLib._myGlobal._languageNumber);
        }

        public static _myResourceType _findResource(string codeFirst, string defaultName, Boolean insert, int languageNumber)
        {
            _myResourceType __result = new _myResourceType();
            try
            {
                if (MyLib._myGlobal._webServiceServer == null || codeFirst.ToLower().IndexOf("roworder") != -1)
                {
                    __result._str = defaultName;
                    return (__result);
                }
                StringBuilder __code = new StringBuilder();
                __code.Append(codeFirst.ToUpper());
                __result._str = __code.ToString();
                string[] __split = codeFirst.Split('.');
                string __wordBegin = "";
                if (__split.Length > 1)
                {
                    string __getStr = (__split[__split.Length - 1].Length == 0) ? "" : __split[__split.Length - 1].Substring(0, 1);
                    if (__getStr.Equals("*"))
                    {
                        __code = new StringBuilder();
                        for (int __loop = 0; __loop < __split.Length - 1; __loop++)
                        {
                            if (__loop != 0)
                            {
                                __code.Append(".");
                            }
                            __code.Append(__split[__loop]);
                        }
                        if (__split[__split.Length - 1].Equals("*begin"))
                        {
                            __wordBegin = "จาก";
                        }
                        else
                            if (__split[__split.Length - 1].Equals("*end"))
                        {
                            __wordBegin = "ถึง";
                        }
                    }
                }

                if (__code.Length > 0)
                {
                    if (_resource != null)
                    {
                        if (_resource.Rows.Count > 0)
                        {
                            //DataRow[] getRows = _resource.Select(string.Concat("code=\'", __code, "\'"));
                            DataRow __getRows = _resource.Rows.Find(__code.ToString().ToLower());
                            if (__getRows != null)
                            {
                                __result._str = __getRows[languageNumber + 1].ToString();
                                if (__result._str.Trim().Length == 0)
                                {
                                    // กรณีไม่มี เอาภาษาอังกฤษ 
                                    __result._str = __getRows[2].ToString();
                                    if (__result._str.Trim().Length == 0)
                                    {
                                        // ถ้าไม่มีอีก เอาภาษาไทย
                                        __result._str = __getRows[1].ToString();
                                    }
                                }
                                try
                                {
                                    string __getData = __getRows[7].ToString();
                                    __result._length = (__getData.Length == 0) ? 0 : MyLib._myGlobal._intPhase(__getData);
                                }
                                catch
                                {
                                    // Debugger.Break();
                                    __result._length = 0;
                                }
                            }
                            else
                            {
                                __result = _insertResource(__code.ToString(), defaultName);
                            }
                        }
                        else
                        {
                            __result = _insertResource(__code.ToString(), defaultName);
                        }
                    }
                    else
                    {
                        __result = _insertResource(__code.ToString(), defaultName);
                    }
                }
                __result._str = string.Concat(__wordBegin, __result._str);
                //Console.WriteLine(codeFirst + "," + __result._str);
                return (__result);
            }
            catch
            {
                __result._str = defaultName;
                return (__result);
            }
        }

        public static void _updateResource(string codeFirst, string name)
        {
            if (_resource != null && _resource.Rows.Count > 0)
            {
                DataRow dr = _resource.Rows.Find(codeFirst.ToString().ToLower()); //.Select("Product_id=2").FirstOrDefault(); // finds all rows with id==2 and selects first or null if haven't found any
                if (dr != null)
                {
                    dr[MyLib._myGlobal._languageNumber + 1] = name; //changes the Product_name
                }
            }
        }
    }
}
