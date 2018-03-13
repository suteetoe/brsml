using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Json;
using System.Net;
using MyLib;

namespace SMLSINGHAControl
{
    public partial class _transferControl : UserControl
    {
        protected synctablename type = synctablename.Null;
        protected string uri = "";
        public synctablename _type
        {
            get { return this.type; }
            set {
                this.type = value;
                this._build();
            }

        }

        public _transferControl()
        {
            InitializeComponent();
        }
        protected virtual void _build()
        {
           
        }

        public void _loaddata()
        {
            //_singhaGridGetdata._priceStruct __result = new _singhaGridGetdata._priceStruct();

            WebClient __n = new WebClient();

            // string __url = __urlServerSplit[0] + "http://dev.smlsoft.com:7400/getdb/erp_expenses_list";

            string __getCompanyRestUrl = uri; //"http://192.168.2.98:7400/getdb/erp_expenses_list";
            _restClient __rest = new _restClient(__getCompanyRestUrl);
            string __response = __rest.MakeRequest();


            JsonValue __jsonObject = JsonValue.Parse(__response);

            if (__jsonObject.Count > 0) {
                this._preparedata(__jsonObject);
            }

        }

        protected virtual  void _preparedata(JsonValue jObj)
        {

        }

        protected virtual void _process()
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this._loaddata();
        }

        private void button_process_Click(object sender, EventArgs e)
        {
            this._process();
        }
    }
    public enum synctablename
    {
        erp_expenses_list,
        erp_bank,
        Null


    }
}
