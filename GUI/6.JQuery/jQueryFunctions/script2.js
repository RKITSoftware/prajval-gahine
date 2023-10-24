const objTarget2 = {
    name: "prajval",
    hobbies: {
        cricket: true,
        chess: false
    }
}
console.log("objTarget2", objTarget2.hobbies);

const obj21 = {
    hobbies: {
        football: false,
        travelling: true
    }
}
console.log("obj21", obj21.hobbies);
// console.log("$.extend() with deep flag as false ", $.extend(objTarget2, obj21));
console.log("$.extend() with deep flag as true ", $.extend(true, objTarget2, obj21));

// const obj1 = {
//     name: "png"
// };
// console.log(obj1);

// obj1.name = "prajval";

// console.log(obj1);