angular.module('chooie').controller("PackagesController", ['$scope', 'PackagesService', 'ClientMessenger',
    function($scope, packagesService, clientMessenger) {

        var getPackages = function() {
            packagesService.getPackages().success(function(result) {
                $scope.packages = result;
            });
        };

        clientMessenger.registerCallback("Packages", getPackages);
        getPackages();

        $scope.installPackage = function(package) {
            packagesService.installPackage(package)
        };

        $scope.uninstallPackage = function(package) {
            packagesService.uninstallPackage(package)
        };
}]);