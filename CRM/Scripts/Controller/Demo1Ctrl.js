
(function () {

    angular.module('CRMApp.Controllers')
          .controller("Demo1Ctrl",
          ["$scope", "$rootScope", "$timeout",
              Demo1Ctrl]);

    function Demo1Ctrl($scope, $rootScope, $timeout) {
        $scope.abc = 'akash';
        $scope.gridObj = {
            columnsInfo: [
               { "title": "CategoryId", "data": "CategoryId", filter: false, visible: false },
               { "title": "CategoryName", "data": "CategoryName", sort: false, filter: false, },
               {
                   "title": "Action",
                   'render': '<button  data-ng-click="$parent.$parent.$parent.Edit(row)">Edit</button><button data-ng-click="$parent.$parent.$parent.Delete(row)">Delete</button> '
               }
            ],
            Sort: { 'CategoryId': 'asc' },
            modeType: "CategoryMaster",
            title: "Category List"
        }
    }
})()