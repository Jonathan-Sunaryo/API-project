////console.log("Test login js berhasil");

////function Login() {
////    var obj = new Object(); //sesuaikan sendiri nama objectnya dan beserta isinya
////    //ini ngambil value dari tiap inputan di form nya
////    obj.Email = $("#user").val();
////    obj.Phone = $("#phone").val();
////    obj.Password = $("#pass").val();
////    console.log(obj);
////    $.ajax({
////        url: "/Employees/Auth",
////        type: "POST",
////        data: obj,
////        success: function (response) {
////            if (response.result.idtoken == null && response.result.statusCode == 0) {
////                console.log(response);
////                Swal.fire(
////                    'Oppss!',
////                    'Email/Phone/Password kurang tepat.',
////                    'error'
////                )
////            } else {
////                console.log(response);
////                Swal.fire({
////                    icon: 'success',
////                    title: 'Success',
////                    text: 'Login Berhasil'
                    
////                }),
////                    window.location.href = '/dashboards';
////            }


////            //Swal.fire({
////            //    title: 'Login Berhasil',
////            //    icon: 'success',
////            //    timer: 10000,
////            //    showConfirmButton: false,
////            //    allowOutsideClick: false,
////            //    didOpen: () => {
////            //        timerInterbal = setInterval(() => {
////            //            Swal.getHtmlContainer().querySelector('strong')
////            //                .textContext = (Swal.getTimerLeft() / 1000)
////            //                    .toFixed(0)
////            //        },100)

////            //        })
////        }
////    })
////}

      
////    //isi dari object kalian buat sesuai dengan bentuk object yang akan di post
////    //$.ajax({
////    //    // headers: { 'Content-Type': 'application/json' },
////    //   // url: "https://localhost:44348/API/Employees/Login",
////    //    // url: "/Login/login",
////    //    type: "POST",
////    //    data: obj,
////    //    // data: JSON.stringify(obj),
////    //   // dataType: 'json'
////    //}).done((result) => {
////    //        Swal.fire({
////    //            icon: 'success',
////    //            title: 'Success',
////    //            text: 'Success login',
////    //        }),
////    //            location.replace("https://localhost:7154/dashboards")
////    //    // else {
////    //    //    Swal.fire({
////    //    //        icon: 'error',
////    //    //        title: 'Error',
////    //    //        text: 'Failed login',
////    //    //    })
////    //    //}
////    //}).fail((error) => {

////    //    Swal.fire({
////    //        icon: 'error',
////    //        title: 'Error',
////    //        text: 'Failed login',
////    //    })
////    //})