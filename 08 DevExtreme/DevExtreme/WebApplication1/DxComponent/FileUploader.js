import { renewBaseContainer } from "../Utility/Container.js";

export default function addFileUploader() {
    const baseContainer = renewBaseContainer();
    const container = $("<div>", { id: "container" }).appendTo(baseContainer);

    const defaultFileUploaderWidgetContainer = $("<div>", { class: "demo-box" }).dxFileUploader();
    const defaultFileUploaderWidget = defaultFileUploaderWidgetContainer.dxFileUploader("instance");

    const buttonUploaderWidgetContainer = $("<div>", { class: "demo-box" }).dxFileUploader({
        uploadMode: "useButtons",
        allowCanceling: false,
        allowedFileExtensions: ['.jpg', '.jpeg', '.gif', '.png'],
        labelText: "Drop an image",
        selectButtonText: "Select an image",
        activeStateEnabled: false,
        focusStateEnabled: false,
        hoverStateEnabled: false,
        isValid: true,
        maxFileSize: 100,
    });
    const buttonFileUploaderWidget = buttonUploaderWidgetContainer.dxFileUploader("instance");

    const chunkUploaderWidgetContainer = $("<div>", { class: "demo-box" }).dxFileUploader({
        uploadMode: "useButtons",
        labelText: "Drop to-chunk file",
        selectButtonText: "Select to-chunk file",
        chunkSize: 2048, // 2kb
        uploadUrl: 'https://js.devexpress.com/Demos/WidgetsGalleryDataService/api/ChunkUpload',
        onUploadStarted: function (e) {
            console.log(e); // component, element (container), event (undefined), file, xhr
            chunkUploaderWidgetContainer.find(".uplaod-detail-panel").html("");
        },
        onProgress: function (e) {
            console.log(e); // above all, bytesLoaded, bytesTotal (constant), segmnetSize (constant)
            chunkUploaderWidgetContainer.find(".uplaod-detail-panel").append(`<div>${e.segmentSize} ${e.bytesLoaded} ${e.bytesTotal}</div>`);
        }
    });
    const chunkUploaderWidget = chunkUploaderWidgetContainer.dxFileUploader("instance");
    chunkUploaderWidgetContainer.append($("<div>", { class: "uplaod-detail-panel" }));



    // -------------------------------------------------------------------------------------------------
    // -------------------------------------- dialog and drop box --------------------------------------
    // -------------------------------------------------------------------------------------------------

    const dialogUploaderWrapper = $("<div>", { id: "custome-drag-drop-wrapper", class: "demo-box" });
    const dialogUploaderContainer = $("<div>");

    dialogUploaderWrapper.append(dialogUploaderContainer);
    const dialogTriggerBox = $("<div id='custom-dialog-trigger' class='custom-blue-container'>Dialog trigger</div>").appendTo(dialogUploaderWrapper);
    const dropZoneBox = $("<div id='custom-drop-zone' class='custom-blue-container'>Drop Zone</div>").appendTo(dialogUploaderWrapper);

    dialogUploaderContainer.dxFileUploader({
        uploadMode: "useButtons",
        labelText: "Dialog & Drop",
        selectButtonText: "Select",
        dialogTrigger: '#custom-dialog-trigger',
        dropZone: "#custom-drop-zone",
        dialogTrigger: dialogTriggerBox,
        allowedFileExtensions: ['.jpg'],
        dropZone: dropZoneBox,
        onContentReady: function (e) {
            // Remove the default button and label elements
            $(e.element).find('.dx-fileuploader-button, .dx-fileuploader-input-label').remove();
            $(e.element).hide();
        },
    });



    // -------------------------------------------------------------------------------------------------
    // ------------------------------------------ form -------------------------------------------------
    // -------------------------------------------------------------------------------------------------
    function submitForm(e) {
        e.preventDefault();
    }
    const formFileUploaderWrapper = $("<div>", { class: "demo-box" });
    const form = $("<form>").appendTo(formFileUploaderWrapper);
    form.attr({
        "action": "https://js.devexpress.com/Demos/WidgetsGalleryDataService/api/ChunkUpload",
        "method": "POST",
        "enctype": "multipart/form-data",
    });
    form.on("submit", function (e) {
        //e.preventDefault();
    });
    form.append('<button type="submit">Submit</button>');
    const formFileUploaderContainer = $("<div>").appendTo(form);

    formFileUploaderContainer.dxFileUploader({
        uploadMode: "useForm",
        //uploadUrl: 'https://js.devexpress.com/Demos/WidgetsGalleryDataService/api/ChunkUpload',   // no sense
        //dropZone: 'dropZoneBox',  // a custom drop down will not work
        //allowedFileExtensions: ['.jpg'],
    });



    // -------------------------------------------------------------------------------------------------
    // ---------------------------- events related to file uploader ------------------------------------
    // -------------------------------------------------------------------------------------------------

    const eventFileUploaderContainer = $("<div>", { class: "demo-box" }).dxFileUploader({
        labelText: "Events",
        uploadMode: "useButtons",
        uploadUrl: 'https://js.devexpress.com/Demos/WidgetsGalleryDataService/api/ChunkUpload',
        chunkSize: 10240,
        onInitialized: function (e) {
            // 1
            console.log(e);
            DevExpress.ui.notify("onInitialized executed");
        },
        onContentReady: function (e) {
            // 2
            console.log(e);
            //DevExpress.ui.notify("onContentReady executed");
        },
        onOptionChanged: function (e) {
            // e.name (option name) value, progress
            // 3, also executed on click on X
            console.log(e);
            DevExpress.ui.notify("onOptionChanged executed");
        },
        onDropZoneEnter: function (e) {
            console.log(e);
            DevExpress.ui.notify("onDropZoneEnter executed");
        },
        onDropZoneLeave: function (e) {
            console.log(e);
            DevExpress.ui.notify("onDropZoneLeave executed");
        },
        onProgress: function (e) {
            console.log(e);
            DevExpress.ui.notify("onProgress executed");
        },
        onUploadAborted: function (e) {
            console.log(e);
            DevExpress.ui.notify("onUploadAborted executed");
        },
        onUploaded: function (e) {
            console.log(e);
            DevExpress.ui.notify("onUploaded executed");
        },
        onFilesUploaded: function (e) {
            // even executed when error occurs
            console.log(e);
            DevExpress.ui.notify("onFilesUploaded executed");
        },
        onUploadError: function (e) {
            console.log(e);
            DevExpress.ui.notify("onUploadError executed");
        },
        onValueChanged: function (e) {
            // 4
            console.log(e);
            DevExpress.ui.notify("onValueChanged executed");
        },
        onBeforeSend: function (e) {
            // 3.1, when clicked on upload
            console.log(e);
            DevExpress.ui.notify("onBeforeSend executed");
        }
    });
    const eventFileUploaderWidget = DevExpress.ui.dxFileUploader.getInstance(eventFileUploaderContainer);

    eventFileUploaderWidget.off("dropZoneEnter");
    eventFileUploaderWidget.off("dropZoneLeave");


    const customChunkUploadContainer = $("<div>", { class: "demo-box" });
    const customChunkUploadWidget = customChunkUploadContainer.dxFileUploader({
        labelText: "Custom Chunk Uploader",
        multiple: true,
        //chunkSize: 2048,
        uploadUrl: 'https://js.devexpress.com/Demos/WidgetsGalleryDataService/api/ChunkUpload',
        uploadCustomData: {
            url: 'https://js.devexpress.com/Demos/WidgetsGalleryDataService/api/ChunkUpload',
        },
        uploadCustomData: {
            token: "This is my token",
        },
        onProgress: function (e) {

        },
        uploadedMessage: "Upload ho gaya",
        uploadFailedMessage: "Upload fail ho gaya",
        uploadChunk: function (file, chunkInfo) {
            // write ur xhr or fetch logic of uploading the chunk data
            //console.log(chunkInfo);

            // NOTE: "this" will not refer to the widget object, it will be undefined
            // made modification in dx.all.debug.js at line no. 58074
            console.log(this);

            let offset = chunkInfo.bytesUploaded;
            let toIndex = offset + chunkInfo.chunkBlob.size;
            let totalChunkCount = chunkInfo.chunkCount;

            const chunk = file.slice(offset, toIndex);

            const formData = new FormData();
            formData.append("chunk", chunk);
            formData.append("fileName", file.name);
            formData.append("offset", offset);
            formData.append("totalChunks", totalChunkCount);
            formData.append("token", customChunkUploadWidget.option("uploadCustomData").token);

            // chunkInfo's chunkIndex and byteUploaded are updated implicitly by dx

            //return Promise.resolve();
            
            return fetch(customChunkUploadWidget.option("uploadUrl"), {
                method: "POST",
                body: formData,
            }).then((response) => {
                //console.log(response);
                if (response.ok) {
                    DevExpress.ui.notify("Chunk uploaded");
                }
                else {
                    DevExpress.ui.notify({
                        message: "Chunk failed to upload",
                        type: "error",
                    });
                }
            });
        },
        uploadFile: function (file, progressCallback) {
            // if used chunkSize then this method will never be called
            const url = customChunkUploadWidget.option("uploadUrl");
            return fetch(url, {
                method: "POST",
                body: file
            });
        }
    }).dxFileUploader("instance");


    container.append([
        defaultFileUploaderWidgetContainer,
        buttonUploaderWidgetContainer,
        chunkUploaderWidgetContainer, 
        dialogUploaderWrapper,
        formFileUploaderWrapper,
        eventFileUploaderContainer,
        customChunkUploadContainer
    ]);
}