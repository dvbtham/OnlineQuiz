(function () {
    function editRequest() {
        $("#editInfo").on('click', function (e) {
            e.preventDefault();

            if ($(this).text().toLowerCase() === "Gửi".toLowerCase()) {
                const data = {
                    IDExaminee: $("#examineeID").text(),
                    Note: $("#note").val()
                };

                $.ajax({
                    url: "/Examinee/EditExamineeInfo",
                    type: "post",
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    data: JSON.stringify(data),
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