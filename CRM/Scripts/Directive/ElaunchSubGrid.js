(function () {
    'use strict';
    angular
        .module("CRMApp.Directives")
        .directive("elaunchgrid", [
            "$rootScope", "NgTableParams", "$http", "$compile", "$interpolate", "$sce",
            function ($rootScope, NgTableParams, $http, $compile, $interpolate, $sce) {
                return {
                    restrict: "E",
                    scope: {
                        columnInfo: '=gridObj',
                        isExport: '@isExport',
                        title: '@title',
                        isAdd: '=',
                        isColumns: '=',
                        mode: '@mode',
                        onrowdblclick: '&onrowdblclick',
                        modeType: '@mode',
                        setFn: '&',
                        onaddnew: '&onaddnew',
                        fixedclause: '@fixedclause',
                        onrowclick: '&onrowclick'
                    },
                    link: function (scope, element, attrs) {

                        scope.updateMap = function () {
                            scope.getInformation();
                        }

                        scope.dateOptions = {
                            formatYear: 'yy',
                            //minDate: new Date(2016, 8, 5),
                            //maxDate: new Date(2017, 5, 22),
                            startingDay: 1
                        };
                        scope.dateFormat = "yyyy-MM-dd";
                        scope.setFn({ theDirFn: scope.updateMap });
                        scope.getInformation = function () {
                            scope.tableParams = new NgTableParams({
                                page: 1,
                                count: 10
                            }, {
                                total: 0,
                                filterDelay: 750,
                                getData: function ($defer, params) {
                                    // START
                                    var setSearch = {};
                                    $.each(params.filter(), function (index, value) {
                                        var getSearch = {};
                                        if (value != null) {
                                            if (typeof (value) === 'object') {
                                                if (isDate(value)) {
                                                    getSearch = ({ [index]: value });
                                                }
                                                else {
                                                    getSearch = ({ [index]: value.Display });
                                                }
                                            } else {
                                                getSearch = ({ [index]: value });
                                            }
                                        }
                                        $.extend(setSearch, getSearch);
                                    });
                                    // END
                                    var customPara = {
                                        Sort: params.sorting(),
                                        Filter: JSON.stringify(setSearch),
                                        FixClause: scope.fixedclause,
                                        PageNumber: params.page(),
                                        RecordPerPage: params.count(),
                                        Mode: scope.modeType
                                    }
                                    //   //console.log(customPara);
                                    $http({
                                        method: 'post',
                                        url: '/Home/getData',
                                        contentType: 'application/json; charset=utf-8',
                                        data: customPara,
                                        responseType: 'json',
                                    }).then(function (result) {
                                        // //console.log(result);
                                        $defer.resolve(scope.documents = result.data.data);
                                        params.total(result.data.recordsTotal);
                                    }, function (response) {
                                        //  //console.log("Some thing wrong");
                                    });
                                    function isDate(val) {
                                        var d = new Date(val);
                                        return !isNaN(d.valueOf());
                                    }
                                }
                            });
                        };
                        scope.RowDblClick = function (row) {
                            ////console.log(word);
                            scope.onrowdblclick({ value: row });
                        }
                        scope.RowClick = function (row) {
                            debugger
                            angular.forEach(scope.documents, function (obj, index) {
                                if (obj["selected"]) {
                                    obj["selected"] = false;
                                }
                            });
                            row["selected"] = true;

                            //scope.onrowclick({ value: row });
                        }
                        scope.AddNew = function () {
                            scope.onaddnew();
                        }

                        scope.Export = function (type) {
                            function isDate(val) {
                                var d = new Date(val);
                                return !isNaN(d.valueOf());
                            }
                            $("#LoadingMainImg").show();
                            var params = this.tableParams;
                            debugger;
                            var columns = "";
                            for (var a = 0; a < scope.columnInfo.columnsInfo.length; a++) {
                                if (scope.columnInfo.columnsInfo[a].show == true || scope.columnInfo.columnsInfo[a].visible == true) {
                                    columns += columns == "" ? scope.columnInfo.columnsInfo[a].field + " as [" + scope.columnInfo.columnsInfo[a].title + "]" : "," + scope.columnInfo.columnsInfo[a].field + " as [" + scope.columnInfo.columnsInfo[a].title + "]";
                                }
                            }
                            // START
                            var setSearch = {};
                            $.each(params.filter(), function (index, value) {
                                var getSearch = {};
                                if (value != null) {
                                    if (typeof (value) === 'object') {
                                        if (isDate(value)) {
                                            getSearch = ({ [index]: value });
                                        }
                                        else {
                                            getSearch = ({ [index]: value.Display });
                                        }
                                    } else {
                                        getSearch = ({ [index]: value });
                                    }
                                }
                                $.extend(setSearch, getSearch);
                            });
                            // END
                            var customPara = {
                                Sort: params.sorting(),
                                Filter: JSON.stringify(setSearch),
                                FixClause: (scope.fixedclause === undefined || scope.fixedclause == null || scope.fixedclause == '') ? '' : scope.fixedclause,
                                PageNumber: params.page(),
                                RecordPerPage: params.count(),
                                Mode: this.modeType,
                                Columns: columns
                            }
                            var winName = 'MyWindow';
                            var winURL = '/ExportData.aspx';
                            var windowoption = 'resizable=yes,height=600,width=800,location=0,menubar=0,scrollbars=1';
                            //  var params = { 'param1': '1', 'param2': '2' };

                            var form = document.createElement("form");
                            form.setAttribute("method", "post");
                            form.setAttribute("action", winURL);
                            form.setAttribute("target", winName);

                            var ColumnInput = document.createElement("input");
                            ColumnInput.type = 'hidden';
                            ColumnInput.name = "Columns";
                            ColumnInput.value = customPara.Columns;
                            form.appendChild(ColumnInput);

                            var ModeInput = document.createElement('input');
                            ModeInput.type = 'hidden';
                            ModeInput.name = "Mode";
                            ModeInput.value = customPara.Mode;
                            form.appendChild(ModeInput);

                            var FilterInput = document.createElement('input');
                            FilterInput.type = 'hidden';
                            FilterInput.name = "Filter";
                            FilterInput.value = customPara.Filter;
                            form.appendChild(FilterInput);

                            var FixClauseInput = document.createElement('input');
                            FixClauseInput.type = 'hidden';
                            FixClauseInput.name = "FixClause";
                            FixClauseInput.value = customPara.FixClause;
                            form.appendChild(FixClauseInput);

                            var SortInput = document.createElement('input');
                            SortInput.type = 'hidden';
                            SortInput.name = "SortColumn";
                            SortInput.value = Object.keys(customPara.Sort).length == 0 ? "" : Object.keys(customPara.Sort)[0];
                            form.appendChild(SortInput);

                            var SortOrderInput = document.createElement('input');
                            SortOrderInput.type = 'hidden';
                            SortOrderInput.name = "SortOrder";
                            SortOrderInput.value = Object.keys(customPara.Sort).length == 0 ? "asc" : customPara.Sort[Object.keys(customPara.Sort)[0]];
                            form.appendChild(SortOrderInput);

                            var TypeInput = document.createElement('input');
                            TypeInput.type = 'hidden';
                            TypeInput.name = "type";
                            TypeInput.value = type;
                            form.appendChild(TypeInput);

                            document.body.appendChild(form);
                            // window.open('', winName, windowoption);
                            form.target = "exportFram";//winName;
                            form.submit();
                            document.body.removeChild(form);
                            $("#LoadingMainImg").hide();
                        }
                        scope.ConvertDate = function (data, format) {
                            if (data == null) return '';
                            var r = /\/Date\(([0-9]+)\)\//gi
                            var matches = data.match(r);
                            if (matches == null) return '';
                            var result = matches.toString().substring(6, 19);
                            var epochMilliseconds = result.replace(
                                /^\/Date\(([0-9]+)([+-][0-9]{4})?\)\/$/,
                                '$1');
                            var b = new Date(parseInt(epochMilliseconds));
                            var c = new Date(b.toString());
                            var curr_date = c.getDate();
                            var curr_month = c.getMonth() + 1;
                            var curr_year = c.getFullYear();
                            var curr_h = c.getHours();
                            var curr_m = c.getMinutes();
                            var curr_s = c.getSeconds();
                            var curr_offset = c.getTimezoneOffset() / 60
                            //var d = curr_month.toString() + '/' + curr_date + '/' + curr_year;
                            //return d;
                            return format.replace('mm', curr_month).replace('dd', curr_date).replace('yyyy', curr_year);
                        }

                        scope.LimitString = function (content, maxCharacters) {
                            if (content == null) return "";
                            content = "" + content;
                            content = content.trim();
                            if (content.length <= maxCharacters) return content;
                            content = content.substring(0, maxCharacters);
                            var lastSpace = content.lastIndexOf(" ");
                            if (lastSpace > -1) content = content.substr(0, lastSpace);
                            return content + '...';
                        }

                        scope.ConvertTime = function (Time) {
                            if (Time == null)
                                return "";
                            var d = ('0' + Time.Hours).slice(-2) + ':' + ('0' + Time.Minutes).slice(-2);
                            if (d == '00:00') {
                                return '';
                            } else {
                                return d;
                            }
                        }
                        scope.getHtml = function (html) {
                            return $sce.trustAsHtml(html);
                            // return $compile($sce.trustAsHtml(html))(scope);
                        }

                        scope.getInformation()
                        scope.renderGrid = function (scope) {
                            debugger;
                            var tempText = '';
                            if (scope.isExport != "false") {
                                tempText += '<iframe id="exportFram" name="exportFram" width="0" height="0" src="/ExportData.aspx" style="visibility: hidden;display:none;"></iframe>';
                            }
                            tempText += '<div class="row">'
                                + '<div class="col-md-4">'
                                + '<h4><strong>' + scope.title + '</strong>'
                                + '</h4></div>';
                            if (scope.isExport != "false") {
                                tempText += '<div class="col-md-8 text-right">'
                                    + '<ul class="list-inline">'
                                    + '<a ng-click="Export(\'pdf\')" data-uib-tooltip="Export to PDF" class="btn btn-primary btn-sm xs-mr-5"><i class="fa fa-file-pdf-o"></i> PDF</a>'
                                    + ' <a ng-click="Export(\'word\')" data-uib-tooltip="Export to Word" class="btn btn-primary btn-sm xs-mr-5"><i class="fa fa-file-word-o"></i> Word</a>'
                                    + '<a ng-click="Export(\'excel\')" data-uib-tooltip="Export to Excel" class="btn btn-primary btn-sm xs-mr-5"><i class="fa fa-file-excel-o"></i> Excel</a>'
                                    + '<a ng-click="Export(\'csv\')" data-uib-tooltip="Export to CSV" class="btn btn-primary btn-sm xs-mr-5"><i class="fa fa-file-text-o"></i> CSV</a>';
                                if (scope.isAdd == false) {
                                    //alert('false')
                                }
                                else {
                                    tempText += '<button type="button" class="btn btn-sm btn-info xs-mr-5" data-ng-click="AddNew()" tabindex="1"><i class="fa fa-plus"></i> Add New </button>'
                                }
                                if (scope.isColumns == false) { }
                                else {
                                    tempText += '<div class="dropdown" style="display: inline-block;" ><button id="dLabel" type="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="true" class="btn btn-sm btn-primary xs-mr-5" data-uib-tooltip="Show/Hide Columns"><i class="fa fa-columns"></i> Columns</button>'
                                        + '<ul class="dropdown-menu" aria-labelledby="dLabel" style="margin-left:-160px;padding: 5px;min-width:200px;max-width:240px;max-height: 400px;overflow-y: scroll;border: 1px solid grey;"><label data-ng-repeat="col in columnInfo.columnsInfo" style="padding: 5px;width:100%;"><input type="checkbox" ng-change="SetColumnVisibility(col,$index)" ng-model="col.show"/> {{col.title}}</label></ul></div>'
                                }
                                tempText += '</ul>'
                                    + '</div>';
                            }
                            tempText += '</div>'
                                + '<div class="row">'
                                + '<div class="col-sm-12">'
                                + '<div class="box">'
                                + '<div class="box-body table-responsive">'
                                + '<table ng-table-dynamic="tableParams with columnInfo.columnsInfo" show-filter="true"'
                                + 'class="table table-condensed table-bordered table-striped">'
                            var trtempText = '';
                            if (scope.onrowdblclick) {
                                trtempText = '<tr data-ng-repeat="row in $data" ng-click="RowClick(row)"  ng-dblclick="RowDblClick(row)">'
                                trtempText += ' <td ng-repeat="col in columnInfo.columnsInfo" ><div ng-if="col.cellTemplte" ng-bind-html="col.cellTemplte(this,row)" compile-template></div><span ng-if="col.cellTemplte == undifined">{{row[col.field]}}</span></td>'

                                    + '<td><button ng-click="expandRowFun(row)"><span ng-if="row[columnInfo.columnsInfo[columnInfo.columnsInfo.length]]"><i class="fa fa-minus"></i></span><span ng-if="!Inq.expandedGrid"><i class="fa fa-plus"></i></span></button></td>'

                                    + ' </tr>'
                            }
                            else {
                                trtempText = '<tr ng-repeat="row in $data">'
                                trtempText += ' <td ng-repeat="col in columnInfo.columnsInfo" ><div ng-if="col.cellTemplte" ng-bind-html="col.cellTemplte(this,row)" compile-template></div><span ng-if="col.cellTemplte == undifined">{{row[col.field]}}</span></td>'

                                    + '<td><button ng-click="expandRowFun(Inq.expandedGrid,Inq.InqId)"><span ng-if="Inq.expandedGrid"><i class="fa fa-minus"></i></span><span ng-if="!Inq.expandedGrid"><i class="fa fa-plus"></i></span></button></td>'

                                    + ' </tr>'
                            }
                            //+ ' <tr ng-if="row[col.field]">'
                            //+ ' <div ng-if="col.cellTemplte" ng-bind-html="col.cellTemplte(this,row)" compile-template></div><span ng-if="col.cellTemplte == undifined">{{row[col.field]}}</span>'
                            //+ ' </tr>'

                            + ' </table>'
                            tempText += ' <span>Disply {{tableParams.data.length}} of {{tableParams.total()}} Rows</span>';
                            tempText += '</div>'
                                + '</div>'
                                + '</div>'
                                + '</div>';
                            element.replaceWith($compile(tempText)(scope));
                        }

                        scope.renderGrid(scope);
                        scope.SetColumnVisibility = function (data, i) {
                            scope.columnInfo.columnsInfo[i] = data;
                            scope.renderGrid(scope);
                        }
                    }

                }
            }]);

})()