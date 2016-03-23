(function () {
    angular
	  .module('ddshopApp')
      .controller('homeCtrl', homeCtrl);

    homeCtrl.$inject = ['$scope', '$location'];
    function homeCtrl($scope, $location) {
        var vm = this;

        vm.currentPath = $location.path();

        vm.message = "Hello";

    }

})();