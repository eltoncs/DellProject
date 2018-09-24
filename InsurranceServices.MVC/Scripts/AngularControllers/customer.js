(function () {    
    
    mainApp.controller('CustomerController', ['$scope', 'HttpService',
        'CommonService',
        function ($scope, HttpService, CommonService)
        {
            $scope.customer = {};
            $scope.customers = [];
            $scope.insurances = [];

            $scope.addNewCustomer = addNewCustomer;
            $scope.saveCustomer = saveCustomer;
            $scope.editCustomer = editCustomer;
            $scope.removeConfirm = removeConfirm;

            getAll();        
        
            function saveCustomer() {
                var url = 'Api/CustomerService/Save';

                $scope.customer.Insurances = getSelectedInsurances();

                HttpService.executeRequest(url, 'POST', $scope.customer, 'application/x-www-form-urlencoded', null,
                    function (response) {
                        if (response.data.Success) {
                            $scope.customer = {};
                            getAll();
                            getCheckList();
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

            function getSelectedInsurances() {
                var selected = [];

                for (var i = 0; i < $scope.insurances.length; i++) {
                    $scope.insurances[i].InsuranceTypeId = $scope.insurances[i].Id;

                    if ($scope.insurances[i].Checked) selected.push($scope.insurances[i]);
                }

                return selected;
            }

            function getAll() {
                var url = 'Api/CustomerService/GetAll';

                HttpService.executeRequest(url, 'GET', $scope.partner, 'application/x-www-form-urlencoded', null,
                    function (response) {
                        $scope.customers = response.data.Data;
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

            function getCheckList() {
                var url = 'Api/InsuranceTypeService/GetCheckList';

                HttpService.executeRequest(url, 'GET', $scope.partner, 'application/x-www-form-urlencoded', null,
                    function (response) {
                        $scope.insurances = response.data.Data;
                        checkInsurances();
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

            function checkInsurances() {
                if (!$scope.customer.Insurances) return;

                for (var i = 0; i < $scope.customer.Insurances.length; i++) {
                    for (var x = 0; x < $scope.insurances.length; x++) {
                        if ($scope.insurances[x].Id == $scope.customer.Insurances[i].InsuranceTypeId) {
                            $scope.insurances[x].Checked = true;
                        }
                    }
                }
            }

            function getById(id) {
                var url = 'Api/CustomerService/GetById?id='+id;

                HttpService.executeRequest(url, 'GET', $scope.partner, 'application/x-www-form-urlencoded', null,
                    function (response) {
                        $scope.customer = response.data.Data;
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
                var url = 'Api/CustomerService/Delete?id=' + id;

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

            function addNewCustomer() {
                $scope.customer = {};
                getCheckList();
                $("#modalCustomer").modal("show");
            }

            function editCustomer(id) {
                var url = 'Api/CustomerService/GetById?id=' + id;

                HttpService.executeRequest(url, 'GET', $scope.partner, 'application/x-www-form-urlencoded', null,
                    function (response) {
                        $scope.customer = response.data.Data;
                        getCheckList();
                    },
                    function () {
                        swal({
                            title: "Error",
                            text: "Service unavailable",
                            type: "warning"
                        });
                    }
                );

                $("#modalCustomer").modal("show");
            }

            function removeConfirm(id) {
                swal({
                    title: "Delete Customer",
                    text: "Do you want to delete this customer?",
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