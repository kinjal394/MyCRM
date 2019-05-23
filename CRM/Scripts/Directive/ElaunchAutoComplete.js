(function () {
    'use strict';

    // return dasherized from  underscored/camelcased string
    var dasherize = function (string) {
        return string.replace(/_/g, '-').
          replace(/([a-z])([A-Z])/g, function (_, $1, $2) {
              return $1 + '-' + $2.toLowerCase();
          });
    };

    // accepted attributes
    var autoCompleteAttrs = [
      'placeholder', 'listFormatter', 'prefillFunc',
      //'ngModel', 'valueChanged', 'source', 'pathToData', 'minChars',
      'ngModel', 'valueChanged', 'minChars', 'mode',
      'defaultStyle', 'valueProperty', 'displayProperty'
    ];

    var TestScope = function (data) {
        console.log(data);
    }

    // build autocomplet-div tag with input and select
    var buildACDiv = function (attrs) {
        var acDiv = document.createElement('auto-complete-div');
        // var acDiv = document.createElement('input');
        //var inputEl = document.createElement('input');
        //attrs.ngDisabled &&
        //  inputEl.setAttribute('ng-disabled', attrs.ngDisabled);
        //acDiv.appendChild(inputEl);

        var ulEl = document.createElement('ul');
        ulEl.style.borderTop = 'none';
        ulEl.style.borderRadius = '0 0 6px 6px';
        acDiv.appendChild(ulEl);

        autoCompleteAttrs.map(function (attr) {
            attrs[attr] && acDiv.setAttribute(dasherize(attr), attrs[attr]);
        });
        acDiv.style.position = 'absolute';
        acDiv.style.top = '34px';
        acDiv.style.left = 0;
        acDiv.style.display = 'none';
        acDiv.style.width = '100%';
        acDiv.style.maxHeight = '279px';
        acDiv.style.overflowY = 'auto';
        acDiv.style.borderTop = 'none';
        acDiv.style.borderRadius = '0px 0px 6px 6px';
        acDiv.style.borderRight = '1px solid #ccc';
        acDiv.style.borderBottom = '1px solid #ccc';
        acDiv.style.zIndex = 9;
        return acDiv;
    };

    var compileFunc = function (element, attrs) {
        element[0].style.position = 'relative';
        var controlEl = element[0].querySelector('input, select');

        attrs.valueProperty = attrs.valueProperty || 'ID';
        attrs.displayProperty = attrs.displayProperty || 'Name';
        //attrs.ngModel = controlEl.getAttribute('ng-model');
        // attrs.ngModel = attrs.ngModel;
        if (controlEl.tagName == 'SELECT') {
            var placeholderEl = document.createElement('div');
            placeholderEl.className = 'select-placeholder';
            element[0].appendChild(placeholderEl);
        }
        // controlEl.value = attrs.ngModel;
        var acDiv = buildACDiv(attrs);
        acDiv.id = attrs.id;
        $(element[0]).find(".input-group").append(acDiv);
        var btn = element.find('#btnAddNew')[0];
        btn.addEventListener('click', function () {
            OpenPopup(attrs, element, acDiv.id);
        }, false);
        controlEl.addEventListener('click', function () {
            console.log("change event");
        });
       
    }; // compileFunc

    var OpenPopup = function (attrs, element, cntrlId) {
        debugger
        var val = 'element.scope()';
        var relVal = '';
        if (attrs.relatedValue) {
            var results = attrs.relatedValue.split('.');
            if (results.length == 7) {
                val= eval(element.scope()["$ctrl"]["productData"]["ProductData"]["Value"]);
                val += '|';
                val += eval(element.scope()["$ctrl"]["productData"]["SupplierModelData"]["Value"]);
                relVal = val;
            }
            else {
                for (var i = 0; i < results.length; i++)
                    val += '["' + results[i] + '"]';
                relVal = eval(val);
            }
        }
        if (document.getElementById("myPopupModal"))
            document.getElementById("myPopupModal").remove();
        var abc = "<div id='myPopupModal' class='modal'><div class='modal-dialog modal-lg'><div class='modal-content'><div class='modal-header'><button type= 'button' class='close' data-dismiss='modal' data-ng-click='$ctrl.ok()' >&times;</button><h3 class='modal-title' id='modal-title'>Select From the List</h3></div ><div class='modal-body' style='padding:0 !important;'><div class='row'><div class='col-sm-12'><div id='myModalContent'><iframe style='border: none;' width='100%' height='450' src='/Demo/DemoPopup?Mode=" + attrs.mode + "&RelatedValue=" + relVal + "&cntrlId=" + cntrlId + "'></iframe></div></div></div></div><div class='modal-footer'><button type='submit' class='btn btn-default btn-info btn-icon icon-left' data-ng-if='$ctrl.signatureData.SignatureId <= 0 && isClicked == false' data-ng-click='$ctrl.signatureData.UserName.Display != '' && $ctrl.signatureData.Signature != '' && signatureInfo.$valid && $ctrl.SaveSignature()'><i class='fa fa-save'></i>Select</button><button type='button' class='btn btn-default' data-dismiss='modal' data-ng-click='$ctrl.ok()'>Close</button></div></div></div></div>";
        $("body").append(abc);
        var options = { "backdrop": "static", keyboard: true };
        $('#myPopupModal').modal('show');
    }

    var closePopup = function () {
        if (document.getElementById("myPopupModal"))
            document.getElementById("myPopupModal").remove();
    }

    angular.module('CRMApp.Directives').
       directive('elaunchAutocomplete', function () {
           return {
               restrict: "E",
               scope: {
                   ngModel: '=',
                   // source: '=',
                   mode: '@',
                   minChars: '=',
                   multiple: '=',
                   // ngModel: '=',
                   defaultStyle: '=',
                   listFormatter: '=',
                   // pathToData: '@',
                   valueProperty: '@',
                   displayProperty: '@',
                   placeholder: '@',
                   prefillFunc: '&',
                   valueChanged: '&',
                   relatedValue: '=',
                   id: '@',
                   isdisplay: '='
               },
               compile: compileFunc,
               template: '<div class="input-group"><input id="AutoComplete" ng-disabled="!isdisplay" autocomplete="off" ng-model="ngModel.Display" class="form-control" value="{{ngModel.Display}}" style="background: #fff;" /><div class="input-group-btn"><button type="button" id="btnAddNew" ng-disabled="!isdisplay" class="btn btn-default"><i class="glyphicon glyphicon-search"></i></button></div><input type="hidden" id="hdnVal" ng-model="ngModel.Value" value="{{ngModel.Value}}" /></div>',
           };
       });
})();

