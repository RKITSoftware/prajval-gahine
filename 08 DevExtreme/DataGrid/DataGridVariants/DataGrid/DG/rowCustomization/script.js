
var array;
var arrayStore;
var dataSource;
var dataGrid;

$(function () {
    // build ui template
    const root = $("#root");
    const container = $("<div>", { id: "container" });

    // array - data
    const array = $.extend(true, [], generateEmployees(500).splice(0, 50));
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

    // dx data grid
    const dataGrid = container.dxDataGrid({
        dataSource,
        rowTemplate: function (container, options) {
            window.options = options;
            $(`<tr class="dx-row dx-data-row dx-row-lines" role="row" aria-selected="false" aria-rowindex="4">
                  <td class="dx-command-expand dx-datagrid-group-space" aria-selected="false" role="gridcell" aria-colindex="1" style="text-align: left;">&nbsp;</td>
                  <td class="dx-command-expand dx-datagrid-group-space" aria-selected="false" role="gridcell" aria-colindex="2" style="text-align: left;">&nbsp;</td>
                  <td class="dx-command-expand dx-datagrid-group-space" aria-selected="false" role="gridcell" aria-colindex="3" style="text-align: right;">&nbsp;</td>
                  <td aria-describedby="dx-col-1" aria-selected="false" role="gridcell" aria-colindex="4" style="text-align: right;" tabindex="0" class="dx-cell-focus-disabled">${options.data.id}</td>
                  <td aria-describedby="dx-col-2" aria-selected="false" role="gridcell" aria-colindex="5" style="text-align: left;">${options.data.firstName}</td>
                  <td aria-describedby="dx-col-3" aria-selected="false" role="gridcell" aria-colindex="6" style="text-align: left;">${options.data.lastName}</td>
                  <td aria-describedby="dx-col-4" aria-selected="false" role="gridcell" aria-colindex="7" style="text-align: left;">${options.data.birthDate}</td>
                  <td aria-describedby="dx-col-7" aria-selected="false" role="gridcell" aria-colindex="8" style="text-align: right;">${options.data.salary}</td>
            </tr>`)
                .appendTo(container);
        },
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
            grouping: false,
        }
    }).dxDataGrid("instance");
    window.dataGrid = dataGrid;

    root.append([
        container,
    ]);
});