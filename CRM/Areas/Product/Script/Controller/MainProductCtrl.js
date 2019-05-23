(function () {
    "use strict";
    angular.module("CRMApp.Controllers")
        .controller("MainProductCtrl", [
         "$scope", "MainProductService", "$uibModal",
           function MainProductCtrl($scope, MainProductService, $uibModal) {
               $scope.storage = {};
               $scope.Add = function () {
                   var _isdisable = 0;
                   debugger;
                   $scope.mainProductObj = {
                       SubCategoryId: 0,
                       SubCategoryName: '',
                       CategoryId: 0,
                       CategoryName: '',
                       MainProductName: '',
                       MainProductId: 0,
                       CategoryData: {
                           Display: "",
                           Value: ""
                       },
                       SubCategoryData: {
                           Display: "",
                           Value: ""
                       }
                   };
                   var modalInstance = $uibModal.open({
                       backdrop: 'static',
                       animation: true,
                       ariaLabelledBy: 'modal-title',
                       ariaDescribedBy: 'modal-body',
                       templateUrl: 'mainProductModalContent.html',
                       controller: 'mainProductModalInstanceCtrl',
                       controllerAs: '$ctrl',
                       size: 'lg',
                       resolve: {
                           mainProductObj: function () {
                               return $scope.mainProductObj;
                           },
                           storage: function () {
                               return $scope.storage;
                           },
                           isdisable: function () { return _isdisable; }
                       }
                   })
                   modalInstance.result.then(function (selectedItem) {
                       debugger;
                       $scope.refreshGrid();
                       // $scope.tableParams.reload();
                   }, function () {
                       // $log.info('Modal dismissed at: ' + new Date());
                   });
               }

               $scope.setDirectiveRefresh = function (refreshGrid) {
                   debugger;
                   $scope.refreshGrid = refreshGrid;
               };

               $scope.gridObj = {
                   columnsInfo: [
                      { "title": "MainProductId", "data": "MainProductId", filter: false, visible: false },
               { "title": "Sr.", "data": "RowNumber", filter: false, sort: false },
                      { "title": "Main Category", "data": "CategoryName", sort: true, filter: true, },
                      { "title": "Sub Category", "data": "SubCategoryName", sort: true, filter: true, },
                      { "title": "Main Product", "data": "MainProductName", sort: true, filter: true, },
                      {
                          "title": "Action", sort: false, filter: false,
                          'render': '<a class="btn btn-primary btn-xs"  data-ng-click="$parent.$parent.$parent.Edit(row)" data-uib-tooltip="Edit"><i class="fa fa-pencil" ></i></a>' +
                                   '<a class="btn btn-danger btn-xs" data-final-confirm-box=""  data-callback="$parent.$parent.$parent.Delete(row.MainProductId)"  data-message="Are you sure want to delete?" data-uib-tooltip="delete"><i class="fa fa-trash"></i></a> '+
                                   '<a class="btn btn-info btn-xs"  data-ng-click="$parent.$parent.$parent.View(row)" data-uib-tooltip="View"><i class="fa fa-eye" ></i></a>'
                      }
                   ],
                   Sort: { 'MainProductId': 'asc' },
               }

               $scope.Delete = function (productId) {
                   debugger;
                   MainProductService.DeleteMainProduct(productId).then(function (result) {
                       if (result.data.ResponseType == 1) {
                           toastr.success(result.data.Message)
                           $scope.refreshGrid();
                       }
                       else {
                           toastr.error(result.data.Message)
                       }
                   }, function (errorMsg) {
                       toastr.error(errorMsg, 'Opps, Something went wrong');
                   })
               }

               $scope.Edit = function (row) {
                   var _isdisable = 0;
                   $scope.mainProductObj = {
                       SubCategoryId: row.SubCategoryId,
                       SubCategoryName: row.SubCategoryName,
                       CategoryId: row.CategoryId,
                       CategoryName: row.CategoryName,
                       MainProductName: row.MainProductName,
                       MainProductId: row.MainProductId,
                       CategoryData: {
                           Display: row.CategoryName,
                           Value: row.CategoryId
                       },
                       SubCategoryData: {
                           Display: row.SubCategoryName,
                           Value: row.SubCategoryId,
                       }
                   };
                   $scope.storage = angular.copy($scope.mainProductObj);
                   var modalInstance = $uibModal.open({
                       backdrop: 'static',
                       animation: true,
                       ariaLabelledBy: 'modal-title',
                       ariaDescribedBy: 'modal-body',
                       templateUrl: 'mainProductModalContent.html',
                       controller: 'mainProductModalInstanceCtrl',
                       controllerAs: '$ctrl',
                       size: 'lg',
                       resolve: {
                           mainProductObj: function () {
                               debugger;
                               return $scope.mainProductObj;
                           },
                           storage: function () {
                               return $scope.storage;
                           },
                           isdisable: function () { return _isdisable; }
                       }
                   })
                   modalInstance.result.then(function (selectedItem) {
                       debugger;
                       $scope.refreshGrid();
                       // $scope.tableParams.reload();
                   }, function () {
                       // $log.info('Modal dismissed at: ' + new Date());
                   });
               }
               $scope.View = function (row) {
                   var _isdisable = 1;
                   $scope.mainProductObj = {
                       SubCategoryId: row.SubCategoryId,
                       SubCategoryName: row.SubCategoryName,
                       CategoryId: row.CategoryId,
                       CategoryName: row.CategoryName,
                       MainProductName: row.MainProductName,
                       MainProductId: row.MainProductId,
                       CategoryData: {
                           Display: row.CategoryName,
                           Value: row.CategoryId
                       },
                       SubCategoryData: {
                           Display: row.SubCategoryName,
                           Value: row.SubCategoryId,
                       }
                   };
                   $scope.storage = angular.copy($scope.mainProductObj);
                   var modalInstance = $uibModal.open({
                       backdrop: 'static',
                       animation: true,
                       ariaLabelledBy: 'modal-title',
                       ariaDescribedBy: 'modal-body',
                       templateUrl: 'mainProductModalContent.html',
                       controller: 'mainProductModalInstanceCtrl',
                       controllerAs: '$ctrl',
                       size: 'lg',
                       resolve: {
                           mainProductObj: function () {
                               debugger;
                               return $scope.mainProductObj;
                           },
                           storage: function () {
                               return $scope.storage;
                           },
                           isdisable: function () { return _isdisable; }
                       }
                   })
                   modalInstance.result.then(function (selectedItem) {
                       debugger;
                       $scope.refreshGrid();
                       // $scope.tableParams.reload();
                   }, function () {
                       // $log.info('Modal dismissed at: ' + new Date());
                   });
               }
           }]);

    angular.module('CRMApp.Controllers')
        .controller('mainProductModalInstanceCtrl', [
            "$scope", "$uibModalInstance", "mainProductObj", "MainProductService", "storage","isdisable",
            function ($scope, $uibModalInstance, mainProductObj, MainProductService, storage, isdisable) {
                $scope.isClicked = false;
                if (isdisable == 1) {
                    $scope.isClicked = true;
                }
                debugger;
                var $ctrl = this;

                $ctrl.mainProductObj = mainProductObj;
                $ctrl.storage = storage;
                $ctrl.close = function () {
                    debugger;
                    $uibModalInstance.dismiss();
                };

                $ctrl.cancel = function () {
                    debugger;
                    $uibModalInstance.dismiss('cancel');
                };

                $ctrl.Create = function () {
                    debugger;
                    $ctrl.mainProductObj.SubCategoryId = $ctrl.mainProductObj.SubCategoryData.Value
                    console.log($ctrl.mainProductObj.SubCategoryData.Value);
                    MainProductService.SaveMainProduct($ctrl.mainProductObj).then(function (result) {
                        if (result.data.ResponseType == 1) {
                            $uibModalInstance.close();
                            toastr.success(result.data.Message)
                        }
                        else {
                            toastr.error(result.data.Message)
                        }
                    }, function (errorMsg) {
                        toastr.error(errorMsg, 'Opps, Something went wrong');
                    })
                }

                $ctrl.Update = function () {
                    debugger;
                    MainProductService.UpdateMainProduct($ctrl.mainProductObj).then(function (result) {
                        if (result.data.ResponseType == 1) {
                            $uibModalInstance.close();

                            toastr.success(result.data.Message)
                        }
                        else {
                            toastr.error(result.data.Message)
                        }

                    }, function (errorMsg) {
                        toastr.error(errorMsg, 'Opps, Something went wrong');
                    })
                }

                $ctrl.Reset = function () {

                    if ($ctrl.mainProductObj.MainProductId > 0) {

                        $ctrl.mainProductObj = angular.copy($ctrl.storage);

                    } else {
                        ResetForm();
                    }
                }


                function ResetForm() {

                    $ctrl.mainProductObj = {
                        MainProductId: 0,
                        CategoryName: '',
                        SubCategoryName: '',
                        MainProductName: '',
                        
                    }
                    $ctrl.storage = {};
                    $ctrl.addMode = true;
                    $ctrl.saveText = "Save";
                    if ($ctrl.$parent.MainProductInfo)
                        $ctrl.$parent.MainProductInfo.$setPristine();
                }

            }]);
})()