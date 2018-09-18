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
    $(".album").removeClass("mt-20");

    //$("#content").on('change', '.question-item input', function () {
    //    var data = $(this).val();
    //    var qid = data.substr(2, data.length);

    //    if (questions.filter(x => x.substr(2, data.length) === qid).length === 0) {
    //        questions.push(data);
    //    }
    //    else {
    //        for (var i = questions.length - 1; i >= 0; i--) {
    //            if (questions[i].substr(2, questions[i].length) === data.substr(2, data.length)) {
    //                questions.splice(i, 1);
    //                questions.push(data);
    //            }
    //        }
    //    }
    //    console.log(data);
    //    localStorage.setItem("questions", JSON.stringify(questions));
    //});

    $("#content").on('click', '.qnext', function (e) {
        e.preventDefault();
        save(e);
        return false;
    });

    $("#content").on('click', '.qprev', function (e) {
        e.preventDefault();
        save(e);
        return false;
    });

    $("#content").on('click', '#complete', function (e) {
        e.preventDefault();
        if (!confirm("Bạn sẽ không được làm lại bài thi này, bạn có muốn tiếp tục?")) return;
        save(e);
        clearInterval(interval);
        return false;
    });

    function save(e) {
        var link = $(e.target);
        const page = link.data("page");
        const checkedOptions = $("input[type='radio']:checked");
        var questions = [];
        $.each(checkedOptions, function (i, e) {
            questions.push($(e).val());
        });
        duration = parseInt($("#duration").text().substring(0, 2));
        const data = {
            ExamResultID: $("#ExamResultID").val(),
            IDExaminee: $("#idExaminee span").text(),
            Page: page,
            Content: JSON.stringify(questions),
            RemainingTime: duration < 0 ? 0 : duration

        };

        console.log(data);

        $.ajax({
            url: "/Test/Save",
            type: "post",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify(data),
            success: function (res) {
                //if (res.isRenderHtml)
                    $("#content").html(res.data);
               
            },
            error: function () {
                alert("Đã xảy ra lỗi. Vui lòng kiểm tra lại!");
            }
        });
    }

})();

