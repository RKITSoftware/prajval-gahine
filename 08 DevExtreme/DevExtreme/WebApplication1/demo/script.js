$(function () {
    // application root element
    const root = $("#root");

    // heading
    const formHeadingContainer = $("<div>", { id: "formHeadingContainer" });
    formHeadingContainer.append($("<h2>", { text: "Student Registration Form" }));

    // mainContainer
    const mainContainer = $("<div>", { id: "mainContainer" });
    const form = $("<div>", { id: "form" });

    let cityWidget = null;

    // form
    let formWidget = form.dxForm({
        formData,
        showColonAfterLabel: true,
        showValidationSummary: true,
        validationGroup: 'studentData',
        items: [
            // Personal Details
            {
                itemType: "group",
                caption: "Personal Details",
                items: [
                     // firstName
                    {
                        dataField: 'firstName',
                        editorOptions: {
                            stylingMode: "outlined",
                            height: "40px",
                        },
                        label: {
                            //location: "left",
                            //alignment: "center",
                            //showColon: false,
                            //isRequiredMarker: false,
                            text: "First Name",
                        },
                        validationRules: [
                            {
                                type: "required",
                                message: "First Name is required"
                            },
                            {
                                type: 'pattern',
                                pattern: '^[^0-9]+$',
                                message: 'Do not use digits in the Name',
                            }
                        ]
                    },
                    // lastName
                    {
                        dataField: 'lastName',
                        editorOptions: {
                            stylingMode: "outlined",
                            height: "40px",
                        },
                        label: {
                            text: "Last Name",
                        },
                        validationRules: [
                            {
                                type: "required",
                                message: "Last Name is required"
                            },
                            {
                                type: 'pattern',
                                pattern: '^[^0-9]+$',
                                message: 'Do not use digits in the Name',
                            }
                        ]
                    },
                    // dob
                    {
                        dataField: "dob",
                        editorType: "dxDateBox",
                        editorOptions: {
                            displayFormat: "dd/MM/yyyy",
                            inputAttr: {
                                id: "dateInput"
                            },
                            placeholder: "Select DOB",
                            pickerType: "calendar",
                            dropDownOptions: {
                                position: {
                                    my: 'right top',    // right top of drop down
                                    at: 'right bottom', // right bottom of taget (dateInput)
                                    of: '#dateInput'  // target is 
                                }
                            },
                            onContentReady: function (e) {
                                var input = e.component._input();
                                input.attr('id', 'dateInput');
                            }
                        },
                        label: {
                            text: "Date of Birth",
                        },
                        validationRules: [
                            {
                                type: "required",
                                message: "Date of Birth is required",
                            },
                            {
                                type: "range",
                                max: new Date().setFullYear(new Date().getFullYear() - 17),
                                message: "Student must be above 17 years",
                            }
                        ]
                    },
                    // gender
                    {
                        dataField: "gender",
                        editorType: "dxRadioGroup",
                        editorOptions: {
                            valueExpr: "value",
                            displayExpr: "text",
                            layout: "horizontal",
                            items: [
                                {
                                    value: "male",
                                    text: "Male",
                                },
                                {
                                    value: "female",
                                    text: "Female",
                                },
                                {
                                    value: "other",
                                    text: "Other",
                                },
                            ]
                        },
                        label: {
                            text: "Gender",
                        },
                        validationRules: [
                            {
                                type: "required",
                                message: "Gender is required",
                            }
                        ]
                    },
                    // hobbies
                    {
                        dataField: "hobbies",
                        template: function (fieldInfo, itemElement) {
                            itemElement.addClass("flex-container");
                            // add predefined hobbies
                            ["Circket", "Football", "Hockey", "Other"].forEach(function (optionText) {
                                $("<div>").addClass("flex-item").dxCheckBox({
                                    value: formData.hobbies.indexOf(optionText) !== -1,
                                    text: optionText,
                                    onValueChanged: function (e) {
                                        let value = e.component.option("value");
                                        if (optionText == "Other") {
                                            if (!value) {
                                                let inputValue = otherHobbyInputWidget.option("value");
                                                let index = formData.hobbies.indexOf(inputValue);
                                                if (index != -1) {
                                                    formData.hobbies.splice(index, 1);
                                                    otherHobbyInputWidget.option("value", '')
                                                }
                                            }
                                            otherHobbyInputWidget.option("disabled", !value);
                                            value ? otherHobbyInputWidget.focus() : '';
                                            return;
                                        }

                                        let index = formData.hobbies.indexOf(optionText);

                                        if (value && index === -1) {
                                            formData.hobbies.push(optionText);
                                        } else if (!value && index !== -1) {
                                            formData.hobbies.splice(index, 1);
                                        }
                                    }
                                }).appendTo(itemElement);
                            });

                            // add a text area to enable other hobbies
                            const otherHobbyInputWidget = $("<div>", {style: "display: inline"}).dxTextBox({
                                value: "",
                                disabled: true,
                                onValueChanged: function (e) {
                                    if (e.value) {
                                        let index = formData.hobbies.indexOf(e.previousValue);
                                        if (index == -1) {
                                            formData.hobbies.push(e.value);
                                        }
                                        else {
                                            formData.hobbies[index] = e.value;
                                        }
                                    }
                                }
                            })
                                .appendTo(itemElement)
                                .dxTextBox("instance");
                        }
                    },  
                ]
            },
            // Contact Details
            {
                itemType: "group",
                caption: "Contact Details",
                items: [
                    // email
                    {
                        dataField: "email",
                        editorOptions: {
                            stylingMode: "outlined",
                            height: "40px",
                        },
                        label: {
                            text: "Email",
                        },
                        validationRules: [
                            {
                                type: "required",
                                message: "Email is required",
                            },
                            {
                                type: "email",
                                message: "Invalid email"
                            }
                        ]
                    },
                    // mobileNo
                    {
                        dataField: "mobileNo",
                        editorOptions: {
                            stylingMode: "outlined",
                            height: "40px",
                            mask: "+\\91 00000 00000",
                            maskRules: {},
                            maskInvalidMessage: "Invalid mobile number",
                            showClearButton: true
                        },
                        label: {
                            text: "Mobile Number",
                        },
                        validationRules: [
                            {
                                type: "required",
                                message: "Mobile Number is required",
                            }
                        ]
                    }
                ]
            },
            // Address Details
            {
                itemType: "group",
                caption: "Address Details",
                items: [
                    // address
                    {
                        dataField: "address",
                        label: {
                            text: "Address",
                        },
                        editorType: "dxTextArea",
                        editorOptions: {
                            stylingMode: "outlined",
                            maxLength: 100,
                        },
                        validationRules: [
                            {
                                type: "required",
                                message: "Address is required",
                            }
                        ]
                    },
                    // state
                    {
                        dataField: "state",
                        label: {
                            text: "State"
                        },
                        editorType: "dxSelectBox",
                        editorOptions: {
                            items: Object.keys(region),
                            placeholder: "Select State",
                            onValueChanged: function (e) {
                                cityWidget.option("value", '');
                                let valRules = formWidget.itemOption("city", "validationRules");
                                formWidget.itemOption("city", "validationRules", null);
                                cityWidget.option("items", region[e.value]);
                                formWidget.itemOption("city", "validationRules", valRules);
                            }
                        },
                        validationRules: [
                            {
                                type: "required",
                                message: "State is required",
                            }
                        ]
                    },
                    // city
                    {
                        dataField: "city",
                        label: {
                            text: "City",
                        },
                        editorType: "dxSelectBox",
                        editorOptions: {
                            items:[],
                            placeholder: "Select City",
                            onInitialized: function (e) {
                                cityWidget = e.component;
                            },
                        },
                        validationRules: [
                            {
                                type: "required",
                                message: "City is required",
                            }
                        ]
                    }
                ]
            },
            // Curriculum
            {
                itemType: "group",
                caption: "Curriculum",
                items: [
                    // grade
                    {
                        dataField: "grade",
                        editorType: "dxNumberBox",
                        label: {
                            text: "Grade"
                        },
                        editorOptions: {
                            format: "#.##",
                        },
                        validationRules: [
                            {
                                type: "required",
                                min: 0,
                                max: 100,
                                message: "Grade is required",
                            },
                            {
                                type: "range",
                                min: 0,
                                max: 100,
                                message: "Grade must be in 0 to 100 range",
                            }
                        ]
                    },
                    // marksheet
                    {
                        dataField: "marksheet",
                        editorType: "dxFileUploader",
                        label: {
                            text: "Marksheet",
                        },
                        editorOptions: {
                            uploadMode: "useForm",
                            allowedFileExtensions: ['.jpg', '.jpeg', '.png', '.pdf'],
                        },
                        validationRules: [
                            {
                                type: "required",
                                message: "Marsheet is required",
                            }
                        ]
                    }
                ]
            },
            {
                itemType: "button",
                horizontalAlignment: "left",
                buttonOptions: {
                    text: "Register",
                    type: "default",
                    onClick: function (e) {
                        var formWidget = $("#form").dxForm("instance");
                        var validationResult = formWidget.validate();
                        if (validationResult.isValid) {
                            e.component.option("disabled", true);
                            let formData = formWidget.option("formData");
                            //formData.marksheet = '';
                            sessionStorage.setItem("studentData", JSON.stringify(formData));
                            DevExpress.ui.notify("Student registered successfully.");
                        }
                        else {
                            DevExpress.ui.notify({
                                message: "Form inputs are invalid",
                                type: "error",
                            });
                        }
                    }
                }
            }
        ]
    }).dxForm("instance");

    mainContainer.append(form);

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