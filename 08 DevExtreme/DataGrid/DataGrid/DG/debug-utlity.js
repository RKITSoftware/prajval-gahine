

function safeStringify(obj, replacer = null, space = 2) {
    const cache = new Set();
    const str = JSON.stringify(obj, (key, value) => {
        if (typeof value === 'object' && value !== null) {
            if (cache.has(value)) {
                return '[Circular]';
            }
            cache.add(value);
        }
        return value;
    }, space);
    cache.clear();
    return str;
}

function findKeysWithValue(obj, searchValue, path = '', result = []) {
    for (let key in obj) {
        if (obj.hasOwnProperty(key)) {
            const newPath = path ? `${path}.${key}` : key;
            if (typeof obj[key] === 'object') {
                findKeysWithValue(obj[key], searchValue, newPath, result);
            } else if (typeof obj[key] === 'string' && obj[key].includes(searchValue)) {
                result.push(newPath);
            }
        }
    }
    return result;
}
function findKeysPath(obj, searchKey, path = '', result = []) {
    for (let key in obj) {
        if (obj.hasOwnProperty(key)) {
            const newPath = path ? `${path}.${key}` : key;
            if (key.toLowerCase().includes(searchKey.toLowerCase())) {
                result.push(newPath);
            }
            if (typeof obj[key] === 'object' && !Array.isArray(obj[key])) {
                findKeysPath(obj[key], searchKey, newPath, result);
            }
        }
    }
    return result;
}

function findRefPath(obj, searchValue, path = '', result = []) {
    for (let key in obj) {
        if (obj.hasOwnProperty(key)) {
            const newPath = path ? `${path}.${key}` : key;
            if (obj[key] === searchValue) {
                result.push(newPath);
            }
            if (typeof obj[key] === 'object' && !Array.isArray(obj[key])) {
                findRefPath(obj[key], searchValue, newPath, result);
            }
        }
    }
    return result;
}


function xFindValuePathsInObj(obj, value) {
    let strObj = safeStringify(obj);
    let safeObj = JSON.parse(strObj);
    return findKeysWithValue(safeObj, value);
}

function xfindKeyPathsInObj(obj, key) {
    let strObj = safeStringify(obj);
    let safeObj = JSON.parse(strObj);
    return findKeysPath(safeObj, key);
}

function xfindRefPathsInObj(obj, ref) {
    let strObj = safeStringify(obj);
    let safeObj = JSON.parse(strObj);
    return findRefPath(safeObj, ref);
}

function safeStringify2(obj, replacer = null, space = 2) {
    let seenObjects = new WeakMap();

    function serializer(key, value) {
        if (typeof value === 'object' && value !== null) {
            if (seenObjects.has(value)) {
                return '[Circular]';
            }

            // Add the current object to the seen map
            seenObjects.set(value, true);

            // Traverse the object recursively
            const originalValue = value;
            value = Array.isArray(value) ? [] : {};

            for (const prop in originalValue) {
                if (Object.prototype.hasOwnProperty.call(originalValue, prop)) {
                    value[prop] = serializer(prop, originalValue[prop]);
                }
            }

            // Remove the current object from the seen map after traversal
            seenObjects.delete(originalValue);

            return value;
        }
        return value;
    }

    // Use JSON.stringify with the custom serializer
    const result = JSON.stringify(obj, (key, value) => serializer(key, value), space);
    return result;
}


function xShuffleArrayBetweenIndices(arr, n = 0, m = arr.length - 1, inPlace = true) {
    // Ensure n and m are within bounds and n is less than or equal to m
    if (n < 0 || m >= arr.length || n > m) {
        throw new Error("Invalid indices");
    }

    // Make a copy of the array if not shuffling in place
    const arrayToShuffle = inPlace ? arr : [...arr];

    for (let i = m; i > n; i--) {
        // Generate a random index between n and i
        const j = Math.floor(Math.random() * (i - n + 1)) + n;

        // Swap elements at i and j
        [arrayToShuffle[i], arrayToShuffle[j]] = [arrayToShuffle[j], arrayToShuffle[i]];
    }

    return arrayToShuffle;
}