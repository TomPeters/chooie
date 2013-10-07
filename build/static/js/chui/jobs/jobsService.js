angular.module('chui').factory("JobsService", ['$http', function($http) {
    return {
        getJobs: function() {
            return $http.get('/jobs');
        }
    }
}]);