$(document).ready(function () {
    radioButtonChecker();
    totalRequiredFields = $('.required-marker:visible').length;
 // console.log($('.required-marker').not(":visible").length);

    // track input fields on load
    $(':input').each(function () {
        // check if current input is radio button
        if ($(this).hasClass('radio-checker')) {
            if ($(this).is(':checked')) {
                var toUpdate = $('input[type=hidden][name="' + $(this).attr('name') + '"]');
                toUpdate.val($(this).val());
            }
        }
        trackFormProgress();
    });
});

// show/hide education outside us according to current selection
function radioButtonChecker() {
    if ($('.had-outside').is(":checked")) {
        $('#education-outside-us').show();
        $('#education-outside-us :input').attr('required', true);
        totalRequiredFields = $('.required-marker').length;
    } else {
        $('#education-outside-us').hide();
        $('#education-outside-us :input').removeAttr('required');
        totalRequiredFields = $('.required-marker:visible').length;
    }
}

$(':input').change(function () {
    // check if current input is radio button
    if ($(this).hasClass('radio-checker')) {
        var toUpdate = $('input[type=hidden][name="' + $(this).attr('name') + '"]');
        toUpdate.val($(this).val());
    }
    radioButtonChecker();
    trackFormProgress();
});
