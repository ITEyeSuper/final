jQuery(function ($) {
  'use strict';

  /****======  Sticky Menu 
   * 更多高端模板：http://www.bootstrapmb.com
   * ======*******/
  var header = $(".menu-box");
  $(window).on("scroll", function () {
    if ($(this).scrollTop() < 100) {
      header.removeClass("animated fadeInDown fixed");
    } else {
      header.addClass("animated fadeInDown fixed");
    }
  });

  /****====== Mobile Menu Js ======*******/
  $(".mobile-nav .dropdown-list .mnav-plus-minus").on("click", function () {
    $(this).parent().parent().find(".dropdown").slideToggle();
    $(this).toggleClass("active");
  });
 
  /****====== Anouncement Cross ======*******/
  $(".catbtn").on("click", function () {
    $(".catmenu").toggleClass("d-none");
  });  

  /**********************
   Global Slider 
  ***********************/ 
  $(document).ready(function () {
    var $elementCarousel = $(".globalSlider");
    $elementCarousel.each(function () {
      var appendArrow = $(this).siblings('.slick-arrow-container');
      $(this).slick({
        appendArrows: appendArrow,
        prevArrow: `<span class="slick-btn slick-prev"><i class="flaticon-back"></i></span>`,
        nextArrow: `<span class="slick-btn slick-next"><i class="flaticon-next"></i></span>`
      });
    })
  });
  

  /****====== price-range Js ======*******/
  if ($("#price-range").length) {
    $("#price-range").slider({
      range: true,
      min: 0,
      max: 800,
      values: [100, 650],
      slide: function (event, ui) {
        $("#priceRange").val("$" + ui.values[0] + " - $" + ui.values[1]);
      }
    });
    $("#priceRange").val("$" + $("#price-range").slider("values", 0) + " - $" + $("#price-range").slider("values", 1));
  };

  if ($("#price-range2").length) {
    $("#price-range2").slider({
      range: true,
      min: 0,
      max: 800,
      values: [100, 650],
      slide: function (event, ui) {
        $("#priceRange2").val("$" + ui.values[0] + " - $" + ui.values[1]);
      }
    });
    $("#priceRange2").val("$" + $("#price-range2").slider("values", 0) + " - $" + $("#price-range").slider("values", 1));
  };
 
  /* password show hide on form field  */
  if ($(".eye .icon-2").length) {
    $(".eye .icon-2").click(function () {
      var x = document.getElementById("password-field");
      if (x.type === "password") {
        x.type = "text";
      } else {
        x.type = "password";
      }
      $(this).hide();
      $(".eye .icon-1").css("display", "block");
    });
  };

  if ($(".eye .icon-1").length) {
    $(".eye .icon-1").click(function () {
      var x = document.getElementById("password-field");
      if (x.type === "text") {
        x.type = "password";
      } else {
        x.type = "text";
      }
      $(this).hide();
      $(".eye .icon-2").css("display", "block");
    });
  };

  /****======  Increase and decrease number by click  ======*******/
  if ($(".increaseQty").length) {
    var minVal = 1,
      maxVal = 20;
    $(".increaseQty").on("click", function () {
      var $parentElm = $(this).parents(".qtySelector");
      $(this).addClass("clicked");
      setTimeout(function () {
        $(".clicked").removeClass("clicked");
      }, 100);
      var value = $parentElm.find(".qtyValue").val();
      if (value < maxVal) {
        value++;
      }
      $parentElm.find(".qtyValue").val(value);
    });
  };

  if ($(".decreaseQty").length) {
    $(".decreaseQty").on("click", function () {
      var $parentElm = $(this).parents(".qtySelector");
      $(this).addClass("clicked");
      setTimeout(function () {
        $(".clicked").removeClass("clicked");
      }, 100);
      var value = $parentElm.find(".qtyValue").val();
      if (value > 1) {
        value--;
      }
      $parentElm.find(".qtyValue").val(value);
    });
  };

  /****======  Jquery tabs  ======*******/
  if ($(".tabs").length) {
    $(".tabs").tabs({
      neighbors: {
        prev: $("button.prev"),
        next: $("button.next")
      }
    });
  };

  /****======  Product Details 2 ======*******/
  if ($(".slider-for-two").length) {
    $(".slider-for-two").slick({
      slidesToShow: 1,
      slidesToScroll: 1,
      arrows: true,
      fade: true,
      prevArrow: '<button class="arrow-back"><i class="flaticon-back-1"></i> </button>',
      nextArrow: '<button class="arrow-next"><i class="flaticon-next-1"></i></button>',
      asNavFor: '.slider-nav-two',
      infinite: false
    });
  };

  if ($(".slider-nav-two").length) {
    $(".slider-nav-two").slick({
      slidesToShow: 5,
      slidesToScroll: 1,
      asNavFor: '.slider-for-two',
      dots: false,
      centerMode: false,
      focusOnSelect: true,
      arrows: true,
      prevArrow: '<button class="arrow-back"><i class="flaticon-back-1"></i> </button>',
      nextArrow: '<button class="arrow-next"><i class="flaticon-next-1"></i></button>',
      infinite: false,
      verticalSwiping: true,
      vertical: true,
      responsive: [{
          breakpoint: 1600,
          settings: {
            slidesToShow: 5,
          }
        },
        {
          breakpoint: 1399,
          settings: {
            slidesToShow: 4,
          }
        },
        {
          breakpoint: 1199,
          settings: {
            slidesToShow: 3,
          }
        },
        {
          breakpoint: 991,
          settings: {
            slidesToShow: 5,
          }
        },
        {
          breakpoint: 767,
          settings: {
            verticalSwiping: false,
            vertical: false,
            slidesToShow: 4,
          }
        }
      ]
    });
  };

  /****======  Product Details 3 ======*******/
  if ($(".slider-for-three").length) {
    $(".slider-for-three").slick({
      slidesToShow: 1,
      slidesToScroll: 1,
      arrows: true,
      fade: true,
      prevArrow: '<button class="arrow-back"><i class="flaticon-back-1"></i> </button>',
      nextArrow: '<button class="arrow-next"><i class="flaticon-next-1"></i></button>',
      asNavFor: '.slider-nav-three',
      infinite: false
    });
  };

  if ($(".slider-nav-three").length) {
    $(".slider-nav-three").slick({
      slidesToShow: 4,
      slidesToScroll: 1,
      asNavFor: '.slider-for-three',
      dots: false,
      centerMode: false,
      focusOnSelect: true,
      arrows: false,
      infinite: false,
      responsive: [{
          breakpoint: 1600,
          settings: {
            slidesToShow: 4,
          }
        },
        {
          breakpoint: 1399,
          settings: {
            slidesToShow: 4,
          }
        },
        {
          breakpoint: 1199,
          settings: {
            slidesToShow: 3,
          }
        },
        {
          breakpoint: 991,
          settings: {
            slidesToShow: 5,
          }
        },
        {
          breakpoint: 767,
          settings: {
            slidesToShow: 4,
          }
        }
      ]
    });
  };

  /****======  Zoom Product  ======*******/
  if ($(".zoom-img").length) {
    $(".zoom-img").zoom();
  }; 

  /****====== Video Activition Js ======*******/
  if ($(".popupVideo").length) {
    $('.popupVideo').magnificPopup({
      type: 'iframe'
    });
  }; 

  /****====== CountDown Timer One ======*******/
  if ($(".time-countdown").length) {
    $(".time-countdown").each(function () {
      var Self = $(this);
      var countDate = Self.data("countdown-time"); // getting date
      Self.countdown(countDate, function (event) {
        $(this).html(' <li> <div class="box"> <span class="days">' + event.strftime('%D') + '</span> <span class="timeRef">days</span> </div> </li> <li>  <div class="box"> <span class="hours">' + event.strftime('%H') + '</span> <span class="timeRef clr-1">hours</span>  </div>  </li><li> <div class="box"> <span class="minutes">' + event.strftime('%M') + '</span> <span class="timeRef clr-2">min</span>  </div>  </li> <li> <div class="box"> <span class="seconds">' + event.strftime('%S') + '</span> <span class="timeRef clr-3">sec</span>  </div> </li>');
      });
    });
  }; 

  /****======  Bottom to Top Scroll Js  ======*******/
  var ScrollTop = $(".scrollToTop");
  $(window).on("scroll", function () {
    if ($(this).scrollTop() < 100) {
      ScrollTop.removeClass("active");
    } else {
      ScrollTop.addClass("active");
    }
  });

  $(".scrollToTop").on("click", function () {
    $("html, body").animate({
      scrollTop: 0
    }, 250);
    return false;
  });

  $(window).on("load", function () {
    /****======  Newslettser Modal  ======*******/
    //$("#newsletter-modal").modal("show");

    /****======  Newslettser Modal  ======*******/
    //$(".slider-nav-two").resize();
    //$(".slider-nav-two").slick("refresh"); 

  });

}(jQuery));