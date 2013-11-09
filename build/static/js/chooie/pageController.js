angular.module('chooie').controller("PageController", ['$scope', 'PackagesService',
    function($scope, packagesService) {

        $scope.packageFilter = {};

        var updatePackages = function() {
            packagesService.updatePackages();
        };
        $scope.updatePackages = updatePackages;
    }]);