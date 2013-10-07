angular.module('chui').controller("PackagesController", ['$scope', 'PackagesService', 'ClientMessenger',
    function($scope, packagesService, clientMessenger) {

        var updatePackages = function() {
            packagesService.updatePackages();
        }

        var getPackages = function() {
            packagesService.getPackages().success(function(result) {
                $scope.packages = result;
            });
        }

        $scope.updatePackages = updatePackages;
        clientMessenger.registerCallback("Packages", getPackages);
        getPackages();

        $scope.installPackage = function(package) {
            console.log("install package " + package.Name);
            packagesService.installPackage(package).success(function() {
                console.log("package installed " + package.Name);
            });
        }
}]);