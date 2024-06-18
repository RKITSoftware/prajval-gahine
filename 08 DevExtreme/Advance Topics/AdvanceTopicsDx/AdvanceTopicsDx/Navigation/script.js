//var serverURL = "http://localhost:5114/api";
var serverURL = "http://localhost:5000/api";

$(function () {
    const root = $("#root");

    const main = $("<div>", { id: "main" });

    const menuCs = new DevExpress.data.CustomStore({
        load: function (loadOptions) {
            var deferred = $.Deferred();

            $.ajax({
                url: `${serverURL}/ui/menu`,
                method: "GET",
                success: function (menus) {
                    menus.map(menu => {

                        if (menu.id.length === 1) {
                            menu.isTopLevelMenu = true;
                        }

                        if (menu.id === "1") {
                            menu.icon = "user";
                        }
                        else if (menu.id === "2") {
                            menu.icon = "product";
                        }
                        else {
                            menu.icon = "cart";
                        }
                    });

                    menus[0].items = [...menus[0].items, ...menus[0].items, ...menus[0].items, ...menus[0].items, ...menus[0].items, ...menus[0].items, ...menus[0].items, ...menus[0].items, ...menus[0].items, ...menus[0].items, ...menus[0].items, ...menus[0].items, ...menus[0].items, ...menus[0].items, ...menus[0].items, ...menus[0].items, ...menus[0].items, ...menus[0].items, ...menus[0].items, ...menus[0].items, ...menus[0].items, ...menus[0].items, ...menus[0].items, ...menus[0].items, ...menus[0].items, ...menus[0].items, ...menus[0].items, ...menus[0].items, ...menus[0].items, ...menus[0].items, ...menus[0].items, ...menus[0].items, ...menus[0].items, ...menus[0].items, ...menus[0].items, ...menus[0].items, ...menus[0].items, ...menus[0].items,]
                    deferred.resolve([...menus, ...menus, ...menus, ...menus, ...menus, ...menus, ...menus]);
                }
            });

            return deferred.promise();
        }
    });

    const menuBarContainer = $("<div>", { id: "menuBar"})
        .dxMenu({
            dataSource: menuCs,
            displayExpr: "name",
            hideSubmenuOnMouseLeave: false,
            orientation: "vertical",
            showFirstSubmenuMode: {
                delay: {
                    show: 0,
                    hide: 500,
                },
                name: "onClick",
            },
            showSubmenuMode: {
                delay: {
                    show: 0,
                    hide: 500,
                },
                name: "onClick"
            },
            onItemClick: function (e) {
                console.log(e);
                if (e.itemData.id == "1") {

                    var popup = $("<div>", {id : "popymy"})
                        .dxPopup({
                            height: 300,
                            width: 'auto',
                            animation: {
                                hide: 0,
                                show: 0
                            },
                            onShowing: function (e) {
                                e.component.topToolbar().dxToolbarBase("instance").option("visible", false)
                                //e.component.bottomToolbar().dxToolbarBase("instance").option("visible", false)
                            },
                            title: false,
                            showCloseButton: false,
                            toolbarItems: null,
                            shading: false,
                            closeOnOutsideClick: true,
                            contentTemplate: function (container) {
                                console.log("container", container);
                                $("<div>")
                                    .dxList({
                                        items: e.itemData.items,
                                        displayExpr: "name",
                                        itemTemplate: function (data, container) {
                                            if (data.items) {
                                                return (
                                                `<div class="my-menu-item">
                                                    <div>${data.name}</div>
                                                    <i class="dx-icon dx-icon-spinnext"></i>
                                                </div>`);
                                            }
                                            return `<div>${data.name}</div>`;
                                        }
                                    })
                                    .appendTo(container);
                            },
                            position: {
                                at: 'right top',
                                my: 'left top',
                                of: e.itemElement,
                            },
                        })
                        .dxPopup("instance");

                        $("#main").append(popup.element());

                        
                    window.popup = popup;
                    popup.show();
                    e.cancel = true;

                }
            },
            onSubmenuShowing: function (e) {
                e.cancel = true;
            },
            onSubmenuShown: function (e) {
                console.log(arguments);
                //if(e.submenu._userOptions.)
            },
            onSubmenuHidden: function () {
                console.log(arguments);
            },
        });

    main.append([
        menuBarContainer
    ]);

    root.append([
        main
    ]);

    attachToWindow();
});


function attachToWindow() {
    window.menuBarWidget = $("#menuBar").dxMenu("instance");
}