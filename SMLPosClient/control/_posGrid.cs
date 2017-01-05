using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace SMLPosClient.control
{
    class _posGrid : MyLib._myGrid
    {
        string _price_type_column = "price_type";
        private string _idResult;
        public string _id
        {
            get { return _idResult; }
            set { _idResult = value; }
        }

        public _posGrid()
        {
            this.IsEdit = false;
            this._beforeDisplayRow += new MyLib.BeforeDisplayRowEventHandler(_posGrid__beforeDisplayRow);
            this._totalCheck += new MyLib.TotalCheckEventHandler(_posGrid__totalCheck);
        }

        bool _posGrid__totalCheck(object sender, int row,int column)
        {
            int __barcodeColumnNumber = this._findColumnByName(_g.d.ic_trans_detail._barcode);
            int __qtyColumnNumber = this._findColumnByName(_g.d.ic_trans_detail._qty);
            if (__qtyColumnNumber == column)
            {
                if (this._cellGet(row, __barcodeColumnNumber).ToString().Trim().Length == 0)
                {
                    return false;
                }
            }
            return true;
        }

        MyLib.BeforeDisplayRowReturn _posGrid__beforeDisplayRow(MyLib._myGrid sender, int row, int columnNumber, string columnName, MyLib.BeforeDisplayRowReturn senderRow, MyLib._myGrid._columnType columnType, System.Collections.ArrayList rowData)
        {
            int __columnIsPrice = this._findColumnByName(_price_type_column);
            if (__columnIsPrice != 1)
            {
                int __type = (int)this._cellGet(row, __columnIsPrice);

                switch (__type)
                {
                    case 2:
                        senderRow.newColor = Color.Magenta;
                        break;
                    case 3:
                        senderRow.newColor = Color.MediumVioletRed;
                        break;
                    case 6:
                        senderRow.newColor = Color.DarkOrange;
                        break;
                    case 7:
                        senderRow.newColor = Color.Blue;
                        break;
                    default:
                        senderRow.newColor = Color.Black;
                        break;
                }
                //{
                //    senderRow.newColor = Color.YellowGreen;
                //}
            }

            return (senderRow);
        }
    }
}
