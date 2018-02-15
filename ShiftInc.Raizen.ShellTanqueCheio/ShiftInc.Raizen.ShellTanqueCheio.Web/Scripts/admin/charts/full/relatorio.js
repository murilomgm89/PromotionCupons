$(function () {	
	var TypeReport = 1;
	
	$(".info-blocks li").click(function () {	
		$(".info-blocks li span").attr("class", "bottom-info bg-primary");
		$(".info-blocks li").css('opacity', 0.4);
		$("#" + this.id + " span").attr("class", "bottom-info bg-danger");
		$("#" + this.id).css('opacity', 1);
		$("#VisaoDetalhada").hide();
		$("#VisaoDetalhada2").hide();
		if(this.id == "extracao"){
			TypeReport=1;
		}else{
			TypeReport=0;
		}
	});
	
	$("#btVisao").on('click', function () {
        $("#VisaoDetalhada2").hide();
		$("#VisaoDetalhada").hide();
		if(TypeReport == 1){
			$("#VisaoDetalhada2").show();
			$('html, body').animate({
			scrollTop: $("#TextNameFila2").offset().top
			}, 2000);
		}else{
			$("#VisaoDetalhada").show();
			$('html, body').animate({
			scrollTop: $("#TextNameFila").offset().top
			}, 2000);
		}
		
    });
	
	
    $("#dataDe").on('click', function () {
        $(this).removeClass('errorValidation');
    });

    $("#dataAte").on('click', function () {
        $(this).removeClass('errorValidation');
    });

    $(".datepicker").datepicker({
        showOtherMonths: true
    });

    $(".datepicker-inline").datepicker({ showOtherMonths: true });

    $(".datepicker-multiple").datepicker({
        showOtherMonths: true,
        numberOfMonths: 3
    });

    $(".datepicker-trigger").datepicker({
        showOn: "button",
        buttonImage: "images/interface/datepicker_trigger.png",
        buttonImageOnly: true,
        showOtherMonths: true
    });

    $(".from-date").datepicker({       
        numberOfMonths: 1,
        showOtherMonths: false,
        dateFormat: 'dd/mm/yy',
        dayNames: ['Domingo', 'Segunda', 'Terça', 'Quarta', 'Quinta', 'Sexta', 'Sábado'],
        dayNamesMin: ['D', 'S', 'T', 'Q', 'Q', 'S', 'S', 'D'],
        dayNamesShort: ['Dom', 'Seg', 'Ter', 'Qua', 'Qui', 'Sex', 'Sáb', 'Dom'],
        monthNames: ['Janeiro', 'Fevereiro', 'Março', 'Abril', 'Maio', 'Junho', 'Julho', 'Agosto', 'Setembro', 'Outubro', 'Novembro', 'Dezembro'],
        monthNamesShort: ['Jan', 'Fev', 'Mar', 'Abr', 'Mai', 'Jun', 'Jul', 'Ago', 'Set', 'Out', 'Nov', 'Dez'],
        nextText: 'Próximo',
        prevText: 'Anterior',
        onClose: function (selectedDate) {
            $(".to-date").datepicker("option", "minDate", selectedDate);
        }
    });
    $(".to-date").datepicker({       
        numberOfMonths: 1,
        showOtherMonths: true,
        dateFormat: 'dd/mm/yy',
        dayNames: ['Domingo', 'Segunda', 'Terça', 'Quarta', 'Quinta', 'Sexta', 'Sábado'],
        dayNamesMin: ['D', 'S', 'T', 'Q', 'Q', 'S', 'S', 'D'],
        dayNamesShort: ['Dom', 'Seg', 'Ter', 'Qua', 'Qui', 'Sex', 'Sáb', 'Dom'],
        monthNames: ['Janeiro', 'Fevereiro', 'Março', 'Abril', 'Maio', 'Junho', 'Julho', 'Agosto', 'Setembro', 'Outubro', 'Novembro', 'Dezembro'],
        monthNamesShort: ['Jan', 'Fev', 'Mar', 'Abr', 'Mai', 'Jun', 'Jul', 'Ago', 'Set', 'Out', 'Nov', 'Dez'],
        nextText: 'Próximo',
        prevText: 'Anterior',
        onClose: function (selectedDate) {
            $(".from-date").datepicker("option", "maxDate", selectedDate);
        }
    });

    $(".datepicker-restricted").datepicker({ minDate: -20, maxDate: "+1M +10D", showOtherMonths: true });




    $(".dataTables_length select").select2({
        minimumResultsForSearch: "-1"
    });


    //===== Default select =====//

    $(".select").select2({
        minimumResultsForSearch: "-1",
        width: 200
    });


    //===== Liquid select =====//

    $(".select-liquid").select2({
        minimumResultsForSearch: "-1",
        width: "off"
    });


    //===== Full width select =====//

    $(".select-full").select2({
        width: "100%"
    });


    //===== Select with filter input =====//

    $(".select-search").select2({
        width: 200
    });


    //===== Multiple select =====//

    $(".select-multiple").select2({
        width: "100%"
    });


    //===== Loading data select =====//

    $("#loading-data").select2({
        placeholder: "Enter at least 1 character",
        allowClear: true,
        minimumInputLength: 1,
        query: function (query) {
            var data = { results: [] }, i, j, s;
            for (i = 1; i < 5; i++) {
                s = "";
                for (j = 0; j < i; j++) { s = s + query.term; }
                data.results.push({ id: query.term + i, text: s });
            }
            query.callback(data);
        }
    });


    //===== Select with maximum =====//

    $(".maximum-select").select2({
        maximumSelectionSize: 3,
        width: "100%"
    });


    //===== Allow clear results select =====//

    $(".clear-results").select2({
        placeholder: "Select a State",
        allowClear: true,
        width: 200
    });


    //===== Select with minimum =====//

    $(".minimum-select").select2({
        minimumInputLength: 2,
        width: 200
    });


    //===== Multiple select with minimum =====//

    $(".minimum-multiple-select").select2({
        minimumInputLength: 2,
        width: "100%"
    });


    //===== Disabled select =====//

    $(".select-disabled").select2(
        "enable", false
    );
});