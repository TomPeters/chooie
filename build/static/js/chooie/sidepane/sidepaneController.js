angular.module('chooie').controller("SidepaneController", ['$scope',
    function($scope) {
        $scope.jobsTabsIsDisplayed = true;
        $scope.logTabsIsDisplayed = true;

        var hideTabs = function() {
            $scope.jobsTabsIsDisplayed = false;
            $scope.logTabsIsDisplayed = false;
        }
        $scope.switchToTab = function(tabName) {
            hideTabs();
            if(tabName === 'jobs') {
                $scope.jobsTabsIsDisplayed = true;
            } else if (tabName === 'log') {
                $scope.logTabsIsDisplayed = true;
            }
        }
    }]);