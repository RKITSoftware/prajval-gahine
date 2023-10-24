$(document).ready(function() {
    $("#additionForm").submit(function(e) {
        e.preventDefault();
        const queryString = $(this).serialize();
        console.log(queryString);
        additionOfTwoNumber(queryString);
        return false;
    });
});

function additionOfTwoNumber(queryString) {
    $.ajax({
        method: "POST",
        url: "http://localhost:8080/ajax2/add.php",
        data: queryString,
        dataType: "text",
        success: successFunction,
        error: errorFunction,
        complete: completeFunction,
    });
}

function successFunction(result) {
    $("#result").html(result);
}

function errorFunction(xhr, status, errorString) {
    $("#result").html(status);
} 

function completeFunction() {
    console.log("rq was handled");
}