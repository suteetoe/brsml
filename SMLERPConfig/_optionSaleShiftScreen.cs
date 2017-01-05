using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace SMLERPConfig
{
    public partial class _optionSaleShiftScreen : UserControl
    {
        public _optionSaleShiftScreen()
        {
            InitializeComponent();
            if (this.DesignMode == false)
            {
                this._saleShiftScreen._table_name = _g.d.erp_option._table;
                this._saleShiftScreen._maxColumn = 1;
                this._saleShiftScreen._addTextBox(1, 0, _g.d.erp_option._sale_shift_id, 0);
            }
        }

        private void _createShiftButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("ยืนยันการสร้างรอบการขายใหม่", "confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
            {
                MyLib._myFrameWork __myFramework = new MyLib._myFrameWork();

                if (MyLib._myGlobal._OEMVersion == "tvdirect")
                {
                    if (_g.g._companyProfile._activeSync == true && _g.g._companyProfile._use_sale_shift == true)
                    {
                        Boolean __pass = false;
                        // check sync old record transfer to server
                        // check ic_trans & ic_trans_detail
                        StringBuilder __queryList = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");

                        string __checkTransQuery = "select count(*) as xcount from ic_trans where send_success = false ";
                        __queryList.Append(MyLib._myUtil._convertTextToXmlForQuery(__checkTransQuery));

                        string __checkTransDetailQuery = "select count(*) as xcount from ic_trans_detail where send_success = false ";
                        __queryList.Append(MyLib._myUtil._convertTextToXmlForQuery(__checkTransDetailQuery));

                        string __checkApArTransQuery = "select count(*) as xcount from ap_ar_trans where send_success = false ";
                        __queryList.Append(MyLib._myUtil._convertTextToXmlForQuery(__checkApArTransQuery));

                        string __checkApArTransDetailQuery = "select count(*) as xcount from ap_ar_trans_detail where send_success=  false";
                        __queryList.Append(MyLib._myUtil._convertTextToXmlForQuery(__checkApArTransDetailQuery));

                        string __checkCbTransQuery = "select count(*) as xcount from cb_trans where send_success = false";
                        __queryList.Append(MyLib._myUtil._convertTextToXmlForQuery(__checkCbTransQuery));

                        string __checkCbTransDetailQuery = "select count(*) as xcount from cb_trans_detail where send_success= false";
                        __queryList.Append(MyLib._myUtil._convertTextToXmlForQuery(__checkCbTransDetailQuery));

                        string __checkCustomerQuery = "select count(*) as xcount from ar_customer where send_success= false";
                        __queryList.Append(MyLib._myUtil._convertTextToXmlForQuery(__checkCustomerQuery));

                        string __checkPosconfig = "select count(*) as xcount from posconfig ";
                        __queryList.Append(MyLib._myUtil._convertTextToXmlForQuery(__checkPosconfig));

                        __queryList.Append("</node>");
                        // check ar_customer

                        ArrayList __result = __myFramework._queryListGetData(MyLib._myGlobal._databaseName, __queryList.ToString());
                        Boolean __foundPosConfig = false;

                        if (__result.Count > 0)
                        {
                            try
                            {
                                for (int __i = 0; __i < 7; __i++)
                                {
                                    DataSet __ds = ((DataSet)__result[__i]);

                                    if (__ds.Tables.Count > 0 && __ds.Tables[0].Rows.Count > 0)
                                    {
                                        decimal __xcount = MyLib._myGlobal._decimalPhase(__ds.Tables[0].Rows[0][0].ToString());
                                        if (__xcount == 0)
                                        {
                                            __pass = true;
                                        }
                                        else
                                        {
                                            string __tableName = "";
                                            switch (__i)
                                            {
                                                case 1:
                                                    __tableName = "ic_trans_detail";
                                                    break;
                                                case 2:
                                                    __tableName = "ap_ar_trans";
                                                    break;
                                                case 3:
                                                    __tableName = "ap_ar_trans_detail";
                                                    break;
                                                case 4:
                                                    __tableName = "cb_trans";
                                                    break;
                                                case 5:
                                                    __tableName = "cb_trans_detail";
                                                    break;
                                                case 6:
                                                    __tableName = "ar_customer";
                                                    break;
                                                default:
                                                    __tableName = "ic_trans";
                                                    break;
                                            }
                                            MessageBox.Show(__tableName + " ส่งข้อมูลไม่ครบ กรุณาตรวจสอบรายการส่งข้อมูล", "warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                            __pass = false;
                                            break;
                                        }
                                    }
                                }

                                // checkconfig 
                                DataTable __dt = ((DataSet)__result[7]).Tables[0];
                                if (MyLib._myGlobal._decimalPhase(__dt.Rows[0][0].ToString()) > 0)
                                {
                                    __foundPosConfig = true;
                                }


                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.ToString());
                                __pass = false;
                            }
                        }
                        else
                        {
                            MessageBox.Show(__result.ToString());
                        }

                        if (__pass == true)
                        {
                            // clear old data
                            string __resultStr = "";

                            // check old id
                            string __getSaleShiftOldId = this._saleShiftScreen._getDataStr(_g.d.erp_option._sale_shift_id).ToString();
                            string __sale_shift_id = "";
                            if (__getSaleShiftOldId.Equals(_g.g._companyProfile._sale_shift_id))
                            {
                                // gen ใหม่

                                // check gen new sale_shift_id            
                                string __guid = Guid.NewGuid().ToString();
                                __guid = __guid.Substring(0, 8);
                                string __shiftdate = DateTime.Now.ToString("yyMMdd", new System.Globalization.CultureInfo("en-US"));
                                __sale_shift_id = string.Format("{2}{0}-{1}", __shiftdate, __guid, _g.g._companyProfile._activeSyncBranchCode.ToUpper());

                            }
                            else
                            {
                                __sale_shift_id = __getSaleShiftOldId;
                                _g.g._companyProfile._sale_shift_id = __sale_shift_id;

                            }

                            string __customerFormat = __sale_shift_id.Substring(0, 20).ToUpper();

                            StringBuilder __query = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");

                            // truncate 
                            // ic_trans
                            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("truncate " + _g.d.ic_trans._table + ""));
                            // ic_trans_detail
                            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("truncate " + _g.d.ic_trans_detail._table + ""));
                            // cb_trans
                            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("truncate " + _g.d.cb_trans._table + ""));
                            // cb_trans_detai
                            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("truncate " + _g.d.cb_trans_detail._table + ""));
                            // ap_ar_trans
                            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("truncate " + _g.d.ap_ar_trans._table + ""));
                            // ap_ar_trans_detail
                            __query.Append(MyLib._myUtil._convertTextToXmlForQuery( "truncate " + _g.d.ap_ar_trans_detail._table + ""));
                            // ar_customer
                            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("truncate " + _g.d.ar_customer._table + ""));

                            
                            // insert erp_doc_format ar_customer
                            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.erp_doc_format._table + " where " + MyLib._myGlobal._addUpper(_g.d.erp_doc_format._screen_code) + "=\'AR\'"));
                            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.erp_doc_format._table + " (" + _g.d.erp_doc_format._screen_code + "," + _g.d.erp_doc_format._code + "," + _g.d.erp_doc_format._name_1 + "," + _g.d.erp_doc_format._format + " ) values (\'AR\', \'AR\', \'ทะเบียนลูกค้า\', \'" + __customerFormat + "-####\')"));
                            
                            // insert customer default
                            string __cust_default_id = string.Format("{0}-0000", __customerFormat);
                            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.ar_customer._table + "(code, name_1,status, " + _g.d.ar_customer._sale_shift_id + ") values (\'" + __cust_default_id + "\', \'ลูกค้าเริ่มต้น " + __sale_shift_id + "\',1, \'" + __sale_shift_id + "\')"));

                            // update customer default for pos
                            if (__foundPosConfig == true)
                                __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.POSConfig._table + " set " + _g.d.POSConfig._cust_code_default + "=\'" + __cust_default_id + "\'"));
                            else
                                __query.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.POSConfig._table + " (" + _g.d.POSConfig._pos_config_number + "," + _g.d.POSConfig._cust_code_default + " ) values (1,\'" + __cust_default_id + "\')"));

                            // update global
                            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.erp_option._table + " set " + _g.d.erp_option._sale_shift_id + "=\'" + __sale_shift_id + "\'"));

                            __query.Append("</node>");

                            __resultStr = __myFramework._queryList(MyLib._myGlobal._databaseName, __query.ToString());
                            if (__resultStr.Length == 0)
                            {
                                this._saleShiftScreen._setDataStr(_g.d.erp_option._sale_shift_id, __sale_shift_id);
                                _g.g._companyProfile._sale_shift_id = __sale_shift_id;

                                MessageBox.Show("สร้างรอบการขายสำเร็จ", "success");
                            }
                            else
                            {
                                MessageBox.Show(__resultStr.ToString(), "warning");
                            }
                        }
                    }
                }

            }

        }


    }
}
