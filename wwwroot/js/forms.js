document.addEventListener("submit", function (e) {
    const form = e.target.closest("form");
    if (form) {
        sessionStorage.setItem("scrollY", window.scrollY);
    }
});

document.addEventListener("DOMContentLoaded", function () {
    const scrollY = sessionStorage.getItem("scrollY");
    if (scrollY) {
        window.scrollTo({ top: parseInt(scrollY, 10), behavior: "instant" });
        sessionStorage.removeItem("scrollY");
    }
})