angular.module('chooie').factory("LogService", ['$http', function($http) {
    return {
        getLog: function() {
            return $http.get('/log');
        }
    }
}]);