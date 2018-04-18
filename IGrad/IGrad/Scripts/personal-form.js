var nativeTribalCounter = 0;

$(document).ready(function () {
    DetectChecked();
    TrackRaceChanges();
});

function DetectChecked() {
    //check for checked boxes when page loads
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
    var count = 0;
    var $list = $("#studentRaceList");

    if (isChecked === true) {
        // add to the right and increment count
        $list.append("<li data-value='" + currentCheckbox + "'>" + currentCheckbox + "<input type='hidden' name='studentRace[]' value='" + currentCheckbox + "'/></li>");
        count++;

        // notifying selected race
        $list.addClass("added-race");
        setTimeout(function () {
            $list.removeClass("added-race");
        }, 6000);
    }
    else {
        //hide to the right
        $list.find('li[data-value="' + currentCheckbox + '"]').remove();

        //verify list is not empty
        if ($list.children().length > 0) {
            count++;
        }
    }

    updateProgressBar(count);
}

function NativeTribalToggle(nativeTribalCounter) {
    // show/hide native-tribal area
    if (nativeTribalCounter > 0) {
        $("#native-tribal-wrapper").show();
        $("#native-tribal-information").load('@Url.Action("GetNativeAmericanEducationForm", "Application")');
    }
    else {
        $("#native-tribal-wrapper").hide();
    }
}


<script>

    var nativeTribalCounter = 0;

$(document).ready(function () {
        DetectChecked();
    TrackRaceChanges();
});

function DetectChecked() {
        //check for checked boxes when page loads
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
    var count = 0;
    var $list = $("#studentRaceList");

    if (isChecked === true) {
        // add to the right and increment count
        $list.append("<li data-value='" + currentCheckbox + "'>" + currentCheckbox + "<input type='hidden' name='studentRace[]' value='" + currentCheckbox + "'/></li>");
    count++;

        // notifying selected race
        $list.addClass("added-race");
        setTimeout(function () {
        $list.removeClass("added-race");
    }, 6000);
    }
    else {
        //hide to the right
        $list.find('li[data-value="' + currentCheckbox + '"]').remove();

    //verify list is not empty
    if ($list.children().length > 0) {
        count++;
    }
    }

    updateProgressBar(count);
}

function NativeTribalToggle(nativeTribalCounter) {
    // show/hide native-tribal area
    if (nativeTribalCounter > 0) {
        $("#native-tribal-wrapper").show();
    $("#native-tribal-information").load('@Url.Action("GetNativeAmericanEducationForm", "Application")');
    }
    else {
        $("#native-tribal-wrapper").hide();
    }
}


    $(":input").keyup(function () {
        var count = 0;
        $(":input .required-checker").each(function () {
            if (this.validity.valid) {
        count++;
    }
        });
        updateProgressBar(count);
    });


    // calculate and update progress bar base on count
    function updateProgressBar(count) {
        var totalRequiredFields = ($('.required-marker').length);
        var progress = $("#application-progress");
        var calculateProgress = (count / totalRequiredFields) * 100;

        console.log("in progress update " + calculateProgress + " " + totalRequiredFields + " " + count)
        progress.attr("value", calculateProgress);
    }
</script>