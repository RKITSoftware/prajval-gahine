
import { renewBaseContainer } from "../Utility/Container.js";

export default function addList() {
    const baseContainer = renewBaseContainer();
    const container = $("<div>", { id: "container" }).appendTo(baseContainer);


    container.dxList({
        dataSource: ["Apple", "Mango", "Banana", "Chiku"],
        searchEnabled: true,
        searchEditorOptions: {
            placeholder: 'Search Fruits',
            showClearButton: true,
            inputAttr: { 'aria-label': 'Search' },
        },
        selectionMode: "single",
        onSelectionChanged: function () {

        }
    });
}