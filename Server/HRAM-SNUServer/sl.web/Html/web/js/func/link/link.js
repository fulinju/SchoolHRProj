
$(document).ready(function () {
    funcGetFlType();
});

//获取类型
function funcGetFlType() {
    $.ajax({
        type: "get",
        dataType: "json",
        url: '/api/Type/GetTypeList',
        data: {},
        success: function (data) {
            funcInitFlType(data);
        }, error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert("未能获取友情链接类型");
        }
    });
}

//初始化类型
function funcInitFlType(flType) {
    var segFlTable = $('#segFlTable');
    var num;
    var flTypeIds = new Array();

    for (var i = 0; i < flType.result.length; i++) {
        flTypeIds[i] = flType.result[i].flTypeID;
        if (i == 0) {
            segFlTable.append('<tr><td>&nbsp;</td></tr>'
                                + '<tr> <td class="table_second_bar_td on1" id="bg_2_1"   onmouseover=showtab(2,1,5)><a class="pl35" id="td_2_1"  href="">'
                                + flType.result[i].flTypeValue + '</a> </td> </tr>');
        } else {
            num = i + 1;
            segFlTable.append('<tr><td>&nbsp;</td></tr>'
                              + '<tr> <td class="table_second_bar_td"  id="bg_2_' + num + '" onmouseover=showtab(2,' + num + ',5)><a class="pl35" id="td_2_' + num + '"  href="">'
                              + flType.result[i].flTypeValue + '</a> </td> </tr>');
        }
    }

    funcGetFl(flTypeIds)
}

//获取友情链接
function funcGetFl(flTypeIds) {
    var models = JSON.stringify(flTypeIds);
    console.log(models)

    $.ajax({
        type: 'POST',
        url: '/api/FriendlyLink/FindFriendlyLinkListByTypeTop12',
        contentType: "application/json",  //加上这个
        data: models,
        dataType: 'json',
        success: function (data, textStatus) {
            funcInitFl(data);
        },
        error: function (xmlHttpRequest, textStatus, errorThrown) {
        }
    });
}

//初始化友情链接
function funcInitFl(fl) {
    console.log(JSON.stringify(fl));

    var num;
    var tdFl = $('#tdFl');
    var table;

    for (var i = 0; i < fl.result.length; i++) {
        num = i + 1;

        if (i == 0) {
            table = $('<table id="tab_2_' + num + '" style="display:block; width:94%" > <tr><td height="20" colspan="3"></td> </tr>');
        } else {
            table = $('<table id="tab_2_' + num + '" style="display:none; width:94%"  > <tr><td height="20" colspan="3"></td> </tr>');
        }
        
        for (var j = 0; j < fl.result[i].length; j += 2) {
            if (j + 1 < fl.result[i].length) {
                  table.append('<tr><td width="12" height="28" align="center"></td><td><a href="' + fl.result[i][j].flURL + '" target="_blank">' + fl.result[i][j].flName + '</a></td>'
                + '<td  class="right"><a href="' + fl.result[i][j + 1].flURL + '" target="_blank">' + fl.result[i][j + 1].flName + '</a></td> </tr><tr> <td height="1" colspan="3" ></td> </tr>'); //如果不是创建标签，append一行需要append整个
            } else {
                table.append('<tr><td width="12" height="28" align="center"></td><td><a href="' + fl.result[i][j].flURL + '" target="_blank">' + fl.result[i][j].flName + '</a></td></tr>');
            }
        }
        tdFl.append(table);
    }
    console.log(tdFl)

}