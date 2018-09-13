function populateExamInfo() {
    var data = JSON.parse(localStorage.getItem("examinfo"));

    $("#credit").text("Tên chứng chỉ: " + data.CreditName);
    if (data.ModuleName === "") {
        $("#moduleName").hide();
    }
    else $("#moduleName").text("Tên module: " + data.ModuleName);

    $("#idExaminee").text("Mã thí sinh: " + data.IDExaminee);
    $("#fullName").text("Họ tên: " + data.FullName);
}

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

jQuery(function ($) {
    var dur = 60 * 20,
        display = $('.time');
    startTimer(dur, display);
});

populateExamInfo();
