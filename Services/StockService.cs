using Microsoft.AspNet.Identity;
using StockWatch.Domain;
using StockWatch.Infrastructure;
using StockWatch.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StockWatch.Services {
    public class StockService {

        private StockRepository _stockRepo;
        private TransactionRepository _transactionRepo;
        private ApplicationUserManager _userRepo;

        public StockService(StockRepository stockRepo, TransactionRepository transactionRepo, ApplicationUserManager userRepo) {
            _stockRepo = stockRepo;
            _transactionRepo = transactionRepo;
            _userRepo = userRepo;
        }

        public IList<StockDTO> Search(string searchTerms) {
            return (from s in _stockRepo.FindStocksLikeSearchTerms(searchTerms)
                    select new StockDTO() {
                        Ticker = s.Ticker,
                        Price = s.Price,
                        Name = s.Name,
                        OpenPrice = s.OpenPrice
                    }).ToList();
        }

        public ExpandedStockDTO GetStockWithTransactions(string ticker, string username) {
            return (from s in _stockRepo.FindStock(ticker)
                    select new ExpandedStockDTO() {
                       Ticker = s.Ticker,
                       Price = s.Price,
                       Name = s.Name,
                       OpenPrice = s.OpenPrice,
                       LowPrice = s.LowPrice,
                       HighPrice = s.HighPrice,
                       Description = s.Description,
                       Transactions = (from t in s.Transactions
                                       where t.User.UserName == username
                                       orderby t.DateCreated descending
                                       select new TransactionDTO() {
                                           Price = t.Price,
                                           Quantity = t.Quantity,
                                           DateCreated = t.DateCreated
                                       }).ToList()
                    }).FirstOrDefault();
        }

        public TransactionDTO Trade(string type, string ticker, int quantity, string username) {

            var stock = _stockRepo.FindStock(ticker).FirstOrDefault();
            var user = _userRepo.FindByName(username);

            var transaction = new Transaction() {
                Stock = stock,
                User = user,
                Price = stock.Price,
                Type = type,
                Quantity = type == "buy" ? quantity : -quantity
            };

            _transactionRepo.Add(transaction);
            _transactionRepo.SaveChanges();

            return new TransactionDTO() {
                Price = transaction.Price,
                Quantity = transaction.Quantity,
                DateCreated = transaction.DateCreated,
                Ticker = transaction.Stock.Ticker
            };
        }

        public bool CheckExists(string ticker) {
            return _stockRepo.CheckExists(ticker);
        }


        //public TransactionDTO Buy(string ticker, int quantity, string username) {

        //    var stock = _stockRepo.FindStock(ticker).FirstOrDefault();
        //    var user = _userRepo.FindByName(username);

        //    var buy = new Transaction() {
        //        Stock = stock,
        //        User = user,
        //        Price = stock.Price,
        //        Type = "buy",
        //        Quantity = quantity
        //    };

        //    _transactionRepo.Add(buy);
        //    _transactionRepo.SaveChanges();

        //    return new TransactionDTO() {
        //        Price = buy.Price,
        //        Quantity = buy.Quantity,
        //        DateCreated = buy.DateCreated,
        //        Ticker = buy.Stock.Ticker
        //    };
        //}

        //public TransactionDTO Sell(string ticker, int quantity, string username) {

        //    var stock = _stockRepo.FindStock(ticker).FirstOrDefault();
        //    var user = _userRepo.FindByName(username);

        //    var sell = new Transaction() {
        //        Stock = stock,
        //        User = user,
        //        Price = stock.Price,
        //        Type = "sell",
        //        Quantity = -quantity
        //    };

        //    _transactionRepo.Add(sell);
        //    _transactionRepo.SaveChanges();

        //    return new TransactionDTO() {
        //        Price = sell.Price,
        //        Quantity = sell.Quantity,
        //        DateCreated = sell.DateCreated,
        //        Ticker = sell.Stock.Ticker
        //    };
        //}

        //public bool CheckExists(string ticker) {
        //    return _stockRepo.CheckExists(ticker);
        //}
    }
}