angular.module('chooie').factory("PackagesService", ['$http', function($http) {
    return {
        getPackages: function() {
            return $http.get('/packages');
        },

        updatePackages: function() {
            return $http.post('/packages/update');
        },

        installPackage: function(package) {
            return $http.post('/packages/install', package);
        },

        uninstallPackage: function(package) {
            return $http.post('/packages/uninstall', package);
        }
    }
}]);