using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Json;

namespace SMLSINGHAControl
{
    public partial class _branch_list : _transferControl
    {
        public _branch_list()
        {
            InitializeComponent();
        }



        //protected override void _build()
        //{
        //    switch (this.type)
        //    {
        //        case synctablename.erp_expenses_list:
        //            {
        //                this.uri = "http://dev.smlsoft.com:7400/getdb/erp_expenses_list";
        //            }
        //            break;
        //    }
        //}

        //protected override void _preparedata(JsonValue jObj)
        //{
        //    if (jObj.Count > 0)
        //    {
        //        string __value = "";
        //        string __value2 = "";
        //        // string __value = __jsonObject["code"].ToString();
        //        _singhaGridGetdata __grid = new _singhaGridGetdata();
        //        for (int __row1 = 0; __row1 < jObj.Count; __row1++)
        //        {
        //            __value = jObj[__row1]["code"].ToString().Replace("\"", string.Empty);
        //            __value2 = jObj[__row1]["name_1"].ToString().Replace("\"", string.Empty);
        //            this._singhaGridGetdata1._addRow(__row1);
        //            this._singhaGridGetdata1._cellUpdate(__row1, 0, __value, false);
        //            this._singhaGridGetdata1._cellUpdate(__row1, 1, __value2, false);
        //        }

        //    }
        //}

       


    }
}
