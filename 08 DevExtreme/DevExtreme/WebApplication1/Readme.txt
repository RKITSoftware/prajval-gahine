To add a new UI Component:

1. In data.js add new item in lstLink array.
    E.g., { text: "Number Box", handler: "addNumberBox" },

2. Create a new File in dxComponent Folder with a function (prefixed with add) exported default.
    E.g., export default function addNumberBox() {...}

3. import in index.js file
    E.g., import addNumberBox from "./DxComponent/NumberBox.js";

4. And at last add the addNumberBox handler in window.lstDemoHandler


