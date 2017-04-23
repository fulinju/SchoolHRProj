//封装的EasyUI通用方法
var grid;

//$(function () {
//    $("#btnSearch").click(function () {
//        grid.datagrid({ queryParams: form2Json("searchForm") });
//    });
//    //$(".validate").form('disableValidation');
//    $("#btnReload").on("click", reload);
//});

function SingleUpload(repath, uppath, iswater, isThumbnail) {
    var url = "/upload/singleupload.html";
    var submitUrl = url + "?ReFilePath=" + repath + "&UpFilePath=" + uppath;
    //判断是否打水印
    if (arguments.length == 3) {
        submitUrl = url + "?ReFilePath=" + repath + "&UpFilePath=" + uppath + "&IsWater=" + iswater;
    } if (arguments.length == 4) {
        submitUrl = stringFormat(url + "?ReFilePath={0}&UpFilePath={1}&IsWater={2}&IsThumbnail={3}", repath, uppath, iswater, isThumbnail);
    }
    //开始提交
    $(".validate").ajaxSubmit({
        beforeSubmit: function (formData, jqForm, options) {
            $("#" + repath).nextAll(".files").eq(0).hide();
            $("#" + repath).nextAll(".uploading").eq(0).show();
        },
        success: function (data, textStatus) {
            if (data.state == 1) {
                $("#" + repath).val(data.message);
            } else {
                $.messager.alert('错误', data.message);
            }
            $("#" + repath).nextAll(".files").eq(0).show();
            $("#" + repath).nextAll(".uploading").eq(0).hide();

        },
        error: function (data, status, e) {
            alert("上传失败，错误信息：" + e);
            $("#" + repath).nextAll(".files").eq(0).show();
            $("#" + repath).nextAll(".uploading").eq(0).hide();
        },
        url: submitUrl,
        type: "post",
        dataType: "json",
        timeout: 600000
    });
    return false;
};


//将表单数据转为json
function form2Json(id) {
    var arr = $("#" + id).serializeArray();
    var jsonStr = "";

    jsonStr += '{';
    for (var i = 0; i < arr.length; i++) {
        jsonStr += '"' + arr[i].name + '":"' + arr[i].value + '",';
    }
    jsonStr = jsonStr.substring(0, (jsonStr.length - 1));
    jsonStr += '}';
    var json = JSON.parse(jsonStr);
    return json;
}

//function reload() {
//    grid.datagrid("reload");
//}


function Reload(dgid) {
    $("#" + dgid).datagrid("reload");
}

function Search(dgid, formid) {
    $("#" + dgid).datagrid({ queryParams: form2Json(formid) });
}

//添加的页面
function Add(dgid, title, url, width, height, btnText) {
    var buttons = null;
    if (btnText != "-1") {
        buttons = [{
            text: btnText || '保 存',
            iconCls: 'ext-icon-accept',
            handler: function () {
                dialog.find('iframe').get(0).contentWindow.submitForm(dialog, $("#" + dgid), parent.$);
            }
        }, {
            text: '取 消',
            iconCls: 'ext-icon-cancel',
            handler: function () {
                dialog.dialog('destroy');
            }
        }];
    }
    var dialog = parent.sy.modalDialog({
        title: "&nbsp;" + title,
        iconCls: 'icon-edit',
        url: url,
        width: width || 800,
        height: height || 600,
        resizable: true,
        maximizable: true,
        onClose: function () {
            $("#" + dgid).datagrid('load').datagrid('clearSelections').datagrid('clearChecked');
            dialog.dialog('destroy');
        },
        buttons: buttons
    });
}


function submitForm($dialog, $grid, $pjq) {
    var form = $('.validate');
    form.validInput(function (r) {
        if (r === false) return;
        else {
            form.form('submit', {
                success: function (data) {
                    var json = eval('(' + data + ')');
                    if (json.state == "1") {
                        $.messager.alert('提示', json.message, "success", function () {
                            $grid.datagrid('reload').datagrid('clearSelections').datagrid('clearChecked');
                            $dialog.dialog('destroy');
                            try {
                                $grid.treegrid('reload').treegrid('clearSelections').treegrid('clearChecked');
                            } catch (e) { }

                        });
                    } else {
                        $pjq.messager.alert('错误', json.message, 'error');
                    }
                }
            });
        }
    });
}


function Delete(dgid, url) {
    var row = $("#" + dgid).datagrid("getSelections");
    if (row.length < 1) {
        parent.$.messager.alert("提示!", "请选择一条记录!", "info");
        return;
    } else {
        parent.$.messager.confirm('提示', '确定要删除?', function (r) {
            if (r) {
                var chosen = JSON.stringify(row);
                console.log(chosen)
                $.post(url, { model: chosen }, function (data) {
                    if (data.state == "1") {
                        $("#" + dgid).datagrid('reload').datagrid('clearSelections').datagrid('clearChecked');
                        try {
                            $("#" + dgid).treegrid('reload').treegrid('clearSelections').treegrid('clearChecked');
                        } catch (e) { }
                    } else {
                        $.messager.alert('错误', data.message);
                    }
                }, 'json');
            }
        });
    }
}


