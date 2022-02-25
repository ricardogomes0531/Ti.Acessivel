function enviar() {
    var email = $("#email").val();
    if (email == "") {
        $("#alerta").html("<span>Favor preencher o campo e-mail.</span>");
        return;
    }
    $("#alerta").html("<span>Aguarde enquanto enviamos o e-mail para você...</span>");
    $("#btnSalvar").prop("disabled", true);
    $.ajax({
        type: 'POST',
        url: "/Lead/SalvarEEnviar",
        async: true,
        datatype: "html",
        data: {
            email: email
        },
        success: function (data) {
            if (data.ExisteErro) {
                $("#alerta").html("<span>" + data.Mensagem + "</span>");
                $("#btnSalvar").prop("disabled", false);
            }
            else {
                alert(data.Mensagem);
                document.location.href = "/home";
            }


        }
    });

}
