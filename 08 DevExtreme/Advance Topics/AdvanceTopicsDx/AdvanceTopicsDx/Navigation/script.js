﻿var serverURL = "http://localhost:5114/api";
//var serverURL = "http://localhost:5000/api";

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
                            //menu.isDisabled = true;
                        }
                        else if (menu.id === "2") {
                            menu.icon = "product";
                        }
                        else {
                            menu.icon = "cart";
                            //menu.isDisabled = true;
                        }
                    });

                    // illustrating users many sub menu
                    menus[0].items = [...menus[0].items, ...menus[0].items, ...menus[0].items, ...menus[0].items, ...menus[0].items, ...menus[0].items, ...menus[0].items, ...menus[0].items, ...menus[0].items, ...menus[0].items, ...menus[0].items, ...menus[0].items, ...menus[0].items, ...menus[0].items, ...menus[0].items, ...menus[0].items, ...menus[0].items, ...menus[0].items, ...menus[0].items, ...menus[0].items, ...menus[0].items, ...menus[0].items, ...menus[0].items, ...menus[0].items, ...menus[0].items, ...menus[0].items, ...menus[0].items, ...menus[0].items, ...menus[0].items, ...menus[0].items, ...menus[0].items, ...menus[0].items, ...menus[0].items, ...menus[0].items, ...menus[0].items, ...menus[0].items, ...menus[0].items, ...menus[0].items,]
                    // illustrating many menus
                    deferred.resolve([...menus, ...menus, ...menus, ...menus, ...menus, ...menus, ...menus]);

                    //deferred.resolve(menus);
                }
            });

            return deferred.promise();
        }
    });

    const localStore = new DevExpress.data.LocalStore({
        data1: [{
            productId: 1,
            productName: "Hello",
            price: 100.2,
            category: "World"
        }],
        immediate: true,
        key: "productId",
        name: "productList"
    });
    const $toast = $('<div>').dxToast({ displayTime: 10000 });
    const toast = $toast.dxToast('instance');

    const $globalLoader = $("<div>").dxLoadPanel({
        shadingColor: 'rgba(0,0,0,0.4)',
        visible: false,
        showIndicator: true,
        showPane: true,
        shading: true,
    });
    const globalLoader = $globalLoader.dxLoadPanel("instance");




    let popup;
    let productForm;
    const $productFormPopup = $("<div>")
        .dxPopup({
            shading: false,
            deferRendering: false,
            onInitialized: function (e) {
                popup = e.component;
            },
            contentTemplate: function (container, that) {
                const $productForm = $("<div>")
                    .dxForm({
                        onInitialized: function (e) {
                            productForm = e.component;
                        },
                        onContentReady: async function (e) {
                            const productIdEditor = this.getEditor("productId");
                                const formData = await localStore.load();
                                let nextProductId;
                                if (formData == undefined) {
                                    nextProductId = 1;
                                }
                                else {
                                    if (Array.isArray(formData) && formData.length > 0) {
                                        nextProductId = Math.max(...formData.map(product => product.productId)) + 1;
                                    }
                                    else {
                                        nextProductId = 1;
                                    }
                                }
                            productIdEditor.option("value", nextProductId);
                        },
                        items: [
                            {
                                dataField: "productId",
                                editorOptions: {
                                    disabled: true,
                                }
                                //template: async function (data, itemElement) {
                                //    const formData = await localStore.load();
                                //    let nextProductId;
                                //    if (formData == undefined) {
                                //        nextProductId = 1;
                                //    }
                                //    else {
                                //        if (Array.isArray(formData) && formData.length > 0) {
                                //            nextProductId = Math.max(...formData.map(product => product.productId)) + 1;
                                //        }
                                //        else {
                                //            nextProductId = 1;
                                //        }
                                //    }

                                //    $('<div>')
                                //        .dxTextBox({
                                //            value: nextProductId,
                                //            disabled: true,
                                //        })
                                //        .appendTo(itemElement);

                                //    this.updateData("productId", nextProductId);
                                //}
                            },
                            {
                                dataField: "productName",
                            },
                            {
                                dataField: "price",
                                editorType: "dxNumberBox"   
                            },
                            {
                                dataField: "category",
                                editorType: "dxSelectBox",
                                editorOptions: {
                                    items: ["Household", "Electronics", "Clothing", "Toys", "Groceries"],
                                    searchEnabled: true,
                                    value: '',
                                }
                            },
                            {
                                editorType: 'dxButton',
                                editorOptions: {
                                    text: "Save",
                                    onClick: function (e) {
                                        console.log(e);
                                        const productForm = $productForm.dxForm("instance");
                                        const formData = productForm.option("formData");

                                        globalLoader.option("message", "Saving Product");
                                        globalLoader.show();

                                        localStore.insert(formData)
                                            .then(function () {
                                                setTimeout(function () {
                                                    globalLoader.hide();

                                                    toast.option({ message: `Product ${formData.productName} added successfully`, type: 'success' });
                                                    toast.show();
                                                }, 1000);
                                            });
                                        popup.hide();
                                    }
                                }
                            }
                        ],
                    });

                container.append($productForm);
            }
        });
    root.append($productFormPopup);






    const menuBarContainer = $("<div>", { id: "menuBar"})
        .dxMenu({
            cssClass: "greenbg",
            dataSource: menuCs,
            displayExpr: "name",
            hideSubmenuOnMouseLeave: false,
            hoverStateEnabled: false,
            orientation: "vertical",
            disabled: false,
            disabledExpr: "isDisabled",
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
            onContentReady: function (e) {
                if (e.component.option("orientation") === "horizontal") {
                    e.component.option("width", "100%");
                }
                else {
                    e.component.option("width", "120px");
                }
            },
            onItemClick: function (e) {
                console.log(e);
                if (e.itemData.items == null && e.itemData.id === "2_1") {

                    popup.show();
                    productForm.resetValues();
                    productForm.repaint();
                    return;
                }

                if (e.itemData.id == "1") {
                    $(e.itemElement).css({
                        "background-color": "#fff",
                    });
                    // set menu item border
                    $(e.itemElement).addClass("my-menu-item-border");

                    let popup = $("#popupmy").dxPopup("instance");

                    if (popup && popup._menuItemElement?.[0] == e.itemElement?.[0]) {
                        if (!popup.option("visible")) {
                            popup.show();
                        }
                        e.cancel = true;
                        return;
                    }
                    if (popup) {
                        let contentTemplate = popup.option("contentTemplate");
                        popup._menuItemElement = e.itemElement;
                        popup._menuItemData = e.itemData;
                        popup.option("contentTemplate", (c) => contentTemplate(c, popup));
                        //let popupOptions = popup.option();
                        //popup.dispose();
                        //popup = $("#popupmy").dxPopup(popupOptions).dxPopup("instance");

                        popup.show();
                    }
                    e.cancel = true;
                }
            },
            onSubmenuShowing: function (e) {
                e.cancel = true;
            },
            onSubmenuShown: function (e) {
                console.log(arguments);
            },
            onSubmenuHidden: function () {
                console.log(arguments);
            },
        });

    var $popup = $("<div>", { id: "popupmy" })
        .dxPopup({
            visible: false,
            height: 300,
            width: 150,
            animation: {
                hide: 0,
                show: 0
            },
            onShowing: function (e2) {
                //e2.component.repaint();
                //e2.component.bottomToolbar().dxToolbarBase("instance").option("visible", false)
            },
            onHiding: function (e2) {
                this.beginUpdate();
                console.log("onHiding", arguments);
                if (event && $(event.toElement).closest(this._menuItemElement).length) {
                    e2.cancel = true;
                }
                else {
                    $(e2.component._menuItemElement).removeClass("my-menu-item-border");
                    $(this._menuItemElement).css({
                        "background-color": ""
                    });
                }
            },
            contentTemplate: function (container, that) {
                console.log(that);
                console.log("container", container);
                $("<div>").dxList({
                    activeStateEnabled: false,
                    elementAttr: {
                        id: "popupMyList"
                    },
                    //showScrollbar: false,
                    useNativeScrolling: false,
                    items: that._menuItemData.items,
                    displayExpr: "name",
                    itemTemplate: function (data, container) {
                        if (data) {
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
            onShown: function (e2) {
            },
            onContentReady: function (e2) {

                //this.beginUpdate();

                const content = e2.component.content();
                const items = $(content).find(".dx-item,.dx-list-item");

                e2.component.topToolbar().dxToolbarBase("instance").option("visible", false);

                const topY = this._menuItemElement[0].getBoundingClientRect().top;
                const bottomY = this._menuItemElement[0].getBoundingClientRect().bottom;

                const vh = Math.max(document.documentElement.clientHeight || 0, window.innerHeight || 0);
                const remainingDownVh = vh - topY;
                const remainingUpVh = vh - (vh - bottomY);

                let height, position, isPlaceAtTop;
                let isMenuHorizontal = menuBarContainer.dxMenu("instance").option("orientation") == "horizontal";

                if (isMenuHorizontal) {
                    position = {
                        at: "left bottom",
                        my: "left top",
                        of: this._menuItemElement,
                    };
                    isPlaceAtTop = true;
                }
                else {
                    if (300 <= remainingDownVh) {
                        position = {
                            at: 'right top',
                            my: 'left top',
                            of: this._menuItemElement,
                        };
                        isPlaceAtTop = true;
                    }
                    else if (300 <= remainingUpVh) {
                        position = {
                            at: 'right bottom',
                            my: 'left bottom',
                            of: this._menuItemElement,
                        };
                        isPlaceAtTop = false;
                    }
                    else {
                        height = (remainingDownVh > remainingUpVh ? remainingDownVh : remainingUpVh) - 20;
                        let dir = remainingDownVh > remainingUpVh ? 'top' : 'bottom';
                        isPlaceAtTop = remainingDownVh > remainingUpVh;
                        position = {
                            at: 'right ' + dir,
                            my: 'left ' + dir,
                            of: this._menuItemElement,
                        };
                    }

                    content.css({
                        position: "relative",
                    });
                    const delemeterCss = {
                        position: "absolute",
                        left: "0px",
                        width: "2px",
                        height: items[0].clientHeight + "px",
                        "background-color": "#ddd",
                    };
                    isPlaceAtTop ? delemeterCss.top = "0px" : delemeterCss.bottom = "0px";
                    content.prepend($("<div>", {
                        id: "popupDelemeter",
                        css: delemeterCss
                    }));
                }

                e2.component.option("position", position);

                this.endUpdate();
            },
            title: false,
            showCloseButton: false,
            toolbarItems: null,
            shading: false,
            closeOnOutsideClick: true,
        });

    window.popup = $popup.dxPopup("instance");
    //window.

    main.append([
        menuBarContainer,
        $popup,
        $toast,
        $globalLoader
    ]);

    root.append([
        main
    ]);

    attachToWindow();
});


function attachToWindow() {
    window.menuBarWidget = $("#menuBar").dxMenu("instance");
}