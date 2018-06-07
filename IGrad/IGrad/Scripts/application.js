function hideTarget(targetId) {
    $('#' + targetId).addClass('hidden');
}

function showTarget(targetId) {
    $('#' + targetId).removeClass('hidden');
}

$(":input").focus(function(){
    var parent = $(this).parent().closest('.row');
    parent.addClass("row-clarify");
});

$(":input").focusout(function(){
    var parent = $(this).parent().closest('.row');
    parent.removeClass("row-clarify");
});