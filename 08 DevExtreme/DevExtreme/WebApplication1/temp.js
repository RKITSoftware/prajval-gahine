$("<div>").appendTo(customDDContainer).dxDropDownBox({
    dataSource: lstCountry,
    placeholder: "Select Country",
    contentTemplate: function (args) {
        return $("<div>").dxList({
            dataSource: args.component.getDataSource(),
            selectionMode: "single",
            itemTemplate: function (itemData) {
                return $("<div>").text(itemData);
            },
            onSelectionChanged: function (eventData) {
                args.component.option("value", eventData.itemData.id);
                args.component.close();
            }
        });
    }
});


$("<div>").appendTo(customDDContainer).dxDropDownBox({
    dataSource: [...lstCountry],
    placeholder: "Select Country",
    //value: lstCountry[0],
    contentTemplate: function (args) {
        return $("<div>").dxList({
            dataSource: args.component.getDataSource(),
            showSelectionControls: "single",
            itemTemplate: function (countryName) {
                return $("<div>").text(countryName);
            },
            showSelectionControls: true,
            onSelectionChanged: function (eventData) {
                args.component.option("value", eventData.itemData.id);
                args.component.close();
            }
        });
    }
});





window.myTemplates = {};
window.date1 = performance.now();
window.myTemplates["field2"] = $("<div>", { class: "dx-field" })
    .append($("<div>", { class: "dx-field-label" }).text("No Text"))
    .append($("<div>", { class: "dx-field-value" }));
window.date2 = performance.now();
window.field2TimeDiff = date2 - date1;

window.date1 = performance.now();
window.myTemplates["field"] = '<div class="dx-field"><div class="dx-field-label"></div><div class="dx-field-value"></div></div>';
window.date2 = performance.now();
window.fieldTimeDiff = date2 - date1;


//--------------------------------
window.date1 = performance.now();
var simpleContainer = window.myTemplates["field2"].clone();
window.date2 = performance.now();
window.field2TimeDiff += date2 - date1;


window.date1 = performance.now();
var simpleContainer = $(window.myTemplates["field"]);
window.date2 = performance.now();
window.fieldTimeDiff += date2 - date1;

console.log("clone", field2TimeDiff);
console.log("string", fieldTimeDiff);