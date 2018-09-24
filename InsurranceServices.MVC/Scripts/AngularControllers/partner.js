(function () {     
    mainApp.controller('PartnerController', ['$scope', 'HttpService',
        'CommonService',
        function ($scope, HttpService, CommonService)
        {
            $scope.partner = {};
            $scope.partner.Name = '';
            $scope.partner.Id = '';
            $scope.partners = [];

            $scope.savePartner = savePartner;
            $scope.getById = getById;
            $scope.removeConfirm = removeConfirm;
            $scope.newPartner = newPartner;

            getAll();

            function newPartner() {
                $scope.partner = {};
                $scope.partner.Name = '';
                $scope.partner.Id = '';
            }

            function savePartner() 
            {   
                var url = 'Api/PartnerService/Save';
                HttpService.executeRequest(url, 'POST', $scope.partner, 'application/x-www-form-urlencoded', null,
                    function (response) {
                        if (response.data.Success) {
                            $scope.partner = {};
                            getAll();
                        } else {
                            swal({
                                title: "Error",
                                text: response.data.Message,
                                type: "warning"}
                            );                        
                        }                    
                    },
                    function () {
                        swal({
                            title: "Error",
                            text: "Service unavailable",
                            type: "warning"
                        });
                    }
                );  
            }

            function getAll() {
                var url = 'Api/PartnerService/GetAll';

                HttpService.executeRequest(url, 'GET', $scope.partner, 'application/x-www-form-urlencoded', null,
                    function (response) {
                        $scope.partners = response.data.Data;
                    },
                    function () {
                        swal({
                            title: "Error",
                            text: "Service unavailable",
                            type: "warning"
                        });
                    }
                );
            }

            function getById(id) {
                var url = 'Api/PartnerService/GetById?id='+id;

                HttpService.executeRequest(url, 'GET', $scope.partner, 'application/x-www-form-urlencoded', null,
                    function (response) {
                        $scope.partner = response.data.Data;
                    },
                    function () {
                        swal({
                            title: "Error",
                            text: "Service unavailable",
                            type: "warning"
                        });
                    }
                );
            }

            function remove(id) {
                var url = 'Api/PartnerService/Delete?id=' + id;

                HttpService.executeRequest(url, 'POST', $scope.partner, 'application/x-www-form-urlencoded', null,
                    function (response) {
                        if (response.data.Success) {
                            $("#" + id).remove();
                        } else {
                            swal({
                                title: "Error",
                                text: response.data.Message,
                                type: "warning"
                            });
                        }
                    },
                    function () {
                        swal({
                            title: "Error",
                            text: "Service unavailable",
                            type: "warning"
                        });
                    }
                );
            }

            function removeConfirm(id) {
                swal({
                    title: "Delete Partner",
                    text: "Do you want to delete this partner?",
                    type: "warning",
                    showCancelButton: true,
                    confirmButtonColor: "red",
                    confirmButtonText: "Yes",
                    cancelButtonText: "No",
                    closeOnConfirm: true,
                    closeOnCancel: true
                }, function (isConfirm) {
                    if (isConfirm) {
                        remove(id);
                    }
                });
            }

        }]);
})();