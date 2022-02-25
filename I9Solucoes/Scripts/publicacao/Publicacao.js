function salvar(){
var titulo=$("#titulo").val();
var texto=$("#texto").val();
if (titulo =="" || texto =="")
{alert("Favor preencher o título e o conteúdo da publicação.");
return;}

$("#alerta").html("<span>Aguarde enquanto salvamos sua publicação...</span>");
$("#btnSalvar").prop("disabled",true);
    $.ajax({
        type: 'POST',
        url: "/Publicacao/Cadastrar",
        async: true,
        datatype: "html",
data:{
titulo: titulo,
texto: texto
},
        success: function (data) {
if (data.ExisteErro)
{$("#alerta").html("<span>"+data.Mensagem+"</span>");
$("#btnSalvar").prop("disabled",false);}
else
{
alert(data.Mensagem);
document.location.href="/home";
            }


        }
    });

}

