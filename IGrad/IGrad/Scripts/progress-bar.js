$(':input').change(function () {
    var count = 0;
    var totalRequiredFields = ($('.required-marker').length);
    console.log(totalRequiredFields);

    $(':input[required]:visible').each(function () {
        if ((this.validity.valid) == true) {
            count++;
            console.log(this);
        }
    });
    var progress = $("#application-progress");
    var calculateProgress = (count / totalRequiredFields) * 100;
    console.log(calculateProgress);
    progress.attr("value", calculateProgress);
});