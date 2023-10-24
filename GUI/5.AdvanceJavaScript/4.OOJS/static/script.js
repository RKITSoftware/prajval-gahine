// a static class in js
class ClassStatic {
    static staticVariable;
    constructor(){
        if(this instanceof ClassStatic) {
            throw Error("A static class cannot be intantiated");
        }
    }
    static staticMethod() {
        console.log("This is a static method");
    }
}

ClassStatic.staticMethod();
ClassStatic.staticVariable = 10;
// const obj = new ClassStatic();   // will throw an error



// static methods
class Article{
    // constructor for Article class
    constructor(name, date) {
        console.log(this);
        this.name = name;
        this.date = date;
    }
    // createTodays() - a static method to create todays article
    static createTodays(name) {
        return new this(name, new Date());
    }
    // compare() - a static method to compare dates of two articles
    static compare(article1, article2) {
        return article1.date - article2.date;
    }
}

const articles = [new Article("HTML", new Date(2019, 12, 7)), Article.createTodays("JS"), new Article("CSS", new Date(2002, 7, 1))];

Article.createTodays();

articles.sort(Article.compare);
alert(articles[0].name);

