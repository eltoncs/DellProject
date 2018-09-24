(function () {
    mainApp.controller('SimulationController', ['$scope', 'HttpService',
                       'CommonService', function ($scope, HttpService,
                        CommonService) {
        $scope.simulation = {};
        $scope.propertyTypes = [];
        $scope.propertyType = {};
        $scope.result = "0.00";
        $scope.partners = [];
        $scope.simulate = simulate;
        $scope.pointPropertyType = pointPropertyType;
        $scope.simulation.ManufacacturedIn = new Date().getFullYear();
        $scope.thisYear = new Date().getFullYear();

        getPartners();
        getInsuranceTypes();

        function simulate() {
            var url = 'Api/SimulationService/Simulate';
            var simulationView = mytools.clone($scope.simulation);
            simulationView.Value = parseFloat(simulationView.Value.split(',').join(""));

            HttpService.executeRequest(url, 'POST', simulationView, 'application/x-www-form-urlencoded', null,
                function (response) {
                    if (response.data.Success) {
                        $scope.result = response.data.Data.formatMoney(2, '.', ',');
                    } else {
                        swal({
                            title: "Error",
                            text: response.data.Message,
                            type: "warning"
                        });
                        $scope.result = "ERROR";
                    }
                },
                function () {
                    swal({
                        title: "Error",
                        text: "Service unavailable",
                        type: "warning"
                    });
                    $scope.result = "ERROR";
                }
            );
        }

        function getInsuranceTypes() {
            var url = 'Api/InsuranceTypeService/GetAll';

            HttpService.executeRequest(url, 'GET', $scope.partner, 'application/x-www-form-urlencoded', null,
                function (response) {
                    
                    if (response.data.Success) {
                        $scope.propertyTypes = response.data.Data;
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
                });
        }

        function getPartners() {
            var url = 'Api/PartnerService/GetAll';

            HttpService.executeRequest(url, 'GET', $scope.partner, 'application/x-www-form-urlencoded', null,
                function (response) {
                    if (response.data.Success) {
                        $scope.partners = response.data.Data;
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
                });
        }

        function pointPropertyType() {
            if ($scope.simulation.PropertyType != null) {
                for (var i = 0; i < $scope.propertyTypes.length; i++) {
                    if ($scope.propertyTypes[i].Name == $scope.simulation.PropertyType) {
                        $scope.propertyType = $scope.propertyTypes[i];
                    }
                }
            }
        }
        
    }]);   
})();

$(document).ready(function () {
    $('#value').mask("#,##0.00", { reverse: true });
});