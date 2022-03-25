    //$.ajax({
    //    url: "",
    //    success: function (result) {
    //        var text = "";
    //        $.each(result, (function (key, val) {
    //            text += ` <tr>
    //                    <td>${val.nik}</td>
    //                    <td>${val.firstName}</td>
    //                    <td>${val.lastName}</td>
    //                    <td>${val.phone}</td>
    //                    <td>${val.birtDate}</td>
    //                    <td>${val.salary}</td>
    //                    <td>${val.email}</td>
    //                </tr>`;
    //        })
    //        )
    //        $('#tableEmployee').html(text);
    //        console.log(result);
    //    }

//const { error } = require("jquery");

    //})
function Insert() {
    var obj = new Object(); //sesuaikan sendiri nama objectnya dan beserta isinya
    //ini ngambil value dari tiap inputan di form nya
    obj.firstName = $("#firstName").val();
    obj.lastName = $("#lastName").val();
    obj.phone = $("#phone").val();
    obj.BirthDate = $("#BirthDate").val();
    obj.salary = Number($("#salary").val());
    obj.email = $("#email").val();
    obj.gender = $("#gender").val();
    obj.password = $("#password").val();
    obj.degree = $("#degree").val();
    obj.gpa = $("#gpa").val();
    obj.university_id = parseInt($("#inputUniv").val());

    console.log(obj);
    //isi dari object kalian buat sesuai dengan bentuk object yang akan di post
    $.ajax({
            headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
            url: "https://localhost:44361/API2/Accounts/register",
            type: "POST",
            dataType: "json",
            data: JSON.stringify(obj)
        }).done((result) => {
            alert("Success create new data employee");
            window.location.reload();
        //buat alert pemberitahuan jika success
        }).fail((error) => {
            alert("Fail create new data employee");
            console.log(error);
    //alert pemberitahuan jika gagal
    })
}

//tampil Univ
$.ajax({
    type: "GET",
    url: "https://localhost:44361/API2/Universities",
    data: {}
}).done((result) => {
    let univ = ``;
    $.each(result, function (index, data) {
        univ += ` <option value="${data.id}">${data.name}</option>`;
    });
    $("#inputUniv").html(univ);
}).fail((e) => {
    console.log(e);
})

//get data emp di modal
function dataUpdate(nik) {
    $.ajax({
        type: "GET",
        url: "https://localhost:44361/API2/Employees/" + nik,
        data: {}
    }).done((result) => {
        console.log(result);
       
        $("#updateNIK").html(`                               
                                <div class="controls">
                                    <input type="text" id="updateNIK" class="form-control" placeholder="${result.nik}" value="${result.nik}" disabled>
                                </div>
                            `);
        $("#updateFirstName").html(`                               
                                <div class="controls">
                                    <input type="text" id="updateFirstName" class="form-control" placeholder="${result.firstName}" value="${result.firstName}">
                                </div>
                            `);
        $("#updateLastName").html(`                               
                                <div class="controls">
                                    <input type="text" id="updateLastName" class="form-control" placeholder="${result.lastName}" value="${result.lastName}">
                                </div>
                            `);
        $("#updatePhone").html(`                               
                                <div class="controls">
                                    <input type="text" id="updatePhone" class="form-control" placeholder="${result.phone}" value="${result.phone}" disabled>
                                </div>
                            `);
        $("#updateBirthDate").html(`                               
                                <div class="controls">
                                    <input type="text" id="updateBirthDate" class="form-control" placeholder="${result.birtDate}" value="${result.birtDate}" disabled>
                                </div>
                            `);
        $("#updateSalary").html(`                               
                                <div class="controls">
                                    <input type="text" id="updateSalary" class="form-control" placeholder="${result.salary}" value="${result.salary}">
                                </div>
                            `);
        $("#updateEmail").html(`                               
                                <div class="controls">
                                    <input type="text" id="updateEmail" class="form-control" placeholder="${result.email}" value="${result.email}" disabled>
                                </div>
                            `);
        $("#udpateGender").html(`                               
                                <div class="controls">
                                    <input type="text" id="udpateGender" class="form-control" placeholder="${result.gender}" value="${result.gender}" disabled>
                                </div>
                            `);

        
    }).fail((error) => {
        console.log(error);
    });
}

function saveUpdate() {
    var obj = new Object(); //sesuaikan sendiri nama objectnya dan beserta isinya
    //ini ngambil value dari tiap inputan di form nya
    obj.nik = $("#updateNIK").val();
    obj.firstName = $("#updateFirstName").val();
    obj.lastName = $("#updateLastName").val();
    obj.phone = $("#updatePhone").val();
    obj.birtDate = ($("#updateBirthDate").val());
    obj.salary = Number($("#updateSalary").val());
    obj.email = $("#updateEmail").val();
    obj.gender = $("#udpateGender").val();

   
    console.log(obj);
    $.ajax({
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        url: "https://localhost:44361/API2/Employees/",
        type: "PUT",
        dataType: "json",
        data: JSON.stringify(obj)
    }).done((result) => {
        alert("Success Update data employee");
        window.location.reload();
        //buat alert pemberitahuan jika success
    }).fail((error) => {
        alert("Fail Update data employee");
        console.log(error);
        //alert pemberitahuan jika gagal
    });
}

