

const person1 = {
    age: "prajval",
};

const person2 = {
    age: "gahine",
};

function getage(){
    console.log(this.age);
}

// getage is called in the global context
var age = "png - global context";
getage();

// getage is called in the person1 context. call method is used to change the context wrt function.
getage.call(person1);

// bind method bind a context to a function. And is called in near future.
const getage1 = getage.bind(person1);
getage1();



// object using a function
function Person(age) {
    const person = {};
    person.age = age;
    person.callage = function() {
        console.log(age);
    }
    return person;
}

const person3 = Person("prajval");
console.log(person3);


// object using a class
class PersonB {
    constructor(age) {
        this.age = age;
    }
    callage() {
        console.log(this.age);
    }
}

const person4 = new PersonB("uday");
person4.callage();
console.log(person4);

// normal way to intiate an object
age = 100;
const person5 = {
    age: "pritesh",
    callage: () => {
        console.log(age);
    }
}

console.log(person5);
person5.callage();
console.log(this);


function funcOut(){
    var abc = 200;
    function funcIn(){
        console.log(abc);
    }
    funcIn();
}

funcOut();



// old (before EcmaScript-2015) way to perform OOJS => constructor function to do OOP in JS

function Pen(name, color, price) {
    this.name = name;
    this.color = color;
    this.price = price;
    this.showPrice = function() {
        console.log(price*100);
    }
    console.log(this);
}

Pen("ss", "blue", 5);   // Pen acts as normal function. this refers the global object(window object).
console.log(Pen);
console.log(Pen.prototype);
Pen.prototype.showPrice = function() {
    console.log(this.price);
}


const pen1 = new Pen("ss", "blue", 5);  // Pen acts as constructor function. this refers to the object for which it is called.
console.log(pen1.name, pen1.color, pen1.price);
console.log(pen1.__proto__);
const o1 = {
    name: "prajval",
};
console.log(o1.__proto__);
// pen1.prototype.showPrice();