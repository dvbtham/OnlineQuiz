
function populateExamperiod(id) {
    $.ajax({
        url: "/Admin/Registrations/PopulateExamPeriods",
        type: "post",
        data: {
            epId: id
        },
        success: function (res) {
            $("#examDateLabel").text(res.startDate + " - " + res.endDate);
        },
        error: function (res) {
            console.log(res);
            alert("Đã xảy ra lỗi");
        }
    });
}


function populateExamRoom(id) {
    $.ajax({
        url: "/Admin/ExaminationRooms/GetKeyValue",
        type: "post",
        data: {
            id: id
        },
        success: function (res) {
            $("#computerCount").text(res.data.Quantity);
        },
        error: function (res) {
            console.log(res);
            alert("Đã xảy ra lỗi");
        }
    });
}

function saveSchedule() {
    var data = {
        ExamPeriodID: $("#examPeriod").val(),
        ExaminationDate: $("#examDate").val(),
        StartEndTimeID: $("#statEndTime").val(), 
        QuestionModuleID: $("#modules").val(),
        ExaminationRoomID: $("#room").val(),
        InformationTechnologyID: $("#techSkills").val(),
        TechName: $("#techSkills").find(":selected").text(),
        Remark: $("#classList").val(),
        ExamineeQuantityOfRoom: parseInt($("#computerCount").text()) - parseInt($("#smdt").val())
    };

    $.ajax({
        url: "/Admin/ExamSchedules/SaveSchedule",
        type: "post",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: JSON.stringify(data),
        success: function (res) {
            console.log(res);
            alert(res.message);
        },
        error: function (res) {
            console.log(res);
            alert("Đã xảy ra lỗi");
        }
    });
}

function generateClassName() {
    $.ajax({
        url: "/Admin/ExamSchedules/GetExamClassName",
        type: "post",
        data: {
            examPeriodId: $("#examPeriod").val(),
            techId: $("#techSkills").val(),
            techName: $("#techSkills").find(":selected").text(),
            moduleId: $("#modules").val(),
            quantity: parseInt($("#computerCount").text()) - parseInt($("#smdt").val())
        },
        success: function (res) {
            if (res.data.length === 0) {
                var moduleText = $("#modules").find(":selected").text();
                var periodText = $("#examPeriod").find(":selected").text();
                alert(periodText + " chưa có thí sinh nào đăng ký module " + moduleText);
                $("#classList").html("");
                return;
            }
            var html = "";
            $.each(res.data, function (i, e) {
                html += "<option value='" + e.Key + "'>" + e.Key + " (" + e.Value + ")" + "</option>";
            });
            $("#classList").html(html);
        },
        error: function (res) {
            console.log(res);
            alert("Đã xảy ra lỗi");
        }
    });
}

function populateQuestionModules(id) {
    $.ajax({
        url: "/Admin/Registrations/PopulateQuestionModules",
        type: "post",
        data: {
            techId: id
        },
        success: function (res) {
            var html = "";

            $.each(res.data, function (i, e) {
                html += "<option value='" + e.Key + "'>" + e.Value + "</option>";
            });

            $("#modules").html(html);
        },
        error: function (res) {
            console.log(res);
            alert("Đã xảy ra lỗi");
        }
    });
}

function isValidDate(input, str1, str2) {
    str1 = str1.split("/").reverse().join("-");
    str2 = str2.split("/").reverse().join("-");

    return new Date(input) >= new Date(str1) && new Date(input) <= new Date(str2);
}

$("#examPeriod").on('change', function (e) {
    populateExamperiod($(this).val());
});

$("#techSkills").on('change', function (e) {
    populateQuestionModules($(this).val());
});

$("#createClass").on('click', function (e) {
    if ($("#smdt").val() === "") {
        alert("Chưa nhập số máy dự trữ");
        $("#smdt").focus();
        return;
    }
    
    generateClassName();
});

$("#saveSchedule").on('click', function (e) {
    var dateString = $("#examDateLabel").text();
    var fromDate = dateString.split("-")[0].trim();
    var endDate = dateString.split("-")[1].trim();
    var examDate = $("#examDate").val();
    if (!isValidDate(examDate, fromDate, endDate)) {
        $("#examDate").focus();
        alert("Ngày thi không hợp lệ!");
        return;
    }
    if ($("#smdt").val() === "") {
        alert("Chưa nhập số máy dự trữ");
        $("#smdt").focus();
        return;
    }
    if ($("#classList").val() === null || $("#classList").val() === "") {
        alert("Chưa chọn lớp");
        $("#classList").focus();
        return;
    }
    saveSchedule();
});

$("#room").on('change', function (e) {
    populateExamRoom($(this).val());
});

populateExamRoom($("#room").find(":selected").val());
populateExamperiod($("#examPeriod").find(":selected").val());
populateQuestionModules($("#techSkills").find(":selected").val());


