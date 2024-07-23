var serverURL = "http://localhost:5114/api";
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

                    deferred.resolve(menus);
                }
            });

            return deferred.promise();
        }
    });

    const localStore = new DevExpress.data.LocalStore({
        data1: [],
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




    let addProductPopup;
    let addProductForm;
    
    const $addProductPopup = $("<div>")
        .dxPopup({
            shading: false,
            onShowing: function () {
                addProductForm.resetValues();
                addProductForm.repaint();
            },
            deferRendering: false,
            onInitialized: function (e) {
                addProductPopup = e.component;
            },
            contentTemplate: function (container, that) {
                const $productForm = $("<div>")
                    .dxForm({
                        onInitialized: function (e) {
                            addProductForm = e.component;
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
                                                    addProductPopup.hide();

                                                    toast.option({ message: `Product ${formData.productName} added successfully`, type: 'success' });
                                                    toast.show();
                                                }, 1000);
                                            });
                                    }
                                }
                            }
                        ],
                    });

                container.append($productForm);
            }
        });
    root.append($addProductPopup);

    // --------------------------------------------------------------
    let displayProductsPopup;
    let productDataGrid;

    const $displayProductsPopup = $("<div>")
        .dxPopup({
            onShowing: function () {
                productDataGrid.refresh();
            },
            shading: false,
            deferRendering: false,
            onInitialized: function (e) {
                displayProductsPopup = e.component;
            },
            contentTemplate: function (container, that) {
                const $productDataGrid = $("<div>")
                    .dxDataGrid({
                        dataSource: localStore,
                        showBorders: true,
                        columns: [
                            {
                                dataField: "productId"
                            },
                            {
                                dataField: "productName"
                            },
                            {
                                dataField: "price"
                            },
                            {
                                dataField: "category"
                            },
                        ],
                    });
                productDataGrid = $productDataGrid.dxDataGrid("instance");

                container.append($productDataGrid);
            }
        });
    root.append($displayProductsPopup);

    
    const menuBarContainer = $("<div>", { id: "menuBar" })
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
                if (e.itemData.items == null) {
                    switch (e.itemData.id) {
                        case "2_1":
                            addProductPopup.show();
                            break;
                        case "2_2_1":
                            productDataGrid.refresh();
                            displayProductsPopup.show();
                            break;
                    }
                    return;
                }

            }
        });

    main.append([
        menuBarContainer,
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