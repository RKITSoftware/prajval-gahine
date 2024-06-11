const products = [
    { id: 1, name: "Laptop", rate: 1200 },
    { id: 2, name: "Earbuds", rate: 800 },
    { id: 3, name: "Tablet", rate: 500 },
    { id: 4, name: "Smartwatch", rate: 250 },
    { id: 5, name: "Headphones", rate: 150 }
];


const orders = [
    {
        id: 1,
        products: [
            {
                productID: 2,
                quantity: 4,
            },
            {
                productID: 1,
                quantity: 1,
            },
            {
                productID: 4,
                quantity: 10,
            },
        ],
        userID: 5,
    },
    {
        id: 2,
        products: [
            {
                productID: 1,
                quantity: 1,
            },
            {
                productID: 5,
                quantity: 5,
            },
            {
                productID: 4,
                quantity: 10,
            },
        ],
        userID: 7,
    },
    {
        id: 3,
        products: [{
            productID: 3,
            quantity: 2,
        }],
        userID: 15,
    },
    {
        id: 4,
        products: [{
            productID: 4,
            quantity: 10,
        }],
        userID: 8,
    },
    {
        id: 5,
        products: [{
            productID: 5,
            quantity: 5,
        }],
        userID: 3,
    },
];