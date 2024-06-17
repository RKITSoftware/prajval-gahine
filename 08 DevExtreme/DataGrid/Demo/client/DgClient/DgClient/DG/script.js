
//var serverBaseUrl = 'http://localhost:5000/api';
let serverBaseUrl = 'http://localhost:5114/api';

$(async () => {
    const root = $("#root");

    // header
    const main = $("<div>", { id: "main" });
    // footer

    // options
    const userDgOptions = {
        showBorders: true,
        columnAutoWidth: true,
        dataSource: createDs("user"),
        height: "450px",
        columns: [
            {
                dataField: "id",
                caption: "Id",
                allowEditing: false,
                fixed: true,
                fixPosition: "left",
            },
            {
                dataField: "name",
                caption: "Name",
                fixed: true,
                fixPosition: "left",
            },
            {
                dataField: "email",
                caption: "Email",
            },
            {
                caption: "Address",
                headerCellTemplate: function (header, info) {
                    header.parent().css({
                        "text-align": "center",
                    });
                    $(header).append(`<span>${info.column.caption}</span>`);
                },
                columns: [
                    {
                        dataField: "permanentAddress.plotNo",
                        caption: "Plot No"
                    },
                    {
                        dataField: "permanentAddress.street",
                        caption: "Street"
                    },
                    {
                        dataField: "permanentAddress.town",
                        caption: "Town"
                    },
                    {
                        dataField: "permanentAddress.city",
                        caption: "City"
                    },
                    {
                        dataField: "permanentAddress.state",
                        caption: "State"
                    },
                    {
                        dataField: "permanentAddress.pincode",
                        caption: "Pincode"
                    },
                ],
            },
        ],
        onRowUpdating: function (e) {
            //deepMerge(e.newData, e.oldData);
            const newData = {};
            $.extend(true, newData, e.oldData, e.newData);
            e.newData = newData;
        },
        editing: {
            allowAdding: true,
            allowUpdating: true,
            allowDeleting: true,
            mode: "batch"
        },
        scrolling: {
            mode: "infinite",
            preloadEnabled: true,
            rowRenderingMode: "virtual",
        },
        paging: {
            enabled: true,
            pageSize: 10,
            pageIndex: 0,
        },
        remoteOperations: {
            paging: true,
        },
        onContentReady: function (e) {
            contentReadyCallCount++;
        }
    }
    window.contentReadyCallCount = 0;

    const productDgOptions = {
        showBorders: true,
        columnAutoWidth: true,
        dataSource: createDs("product"),
        height: "450px",
        //allowDragging: true,
        rowDragging: {
            group: "xyz",
            //onRemove: function (e) {
            //    e.component._draggedRow = e.component.getKeyByRowIndex(e.fromIndex);
            //},
            //onDragStart: function (e) {
            //    e.component._draggedRow = e.component.getKeyByRowIndex(e.fromIndex);
            //},
        },
        columns: [
            {
                dataField: "id",
                caption: "Id",
                allowEditing: false,
            },
            {
                dataField: "name",
                caption: "Name",
            },
            {
                dataField: "price",
                caption: "Price",
            },
            {
                dataField: "category",
                caption: "Category",
            },
        ],
        onRowUpdating: function (e) {
            //deepMerge(e.newData, e.oldData);
            const newData = {};
            $.extend(true, newData, e.oldData, e.newData);
            e.newData = newData;
        },
        editing: {
            allowAdding: true,
            allowUpdating: true,
            allowDeleting: true,
            mode: "batch"
        },
        paging: {
            enabled: true,
            pageSize: 10,
            pageIndex: 0,
        },
        remoteOperations: {
            paging: true,
        },
        pager: {
            visible: true,
            showNavigationButtons: true,
            showPageSizeSelected: true,
        }
    }

    const orderDgOptions = {
        showBorders: true,
        columnAutoWidth: true,
        columnHidingEnabled: true,
        dataSource: createDs("order"),
        height: "450px",
        allowDragging: true,
        rowDragging: {
            group: "xyz",
            allowDropInsideItem: true,
            //allowReordering: true,
            onAdd: function (e) {
                var popup = $("#temp")
                    .dxPopup({
                        height: 'auto',
                        width: '300px',
                        visible: false,
                        onShown: function (e) {
                            $("#product-quantity").dxNumberBox("instance").focus();
                        },
                        contentTemplate: async function (container) {

                            $("<div>")
                                .dxList({
                                    dataSource: [
                                        {
                                            key: "Product Name",
                                            value: (await e.fromComponent.byKey(e.fromComponent.getKeyByRowIndex(e.fromIndex))).name,
                                        },
                                        {
                                            key: "Order Id",
                                            value: (await e.toComponent.byKey(e.toComponent.getKeyByRowIndex(e.toIndex))).id,
                                        },
                                        {
                                            key: "User Id",
                                            value: (await e.toComponent.byKey(e.toComponent.getKeyByRowIndex(e.toIndex))).userId,
                                        }
                                    ],
                                    itemTemplate: function (data) {
                                        return "<div><b>" + data.key + ":</b> " + data.value + "</div>";
                                    },
                                    height: 'auto'
                                })
                                .appendTo(container);

                            $("<div>")
                                .dxNumberBox({
                                    elementAttr: {
                                        id: "product-quantity",
                                    },
                                    placeholder: "Enter Quantity",
                                    onFocusIn: function (e) {
                                        // Select the entire text when the TextBox gains focus
                                        e.component._input().select();
                                    }
                                })
                                .appendTo(container);

                            $("<div>")
                                .dxButton({
                                    id: "quantity-submit-button",
                                    text: "Submit",
                                    onClick: function () {
                                        var quantity = $("#product-quantity").dxNumberBox("instance").option("value");

                                        $.post(`${serverBaseUrl}/order/product?orderId=${e.toComponent.getKeyByRowIndex(e.toIndex)}&productId=${e.fromComponent.getKeyByRowIndex(e.fromIndex)}&quantity=${quantity}`)
                                            .done(function (response) {
                                                //e.toComponent.refresh();
                                                var productContainer = $(`#containerProduct${e.toComponent.getKeyByRowIndex(e.toIndex)}`);
                                                if (productContainer.length != 0) {
                                                    productContainer.dxDataGrid("instance").refresh();
                                                }
                                            })
                                            .always(function () {
                                                popup.hide();
                                            });
                                    }
                                })
                                .appendTo(container);
                        }
                    })
                    .dxPopup("instance");

                popup.show();

            },
        },
        export: {
            enabled: true,
        },
        onToolbarPreparing: function (e1) {
            e1.toolbarOptions.items.unshift(
                {
                    location: "after",
                    widget: "dxTextBox",
                    options: {
                        placeholder: "Filter by State",
                        onValueChanged: function (e2) {
                            var filterValue = e2.value;
                            e1.component.getDataSource().filter(["deliveryAddress.state", "contains", filterValue]);
                            e1.component.getDataSource().load();
                        }
                    }
                }
            );
        },
        onEditorPreparing: function (e) {
            if (e.dataField == "userId" && e.parentType === "filterRow") {
                console.log("onEditorPreparing", e);
                e.editorName = "dxTextBox";
                e.editorOptions = {
                    onChange: function (e2) {
                        console.log(e2);
                        console.log(e2.event.target.value);

                        if (e2.event.target.value === '') {
                            e.component.getDataSource().filter(null);
                            e.component.getDataSource().reload();
                            //e.component.refresh();
                            return;
                        }

                        $.ajax({
                            url: `${serverBaseUrl}/user/id?name=${e2.event.target.value}`,
                            success: function (id) {
                                //e.setValue(id);
                                e.component.getDataSource().filter(["userId", "=", id]);
                                e.component.refresh();
                            }
                        })
                    }
                };
            }
        },
        onExporting: async function (e) {
            const workbook = new ExcelJS.Workbook();
            const worksheet = workbook.addWorksheet('Orders');

            const exportGrid = (component, worksheet) => {
                return DevExpress.excelExporter.exportDataGrid({
                    //topLeftCell: { row: 2, column: 2},
                    component: component,
                    worksheet: worksheet,
                    customizeCell: async function (options) {
                        const { excelCell, gridCell } = options;

                        if (gridCell.rowType == 'data' || gridCell.rowType == 'header') {
                            excelCell.fill = {
                                type: 'pattern',
                                pattern: 'solid',
                                bgColor: { argb: 'FFFFCC' },  // Light yellow color
                                fgColor: { argb: "d2ebff" },
                            };
                            excelCell.style.border = {
                                top: { style: 'thin' },
                                left: { style: 'thin' },
                                bottom: { style: 'thin' },
                                right: { style: 'thin' }
                            };
                        }

                        if (gridCell.rowType === 'data' && gridCell.column.dataField === 'userId') {
                            const userId = gridCell.value;
                            await $.ajax({
                                url: `${serverBaseUrl}/user/${userId}`,
                                async: false,
                                success: function (result) {
                                    excelCell.value = result.name;
                                }
                            });
                        }
                    }
                });
            };
            const cellRange = await exportGrid(e.component, worksheet);
            
            const visibleRows = e.component._controllers.selection._dataController._items;

            let currentRowIndex = cellRange.to.row + 1;

            for (var row of visibleRows) {
                //currentRowIndex++;
                if (row.isExpanded) {
                    const detailDataSource = new DevExpress.data.DataSource({
                        store: new DevExpress.data.CustomStore({
                            load: function (loadOptions) {
                                const deferred = $.Deferred();

                                $.ajax({
                                    url: `${serverBaseUrl}/product/order/${row.data.id}`,
                                    success: function (result) {
                                        deferred.resolve(result);
                                    },
                                    error: function () {
                                        deferred.reject(`Error while loading products for order id: ${row.data.id}`);
                                    }
                                });

                                return deferred.promise();
                            }
                        })
                    });

                    let dataGrid2x = $(`#containerProduct${row.key}`).dxDataGrid("instance");
                    let detailData2 = dataGrid2x.getDataSource().items();

                    currentRowIndex = -1;
                    worksheet.eachRow(function (row2, rowNumber) {
                        const orderId = row2.getCell('A').value; // Assuming orderId is in column A
                        if (orderId === row.key) {
                            currentRowIndex = rowNumber;
                            return false; // Stop iteration
                        }
                    });

                    if (currentRowIndex !== -1) {
                        worksheet.spliceRows(currentRowIndex + 1, 0, [null, 'Product Id', 'Name', 'Price', 'Quantity', 'Amount', 'Category']);
                        
                        const prodHeading = worksheet.getRow(currentRowIndex + 1);

                        let producRowStartIndex = currentRowIndex + 1;

                        // Set the font of each cell in the row to bold
                        prodHeading.eachCell({ includeEmpty: false }, function (cell) {
                            cell.font = { bold: true };
                            cell.fill = {
                                type: 'pattern',
                                pattern: 'solid',
                                bgColor: { argb: 'FFFFCC' },  // Light yellow color
                                fgColor: { argb: "88cafc" },
                            };
                            cell.style.border = {
                                top: { style: 'thin' },
                                left: { style: 'thin' },
                                bottom: { style: 'thin' },
                                right: { style: 'thin' }
                            };
                        });
                        currentRowIndex++;

                        dataGrid2x.getDataSource().load();
                        detailData2 = dataGrid2x.getDataSource().items();
                        // Insert detail data
                        detailData2.forEach((detailRow) => {
                            worksheet.spliceRows(currentRowIndex + 1, 0, [
                                null,
                                detailRow.items[0].id,
                                detailRow.items[0].name,
                                detailRow.items[0].price,
                                detailRow.items[0].quantity,
                                detailRow.items[0].price * detailRow.items[0].quantity,
                                detailRow.items[0].category
                            ]);
                            currentRowIndex++;
                        });

                        let productRowEndIndex = currentRowIndex;

                        for (let i = producRowStartIndex; i <= productRowEndIndex; i++) {
                            const row = worksheet.getRow(i);
                            row.eachCell({ includeEmpty: false }, function (cell) {
                                cell.fill = {
                                    type: 'pattern',
                                    pattern: 'solid',
                                    bgColor: { argb: 'FFFFCC' },  // Light yellow color
                                    fgColor: { argb: "88cafc" },
                                };
                                cell.style.border = {
                                    top: { style: 'thin' },
                                    left: { style: 'thin' },
                                    bottom: { style: 'thin' },
                                    right: { style: 'thin' }
                                };
                            });
                        }
                    }
                }
            }

            worksheet.eachRow((row) => {
                if (row.getCell('A').value) {
                    row.outlineLevel = 0;
                }
                else {
                    row.outlineLevel = 1;
                }
            });

            worksheet.properties.otulineLevelRow = 1;
            worksheet.properties.otulineLevelCol = 1;
            
            const buffer = await workbook.xlsx.writeBuffer();

             saveAs(new Blob([buffer], { type: 'application/octet-stream' }), 'Orders.xlsx');

            e.cancel = true;
        },
        masterDetail: {
            enabled: true,
            template(container, options) {
                $("<div>")
                    .dxDataGrid({
                        elementAttr: {
                            id: `containerProduct${options.key}`
                        },
                        dataSource: new DevExpress.data.DataSource({
                            store: new DevExpress.data.CustomStore({
                                load: function (loadOptions) {
                                    const deferred = $.Deferred();

                                    $.ajax({
                                        url: `${serverBaseUrl}/product/order/${options.data.id}`,
                                        success: function (result) {
                                            deferred.resolve(result);
                                        },
                                        error: function () {
                                            deferred.reject(`Error while loading products for order id: ${options.data.id}`);
                                        }
                                    });

                                    return deferred.promise();
                                }
                            }),
                        }),
                        columns: [
                            {
                                dataField: "id",
                                caption: "Product Id",
                            },
                            {
                                dataField: "name",
                                caption: "Name",
                            },
                            {
                                dataField: "price",
                                caption: "Price",
                                format: 'currency',
                            },
                            {   
                                dataField: "quantity",
                                caption: "Quantity",
                            },
                            {
                                caption: "Amount",
                                calculateCellValue: function (rowData) {
                                    return rowData.price * rowData.quantity;
                                },
                                allowSorting: true,
                                format: 'currency',
                            },
                            {
                                dataField: "category",
                                caption: "Category",
                                groupIndex: 0,
                            },

                        ],
                        summary: {
                            groupItems: [
                                {
                                    column: 'id',
                                    summaryType: 'count',
                                    displayFormat: '{0} products',
                                    showInGroupFooter: true,
                                    alignByColumn: true,
                                },
                                {
                                    column: "Amount",
                                    summaryType: 'sum',
                                    valueFormat: 'currency',
                                    displayFormat: 'Total: {0}',
                                    showInGroupFooter: true,
                                    alignByColumn: true,
                                }
                            ],
                            totalItems: [
                                {
                                    column: "quantity",
                                    summaryType: "sum",
                                    displayFormat: 'Items: {0}',
                                },
                                {
                                    column: "Amount",
                                    summaryType: "sum",
                                    valueFormat: 'currency',
                                },
                            ]
                        }
                    })
                    .appendTo(container);
            }
        },

        columns: [
            {
                dataField: "id",
                caption: "Id",
                allowEditing: false,
                fixed: true,
                fixPosition: "left",
                headerFilter: {
                    groupInterval: 10,
                    allowSearch: true,
                }
            },
            {
                dataField: "userId",
                caption: "User",
                fixed: true,
                fixPosition: "left",
                allowEditing: false,
                lookup: {
                    dataSource: {
                        key: "id",
                        load: function (options) {
                            console.log(options);
                            return $.getJSON(`${serverBaseUrl}/user?all=true`);
                        },
                        byKey: function (options) {
                            console.log(options);
                            return $.getJSON(`${serverBaseUrl}/user?id=` + options);
                        }
                    },
                    valueExpr: "id",
                    displayExpr: "name"
                },
                headerFilter: {
                    dataSource: {
                        key: "id",
                        load: function (options) {
                            console.log(options);
                            return $.getJSON(`${serverBaseUrl}/user?all=true`)              ;
                        },
                        postProcess: function (results) {
                            return results.map(user => ({
                                value: user.id,
                                text: user.name,
                                id: user.id,
                            }));
                        },
                        byKey: function (options) {
                            console.log(options);
                            return $.getJSON(`${serverBaseUrl}/user?id=` + options);
                        }
                    },
                }
            },
            {
                caption: "Delivery Address",
                allowEditing: false,
                isBand: true,
                //hidingPriority: 0,
                headerCellTemplate: function (header, info) {
                    header.parent().css({
                        "text-align": "center",
                    });
                    $(header).append(`<span>${info.column.caption}</span>`);
                },
                columns: [
                    {
                        dataField: "deliveryAddress.plotNo",
                        caption: "Plot No",
                    },
                    {
                        dataField: "deliveryAddress.street",
                        caption: "Street",
                    },
                    {
                        dataField: "deliveryAddress.town",
                        caption: "Town",
                    },
                    {
                        dataField: "deliveryAddress.city",
                        caption: "City",
                    },
                    {
                        dataField: "deliveryAddress.state",
                        caption: "State",
                    },
                    {
                        dataField: "deliveryAddress.pincode",
                        caption: "Pincode",
                    },
                ],
            },
            {
                dataField: "isDelivered",
                caption: "Is Delivered",
                dataType: "boolean",
            },
            {
                dataField: "orderDate",
                caption: "Order Date",
                dataType: "date",
            }
        ],
        onRowUpdating: function (e) {
            //deepMerge(e.newData, e.oldData);
            const newData = {};
            $.extend(true, newData, e.oldData, e.newData);
            e.newData = newData;
        },
        editing: {
            allowAdding: true,
            allowUpdating: true,
            allowDeleting: true,
            mode: "batch"
        },
        scrolling: {
            mode: "infinite",
            preloadEnabled: true,
            rowRenderingMode: "virtual",
        },
        paging: {
            enabled: true,
            pageSize: 10,
            pageIndex: 0,
        },
        remoteOperations: {
            paging: true,
        },
        onContentReady: function (e) {
            contentReadyCallCount++;
        },
        headerFilter: {
            visible: true,
        },
        filterRow: {
            visible: true,
        }
    }

    const userDGW = createDGWrapper("USER DETAILS", userDgOptions, "userDgContainer");
    const productDGW = createDGWrapper("PRODUCT DETAILS", productDgOptions, "productDgContainer");
    const orderDGW = createDGWrapper("ORDER DETAILS", orderDgOptions, "orderDgContainer");
    const temp = $("<div>", { id: "temp"});

    main.append([
        userDGW,
        productDGW,
        orderDGW,
        temp
    ]);

    root.append([
        // header,
        main,
        // footer
    ]);

    attachWidgetsToWindow();
});



