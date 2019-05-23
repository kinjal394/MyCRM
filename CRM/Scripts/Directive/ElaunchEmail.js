(function () {
    'use strict';
    angular
      .module("CRMApp.Directives")
        .directive("elaunchEmail", [
             function () {
                 return {
                     restrict: 'EA',
                     scope: {
                         ngModel: "=",
                     },
                     link: function (scope, elm) {
                         scope.num = scope.ngModel;
                         scope.emailAddress = ''
                         if (scope.num == undefined || scope.num == '')
                             scope.num = [];
                         scope.AddEmail = function () {
                             if (scope.emailAddress == undefined) {
                                 scope.errorEmail = "Please Enter Valid Email"
                             }
                             if (scope.emailAddress.trim() == '') {
                                 scope.errorEmail = "Please Enter Email"
                             }
                             else {
                                 scope.errorEmail = ''
                                 var _isTag = 0;
                                 angular.forEach(scope.num, function (value) {
                                     if (value.trim() == scope.emailAddress.toString().trim()) {
                                         scope.errorEmail = "Email address already exists."
                                         _isTag = 1;
                                         return forEach.break();
                                     }
                                 });
                                 if (_isTag == 0) {
                                     scope.num.push(scope.emailAddress.toString());
                                     scope.emailAddress = '';
                                     scope.ngModel = scope.num
                                 }
                             }
                         }
                         scope.RemoveEmail = function (index) {
                             scope.num.splice(index, 1)
                             scope.ngModel = scope.num
                         }
                         scope.$watch('ngModel', function (newValue, oldValue) {
                             if (newValue != oldValue) {
                                 scope.num = newValue;
                             }
                         });
                     },
                     template: '<div class="form-group"><div class="input-group"><input type="email" ng-model="emailAddress" placeholder="Enter Email" class="form-control"/><div class="input-group-btn"><input type="button" value="Add" ng-click="AddEmail()" class="btn btn-primary"></div></div></div><font style="color:red;">{{errorEmail}}</font><div ng-repeat="m in num" style="padding-bottom:2px;">{{m}}&nbsp;<input type="button" ng-click="RemoveEmail($index)" value="Remove"  class="btn btn-danger btn-xs"></div>',
                 }
             }])
})()