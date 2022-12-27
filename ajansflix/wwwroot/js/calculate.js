
// Sepet Bölümü fixed 

$(window).scroll(function () {
    var sticky = $('#fixed'),
        scroll = $(window).scrollTop();

    if (scroll >= 300) sticky.addClass('cart-fixed');
    else sticky.removeClass('cart-fixed');
});

// Sepet Bölümü fixed 

//----------------------------------------//

var fillDataToCookieList = function (arrayList, arrayListFree) {

    var buttons = []

    if (arrayList.length > 0) {
        component = JSON.parse(arrayList);
        for (let i = 0; i < component.length; i++) {
            var button = document.getElementById("btn_class_variant_" + component[i].BaseId);
            button.classList.add("product-button-active");
            buttons.push(button);       
        }
    }
    if(arrayList.length == 0)
    {
        if (arrayListFree.length > 0) {
            component = JSON.parse(arrayListFree);

            for (let i = 0; i < component.length; i++) {
                var button = document.getElementById("btn_class_variant_" + component[i].BaseId);
                if (button != null) {
                    button.classList.add("product-button-active");
                    buttons.push(button);
                }             
            }
        }
    }
}

var fillDataCartList = function (data) {
    if (data.length > 0) {
        cart = JSON.parse(data.replace(/&quot;/g, '"'));
    }
}

//----------------------------------------//

// Sepete Ekle

var insertCart = function () {

    var cartItem = { Id: productId, Item: item, BasePrice: Number.parseInt(total.innerText), Image: imageUrl };
    const newComponent = essizKayitlar(component, 'CompName');

    const foundItem = cart.find(x => x.Id === cartItem.Id);

    $.ajax({
        type: "POST",
        url: "/hizmet/sepetekle",
        data: { cartItem: cartItem, cartData: JSON.stringify(newComponent) },
        success: function (response) {

            setTimeout(function () {
                openCart.classList.remove("noShowRelational");
                openCart.classList.add("ShowRelational");

            }, 1000);

            buttonCart.innerHTML = "Sepete Eklendi";
            buttonCart.classList.add("isDisabled");
            payButton.classList.add("showPayButton");

            if (foundItem == null) {

                totalCartCount += 1;

                if (cartCountFill != null) {
                    var countCart = Number.parseInt(cartCountFill.innerText);
                    if (countCart > 0) {
                        countCart += totalCartCount;
                        cartCountFill.innerText = countCart;
                        insertDynamicListData(newComponent, cartItem.BasePrice);

                        setTimeout(function () {
                            $(this).toggleClass('minus');
                            $(this).parent().find(".dropdown-menu").slideToggle(function () {
                                $(this).next().stop(true).toggleClass('open', $(this).is(":visible"));
                            });
                            dropdownMenus.style.display = "block";
                        }, 2000);
                        totalPriceCart.innerText = Number.parseInt(totalPriceCart.innerText) + cartItem.BasePrice;
                    }
                }
                else {
                    cartCountEmpty.innerText = Number.parseInt(totalCartCount);
                    linkCart.setAttribute('aria-expanded', 'true');
                    $(this).toggleClass('minus');
                    cartDiv.setAttribute('onclick', 'window.location.href="/sepet"');
                    $(this).parent().find(".dropdown-menu").slideToggle(function () {
                        $(this).next().stop(true).toggleClass('open', $(this).is(":visible"));
                    });
                    //    totalPriceCart.innerText = Number.parseInt(totalPriceCart.innerText) + cartItem.BasePrice;
                }
            }
        }
    });

}

