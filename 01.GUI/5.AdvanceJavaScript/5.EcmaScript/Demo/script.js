

// validation helper functions

function validateFullName(fullName) {
    const fullNameDescriptor = document.getElementById("fullNameDescriptor");
    if(!fullName) {
        fullNameDescriptor.textContent = "Enter Full Name.";
        fullNameDescriptor.style.color = "red";
        return false;
    }
    fullNameDescriptor.textContent = "";
    return true;    
}

function validateEmail(email){
    if(email === ""){
        emailDescriptor.textContent = "";
        return true;
    }
    const atIndex = email.indexOf("@");
    const dotIndex = email.indexOf(".");
    if(atIndex<1 || dotIndex-2 < atIndex || dotIndex + 2 >= email.length){
        const emailDescriptor = document.getElementById("emailDescriptor");
        emailDescriptor.textContent = "Invalid Email!";
        emailDescriptor.style.color = "red";
        return false;
    }
    emailDescriptor.textContent = "Valid Email!";
    emailDescriptor.style.color = "green";
    return true;
}

function validatePassword(password) {
    const passwordDescriptor = document.getElementById("passwordDescriptor");
    if(!password || password.length < 8) {
        passwordDescriptor.textContent = "Enter password in correct format.";
        passwordDescriptor.style.color = "red";
        return false;
    }
    passwordDescriptor.textContent = "";
    return true;    
}

function validateGender(isMaleChecked, isFemaleChecked) {
    const genderDescriptor = document.getElementById("genderDescriptor");
    if(!(isMaleChecked || isFemaleChecked)) {
        genderDescriptor.textContent = "Either of genders must be selected.";
        genderDescriptor.style.color = "red";
        return false;
    }
    genderDescriptor.textContent = "";
    return true;    
}

function validateLanguage(isMarathiKnown, isGujaratiKnown, isHindiKnown, isEnglishKnown) {
    const languageDescriptor = document.getElementById("languageDescriptor");
    if(!(isMarathiKnown || isGujaratiKnown || isHindiKnown || isEnglishKnown)) {
        languageDescriptor.textContent = "Atleast one language must be known.";
        languageDescriptor.style.color = "red";
        return false;
    }
    languageDescriptor.textContent = "";
    return true;    
}

function validateDateOfBirth(dateOfBirthString) {
    const dateOfBirth = new Date(dateOfBirthString);
    const dateOfBirthDescriptor = document.getElementById("dateOfBirthDescriptor");
    if(isNaN(dateOfBirth.getTime())){
        dateOfBirthDescriptor.textContent = "Enter a valid Date Of Birth.";
        dateOfBirthDescriptor.style.color = "red";
        return false;
    }
    dateOfBirthDescriptor.textContent = "";
    return true;    
}

function validatePhoto(length) {
    const photoDescriptor = document.getElementById("photoDescriptor");
    if(length === 0) {
        photoDescriptor.textContent = "Select a photo.";
        photoDescriptor.style.color = "red";
        return false;
    }
    photoDescriptor.textContent = "";
    return true;    
}

function validateQualification(highestQualification) {
    const highestQualificationDescriptor = document.getElementById("highestQualificationDescriptor");
    if(highestQualification === "notSelected") {
        highestQualificationDescriptor.textContent = "Select Highest Qualification.";
        highestQualificationDescriptor.style.color = "red";
        return false;
    }
    highestQualificationDescriptor.textContent = "";
    return true;    
}

// form validation on submit
function validate(){
    const fullName = document.forms["form"]["fullName"].value;
    const email = document.forms["form"]["email"].value;
    const password = document.forms["form"]["passwordInput"].value;
    const isMaleChecked = document.forms["form"]["male"].checked;
    const isFemaleChecked = document.forms["form"]["female"].checked;
    const isMarathiKnown = document.forms["form"]["marathi"].checked;
    const isGujaratiKnown = document.forms["form"]["gujarati"].checked;
    const isHindiKnown = document.forms["form"]["hindi"].checked;
    const isEnglishKnown = document.forms["form"]["english"].checked;
    const dateOfBirthString = document.forms["form"]["dateOfBirth"].value + "T00:00:00Z";
    const photo = document.forms["form"]["photo"];
    const highestQualification = document.forms["form"]["qualification"].value;
    let isFormValid = true;
    
    // full name validation
    isFormValid = validateFullName(fullName);

    // email validation
    isFormValid = validateEmail(email);

    // password validation
    isFormValid = validatePassword(password);
    
    // gender validation
    isFormValid = validateGender(isMaleChecked, isFemaleChecked);

    // language validation
    isFormValid = validateLanguage(isMarathiKnown, isGujaratiKnown, isHindiKnown, isEnglishKnown);
    // no validation for description

    // date of birth validation
    isFormValid = validateDateOfBirth(dateOfBirthString);

    // file validation
    isFormValid = validatePhoto(photo.files.length);

    // highestQualification validation
    isFormValid = validateQualification(highestQualification);

    const formDescriptor = document.getElementById("formDescriptor");
    if(isFormValid) {
        formDescriptor.textContent = "Form data submitted successfully";
        formDescriptor.style.color = "green";
        return true;
    }
    formDescriptor.textContent = "Some form content is invalid. Please check!";
    formDescriptor.style.color = "red";
    return false;
}

// form reset
function resetForm() {
    validateFullName("a");
    validateEmail("");
    validatePassword("11111111");
    validateGender(true, true);
    validateLanguage(true, true, true, true);
    validateDateOfBirth(new Date().toDateString());
    validatePhoto(1);
    validateQualification("ssc");
    const formDescriptor = document.getElementById("formDescriptor");
    formDescriptor.textContent = "";
}

// password show/hide functionality
const passwordDescriptor = document.querySelector("#password");
passwordDescriptor.addEventListener("click", (e) => {
    const password = document.getElementById("passwordInput");
    if(e.target.textContent == "show"){
        password.type = "text";
        e.target.textContent = "hide";
    } else{
        password.type = "password";
        e.target.textContent = "show";
    }
});

// Applying event listeners on input elements
const fullname = document.getElementById("fullName");
fullname.addEventListener("keyup", (e) => {
    validateFullName(e.target.value);
});

const email = document.getElementById("email");
email.addEventListener("keyup", (e) => {
    validateEmail(e.target.value);
});

const password = document.getElementById("passwordInput");
password.addEventListener("keyup", (e) => {
    validatePassword(e.target.value);
});

const genders = document.querySelectorAll('[name="gender"]');
genders.forEach((gender) => {
    gender.addEventListener("change", () => {
        validateGender(genders[0].checked, genders[1].checked);
    });
});

const languages = document.querySelectorAll('[name="language"]');
languages.forEach((language) => {
    language.addEventListener("change", () => {
        validateLanguage(languages[0].checked, languages[1].checked, languages[2].checked, languages[3].checked);
    });
});

const dateOfBirth = document.getElementById("dateOfBirth");
dateOfBirth.addEventListener("change", (e) => {
    validateDateOfBirth(e.target.value);
});

const qualification = document.getElementById("qualification");
qualification.addEventListener("change", (e) => {
    validateQualification(e.target.value);
});

const photo = document.getElementById("photo");
photo.addEventListener("change", (e) => {
    validatePhoto(e.target.files.length);
});

