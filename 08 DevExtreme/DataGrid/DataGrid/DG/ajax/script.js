var customStore;
var dataSource;
var array;

$(function () {
    
    const root = $("#root");

    const container = $("<div>", { id: "container" });

    let cache = [];
    const customStore = new DevExpress.data.CustomStore({
        load: function () {
            if (cache.length == 0) {
                let p = fetch("https://jsonplaceholder.typicode.com/todos")
                    .then(response => response.json());
                p.then(data => {
                    cache = data;
                    array = data;
                });
                return p;
            }
            return cache;
        },
        //onUpdating: function (x, y) {
        //    console.log(x, y)
        //},
        update: function (e, v) {
            console.log(e, v);
            let index = cache.findIndex(it => it.id === e.id);
            let obj = cache[index];
            cache[index] = { ...obj, ...v };
        }
    });
    window.customStore = customStore;

    const dataSource = new DevExpress.data.DataSource({
        store: customStore
    });

    // --------------------------------------------------------------------------------------------------------
    // ----------------------------------------- DataGrid -----------------------------------------------------
    // --------------------------------------------------------------------------------------------------------

    const dataGrid = container.dxDataGrid({
        dataSource,
        keyExpr: "ID",  // need not specify if keyed in store
        cacheEnabled: false,
        columns: [
            {
                caption: "USER ID",
                dataField: "userId",
                allowGrouping: true,
                //groupIndex: 0,
            },
            {
                caption: "ID",
                dataField: "id",
                allowEditing: false,
                visibleIndex: 0,
            },
            {
                caption: "TITLE",
                dataField: "title",
            },
            {
                caption: "COMPLETED",
                dataField: "completed",
                allowEditing: true,
            },
        ],
        editing: {
            allowUpdating: true,
            mode: "form", // popup, form, batch
            form: {
                items: [
                    {
                        itemType: "group",
                        colCount: 3,
                        items: [
                            { dataField: "userId"},
                            { dataField: "title", colSpan: 2 },
                            { dataField: "completed" }
                        ]
                    }
                ]
            }
        },
        grouping: {
            //autoExpandAll: true,
        },
    }).dxDataGrid('instance');

    // -------------------------------------------------------------------------------------------------------

    root.append(container);

    // export to window
    {
        window.dataSource = dataSource
        window.dataGrid = dataGrid
    }
});