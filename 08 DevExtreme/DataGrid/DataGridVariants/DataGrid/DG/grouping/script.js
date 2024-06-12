
var array;
var arrayStore;
var dataSource;
var dataGrid;

var customStore2;
var dataSource2;
var dataGrid2;

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


    const loopupArray = [
        {
            departmentId: 1,
            deptName: "HR"
        },
        {
            departmentId: 2,
            deptName: "Marketing"
        },
        {
            departmentId: 3,
            deptName: "Development"
        },
    ];

    let currentEditingRow;
    // dx data grid
    const dataGrid = container.dxDataGrid({
        dataSource,
        columns: [
            {
                dataField: "id",
                allowEditing: false,
            },
            {
                dataField: "firstName",
                sortOrder: "asc"
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
                groupIndex: 1,
            },
            {
                dataField: "salary",
                dataType: "number",
                groupIndex: 2,
                calculateGroupValue: function (rowData) {
                    if (rowData.salary < 50000) {
                        return "basic ";
                    }
                    else if (rowData.salary < 100000) {
                        return "moderate";
                    }
                    return "high";
                },
                sortingMethod: function (value1, value2) {
                    const order = ['basic', 'moderate', 'high'];
                    return order.indexOf(value1) - order.indexOf(value2);
                },
                groupCellTemplate: function (element, options) {
                    element.text(options.value);
                },
            },
            {
                dataField: "salaryWithValue",
                dataType: "number",
                calculateDisplayValue: function (rowData) {
                    return rowData.salary;
                },
            },
            {
                dataField: "departmentId",
                allowGrouping: true,
                groupIndex: 0,
                lookup: {
                    dataSource: loopupArray,
                    valueExpr: "departmentId",
                    displayExpr: "deptName",
                    allowClearing: true,
                }
            },
        ],
        grouping: {
            autoExpandAll: false,
            //allowCollapsing: false,
            //contextMenuEnabled: true,
            //columns: ["departmentId", "position"],
            expandMode: "rowClick", // buttonClick
            texts: {
                groupByThisColumn: "CM: GBTC",
                groupContinuedMessage: "CM: GCM",
                groupContinuesMessage: "CM: GCsM",
                ungroup: "CM: ungroup",
                ungroupAll: "CM: ungroup all"
            }
        },
        groupPanel: {
            allowColumnDragging: true,
            emptyPanelText: "Drag Column here to group",
            visible: true,
        },
        remoteOperations: {
            grouping: true,
        }
    }).dxDataGrid("instance");
    window.dataGrid = dataGrid;



    const groupArrayResponse = {
        "data": [
            {
                "key": "Sales",
                "items": [
                    {
                        "key": "basic",
                        "items": [
                            {
                                "id": 1,
                                "name": "John Doe",
                                "department": "Sales",
                                "salary": 45000
                            }
                        ],
                        "count": 1,
                        "summary": [45000]
                    },
                    {
                        "key": "moderate",
                        "items": [
                            {
                                "id": 2,
                                "name": "Jane Smith",
                                "department": "Sales",
                                "salary": 75000
                            }
                        ],
                        "count": 1,
                        "summary": [75000]
                    }
                ],
                "count": 2,
                "summary": [60000]
            },
            {
                "key": "Engineering",
                "items": [
                    {
                        "key": "high",
                        "items": [
                            {
                                "id": 3,
                                "name": "Alice Johnson",
                                "department": "Engineering",
                                "salary": 120000
                            },
                            {
                                "id": 4,
                                "name": "Bob Brown",
                                "department": "Engineering",
                                "salary": 85000
                            }
                        ],
                        "count": 2,
                        "summary": [102500]
                    }
                ],
                "count": 2,
                "summary": [102500]
            }
        ],
        "totalCount": 4,
        "summary": [85000],
        "groupCount": 2
    };


    const customStore2 = new DevExpress.data.CustomStore({
        load: function (laodOptions) {
            const deferred = $.Deferred();
            deferred.resolve(groupArrayResponse);
            return deferred.promise();
        }
    });
    window.customStore2 = customStore2;

    const dataSource2 = new DevExpress.data.DataSource({
        store: customStore2,
    });
    window.dataSource2 = dataSource2;

    const dataGrid2Container = $("<div>", { class: "custom-dark-mode"}).dxDataGrid({
        dataSource: dataSource2,
        columns: [
            { dataField: 'name', caption: 'Name' },
            { dataField: 'department', caption: 'Department', groupIndex: 0 },
            {
                dataField: 'salary',
                caption: 'Salary',
                calculateGroupValue: function (rowData) {
                    if (rowData.salary < 50000) {
                        return "basic";
                    } else if (rowData.salary < 100000) {
                        return "moderate";
                    }
                    return "high";
                },
                groupIndex: 1
            },
            {
                dataField: 'salary',
                caption: 'Salary',
                calculateGroupValue: function (rowData) {
                    return rowData.salary;
                },
            }
        ],
        summary: {
            groupItems: [{
                column: "salary",
                summaryType: "avg",
                displayFormat: "Average: {0}"
            }],
            totalItems: [{
                column: "salary",
                summaryType: "avg",
                displayFormat: "Average: {0}"
            }]
        },
        remoteOperations: {
            grouping: true,
        }
    });
    const dataGrid2 = dataGrid2Container.dxDataGrid("instance");
    window.dataGrid2 = dataGrid2;

    root.append([
        container,
        dataGrid2Container
    ]);
});