import { renewBaseContainer } from "../Utility/Container.js";

export default function addValidation() {
    const baseContainer = renewBaseContainer();
    const container = $("<div>", { id: "container" }).appendTo(baseContainer);

    const nameTBContainer = $("<div>")
        .dxTextBox({
            label: "Enter username",
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
                    validationCallback: function () {

                    }
                }
            ],
        });
}