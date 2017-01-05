using System;
using System.Collections.Generic;
using System.Text;

namespace SMLGetPrice
{
    public class _datacenterDataClass
    {
        public double price { get; set; }
        public string barcode { get; set; }
        public string itemCode;
        public string itemName;
        public string unitCode;
        public string unitName;
        public double oldPrice;
    }

    public class _clientDataClass
    {
        public string _barcode;
        public double _price;
    }
    
}
