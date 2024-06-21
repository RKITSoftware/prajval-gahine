
//var server = "http://localhost:5000/api";
var server = "http://localhost:5114/api";

$(function () {
    const root = $("#root");
    const main = $("<div>", { id: "main" });

    const $treeView = $("<div>")
        .dxTreeView({
            elementAttr: {
                class: "bg-prime",
            },
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
            rootValue: "",
            expandNodesRecursive: false,
            createChildren: function (parent) {
                const parentId = parent ? parent.itemData.id : "F:\\prajval-gahine\\filedir";

                var deferred = $.Deferred();

                return $.ajax({
                    url: server + "/ui/folderitems",
                    type: "POST",
                    dataType: 'json',
                    data: {
                        parentId,
                    },
                    success: function () {
                        const parentNode = treeView._getNode(parentId);
                        if (parentNode) {
                            const parentElement = treeView._getNodeElement(parentNode);
                            const template = $(parentElement).find(".my-treeview-item-wrapper");

                            const button = $("<div>")
                                .dxButton({
                                    //height: 30,
                                    //width: 30,
                                    icon: "refresh",
                                    onClick: function (e) {
                                        //e.cancel = true;
                                        e.event.stopPropagation();
                                        const path = parentId;

                                        $.ajax({
                                            url: server + "/ui/folderitems",
                                            type: "POST",
                                            dataType: 'json',
                                            data: {
                                                parentId: path,
                                            },
                                            success: function (newNodes) {
                                                const treeView = $treeView.dxTreeView("instance");
                                                let items = treeView.option("items");

                                                // remove all existing node having parentId = path and append new nodes
                                                console.log(items);
                                                items = items.filter(item => item.parentId != path);
                                                items.push(...newNodes);
                                                console.log(items);

                                                treeView.option("items", items);
                                            }
                                        })
                                    }
                                });
                            template.append(button);
                        }
                    }
                })

                return deferred.promise();
            },
            onItemExpanded: function (e) {
                e.itemData.isRefreshButton = true;
            },
            onItemClick: function (e) {
                //e.event.stopPropogation();
            },
            itemTemplate: function (itemData, index, itemElement) {
                const icon = itemData.type == "file" ? "file" : "folder";
                const template = $("<div>", { class: "my-treeview-item-wrapper"})
                    .append($("<span>", { class: `dx-icon dx-icon-${icon} make-child-center` }));
                template.append($("<span>", { text: itemData.name }));

                if (itemData.type == "folder") {
                    
                }

                template.appendTo(itemElement);

                //console.log(arguments);
            },
            animationEnabled: false,
            valueExpr: "id",
            displayExpr: "name",
            //itemsExpr: "children",
            expandEvent: "click",
            width: 300,
            onInitialized: function (e) {
                window.treeView = e.component;
                //console.log(e);
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