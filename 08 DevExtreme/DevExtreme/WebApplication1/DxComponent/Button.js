export default function addButton() {
    let container = $("#container");

    if (container) {
        // dispose the main container
        container.remove();
    }

    // append #container after linkContainer
    $("#linkContainer").after('<div id="container"></div>');

    container = $("#container");

    const buttonContainer = $("<div>", {id: "buttonContainer", class: "myClass, dx-myClass", style: "display: block; height: 100px; width: 100px; background-color: green;" }).appendTo(container);

    // creating a button instance inside #container
    buttonContainer.dxButton();

    // get the dx button reference
    const button = buttonContainer.dxButton("instance");

    // access some properties of button using option method
    button.option("text", "submit");
    button.option("focusStateEnabled", true);
    button.option("activeStateEnabled", true);
    button.option("hint", "This is a submit button.");

    // attach some click handler to the button
    button.option("onClick", function () {
        alert("Option's on click 1.");
    });

    // it overwrite above handler
    button.option("onClick", function () {
        alert("Option's on click 2.");
    });

    // another way to attach events
    button.on("click", function () {
        alert("On method on click 1.");
    });

    button.on("click", function () {
        alert("On method on click 2.");
    });

    // attaching a custom event
    button.on("prajval", function () {
        alert("prajval 1.");
    });

    // above code is as good as writting this
    button?._eventsStrategy._events.prajval._list.push(function () {
        alert("List pushed on click.");
    });

    // unsubsribing from an event, it just empty the _list
    //button.off("prajval");
    // button.off("prajval", handler1);

    // disposing an UI component
    const disposeButtonContainer = $("<div>", { id: "disposeButtonContainer" }).appendTo(container);
    disposeButtonContainer.dxButton({
        text: "Dispose Ui Comp",
        onClick: function () {
            buttonContainer.dxButton("dispose");
        }
    });

    // remove the container itself using jQuery .remove method
    const removeButtonContainer = $("<div>", { id: "removeButtonContainer" }).appendTo(container);
    removeButtonContainer.dxButton({
        text: "Remove Container",
        onClick: function () {
            buttonContainer.remove();
        }
    });
}