function insertCartHomePage(Id) {

    var imageUrl = document.getElementById("image_" + Id).getAttribute("src");
    var total = document.getElementById("basePrice_" + Id);
    var item = document.getElementById("itemName_" + Id).innerText;
    var cartButtons = document.getElementById("cartButton_" + Id);

    var cartHome = { Id: Id, Item: item, BasePrice: Number.parseInt(total.innerText), Image: imageUrl };
    const foundItem = cart.find(x => x.Id === cartHome.Id);

    $.ajax({
        type: "POST",
        url: "/hizmet/sepetekleHome",
        data: cartHome,
        success: function (response) {
            if (response == true) {
                if (foundItem == null) {

                    totalCartCount += 1;

                    if (cartCountFill != null) {
                        var countCart = Number.parseInt(cartCountFill.innerText);
                        if (countCart > 0) {
                            countCart += totalCartCount;
                            cartCountFill.innerText = countCart;

                            setTimeout(function () {
                                $(this).toggleClass('minus');
                                $(this).parent().find(".dropdown-menu").slideToggle(function () {
                                    $(this).next().stop(true).toggleClass('open', $(this).is(":visible"));
                                });
                                dropdownMenus.style.display = "block";
                            }, 2000);
                            totalPriceCart.innerText = Number.parseInt(totalPriceCart.innerText) + cartHome.BasePrice;
                            cartButtons.classList.add("cart-active");

                            setTimeout(function () {
                                $('html, body').animate({ scrollTop: $('#cartHeader').offset().top }, 'slow');
                            }, 1000)

                            insertDynamicListDataToHome(cartHome);

                        }
                    }
                    else {
                        cartCountEmpty.innerText = Number.parseInt(totalCartCount);
                        linkCart.setAttribute('aria-expanded', 'true');
                        $(this).toggleClass('minus');
                        cartDiv.setAttribute('onclick', 'window.location.href="/sepet"');
                        $(this).parent().find(".dropdown-menu").slideToggle(function () {
                            $(this).next().stop(true).toggleClass('open', $(this).is(":visible"));
                        });
                        cartButtons.classList.add("cart-active");

                        setTimeout(function () {
                            $('html, body').animate({ scrollTop: $('#cartHeader').offset().top }, 'slow');
                        }, 1000)
                        insertDynamicListDataToHome(cartHome);
                    }
                }
                else {
                    setTimeout(function () {
                        $('html, body').animate({ scrollTop: $('#cartHeader').offset().top }, 'slow');
                    }, 100)

                    setTimeout(function () {
                        alert("Bu ürün zaten sepete eklendi!");
                    }, 1000)
                }
            }
            else {
                alert("Sepete eklenirken sorun oluştu!");
            }
            
        }
    });
}

function insertDynamicListDataToHome(cart) {

    var listEmpty = document.getElementById("empty_list");

    if (listEmpty != null) {
        listEmpty.remove();
    }

    var li = document.createElement('li');
    li.innerHTML = dynamicelementdataToHome(cart);
    document.getElementById("uniqeListPrice").appendChild(li);
}

function dynamicelementdataToHome(cart) {

    return "<li class='item-cart' id='listCartItem_" + cart.Id + "'>" +
        "<div class='product-img-wrap'>" +
        "<a href='" + cart.Image + "')'>" +
        "<img src='" + cart.Image + "' alt='" + cart.Image + "' title='" + cart.Image + "' class='img-reponsive'></a> </div>" +
        "<div class='product-details'>" +
        "<div class='inner-left'> <div class='product-name'><a style='font-size:17px;' href='/detay/" + cart.Id + "/" + cart.Item + "'>" + cart.Item + " </a></div> <div class='product-price'> <span id='price_" + cart.Id + "'>" + cart.BasePrice + "</span> TL </div>" +
        "<hr style='margin-top: 5px; margin-bottom: 5px;' /> </div></div>"+
        "<a style='cursor:pointer;' onclick='removeFromCartItem(" + cart.Id + ");' class='e-del'><i class='ion-ios-close-empty'></i></a> </li>";
}

function insertDynamicListData(newComponent, BasePriceCart) {

    var listEmpty = document.getElementById("empty_list");

    if (listEmpty != null) {
        listEmpty.remove();
    }

    var li = document.createElement('li');
    li.innerHTML = dynamicelementdata(newComponent, BasePriceCart);
    document.getElementById("uniqeListPrice").appendChild(li);

}

