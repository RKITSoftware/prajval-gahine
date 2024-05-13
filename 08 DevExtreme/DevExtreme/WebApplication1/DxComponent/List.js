export default function addList() {
    let container = $("#container");

    if (container) {
        // dispose the main container
        container.remove();
    }

    // append #container after linkContainer
    $("#linkContainer").after('<div id="container"></div>');

    container = $("#container");

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