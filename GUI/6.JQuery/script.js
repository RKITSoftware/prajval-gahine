// $(document).ready(function () {
  /*
    $("#navUpDowm").click(function() {
        $("#openerUpDowm").slideToggle(500, "linear", () => {
            console.log("toggled");
        });
    });

    $("#hamburgerLeftRight").click(function() {
        const opener = $("#openerLeftRight");
        if(opener.hasClass('hide')) {
            opener.show("slide", { direction: "left" }, 400);
            opener.removeClass('hide');
            opener.addClass('show');
        } else {
            opener.hide("slide", { direction: "left" }, 400);
            opener.remove('show');
            opener.addClass('hide');
        }
    });
    */

  // better code using jQuery.css() method
  $("#navUpDowm").click(function () {
    $("#openerUpDowm").slideToggle(500, "linear");
  });

  $("#hamburgerLeftRight").click(function () {
    const opener = $("#openerLeftRight");
    if (opener.css("display") === "none") {
      opener.show("slide", { direction: "left" }, 400);
    } else {
      opener.hide("slide", { direction: "left" }, 400);
    }
  });

  console.log($("#parent").text());
  console.log($("#parent").html());
  console.log($("#parent").val());

  let interval;
  $("#buttonStartAnimation").click(function () {
    if (interval) {
      clearInterval(interval);
      interval = undefined;
    } else {
      interval = setInterval(() => {
        $("#animate").animate({ height: "+=50px", width: "+=50px" }, 500);
        $("#animate").animate({ height: "-=50px", width: "-=50px" }, 500);
      }, 1000);
    }
  });
// });
