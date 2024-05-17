
import addButton from "./DxComponent/Button.js";
import addCheckBox from "./DxComponent/CheckBox.js";
import addDateBox from "./DxComponent/DateBox.js";
import addDropDown from "./DxComponent/DropDown.js";
import addList from "./DxComponent/List.js";
import addNumberBox from "./DxComponent/NumberBox.js";
import addSelectBox from "./DxComponent/SelectBox.js";
import addTextArea from "./DxComponent/TextArea.js";
import addTextBox from "./DxComponent/TextBox.js";

// setting a field template in myTemplates object
window.myTemplates = {};
window.myTemplates["field"] = '<div class="dx-field"><div class="dx-field-label"></div><div class="dx-field-value"></div></div>';

$(function () {
    // initialize the link container
    $("#linkContainer").dxButtonGroup({
        items: lstLink.map(link => {
            return {
                text: link.text,
                elementAttr: {
                    'data-handler-name': link.handler,
                    'style': 'display: block !important;'
                },
                onClick: function (e) {
                    window.lstDemoHandler[e.itemData.elementAttr['data-handler-name']]();
                }
            }
        })
    });

    // make the link container as display block overridding display:flex
    $("#linkContainer .dx-buttongroup-wrapper")
        //.addClass("custom-link-list");

    // attack the demo handlers to window object globally
    window.lstDemoHandler = {
        addButton,
        addCheckBox,
        addDateBox,
        addDropDown,
        addList,
        addNumberBox,
        addSelectBox,
        addTextArea,
        addTextBox,
    };


    //var button = $("#container").dxButton({
    //    text: "submit",
    //    onInitialized: function () {
    //        console.log("onInitialized");
    //    },
    //    onContentReady: function () {
    //        console.log("onContentReady");
    //    }
    //});

    //// beginUpdate and endUpdate
    //var button = $("#container").dxButton({
    //    text: "Click me",
    //    onClick: function () {
    //        // Begin batch updates
    //        button.beginUpdate();
    //        //button.option("text", "Click me 2");
    //        button._options._optionManager._options.text = "Click me 2";

    //        // Toggle a CSS class on an element
    //        //$("#container").toggleClass("highlight");
    //        var currentClass = $("#container").dxButton("option", "elementAttr")["class"];
    //        if (!currentClass) {
    //            currentClass = "";
    //        }
    //        var newClass = currentClass.includes("highlight") ? currentClass.replace("highlight", "") : currentClass + " highlight";
    //        button.option("elementAttr", { "class": newClass });

    //        // Simulate a time-consuming operation
    //        setTimeout(function () {
    //            button.repaint();

    //            // End batch updates
    //            button.endUpdate();
    //        }, 1000); // Wait for 1 second before ending updates
    //    }
    //}).dxButton("instance");
});