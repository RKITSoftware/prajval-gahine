$(document).ready(function() {
    // when the document is loaded => we have to populate the table body with the bills entry from local storage
    const billsString = localStorage.getItem("bills");
    let bills;
    billsString ? bills = JSON.parse(billsString) : bills = [];

    // converting bills array into array of tr's
    const innerHTMLtrArray = $.map(bills, function(bill, index) {
        return `
            <tr class="cursor-pointer billRecord" data-billId=${bill.billId}>
                <td>${index+1}</td>
                <td>${bill.billId}</td>
                <td>${bill.companyName}</td>
                <td>${bill.totalAmount}</td>
            </tr>
        `;
    });
    // reducing "array of tr's" to "string of tr's"
    const innerHTMLString = innerHTMLtrArray.reduce(function(acc, innerHTMLtr) {
        return acc + innerHTMLtr;
    }, "");
    $("#billsTBody").html(innerHTMLString);

    // attach an event listener to tbody. We didn't attached this handller to each tr => 
    // b/c eventually it could blot up many event handler's in application's memory.
    document.getElementById("billsTBody").addEventListener("click", function(e) {
        const billId = e.target.parentElement.dataset.billid;
        const bill = bills.find(function(bill) {
            return bill.billId == billId;
        });
        const innerHTMLTrStringForBill = bill.billRecords.reduce(function(acc, item, index){
            return acc + `
            
                <tr>
                    <th>${index+1}</th>
                    <th>${item.itemCode}</th>
                    <th>${item.productName}</th>
                    <th>${item.rate}</th>
                    <th>${item.quantity}</th>
                    <th>${item.amount}</th>
                </tr>

            `;
        }, "");

        $("#billId").text(billId);
        $("#companyName").text(bill.companyName);

        $("#billRecordsTBody").html(innerHTMLTrStringForBill);
        if($("#billViewWindow").hasClass("hide")) {
            $("#billViewWindow").removeClass("hide");
        }
    });
});