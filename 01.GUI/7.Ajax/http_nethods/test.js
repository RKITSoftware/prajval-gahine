function helloWorld() {
    console.log(this);
}

helloWorld.greet = function() {
    console.log(this);
    console.log("HELLO WORLD");
}

helloWorld();
helloWorld.greet()