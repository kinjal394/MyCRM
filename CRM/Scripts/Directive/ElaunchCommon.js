(function () {
    'use strict';
    angular
      .module("CRMApp.Directives")
        .directive("firstAlphabet", [
             function () {
                 return {
                     restrict: 'A',
                     scope: {
                         ngModel: "=",
                         place: "@place"
                     },
                     link: function (scope, elm) {
                         scope.$watch('ngModel', function (Value) {
                             if (Value) {
                                 var first = Value.substring(0, 1);
                                 if (!isNaN(first) && angular.isNumber(first) == false) {
                                     scope.ngModel = '';
                                 } else {
                                     scope.ngModel = Value;
                                 }
                             }
                         });
                     },
                     template: '<input type="text" ng-model="ngModel" placeholder="{{place}}" class="form-control"  />',
                 }
             }])
})();

(function () {
    'use strict';
    angular
      .module("CRMApp.Directives")
        .directive("elaunchChat", [
             function () {
                 return {
                     restrict: 'EA',
                     scope: {
                         ngModel: "=",
                         chatcodeArray: "="
                     },
                     link: function (scope, elm) {
                         scope.outputcode = []
                         scope.num = scope.ngModel;
                         console.log(scope.num);
                         scope.number1 = '';
                         if (scope.num == undefined || scope.num == '')
                             scope.num = [];
                         scope.AddChat = function () {
                             if (scope.outputcode.length == 0) {
                                 scope.errorCode = "Please Select Chat Source."
                             }
                             else if (scope.outputcode.length > 0) {
                                 scope.errorCode = '';
                                 if (scope.number1.toString().trim() == '') {
                                     scope.errorNumber = "Please Select Chat Value."
                                 }
                                 else {
                                     var _isTag = 0;
                                     angular.forEach(scope.num, function (value) {
                                         var data = value.split(' ');
                                         if (data[0].toString().trim() == scope.code.toString().trim() && data[1].toString().trim() == scope.number1.toString().trim()) {
                                             scope.errorNumber = "Chat Details already exists."
                                             _isTag = 1;
                                             return forEach.break();
                                         }
                                     });
                                     if (_isTag == 0) {
                                         scope.num.push(scope.code + scope.number1.toString());
                                         scope.number1 = '';
                                         scope.errorNumber = '';
                                         scope.code = '';
                                         scope.ngModel = scope.num
                                         _.each(scope.chatcodeArray, function (value, key, list) {
                                             value.ticked = false;
                                         });
                                     }
                                 }
                             }
                         }
                         scope.RemoveChat = function (index) {
                             scope.num.splice(index, 1)
                             scope.ngModel = scope.num
                         }
                         scope.isFocus = false;
                         scope.Test = function (d) {
                             scope.code = d.icon + ' : ';
                             scope.isFocus = true;
                         }
                         scope.fReset = function () {
                             scope.code = _.filter(scope.chatcodeArray, function (num) { return num.ticked == true; })[0].code + " ";
                         }
                         scope.$watch('ngModel', function (newValue, oldValue) {
                             if (newValue != oldValue) {
                                 scope.num = newValue;
                             }
                         });
                     },
                     template: '<div class="form-group">' +
                                    '<div class="input-group" >' +
                                        '<div class="input-group-btn" >' +
                                            '<div isteven-multi-select  class="isMultiwidth" input-model="chatcodeArray" output-model="outputcode" button-label="icon"item-label="icon code name" tick-property="ticked" output-properties="name ticked code" on-item-click="Test(data)"  on-reset="fReset()" selection-mode="single" >' +
                                        '</div>' +
                                    '</div>' +
                                    '<input type="text" ng-model="number1" ng-focus="true" placeholder="Enter Chat value" class="form-control" />' +
                                    '<div class="input-group-btn"><input type="button" value="Add" ng-click="AddChat()" class="btn btn-primary">' +
                                    '</div>' +
                               '</div>' +
                               '<font style="color:red;">{{errorCode}}{{errorNumber}}</font>' +
                               '<div ng-repeat="m in num" style="padding-bottom:2px;">' +
                               '{{m}}&nbsp;<input type="button" ng-click="RemoveChat($index)" value="Remove" class="btn btn-danger btn-xs">' +
                               '</div>',
                 }
             }])
})();

