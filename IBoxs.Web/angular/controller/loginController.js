var loginController = angular.module('appIBoxs.login');

loginController.controller('loginController', ['$rootScope', '$scope', '$injector', '$filter', '$state', 'loginService', function ($rootScope, $scope, $injector, $filter, $state, loginService) {
    var blockUi = $injector.get("blockUI");
    var lojaService = $injector.get("lojaService");
    $scope.Login = {
        Id: '',
        Password: ''
    };

    $scope.Cadastro = {
        TipoDoc: "-1",
        RazaoSocial: "",
        NomeFantasia: "",
        Documento: "",
        Email: "",
        Senha: "",
        ConfirmaSenha: "",
        Celular: ""
    };

    $scope.tiposDocumentos = [
        { id: "-1", name: "Selecione", maxlength: 0, mask: "", placeHolderNome: "Nome", placeHolderDoc: "Informe o documento" },
        { id: "0", name: "CPF", maxlength: "14", mask: "000.000.000-00", placeHolderNome: "Nome", placeHolderDoc: "Informe o CPF" },
        { id: "1", name: "CNPJ", maxlength: "18", mask: "00.000.000/0000-00", placeHolderNome: "Nome Fantasia", placeHolderDoc: "Informe o CNPJ" }
    ];

    $scope.Init = function () {
        var footer = document.getElementsByTagName('footer');
        footer[0].style.display = 'none';
        var cabecalho = document.getElementById("cabecalho");
        cabecalho.style.display = 'none';

    };
    $scope.Autenticar = function () {
        if ($scope.Login.Id == undefined || $scope.Login.Id == '') {
            Messaging.Information("Login", "Informe os dados corretos para autenticar");
            return
        }
        if ($scope.Login.Password == undefined || $scope.Login.Password == '') {
            Messaging.Information("Login", "Informe os dados corretos para autenticar");
            return
        }
        blockUi.start("Aguarde, estamos verificando seus dados de acesso");
        loginService.Autenticar($scope.Login).then(function (result) {
            if (result != undefined && result.data != undefined && result.data == true) {
                $state.go("menu", { Id: $scope.Login.Id });
            }
            else {
                Messaging.Information("Login", "Não foi encontrado nenhum cadastro com o Id/Senha informado.");
            }
        }, function (error) {
            Messaging.ShowMessaging(error);
        }).finally(function () {
            blockUi.stop();
        });
    };

    $scope.TamanhoDocumento = function (tipo) {
        var tipoSelecionado = $filter("filter")($scope.tiposDocumentos, { id: tipo }, true)[0];
        $scope.Cadastro.Documento = "";
        $("#Documento").mask(tipoSelecionado.mask);
        $("#Documento").attr("placeholder", tipoSelecionado.placeHolderDoc);
        $("#Nome").attr("placeholder", tipoSelecionado.placeHolderNome);
        $scope.TamanhoMaximoDocumento = parseInt(tipoSelecionado.maxlength);
        $scope.MascaraDocumento = tipoSelecionado.mask;
    };

    $scope.Salvar = function () {
        blockUi.start("Aguarde, estamos fazendo seu cadastro.");
        lojaService.Cadastro($scope.Cadastro).then(function () {
            Messaging.Success("Cadastro efetuado com sucesso!");
            $scope.Cadastro = {
                TipoDoc: "-1",
                RazaoSocial: "",
                NomeFantasia: "",
                Documento: "",
                Email: "",
                Senha: "",
                ConfirmaSenha: "",
                Celular: ""
            };
            $("#btnFechar").click();
        }, function (error) {
            Messaging.ShowMessaging(error);
        }).finally(function () {
            blockUi.stop();
        })
    };
}]);