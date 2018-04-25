$(document).ready(function () {
    // track input fields on load
    $(':input').each(function () {
        trackFormProgress();
    });
});

$(':input').change(function () {
    // check if current input is radio button
    if ($(this).hasClass('required-checker')) {
        var toUpdate = $('input[type=hidden][name=' + $(this).attr('name') + ']');
        toUpdate.val($(this).val());
    }

    trackFormProgress();
});