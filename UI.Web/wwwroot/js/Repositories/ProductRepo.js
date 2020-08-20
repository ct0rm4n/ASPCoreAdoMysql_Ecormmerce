function Insert_Dao() {
    var create = $("#Create").serialize();
    console.log(create);
    var formData = new FormData();
    formData.append('file', $('#file')[0].files[0]); // myFile is the input type="file" control
    formData.append('Name', $('#Name').val());
    formData.append('Value', $('#Value').val());
    formData.append('Description', $('#Description').val());
    formData.append('CategoryId', $('#CategoryId').val());
    formData.append('Stock', $('#Stock').val());
    $.ajax({
        type: "Post",
        url: "api/Product/Add/",
        data: formData,
        processData: false,  // tell jQuery not to process the data
        contentType: false,
        success: function (data) {            
            if (data.success == true) {
                $('#result').html("<div class='alert alert-info col-lg-10' align='left'><i class='glyphicon glyphicon-exclamation-sign'>&nbsp;</i><span>Inserido com sucesso - </span>" + data.message + "</br></br></div>");
                setTimeout(function () {
                    window.location.reload(1);
                }, 15000);
            } else {
                $('#result').html("<div class='alert alert-danger col-lg-10' align='left'><i class='glyphicon glyphicon-exclamation-sign'>&nbsp;</i><span>Ocorreu um erro na validação - </span>" + data.message + "</br></br></div>");
            }
        }
    })
}
function Edit_Dao() {
    var create = $("#Edit").serialize();
    console.log(create);
    var formData = new FormData();
    formData.append('file', $('#file')[0].files[0]); // myFile is the input type="file" control
    formData.append('Name', $('#Name').val());
    formData.append('Value', $('#Value').val());
    formData.append('Description', $('#Description').val());
    formData.append('ProductId', $('#ProductId').val());
    formData.append('CategoryId', $('#CategoryId').val());
    formData.append('Stock', $('#Stock').val());
    $.ajax({
        type: "Post",
        url: "api/Product/Edit/",
        data: formData,
        processData: false,  // tell jQuery not to process the data
        contentType: false,
        success: function (data) {
            if (data.success == true) {
                $('#result').html("<div class='alert alert-info col-lg-10' align='left'><i class='glyphicon glyphicon-exclamation-sign'>&nbsp;</i><span>Editado com sucesso - </span>" + data.message + "</br></br></div>");
                setTimeout(function () {
                    window.location.reload(1);
                }, 15000);
            } else {
                $('#result').html("<div class='alert alert-danger col-lg-10' align='left'><i class='glyphicon glyphicon-exclamation-sign'>&nbsp;</i><span>Ocorreu um erro na validação - </span>" + data.message + "</br></br></div>");
            }
        }
    })
}

function Remove_Dao() {
    var create = $("#Remove").serialize();
    console.log(create);
    var formData = new FormData();
    
    formData.append('Name', $('#Name').val());
    
    formData.append('Description', $('#Description').val());
    formData.append('ProductId', $('#ProductId').val());
    formData.append('CategoryId', $('#CategoryId').val());
    
    $.ajax({
        type: "Post",
        url: "pi/Product/Remove/",
        data: formData,
        processData: false,  // tell jQuery not to process the data
        contentType: false,
        success: function (data) {
            if (data.success == true) {
                $('#result').html("<div class='alert alert-info col-lg-10' align='left'><i class='glyphicon glyphicon-exclamation-sign'>&nbsp;</i><span>Removido - </span>" + data.message + "</br></br></div>");
                setTimeout(function () {
                    window.location.reload(1);
                }, 15000);
            } else {
                $('#result').html("<div class='alert alert-danger col-lg-10' align='left'><i class='glyphicon glyphicon-exclamation-sign'>&nbsp;</i><span>Ocorreu um erro na validação - </span>" + data.message + "</br></br></div>");
            }
        }
    })
}