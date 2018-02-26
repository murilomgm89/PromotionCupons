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
  if (cpfValue !== "" || cpfValue !== undefined) {
  	cpf.classList.add("error");
  	return false;
  }

  if ($("#file")[0].files.length < 1) {
      $("#MsgErro").html("Cadastre sua imagem");
      $("#MsgErro").show();
    return false;
  }
  return true;
}


