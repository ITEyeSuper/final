$(document).ready(function () {
    if ($("th").length > 9) {
        $('#example').DataTable({ scrollX: true })
    }
    else {
        $('#example').DataTable({ scrollX: false })
    }
});