(function () {
    'use strict';
    var $compile;

    // return dasherized from  underscored/camelcased string
    var dasherize = function (string) {
        return string.replace(/_/g, '-').
          replace(/([a-z])([A-Z])/g, function (_, $1, $2) {
              return $1 + '-' + $2.toLowerCase();
          });
    };

    // accepted attributes
    var autoCompleteAttrs = [
      'placeholder', 'multiple', 'listFormatter', 'prefillFunc',
      //'ngModel', 'valueChanged', 'source', 'pathToData', 'minChars',
       'ngModel', 'valueChanged', 'minChars', 'mode',
      'defaultStyle', 'valueProperty', 'displayProperty'
    ];

    // build autocomplet-div tag with input and select
    var buildACDiv = function (attrs) {
        var acDiv = document.createElement('auto-complete-div');

        var inputEl = document.createElement('input');
        attrs.placeholder = attrs.placeholder || 'Select';
        inputEl.setAttribute('placeholder', attrs.placeholder);
        inputEl.setAttribute('size', attrs.placeholder.length);

        attrs.ngDisabled &&
          inputEl.setAttribute('ng-disabled', attrs.ngDisabled);
        acDiv.appendChild(inputEl);

        var ulEl = document.createElement('ul');
        acDiv.appendChild(ulEl);

        autoCompleteAttrs.map(function (acAttr) {
            if (attrs[acAttr]) {
                var attrValue = attrs[acAttr];
                acDiv.setAttribute(dasherize(acAttr), attrValue);
            }
        });
        acDiv.style.position = 'relative';
        //acDiv.style.display = 'none';
        return acDiv;
    };

    var buildMultiACDiv = function (attrs) {
        var deleteLink = document.createElement('button');
        deleteLink.innerHTML = 'x';
        deleteLink.className += ' delete';
        deleteLink.setAttribute('ng-click', attrs.ngModel + '.splice($index, 1); $event.stopPropagation()');

        var ngRepeatDiv = document.createElement('span');
        ngRepeatDiv.className += ' auto-complete-repeat';
        ngRepeatDiv.setAttribute('ng-repeat', 'obj in ' + attrs.ngModel + ' track by $index');
        if (attrs.listFormatter) {
            ngRepeatDiv.innerHTML = '<span ng-bind-html="listFormatter(obj)"></span>';
        } else {
            ngRepeatDiv.innerHTML = '<b>({{obj.' + attrs.valueProperty + '}})</b>' +
              '<span>{{obj.' + attrs.displayProperty + '}}</span>';
        }
        ngRepeatDiv.appendChild(deleteLink);

        var multiACDiv = document.createElement('div');
        multiACDiv.className = 'auto-complete-div-multi-wrapper';
        multiACDiv.appendChild(ngRepeatDiv);

        return multiACDiv;
    };

    var compileFunc = function (element, attrs) {
        element[0].style.position = 'relative';

        var controlEl = element[0].querySelector('select');
        controlEl.style.display = 'none';

        attrs.valueProperty = attrs.valueProperty || 'ID';
        attrs.displayProperty = attrs.displayProperty || 'Name';
        // attrs.ngModel = controlEl.getAttribute('ng-model');
        attrs.multiple = true;

        // 1. build <auto-complete-div>
        var multiACDiv = buildMultiACDiv(attrs);
        var acDiv = buildACDiv(attrs);
        multiACDiv.appendChild(acDiv);
        element[0].appendChild(multiACDiv);

    }; // compileFunc

    angular.module('CRMApp.Directives').
      directive('autoCompleteMulti', ['$compile', function (_$compile_) {
          $compile = _$compile_;
          return { compile: compileFunc };
      }]);
})();

