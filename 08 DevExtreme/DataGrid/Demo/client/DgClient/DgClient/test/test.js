export: {
    enabled: true,
        allowExportSelectedData: true,
        },
onExporting: function (e) {
    var workbook = new ExcelJS.Workbook();
    var worksheet = workbook.addWorksheet('Bills');

    let masterRows = [];

    DevExpress.excelExporter.exportDataGrid({
        component: e.component,
        worksheet: worksheet,
        topLeftCell: { row: 2, column: 2 },
        customizeCell: function ({ gridCell, excelCell }) {
            if (gridCell.column.index === 1 && gridCell.rowType === 'data') {
                masterRows.push({ rowIndex: excelCell.fullAddress.row + 1, data: gridCell.data });
            }
        }
    }).then((cellRange) => {
        const borderStyle = { style: "thin", color: { argb: "FF7E7E7E" } };
        let offset = 0;

        const insertRow = (index, offset, outlineLevel) => {
            const currentIndex = index + offset;
            const row = worksheet.insertRow(currentIndex, [], 'n');

            for (var j = worksheet.rowCount + 1; j > currentIndex; j--) {
                worksheet.getRow(j).outlineLevel = worksheet.getRow(j - 1).outlineLevel;
            }

            row.outlineLevel = outlineLevel;

            return row;
        }

        for (var i = 0; i < masterRows.length; i++) {
            let row = insertRow(masterRows[i].rowIndex + i, offset++, 2);
            let columnIndex = cellRange.from.column + 1;
            row.height = 40;

            let billsData = billArray.find((item) => item.id === masterRows[i].data.id);

            Object.assign(row.getCell(columnIndex), {
                value: getCaption(billsData),
                fill: { type: 'pattern', pattern: 'solid', fgColor: { argb: 'BEDFE6' } }
            });
            worksheet.mergeCells(row.number, columnIndex, row.number, 3);

            const columns = ["name", "quantity", "price"];

            row = insertRow(masterRows[i].rowIndex + i, offset++, 2);

            columns.forEach((columnName, currentColumnIndex) => {
                Object.assign(row.getCell(columnIndex + currentColumnIndex), {
                    value: columnName,
                    fill: { type: 'pattern', pattern: 'solid', fgColor: { argb: 'BEDFE6' } },
                    font: { bold: true },
                    border: { bottom: borderStyle, left: borderStyle, right: borderStyle, top: borderStyle }
                });
            });

            billsData.products = billsData.products.map(val => val.id);

            getProducts(billsData.products).forEach((product, index) => {
                row = insertRow(masterRows[i].rowIndex + i, offset++, 2);

                columns.forEach((columnName, currentColumnIndex) => {
                    Object.assign(row.getCell(columnIndex + currentColumnIndex), {
                        value: product[columnName],
                        fill: { type: 'pattern', pattern: 'solid', fgColor: { argb: 'BEDFE6' } },
                        border: { bottom: borderStyle, left: borderStyle, right: borderStyle, top: borderStyle }
                    });
                });
            });
            offset--;
        }
    }).then(function () {
        // https://github.com/exceljs/exceljs#writing-xlsx
        workbook.xlsx.writeBuffer().then(function (buffer) {
            saveAs(new Blob([buffer], { type: 'application/octet-stream' }), 'Bills.xlsx');
        });
    });
    e.cancel = true;
},