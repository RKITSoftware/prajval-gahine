
var customStore;
var dataSource;
var dataGrid;


$(function () {


    function fakeAPIGetUnsorted(){
        var unsortedEmployees = 
        [
            {
                "ID": 3,
                "FirstName": "Emma",
                "LastName": "Michael",
                "Position": "Sales",
                "BirthDate": "1975-02-15",
                "Salary": 136472
            },
            {
                "ID": 5,
                "FirstName": "David",
                "LastName": "Michael",
                "Position": "Sales",
                "BirthDate": "1990-12-01",
                "Salary": 136037
            },
            {
                "ID": 2,
                "FirstName": "PRAJVAL GAHINE",
                "LastName": "Linda",
                "Position": "Finance",
                "BirthDate": "1960-11-01",
                "Salary": 79795
            },
            {
                "ID": 4,
                "FirstName": "Samuel",
                "LastName": "Michael",
                "Position": "Finance",
                "BirthDate": "1988-04-18",
                "Salary": 83138
            },
            {
                "ID": 1,
                "FirstName": "Oliver loremloremloremloremloremloremloremloremloremloremloremloremloremloremloremloremloremloremloremloremloremlorem",
                "LastName": "Sophia",
                "Position": "Developer",
                "BirthDate": "1960-11-01",
                "Salary": 135624
            },
        ]

        const deferred = $.Deferred();

        setTimeout(function(){
            deferred.resolve(unsortedEmployees);
        }, 200);

        return deferred.promise();
    }


    // simulate an API for virtual scrolling
    function virtualScollAPI(skip = 0, take = 10, sortInfo) {

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
        key: "ID",
        load: function (loadOptions) {

            if (loadOptions.skip == 0) {
                loaderW.show(); // start the loader
            }

            let deferred;

            if(loadOptions.skip === undefined){
                deferred = fakeAPIGetUnsorted();
            }
            else{
                deferred = virtualScollAPI(loadOptions.skip, loadOptions.take, loadOptions?.sort?.[0]);
            }

            deferred.then(function () {
                if (loadOptions.skip == 0) {
                    loaderW.hide();
                }
            }).catch(function () {
                if (loadOptions.skip == 0) {
                    loaderW.hide();
                }
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
            preloadEnabled: true,
            rowRenderingMode: "virtual",
            showScrollbar: "always",
            useNative: false,
            scrollByThumb: true,
            scrollByContent: false,
        },
        height: "280px",
        loadPanel: {
            enabled: false
        },
        paging: {
            enabled: true,
            pageSize: 20,
            pageIndex: 0,
        },
        pager: {
            showPageSizeSelector: true,
            allowedPageSizes: [10, 20, 50],
            showInfo: true
        },
        remoteOperations: {
            paging: true,
            sorting: false,
        },
        sorting: {
            mode: "single"
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