(function () {
    'use strict';
    var $timeout, $filter, AutoComplete;

    var showLoading = function (ulEl, show) {
        if (show) {
            ulEl.innerHTML = '<li class="loading"> Loading </li>';
        } else {
            var loadingNode = ulEl.querySelector('li.loading');
            if (loadingNode) { ulEl.removeChild(loadingNode); }
        }
    };

    var defaultListFormatter = function (obj, scope) {
        //return '<b>('+obj[scope.valueProperty]+')</b>' +
        //  '<span>'+obj[scope.displayProperty]+'</span>';
        return '<span>' + obj["Name"] + '</span>';
    };

    var addListElements = function (scope, data) {
        var ulEl = scope.ulEl;
        var getLiEl = function (el) {
            var viewValue = typeof el == 'object' ? el[scope.displayProperty] : el;
            var modelValue = typeof el == 'object' ? el[scope.valueProperty] : el;
            var liEl = document.createElement('li');
            if (scope.listFormatter && typeof el == 'object') {
                liEl.innerHTML = scope.listFormatter(el);
            } else if (typeof el == 'object') {
                liEl.innerHTML = defaultListFormatter(el, scope);
            } else {
                liEl.innerHTML = viewValue;
            }
            liEl.model = el;
            liEl.modelValue = modelValue;
            liEl.viewValue = viewValue;
            return liEl;
        };
        if (scope.placeholder &&
            !scope.multiple &&
            scope.controlEl.tagName == 'SELECT') {
            ulEl.appendChild(getLiEl(scope.placeholder));
        }
        data.forEach(function (el) {
            ulEl.appendChild(getLiEl(el));
        });
    };

    var delay = (function () {
        var timer = 0;
        return function (callback, ms) {
            $timeout.cancel(timer);
            timer = $timeout(callback, ms);
        };
    })();

    var loadList1 = function (scope) {
        //console.log(scope);
    }

    var loadList = function (scope) {
        var inputEl = scope.inputEl, ulEl = scope.ulEl;
        while (ulEl.firstChild) {
            ulEl.removeChild(ulEl.firstChild);
        }
        //if (scope.source.constructor.name == 'Array') { // local source
        //  var filteredData = $filter('filter')(scope.source, inputEl.value);
        //  ulEl.style.display = 'block';
        //  addListElements(scope, filteredData);
        //} else { // remote source
        ulEl.style.display = 'none';
        if (inputEl.value.length >= (scope.minChars || 0)) {
            ulEl.style.display = 'block';
            showLoading(ulEl, true);
            //var query = { keyword: inputEl.value, Mode: scope.mode, RelatedValue: scope.$parent.relatedValue ? scope.$parent.relatedValue : '' };
            var query = { keyword: inputEl.value, Mode: $("#" + scope.containerEl.id + "").attr("mode"), RelatedValue: scope.$parent.relatedValue ? scope.$parent.relatedValue : '' };
            if (scope.ngModel) query.ngModel = scope.ngModel;
            AutoComplete.getRemoteData(
              //scope.source, query, scope.pathToData).then(
              '/Home/GetAutoCompleteData', query, '').then(
              function (data) {
                  showLoading(ulEl, false);
                  addListElements(scope, data);
                  if (data.length == 0) {
                      scope.inputEl.style.color = 'red'
                      scope.inputEl.style.border = '1px solid red'
                      scope.isContainData = false;
                  } else {
                      scope.inputEl.style.color = 'black'
                      scope.inputEl.style.border = '1px solid #ccc';
                  }
                  scope.containerEl.style.display = 'block';
                  $(ulEl).parent().find('li:first-child').css("background", "#ccc");
              }, function () {
                  showLoading(ulEl, false);
              });
        } // if
        //} // else remote source
    };

    var hideAutoselect = function (scope) {
        var elToHide = scope.multiple ? scope.ulEl : scope.containerEl;
        elToHide.style.display = 'none';
    };

    var focusInputEl = function (scope) {
        scope.ulEl.style.display = 'block';
        scope.inputEl.focus();
        //scope.inputEl.value = '';
        loadList(scope);
    };

    var inputElKeyHandler = function (scope, evt) {
        var selected = scope.ulEl.querySelector('.selected');
        switch (evt.keyCode) {
            case 27: // ESC
                selected.className = '';
                hideAutoselect(scope);
                break;
            case 38: // UP
                if (selected.previousSibling) {
                    selected.className = '';
                    selected.previousSibling.className = 'selected';
                }
                break;
            case 40: // DOWN
                scope.ulEl.style.display = 'block';
                if (selected && selected.nextSibling) {
                    selected.className = '';
                    selected.nextSibling.className = 'selected';
                } else if (!selected) {
                    scope.ulEl.firstChild.className = 'selected';
                }
                break;
            case 13: // ENTER
                selected && scope.select(selected);
                evt.preventDefault();
                break;
            case 9: // Tab
                selected && scope.select(selected);
                evt.preventDefault();
                break;
            case 8: // BACKSPACE
                // remove the last element for multiple and empty input
                if (scope.multiple && scope.inputEl.value === '') {
                    $timeout(function () {
                        scope.ngModel.pop();
                    });
                }
        }
    };

    var linkFunc = function (scope, element, attrs) {
        //console.log(scope);
        var inputEl, ulEl, containerEl;

        scope.containerEl = containerEl = element[0];
        // scope.inputEl = inputEl = element[0].querySelector('input');
        scope.inputEl = inputEl = element.parent().find('input')[0];
        scope.ulEl = ulEl = element[0].querySelector('ul');

        var parentEl, controlEl, placeholderEl;
        if (scope.multiple) {
            parentEl = element[0].parentElement.parentElement; //acDiv->wrapper->acMulti
            scope.controlEl = controlEl = parentEl.querySelector('select');
        } else {
            parentEl = element[0].parentElement;
            scope.controlEl = controlEl = parentEl.querySelector('input, select');
            placeholderEl = parentEl.querySelector('.select-placeholder');
        }

        if (controlEl && !scope.multiple) {
            //controlEl.readOnly = true;

            if (controlEl.tagName == 'SELECT') {

                var controlBCR = controlEl.getBoundingClientRect();
                placeholderEl.style.lineHeight = controlBCR.height + 'px';

                if (scope.prefillFunc) {
                    scope.prefillFunc().then(function (html) {
                        placeholderEl.innerHTML = html;
                    });
                }

                if (attrs.ngModel) {
                    scope.$parent.$watch(attrs.ngModel, function (val) {
                        !val && (placeholderEl.innerHTML = attrs.placeholder);
                    });

                }

                controlEl.addEventListener('mouseover', function () {
                    for (var i = 0; i < controlEl.children.length; i++) {
                        controlEl.children[i].style.display = 'none';
                    }
                });
                controlEl.addEventListener('mouseout', function () {
                    for (var i = 0; i < controlEl.children.length; i++) {
                        controlEl.children[i].style.display = '';
                    }
                });

                controlEl.addEventListener('keydown', function () {
                   alert('dfd')
                });

            }

            controlEl.addEventListener('click', function () {
                if (!controlEl.disabled) {
                    containerEl.style.display = 'block';
                    var controlBCR = controlEl.getBoundingClientRect();
                    containerEl.style.width = controlBCR.width + 'px';
                    // inputEl.style.width = (controlBCR.width - 30) + 'px';
                    inputEl.style.height = controlBCR.height + 'px';
                    inputEl.focus();
                }
            });

        } else if (scope.multiple) {

            scope.prefillFunc && scope.prefillFunc();

            parentEl.addEventListener('click', function () {
                if (controlEl) {
                    inputEl.disabled = controlEl.disabled;
                    if (!controlEl.disabled) {
                        containerEl.style.display = 'inline-block';
                        inputEl.focus();
                    }
                }
            });
        }

        // add default class css to head tag
        if (scope.defaultStyle !== false) {
            containerEl.className += ' default-style';
            AutoComplete.injectDefaultStyle();
        }

        scope.select = function (liEl) {
            liEl.className = '';
            hideAutoselect(scope);
            $timeout(function () {
                if (attrs.ngModel) {
                    if (scope.multiple) {
                        if (!scope.ngModel) {
                            scope.ngModel = [];
                        }
                        scope.ngModel.push(liEl.model);
                    } else if (controlEl) {
                        if (controlEl.tagName == 'INPUT') {
                            scope.ngModel = $(liEl).text();
                            scope.$parent.ngModel = { Display: $(liEl).text(), Value: $(liEl).context.modelValue }
                        } else if (controlEl.tagName == 'SELECT') {
                            scope.ngModel = liEl.modelValue;

                            if (scope.listFormatter && typeof liEl.model == 'object') {
                                placeholderEl.innerHTML = scope.listFormatter(liEl.model);
                            } else {
                                placeholderEl.innerHTML = liEl.viewValue;
                            }
                        }
                    } else {
                        scope.ngModel = liEl.modelValue;
                    }
                }

                scope.controlEl.value = scope.ngModel;
                scope.valueChanged({ value: liEl.model }); //user scope
            });
        };

        inputEl.addEventListener('focus', function () {
            if (controlEl) {
                !controlEl.disabled && focusInputEl(scope);
            } else {
                focusInputEl(scope);
            }
        });

        inputEl.addEventListener('blur', function () {
            hideAutoselect(scope);
            //var mode = scope.mode;
            //var val = scope.inputEl.value;
            //var query = { keyword: scope.inputEl.value, Mode: scope.mode, RelatedValue: scope.$parent.relatedValue ? scope.$parent.relatedValue : '' };
            //AutoComplete.getRemoteData(
            ////scope.source, query, scope.pathToData).then(
            //'/Home/CheckAutoComplete', query, '').then(
            //function (data) {
            //    if (data.length > 0) {
            //        scope.ngModel = { Display: val, Value: '' }
            //    }
            //    else {
            //        scope.ngModel = { Display: val, Value: '' }
            //    }
            //}, function () {
            //    showLoading(ulEl, false);
            //});
        }); // hide list

        inputEl.addEventListener('keydown', function (e) {
            var li = $(this).parent().find('li');
            var cur;
            $(li).each(function (k,v) {
                if ($(v).hasClass("active")) {
                    cur = $(v);
                    $(v).removeClass("active");
                }
                $(v).css("background","#fff");
            });
            if ($(cur).length === 0) {
                cur = $(li).first();
                //cur.css("background", "#ccc");
            }
            if (e.keyCode == 40) {
                $(cur).next().addClass("active");
                $(cur).next().css("background", "#ccc");
                return false;
            } else if (e.keyCode == 38) {
                $(cur).prev().addClass("active");
                $(cur).prev().css("background", "#ccc");
                return false;
            }
            if (e.keyCode == 13) {
                if ($("auto-complete-div").is(":visible")) {
                    scope.select(cur);
                    e.preventDefault();
                    return false;
                }
            }
            if (e.keyCode == 9) {
                if ($("auto-complete-div").is(":visible")) {
                    scope.select(cur);
                    e.preventDefault();
                    return false;
                }
            }
        });
        

        ulEl.addEventListener('mousedown', function (evt) {
            if (evt.target !== ulEl) {
                var liTag = evt.target;
                while (liTag.tagName !== 'LI') {
                    liTag = liTag.parentElement;
                }

                // Select only if it is a <li></li> and the class is not 'loading'
                if (liTag.tagName == 'LI' && liTag.className != "loading") {
                    scope.select(liTag);
                }
            }
        });


        /** when enters text to search, reload the list */
        inputEl.addEventListener('input', function () {
            //var delayMs = scope.source.constructor.name == 'Array' ? 10 : 500;
            var delayMs = 500;
            delay(function () { //executing after user stopped typing
                loadList(scope);
            }, delayMs);

            if (scope.multiple) {
                var placeholderLength = 0;
                if (attrs.placeholder) {
                    placeholderLength = attrs.placeholder.length;
                }
                var newSize = Math.max(placeholderLength, inputEl.value.length + 1);
                inputEl.setAttribute('size', newSize);
            }
        });

    };

    var autoCompleteDiv =
      function (_$timeout_, _$filter_, _AutoComplete_) {
          $timeout = _$timeout_;
          $filter = _$filter_;
          AutoComplete = _AutoComplete_;

          return {
              restrict: 'E',
              scope: {
                  ngModel: '=',
                  // source: '=',
                  mode: '@',
                  minChars: '=',
                  multiple: '=',
                  //ngModel: '=',
                  defaultStyle: '=',
                  listFormatter: '=',
                  // pathToData: '@',
                  valueProperty: '@',
                  displayProperty: '@',
                  placeholder: '@',
                  prefillFunc: '&',
                  valueChanged: '&'
              },
              link: linkFunc
          };
      };
    autoCompleteDiv.$inject = ['$timeout', '$filter', 'AutoComplete'];

    angular.module('CRMApp.Directives').
      directive('autoCompleteDiv', autoCompleteDiv);
})();

