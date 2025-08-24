// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

let InputSearch = document.getElementById("SearchInput");

InputSearch.addEventListener("keyup", () => {
    var xhr = new XMLHttpRequest();
    xhr.open("GET", `https://localhost:44302/Employee?SearchInput=${InputSearch.value}`
, true);

    // When response is ready
    xhr.onload = function () {
        if (xhr.status === 200) {
            console.log("Response:", JSON.parse(xhr.responseText));
        } else {
            console.error("Error:", xhr.statusText);
        }
    };
    xhr.send();
})