// function to crete a new array and add each element by 2 in it
function copyArrayAndAddBy2(arr) {
    let output = [];
    for(i in arr) {
        output[i] = arr[i] + 2;
    }
    return output;
}
console.log(copyArrayAndAddBy2([100, 200, 300]));

// function to crete a new array and multiply each element by 2 in it
function copyArrayAndMultiplyBy2(arr) {
    let output = [];
    for(i in arr) {
        output[i] = arr[i] * 2;
    }
    return output;
}
console.log(copyArrayAndMultiplyBy2([100, 200, 300]));

// preventing the DRY principle using callbacks in js

// function to return num+2
function addBy2(num) {
    return num+2;
}

// function to return num*2
function multiplyBy2(num) {
    return num*2;
}

// a general function to manipulate an array based on the operation callback
function copyArrayAndManipulate(arr, operation) {
    let output = [];
    for(i in arr) {
        output[i] = operation(arr[i]);
    }
    return output;
}

console.log(copyArrayAndManipulate([100, 200, 300], addBy2));
console.log(copyArrayAndManipulate([100, 200, 300], multiplyBy2));