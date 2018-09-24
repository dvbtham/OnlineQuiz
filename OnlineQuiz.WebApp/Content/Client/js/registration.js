
$("#IdentityCard").on("keyup", function () {
    $("#idCard").val($(this).val());
    populateExamineeInfo($(this).val());
});

$("#examPeriod").on('change', function (e) {
    populateExamperiod($(this).val());
});

$("#techSkills").on('change', function (e) {
    $("#TechSkillValue").val($(this).find(":selected").text());
    populateQuestionModules($(this).val());
});

$("#registration").on("click", function () {
    populateRegistrationResult();
});

$("#idCard").val($("#IdentityCard").val());
$("#TechSkillValue").val($("#techSkills").find(":selected").text());

function populateExamperiod(id) {
    $.ajax({
        url: "/Admin/Registrations/PopulateExamPeriods",
        type: "post",
        data: {
            epId: id
        },
        success: function (res) {
            $("#fromDate").text(res.startDate);
            $("#toDate").text(res.endDate);
        },
        error: function (res) {
            console.log(res);
            alert("Đã xảy ra lỗi");
        }
    });
}

function populateRegistrationResult() {
    $.ajax({
        url: "/Admin/Registrations/PopulateRegistrationResult",
        type: "post",
        data: {
            idCard: $("#IdentityCard").val().trim(),
            examPeriodId: $("#examPeriod").val(),
            techName: $("#techSkills").find(":selected").text()
        },
        success: function (res) {
            console.log(res);
            $("#regisResult").html(res.view);
        },
        error: function (res) {
            console.log(res);
            alert("Đã xảy ra lỗi");
        }
    });
}

function populateExamineeInfo(idCard) {
    $.ajax({
        url: "/Admin/Registrations/PopulateExamineeByIdCard",
        type: "post",
        data: {
            idCard: idCard
        },
        success: function (res) {
            $("#fname").val(res.data.FirstName);
            $("#lname").val(res.data.LastName);
            $("#phone").val(res.data.Mobile);
            var str = res.data.DateOfBirth;
            str = str.replace(/[^0-9\.]/g, '');
            var date = new Date(parseInt(str));
            var dob = date.toLocaleDateString("vi");

            $("#dob").attr("type", "text");
            $("#dob").val(dob);
            $("#exEmail").val(res.data.Email);
            if (res.data.Gender)
                $("#nam").attr("checked", "checked");
            else
                $("#nu").attr("checked", "checked");

            if (res.data.FirstName !== null) {
                $("#saveExaminee").text("Cập nhật thông tin");
                $("#registration").removeAttr("disabled");
                $("#register").removeAttr("disabled");
            }
            else {
                $("#saveExaminee").text("Đăng ký");
                $("#registration").attr("disabled", "disabled");
                $("#register").attr("disabled", "disabled");
                $("#regisResult").html("");
            }

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

populateExamperiod($("#examPeriod").find(":selected").val());
populateQuestionModules($("#techSkills").find(":selected").val());
populateRegistrationResult();
