// Call the dataTables jQuery plugin
$(document).ready(function () {
    $('#dataTable').DataTable({
        "ordering": false,
        //"columnDefs": [{
        //    "targets": -1,
        //    "orderable": false
        //}]
    });
});

$(document).ready(function () {
    $('#dataTable1').DataTable({
        "ordering": false,
        "sorting": false,
        "paging": false,
        "searching": false,
        "info": false,
        language: {
            "zeroRecords": " "
        },

        //"columnDefs": [{
        //    "targets": -1,
        //    "orderable": false
        //}]
    });
});
