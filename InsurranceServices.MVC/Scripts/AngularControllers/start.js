"use strict";
var mainApp = angular.module('mainApp', []);
var tools = {};

(function () {   
    mainApp.factory('HttpService', HttpService);
    mainApp.factory('PagerService', PagerService);
    mainApp.factory('CommonService', CommonService);
   
    function HttpService($http) {
        var service = {};
        service.executeRequest = executeRequest;
        return service;

        function executeRequest(url, method, data, contentType, token, success, error) {
            var auth = null;
            method = method || 'GET';
            data = data || null;

            if (token) $http.defaults.headers.common['Authorization'] = 'Bearer ' + token;

            if (contentType) $http.defaults.headers.common['Content-Type'] = contentType;
            waitProcess();
            return $http({
                method: method,
                url: url,
                data: data
            }).then(function (response) {
                $('#waitdiv').remove();
                success(response);
            }, function (response) {
                $('#waitdiv').remove();
                error(response);
            }
                );
        }

        function waitProcess() {
            var div = document.createElement("DIV");
            div.id = 'waitdiv';
            div.style.position = 'fixed';
            div.style.display = 'block';
            div.style.width = '100%';
            div.style.height = '100%';
            div.style.backgroundColor = '#fff';
            div.style.zIndex = '100';
            div.style.top = 0;
            div.style.left = 0;
            div.style.opacity = 0.7;
            div.style.textAlign = 'center';
            div.style.backgroundImage = "url('/content/images/progress.gif')";
            div.style.backgroundRepeat = 'no-repeat';
            div.style.backgroundSize = '150px auto';
            div.style.backgroundPosition = 'center';
            $('body').append(div);
        }
    }

    function CommonService() {
        var service = {};
        service.StatusColorClass = StatusColorClass;
        service.StatusColorClassPercent = StatusColorClassPercent;
        service.Percent = Percent;
        service.imgNotNull = imgNotNull;

        return service;

        function StatusColorClass(total, remain) {
            var percent = (remain * 100) / total;

            if (percent >= 50) return 'progress-success';
            if (percent >= 25) return 'progress-warning';
            return 'progress-danger';
        }

        function StatusColorClassPercent(percent) {
            if (percent <= 0) return "";

            if (percent >= 50) return 'progress-success';
            if (percent >= 25) return 'progress-warning';
            return 'progress-danger';
        }

        function Percent(total, value) {
            var ret = parseInt((value * 100) / total);

            if (ret > 100) return 100;
            if (ret < 0) return 0;
            return ret;
        }

        function imgNotNull(imgPath) {
            if (imgPath == null || imgPath == '') return false;    
            return true;
        }
    }

    function PagerService() {
        var service = {};
        service.GetPager = GetPager;

        return service;

        function GetPager(totalItems, currentPage, pageSize) {
            // default to first page
            currentPage = currentPage || 1;

            // default page size is 10
            pageSize = pageSize || 10;

            // calculate total pages
            var totalPages = Math.ceil(totalItems / pageSize);

            var startPage, endPage;
            if (totalPages <= 10) {
                // less than 10 total pages so show all
                startPage = 1;
                endPage = totalPages;
            } else {
                if (currentPage <= 6) {
                    startPage = 1;
                    endPage = 10;
                } else if (currentPage + 4 >= totalPages) {
                    startPage = totalPages - 9;
                    endPage = totalPages;
                } else {
                    startPage = currentPage - 5;
                    endPage = currentPage + 4;
                }
            }

            var startIndex = (currentPage - 1) * pageSize;
            var endIndex = Math.min(startIndex + pageSize - 1, totalItems - 1);
            var pages = createRangeArray(startPage, endPage + 1);

            function createRangeArray (start, end) {
                var ret = [];
                for (var i = start; i < end; i++) {
                    ret.push(i);
                }

                return ret;
            }

            return {
                totalItems: totalItems,
                currentPage: currentPage,
                pageSize: pageSize,
                totalPages: totalPages,
                startPage: startPage,
                endPage: endPage,
                startIndex: startIndex,
                endIndex: endIndex,
                pages: pages
            };
        }
    }   

    Number.prototype.formatMoney = function (c, d, t) {
        var n = this,
            c = isNaN(c = Math.abs(c)) ? 2 : c,
            d = d == undefined ? "." : d,
            t = t == undefined ? "," : t,
            s = n < 0 ? "-" : "",
            i = String(parseInt(n = Math.abs(Number(n) || 0).toFixed(c))),
            j = (j = i.length) > 3 ? j % 3 : 0;
        return s + (j ? i.substr(0, j) + t : "") + i.substr(j).replace(/(\d{3})(?=\d)/g, "$1" + t) + (c ? d + Math.abs(n - i).toFixed(c).slice(2) : "");
    };

})();

var mytools = {};
mytools.clone = function clone(obj) {
    if (!obj) return;

    if (null == obj || "object" != typeof obj) return obj;
    var copy = obj.constructor();
    for (var attr in obj) {
        if (obj.hasOwnProperty(attr)) copy[attr] = obj[attr];
    }
    return copy;
}
