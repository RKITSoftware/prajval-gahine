
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
            },
            {
                dataField: "salary",
                dataType: "number"
            },
            {
                dataField: "departmentId",
                lookup: {
                    dataSource: loopupArray,
                    valueExpr: "departmentId",
                    displayExpr: "deptName",
                    allowClearing: true,
                }
            },
        ],
        editing: {
            newRowPosition: "top", // Ensure new row is added at the top
            mode: 'popup',
            allowUpdating: true,
            allowDeleting: true,
            allowAdding: true,
            confirmDelete: true,
            editColumnName: null,    // cell or batch edit, it contains col name
            refreshMode: "reload",
            selectTextOnEditStart: true,
            startEditAction: "dblClick", // click
            useIcons: true,
            texts: {
                addRow: "CM - Add entry", // hint for add button (editing.allowAdding: true)
                cancelAllChanges: "CM - Undo all changes", // hint for discard button (editing.mode: batch)
                cancelRowChanges: "CM - Undo row changes", // hint for row discard button (editing.allowUpdating: true and editing. mode: row)
                confirmDeleteMessage: "CM - This will delete the record, do you want to proceed?",
                confirmDeleteTitle: "CM - Delete",
                deleteRow: "CM - Delete this row",   // hint for delete button on row (editing.allowDeleting: true)
                editRow: "CM - Edit this row", // hint for edit button on row (editing.allowUpdating: true)
                saveAllChanges: "CM - Save every changes", // hint for save changes button (editing.mode: batch)
                saveRowChanges: "CM - Save row changes", // hint for save button of row (editing.allowUpdating: ture and editing.mode: row)
                undeleteRow: "CM - undelete",   // hit for undelete button on row (allowDeleting: true and editing.mode: batch)
                validationCancelChanges: "CM - Validation cancel chenges" // hint for  button that cancels changes in a cell
            },
            popup: {
                title: "Popup - add or edit",
                showTitle: true,
                resizeEnabled: true,
                toolbarItems: [{
                    locateInMenu: 'always',
                    zwidget: 'dxButton',
                    toolbar: 'top',
                    options: {
                        text: 'More info',
                        onClick(e) {
                            const message = `More info about ${currentEditingRow.firstName} ${currentEditingRow.lastName}`;

                            DevExpress.ui.notify({
                                message,
                                position: {
                                    my: 'center top',
                                    at: 'center top',
                                },
                            }, 'success', 3000);
                        },
                    },
                },
                {
                    widget: "dxButton",
                    toolbar: "bottom",
                    location: "before",
                    options: {
                        text: "Save",
                        onClick: function (e) {
                            dataGrid.saveEditData();
                        }
                    }
                },
                {
                    widget: "dxButton",
                    toolbar: "bottom",
                    location: "after",
                    options: {
                        text: "Cancel",
                        onClick: function (e) {
                            dataGrid.cancelEditData();
                        }
                    }
                }],
            },
            form: {
                items: [
                    {
                        itemType: "group",
                        caption: "Personal Details",
                        colCount: 3,
                        items: [
                            {
                                dataField: "firstName",
                                colSpan: 2,
                            },
                            {
                                dataField: "lastName",
                                colSpan: 1,
                            },
                            {
                                dataField: "birthDate",
                                colSpan: 3,
                            },
                        ],
                    },
                    {
                        itemType: "group",
                        caption: "Job Details",
                        items: ["position", "salary", "departmentId"],
                    },
                ],
            }
        },
        onEditingStart: function (e) {
            currentEditingRow = e.data;
            console.log(e);
        },
        repaintChangesOnly: true,
    }).dxDataGrid("instance");
    window.dataGrid = dataGrid;

    const radioGroupArray = [
        { text: "Row" },
        { text: "Cell" },
        { text: "Batch" },
        { text: "Popup" },
        { text: "Form" },
    ];

    const radioGroupMode = $("<div>").dxRadioGroup({
        items: radioGroupArray,
        value: radioGroupArray[3],
        layout: "horizontal",
        onValueChanged: function (e) {
            dataGrid.option("editing.mode", e.value.text.toLowerCase());
        },
    });

    const radioGroupRMArray = [
        { text: "reload" },
        { text: "reshape" },
        { text: "repaint" },
    ];
    const radioGroupRefreshMode = $("<div>").dxRadioGroup({
        items: radioGroupRMArray,
        value: radioGroupRMArray[0],
        layout: "horizontal",
        onValueChanged: function (e) {
            dataGrid.option("editing.refreshMode", e.value.text.toLowerCase());
        },
    });

    const buttonEmptyUnderlyingArray = $("<div>").dxButton({
        text: "Clear underlying array",
        label: "Empty underlying array?",
        onClick: function () {
            while (window.array.length > 0) {
                window.array.pop();
            }
        }
    });


    root.append([
        radioGroupMode,
        radioGroupRefreshMode,
        buttonEmptyUnderlyingArray,
        container,
    ]);
});