function deleteEmp(nik) {
    
    Swal.fire({
        title: 'Delete This Data?',
        text: `Are you Sure Delete Data with Id ${nik}?`,
        icon: 'warning',
        showCancelButton: true,
        confirmButtonClass: '#0000FF',
        confirmButtonText: 'Yes',
        cancelButtonText: 'No'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                type: 'delete',
                url: 'https://localhost:44361/API2/Employees/'+nik,
                async: false
            }).done((deleted) => {
                Swal.fire(
                    'Deleted Employee',
                ).then((result) => {
                    window.location.reload();
                })
            }).fail((error) => {
                console.log(error);
            })
        
        }
    })
}

//edit data emloyee
//$.ajax({
//    url: "https://localhost:44361/API2/Employees/mEmployee"
//}).done((result) => {
//    console.log(result)
//    console.log(result.results);
//    var btn = "";
//    $.each(result.results, function (key, val) {
//        test += ` 
//`;
//    })
//    $('#detailPoke').html(test);


//}).fail((err) => {
//    console.log(err);
//})


////tampil Gender
//$.ajax({
//    type: "GET",
//    url: "https://localhost:44361/API2/Universities",
//    data: {}
//}).done((result) => {
//    let univ = ``;
//    $.each(result, function (index, data) {
//        univ += ` <option value="${data.id}">${data.name}</option>`;
//    });
//    $("#inputUniv").html(univ);
//}).fail((e) => {
//    console.log(e);
//})



//function showDetailEmp(url) {
//    $.ajax({
//        url: url
//    }).done((result) => {
//            console.log(result);
//    }).fail((err)=>{
//        console.log(err);
//    })
//}

$(document).ready(function () {
    var table = $("#detailEmployee_tb").DataTable({

        "ajax": {
            "url": "https://localhost:44361/API2/Employees/mEmployee",
            "dataType": "JSON",
            "dataSrc": ""
        },
        lengthMenu: [ [5, 10, 25, 50, 100, -1] , [5, 10, 25, 50, 100, "All"] ],
        info: true,
        dom: 'Bfltrip',
        buttons: [
            {
                extend: 'excelHtml5',
                text: '<i class="fa fa-file-excel-o"></i> Excel',
                titleAttr: 'Excel',
                exportOptions: {
                    columns: [0, 1, 2, 3, 4, 5, 6, 7, 8]
                }
            },
            {
                extend: 'pdfHtml5',
                text: '<i class="fa fa-file-pdf-o"></i> Pdf',
                titleAttr: 'PDF',
                orientation: 'landscape',
                pageSize: 'A4',
                download: 'open',
                exportOptions: {
                    columns: [0, 1, 2, 3, 4, 5, 6, 7, 8]
                }
            },
            {
                extend: 'print',
                text: '<i class="fa fa-print"></i> Print',
                titleAttr: 'Print',
                exportOptions: {
                    columns: [0, 1, 2, 3, 4, 5, 6, 7, 8]
                }
            }
        ],
        pagingType: "simple",

        "columns": [

            {
                "data": null, "sortable": false,
                render: function (data, type, row, meta) {
                    return meta.row + meta.settings._iDisplayStart + 1;
                }
            },
            { "data": "nik" },
            {
                "data": "fullName"

            },
            {
                "data": null,
                render: function (data, type, row) {
                    return "(+62) " + row['phone'].substring(1);
                }
            },
            { "data": "birthDate" },
            {
                "data": null,
                render: function (data, type, row) {
                    return "Rp. " + row['salary']
                }
            },
            { "data": "email" },
            { "data": "universityName" },
            { "data": "degree" },
            {
                "data": null,
                render: function (data, type, row) {
                    
                    var detailButton = `<button type="button" class="btn btn-primary btn-sm" data-toggle="modal" data-target="#detailEmployee" data-id=""><i class='fa fa-info'></i></button>&nbsp`
                    var editButton = `<button onclick="dataUpdate(${data.nik})" class="btn btn-warning btn-sm" data-toggle="modal" data-target="#editEmployee"><i class='fa fa-edit'></i> </button>&nbsp`
                    var deleteButton = `<button onclick="deleteEmp(${data.nik})" type="button" class="btn btn-danger btn-sm" data-toggle="modal" data-target="#deleteEmployee" data-id=""><i class='fa fa-trash'></i></button>&nbsp`
                    return detailButton + `` + editButton + `` + deleteButton + ``;
                }
            },

        ]
    });
    table.buttons().container()
        .appendTo('#detailEmployee_tb .col-md-6:eq(0)');
});
//$("#detailEmployee").on('show.bs.modal', function (e) {
//    console.log(e);
//    let triggerLink = $(e.relatedTarget);
//    let id = triggerLink[0].dataset['id'];


//    $("#modalTitle").text(id);
//    $(this).find(".modal-body").html("<h5>id: " + id + "</h5> + <p>+exitMessage</p>");

//});

