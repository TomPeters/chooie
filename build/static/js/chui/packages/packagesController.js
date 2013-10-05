angular.module('chui').controller("PackagesController", ['$scope', 'PackagesService',
    function($scope, packagesService) {
        packagesService.getPackages().success(function(result) {
            $scope.packages = result;
        });

        $scope.installPackage = function(package) {
            console.log("install package " + package.Name);
            packagesService.installPackage(package).success(function() {
                console.log("package installed " + package.Name);
            });
        }
}]);