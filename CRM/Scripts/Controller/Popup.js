
(function () {

    angular.module('CRMApp.Controllers')
          .controller("PopupCrtl",
          ["$scope", "$rootScope", "$timeout",
              PopupCrtl]);

    function PopupCrtl($scope, $rootScope, $timeout) {
        var data = JSON.parse(document.getElementById("hdnColumns").value);
        debugger
        for (i = 0; i < data.length; i++) {
            if (data[i].cellTemplte) {
                var actualMarkup = data[i].cellTemplte;
                data[i].cellTemplte = function ($scope, row) {
                    var element = actualMarkup;
                    return $scope.getHtml(element);
                }
            }
        }
        $scope.gridObj = {
            columnsInfo: data,
            //Sort: { 'CategoryId': 'asc' },
        }
        $scope.fixedclause = $("#hdnRelatedValue").val()
        $scope.getData = function (row) {
            
            $(window.parent.document.getElementById(document.getElementById("hdncntrlId").value)).find("#hdnVal").val(row[document.getElementById("hdnValue").value]);
            $(window.parent.document.getElementsByClassName("modal-backdrop in")).remove();
            //  $(window.parent.document.getElementById("myPopupModal")).parent().find("#hdnVal").trigger('input');
            window.parent.angular.element($(window.parent.document.getElementById(document.getElementById("hdncntrlId").value)).find("#hdnVal")).scope().ngModel = { Display: row[document.getElementById("hdnDisplay").value], Value: row[document.getElementById("hdnValue").value] };
            //  $(window.parent.document.getElementById("myPopupModal")).parent().find("#AutoComplete").trigger('input');
            $(window.parent.document.getElementById(document.getElementById("hdncntrlId").value)).find("#AutoComplete").val(row[document.getElementById("hdnDisplay").value]);
            //  $(window.parent.document.getElementById("myPopupModal")).parent().find("#AutoComplete").trigger('change');
            var $scope = window.parent.angular.element($(window.parent.document.getElementById(document.getElementById("hdncntrlId").value)).find("#hdnVal")).scope();
            $scope.$apply(function () {
                $scope.ngModel = { Display: row[document.getElementById("hdnDisplay").value], Value: row[document.getElementById("hdnValue").value] };
            });
            $(window.parent.document.getElementById("myPopupModal")).remove();

        }
    }
})()