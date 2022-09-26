var LoginVm = {};

$(document).ready(function () {

    var count = 2;
    $(".fa-lock").on("click", function () {
        count++;
        if (count % 2 === 0) {
            $("#sifre").attr("type", "password");

        }
        else if (count % 2 === 1) {

            $("#sifre").attr("type", "text");
        }
    })

})

function Login() {
    LoginVm = {}


    LoginVm["Mail"] = $("#email").val();
    LoginVm["Password"] = $("#sifre").val();

    model = LoginVm;
    $.ajax({
        type: "POST",
        url: "/auth/LoginControl/",
        dataType: "json",
        data: model,
        success: function (result) {
            if (result == true) {
                alertim.toast("Giriş Başarılı", alertim.types.success);
                setTimeout(function () {

                    window.location.href = "/products/Products";

                }, 1000);
            }
            else {
                alertim.toast("Lütfen Şifre ve Mail'inizi Kontrol Ediniz", alertim.types.warning);

            }

        },
        error: function (e) {

            console.log(e);
            errorHandler(e);
        }
    });
}

$(".container-fluid").on('keydown', 'input', function (e) {
    if (e.keyCode === 13) {
        Login()
    }
});


