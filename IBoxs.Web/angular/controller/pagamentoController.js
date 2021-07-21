var pagController = angular.module('appIBoxs.pagamento');

pagController.controller('pagamentoController', ['$rootScope', '$scope', '$injector', '$filter', '$state', 'pagamentoService', function ($rootScope, $scope, $injector, $filter, $state, pagamentoService) {
    var filter = $injector.get('$filter');
    var lojaService = $injector.get('lojaService');
    var carrinhoService = $injector.get('carrinhoService');
    var timeOut = $injector.get('$timeout');
    var blockUi = $injector.get("blockUI");
    var taxaBoleto = 0;
    var nomeCookie = '';
    $rootScope.LojaId = $state.params.LojaId;
    var doSubmit = false;
    $scope.TotalFrente = 0;
    $scope.pagamento = {
        Id: 0,
        TipoPagamento: 4,
        Documento: "",
        TipoDocumento: 0,
        NroCartao: "",
        CodigoSeguranca: "",
        NomeTitular: "",
        MesVencimento: "",
        AnoVencimento: "",
        Parcelas: 1,
        Email: "",
        Token: "",
        AccessToken: "",
        ValorCompra: "",
        Descricao: "Compra feita no mercado pago",
        CodigoCupom: "",
        ValorCupom: 0,
        MetodoPagamento: "",
        Status: 0,
        DetalheStatus: "",
        Pagador: {},
        InformacoesAdicionais: {},
        PublicKey: "",
        TotalPagar: 0,
        TipoEnvio: "0",
        TipoFrete: "",
        ValorFrete: 0,
        TaxaBoleto: 0,
        CarrinhoId: "",
        LojaId: "",
        PrazoEntrega: "",
        DataEntrega: "",
        QuatroUltimoDigitos: "",
        BandeiraCartao: ""
    };

    $scope.pagador = {
        Id: 0,
        Email: "",
        PrimeiroNome: "",
        UltimoNome: "",
        Type: 3,
        TipoEntidade: 0,
        Endereco: {},
        Whatsapp: ""
    }

    $scope.Endereco = {
        Cep: "",
        Logradouro: "",
        Complemento: "",
        Numero: "",
        Bairro: "",
        Cidade: "",
        Estado: "Selecione",
        CodigoEstado: 0,
        CodigoCidade: 0,
        Ibge: ""
    };

    $scope.item = {
        Id: 0,
        Titulo: "",
        Descricao: "",
        Quantidade: 1,
        PrecoUnitario: 0
    };

    $scope.pagadorIdentificacao = {
        NumeroDocumento: "",
        TipoDoc: "",
    };

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

    $scope.ListaTipoEnvio = [
        { id: "0", nome: "Selecione" },
        { id: "1", nome: "Retirada" },
        { id: "04014", nome: "SEDEX à vista" },
        { id: "04510", nome: "PAC à vista" },
        { id: "04782", nome: "SEDEX 12 ( à vista)" },
        { id: "04790", nome: "SEDEX 10 (à vista)" },
        { id: "04804", nome: "SEDEX Hoje à vista" }
    ];

    $scope.IdCarrinho = $state.params.Id;

    $scope.tiposDocumentos = [];
    $scope.TamanhoMaximoDocumento = 14;
    $scope.MascaraDocumento = "000.000.000-00";
    $scope.LabelTitular = "Títular do cartão*";
    $scope.PlaceHolderTitular = "Informe o nome igual ao cartão";
    $scope.PagamentoRetira = false;
    var removeMascara = function (value) {
        var result = value.toString().replace(/ /g, '').replace(/\.+$/, "").replace('-', '').replace('/', '').replace('string', '');
        return result;
    };

    var getLoja = function () {
        lojaService.BuscarLoja($rootScope.LojaId).then(function (result) {
            var loja = result.data;
            $rootScope.NomeLoja = loja.Nome;
            $rootScope.Whatsapp = loja.Whatsapp;
            $scope.pagamento.AccessToken = loja.AccessToken;
            $scope.pagamento.PublicKey = loja.PublicKey;
            $scope.PagamentoRetira = loja.PagamentoRetira;
            $scope.cepOrigem = loja.CEP;

            $rootScope.QuemSomos = loja.QuemSomos;
            $rootScope.PoliticaPrivacidade = loja.PoliticaPrivacidade;
            $rootScope.PoliticaTrocaDevolucao = loja.PoliticaTrocaDevolucao;
            taxaBoleto = parseFloat(loja.TaxaBoleto);
            window.Mercadopago.setPublishableKey($scope.pagamento.PublicKey);

            if (loja.Logo) {
                if (loja.Logo !== undefined || loja.Logo !== "") {
                    var urlImage = window.location.origin + window.location.pathname + "/image/" + loja.Logo;
                    var cabecalho = document.getElementById("cabecalho");
                    cabecalho.style.backgroundImage = 'url(' + urlImage + ')';
                }
            }

            getCarrinho();
        });

    };

    var getCarrinho = function () {
        carrinhoService.AbrirCarrinho($scope.IdCarrinho).then(function (result) {
            $rootScope.CarrinhoCompra = result.data;
            var valorTotal = 0;
            for (var i = 0; i < result.data.length; i++) {
                valorTotal += result.data[i].ValorTotal;
            }
            $scope.pagamento.ValorCompra = valorTotal;
            window.fbq('track', 'InitiateCheckout', { currency: "BRL", value: valorTotal })
            calcularValorTotal();
            getTiposDocumentos();
        });
    };

    var getTiposDocumentos = function () {
        pagamentoService.RecuperarTipoDocumento($scope.pagamento.PublicKey).then(function (data) {
            $scope.tiposDocumentos = data.data;
            blockUi.stop();
        });
    };

    $scope.Init = function () {
        blockUi.start("Aguarde. Carregando os dados para o pagamento");
        $scope.pagamento.CarrinhoId = $scope.IdCarrinho;
        $scope.pagamento.LojaId = $rootScope.LojaId;
        getLoja();
    };

    $scope.tipoPagamento = function (tipoPagamento) {
        $scope.pagamento.TipoPagamento = tipoPagamento;
        $scope.pay.$setPristine();
        doSubmit = false;
        switch (tipoPagamento) {
            case 0:
                $scope.pagamento.MetodoPagamento = "";
                $scope.LabelTitular = "Nome completo*";
                $scope.PlaceHolderTitular = "Informe o nome do comprador";
                $scope.pagamento.TaxaBoleto = parseFloat(0);
                $scope.pagamento.TipoEnvio = "1";
                break;
            case 1:
                $scope.pagamento.MetodoPagamento = "bolbradesco";
                $scope.LabelTitular = "Nome completo*";
                $scope.PlaceHolderTitular = "Informe o nome do comprador";
                $scope.pagamento.TaxaBoleto = parseFloat(taxaBoleto);
                $scope.pagamento.TipoEnvio = "0";
                break;
            case 4:
                $scope.pagamento.MetodoPagamento = "";
                $scope.LabelTitular = "Títular do cartão*";
                $scope.PlaceHolderTitular = "Informe o nome igual ao cartão";
                $scope.pagamento.TaxaBoleto = parseFloat(0);
                $scope.pagamento.TipoEnvio = "0";
                break;
            default:
                Messaging.Information("Tipo de pagamento", "Selecione um tipo de pagamento");
                break;
        }
        calcularValorTotal();
    }

    $scope.TamanhoDocumento = function (tipo) {
        var tipoSelecionado = $filter("filter")($scope.tiposDocumentos, { id: tipo }, true)[0];
        $scope.pagadorIdentificacao.NumeroDocumento = "";
        $scope.TamanhoMaximoDocumento = parseInt(tipoSelecionado.max_length) + 6;
        var element = document.getElementById("docNumber");
        if (tipoSelecionado.id === "CNPJ") {
            $scope.MascaraDocumento = "00.000.000/0000-00";
            element.setAttribute("data-mask", "00.000.000/0000-00");
        }
        else {
            $scope.MascaraDocumento = "000.000.000-00";
            element.setAttribute("data-mask", "000.000.000-00");
        }

    };


    $scope.guessPaymentMethod = function (value) {
        if (Number(value).toString() !== "NaN") {
            if (value.length >= 6) {
                let bin = value.substring(0, 6);
                try {
                    window.Mercadopago.getPaymentMethod({
                        "bin": bin
                    }, setPaymentMethod);
                }
                catch (e) {
                    Messaging.Error("Erro metodo pagamento", e);
                }
            }
        }
        else {
            $scope.pagamento.MetodoPagamento = "";
        }
    };

    function setPaymentMethod(status, response) {
        if (status == 200) {
            $scope.pagamento.MetodoPagamento = response[0].id;
            getInstallments();
        } else {
            $scope.pagamento.MetodoPagamento = "";
            var mensagem = exibirErroResponse(response);
            Messaging.Information("Erro", mensagem);
        }
    }

    function getInstallments() {
        window.Mercadopago.getInstallments({
            "payment_method_id": $scope.pagamento.MetodoPagamento,
            "amount": parseFloat($scope.pagamento.TotalPagar)

        }, function (status, response) {
            if (status !== 200) {
                var mensagem = exibirErroResponse(response);
                Messaging.Information("Erro", mensagem);
            }
            $scope.CalculoParcelas = response[0].payer_costs;
        }, function (error) {
            Messaging.Error("Erro calcular parcelas", error.data.ExceptionMessage);
        });
    }

    $scope.Pagar = function (form, event) {
        event.preventDefault();
        event.stopPropagation();
        if ($scope.pagamento.TipoEnvio == "0") {
            Messaging.Information("Tipo de envio", "Informe o tipo de envio válido");
            return;
        }
        if ($scope.Endereco.Estado == "Selecione") {
            Messaging.Information("UF", "Informe o UF válido");
            return;

        }
        window.fbq('track', 'AddPaymentInfo', { currency: "BRL", value: $scope.pagamento.ValorCompra })
        switch ($scope.pagamento.TipoPagamento) {
            case 0:
                FinalizarCompra(form);
                break;
            case 1:
                PagarBoleto(form);
                break;
            case 4:
                if (!doSubmit && form.$valid) {
                    var f = document.querySelector('#pay');
                    window.Mercadopago.createToken(f, sdkResponseHandler);
                    return false;
                }
                break;
            default:
                Messaging.Information("Pagamento", "Selecione uma forma de pagamento");
                break;

        }
    };

    var FinalizarCompra = function (form) {
        if (!doSubmit && form.$valid) {
            completarPagamento("", "");
            pagamentoService.FinalizarCompra($scope.pagamento).then(function (result) {
                var f = document.querySelector('#pay');
                f.reset();
                doSubmit = true;
                nomeCookie = $rootScope.CarrinhoCompra[0].LojaId + $rootScope.CarrinhoCompra[0].TokenProdutos;
                cookies.apagarCookies(nomeCookie, $scope.IdCarrinho);
                Messaging.Information("Compra finalizada", "Compra finalizada com êxito. Você irá receber um e-mail com a confirmação da compra.");
                $state.go("home", { LojaId: $rootScope.CarrinhoCompra[0].LojaId, TokenProdutos: $rootScope.CarrinhoCompra[0].TokenProdutos });
            }, function (erro) {
                Messaging.ShowMessaging(erro);
                doSubmit = false;
            });
        }
    }

    var PagarBoleto = function (form) {
        if (!doSubmit && form.$valid) {
            completarPagamento("", "");
            pagamentoService.Pagar($scope.pagamento).then(function (data) {
                var f = document.querySelector('#pay');
                var result = data.data;
                f.reset();
                doSubmit = true;
                nomeCookie = $rootScope.CarrinhoCompra[0].LojaId + $rootScope.CarrinhoCompra[0].TokenProdutos;
                Messaging.Information("Pagamento", result.TextoStatusPagamento + '\n' + result.Url);
                window.open(result.Url);
                cookies.apagarCookies(nomeCookie, $scope.IdCarrinho);
                $state.go("home", { LojaId: $rootScope.CarrinhoCompra[0].LojaId, TokenProdutos: $rootScope.CarrinhoCompra[0].TokenProdutos });
            }, function (erro) {
                Messaging.ShowMessaging(erro);
                doSubmit = false;
            });
        }
    };

    var splitNomeTitular = function (nomeCompleto) {
        var arrayNomes = nomeCompleto.split(" ");
        var primeiroNome = arrayNomes[0];
        var segundoNome = "";
        var retorno = [];
        for (i = 1; i < arrayNomes.length; i++) {
            segundoNome += arrayNomes[i] + " ";
        }
        retorno = [primeiroNome, segundoNome];
        return retorno;
    };

    function sdkResponseHandler(status, response) {
        if (status != 200 && status != 201) {
            if (response != undefined && response.cause != undefined) {
                if (response.cause[0].code == "011") {
                    Messaging.Information("Mercado Pago", "Tivemos um erro ao integrar o pagamento, atualize a página e tente novamente.");
                }
                else {
                    var mensagem = exibirErroResponse(response);
                    console.log(mensagem);
                    Messaging.Error("Error criar token cartão", mensagem);
                }
            }
        } else {
            completarPagamento(response.id, response.last_four_digits);
            pagamentoService.Pagar($scope.pagamento).then(function (data) {
                var f = document.querySelector('#pay');
                var result = data.data;
                if (result.StatusPagamento === 1) {
                    f.reset();
                    doSubmit = true;
                    nomeCookie = $rootScope.CarrinhoCompra[0].LojaId + $rootScope.CarrinhoCompra[0].TokenProdutos;
                    Messaging.Information("Pagamento", result.TextoStatusPagamento);
                    cookies.apagarCookies(nomeCookie, $scope.IdCarrinho);
                    $state.go("home", { LojaId: $rootScope.CarrinhoCompra[0].LojaId, TokenProdutos: $rootScope.CarrinhoCompra[0].TokenProdutos });
                }
                else {
                    Messaging.Information("Pagamento", result.TextoStatusPagamento);
                    doSubmit = false;
                }
            }, function (erro) {
                Messaging.ShowMessaging(erro);
                doSubmit = false;
            });
        }
    };
    var completarPagamento = function (tokenCartao, quatroDigitos) {
        preencherItens();
        $scope.Endereco.Cep = removeMascara($scope.Endereco.Cep);
        $scope.pagamento.TokenCartao = tokenCartao;
        $scope.pagamento.QuatroUltimoDigitos = quatroDigitos;
        $scope.pagamento.BandeiraCartao = $scope.pagamento.MetodoPagamento;
        $scope.pagador.PrimeiroNome = splitNomeTitular($scope.pagamento.NomeTitular)[0].trim();
        $scope.pagador.UltimoNome = splitNomeTitular($scope.pagamento.NomeTitular)[1].trim();
        $scope.pagador.Identificacao = $scope.pagadorIdentificacao;
        $scope.pagador.Endereco = $scope.Endereco;
        $scope.pagamento.Pagador = $scope.pagador;
        $scope.pagamento.InformacoesAdicionais.Itens = [$scope.item];
    };

    var preencherItens = function () {
        $scope.item.Titulo = $scope.pagamento.Descricao;
        $scope.item.Descricao = $scope.pagamento.Descricao;
        $scope.item.Quantidade = 1;
        $scope.item.PrecoUnitario = parseFloat($scope.pagamento.ValorCompra);
    };

    var exibirErroResponse = function (response) {
        if (response.error == undefined)
            return '';
        var error = {};
        if (response !== null) {
            error.erro = response.error;
            error.mensagem = response.message;
            for (i = 0; i < response.cause.length; i++) {
                if (error.motivo === undefined) {
                    error.motivo = "";
                }
                error.motivo += response.cause[i].description + ";";
            }
        }
        return error.mensagem + "/" + error.motivo;
    }

    $scope.pesquisaCep = function (event) {
        try {
            event.preventDefault();
            event.stopPropagation();
            var cepPesquisa = removeMascara($scope.Endereco.Cep);
            if ($scope.Endereco.Cep == "" || $scope.Endereco.Cep == undefined || cepPesquisa.length < 8) {
                Messaging.Information("Pesquisar CEP", "Informe um CEP válido");
                var cep = document.getElementById("cep");
                cep.focus();
                return;
            }
            pagamentoService.PesquisaCep(removeMascara($scope.Endereco.Cep)).then(function (result) {
                var data = result.data;
                if (data.erro == undefined) {
                    $scope.Endereco.Logradouro = data.logradouro;
                    $scope.Endereco.Complemento = data.complemento;
                    $scope.Endereco.Bairro = data.bairro;
                    $scope.Endereco.Cidade = data.localidade;
                    $scope.Endereco.Estado = data.uf;
                    $scope.Endereco.Ibge = data.ibge;
                    var num = document.getElementById("numero");
                    num.focus();
                    if ($scope.pagamento.TipoEnvio !== "0" && $scope.pagamento.TipoEnvio !== "1") {
                        $scope.calculaFrete();
                    }
                }
                else {
                    $scope.Endereco.Logradouro = '';
                    $scope.Endereco.Complemento = '';
                    $scope.Endereco.Bairro = '';
                    $scope.Endereco.Cidade = '';
                    $scope.Endereco.Estado = 'Selecione';
                    $scope.Endereco.Ibge = '';
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

    $scope.calculaFrete = function () {
        if ($scope.pagamento.TipoEnvio == "0") {
            Messaging.Information("Tipo de envio", "Informe o tipo de envio válido");
            return;
        }
        if ($scope.pagamento.TipoEnvio == "1") {
            $scope.TotalFrente = 0;
            calcularValorTotal();
            return;
        }
        if ($scope.Endereco.Cep == "" || $scope.Endereco.Cep == undefined) {
            Messaging.Error("Cep destino", "Informe o de destino");
            return;
        }
        var calcFrete = {
            nCdEmpresa: "",
            sDsSenha: "",
            sCepOrigem: removeMascara($scope.cepOrigem),
            sCepDestino: removeMascara($scope.Endereco.Cep),
            Formato: 1,
            nCdServico: $scope.pagamento.TipoEnvio,
            nVlPeso: "1",
            nVlComprimento: "20",
            nVlAltura: "20",
            nVlLargura: "20",
            nVlDiametro: "0",
            sCdMaoPropria: "N",
            nVlValorDeclarado: "0",
            sCdAvisoRecebimento: "N"
        };
        blockUi.start("Calculando o valor do frete");
        pagamentoService.CalculaFrete(calcFrete).then(function (result) {
            var data = result.data;
            $scope.TotalFrente = parseFloat(data.Valor);
            $scope.pagamento.PrazoEntrega = data.PrazoEntrega;
            $scope.pagamento.DataEntrega = data.DataMaxEntrega;
        }, function (error) {
            $scope.pagamento.TipoEnvio = "0";
            $scope.TotalFrente = 0;
            $scope.pagamento.PrazoEntrega = "";
            $scope.pagamento.DataEntrega = "";
            throw error;
        }).finally(function () {
            calcularValorTotal();
            blockUi.stop();
        });
    };

    var calcularValorTotal = function () {
        $scope.pagamento.TotalPagar = $scope.pagamento.ValorCompra + $scope.TotalFrente + $scope.pagamento.TaxaBoleto;
        $scope.pagamento.ValorFrete = $scope.TotalFrente;
        $scope.pagamento.TipoFrete = $filter("filter")($scope.ListaTipoEnvio, { id: $scope.pagamento.TipoEnvio }, true)[0].nome;
        if ($scope.pagamento.TipoPagamento == 4 && ($scope.pagamento.NroCartao !== undefined && $scope.pagamento.NroCartao !== '')) {
            $scope.guessPaymentMethod($scope.pagamento.NroCartao);
        }
    };

    $rootScope.AbrirCarrinho = function (event, carrinho) {
        event.preventDefault();
        $state.go("carrinho", { LojaId: carrinho[0].LojaId, Id: carrinho[0].Id });
    };
}]);