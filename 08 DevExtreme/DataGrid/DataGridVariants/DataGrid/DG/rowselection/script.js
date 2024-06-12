
var root;
var gridContainer;
var gridWidget;
var array;
var arrayStore;
var dataSource;


$(function () {
    const root = $("#root");
    window.root = root;

    const gridContainer = $("<div>", { id: "gridContainer" });
    window.gridContainer = gridContainer;

    const array = $.extend(true, [], arrayStatic);
    window.array = array;

    const arrayStore = new DevExpress.data.ArrayStore({
        data: array,
        key: "id"
    });
    window.arrayStore = arrayStore;

    const dataSource = new DevExpress.data.DataSource({
        store: arrayStore,
    });
    window.dataSource = dataSource; 

    const gridWidget = gridContainer.dxDataGrid({
        dataSource,
        columns: [
            {
                dataField: "id",
            },
            {
                dataField: "firstName",
            },
            {
                dataField: "lastName",
            },
            {
                dataField: "birthDate",
            },
            {
                dataField: "position",
            },
            {
                dataField: "salary",
            },
        ],
        selection: {
            allowSelectAll: true,
            mode: "multiple",
            selectAllMode: "allPages",
            deferred: false,
        },
    }).dxDataGrid("instance");
    window.gridWidget = gridWidget;
    // gridWidget.selectAll();
    // gridWidget.deselectAll();

    root.append([
        gridContainer
    ]);
});