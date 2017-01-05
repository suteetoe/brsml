using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Npgsql;
using Newtonsoft.Json;
using System.Net;
using System.Threading;

namespace SMLGetPrice
{
    public partial class Form1 : Form
    {
        List<_datacenterDataClass> __serverData = new List<_datacenterDataClass>();
        List<_clientDataClass> __clientData = new List<_clientDataClass>();
        List<_datacenterDataClass> __clientBarcode = new List<_datacenterDataClass>();
        Thread _processThread;

        _onAddChangeGridInvoke _addGrid;
        public Form1()
        {
            InitializeComponent();

            _routine __routine = new _routine();
            _global __global = __routine._loadConfig();
            this._smlServerTextBox.Text = __global._smlConnectProvider;
            this._smlUserTextBox.Text = __global._smlConnectUser;
            this._smlPasswordTextBox.Text = __global._smlConnectPassword;
            this._smlDatabaseNameTextBox.Text = __global._smlConnectDatabaseName;
            //
            this._dataCenterUrlTextBox.Text = __global._dataCenterConnectUrl;
            this._dataCenterUserTextBox.Text = __global._dataCenterConnectUser;
            this._dataCenterPasswordTextBox.Text = __global._dataCenterConnectPassword;
            this._dataCenterDatabaseNameTextBox.Text = __global._dataCenterConnectDatabaseName;
            this._dataCenterBranchCodeTextBox.Text = __global._dataCenterBranchCode;
            this._dataCenterProviderTextBox.Text = __global._dataCenterProvider;
            //
            this._testConnectDatabase();
            this._addGrid = new _onAddChangeGridInvoke(_addChangeGrid);
        }

        void _testConnectDatabase()
        {
            try
            {
                _routine __routine = new _routine();
                _global __global = __routine._loadConfig();
                NpgsqlConnection __conn = __routine._smlConnection(__global);
                __conn.Open();
                __conn.Close();
                this._postgreSqlConnectStatus.Text = "เชื่อม SML (" + __global._smlConnectProvider + "->" + __global._smlConnectDatabaseName + ") สำเร็จ";
                this._postgreSqlConnectStatus.ForeColor = Color.Blue;
            }
            catch
            {
                this._postgreSqlConnectStatus.Text = "เชื่อม SML ไม่สำเร็จ";
                this._postgreSqlConnectStatus.ForeColor = Color.Red;
            }
            try
            {
                _routine __routine = new _routine();
                _global __global = __routine._loadConfig();
                /*SqlConnection __conn = __routine._bcConnection(__global);
                __conn.Open();
                __conn.Close();
                this._microsoftSqlConnectStatus.Text = "เชื่อม CHAMP (" + __global._bcConnectProvider + "->" + __global._bcConnectDatabaseName + ") สำเร็จ";
                this._microsoftSqlConnectStatus.ForeColor = Color.Blue;*/
            }
            catch
            {
                //this._microsoftSqlConnectStatus.Text = "เชื่อม CHAMP ไม่สำเร็จ";
                //this._microsoftSqlConnectStatus.ForeColor = Color.Red;
            }
        }

        private void _smlConnectButton_Click(object sender, EventArgs e)
        {
            try
            {
                _routine __routine = new _routine();
                _global __global = __routine._loadConfig();
                __global._smlConnectProvider = this._smlServerTextBox.Text.Trim();
                __global._smlConnectUser = this._smlUserTextBox.Text.Trim();
                __global._smlConnectPassword = this._smlPasswordTextBox.Text.Trim();
                __global._smlConnectDatabaseName = this._smlDatabaseNameTextBox.Text.Trim();
                NpgsqlConnection __conn = __routine._smlConnection(__global);
                __conn.Open();
                __conn.Close();
                this._smlConnectStatusTextBox.Text = "เชื่อมต่อสำเร็จ";
                __routine._saveConfig(__global);
            }
            catch (Exception __ex)
            {
                this._smlConnectStatusTextBox.Text = "เชื่อมต่อไม่สำเร็จ : " + __ex.Message.ToString();
            }
            this._testConnectDatabase();
        }

        private void _dataCenterConnectButton_Click(object sender, EventArgs e)
        {
            _routine __routine = new _routine();
            _global __global = __routine._loadConfig();
            __global._dataCenterConnectUrl = this._dataCenterUrlTextBox.Text;
            __global._dataCenterConnectUser = this._dataCenterUserTextBox.Text;
            __global._dataCenterConnectPassword = this._dataCenterPasswordTextBox.Text;
            __global._dataCenterConnectDatabaseName = this._dataCenterDatabaseNameTextBox.Text;
            __global._dataCenterBranchCode = this._dataCenterBranchCodeTextBox.Text;
            __global._dataCenterProvider = this._dataCenterProviderTextBox.Text;
            //
            this._testConnectDatabase();
            this._dataCenterConnectStatusTextBox.Text = "เชื่อมต่อสำเร็จ";
            __routine._saveConfig(__global);
        }

