using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MyLib;
using Crom.Controls.Docking;

namespace SMLHealthyConfig
{
    public class _selectMenu
    {
        MyLib._manageMasterCodeFull __screenFull;
        MyLib._myFrameWork _myFrameWork = new _myFrameWork();
        public static Control _getObject(string menuName, string screenName)
        {
            MyLib._manageMasterCodeFull __screenFull;
            switch (menuName.ToLower())
            {
                case "menu_ar_detail_healthy": return (new SMLERPAR._ar());
                case "menu_healthy_m_healty_standard": MyLib._myUtil._startDialog(MyLib._myGlobal._mainForm, screenName, new _m_information()); break; // กำหนดค่ามาตรฐานสุขภาพ*********
                case "menu_setup_ic_healthy_m_properties": // กำหนดรหัสสรรพคุณยา
                    __screenFull = new MyLib._manageMasterCodeFull();
                    __screenFull._labelTitle.Text = screenName;
                    __screenFull._dataTableName = _g.d.m_properties._table;
                    __screenFull._addColumn(_g.d.m_properties._code, 10, 10);
                    __screenFull._inputScreen._setUpper(_g.d.m_properties._code);
                    __screenFull._addColumn(_g.d.m_properties._name_1, 100, 30);
                    __screenFull._addColumn(_g.d.m_properties._name_2, 100, 30);
                    __screenFull._inputScreen._addCheckBox(__screenFull._rowScreen, 0, _g.d.m_properties._status, false, true, true);
                    __screenFull._finish();
                    return __screenFull;
                case "menu_setup_ic_healthy_m_mim_group": // ชื่อสามัญทางยา
                    __screenFull = new MyLib._manageMasterCodeFull();
                    __screenFull._labelTitle.Text = screenName;
                    __screenFull._dataTableName = _g.d.m_mim_group._table;
                    __screenFull._addColumn(_g.d.m_mim_group._code, 10, 10);
                    __screenFull._inputScreen._setUpper(_g.d.m_mim_group._code);
                    __screenFull._addColumn(_g.d.m_mim_group._name_1, 100, 30);
                    __screenFull._addColumn(_g.d.m_mim_group._name_2, 100, 30);
                    __screenFull._inputScreen._addCheckBox(__screenFull._rowScreen, 0, _g.d.m_mim_group._status, false, true, true);
                    __screenFull._finish();
                    return __screenFull;
                case "menu_setup_ic_healthy_m_times": // ช่วงเวลาการใช้ยา
                    __screenFull = new MyLib._manageMasterCodeFull();
                    __screenFull._labelTitle.Text = screenName;
                    __screenFull._dataTableName = _g.d.m_times._table;
                    __screenFull._addColumn(_g.d.m_times._code, 10, 10);
                    __screenFull._inputScreen._setUpper(_g.d.m_times._code);
                    __screenFull._addColumn(_g.d.m_times._name_1, 100, 30);
                    __screenFull._addColumn(_g.d.m_times._name_2, 100, 30);
                    __screenFull._inputScreen._addCheckBox(__screenFull._rowScreen, 0, _g.d.m_times._status, false, true, true);
                    __screenFull._finish();
                    return __screenFull;
                case "menu_setup_ic_healthy_m_frequency": // ข้อกำหนดเวลาใช้ยา
                    __screenFull = new MyLib._manageMasterCodeFull();
                    __screenFull._labelTitle.Text = screenName;
                    __screenFull._dataTableName = _g.d.m_frequency._table;
                    __screenFull._addColumn(_g.d.m_frequency._code, 10, 10);
                    __screenFull._inputScreen._setUpper(_g.d.m_frequency._code);
                    __screenFull._addColumn(_g.d.m_frequency._name_1, 100, 30);
                    __screenFull._addColumn(_g.d.m_frequency._name_2, 100, 30);
                    __screenFull._inputScreen._addCheckBox(__screenFull._rowScreen, 0, _g.d.m_frequency._status, false, true, true);
                    __screenFull._finish();
                    return __screenFull;
                case "menu_setup_ic_healthy_m_dozen": // ขนาดและวิธีใช้ยา
                    __screenFull = new MyLib._manageMasterCodeFull();
                    __screenFull._labelTitle.Text = screenName;
                    __screenFull._dataTableName = _g.d.m_dozen._table;
                    __screenFull._addColumn(_g.d.m_dozen._icode, 10, 10);
                    __screenFull._inputScreen._setUpper(_g.d.m_dozen._icode);
                    __screenFull._addColumn(_g.d.m_dozen._name_1, 100, 30);
                    __screenFull._addColumn(_g.d.m_dozen._name_2, 100, 30);
                    __screenFull._inputScreen._addCheckBox(__screenFull._rowScreen, 0, _g.d.m_dozen._status, false, true, true);
                    __screenFull._finish();
                    return __screenFull;

                case "menu_setup_ic_healthy_m_warning": // คำเตือนการใช้ยา
                    __screenFull = new MyLib._manageMasterCodeFull();
                    __screenFull._labelTitle.Text = screenName;
                    __screenFull._dataTableName = _g.d.m_warning._table;
                    __screenFull._addColumn(_g.d.m_warning._icode, 10, 10);
                    __screenFull._inputScreen._setUpper(_g.d.m_warning._icode);
                    __screenFull._addColumn(_g.d.m_warning._name_1, 100, 30);
                    __screenFull._addColumn(_g.d.m_warning._name_2, 100, 30);
                    __screenFull._inputScreen._addCheckBox(__screenFull._rowScreen, 0, _g.d.m_warning._status, false, true, true);
                    __screenFull._finish();
                    return __screenFull;
                case "menu_setup_ic_healthy_m_disease": // ชนิดโรค
                    __screenFull = new MyLib._manageMasterCodeFull();
                    __screenFull._labelTitle.Text = screenName;
                    __screenFull._dataTableName = _g.d.m_disease._table;
                    __screenFull._addColumn(_g.d.m_disease._code, 10, 10);
                    __screenFull._inputScreen._setUpper(_g.d.m_disease._code);
                    __screenFull._addColumn(_g.d.m_disease._name_1, 100, 20);
                    __screenFull._addColumn(_g.d.m_disease._name_2, 100, 20);
                    __screenFull._inputScreen._addTextBox(__screenFull._rowScreen, 0, 3, 0, _g.d.m_disease._symptom, 1, 0, 0, true, false, true);
                    __screenFull._rowScreen += 3;
                    __screenFull._inputScreen._addTextBox(__screenFull._rowScreen, 0, 3, 0, _g.d.m_disease._therapy, 1, 0, 0, true, false, true);
                    __screenFull._finish();
                    return __screenFull;
                case "menu_setup_ic_healthy_m_kkos": // ตารางความเสี่ยงกระดูกพรุน***
                    __screenFull = new MyLib._manageMasterCodeFull();
                    __screenFull._labelTitle.Text = screenName;
                    __screenFull._dataTableName = _g.d.m_kkos._table;
                    __screenFull._refFieldAdd = _g.d.m_kkos._age_max;
                    __screenFull._manageDataScreen._dataList._referFieldAdd(_g.d.m_kkos._age_max, 4);                    
                    __screenFull._manageDataScreen._dataList._gridData._addColumn("check", 11, 0, 10, false, false, false, false);
                    __screenFull._manageDataScreen._dataList._gridData._addColumn(_g.d.m_kkos._table + "." + _g.d.m_kkos._age_max, 2, 10, 40);
                    __screenFull._manageDataScreen._dataList._gridData._addColumn(_g.d.m_kkos._table + "." + _g.d.m_kkos._max_weight, 2, 10, 50);
                    __screenFull._inputScreen._addNumberBox(__screenFull._rowScreen++, 0, 0, 0, _g.d.m_kkos._age_max, 1, 0, true);
                    __screenFull._inputScreen._addNumberBox(__screenFull._rowScreen++, 0, 0, 0, _g.d.m_kkos._age_min, 1, 0, true);
                    __screenFull._inputScreen._addNumberBox(__screenFull._rowScreen++, 0, 0, 0, _g.d.m_kkos._max_weight, 1, 0, true);
                    __screenFull._inputScreen._addNumberBox(__screenFull._rowScreen++, 0, 0, 0, _g.d.m_kkos._min_weight, 1, 0, true);                                        
                    __screenFull._inputScreen._addNumberBox(__screenFull._rowScreen++, 0, 0, 0, _g.d.m_kkos._probability_osteoporosis, 1, 0, true);
                    __screenFull._inputScreen._addCheckBox(__screenFull._rowScreen++, 0, _g.d.m_kkos._status, false, true, true);
                    __screenFull._finish();
                    return __screenFull;
                case "menu_setup_ic_healthy_m_bodymassindex": // ดัชนีมวลกาย*****************
                    __screenFull = new MyLib._manageMasterCodeFull();
                   
                    __screenFull._labelTitle.Text = screenName;
                    __screenFull._dataTableName = _g.d.m_bodymassindex._table;
                    __screenFull._refFieldAdd = _g.d.m_bodymassindex._bmi;
                    __screenFull._manageDataScreen._dataList._referFieldAdd(_g.d.m_bodymassindex._bmi, 4);
                    __screenFull._manageDataScreen._dataList._gridData._addColumn("check", 11, 0, 10, false, false, false, false);
                    __screenFull._manageDataScreen._dataList._gridData._addColumn(_g.d.m_bodymassindex._table + "." + _g.d.m_bodymassindex._bmi, 2, 10, 50);
                    __screenFull._manageDataScreen._dataList._gridData._addColumn(_g.d.m_bodymassindex._table + "." + _g.d.m_bodymassindex._bmi_description, 1, 10, 50);
                    __screenFull._inputScreen._addTextBox(__screenFull._rowScreen++, 0, 0, 0, _g.d.m_bodymassindex._bmi, 1, 0, 0, true, false, true);
                    __screenFull._inputScreen._addNumberBox(__screenFull._rowScreen++, 0, 0, 0, _g.d.m_bodymassindex._bmi_max, 1, 0, true);
                    __screenFull._inputScreen._addNumberBox(__screenFull._rowScreen++, 0, 0, 0, _g.d.m_bodymassindex._bmi_min, 1, 0, true);
                    __screenFull._inputScreen._addTextBox(__screenFull._rowScreen++, 0, 3, 0, _g.d.m_bodymassindex._bmi_description, 1, 0, 0, true, false, true);
                    __screenFull._inputScreen._addCheckBox(__screenFull._rowScreen, 0, _g.d.m_bodymassindex._status, false, true, true);
                    __screenFull._finish();
                    return __screenFull;
                case "menu_setup_healthy_m_nationality": // สัญชาติ
                    __screenFull = new MyLib._manageMasterCodeFull();
                    __screenFull = new MyLib._manageMasterCodeFull();
                    __screenFull._labelTitle.Text = screenName;
                    __screenFull._dataTableName = _g.d.m_nationality._table;
                    __screenFull._addColumn(_g.d.m_nationality._code, 10, 10);
                    __screenFull._inputScreen._setUpper(_g.d.m_nationality._code);
                    __screenFull._addColumn(_g.d.m_nationality._name_1, 100, 30);
                    __screenFull._addColumn(_g.d.m_nationality._name_2, 100, 30);
                    __screenFull._inputScreen._addCheckBox(__screenFull._rowScreen, 0, _g.d.m_nationality._status, false, true, true);
                    __screenFull._finish();
                    return __screenFull;
                // ระบบข้อมูล menu_view_manager                            
                case "menu_import_data_master": return (new MyLib._databaseManage._importDataControl());
                case "menu_permissions_user": return (new MyLib._databaseManage._menupermissions_user());
                case "menu_permissions_group": return (new MyLib._databaseManage._menupermissions_group());
                case "menu_view_manager": return (new MyLib._databaseManage._viewManage(true));
                case "menu_database_struct": return (new MyLib._databaseManage._databaseStruct());
                case "menu_import": return (new MyLib._databaseManage._importExport._import());
                case "menu_query": return (new MyLib._databaseManage._queryDataView());
                case "menu_verify_database": MyLib._myUtil._startDialog(MyLib._myGlobal._mainForm, screenName, new MyLib._databaseManage._verifyDatabase()); break;
                case "menu_shink_database": MyLib._myUtil._startDialog(MyLib._myGlobal._mainForm, screenName, new MyLib._databaseManage._shrinkDatabase()); break;
                case "menu_change_password": MyLib._myUtil._startDialog(MyLib._myGlobal._mainForm, screenName, new MyLib._databaseManage._smlChangePassword("xxxxxxxx")); break;
            }
            return null;
        }
    }
}
