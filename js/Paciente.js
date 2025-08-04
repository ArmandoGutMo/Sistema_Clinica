function AddrowDT(data) {
    tabla = $("#tbl_pacientes").Datatable();
    for (var i = 0; i < datalength; i++) {
        tabla.fnAddData([
            data[i].IdPaciente,
            data[i].Nombres,
            (data[i].ApPaterno + "" + data[i].ApMaterno),
            ((data[i].Sexo == 'M') ? 'Masculino' : 'Femenino'),
            data[i].Edad,
            data[i].Direccion,
            data[i].Telefono,
            ((data[i].Estado == true) ? "Activo" : "Inactivo")
        ])
    }
}

function sendDataAjax() {
    $.ajax({
        type: "POST",
        url: "GestionarPaciente.aspx/ListarPacientes",
        data: {},
        contentType: 'application/json; charset=utf-8',

        error: function (xhr, ajaxOptions, thrownError) {
            console.log(xhr.status + " \n" + xhr.responseText, "\n" + thrownError);
        },
        success: function (data) {
            console.log(data);
            AddrowDT(data.d);
        }
    });
}

sendDataAjax;