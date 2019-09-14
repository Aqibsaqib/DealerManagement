// For JQUERY DataTable initilzation 

$(document).ready(function () {
    $('#currentInventoryTable').DataTable({
        searching: false,
        language: {
            paginate: {
                next: '&#8594;', // or '→'
                previous: '&#8592;' // or '←' 
            }
        }
    });

    $('#soldInventoryTable').DataTable({
        searching: false,
        language: {
            paginate: {
                next: '&#8594;', // or '→'
                previous: '&#8592;' // or '←' 
            }
        }
    });

    setTimeout(function () {
        $('#myModel').modal('hide');
    }, 5000);

});
