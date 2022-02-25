function converteDataJsonParaJavascript(value) {
    if (value == null)
        return null;
    var dataTexto = value.replace('/', '').replace('/', '').replace('Date', '').replace('(', '').replace(')', '');
    var date = new Date(parseInt(dataTexto));
    var dia = date.getDate();
    var mes = date.getMonth() + 1;
    var ano = date.getFullYear();
    if (mes != 10 && mes != 11 && mes != 12) {
        mes = "0" + mes;
    }

    return dia + "/" + mes + "/" + ano;
};

$(document).ready(function () {
$("#canalYoutube").load("/home/ListarVideos");
    $.ajax({
        type: 'GET',
        url: '/Home/Cursos',
        async: true,
        datatype: "html",
        success: function (data) {
var html="<ul>";
            for (var dados in data) {
                html += "<li><h3>" + data[dados].Nome + "</h3>";
                html += "<p>" + data[dados].Descricao + "<br>";
                html += "<b>Duração: " + data[dados].TempoPrevistoDuracao + " Meses</b><br>";
                html += "<b>Inicia em: " + converteDataJsonParaJavascript(data[dados].DataInicio) + "</b><br>";
                html += "<a href='/Curso/Detalhe?id="+data[dados].Id+"'>Saber Mais Informações</a>";
if (data[dados].AceitaMatricula)
{
                html += "&nbsp;&nbsp;&nbsp;<a href='/Curso/Inscrever?idCurso="+data[dados].Id+"'>Inscrever-se neste curso</a>";
}
else
{
                html += "<strong>Não existem turmas abertas para este curso no momento.</strong>";
}
html += "</p></li>";
            }
html += "</ul>";
            $("#cursos").html(html);

        }
    });
});

