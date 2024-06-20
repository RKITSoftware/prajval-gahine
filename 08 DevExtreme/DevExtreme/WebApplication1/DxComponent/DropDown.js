
import { renewBaseContainer } from "../Utility/Container.js";

export default function addDropDown() {

    const baseContainer = renewBaseContainer();
    const container = $("<div>", { id: "container" }).appendTo(baseContainer);


    // dx drop down ? container a text field (display current value) and drop-down field (any UI component)
    const singleSelectionDDWrapper = $("<div>").appendTo(container);
    $("<div>", { class: "dx-field-label" }).text("Single Selection").appendTo(singleSelectionDDWrapper);
    const singleSelectionDDContainer = $("<div>", { class: "dx-field-value" })
        .appendTo(singleSelectionDDWrapper);

    $("<div>").appendTo(singleSelectionDDContainer).dxDropDownBox({
        dataSource: [
            {
                id: 1,
                name: "prajval",
            },
            {
                id: 2,
                name: "gaurav",
            },
            {
                id: 3,
                name: "admin",
            },
        ],
        onContentReady: function (e) {

        },
        placeholder: "Select name",
        valueExpr: "id",
        displayExpr: "name",
        contentTemplate: function (args) {
            return $("<div>").dxList({
                dataSource: args.component.getDataSource(),
                selectionMode: "single",
                itemTemplate: function (itemData) {
                    return $("<div>").text(itemData.name);
                },
                onItemClick: function (eventData) {
                    args.component.option("value", eventData.itemData.id);
                    args.component.close();
                }
            });
        }
    });

    const singleSelectionDDWithControlWrapper = $("<div>").appendTo(container);
    $("<div>", { class: "dx-field-label" }).text("Single Selection with Control").appendTo(singleSelectionDDWithControlWrapper);
    const singleSelectionDDWithControlContainer = $("<div>", { class: "dx-field-value" })
        .appendTo(singleSelectionDDWithControlWrapper);

    $("<div>").appendTo(singleSelectionDDWithControlContainer).dxDropDownBox({
        dataSource: [
            {
                code: 11,
                name: "Maharashtra",
            },
            {
                code: 12,
                name: "Gujarat",
            },
            {
                code: 13,
                name: "Kerela",
            },
        ],
        placeholder: "Select State",
        valueExpr: "code",
        displayExpr: "name",
        contentTemplate: function (args) {
            return $("<div>").dxList({
                dataSource: args.component.getDataSource(),
                selectionMode: "single",
                itemTemplate: function (itemData) {
                    return $("<div>").text(itemData.name);
                },
                showSelectionControls: true,
                onSelectionChanged: function (eventData) {
                    args.component.option("value", eventData.addedItems[0].code);
                    args.component.close();
                }
            });
        }
    });

    const multipleSelectionDDWrapper = $("<div>").appendTo(container);
    $("<div>", { class: "dx-field-label" }).text("Multiple Selection").appendTo(multipleSelectionDDWrapper);
    const multipleSelectionDDContainer = $("<div>", { class: "dx-field-value" })
        .appendTo(multipleSelectionDDWrapper);

    $("<div>").appendTo(multipleSelectionDDContainer).dxDropDownBox({
        dataSource: [
            {
                code: 111,
                name: "Surat",
            },
            {
                code: 112,
                name: "Pune",
            },
            {
                code: 113,
                name: "Mumbai",
            },
        ],
        elementAttr: {
            id: "multipleSelectionDD"
        },
        placeholder: "Select city",
        value: [111],
        valueExpr: "code",
        displayExpr: "name",
        myIsFirstSelection: true,
        contentTemplate: function (args) {
            return $("<div>").dxList({
                selectedItems: [111],
                dataSource: args.component.getDataSource(),
                selectionMode: "multiple",
                itemTemplate: function (itemData) {
                    return $("<div>").text(itemData.name);
                },
                showSelectionControls: true,
                onSelectionChanged: function (eventData) {
                    var removedItemCode = eventData.removedItems?.[0]?.code;
                    var addedItemCode = eventData.addedItems?.[0]?.code;

                    if (removedItemCode) {
                        args.component.option("value", args.component.option("value").filter(code => code != removedItemCode));
                    }
                    else {
                        if (args.component.option("myIsFirstSelection")) {
                            args.component.option("value", [addedItemCode]);
                            args.component.option("myIsFirstSelection", false);
                        }
                        else {
                            args.component.option("value", [...new Set([...args.component.option("value"), addedItemCode])]);
                        }
                    }
                }
            });
        }
    });


    const customDDWrapper = $("<div>").appendTo(container);
    $("<div>", { class: "dx-field-label" }).text("Custom Drop Down").appendTo(customDDWrapper);
    const customDDContainer = $("<div>", { class: "dx-field-value" })
        .appendTo(customDDWrapper);
    const lstCountry = ["India", "UK", "USA", "Japan", "Kazakhstan"];

    const customDDWidget = $("<div>").appendTo(customDDContainer).dxDropDownBox({
        //dataSource: lstCountry,
        items: lstCountry,
        placeholder: "Select Country",
        acceptCustomValue: true,
        openOnFieldClick: false,
        dropDownOptions: {
            animation: {
                show: {
                    type: 'pop',
                    duration: 200,
                    from: {
                        scale: 0.55
                    }
                },
                hide: {
                    type: 'pop',
                    duration: 200,
                    to: {
                        opacity: 0,
                        scale: 0.55
                    },
                    from: {
                        opacity: 1,
                        scale: 1
                    }
                }
            }
        },
        buttons: [
           {
               name: "currentCountry",
               location: "before",
               options: {
                   icon: "pin",
                   hint: "Current Country",
                   //cssClass: "rotate",
                   onClick: function () {
                       $.get("https://ipinfo.io", function (response) {
                           let regionNames = new Intl.DisplayNames(['en'], { type: 'region' });
                           let name = regionNames.of(response.country);
                           customDDWidget.option("value", name);
                       }, "jsonp");
                   },
                   onInitialized: function (e) {
                       $(e.element).addClass("rotate");
                   }
               }
            },
           "dropDown"
        ],
        contentTemplate: function (args) {
            return $("<div>").dxList({
                //dataSource: args.component.getDataSource(),
                items: args.component.option("items"),
                selectionMode: "single",
                itemTemplate: function (countryName) {
                    return $("<div>").text(countryName);
                },
                onItemClick: function (eventData) {
                    args.component.option("value", eventData.itemData);
                    args.component.close();
                }
            });
        },
        onInitialized: function (e) {
            e.component.registerKeyHandler(113, function (ea) {
                if (ea.shiftKey)
                    e.component.open();
            });
        } ,
        onOpened: function (e) {
            console.log("Opened");
        }
   }).dxDropDownBox("instance");
}





