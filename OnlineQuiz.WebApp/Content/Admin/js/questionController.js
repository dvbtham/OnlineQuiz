$(document).ready(function () {
    $(document).on("keyup", function (e) {
        if (e.keyCode === 27) {
            const parent = $("tbody tr[enabling='true']");
            clickEvent(e, parent);
            saveDone();
        }
    });
    $("#btnImport").on('click', function (e) {
        e.preventDefault();
        $("#btnSaveImport").removeClass('hide');
        $("#btnCancel").removeClass('hide');
        $("#btnAdd").addClass('hide');
        $("#importedQuestionFile")
            .fadeIn(160, function () {
                $(this).removeClass('hide');
            });
        $(this).addClass('hide');
    });

    $("#btnSaveImport").on('click', function (e) {
        e.preventDefault();
        $("#importForm").submit();
    });

    $("#btnCancel").on('click', function (e) {
        e.preventDefault();
        window.location.reload();
    });

    $(".dataTable").on("click", ".btn-save", function (e) {
        e.preventDefault();
        $("tbody tr").attr("enabling", "false");
        clickEvent(e);
        saveEdit(e);
    });

    $(".dataTable").on("click", ".edit-row", function (e) {
        e.preventDefault();
        clickEvent(e);
    });

    function inputData(e) {
        e.preventDefault();
        var parent = $(e.target).parent().parent().parent();
        const data = {
            ID: $(parent).data("id"),
            QuestionContent: $(parent).find(".questionContent").val().trim(),
            AAnswer: $(parent).find(".aAnswer").val(),
            BAnswer: $(parent).find(".bAnswer").val(),
            CAnswer: $(parent).find(".cAnswer").val(),
            DAnswer: $(parent).find(".dAnswer").val(),
            Answer: $(parent).find(".answer").val(),
            ResultAnswer: $(parent).find(".resultAnswer").val()
        };
        
        return data;
    }

    function saveDone() {
        $("tbody tr").removeAttr("enabling");
        $.each($("tbody tr"), function (i, e) {
            if ($(this).attr("enabling") !== "true") {
                $(e).find(".fa-edit").removeClass("hide");
                $(e).find(".btn-save").addClass("hide");
            }
        });
    }

    function saveEdit(e) {
        e.preventDefault();
        saveDone();
        $.ajax({
            url: "/Admin/Question/SaveEdit",
            type: "post",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify(inputData(e)),
            success: function (res) {
                if (!res.status) {
                    console.log(res.stackTrace);
                    alert("Cập nhật không thành công.");
                }
                else {
                    var parent = $(e.target).parent().parent().parent();
                    $(parent).addClass("success");
                    setTimeout(() => {
                        $(parent).removeClass("success");
                    }, 2000);
                }

            },
            error: function () {
                alert("Đã xảy ra lỗi. Vui lòng kiểm tra lại!");
            }
        });
       
    }

    function clickEvent(e, mparent) {
        e.preventDefault();

        var parent = $(e.target).parent().parent().parent();
        if (mparent)
            parent = mparent;
        $(parent).attr("enabling", "true");
        $(parent).find(".fa-edit").toggleClass("hide");
        
        $(".module").toggleClass("hide");
        $(".level").toggleClass("hide");
        $(parent).find(".btn-save").toggleClass("hide");
       
        
        $(".pagination").toggleClass("hide");
        $.each($("tbody tr"), function (i, e) {
            if ($(this).attr("enabling") !== "true") {
                $(e).find(".fa-edit").addClass("hide");
            }
        });
        $(e).find(".fa-edit").removeClass("fa-edit").addClass("fa-save");
        $(parent).find(".editor").toggleClass("hide");
        $(parent).find(".text-show").toggleClass("hide");
    }
});