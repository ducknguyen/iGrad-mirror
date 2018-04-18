
var count = 0;

$(document).ready(function () {
    checkProgress();
    checkRadioButtonProgress();
});

$('.required-checker').change(function () {
/*    if ($(this).val().length > 0) {
        count++;
    }
    else if ($(this).children().length > 0) {
        if ($(this).children(":first").val().length > 0) {
            count++;
        }
    }
    else {
        count--;
    }*/
    if (this.validity.valid) {
        count++
    }
    console.log("checker " + count);
    updateProgressBar(count);
});

$(':input[type="radio"]').change(function () {
    //checkRadioButtonProgress();
    //count--;
    if (this.validity.valid) {
        count++
    }
    updateProgressBar(count);
    console.log("radio " + count);
});

// CHECK INITIAL PROGRESS OF ALL REQUIRED-CHECKERS AND RADIO-BUTTONS 
function checkProgress() {
    $('.required-checker').each(function () {
        if ($(this).val().length > 0) {
            count++;
        }
        else if ($(this).children().length > 0) {
            if ($(this).children(":first").val().length > 0) {
                count++;
            }
        }
    });
    updateProgressBar(count);
}

function checkRadioButtonProgress() {
    $(':input[type="radio"]').each(function () {
        if ($(this).is(':checked')) {
            count++;
        }
    });
    updateProgressBar(count);
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
