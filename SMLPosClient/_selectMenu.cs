using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using MyLib;
using System.IO;
using System.Xml.Serialization;

namespace SMLPosClient
{
    public class _selectMenu
    {
        public static Control _getObject(string menuName, string screenName)
        {
            switch (menuName.ToLower())
            {
                case "menu_config_pos_screen":
                    MyLib._myUtil._startDialog(MyLib._myGlobal._mainForm, screenName, new SMLPosClient._configPOSScreen());
                    break;
                case "menu_pos_screen":


                    // wraning ไม่ได้กำหนดจอขาย
                    string _savePosScreenConfigFileName = "smlPOSScreenConfig";
                    string __configFileName = _savePosScreenConfigFileName + MyLib._myGlobal._databaseName + ".xml";
                    string __path = Path.GetTempPath() + "\\" + __configFileName.ToLower();

                    string __localPath = string.Format(@"c:\\smlsoft\\smlPOSScreenConfig-{0}-{1}-{2}.xml",  MyLib._myGlobal._encapeStringForFilePath(MyLib._myGlobal._getFirstWebServiceServer), MyLib._myGlobal._providerCode, MyLib._myGlobal._databaseName);

                    SMLPOSControl._posScreenConfig __config = null;
                    bool _masterScreenIsDefine = false;

                    if (MyLib._myGlobal._isDemo)
                    {
                        _masterScreenIsDefine = true;
                    }

                    // อ่าน config
                    try
                    {
                        TextReader readFile = new StreamReader(__localPath);
                        XmlSerializer __xsLoad = new XmlSerializer(typeof(SMLPOSControl._posScreenConfig));
                        __config = (SMLPOSControl._posScreenConfig)__xsLoad.Deserialize(readFile);
                        readFile.Close();
                    }
                    catch
                    {
                        try
                        {
                            TextReader readFile = new StreamReader(__path);
                            XmlSerializer __xsLoad = new XmlSerializer(typeof(SMLPOSControl._posScreenConfig));
                            __config = (SMLPOSControl._posScreenConfig)__xsLoad.Deserialize(readFile);
                            readFile.Close();
                        }
                        catch (Exception ex)
                        {
                        }
                    }

                    if (_masterScreenIsDefine == false)
                    {
                        if (__config == null)
                        {
                            _masterScreenIsDefine = false;
                        }
                        else
                        {
                            for (int __i = 0; __i < __config._screenConfig.Count; __i++)
                            {
                                if (((SMLPOSControl._screenConfig)__config._screenConfig[__i])._isMasterScreen && !((SMLPOSControl._screenConfig)__config._screenConfig[__i])._screen_code.Equals("None"))
                                {
                                    _masterScreenIsDefine = true;
                                }
                            }
                        }
                    }

                    if (_masterScreenIsDefine == false)
                    {
                        MessageBox.Show(MyLib._myGlobal._mainForm, "ยังไม่ได้กำหนด จอขายหลัก ให้กับระบบ POS", "wraning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return null;
                    }
                    _posLoginForm __login = new _posLoginForm();
                    if (MyLib._myGlobal._isDemo == false)
                    {
                        __login.ShowDialog(MyLib._myGlobal._mainForm);
                    }
                    else
                    {
                        // demo user must fix name

                        __login._userCodeTextBox.Text = "DEMO";
                        __login._userCode = "DEMO";
                        __login._passwordTextBox.Text = "DEMO";
                        __login._isPassed = true;

                    }

                    if (__login._isPassed)
                    {
                        //_createAndSelectTab(menuName, menuName, __screenName, new _posClientForm(__login._userCode,__login._passwordTextBox.Text));
                        return new _posClientForm(__login._userCode, __login._passwordTextBox.Text);
                    }
                    break;
                case "menu_save_send_money_pos":
                    return new _posSaveSendMoney();
                // ส่งเงิน
                /*
                _manageMasterCodeFull __screenFull = new MyLib._manageMasterCodeFull();
                __screenFull._labelTitle.Text = screenName;
                __screenFull._dataTableName = _g.d.POSCashierSettle._table;
                __screenFull._addColumn(_g.d.POSCashierSettle._DocNo, 10, 100);
                __screenFull._inputScreen._addDateBox(__screenFull._rowScreen++, 0, 1, 0, _g.d.POSCashierSettle._DocDate, 1, true);
                //__screenFull._addColumn(_g.d.POSCashierSettle._DocDate, 100, 100);
                __screenFull._addColumn(_g.d.POSCashierSettle._CashierCode, 100, 100);
                __screenFull._addColumn(_g.d.POSCashierSettle._MACHINECODE, 100, 100);
                __screenFull._addColumn(_g.d.POSCashierSettle._POS_ID, 100, 100);
                __screenFull._inputScreen._addNumberBox(__screenFull._rowScreen++, 0, 1, 0, _g.d.POSCashierSettle._CashAmount, 1, 2, true);
                __screenFull._inputScreen._addNumberBox(__screenFull._rowScreen++, 0, 1, 0, _g.d.POSCashierSettle._CreditCardAmount, 1, 2, true);
                __screenFull._inputScreen._addNumberBox(__screenFull._rowScreen++, 0, 1, 0, _g.d.POSCashierSettle._CoupongAmount, 1, 2, true);
                __screenFull._inputScreen._addNumberBox(__screenFull._rowScreen++, 0, 1, 0, _g.d.POSCashierSettle._ChqAmount, 1, 2, true);
                __screenFull._inputScreen._addNumberBox(__screenFull._rowScreen++, 0, 1, 0, _g.d.POSCashierSettle._TransferAmount, 1, 2, true);
                __screenFull._finish();
                return __screenFull;
                */
                case "menu_save_pos_receive_money":
                    // รับเงินทอน
                    return new _posSaveMoneyReceive();
                case "pm_counseling":

                    // เช็ค config ก่อน ถ้า โหลด config ไม่ขึ้นให้ฟ้อง ดีมะ
                    string __labelConfigFileName = string.Format(MyLib._myGlobal._smlConfigFile + "{2}-{0}-{1}-LabelConfig.xml", MyLib._myGlobal._encapeStringForFilePath(MyLib._myGlobal._getFirstWebServiceServer), MyLib._myGlobal._providerCode, MyLib._myGlobal._databaseName).ToLower();

                    if (MyLib._myUtil._fileExists(__labelConfigFileName))
                    {
                        // จอเภสัช
                        _posLoginForm __pharmacistLogin = new _posLoginForm();
                        __pharmacistLogin.ShowDialog();
                        if (__pharmacistLogin._isPassed)
                        {
                            _posClientForm __pharmacistForm = new _posClientForm("SOM001", true, __pharmacistLogin._userCode, __pharmacistLogin._passwordTextBox.Text);
                            return __pharmacistForm;
                        }
                    }
                    else
                    {
                        MessageBox.Show("ยังไม่ได้กำหนดค่าฉลากยา", "กำหนดค่าฉลากยา", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    break;
                case "menu_config_print_label_healthy":
                    MyLib._myUtil._startDialog(MyLib._myGlobal._mainForm, screenName, new SMLPosClient._counSellingConfigForm());
                    break;
            }
            return null;
        }
    }
}
