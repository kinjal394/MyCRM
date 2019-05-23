(function () {
    'use strict';
    angular
      .module("CRMApp.Directives")
        .directive("elaunchMobile", [
             function () {
                 return {
                     restrict: 'EA',

                     scope: {
                         ngModel: "=",
                         telcodeArray: "=",
                         placeholder: "@placeholder"
                     },
                     link: function (scope, elm) {
                         scope.outputcode = []
                         scope.num = scope.ngModel;
                         scope.number1 = '';
                         if (scope.telcodeArray.length > 0)
                             scope.code = _.filter(scope.telcodeArray, function (num) { return num.ticked == true; })[0].code + " ";
                         if (scope.num == undefined || scope.num == '')
                             scope.num = [];
                         scope.AddMobile = function () {
                             if (scope.outputcode.length == 0) {
                                 scope.errorCode = "Please Select Country Code."
                             }
                             else if (scope.outputcode.length > 0) {
                                 scope.errorCode = '';
                                 if (scope.number1.toString().trim() == '') {
                                     scope.errorNumber = "Please Select Mobile Number."
                                 }
                                 else if (scope.number1.toString().trim().charAt(0) == '0') {
                                     scope.errorNumber = "First Digit Zero Not Allow. Please Change Mobile No."
                                 }
                                 else if (!(/^\d+$/.test(scope.number1.toString().trim()))) {
                                     scope.errorNumber = "Only Number Allow."
                                 }
                                 else {
                                     var _isTag = 0;
                                     angular.forEach(scope.num, function (value) {
                                         var data = value.split(' ');
                                         if (data[0].toString().trim() == scope.code.toString().trim() && data[1].toString().trim() == scope.number1.toString().trim()) {
                                             scope.errorNumber = "Mobile number already exists."
                                             _isTag = 1;
                                             return forEach.break();
                                         }
                                     });
                                     if (_isTag == 0) {
                                         scope.num.push(scope.code.toString() + scope.number1.toString());
                                         scope.number1 = '';
                                         scope.errorNumber = '';
                                         scope.ngModel = scope.num
                                         _.each(scope.telcodeArray, function (value, key, list) {
                                             if (value.code == "+91") {
                                                 value.ticked = true;
                                                 scope.code = value.code + " ";
                                             }
                                             else {
                                                 value.ticked = false;
                                             }

                                         })
                                     }
                                 }
                             }
                         }
                         scope.RemoveMobile = function (index) {
                             scope.num.splice(index, 1)
                             scope.ngModel = scope.num
                         }
                         scope.isFocus = false;
                         scope.Test = function (d) {
                             scope.code = d.code + ' ';
                             scope.isFocus = true;
                         }
                         scope.fReset = function () {
                             scope.code = _.filter(scope.telcodeArray, function (num) { return num.ticked == true; })[0].code + " ";
                         }
                         scope.$watch('telcodeArray', function (newValue, oldValue) {
                             if (newValue != oldValue) {
                                 scope.code = _.filter(scope.telcodeArray, function (num) { return num.ticked == true; })[0].code + " ";
                             }
                         });
                         scope.$watch('ngModel', function (newValue, oldValue) {
                             if (newValue != oldValue) {
                                 scope.num = newValue;
                             }
                         });
                     },

                     template: '<div class="form-group">' +
                                    '<div class="input-group">' +
                                        '<div class="input-group-btn">' +
                                            '<div isteven-multi-select input-model="telcodeArray" output-model="outputcode" button-label="icon"item-label="icon code name" tick-property="ticked" output-properties="name ticked code" on-item-click="Test(data)"  on-reset="fReset()" selection-mode="single">' +
                                        '</div>' +
                                    '</div>' +
                                    '<div class="input-group-addon"><lable for="code" ng-model="code">{{code}}</lable></div>' +
                                    '<input type="text" ng-model="number1" ng-focus="true" placeholder="{{placeholder}}" oninput="this.value = this.value.replace(/[^0-9.]/g, \'\');" maxlength="100" class="form-control" />' +
                                    '<div class="input-group-btn"><input type="button" value="Add" ng-click="AddMobile()" class="btn btn-primary">' +
                                    '</div>' +
                               '</div>' +
                               '<font style="color:red;">{{errorCode}}{{errorNumber}}</font>' +
                               '<div ng-repeat="m in num" style="padding-bottom:2px;">' +
                               '{{m}}&nbsp;<input type="button" ng-click="RemoveMobile($index)" value="Remove" class="btn btn-danger btn-xs">' +
                               '</div>',
                 }
             }])
})()

