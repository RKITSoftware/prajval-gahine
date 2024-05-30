
import { renewBaseContainer } from "../Utility/Container.js";

export default function DateBox() {
    const baseContainer = renewBaseContainer();
    const container = $("<div>", { id: "container" }).appendTo(baseContainer);

    container.append("<div id='dateContainer'></div>");
    container.append("<div id='timeContainer'></div>");
    container.append("<div id='dateTimeContainer'></div>");

    const millisecondsInDay = 24 * 60 * 60 * 1000;

    const dateBox = $("#dateContainer").dxDateBox({
        type: "date",
        displayFormat: "dd-MM-yyyy",
        //value: new Date(2024, 0, 15).getTime(),
        //dateSerializationFormat: "yyyy-dd-MM",
        applyValueMode: "instantly",    // useButtons,
        //activeStateEnabled: true,
        //buttons: [
        //    {
        //        name: 'prevDate',
        //        location: 'before',
        //        options: {
        //            icon: 'spinprev',
        //            stylingMode: 'text',
        //            onClick() {
        //                const currentDate = dateBox.option('value');
        //                dateBox.option('value', currentDate - millisecondsInDay);
        //            },
        //        },
        //    },
        //    {
        //        name: 'nextDate',
        //        location: 'after',
        //        options: {
        //            icon: 'spinnext',
        //            stylingMode: 'text',
        //            onClick() {
        //                const currentDate = dateBox.option('value');
        //                dateBox.option('value', currentDate + millisecondsInDay);
        //            },
        //            disabled: false
        //        }
        //    },
        //    "dropDown"
        //],
        pickerType: "calendar", // native, rollers, list (only when type is time)
        cancelButtonText: "Drop",
        min: new Date(2024, 0, 13),
        max: new Date(2024, 0, 23),
        dateOutOfRangeMessage: "Not in range.",
        deferRendering: true,
        //disabled: true,
        //disabledDates: true,
        //dropDownButtonTemplate: function (icon, text) {
        //    icon = "ddropdowneditor-icon";
        //    icon = "chevrondown";
        //    text.prepend(`<span class="dx-icon-${icon}"></span>`);
        //},
        hint: "This is my date picker",
        //onChange: function (eventData) {
        //    // is triggered when value is changed only by user interaction
        //    console.log(eventData);
        //    let lstPart = eventData.event.target.value.split('-');
        //    eventData.component.option("value", new Date(lstPart[2], lstPart[1] - 1, lstPart[0]).getTime());
        //    console.log("date changed");
        //},
        //onClosed: function (eventData) {
        //    // is invoker after a delay of 500 ms, b/c of animation
        //    console.log(eventData);
        //    console.log("datebox popup closed");
        //},
        //onContentReady: function (eventData) {
        //    console.log(eventData);
        //    console.log("datebox is ready");
        //},
        //onCopy: function (eventData) {
        //    console.log(eventData);
        //    console.log("date copied");
        //},
        //onCut: function (eventData) {
        //    console.log(eventData);
        //    console.log("date cut");
        //},
        //onPaste: function (eventData) {
        //    console.log(eventData);
        //    console.log("date paste");
        //},
        //onDisposing: function (eventData) {
        //    console.log(eventData);
        //    console.log("datebox disposing.");
        //},
        //onEnterKey: function (eventData) {
        //    console.log(eventData);
        //    console.log("datebox enter key pressed while focus.");
        //},
        //onInput: function (eventData) {
        //    console.log(eventData);
        //    console.log("input changed");
        //},
        //onOpened: function (eventData) {
        //    console.log(eventData);
        //    console.log("datebox popped up.");
        //},
        //onOptionChanged: function (eventData) {
        //    console.log(eventData);
        //    console.log("hoverStateEnabled", eventData.component._options._optionManager._options.hoverStateEnabled);
        //    console.log("datebox option changed.");
        //},
        //onValueChanged: function (eventData) {
        //    // is triggered when value is changed programatically or user interaction
        //    console.log(eventData);
        //    console.log("value changed programtically or user interaction.");
        //},
        openOnFieldClick: false,  //defalt false,
        placeholder: "select a date.",
        //readOnly: true,  // default false
        //useMaskBehavior: true,
        validationMessageMode: "always",
        validationStatus: "invalid",
        valueChangeEvent: "change blur",
        //visible: false,

    }).dxDateBox("instance");

    $("#timeContainer").dxDateBox({
        type: "time",
        displayFormat: "HH:mm:ss",
        value: new Date(),
        pickerType: "native",   // native
        interval: 15,
        invalidDateMessage: "Invalid date - custom"
    });

    const minDateWidgetContainer = $("<div>").dxDateBox({
        placeholder: "Select min date.",
        value: null,
        displayFormat: "dd-MM-yyyy",
        onValueChanged: function (e) {
            maxDateWidget.option("min", e.value);
        }
    });
    const minDateWidget = minDateWidgetContainer.dxDateBox("instance");

    const maxDateWidgetContainer = $("<div>").dxDateBox({
        placeholder: "Select max date.",
        value: null,
        onValueChanged: function (e) {
            minDateWidget.option("max", e.value);
        },
        displayFormat: "dd-MM-yyyy",
    });
    const maxDateWidget = maxDateWidgetContainer.dxDateBox("instance");

    container.append(minDateWidgetContainer);
    container.append(maxDateWidgetContainer);

    //container.dxDateBox({
    //    type: "date",
    //    value: new Date(),
    //    //adaptivityEnabled: false,
    //    applyButtonText: "PNG",
    //    acceptCustomValue: false,   // default - true,
    //    accessKey: "j",
    //    activeStateEnabled: true, // default - true,
    //    onValueChanged: function (e) {
    //        alert("Selected date: " + e.value);
    //    }
    //});
}