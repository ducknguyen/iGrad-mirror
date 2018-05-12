var selectedRace = [];
var selectedMilitary = [];
var nativeTribalCounter = 0;

$(document).ready(function () {
    CheckCurrentProgress();
    TrackRaceChanges();

    // track input fields on load
    $(':input').each(function () {
        if ($(this).hasClass('race-checkbox')) {
            var toUpdate = $('input[type=hidden][name=selectedRaces]');
            if ($(this).is(':checked')) {
                selectedRace.push($(this).val());
            }
            toUpdate.val(selectedRace);
        }
        if ($(this).hasClass('miltary-checkbox')) {
            var toUpdate = $('input[type=hidden][name=selectedMilitary]');
            if ($(this).is(':checked')) {
                selectedMilitary.push($(this).val());
            }
            toUpdate.val(selectedMilitary);
        }
        trackPersonalFormProgress();
    });

    $('.fa-minus-circle').hide();
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


$(':input').change(function () {
    // check if current input is radio button
    if ($(this).hasClass('required-checker')) {
        var toUpdate = $('input[type=hidden][name="' + $(this).attr("name") + '"]');
        toUpdate.val($(this).val());
    }

    // check if current input is race checkbox
    if ($(this).hasClass('race-checkbox')) {
        var toUpdate = $('input[type=hidden][name=selectedRaces]');
        if ($(this).is(':checked')) {
            selectedRace.push($(this).val());
        } else {
            selectedRace.pop();
        }
        toUpdate.val(selectedRace);
    }

    // check if current input is military checkbox
    if ($(this).hasClass('miltary-checkbox')) {
        var toUpdate = $('input[type=hidden][name=selectedMilitary]');
        if ($(this).is(':checked')) {
            selectedMilitary.push($(this).val());
        } else {
            selectedMilitary.pop();
        }
        toUpdate.val(selectedMilitary);
    }
    trackPersonalFormProgress();
});

$('a[data-parent="#accordion"]').click(function () {
    console.log("test");
    var current = $(this);
    if (current.hasClass('collapsed')) {
        $(this).find(".fa-plus-circle").hide();
        $(this).find(".fa-minus-circle").show();
    } else {
        $(this).find(".fa-minus-circle").hide();
        $(this).find(".fa-plus-circle").show();
    }
});

function processTribalSubmission() {
    $('#tribal-success-prompt').removeClass("hidden");
}
/*
$('#native-education').submit(function () {
    var count = 0;
    $('#native-education input:visible:not(:submit)').each(function () {
        if ($(this).length > 0) {
            count++;
        }
    });
    console.log(count);
    if (count > 0) {
        $('#tribal-success-prompt').show();
    }
});
*/