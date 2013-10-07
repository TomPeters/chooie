angular.module('chui').controller("JobsController", ['$scope', 'JobsService', 'ClientMessenger',
    function($scope, jobsService, clientMessenger) {
        var getJobs = function() {
            jobsService.getJobs().success(function(jobs) {
                $scope.jobs = jobs;
            });
        }
        getJobs();
        clientMessenger.registerCallback("JobList", getJobs);
    }]);