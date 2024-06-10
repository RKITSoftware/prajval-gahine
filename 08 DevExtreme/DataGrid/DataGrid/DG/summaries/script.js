var arrayStore;
var dataSource;
var array;

$(function () {
    
    const root = $("#root");

    const container = $("<div>", { id: "container" });

    let array = $.extend(true, [], arrayStatic);

    const arrayStore = new DevExpress.data.ArrayStore({
        data: array,
        key: "id",
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
        keyExpr: "id",  // need not specify if keyed in store
        cacheEnabled: false,
        columns: [
            {
                caption: "ID",
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
        grouping: {
            //autoExpandAll: true,
        },
        summary: {
            calculateCustomSummary: function (options) {

                function calculateAge(birthDate) {
                    const today = new Date();
                    birthDate = new Date(birthDate);

                    let year = today.getFullYear() - birthDate.getFullYear();

                    let month = today.getMonth() - birthDate.getMonth();

                    if (month < 0 || (month == 0 && today.getDate() - birthDate.getDate())) {
                        year--;
                    }
                    return year;
                }

                if (options.name === "avgAgeSummary") {
                    if (options.summaryProcess === "start") {
                        options.totalValue = 0;
                        options.count = 0;
                    }
                    else if (options.summaryProcess === "calculate") {
                        if (options.component.isRowSelected(options.value.id)) {
                            options.count++;
                            options.totalValue += calculateAge(options.value.birthDate);
                        }
                    }
                    else if (options.summaryProcess === "finalize") {

                        options.totalValue /= options.count;
                        options.totalValue = Math.round((options.totalValue + Number.EPSILON) * 100) / 100
                    }
                    // console.log(options);
                }
            },
            groupItems: [
                { column: 'salary', summaryType: 'max', showInGroupFooter: false, showInColumn: 'salary', alignByColumn: true, },
            ],
            totalItems: [
                // min are column and summaryType
                { column: 'salary', summaryType: 'sum', displayFormat: 'Total: {0}' },
                { column: 'salary', summaryType: 'avg', displayFormat: 'Average: {0}' },
                { showInColumn: 'birthDate', summaryType: 'custom', name: 'avgAgeSummary', displayFormat: 'Average Age: {0}' }
            ],
            recalculateWhileEditing: true,
        },
        grouping: {
            enabled: true,
        },
        groupPanel: {
            visible: true,
        },
        onSelectionChanged: function (selectedItems) {
            // Recalculate summaries when row selection changes
            dataGrid.refresh();
        },
        selection: {
            allowSelectAll: true,
            mode: "multiple",
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