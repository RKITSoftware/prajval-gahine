import { renewBaseContainer } from "../Utility/Container.js";

export default function addTextBox() {
    const baseContainer = renewBaseContainer();
    const container = $("<div>", { id: "container" }).appendTo(baseContainer);


    // text box - password
    const passwordTextBoxWrapper = $(window.myTemplates["field"]);
    passwordTextBoxWrapper.find(".dx-field-label").text("Text Box");
    const passwordTextBoxContainer = passwordTextBoxWrapper.find(".dx-field-value");

    passwordTextBoxContainer.dxTextBox({
        mode: "password",
        placeholder: "Enter password",
        hint: "a text box",
        hoverStateEnabled: false,
    });

    // masked text area
    const maskedTextBoxWrapper = $(window.myTemplates["field"]);
    maskedTextBoxWrapper.find(".dx-field-label").text("GST No.");
    const maskedTextBoxContainer = maskedTextBoxWrapper.find(".dx-field-value");

    maskedTextBoxContainer.dxTextBox({
        mask: "00-LLLLL0000L-Z-0",
        maskRules: {L: /[A-Z]/},
        placeholder: "Enter password",
        hint: "a text box",
        hoverStateEnabled: false,
    });


    container.append(passwordTextBoxWrapper);
    container.append(maskedTextBoxWrapper);
}