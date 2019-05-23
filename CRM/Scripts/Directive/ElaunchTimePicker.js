(function () {
    'use strict';
    angular
      .module("CRMApp.Directives")
        .directive("elaunchTimepicker", [
             function () {
                 return {
                     restrict: 'EA',
                     scope: {
                         ngModel: "=",
                         hstep: "=",
                         mstep: "=",
                         isMeridian:"=",
                         isRequired: "=",
                         update:"&"
                     },
                     link: function ($scope, element, attrs) {
                        // $scope.mytime = new Date();
                         //$scope.hstep = 1;
                         //$scope.mstep = 1;

                         $scope.options = {
                             hstep: [1, 2, 3],
                             mstep: [1, 5, 10, 15, 25, 30]
                         };

                         $scope.ismeridian = true;
                         //$scope.toggleMode = function () {
                         //    $scope.ismeridian = !$scope.ismeridian;
                         //};

                         $scope.update = function () {
                             var d = new Date();
                             d.setHours(14);
                             d.setMinutes(0);
                             $scope.ngModel = d;
                         };

                         //$scope.changed = function () {
                         //    $log.log('Time changed to: ' + $scope.ngModel);
                         //};

                         //$scope.clear = function () {
                         //    $scope.ngModel = null;
                         //};
                     },
                     template: '<div uib-timepicker ng-model="ngModel" ng-change="changed()" hour-step="hstep" minute-step="mstep" show-meridian="isMeridian" ng-required="isRequired"></div>'
                     //template: '<span class="input-group"><input type="text" class="form-control" uib-datepicker-popup="{{format}}" ng-model="ngModel" is-open="popup1.opened" datepicker-options="dateOptions" ng-required="isRequired" close-text="Close" alt-input-formats="altInputFormats" /><span class="input-group-btn"><button type="button" class="btn btn-default" ng-click="open1()"><i class="fa fa-calendar"></i></button></span></span>'
                 }
             }])
})()