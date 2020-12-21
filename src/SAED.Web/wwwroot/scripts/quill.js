function createExpandedQuill(selector) {
    const toolbarOptions = [
        ["bold", "italic", "underline", "strike"], // toggled buttons
        ["blockquote", "code-block"],
        [{ 'header': 1 }, { 'header': 2 }], // custom button values
        [{ 'list': "ordered" }, { 'list': "bullet" }],
        [{ 'script': "sub" }, { 'script': "super" }], // superscript/subscript
        [{ 'indent': "-1" }, { 'indent': "+1" }], // outdent/indent
        [{ 'direction': "rtl" }], // text direction
        [{ 'size': ["small", false, "large", "huge"] }], // custom dropdown
        [{ 'header': [1, 2, 3, 4, 5, 6, false] }],
        ["link", "image", "video", "formula"], // add's image support
        [{ 'color': [] }, { 'background': [] }], // dropdown with defaults from theme
        [{ 'font': [] }],
        [{ 'align': [] }],
        ["clean"] // remove formatting button
    ];

    return new Quill(selector,
        {
            theme: "snow",
            modules: {
                toolbar: toolbarOptions,
                imageResize: {
                    displaySize: true // default false
                }
            },
        });
}

function createTinyQuill(selector) {
    return new Quill(selector, { theme: "snow" });
}

function createEmptyQuill(selector, readOnly = false) {
    return new Quill(selector, { theme: "snow", modules: { toolbar: false }, readOnly });
}

function bindDeltaToInput(selector, quill) {
    const deltaOps = document.querySelector(selector).value;
    if (deltaOps === "") {
        deltaOps = "{}";
    }
    quill.setContents(JSON.parse(deltaOps));
}