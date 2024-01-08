
const itemCode = $("#itemCode");
const productName = $("#productName");
const rate = $("#rate");
const quantity = $("#quantity");
const amount = $("#amount");

const gstNo = $("#gstNo");
const companyName = $("#companyName");
const billId = $("#billId");
const billDate = $("#billDate");
const billAddress = $("#billAddress");
const billSubTotal = $("#billSubTotal");
const gstPercentage = $("#gstPercentage");
const totalAmount = $("#billTotalAmount");


// creating a mapping for index and value
const mappingForGSTIN = [
    '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 
    'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'
];
// 33AIOPC3903A1ZZ
// an algorithm for checksum of gstin
function validateHash(str) {
    // taking last digit which is checksum into checkSum variable
    const checkSum = str[str.length-1];
    // getting corresponding index code
    let checkSumCodeFromStr = mappingForGSTIN.indexOf(checkSum);
    let multiplier = 1;
    let hashSum = 0;
    for(let i = 0; i<str.length-1; i++) {
        const code = mappingForGSTIN.indexOf(str[i]);
        const product = code * multiplier;
        multiplier == 1 ? multiplier = 2 : multiplier = 1;
        const hash = Math.floor(product/36) + product%36;
        hashSum += hash;
    }
    const remainderOfHashSum = hashSum%36;
    const checkSumCode = (36 - remainderOfHashSum)%36;

    if(checkSumCodeFromStr === checkSumCode) {
        return true;
    }
    return false;
}

// algorithm for pan validity
function isValidPAN(pan) {
    const panType = ['T', 'F', 'H', 'P', 'C'];
    const number = +pan.slice(5, 9);
    if(!pan.slice(0, 3).match(/[A-Z]{3}/)?.length) {
        return false;
    }
    if(!panType.includes(pan[3])) {
        return false;
    }
    if(!pan[4].match(/[A-Z]/)?.length) {
        return false;
    }
    if(isNaN(number)) {
        return false;
    }
    // the tenth character (index 9) is checksum and it's algorithm is not publically available for security reason.
    return true;
}

// algorithm for gstin validity
function isValidGSTIN(gstin) {
    // gstin must be of 15 length long
    if(gstin.length != 15) {
        return false;
    }
    // validate whether the first 2 digits are number which ressembles a state code
    const stateCode = +gstin.slice(0, 2);
    if(!(stateCode && (stateCode>=1 && stateCode<=38 || stateCode==98))) {
        return false;
    }
    // validating the next 10 character string is PAN or not
    if(!isValidPAN(gstin.slice(2, 12))) {
        return false;
    }
    // checking if char at 12 index is a digit or not
    const entityNumber = +gstin[12];
    if(isNaN(entityNumber)) {
        return false;
    }
    // char at 13 index is bydefault Z
    if(gstin[13] != 'Z') {
        return false;
    }
    // performing checksum over gstin
    return validateHash(gstin);
}

// jQuery validator for gstNo input field to validate gstNo starts with 24 and is of 15 characters long 

$.validator.addMethod("validGSTNo", function(value, element) {
    return isValidGSTIN(value.toUpperCase());
}, "Enter valid GST No.");

// $.validator.addMethod("validGSTNo", function(value, element) {
//      return value.startsWith("24") && value.length === 15;
// }, "Enter valid GST No.");

// jQuery validator for billId to validate billId is of 4 characters long
$.validator.addMethod("length4", function(value, element) {
    return value.length === 4;
}, "Bill No. should be 4 numbers long");

// jQuery validator for rate to validate if the rate is +ve
$.validator.addMethod("positiveRate", function(value, element) {
    return Number(value) >= 0;
}, "Rate cannot be negative");

// jQuery validator for quantity to validate if the quantity is +ve
$.validator.addMethod("positiveQuantity", function(value, element) {
    return Number(value) > 0;
}, "Quantity cannot be less than 1");

// jQuery validator for gstPercentage to validate if the gstPercentage is +ve
$.validator.addMethod("positiveGSTPercentage", function(value, element) {
    return Number(value) >= 0;
}, "GST Pecentage cannot be negative");

