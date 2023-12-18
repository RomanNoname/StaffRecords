window.downloadFile = function (data, fileName) {
    var link = document.createElement('a');
    link.href = data;
    link.download = fileName;
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
};