(function () {
    'use strict';
    angular
      .module("CRMApp.Directives")
        .directive("elaunchWebsite", [
             function () {
                 return {
                     restrict: 'EA',
                     scope: {
                         ngModel: "=",
                     },
                     link: function (scope, elm) {
                         scope.num = scope.ngModel;
                         scope.WebsiteAddress = ''
                         if (scope.num == undefined || scope.num == '')
                             scope.num = [];
                         scope.AddWebsite = function () {
                             if (scope.WebsiteAddress == undefined) {
                                 scope.errorWebsite = "Please Enter Valid Website."
                             }
                             if (scope.WebsiteAddress.trim() == '') {
                                 scope.errorWebsite = "Please Enter Website."
                             }
                             else {
                                 scope.errorWebsite = ''
                                 var _isTag = 0;
                                 angular.forEach(scope.num, function (value) {
                                     if (value.trim() == scope.WebsiteAddress.toString().trim()) {
                                         scope.errorWebsite = "Website address already exists."
                                         _isTag = 1;
                                         return forEach.break();
                                     }
                                 });
                                 if (_isTag == 0) {
                                     scope.num.push(scope.WebsiteAddress.toString());
                                     scope.WebsiteAddress = '';
                                     scope.ngModel = scope.num
                                 }
                             }
                         }
                         scope.RemoveWebsite = function (index) {
                             scope.num.splice(index, 1)
                             scope.ngModel = scope.num
                         }
                         scope.$watch('ngModel', function (newValue, oldValue) {
                             if (newValue != oldValue) {
                                 scope.num = newValue;
                             }
                         });
                     },
                     template: '<div class="form-group"><div class="input-group"><input ng-model="WebsiteAddress" pattern="www?.[A-Za-z0-9].+" placeholder="Enter Website" class="form-control"/><div class="input-group-btn"><input type="button" value="Add" ng-click="AddWebsite()" class="btn btn-primary"></div></div></div><font style="color:red;">{{errorWebsite}}</font><div ng-repeat="m in num" style="padding-bottom:2px;">{{m}}&nbsp;<input type="button" ng-click="RemoveWebsite($index)" value="Remove"  class="btn btn-danger btn-xs"></div>',
                 }
             }])
})();

(function () {
    'use strict';
    angular
      .module("CRMApp.Directives")
        .directive("elaunchTextarea", [
             function () {
                 return {
                     restrict: 'EA',
                     scope: {
                         ngModel: "=",
                         place: "@place"
                     },
                     link: function (scope, elm) {
                         scope.num = scope.ngModel;
                         scope.Textarea = ''
                         if (scope.num == undefined || scope.num == '')
                             scope.num = [];
                         scope.AddTextarea = function () {
                             if (scope.Textarea.trim() == '') {
                                 scope.errorTextarea = scope.place
                             } else {
                                 var text = scope.Textarea.toString().replace('|', ' ');
                                 scope.num.push(text);
                                 scope.Textarea = '';
                                 scope.errorTextarea = '';
                                 scope.ngModel = scope.num;
                             }

                         }
                         scope.RemoveTextarea = function (index) {
                             scope.num.splice(index, 1)
                             scope.ngModel = scope.num
                         }
                         scope.$watch('ngModel', function (newValue, oldValue) {
                             if (newValue != oldValue) {
                                 scope.num = newValue;
                             }
                         });
                     },
                     template: '<div class="form-group"><div class="input-group"><textarea rows="2" cols="60" ng-model="Textarea" class="form-control"></textarea><div class="input-group-addon"><input type="button" value="Add" ng-click="AddTextarea()" class="btn btn-primary"></div></div></div><font style="color:red;">{{errorTextarea}}</font><div ng-repeat="m in num" style="padding-bottom:2px;">{{m}}&nbsp;<input type="button" ng-click="RemoveTextarea($index)" value="Remove"  class="btn btn-danger btn-xs"></div>',
                 }
             }])
})();

//(function () {
//    'use strict';
//    angular
//      .module("CRMApp.Directives")
//        .directive("camelAlphabet", [
//             function () {
//                 return {
//                     restrict: 'A',
//                     scope: {
//                         ngModel: "=",
//                         place: "@place"
//                     },
//                     link: function (scope, elm) {
//                         scope.$watch('ngModel', function (Value) {
//                             if (Value) {
//                                 var camelCase = '';
//                                 var camelCasearry = Value.trim().split(' ');
//                                 for (var i = 0; i < camelCasearry.length; i++) {
//                                     camelCasearry[i] = camelCasearry[i].charAt(0).toUpperCase() + camelCasearry[i].substr(1).toLowerCase();
//                                 }
//                                 scope.ngModel = camelCasearry.join(' ');;
//                             }

//                         });
//                     },
//                     template: '<input type="text" ng-model="ngModel" placeholder="{{place}}" class="form-control"  />',
//                 }
//             }])
//})();


(function () {
    'use strict';
    angular
      .module("CRMApp.Directives")
        .directive("camelText", [
             function () {
                 return {
                     restrict: "A",
                     require: "?ngModel",
                     link: function (scope, element, attrs, ngModel) {
                         ngModel.$parsers.push(function (input) {
                             var test = "";
                             var words = input.split(' ');
                             for (var i = 0, len = words.length; i < len; i++) {
                                 words[i] = words[i].charAt(0).toUpperCase() + words[i].slice(1).toLowerCase();
                                 test= words.join(' ');
                               
                             }
                             ngModel.$setViewValue(test);
                             ngModel.$render();
                             return test;
                         });
                     }
                 };
             }])
})()




//(function () {
//    angular
//        .module('CRMApp.Filters', [])
//        .filter("capitalize", function () {
//            return function (input, scope) {
//                console.log("Sample");
//                if (input != null)
//                    input = input.toLowerCase();
//                return input.substring(0, 1).toUpperCase() + input.substring(1);
//            };
//        });
//})()

