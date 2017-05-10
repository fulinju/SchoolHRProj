$(document).ready(function () {
    funcGetLeftDownload();
    funcGetRightDownload();
});

//获取左边的下载
function funcGetLeftDownload() {
    $.ajax({
        type: "get",
        dataType: "json",
        url: 'http://123.57.26.127/api/Download/GetDownloadList?pageIndex=1&pageSize=20&dmTypeValue=政策法规下载',
        data: {},
        success: function (data) {
            funcInitLeftDownload(data)
        }, error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert("未能获取下载列表");
        }
    });
}

//初始化左边下载
function funcInitLeftDownload(leftData) {
    var leftUL = $('#leftUL');
    for(var i=0;i <leftData.resultList.length;i++){
        leftUL.append('<li><a href="' + leftData.resultList[i].dmFileURL + '" target="_blank">' + leftData.resultList[i].dmTitle + '</a></li>');
    }
}

//获取右边的下载
function funcGetRightDownload() {
    $.ajax({
        type: "get",
        dataType: "json",
        url: 'http://123.57.26.127/api/Download/GetDownloadList?pageIndex=1&pageSize=20&dmTypeValue=HR表格下载',
        data: {},
        success: function (data) {
            funcInitRightDownload(data)
        }, error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert("未能获取下载列表");
        }
    });
}

//初始化右边下载
function funcInitRightDownload(rightData) {
    var rightUL = $('#rightUL');
    for (var i = 0; i < rightData.resultList.length; i++) {
        rightUL.append('<li><a href="' + rightData.resultList[i].dmFileURL + '" target="_blank">' + rightData.resultList[i].dmTitle + '</a></li>');
    }
}
