angular.module('chooie').controller("SidepaneController", ['$scope',
    function($scope) {
        var currentTab = 'jobs';
        var isTabBeingDisplayedFunctionGenerator = function(tabName) {
            return function() {
                return currentTab === tabName;
            }
        }
        $scope.jobsTabsIsDisplayed = isTabBeingDisplayedFunctionGenerator('jobs');
        $scope.logTabsIsDisplayed = isTabBeingDisplayedFunctionGenerator('log');

        $scope.switchToTab = function(tabName) {
            currentTab = tabName;
        }
    }]);