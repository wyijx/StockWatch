using StockWatch.Services;
using StockWatch.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace StockWatch.Presentation.Controllers {
    public class StocksController : ApiController {

        public StockService _stockService { get; set; }

        public StocksController(StockService stockService) {
            _stockService = stockService;
        }

        [HttpGet]
        [Authorize]
        [Route("api/stocks/search/{searchTerms}")]
        public IList<StockDTO> Search(string searchTerms) {
            return _stockService.Search(searchTerms);
        }

        [HttpGet]
        [Authorize]
        [Route("api/stocks/{ticker}")]
        public ExpandedStockDTO GetStock(string ticker) {
            return _stockService.GetStockWithTransactions(ticker, User.Identity.Name);
        }

        [HttpPost]
        [Authorize]
        [Route("api/trade/buy")]
        public IHttpActionResult Buy(TransactionDTO transaction) {
            if (ModelState.IsValid && _stockService.CheckExists(transaction.Ticker)) {
                return Ok(_stockService.Buy(transaction.Ticker, transaction.Quantity, User.Identity.Name));
            }
            return BadRequest();
        }

        [HttpPost]
        [Authorize]
        [Route("api/trade/{type}")]
        public IHttpActionResult Trade(TransactionDTO transaction, string type) {
            if(ModelState.IsValid && _stockService.CheckExists(transaction.Ticker)) {
                return Ok();
            }
            return BadRequest();
        }


        [HttpPost]
        [Authorize]
        [Route("api/trade/sell")]
        public IHttpActionResult Sell(TransactionDTO transaction) {
            if (ModelState.IsValid && _stockService.CheckExists(transaction.Ticker)) {
                return Ok(_stockService.Sell(transaction.Ticker, transaction.Quantity, User.Identity.Name));
            }
            return BadRequest();
        }
    }
}