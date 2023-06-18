function inscrever(){
var idCurso=$("#idCurso").val();
var tempoAssinatura=$("#tempoAssinatura").val();
$("#botaoInscrever").prop("disabled","true");
    $.ajax({
        type: 'POST',
        url: "/Curso/InscreverNoCurso",
        async: true,
        datatype: "html",
data:{
idCurso: idCurso,
tempoAssinatura: tempoAssinatura
},
        success: function (data) {
if (!data.ExisteErro)
{
$("#alerta").html("<h2>"+data.Mensagem+"</h2><br><a href='/home/meuscursos'>Ir para meus cursos</a>");
$("#inscricao").hidd();}
else
{
$("#botaoInscrever").prop("enabled","true");
$("#alerta").html("<h2>"+data.Mensagem+"</h2><a href='javascript:window.history.go(-1)'>Voltar</a>");}
        }
    });
}


function inscreverGratis(){
var idCurso=$("#idCurso").val();
var tempoAssinatura=$("#tempoAssinatura").val();
var demonstracao=$("#codigoDemonstracao").val();
$("#botaoInscreverGratis").prop("disabled","true");
$("#botaoInscrever").prop("disabled","true");
    $.ajax({
        type: 'POST',
        url: "/Curso/InscreverNoCursoDemonstrativo",
        async: true,
        datatype: "html",
data:{
idCurso: idCurso,
tempoAssinatura: tempoAssinatura,
codigoDemonstracao: demonstracao
},
        success: function (data) {
if (!data.ExisteErro)
{
$("#alerta").html("<h2>"+data.Mensagem+"</h2><br><a href='/home/meuscursos'>Ir para meus cursos</a>");
$("#inscricao").hidd();}
else
{
$("#botaoInscrever").prop("enabled","true");
$("#botaoInscreverGratis").prop("enabled","true");
$("#alerta").html("<h2>"+data.Mensagem+"</h2><a href='javascript:window.history.go(-1)'>Voltar</a>");}
        }
    });
}