import { users } from "./Data.js";

$(() => {

    var storeArray = new DevExpress.data.ArrayStore({
        data: users,
        errorHandler: function (error) {
            console.log(error, error.message);
        },
        key: "id",
        onInserting: function (values) {
            console.log(values);
            //  alert(`${values} will be inserted`);
        },
        onInserted: function (values, key) {
            console.log(`${values} and ${key} inserted successfully`);
            //storeArray.load();
        },
    });

    function loadFilteredData(filterValue) {
        storeArray.load({
            filter: ["age", "contains", filterValue],
            sort: [{ selector: "name", desc: false }],
            skip: 1,
            skip: 0,
            take: 10    
        }).done(function (data) {
            console.log("Filtered Data:", data);
        }).fail(function (error) {
            console.log("Load Error:", error);
        });
    }


    const arrayStore = $("#arrayStore").dxSelectBox({
        dataSource: storeArray,
        displayExpr: "name",
        acceptCustomValue: true,
        valueExpr: "id",
        onCustomItemCreating: function (data) {
            //customItemHandler(data);
            data.customItem = null;
        },
        buttons: [{
            name: "add",
            location: "after",
            options: {
                icon: "add",
                onClick: function (e) {
                    var value = arrayStore.option("text");
                    var id = users[users.length - 1].id;
                    //storeArray.push([{ type: "insert", data: { id: ++id, name: value } }]);
                    storeArray.insert({ id: ++id, name: value });
                    arrayStore.option("dataSource", storeArray);
                    //console.log(arrayStore);
                    //arrayStore._refresh();
                    //storeArray.load()
                    //    .done(function (data) {
                    //        console.log("Data", data);
                    //    })
                    //    .fail(function (error) {
                    //        console.log("Error", error);
                    //    });
                }
            }
        }, {
            name: "filter",
            location: "after",
            options: {
                icon: "dragvertical",
                onClick: function (e) {
                    var filterValue = 22;
                    loadFilteredData(filterValue);
                    storeArray.totalCount().done(function (count) {
                        console.log(count);
                    }).fail(function (error) {
                        console.log("Total Count Error:", error);
                    });
                    setTimeout(() => {
                        storeArray.clear();
                        storeArray.totalCount().done(function (count) {
                            console.log(count);
                        }).fail(function (error) {
                            console.log("Total Count Error:", error);
                        });
                    }, 5000);
                }
            }
        }, 'dropDown'],
        onValueChanged: function (e) {
            storeArray.byKey(1)
                .done(function (dataItem) {
                    console.log(dataItem);
                })
                .fail(function (error) {
                    console.log(error);
                });
        }
    }).dxSelectBox("instance");

});