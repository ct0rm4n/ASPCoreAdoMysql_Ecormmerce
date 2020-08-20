function Insert() {
    $("#MeuModal").load("/api/Product/Add",
        function () {            
            $('#myModal').modal("show");
        });
    return [true];
}

function Edit(id) {
    $("#MeuModal").load("/api/Product/Edit/"+id,
        function () {
            $('#myModal').modal("show");
        });
    return [true];
}

function Remove(id) {
    $("#MeuModal").load("/api/Product/Remove/" + id,
        function () {
            $('#myModal').modal("show");
        });
    return [true];
}