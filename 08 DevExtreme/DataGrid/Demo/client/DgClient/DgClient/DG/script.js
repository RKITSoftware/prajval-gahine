
import { createUserDs } from "./DataSources/userDs.js";

$(() => {
    const root = $("#root");

    // header
    const main = $("<div>", { id: "main" });

    // footer

    const userDs = createUserDs();

    const userDGC = $("<div>")
        .dxDataGrid({
            dataSource: userDs,
        });

    main.append([
        userDGC
    ]);

    root.append([
        // header,
        main,
        // footer
    ]);
});