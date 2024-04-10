(async function(){
    const root = document.getElementById("root");
    console.log('\0');

    const header = await getComponent("header.html");
    root.appendChild(header);

    const companyItem = document.getElementById("company-item");
    companyItem.addEventListener("click", async function(){
        hero.innerHTML = null;
        const companyDetails = await getComponent("company-details.html");
        hero.appendChild(companyDetails);
    });

    const hero = await getComponent("hero.html");
    root.appendChild(hero);

    const slidepanel = await getComponent("slidepanel.html");
    slidepanel.classList.add("hide");
    root.appendChild(slidepanel);

    const hamburger = document.getElementById("hamburger");
    hamburger.addEventListener("click", function(e){
        slidepanel.classList.replace("hide", "show");
        slidepanel.firstElementChild.classList.replace("hide-slidepanel-bg", "show-slidepanel-bg");
        slidepanel.lastElementChild.classList.replace("hide-slidebar", "show-slidebar");
        // e.stopPropagation();
    });

    const slidepanelBg = document.getElementById("slidepanel-bg");
    slidepanelBg.addEventListener("click", function(e){
        slidepanel.firstElementChild.classList.replace("show-slidepanel-bg", "hide-slidepanel-bg");
        slidepanel.lastElementChild.classList.replace("show-slidebar", "hide-slidebar");
        slidepanel.classList.replace("show", "hide");
        // e.stopPropagation();
    });

    //populate hero section for company details
    const companyDetails = await getComponent("company-details.html");
    hero.appendChild(companyDetails);

    const weekList = document.getElementById("week-list");
    const weekItem = await getComponent("week-list-item.html");

    // get data from data-source.json
    const response = await fetch("/wwwroot/data/data-source.json");
    let data = await response.json();
    data.sort(function(w1, w2){
        return w1.No - w2.No
    });

    data.forEach(function(week){
        let newWeekItem = weekItem.cloneNode(true);
        newWeekItem.innerText = "Week - " + week.No;
        newWeekItem.dataset.weekno = week.No;
        weekList.appendChild(newWeekItem);
    });

    weekList.addEventListener("click", async function(e){
        if(e.target.classList.contains("week-list-item")){
            e.stopPropagation();

            const weekDetails = await getComponent("week-details.html");

            const weekNo = e.target.dataset.weekno;

            
            const week = data.filter(function(week){
                return week.No == Number(weekNo)
            })[0];
            
            // populate week data in week-detail-section
            const weekHeading = weekDetails.firstElementChild;
            weekHeading.innerText = `Week - ${week.No}, ${week.fromDate} to ${week.toDate}`;

            let ulEl = document.createElement("ul");
            ulEl.classList.add("week-points-list");

            ulEl = week.work.reduce(function(ulEl, point){
                let pointLi = document.createElement("li");
                pointLi.classList.add("point-item");
                pointLi.innerText = point;
                ulEl.appendChild(pointLi);
                return ulEl;
            }, ulEl);

            const weekPoints = weekDetails.lastElementChild;

            weekPoints.innerHTML = null;
            weekPoints.appendChild(ulEl);

            hero.innerHTML = null;
            hero.appendChild(weekDetails);

            // hide slidebar    
            slidepanel.firstElementChild.classList.replace("show-slidepanel-bg", "hide-slidepanel-bg");
            slidepanel.lastElementChild.classList.replace("show-slidebar", "hide-slidebar");
            slidepanel.classList.replace("show", "hide");

            // update url
            // window.history.pushState(
            //     JSON.stringify(document.getElementsByTagName('html')[0].outerHTML),
            //     `week${weekNo}`,
            //     `/week${weekNo}`
            // );
        }
    });
    
})();

async function getComponent(fileName, id){
    const src = "/wwwroot/Components/" + fileName;
    const response = await fetch(src);
    const htmlTxt = await response.text();

    const parser = new DOMParser();
    const doc = parser.parseFromString(htmlTxt, "text/html");

    const component = doc.body.firstElementChild;
    return component;
}





// function f() {
//     console.log('In f');
// }

// async function g() {
//     console.log('Starting g');
//     await f();
//     console.log('Finishing g');
// }


// console.log('Starting the script')
// g();
// console.log('Finishing the script')