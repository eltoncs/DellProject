(function () {
    mainApp.controller('DashBoardController', ['$scope', 'HttpService',
        'CommonService',
        function ($scope, HttpService, CommonService) {
            $scope.partners = [];
            $scope.totalSimulations = 0;
            $scope.maxSimulation = 0;
            $scope.simulations = [];
            $scope.summary = [];
            var donnutChart = [];

            $scope.getAllPartners = getAllPartners;
            $scope.GetAllSimulations = GetAllSimulations;
            $scope.GetSummaryStatistics = GetSummaryStatistics;
            $scope.showDonnut = showDonnut;
            $scope.showDetail = showDetail;

            getAllPartners();
            GetAllSimulations();
            GetSummaryStatistics();        

            function getAllPartners() {
                var url = 'Api/SimulationService/GetAllPartnersWithSimulations';

                HttpService.executeRequest(url, 'GET', $scope.simulation, 'application/x-www-form-urlencoded', null,
                    function (response) {
                        if (response.data.Success) {
                            $scope.partners = response.data.Data;
                            $scope.partners.unshift({Id:null, Name: "Todos"})
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
                        $scope.result = "ERROR";
                    }
                );
            }

            function GetSummaryStatistics() {
                var url = 'Api/SimulationService/GetSummaryStatistics?partnerId=' + $scope.partnerId;

                HttpService.executeRequest(url, 'GET', $scope.simulation, 'application/x-www-form-urlencoded', null,
                    function (response) {
                        if (response.data.Success) {
                            $scope.summary = response.data.Data;
                            showDonnut();
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
                        $scope.result = "ERROR";
                    }
                );
            }

            function GetAllSimulations() {
                var url = 'Api/SimulationService/GetAllSimulations';

                HttpService.executeRequest(url, 'GET', $scope.partner, 'application/x-www-form-urlencoded', null,
                    function (response) {

                        if (response.data.Success) {
                            $scope.simulations = response.data.Data.Simulations;
                            $scope.totalSimulations = response.data.Data.Total;

                            if ($scope.simulations.length > 0) $scope.maxSimulation = $scope.simulations[0].SimulationTimes;
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

            function showDetail() {
                GetSummaryStatistics();
            }

            function showDonnut() {
                $("#donnut").remove();
                var canvas = document.createElement("canvas");
                canvas.id = "donnut";
                $(canvas).addClass("my-donnut");
                $("#donnut-area").append(canvas);
                var ctx = canvas.getContext('2d');            
                var labels = createChartLabels();
                var values = createChartValues();
                donnutChart = new Chart(ctx, {
                    type: 'doughnut',
                    data: {
                        labels: labels,
                        datasets: [{
                            data: values,
                            backgroundColor: [
                                'blue',
                                'green',
                                'red',
                                'gray'
                            ],
                            borderColor: [
                                'rgba(255,99,132,1)',
                                'rgba(54, 162, 235, 1)'
                            ],
                            borderWidth: 1
                        }]
                    },

                    options: {
                        legend: {
                            display: true
                        },
                        cutoutPercentage: 50,
                        maintainAspectRatio: false
                    }
                });
            }

            function createChartLabels() {
                var ret = [];
                for (var i = 0; i < $scope.summary.length; i++) {
                    ret.push($scope.summary[i].InsuranceType);
                }

                return ret;
            }

            function createChartValues() {
                var ret = [];
                for (var i = 0; i < $scope.summary.length; i++) {
                    ret.push($scope.summary[i].Quantity);
                }

                return ret;
            }
    }]);
})();