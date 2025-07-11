$(function () {

    let baseUrl = "/Blog";
    const url = new URL(location.href);
    const param = new URLSearchParams(url.search);

    let categoryId = param.get("CategoryId");
    let search = param.get("Search");

    console.log(search);
    console.log(categoryId);

    let queryParts = [];

    if (categoryId) {
        queryParts.push("CategoryId=" + categoryId);
    }

    if (search) {
        queryParts.push("Search=" + search);
    }

    let finalUrl = baseUrl;
    if (queryParts.length > 0) {
        finalUrl += "?" + queryParts.join("&");
    }

    $("#blogSearchBtn").on("click", function () {
        let searchVal = $("#search").val().trim();
        let searchUrl = baseUrl;
        let newParams = [];

        if (categoryId) {
            newParams.push("CategoryId=" + categoryId);
        }

        if (searchVal !== "") {
            newParams.push("Search=" + searchVal);
        }

        if (newParams.length > 0) {
            searchUrl += "?" + newParams.join("&");
        }

        $.ajax({
            url: searchUrl,
            method: "GET",
            success: function () {
                window.location.href = searchUrl;
            }
        });
    });

});
