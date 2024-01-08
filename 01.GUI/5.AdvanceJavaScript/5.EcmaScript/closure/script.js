function outer() {
    let counter = 0;
    const incrementCounter =() => {
        counter++;
    }
    return incrementCounter;
}

const newIncrementCounter = outer();
newIncrementCounter();
newIncrementCounter();