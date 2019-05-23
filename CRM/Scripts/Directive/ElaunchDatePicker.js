(function () {
    'use strict';
    angular
      .module("CRMApp.Directives")
        .directive("elaunchDatepicker", [
             function () {
                 return {
                     restrict: 'EA',
                     scope: {
                         ngModel: "=",
                         dateOptions: "=",
                         opened: "=",
                         weekOff: "=",
                         isRequired: "=",
                         dateFormat: "=",
                         isDisabled: "=",
                         dateModes: "=" // Default = 0,Birthday = 1
                     },
                     link: function ($scope, element, attrs) {
                         //$scope.dtName = $scope.datename
                         debugger
                         $scope.datename = attrs.name;
                         if (!attrs.dateFormat) {
                             $scope.format = 'dd-MM-yyyy';
                         }
                         else {
                             $scope.format = $scope.dateFormat;
                         }
                         if (!attrs.dateOptions) {
                             $scope.inlineOptions = {
                                 customClass: getDayClass,
                                 minDate: new Date(),
                                 showWeeks: 'false'
                             };
                             $scope.dateOptions = {
                                 dateDisabled: disabled,
                                 formatYear: 'yy',
                                 maxDate: new Date(2030, 5, 22),
                                 minDate: new Date(1999, 9, 16),
                                 startingDay: 1,
                                 showWeeks: 'false'
                             };
                         } else {
                             //if ($scope.dateModes == '0') {
                                 var dateOption = {
                                     formatYear: 'yy',
                                     maxDate: new Date(2030, 11, 31),
                                     minDate: new Date(1999, 0, 1),
                                     startingDay: 1,
                                     showWeeks: 'false'
                                 };
                                 $scope.dateOptions = dateOption;
                             //} else if ($scope.dateModes == '1') {
                             //    var dateOption = {
                             //        formatYear: 'yy',
                             //        maxDate: new Date(),
                             //        minDate: new Date(1950, 0, 1),
                             //        startingDay: 1,
                             //        showWeeks: 'false'
                             //    };
                             //    $scope.dateOptions = dateOption;
                             //}
                         }
                         $scope.dateOptions["showWeeks"] = false;
                         $scope.today = function () {
                             $scope.ngModel = new Date();
                         };
                         // $scope.today();

                         $scope.clear = function () {
                             $scope.ngModel = null;
                         };


                         // Disable weekend selection
                         function disabled(data) {
                             var date = data.date,
                               mode = data.mode;
                             return mode === 'day' && (date.getDay() === $scope.weekOff);
                         }


                         $scope.toggleMin = function () {
                             $scope.dateOptions.minDate = $scope.dateOptions.minDate ? $scope.dateOptions.minDate : new Date(2001, 1, 1);
                             $scope.dateOptions.maxDate = $scope.dateOptions.maxDate ? $scope.dateOptions.maxDate : new Date(2020, 1, 1);
                         };

                         $scope.toggleMin();

                         $scope.open1 = function () {
                             $scope.popup1.opened = true;
                         };

                         $scope.open2 = function () {
                             $scope.popup2.opened = true;
                         };

                         $scope.setDate = function (year, month, day) {
                             $scope.ngModel = new Date(year, month, day);
                         };

                         $scope.formats = ['dd-MM-yyyy', 'yyyy/MM/dd', 'dd.MM.yyyy', 'shortDate'];
                         // $scope.format = $scope.dateFormat;
                         $scope.altInputFormats = ['M!/d!/yyyy'];

                         $scope.popup1 = {
                             opened: false
                         };

                         $scope.popup2 = {
                             opened: false
                         };

                         var tomorrow = new Date();
                         tomorrow.setDate(tomorrow.getDate() + 1);
                         var afterTomorrow = new Date();
                         afterTomorrow.setDate(tomorrow.getDate() + 1);
                         $scope.events = [
                           {
                               date: tomorrow,
                               status: 'full'
                           },
                           {
                               date: afterTomorrow,
                               status: 'partially'
                           }
                         ];

                         function getDayClass(data) {
                             var date = data.date,
                               mode = data.mode;
                             if (mode === 'day') {
                                 var dayToCheck = new Date(date).setHours(0, 0, 0, 0);

                                 for (var i = 0; i < $scope.events.length; i++) {
                                     var currentDay = new Date($scope.events[i].date).setHours(0, 0, 0, 0);

                                     if (dayToCheck === currentDay) {
                                         return $scope.events[i].status;
                                     }
                                 }
                             }

                             return '';
                         }
                     },
                     template: '<span class="input-group"><input type="text" name="{{datename}}" class="form-control" ng-disabled="isDisabled" ng-focus="popup1.opened= true"  uib-datepicker-popup="{{format}}" ng-model="ngModel" is-open="popup1.opened" datepicker-options="dateOptions" close-text="Close" alt-input-formats="altInputFormats" /><span class="input-group-btn"><button type="button" class="btn btn-default" ng-click="open1()"><i class="fa fa-calendar"></i></button></span></span>'
                 }
             }])
})()