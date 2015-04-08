$(document).ready(function () {
	initLogInButtonClick();
});

function initLogInButtonClick() {
	$("#logInButton").click(function (e) {
		e.preventDefault();

		var $form = $("#logInUserForm");
		var formData = $form.serialize();

		var $password = $("#Password").val();

		if (!$password) {
			showError("Пароль не должен быть пустым!");
		} else {
			$.ajax({
				method: "post",
				url: logIn,
				dataType: "json",
				data: formData,
				success: function (response) {
					if (response.success) {
						window.location.href = response.nextPage;
					} else {
						showError(response.message);
					}
				}
			});
		}


	});
}

function showError(text) {
	$("#hiddenErrorMessageDiv").removeClass("hidden");
	$("#hiddenErrorMessageDiv p").text(text);
}