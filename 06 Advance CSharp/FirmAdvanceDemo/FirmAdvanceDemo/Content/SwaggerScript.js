console.log("Custom script executing...");

const input_apiKey = document.getElementById("input_apiKey");
input_apiKey.value = "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjIiLCJleHBpcmVzIjoiMTcyMTc1NDc3MiJ9.X7GwUrKteTKIvQ1DkyV0VnU-B1XcfXc4q5FoHhyWst4";

input_apiKey.addEventListener("blur", async () => {

    //console.log("INSIDE EVENT");
    let authToken = input_apiKey.value;

    try {
        let response1 = await fetch("http://localhost:53809/api/data/getusername", {
            method: 'get',
            headers: new Headers({
                'Authorization': authToken
            }),
        });

        let response2 = await fetch("http://localhost:53809/api/data/getroles", {
            method: 'get',
            headers: new Headers({
                'Authorization': authToken
            }),
        });

        let data1 = await response1.json();
        let data2 = await response2.json();

        const info_title = document.getElementsByClassName("info_title")[0];
        if (info_title.innerHTML == "FirmAdvanceDemo") {
            info_title.innerText += " " + data1.username.toUpperCase() + data2.roles;
        }
        else {
            info_title.innerText = "FirmAdvanceDemo " + data1.username.toUpperCase() + data2.roles;
        }
    }
    catch {}
});

//alert("Hello world");