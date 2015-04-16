var teachersViewModels;

$(document).ready(function () {
	teachersViewModels = JSON.parse(getTeacherViewModels()).teacherViewModels;
	initAutocomplete();
	initCGDButtonsClick();
});

function initAutocomplete() {
	var groupViewModels = JSON.parse(getGroupViewModels()).groupViewModels;
	var disciplineViewModels = JSON.parse(getDisciplineViewModels()).disciplineViewModels;

	$("#GroupIdTextBox").typeahead({
		highlight: true,
		hint: true,
		minLenght: 1
	},
	{
		name: "groupViewModels",
		displayKey: "Code",
		source: function (query, process) {
			var elements = getMatchedElements(query, groupViewModels, "Code");
			process(elements);
		}
	});

	$("#GroupIdTextBox").bind("typeahead:selected", function (obj, selectedItem, sourceName) {
		$("#GroupId").val(selectedItem.Id);
	});

	$("#DisciplineIdTextBox").typeahead({
		highlight: true,
		hint: true,
		minLenght: 1
	},
	{
		name: "disciplineViewModels",
		displayKey: "Code",
		source: function (query, process) {
			var elements = getMatchedElements(query, disciplineViewModels, "Code");
			process(elements);
		}
	});

	$("#DisciplineIdTextBox").bind("typeahead:selected", function (obj, selectedItem, sourceName) {
		$("#DisciplineId").val(selectedItem.Id);
	});

	var $teachersTypeaheads = $("#teacherTable .twitter-typeahead");

	$teachersTypeaheads.each(function (index) {
		addTeachersTypeahead($(this));
	});
}

function initCGDButtonsClick() {
	$("#addTeacher").click(function () {
		var $tbody = $(this).closest("tbody");
		$tbody.append('<tr>' +
			'<input class="typeahed-teacher-id" name="1b48b561-9c81-48c5-b1a4-bee74bf304d4" type="hidden" value="">' +
			'<td class="field-value"><input class="form-control twitter-typeahead" id="TeacherIdTextBox" name="TeacherIdTextBox" type="text" value=""></td>' +
			'<td class="field-lesson-type"><select class="form-control teacher-lesson-type" id="TeacherTypeId" name="TeacherTypeId"><option value="1">Теоретическое занятие</option>' +
			'<option value="2">Практическое занятие</option>' +
			'</select></td>' +
			'<td class="field-hours"><input class="form-control teacher-hours" id="TeacherHours" name="TeacherHours" type="text" value=""></td>' +
			'<td>' +
			'<button class="btn btn-danger" id="addTeacher" type="button"><span class="glyphicon glyphicon-minus"></span></button>' +
			'</td>' +
			'</tr>');

		$tbody.find("tr:last td:last .btn").click(function () {
			this.closest("tr").remove();
		});

		var $textbox = $tbody.find("tr:last .twitter-typeahead");

		addTeachersTypeahead($textbox);
	});

	$("#submit").click(function () {
		var $groupId = $("#GroupId").val(),
			$disciplineId = $("#DisciplineId").val(),
			gdTeachers = [];


		$("#teacherTable tbody tr").each(function (index) {

			var $this = $(this),
				$teacherId = $this.find(".typeahed-teacher-id").val(),
				$lessonTypeId = $this.find(".teacher-lesson-type").val(),
				$teacherHours = $this.find(".teacher-hours").val();

			gdTeachers.push({ TeacherId: $teacherId, ActualHoursNumber: $teacherHours, InitialHoursNumber: $teacherHours, LessonTypeId: $lessonTypeId });
		});

		var value = JSON.stringify(gdTeachers);

		$.ajax({
			method: "post",
			url: saveGroupDiscipline,
			dataType: "json",
			data: { groupId: $groupId, disciplineId: $disciplineId, gdTeachers: value },
			success: function (response) {
				window.location.href = response.nextPage;
			}
		});
	});
}

function getGroupViewModels() {
	return $.ajax({
		method: "get",
		url: groupViewModels,
		dataType: "json",
		async: false
	}).responseText;
}

function getDisciplineViewModels() {
	return $.ajax({
		method: "get",
		url: disciplineViewModels,
		dataType: "json",
		async: false
	}).responseText;
}

function getTeacherViewModels() {
	return $.ajax({
		method: "get",
		url: teachersViewModels,
		dataType: "json",
		async: false
	}).responseText;
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