function dynamicelementdata(newComponent, BasePriceCart) {

    var liPlus = "";

    for (let i = 0; i < newComponent.length; i++) {
        liPlus += "<li><strong>" + newComponent[i].CompName + ":</strong> " + newComponent[i].CompValue + "</li>";
    }

    return "<li class='item-cart' id='listCartItem_" + newComponent[i].ProductId + "'>" +
        "<div class='product-img-wrap'>" +
        "<a href='" + imageUrl + "')'>" +
        "<img src='" + imageUrl + "' alt='" + imageUrl + "' title='" + imageUrl + "' class='img-reponsive'></a> </div>" +
        "<div class='product-details'>" +
        "<div class='inner-left'>" +
        "<div class='product-name'><a style='font-size:17px;' href='/detay/" + newComponent[i].ProductId + "/" + productName + "'>" + productNameReal + " </a></div>" +
        "<div class='product-price'> <span id='price_" + newComponent[i].ProductId + "'>" + BasePriceCart + "</span> TL </div>" +
        "<hr style='margin-top: 5px; margin-bottom: 5px;' />" +
        "<ul class='product-desc-list'>" + liPlus + " </ul> </div> </div>" +
        "<a style='cursor:pointer;' onclick='removeFromCartItem(" + newComponent[i].ProductId + ");' class='e-del'><i class='ion-ios-close-empty'></i></a> </li>";
}

function insertToPrice(dropdownTotal) {

    totalPrice += dropdownTotal;
    return Number.parseInt(totalPrice);
}

// Sepete Ekle

//----------------------------------------//

//Dropdown ile seçince çalışan method

function callChangefunc(val) {

    var compDetailId = val;

    $.ajax({
        type: 'GET',
        url: '/hizmet/bilesengetir?id=' + compDetailId,
        data: {},
        success: function (result) {
            let dropdown = "dropdown";
            if (Number.parseInt(result.price) > 0) {
                insertCalculate(result.price, result.compName, result.compValue, productId, compDetailId, result.compId, dropdown);
            }

            //total.innerText = insertToPrice(dropdownTotal);

        }
    });

}

//Dropdown ile seçince çalışan method

//----------------------------------------//

//Button ile seçince çalışan metod

var insertCompInValueWithButton = function (compId) {

    $('#productsTable > tbody > tr').each(function (i, row) {
        var price = $(row).find("#price_" + compId).val();
        var compName = $(row).find("#name_" + compId).val();
        var compValue = $(row).find("#value_" + compId).text();
        var detailId = $(row).find("#detail_" + compId).val();
        var baseId = compId;

        btn_variant = $(row).find("#btn_class_variant_" + compId);

        if (Number.parseInt(price) > 0) {
            insertCalculate(price, compName, compValue, productId, detailId, baseId);
        }
        if (Number.parseInt(price) == 0) {
            insertCalculate(price, compName, compValue, productId, detailId, baseId);
        }

    });

}

//Button ile seçince çalışan metod

// Tekrar eden dataları ayıklayan metot

function essizKayitlar(arr, comp) {

    const essiz = arr.map(e => e[comp]).map((e, i, final) => final.indexOf(e) === i && i)
        .filter(e => arr[e]).map(e => arr[e]);

    return essiz;
}

// Tekrar eden dataları ayıklayan metot

//-----------------------------------------

// Aynı component içindeki seçilmiş ikinci veya daha sonraki veriyi replace eden metot

$("#btn_clear").on("click", function () {
    var cartItem = { Id: productId, Item: item, BasePrice: Number.parseInt(total.innerText), Image: imageUrl };
    const newComponent = essizKayitlar(component, 'CompName');

    $.ajax({
        type: "POST",
        url: "/hizmet/secimleritemizle",
        data: { cartItem: cartItem, cartData: JSON.stringify(newComponent) },
        success: function (response) {
            window.location.href = "/detay/" + productId + "/" + productName;
        }
    });
});

