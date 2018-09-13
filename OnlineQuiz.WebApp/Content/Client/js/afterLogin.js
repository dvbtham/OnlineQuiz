(function () {

    $("#questionModule").on('change', function (e) {
        loadQuestionModuleInfo($(e.target));
    });

    function loadQuestionModuleInfo(el, mtitle) {
        var title = $(el).find(":selected").text();
        if (mtitle)
            title = mtitle;
        $.ajax({
            url: '/Examinee/AdvanceInfo?id=' + $("#examineeID").val() + "&title=" + title,
            type: 'get',
            success: function (res) {
                if (res.data) {
                    $("#examDate").html(formatDate(res.data.ExaminationDate));
                    $("#remark").html(res.data.Remark);
                }
                else {
                    $("#questionModule").html("");
                }

            },
            error: function (res) {
                console.log(res);
            }
        });
    }

    function loadQuestionModules() {
        $.ajax({
            url: '/Examinee/AdvanceModules?id=' + $("#examineeID").val(),
            type: 'get',
            success: function (res) {
                if (res.data.length > 0) {
                    var html = "";
                    $.each(res.data, function (i, e) {
                        const isSelected = i === 0 ? 'selected = "selected"' : '';
                        html += "<option " + isSelected + ">" + e.QuestionModuleName + "</option>";
                    });
                    $("#questionModule").html(html);
                    $("#questionModule").trigger('change');
                }
                else {
                    $("#questionModule").html("");
                    $("#examDate").html("");
                    $("#remark").html("");
                }

            },
            error: function (res) {
                console.log(res);
            }
        });
    }

    function loadBasicModules() {
        $.ajax({
            url: '/Examinee/BasicInfo?id=' + $("#examineeID").val(),
            type: 'get',
            success: function (res) {
                $("#questionModule").html("");
                if (res.data) {
                    $("#examDate").html(formatDate(res.data.ExaminationDate));
                    $("#remark").html(res.data.Remark);
                }
                else {
                    $("#examDate").html("");
                    $("#remark").html("");
                }

            },
            error: function (res) {
                console.log(res);
            }
        });
    }

    $("#Module").on('change', function (e) {
        const title = $(this).find(":selected").text();
        if (title === "Chuẩn kỹ năng sử dụng CNTT cơ bản") {
            $("#questionModule").attr("disabled", true);
            loadBasicModules();
        }
        else {
            $("#questionModule").attr("disabled", false);
            loadQuestionModules();
        }

    });

    function getInputData() {
        const data = {
            IDExaminee: $("#examineeID").val(),
            FullName: $("#fullname").find("input").val(),
            DOB: $("#dateOB").find("input").val(),
            Gender: $("#gender").find("select").val(),
            CMND: $("#cmnd").find("input").val()
        };
        return data;
    }

    function inputToggle() {

        $("#fullname").find("input").toggleClass("hidden");
        $("#fullname").find("label").toggleClass("hidden");

        $("#dateOB").find("input").toggleClass("hidden");
        $("#dateOB").find("label").toggleClass("hidden");

        $("#gender").find("select").toggleClass("hidden");
        $("#gender").find("label").toggleClass("hidden");

        $("#cmnd").find("input").toggleClass("hidden");
        $("#cmnd").find("label").toggleClass("hidden");
    }

    function editRequest() {
        $("#editInfo").on('click', function (e) {
            e.preventDefault();

            if ($(this).text().toLowerCase() === "Lưu lại".toLowerCase()) {

                $.ajax({
                    url: "/Examinee/EditExamineeInfo",
                    type: "post",
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    data: JSON.stringify(getInputData()),
                    success: function () {
                        localStorage.setItem("msg", "Thông tin đã được cập nhật");
                        window.location.reload();
                    },
                    error: function (res) {
                        console.log(res);
                    }
                });

            }
            else {
                inputToggle();
                $(this).text("Lưu lại");
            }
        });
    }

    function goToTest() {
        $("#test").on('click', function (e) {
            e.preventDefault();
            const data = {
                IDExaminee: $("#examineeID").val(),
                FullName: $("#fullname").find("label").text(),
                CreditName: $("#Module").find(":selected").text(),
                ModuleName: $("#questionModule").find(":selected").text()
            };
            localStorage.setItem("examinfo", JSON.stringify(data));
            console.log(data);
            window.location.href = "/Test";
        });
    }
    editRequest();
    loadBasicModules();
    goToTest();
    if (localStorage.getItem("msg")) {
        $("#alert").removeClass("hidden").html(localStorage.getItem("msg"));
        setTimeout(function () {
            $("#alert").fadeOut(100);
        }, 5000);
        localStorage.removeItem("msg");
    }
})();