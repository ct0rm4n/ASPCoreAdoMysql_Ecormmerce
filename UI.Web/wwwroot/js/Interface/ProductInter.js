﻿function Insert() {
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