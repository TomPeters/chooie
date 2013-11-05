angular.module('chooie').factory("JobsService", ['$http', function($http) {
    return {
        getJobs: function() {
            return $http.get('/jobs');
        }
    }
}]);