import { renewContainer } from "../Utility/Container.js";

export default function addTextArea() {
    let container = renewContainer("#container");

    // text area
    const textAreaFieldWrapper = $(window.myTemplates["field"]);
    textAreaFieldWrapper.find(".dx-field-label").text("Text Area");
    const textAreaContainer = textAreaFieldWrapper.find(".dx-field-value");

    const textAreaWidget = textAreaContainer.dxTextArea({
        autoResizeEnabled: true,
        onValueChanged: function (e) {
            previewTAWidget.option("value", e.value);
        },
        hint: "a text area",
        hoverStateEnabled: false,
    }).dxTextArea("instance");

    // preview text area
    const previewTAWrapper = $(window.myTemplates["field"]);
    previewTAWrapper.find(".dx-field-label").text("Text Area");
    const previewTAContainer = previewTAWrapper.find(".dx-field-value");

    const previewTAWidget = previewTAContainer.dxTextArea({
        disabled: true,
    }).dxTextArea("instance");

    // select box for configuring text area
    const selectBoxWrapper = $(window.myTemplates["field"]);
    selectBoxWrapper.find(".dx-field-label").text("Configure Text Area");
    const selectBoxContainer = selectBoxWrapper.find(".dx-field-value");
    selectBoxContainer.dxSelectBox({
        value: "change",
        items: [
            {
                name: "On Change",
                handlerName: "change",
            },
            {
                name: "On Input",
                handlerName: "input",
            },
        ],
        displayExpr: "name",
        valueExpr: "handlerName",
        // onSelectionChanged => onValueChanged
        onValueChanged: function (e) {
            textAreaWidget.option("valueChangeEvent", e.value);
        }
    });

    container.append(textAreaFieldWrapper);
    container.append(selectBoxWrapper);
    container.append(previewTAWrapper);
}