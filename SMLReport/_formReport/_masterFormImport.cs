using System;
using System.Collections.Generic;
using System.Text;

namespace SMLReport._formReport
{
    public class _masterFormImport
    {
        public _masterFormImport()
        {

        }

        public static string _importForm(string formCode, string savename)
        {
            string __result = "";
            try
            {
                MyLib._myFrameWork __smlMasterFrameWork = new MyLib._myFrameWork(MyLib._myGlobal._masterWebservice, MyLib._myGlobal._masterConfigXmlName, MyLib._myGlobal._masterDatabaseType);

                string __query = "select " + _g.d.formdesign._formdesigntext + " from " + _g.d.formdesign._table + " where upper(" + _g.d.formdesign._formcode + ") = upper('" + formCode + "')";

                byte[] __byte = __smlMasterFrameWork._queryByte(MyLib._myGlobal._masterDatabaseName, __query);
                              
                //DataTable __table = __result.Tables[0];

                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                string __insQuery = string.Format("insert into " + _g.d.formdesign._table + "(" + _g.d.formdesign._formcode + "," + _g.d.formdesign._formname + "," + _g.d.formdesign._formguid_code+ ", " + _g.d.formdesign._formdesigntext + ") VALUES('{0}','{1}', '{2}', ?)", formCode, savename, Guid.NewGuid().ToString().ToUpper());
                
                __result = __myFrameWork._queryByteData(MyLib._myGlobal._databaseName, __insQuery, new object[] { __byte });                

            }
            catch
            {
            }

            return __result;
        }
    }
}
