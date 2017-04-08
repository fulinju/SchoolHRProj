/*
文件说明：
icon选取

接口方法：
1，打开窗口方法：f_openIconsWin
2，保存下拉框ligerui对象：currentComboBox

例子：
可以这样使用(选择ICON完了以后，会把icon src保存到下拉框的inputText和valueField)：
onBeforeOpen: function ()
{
currentComboBox = this;
f_openIconsWin();
return false;
}

*/

//图标
var jiconlist, winicons, currentComboBox;
$(function () {
    jiconlist = $("div > .iconlist:first");
    if (!jiconlist.length) jiconlist = $('<ul class="iconlist"></ul>').appendTo('body');

    $(".iconlist").on('mouseover', 'li', function () {
        $(this).addClass("over");
    }).on('mouseout', 'li', function () {
        $(this).removeClass("over");
    }).on('click', 'li', function () {
        if (!winicons) return;
        var src = $("img", this).attr("src");
        currentComboBox.value = src;
        winicons.window('close');
    });
});


function f_openIconsWin() {
    if (winicons) {
        winicons.window('open');
        return;
    }
    winicons = $("#iconImg").window({
        title: '选取图标',
        target: jiconlist,
        width: 470, height: 280, modal: true
    });
    if (!jiconlist.attr("loaded")) {
        $.ajax({
            url: '/Manager/System/GetIcons',
            loading: '正在加载图标中...',
            success: function (data) {
                var s = "";
                $.each(data, function (i) {
                    s += "<li><img src='" + data[i] + "' /></li>";
                });
                jiconlist.append(s);
                jiconlist.attr("loaded", true);
            },
            dataType: 'json'
        });
    }
}