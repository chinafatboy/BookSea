function supporthHtml5() {
    return (typeof(Worker) !== "undefined") ? true : false;
}
$(document).ready(function() {
    jQuery.ajaxSetup({cache:false});
});



$(document).ready(function () {

    /*
     * 计算购物车中每一个产品行的金额小计
     *
     * 参数 row 购物车表格中的行元素tr
     *
     */
    function getSubTotal(row) {
        var price = parseFloat($(row).find(".selling-price").data("bind"));
        var qty = parseInt($(row).find(":text").val());
        var result = price * qty;
        $(row).find(".selling-price").text(price.toFixed(2));;
        $(row).find(".subtotal").text(result.toFixed(2));
    };

    /*
     * 计算购物车中产品的累计金额
     */
    function getTotal() {
        var priceTotal = 0;
        $(cartTable).find("tr:gt(0)").each(function () {
            getSubTotal(this);
            if ($(this).find(":checkbox").prop("checked") == true) {
                priceTotal += parseFloat($(this).find(".subtotal").text());
            }
        });
        $("#priceTotal").text(priceTotal.toFixed(2));
    };

    var cartTable = $("#cartTable");

    getTotal();

    //为每一个勾选框指定单击事件
    $(cartTable).find(":checkbox").click(function () {
        //设置结算按钮disabled属性
        $("#btn_settlement").attr("disabled", items.find(":checkbox:checked").length == 0);

        getTotal();
    });

    //为数量调整的＋ －号提供单击事件，并重新计算产品小计
    /*
     * 为购物车中每一行绑定单击事件，以及每行中的输入框绑定键盘事件
     * 根据触发事件的元素执行不同动作
     *   增加数量
     *   减少数量
     *   删除产品
     *
     */
    $(cartTable).find("tr:gt(0)").each(function () {
        var input = $(this).find(":text");

        //为数量输入框添加事件，计算金额小计，并更新总计
        $(input).keyup(function () {
            var val = parseInt($(this).val());
            if (isNaN(val) || (val < 1)) { $(this).val("1"); }
            getSubTotal($(this).parent().parent()); //tr element
            getTotal();
        });

        //为数量调整按钮、删除添加单击事件，计算金额小计，并更新总计
        $(this).click(function () {
            var val = parseInt($(input).val());
            if (isNaN(val) || (val < 1)) { val = 1; }

            if ($(window.event.srcElement).hasClass("minus")) {
                if (val > 1) val--;
                input.val(val);
                getSubTotal(this);
            }
            else if ($(window.event.srcElement).hasClass("plus")) {
                if (val < 9999) val++;
                input.val(val);
                getSubTotal(this);
            }
            else if ($(window.event.srcElement).hasClass("delete")) {
                if (confirm("确定要从购物车中删除此产品？")) {
                    $(this).remove();
                }
            }
            getTotal();
        });
    });
});