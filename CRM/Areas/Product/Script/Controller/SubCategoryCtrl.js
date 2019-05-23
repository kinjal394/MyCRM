(function () {
    "use strict";
    angular.module("CRMApp.Controllers")
        .controller("SubCategoryCtrl", [
         "$scope", "SubCategoryService", "$uibModal",
         SubCategoryCtrl]);

    function SubCategoryCtrl($scope, SubCategoryService, $uibModal) {

        $scope.objSubcategoryData = $scope.objSubcategoryData || {};
        $scope.objSubcategoryData = {
            SubCategoryId: 0,
            SubCategoryName: '',
            CategoryId: 0,
            CategoryData: {
                Display: "",
                Value: ""
            }
        }

        $scope.Add = function (data, _isdisable) {
            if (_isdisable === undefined) _isdisable = 0;
            var modalInstance = $uibModal.open({
                backdrop: 'static',
                templateUrl: 'myModalContent.html',
                controller: ModalInstanceCtrl,
                resolve: {
                    SubCategoryService: function () { return SubCategoryService; },
                    objSubcategoryData: function () { return data; },
                    isdisable: function () { return _isdisable; }
                }
            });
            modalInstance.result.then(function () {
                $scope.refreshGrid();
            }, function () {
            });
        }

        $scope.setDirectiveRefresh = function (refreshGrid) {
            $scope.refreshGrid = refreshGrid;
        };

        $scope.gridObj = {
            columnsInfo: [
               //{ "title": "SubCategoryId", "field": "SubCategoryId", filter: false, show: true },
               { "title": "Sr.", "field": "RowNumber", filter: false, show: true },
               //{ "title": "CategoryId", "field": "CategoryId", filter: false, show: true },
               { "title": "Main Category", "field": "CategoryName", sortable: "CategoryName", filter: { CategoryName: "text" }, show: true },
               { "title": "Sub Category", "field": "SubCategoryName", sortable: "SubCategoryName", filter: { SubCategoryName: "text" }, show: true },
               {
                   "title": "Action", show: true,
                   'cellTemplte': function ($scope, row) {
                       var element = '<a class="btn btn-primary btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.Edit(row)" data-uib-tooltip="Edit"><i class="fa fa-pencil" ></i></a>' +
                            //'<a class="btn btn-danger btn-xs" data-final-confirm-box=""  data-callback="$parent.$parent.$parent.$parent.$parent.$parent.Delete(row.SubCategoryId)"  data-message="Are you sure want to delete?" data-uib-tooltip="delete"><i class="fa fa-trash"></i></a> '+
                            '<a class="btn btn-info btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.View(row)" data-uib-tooltip="View"><i class="fa fa-eye" ></i></a>';
                       return $scope.getHtml(element);
                   }
               }
            ],
            Sort: { 'SubCategoryId': 'asc' }
        }

        $scope.Edit = function (data) {

            var objSubcategoryData = {
                SubCategoryId: data.SubCategoryId,
                SubCategoryName: data.SubCategoryName,
                CategoryId: data.CategoryId,
                CategoryData: {
                    Display: data.CategoryName,
                    Value: data.CategoryId
                }
            };
            $scope.Add(objSubcategoryData, 0);
        }

        $scope.View = function (data) {

            var objSubcategoryData = {
                SubCategoryId: data.SubCategoryId,
                SubCategoryName: data.SubCategoryName,
                CategoryId: data.CategoryId,
                CategoryData: {
                    Display: data.CategoryName,
                    Value: data.CategoryId
                }
            };
            $scope.Add(objSubcategoryData, 1);
        }

        $scope.setDirectiveRefresh = function (refreshGrid) {
            $scope.refreshGrid = refreshGrid;
        };

        $scope.Delete = function (id) {
            SubCategoryService.DeleteSubCategory(id).then(function (result) {
                if (result.data.ResponseType == 1) {
                    toastr.success(result.data.Message);
                    $scope.refreshGrid()
                }
                else {
                    toastr.error(result.data.Message, 'Opps, Something went wrong');
                }
            }, function (errorMsg) {
                toastr.error(errorMsg, 'Opps, Something went wrong');
            })
        }
    }

    var ModalInstanceCtrl = function ($scope, $uibModalInstance, SubCategoryService, objSubcategoryData, isdisable) {
        $scope.isClicked = false;
        if (isdisable == 1) {
            $scope.isClicked = true;
        }
        $scope.objSubCategory = $scope.objSubCategory || {};
        $scope.addMode = true;
        $scope.saveText = "Save";
        $scope.storage = {};

        if (objSubcategoryData.SubCategoryId > 0) {
            $scope.objSubCategory = objSubcategoryData;
            $scope.storage = angular.copy(objSubcategoryData);
        } else {
            ResetForm();
        }

        function ResetForm() {
            $scope.objSubCategory = {
                SubCategoryId: 0,
                SubCategoryName: '',
                CategoryId: 0,
                CategoryData: {
                    Display: "",
                    Value: ""
                }
            }
            $scope.storage = {};
            $scope.addMode = true;
            $scope.saveText = "Save";
            if ($scope.FormCategoryInfo)
                $scope.FormCategoryInfo.$setPristine();
        }

        $scope.CreateUpdate = function (data) {
            var ProductSub = {
                SubCategoryId: data.SubCategoryId,
                CategoryId: data.CategoryData.Value,
                SubCategoryName: data.SubCategoryName
            }
            SubCategoryService.CreateUpdate(ProductSub).then(function (result) {
                if (result.data.ResponseType == 1) {
                    $uibModalInstance.close();
                    ResetForm();
                    toastr.success(result.data.Message);
                }
                else {
                    toastr.error(result.data.Message);
                }
            }, function (errorMsg) {
                toastr.error(errorMsg, 'Opps, Something went wrong');
            })
        }

        $scope.Reset = function () {
            if ($scope.objSubCategory.SubCategoryId > 0) {
                $scope.objSubCategory = angular.copy($scope.storage);
            } else {
                ResetForm();
            }
        }

        $scope.close = function () {
            $uibModalInstance.close();
        };
    }
    ModalInstanceCtrl.$inject = ['$scope', '$uibModalInstance', 'SubCategoryService', 'objSubcategoryData', 'isdisable']
})()


