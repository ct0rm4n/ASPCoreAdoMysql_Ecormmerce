function Insert() {
    $("#MeuModal").load("/Product/Add",
        function () {            
            $('#myModal').modal("show");
        });
    return [true];
}

function Edit(id) {
    $("#MeuModal").load("/Product/Edit/"+id,
        function () {
            $('#myModal').modal("show");
        });
    return [true];
}

function Remove(id) {
    $("#MeuModal").load("/Product/Remove/" + id,
        function () {
            $('#myModal').modal("show");
        });
    return [true];
}
function PromotionalOn() {
    if ($("#PromotionValue").is(':disabled')) {
        $("#PromotionValue").removeAttr('disabled')
        $("#End").removeAttr('disabled')
        $("#Begin").removeAttr('disabled')        
        console.log("turn off")
    } else {
        console.log("turn disabled")
        $("#PromocionalValue").val('');
        $("#PromocionalValue").prop("disabled", true)
        $("#End").prop("disabled", true)
        $("#Begin").prop("disabled", true)
    }
}