angular.module('chui').factory("PackagesService", ['$http', function($http) {
    return {
        getPackages: function() {
            return $http.get('/packages');
        },

        installPackage: function(package) {
            return $http.post('/packages/install', package);
        }
    }
}]);