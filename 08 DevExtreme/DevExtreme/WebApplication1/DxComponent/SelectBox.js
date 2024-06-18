import { renewBaseContainer } from "../Utility/Container.js";

export default function addSelectBox() {
    const baseContainer = renewBaseContainer();
    const container = $("<div>", { id: "container" }).appendTo(baseContainer);


    const lstProduct = [
        {
            price: 399,
            name: "Lenovo ideapad slim 3",
        },
        {
            price: 1999,
            name: "Apple mac book pro",
        },
        {
            price: 899,
            name: "Samsung galaxy book 2",
        },
        {
            price: 599,
            name: "Asus handlbook",
        },
    ];

    const itemsSelectBox = $(window.myTemplates["field"]);

    itemsSelectBox.find(".dx-field-label").text("Items Select Box");
    const selectBoxWidget = itemsSelectBox.find(".dx-field-value").dxSelectBox({
        items: [...lstProduct],
        acceptCustomValue: true,
        searchEnabled: true,
        searchTimeout: 50,
        //showClearButton: true,
        openOnFieldClick: false,
        displayExpr: "name",
        valueExpr: "price",
        buttons: [
            'clear',
            {
                name: "highPrice",
                location: "after",
                options: {
                    icon: "arrowup",
                    onClick: function (e) {
                        let highPriceItem = selectBoxWidget.option("items").reduce((acc, item) => {
                            return (item.price > acc.price) ? item : acc;
                        }, { price: -1 });
                        selectBoxWidget.option("value", highPriceItem.price);
                    }
                },
            },
            "dropDown",
        ]
    }).dxSelectBox("instance");

    const dsSelectBox = $(window.myTemplates["field"]);

    dsSelectBox.find(".dx-field-label").text("DS Select Box");
    const dsSelectBoxWidget = dsSelectBox.find(".dx-field-value").dxSelectBox({
        grouped: true,
        groupTemplate(data) {
            return $(`<div class='custom-icon'><span class='dx-icon-box icon'></span> ${data.key}</div>`);
        },
        acceptCustomValue: true,
        //dropDownOptions: {
        //    contentTemplate: function (container) {
        //        var searchValue = 
        //    }
        //},
        itemTemplate(data) {
            return `<div class='custom-item'><div class="custom-item-image"><img alt='Product name' src='${data.imgSrc}' /></div><div class='product-name'>${data.name}</div></div>`;
        },
        fieldTemplate(data, container) {
            let result;
            if (data == null) {
                result = $("<div>")
                    .dxTextBox({
                        placeholder: "Select Product",
                        readOnly: false,
                    });
            }
            else {
                result = $(`<div class='custom-item'><div class="custom-item-image"><img alt='Product name' src='${data ? data.imgSrc : ''
                    }' /></div><div class='product-name'></div></div>`);

                result
                    .find('.product-name')
                    .dxTextBox({
                        value: data && data.name,
                        readOnly: false,
                        inputAttr: { 'aria-label': 'Name' },
                    });
            }
            container.append(result);
        },
        dataSource: [
            {
                key: "Laptop",
                items: [
                    {
                        price: 399,
                        name: "Lenovo ideapad slim 3",
                        imgSrc: "Assests/Laptop/Lenovo ideapad slim 3.jpg",
                    },
                    {
                        price: 1999,
                        name: "Apple mac book pro",
                        imgSrc: "Assests/Laptop/Apple mac book pro.jpg",
                    },
                    {
                        price: 899,
                        name: "Samsung galaxy book 2",
                        imgSrc: "Assests/Laptop/Samsung galaxy book 2.jpg",
                    },
                    {
                        price: 599,
                        name: "Asus handlbook",
                        imgSrc: "Assests/Laptop/Asus handlbook.jpg",
                    },
                ]
            },
            {
                key: "Mobile",
                items: [
                    {
                        price: 199,
                        name: "Redim 14",
                        imgSrc: "Assests/Mobile/Redim 14.jpg",
                    },
                    {
                        price: 799,
                        name: "Oneplus 12",
                        imgSrc: "Assests/Mobile/Oneplus 12.jpg",
                    },
                    {
                        price: 1699,
                        name: "IPhone 15 pro max",
                        imgSrc: "Assests/Mobile/IPhone 15 pro max.jpg",
                    },
                    {
                        price: 349,
                        name: "Iqoo neo 9T sdhuashdaksjdhajhdakjhdkajsdhkjahdkjaksjkdhkajhsdkjah",
                        imgSrc: "Assests/Mobile/Iqoo neo 9T.jpg",
                    },
                ]
            }
        ],
        acceptCustomValue: true,
        showClearButton: true,
        searchEnabled: true,
        searchTimeout: 1,
        openOnFieldClick: false,
        displayExpr: "name",
        valueExpr: "price",
        wrapItemText: true,
        onInitialized: function (e) {
            e.component.getDataSource().load();
        }
    }).dxSelectBox("instance");
    window.dsSelectBoxWidget = dsSelectBoxWidget;
    // search and edit select box
    const sESelectBoxFieldWrapper = $(window.myTemplates["field"]);
    sESelectBoxFieldWrapper.find(".dx-field-label").text("Search & Edit");
    let instance1 = sESelectBoxFieldWrapper.find(".dx-field-value").dxSelectBox({
        acceptCustomValue: true,
        dataSource: [
            {
                id: 1,
                name: "Google",
                location: "US"
            },
            {
                id: 2,
                name: "Apple",
                location: "UK",
            }
        ],
        onInitialized: function (e) {
            e.component.getDataSource().load();
        },
        displayExpr: function (item) {
            return item && `${item.name} ${item.location}`;
        },
        valueExpr: "id",
        // onCustomItemCreating => onChange => onSelectionChanged => onValueChanged
        onSelectionChanged: function (e) {
        },
        onChange: function (e) {
        },
        onValueChanged: function (e) {
        },
        onCustomItemCreating(e) {

            // if empty string then set customItem to null
            if (!e.text) {
                e.customItem = null;
                return;
            }

            let nameLocation = e.text.split(" ");
            if (nameLocation.length != 2) {
                e.customItem = null;
                return;
            }

            // check if text is already present in store
            let companyDataStore = e.component.getDataSource();
            let extItem = companyDataStore.items().filter(company => company.name === nameLocation[0])?.[0];
            if (extItem) {
                // if yes then set customItem to existing item, updating ui will be done implicity based on customItem
                e.customItem = extItem;
                return;
            }

            // text is not present in store, then add a new iten to store
            const lstCompanyID = companyDataStore.items().map((item) => item.id);
            const nextID = Math.max.apply(null, lstCompanyID) + 1;

            // create a new item
            const newItem = {
                id: nextID,
                name: nameLocation[0],
                location: nameLocation[1],
            };

            // insert the new item to store
            e.customItem = companyDataStore.store().insert(newItem)
                .then(function () {
                    return companyDataStore.load();
                })
                .then(function () {
                    let x = { ...newItem };
                    //x = {
                    //    id: 1,
                    //    name: "JhagJhag"
                    //}
                    return x;
                });
        },
        useItemTextAsTitle: true,
        spellCheck: true,
    }).dxSelectBox("instance");

    instance1.on("cut", function () {
        alert("CUT");
        if (confirm("Want to remove cut?")) {
            instance1.off("cut");
        }
    })

    var testArray = [
        {
            key: "Group 1",
            items: [
                { name: "guj" },
                { name: "rgb" }
            ]
        },
        {
            key: "Group 2",
            items: [
                { name: "mh" },
                { name: "gujarat" }
            ]
        }
    ];

    const testSelectBox = $("<div>")
        .dxSelectBox({
            acceptCustomValue: true,
            searchEnabled: true,
            valueExpr: "name",
            displayExpr: "name",
            dataSource: testArray,
            displayExpr: "name",
            valueExpr: "name",
            grouped: true,
            groupTemplate: function (data) {
                return `<div>${data.key}</div>`;
            },
            searchEnabled: true,
            searchExpr: ["name", "key"], // Enable searching on both item name and group key
            itemTemplate: function (data) {
                return `<div>${data.name}</div>`;
            }
        });

    container.append(testSelectBox);
    container.append(itemsSelectBox);
    container.append(dsSelectBox);
    container.append(sESelectBoxFieldWrapper);
}