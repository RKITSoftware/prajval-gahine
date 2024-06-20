
//var server = "http://localhost:5000/api";
var server = "http://localhost:5114/api";

$(function () {
    const root = $("#root");
    const main = $("<div>", { id: "main" });

    const $treeView = $("<div>")
        .dxTreeView({
            //dataSource1: new DevExpress.data.DataSource({
            //    store: new DevExpress.data.CustomStore({
            //        load: function () {
            //            var deferred = $.Deferred();

            //            $.ajax({
            //                url: server + "/ui/filehierarchy",
            //                success: function (data) {
            //                    deferred.resolve(JSON.parse(data));
            //                }
            //            });

            //            return deferred.promise();
            //        }
            //    })
            //}),
            //items1: hierarchyData2,
            dataStructure: "plain",
            rootValue: "0",
            createChildren: function (parent) {
                const parentId = parent ? parent.id : "F:\\prajval-gahine\\root";

                var deferred = $.Deferred();

                return $.ajax({
                    url: server + "/ui/folderitems",
                    data: {
                        parentId,
                    },
                    //success: function (data) {
                    //    deferred.resolve(JSON.parse(data));
                    //}
                })

                //return deferred.promise();
            },
            animationEnabled: false,
            valueExpr: "id",
            displayExpr: "name",
            itemsExpr: "children",
            expandEvent: "click",
            width: 300,
            onInitialized: function (e) {
                window.treeView = e.component;
                console.log(e);
            }
        });



    main.append([
        $treeView
    ]);
    root.append([
        main
    ]);

    attachToWindow();
});


function attachToWindow() {
}