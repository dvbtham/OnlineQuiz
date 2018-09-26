(function () {
    var duration = parseInt($("#duration").text().substring(0, 2));
    var timer = duration * 60, minutes, seconds;
    var interval = setInterval(function () {

        minutes = parseInt(timer / 60, 10);
        seconds = parseInt(timer % 60, 10);

        minutes = minutes < 10 ? "0" + minutes : minutes;
        seconds = seconds < 10 ? "0" + seconds : seconds;

        $("#duration").text(minutes + ":" + seconds);

        if (--timer < 0) {
            console.log(duration);
            clearInterval(interval);
            $("input[type='radio']").attr("disabled", true);
            $(".li-prev").addClass("disabled");
            $(".li-next").addClass("disabled");
            $(".qprev").addClass("disabled");
            $(".qnext").addClass("disabled");
        }
    }, 1000);

    var saveInterval = setInterval(() => {
        save();
    }, 1000);

    $("input[type='radio']").on('change', function (e) {
        const qnavClassName = $(e.target).data('qnav');
        $("." + qnavClassName + "").addClass('highlight-question');
    });
    
    $("#content").on('click', '#complete', function (e) {
        e.preventDefault();
        swal("Bạn sẽ không được làm lại bài thi này, bạn có muốn tiếp tục?", {
            buttons: {
                cancel: "Đóng",
                agree: {
                    text: "Tiếp tục",
                    value: "agree"
                }
            }
        })
            .then((value) => {
                switch (value) {

                    case "agree":
                        save(e);
                        clearInterval(interval);
                        break;

                    default:
                        return;
                }
            });
        return false;
    });

    function save(e) {
       
        const checkedOptions = $("input[type='radio']:checked");
        var questions = [];

        $.each(checkedOptions, function (i, e) {
            questions.push($(e).val());
        });

        duration = parseInt($("#duration").text().substring(0, 2));

        const data = {
            ExamResultID: $("#ExamResultID").val(),
            IDExaminee: $("#idExaminee span").text(),
            Content: JSON.stringify(questions),
            RemainingTime: duration < 0 ? 0 : duration + 1,
            Status: false
        };

        if (e) {
            data.Status = true;
            clearInterval(saveInterval);
        }

        $.ajax({
            url: "/Test/Save",
            type: "post",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify(data),
            success: function (res) {
                if (res.url) {
                    window.location.href = res.url;
                }
            },
            error: function (res) {
                console.log(res);
                alert("Đã xảy ra lỗi. Vui lòng kiểm tra lại!");
            }
        });
    }

})();

