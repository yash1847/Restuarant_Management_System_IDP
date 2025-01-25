// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// Confirm Delete Button
function Delete(url, element) {
    var message = $(element).data("message");
    if (!message) {
        message = "Once deleted, you will not be able to recover";
    }
    swal({
        title: "Are you sure?",
        text: message,
        icon: "warning",
        buttons: true,
        dangerMode: true
    }).then((willDelete) => {
        if (willDelete) {
            $.ajax({
                type: "POST",
                url: url,
                success: function (data) {
                    if (data.success) {
                        swal({
                            title: "Success!",
                            text: data.message,
                            icon: "success",
                            button: "Close"
                        }).then(() => {
                            location.reload(true);
                        });
                    }
                    else {
                        swal({
                            title: "Error!",
                            text: data.message,
                            icon: "error",
                            button: "Close"
                        });
                        //console.log(url);
                    }
                }
            });
        }
    });
}