angular.module('chui').factory("JobStateDescriptionConverter", [function() {  // TODO COnvert to factory
    return {
        convertJobStateToJobStateDescription: function(jobState) {
            switch(jobState) {
                case 0:
                    return "Pending";
                case 1:
                    return "Running";
                case 2:
                    return "Finished";
                case 3:
                    return "Error";
                default:
                    throw "Invalid job state";
            }
        }
    };
}]);