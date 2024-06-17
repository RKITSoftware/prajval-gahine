

// order data
const orders = [
    { orderId: 1, customerName: "Prajval Gahine", orderDate: "2024-06-15", totalAmount: 150.25 },
    { orderId: 2, customerName: "Pritesh Gahine", orderDate: "2024-05-16", totalAmount: 650.75 },
    { orderId: 3, customerName: "Gaurav Wagh", orderDate: "2024-05-18", totalAmount: 1766.00 },
    { orderId: 4, customerName: "Aman Lathiya", orderDate: "2024-05-20", totalAmount: 25000.00 },
];

const products = [
    {
        orderId: 1, products: [
            { productId: 101, productName: "Laptop", quantity: 1, price: 1200.00 },
            { productId: 102, productName: "Mouse", quantity: 2, price: 25.50 },
        ],
    },
    {
        orderId: 2, products: [
            { productId: 103, productName: "Washing Machine", quantity: 1, price: 987.11 },
            { productId: 104, productName: "Air Conditioner", quantity: 1, price: 2500.00 },
            { productId: 105, productName: "Fan", quantity: 3, price: 700.55 },
            { productId: 105, productName: "Fan", quantity: 3, price: 700.55 },
            { productId: 105, productName: "Fan", quantity: 3, price: 700.55 },
            { productId: 105, productName: "Fan", quantity: 3, price: 700.55 },
        ],
    },
    {
        orderId: 3, products: [
            { productId: 103, productName: "Washing Machine", quantity: 1, price: 987.11 },
            { productId: 104, productName: "Air Conditioner", quantity: 1, price: 2500.00 },
            { productId: 105, productName: "Fan", quantity: 3, price: 700.55 },
        ],
    },
    {
        orderId: 4, products: [
            { productId: 103, productName: "Washing Machine", quantity: 1, price: 987.11 },
            { productId: 104, productName: "Air Conditioner", quantity: 1, price: 2500.00 },
            { productId: 105, productName: "Fan", quantity: 3, price: 700.55 },
        ],
    }
];

// create a workbook and a worksheet
const workbook = new ExcelJS.Workbook();
const worksheet = workbook.addWorksheet("orders");

(async function () {

    /*

    // add order headers
    worksheet.addRow(Object.keys(orders[0]).map(key => camelCaseToTitleCase(key)));
    //worksheet.getRow(0).eachCell((cell, colNumber) => {
    //    cell.font = {
    //        bold: true,
    //        //color: {
    //        //    argb: 'FFFFFF'
    //        //},
    //    };
    //    //cell.fill = {
    //    //    type: 'solid',
    //    //    fgColor: {
    //    //        argb: '000000'
    //    //    }
    //    //};
    //    cell.alignment = {
    //        vertical: 'middle',
    //        horizontal: 'center'
    //    };
    //});
    worksheet.getRow(1).eachCell((cell, colNo) => {
        cell.font = {
            bold: true
        };
    });

    worksheet.getRow(1).outlineLevel = 1;


    window.orderIdsWithProductInfo = products.map((product, index) => ({ index, orderId: product.orderId  }));
    window.detailFutureRowInfo = [];

    // add orders row
    orders.forEach(order => {
        const { orderId, customerName, orderDate, totalAmount } = order;
        const row = worksheet.addRow([orderId, customerName, orderDate, totalAmount]);
        row.outlineLevel = 1;
        var orderProductInfo = orderIdsWithProductInfo.find(o => o.orderId === orderId);
        if (orderProductInfo) {
            detailFutureRowInfo.push({ rowNumber: row.number, productIndex: orderProductInfo.index });
        }
    });


    var offset = 0;

    // adding products as detail of master order row
    detailFutureRowInfo.forEach(productInfo => {
        var product = products[productInfo.productIndex].products;
        var rowIndexToInsert = productInfo.rowNumber + 1 + offset;
        var tempIndex = rowIndexToInsert;

        
        var rowH = worksheet.insertRow(tempIndex, [null, ...Object.keys(product[0]).map(key => camelCaseToTitleCase(key))]);
        rowH.outlineLevel = 2;
        rowH.eachCell((cell, colNo) => {
            cell.font = {
                bold: true
            };
        });
        tempIndex++;
        offset++;
        product.forEach((product2, i2) => {
            const { productId, productName, quantity, price } = product2;
            const row = worksheet.insertRow(tempIndex, [null, productId, productName, quantity, price]);
            row.outlineLevel = 2;
            offset++;
            tempIndex++;
        });
    });

    worksheet.properties.outlineLevelCol = 2;
    worksheet.properties.outlineLevelRow = 2;

    autoFitColumns(worksheet);

    */



    var row1 = worksheet.addRow(["a1", 'b1', 'c1']);
    row1.outlineLevel = 101;
    row1.style["abc"] = "1";
    row1._cells[3] = row1._cells[0];
    row1.row1 = true;
    row1.mycell = row1._cells;

    var row2 = worksheet.addRow(["h2", 'i2', 'j2']);
    row2.outlineLevel = 102;
    row2.style["hij"] = "2";
    row2._cells[3] = row2._cells[0];
    row2.row2 = true;
    row2.mycell = row2._cells;

    var row3 = worksheet.addRow(["k3", 'l3', 'm3']);
    row3.outlineLevel = 103;
    row3.style["klm"] = "3";
    row3._cells[3] = row3._cells[0];
    row3.row3 = true;
    row3.mycell = row3._cells;

    var row4 = worksheet.addRow(["n4", 'o4', 'p4']);
    row4.outlineLevel = 103;
    row4.style["klm"] = "3";
    row4._cells[3] = row4._cells[0];
    row4.row4 = true;
    row4.mycell = row4._cells;

    var row5 = worksheet.insertRow(2, ['e', 'f', 'g']);
    //row2.outlineLevel = 102;
    //row2.style["2"] = "row2";
    //row2._cells[3] = row2._cells[0];

    worksheet.properties.outlineLevelCol = 1;
    worksheet.properties.outlineLevelRow = 1;
    window.buffer = await workbook.xlsx.writeBuffer();
    window.blob = new Blob([buffer], { type: "application/octet-stream" });
    saveAs(blob, "example.xlsx");
})();
function camelCaseToTitleCase(camelCase) {
    return camelCase
        .replace(/([A-Z])/g, ' $1') // Insert space before capital letters
        .replace(/^./, str => str.toUpperCase()); // Capitalize the first letter
};

function calculateCellWidth (value) {
    // approx char pixel width = 7, padding = 2
    return value.length + 2;
}

function autoFitColumns(worksheet) {
    worksheet.columns.forEach(column => {
        var maxWidth = 0;
        column.eachCell({ includeEmpty: true }, cell => {
            if (cell.value) {
                var columnWidth = calculateCellWidth(cell.value.toString());
                if (columnWidth > maxWidth) {
                    maxWidth = columnWidth;
                }
            }
        });
        column.width = maxWidth > 10 ? maxWidth : 10;
    });
}