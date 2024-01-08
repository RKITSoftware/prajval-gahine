$("#input").keyup(function (e) {
  console.log(e.keyCode);
});

$("#input").click(function () {
  console.log("clicked");
});
$("#input").focus(function () {
  console.log("focused");
});

const e1 = jQuery.Event("keyup", { keyCode: 64 });
$("#input").trigger(e1);

const e2 = jQuery.Event("click");
$("#input").trigger(e2);

// Difference between trigger and triggerHandler
const e3 = jQuery.Event("focus");
// $("#input").trigger(e3);
$("#input").triggerHandler("focus");

// $('form[id="form1"]').submit(function(e) {
//     e.preventDefault();
//     var myName = $("#formInput");
//     if(!myName.val()) {
//         myName.after('<p>Name is required</p>');
//     }
//     console.log("pprajval");
// });

$("#form1").validate({
  rules: {
    myName: "required",
  },
  messages: {
    myName: "Name is required",
  },
//   submitHandler: function (form) {
//     form.submit();
//   },
});

// $("#form1").validate();