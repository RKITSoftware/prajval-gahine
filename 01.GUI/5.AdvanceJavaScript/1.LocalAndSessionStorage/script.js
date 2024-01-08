
const localOutput = document.getElementById("localOutput");
if(localStorage.getItem("count")) {
    localOutput.textContent = localStorage.getItem("count");
} else {
    localOutput.textContent = 0;
}
const local = document.getElementById("local");
local.addEventListener("click", () => {
    const count = localStorage.getItem("count");
    if(count) {
        localStorage.setItem("count", Number(count)+1);
    } else {
        localStorage.setItem("count", 1);
    }
    localOutput.textContent = localStorage.getItem("count");
});



const sessionOutput = document.getElementById("sessionOutput");
if(sessionStorage.getItem("count")) {
    sessionOutput.textContent = sessionStorage.getItem("count");
} else {
    sessionOutput.textContent = 0;
}
const session = document.getElementById("session");
session.addEventListener("click", () => {
    const count = sessionStorage.getItem("count");
    if(count) {
        sessionStorage.setItem("count", Number(count)+1);
    } else {
        sessionStorage.setItem("count", 1);
    }
    sessionOutput.textContent = sessionStorage.getItem("count");
});