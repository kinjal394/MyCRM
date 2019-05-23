(function () {
    'use strict';
    angular
      .module("CRMApp.Directives")
        .directive("elaunchflags", [
            "$rootScope", "$http", "$compile", "$interpolate",
                  function ($rootScope, $http, $compile, $interpolate) {
                      return {
                          restrict: "E",
                          scope: {
                              ngModel: '=',
                              onclick: '&onclick'
                          },
                          link: function (scope, element, attrs) {
                              $http({
                                  method: 'post',
                                  url: '/Home/getCountryData',
                                  contentType: 'application/json; charset=utf-8',
                                  data: {},
                                  responseType: 'json',
                              }).then(function (result) {
                                  // //console.log(result);
                                  scope.MobCodesList = result.data;
                                  var tempText = '<div class="dropdown">'
                                       + '<button style="border-top-left-radius: 4px;border-bottom-left-radius: 4px;border-left: #ccc 1px solid;" class="btn btn-default dropdown-toggle" type="button" id="dropdownMenu1" data-toggle="dropdown" aria-haspopup="true" aria-expanded="true">'
                                          + '{{ngModel.Display}}'
                                           + '<span class="caret">'
                                           + '</span>'
                                       + '</button>'
                                       + '<ul class="dropdown-menu" aria-labelledby="dropdownMenu1" style="height: 242px;overflow-y: auto;">'
                                           + '<li ng-repeat="a in MobCodesList" style="border-bottom: #f7f7f7 1px solid;">'
                                               + '<a ng-click="selectCode(a)">'
                                                   + '<img src="/Content/lib/CountryFlags/flags/{{a.CountryFlag}}" width="32px" height="32px" style="padding: 2px;background: #f3f3f3;line-height: normal;margin-right: 8px;" />'
                                                   + '<span>'
                     + '{{a.CountryCallCode}}({{a.CountryName}})'
                     + '</span></a></li></ul></div>';
                                  element.replaceWith($compile(tempText)(scope));
                              }, function (response) {
                                  //  //console.log("Some thing wrong");
                              });

                              scope.selectCode = function (row) {
                                  console.log(row);
                                  scope.ngModel = { Display: row.CountryCallCode, Value: row.CountryId };
                                  scope.onclick({ value: row });
                              }
                              scope.AddNew = function () {
                                  scope.onaddnew();
                              }

                          }
                      }
                  }]);
})()