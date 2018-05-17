var studentLivesWith = [];
var primaryGuardian = [];
var secondaryGuardian = [];
var emergencyContacts = [];

$(document).ready(function () {
    //homelessToggled();
    trackCurrentTablesStatus();

    totalRequiredFields = $('.required-marker:visible').length;

    $(':input').each(function () {
        // check if current input is radio button
        if ($(this).hasClass('radio-checker')) {
            if ($(this).is(':checked')) {
                var toUpdate = $('input[type=hidden][name="' + $(this).attr("name") + '"]');
                toUpdate.val($(this).val());
            }
        }

        // check if this is student lives with checkbox
        if ($(this).hasClass('live-with')) {
            if ($(this).is(':checked')) {
                var toUpdate = $('input[type=hidden][name="student-live-with"]');
                studentLivesWith.push(studentLivesWith.length + 1);
                toUpdate.val(studentLivesWith);
            }
        }
    })

    residentIsMailingAddress();

    trackFormProgress();
})

function trackCurrentTablesStatus() {
    $('#primary-guardians tbody tr').each(function () {
    console.log('inside primary');
        primaryGuardian.push(primaryGuardian.length+1);
    })

    $('#secondary-guardians tbody tr').each(function () {
    console.log('inside secondary');
        secondaryGuardian.push(secondaryGuardian.length+1);
    })

    $('#emergency-contacts-table tbody tr').each(function () {
        console.log('inside emergency');
        emergencyContacts.push(emergencyContacts.length+1);
    })

    var primaryHidden = $('input[type=hidden][name="primary-guardian-selection"]');
    primaryHidden.val(primaryGuardian);
    var secondaryHidden = $('input[type=hidden][name="secondary-guardian-selection"]');
    secondaryHidden.val(secondaryGuardian);
    var emergencyHidden = $('input[type=hidden][name="emergency-selection"]');
    emergencyHidden.val(emergencyContacts);
}

function residentIsMailingAddress() {
    if ($('input[name=ResidentAddressIsMailingAddress]:checked').val() == 'True') {
        hideTarget('mailing-address');
    } else {
        showTarget('mailing-address');
    }
}

/* GUARDIAN FUNCTIONS */
function processAddGuardian(type){
    hideTarget('add-' + type + '-guardian');
    showTarget('add-' + type + '-guardian-button');

    var toUpdate = $('input[type=hidden][name="' + type + '-guardian-selection"]');
    if (type == "primary") {
        primaryGuardian.push(primaryGuardian.length);
        toUpdate.val(primaryGuardian);
    } else {
        secondaryGuardian.push(secondaryGuardian.length);
        toUpdate.val(secondaryGuardian);
    }

    trackFormProgress();
}

function guardianFormCancel(type) {
    showTarget('add-' + type + '-guardian-button');
    hideTarget('add-' + type + '-guardian')
}

/* EMERGENCY CONTACT FUNCTIONS */
function processAddEmergencyContact() {
    hideTarget('add-emergency-contact');
    showTarget('add-emergency-contact-button');

    var toUpdate = $('input[type=hidden][name="emergency-selection"]');
    emergencyContacts.push(emergencyContacts.length);
    toUpdate.val(emergencyContacts);

    trackFormProgress();
}

function emergencyContactFormCancel() {
    showTarget('add-emergency-contact-button');
    hideTarget('add-emergency-contact');
}

// track input changes
$(':input').change(function () {
    // check if current input is radio button
    if ($(this).hasClass('radio-checker')) {
        var toUpdate = $('input[type=hidden][name="' + $(this).attr("name") + '"]');
        toUpdate.val($(this).val());
    }

    // check if this is student lives with checkbox
    if ($(this).hasClass('live-with')) {
        var toUpdate = $('input[type=hidden][name="student-live-with"]');
        if ($(this).is(':checked')) {
            studentLivesWith.push(studentLivesWith.length + 1);
        } else {
            studentLivesWith.pop();
        }
        toUpdate.val(studentLivesWith);
    }

    trackFormProgress();
});

/* HELPER FUNCTIONS */
function hideTarget(targetId) {
    $('#' + targetId).addClass('hidden');
}

function showTarget(targetId) {
    $('#' + targetId).removeClass('hidden');
}
