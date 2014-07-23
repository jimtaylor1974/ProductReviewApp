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
            templateUrl: 'app,3,routes,design,service/templates/home.html',
            controller: 'HomeCtrl'
        })
        .when('/feature-requests', {
            templateUrl: 'app,3,routes,design,service/templates/feature-requests.html',
            controller: 'FeatureRequestsCtrl'
        })
        .otherwise({
            redirectTo: '/home'
        });
    }])

    .value('serviceBase', '/')
    .service('DataService', ['$http', 'serviceBase', function ($http, serviceBase) {
        var self = this;

        self.feature = function () {
            var url = serviceBase + 'api/feature';

            return $http.get(url);
        };

        self.featureRequests = {
            get: function (orderBy) {
                var url = serviceBase + 'api/featurerequests?orderBy=' + encodeURIComponent(orderBy);

                return $http.get(url);
            },
            save: function (requestFeatureCommand) {
                var url = serviceBase + 'api/featurerequests';

                return $http.post(url, requestFeatureCommand);
            }
        };

        self.feedback = {
            get: function (featureId) {
                var url = serviceBase + 'api/feedback?featureId=' + encodeURIComponent(featureId);

                return $http.get(url);
            },
            save: function (leaveFeedbackCommand) {
                var url = serviceBase + 'api/feedback';

                return $http.post(url, leaveFeedbackCommand);
            }
        };

        self.vote = function (voteCommand) {
            var url = serviceBase + 'api/vote';

            return $http.post(url, voteCommand);
        }

        return self;
    }])
;