var boxProdutoController = angular.module('appIBoxs.produto');

boxProdutoController.controller('produtoController', ['$rootScope', '$scope', '$injector', '$filter', '$state', 'produtoService', function ($rootScope, $scope, $injector, $filter, $state, produtoService) {
    var carrinhoService = $injector.get('carrinhoService');
    var lojaService = $injector.get('lojaService');
    var filter = $injector.get('$filter');
    var blockUi = $injector.get("blockUI");
    $scope.serviceForFilter = "produtoService";

    $scope.LojaId = $state.params.LojaId;
    $rootScope.pesquisa = "";
    $rootScope.QuemSomos = "";
    $rootScope.PoliticaPrivacidade = "";
    $rootScope.PoliticaTrocaDevolucao = "";
    $scope.TokenProdutos = $state.params.TokenProdutos;
    $scope.ListaProdutos = [];
    $scope.VerEstoque = false;
    $scope.produto = {
        Nome: "",
        Cor: "",
        Tamanho: "",
        Quantidade: 1,
        QtdEstoque: 0
    };
    $scope.Tamanhos = ["XP", "P", "M", "G", "XG", "XXG"];
    $scope.Cores = ["Branco", "Preto", "Cinza"];
    $rootScope.CarrinhoCompra = [];
    var nomeCookie = $scope.LojaId + $scope.TokenProdutos;
    $scope.CarregarProdutos = function () {
        blockUi.start("Aguarde. O IBoxs está buscando produtos selecionados especialmente para você.");
        window.scrollTo(0, 0);
        lojaService.BuscarLoja($scope.LojaId).then(function (result) {
            if (result == undefined || result.data == undefined || result.data.length == 0 || result.data == "null") {
                Messaging.Information("Loja não encontrada", "Nenhuma loja encontrada para o id " + $scope.LojaId);
                window.location.href = "http://www.fabrisistemas.com.br/Iboxs";
            }
            else {
                var loja = result.data;
                $rootScope.NomeLoja = loja.Nome;
                $rootScope.Whatsapp = loja.Whatsapp;
                $rootScope.QuemSomos = loja.QuemSomos;
                $rootScope.PoliticaPrivacidade = loja.PoliticaPrivacidade;
                $rootScope.PoliticaTrocaDevolucao = loja.PoliticaTrocaDevolucao;
                $scope.VerEstoque = loja.VerEstoque;
                $scope.QtdMinima = loja.QtdMinima;
                $rootScope.MessagemCustomizada = loja.MensagemCompartilhamento;
                configurarMensagens();
                if (loja.IdFacebookPixel != undefined && loja.IdFacebookPixel != "") {
                    window.fbq('init', loja.IdFacebookPixel);
                    window.fbq('track', 'PageView');
                }
                if (loja.Logo) {
                    if (loja.Logo !== undefined || loja.Logo !== "") {
                        var urlImage = window.location.origin + window.location.pathname + "/image/" + loja.Logo;
                        var cabecalho = document.getElementById("cabecalho");
                        cabecalho.style.backgroundImage = 'url(' + urlImage + ')';
                    }
                }
                listarProdutos();
            }
        });
    };

    var configurarMensagens = function () {
        if ($rootScope.MessagemCustomizada) {
            document.querySelector('meta[name="description"]').setAttribute("content", $rootScope.MessagemCustomizada);
            var a2a_config = window.a2a_config;
            a2a_config.templates = a2a_config.templates || {};
            a2a_config.templates.whatsapp = {
                text: "${title} " + $scope.MessagemCustomizada + " ${link}"
            };
            a2a_config.callbacks = a2a_config.callbacks || [];
            a2a_config.callbacks.push({
                share: copyLinkText
            });
        }
    };

    var copyLinkText = function(share_data) {
        if (share_data.service == "Copy Link") {
            var old_url = share_data.url;
            var new_url = old_url;
            if (old_url.indexOf($scope.MessagemCustomizada, old_url.length - $scope.MessagemCustomizada) === -1) {
                new_url = $scope.MessagemCustomizada + " " + old_url;
            }

            if (new_url != old_url) {
                return {
                    url: new_url
                };
            }
        }
    };

    $scope.verificarCarrinho = function (cookie) {
        blockUi.start("Verificando itens do seu carrinho.");
        carrinhoService.AbrirCarrinho(cookie).then(function (result) {
            if (result.data.length > 0) {
                var data = result.data;
                $rootScope.CarrinhoCompra = data;
                for (var i = 0; i < data.length; i++) {
                    VerificaProdutoAdicionado(data[i]);
                }
            }
        }, function (error) {
            Messaging.ShowMessaging(error);
        }).finally(function () {
            blockUi.stop();
        });
    };

    var listarProdutos = function () {
        var cookie = cookies.verificarCookies(nomeCookie);
        var pesquisa = "$skip=0&$top=4&$orderby=Valor asc";
        produtoService.BuscarProdutos($scope.TokenProdutos, $scope.LojaId).then(function (result) {
            if (result == undefined || result.data == undefined || result.data.length == 0) {
                Messaging.Information("Loja sem produto", "Nenhum produto encontrado para a loja " + $scope.LojaId);
                window.location.href = "http://www.fabrisistemas.com.br/Iboxs";
            }
            else {
                $scope.ListaProdutos = result.data;
                $rootScope.DataExpira = result.data[0].DataExpira;
                if (cookie !== undefined) {
                    $scope.verificarCarrinho(cookie);
                }
                else {
                    blockUi.stop();
                }
            }
        }, function (error) {
            Messaging.ShowMessaging(error);
        }).finally(function () {
            blockUi.stop();
        });

    };

    $scope.PesquisarEstoque = function (produto) {
        produtoService.PesquisarEstoque(produto.Id, $scope.TokenProdutos, $scope.LojaId, produto.CorSelecionada, produto.TamanhoSelecionado).then(function (result) {
            produto.QtdEstoque = result.data;
        }, function (error) {
            produto.QtdEstoque = 0;
        })
    }

    $scope.AdicionarCarrinho = function (produto) {
        var produtoSelecionado = {
            CodigoReferenciaProduto: produto.Id,
            QuantidadeProduto: produto.Quantidade,
            ValorProduto: produto.EhPromocao == true ? produto.ValorPromocao : produto.Valor,
            CorSelecionada: produto.CorSelecionada,
            TamanhoSelecionado: produto.TamanhoSelecionado,
            NomeProduto: produto.Nome,
            Id: $rootScope.CarrinhoCompra.length == 0 ? "" : $rootScope.CarrinhoCompra[0].Id,
            LojaId: $scope.LojaId,
            TokenProdutos: $scope.TokenProdutos
        };

        produtoService.AdicionarItem(produtoSelecionado).then(function (result) {
            var data = result.data;
            VerificaProdutoAdicionado(data);
            var itemExistente = $filter("filter")($rootScope.CarrinhoCompra, { CodigoCompletoProduto: data.CodigoCompletoProduto }).length;
            if (itemExistente == 0)
                $rootScope.CarrinhoCompra.push(data);
            cookies.criarCookies(nomeCookie, result.data.Id)
            Messaging.Success("Produto adicionado com sucesso");
            window.fbq('track', 'AddToCart', {
                currency: "BRL",
                value: data.ValorProduto, content_name: data.NomeProduto,
                contents: [{ 'id': data.CodigoReferenciaProduto, 'quantity': data.QuantidadeProduto }]
            });
        }, function (erro) {
            Messaging.ShowMessaging(erro);
        });
    };

    var VerificaProdutoAdicionado = function (produto) {
        if (produto != undefined) {
            var produtoPesquisado = $filter("filter")($scope.ListaProdutos, { Id: produto.CodigoReferenciaProduto }, true);
            if (produtoPesquisado != undefined && produtoPesquisado.length > 0) {
                var produtoSelecionado = produtoPesquisado[0];
                produtoSelecionado.AdicionadoCarrinho = true;
            }
        }
    }

    $scope.increaseValue = function (p) {
        $scope.produto = p;
        var value = $scope.produto.Quantidade;
        value = isNaN(value) ? 0 : value;
        value++;
        $scope.produto.Quantidade = value;
    };

    $scope.decreaseValue = function (p) {
        $scope.produto = p;
        var value = $scope.produto.Quantidade;
        value = isNaN(value) ? 0 : value;
        value--;
        value <= 1 ? value = 1 : value;
        $scope.produto.Quantidade = value;
    };

    $rootScope.AbrirCarrinho = function (event, carrinho) {
        event.preventDefault();
        $state.go("carrinho", { LojaId: carrinho[0].LojaId, Id: carrinho[0].Id });
    };

    $scope.Compartilhar = function (e) {
        e.preventDefault();
        var url = "";
    };

}]);
