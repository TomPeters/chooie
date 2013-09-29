angular.module('chui').controller("PackagesController", ['$scope', 'PackagesService',
    function($scope, packagesService) {
        packagesService.getPackages().success(function(result) {
            $scope.packages = result;
        });
}]);