function createDGWrapper(heading, dgOptions, containerId) {

    const dataGridContainer = $('<div>', { id: containerId })
        .dxDataGrid(dgOptions);

    return $("<div>", { class: "ctm-dg-wrapper" })
        .append(`<h2 class='dx-datagrid-headers'>${heading}</h2>`)
        .append(dataGridContainer);
}

function createDs(resourceName) {

    const customStore = new DevExpress.data.CustomStore({
        // loadMode: "raw",
        key: "id",
        load: function (options) {
            let deferred = $.Deferred();
            // console.log(options);
            $.ajax({
                url: `${serverBaseUrl}/` + resourceName,
                method: "GET",
                data: options.skip != undefined ? { skip: options.skip, take: options.take } : undefined,
                success: function (result) {
                    //deferred.resolve([...result, ...result, ...result]);
                    deferred.resolve(result);
                },
                error: function () {
                    deferred.reject(`Data Loading for ${resourceName} Error`);
                }
            });

            return deferred.promise();
        },
        byKey: function (key) {
            let deferred = $.Deferred();
            // console.log(options);
            $.ajax({
                url: `${serverBaseUrl}/` + resourceName + "/" + key,
                method: "GET",
                success: function (result) {
                    //deferred.resolve([...result, ...result, ...result]);
                    deferred.resolve(result);
                },
                error: function () {
                    deferred.reject(`Data Loading for ${resourceName} Error`);
                }
            });

            return deferred.promise();
        },
        insert: function (itemData) {
            const deferred = $.Deferred();
            $.ajax({
                url: `${serverBaseUrl}/` + resourceName,
                method: "POST",
                contentType: "application/json",
                data: JSON.stringify(itemData),
                success: function (result) {
                    deferred.resolve(result);
                },
                error: function () {
                    deferred.reject(`Data Loading for ${resourceName} Error`);
                }
            });
            return deferred.promise();
        },
        update: function (key, values) {
            const deferred = $.Deferred();


            $.ajax({
                url: `${serverBaseUrl}/` + resourceName + "/" + key,
                method: "PUT",
                contentType: "application/json",
                data: JSON.stringify(values),
                success: function (result) {
                    deferred.resolve(result);
                },
                error: function () {
                    deferred.reject(`Data Loading for ${resourceName} Error`);
                }
            });
            return deferred.promise();
        },
        remove: function (key) {
            const deferred = $.Deferred();

             $.ajax({
                url: `${serverBaseUrl}/` + resourceName + "/" + key,
                method: "DELETE",
                success: function (result) {
                    deferred.resolve(result);
                },
                error: function () {
                    deferred.reject(`Error while deleting ${resourceName} ${key}`);
                }
             });

            return deferred;
        }
    });

    return new DevExpress.data.DataSource({
        store: customStore
    });
}


