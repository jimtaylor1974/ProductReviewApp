var app = angular.module('ProductReview', ['ngRoute']);

app
    .controller('MainCtrl', ['$scope', function ($scope) {

    }])
    .controller('HomeCtrl', ['$scope', function ($scope) {

    }])
    .controller('FeatureRequestsCtrl', ['$scope', function ($scope) {

    }])

    .config(['$routeProvider', function ($routeProvider) {
        $routeProvider
        .when('/home', {
            templateUrl: 'app,2,routes,design/templates/home.html',
            controller: 'HomeCtrl'
        })
        .when('/feature-requests', {
            templateUrl: 'app,2,routes,design/templates/feature-requests.html',
            controller: 'FeatureRequestsCtrl'
        })
        .otherwise({
            redirectTo: '/home'
        });
    }])
;