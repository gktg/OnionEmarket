var Products = [];
var Categories = [];


$(document).ready(function () {

    GetAllProducts()
    GetAllCategories()

    $("#drdSortBy").on("change", function () {
        var selectedValue = $("#drdSortBy").val();
        if (selectedValue == 1) {
            $("#productRepeatedArea").find("div[id^='productDiv']").each(function (x, y) {

                y.remove();
            })

            Products.sortByKeyAsc("price")
            ProductRepeated(Products);

        }
        else if (selectedValue == 2) {
            $("#productRepeatedArea").find("div[id^='productDiv']").each(function (x, y) {

                y.remove();
            })

            Products.sortByKeyDesc("price")
            ProductRepeated(Products);

        }
    })

})

function GetAllProducts() {
    $.ajax({
        type: "Get",
        url: "/Products/GetAllProducts/",
        dataType: "json",
        data: null,
        async: false,
        success: function (result) {
            console.log(result)
            if (result != null) {

                Products = result;
                ProductRepeated(Products);

            }
            else {
                alertim.toast("İşlem Gerçekleşemedi", alertim.types.warning)

            }


        },
        error: function (e) {

            console.log(e);
        }
    })
}

function AddProductToBasket(productID) {
    $.ajax({
        type: "Post",
        url: "/Basket/AddProductToBasket/" + productID,
        dataType: "json",
        data: null,
        success: function (result) {
            if (result != null) {
                GetBasket()
            }
        },
        error: function (e) {

            console.log(e);
        }
    })
}


function ProductRepeated(productsList) {
    for (var x = 0; x < productsList.length; x++) {

        var html = `    <div class="col-md-3" id="productDiv-${productsList[x].id}">
                            <div class="card">
                                <div class="card-body">
                                    <img src="${productsList[x].media}" class="img-fluid"/>
<div class="row mt-3">
<div class="col-md-12">
                                    <span id="name{x}">${productsList[x].name}</span>
</div>
</div>
<div class="row mt-3">
<div class="col-md-12">
                                    <span id="price{x}">${productsList[x].price} TL</span>
</div>
</div>
<div class="row mt-3">
<div class="col-md-12">
                                    <span id="Category${x}" data-categoryID="${productsList[x].categoryID}">${productsList[x].categoryName}</span>
</div>
</div>
<div class="row mt-3">
<div class="col-md-9">
                                    <span id="stok${x}" style="vertical-align: -webkit-baseline-middle;">Stock: ${productsList[x].stock}

                                    </span>
</div>
<div class="col-md-3">
<button id="BtnFavori${productsList[x].id}" class="btn" onclick="FavoriUrunSil(${productsList[x].id})">
                                        <img id="btnFavorite${productsList[x].id}" src="/images/heartwhite.png" width="25"/>
</button>
</div>
</div>
                                    <button class="form-control mt-3  btnSepet" id="${productsList[x].id}" onclick="AddProductToBasket(this.id)">Add to Basket</button>
                                </div>
                            </div>
                        </div>`;
        $("#productRepeatedArea").append(html);
    }


}

function GetAllCategories() {
    $.ajax({
        type: "Get",
        url: "/categories/GetAllCategories/",
        dataType: "json",
        data: null,
        async: false,
        success: function (result) {
            if (result != null) {
                console.log(result)
                Categories = result;
                CategoryRepeated();

            }
            else {
                alertim.toast(siteLang.Hata, alertim.types.warning)

            }


        },
        error: function (e) {

            console.log(e);
        }
    })
}

function CategoryRepeated() {
    for (var y = 0; y < Categories.length; y++) {
        var html = `<div class="col-md-12" id="kategoriDiv${Categories[y].id}">
                <input class="mr-2" name="${Categories[y].name}" data-id="${Categories[y].id}" type="checkbox"/><span">${Categories[y].name}</span>
            </div> `;
        $("#categoryRepeatedArea").append(html);
    }
    var btn = `<div class="col-md-12 mt-3">
                <button class="form-control" onclick="Filter()">Apply Filter</button>
                <button class="form-control mt-2" onclick="FilterClear()">Clear Filters</button>
            </div> `;
    $("#categoryRepeatedArea").append(btn);

    var sortby = `<div class="col-md-12 mt-3">
                  <select class="form-control" id="drdSortBy">
                        <option value="0">Choose</option>
                        <option value="1">Price Increasing</option>
                        <option value="2">Price Descending</option>
                   </select>
            </div> `
    $("#categoryRepeatedArea").append(sortby);

}



var filterCategory = [];
function Filter() {
    filterCategory = [];
    $("#categoryRepeatedArea").find("input[type='checkbox']").each(function (x, y) {
        if ($(y).is(":checked")) {
            $(y).attr("disabled", true)
            var filter = (y).getAttribute("data-id");
            filterCategory.push((filter));

        }

    })

    if (filterCategory.length != 0) {
        GetProductByCategory()
    }
}


function GetProductByCategory() {
    $.ajax({
        type: "Post",
        url: "/products/GetProductByCategory/",
        dataType: "json",
        data: { filterCategoryList: filterCategory },
        success: function (result) {
            if (result != null) {
                $("#productRepeatedArea").find("div[id^='productDiv']").each(function (x, y) {

                    y.remove();
                })
                $("#drdSortBy").val(0).change();
                Products = result;
                ProductRepeated(Products);

            }
        },
        error: function (e) {

            console.log(e);
        }
    })
}



function FilterClear() {

    $("#productRepeatedArea").find("div[id^='productDiv']").each(function (x, y) {

        y.remove();
    })
    $("#categoryRepeatedArea").find("input[type='checkbox']").each(function (x, y) {

        $(y).prop("checked", false)
        $(y).attr("disabled", false)

    })
    $("#drdSortBy").val(0).change();

    GetAllProducts()
}


//function SepeteUrunEkle(urunID) {
//    $.ajax({
//        type: "Post",
//        url: "/emarket/SepeteUrunEkle/" + urunID,
//        dataType: "json",
//        data: null,
//        success: function (result) {
//            if (result != null) {
//                SepetiGetir()
//            }
//        },
//        error: function (e) {

//            console.log(e);
//        }
//    })
//}

//function FavoriUrunEkle(urunID) {
//    $.ajax({
//        type: "Post",
//        url: "/emarket/FavoriUrunEkle/" + urunID,
//        dataType: "json",
//        data: null,
//        success: function (result) {
//            if (result != null) {
//                $("#btnFavori" + urunID).attr("src", "/images/heartred.png")
//                $("#BtnFavori" + urunID).removeAttr("onclick")
//                $("#BtnFavori" + urunID).attr("onclick", `FavoriUrunSil(${urunID})`)
//            }
//        },
//        error: function (e) {

//            console.log(e);
//        }
//    })

//}
//function FavoriUrunSil(urunID) {
//    $.ajax({
//        type: "Post",
//        url: "/emarket/FavoriUrunSil/" + urunID,
//        dataType: "json",
//        data: null,
//        success: function (result) {
//            if (result != null) {
//                $("#btnFavori" + urunID).attr("src", "/images/heartwhite.png")
//                $("#BtnFavori" + urunID).removeAttr("onclick")
//                $("#BtnFavori" + urunID).attr("onclick", `FavoriUrunEkle(${urunID})`)

//            }
//        },
//        error: function (e) {

//            console.log(e);
//        }
//    })

//}



