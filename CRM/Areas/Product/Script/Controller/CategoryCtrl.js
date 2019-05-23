(function () {
    "use strict";
    angular.module("CRMApp.Controllers")
        .controller("CategoryCtrl", [
         "$scope", "CategoryService", "$uibModal",
         CategoryCtrl]);

    function CategoryCtrl($scope, CategoryService, $uibModal) {

        $scope.Add = function (id, _isdisable) {
            if (_isdisable === undefined) _isdisable = 0;
            var modalInstance = $uibModal.open({
                backdrop: 'static',
                templateUrl: 'myModalContent.html',
                controller: ModalInstanceCtrl,
                resolve: {
                    CategoryService: function () { return CategoryService; },
                    id: function () { return id; },
                    isdisable: function () { return _isdisable;}
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
               //{ "title": "CategoryId", "field": "CategoryId", filter: false, visible: false },
               { "title": "Sr.", "field": "RowNumber", filter: false, show: true },
               { "title": "Main Category", "field": "CategoryName", sortable: "CategoryName", filter: { CategoryName: "text" }, show: true },
               {
                   "title": "Action", show: true,
                   'cellTemplte': function ($scope, row) {
                       var element = '<a class="btn btn-primary btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.Edit(row.CategoryId)" data-uib-tooltip="Edit"><i class="fa fa-pencil" ></i></a>' +
                       //'<a class="btn btn-danger btn-xs" data-final-confirm-box=""  data-callback="$parent.$parent.$parent.Delete(row.CategoryId)"  data-message="Are you sure want to delete?" data-uib-tooltip="delete"><i class="fa fa-trash"></i></a> '+
                           '<a class="btn btn-info btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.View(row.CategoryId)" data-uib-tooltip="View"><i class="fa fa-eye" ></i></a>'
                       return $scope.getHtml(element);
                   }
                  
               }
            ],
            Sort: { 'CategoryId': 'asc' },
            
            //modeType: "CategoryMaster",
            //Title: "Category List"
        }

        $scope.Edit = function (id) {
            $scope.Add(id,0);
        }
        $scope.View = function (id) {
            $scope.Add(id,1);
        }
        $scope.Delete = function (id) {
            CategoryService.DeleteCategory(id).then(function (result) {
                if (result.data.ResponseType == 1) {
                    toastr.success(result.data.Message);
                    $scope.refreshGrid()
                } else {
                    toastr.error(result.data.Message, 'Opps, Something went wrong');
                }
            }, function (errorMsg) {
                toastr.error(errorMsg, 'Opps, Something went wrong');
            })
        }

        $scope.setDirectiveRefresh = function (refreshGrid) {
            $scope.refreshGrid = refreshGrid;
        };
    }

    var ModalInstanceCtrl = function ($scope, $uibModalInstance, CategoryService, id, isdisable) {
        $scope.isClicked = false;
        if (isdisable == 1) {
            $scope.isClicked = true;
        }
        $scope.objCategory = $scope.objCategory || {};
        $scope.addMode = true;
        $scope.saveText = "Save";
        $scope.timeZone = new Date().getTimezoneOffset().toString();
        $scope.id = id;
        $scope.storage = {};


        if (id && id > 0) {
            CategoryService.GetCategoryById(id).then(function (result) {
                if (result.data.ResponseType == 1) {
                    $scope.objCategory = {
                        CategoryId: result.data.DataList.CategoryId,
                        CategoryName: result.data.DataList.CategoryName
                    }
                    $scope.storage = angular.copy($scope.objCategory);
                    //$scope.objCategory = result.data.DataList.CategoryName;
                } else {
                    toastr.error(result.data.Message, 'Opps, Something went wrong');
                }
            }, function (errorMsg) {
                toastr.error(errorMsg, 'Opps, Something went wrong');
            })
        } else {
            ResetForm();
        }

        function ResetForm() {
            $scope.objCategory = {
                CategoryId: 0,
                CategoryName: ''
            }
            $scope.storage = {};
            $scope.addMode = true;
            $scope.saveText = "Save";
            if ($scope.$parent.FormCategoryInfo)
                $scope.$parent.FormCategoryInfo.$setPristine();
        }

        $scope.CreateUpdate = function (data) {
            CategoryService.CreateUpdateCategory(data).then(function (result) {
                if (result.data.ResponseType == 1) {
                    $uibModalInstance.close();
                    toastr.success(result.data.Message);

                } else {
                    toastr.error(result.data.Message);
                }
            }, function (errorMsg) {
                toastr.error(errorMsg, 'Opps, Something went wrong');
            })
        }

        $scope.Reset = function () {
            if ($scope.objCategory.CategoryId > 0) {
                $scope.objCategory = angular.copy($scope.storage);
            } else {
                ResetForm();
            }
        }

        $scope.close = function () {
            $uibModalInstance.close();
        };

    }
    ModalInstanceCtrl.$inject = ['$scope', '$uibModalInstance', 'CategoryService', 'id','isdisable']
})()




