function salvar(){
var nome=$("#nome").val();
var dataNascimento=$("#dataNascimento").val();
var cpf=$("#cpf").val();
var sexo=$("#sexo").val();
var mail=$("#mail").val();
var celular=$("#celular").val();
var whatsapp=$("#whatsapp").val();
var senha=$("#criarSenha").val();
var confirmarSenha=$("#confirmarSenha").val();
if (nome=="")
{$("#alerta").html("<span>Favor preencher o campo nome.</span>");
return;}
else if (dataNascimento=="")
{$("#alerta").html("<span>Favor preencher o campo data de nascimento.</span>");
return;}
else if (cpf=="")
{$("#alerta").html("<span>Favor preencher o campo CPF.</span>");
return;}
else if (sexo=="")
{$("#alerta").html("<span>Favor preencher o campo sexo.</span>");
return;}
else if (mail=="")
{$("#alerta").html("<span>Favor preencher o campo e-mail.</span>");
return;}
else if (celular=="")
{$("#alerta").html("<span>Favor preencher o campo celular.</span>");
return;}
else if (whatsapp=="")
{$("#alerta").html("<span>Favor preencher o campo informando se seu celular é ou não é Whatsapp.</span>");
return;}
else if (senha!=confirmarSenha)
{$("#alerta").html("<span>As senhas digitadas não são iguais.</span>");
return;}

else if (senha=="")
{$("#alerta").html("<span>Preencher a senha.</span>");
return;}
else if (confirmarSenha=="")
{$("#alerta").html("<span>Preencher a confirmação da senha.</span>");
return;}
$("#alerta").html("<span>Aguarde enquanto cadastramos você...</span>");
$("#btnSalvar").prop("disabled",true);
    $.ajax({
        type: 'POST',
        url: "/Cadastro/Salvar",
        async: true,
        datatype: "html",
data:{
nome: nome,
dataNascimento: dataNascimento,
cpf: cpf,
sexo: sexo,
email: mail,
celular: celular,
whatsapp: whatsapp,
senha: senha
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


        },
      error: function (xhr, ajaxOptions, thrownError) {
        alert("Ouve um erro ao acessar o recurso solicitado. Nossa equipe está ciente do problema e o resolverá o mais breve possível. Pedimos desculpa pelo transtorno.");
      }
    });

}

function completaData(){
var dataNascimento=document.formCadastro.dataNascimento.value.length;
if (dataNascimento==2)
{document.formCadastro.dataNascimento.value=document.formCadastro.dataNascimento.value+"/"}
else
if (dataNascimento==5)
{document.formCadastro.dataNascimento.value=document.formCadastro.dataNascimento.value+"/"}
}

function validaCelular(){
var celular = document.formCadastro.celular.value.length;
if (celular>11)
{alert("Você digitou mais caracteres do que devia para este campo. Informe apenas os dois dígitos do DDD e os 9 dígitos do celular.");
document.formCadastro.celular.value="";
document.formCadastro.celular.focus()}
}

function validaSenha()
{
var senha = document.formCadastro.criarSenha.value.length;
if (senha>10)
{alert("A senha não pode ter mais de 10 caracteres.");
document.formCadastro.criarSenha.value="";
document.formCadastro.criarSenha.focus()}
}

function validaCpf()
{
var cpf = document.formCadastro.cpf.value.length;
if (cpf>11)
{alert("O CPF não pode ter mais de 11 dígitos. Por favor, informe apenas os números, sem traços e pontos.");
document.formCadastro.cpf.value="";
document.formCadastro.cpf.focus()}
}