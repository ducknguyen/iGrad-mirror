$(document).ready(function () {
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

//Adds student race to visible list on race selection/de-selection
$(".raceCheck").change(function () {
    // If checked
    var value = $(this).val(),
        $list = $("#studentRaceList");
    if (this.checked) {
        //add to the right
        $list.append("<li data-value='" + value + "'>" + value + "<input type='hidden' name='studentRace[]' value='" + value + "'/></li>");
    }
    else {
        //hide to the right
        $list.find('li[data-value="' + value + '"]').remove();
    }
});