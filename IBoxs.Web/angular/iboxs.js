var boxCommerce = angular.module('appIBoxs', ['ngAnimate', 'ngMessages', 'angular-jquery-mask',
    'ui.mask', 'ui.router', 'appIBoxs.route', 'blockUI', 'ngSanitize',
    'appIBoxs.produto', 'appIBoxs.carrinho', 'appIBoxs.pagamento', 'appIBoxs.loja', 'appIBoxs.cep', 'appIBoxs.fileUpload',
    'appIBoxs.scrollPages', 'appIBoxs.login', 'appIBoxs.perfil']);

boxCommerce.config(['$httpProvider', function ($httpProvider) {
    //To make all http requests that return in around the same time resolve
    $httpProvider.useApplyAsync(true);
}]);

boxCommerce.config(['blockUIConfig', function (blockUiConfig) {

    // Change the default overlay message
    blockUiConfig.message = 'Por favor aguarde...'

    // Change the default delay to 100ms before the blocking is visible
    blockUiConfig.delay = 500;

    blockUiConfig.requestFilter = function (config) {

        var message;

        switch (config.method) {
            case 'GET':
                message = 'Por favor aguarde...';
                return false;

            case 'POST':
                message = 'Processando...';
                break;

            case 'DELETE':
                message = 'Excluindo...';
                break;

            case 'PUT':
                message = 'Atualizando...';
                break;
            default:
                message = 'Por favor aguarde ...';
        };

        return message;
    };
}]);


boxCommerce.config(['$qProvider', function ($qProvider) {
    $qProvider.errorOnUnhandledRejections(true);
}]);
