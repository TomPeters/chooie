angular.module('chui').controller("JobsController", ['$scope', 'JobsService', 'ClientMessenger', 'JobStateDescriptionConverter',
    function($scope, jobsService, clientMessenger, jobStateDescriptionConverter) {
        var getJobs = function() {
            jobsService.getJobs().success(function(jobs) {
                $scope.jobs = jobs.map(function(job) {
                    job.State = jobStateDescriptionConverter.convertJobStateToJobStateDescription(job.State);
                    return job;
                });
            });
        }
        getJobs();
        clientMessenger.registerCallback("JobList", getJobs);
    }]);