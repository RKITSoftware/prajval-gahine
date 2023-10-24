

function success(e) {
    console.log("SUCCESS");
    console.log(e);
    console.log(this);
    console.log(typeof this.response);
    console.log(this.response);
    // console.log(typeof this.responseText);
    // console.log(this.responseText);
}
function error(e) {
    console.log("ERROR");
    console.log(e);
    console.log(this);
}



const xhr = new XMLHttpRequest();

xhr.onload = success;
xhr.onerror = error;
xhr.open("get", "https://jsonplaceholder.typicode.com/todos/");
xhr.send();
const xhr = new XMLHttpRequest();
xhr.onload = success;
xhr.onerror = error;
xhr.open("get", "/file.xml", true);
xhr.send();
console.log(xhr);

// const xhr2 = new XMLHttpRequest();
// xhr2.responseType = "json";
// xhr2.onload = success;
// xhr2.onerror = error;
// xhr2.open("get", "/file.json");
// console.log("helllo");
// xhr2.send();

// console.log("world");
// console.log("I am done");

