
//let serverBaseUrl = 'http://localhost:5000/api';
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

        masterDetail: {
            enabled: true,
            template(container, options) {
                $("<div>")
                    .dxDataGrid({
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