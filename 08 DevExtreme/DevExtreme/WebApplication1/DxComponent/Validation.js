import { renewBaseContainer } from "../Utility/Container.js";

export default function addValidation() {
    const baseContainer = renewBaseContainer();
    const container = $("<div>", { id: "container" }).appendTo(baseContainer);

    const nameTBContainer = $("<div>").dxTextBox({
        label: "Enter Name",
    });
}