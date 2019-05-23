(function () {
    'use strict';
    angular
    .module("CRMApp.Directives")
    .directive('ckEditor', function () {
      return {
          require: '?ngModel',
          link: function (scope, elm, attr, ngModel) {
              var ck = CKEDITOR.replace(elm[0]);
              if (!ngModel) return;
             
              //ck.on('instanceReady', function () {
              //    ck.setData(ngModel.$viewValue);
              //});
              //function updateModel() {
              //    scope.$apply(function () {
              //        ngModel.$setViewValue(ck.getData());
              //    });
              //}

              //var throttle = -1;
              //function updateModelThrottle() {
              //    if (throttle != -1) {
              //        //if (console && console.log) { console.log("Throttled!"); }
              //        clearTimeout(throttle);
              //    }
              //    throttle = setTimeout(updateModel, 100);
              //}
              //ck.on('change', updateModelThrottle);
              //ck.on('key', updateModelThrottle);
              //ck.on('dataReady', updateModelThrottle);
             
              ck.on('pasteState', function () {
                  scope.$apply(function () {
                      ngModel.$setViewValue(ck.getData());
                  });
              });
              ngModel.$render = function (value) {
                  ck.setData(ngModel.$viewValue);
              };
          }
      };
  })
})()