        private void _compareButton_Click(object sender, EventArgs e)
        {
            this._dataGridView.Rows.Clear();
            _routine __routine = new _routine();
            _global __global = __routine._loadConfig();
            using (WebClient __webClient = new System.Net.WebClient())
            {
                WebClient __n = new WebClient();
                // var __json = __n.DownloadString("http://smldata2.smldatacenter.com:8088/SMLJavaRESTService/service/pricelist/barcodeprice?provider=datatest&dbname=smlpricecenter&branch=X01");
                var __json = __n.DownloadString(__global._dataCenterConnectUrl + "/SMLJavaRESTService/service/pricelist/barcodeprice?provider=" + __global._dataCenterProvider + "&dbname=" + __global._dataCenterConnectDatabaseName + "&branch=" + __global._dataCenterBranchCode.ToUpper());
                this.__serverData = JsonConvert.DeserializeObject<List<_datacenterDataClass>>(__json);
                this.__serverData.Sort((__x, __y) => __x.barcode.CompareTo(__y.barcode));
                this.__clientData.Clear();
                this.__clientBarcode.Clear();
                {
                    NpgsqlConnection __connPostgre = __routine._smlConnection(__global);
                    __connPostgre.Open();
                    NpgsqlCommand __command = new NpgsqlCommand("select barcode,price, ic_code,(select name_1 from ic_inventory where code=ic_code) as ic_name,unit_code,(select name_1 from ic_unit where code=unit_code) as unit_name from ic_inventory_barcode order by barcode", __connPostgre);
                    NpgsqlDataReader __dr = __command.ExecuteReader();
                    while (__dr.Read())
                    {
                        _clientDataClass __data = new _clientDataClass();
                        __data._barcode = __dr[0].ToString();
                        __data._price = Double.Parse(__dr[1].ToString());
                        this.__clientData.Add(__data);

                        _datacenterDataClass __barcode = new _datacenterDataClass();
                        __barcode.barcode = __dr[0].ToString();
                        __barcode.price = Double.Parse(__dr[1].ToString());

                        __barcode.itemCode = __dr[2].ToString();
                        __barcode.itemName = __dr[3].ToString();
                        __barcode.unitCode = __dr[4].ToString();
                        __barcode.unitName = __dr[5].ToString();
                        __clientBarcode.Add(__barcode);
                    }
                    __connPostgre.Close();
                    this.__clientData.Sort((__x, __y) => __x._barcode.CompareTo(__y._barcode));
                }

                // process compare
                __curentProcess = 0;
                __maxProcess = this.__serverData.Count;

                this._progressBar.Value = 0;
                this._progressBar.Maximum = __maxProcess;

                timer1.Start();
                this._dataGridView.Rows.Clear();
                this._dataGridView.Invalidate();
                _onProcess = true;

                // start thread
                //_process();
                _processThread = new Thread(new ThreadStart(_process));
                this._processThread.IsBackground = true;
                this._processThread.Start();
            }

        }

        Boolean _onProcess = false;
        int __curentProcess = 0;
        int __maxProcess = 0;

        void _addChangeGrid(_datacenterDataClass __dataRow)
        {
            this._dataGridView.Rows.Add(__dataRow.barcode, __dataRow.itemCode, __dataRow.itemName, __dataRow.unitCode, __dataRow.unitName, __dataRow.oldPrice, __dataRow.price);
        }

        private delegate void _onAddChangeGridInvoke(_datacenterDataClass dataRow);

