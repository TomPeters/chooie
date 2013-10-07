angular.module('chui').factory("PackagesService", ['$http', function($http) {
    return {
        getPackages: function() {
            return $http.get('/packages');
        },

        updatePackages: function() {
            return $http.post('/packages/update');
        },

        installPackage: function(package) {
            return $http.post('/packages/install', package);
        }
    }
}]);