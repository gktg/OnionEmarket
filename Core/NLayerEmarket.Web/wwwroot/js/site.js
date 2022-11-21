var basket = [];
var totalPrice = 0;
var basketQuantity = 0;

$(document).ready(function () {

    GetBasket()

})

function GetBasket() {
    $.ajax({
        type: "get",
        url: "/Basket/GetBasket/",
        dataType: "json",
        data: null,
        success: function (result) {
            basket = [];
            totalPrice = 0;
            basketQuantity = 0;
            if (result != null) {
                console.log(result)
                basket = result;
                $.each(basket, function (x, y) {
                    totalPrice += parseFloat(y.price)
                    basketQuantity += y.quantity
                });
                BasketRepeated()


            }
        },
        error: function (e) {

            console.log(e);
        }
    })
}




function BasketRepeated() {
    $("#basketDiv").find(".cart-detail").each(function (i, e) {
        $(e).remove()
    })
    $("#total").text(totalPrice + " TL")
    if (basketQuantity != 0) {
        $(".badge").text(basketQuantity)
    }
    else if (basketQuantity == 0) {
        $(".badge").text("")
    }
    $.each(basket, function (x, y) {
        var basketHtml = ` <div class="row cart-detail">
                                <div class="col-lg-4 col-sm-4 col-4 cart-detail-img">
                                    <img src="${y.media}">
                                </div>
                                <div class="col-lg-8 col-sm-8 col-8 cart-detail-product">
                                    <p>${y.name}</p>
                                    <span class="price text-info">${y.price} TL</span>
                                    <span class="count"> Quantity:${y.quantity}</span>
                                    <span id="${y.id}" onclick="DeleteProductFromBasket(this.id)"><i class="fas fa-trash-alt"></i></span>
                                </div>
                            </div>`;
        $("#basketDiv").append(basketHtml);

    });

}

function DeleteProductFromBasket(id) {
    $.ajax({
        type: "Post",
        url: "/basket/DeleteProductFromBasket/" + id,
        dataType: "json",
        data: null,
        success: function (result) {
            basket = [];
            totalPrice = 0;
            basketQuantity = 0;
            if (result != null) {
                basket = result;
                $.each(basket, function (x, y) {
                    totalPrice += parseFloat(y.price)
                    basketQuantity += y.quantity
                });
                BasketRepeated()

            }
        },
        error: function (e) {

            console.log(e);
        }
    })
}


function Logout() {
    $.ajax({
        type: "get",
        url: "/auth/Logout/",
        dataType: "json",
        data: null,
        success: function (result) {
            location.href = "/"
        },
        error: function (e) {

            console.log(e);
        }
    })
}