export function renewContainer(idSelector) {
    let element = $(idSelector);
    if (element.length > 0) {
        let index = element.index();
        let parent = element.parent();

        // dispose the element (container)
        element.remove();

        // add element at it's original location
        if (index == 0) {
            return $('<div id="container" class="field-container"></div>').prependTo(parent);
        }
        return $('<div id="container" class="field-container"></div>').insertAfter(parent.children().eq(index - 1));
    }
}