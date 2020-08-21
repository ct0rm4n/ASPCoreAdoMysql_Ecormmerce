function InsertCatDao() {
    var create = document.querySelector("#Create");
    var data = new FormData(create);
    console.log(create)
    $.ajax({
        type: "Post",
        url: "../Category/Add_/",
        processData: false,
        contentType: false,
        data: data,
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