////let button1 = document.getElementById("button1");

////let button2 = document.getElementById("button2");

////let button3 = document.getElementById("button3");

////let konten1 = document.getElementById("konten1");

////let konten2 = document.getElementById("konten2");

////let konten3 = document.getElementById("konten3");



////button1.addEventListener('click', function () {
////    konten1.style.backgroundColor = 'red';
////})

////button2.addEventListener('click', function () {
////    konten2.innerHTML = Date();
////})

////button3.addEventListener('click', function () {
////    konten3.style.fontSize = '50px'
////})

//var text = "";
//$.ajax({
//    url: "https://swapi.dev/api/people"
//}).done((result) => {
//    console.log(result.results);
//    $.each(result.results, function (key, val) {
//        text += `<tr>
//                    <td>${key+1}</td>
//                    <td>${val.name}</td>
//                    <td>${val.gender}</td>
//                    <td>${val.height}</td>
//                    <td>${val.mass}</td>
//                    <td>${val.birth_year}</td>
//                </tr>`;
//    });
//    $("#listSW").html(text);
//}).fail((error) => {
//    console.log(error);
//});
//var text = "";
//$.ajax({
//    url: "https://pokeapi.co/api/v2/pokemon"
//}).done((result) => {
//    console.log(result.results);
//    $.each(result.results, function (key, val) {
//        text += `<tr>
//                    <td>${key + 1}</td>
//                    <td>${val.name}</td>
//                    <td><button type="button" class="btn btn-primary" data-bs-toggle="modal" data-url="${val.url}" onclick="getData('${val.url}')" data-bs-target="#exampleModal">
//  Pokemon Details
//</button></td>
//                </tr>`;
//    });
//    $("#listSW").html(text);
//}).fail((error) => {
//    console.log(error);
//});

//   if (result.types[0].type.name == "Grass" || result.types[1].type.name == "Grass")
//{
//    <span class="badge badge-success" > Grass</span>
//    }

/*<img src="https://www.w3schools.com/images/w3schools_green.jpg" alt="W3Schools.com">*/



function getDataPokemon(url) {
    $.ajax({
        url: url
    }).done((result) => {
        console.log(result);

        var img = "";
        img = `
<img  src="${result.sprites.other["official-artwork"].front_default}" alt="Gambar Pokemon">
`
        var type = "";
        for (var i = 0; i < result.types.length; i++) {
            switch (result.types[i].type.name) {
                case 'grass':
                    type += `<span class="badge badge-success"> Grass</span>`
                    break;
                case 'bug':
                    type += `<span class="badge badge-success"> Bug</span>`
                    break;
                case 'fire':
                    type += `<span class="badge badge-danger"> Fire</span>`
                    break;
                case 'poison':
                    type += `<span class="badge badge-dark"> Poison</span>`
                    break;
                case 'normal':
                    type += `<span class="badge badge-info"> Flying</span>`
                    break;
                case 'flying':
                    type += `<span class="badge badge-info"> Flying</span>`
                    break;
                case 'water':
                    type += `<span class="badge badge-primary"> Water</span>`
                    break
                case 'ice':
                    type += `<span class="badge badge-primary"> Ice</span>`
                    break;
                case 'steel':
                    type += `<span class="badge badge-secondary"> Steel</span>`
                    break;
                case 'rock':
                    type += `<span class="badge badge-secondary"> Rock</span>`
                    break;
                case 'electric':
                    type += `<span class="badge badge-warning"> Electric</span>`
                    break;
                case 'ground':
                    type += `<span class="badge badge-warning"> Ground</span>`
                    break;
                case 'dark':
                    type = `<span class="badge badge-dark"> Dark</span>`
                    break;
                default:
                    type = ``
            }
        }
        var text = "";
        text = `
            <table>
            <tr>
                <td>Name</td>
                <td>:</td>
                <td>${result.name}</td>
            </tr>
            <tr>
                <td>Abilites</td>
                <td>:</td>
                <td>${result.abilities[0].ability.name}</td>
            </tr>
            <tr>
                <td>Height</td>
                <td>:</td>
                <td>${result.height}</td>
            </tr>
<tr>
                <td>Weight</td>
                <td>:</td>
                <td>${result.weight}</td>
            </tr>
</table>
        `
        $('.modal-body').html(img + type + text);
    }).fail((error) => {
        console.log(error);
    })
        ;
}

//function getAccount(url) {
//    $.ajax({
//        url: url
//    }).done((result) => {
//        console.log(result);

//        var text = "";
//        text = `
//            <table>
//            <tr>
//                <td>ID</td>
//                <td>:</td>
//                <td>${result.account.nik}</td>
//            </tr>
//            <tr>
//                <td>Password</td>
//                <td>:</td>
//                <td>${result.account.password}</td>
//            </tr>
//</table>
//        `
//        $('.modal-body').html(img + type + text);
//    }).fail((error) => {
//        console.log(error);
//    })
//        ;
//}

function Insert() {
    var obj = new Object(); //sesuaikan sendiri nama objectnya dan beserta isinya
    //ini ngambil value dari tiap inputan di form nya
    obj.nik = $("#nik").val();
    obj.firstName = $("#firstName").val();
    obj.lastName = $("#lastName").val();
    obj.phone = $("#phone").val();
    obj.salary = $("#salary").val();
    obj.email = $("#email").val();
    obj.gender = $("#gender").val();
    obj.birthDate = $("#birthDate").val();
    //isi dari object kalian buat sesuai dengan bentuk object yang akan di post
    $.ajax({
        headers: { 'Content-Type': 'application/json' },
        url: "https://localhost:44348/API/Employees",
        type: "POST",
        data: JSON.stringify(obj),
        dataType: 'json'
    }).done((result) => {
        
        Swal.fire({
            icon: 'success',
            title: 'Success',
            text: 'Adding Employee to database',
        }),
        Table.ajax.reload()
            
    }).fail((error) => {

        Swal.fire({
            icon: 'error',
            title: 'Error',
            text: 'Failed adding Employee to Database',
        })
    })
}

