
var array;
var arrayStore;
var dataSource;
var dataGrid;

$(function () {
    // build ui template
    const root = $("#root");
    const container = $("<div>", { id: "container" });

    // array - data
    const array = $.extend(true, [], generateEmployees(500));
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
        return (new Date(rowData.OrderDate)).getDay();
    }

    // dx data grid
    const dataGrid = container.dxDataGrid({
        dataSource,
        //filterBuilder: {
        //    enabled: true,
        //    applyFilter: "auto" // Apply filter automatically when a condition is added or changed
        //},
        filterValue: [['firstName', '=', 'Prajval'], 'and', ['birthDate', 'weekends']],
        filterBuilder: {
            enabled: true,
            customOperations: [{
                name: 'weekends',
                caption: 'Weekends',
                dataTypes: ['date'],
                icon: 'check',
                hasValue: false,
                calculateFilterExpression() {
                    return [[getOrderDay, '=', 0], 'or', [getOrderDay, '=', 6]];
                },
            }],
            allowHierarchicalFields: true,
        },
        columns: [
            {
                dataField: "id",
                allowEditing: false,
                allowFiltering: true,   // false
                allowHeaderFiltering: true,
                filterType: "exclude",
                filterOperations: ["="],
            },
            {
                dataField: "firstName",
                //sortOrder: "asc",
                allowFiltering: true,
                filterOperations: ["contains", "containsNot", "="], // Include custom operation
                calculateFilterExpression: function (filterValue, selectedFilterOperation) {
                    if (selectedFilterOperation === "containsNot") {
                        //return ["!", ["contains", "columnName", filterValue]];
                        return ["=", "firstName", filterValue];
                    }
                    return ["firstName", "=", filterValue];
                }
            },
            {
                dataField: "lastName",
                validationRules: [
                    {
                        type: "required",
                        message: "Last Name is required",
                    }
                ]
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
                    allowSearch: true,
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