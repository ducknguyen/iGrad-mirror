// FIELDS FOR PROGRESS BAR
var totalRequiredFields = $('.required-marker').length;
var progressBar = $("#application-progress");
var currentProgress = progressBar.attr("aria-valuenow");

/**
 * TRACK PROGRESS FOR PERSONAL FORM 
 */
function trackPersonalFormProgress() {
    var count = 0;

    // check all hidden fields
    $('input.progressbar-checker').each(function () {
        if (this.validity.valid && ($(this).val()).length != 0) {
            count++;
        }
    });

    // check other required input fields
    $(':input[required]').each(function () {
        if (this.validity.valid) {
            count++;
        }
    });

    
    currentProgress = (count / totalRequiredFields) * 100;
    console.log(currentProgress + " - " + count + " - " + totalRequiredFields);

    // append value to progressbar
    updateTextDisplay(currentProgress, count, totalRequiredFields);
}

/**
 * TRACK PROGRESS FOR OTHER FORMS
 */
function trackFormProgress() {
    var count = 0;
    
    // check all hidden fields
    $(':input.progressbar-checker').each(function () {
        if (this.validity.valid && ($(this).val()).length != 0) {
            count++;
        }
    });

    // check other required input fields
    $(':input[required]').each(function () {
        if (this.validity.valid) {
            count++;
        }
    });

    
    currentProgress = (count / totalRequiredFields) * 100;
    console.log(currentProgress + " - " + count + " - " + totalRequiredFields);

    // append value to progressbar
    updateTextDisplay(currentProgress, count, totalRequiredFields);
}

function updateTextDisplay(currentProgress, count, totalCount) {
    //console.log("inside update " + currentProgress + " - " + count + " - " + totalCount);
    progressBar.attr("aria-valuenow", currentProgress);
    progressBar.css("width", currentProgress + "%");
    $('#current-progress-count').text(count);
    $('#total-count').text(totalCount);
}