
$(document).ready(function() {
	
	 getPolicyType();	
	 
});
//通知公告数据类型获取
function getPolicyType(){
	 var data = '{"result":[{"Infvalue":"云南省人力资源和社会保障厅关于印发云南省工伤保险辅助器具配置项目标准的通知"},{"Infvalue":"云南省人力资源和社会保障厅关于 继续落实降低生育保险费率措施的通知"},{"Infvalue":"云南省高层次人才直聘暂行办法的政策解读"},{"Infvalue":"《关于深化职称制度改革的意见》印发"},{"Infvalue":"关于鼓励高等院校和科研院所专业技术人员离岗创业的实施意见政策解读"},{"Infvalue":"企业劳动争议协商调解规定"},{"Infvalue":"昆明加强协调劳动关系三方机制的若干规定"},{"Infvalue":"云南省人事争议仲裁暂行办法"}]}';
    var inform=JSON.parse(data);
	var news_second1=$('#news_second1');
	var conten2_ul1=$('#conten2_ul1');
	var conten2_ul2=$('#conten2_ul2');
	
	var n;
	for(var i=0 ;i<inform.result.length;i++){ 
	    n=i+1;
	    news_second1.append('<li><a href="http://123.57.26.127/Html/web/model.html?id=' + n + 2 + '"' + '" target="_blank" >' + inform.result[i].Infvalue + '</a></li>');
	    conten2_ul1.append('<li><a href="http://123.57.26.127/Html/web/model.html?id=' + n + 2 + '"' + '" target="_blank" >' + inform.result[i].Infvalue + '</a></li>');
	    conten2_ul2.append('<li><a href="http://123.57.26.127/Html/web/model.html?id=' + n + 2 + '"' + '" target="_blank" >' + inform.result[i].Infvalue + '</a></li>');
	  
	
    }  
}


/*// JavaScript Document
$(document).ready(function() {
    funcGetLeftType();
	funcGetRighttType();
});

//获取左边导航栏数据类型
function funcGetLeftType(){
	$.ajax({
		type:'GET',
		dataType:"json",
		url:"http://123.57.26.127/api/Type/GetTypeList",
		data:{},
		success:function(data){
			funcInit(data);
		},error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert("未能获取友情链接类型");
        }
	    	
	});

}

//初始化左边导航栏
function funcInit(flType){
	var setTable = $('#setTable');
    var num;
    var flTypeIds = new Array();
	for (var i = 0; i < flType.result.length; i++) {
		flTypeIds[i] = flType.result[i].flTypeID;
		if (i == 0) {
            setTable.append('<tr><td>&nbsp;</td></tr>'
                                + '<tr> <td class="table_second_bar_td on1" id="bg_2_1"   onmouseover=showtab(2,1,5)><a class="pl35" id="td_2_1"  href="">'
                                + flType.result[i].flTypeValue + '</a> </td> </tr>');
        } else {
            num = i + 1;
            setTable.append('<tr><td>&nbsp;</td></tr>'
                              + '<tr> <td class="table_second_bar_td"  id="bg_2_' + num + '" onmouseover=showtab(2,' + num + ',5)><a class="pl35" id="td_2_' + num + '"  href="">'
                              + flType.result[i].flTypeValue + '</a> </td> </tr>');
        }
    }

}
*/