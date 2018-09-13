﻿$(document).ready(function () {
    (function () {
        $('#AlertBox').removeClass('hide');
        $('#AlertBox').delay(5000).slideUp(500);
    })();

    const options = {
        'lengthMenu': [[10, 20, 30, 50, -1], [10, 20, 30, 50, "Tất cả"]],
        "paging": true,
        "searching": true,
        "ordering": false,
        "info": false,
        "autoWidth": false,
        "language": {
            sProcessing: "Đang xử lý...",
            sLengthMenu: "Xem _MENU_ mục",
            sZeroRecords: "Không tìm thấy dòng nào phù hợp",
            sInfo: "Đang xem _START_ đến _END_ trong tổng số _TOTAL_ mục",
            sInfoEmpty: "Đang xem 0 đến 0 trong tổng số 0 mục",
            sInfoFiltered: "(được lọc từ _MAX_ mục)",
            sInfoPostFix: "",
            sSearch: "Tìm:",
            sUrl: "",
            oPaginate: {
                sFirst: "Đầu",
                sPrevious: "Trước",
                sNext: "Tiếp",
                sLast: "Cuối"
            }
        }
    };
    $(".dataTable").DataTable(options);
});