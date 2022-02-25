$(document).ready(function () {
    $.ajax({
        type: 'GET',
        url: '/Fornecedor/Listar',
        async: true,
        datatype: "html",
        success: function (data) {
            var html = "<option value=''>Selecione...</option>";
            for (var dados in data) {
                html += "<option value='" + data[dados].Id + "'>"+data[dados].Nome+"</option > ";
            }
            $("#fornecedor").html(html);

        }
    });
});
