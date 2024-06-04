﻿
var token = localStorage.getItem("token")
var userRoles = [];
if (token == null) {

    $(".NotLogined").show();
    $(".MyListings").hide();
    $(".Logined").hide();
    $(".AdminCheck").hide();

} else {
    $(".NotLogined").hide();
    $(".Logined").show();
    $(".MyListings").show();
    var payload = parseJwt(token);
    var username = payload["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"];
    userRoles = payload["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];

    $("#UserName").html(username);
    if (userRoles.includes("Admin")) {
        $(".AdminCheck").show();
    } else {
        $(".AdminCheck").hide();
    }
}



function parseJwt(token) {
    var base64Url = token.split('.')[1];
    var base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
    var jsonPayload = decodeURIComponent(window.atob(base64).split('').map(function (c) {
        return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
    }).join(''));

    return JSON.parse(jsonPayload);

}
$("#Logout").click(function () {
    localStorage.removeItem("token");
});

