var root;

var productGridContainer;
var productGridWidget;
var productArray;
var productArrayStore;
var productDataSource;

var orderGridContainer;
var orderGridWidget;
var orderArray;
var orderArrayStore;
var orderDataSource;


$(function () {
    const root = $("#root");
    window.root = root;

    const productGridContainer = $("<div>", { id: "productGridContainer" });
    window.productGridContainer = productGridContainer;

    const productArray = $.extend(true, [], products);
    window.productArray = productArray;

    const productArrayStore = new DevExpress.data.ArrayStore({
        data: productArray,
        key: "productID"
    });
    window.productArrayStore = productArrayStore;

    const productDataSource = new DevExpress.data.DataSource({
        store: productArrayStore,
    });
    window.productDataSource = productDataSource;


    // -----------------------------------------------------------

    const orderGridContainer = $("<div>", { id: "orderGridContainer" });
    window.orderGridContainer = orderGridContainer;

    const orderArray = $.extend(true, [], orders);
    window.orderArray = orderArray;

    const orderArrayStore = new DevExpress.data.ArrayStore({
        data: orderArray,
        key: "orderID",
    });
    window.orderArrayStore = orderArrayStore;

    const orderDataSource = new DevExpress.data.DataSource({
        store: orderArrayStore,
    });
    window.orderDataSource = orderDataSource;

    // ----------------------------------------------------------------

    const productGridWidget = productGridContainer.dxDataGrid({
        dataSource: productDataSource,
        showBorders: true,
        rowDragging: {
            group: "productOrder",
            allowReordering: true,
            autoScroll: true,
            onRemove: function (e) {
                const removeID = e.itemData.productID;
                productArrayStore.remove(removeID);
                productGridWidget.refresh();
            },
            onReorder: function (e) {
                productArray.splice(e.toIndex, 0, productArray.splice(e.fromIndex, 1)[0]);
                productGridWidget.refresh();
            }
        },
    }).dxDataGrid("instance");
    window.productGridWidget = productGridWidget;

    let isDragged = false;
    let nextOrderID = 1;
    const orderGridWidget = orderGridContainer.dxDataGrid({
        dataSource: orderDataSource,
        showBorders: true,
        columns: [
            {
                dataField: "orderID",
            },
            {
                dataField: "productID",
            },
            {
                dataField: "productName",
            },
            {
                dataField: "quantity",
                allowUpdating: true,
            }
        ],
        editing: {
            allowUpdating: true,
            mode: "cell",
        },
        rowDragging: {

            keyExpr: "orderID",
            allowReordering: true,
            autoScroll: true,
            onReorder: function (e) {
                orderArray.splice(e.toIndex, 0, orderArray.splice(e.fromIndex, 1)[0]);
                orderGridWidget.refresh();

            },
            onAdd: function (e) {
                isDragged = true;
                orderArrayStore.insert({
                    orderID: nextOrderID++,
                    productID: e.itemData.productID,
                    productName: e.itemData.name,
                    //quantity: 0,
                });
                orderGridWidget.refresh().done(function (e) {
                    const rowIndex = orderGridWidget.getVisibleRows().length - 1;
                    orderGridWidget.editCell(rowIndex, "quantity");
                });

            }
        },
        onRowInserting: function (e) {
            if (isDragged) {
            }
        }
    }).dxDataGrid("instance");
    window.orderGridWidget = orderGridWidget;

    // -----------------------------------------------------------

    root.append([
        productGridContainer,
        orderGridContainer,
    ]);
});