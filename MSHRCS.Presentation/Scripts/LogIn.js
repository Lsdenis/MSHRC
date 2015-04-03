$(document).ready(function () {
	initButtonClick();
});

function initButtonClick() {
	$("#logInButton").click(function () {
		var $form = $("#logInUserForm");
		var formData = $form.serialize();

		//		var $roleId = $("#userSelector").val();
		//		var $password = $("#Password").val();
		var $rememberMe = $("#RememberMe").val();

		$.ajax({
			method: "post",
			url: logIn,
			dataType: "json",
			data: formData,
			success: function (response) {
				if (response.success) {
//					drawPriceForMail(response.data);
				}
			},
		});
	});
}