
function myFunction(){
    x=document.getElementById("mySidenav")
    y=document.getElementById("down")
    if(x.style.display=="none"){
        x.style.display="flex"
        y.style.transform="rotate(180deg)"
    }else{
        x.style.display="none"
        y.style.transform="rotate(360deg)"
    }
}

function slideright(){
  slide = document.getElementById("asd")
  slide.style.right="50%";
}


function slideleft(){
  slide = document.getElementById("asd")
  slide.style.right="0";
}


function openright(){
    let r = document.getElementById("myRightbar")
    if(r.style.right=="-100%"){
        r.style.right="0"
        document.getElementById("hide").style.display="none"
        document.getElementById("tr-right").style.transform="rotate(50deg)"
        document.getElementById("tr-right").style.position="absolute"
        document.getElementById("tr-left").style.transform="rotate(130deg)"
        document.getElementById("tr-left").style.position="absolute"
        document.getElementById("x-menu").style.justifyContent="center"
        document.getElementById("x-menu").style.backgroundColor="rgb(18, 18, 18)"
        document.body.style.overflow = 'hidden';

    }else{
        r.style.right="-100%"
        document.getElementById("hide").style.display="block"
        document.getElementById("tr-right").style.transform="rotate(0)"
        document.getElementById("tr-right").style.position="static"
        document.getElementById("tr-left").style.transform="rotate(0)"
        document.getElementById("tr-left").style.position="static"
        document.getElementById("x-menu").style.justifyContent="space-evenly"
        document.getElementById("x-menu").style.backgroundColor="#0078f2"
        document.body.style.overflow = 'visible';
    }
}

var swiper = new Swiper(".first-swipe", {
  slidesPerView: "auto",
  spaceBetween: 1,
  pagination: {
    el: ".swiper-pagination",
    clickable: true,
  },
});

var swiper = new Swiper(".second-swipe", {
    slidesPerView: "auto",
    spaceBetween: 20,
    pagination: {
      el: ".swiper-pagination",
      clickable: true,
    },
  });

// var swiper = new Swiper(".third-swipe", {
//   slidesPerView: "auto",
//   spaceBetween: 6,
//   pagination: {
//     el: ".swiper-pagination",
//     clickable: true,
//   },
// });


var swiper = new Swiper(".third-swipe", {
  slidesPerView: "auto",
  spaceBetween: 6,
  pagination: {
    el: ".swiper-pagination",
    clickable: true,
  },
});

var swiper = new Swiper(".fourth-swipe", {
  slidesPerView: "auto",
  spaceBetween: 20,
  pagination: {
    el: ".swiper-pagination",
    clickable: true,
  },
});


var swiper = new Swiper(".mySwiper3", {
  loop: true,
  spaceBetween: 10,
  slidesPerView: 0,
  freeMode: true,
  watchSlidesProgress: true
});

var swiper2 = new Swiper(".mySwiper2", {
  loop: true,
  spaceBetween: 20,
  autoplay: {
    delay: 7000,
    disableOnInteraction: false
  },
  thumbs: {
    swiper: swiper
  }
});


