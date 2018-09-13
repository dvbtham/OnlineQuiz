
window.formatDate = function (inputDate) {
    const date = new Date(inputDate);
    if (!isNaN(date.getTime()))
        return date.getDate() + '/' + (date.getMonth() + 1) + '/' + date.getFullYear();
}
