const arr = [35,
28,
37,
12,
18,
10,
4,
22,
26,
25,
7,
30,
24,
6,
2,
1,
20,
29,
32,
31,
23,
27,
14,
17,
15,
13,
21,
34,
3,
8,
11,
33,
36,
16,
9,
5,
19,];

function compare(a, b) {
    return a-b;
}
const arrSorted = arr.sort(compare);
console.log(arrSorted);


/*
gstin format
    first 2 char => respresent state tin no, which varies from 01 to 38 and 97
*/

/*
const map = {
    '0': 0, '1': 1, '2': 2, '3': 3, '4': 4, '5': 5, '6': 6, '7': 7, '8': 8, '9': 9, 'A': 10, 'B': 11, 'C': 12, 'D': 13, 'E': 14, 'F': 15, 'G': 16, 'H': 17, 'I': 18,
    'J': 19, 'K': 20, 'L': 21, 'M': 22, 'N': 23, 'O': 24, 'P': 25, 'Q': 26, 'R': 27, 'S': 28, 'T': 29, 'U': 30, 'V': 31, 'W': 32, 'X': 33, 'Y': 34, 'Z': 35
};
*/

// creating a mapping for index and value
const mappingForGSTIN = [
                    '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 
                    'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'
                ];

// an algorithm for checksum of gstin
function validateHash(str) {
    // taking last digit which is checksum into checkSum variable
    const checkSum = str[str.length-1];
    // getting corresponding index code
    let checkSumCodeFromStr = mappingForGSTIN.indexOf(checkSum);
    let multiplier = 1;
    let hashSum = 0;
    for(let i = 0; i<str.length-1; i++) {
        const code = mappingForGSTIN.indexOf(str[i]);
        const product = code * multiplier;
        multiplier == 1 ? multiplier = 2 : multiplier = 1;
        const hash = Math.floor(product/36) + product%36;
        hashSum += hash;
    }
    const remainderOfHashSum = hashSum%36;
    const checkSumCode = (36 - remainderOfHashSum)%36;

    if(checkSumCodeFromStr === checkSumCode) {
        return true;
    }
    return false;
}

// validateHash("07AAECR2971C1Z");
// algorithm for pan validity
function isValidPAN(pan) {
    const panType = ['T', 'F', 'H', 'P', 'C'];
    const number = +pan.slice(5, 9);
    if(!pan.slice(0, 3).match(/[A-Z]{3}/)?.length) {
        return false;
    }
    if(!panType.includes(pan[3])) {
        return false;
    }
    if(!pan[4].match(/[A-Z]/)?.length) {
        return false;
    }
    if(isNaN(number)) {
        return false;
    }
    // the tenth character (index 9) is checksum and it's algorithm is not publically available for security reason.
    return true;
}

// algorithm for gstin validity
function isValidGSTIN(gstin) {
    // gstin must be of 15 length long
    if(gstin.length != 15) {
        return false;
    }
    // validate whether the first 2 digits are number which ressembles a state code
    const stateCode = +gstin.slice(0, 2);
    if(!(stateCode && (stateCode>=1 && stateCode<=38 || stateCode==98))) {
        return false;
    }
    // validating the next 10 character string is PAN or not
    if(!isValidPAN(gstin.slice(2, 12))) {
        return false;
    }
    // checking if char at 12 index is a digit or not
    const entityNumber = +gstin[12];
    if(isNaN(entityNumber)) {
        return false;
    }
    // char at 13 index is bydefault Z
    if(gstin[13] != 'Z') {
        return false;
    }
    // performing checksum over gstin
    return validateHash(gstin);
}

console.log(validateHash("33AIOPC3903A1ZZ"));