function editModal(nik) {
    $.ajax({
        url: "https://localhost:44348/API/Employees/" + nik,
        success: function (result) {
            console.log(result)
            var data = result.result
            $("#employeeNik").attr("value", data.nik)
            $("#employeeFirstName").attr("value", data.firstName)
            $("#employeeLastName").attr("value", data.lastName)
            $("#employeeEmail").attr("value", data.email)
            $("#employeePhone").attr("value", data.phone)
            $("#employeeSalary").attr("value", data.salary)
            $("#employeeBirthDate").attr("value", data.birthDate)
            $("#employeeGender").attr("value", data.gender)
        },
        error: function (error) {
            console.log(error)
        }
    })
}




function Update(nik) {
    var obj = new Object(); //sesuaikan sendiri nama objectnya dan beserta isinya
    //ini ngambil value dari tiap inputan di form nya
    obj.nik = $("#employeeNik").val();
    obj.firstName = $("#employeeFirstName").val();
    obj.lastName = $("#employeeLastName").val();
    obj.phone = $("#employeePhone").val();
    obj.salary = $("#employeeSalary").val();
    obj.email = $("#employeeEmail").val();
    obj.gender = $("#employeeGender").val();
    obj.birthDate = $("#employeeBirthDate").val();
    $.ajax({
        headers: { 'Content-Type': 'application/json' },
        url: "https://localhost:44348/API/Employees/"+nik,
        type: "PUT",
        data: JSON.stringify(obj),
        dataType: 'json'
    }).done((result) => {

        Swal.fire({
            icon: 'success',
            title: 'Success',
            text: 'Adding Employee to database',
        }),
            Table.ajax.reload()

    }).fail((error) => {

        Swal.fire({
            icon: 'error',
            title: 'Error',
            text: 'Failed adding Employee to Database',
        })
    })
}

function Delete(nik) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            //isi dari object kalian buat sesuai dengan bentuk object yang akan di post
            $.ajax({
                headers: { 'Content-Type': 'application/json' },
                url: "https://localhost:44348/API/Employees/" + nik,
                type: "DELETE",
                dataType: 'json'
            }).done((result) => {
                Swal.fire({
                    icon: 'success',
                    title: 'Success',
                    text: 'Delete Employee from database',
                }),
                    Table.ajax.reload()
            }).fail((error) => {
                Swal.fire({
                    icon: 'error',
                    title: 'Error',
                    text: 'Failed deleting Employee from Database',
                })
            })
        }
    })
 
}

//$(document).ready(function () {
//    $("#customers").DataTable({
//        "ajax": {
//            "url": "https://pokeapi.co/api/v2/pokemon",
//            "dataSrc": "results"
//        },
//        "columns": [
//            { "data": "url" },
//            { "data": "name" },
//            {
//                "data": null,
//                "render": function (data, type, row) {
//                    return `<button type="button" class="btn btn-primary" data-bs-toggle="modal" onclick="getData('${row["url"]}')" data-bs-target="#exampleModal">
//                              Detail Character
//                            </button>`;
//                }
//            }
//        ]
//    });

    $(document).ready(function () {
        Table = $("#employees").DataTable({
            dom: 'Bfrtip',
            buttons: [
                'copy', 'csv', 'excel', 'pdf', 'print'
            ],
            "ajax": {
                "url": "https://localhost:44348/API/Employees",
                "dataSrc": "result"
            },
            "columns": [
                { "data": "nik" },
                {
                    "data": null,
                    "render": function (data, type, row) {
                        return row["firstName"] + " " + row["lastName"];
                    }
                },
                {
                    "data": null,
                    "render": function (data, type, row) {
                        return "+62" + row["phone"];
                    }
                },
                { "data": "birthDate" },
                {
                    "data": null,
                    "render": function (data, type, row) {
                        return "Rp" + row["salary"];
                    }
                },
                { "data": "email" },
                {
                    "data": null,
                    "render": function (data, type, row) {
                        return row["gender"] == 0 ? "Male" : "Female";
                    }
                },
                {
                    "data": null,
                    "orderable": false,
                    "render": function (data, type, row) {
                        return `<button type="button" class="fas fa-edit" data-bs-toggle="modal" onclick="editModal('${row["nik"]}')" data-bs-target="#editEmployeeModal2">
                              Edit
                            </button>`;
                    }
                },
                {
                    "data": null,
                    "orderable": false,
                    "render": function (data, type, row) {
                        return `<button type="button" class="fas fa-trash" data-bs-toggle="modal" onclick="Delete('${row["nik"]}')">
                              Delete
                            </button>`;
                    }
                }
            ],

            dom: 'Bfrtip',
            buttons: [
                'copy', 'csv', 'pdf', 'print',
                {
                    extend: 'excelHtml5',
                    text: '<i class="fa fa-file-excel-o"></i> Excel',
                    titleAttr: 'Export to Excel',
                    title: 'Employees',
                    exportOptions: {
                        columns: ':not(:last-child)',
                    }
                },
            ]

        });

    });


src = "https://cdn.jsdelivr.net/npm/jquery-validation@1.19.3/dist/jquery.validate.js"
src = "https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"