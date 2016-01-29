var StockWatch;
(function (StockWatch) {
    var Controllers;
    (function (Controllers) {
        var StockDetailsController = (function () {
            function StockDetailsController($http, $routeParams) {
                var _this = this;
                this.$http = $http;
                this.$routeParams = $routeParams;
                $http.get("/api/stocks/" + $routeParams.ticker)
                    .then(function (response) {
                    _this.stock = response.data;
                });
            }
            StockDetailsController.prototype.trade = function (type, quantity) {
                var _this = this;
                if (quantity) {
                    this.$http.post("/api/trade/" + type, {
                        ticker: this.stock.ticker,
                        quantity: quantity,
                        type: type
                    })
                        .then(function (response) {
                        _this.stock.transactions.unshift(response.data);
                    })
                        .catch(function (response) {
                    });
                }
            };
            return StockDetailsController;
        })();
        Controllers.StockDetailsController = StockDetailsController;
    })(Controllers = StockWatch.Controllers || (StockWatch.Controllers = {}));
})(StockWatch || (StockWatch = {}));
