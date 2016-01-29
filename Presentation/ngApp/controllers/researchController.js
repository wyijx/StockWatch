var StockWatch;
(function (StockWatch) {
    var Controllers;
    (function (Controllers) {
        var ResearchController = (function () {
            function ResearchController($http) {
                this.$http = $http;
            }
            ResearchController.prototype.updateStocks = function (searchTerms) {
                var _this = this;
                this.stocks = [];
                if (searchTerms) {
                    this.$http.get("/api/stocks/search/" + searchTerms)
                        .then(function (response) {
                        _this.stocks = response.data;
                    });
                }
            };
            return ResearchController;
        })();
        Controllers.ResearchController = ResearchController;
    })(Controllers = StockWatch.Controllers || (StockWatch.Controllers = {}));
})(StockWatch || (StockWatch = {}));
