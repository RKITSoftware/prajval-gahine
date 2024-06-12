
window.jsPDF = window.jspdf.jsPDF;
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
    // -----------------------------------------------------------

    const orderGridContainer = $("<div>", { id: "orderGridContainer" });
    window.orderGridContainer = orderGridContainer;

    const orderArray = $.extend(true, [], orders);
    window.orderArray = orderArray;

    const orderArrayStore = new DevExpress.data.ArrayStore({
        data: orderArray,
        key: "id",
    });
    window.orderArrayStore = orderArrayStore;

    const orderDataSource = new DevExpress.data.DataSource({
        store: orderArrayStore,
    });
    window.orderDataSource = orderDataSource;

    // ----------------------------------------------------------------

    const productArray = $.extend(true, [], products);
    window.productArray = productArray;

    const productArrayStore = new DevExpress.data.ArrayStore({
        data: productArray,
        key: "id",
    });
    window.productArrayStore = productArrayStore;

    const productDataSource = new DevExpress.data.DataSource({
        store: productArrayStore,
    });
    window.productDataSource = productDataSource;

    // ----------------------------------------------------------------
    let prdListCount = 0;
    const orderGridWidget = orderGridContainer.dxDataGrid({
        dataSource: orderDataSource,
        showBorders: true,
        width: 940,
        masterDetail: {
            enabled: true,
            autoExpandAll: true,
            template: function (container, options) {
                const rowData = options.data;

                $("<div>")
                    .addClass("master-detail-caption")
                    .text(`Order Id: ${options.key} product list`)
                    .appendTo(container);

                const productGrid = $("<div>")
                    .dxDataGrid({
                        columnAutoWidth: true,
                        showBorders: true,
                        keyExpr: "id",
                        editing: {
                            allowAdding: true,
                            allowUpdating: true,
                            mode: "cell",
                        },
                        columns: [
                            { dataField: "id", caption: "Product Id" },
                            { dataField: "name", caption: "Product Name" },
                            { dataField: "rate", caption: "Product Rate" },
                            { dataField: "quantity", caption: "Product Quantity", allowEditing: true,  },
                            { dataField: "amount", caption: "Amount" },
                        ],
                        dataSource2: rowData.products.map(function (product) {
                            const productInfo = products.find(productInfo => productInfo.id === product.productID);
                            return {
                                id: product.productID,
                                name: productInfo.name,
                                rate: productInfo.rate,
                                quantity: product.quantity,
                                amount: productInfo.rate * product.quantity,
                            };
                        }),
                        dataSource: products,
                        summary: {
                            totalItems: [
                                { column: "amount", summaryType: "sum", displayFormat: "Total: {0}" },
                            ]
                        }
                    });

                const productWidget = productGrid.dxDataGrid("instance");

                window["productGrid" + prdListCount] = productWidget;
                prdListCount++;

                productGrid
                    .appendTo(container);
            },
        },
        showBorders: true,
        columns: [
            {
                dataField: "id",
                caption: "Order Id",
            },
            {
                dataField: "userID",
                caption: "User Id",
            }
        ],
        export: {
            enabled: true,
        },
        onExporting(e) {
            const doc = new jsPDF();

            DevExpress.pdfExporter.exportDataGrid({
                jsPDFDocument: doc,
                component: e.component,
                indent: 5,
            }).then(() => {
                doc.save('orders.pdf');
            });
        },
    }).dxDataGrid("instance");
    window.orderGridWidget = orderGridWidget;

    // -----------------------------------------------------------

    root.append([
        orderGridContainer,
    ]);
});