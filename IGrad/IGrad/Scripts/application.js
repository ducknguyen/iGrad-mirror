function hideTarget(targetId) {
    $('#' + targetId).addClass('hidden');
}

function showTarget(targetId) {
    $('#' + targetId).removeClass('hidden');
}

$("input:not(.race-checkbox)").focus(function(){
    console.log("inside focus");
    var parent = $(this).parent().closest('.row');
    $('.row').addClass("blur-content");
    parent.removeClass("blur-content");
    parent.addClass("clear-content");
});

$("input:not(.race-checkbox)").focusout(function(){
    console.log("inside focusout");
    var parent = $(this).parent().closest('.row');
    $('.row').removeClass("blur-content");
    parent.removeClass("clear-content");
});