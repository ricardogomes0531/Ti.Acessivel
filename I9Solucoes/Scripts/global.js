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
