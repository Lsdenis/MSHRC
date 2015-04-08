$(document).ready(function () {
	initGeneralTableButtonClick();
});

function initGeneralTableButtonClick() {
	$("#filter").click(function () {
		var group = $("#group").val(),
			timeId = $("#time").val(),
			teacher = $("#teacher").val(),
			cabinet = $("#cabinet").val(),
			discipline = $("#discipline").val();

		$.ajax({
			method: "get",
			url: generalTableData,
			dataType: "html",
			data: { group: group, timeId: timeId, teacher: teacher, cabinet: cabinet, discipline: discipline, page: 1 },
			success: function (response) {
				$("#tableDiv").html(response);
			}
		});
	});
}