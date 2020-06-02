'use strict';

////////////////////////////////
// Define AngularJS application
////////////////////////////////

angular.module('myApp', ['ui.bootstrap', 'dataGrid', 'pagination'])
    .constant('__SmsGatewayEnv', window.__SmsGatewayEnv)
    .controller('myAppController',
        [
            '$scope', 'myAppFactory', function($scope, myAppFactory) {

                $scope.gridOptions = {
                    data: [],
                    urlSync: true
                };

                $scope.newMessage = {};

                $scope.addMessage = function() {
                    myAppFactory.addMessage($scope.newMessage).then(function(data) {
                        $scope.newMessage = {};
                        $scope.refreshData();
                    });
                }
                $scope.refreshData = function() {
                    myAppFactory.getData().then(function(responseData) {
                        $scope.gridOptions.data = responseData.data;
                    });
                }

                $scope.statusText = function(status) {
                    switch (status) {
                    case 0:
                        return 'To Send';
                    case 1:
                        return 'In Progress';
                    case 2:
                        return 'Sent';
                    }
                }
                $scope.refreshData();

            }
        ])
    .factory('myAppFactory', ['$http', '__SmsGatewayEnv', function ($http,__SmsGatewayEnv) {
        return {
            getData: function () {
                return $http({
                    method: 'GET',
                    headers: {
                        "API-KEY": "3b1b1cc5-1355-412e-a1eb-00f5f3ca7c9a"
                    },
                    url: __SmsGatewayEnv.smsServiceUrl + '/api/Message/All/'
                });
            },
            addMessage: function (message) {
                return $http({
                    method: 'POST',
                    headers: {
                        "API-KEY": "3b1b1cc5-1355-412e-a1eb-00f5f3ca7c9a"
                    },
                    data: message,
                    url: __SmsGatewayEnv.smsServiceUrl + '/api/Message/Add'

                });

            }
        }
    }]);
