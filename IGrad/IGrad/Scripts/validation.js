function validateBirthday() {
    var datePattern = /^\d{4}\-(0?[1-9]|1[012])\-(0?[1-9]|[12][0-9]|3[01])$/;
    var birthdayInput = $('#Birthday').val();
    var birthdayValidationOutputField = $("span[data-valmsg-for='Birthday']");

    validateField(birthdayInput, datePattern, birthdayValidationOutputField, "date for birthday");
}

function validateEmail() {
    var emailPattern = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    var emailInput = $('#Email').val().toLowerCase();
    var emailValidationOutputField = $("span[data-valmsg-for='Email'");

    validateField(emailInput, emailPattern, emailValidationOutputField, "email");
}

function validatePhoneNumber() {
    var phoneNumberPattern = /^\(?\d{3}\)?-? *\d{3}-? *-?\d{4}$/;
    var phoneNumberInput = $('#PhoneInfo_PhoneNumber').val();
    var phoneNumberValidationOuputField = $("span[data-valmsg-for='PhoneInfo.PhoneNumber']");

    validateField(phoneNumberInput, phoneNumberPattern, phoneNumberValidationOuputField, "phone number");
}

function validateField(fieldInput, pattern, outputSpan, fieldDescriptor) {
    if (!pattern.test(fieldInput)) {
        outputSpan.text("Please enter a valid " + fieldDescriptor);
    } else {
        outputSpan.text("");
    }
}