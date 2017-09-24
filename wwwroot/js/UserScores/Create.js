$(document).ready(function () {
    $('#registerScore').click(function() {
        console.log('RegisterScore clicked');
        $('#quickReg').toggle();
        $('#registerForm').toggle();
        $(this).toggle();
        $('#prevBtn').toggle();
        $('#nextBtn').toggle();
    });

    $('#cancelBtn').click(function() {
        console.log('Cancel button clicked');
        $('#quickReg').toggle();
        $('#registerForm').toggle();
        $('#registerScore').toggle();
        $('#prevBtn').toggle();
        $('#nextBtn').toggle();

    });

    $('#prevBtn').click(function() {

    });

    $('#nextBtn').click(function() {
        
    })

    $('.btn-number').click(function() {
        var inputField = null;
        var currentVal = 0;
        if ($(this).attr('data-type') === 'minus') {
            inputField = $(this).parent().nextAll('input');
            currentVal = inputField.val();
            var minVal = inputField.attr('min');
            if (minVal < currentVal) {
                currentVal--;
                inputField.val(currentVal);
            }
        }
        if ($(this).attr('data-type') === 'plus') {
            inputField = $(this).parent().prevAll('input');
            currentVal = Number(inputField.val());
            var maxVal = Number(inputField.attr('max'));
            if (maxVal > currentVal) {
                currentVal = Number(currentVal) + 1;
                inputField.val(currentVal);
            }
        }

    });


})