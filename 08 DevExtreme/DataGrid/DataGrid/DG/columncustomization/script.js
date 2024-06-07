
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

    let nextIncId = 501;
    const gridWidget = gridContainer.dxDataGrid({
        dataSource,
        columns: [
            {
                dataField: "id",
                calculateFilterExpression: function (filterValue, selectedFilterOperation, target) {
                    if (selectedFilterOperation === ">=") {
                        return [this.dataField, ">=", filterValue];
                    }
                }
            },
            {
                caption: "Name",
                columns: [

                    {
                        dataField: "firstName", //ud

                        alignment: "right",
                        allowEditing: false, // t
                        allowExporting: false,  // t
                        allowFiltering: false,   // t
                        allowFixing: true,   // t
                        allowGrouping: false,    // t
                        allowHeaderFiltering: true,   // t
                        allowHiding: false,   // t
                        allowReordering: false,   // t
                        allowResizing: false,   // t
                        allowSearch: false,   // t  searchPanel: t,
                        allowSorting: false,   // t
                        autoExpandGroup: false,   // t

                        //groupIndex: 0,
                        calculateGroupValue: "lastName"
                    },
                    {
                        dataField: "lastName",
                        calculateCellValue: function (rowData) {  // can also be string - data field
                            // NOTE 378: calculateCellValue as function is v. much useful in lookups
                            return rowData.lastName.toUpperCase();
                        }
                    },
                ],
            },
            {
                caption: "Full Name",
                calculateCellValue: function (rowData) {
                    return rowData.firstName + " " + rowData.lastName;
                },
                setCellValue: function (newRowData, value, rowData) {
                    rowData.fullName = value; // Update the age when the custom input changes
                    $.extend(true, newRowData, rowData)
                },
                cellTemplate: function (container, options) {
                    container.append($("<span>", { class: "redbg font-white", text: options.value}));
                }
            },
            {
                dataField: "birthDate",

                //groupIndex: 0,
                calculateGroupValue: function (rowData) {
                    const date = new Date(rowData.birthDate);
                    const dayOfWeek = date.getDay();
                    if (dayOfWeek === 0 || dayOfWeek === 6) { // 0 is Sunday, 6 is Saturday
                        return "Weekend";
                    } else {
                        return "Weekday";
                    }
                }
            },
            {       
                width: 250,
                type: "buttons",
                buttons: ["edit", "delete", {
                    hint: "Clone",
                    icon: "copy",
                    visible: function (e) {
                        console.log("visible", e);
                        return !e.row.isEditing;
                    },
                    disabled: function (e) {
                        return e.row.data.position != 'Engineer';
                    },
                    onClick: function (e) {
                        const clonedRow = $.extend(true, {}, e.row.data, { id: nextIncId++ });

                        array.splice(e.row.rowIndex + 1, 0, clonedRow);
                        gridWidget.refresh(true);
                        e.event.preventDefault();
                    }
                }],
            }
        ],
        stateStoring: {
            enabled: true,
            type: "localStorage",
            storageKey: "myDataGridState"
        },
        allowColumnReordering: true,
        allowColumnResizing: true,
        searchPanel: {
            visible: true,
        },
        sortingMode: {
            mode: "singl,",
        },
        grouping: {
            contextMenuEnabled: true,
        },
        groupPanel: {
            visible: true,
        },
        columnFixing: {
            enabled: true,
        },
        editing: {
            allowUpdating: true,
            //allowDeleting: true,
        },
        filterRow: {
            visible: true,
        },
        headerFilter: {
            visible: true,
        }
    }).dxDataGrid("instance");
    window.gridWidget = gridWidget;

    root.append([
        gridContainer
    ]);
});