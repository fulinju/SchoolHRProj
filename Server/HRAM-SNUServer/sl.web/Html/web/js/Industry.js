
$(document).ready(function() {
	 getIndustryType();	

});
//通知公告数据类型获取
function getIndustryType(){
	 var data = '{"result":[{"Infvalue":"国家卫生计生委：贫困人口大病保险起付线降低50%"},{"Infvalue":"新动能对就业贡献大"},{"Infvalue":"昆明市医养结合工作全面启动 建立“三体一式一型”311模式工作机制，鼓励社会力量办非营利性养老机"},{"Infvalue":"省人社厅关于《关于推进三医联动改革促进人民健康优先发展的实施意见》的政策解读"},{"Infvalue":"人社部:完善新就业形态的用工和社保制度"},{"Infvalue":"征求意见稿：单位可否单方面对劳动者调岗？​"},{"Infvalue":"人力资源社会保障部办公厅财政部办公厅关于进一步做好2017年就业重点工作的通知"},{"Infvalue":"人社部：实现养老保险基金长期平衡 严控提前退休"},{"Infvalue":"这10个时间节点很重要 错过后亏的不只是钱！"}]}';
    var inform=JSON.parse(data);
	var news_second2=$('#news_second2');
	var conten2_ul1=$('#conten2_ul1');
	var conten2_ul2=$('#conten2_ul2');
	var n;
 	for(var i=0 ; i<inform.result.length;i++){
		n=i+1;
		news_second2.append('<li><a href="http://123.57.26.127/Html/web/model.html?id=' + n + 3 + '"' + '" target="_blank" >' + inform.result[i].Infvalue + '</a></li>');
	    conten2_ul1.append('<li><a href="http://123.57.26.127/Html/web/model.html?id=' + n + 3 + '"' + '" target="_blank" >' + inform.result[i].Infvalue + '</a></li>');
	    conten2_ul2.append('<li><a href="http://123.57.26.127/Html/web/model.html?id=' + n + 3 + '"' + '" target="_blank" >' + inform.result[i].Infvalue + '</a></li>');
	
    }  
}