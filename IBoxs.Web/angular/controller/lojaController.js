var lojaController = angular.module('appIBoxs.loja');

lojaController.controller('lojaController', ['$rootScope', '$scope', '$injector', '$filter', '$state', 'lojaService', function ($rootScope, $scope, $injector, $filter, $state, lojaService) {
    var blockUi = $injector.get("blockUI");
    var cepService = $injector.get("cepService");
    $scope.propertyName = 'Id';
    $scope.reverse = true;
    $scope.IntegradorPagamento = [
        { Id: 1, Nome: "MercadoPago" }
    ];

    $scope.ListaEstado = [
        { sigla: "Selecione", nome: "Selecione" },
        { sigla: "AC", nome: "Acre(AC)" },
        { sigla: "AL", nome: "Alagoas(AL)" },
        { sigla: "AP", nome: "Amapá(AP)" },
        { sigla: "AM", nome: "Amazonas(AM)" },
        { sigla: "BA", nome: "Bahia(BA)" },
        { sigla: "CE", nome: "Ceará(CE)" },
        { sigla: "DF", nome: "Distrito Federal(DF)" },
        { sigla: "ES", nome: "Espírito Santo(ES)" },
        { sigla: "GO", nome: "Goiás(GO)" },
        { sigla: "MA", nome: "Maranhão(MA)" },
        { sigla: "MT", nome: "Mato Grosso(MT)" },
        { sigla: "MS", nome: "Mato Grosso do Sul(MS)" },
        { sigla: "MG", nome: "Minas Gerais(MG)" },
        { sigla: "PA", nome: "Pará(PA)" },
        { sigla: "PB", nome: "Paraíba(PB)" },
        { sigla: "PR", nome: "Paraná(PR)" },
        { sigla: "PB", nome: "Pernambuco(PE)" },
        { sigla: "PI", nome: "Piauí(PI)" },
        { sigla: "RJ", nome: "Rio de Janeiro(RJ)" },
        { sigla: "RN", nome: "Rio Grande do Norte(RN)" },
        { sigla: "RS", nome: "Rio Grande do Sul(RS)" },
        { sigla: "RO", nome: "Rondônia(RO)" },
        { sigla: "RR", nome: "Roraima(RR)" },
        { sigla: "SC", nome: "Santa Catarina(SC)" },
        { sigla: "SP", nome: "São Paulo(SP)" },
        { sigla: "SE", nome: "Sergipe(SE)" },
        { sigla: "TO", nome: "Tocantins(TO)" }
    ];

    var Inicial = function () {
        $scope.Loja = {
            Id: "",
            Nome: "",
            CaminhoBancoDados: "",
            IntegradorPagamento: "",
            PublicKey: "",
            AccessToken: "",
            Logradouro: "",
            Numero: "",
            CEP: "",
            Bairro: "",
            Cidade: "",
            Estado: "",
            TaxaBoleto: 0.00,
            Whatsapp: "",
            Email: "",
            CNPJ: "",
            Logo: "",
            QuemSomos: "",
            PoliticaPrivacidade: "",
            PoliticaTrocaDevolucao: "",
            Ativo: true,
            VerEstoque: true,
            PagamentoRetira: true,
            QtdMinima: 1,
            IdFacebookPixel: ""
        };
    };

    $scope.Init = function () {
        blockUi.start("Aguarde");
        //document.getElementById("btnWhats").style.display = "none";
        lojaService.ListarLojas().then(function (result) {
            if (result != undefined) {
                if (result.data != undefined) {
                    $scope.ListaLojas = result.data;
                    blockUi.stop();
                }
            }
        });
    };

    $scope.OnSort = function (propertyName) {
        $scope.reverse = ($scope.propertyName === propertyName) ? !$scope.reverse : false;
        $scope.propertyName = propertyName;
    };

    $scope.EditarLoja = function (event, Id) {
        event.preventDefault();
        blockUi.start("Aguarde");
        lojaService.BuscarLoja(Id).then(function (result) {
            if (result != undefined) {
                if (result.data != undefined) {
                    $scope.Loja = result.data;
                    blockUi.stop();
                }
                else {
                    Messaging.Information("Loja não encontrada", "Loja não encontrada");
                }
            }
        });
    };

    $scope.Salvar = function () {
        lojaService.Salvar($scope.Loja).then(function () {
            Messaging.Success("Cadastro salvo com sucesso!");
            $("#ModalLoja").modal("toggle");
            $scope.Init();
        });
    };

    $scope.Adicionar = function (event) {
        event.preventDefault();
        Inicial();
    };

    $scope.pesquisaCep = function (event) {
        try {
            event.preventDefault();
            event.stopPropagation();
            var cepPesquisa = cepService.removeMascara($scope.Loja.CEP);
            if ($scope.Loja.CEP == "" || $scope.Loja.CEP == undefined || cepPesquisa.length < 8) {
                Messaging.Information("Pesquisar CEP", "Informe um CEP válido");
                var cep = document.getElementById("cep");
                cep.focus();
                return;
            }
            cepService.PesquisaCep(cepPesquisa).then(function (result) {
                var data = result.data;
                if (data.erro == undefined) {
                    $scope.Loja.Logradouro = data.logradouro;
                    $scope.Loja.Complemento = data.complemento;
                    $scope.Loja.Bairro = data.bairro;
                    $scope.Loja.Cidade = data.localidade;
                    $scope.Loja.Estado = data.uf;
                    var num = document.getElementById("numero");
                    num.focus();
                }
                else {
                    $scope.Loja.Logradouro = '';
                    $scope.Loja.Complemento = '';
                    $scope.Loja.Bairro = '';
                    $scope.Loja.Cidade = '';
                    $scope.Loja.Estado = 'Selecione';
                    Messaging.Information("Pesquisa de CEP", "CEP não encontrado");
                }
            }, function (error) {
                var data = error.data;
                Messaging.Error("Erro", data.ExceptionMessage);
            });
        }
        catch (e) {
            Messaging.Error("Erro pesquisa cep", e);
        }
    };

    $scope.ExcluirLoja = function (event, id) {
        event.preventDefault();
        event.stopPropagation();
        var resposta = confirm("Deseja excluir a loja " + id + "?");
        if (resposta) {
            lojaService.ExcluirLoja(id).then(function () {
                $scope.Init();
            })
        }
        
    }
}]);