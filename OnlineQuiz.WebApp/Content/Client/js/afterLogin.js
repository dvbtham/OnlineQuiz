(function () {
    $("#cancel").on('click', function (e) {
        e.preventDefault();
        $("#editInfo").removeClass("mt-10").text("Yêu cầu sửa");
        $(this).toggleClass("hidden");
        $("#editRequest").addClass("hidden");
    });
    function editRequest() {
        $("#editInfo").on('click', function (e) {
            e.preventDefault();

            if ($(this).text().toLowerCase() === "Gửi".toLowerCase()) {
                const data = {
                    IDExaminee: $("#examineeID").text(),
                    Note: $("#editRequest").val()
                };

                $.ajax({
                    url: "/Examinee/EditExamineeInfo",
                    type: "post",
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    data: JSON.stringify(data),
                    success: function (res) {
                        $("#alert").removeClass("hidden").text(res.message);
                        $("#editRequest").addClass("hidden");
                        $("#editInfo").text("Yêu cầu sửa");
                        setTimeout(function () {
                            $("#alert").fadeOut();
                        }, 5000);
                    },
                    error: function (res) {
                        console.log(res);
                    }
                });

            }
            else {
                $("#cancel").removeClass("hidden");
                $("#editInfo").addClass("mt-10");
                $("#editRequest").removeClass("hidden");
                $(this).text("Gửi");
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
    goToTest();

})();