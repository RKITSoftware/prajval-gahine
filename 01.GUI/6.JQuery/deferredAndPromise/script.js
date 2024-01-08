
// typical async function w/o deferred object in js
function doAsync( callback ) {
    setTimeout(function() {
        callback();
    }, 1000);
}

doAsync(function() {
    console.log("Executed after a delay 1");
});

// same thing can be achieved using jQuery.deferred object
function doAsync2() {
    let deferred = $.Deferred();
    setTimeout(function() {
        deferred.resolve();
    }, 1000);
    return deferred.promise();
}

let promise2 = doAsync2();
promise2.then(function() {
    console.log("Executed after a delay 2");
});

// demonstrating both resolve and fail code
function doAsync3() {
    let deferred = $.Deferred();
    // performing some async operation
    setTimeout(function() {
        let num = Math.random();
        if(num < 0.5) {
            deferred.resolve();
        } else {
            deferred.reject();
        }
    }, 1000);
    return deferred.promise();
}
const promise3 = doAsync3();
promise3.done(function() {
    console.log("No. is less than 0.5");
}).fail(function() {
    console.log("No. is greater or equal to than 0.5");
}).always(function() {
    console.log("The promise has been fulfilled");
});