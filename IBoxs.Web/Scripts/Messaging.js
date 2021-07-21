var deleteFunction = null;
var promptFunction = null;
var deactivateFunction = null;
var cancelPromptFunction = null;

var Messaging = new function () {
    var previousWindowKeyDown = window.onkeydown;
    this.Success = function (title) {
        window.swal.close();
        window.toastr.options = {
            "closeButton": false,
            "debug": false,
            "newestOnTop": false,
            "progressBar": false,
            "positionClass": "toast-bottom-center",
            "preventDuplicates": false,
            "onclick": null,
            "showDuration": "300",
            "hideDuration": "1000",
            "timeOut": "5000",
            "extendedTimeOut": "1000",
            "showEasing": "swing",
            "hideEasing": "linear",
            "showMethod": "fadeIn",
            "hideMethod": "fadeOut"
        };

        window.toastr["success"](title);
    };

    this.ShowMessaging = function (error)
    {
        if (error != undefined) {
            if (error.data != undefined) {
                if (error.data.Messages != undefined && error.data.Messages.length > 1) {
                    this.Error(error.data.Messages[0], error.data.Messages[1]);
                }
                else if (error.data.Messages != undefined && error.data.Messages.length > 0) {
                    this.Error("Erro", error.data.Messages[0]);
                }
            }
        }
    }

    this.Error = function (title, message) {
        swal({ title: title, text: message, type: "error", confirmButtonColor: "#00BFFF" });
    };

    this.Warning = function (btnConfirm, msgTextCuston) {
        // Display an error toast, with a title
        var msgconfirn = btnConfirm || window.Translate("btnConfirmDeactivate");
        var msgtext = msgTextCuston || window.Translate("msgNotRecoverRecord");

        swal({
            title: window.Translate("msgAreUSure"),
            text: msgtext,
            type: "warning",
            showCancelButton: true,
            closeOnConfirm: false,
            showLoaderOnConfirm: true,
            confirmButtonColor: "#00BFFF",
            cancelButtonText: window.Translate("btnCancelDeactivate"),
            confirmButtonText: msgconfirn
        },
            function () {
                window.onkeydown = previousWindowKeyDown;

                deactivateFunction();
                deactivateFunction = null;
            });
    };

    this.Information = function (title, message) {
        // Display an error toast, with a title
        swal({ title: title, text: message, type: "info" });
    };

    this.DeleteQuestion = function (btnConfirm, msgTextCuston) {

        var msgconfirn = btnConfirm || window.Translate("btnConfirmDelete");
        var msgtext = msgTextCuston || window.Translate("msgNotRecoverRecord");

        swal({
            title: window.Translate("msgAreUSure"),
            text: msgtext,
            type: "warning",
            showCancelButton: true,
            closeOnConfirm: false,
            showLoaderOnConfirm: true,
            confirmButtonColor: "#00BFFF",
            cancelButtonText: window.Translate("btnCancelDelete"),
            confirmButtonText: msgconfirn
        },
            function () {
                window.onkeydown = previousWindowKeyDown;

                deleteFunction();
                deleteFunction = null;
            });
    };

    this.Close = function () {
        window.swal.close();

        window.onkeydown = previousWindowKeyDown;
    };

    this.Prompt = function (title, text, textBtnOk, placeHolder, errorInput, inputType, inputValue) {
        swal({
            title: title,
            text: text,
            type: "input",
            inputType: inputType,
            inputValue: inputValue,
            showCancelButton: true,
            closeOnConfirm: false,
            animation: "slide-from-top",
            inputPlaceholder: placeHolder,
            cancelButtonText: window.Translate("btnAbandon"),
            confirmButtonText: textBtnOk,
            confirmButtonColor: "#00BFFF",
            showLoaderOnConfirm: true
        },
        function (inputVal) {
            window.onkeydown = previousWindowKeyDown;

            if (inputVal === false) return false;

            if (inputVal === "") {
                swal.showInputError(errorInput);
                return false;
            }

            promptFunction(inputVal);

            promptFunction = null;

            return true;
        });

    };

    this.GenericPrompt = function (title, text, msgTextConfirm, msgTextCancel) {
        swal({
            title: title,
            text: text,
            type: "info",
            showCancelButton: true,
            closeOnConfirm: false,
            closeOnCancel: false,
            animation: "slide-from-top",

            cancelButtonText: msgTextCancel === undefined ? window.Translate("btnNo") : msgTextCancel,
            confirmButtonText: msgTextConfirm === undefined ? window.Translate("btnYes") : msgTextConfirm,
            showLoaderOnConfirm: true
        },
            function (confirm) {
                window.onkeydown = previousWindowKeyDown;

                if (confirm) {
                    promptFunction();
                    promptFunction = null;
                    return true;
                } else {
                    cancelPromptFunction();
                    cancelPromptFunction = null;
                    return false;
                }
            });
    };

    this.SignalROffline = function () {
		window.toastr.options = {
			"closeButton": false,
			"debug": false,
			"newestOnTop": false,
			"progressBar": false,
			"positionClass": "toast-bottom-right",
			"preventDuplicates": true,
			"onclick": null,
			"showDuration": "0",
			"hideDuration": "0",
			"timeOut": 0,
			"extendedTimeOut": 0,
			"showEasing": "swing",
			"hideEasing": "linear",
			"showMethod": "fadeIn",
			"hideMethod": "fadeOut",
			"tapToDismiss": false
		};
		window.toastr["error"]('<input style="background: darkseagreen;" class="btn btn-dark" type="button" value="Refresh Page" onClick="window.location.reload()">', "Real time connection offline");
    };

    this.SignalRReconecting = function () {
		toastr.options = {
			"closeButton": false,
			"debug": false,
			"newestOnTop": false,
			"progressBar": false,
			"positionClass": "toast-bottom-right",
			"preventDuplicates": true,
			"onclick": null,
			"showDuration": "0",
			"hideDuration": "0",
			"timeOut": 0,
			"extendedTimeOut": 0,
			"showEasing": "swing",
			"hideEasing": "linear",
			"showMethod": "fadeIn",
			"hideMethod": "fadeOut",
			"tapToDismiss": false
		};
        window.toastr["warning"]("Real time re-connecting");
    };

    this.ClearToastr = function () {
        window.toastr.clear();
    };

    this.Notes = function (title, text, preventDuplicates, type, closeButton) {
        if(!type){
            type = 'info';
        }
        if(closeButton === undefined){
            closeButton = true;
        }

        toastr.options = {
            "closeButton": closeButton,
            "debug": false,
            "newestOnTop": false,
            "progressBar": false,
            "positionClass": "toast-top-right",
            "preventDuplicates": preventDuplicates,
            "onclick": null,
            "showDuration": "300",
            "hideDuration": "1000",
            "timeOut": "0",
            "extendedTimeOut": "0",
            "showEasing": "swing",
            "hideEasing": "linear",
            "showMethod": "slideDown",
			"hideMethod": "slideUp",
			"tapToDismiss": false
        }

        window.toastr[type](text, title);
    };
};