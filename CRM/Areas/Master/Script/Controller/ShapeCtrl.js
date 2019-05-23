(function () {
    "use strict";
    angular.module("CRMApp.Controllers")
        .controller("ShapeCtrl", [
         "$scope", "$rootScope", "$timeout", "$filter", "ShapeService", "$uibModal",
         ShapeCtrl
        ]);

    function ShapeCtrl($scope, $rootScope, $timeout, $filter, ShapeService, $uibModal) {

        $scope.objShape = $scope.objShape || {};
        $scope.objShape = {
            ShapeId: 0,
            ShapeName: '',
            Description: '',
            Photo: ''
        }

        $scope.Add = function (data, _isdisable) {
            if (_isdisable === undefined) _isdisable = 0;
            var modalInstance = $uibModal.open({
                backdrop: 'static',
                templateUrl: 'ShapeModal.html',
                controller: ModalInstanceCtrl,
                resolve: {
                    ShapeCtrl: function () { return $scope },
                    ShapeService: function () { return ShapeService; },
                    data: function () { return data; },
                    isdisable: function () { return _isdisable; }
                }
            });
            modalInstance.result.then(function () {
                $scope.refreshGrid();
            }, function () {
            });
        }

        $scope.gridObj = {
            columnsInfo: [
               //{ "title": "ShapeId", "data": "ShapeId", filter: false, visible: false },
               { "title": "Sr.", "field": "RowNumber", show: true, },
               { "title": "Shape Name", "field": "ShapeName", sortable: "ShapeName", filter: { ShapeName: "text" }, show: true, },
               //{ "title": "Description", "data": "Description", sort: true, filter: true },
               {
                   "title": "Description", "field": "Description", sortable: "Description", filter: { Description: "text" }, show: true,
                   'cellTemplte': function ($scope, row) {
                       var element = '<p data-uib-tooltip="{{row.Description}}" ng-bind="LimitString(row.Description,100)">'
                       return $scope.getHtml(element);
                   }
               },
               {
                   "title": "Images", "field": "Photo", sortable: "Photo", show: true,
                   'cellTemplte': function ($scope, row) {
                       var element = '<img src="/UploadImages/Shape/{{row.Photo}}"  data-ng-if="row.Photo" style="width: 40px;height: 40px;"/>' +
                             '<img src="http://placehold.it/350x150" data-ng-if="!row.Photo"  style="width: 40px;height: 40px;"/>'
                       return $scope.getHtml(element);
                   }
               },
               {
                   "title": "Action", show: true,
                   'cellTemplte': function ($scope, row) {
                       var element = '<a class="btn btn-primary btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.Edit(row)" data-uib-tooltip="Edit"><i class="fa fa-pencil" ></i></a>' +
                            //'<a class="btn btn-danger btn-xs" data-final-confirm-box=""  data-callback="$parent.$parent.$parent.Delete(row.ShapeId)"  data-message="Are you sure want to delete?" data-uib-tooltip="delete"><i class="fa fa-trash"></i></a> '+
                            '<a class="btn btn-info btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.View(row)" data-uib-tooltip="View"><i class="fa fa-eye" ></i></a>'
                       return $scope.getHtml(element);
                   }
               }
            ],
            Sort: { 'ShapeId': 'asc' },
        }

        $scope.Edit = function (data) {
            $scope.Add(data, 0);
        }
        $scope.View = function (data) {
            $scope.Add(data, 1);
        }
        $scope.Delete = function (id) {
            ShapeService.Delete(id).then(function (result) {
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

    var ModalInstanceCtrl = function ($scope, $uibModalInstance, ShapeCtrl, ShapeService, data, Upload, isdisable) {
        $scope.isClicked = false;
        if (isdisable == 1) {
            $scope.isClicked = true;
        }

        $scope.objShape = $scope.objShape || {};
        $scope.addMode = true;
        $scope.saveText = "Save";
        $scope.timeZone = new Date().getTimezoneOffset().toString();
        $scope.storage = {};

        if (data.ShapeId <= 0) {
            ResetForm()
        } else {
            $scope.tempImagePath = '';
            $scope.objShape = {
                ShapeId: data.ShapeId,
                ShapeName: data.ShapeName,
                Description: data.Description,
                Photo: data.Photo,
            }
            $scope.storage = angular.copy($scope.objShape);
        }

        $scope.uploadImgFile = function (file) {
            $scope.objShape.Photo = '';
            Upload.upload({
                url: "/Handler/FileUpload.ashx",
                method: 'POST',
                file: file,
            }).then(function (result) {
                if (result.status == 200) {
                    if (result.data.length > 0) {
                        $scope.objShape.Photo = result.data[0].imageName;
                        $scope.tempImagePath = result.data[0].imagePath;
                    }
                }
                else {
                    $scope.objShape.Photo = '';
                }
            });
        }

        function ResetForm() {
            $scope.tempImagePath = '';
            $scope.objShape = {
                ShapeId: 0,
                ShapeName: '',
                Description: '',
                Photo: ''
            }
            $scope.storage = {};
            $scope.addMode = true;
            $scope.saveText = "Save";
            if ($scope.FormShapeInfo)
                $scope.FormShapeInfo.$setPristine();
        }

        $scope.CreateUpdate = function (data) {
            if ($scope.objShape.Photo != null && $scope.objShape.Photo != '') {
                ShapeService.CreateUpdate(data).then(function (result) {
                    if (result.data.ResponseType == 1) {
                        $uibModalInstance.close();
                        toastr.success(result.data.Message);
                    } else {
                        toastr.error(result.data.Message);
                    }
                }, function (errorMsg) {
                    toastr.error(errorMsg, 'Opps, Something went wrong');
                })
            } else {
                $scope.Message = "Please Select Image";
                toastr.error($scope.Message);
            }
        }

        $scope.Reset = function () {
            if ($scope.objShape.ShapeId > 0) {
                $scope.tempImagePath = '';
                $scope.objShape = angular.copy($scope.storage);
            } else {
                ResetForm();
            }
        }

        $scope.close = function () {
            $uibModalInstance.close();
        };
    }
    ModalInstanceCtrl.$inject = ['$scope', '$uibModalInstance', 'ShapeCtrl', 'ShapeService', 'data', 'Upload', 'isdisable']

})()
