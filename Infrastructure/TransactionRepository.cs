using StockWatch.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace StockWatch.Infrastructure {
    public class TransactionRepository : GenericRepository<Transaction>{

        public TransactionRepository(DbContext db) : base(db) { }
    }
}