// recordsUpdator() - function to update the tbody view using the billRecords data in the session storage.
function recordsUpdator() {
    const sessionBillRecords = JSON.parse(sessionStorage.getItem("billRecords")) || [];
    const sessionCustomerDetail = JSON.parse(sessionStorage.getItem("customerDetail")) || {};
    if(!sessionCustomerDetail.billSubTotal) {
        sessionCustomerDetail.billSubTotal = "";
    }
    $("#billSubTotal").val(sessionCustomerDetail.billSubTotal);

    if(!sessionCustomerDetail.totalAmount) {
        sessionCustomerDetail.totalAmount = "";
    }
    $("#billTotalAmount").val(sessionCustomerDetail.totalAmount);

    $("tbody").empty();
    sessionBillRecords.forEach((record, index) => {
        $("tbody").append(`
            <tr>
                <th scope="row">${index+1}</th>
                <td>${record.itemCode}</td>
                <td>${record.productName}</td>
                <td>${record.rate}</td>
                <td>${record.quantity}</td>
                <td>${record.amount}</td>
                <td><button class="deleteButton" data-index="${index}"><i class="fa-solid fa-square-xmark"></i></button></td>
            </tr>
        `);
    });
    attackClickEventToDeleteButton();
}

// customerDetailUpdator() is used to update input field of customer detail section
function customerDetailUpdator() {
    const customerDetailString = sessionStorage.getItem("customerDetail");

    if(customerDetailString) {
        const customerDetail = JSON.parse(customerDetailString);
        gstNo.val(customerDetail.gstNo);
        companyName.val(customerDetail.companyName);
        billId.val(billNo);
        billDate.val(customerDetail.billDate);
        billAddress.val(customerDetail.billAddress);
        billSubTotal.val(customerDetail.billSubTotal);
        gstPercentage.val(customerDetail.gstPercentage);
        totalAmount.val(customerDetail.totalAmount);
    } else {
        billId.val(billNo);
        billDate.val(new Date().toISOString().split("T")[0]);
    }
}

// when a bill is saved then all input field on the page need to be reset hence pageInputResetter() function resets all input field
function pageInputResetter() {

    const tbody = $("tbody");

    gstNo.val("");
    companyName.val("");
    billId.val(localStorage.getItem("billNo"));
    billDate.val(new Date().toISOString().split("T")[0])
    billAddress.val("");
    billSubTotal.val("");
    gstPercentage.val(0);
    totalAmount.val("");
    tbody.html("");

    itemCode.val("");
    productName.val("");
    rate.val("");
    quantity.val("");
    amount.val("");

    sessionStorage.removeItem("billRecords");
    sessionStorage.removeItem("customerDetail");
}

function attackClickEventToDeleteButton() {
    $(".deleteButton").on("click", function(e) {
        const index = Number($(this).attr("data-index"));
        if(index >= 0) {
            const billRecords = JSON.parse(sessionStorage.getItem("billRecords")) || [];
            const record = billRecords[index];
            billRecords.splice(index, 1);
            sessionStorage.setItem("billRecords", JSON.stringify(billRecords));
            const sessionCustomerDetail =JSON.parse(sessionStorage.getItem("customerDetail"));
            sessionCustomerDetail.billSubTotal = sessionCustomerDetail.billSubTotal - record.amount;
            sessionCustomerDetail.totalAmount = sessionCustomerDetail.billSubTotal * sessionCustomerDetail.gstPercentage / 100 + sessionCustomerDetail.billSubTotal;
            sessionStorage.setItem("customerDetail", JSON.stringify(sessionCustomerDetail));
            $("#billSubTotal").val(Number($("#billSubTotal").val()) - Number(record.amount));
            recordsUpdator();
        }
    });
}

// a function to make an ajax call for populating the nav list
function populateNavBar() {
    $.ajax({
        method: "get",
        url: "./navbar.json",
        dataType: "json",
        success: function(result) {
            const innerHTMLString = result.reduce(function(string, item) {
                return string + `<a href=${item.src}><li class="navbar-item h5 bill-nav-item">${item.value}</li></a>`;
            }, "");
            $("#navBarList").html(innerHTMLString);
        },
        error: function(xhr, status, errorString) {
            console.error(status);
        }
    });
}

let billNo = localStorage.getItem("billNo") ? Number(localStorage.getItem("billNo")) : 1001;

