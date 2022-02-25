$(document).ready(function () {
    $.ajax({
        type: 'GET',
        url: '/Fornecedor/Listar',
        async: true,
        datatype: "html",
        success: function (data) {
            var html = "<table border='0' cellpadding='0' cellspacing='0' style='width:100%;'>";
            html += "<tbody>";
            html += "<tr class='tab-titulo'>";
            html += "<td>Nome</td><td>Data de cadastro</td><td>Data de alteração</td><td>Usuário que cadastrou</td><td>Usuário que alterou</td><td>Endereço</td><td>CEP</td><td>UF</td><td>Cidade</td>";
            html += "<td>Número</td><td>Complemento</td>";
            html += "</tr>";
            html += "<tr>";

            for (var dados in data) {
                html += "<td>" + data[dados].Nome + "</td>";
                html += "<td>" + converteDataJsonParaJavascript(data[dados].DataCadastro) + "</td>";
                html += "<td>" + converteDataJsonParaJavascript(data[dados].DataAlteracao) + "</td>";
                html += "<td>" + data[dados].UsuarioCadastro + "</td>";
                html += "<td>" + data[dados].UsuarioAlteracao + "</td>";
                html += "<td>" + data[dados].Endereco + "</td>";
                html += "<td>" + data[dados].Cep + "</td>";
                html += "<td>" + data[dados].Uf + "</td>";
                html += "<td>" + data[dados].Cidade + "</td>";
                html += "<td>" + data[dados].Numero + "</td>";
                html += "<td>" + data[dados].Complemento + "</td>";
            }
            html += "</tr></tbody></table>";
            $("#fornecedores").html(html);

        }
    });
});

function cadastrar() {
    $.ajax({
        type: "POST",
        url: '/Fornecedor/Cadastra',
        async: true,
        datatype: "html",
        data: {
            nome: $("#nome").val(),
            usuarioCadastro: 'ejricardogomes',
            endereco: $("#endereco").val(),
            cep: $("#cep").val(),
            uf: $("#uf").val(),
            cidade: $("#cidade").val(),
            numero: $("#numero").val(),
            complemento: $("#complemento").val()
        },
        success: function (data) {
            if (!data) {
                toastr.info("Problema ao executar operação", "Atenção");
            }
            else {
                swal({
                    title: "Êxito",
                    text: "Dados salvos com sucesso.",
                    icon: "info"
                }).then(() => { document.location.href = "/fornecedor"; });

            }
        }
    });
}

