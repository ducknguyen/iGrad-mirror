var highSchoolList = [];
var violationList = [];

$(document).ready(function () {
    checkCurrentHighSchoolHistory();
    totalRequiredFields = $('.required-marker:visible').length;
    checkCurrentViolationHistory();

    $(':input').each(function () {
        trackFormProgress();
    })
});


/* HIGH SCHOOL RELATED FUNCTIONS */
// check current highschool in table
function checkCurrentHighSchoolHistory() {
    $('#highschool-info-table tbody tr').each(function (i, el) {
        var $tds = $(this).find('td'),
            year = $tds.eq(0).text(),
            name = $tds.eq(1).text(),
            isLastAttended = $tds.eq(5).text();

        var currentHighSchool = new HighSchoolObject(name, year, isLastAttended);
        addToHighSchoolList(currentHighSchool);
    });
    updateHighSchoolHistory(highSchoolList);
}
// high school object
function HighSchoolObject(name, year, isLastAttended){
    this.name = name;
    this.year = year;
    this.isLastAttended = isLastAttended;
}

// verifies highschool as last attended and add accordingly to HighSchoolList
function addToHighSchoolList(highSchool) {
    if (highSchool.isLastAttended == "True") {
        highSchoolList.unshift(highSchool);
        $('input[name="SchoolInfo.LastYearAttended"]').val(highSchool.year);
    } else {
        highSchoolList.push(highSchool);
    }
}

// set the value of hidden highschool history field 
function updateHighSchoolHistory(highSchoolList) {
    var toUpdate = $('input[type=hidden][name=highschool-history]');
    toUpdate.val(highSchoolList);
}

// process deleting high schol
function processDeleteHighSchool(fieldId) {
    if ($('#' + fieldId + ' #last-attended').length > 0) {
        highSchoolList.shift();
        $('input[name="SchoolInfo.LastYearAttended"]').val('');
    } else {
        highSchoolList.pop();
    }
    updateHighSchoolHistory(highSchoolList);
    trackFormProgress();
}
/* HIGH SCHOOL RELATED FUNCTIONS END*/

/* VIOLATIONS RELATED FUNCTIONS */
// check current violations in table
function checkCurrentViolationHistory() {
    $('#violation-info-table tbody tr').each(function (i, el) {
        violationList.push(violationList.length);
    });
    updateViolationHistory(violationList);
}
function processAddViolation() {
    violationList.push(violationList.length);
    updateViolationHistory(violationList);
    hideTarget('add-violation-information');
}
function processDeleteViolation() {
    violationList.pop();
    updateViolationHistory(violationList);
    hideTarget('add-violation-information');
    trackFormProgress();
}
function updateViolationHistory(violationList) {
    var toUpdate = $('input[type=hidden][name=violation-history]');
    toUpdate.val(violationList);
}

function noViolationPrompt(){
    hideTarget('add-violation-information');

    violationList.push(1);
    updateViolationHistory(violationList);
    $('#no-violation-prompt').removeClass('hidden');

    trackFormProgress();
}
/* VIOLATIONS RELATED FUNCTIONS END */

// track input changes
$(':input').change(function () {
    // check if current input is radio button
    if ($(this).hasClass('radio-checker')) {
        var toUpdate = $('input[type=hidden][name="' + $(this).attr("name") + '"]');
        toUpdate.val($(this).val());
    }
    trackFormProgress();
});