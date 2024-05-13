export default function addButton() {
    let container = $("#container");

    if (container) {
        // dispose the main container
        container.remove();
    }

    // append #container after linkContainer
    $("#linkContainer").after('<div id="container"></div>');

    container = $("#container");

    // creating a button instance inside #container
    container.dxButton();

    // get the dx button reference
    const button = container.dxButton("instance");

    // access some properties of button using option method
    button.option("text", "submit");
    button.option("focusStateEnabled", true);
    button.option("activeStateEnabled", true);
    button.option("hint", "This is a submit button.");

    // attach some click handler to the button
    button.option("onClick", function () {
        alert("button on clicked 1.");
    });

    // it overwrite above handler
    button.option("onClick", function () {
        alert("button on clicked 2.");
    });

    // another way to attach events
    button.on("click", function () {
        alert("button clicked 3.");
    });

    button.on("click", function () {
        alert("button clicked 4.");
    });

    // attaching a custom event
    button.on("prajval", function () {
        alert("prajval 1.");
    });

    // above code is as good as writting this
    button?._eventsStrategy._events.prajval._list.push(function () {
        alert("prajval 2.");
    });

    // unsubsribing from an event, it just empty the _list
    button.off("prajval");
    // button.off("prajval", handler1);

    // disposing an UI component
    const disposeUiComp = $("#disposeUiComp");
    disposeUiComp.dxButton({
        text: "Dispose Ui Comp",
        onClick: function () {
            container.dxButton("dispose");
        }
    });

    // remove the container itself using jQuery .remove method
    const removeContainer = $("#removeContainer");
    removeContainer.dxButton({
        text: "Remove Container",
        onClick: function () {
            container.remove();
        }
    });
}