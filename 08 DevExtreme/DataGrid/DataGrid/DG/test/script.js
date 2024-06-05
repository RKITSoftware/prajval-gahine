$(function () {
    var validationGroup = $("#validationGroupContainer").dxValidationGroup({}).dxValidationGroup("instance");

    $("#name").dxTextBox().dxValidator({
        validationGroup: validationGroup,
        validationRules: [{ type: "required", message: "Name is required", auto: "never" }]
    });

    $("#age").dxNumberBox().dxValidator({
        validationGroup: validationGroup,
        validationRules: [{ type: "range", min: 18, max: 100, message: "Age must be between 18 and 100" , auto: "never" }]
    });

    $("#email").dxTextBox()
        .dxValidator({
            validationGroup: validationGroup,
            validationRules: [{ type: "email", message: "Invalid email format" , auto: "never" }]
        })        ;

    $("#validateButton").dxButton({
        text: "Validate",
        onClick: function () {
            validationGroup.validate();
        }
    });

    $("#name, #age, #email").dxValidator("option", "adapter", {
        bypass: function () { return true; }
    });
});