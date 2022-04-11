let dark = document.getElementById("btn-toggle")
let up = document.getElementById("up")


dark.onclick = function(){
    document.body.classList.toggle("dark-theme")
}


window.onscroll = function() {scrollFunction()};

function scrollFunction() {
  if (document.body.scrollTop > 800 || document.documentElement.scrollTop > 800) {
    up.style.cssText = "color :var(--main-color)";
  } 
  else{
    up.style.cssText = "color :transparent";
  }
}
// function topFunction() {
//     document.documentElement.scrollTop = 0; // For Chrome, Firefox, IE and Opera
//   }

// console.log(dark)