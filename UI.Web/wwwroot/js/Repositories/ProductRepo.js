function Insert_Dao() {
    var create = $("#Create").serialize();
    console.log(create);
    var model = new FormData();
    model.append('file', $('#file')[0].files[0]);
    model.append('Name', $('#Name').val());
    model.append('Value', $('#Value').val());
    model.append('Description', $('#Description').val());
    model.append('CategoryId', $('#CategoryId').val());
    model.append('Stock', $('#Stock').val());
    $.ajax({
        type: "Post",
        url: "../Product/Add_/",
        processData: false,
        contentType: false,
        data: model,
        type: "post",
        dataType: "json",
        beforeSend: function (XMLHttpRequest) {
            
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            $('#result').html("<div class='alert alert-danger col-lg-10' align='left'><i class='glyphicon glyphicon-exclamation-sign'>&nbsp;</i><span>Ocorreu um erro na validação - </span>" + data.message + "</br></br></div>");
            console.log("XMLHttpRequest:" + XMLHttpRequest + "textStatus" + textStatus + "errorThrown" + errorThrown)
        },
        success: function (data, textStatus, XMLHttpRequest) {            
            if (data.success == true) {
                $('#result').html("<div class='alert alert-info col-lg-10' align='left'><i class='glyphicon glyphicon-exclamation-sign'>&nbsp;</i><span>Inserido com sucesso - </span>" + data.message + "</br></br></div>");
                setTimeout(function () {
                    window.location.reload(1);
                }, 15000);
            } else {
                $('#result').html("<div class='alert alert-danger col-lg-10' align='left'><i class='glyphicon glyphicon-exclamation-sign'>&nbsp;</i><span>Ocorreu um erro na validação - </span>" + data.message + "</br></br></div>");
            }
        },
        error: function (e) {
            alert("Erro acces API Net Core")
            console.log(e)
        }
    })
}
function Edit_Dao() {
    var create = $("#Edit").serialize();
    console.log(create);
    var formData = new FormData();
    formData.append('file', $('#file')[0].files[0]); 
    formData.append('Name', $('#Name').val());
    formData.append('Value', $('#Value').val());
    formData.append('Description', $('#Description').val());
    formData.append('ProductId', $('#ProductId').val());
    formData.append('CategoryId', $('#CategoryId').val());
    formData.append('Stock', $('#Stock').val());
    $.ajax({
        type: "Post",
        url: "../Product/Edit_/",
        processData: false,
        contentType: false,
        data: formData,
        type: "post",
        dataType: "json",
        beforeSend: function (XMLHttpRequest) {

        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            $('#result').html("<div class='alert alert-danger col-lg-10' align='left'><i class='glyphicon glyphicon-exclamation-sign'>&nbsp;</i><span>Ocorreu um erro na validação - </span>" + data.message + "</br></br></div>");
            console.log("XMLHttpRequest:" + XMLHttpRequest + "textStatus" + textStatus + "errorThrown" + errorThrown)
        },
        success: function (data, textStatus, XMLHttpRequest) {
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
        url: "../Product/Remove_/",
        processData: false,
        contentType: false,
        data: formData,
        type: "post",
        dataType: "json",
        beforeSend: function (XMLHttpRequest) {

        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            $('#result').html("<div class='alert alert-danger col-lg-10' align='left'><i class='glyphicon glyphicon-exclamation-sign'>&nbsp;</i><span>Ocorreu um erro na validação - </span>" + data.message + "</br></br></div>");
            console.log("XMLHttpRequest:" + XMLHttpRequest + "textStatus" + textStatus + "errorThrown" + errorThrown)
        },
        success: function (data, textStatus, XMLHttpRequest) {
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
function ExportToPDF() {
    var exportURL = getRootUrl() + "YourControllerName/ExportPDF?type=" + type;
    window.location.href = exportURL;
}

function getRootUrl() {
    return window.location.origin ? window.location.origin + '/' : window.location.protocol + '/' + window.location.host + '/';
} 