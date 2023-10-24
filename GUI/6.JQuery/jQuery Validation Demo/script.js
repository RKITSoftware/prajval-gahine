$(document).ready(function () {
  $.validator.addMethod("emailValidity", function() {

  });
  $("#form").validate({
    rules: {
        firstname: "required",
        lastname: "required",
        email: {
            required: true,
            emailValidity: true,
        }
    }
  });
});
