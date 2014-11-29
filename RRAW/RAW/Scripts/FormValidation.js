/// <reference path="http://ajax.googleapis.com/ajax/libs/jquery/1.6.2/jquery.min.js" />

var firstItem;

function isFormValid(focusFirstItem) {
	firstItem = undefined;

	if (!(ValidateFields("validateRequired", "", "Required", "req"))) return IfInvalid(focusFirstItem);

	if (!(ValidateFields("validateNumber", /^[\-\+]?(([0-9]+)([\.,]([0-9]+))?|([\.,]([0-9]+))?)$/, "Invalid Number", "num"))) return IfInvalid(focusFirstItem);

	if (!(ValidateFields("validateDate", /^((0?[1-9]|1[012])[\/\-](0?[1-9]|[12][0-9]|3[01])[\/\-]\d{4})?$/, "Invalid Date", "dt"))) return IfInvalid(focusFirstItem);

	return true;
}

function RemoveAllErrorNotes(errorSpanPrefix) {
	$.each($("[id^='" + errorSpanPrefix + "Err']"), function (key, item) {
		$(item).remove();
	});
}

function ValidateFields(fieldValidationClass, regx, errorText, errorSpanPrefix) {
	var valid = true;

	RemoveAllErrorNotes(errorSpanPrefix);

	//Check for fields based on class
	$.each($("." + fieldValidationClass), function (key, item) {
		item = $(item);
		if (!item.val().trim().match(regx) || (regx == "" && item.val().trim() == regx)) {
			valid = false;

			//Create error notes for error fields
			$(item).after("<span id='" + errorSpanPrefix + "Err" + $(item).prop("id") + "' class='error'>" + errorText + "</span>");
			
			//Add events to add/remove error notes
			$(item).bind('blur', function () {
				if (!item.val().trim().match(regx) || (regx == "" && item.val().trim() == regx)) {
					$("#" + errorSpanPrefix + "Err" + $(item).prop("id")).remove();
					$(item).after("<span id='" + errorSpanPrefix + "Err" + $(item).prop("id") + "' class='error'>" + errorText + "</span>");
				}
				else {
					$("#" + errorSpanPrefix + "Err" + $(item).prop("id")).remove();
					//requiredErrorItems.splice($.inArray($(item), requiredErrorItems), 1);
				}
			});

			if (firstItem == undefined) {
				firstItem = item;
			}
		}
		else {
		}
	});

	return valid;
}

function IfInvalid(focusFirstItem) {
	if (focusFirstItem) {
		firstItem.focus();
	}
	return false;
}