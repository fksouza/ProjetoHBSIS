// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

//Referência - https://www.devmedia.com.br/validar-cpf-com-javascript/23916

function ValidaCPF(strCPF) {
    var Soma;
    var Resto;
    Soma = 0;
    if (strCPF == "00000000000") {
        $("#alertCPF").text("CPF " + strCPF + " Inválido!");
        $("#cpf").val("");
        $("#cpf").focus();
        return false;
    };

    for (i = 1; i <= 9; i++) Soma = Soma + parseInt(strCPF.substring(i - 1, i)) * (11 - i);
    Resto = (Soma * 10) % 11;

    if ((Resto == 10) || (Resto == 11)) Resto = 0;
    if (Resto != parseInt(strCPF.substring(9, 10))) {
        $("#alertCPF").text("CPF " + strCPF + " Inválido!");
        $("#cpf").val("");
        $("#cpf").focus();
        return false;
    }

    Soma = 0;
    for (i = 1; i <= 10; i++) Soma = Soma + parseInt(strCPF.substring(i - 1, i)) * (12 - i);
    Resto = (Soma * 10) % 11;

    if ((Resto == 10) || (Resto == 11)) Resto = 0;
    if (Resto != parseInt(strCPF.substring(10, 11))) {
        $("#alertCPF").text("CPF " + strCPF + " Inválido!");
        $("#cpf").val("");
        $("#cpf").focus();
        return false;
    }

    $("#alertCPF").text("")
    return true;
}