function attachWidgetsToWindow() {
    window.userDGW = $('#userDgContainer').dxDataGrid("instance");
    window.productDGW = $("#productDgContainer").dxDataGrid("instance");
    window.orderDGW = $("#orderDgContainer").dxDataGrid("instance");
}



/*
                    
// Create a new workbook and worksheet
const workbook = new ExcelJS.Workbook();
const worksheet = workbook.addWorksheet('Outline Example');

// Set column headers
worksheet.columns = [
  { header: 'Category', key: 'category' },
  { header: 'Value', key: 'value' }
];

// Add some data with outline levels
worksheet.addRow({ category: 'Category 1', value: 100, outlineLevel: 1 }); // Outline level 1
worksheet.addRow({ category: 'Subcategory 1.1', value: 50, outlineLevel: 2 }); // Outline level 2
worksheet.addRow({ category: 'Subcategory 1.2', value: 50, outlineLevel: 2 }); // Outline level 2
worksheet.addRow({ category: 'Category 2', value: 200, outlineLevel: 1 }); // Outline level 1
worksheet.addRow({ category: 'Subcategory 2.1', value: 100, outlineLevel: 2 }); // Outline level 2
worksheet.addRow({ category: 'Subcategory 2.2', value: 100, outlineLevel: 2 }); // Outline level 2

// Apply outline settings to the worksheet
worksheet.properties.outlineLevelCol = 1; // Show outline level on the column
worksheet.properties.outlineLevelRow = 1; // Show outline level on the row

// Set the view to show outlines
worksheet.views = [
  { state: 'outline', showOutlineSymbols: true }
];

// Save the workbook

saveAs(workbook.xlsx.writeBuffer(), { type: 'application/octet-stream' }), 'Orders.xlsx');
*/