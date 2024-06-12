var arrayStore;
var dataSource;
var array;

var gridContainer;
var gridWidget;


$(function () {
    
    const root = $("#root");

    const container = $("<div>", { id: "container" });
    window.gridContainer = container;

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
        showBorders: true,
        //columnAutoWidth: false,
        columnHidingEnabled: true,
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
                allowEditing: false,
                hidingPriority: 0
            },
            {
                caption: "Salary",
                dataField: "salary",
                allowEditing: true,
                hidingPriority: 1,
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
        onAdaptiveDetailRowPreparing: function (e) {
            console.log(e);
            for (let item of e.formOptions.items) {
                if (item.dataField == "birthDate") {
                    item.isRequired = true;
                }
            }
        },
    }).dxDataGrid('instance');
    window.gridWidget = dataGrid;


    // -------------------------------------------------------------------------------------------------------

    root.append(container);

    // export to window
    {
        window.dataSource = dataSource
        window.dataGrid = dataGrid
    }
});