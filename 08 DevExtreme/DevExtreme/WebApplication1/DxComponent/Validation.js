import { renewBaseContainer } from "../Utility/Container.js";

export default function addValidation() {
    const baseContainer = renewBaseContainer();
    const container = $("<div>", { id: "container", class: "demo-box short-width-box" }).appendTo(baseContainer);

    const mapping = {
        Mr: "male",
        Mrs: "female"
    }

     // ------------------------------------------------------------------------------------------------
    // --------------------------------------------- Name ----------------------------------------------
    // -------------------------------------------------------------------------------------------------
    const nameWrapper = $("<div>", { class: "dx-field" });

    $("<div>", { class: "dx-field-label", text: "Name" })
        .appendTo(nameWrapper);

    const nameFieldValue = $("<div>", { class: "dx-field-value", style: "display: flex" });
    nameFieldValue.appendTo(nameWrapper);


    $("<div>").dxSelectBox({
        items: ["Mr", "Mrs"],
        elementAttr: {style: "width: 85px;"},
        value: "Mr",
    }).appendTo(nameFieldValue);

    $("<div>")
        .dxTextBox({
            placeholder: "Enter name",
        })
        .dxValidator({
            validationRules: [
                {
                    type: "required",
                    message: "Name is required.",
                }
            ],
        })
        .appendTo(nameFieldValue);


    // -------------------------------------------------------------------------------------------------
    // ------------------------------------------- Username --------------------------------------------
    // -------------------------------------------------------------------------------------------------
    const usernameWrapper = $("<div>", { class: "dx-field" });
    $("<div>", { text: "Username", class: "dx-field-label" }).appendTo(usernameWrapper);
    const usernameContainer = $("<div>", { class: "dx-field-value" })
        .dxTextBox({
            label: "Enter username",
            placeholder: "Enter username",
        })
        .dxValidator({
            validationRules: [
                {
                    type: "required",
                    message: "Username is required.",
                },
                {
                    type: "email",
                    message: "Invalid email.",
                },
                {
                    type: "async",
                    message: "Email not register.",
                    validationCallback: function (e) {
                        let d = $.Deferred();
                        setTimeout(() => {
                            d.resolve(lstUsername.includes(e.value));
                        }, 2000);
                        return d.promise();
                    }
                }
            ],
        });
    usernameContainer.appendTo(usernameWrapper);

    // -------------------------------------------------------------------------------------------------
    // ------------------------------------------- Password --------------------------------------------
    // -------------------------------------------------------------------------------------------------
    const passwordWrapper = $("<div>", {class: "dx-field"});

    $("<div>", { class: "dx-field-label", text: "Password" }).appendTo(passwordWrapper);
    const passwordContainer = $("<div>", {class: "dx-field-value"}).dxTextBox({
        //mode: "password",
        mode: "text",
        valueChangeEvent: 'keyup',
        inputAttr: {id: "password"},
        placeholder: "Enter password",
        onValueChanged() {
            if (confirmPasswordWidget.option("value")) {
                confirmPasswordWidget.element().dxValidator("validate");
            }
        }

    });
    passwordContainer.appendTo(passwordWrapper);


    // -------------------------------------------------------------------------------------------------
    // --------------------------------------- Confirm Password ----------------------------------------
    // -------------------------------------------------------------------------------------------------
    const confirmPasswordWrapper = $("<div>", { class: "dx-field" });

    $("<div>", { class: "dx-field-label", text: "Confirm Password" }).appendTo(confirmPasswordWrapper);
    const confirmPasswordContainer = $("<div>", { class: "dx-field-value" })
        .dxTextBox({
            //mode: "password",
            mode: "text",
            placeholder: "Enter confirm Password",
            valueChangeEvent: 'keyup',
        })
        .dxValidator({
            validationRules: [
                {
                    type: "required",
                    message: "Confirm Password is required",
                },
                {
                    type: "compare",
                    comparisonTarget() {
                        return document.getElementById("password").value;
                    },
                    comparisonType: "==",
                    message: "Password and Confirm password donot matched.",
                }
            ]
        });
    const confirmPasswordWidget = confirmPasswordContainer.dxTextBox("instance");
    confirmPasswordContainer.appendTo(confirmPasswordWrapper);


    // -------------------------------------------------------------------------------------------------
    // ---------------------------------------- Date of Birth ------------------------------------------
    // -------------------------------------------------------------------------------------------------
    const dobWrapper = $("<div>", { class: "dx-field" });
    $("<div>", { class: "dx-field-label", text: "DOB" }).appendTo(dobWrapper);
    const dobContainer = $("<div>", { class: "dx-field-value" })
        .dxDateBox({
            placeholder: "Select DOB",
            max: new Date(),
            acceptCustomValue: false,
            openOnFieldClick: true,
        })
        .dxValidator({
            validationRules: [
                {
                    type: "required",
                    message: "DOB is required.",
                },
                {
                    type: "range",
                    max: new Date().setFullYear(new Date().getFullYear() - 18),
                    message: "Age must 18 or above.",
                }
            ],
        })
    dobWrapper.append(dobContainer);

    container.append(
        [
            nameWrapper,
            usernameWrapper,
            passwordWrapper,
            confirmPasswordWrapper,
            dobWrapper
        ]
    );
}