//重构Edit
function Edit(dgid, title, url, width, height, btnText) {
    var buttons = null;
    if (btnText != "-1") {
        buttons = [{
            id: 'editSubmit',
            text: btnText || '保 存',
            iconCls: 'ext-icon-accept',
            handler: function () {
                dialog.find('iframe').get(0).contentWindow.submitForm(dialog, $("#" + dgid), parent.$);
            }
        }, {
            text: '取 消',
            iconCls: 'ext-icon-cancel',
            handler: function () {
                dialog.dialog('destroy');
            }
        }];
    }
    var rows = $("#" + dgid).datagrid("getSelections");


    if (rows.length != 1) {
        parent.$.messager.alert("提示!", "请选择一条记录!", "info");
        return;
    }

    //var id = getId();
    var id = rows[0].pkId; //获取pkId (怎么改写成通用的？暂时把主键都用pkId表示),pkId也传入
    //var id = getId(dgid);
    console.log(dgid);
    console.log(id);
    console.log(rows.length);
    if (id) {
        var dialog = parent.sy.modalDialog({
            title: "&nbsp;" + title,
            iconCls: 'icon-edit',
            url: url + "?id=" + id,
            width: width || 700,
            height: height || 500,
            resizable: true,
            maximizable:true,
            onClose: function () {
                $("#" + dgid).datagrid("reload").datagrid('clearSelections').datagrid('clearChecked');
            },
            buttons: buttons
        });
    } else {
        alert("id not found please check you initGrid()")
    }
}

function getId(dgid) {
    var id = $("#" + dgid).find("tr[class*='datagrid-row-selected']").find("td[style*='none']").text();
    alert(id);
    return id || -1;
}

//function getId() {
//    var id = $(".datagrid-view1 .datagrid-btable").find("tr[class*='datagrid-row-selected']").find("td[style*='none']").text();
//    return id || -1;
//}

//重构initGrid
function initGrid(dgid, url, idField, sortName, columns, remoteSort, singleSelect, title) {
    grid = $("#" + dgid).datagrid({
        title: title || '列表',
        height: 450,
        fit: true,
        method: "post",
        border: false,
        url: url,
        idField: idField || "",
        pagination: true,
        rownumbers: true,
        singleSelect: singleSelect || false,
        remoteSort: remoteSort || false,
        sortName: sortName || "",
        pageSize: 20,
        pageList: [20, 40, 60],
        toolbar: "#toolbar",
        iconCls: 'ext-icon-application_view_detail',
        frozenColumns: [[
                    { field: 'ck', checkbox: true, width: '5%' },
                    { field: idField, title: '编号', hidden: true }
        ]],
        columns: columns
    });
}

function initTreeGrid(dgid, url, title, idField, treeField, columns) {
    grid = $("#" + dgid).treegrid({
        title: title || '列表',
        method: "post",
        border: false,
        url: url,
        treeField: treeField,
        idField: idField || "",
        rownumbers: true,
        toolbar: "#toolbar",
        iconCls: 'ext-icon-application_view_detail',
        frozenColumns: [[
                    { field: idField, title: '编号', width: '5%', hidden: true }
        ]],
        columns: columns
    });
}
function fixWidthTable(percent) {
    return getWidth(1) * percent;
}

function getWidth(percent) {
    return $(window).width() * percent;
}
Date.prototype.format = function (fmt) {
    var o = {
        "M+": this.getMonth() + 1, //月份 
        "d+": this.getDate(), //日 
        "h+": this.getHours(), //小时 
        "m+": this.getMinutes(), //分 
        "s+": this.getSeconds(), //秒 
        "q+": Math.floor((this.getMonth() + 3) / 3), //季度 
        "S": this.getMilliseconds() //毫秒 
    };
    if (/(y+)/.test(fmt)) fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    for (var k in o)
        if (new RegExp("(" + k + ")").test(fmt)) fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
    return fmt;
};
function formatDateTime(str) {
    var date = new Date(str.replace(/-/g, "/"));
    return date.format("yyyy-MM-dd hh:mm:ss");
}

function formatDate(str) {
    var date = new Date(str.replace(/-/g, "/"));
    return date.format("yyyy-MM-dd");
}

function stringFormat() {
    if (arguments.length == 0)
        return null;
    var str = arguments[0];
    for (var i = 1; i < arguments.length; i++) {
        var re = new RegExp('\\{' + (i - 1) + '\\}', 'gm');
        str = str.replace(re, arguments[i]);
    }
    return str;
}


function isDisable(val) {
    return val ? "已禁用" : "启用";
}