//<div class="dx-scrollable dx-scrollview dx-visibility-change-handler dx-scrollable-vertical dx-scrollable-simulated dx-list dx-widget dx-collection dx-has-next" tabindex="0" role="listbox" aria-activedescendant="dx-d848685e-5371-b3ed-ddb6-a25bf0a82ffd"><div class="dx-scrollable-wrapper"><div class="dx-scrollable-container"><div class="dx-scrollable-content" style="left: 0px; top: 0px; transform: none;"><div class="dx-scrollview-top-pocket"><div class="dx-scrollview-pull-down" style="display: none;"><div class="dx-scrollview-pull-down-image"></div><div class="dx-scrollview-pull-down-indicator"><div class="dx-loadindicator dx-widget"><div class="dx-loadindicator-wrapper"><div class="dx-loadindicator-content"><div class="dx-loadindicator-icon"><div class="dx-loadindicator-segment dx-loadindicator-segment7"></div><div class="dx-loadindicator-segment dx-loadindicator-segment6"></div><div class="dx-loadindicator-segment dx-loadindicator-segment5"></div><div class="dx-loadindicator-segment dx-loadindicator-segment4"></div><div class="dx-loadindicator-segment dx-loadindicator-segment3"></div><div class="dx-loadindicator-segment dx-loadindicator-segment2"></div><div class="dx-loadindicator-segment dx-loadindicator-segment1"></div><div class="dx-loadindicator-segment dx-loadindicator-segment0"></div></div></div></div></div></div><div class="dx-scrollview-pull-down-text"><div class="dx-scrollview-pull-down-text-visible">Pull down to refresh...</div><div>Release to refresh...</div><div>Refreshing...</div></div></div></div><div class="dx-scrollview-content"><div class="dx-item dx-list-item" role="option" aria-selected="false"><div class="dx-item-content dx-list-item-content"><div>India</div></div></div><div class="dx-item dx-list-item dx-list-item-selected" role="option" aria-selected="true" id="dx-d848685e-5371-b3ed-ddb6-a25bf0a82ffd"><div class="dx-item-content dx-list-item-content"><div>UK</div></div></div><div class="dx-item dx-list-item" role="option" aria-selected="false"><div class="dx-item-content dx-list-item-content"><div>USA</div></div></div><div class="dx-item dx-list-item" role="option" aria-selected="false"><div class="dx-item-content dx-list-item-content"><div>Japan</div></div></div><div class="dx-item dx-list-item" role="option" aria-selected="false"><div class="dx-item-content dx-list-item-content"><div>Kazakhstan</div></div></div></div><div class="dx-scrollview-bottom-pocket"><div class="dx-scrollview-scrollbottom" style="display: none;"><div class="dx-scrollview-scrollbottom-indicator"><div class="dx-loadindicator dx-widget"><div class="dx-loadindicator-wrapper"><div class="dx-loadindicator-content"><div class="dx-loadindicator-icon"><div class="dx-loadindicator-segment dx-loadindicator-segment7"></div><div class="dx-loadindicator-segment dx-loadindicator-segment6"></div><div class="dx-loadindicator-segment dx-loadindicator-segment5"></div><div class="dx-loadindicator-segment dx-loadindicator-segment4"></div><div class="dx-loadindicator-segment dx-loadindicator-segment3"></div><div class="dx-loadindicator-segment dx-loadindicator-segment2"></div><div class="dx-loadindicator-segment dx-loadindicator-segment1"></div><div class="dx-loadindicator-segment dx-loadindicator-segment0"></div></div></div></div></div></div><div class="dx-scrollview-scrollbottom-text">Loading...</div></div></div></div><div class="dx-scrollable-scrollbar dx-widget dx-scrollbar-vertical dx-scrollbar-hoverable" style="display: none;"><div class="dx-scrollable-scroll dx-state-invisible" style="height: 199px; transform: translate(0px, 0px);"><div class="dx-scrollable-scroll-content"></div></div></div></div></div><div class="dx-scrollview-loadpanel dx-overlay dx-widget dx-state-invisible dx-visibility-change-handler dx-loadpanel"><div class="dx-overlay-content" aria-hidden="true" style="width: 222px; height: 90px;"></div></div></div>