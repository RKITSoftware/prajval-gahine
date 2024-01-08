$(document).ready(function() {
    getHTMLData();
});

// a function to make a call for an html element to append into #container
function getHTMLData() {
    $.ajax({
        method: "get",
        url: "data.htm",
        data: "text/html",
        dataType: "text",
        success: successFunction,
        error: errorFunction,
        complete: completeFunction,
    });
}

// definition for a success function that will be invoked when the corresponding ajax rq will be successfull
function successFunction(response) {
    console.log(response);
    $("#container").append(response);
    console.log("request was successful");
}

// definition for a error function that will be invoked when the corresponding ajax rq will be unsuccessfull
function errorFunction(xhr, status, errorString) {
    console.log(xhr);
    console.log(status);
    console.log(errorString);
}

// definition for a complete function that will be invoked when the corresponding ajax rq will be handled
function completeFunction() {
    console.log("the request was handled");
}