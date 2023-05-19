var baseAddress = "https://localhost:7117"
var vueAPP = {
    data() {
        return {
            ProductDTOes: [],
            changeGrid: true,
            changeList: false,
            modalInfo: {},
            shoppoingitem: [],
            sum:0,
        };
    },
    mounted: function () {
        //$('body>div').
        let _this = this;
        _this.getProducts();
        _this.increaseDecreas();
    },
    methods: {
        getProducts: function () {
            let _this = this;
            axios.get(`${baseAddress}/api/Products`).then(response => {
                _this.ProductDTOes = response.data;
                var Productlist = [];
                for (var i = 0; i < _this.ProductDTOes.length; i++) {
                    var item = _this.ProductDTOes[i];
                    item.ProductPic = `./images/products/${_this.ProductDTOes[i].productPic}.jpg`;
                    Productlist.push(item);
                }
                _this.ProductDTOes = Productlist;
                //alert((_this.ProductDTOes[0]).productId);
                var CustomerId = $("#customerid").text()
                axios.get(`${baseAddress}/api/Products/${CustomerId}`).then(response => {
                    for (item of response.data) {

                        var isFavoriteSpan = $(`#${item.productId} #isFavorite`)
                        isFavoriteSpan.text("true1");
                        $(`#${item.productId} a`).css("background-color", "red");
                        $(`#${item.productId} a svg`).attr("style", "stroke:white !important");
                    }
                });
                axios.get(`${baseAddress}/api/Products/cart/${CustomerId}`).then(response => {
                    for (item of response.data) {
                         $(`#${item.productId}`).parent().next().children().text('true')
                    }
                })
                this.showShoppingCart();
            })
        },
        changelist: function () {
            this.changeList = true;
            this.changeGrid = false;
            this.getProducts();
        },
        changegrid: function () {
            this.changeList = false;
            this.changeGrid = true;
            this.getProducts();
        },
        fillmodal: function (productInfo) {
            var modalinfo = productInfo.split('|');
            this.modalInfo.productName = modalinfo[0];
            this.modalInfo.productUnitPrice = modalinfo[1];
            this.modalInfo.productPic = modalinfo[2];
            this.modalInfo.productQty = modalinfo[3];
            this.modalInfo.productId = modalinfo[4];
            axios.get(`${baseAddress}/api/Products/cart/${$('#customerid').text()}/${modalinfo[4]}`).then(response => {
                if (response.data[0] == null) {
                    this.modalInfo.productAmount = 1;
                    $(`[data-eyeisadd=${modalinfo[4]}]`).text('')

                }
                else {
                    this.modalInfo.productAmount = response.data[0].productQty;
                    alert(this.modalInfo.productAmount)
                    $(`[data-eyeisadd=${modalinfo[4]}]`).text('true')
                }
            })
        },
        addFavorite: function (productid) {
            let _this = this;
            var isFavoriteSpan = $(`#${productid} #isFavorite`)
            var isFavorite = Boolean(isFavoriteSpan.text());
            if (isFavorite) {
                $(`#${productid} a`).css("background-color", "white");
                $(`#${productid} a svg`).attr("style", "stroke:black !important");
                isFavoriteSpan.text("");
                var CustomerId = $('#customerid').text();
                axios.delete(`${baseAddress}/api/Products/${productid}/${CustomerId}`).then(response => {
                    //alert(response.data);
                })
            }
            else {
                var me = $(`#${productid} a`).css("background-color", "red");
                var me = $(`#${productid} a svg`).attr("style", "stroke:white !important");
                isFavoriteSpan.text("true");
                var CustomerId = $('#customerid').text()
                var favoriteList = {};
                favoriteList.CustomerId = $("#customerid").text();
                favoriteList.ProductId = productid;
                axios.post(`${baseAddress}/api/Products/Favorite`, favoriteList).then(response => {
                    alert(response.data);
                })
            }
        },
        addcart: function (productid) {
            let _this = this;
            //alert($(`#${productid}`).parent().next().children().text())
            var isadd = Boolean($(`#${productid}`).parent().next().children().text());
            var Unitprice = $(`#${productid}`).parent().parent().parent().next().children().eq(2).children().eq(0).text().slice(3);
            var shoppingCart = {};
            shoppingCart.ProductId = productid;
            shoppingCart.customerId = $('#customerid').text();
            shoppingCart.productUnitPrice = Unitprice;
            if (!isadd) {
                shoppingCart.qty = 1;
                $(`#${productid}`).parent().next().children().text('true')
                console.log($(`#${productid} #isadd`).text())
                axios.post(`${baseAddress}/api/Products`, shoppingCart).then(response => {
                    alert(response.data);
                    this.showShoppingCart();
                });
            }
            else {
                axios.get(`${baseAddress}/api/Products/cart/${shoppingCart.customerId}/${productid}`).then(response => {
                    shoppingCart.qty = response.data[0].productQty + 1;
                    axios.put(`${baseAddress}/api/Products/cart/${shoppingCart.customerId}/${productid}/${shoppingCart.qty}`).then(response => {
                        alert(response.data)
                        this.showShoppingCart()
                    })
                    
                })
            }
        },
        eyeaddcart: function (productid) {
            alert(productid)
            let _this = this;
            var productidarray = productid.split("|");
            var eyeisadd = Boolean($(`[data-eyeisadd=${productidarray[0]}]`).text());
            var shoppingCart = {};
            shoppingCart.ProductId = productidarray[0];
            shoppingCart.customerId = $('#customerid').text();
            shoppingCart.productUnitPrice = productidarray[1];
            shoppingCart.productQty = this.modalInfo.productAmount = $(`[data-eyeid=${productidarray[0]}]`).val();
            if (!eyeisadd) {
                $(`[data-eyeisadd=${productidarray[0]}]`).text('true')
                axios.post(`${baseAddress}/api/Products`, shoppingCart).then(response => {
                    alert(response.data);
                    this.showShoppingCart();
                });
            }
            else {
                axios.put(`${baseAddress}/api/Products/cart/${shoppingCart.customerId}/${shoppingCart.ProductId}/${shoppingCart.productQty}`).then(response => {
                    alert(response.data)
                    this.showShoppingCart()
                })
            }
        },
        showShoppingCart: function () {
            axios.get(`${baseAddress}/api/Products/cart/${$('#customerid').text()}`).then(response => {
                //alert(response.data.length);
                var newresponse = response.data;
                for (var i = 0; i < response.data.length; i++) {
                    newresponse[i].countprice = response.data[i].productUnitPrice * response.data[i].productQty;
                    newresponse[i].productPic = `./images/products/${this.ProductDTOes[i].productPic}.jpg`;
                };
                this.shoppoingitem = newresponse;
                //alert(this.shoppoingitem)
            }).then(reponse => {
                this.total();
            })
        },
        increaseDecreas: function () {
            if ($(".increaseQty").length) {
                var minVal = 1,
                    maxVal = 100;
                $(".increaseQty").on("click", function () {
                    var $parentElm = $(this).parents(".qtySelector");
                    $(this).addClass("clicked");
                    setTimeout(function () {
                        $(".clicked").removeClass("clicked");
                    }, 100);
                    var value = $parentElm.find(".qtyValue").val();
                    if (value < maxVal) {
                        value++;
                    }
                    $parentElm.find(".qtyValue").val(value);
                });
            };

            if ($(".decreaseQty").length) {
                $(".decreaseQty").on("click", function () {
                    var $parentElm = $(this).parents(".qtySelector");
                    $(this).addClass("clicked");
                    setTimeout(function () {
                        $(".clicked").removeClass("clicked");
                    }, 100);
                    var value = $parentElm.find(".qtyValue").val();
                    if (value > 1) {
                        value--;
                    }
                    $parentElm.find(".qtyValue").val(value);
                });
            };
        },
        amountchange: function (productid) {
            //alert($(`[data-id=${productid}]`).val())
            if (($(`[data-id=${productid}]`).val()) < "1") {
                $(`[data-id=${productid}]`).val("1");
            }
            axios.put(`${baseAddress}/api/Products/cart/${$('#customerid').text()}/${productid}/${$(`[data-id=${productid}]`).val()}`).then(response => {
                //alert(response.data);
                this.showShoppingCart();
            })
        },
        amountdecrease: function (productid) {
            //alert($(`[data-id=${productid}]`).val())
            if (parseInt($(`[data-id=${productid}]`).val()) < 2) {
                $(`[data-id=${productid}]`).val() = "1";
            }
            axios.put(`${baseAddress}/api/Products/cart/${$('#customerid').text()}/${productid}/${parseInt($(`[data-id=${productid}]`).val()) - 1}`).then(response => {
                //alert(response.data);
                this.showShoppingCart();
            })
        },
        amountincrease: function (productid) {
            //alert($(`[data-id=${productid}]`).val())
            axios.put(`${baseAddress}/api/Products/cart/${$('#customerid').text()}/${productid}/${parseInt($(`[data-id=${productid}]`).val()) + 1}`).then(response => {
                //alert(response.data);
                this.showShoppingCart();
            })
        },
        Eyeamountchange: function (productid) {
            //alert($(`[data-id=${productid}]`).val())
            if (($(`[data-id=${productid}]`).val()) < "1") {
                $(`[data-id=${productid}]`).val("1");
            }
            axios.put(`${baseAddress}/api/Products/cart/${$('#customerid').text()}/${productid}/${$(`[data-eyeid=${productid}]`).val()}`).then(response => {
                //alert(response.data);
                this.showShoppingCart();
                this.modalInfo.productAmount = $(`[data-eyeid=${productid}]`).val()
            })
        },
        Eyeamountdecrease: function (productid) {
            //alert($(`[data-id=${productid}]`).val())
            if (parseInt($(`[data-id=${productid}]`).val()) < 2) {
                $(`[data-id=${productid}]`).val() = "1";
                alert($(`[data-id=${productid}]`).val())
            }
            axios.put(`${baseAddress}/api/Products/cart/${$('#customerid').text()}/${productid}/${parseInt($(`[data-eyeid=${productid}]`).val()) - 1}`).then(response => {
                //alert(response.data);
                this.showShoppingCart();
                this.modalInfo.productAmount = parseInt($(`[data-eyeid=${productid}]`).val())
            })
        },
        Eyeamountincrease: function (productid) {
            //alert($(`[data-id=${productid}]`).val())
            axios.put(`${baseAddress}/api/Products/cart/${$('#customerid').text()}/${productid}/${parseInt($(`[data-eyeid=${productid}]`).val()) + 1}`).then(response => {
                //alert(response.data);
                this.showShoppingCart();
                this.modalInfo.productAmount = parseInt($(`[data-eyeid=${productid}]`).val())
            })
        },
        total: function () {
            this.sum = 0;
            for (var i = 0; i < this.shoppoingitem.length; i++) {
                this.sum += this.shoppoingitem[i].countprice
            }

        },
        productdetail: function () {
            console.log($("body>div").children())
            //$("body>div").children().eq(0).removeClass("d-none");
            //$("body>div").children().eq(1).addClass("d-none");
        }
    }
};
var app = Vue.createApp(vueAPP).mount("#app")

