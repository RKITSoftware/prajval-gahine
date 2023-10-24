
/*
 jQuery.map() - has 2 implementations
 jQuery.map(array, callback)
    - array => the array or arrayLikeObject to translate,
    - callback => (Object elOfArray, Integer indexOfArray) => if returning value is an array then it will be flatten
 jQuery.map(Object, callback)
    - Object - is the non-arrayLikeObject to translate
    - callback - (valueOfObjectEl, keyOfObject)
*/

const arr = [100, 200, 300];
function callback(el, i) {
    return el*2;
}
const updatedArr = jQuery.map(arr, callback);
console.log(arr);
console.log("$.map() - translates an array to an array", updatedArr);

const obj = {
    fname: "prajval",
    lname: "gahine"
}
function callback2(value, key) {
    return value.toUpperCase();
}
const updatedObj = jQuery.map(obj, callback2);
console.log(obj);
console.log("$.map() - translates an object to an array", updatedObj);

/*
 jQuery.grep(arr, filterFunction) -  has just one implementation
 it finds the element if an array which satisfiesa "filter function"
 Here also the original array is not effected
 filterFunction is processed on each item (item, index)
 So, $.grep() mthd removes el from array so that all remaining items passes a provide test.
 - invert argument specifies whether the test should be considered +ve test or -ve test.
*/

function filterCallback(item, ind) {
    return item > 200;
}
console.log(arr);
console.log("$.grep() w/o invert flag", jQuery.grep(arr, filterCallback));
console.log("$.grep() with invert flag", jQuery.grep(arr, filterCallback, true));

/*
 jQuery.extend - merges the content of two or "more" "objects" together into first object(target).
 $.extend(target, obj1 [, objN]) => target-object that rcvs all new props,
    obj1 [, objN] - objects containing additional props to merge.
 $.extend(deep, target, obj1 [, objN]) => deep flag tells that whether the merge should be recursive(true) or not => aka deep copy
    #note - passing false is not supported.
 $.extend(obj) - if only one arg is supplied that means no target object was supplied and hence by default it assumes
    target object as jQuey object. => by this u can attach additional props to jQuery fn namespace.
 $.extend => modifies as well as returns the modified target object
    though u can preserve target object(prevent its modification) by making target object as empty object {}
*/

const objTarget = {
    name: "Prajval"
};
const obj1 = {
    age: 21
};
const obj2 = {
    qualification: "BE CSE"
};
console.log("$.extend() ", $.extend(objTarget, obj1, obj2));
console.log("target modified ", objTarget);

const objTarget2 = {
    name: "prajval",
    hobbies: {
        cricket: true,
        chess: false
    }
}
const obj21 = {
    hobbies: {
        football: false,
        travelling: true
    }
}
console.log("$.extend() with deep flag as false ", $.extend(objTarget2, obj21));
console.log("$.extend() with deep flag as true ", $.extend(true, objTarget2, obj21));


/*
 jQuery.each() - has 2 implementations, iterates seamlessly over 2 "objects" & "array"
 array and arrayLikeObjects are iterated using numeric index(0 to length-1),
 other objects are iterated via their name properties.
 for array and arrayLikeObject - callback(index, value), and for other objects - callback(key, value)
 #NOTE $(selector).each() is a different thing => it is used to iterate over any collection of jQuery object
       where as $.each() is used to iterate over any collection(whether it's object or array) 
 1. jQuery.each(array, callback)
 2. jQuery.each(object, callback)
*/

console.log("jQuery.each - array");
jQuery.each(arr, function(i, value) {
    console.log(arr[i]);
});

console.log("jQuery.each - object");
jQuery.each(obj, function(key, value) {
    console.log(key, value);
});

/*
 jQuery.merge(first, second) - merges the content of "two" arrays or arrayLikeObject into the first array.
 order is preserved ie. second's items are append back of first array.
 jQuery.merge() => modifies the first array and also returns it. to prevent the updation need to create a copy of first array.
    And fortunately jQuery.merge gives the facility to us to do so.
    $.merge($.merge([], first), second)
*/

const first = [11, 22, 33];
const second = [44, 55];
// console.log($.merge($.merge([], first), second));
console.log($.merge(first, second));
console.log(first);
console.log(second);