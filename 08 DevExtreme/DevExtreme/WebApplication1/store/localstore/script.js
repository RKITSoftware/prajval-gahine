
var array = [
    { id: 1, name: 'XYZ', age: 12, ageGroup: "teen" },
    { id: 2, name: 'ASDAS', age: 30, ageGroup: "adult" },
    { id: 3, name: 'MNO', age: 100, ageGroup: "old" },
    { id: 4, name: 'IJK', age: 12, ageGroup: "teen"  },
    { id: 5, name: 'ZZZ', age: 30, ageGroup: "adult"  },
    { id: 6, name: 'EFG', age: 12, ageGroup: "teen"  },
]

var localStore = new DevExpress.data.LocalStore({
    name: "png",
    key: "id",
    data: array,
    //flushInterval: 1000,
    onInserting: function () {
        console.log("onInserting");
    },
    onInserted: function () {
        console.log("onInserted");
    },
    onLoaded: function () {
        console.log("onLoaded");
    },
});
