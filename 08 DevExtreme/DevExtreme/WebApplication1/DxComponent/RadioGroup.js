import { renewBaseContainer } from "../Utility/Container.js";

export default function addRadioGroup(){
    const baseContainer = renewBaseContainer();
    const container = $("<div>", { id: "container" }).appendTo(baseContainer);

    // default radio group
    const defaultRadioGroupContainer = $("<div>", { class: "dx-field" });
    $("<div>", { class: "dx-field-label", text: "Default Radio Group" }).appendTo(defaultRadioGroupContainer);
    $("<div>", { class: "dx-field-value" }).dxRadioGroup({
        items: ["m", "f", "o"],
        value: "m",
    }).appendTo(defaultRadioGroupContainer);

    // horizontal radio group
    const horizonRadioGroupContainer = $("<div>", { class: "dx-field" });
    $("<div>", { class: "dx-field-label", text: "Default Radio Group" }).appendTo(horizonRadioGroupContainer);
    $("<div>", { class: "dx-field-value" }).dxRadioGroup({
        items: ["maths", "physics", "chemistry"],
        value: "maths",
    }).appendTo(horizonRadioGroupContainer);


    const subjectFactultyMap = {
        "maths": {
            time: "7 am to 9 am",
            faculty: "Gaurav"
        },
        "physics": {
            time: "2 pm to 4 pm",
            faculty: "Amin"
        },
        "chemistry": {
            time: "6 pm to 9 pm",
            faculty: "Rahul"
        },
    };

    const factulyInfo = $("<div>", { id: "factulyInfo" });
    factulyInfo.setFacultyInfo = function (info) {
        factulyInfo.empty();
        this.append($("<p>", { text: info.faculty }));
        this.append($("<p>", { text: info.time }));
    }

    // event radio group
    const eventRadioGroupContainer = $("<div>", { class: "dx-field" });
    $("<div>", { class: "dx-field-label", text: "Default Radio Group" }).appendTo(eventRadioGroupContainer);
    $("<div>", { class: "dx-field-value" }).dxRadioGroup({
        items: ["maths", "physics", "chemistry"],
        value: "maths",
        onInitialized: function (e) {
            const info = subjectFactultyMap[e.component.option("value")];
            factulyInfo.setFacultyInfo(info);
        },
        onValueChanged: function (e) {
            const info = subjectFactultyMap[e.value];
            factulyInfo.setFacultyInfo(info);
        }
    }).appendTo(eventRadioGroupContainer);

    container.append([
        defaultRadioGroupContainer,
        horizonRadioGroupContainer,
        eventRadioGroupContainer,
        factulyInfo
    ]);
}