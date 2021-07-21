var perfilController = angular.module('appIBoxs.perfil', []);

perfilController.controller('perfilController', ['$rootScope', '$scope', '$injector', '$filter', '$state', function ($rootScope, $scope, $injector, $filter, $state) {
    var lojaServices = $injector.get("lojaService");
    var Id = $state.params.Id;
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


    $scope.Init = function () {
        if (Id == undefined || Id == '') {
            $state.go("login");
        }
        else {
            lojaServices.BuscarLojaPorCNPJCPF(Id).then(function (result) {
                if (result != undefined && result.data != undefined) {
                    $scope.Loja = result.data;
                    var urlImage = window.location.origin + window.location.pathname + "/Content/imagens/IBOXS_ESFERA.png";
                    var cabecalho = document.getElementById("cabecalho");
                    cabecalho.style.backgroundImage = 'url(' + urlImage + ')';
                    cabecalho.style.display = 'block';
                    cabecalho.style.marginLeft = '100px';
                    cabecalho.style.marginTop = '30px';
                    cabecalho.style.setProperty("width", "90px", "important");
                }

            }, function (erro) {
                $state.go("login");
            });
        }
    };
    var VerificaAutenticar = function () {
        lojaServices.VerificaAutenticar().then(function (result) {
            if (result != undefined) {
                var isAutenthicated = data.result;
                if (!isAutenthicated) {
                    $state.go("login");
                }
            }
        });
    };

    $scope.Salvar = function () {
        lojaServices.Salvar($scope.Loja).then(function () {
            Messaging.Success("Cadastro salvo com sucesso!");
        });
    };

    $scope.FecharCadastro = function () {
        $state.go("login");
    };
}]);