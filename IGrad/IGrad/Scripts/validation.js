var validations = { "#Birthday": false, "#Email": false, "#PhoneInfo_PhoneNumber": false };

function validateBirthday() {
    var datePattern = /^\d{4}\-(0?[1-9]|1[012])\-(0?[1-9]|[12][0-9]|3[01])$/;
    var birthdayInputFieldId = '#Birthday';
    var birthdayValidationOutputField = $("span[data-valmsg-for='Birthday']");

    validateField(birthdayInputFieldId, datePattern, birthdayValidationOutputField, "date for birthday");
}

function validateEmail() {
    var emailPattern = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    var emailInputFieldId = '#Email';
    var emailValidationOutputField = $("span[data-valmsg-for='Email'");

    validateField(emailInputFieldId, emailPattern, emailValidationOutputField, "email");
}

function validatePhoneNumber() {
    var phoneNumberPattern = /^\(?\d{3}\)?-? *\d{3}-? *-?\d{4}$/;
    var phoneNumberInputFieldId = '#PhoneInfo_PhoneNumber';
    var phoneNumberValidationOuputField = $("span[data-valmsg-for='PhoneInfo.PhoneNumber']");

    validateField(phoneNumberInputFieldId, phoneNumberPattern, phoneNumberValidationOuputField, "phone number");
}

function validateField(inputFieldId, pattern, outputSpan, fieldDescriptor) {
    var fieldInput = $(inputFieldId).val();
    if (!pattern.test(fieldInput)) {
        validations[inputFieldId] = false;
        outputSpan.text("Please enter a valid " + fieldDescriptor);
    } else {
        validations[inputFieldId] = true;
        outputSpan.text("");
    }
}

function validateGetNewApplicationSection() {
    validateBirthday();
    validateEmail();
    validatePhoneNumber();

    var isValidForm = true;

    Object.keys(validations).forEach(function (key, index) {
        isValidForm = isValidForm && validations[key];
    });

    for (var validation in validations) {
        if (validations.hasOwnProperty(validation)) {
        }
    }

    if (!isValidForm) {
        alert('Please correct invalid inputs!');
    }

    return isValidForm;
}