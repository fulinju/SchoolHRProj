$(document).ready(function () {
    funcGetMember();
});

//获取类型
function funcGetMember() {
    $.ajax({
        type: "get",
        dataType: "json",
        url: '/api/Member/GetMemberList/?pageIndex=1&pageSize=20',
        data: {},
        success: function (data) {
            funcInitMember(data);
        }, error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert("未能获取友情链接类型");
        }
    });
}

//初始化会员
function funcInitMember(member) {
    var ulLeft = $('#ulLeft');
    var ulRight = $('#ulRight');
	var homepage_members = $('#homepage_members');
    
    for (var i = 0; i < member.resultList.length; i++) {
        if (i % 2 == 0) {
            ulLeft.append('<li><a href="' + member.resultList[i].mURL + '" target="_blank">' + member.resultList[i].mName + '<span >' + member.resultList[i].mApplyTime + '</span></a></li>');
        } else {
            ulRight.append('<li><a href="' + member.resultList[i].mURL + '" target="_blank">' + member.resultList[i].mName + '<span >' + member.resultList[i].mApplyTime + '</span></a></li>');
        }
		//首页会员单位
	    homepage_members.append('<li><a href="' + member.resultList[i].mURL + '" target="_blank">' + member.resultList[i].mName + '</span></a></li>');
    }
	
}
