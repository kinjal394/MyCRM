(function () {
    "use strict";
    angular.module("CRMApp.Controllers")
    .controller("PlugShapeCtrl", [
     "$scope", "PlugShapeService", "$uibModal",
     PlugShapeCtrl
    ]);

    function PlugShapeCtrl($scope, PlugShapeService, $uibModal) {

        $scope.objPlugShape = $scope.objPlugShape || {};
        $scope.objPlugShape = {
            PlugShapeId: 0,
            Title: '',
            Description: '',
            Photo: ''
        }

        $scope.setDirectiveRefresh = function (refreshGrid) {
            $scope.refreshGrid = refreshGrid;
        };

        $scope.Add = function (data, _isdisable) {
            if (_isdisable === undefined) _isdisable = 0;
            var modalInstance = $uibModal.open({
                backdrop: 'static',
                templateUrl: 'PlugShapeModal.html',
                controller: ModalInstanceCtrl,
                resolve: {
                    PlugShapeCtrl: function () { return $scope },
                    PlugShapeService: function () { return PlugShapeService; },
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
               //{ "title": "PlugShapeId", "data": "PlugShapeId", filter: false, visible: false },
               { "title": "Sr.", "field": "RowNumber", show: true, },
               { "title": "Title", "field": "Title", sortable: "Title", filter: { Title: "text" }, show: true, },
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
                       var element = '<img src="/UploadImages/PlugShape/{{row.Photo}}"  data-ng-if="row.Photo" style="width: 40px;height: 40px;"/>' +
                             '<img src="http://placehold.it/350x150" data-ng-if="!row.Photo"  style="width: 40px;height: 40px;"/>'
                       return $scope.getHtml(element);
                   }
               },
               {
                   "title": "Action", show: true,
                   'cellTemplte': function ($scope, row) {
                       var element = '<a class="btn btn-primary btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.Edit(row)" data-uib-tooltip="Edit"><i class="fa fa-pencil" ></i></a>' +
                            //'<a class="btn btn-danger btn-xs" data-final-confirm-box=""  data-callback="$parent.$parent.$parent.Delete(row.PlugShapeId)"  data-message="Are you sure want to delete?" data-uib-tooltip="delete"><i class="fa fa-trash"></i></a> '+
                            '<a class="btn btn-info btn-xs"  data-ng-click="$parent.$parent.$parent.$parent.$parent.$parent.View(row)" data-uib-tooltip="View"><i class="fa fa-eye" ></i></a>'
                       return $scope.getHtml(element);
                   }
               }
            ],
            Sort: { 'CategoryId': 'asc' },
        }

        $scope.Edit = function (data) {
            $scope.Add(data, 0);
        }

        $scope.View = function (data) {
            $scope.Add(data, 1);
        }
        $scope.Delete = function (id) {
            PlugShapeService.Delete(id).then(function (result) {
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

    }

    var ModalInstanceCtrl = function ($scope, $uibModalInstance, PlugShapeCtrl, PlugShapeService, data, Upload, isdisable) {
        $scope.isClicked = false;
        if (isdisable == 1) {
            $scope.isClicked = true;
        }

        $scope.objPlugShape = $scope.objPlugShape || {};
        $scope.addMode = true;
        $scope.saveText = "Save";
        $scope.timeZone = new Date().getTimezoneOffset().toString();
        $scope.storage = {};

        if (data.PlugShapeId <= 0) {
            ResetForm()
        } else {
            $scope.tempImagePath = '';
            $scope.objPlugShape = {
                PlugShapeId: data.PlugShapeId,
                Title: data.Title,
                Description: data.Description,
                Photo: data.Photo
            }
            $scope.storage = angular.copy($scope.objPlugShape);
        }

        $scope.uploadImgFile = function (file) {
            $scope.objPlugShape.Photo = '';
            Upload.upload({
                url: "/Handler/FileUpload.ashx",
                method: 'POST',
                file: file,
            }).then(function (result) {
                if (result.status == 200) {
                    if (result.data.length > 0) {
                        $scope.objPlugShape.Photo = result.data[0].imageName;
                        $scope.tempImagePath = result.data[0].imagePath;
                    }
                }
                else {
                    $scope.objPlugShape.Photo = '';
                }
            });
        }

        function ResetForm() {
            $scope.tempImagePath = '';
            $scope.objPlugShape = {
                PlugShapeId: data.PlugShapeId,
                Title: data.Title,
                Description: data.Description,
                Photo: data.Photo
            }
            $scope.storage = {};
            $scope.addMode = true;
            $scope.saveText = "Save";
            if ($scope.FormPlugShapeInfo)
                $scope.FormPlugShapeInfo.$setPristine();
        }

        $scope.CreateUpdate = function (data) {
            if ($scope.objPlugShape.Photo != null && $scope.objPlugShape.Photo != '') {
                PlugShapeService.CreateUpdate(data).then(function (result) {
                    if (result.data.ResponseType == 1) {
                        $uibModalInstance.close();
                        toastr.success(result.data.Message);
                        PlugShapeCtrl.refreshGrid();
                    } else {
                        toastr.error(result.data.Message, 'Opps, Something went wrong');
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
            if ($scope.objPlugShape.ShapeId > 0) {
                $scope.tempImagePath = '';
                $scope.objPlugShape = angular.copy($scope.storage);
            } else {
                ResetForm();
            }
        }

        $scope.close = function () {
            $uibModalInstance.close();
        };
    }
    ModalInstanceCtrl.$inject = ['$scope', '$uibModalInstance', 'PlugShapeCtrl', 'PlugShapeService', 'data', 'Upload', 'isdisable']

})()

