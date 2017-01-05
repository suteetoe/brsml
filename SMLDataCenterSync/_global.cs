using System;
using System.Collections.Generic;
using System.Text;

namespace SMLDataCenterSync
{
    public class _syncTableListStruct
    {
        public string _tableName = "";
        /// <summary>
        /// 1=ดึงข้อมูลจาก Server,2=ส่งข้อมูลเข้า Server
        /// </summary>
        public int _transferType = 0;
        public int _limitSendRecord = 5000;
        public int _limitReceiveRecord = 5000;
        public string _targetTableName = "";
    }

    public struct _syncFieldListStruct
    {
        public string _fieldNameForSelect;
        public string _fieldNameForInsertUpdate;
        public string _fieldType;
        public string _fieldDefaultValue;
    }
}
