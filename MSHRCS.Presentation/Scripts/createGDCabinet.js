var groupDisciplineViewModels,
	lessonAvailableCabinetsViewModels;

$(document).ready(function () {
	groupDisciplineViewModels = JSON.parse(getJsonGroupDisciplineViewModels()).groupDisciplineViewModels;
	lessonAvailableCabinetsViewModels = JSON.parse(getJsonCabinetLessonViewModels()).lessonAvailableCabinetsViewModels;
	initNewGDCabinetTypeaheads();
	initDatetimePicker();
});

function initNewGDCabinetTypeaheads() {

}

function initDatetimePicker() {
	$('#Date').datepicker({
		todayBtn: "linked",
		language: "ru",
		autoclose: true,
		todayHighlight: true
	});
}

function dateChanged(date) {

}

function getJsonGroupDisciplineViewModels() {
	return $.ajax({
		method: "get",
		url: getGroupDisciplineViewModels,
		dataType: "json",
		async: false
	}).responseText;
}

function getJsonCabinetLessonViewModels() {
	return $.ajax({
		method: "get",
		url: getCabinetLessonViewModels,
		dataType: "json",
		async: false
	}).responseText;
}