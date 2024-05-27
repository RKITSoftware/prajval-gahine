$(function () {
    // application root element
    const root = $("#root");

    // heading
    const formHeadingContainer = $("<div>", { id: "formHeadingContainer" });
    formHeadingContainer.append($("<h2>", { text: "Student Registration Form" }));

    // mainContainer
    const mainContainer = $("<div>", { id: "mainContainer" });

    // first name field
    const firstNameField = createFieldContainer();
    firstNameField.find(".my-field-label").text("First Name");
    firstNameField.find(".my-field-value").append(
        $("<div>").dxTextBox({
            placeholder: "Enter First Name"
        })
    );

    // last name field


    mainContainer.append([
        firstNameField
    ]);

    root.append([
        formHeadingContainer,
        mainContainer
    ]);

    // utility functions
    function createFieldContainer() {
        return $("<div>", { class: "dx-field my-field" })
            .append($("<div>", { class: "dx-field-label my-field-label" }))
            .append($("<div>", { class: "dx-field-value my-field-value" }));
    }
});