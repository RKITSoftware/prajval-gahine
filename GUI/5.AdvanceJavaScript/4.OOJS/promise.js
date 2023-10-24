
// Promise object

console.log("Begin");
let promise1 = new Promise((resolve, reject) => {
    setTimeout(() => {
        resolve("Success 200");
    }, 5000);
    setTimeout(() => {
        reject(new Error("Error 404"));
    }, 2000);
});

console.log("Middle");

promise1.then((value) => {
    console.log("Promise resolved successfully with value ", value);
}, (error) => {
    console.log("Promise rejected with error ", error);
});
console.log("End");