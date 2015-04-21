var teachersViewModels;

$(document).ready(function () {
	teachersViewModels = JSON.parse(getTeacherViewModels()).teacherViewModels;
	initEditGroupDisciplineButtions();
});

function initEditGroupDisciplineButtions() {
	$("#teacherTable thead tr:last td:last button").click(function () {
		var $tbody = $(this).closest("table").find("tbody");
		$tbody.append('<tr class="new-teacher">' +
			'<input class="typeahed-teacher-id" name="1b48b561-9c81-48c5-b1a4-bee74bf304d4" type="hidden" value="">' +
			'<td class="field-value vertical-middle"><input class="form-control twitter-typeahead" id="TeacherIdTextBox" name="TeacherIdTextBox" type="text" value=""></td>' +
			'<td class="field-lesson-type vertical-middle"><select class="form-control teacher-lesson-type" id="TeacherTypeId" name="TeacherTypeId"><option value="1">Теоретическое занятие</option>' +
			'<option value="2">Практическое занятие</option>' +
			'</select></td>' +
			'<td class="field-hours vertical-middle"><input class="form-control teacher-initial-hours" name="TeacherInitialHours" type="text" value=""></td>' +
			'<td class="field-hours vertical-middle"><input class="form-control teacher-actual-hours" name="TeacherActualHours" type="text" value=""></td>' +
			'<td>' +
			'<button class="btn btn-danger" id="addTeacher" type="button"><span class="glyphicon glyphicon-minus"></span></button>' +
			'</td>' +
			'</tr>');

		var $textbox = $tbody.find("tr:last td:first .twitter-typeahead");
		var $button = $tbody.find("tr:last td:last button");
		addTeachersTypeahead($textbox);
		removeTeacherButtonClick($button);
	});

	$("#teacherTable tbody tr").each(function (index) {
		var $button = $(this).find("td button");
		removeTeacherButtonClick($button);
	});

	$("#submit").click(function () {
		var gdTeachers = [],
			existedTeachers = [],
			$gdId = $("#Id").val();

		$("#teacherTable tbody tr.new-teacher").each(function (index) {

			var $this = $(this),
				$teacherId = $this.find(".typeahed-teacher-id").val(),
				$lessonTypeId = $this.find(".teacher-lesson-type").val(),
				$teacherInitialHours = $this.find(".teacher-initial-hours").val(),
				$teacherActualHours = $this.find(".teacher-actual-hours").val();

			gdTeachers.push({ TeacherId: $teacherId, ActualHoursNumber: $teacherActualHours, InitialHoursNumber: $teacherInitialHours, LessonTypeId: $lessonTypeId });
		});

		$("#teacherTable tbody tr.existed-teacher").each(function (index) {

			var $id = $(this).find(".typeahed-teacher-id").val();

			existedTeachers.push($id);
		});

		$.ajax({
			method: "post",
			url: saveGroupDisciplineList,
			dataType: "json",
			data: { gdId: $gdId, exitsedIds: JSON.stringify(existedTeachers), gdTeachers: JSON.stringify(gdTeachers) },
			success: function (response) {
				window.location.href = response.nextPage;
			}
		});
	});
}

function getTeacherViewModels() {
	return $.ajax({
		method: "get",
		url: teachersViewModels,
		dataType: "json",
		async: false
	}).responseText;
}

function addTeachersTypeahead(textbox) {

	textbox.typeahead({
		highlight: true,
		hint: true,
		minLenght: 1
	},
	{
		name: "teachersViewModels",
		displayKey: "FullName",
		source: function (query, process) {
			var elements = getMatchedElements(query, teachersViewModels, "FullName");
			process(elements);
		}
	});

	textbox.bind("typeahead:selected", function (obj, selectedItem, sourceName) {
		$(this).closest("tr").find(".typeahed-teacher-id").val(selectedItem.Id);
	});
}

function removeTeacherButtonClick(button) {
	button.click(function () {
		this.closest("tr").remove();
	});
}

function getMatchedElements(query, array, property) {
	var elements = [];

	$.each(array, function (index, value) {
		if (value[property].toLowerCase().match("^" + query.toLowerCase())) {
			var object = {};
			object.Id = value.Id;
			object[property] = value[property];
			elements.push(object);
		}
	});

	return elements;
}