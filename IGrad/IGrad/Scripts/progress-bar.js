// FIELDS FOR PROGRESS BAR
var totalRequiredFields = $('.required-marker').length;
var progress = $("#application-progress");
var currentProgress = progress.attr("value");

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

    // append value to progressbar
    currentProgress = (count / totalRequiredFields) * 100;
    console.log(currentProgress + " - " + count + " - " + totalRequiredFields);
    progress.attr("value", currentProgress);
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

    // append value to progressbar
    currentProgress = (count / totalRequiredFields) * 100;
    console.log(currentProgress + " - " + count + " - " + totalRequiredFields);
    progress.attr("value", currentProgress);
}