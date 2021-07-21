var route = angular.module('appIBoxs.route', ['ui.router']);

route.config(['$stateProvider', '$urlRouterProvider', '$locationProvider', '$injector', '$sceProvider', '$uiRouterProvider', function ($stateProvider, $urlRouterProvider, $locationProvider, $injector, $sceProvider, $uiRouterProvider) {
    //Para rotas não definidas
    $urlRouterProvider.otherwise(function () {
        return '/';
        //window.location.href = "http://www.fabrisistemas.com.br/Iboxs";
    });

    var localUrl = location.pathname;

    if (localUrl.substr(localUrl.length - 1) !== "/") {
        localUrl += "/";
    }


    if (location.hash == "" || location.hash == "#!/") {
        window.location.href = "http://www.iboxs.com.br/#!/Login";
    }

    $locationProvider.html5Mode(false);
    $sceProvider.enabled(false);
    var stateRegistry = $uiRouterProvider.stateRegistry;

    stateRegistry.register({
        name: "home",
        url: "/:LojaId/:TokenProdutos",
        views: {
            'home': {
                controller: ['$scope', '$state', function ($scope, $state) {
                    var idLoja = $state.params.LojaId
                    var tokenProdutos = $state.params.TokenProdutos
                    $state.go('produtos', { "LojaId": idLoja, "TokenProdutos": tokenProdutos });
                }]
            }
        }
    });
    stateRegistry.register({
        name: "produtos",
        url: "/Prod/:LojaId/:TokenProdutos",
        views: {
            'viewProdutos': {
                templateUrl: localUrl + "angular/view/produtos.html",
                controller: "produtoController"
            }
        }
    });
    stateRegistry.register({
        name: "carrinho",
        url: "/Cart/:LojaId/:Id",
        views: {
            'viewCarrinho': {
                templateUrl: localUrl + "angular/view/carrinho.html",
                controller: "carrinhoController"
            }
        }
    });
    stateRegistry.register({
        name: "pagamento",
        url: "/Pag/:LojaId/:Id",
        views: {
            'viewPagamento': {
                templateUrl: localUrl + "angular/view/pagamento.html",
                controller: "pagamentoController"
            }
        }
    });

    stateRegistry.register({
        name: "credenciamento",
        url: "/Credenciamento",
        views: {
            'viewCredenciamento': {
                templateUrl: localUrl + "angular/view/loja.html",
                controller: "lojaController"
            }
        }
    });
    stateRegistry.register({
        name: "menu",
        url: "/Menu",
        params: {
            Id: ''
        },
        views: {
            'viewMenu': {
                templateUrl: localUrl + "angular/view/menu.html",
                controller: "loginController"
            }
        }
    });
    stateRegistry.register({
        name: "login",
        url: "/Login",
        views: {
            'viewLogin': {
                templateUrl: localUrl + "angular/view/login.html",
                controller: "loginController"
            }
        }
    });
}]);