        void _process()
        {
            {
                //NpgsqlConnection __connPostgre = __routine._smlConnection(__global);
                //__connPostgre.Open();
                for (int __row = 0; __row < this.__serverData.Count; __row++)
                {
                    _datacenterDataClass __dataRow = this.__serverData[__row];
                    if (__dataRow.barcode.Trim().Length > 0)
                    {
                        int __addr = this.__clientData.FindIndex(delegate (_clientDataClass __data)
                        {
                            return __data._barcode.Equals(__dataRow.barcode, StringComparison.Ordinal);
                        });
                        if (__addr == -1)
                        {
                            // ไม่พบ Barcode
                        }
                        else
                        {
                            _clientDataClass __oldData = this.__clientData[__addr];
                            if (__oldData._price != this.__serverData[__row].price)
                            {
                                /*
                                NpgsqlCommand __command = new NpgsqlCommand("select ic_code,(select name_1 from ic_inventory where code=ic_code) as ic_name,unit_code,(select name_1 from ic_unit where code=unit_code) as unit_name,price from ic_inventory_barcode where barcode=\'" + __dataRow.barcode + "\'", __connPostgre);
                                NpgsqlDataReader __dr = __command.ExecuteReader();
                                if (__dr.Read())
                                {
                                    this.__serverData[__row].itemCode = __dr[0].ToString();
                                    this.__serverData[__row].itemName = __dr[1].ToString();
                                    this.__serverData[__row].unitCode = __dr[2].ToString();
                                    this.__serverData[__row].unitName = __dr[3].ToString();
                                    this.__serverData[__row].oldPrice = double.Parse(__dr[4].ToString());
                                    __dataRow = this.__serverData[__row];
                                    this._dataGridView.Rows.Add(__dataRow.barcode, __dataRow.itemCode, __dataRow.itemName, __dataRow.unitCode, __dataRow.unitName, __dataRow.oldPrice, __dataRow.price);
                                }
                                else
                                {

                                }
                                __dr.Close();
                                __command.Dispose();
                                */

                                // โต๋ ลอง compare ใน memory
                                int __barcodeAddr = this.__clientBarcode.FindIndex(delegate (_datacenterDataClass __data)
                                {
                                    return __data.barcode.Equals(__dataRow.barcode, StringComparison.Ordinal);
                                });

                                if (__barcodeAddr != -1)
                                {
                                    _datacenterDataClass __barcode = this.__clientBarcode[__barcodeAddr];
                                    this.__serverData[__row].itemCode = __barcode.itemCode;
                                    this.__serverData[__row].itemName = __barcode.itemName;
                                    this.__serverData[__row].unitCode = __barcode.unitCode;
                                    this.__serverData[__row].unitName = __barcode.unitName;
                                    this.__serverData[__row].oldPrice = __barcode.price;
                                    __dataRow = this.__serverData[__row];

                                    this.Invoke(_addGrid, new object[] { __dataRow });

                                    //this._dataGridView.Rows.Add(__dataRow.barcode, __dataRow.itemCode, __dataRow.itemName, __dataRow.unitCode, __dataRow.unitName, __dataRow.oldPrice, __dataRow.price);
                                }


                            }
                        }
                        //
                    }
                    __curentProcess++;
                }
                //__connPostgre.Close();
            }
            _onProcess = false;
        }

        private void _updatePriceButton_Click(object sender, EventArgs e)
        {
            _validForm __valid = new _validForm();
            __valid.ShowDialog();
            if (__valid._pass)
            {
                _routine __routine = new _routine();
                _global __global = __routine._loadConfig();
                for (int __row = 0; __row < this._dataGridView.Rows.Count; __row++)
                {
                    string __barCode = this._dataGridView.Rows[__row].Cells[0].Value.ToString().Trim().ToUpper();
                    double __priceNew = Double.Parse(this._dataGridView.Rows[__row].Cells[6].Value.ToString());
                    try
                    {
                        NpgsqlConnection __connPostgre = __routine._smlConnection(__global);
                        __connPostgre.Open();
                        NpgsqlCommand __command = new NpgsqlCommand("update ic_inventory_barcode set price=" + __priceNew.ToString() + " where barcode=\'" + __barCode + "\'", __connPostgre);
                        try
                        {
                            __command.ExecuteNonQuery();
                        }
                        catch (Exception __ex)
                        {
                            MessageBox.Show(__ex.Message.ToString());
                        }
                        __command.Dispose();
                        __connPostgre.Close();
                    }
                    catch (Exception __ex)
                    {
                        MessageBox.Show(__ex.Message.ToString());
                    }
                }
                MessageBox.Show("Success");
                this._dataGridView.Rows.Clear();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //long __onProcessPercent =((__curentProcess / __maxProcess) * 100);
            this._progressBar.Value = __curentProcess;// Convert.ToInt32(__onProcessPercent);

            this._processLabel.Text = __curentProcess + "/" + this.__serverData.Count;
            if (_onProcess == false)
            {
                this.timer1.Stop();
                this._progressBar.Enabled = false;
                __curentProcess = 0;
                __maxProcess = 0;
                this._progressBar.Value = 0;
                this._processLabel.Text = "";

                if (this._dataGridView.Rows.Count == 0)
                {
                    MessageBox.Show("ไม่มีราคาเปลี่ยนแปลง");
                }
                else
                {
                    MessageBox.Show("มีราคาเปลี่ยนแปลง กรุณาตรวจสอบ และกดปุ่มเปลี่ยนให้เป็นราคาใหม่");
                }

            }
        }

        private void search()
        {
            string searchValue = this._searchText.Text;

            this._dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            try
            {
                foreach (DataGridViewRow row in this._dataGridView.Rows)
                {
                    if (row.Cells[2].Value.ToString().ToLower().IndexOf(searchValue.ToLower()) != -1)
                    {
                        row.Selected = true;
                        break;
                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void _searchText_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                search();
            }
        }
    }
}