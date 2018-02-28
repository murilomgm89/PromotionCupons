function validateForm() {
  var name = document.forms["intimus"]["fname"]
  var mail = document.forms["intimus"]["email"]
  var cpf = document.forms["intimus"]["cpf"]

  var nameValue = document.forms["intimus"]["fname"].value;
  var mailValue = document.forms["intimus"]["email"].value;
  var cpfValue = document.forms["intimus"]["cpf"].value;

  if (nameValue == "" || nameValue < 3) {
    name.classList.add("error");
    return false;
  }

  if (!ValidateCPF()) {
    return false;
  }
  
  if ($("#file")[0].files.length < 1) {
      $("#MsgErro").html("Cadastre sua imagem");
      $("#MsgErro").show();
    return false;
  }
  return true;
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
}

