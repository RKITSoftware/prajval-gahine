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