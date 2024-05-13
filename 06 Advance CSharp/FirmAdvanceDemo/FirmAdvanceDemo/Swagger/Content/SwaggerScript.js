// console.log("adding token div");

// check if div already exists
const myTokenDivEl = document.getElementById("my_token_div");
if (myTokenDivEl == null) {
    // if no the add it to dom
    const selectElText = `
    <select name="token" id="user_token_select" accessKey="R">
      <option value="Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjEiLCJleHBpcmVzIjoiMTcyMTc1NDY5NiJ9.9_zFdg09-KAYfFU66GsbONCijPkOSJtwf7-Q-BfC3Y0">Prajval</option>
      <option value="Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjIiLCJleHBpcmVzIjoiMTcyMTc1NDc3MiJ9.X7GwUrKteTKIvQ1DkyV0VnU-B1XcfXc4q5FoHhyWst4">Deep</option>
      <option value="Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjMiLCJleHBpcmVzIjoiMTcyMTc0NDc5MiJ9.0SmWu1u7A1eiFUXxfKodYzJowo2rq9xtHhN-FaBtuB8">Yash</option>
      <option value="Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjQiLCJleHBpcmVzIjoiMTcyMTc1NTcwNiJ9.VxbvQmjXII8NeRUtnQ83gD9zlgsHRyR0PqDNxYNTgF4">Krinsi</option>
    </select>`;

    const divEl = document.createElement("div");
    divEl.className = "input";
    divEl.id = "my_token_div";
    divEl.innerHTML = selectElText;

    const apiSelector = document.getElementById("api_selector");
    apiSelector.appendChild(divEl);
}

// input div is present => therefore select the select el.
const userTokenSelectEl = document.getElementById("user_token_select");
const input_apiKey = document.getElementById("input_apiKey");
input_apiKey.value = userTokenSelectEl.value;

fetchAndSetUsernameAndRoles(userTokenSelectEl.value);
addTokenToInputAndTrigger();

userTokenSelectEl.addEventListener("change", function (e) {
    const authToken = e.target.value;

    fetchAndSetUsernameAndRoles(authToken);
    addTokenToInputAndTrigger();
    
});

function addTokenToInputAndTrigger() {
    const userTokenSelectEl = document.getElementById("user_token_select");
    const input_apiKey = document.getElementById("input_apiKey");
    input_apiKey.value = userTokenSelectEl.value;

    // trigger 
    let apiKeyAuth = new SwaggerClient.ApiKeyAuthorization(swashbuckleConfig.apiKeyName, input_apiKey.value, swashbuckleConfig.apiKeyIn);
    window.swaggerUi.api.clientAuthorizations.add("api_key", apiKeyAuth);
}


async function fetchAndSetUsernameAndRoles(authToken) {
    try {
        let response1 = await fetch("http://localhost:53809/api/swaggerui-support/username", {
            method: 'get',
            headers: new Headers({
                'Authorization': authToken
            }),
        });

        let response2 = await fetch("http://localhost:53809/api/swaggerui-support/roles", {
            method: 'get',
            headers: new Headers({
                'Authorization': authToken
            }),
        });

        let data1 = await response1.json();
        let data2 = await response2.json();

        // console.log(data1.username, data2.roles);

        const info_title = document.getElementsByClassName("info_title")[0];
        if (info_title.innerText == "FirmAdvanceDemo") {
            info_title.innerText += " " + data1.username.toUpperCase() + data2.roles;
        }
        else {
            info_title.innerText = "FirmAdvanceDemo " + data1.username.toUpperCase() + data2.roles;
        }
    }
    catch { }
}