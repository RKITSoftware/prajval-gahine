
var customStore;
var dataSource;
var dataGrid;


$(function () {
    function fakeAPIGetUnsorted(){
        console.log("fakeAPIGetUnsorted called");
        const deferred = $.Deferred();

        setTimeout(function(){
            deferred.resolve(generateEmployees(500));
        }, 200);

        return deferred.promise();
    }

    // simulate an API for virtual scrolling
    function fakeAPIGetData(skip = 0, take = 10, sortInfo) {

        console.log("fakeAPIGetData called");

        function compare(prop, isDesc) {
            return function (a, b) {
                if (!isDesc) {
                    return a[prop] > b[prop] ? 1 : -1;
                } else {
                    return a[prop] < b[prop] ? 1 : -1;
                }
            };
        }

        let deferred = $.Deferred();

        // console.log("API Called", skip, take, sortInfo);

        setTimeout(function () {
            let employees = generateEmployees(500);
            let subEmployee;

            if (!sortInfo) {
                subEmployee = $.extend(true, [], employees.slice(skip, skip + take));
            }
            else {
                let clonedEmployees = employees2 = $.extend(true, [], employees);
                clonedEmployees.sort(compare(sortInfo.selector, sortInfo.desc));
                subEmployee = $.extend(true, [], clonedEmployees.slice(skip, skip + take));
            }

            window.subEmployee = subEmployee;
            deferred.resolve({
                data: subEmployee,
                totalCount: subEmployee.length,
            });
            // console.log(subEmployee);
        }, 200);

        return deferred.promise();
    }

    const root = $("#root");

    const loader = $("#loader");

    const loaderW = loader.dxLoadPanel({
        message: "Loading...",
        position: { of: '#container' },
        visible: false,
    }).dxLoadPanel('instance');

    const container = $("<div>", { id: "container" });

    const customStore = new DevExpress.data.CustomStore({
        key: "id",
        load: function (loadOptions) {

            if (loadOptions.skip == 0) {
                loaderW.show(); // start the loader
            }

            let deferred;

            if(loadOptions.skip === undefined){
                deferred = fakeAPIGetUnsorted();
            }
            else{
                deferred = fakeAPIGetData(loadOptions.skip, loadOptions.take, loadOptions?.sort?.[0]);
            }

            deferred.always(function () {
                if (loadOptions.skip == 0) {
                    loaderW.hide();
                }
            }).catch(function () {
                DevExpress.ui.notify({
                    message: "Unable to laod data.",
                    type: "error",
                });
            });

            return deferred;
        }
    });
    window.customStore = customStore;

    const dataSource = new DevExpress.data.DataSource({
        store: customStore,
        //paginate: false,
        //pageSize: 10,
    });

    // --------------------------------------------------------------------------------------------------------
    // ----------------------------------------- DataGrid -----------------------------------------------------
    // --------------------------------------------------------------------------------------------------------

    const dataGrid = container.dxDataGrid({
        dataSource,
        cacheEnabled: true,
        scrolling: {
            mode: "infinite",
            preloadEnabled: false,
            rowRenderingMode: "virtual",
            showScrollbar: "always",
            useNative: false,
            scrollByThumb: true,
            scrollByContent: false,
        },
        height: "380px",
        loadPanel: {
            enabled: false
        },
        paging: {
            enabled: true,
            pageSize: 20,
            pageIndex: 0,
        },
        pager: {
            visible: true,
            showPageSizeSelector: true,
            allowedPageSizes: [10, 20, 50],
            showInfo: true
        },
        remoteOperations: {
            paging: true,
            sorting: true,
        },
        sorting: {
            mode: "single"
        },
    }).dxDataGrid('instance');

    // -------------------------------------------------------------------------------------------------------


    root.append(container);

    $("<div>").dxCheckBox({
        value: true,
        text: "Sorting on remote",
        onValueChanged: function (e) {
            dataGrid.option("remoteOperations.sorting", e.value);
        }
    }).appendTo("#root");

    // export to window
    {
        window.dataSource = dataSource
        window.dataGrid = dataGrid
    }
});