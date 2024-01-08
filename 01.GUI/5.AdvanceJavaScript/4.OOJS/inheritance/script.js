class Person {
    static age = 50;
    static getClassName() {
        console.log("Person");
    }
    constructor(name) {
        this.name = name;
    }
    getName() {
        console.log(this.name);
    }
}

class Employee extends Person {
    constructor(name, salary) {
        super(name);
        console.log(super.age);
        this.salary = salary;
    }
    getSalary() {
        console.log(this.salary);
    }
    getDetails() {
        console.log(super.name, this.salary);
    }
}

const e1 = new Employee("john", 25000);
console.log(e1);
e1.getName();
e1.getSalary();
e1.getDetails();
let a = 20;
let a = 10;