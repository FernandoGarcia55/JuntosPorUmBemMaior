  'use strict';
$(document).ready(function () {
//Basic alert
	document.querySelector('.sweet-1').onclick = function(){
		swal("Here's a message!", "It's pretty, isn't it?")
	};
	//success message
	document.querySelector('.alert-success-msg').onclick = function(){
		swal("Good job!", "You clicked the button!", "success");
	};

	

	//Success or cancel alert
	document.querySelector('.alert-success-cancel').onclick = function(){
		swal({
			title: "Voce tem certeza?",
			text: "Voce gostaria de deletar este Usuario?",
			type: "warning",
					showCancelButton: true,
					confirmButtonClass: "btn-primary",
					confirmButtonText: "Sim, quero cancelar!",
					cancelButtonText: "Nao, nao quero cancelar",
					closeOnConfirm: false,
					closeOnCancel: false
				},
				function(isConfirm) {
					if (isConfirm) {
						swal("Deletado!", "Deletado com sucesso.", "success");
					} else {
						swal("Cancelado", "O usuario não sera cancelado", "error");
					}
				});
	};
	//prompt alert
	document.querySelector('.alert-prompt').onclick = function(){
		swal({
			title: "An input!",
			text: "Write something interesting:",
			type: "input",
			showCancelButton: true,
			closeOnConfirm: false,
			inputPlaceholder: "Write something"
		}, function (inputValue) {
			if (inputValue === false) return false;
			if (inputValue === "") {
				swal.showInputError("You need to write something!");
				return false
			}
			swal("Nice!", "You wrote: " + inputValue, "success");
		});
	};

	//Ajax alert
	document.querySelector('.alert-ajax').onclick = function(){
		swal({
			title: "Ajax request example",
			text: "Submit to run ajax request",
			type: "info",
			showCancelButton: true,
			closeOnConfirm: false,
			showLoaderOnConfirm: true
		}, function () {
			setTimeout(function () {
				swal("Ajax request finished!");
			}, 2000);
		});
	};


		$('#openBtn').on('click',function () {
			$('#myModal').modal({
				show: true
			})
		});

		$(document).on('show.bs.modal', '.modal', function (event) {
			var zIndex = 1040 + (10 * $('.modal:visible').length);
			$(this).css('z-index', zIndex);
			setTimeout(function() {
				$('.modal-backdrop').not('.modal-stack').css('z-index', zIndex - 1).addClass('modal-stack');
			}, 0);
		});
	});
  