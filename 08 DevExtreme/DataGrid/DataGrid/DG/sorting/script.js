
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
                allowSorting: false,    // t
            },
            {
                dataField: "firstName",
                //calculateSortValue: "lastName",
                sortingMethod: function (value1, value2) {

                    if (!value1 && value2) return -1;
                    if (!value1 && !value2) return 0;
                    if (value1 && !value2) return 1;

                    return value1.localeCompare(value2);
                },
                sortOrder: "asc", // ↑
                sortIndex: 0,
            },
            {
                dataField: "lastName",
                sortIndex: 1,
                sortOrder: "desc", // ↑
            },
            {
                dataField: "birthDate",
            },
            {
                dataField: "position",
            },
            {
                dataField: "salary",
                calculateSortValue: function (rowData) {
                    return rowData.salary;
                }
            },
        ],
        sorting: {
            ascendingText: "CM: Ascending order",
            descendingText: "CM: Descending order",
            clearText: "CM: Clear",
            showSortIndex: true,
            mode: "none",
        },
        selection: {
            allowSelectAll: true,
            mode: "multiple",
            selectAllMode: "allPages",
            deferred: false,
        },
    }).dxDataGrid("instance");
    window.gridWidget = gridWidget;
    // gridWidget.clearSorting();

    root.append([
        gridContainer
    ]);
});