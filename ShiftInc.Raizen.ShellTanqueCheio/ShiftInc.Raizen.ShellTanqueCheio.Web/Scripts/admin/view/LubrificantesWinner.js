$(window).load(function () {   
    $("#SearchLubrificantesWinner").click(function () {   
        $.ajax({
            url: '/api/lubrificantes/winners/' + $("#ValNumSort").val(),
            type: 'GET',
            data: "",
            cache: false,
            contentType: false,
            processData: false,
            beforeSend: function () {
            },
            success: function () {
            },
            error: function () {
            },
            complete: function (data) {
                $('#GridWinners').html(data.responseText);
            }
        });
    });

    $('#ValNumSort').keypress(function (event) {
        var tecla = (window.event) ? event.keyCode : event.which;
        if ((tecla > 47 && tecla < 58)) {           
            return true;
        }
        else {
            if (tecla != 8) return false;
            else return true;
        }
    });
})