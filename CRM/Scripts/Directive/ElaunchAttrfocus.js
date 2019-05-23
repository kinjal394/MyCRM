(function () {
    'use strict';
    angular
    .module("CRMApp.Directives")
    .directive('accessibleForm', function () {
        return {
            restrict: 'EA',
            link: function (scope, elem) {
                elem.on('submit', function () {
                    var validaele = elem[0].querySelectorAll('input,textarea,select');
                    for (var j = 0; j <= validaele.length - 1 ; j++) {
                        var item = validaele[j];
                        if (item.id == 'AutoComplete') {
                            // AutoComplete
                            if (item.className.indexOf('ng-empty') !== -1) {
                                if (item.parentNode.parentNode.attributes['ng-required'] == undefined) {
                                    var msg = msgCall(item.parentNode.parentNode.attributes['ng-model'].value + '.');
                                    if (msg != undefined) {
                                        $(item).focus();
                                        toastr.error(msg);
                                        break;
                                    }
                                }
                            }
                        }
                        else if (item.id != '') {
                            // Select
                            if (item.type == 'select-one') {
                                var selectele = item.querySelectorAll('option');
                                var chklen = 0;
                                for (var k = 1; k <= selectele.length - 1 ; k++) {
                                    if (selectele[k].selected == true) {
                                        chklen = 1;
                                        break;
                                    }
                                }
                                if (chklen == 0) {
                                    var msg = msgCall(item.id);
                                    if (msg != undefined) {
                                        $(item).focus();
                                        toastr.error(msg);
                                        break;
                                    }
                                }
                            }
                            // input:text-textarea
                            if (item.className.indexOf('ng-invalid') !== -1) {
                                var msg = msgCall('.' + item.name + '.');
                                if (msg != undefined) {
                                    $(item).focus();
                                    toastr.error(msg);
                                    break;
                                }
                            }
                            // input:radio button
                            if (item.type == 'radio') {
                                if (item.className.indexOf('ng-empty') !== -1) {
                                    var msg = msgCall(item.id);
                                    if (msg != undefined) {
                                        $(item).focus();
                                        toastr.error(msg);
                                        break;
                                    }
                                }
                            }
                        }
                        else if (item.id == '') {
                            // datepicker
                            if (item.attributes['datepicker-options'] != undefined) {
                                if (item.attributes['datepicker-options'].value == 'dateOptions') {
                                    if (item.parentElement.parentElement.attributes['isrequired'] == undefined) {
                                        if (item.attributes['class'].value.indexOf('ng-empty') !== -1) {
                                            $(item).focus();
                                            toastr.error('Please Select Date.');
                                            break;
                                        }
                                    }
                                }
                            }
                            else if (item.attributes['data-ck-editor'] != undefined) {
                                // data-ck-editor
                                if (item.attributes['ng-model'] != undefined) {
                                    var msg = msgCall(item.attributes['ng-model'].value);
                                    if (msg != undefined) {
                                        $(item.parentNode.childNodes[4].childNodes[1].childNodes[0].childNodes[0].childNodes[0].childNodes[1].childNodes[0].childNodes[2]).focus();
                                        toastr.error(msg);
                                        break;
                                    }
                                }
                            }
                            else if (item.attributes['type'] != undefined) {
                                // email
                                if (item.attributes['type'].value == 'email') {
                                    if (item.parentNode.parentNode.parentNode.attributes['ng-required'] == undefined) {
                                        if (item.parentNode.parentNode.parentNode.childNodes[3] == undefined) {
                                            $(item).focus();
                                            toastr.error('Please Enter EmailID.');
                                            break;
                                        }
                                    }
                                }
                                else if (item.parentNode.tagName == 'FIRST-ALPHABET') {
                                    // FIRST-ALPHABET
                                    if (item.parentNode.className.indexOf('ng-empty') !== -1) {
                                        var msg = msgCall(item.parentElement.attributes['ng-model'].value);
                                        if (msg != undefined) {
                                            $(item).focus();
                                            toastr.error(msg);
                                            break;
                                        }
                                        break;
                                    }
                                }
                                else if (item.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.tagName == 'ELAUNCH-MOBILE') {
                                    // Mobile No
                                    if (item.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.attributes['ng-required'] == undefined) {
                                        if (item.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.childNodes[3] == undefined) {
                                            item.parentElement.parentElement.parentElement.parentElement.parentElement.parentElement.parentElement.childNodes[2].focus();
                                            toastr.error('Please Enter Mobile No.');
                                            break;
                                        }
                                    }
                                }
                                    //else if (item.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.tagName == 'ELAUNCH-CHAT') {
                                else if (item.parentNode.parentNode.parentNode.parentNode.tagName == 'ELAUNCH-CHAT') {
                                    // Chat
                                    if (item.parentNode.parentNode.parentNode.parentNode.attributes['ng-required'] == undefined) {
                                        if (item.parentNode.parentNode.parentNode.childNodes[3] == undefined) {
                                            item.parentNode.parentNode.childNodes[1].focus();
                                            toastr.error('Please Enter Chat Value.');
                                            break;
                                        }
                                    }
                                }
                                else if (item.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.tagName == 'ELAUNCH-TIMEPICKER') {
                                    // ELAUNCH-TIMEPICKER
                                    if (item.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.attributes['isrequired'] == undefined) {
                                        if (item.attributes['class'].value.indexOf('ng-empty') !== -1) {
                                            $(item).focus();
                                            toastr.error('Please Select Time.');
                                            break;
                                        }
                                    }
                                }
                                else if (item.parentNode.parentNode.parentNode.parentNode.className.indexOf('multiSelect inlineBlock') !== -1) {
                                    // Isteven multiselect
                                    if (item.parentNode.parentNode.parentNode.parentNode.childNodes[0].childNodes[0].textContent == 'None') {
                                        var msg = msgCall(item.parentNode.parentNode.parentNode.parentNode.parentNode.attributes['output-model'].value);
                                        if (msg != undefined) {
                                            $(item.parentNode.parentNode.parentNode.parentNode.childNodes[0]).click();
                                            toastr.error(msg);
                                            break;
                                        }
                                    }
                                }
                            }


                        }
                    }
                    function msgCall(tagName) {
                        var msgbreack = true;
                        var element = elem[0].querySelectorAll('.errorMsg');
                        for (var i = 0; i <= element.length - 1 ; i++) {
                            if (element[i].attributes['ng-show'] != undefined) {
                                if (element[i].attributes['ng-show'].value.indexOf(tagName) !== -1) {
                                    return element[i].innerText;
                                }
                            }
                        }
                    }
                });
            }
        };
    })
})()