/**
 * 检查设备
 */
if(mui.os.plus) {
	mui.plusReady(function() {
		getArticleList();
	});
} else {
	mui.ready(function() {
		getArticleList();
	});
}

function getArticleList() {
	var URL = 'http://123.57.26.127/api/Article/GetPublishList/?pageIndex=1&pageSize=2';

	$.get(URL, function(data) {
			mui.toast(JSON.stringify(data))
		} //返回的data是字符串类型

	);

	$.ajax({
		url: URL,
		type: 'GET',
//		data: {
//			"pageIndex": 1,
//			"pageSize": 2
//		},
		dataType: {
			'Content-Type': 'application/json'
		},
		success: function(data) {
			//服务器返回响应，根据响应结果
			console.log(JSON.stringify(data));
			mui.toast(JSON.stringify(data))

		},
		error: function(err, type) {
			console.log(JSON.stringify(err));
			mui.toast(JSON.stringify(err));
		},
		beforeSend: function() { //发送请求前调用，可以放一些"正在加载"之类额话
			alert("正在加载");
		}
	})
	//	mui.ajax(URL, {
	//		dataType: 'json', //服务器返回json格式数据
	//		type: 'GET', //HTTP请求类型
	//		timeout: 6000, //超时时间设置为6秒；
	//		headers: {
	//			'Content-Type': 'application/json',
	//		},
	//		success: function(data) {
	//			//服务器返回响应，根据响应结果
	//			console.log(JSON.stringify(data));
	//			mui.toast(JSON.stringify(data))
	//
	//		},
	//		error: function(err, type) {
	//			console.log(JSON.stringify(err));
	//			mui.toast(JSON.stringify(err));
	//		}
	//	});
}