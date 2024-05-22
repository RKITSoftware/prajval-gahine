

import { renewBaseContainer } from "../Utility/Container.js";

export default function addCheckBox() {

    const baseContainer = renewBaseContainer();
    const container = $("<div>", { id: "container" }).appendTo(baseContainer);

    const htmlLstCheckBox = lstCheckBox.reduce(function (acc, curr) {
        return acc + `
            <div class="dx-field" style="width: 288px">
                <div class="dx-field-label">${curr.label}</div>
                <div class="dx-field-value">
                    ${curr.label}
                    <div id="${curr.id}"></div>
                </div>
            </div>`;
    }, "");

    container.append(htmlLstCheckBox);

    const checkedCbContainer = $("#checkedCbContainer");
    checkedCbContainer.dxCheckBox({
        value: true,
        elementAttr: {
            id: "checked1",
            'aria-label': 'Checked'
        },
        activeStateEnabled: true,
        accessKey: "1",
    });

    const uncheckedCbContainer = $("#uncheckedCbContainer");
    uncheckedCbContainer.dxCheckBox({
        value: false
    });

    const disabledCbContainer = $("#disabledCbContainer");
    disabledCbContainer.dxCheckBox({
        value: true,
        disabled: true
    });

    const handlerCbContainer = $("#handlerCbContainer");
    handlerCbContainer.dxCheckBox({
        value: true,
        onValueChanged: function () {
            alert("Value changed.");
        }
    });

    const labledCbContainer = $("#labledCbContainer");
    labledCbContainer.dxCheckBox({
        value: true,
        text: "Labelled"
    });
}