
var array;
var arrayStore;
var dataSource;
var dataGrid;

$(function () {
    // build ui template
    const root = $("#root");
    const container = $("<div>", { id: "container" });

    // array - data
    const array = generateEmployees(500);
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
            },
            {
                dataField: "lastName",
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
        ],
        editing: {
            mode: 'popup',
            allowUpdating: true,
            allowDeleting: true,
            allowAdding: true,
            confirmDelete: false,
            editColumnName: null,    // cell or batch edit, it contains col name
            popup: {
                title: "Popup - add or edit",
                showTitle: true,
                resizeEnabled: true,
                /*
                toolbarItems: [{
                    locateInMenu: 'always',
                    widget: 'dxButton',
                    toolbar: 'top',
                    options: {
                        text: 'More info',
                        onClick() {
                            const message = `More info about ${employee.FirstName} ${employee.LastName}`;

                            DevExpress.ui.notify({
                                message,
                                position: {
                                    my: 'center top',
                                    at: 'center top',
                                },
                            }, 'success', 3000);
                        },
                    },
                }, {
                    widget: 'dxButton',
                    toolbar: 'bottom',
                    location: 'before',
                    options: {
                        icon: 'email',
                        stylingMode: 'contained',
                        text: 'Send',
                        onClick() {
                            const message = `Email is sent to ${employee.FirstName} ${employee.LastName}`;
                            DevExpress.ui.notify({
                                message,
                                position: {
                                    my: 'center top',
                                    at: 'center top',
                                },
                            }, 'success', 3000);
                        },
                    },
                }, {
                    widget: 'dxButton',
                    toolbar: 'bottom',
                    location: 'after',
                    options: {
                        text: 'Close',
                        stylingMode: 'outlined',
                        type: 'normal',
                        onClick() {
                            popup.hide();
                        },
                    },
                    }],
                */
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
                        items: ["position", "salary"],
                    },
                ],
            }
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

    const radioGroup = $("<div>").dxRadioGroup({
        items: radioGroupArray,
        value: radioGroupArray[3],
        layout: "horizontal",
        onValueChanged: function (e) {
            dataGrid.option("editing.mode", e.value.text.toLowerCase());
        }
    });

    root.append([
        radioGroup,
        container,
    ]);
});