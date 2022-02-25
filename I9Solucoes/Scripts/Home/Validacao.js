function validarLogin() {
    var email = $("#email").val();
    var senha = $("#senha").val();
if (email=="" || senha=="")
{$("#resultadoLogin").html("<span role='alert'>É necessário preencher e-mail e senha para fazer login.</span>");
return;}
    $.ajax({
        type: "POST",
        url: '/Home/Logar',
        async: false,
        datatype: "html",
        data: {
            email: email,
            senha: senha
        },
        success: function (data) {
            if (!data) {
$("#resultadoLogin").html("<span role='alert'>E-mail ou senha inválidos.</span>");
            }
            else {
                document.location.href = "/home/MeusCursos";
                        }
            }
        });
}

