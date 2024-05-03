(function () {

    //// initialize the link container
    //$("#linkContainer").dxButtonGroup({
    //    items: lstLink.map(link => {
    //        return {
    //            text: link.text,
    //            elementAttr: {
    //                'data-handler-name': link.handler,
    //                'style': 'display: block !important;'
    //            },
    //            onClick: function (e) {
    //                window.lstDemoHandler[e.itemData.elementAttr['data-handler-name']]();
    //            }
    //        }
    //    })
    //});

    //// make the link container as display block overridding display:flex
    //$("#linkContainer .dx-buttongroup-wrapper").addClass("custom-link-list");

    //// attack the demo handlers to window object globally
    //window.lstDemoHandler = {
    //    addButton: function () {
    //        let container = $("#container");

    //        if (container) {
    //            // dispose the main container
    //            container.remove();
    //        }

    //        // append #container after linkContainer
    //        $("#linkContainer").after('<div id="container"></div>');

    //        container = $("#container");

    //        // creating a button instance inside #container
    //        container.dxButton();

    //        // get the dx button reference
    //        const button = container.dxButton("instance");

    //        // access some properties of button using option method
    //        button.option("text", "submit");
    //        button.option("focusStateEnabled", true);
    //        button.option("hint", "This is a submit button.");

    //        // attach some click handler to the button
    //        button.option("onClick", function () {
    //            alert("button on clicked 1.");
    //        });

    //        // it overwrite above handler
    //        button.option("onClick", function () {
    //            alert("button on clicked 2.");
    //        });

    //        // another way to attach events
    //        button.on("click", function () {
    //            alert("button clicked 3.");
    //        });

    //        button.on("click", function () {
    //            alert("button clicked 4.");
    //        });

    //        // attaching a custom event
    //        button.on("prajval", function () {
    //            alert("prajval 1.");
    //        });

    //        // above code is as good as writting this
    //        button?._eventsStrategy._events.prajval._list.push(function () {
    //            alert("prajval 2.");
    //        });

    //        // unsubsribing from an event, it just empty the _list
    //        button.off("prajval");
    //        // button.off("prajval", handler1);

    //        // disposing an UI component
    //        const disposeUiComp = $("#disposeUiComp");
    //        disposeUiComp.dxButton({
    //            text: "Dispose Ui Comp",
    //            onClick: function () {
    //                container.dxButton("dispose");
    //            }
    //        });

    //        // remove the container itself using jQuery .remove method
    //        const removeContainer = $("#removeContainer");
    //        removeContainer.dxButton({
    //            text: "Remove Container",
    //            onClick: function () {
    //                container.remove();
    //            }
    //        });
    //    },
    //    addCheckBox: function () {

    //        let container = $("#container");

    //        if (container) {
    //            // dispose the main container
    //            container.remove();
    //        }

    //        // append #container after linkContainer
    //        $("#linkContainer").after("<div id='container'></div>");

    //        container = $("#container");

    //        const htmlLstCheckBox = lstCheckBox.reduce(function (acc, curr) {
    //            return acc + `
    //        <div class="dx-field" style="width: 288px">
    //            <div class="dx-field-label">${curr.label}</div>
    //            <div class="dx-field-value">
                    //${ curr.label }
    //                <div id="${curr.id}"></div>
    //            </div>
    //        </div>`;
    //        }, "");

    //        container.append(htmlLstCheckBox);

    //        const checkedCbContainer = $("#checkedCbContainer");
    //        checkedCbContainer.dxCheckBox({
    //            value: true,
    //            elementAttr: {
    //                'aria-label': 'Checked'
    //            },
    //            activeStateEnabled: true,
    //            accessKey: "1",
    //        });

    //        const uncheckedCbContainer = $("#uncheckedCbContainer");
    //        uncheckedCbContainer.dxCheckBox({
    //            value: false
    //        });

    //        const disabledCbContainer = $("#disabledCbContainer");
    //        disabledCbContainer.dxCheckBox({
    //            value: true,
    //            disabled: true
    //        });

    //        const handlerCbContainer = $("#handlerCbContainer");
    //        handlerCbContainer.dxCheckBox({
    //            value: true,
    //            onValueChanged: function () {
    //                alert("Value changed.");
    //            }
    //        });

    //        const labledCbContainer = $("#labledCbContainer");
    //        labledCbContainer.dxCheckBox({
    //            value: true,
    //            text: "Labelled"
    //        });
    //    }
    //};

        
    var button = $("#container").dxButton({
        text: "submit",
        onInitialized: function () {
            console.log("onInitialized");
        },
        onContentReady: function () {
                console.log("onContentReady");
        }
    })
})();