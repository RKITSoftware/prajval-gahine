
// What is a scope ? - Scope is way to define where the variable is accessible.

// variables declared using var (not inside any function) are binded to the global scope.
// variables declared using var (inside a function) are binded to its local scope.

var a = 10; // binded to "global scope" (window object).
let b = 20; // binded to "script scope".

{
    var c = 30; // binded to "global scope" (window object).
    let d = 40; // binded to the "block scope". Add breakpoint at this line
}

console.log(c); // no error, since c is in global scope.
// console.log(d);  // produces reference error: not defined. Since d is not present in the current context.

// FUNCTION SCOPE
// when you invoke a function => you actually creates a local scope for that function.
// And variables(var, let, const) created inside that function are binded to this local scope.

function localScopeDemoFunction() {
    var a = 10;
    let b = 20;
    // both are binded to the local scope.

    // let variable - shadowing
    // let variables can be shadowed(hidden) in nested block.
    // var variables cannot be shadowed inside a function using blocks.
    {
        let b = 50;
        console.log("shadowing b ", b);
    }
    console.log("shadowed b ", b);
}

localScopeDemoFunction();