(function () {
    'use strict';
    var $q, $http;

    var defaultStyle =
      'div[auto-complete] select ~ div.select-placeholder {' +
      '  position: absolute; ' +
      '  padding-left: 12px;' +
      '  top: 0;' +
      '  left: 0;' +
      '  pointer-events: none;' +
      '}' +

      'auto-complete-div.default-style input {' +
      '  outline: none; ' +
      '  border: 2px solid transparent;' +
      '  border-width: 3px 2px;' +
      '  margin: 0;' +
      '  box-sizing: border-box;' +
      '  background-clip: content-box;' +
      '}' +

      'select ~ auto-complete-div.default-style input {' +
      '  border-width: 3px 3px;' +
      '}' +

      'auto-complete-div.default-style ul {' +
      '  background-color: #fff;' +
      '  margin-top: 2px;' +
      '  display : none;' +
      '  width : 100%;' +
      '  overflow-y: auto;' +
      '  list-style-type: none;' +
      '  margin: 0;' +
      '  padding: 0;' +
      '  border: 1px solid #ccc;' +
      '  box-sizing: border-box;' +
      '}' +

      'auto-complete-div.default-style ul li {' +
      '  padding: 5px 5px;' +
      '  border-bottom: 1px solid #eee;' +
      '}' +

      'auto-complete-div.default-style ul li.selected {' +
      '  background-color: #ccc;' +
      '}' +

      'auto-complete-div.default-style ul li:last-child {' +
      '  border-bottom: none;' +
      '}' +

      'auto-complete-div.default-style ul li:hover {' +
      '  background-color: #ccc;' +
      '}' +

      'div .auto-complete-repeat {' +
      '  display: inline-block;' +
      '  padding: 3px; ' +
      '  background : #fff;' +
      '  margin: 3px;' +
      '  border: 1px solid #ccc;' +
      '  border-radius: 5px;' +
      '}' +

      'div .auto-complete-repeat .delete {' +
      '  margin: 0 3px;' +
      '  color: red;' +
      '  border: none;' +
      '  background-color: transparent; ' +
      '}' +

      'div .auto-complete-repeat .delete[disabled] {' +
      '  display: none;' +
      '}' +

      '.auto-complete-div-multi-wrapper {' +
      '  background-color: #ddd;' +
      '  min-height: 2em;' +
      '}' +

      '.auto-complete-div-multi-wrapper auto-complete-div.default-style {' +
      '  position: relative;' +
      '  display: inline-block;' +
      '  margin: 3px;' +
      '  padding: 3px;' +
      '}' +

      '.auto-complete-div-multi-wrapper auto-complete-div.default-style input {' +
      '  background: transparent;' +
      '  border-radius: 0;' +
      '  border: none;' +
      '}' +

      '.auto-complete-div-multi-wrapper auto-complete-div.default-style ul {' +
      '  position: absolute;' +
      '  top: 1.5em;' +
      '  left: 0;' +
      '  width: auto;' +
      '  min-width: 10em;' +
      '}' +

      '';

    // return dasherized from  underscored/camelcased string
    var dasherize = function (string) {
        return string.replace(/_/g, '-').
          replace(/([a-z])([A-Z])/g, function (_, $1, $2) {
              return $1 + '-' + $2.toLowerCase();
          });
    };

    // get style string of an element
    var getStyle = function (el, styleProp) {
        return document.defaultView.
          getComputedStyle(el, null).
          getPropertyValue(styleProp);
    };

    var injectDefaultStyleToHead = function () {
        if (!document.querySelector('style#auto-complete-style')) {
            var htmlDiv = document.createElement('div');
            htmlDiv.innerHTML = '<b>1</b>' +
              '<style id="auto-complete-style">' +
              defaultStyle +
              '</style>';
            document.getElementsByTagName('head')[0].
              appendChild(htmlDiv.childNodes[1]);
        }
    };

    var getRemoteData = function (source, query, pathToData) {
        var deferred = $q.defer(), httpGet;
        if (typeof source == 'string') {
            var keyValues = [];
            for (var key in query) { // replace all keyword to value
                var regexp = new RegExp(key, 'g');
                if (source.match(regexp)) {
                    source = source.replace(regexp, query[key]);
                } else {
                    keyValues.push(key + "=" + query[key]);
                }
            }
            if (keyValues.length) {
                var qs = keyValues.join("&");
                source += source.match(/\?[a-z]/i) ? qs : ('?' + qs);
            }
            httpGet = $http.get(source);
        } else if (source.$promise) {
            httpGet = source(query).$promise;
        } else if (typeof source == 'function') {
            httpGet = source(query);
            httpGet.$promise && (httpGet = source(query).$promise);
            if (!httpGet.then) {
                throw "source function must return a promise";
            }
        }

        httpGet.then(
          function (resp) {
              //var list = resp.constructor.name == 'Resource' ? resp : resp.data;
              var list = resp.data;
              //if (pathToData) {
              //  var paths = pathToData.split('.');
              //  paths.forEach(function(el) {
              //    list = list[el];
              //  });
              //}
              deferred.resolve(list);
          },
          function (error) {
              deferred.reject(error);
          }
        );

        return deferred.promise;
    };

    angular.module('CRMApp.Directives').
      factory('AutoComplete', ['$q', '$http', function (_$q_, _$http_) {
          $q = _$q_, $http = _$http_;
          return {
              defaultStyle: defaultStyle,
              dasherize: dasherize,
              getStyle: getStyle,
              getRemoteData: getRemoteData,
              injectDefaultStyle: injectDefaultStyleToHead
          };
      }]);
})();
