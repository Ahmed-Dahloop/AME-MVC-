// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
document.addEventListener("DOMContentLoaded", function () {
    const title = document.getElementById('title');
    const text = title.innerText;
    title.innerText = '';

    for (let i = 0; i < text.length; i++) {
        setTimeout(function () {
            title.style.opacity = 1; // Fade in the text
            title.innerText += text[i]; // Add one letter at a time
        }, 100 * i); // Adjust the delay as needed
    }
});