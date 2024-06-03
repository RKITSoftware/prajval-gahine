

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

function findValuePathsInObj(obj, value) {
    let strObj = safeStringify(obj);
    let safeObj = JSON.parse(strObj);
    return findKeysWithValue(safeObj, value);
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