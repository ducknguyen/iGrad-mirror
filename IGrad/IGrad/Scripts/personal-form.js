var selectedRace = [];
var nativeTribalCounter = 0;

$(document).ready(function () {
    CheckCurrentProgress();
    TrackRaceChanges();

    // track input fields on load
    $(':input').each(function () {
        trackPersonalFormProgress();
    });

});

function CheckCurrentProgress() {
    $('input[type=checkbox]').each(function () {
        var current = $(this);
        var label = $("label[for='" + current.attr("id") + "']").text();
        var isChecked = current.is(":checked");

        if (current.hasClass("race-checkbox")) {
            UpdateRaceList(label, isChecked);

            if (current.hasClass("native-tribal") && isChecked === true) {
                nativeTribalCounter++;
            }
        }
    });

    NativeTribalToggle(nativeTribalCounter);
}

function TrackRaceChanges() {
    $(".race-checkbox").change(function () {
        var current = $(this);
        var label = $("label[for='" + current.attr("id") + "']").text();
        var isChecked = current.is(":checked");

        if (current.hasClass("native-tribal") && isChecked === true) {
            nativeTribalCounter++;
        }
        else if (current.hasClass("native-tribal") && isChecked === false) {
            nativeTribalCounter--;
        }

        NativeTribalToggle(nativeTribalCounter);
        UpdateRaceList(label, isChecked);
    });
}

function UpdateRaceList(currentCheckbox, isChecked) {
    var $list = $("#studentRaceList");
    if (isChecked === true) {

        $list.append("<li data-value='" + currentCheckbox + "'>" + currentCheckbox + "<input type='hidden' name='studentRace[]' value='" + currentCheckbox + "'/></li>");

        // showcasing selected race
        $list.addClass("added-race");
        setTimeout(function () {
            $list.removeClass("added-race");
        }, 6000);
    }
    else {
        $list.find('li[data-value="' + currentCheckbox + '"]').remove();
    }
}

function NativeTribalToggle(nativeTribalCounter) {
    if (nativeTribalCounter > 0) {
        $("#native-tribal-wrapper").show();
        $("#native-tribal-information").load('@Url.Action("GetNativeAmericanEducationForm", "Application")');
    }
    else {
        $("#native-tribal-wrapper").hide();
    }
}


$(':input').change(function () {
    // check if current input is radio button
    if ($(this).hasClass('required-checker')) {
        var toUpdate = $('input[type=hidden][name=' + $(this).attr('name') + ']');
        toUpdate.val($(this).val());
    }

    // check if current input is checkbox
    if ($(this).hasClass('race-checkbox')) {
        var toUpdate = $('input[type=hidden][name=selectedRaces]');
        if ($(this).is(':checked')) {
            selectedRace.push($(this).val());
        } else {
            selectedRace.pop();
        }
        toUpdate.val(selectedRace);
    }
    trackPersonalFormProgress();
});
