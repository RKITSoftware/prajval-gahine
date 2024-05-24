
import addButton from "./DxComponent/Button.js";
import addCheckBox from "./DxComponent/CheckBox.js";
import addDateBox from "./DxComponent/DateBox.js";
import addDropDown from "./DxComponent/DropDown.js";
import addList from "./DxComponent/List.js";
import addNumberBox from "./DxComponent/NumberBox.js";
import addSelectBox from "./DxComponent/SelectBox.js";
import addTextArea from "./DxComponent/TextArea.js";
import addTextBox from "./DxComponent/TextBox.js";
import addFileUploader from "./DxComponent/FileUploader.js";
import addValidation from "./DxComponent/Validation.js";

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
    addFileUploader,
    addValidation
};

// setting a field template in myTemplates object
window.myTemplates = {};
window.myTemplates["field"] = '<div class="dx-field"><div class="dx-field-label"></div><div class="dx-field-value"></div></div>';

$(function () {

    function getUrlHashValue() {
        let hashValue = window.location.href.split("#")[1];
        if (hashValue) {
            return decodeURI(hashValue);
        }
        return "Button";
    }

    // initialize the link container
    $("#linkContainer").dxSelectBox({
        items: lstLink,
        valueExpr: "text",
        displayExpr: "text",
        value: getUrlHashValue(),
        onValueChanged: function (e) {
            //window.lstDemoHandler[e.itemData.elementAttr['data-handler-name']]();
        },
        onSelectionChanged: function (e) {
            window.lstDemoHandler[e.selectedItem.handler]();
        },
        searchEnabled: true,
    });
});