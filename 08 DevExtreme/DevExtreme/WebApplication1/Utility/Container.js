export function renewBaseContainer() {
    let baseContainer = $("#baseContainer");

    if (baseContainer.length == 0) {
        throw new Error("No base container found.");
    }

    baseContainer.find("#container")?.remove();

    return baseContainer;
}