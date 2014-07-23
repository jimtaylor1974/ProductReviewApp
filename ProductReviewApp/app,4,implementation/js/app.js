var app = angular.module('ProductReview', ['ngRoute']);

app
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

    .controller('MainCtrl', ['$scope', function ($scope) {

    }])
    .controller('HomeCtrl', ['$scope', 'DataService', function ($scope, DataService) {
        $scope.features = [];

        $scope.closeFeedback = function (feature) {
            feature.leavingFeedback = false;
        }

        $scope.leaveFeedback = function (feature) {
            if (feature.leavingFeedback === true) {
                feature.leavingFeedback = false;
                return;
            }

            DataService.feedback.get(feature.id).then(function (response) {
                feature.leavingFeedback = true;
                feature.feedbackText = "";
                feature.feedback = response.data;
                feature.agreement = false;
            });
        };

        $scope.submitFeedback = function (feature) {
            DataService.feedback.save({
                featureId: feature.id,
                createdBy: "Guest user",
                text: feature.feedbackText
            }).then(
            function () {
                feature.feedbackCount++;
                $scope.leaveFeedback(feature);
            });
        };

        function initialize() {
            DataService.feature().then(function (response) {
                $scope.features = response.data;
            });
        }

        initialize();
    }])
    .controller('FeatureRequestsCtrl', ['$scope', 'DataService', function ($scope, DataService) {

        $scope.featureRequests = [];

        $scope.orderBy = "dateCreated";

        $scope.setOrderBy = function (orderBy) {
            $scope.orderBy = orderBy;

            initialize();
        };

        $scope.toggleCreateNewFeatureRequest = function () {
            $scope.showCreateNewFeatureRequest = !$scope.showCreateNewFeatureRequest;
        };

        $scope.initializeNewFeatureRequest = function () {
            $scope.showCreateNewFeatureRequest = false;
            $scope.newFeatureRequest = {
                name: "",
                description: ""
            };
        }

        $scope.submitNewFeatureRequest = function () {
            DataService.featureRequests.save({
                createdBy: "Guest user",
                name: $scope.newFeatureRequest.name,
                description: $scope.newFeatureRequest.description
            }).then(function (response) {
                initialize();
            });
        };

        $scope.vote = function (featureRequest, value) {
            DataService.vote({
                featureId: featureRequest.id,
                value: value
            }).then(function (response) {
                initialize();
            });
        };

        function initialize() {
            $scope.initializeNewFeatureRequest();
            $scope.showCreateNewFeatureRequest = false;

            DataService.featureRequests.get($scope.orderBy).then(function (response) {
                $scope.featureRequests = response.data;
            });
        }

        initialize();
    }])

    .filter('date', function () {
        return function (text) {
            // "2014-07-21T04:27:54.847" -> "21/07/2014"
            return text.replace(/^(\d\d\d\d)-(\d\d)-(\d\d).+$/, "$3/$2/$1");
        }
    })

    .controller('FeedbackCtrl', ['$scope', 'DataService', function ($scope, DataService) {
        function initialize() {
            DataService.feedback.get($scope.feature.id).then(function (response) {
                $scope.feature.leavingFeedback = true;
                $scope.feature.feedback = response.data;
                $scope.feature.feedbackText = "";
                $scope.agreement = false;
            });
        }

        $scope.submitFeedback = function (feature) {
            DataService.feedback.save({
                featureId: feature.id,
                createdBy: "Guest user",
                text: feature.feedbackText
            }).then(
            function () {
                feature.feedbackCount++;
                initialize();
            });
        };

        $scope.closeFeedback = function (feature) {
            feature.leavingFeedback = false;
        };

        initialize();
    }])
    .directive('feedback', function () {
        return {
            restrict: 'E',
            templateUrl: 'app,4,implementation/templates/feedback.html',
            replace: true,
            require: 'feature',
            scope: {
                feature: '=',
                title: '='
            },
            controller: 'FeedbackCtrl'
        };
    })

    .config(['$routeProvider', function ($routeProvider) {
        $routeProvider
        .when('/home', {
            templateUrl: 'app,4,implementation/templates/home.html',
            controller: 'HomeCtrl'
        })
        .when('/feature-requests', {
            templateUrl: 'app,4,implementation/templates/feature-requests.html',
            controller: 'FeatureRequestsCtrl'
        })
        .otherwise({
            redirectTo: '/home'
        });
    }])
;