var arrayStore;
var dataSource;
var array;

$(function () {
    
    const root = $("#root");

    const container = $("<div>", { id: "container" });

    var array = $.extend(true, [], arrayStatic);
    window.array = array;

    const arrayStore = new DevExpress.data.ArrayStore({
        data: array,
    });
    window.arrayStore = arrayStore;

    const dataSource = new DevExpress.data.DataSource({
        store: arrayStore
    });

    // --------------------------------------------------------------------------------------------------------
    // ----------------------------------------- DataGrid -----------------------------------------------------
    // --------------------------------------------------------------------------------------------------------

    const dataGrid = container.dxDataGrid({
        dataSource,
        keyExpr: "ID",  // need not specify if keyed in store
        cacheEnabled: false,
        columns: [
            {
                caption: "Id",
                dataField: "id",
                allowEditing: false,
                visibleIndex: 0,
            },
            {
                caption: "First Name",
                dataField: "firstName",
                allowGrouping: true,
            },
            {
                caption: "Last Name",
                dataField: "lastName",
            },
            {
                caption: "Position",
                dataField: "position",
                allowEditing: true,
            },
            {
                caption: "Birth Date",
                dataField: "birthDate",
                allowEditing: true,
            },
            {
                caption: "Salary",
                dataField: "salary",
                allowEditing: true,
            },
        ],
        editing: {
            allowUpdating: true,
            mode: "cell", // popup, form, batch, cell
            form: {
                items: [
                    {
                        itemType: "group",
                        colCount: 3,
                        items: [
                            { dataField: "userId"},
                            { dataField: "title", colSpan: 2 },
                            { dataField: "completed" }
                        ]
                    }
                ]
            }
        },
        rowAlternationEnabled: true,
        selection: {
            enabled: true,
            mode: "multiple",
        },
        export: {
            allowExportSelectedData: true,
            enabled: true,
        },
        onExporting: function (e) {

            var workbook = new ExcelJS.Workbook();
            window.workbook = workbook;
            var worksheet = workbook.addWorksheet('Main sheet');
            window.worksheet = worksheet;

            // customizing woorksheet
            worksheet.getColumn(1).fill = {
                type: "pattern",
                pattern: "solid",
                fgColor: { argb: "FFFF0000"}
            }

            worksheet.getColumn(1).eachCell({
                includeEmpty: true,
                function(cell) {
                    cell.font = {
                        color: { argb: "FF0000FF"}
                    };
                }
            });

            worksheet.getColumn(1).width = 20;

            // Sorting is not directly supported by ExcelJS, but you can sort the data before exporting to Excel.

            worksheet.autoFilter = "A1:B1";

            worksheet.getCell("A1").alignment = {
                horizontal: "center",
                vertical: "middle",
            };

            worksheet.getCell("A1").border = {
                top: {
                    style: "thin",
                    color: {
                        argb: "FF000000",
                    }
                },
                right: {
                    style: "thin",
                    color: {
                        argb: "FF000000",
                    }
                },
                bottom: {
                    style: "thin",
                    color: {
                        argb: "FF000000",
                    }
                },
                left: {
                    style: "thin",
                    color: {
                        argb: "FF000000",
                    }
                }
            };

            console.log(worksheet);

            DevExpress.excelExporter.exportDataGrid({
                worksheet: worksheet,
                component: e.component,
                customizeCell: function (options) {
                    var excelCell = options;
                    excelCell.font = { name: 'Arial', size: 12 };
                    excelCell.alignment = { horizontal: 'left' };
                }
            }).then(function () {
                workbook.xlsx.writeBuffer()
                    .then(function (buffer) {
                        console.log(buffer);
                        let data = new Blob([buffer], { type: "application/octet-stream" });

                        let url = window.URL.createObjectURL(data);
                        let a = document.createElement('a');
                        a.href = url;
                        a.download = 'employees.xlsx';
                        a.click();

                        window.URL.revokeObjectURL(url);
                    })
            });

            e.cancel = true;
        }
        
    }).dxDataGrid('instance');

    // -------------------------------------------------------------------------------------------------------

    root.append(container);

    // export to window
    {
        window.dataSource = dataSource
        window.dataGrid = dataGrid
    }
});