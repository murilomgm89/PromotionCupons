$(document).ready(function () {
    $('#cpf').inputmask({
        mask: ['999.999.999-99'],
        keepStatic: true
    });
    $('#btnEnviar').click(function (e) {
        if (!validateForm())
            return;
        e.preventDefault();       
        var fdata = new FormData();
        fdata.append("cpf", $('#cpf')[0].value);
        fdata.append("email", $('#email')[0].value);
        fdata.append("name", $('#fname')[0].value);
        if ($("#file")[0].files.length > 0)
            fdata.append("ReceiptFile", $("#file")[0].files[0]);

        $.ajax({
            url: '../Promotion/receipt/save',
            type: 'POST',
            data: fdata,
            async: false,
            beforeSend: function (data) {

            },
            success: function (data) {
                if (data == "sucesso") {
                    $("#MsgErro").html("Participação realizada.");
                }
                if (data == "error_blockedCPF") {
                    $("#MsgErro").html("CPF Bloqueado para participações");
                }
                if (data == "error_participation_computed") {
                    $("#MsgErro").html("CPF já participou!");
                }
                if (data == "error_upload_not_valid") {
                    $("#MsgErro").html("A imagem escolhida não é válida");
                }
                if (data == "error") {
                    $("#MsgErro").html("Tente novamente mais tarde");
                }
                $("#MsgErro").show();
            },
            error: function (data) {
                $("#MsgErro").html("Tente novamente mais tarde");
                $("#MsgErro").show();
            },
            cache: false,
            contentType: false,
            processData: false
        });
    });
    $('#rules').click(function (e) {
        $('#containerRules').removeClass('error');
    });
    $('#file').click(function (e) {
        $('#SpanImage').removeClass('error');
    });
});
function validateForm() {
    var isValid = true;
    if (!ValidateCPF()) {
        isValid = false;
    }
    if (!ValidateMail()) {
        isValid = false;
    }
    if (!ValidateName()) {
        isValid = false;
    }
    if ($("#file")[0].files.length < 1) {
        $('#SpanImage').addClass('error');
        isValid = false;
    }
    if (!$('#rules').is(':checked')) {
        $('#containerRules').addClass('error');
        isValid = false;
    } else {
        $('#containerRules').removeClass('error');
    }
    return isValid;
}
function ValidateCPF() {
    var cpfValue = document.forms["intimus"]["cpf"].value;
    if (cpfValue != '123.123.123-89') {
        cpfValue = cpfValue.replace(/\D+/g, '');
        var i, j, s, r;
        if (cpfValue.length !== 11 || cpfValue.match(/^(\d)\1+$/)) {
            $(cpf).addClass('error');
            return false;
        }
        for (i = 0; i < 2; i++) {
            s = 0;
            for (j = 0; j <= 9 + i; j++)
                s += window.parseInt(cpfValue.substring(j - 1, j) || 0) * (11 + i - j);
            r = (s * 10) % 11;
            r = (r === 10) || (r === 11) ? 0 : r;
            if (r !== window.parseInt(cpfValue.substring(9 + i, 10 + i))) {
                $(cpf).addClass('error');
                return false;
            } else {
                $(cpf).removeClass('error');
            }
        }
    }
    return true;
}
function ValidateMail() {
    var mail = document.forms["intimus"]["email"]
    if (mail.value != "") {
        var RegExp = /^[\w]+@[\w]+\.[\w|\.]+$/;
        if (RegExp.test(mail.value) == true) {
            $(mail).removeClass('error');
            return true;
        } else {
            $(mail).addClass('error');
            return false;
        }
    }
    $(mail).addClass('error');
    return false;    
}
function ValidateName() {
    var name = document.forms["intimus"]["fname"]; 
    if (name.value == "" || name.value < 3) {
        $(name).addClass('error');
        return false;
    } else {
        $(name).removeClass('error');
        return true;
    }
}

