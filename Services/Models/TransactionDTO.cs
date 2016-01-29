using System;
using System.ComponentModel.DataAnnotations;

namespace StockWatch.Services.Models {
    public class TransactionDTO {
        public decimal Price { get; set; }
        [Required]
        public int Quantity { get; set; }
        public DateTime DateCreated { get; set; }
        [Required]
        public string Ticker { get; set; }
    }
}