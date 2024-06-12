
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

    function calculateAge(dob) {
        const birthDate = new Date(dob);
        const today = new Date();

        let age = today.getFullYear() - birthDate.getFullYear();
        const monthDiff = today.getMonth() - birthDate.getMonth();

        // Check if the birth month has not occurred yet this year, or it's the birth month and the day has not occurred yet.
        if (monthDiff < 0 || (monthDiff === 0 && today.getDate() < birthDate.getDate())) {
            age--;
        }

        return age;
    }

    let nextIncId = 501;
    const gridWidget = gridContainer.dxDataGrid({
        dataSource,
        showBorders: true,
        height: "400px",
        //toolbar: {
        //    items: [,],
        //},
        rowDragging: {
            allowReordering: true,
            autoScroll: true,
            onReorder: function (e) {
                array.splice(e.toIndex, 0, array.splice(e.fromIndex, 1)[0]);
                console.log("row dragged and dropped", e);
                e.component.refresh();
            },
            onRemove: function (e) {
                 console.log("row removed ", e);
            },
            container: "#draggable-row",
            cursorOffset: {
                x: 100,
                y: 100
            },
            data: {
                customInfo: "Hello"
            },
            scrollSensitivity: 10,
            scrollSpeed: 100,
            showDragIcons: true,
            //boundary: "#gridContainer"
        },
        onToolbarPreparing: function (e) {
            let toolbars = e.toolbarOptions.items;
            toolbars.push(
                {
                    location: 'after',
                    widget: 'dxButton',
                    options: {
                        icon: 'refresh',
                        onClick() {
                            gridWidget.refresh();
                        },
                    },
                },
                {
                    location: 'after',
                    widget: 'dxButton',
                    options: {
                        icon: 'clear',
                        onClick() {
                            var columns = gridWidget.getVisibleColumns();
                            columns.forEach(function (column) {
                                if (column.sortOrder) {
                                    gridWidget.columnOption(column.index, "sortOrder", null);
                                }
                            });
                            gridWidget.clearFilter();
                            gridWidget.pageSize(100);
                            gridWidget.pageIndex(0);
                        },
                    },
                }
            );
        },
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
                dataField: "address",
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
                caption: "Age",
                calculateCellValue: function (rowData) {
                    return calculateAge(rowData.birthDate);
                },
            },
            {
                dataField: "salary",
                customizeText(cellInfo) {
                    return "$" + cellInfo.value;
                },
                //calculateCellValue: function (rowData) {
                //    return "$" + rowData.salary;
                //}
            },
            {       
                width: 250,
                type: "buttons",
                buttons: ["edit", "delete", {
                    hint: "Clone",
                    icon: "copy",
                    visible: function (e) {
                        // console.log("visible", e);
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
        sorting: {
            mode: "multiple",
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
        },
        onCellPrepared: function (e) {
            if (e.rowType == "data" && e.column.caption == "Age" && e.value < 50) {
                // console.log(e);
                e.cellElement.addClass("redbg font-white");
            }
        },

        paging: {
            pageSize: 100
        }
    }).dxDataGrid("instance");
    window.gridWidget = gridWidget;

    root.append([
        gridContainer
    ]);
});