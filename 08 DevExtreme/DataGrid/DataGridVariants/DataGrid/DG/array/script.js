var arrayStore;
var dataSource;
var array;

$(function () {
    
    const root = $("#root");

    const container = $("<div>", { id: "container" });

    array = $.extend(true, [], arrayStatic);

    const arrayStore = new DevExpress.data.ArrayStore({
        data: employees,
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
                caption: "ID",
                dataField: "ID",
                allowEditing: false,
                visibleIndex: 0,
            },
            {
                caption: "First Name",
                dataField: "FirstName",
                allowGrouping: true,
            },
            {
                caption: "Last Name",
                dataField: "LastName",
            },
            {
                caption: "Position",
                dataField: "Position",
                allowEditing: true,
            },
            {
                caption: "Birth Date",
                dataField: "BirthDate",
                allowEditing: true,
            },
            {
                caption: "Salary",
                dataField: "Salary",
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
    }).dxDataGrid('instance');

    // -------------------------------------------------------------------------------------------------------

    root.append(container);

    // export to window
    {
        window.dataSource = dataSource
        window.dataGrid = dataGrid
    }
});