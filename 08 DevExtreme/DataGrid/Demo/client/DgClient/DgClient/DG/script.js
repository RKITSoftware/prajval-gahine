
let serverBaseUrl = 'http://localhost:5000/api';
//let serverBaseUrl = 'http://localhost:5114/api';

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
        export: {
            enabled: true,
        },
        
        onExporting: async function (e) {
            const workbook = new ExcelJS.Workbook();
            const worksheet = workbook.addWorksheet('Orders');

            const exportGrid = (component, worksheet) => {
                return DevExpress.excelExporter.exportDataGrid({
                    topLeftCell: { row: 2, column: 2},
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
                                url: `${serverBaseUrl}/user?id=${userId}`,
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

                    //let detailData2 = await detailDataSource.load().promise();
                    let detailData2 = $(`containerProduct${row.key}`).dx.getDataSoruce().items();

                    //.then((detailData) => {
                    //    // Insert detail headers
                    //    worksheet.spliceRows(currentRowIndex, 0, [null, 'Product Id', 'Name', 'Price', 'Quantity', 'Amount', 'Category']);
                    //    currentRowIndex++;

                    //    // Insert detail data
                    //    detailData.forEach((detailRow) => {
                    //        worksheet.spliceRows(currentRowIndex, 0, [
                    //            null,
                    //            detailRow.id,
                    //            detailRow.name,
                    //            detailRow.price,
                    //            detailRow.quantity,
                    //            detailRow.price * detailRow.quantity,
                    //            detailRow.category
                    //        ]);
                    //        currentRowIndex++;
                    //    });
                    //})
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

                        // Insert detail data
                        detailData2.forEach((detailRow) => {
                            worksheet.spliceRows(currentRowIndex + 1, 0, [
                                null,
                                detailRow.id,
                                detailRow.name,
                                detailRow.price,
                                detailRow.quantity,
                                detailRow.price * detailRow.quantity,
                                detailRow.category
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

                   
                    //detailPromises.push(

                    //);
                }
            }

            //await visibleRows.forEach(async (row) => {
                
            //});
            
            const buffer = await workbook.xlsx.writeBuffer();
            //.then((buffer) => {
            //    saveAs(new Blob([buffer], { type: 'application/octet-stream' }), 'Orders.xlsx');
            //});
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
                fixPosition: "left"
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
        }
    }

    const userDGW = createDGWrapper("USER DETAILS", userDgOptions, "userDgContainer");
    const productDGW = createDGWrapper("PRODUCT DETAILS", productDgOptions, "productDgContainer");
    const orderDGW = createDGWrapper("ORDER DETAILS", orderDgOptions, "orderDgContainer");

    main.append([
        userDGW,
        productDGW,
        orderDGW,
    ]);

    root.append([
        // header,
        main,
        // footer
    ]);

    attachWidgetsToWindow();
});


function deepMerge(target, source) {
    for (let key in source) {
        if (source.hasOwnProperty(key)) {
            if (typeof source[key] === 'object' && source[key] !== null && !Array.isArray(source[key])) {
                if (!target[key]) {
                    target[key] = {};
                }
                deepMerge(target[key], source[key]);
            } else if (!target.hasOwnProperty(key)) {
                target[key] = source[key];
            }
        }
    }
}


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