﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StockWatch.Domain {
    public class Stock : IActivatable {
        public bool Active { get; set; } = true;
        [Key]
        public string Ticker { get; set; }
        public decimal Price { get; set; }
        public decimal HighPrice { get; set; }
        public decimal LowPrice { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public IList<Transaction> Transactions { get; set; }
        public decimal Change { get; set; }
        public decimal OpenPrice { get; set; }
    }
}