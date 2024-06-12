
var array;
var arrayStore;
var dataSource;
var dataGrid;

$(function () {
    // build ui template
    const root = $("#root");
    const container = $("<div>", { id: "container" });

    // array - data
    const array = $.extend(true, [], arrayStatic);
    window.array = array;

    // array store
    const arrayStore = new DevExpress.data.ArrayStore({
        data: array,
        key: "id",
    });
    window.arrayStore = arrayStore;

    // data source
    const dataSource = new DevExpress.data.DataSource({
        store: arrayStore,
    });
    window.dataSource = dataSource;


    DevExpress.localization.loadMessages({
        "en": {
            "currency": {
                "style": "currency",
                "currency": "USD"
            }
        }
    });

    DevExpress.localization.locale("en");

    function getOrderDay(rowData) {
        return (new Date(rowData.birthDate)).getDay();
    }

    // dx data grid
    const dataGrid = container.dxDataGrid({
        dataSource,
        //filterValue: [['firstName', '=', 'Prajval']],
        filterPanel: {
            visible: true,
        },
        filterBuilder: {
            enabled: true,
            customOperations: [
                {
                    name: 'weekends',
                    caption: 'Weekends',
                    dataTypes: ['date'],
                    icon: 'check',
                    hasValue: false,
                    calculateFilterExpression(value, fields) {
                        return [[getOrderDay, '=', 0], 'or', [getOrderDay, '=', 6]];
                    },
                },
                {
                    name: 'containsNot',
                    caption: 'Custom Contains not',
                    icon: 'check',
                    calculateFilterExpression: function (filterValue, selectedFilterOperation) {
                        if (selectedFilterOperation === "containsNot") {
                            return ["!", ["firstName", "contains", , filterValue]];
                        }
                        return ['firstName', selectedFilterOperation, filterValue];
                    }
                }
            ],
            allowHierarchicalFields: true,
        },
        filterSyncEnabled: true,
        sorting: {
            mode: "multiple",
            showSortIndexes: false,
        },
        /*
        customizeColumns: function (columns) {
            columns.forEach(function (column) {
                if (column.dataField === 'firstName') {
                    column.filterOperations.push('containsNot'); // Add the custom operation to the column
                }
            });
            return columns;
        },
        */
        searchPanel: {
            visible: true,
            width: 240,
            placeholder: 'Search...',
        },
        columns: [
            {
                dataField: "id",
                allowEditing: false,
                allowFiltering: true,   // false
                allowHeaderFiltering: true,
                filterType: "exclude",
                filterOperations: ["="],
                sortOrder: "asc",
                sortIndex: 0,
                headerFilter: {
                    visible: true,
                    searchTimeout: 100,
                    groupInterval: 100,
                    texts: {},
                    height: "400px",
                    width: "300px",
                }
            },
            {
                dataField: "firstName",
                //sortOrder: "asc",
                allowFiltering: true,
                filterOperations: ["contains", "containsNot", "="],
                /*
                calculateFilterExpression: function (filterValue, selectedFilterOperation) {
                    if (selectedFilterOperation === "containsNot") {
                        return ["!", ["firstName", "contains", , filterValue]];
                    }
                    return ['firstName', selectedFilterOperation, filterValue];
                }
                */
            },
            {
                dataField: "lastName",
                validationRules: [
                    {
                        type: "required",
                        message: "Last Name is required",
                    }
                ],
                sortOrder: "asc",
                sortIndex: 1,
            },
            {
                dataField: "birthDate",
                dataType: "date",
            },
            {
                dataField: "position",
            },
            {
                dataField: "salary",
                dataType: "number",
                cellTemplate: function (element, data) {
                    element.append($("<span>", {
                        text: `${DevExpress.localization.formatNumber(data.value, {
                            type: "currency"
                        }) }`}));
                },
                headerFilter: {
                    visible: true,
                    allowSearch: false,
                    dataSource: [
                        {
                            text: "Less than $10000",
                            value: ["salary", "<", 10000]
                        },
                        {
                            text: "$10000 - $49999",
                            value: [["salary", ">=", 10000], ["salary", "<", 10000]]
                        },
                        {
                            text: "$50000 - $100000",
                            value: [["salary", ">=", 50000], ["salary", "<=", 100000]]
                        },
                        {
                            text: "Above than $100000",
                            value: ["salary", ">", 100000]
                        },
                    ],
                }
            },
        ],
        headerFilter: {
            visible: true,
            searchTimeout: 100,
            texts: {},
            height: "400px",
            width: "300px",
        },
        filterRow: {
            visible: true,
            applyFilter: "auto",    // onClick
        },
    }).dxDataGrid("instance");
    window.dataGrid = dataGrid;

    root.append([
        container,
    ]);
});