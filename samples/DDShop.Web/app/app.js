(function () {
    angular
	  .module('ddshopApp',
	  	[
	  		'ngRoute'
	  	]);

    function config($routeProvider, $locationProvider) {
        $routeProvider
	      .when('/home', {
	        templateUrl: '/app/home/home.view.html',
	        controller: 'homeCtrl',
	        controllerAs: 'vm'
	      })
          .when('/orders', {
            templateUrl: '/app/orders/orders.view.html',
            controller: 'ordersCtrl',
            controllerAs: 'vm'
          })
	      .otherwise({
	          redirectTo: '/'
	      });

        $locationProvider.html5Mode({
            enabled: true,
            requireBase: false
        });
    }

    angular
        .module('ddshopApp')
        .config(['$routeProvider', '$locationProvider', config]);

})();