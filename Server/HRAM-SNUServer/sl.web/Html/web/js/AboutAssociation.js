$(document).ready(function() {
	
	 getInformType();	
});

//通知公告数据类型获取
function getInformType(){
	 var data = '{"result":[{"Infvalue":"昆明市劳动和社会保障协会换届大会议程与选举办"},{"Infvalue":"昆明市人力资源和社会保障协会揭牌仪式方案"},{"Infvalue":"第四届全国高校毕业生（秋季）跨区域巡回招聘活动“昆明站”成功举办"},{"Infvalue":"昆明市人力资源和社会保障协会办公会议简报"},{"Infvalue":"昆明市人力资源和社会保障协会理事会会议决议"},{"Infvalue":"昆明市人力资源和社会保障协会征求意见"},{"Infvalue":"关于共同征集和发布高校毕业生需求岗位信息的函"},{"Infvalue":"昆明市双创联盟成立仪式及“春城创业荟”项目路演活动成功举办"}]}';
    var inform=JSON.parse(data);
	var con1=$('#con1');
	var n;
	for(var i=0 ;i<inform.result.length;i++){ 
	    n=i+1;
	    con1.append('<li><a href="http://123.57.26.127/Html/web/model.html?id=' + n + 1 + '"' + '" target="_blank" >' + inform.result[i].Infvalue + '</a></li>');
	  
    } 
	
}

   

/*//通知公告数据类型获取
function getInformType(){
   $.ajax({
		type:'GET',
		dataType:"json",
		url: 'http://123.57.26.127/api/Article/GetPublishDetail?publishID=859424AB-3065-486D-B052-7D6EA3BB85C8',
		data:{},
		success: function(data){
			IntInform(data);
		},
		error:function(XMLHttpRequest,textStatus, errorThrown){alert("未能获取数据类型！");}
	});
}
//通知公告数据类型初始化
function IntInform(data){
	    var con1=$('#con1');
		var n;
		for(var i=0 ;i<=inform.result.length;i++){
			n=i+1;
		   con1.append('<li><a href="file:///C:/Users/wzy/Desktop/zuzu/web/model.html?id=' +n+1+'"' + '" target="_blank">' + inform.result[i].pmText + '</a></li>');
		}
	

}*/
  /*function funcGetDetailsType(infIds){
	var data = '{"result":[{"infId":"20170101","title":"云南省人力资源和社会保障厅关于印发云南省工伤保险辅助器具配置项目标准的通知","resource":"云南省人力资源和社会保障厅"time":"2014/05/05","article":"协会坚持党的基本路线和方针政策，遵守宪法、法律、法规和社会道德风尚，团结吸纳广大城乡企事业单位相关工作部门和工作者以及一切关心、热爱、有志于研究探讨人力资源和社会保障事业的专家、学者，通过学习、实践、研究和探讨，共同推动人力资源和社会保障理论的深化，促进人力资源和社会保障工作的开展，更好地满足用人单位和广大劳动者对人力资源和社会保障的需求，维护用人单位和劳动者的合法权益，为促进我市经济发展和社会稳定做贡献。"}]}';
	var inform=JSON.parse(data);
	var title=$('#title');
	alert(infIds);
	title.html(inform.result.title);
	
	for(var i=0 ;i<=inform.result.length;i++){
		if(infIds[i]==inform.result[i].infId){
		   details.append('<h3 id="title">'+ inform.result[i].title+'</h3>');
		}
	}

}*/
