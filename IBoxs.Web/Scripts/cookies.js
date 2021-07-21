var cookies = new function () {

    this.apagarCookies = function (nome, id) {
        var cname = nome + '=';
        var cookies = document.cookie;

        // Verifica se seu cookie existe
        if (cookies.indexOf(cname) != -1) {
            // Cria uma data no passado 01/01/2010
            var data = new Date(2010, 0, 01);
            // Converte a data para GMT
            data = data.toGMTString();
            var valor = encodeURI(id);
            document.cookie = cname + valor + '; expires=' + data;
        }
    }

    this.verificarCookies = function (nome) {
        //var cname = nome + '=';
        //var cookies = document.cookie;

        //// Verifica se seu cookie existe
        //if (cookies.indexOf(cname) >= 0) {
        //    cookies = cookies.substr(cookies.indexOf(cname), cookies.length);
        //    cookies = cookies.split('=')[1];
        //    return decodeURI(cookies);
        //}

        var cname = nome + '=';
        var cookies = document.cookie;
        var teste = cookies.split(';');
        for (i = 0; i <= teste.length - 1; i++) {
            var outroteste = teste[i].split('=');
            if (outroteste.length > 0 && outroteste[0].trim() == nome.trim()) {
                return decodeURI(outroteste[1]);
            }
        }
    }

    this.criarCookies = function (nome, id) {
        var cname = nome + '=';
        var cookies = document.cookie;

        // Verifica se seu cookie existe
        if (cookies.indexOf(cname) == -1) {
            // Cria uma data 01/01/2020
            var data = new Date();
            data.setDate(data.getDate() + 1);
            // Converte a data para GMT
            data = data.toGMTString();
            // Codifica o valor do cookie para evitar problemas
            var valor = encodeURI(id);
            cname = cname + valor;
            document.cookie = cname;
        }
    };

    this.getJSessionId = function () {
        var jsId = document.cookie.match(/JSESSIONID=[^;]+/);
        if (jsId != null) {
            if (jsId instanceof Array)
                jsId = jsId[0].substring(11);
            else
                jsId = jsId.substring(11);
        }
        return jsId;
    }


}