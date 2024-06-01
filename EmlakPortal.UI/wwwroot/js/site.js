

var token = localStorage.getItem("token")
var userRoles = [];
if (token == null) {

    $(".NotLogined").show();
    $(".MyListings").hide();
    $(".Logined").hide();

} else {
    $(".NotLogined").hide();
    $(".Logined").show();
    $(".MyListings").show();
    var payload = parseJwt(token);
    var username = payload["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"];
    userRoles = payload["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"];

    $("#UserName").html(username);

    console.log(userRoles);
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
    location.href = "Index";
});

$(document).ready(function () {
    toastr.options = {
        "closeButton": true,
        "debug": false,
        "newestOnTop": false,
        "progressBar": true,
        "positionClass": "toast-top-right",
        "preventDuplicates": false,
        "onclick": null,
        "showDuration": "300",
        "hideDuration": "1000",
        "timeOut": "5000",
        "extendedTimeOut": "1000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    };

    function showToastrMessage(type, message) {
        switch (type) {
            case 'success':
                toastr.success(message);
                break;
            case 'info':
                toastr.info(message);
                break;
            case 'warning':
                toastr.warning(message);
                break;
            case 'error':
                toastr.error(message);
                break;
        }
    }
});
