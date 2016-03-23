(function () {
    angular
	  .module('ddshopApp')
      .controller('ordersCtrl', ordersCtrl);

    ordersCtrl.$inject = ['$scope', '$location'];
    function ordersCtrl($scope, $location) {
        var vm = this;

        vm.currentPath = $location.path();

        vm.message = "Hello";

    }

})();