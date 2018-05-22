using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMLDataAPI
{
    class _arCustomer : _processBase
    {
        public _arCustomer()
        {
            this.tableName = "ar_customer";
        }

        protected override void _setdata(ArrayList _getData)
        {
            DataSet __data = (DataSet)_getData[0];

            for (int __row = 0; __row < __data.Tables[0].Rows.Count; __row++)
            {
                Console.WriteLine(__data.Tables[0].Rows[__row]["code"].ToString());

            }
        }
    }
}
