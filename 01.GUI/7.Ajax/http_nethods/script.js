// Below is illustration of GET method response
$.ajax({
  url: "https://jsonplaceholder.typicode.com/todos/",
  type: "get",
  success: function (data) {
    console.log("GET METHOD RESPONSE");
    console.log(data);
  },
  error: function(xhr, status, error){
    console.log(xhr);
    console.log(status);
    console.log(error);
  }
});

// Below is illustration of POST(using AJAX function) method response
$.ajax({
  url: "https://jsonplaceholder.typicode.com/todos/",
  type: "post",
  data: {
    userId: 201,
    id: 450,
    title: "AJAX call using jQuery",
    complete: false,
  },
  success: function (data) {
    console.log("POST METHOD RESPONSE using ajax method");
    console.log(data);
  },
  error: function(xhr, status, error){
    console.log(xhr);
    console.log(status);
    console.log(error);
  }
});

// Below is illustration of POST(using post function) method response
$.post(
  "https://jsonplaceholder.typicode.com/todos/",
  {
    userId: 202,
    id: 451,
    title: "AJAX call using jQuery post method",
    complete: false,
  },
  function (data, status) {
    console.log("POST METHOD RESPONSE using post");
    console.log(status, data);
  }
);

// Below is illustration of PUT(using ajax function) method response
$.ajax({
    url: "https://jsonplaceholder.typicode.com/todos/199",
    type: "PUT",
    data: {
        userId: 202,
        id: 199999,
        title: "AJAX call using jQuery PUT method",
        complete: false,
    },
    success: function (data) {
        console.log("PUT METHOD RESPONSE");
        console.log(data);
    },
    error: function(xhr, status, error){
      console.log(xhr);
      console.log(status);
      console.log(error);
    }
});

// Below is illustration of PATCH(using ajax function) method response
// $.ajax({
//   url: "https://jsonplaceholder.typicode.com/todos/199",
//   type: "PATCH",
//   data: {
//       userId: 2022,
//       id: 19999,
//       title: "AJAX call using jQuery PATCH method",
//       complete: false,
//   },
//   success: function (data) {
//       console.log("PATCH METHOD RESPONSE");
//       console.log(data);
//   },
//   error: function(xhr, status, error){
//     console.log(xhr);
//     console.log(status);
//     console.log(error);
//   }
// });

// Below is illustration of DELETE(using ajax function) method response
$.ajax({
    url: "https://jsonplaceholder.typicode.com/todos/2",
    type: "DELETE",
    success: function (data) {
        console.log("DELETE METHOD RESPONSE");
        console.log(data);
    },
    error: function(xhr, status, error){
      console.log(xhr);
      console.log(status);
      console.log(error);
    }
});
