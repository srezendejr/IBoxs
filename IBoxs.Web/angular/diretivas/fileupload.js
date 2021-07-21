var fileUpload = angular.module("appIBoxs.fileUpload", []);

fileUpload.directive('fileUpload', ['$http', function ($http) {
    return {
        restrict: "A",
        scope: true,
        require: "ngModel",
        link: function (scope, el, attrs, ngModel) {
            el.bind('change', function (event) {
                var files = event.target.files;
                if (files.length > 0) {
                    var url = window.location.origin + window.location.pathname;
                    var formData = new FormData();
                    formData.append('file', files[0])
                    formData.append('descricao', files[0].name)
                    $http.post(url + "api/loja/UploadLogo", formData,
                        {
                            withCredentials: false,
                            headers: { "Content-type": undefined },
                            transformReqest: angular.identity
                        }).then(function (arquivo) {
                            ngModel.$setViewValue(arquivo.data);
                            ngModel.$render();
                        }), function (error) {
                            throw error;
                            console.log(error);
                        };
                }
            });
        }
    };
}]);