$(document).ready(function() {

    // responsivness - adding onclick event listener to hamburger => which triggers customer-detail section to slide left to right(open effect)
    $("#hamburger").on("click", function() {
        const customerDetail = $(".customer-detail");
        customerDetail.show("slide", {direction: "left"}, 400, function() {
            customerDetail.removeAttr("style");
        });
        customerDetail.removeClass('hide');
        customerDetail.addClass('show');
    });
    // responsivness - adding onclick event listener to cross => which triggers customer-detail section to slide right to left(close effect)
    $("#cross").on("click", function() {
        const customerDetail = $(".customer-detail");
        customerDetail.hide("slide", {direction: "left"}, 400, function() {
            customerDetail.removeAttr("style");
        });
        customerDetail.removeClass('show');
        customerDetail.addClass('hide');
    });

    // making an ajax request to fill up the navbar lists
    populateNavBar();

    // updating the ui with the data present in session storage
    customerDetailUpdator();
    recordsUpdator();

    // adding event listener on billForm
    $("#billForm").on("submit", function(e) {
        e.preventDefault();
    });
    // using external js/jQuery library for validating the form
    $("#billForm").validate({
        errorClass: "errors",
        rules: {
            gstNo: {
                required: true,
                validGSTNo: true
            },
            companyName: {
                required: true
            },
            billDate: {
                required: true
            },
            gstPercentage: {
                required: true,
                number: true,
                positiveGSTPercentage: true
            }
        },
        submitHandler: function() {
            const billRecords = JSON.parse(sessionStorage.getItem("billRecords"));
            const customerDetail = JSON.parse(sessionStorage.getItem("customerDetail"));
            let bills = [];
            if(localStorage.getItem("bills")) {
                bills = JSON.parse(localStorage.getItem("bills"));
            }
            bills.push({...customerDetail, billRecords});
            localStorage.setItem("bills", JSON.stringify(bills));
            billNo++;
            localStorage.setItem("billNo", billNo);
            pageInputResetter();
        }
    });

    // adding submit event listener on addItemForm
    $("#addItemsForm").on("submit", function(e) {
        e.preventDefault();
    });
    // using external js/jQuery library for validating the form
    $("#addItemsForm").validate({
        errorClass: "errors",
        rules: {
            itemCode: {
                required: true,
                number: true,
                maxlength: 4
            },
            productName: {
                required: true,
            },
            rate: {
                required: true,
                number: true,
                positiveRate: true,
            },
            quantity: {
                required: true,
                number: true,
                positiveQuantity: true
            },
        },
        submitHandler: function() {

            const billSubTotalValue = billSubTotal.val();
            billSubTotal.val(Number(billSubTotalValue) + Number(amount.val()));

            const gstPercentageValue = Number(gstPercentage.val());

            let customerDetail;
            if(sessionStorage.getItem("customerDetail")) {
                customerDetail = JSON.parse(sessionStorage.getItem("customerDetail"));
                const newSubTotalValue = Number(customerDetail.billSubTotal) + Number(amount.val());
                const newTotalAmountValue = newSubTotalValue * gstPercentageValue / 100 + newSubTotalValue; 

                customerDetail = {
                    gstNo: gstNo.val(),
                    companyName: companyName.val(),
                    billId: billNo,
                    billDate: billDate.val(),
                    billAddress: billAddress.val(),
                    billSubTotal: newSubTotalValue,
                    gstPercentage: gstPercentageValue,
                    totalAmount:  newTotalAmountValue
                };
            } else{
                customerDetail = {
                    gstNo: gstNo.val(),
                    companyName: companyName.val(),
                    billId: billNo,
                    billDate: billDate.val(),
                    billAddress: billAddress.val(),
                    billSubTotal: Number(amount.val()),
                    gstPercentage: gstPercentageValue,
                    totalAmount: Number(amount.val()) * gstPercentageValue / 100 + Number(amount.val())
                };
            }

            sessionStorage.setItem("customerDetail", JSON.stringify(customerDetail));

            let billRecords;
            if(sessionStorage.getItem("billRecords")) {
                billRecords = JSON.parse(sessionStorage.getItem("billRecords"));
            } else {
                billRecords = [];
            }
            billRecords.push({
                itemCode: itemCode.val(),
                productName: productName.val(),
                rate: rate.val(),
                quantity: quantity.val(),
                amount: amount.val(),
            });

            sessionStorage.setItem("billRecords", JSON.stringify(billRecords));

            // updating the ui, since we had added an item into the bill record.
            customerDetailUpdator();
            // And there might be chances the we have updated the customer detail section => so refreshing that ui portion
            recordsUpdator();

            // reseting the inputs of addItemForm
            itemCode.val("");
            productName.val("");
            rate.val("");
            quantity.val("");
            amount.val("");
        }
    });

    // additing kepup event listener for #rate and #quantity to auto fill #amount input field
    $("#rate, #quantity").on("keyup", function() {
        const rate = $("#rate");
        const quantity = $("#quantity")
        $("#amount").val(Number(rate.val()) * Number(quantity.val()));
    });
});