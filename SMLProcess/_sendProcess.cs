using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data;

namespace SMLProcess
{
    public static class _sendProcess
    {
        public static void _process(_sendProcessType processType, string whereIn)
        {
            SMLERPGlobal._smlFrameWork __ws = new SMLERPGlobal._smlFrameWork();
            string __commandName = "";
            switch (processType)
            {
                case _sendProcessType.ap_supplier: // ประมวลผลยอดคงเหลือเจ้าหนี้
                    __commandName = "ap_supplier";
                    break;
            }
            if (__commandName.Length > 0)
            {
                string __query = "insert into " + _g.d.process._table + " (" + _g.d.process._process_name + "," + _g.d.process._wherein + ") values (\'" + __commandName + "\',\'" + whereIn.ToUpper() + "\')";
                string __result = __ws._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __query);
                if (__result.Length != 0)
                {
                    MessageBox.Show(__result, "Error");
                }
            }
        }

        public static int _processCount = 0;
        public static void _procesNow()
        {
            SMLERPGlobal._smlFrameWork __smlFrameWork = new SMLERPGlobal._smlFrameWork();
            // Clear
            if (++_processCount > 60)
            {
                __smlFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, "update process set last_guid=\'\'");
                _processCount = 0;
            }
            //
            string __guid = Guid.NewGuid().ToString().ToLower();
            string __query = "select " + MyLib._myGlobal._getTopAndLimitOneRecord(100)[0].ToString() + " roworder," + _g.d.process._process_name + " from " + _g.d.process._table + " where " + _g.d.process._last_guid + "=\'\' or " + _g.d.process._last_guid + " is null " + MyLib._myGlobal._getTopAndLimitOneRecord(100)[1].ToString();
            DataTable __getProcess = __smlFrameWork._query(MyLib._myGlobal._databaseName, __query).Tables[0];
            StringBuilder __rowOrderPack = new StringBuilder();
            for (int __loop = 0; __loop < __getProcess.Rows.Count; __loop++)
            {
                string __roworder = __getProcess.Rows[__loop][0].ToString();
                if (__rowOrderPack.Length > 0)
                {
                    __rowOrderPack.Append(",");
                }
                __rowOrderPack.Append(__roworder);
            }
            if (__rowOrderPack.Length > 0)
            {
                try
                {
                    string __result = __smlFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, "update " + _g.d.process._table + " set " + _g.d.process._last_guid + "=\'" + __guid + "\' where roworder in (" + __rowOrderPack.ToString() + ") and (" + _g.d.process._last_guid + "=\'\' or " + _g.d.process._last_guid + " is null)");
                    DataTable __myProcess = __smlFrameWork._query(MyLib._myGlobal._databaseName, "select distinct " + _g.d.process._process_name + " from " + _g.d.process._table + " where " + _g.d.process._last_guid + "=\'" + __guid + "\'").Tables[0];
                    if (__myProcess.Rows.Count > 0)
                    {
                        __smlFrameWork._process(__guid);
                    }
                }
                catch
                {
                }
                __smlFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, "update " + _g.d.process._table + " set " + _g.d.process._last_guid + "=null where " + _g.d.process._last_guid + "=\'" + __guid + "\'");
                __smlFrameWork._processKiller();
            }
        }
    }

    public enum _sendProcessType
    {
        /// <summary>
        /// ประมวลผลยอดคงเหลือเจ้าหนี้
        /// </summary>
        ap_supplier
    }
}
