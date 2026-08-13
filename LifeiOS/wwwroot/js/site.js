document.addEventListener("DOMContentLoaded", function () {

    const menuToggle = document.getElementById("menuToggle");
    const sidebar = document.querySelector(".sidebar");

    if (menuToggle && sidebar) {

        menuToggle.addEventListener("click", function () {

            sidebar.classList.toggle("show");

        });

    }

});
document.addEventListener("DOMContentLoaded", function () {

    const forms = document.querySelectorAll("form");
    const overlay = document.getElementById("loadingOverlay");

    forms.forEach(form => {
        form.addEventListener("submit", function () {
            overlay.classList.add("show");
        });
    });

});
document.addEventListener("DOMContentLoaded", function () {

    const forms = document.querySelectorAll("form");
    const overlay = document.getElementById("loadingOverlay");

    forms.forEach(form => {

        form.addEventListener("submit", function () {

            overlay.classList.add("show");

            const submitButtons = form.querySelectorAll('button[type="submit"]');

            submitButtons.forEach(button => {
                button.disabled = true;
            });

        });

    });

});