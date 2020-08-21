function InsertCat() {
    $("#MeuModal").load("Category/Add",
        function () {
            $('#myModal').modal("show");
        });
    return [true];
}

function EditCat(id) {
    $("#MeuModal").load("/Category/Edit/" + id,
        function () {
            $('#myModal').modal("show");
        });
    return [true];
}

function RemoveCat(id) {
    $("#MeuModal").load("Category/Remove/" + id,
        function () {
            $('#myModal').modal("show");
        });
    return [true];
}