function kayitdegistir(id, comp, button) {

    const componentNew = component;
    const filteredComponent = [];
    const Id = id;
    let isCompInsert = false;

    for (let i = 0; i < componentNew.length; i++) {

        var compItemNew = {};

        if (button == null) {
            compItemNew = comp;
            filteredComponent.push(comp);
            button = "";
            isCompInsert = true;

            if (componentNew[i].BaseId != comp.BaseId) {
                compItemNew = {
                    CompName: componentNew[i].CompName,
                    CompValue: componentNew[i].CompValue,
                    Price: Number.parseInt(componentNew[i].Price),
                    CompId: Number.parseInt(componentNew[i].CompId),
                    ProductId: Number.parseInt(componentNew[i].ProductId),
                    Button: button,
                    BaseId: Number.parseInt(componentNew[i].BaseId),
                };
                filteredComponent.push(compItemNew);
            }
            
        }

        else {
            compItemNew = {
                CompName: componentNew[i].CompName,
                CompValue: componentNew[i].CompValue,
                Price: Number.parseInt(componentNew[i].Price),
                CompId: Number.parseInt(componentNew[i].CompId),
                ProductId: Number.parseInt(componentNew[i].ProductId),
                Button: button,
                BaseId: Number.parseInt(componentNew[i].BaseId),
            };

            if (compItemNew.CompId !== Id) {
                if (compItemNew.Button != null && compItemNew.Button != "") {
                    compItemNew.Button.classList.add("product-button-active");
                    filteredComponent.push(compItemNew);
                }
                else {
                    filteredComponent.push(compItemNew);
                }
            }

            else {
                if (compItemNew.Button != null && compItemNew.Button != "") {
                    removeClassFromButton(compItemNew.BaseId);
                    filteredComponent.push(comp);
                    isCompInsert = true;
                }
                else {
                    filteredComponent.push(comp);
                    isCompInsert = true;
                }
            }
        }     
    }

    if (isCompInsert == false) {
        filteredComponent.push(comp);
    }

    return filteredComponent;

}

function removeClassFromButton(baseId) {
    var buttonRemove = document.getElementById("btn_class_variant_" + baseId);
    buttonRemove.classList.remove("product-button-active");
}

// Aynı component içindeki seçilmiş ikinci veya daha sonraki veriyi replace eden metot

//-----------------------------------------

// En son seçilen nesnelerin fiyat hesaplaması

function insertCalculate(price, compName, compValue, productId, compId, baseId, dropdown) {

    var totalData = 0;

    if (dropdown != "dropdown") {
        var button = document.getElementById("btn_class_variant_" + baseId);
    }

    var compItem = {
        CompName: compName,
        CompValue: compValue,
        Price: Number.parseInt(price),
        CompId: Number.parseInt(compId),
        ProductId: Number.parseInt(productId),
        Button: button,
        BaseId: Number.parseInt(baseId),
    };

    if (component.length > 0) {
        component = kayitdegistir(compItem.CompId, compItem, button);
    }
    else {
        component.push(compItem);
    }

    var newComponent = essizKayitlar(component, 'CompName');

    for (var i = 0; i < newComponent.length; i++) {
        var price = Number(newComponent[i].Price);
        totalData += price;
    }

    if (getPrice > 0) {
        totalPrice = totalPrice - getPrice;
        getPrice = totalData;
        totalPrice += getPrice;
        total.innerText = totalPrice;
    }

    else {
        getPrice = totalData;
        totalPrice += getPrice;
        total.innerText = totalPrice;
    }

    if (buttonCart.innerText == "Sepete Eklendi") {
        buttonCart.innerHTML = "Sepeti Güncelle";
        buttonCart.classList.add("btn_primary_color");
        buttonCart.classList.remove("isDisabled");
        buttonCart.setAttribute("onClick", "insertCart();");
    }

}

// En son seçilen nesnelerin fiyat hesaplaması

var removeFromCartItem = function (productId) {

    var price = document.getElementById("price_" + productId);
    var sumTotal = totalPriceCart.innerHTML;
    var cartTotal = cartCount.innerHTML;
    var sumPrice = price.innerHTML;

    $.ajax({
        type: "POST",
        url: "/hizmet/sepettencikar/",
        data: { Id: productId },
        success: function (response) {
            if (response == true) {
                dropdownMenus.style.display = "block";
                removeCartItemFromListWithHtml(productId);
                totalPriceCart.innerText = cutTheTotal(price);
                cartTotal = Number.parseInt(cartTotal) - 1;
                cartCount.innerHTML = cartTotal;
                window.location.reload();
            }
        }
    });

    function removeCartItemFromListWithHtml(Id) {
        var listId = Number.parseInt(Id);
        var li = document.getElementById('listCartItem_' + listId);
        li.remove();
    }

    function cutTheTotal(price) {
        var totalLast = sumTotal - sumPrice;
        return Number.parseInt(totalLast);
    }
}

$("#clearCart").on("click", function () {
    $.ajax({
        type: "POST",
        url: "/hizmet/sepetitemizle",
        success: function (response) {
            if (response == true)
                window.location.reload();
        }
    })
});