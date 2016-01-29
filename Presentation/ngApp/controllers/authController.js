var StockWatch;
(function (StockWatch) {
    var Controllers;
    (function (Controllers) {
        var AuthController = (function () {
            function AuthController($http, $window, $location) {
                this.$http = $http;
                this.$window = $window;
                this.$location = $location;
            }
            AuthController.prototype.register = function (user) {
                this.$http.post('/api/account/register', user)
                    .then(function (response) {
                    console.log('Registered a new user!');
                })
                    .catch(function (response) {
                    console.log(response);
                });
            };
            AuthController.prototype.login = function (username, password) {
                var _this = this;
                var data = "grant_type=password&username=" + username + "&password=" + password;
                this.$http.post('/token', data, {
                    headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
                })
                    .then(function (response) {
                    _this.$window.localStorage.setItem('token', response.data['access_token']);
                    _this.$location.path('/');
                })
                    .catch(function (response) {
                    console.log(response);
                });
            };
            AuthController.prototype.logout = function () {
                this.$window.localStorage.removeItem('token');
            };
            return AuthController;
        })();
        Controllers.AuthController = AuthController;
        angular.module('StockWatch').controller('authController', AuthController);
    })(Controllers = StockWatch.Controllers || (StockWatch.Controllers = {}));
})(StockWatch || (StockWatch = {}));
