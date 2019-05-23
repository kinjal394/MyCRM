(function () {
    'use strict';
    angular
      .module("CRMApp.Directives")
        .directive("elaunchtext", [
             function () {
                 return {
                     restrict: 'EA',
                     scope: {
                         ngModel: "=",
                         isValidate: "=",
                         place: "@place"
                     },
                     link: function (scope, elm) {
                         scope.num = scope.ngModel;
                         if (scope.isValidate === undefined) { scope.isValidate = false; }
                         scope.Isval = scope.isValidate;
                         scope.emailAddress = ''
                         if (scope.num == undefined || scope.num == '')
                             scope.num = [];
                         scope.AddEmail = function () {
                             if (scope.emailAddress.toString().trim() == '') {
                                 scope.errorEmail = "Please " + scope.place;
                             }
                             else {
                                 scope.errorEmail = '';
                                 var _isDuplicate = 0;
                                 var val = scope.emailAddress;//(isNaN(parseInt(scope.emailAddress))) ? scope.emailAddress : parseInt(scope.emailAddress);
                                 if (scope.Isval == true) {
                                     if (/^\d+$/.test(val)) {
                                         //scope.num.push(scope.emailAddress.toString());
                                         _isDuplicate = 1;
                                     } else { scope.errorEmail = 'Only Number Allow.' }
                                 } else {
                                     //scope.num.push(scope.emailAddress.toString());
                                     if (/(http(s)?:\\)?([\w-]+\.)+[\w-]+[.com|.in|.org]+(\[\?%&=]*)?/.test(val)) {
                                         //scope.num.push(scope.emailAddress.toString());
                                         _isDuplicate = 1;
                                     } else { scope.errorEmail = 'Please Enter Valid URL.' }
                                 }
                                 if (_isDuplicate == 1) {
                                     var _isTag = 0;
                                     angular.forEach(scope.num, function (value) {
                                         if (value.trim() == scope.emailAddress.toString().trim()) {
                                             scope.errorEmail = "Already exists."
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
                                 //scope.emailAddress = '';
                                 //scope.ngModel = scope.num
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
                     template: '<div class="form-group"><div class="input-group"><input type="text" ng-model="emailAddress" placeholder="{{place}}" class="form-control"/><div class="input-group-btn"><input type="button" value="Add" ng-click="AddEmail()" class="btn btn-primary"></div></div></div><font style="color:red;">{{errorEmail}}</font><div ng-repeat="m in num" style="padding-bottom:2px;">{{m}}&nbsp;<input type="button" ng-click="RemoveEmail($index)" value="Remove" class="btn btn-danger btn-xs"></div>',
                 }
             }])
})();