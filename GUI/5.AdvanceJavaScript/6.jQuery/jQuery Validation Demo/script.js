$(document).ready(function () {
  $("#form").validate({
    rules: {
        firstname: "required",
        lastname: "required",
        email: {
            required: true,
            email: true
        }
    }
  });
});
