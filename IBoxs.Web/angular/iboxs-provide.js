var boxCommerce = angular.module('appIBoxs');

boxCommerce.config(['$provide', function ($provide) {

    function goToError500() {
        Messaging.Error("Serviço indisponível");
    }

    function goToError400(exception, $state, $injector, $timeout) {
        var error = "";
        var messages = exception.data.Messages;

        if (!angular.isUndefined(messages)) {
            for (var i = 0; i < messages.length; i++) {
                error += messages[i] + "\n";
            }
        }

        Messaging.Error(error);
        return true;
    }

    function goToError409(exception) {
        var error = "";
        var messages = exception.data.Messages;

        if (!angular.isUndefined(messages)) {
            for (var i = 0; i < messages.length; i++) {
                error += messages[i] + "\n";
            }
        }

        Messaging.Error("Erro no banco de dados", error);
    }

    function goToError417(exception) {
        var error = "";
        var messages = exception.data.Messages;

        if (!angular.isUndefined(messages)) {
            for (var i = 0; i < messages.length; i++) {
                error += messages[i] + "\n";
            }
        }

        Messaging.Error(exception.statusText, error);

    }

    function goToErrorMessagesArray(messages) {

        var error = "";

        if (!angular.isUndefined(messages)) {
            for (var i = 0; i < messages.length; i++) {
                error += messages[i] + "\n";
            }
        }
        Messaging.Error(error);
    }

    $provide.decorator('$exceptionHandler', ['$delegate', '$injector', function ($delegate, $injector) {
        return function (exception, cause) {

            $delegate(exception, cause);
            if (exception.indexOf("Possibly unhandled rejection: ") == 0) {
                var mensagem = JSON.parse(exception.replace("Possibly unhandled rejection: ", ""));
                exception = mensagem;
            }


            if (exception.status === 500 || exception.status === 503) {
                goToError500();
            } else if (exception.status === 409) {
                goToError409(exception);
            } else if (exception.status === 412) {

                var $rootScope = $injector.get('$rootScope');
                goToError412($rootScope);
            } else if (exception.status === 417 || exception.status === 403) {
                goToError417(exception);
            } else if (exception.status === 400) {
                var $state = $injector.get("$state");
                var $timeout = $injector.get("$timeout");
                goToError400(exception, $state, $injector, $timeout);
            } else if (angular.isDefined(exception.Messages)) {
                goToErrorMessagesArray(exception.Messages);
            }

        };
    }]);
}]);