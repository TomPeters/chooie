angular.module('chui').controller("PackagesController", ['$scope', 'PackagesService', 'ClientMessenger',
    function($scope, packagesService, clientMessenger) {

        $scope.updatingStatus = false;

        var updatePackages = function() {
            $scope.updatingStatus = true;
            packagesService.updatePackages().success(function(dispatchId) {
                clientMessenger.registerCallback(JSON.parse(dispatchId), getPackages);
            });
        }

        var getPackages = function() {
            packagesService.getPackages().success(function(result) {
                $scope.packages = result;
                if(result.length === 0) {
                    updatePackages();
                } else {
                    $scope.updatingStatus = false;
                }
            });
        }
        getPackages();


        $scope.installPackage = function(package) {
            console.log("install package " + package.Name);
            packagesService.installPackage(package).success(function() {
                console.log("package installed " + package.Name);
            });
        }
}]);