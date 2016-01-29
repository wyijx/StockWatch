var StockWatch;
(function (StockWatch) {
    angular.module('StockWatch', ['ngRoute']);
    angular.module('StockWatch').factory('authInterceptor', function ($q, $window, $location) {
        return {
            request: function (config) {
                config.headers = config.headers || {};
                var token = $window.localStorage.getItem('token');
                if (token) {
                    config.headers.Authorization = "Bearer " + token;
                }
                return config;
            },
            responseError: function (response) {
                if (response.status === 401) {
                    $location.path('/login');
                }
                return $q.reject(response);
            }
        };
    });
    angular.module('StockWatch')
        .config(function ($routeProvider, $httpProvider) {
        $httpProvider.interceptors.push('authInterceptor');
        $routeProvider
            .when('/', {
            templateUrl: '/Presentation/ngApp/views/research.html',
            controller: StockWatch.Controllers.ResearchController,
            controllerAs: 'controller'
        })
            .when('/login', {
            templateUrl: '/Presentation/ngApp/views/login.html',
            controller: StockWatch.Controllers.AuthController,
            controllerAs: 'controller'
        })
            .when('/register', {
            templateUrl: '/Presentation/ngApp/views/register.html',
            controller: StockWatch.Controllers.AuthController,
            controllerAs: 'controller'
        })
            .when('/ticker/:ticker', {
            templateUrl: '/Presentation/ngApp/views/stockDetails.html',
            controller: StockWatch.Controllers.StockDetailsController,
            controllerAs: 'controller'
        });
    });
})(StockWatch || (StockWatch = {}));
