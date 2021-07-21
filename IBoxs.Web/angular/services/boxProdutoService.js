var produtoServices = angular.module('appIBoxs.produto', []);

produtoServices.factory('produtoService', ['$http', function ($http) {
    var produtoService = {};
    var url = window.location.origin + window.location.pathname;

    produtoService.BuscarProdutos = function (TokenProdutos, IdLoja) {
        return $http.get(url + 'api/produto/buscarprodutos?TokenProdutos=' + TokenProdutos + '&IdLoja=' + IdLoja);
    };

    produtoService.BuscarProdutosPaginacao = function (TokenProdutos, IdLoja, param) {
        return $http.get(url + 'api/produto/BuscarProdutosPaginacao?TokenProdutos=' + TokenProdutos + '&IdLoja=' + IdLoja + '&' +param);
    };


    produtoService.AdicionarItem = function (item) {
        return $http.post(url + 'api/CarrinhoCompra/AdicionarItem', item);
    }

    produtoService.PesquisarEstoque = function (Id, TokenProdutos, LojaId, CorSelecionada, TamanhoSelecionado) {
        return $http.get(url + 'api/produto/pesquisarestoque?ReferenciaProduto=' + Id + '&TokenProduto=' + TokenProdutos + '&LojaId=' + LojaId + '&Cor=' + CorSelecionada + '&Tamanho=' + TamanhoSelecionado);
    }

    return produtoService;
}])