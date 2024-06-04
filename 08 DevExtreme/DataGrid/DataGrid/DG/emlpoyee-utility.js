function getRandomElement(array) {
    return array[Math.floor(Math.random() * array.length)];
}

function getRandomDate(start, end) {
    const date = new Date(start.getTime() + Math.random() * (end.getTime() - start.getTime()));
    return date.toISOString().split('T')[0]; // Format as YYYY-MM-DD
}

function shuffleArray(array) {
    for (let i = array.length - 1; i > 0; i--) {
        const j = Math.floor(Math.random() * (i + 1));
        [array[i], array[j]] = [array[j], array[i]]; // Swap elements
    }
    return array;
}

function generateEmployees(numEmployees) {
    const firstNames = ["James", "Emily", "Michael", "Emma", "William", "Olivia", "Daniel", "Sophia", "David", "Isabella"];
    const lastNames = ["Smith", "Johnson", "Williams", "Brown", "Jones", "Garcia", "Miller", "Davis", "Rodriguez", "Martinez"];
    const positions = ["Manager", "Developer", "Analyst", "Designer", "Consultant", "Engineer", "Architect", "Administrator"];
    const minSalary = 50000;
    const maxSalary = 150000;
    const startDate = new Date(1950, 0, 1);
    const endDate = new Date(2000, 11, 31);

    const employees = [];

    for (let i = 1; i <= numEmployees; i++) {
        const employee = {
            "id": i,
            "firstName": getRandomElement(firstNames),
            "lastName": getRandomElement(lastNames),
            "position": getRandomElement(positions),
            "birthDate": getRandomDate(startDate, endDate),
            "salary": Math.floor(Math.random() * (maxSalary - minSalary + 1)) + minSalary
        };
        employees.push(employee);
    }
    return shuffleArray(employees)
    // return employees;
}
