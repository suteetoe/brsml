using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMLERPARAPInfo
{
    public class _selectMenu
    {
        public static Control _getObject(string menuName)
        {
            switch (menuName.ToLower())
            {
                case "menu_ar_analytics_movement": return (new _movement(_apArConditionEnum.ลูกหนี้_เคลื่อนไหว)); // เคลื่อนไหวลูกหนี้
                case "menu_ar_analytics_ageing": return (new _ageing(_apArConditionEnum.ลูกหนี้_อายุลูกหนี้)); // อายุลูกหนี้
                case "menu_ar_analytics_ageing_by_doc": return (new _ageingDoc(_apArConditionEnum.ลูกหนี้_อายุลูกหนี้_แยกลูกหนี้_แยกเอกสาร)); // อายุลูกหนี้ตามเอกสาร
                case "menu_ar_analytics_stat": return (new _stat( _apArConditionEnum.ลูกหนี้_สถานะลูกหนี้)); // สถานะลูกหนี้
                //
                case "menu_ap_analytics_movement": return (new _movement( _apArConditionEnum.เจ้าหนี้_เคลื่อนไหว)); // เคลื่อนไหวเจ้าหนี้
                case "menu_ap_analytics_ageing": return (new _ageing(_apArConditionEnum.เจ้าหนี้_อายุเจ้าหนี้)); // อายุเจ้าหนี้
                case "menu_ap_analytics_ageing_by_doc": return (new _ageingDoc(_apArConditionEnum.เจ้าหนี้_อายุเจ้าหนี้_แยกลูกหนี้_แยกเอกสาร)); // อายุเจ้าหนี้ตามเอกสาร
                case "menu_ap_analytics_stat": return (new _stat(_apArConditionEnum.เจ้าหนี้_สถานะเจ้าหนี้)); // สถานะเจ้าหนี้
            }
            return null;
        }
    }
}
