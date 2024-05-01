$(function () {
    var container = $("#container");

    // create a div inside container with classname "dx-button-content"
    container.dxButton();

    // create an instance of button inside "dx-button-content"
    var button = container.dxButton("instance");

    // sets value of the option specified
    button.option("text", "Submit");

    //button.option("onClick", function () {
    //    alert("Hello world");
    //})

    $("#container").dxButton().focus(function () {
        console.log("Focus using dxButton().");
    });

    button.focus(function () {
        console.log("Focus using button.");
    });

    //button.focus();
});