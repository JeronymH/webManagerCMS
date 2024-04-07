

// ================================================
//     GSAP
// ================================================

// reveal animations
function animateFrom(elem, direction, index) {
    direction = direction || 1;
    var x = 0,
        y = direction * 100;
    if(elem.classList.contains("gs_reveal_fromLeft")) {
      x = -100;
      y = 0;
    } else if (elem.classList.contains("gs_reveal_fromRight")) {
      x = 100;
      y = 0;
    }
    elem.style.transform = "translate(" + x + "px, " + y + "px)";
    elem.style.opacity = "0";
    gsap.fromTo(elem, {x: x, y: y, autoAlpha: 0}, {
      duration: 1.35,
      //delay: 0.08 + (0.08 * index),
      x: 0,
      y: 0, 
      autoAlpha: 1, 
      ease: "expo", 
      overwrite: "auto",
    });
  }
  function hide(elem) {
    gsap.set(elem, {autoAlpha: 0});
  }
  
  document.addEventListener("DOMContentLoaded", function() {
    gsap.registerPlugin(ScrollTrigger);

    gsap.utils.toArray(".gs_reveal").forEach(function(elem, index) {
      hide(elem); // assure that the element is hidden when scrolled into view
      ScrollTrigger.create({
        trigger: elem,
        start: "top bottom-=200px",
        scrub: 1,
        //markers: true,
        once: true,
        onEnter: function() { animateFrom(elem, "", index) }, 
        //onEnterBack: function() { animateFrom(elem, -1) },
        //onLeave: function() { hide(elem) } // assure that the element is hidden when scrolled into view
      });
    });
  });


/* COUNT ANIMATION */
let countTrigger = gsap.utils.toArray(".count");
if((typeof countTrigger !== 'undefined')) {
 
  document.addEventListener("DOMContentLoaded", function() {
    gsap.registerPlugin(ScrollTrigger);

    gsap.utils.toArray(".count").forEach(function(elem, index) {
     
      ScrollTrigger.create({
        trigger: elem,
        scrub: 1,
        once: true,
        onEnter: function() { 
            $(elem).prop('Counter',0).animate({
                Counter: $(elem).text()
            }, {
                duration: 4000,
                easing: 'swing',
                step: function (now) {
                    $(elem).text(Math.ceil(now));
                }
            })
         },
         
         
      });
    });
  });

}


/* FIXED ANCHOR TAG NAVIGATION  */
var stTrigger = document.querySelector(".toc__wrapper");
var stPin = document.querySelector(".toc__stick");

if((typeof stTrigger !== 'undefined')) {

gsap.registerPlugin(ScrollTrigger, ScrollToPlugin);
//responsive
let mm = gsap.matchMedia();

mm.add("(min-width: 992px)", () => {

  /* STICKY MENU */
  let st = ScrollTrigger.create({
    trigger: stTrigger,
    pin: stPin,
    start: "top +=40",
    end: "bottom +=800",
  });

});

/* ANCHOR NAV */
let links = gsap.utils.toArray(".toc__list a");
links.forEach((a, index)  => {
  let element = document.querySelector(a.getAttribute("href")),
      linkST = ScrollTrigger.create({
            trigger: element,
            start: "top top",
          });
          // last elemnt end is end of wrapper
          let nextElement = stTrigger;
          // endTrigger is next element
          if (index < links.length - 1) {
              nextElement = document.querySelector(links[index + 1].getAttribute("href"));
          }

  ScrollTrigger.create({
    trigger: element,
    endTrigger: nextElement,
    start: "top +=150",
    end: "bottom center",
    //markers: true,
    onToggle: self => self.isActive && setActive(a),
  });
  a.addEventListener("click", e => {
    e.preventDefault();
    gsap.to(window, {duration: 1, scrollTo: linkST.start-140, overwrite: "auto"});
  });
});

function setActive(link) {
  links.forEach(el => el.classList.remove("is-active"));
  link.classList.add("is-active");
}
}