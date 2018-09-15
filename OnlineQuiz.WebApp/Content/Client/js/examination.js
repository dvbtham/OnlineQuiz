function startTimer(duration, display) {
    var timer = duration, minutes, seconds;
    setInterval(function () {
        minutes = parseInt(timer / 60, 10);
        seconds = parseInt(timer % 60, 10);

        minutes = minutes < 10 ? "0" + minutes : minutes;
        seconds = seconds < 10 ? "0" + seconds : seconds;

        display.text(minutes + ":" + seconds);

        if (--timer < 0) {
            timer = duration;
        }
    }, 1000);
}

(function () {
    var dur = 60 * 20,
        display = $('.time');
    startTimer(dur, display);
    $(".album").removeClass("py-5");
    
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

    function save(e) {
        var link = $(e.target);
        const page = link.data("page");
        const checkedOptions = $("input[type='radio']:checked");
        var questions = [];
        $.each(checkedOptions, function (i,e) {
            questions.push($(e).val());
        });
       
        const data = {
            IDExaminee: $("#idExaminee span").text(),
            Page: page,
            Content: JSON.stringify(questions)
        };

        $.ajax({
            url: "/Test/Save",
            type: "post",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify(data),
            success: function (res) {
                $("#content").html(res.data);
            },
            error: function () {
                alert("Đã xảy ra lỗi. Vui lòng kiểm tra lại!");
            }
        });
    }

})();

