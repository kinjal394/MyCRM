(function () {

    angular.module('CRMApp.Controllers', [])
           .controller("DemoCtrl",
           ["$scope", "$uibModal", "$rootScope", "$timeout", '$compile',
               DemoCtrl]);

    function DemoCtrl($scope, $uibModal, $rootScope, $timeout, $compile) {
        //$scope.gridObj = {
        //    columnsInfo: [
        //       { "title": "ID", "data": "UserId", filter: false, visible: false },
        //       { "title": "Name", "data": "Identifier", sort: false, filter: false, },
        //       { "title": "Email", "data": "Email", render: '<a href="mailto:{{row.Email}}">{{row.Email}}</a>' },
        //       { "title": "Mobile No", "data": "MobileNo" },
        //       {
        //           "title": "Action",
        //           'render': '<button  data-ng-click="$parent.$parent.$parent.Edit(row)">Edit</button><button data-ng-click="$parent.$parent.$parent.Delete(row)">Delete</button> '
        //       }
        //    ],
        //    Sort: { 'UserId': 'asc' },
        //    modeType: "UserMain"
        //}
        $scope.gridObj = {
            columnsInfo: [
               {
                   "title": "CategoryId", "data": "CategoryId", filter: false, visible: false,
               },
               {
                   "title": "CategoryName", "data": "CategoryName", sort: false, filter: false,
               },
            ],
            Sort: { 'CategoryId': 'asc' },
            modeType: "CategoryMaster",
            Title: "Category List"

        }
        $scope.DblClick = function () {
            alert("aa135a");
        }
        $scope.updateFn = function (data) {
            alert("aaa");
        }
        $scope.Edit = function (data) {
            //console.log(data);
        }
        $scope.Delete = function (data) {
            //console.log(data);
        }
        $scope.IsActive = function (data) {
            //console.log(data);
        }
        $scope.movies = [{ id: "1", text: "The Wolverine" }, { id: "2", text: "The Smurfs 2" }, { id: "3", text: "The Mortal Instruments: City of Bones" }]
        //$scope.movies = ["The Wolverine", "The Smurfs 2", "The Mortal Instruments: City of Bones"]
        //"Drinking Buddies", "All the Boys Love Mandy Lane", "The Act Of Killing", "Red 2", "Jobs", "Getaway", "Red Obsession", "2 Guns", "The World's End", "Planes", "Paranoia", "The To Do List", "Man of Steel"];
        $scope.getmovies = function () {
            return $scope.movies;
        }
        $scope.doSomething = function (typedthings) {
            //console.log("Do something like reload data with this: " + typedthings);
            $scope.newmovies = [{ id: "6", text: "The Wolverine" }, { id: "5", text: "The Smurfs 2" }];
            //$scope.newmovies = ["The Wolverine", "The Smurfs 2"];
            $scope.movies = $scope.newmovies
            //$scope.newmovies.then(function (data) {
            //    $scope.movies = data;
            //});
        }

        $scope.doSomethingElse = function (suggestion) {
            //console.log("Suggestion selected: " + suggestion);
        }

        $scope.open = function () {
            var modalInstance = $uibModal.open({
                animation: true,
                ariaLabelledBy: 'modal-title',
                ariaDescribedBy: 'modal-body',
                templateUrl: 'myModalContent.html',
                controller: 'ModalInstanceCtrl',
                controllerAs: '$ctrl',
                size: 'lg',
                resolve: {
                    gridInfo: function () {
                        return $scope.gridObj;
                    }
                }
            });

            modalInstance.result.then(function (selectedItem) {
                alert(selectedItem)
                // $ctrl.selected = selectedItem;
            }, function () {
                // $log.info('Modal dismissed at: ' + new Date());
            });
        };

    }

    angular.module('CRMApp.Controllers').controller('ModalInstanceCtrl', function ($uibModalInstance, gridInfo, Service) {
        var $ctrl = this;
        $ctrl.gridInfo = gridInfo;
        $ctrl.ok = function () {
            $uibModalInstance.close();
        };

        $ctrl.cancel = function () {
            $uibModalInstance.dismiss('cancel');
        };
        $ctrl.updateFn = function (data) {
            alert("aaa");
            //console.log(data)
            Service.foo = 'I am from contoller 1';
            $ctrl.itemData = Service.foo;
            $uibModalInstance.close($ctrl.itemData);
        }
    });

    angular.module("CRMApp").factory('Service', function () {
        var Service = {
            foo: 'Shared service'
        };
        return Service;
    });
})()