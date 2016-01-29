namespace StockWatch.Controllers {
    export class StockDetailsController {

        public stock;

        constructor(private $http: ng.IHttpService, private $routeParams) {
            $http.get(`/api/stocks/${$routeParams.ticker}`)
                .then((response) => {
                    this.stock = response.data;
                });
        }

        public trade(type: string, quantity: number): void {
            if (quantity) {
                this.$http.post(`/api/trade/${type}`, {
                    ticker: this.stock.ticker,
                    quantity: quantity,
                    type: type
                })
                    .then((response) => {
                        this.stock.transactions.unshift(response.data);
                    })
                    .catch((response) => {

                    });
            }
        }

        //public sell(quantity: number): void {
        //    if (quantity) {
        //        this.$http.post(`/api/trade/sell`, {
        //            ticker: this.stock.ticker,
        //            quantity: quantity
        //        })
        //            .then((response) => {
        //                this.stock.transactions.unshift(response.data);
        //            })
        //            .catch((response) => {

        //            });;
        //    }
        //}
    }
}