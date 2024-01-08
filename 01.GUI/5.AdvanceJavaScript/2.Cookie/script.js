// document.cookie = "name=prajval; SameSite=None; Secure";
document.cookie = "name=prajval; expires=Tue, 10 Aug 2023 12:00:00 UTC;domain=localhost";
const showCookie = document.getElementById("buttonShowCookie");
const hideCookie = document.getElementById("buttonHideCookie");

const output = document.getElementById("output");

showCookie.addEventListener("click", (e) => {
    if(!document.cookie) {
      output.textContent = "cookie is not available";
    } else {
      output.textContent = document.cookie;
    }
});

hideCookie.addEventListener("click", (e) => {
    output.textContent = "";
});

const setCookie = document.getElementById("buttonSetCookie");
setCookie.addEventListener("click", (e) => {
  const key = document.getElementById("cookieKey").value;
  const value = document.getElementById("cookieValue").value;
  document.cookie = `${key}=${value}`;
});