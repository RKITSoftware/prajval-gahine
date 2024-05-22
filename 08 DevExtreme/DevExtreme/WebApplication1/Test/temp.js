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



// button with begin and update

    //var button = $("#container").dxButton({
    //    text: "submit",
    //    onInitialized: function () {
    //        console.log("onInitialized");
    //    },
    //    onContentReady: function () {
    //        console.log("onContentReady");
    //    }
    //});

    //// beginUpdate and endUpdate
    //var button = $("#container").dxButton({
    //    text: "Click me",
    //    onClick: function () {
    //        // Begin batch updates
    //        button.beginUpdate();
    //        //button.option("text", "Click me 2");
    //        button._options._optionManager._options.text = "Click me 2";

    //        // Toggle a CSS class on an element
    //        //$("#container").toggleClass("highlight");
    //        var currentClass = $("#container").dxButton("option", "elementAttr")["class"];
    //        if (!currentClass) {
    //            currentClass = "";
    //        }
    //        var newClass = currentClass.includes("highlight") ? currentClass.replace("highlight", "") : currentClass + " highlight";
    //        button.option("elementAttr", { "class": newClass });

    //        // Simulate a time-consuming operation
    //        setTimeout(function () {
    //            button.repaint();

    //            // End batch updates
    //            button.endUpdate();
    //        }, 1000); // Wait for 1 second before ending updates
    //    }
    //}).dxButton("instance");


// old linkContainer impln

    //const linkContainer = $("#linkContainer").dxButtonGroup({
    //    items: lstLink.map((link, index) => {
    //        return {
    //            text: `${link.text} ${index}`,
    //            elementAttr: {
    //                'data-handler-name': link.handler,
    //                'style': 'display: block !important;',
    //                accessKey: index,
    //            },
    //            onClick: function (e) {
    //                window.lstDemoHandler[e.itemData.elementAttr['data-handler-name']]();
    //            }
    //        }
    //    })
    //});
    //let lstButtons = linkContainer.find(".dx-button");
    //lstButtons.each(function (index, button) {
    //    $(button).attr("tabindex", "0");
    //});
    //$(document).keydown(function (e) {
    //    if (e.ctrlKey && e.altKey) {
    //        if (e.key.toUpperCase() == "F") {
    //            lstButtons.eq(9).focus();
    //        }
    //    }
    //});
    //$(document).on("keydown", function (e) {
    //    if (e.ctrlKey && e.altKey) {
    //        if (e.key.toUpperCase() == "F") {
    //            lstButtons.eq(9)[0].focus();
    //        }
    //    }
    //});

    // make the link container as display block overridding display:flex
    //$("#linkContainer .dx-buttongroup-wrapper")
        //.addClass("custom-link-list");