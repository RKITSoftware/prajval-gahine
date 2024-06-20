
import { renewBaseContainer } from "../Utility/Container.js";

export default function addNumberBox() {
    const baseContainer = renewBaseContainer();
    const container = $("<div>", { id: "container" }).appendTo(baseContainer);


    var simpleContainer = $(window.myTemplates["field"]);

    simpleContainer.find(".dx-field-label").text("Simple number box");
    simpleContainer.find(".dx-field-value").dxNumberBox({
        value: 20.5,    // default 0
        placeholder: "Enter number",
        showSpinButtons: true,
        showClearButton: true,
    });

    var numberRangeontainer = $(window.myTemplates["field"]);

    numberRangeontainer.find(".dx-field-label").text("Range number box");
    numberRangeontainer.find(".dx-field-value").dxNumberBox({
        placeholder: "Enter age",
        min: 18,
        max: 65
    });

    var formateNumberContainer = $("<div>").append(window.myTemplates["field"]);

    formateNumberContainer.find(".dx-field-label").text("Number format");

    function toHindiNumerals(number) {
        var numbers = ['०', '१', '२', '३', '४', '५', '६', '७', '८', '९'];
        return number.toString().replace(/\d/g, function (match) {
            return numbers[parseInt(match)];
        });
    }

    var formatNumberWidget = formateNumberContainer.find(".dx-field-value").dxNumberBox({
        placeholder: "Enter Number",
        //value: 123456.78,
        //value: "P123N456G789",
        value: "123456789",
        format2: {
            type: "currency",
            currency: "INR",  
            precision: 2,
            //locale: "en-IN", // English (India)
            formatter: function (value) {
                 //formatter run several time at startup
                console.log("formatter", value);
                //return toHindiNumerals(value);
                //return value;
                return "7";
            }
        },
        format: {
            type: "custom",
            formatter: function (value) {
                console.log(value);
                // Custom formatting logic
                //return "USD " + value.toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,');
                console.log("input", value);
                console.log("output", value.toString().replace(/\d/g, "$&a"));
                //return "USD 22A" + value.toFixed(2).replace(/\d(?=(\d{3})+\.)/g, '$&,') + " P922NG";
                return value.toString().replace(/\d/g, "$&a");
                //return toHindiNumerals(value).toString();
            }
        },
        //format: "#,##0.00",
        showClearButton: true,
        showSpinButtons: true
    }).dxNumberBox("instance");
    window.formatNumberWidget = formatNumberWidget;

    const dropDownContainer = $("<div>").appendTo(formateNumberContainer);

    dropDownContainer.dxDropDownBox({
        acceptCustomValue: true,
        openOnFieldClick: false,
        items: ["#%", "0", "#0", "00", "#00", "#.00", "#.#", "0.##", "0.00", "#0.#", ",#", "#0.##;(#0.##)"],
        placeholder: "Select format",
        onValueChanged: function (e) {
            formatNumberWidget.option("format", e.value === "" ? null : e.value);
        },
        contentTemplate: function (args) {
            return $("<div>").dxList({
                items: args.component.option("items"),
                selectionMode: "single",
                onSelectionChanged: function (e) {
                    args.component.option("value", e.addedItems[0]);
                    args.component.close();
                }
            });
        }
    });

    dropDownContainer.dxDropDownBox("instance");

    var modeNumberContainer = $(window.myTemplates["field"]);

    modeNumberContainer.find(".dx-field-label").text("Phone number");
    modeNumberContainer.find(".dx-field-value").dxNumberBox({
        placeholder: "Enter phone",
        mode: "tel"
    });

    container.append(simpleContainer);
    container.append(numberRangeontainer);
    container.append(formateNumberContainer);
    container.append(modeNumberContainer);
}