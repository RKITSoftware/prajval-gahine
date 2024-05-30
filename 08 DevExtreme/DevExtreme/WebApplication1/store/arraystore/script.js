var array = [
    { id: 1, name: 'XYZ', age: 12 },
    { id: 1, name: 'ASDAS', age: 30 },
    { id: 3, name: 'MNO', age: 100 },
    { id: 4, name: 'IJK', age: 12 },
    { id: 5, name: 'ZZZ', age: 30 },
    { id: 6, name: 'EFG', age: 12 },
];

var arrayStore = new DevExpress.data.ArrayStore({
    data: array,
    key: "id",
    onLoading: function (loadOptions) {
        console.log('onLoading:', loadOptions);
    },
    onLoaded: function (result) {
        console.log('onLoaded:', result);
    },
    onInserting: function (values) {
        console.log('onInserting:', values);
        // Perform any validation or preprocessing

        return $.Deferred().reject('Insertion failed');
    },
    onInserted: function (values, key) {
        console.log('onInserted:', values, 'Key:', key);
        // Perform any post-insertion actions
    }
});

// Insert a new item
//arrayStore.insert({
//    name: 'Neon'
//}).done(function (values, key) {
//    console.log('Insertion done:', values, 'Key:', key);
//}).fail(function (error) {
//    console.log('Insertion failed:', error);
//});