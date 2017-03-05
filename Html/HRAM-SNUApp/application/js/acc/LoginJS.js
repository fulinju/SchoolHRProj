if(mui.os.plus) {
	mui.plusReady(function() {
		mui.init();
		funcInitView();
	});
} else {
	mui.ready(function() {
		mui.toast("部分功能可能不支持");
	});
}

function funcInitView() {
	var launchFlag = plus.storage.getItem("launchFlag");

	if(!launchFlag) { //为false
		mui.openWindow({
			url: "../guide/GuideView.html",
			id: "GuideView",
			waiting: {
				autoShow: false
			}
		});

		//手动关闭启动页
		mui.later(function() {
			plus.navigator.closeSplashscreen();
		}, 1000);

		//延时执行关闭登录界面
		setTimeout(function() {
			plus.webview.currentWebview().hide();
		}, 5000);

	} else {
		//判断是否记住密码
		if(localStorage.autoLogin == "true") {
			mui.openWindow({
				url: "../main/MainView.html",
				id: "MainView",
				waiting: {
					autoShow: false
				}
			});

			mui.later(function() {
				plus.navigator.closeSplashscreen();
			}, 1000);

		} else {
			plus.navigator.closeSplashscreen();
		}

	}
}

/**
 * 登录
 */
mui(".mui-content-padded").on('tap', '#btnLogin', function() {
	var flag = $('#swtAutoLogin').hasClass('mui-active');
	if(flag == true) {
		localStorage.autoLogin = true;
	} else {
		localStorage.autoLogin = false;
	}

	funcLogin();
})

mui(".link-area").on('tap', '#btnRegister', function() {
	mui('#registerDlg').popover('show');
})

/**
 * 登录操作
 */
function funcLogin() {
	var loginStr = $("#edtLoginName").val();
	var pwdStr = $("#edtPwd").val();
	console.log(loginStr +" "+ pwdStr)
	var pwdMd5 = hex_md5(pwdStr).toUpperCase();

	var URL = 'http://192.168.0.100:8888/api/Account/Login/';

	plus.nativeUI.showWaiting(); //显示原生等待框
	
	console.log(URL)

	mui.ajax(URL, {
		dataType: 'json', //服务器返回json格式数据
		type: 'post', //HTTP请求类型
		timeout: 6000, //超时时间设置为6秒；
		contentType: 'application/json',
		data: {
			'uLoginStr': loginStr,
			'uPassword': pwdMd5
		},
		success: function(data) {
			//服务器返回响应，根据响应结果，分析是否登录成功；
			console.log(JSON.stringify(data));
			plus.nativeUI.closeWaiting(); //关闭原生等待框

			mui.openWindow({
				url: "../main/MainView.html",
				id: "MainView",
				waiting: {
					autoShow: false
				}
			});
		},
		error: function(err, type) {
			plus.nativeUI.closeWaiting(); //关闭原生等待框

			if(type == "timeout") {
				console.log("timeout");
			} else {
				var responseJson = JSON.parse(err.response);
				console.log(responseJson);
			}

		}
	});
}

/**
 * 注册按钮
 */
mui(".mui-content").on('tap', "#btnReg", function() {
	funcRegisterByMail();
})

/**
 * 
 */
function funcRegisterByLoginName() {

}

/**
 * 邮箱注册操作
 */
function funcRegisterByMail() {
	var maibox = $("#edtMaibox").val();
	console.log(mailbox);

	var URL = 'https://cherry-cafeteria.com:443/api/CSOrder_User/Register/';

	plus.nativeUI.showWaiting(); //显示原生等待框

	mui.ajax(URL, {
		dataType: 'json', //服务器返回json格式数据
		type: 'post', //HTTP请求类型
		timeout: 6000, //超时时间设置为6秒；
		contentType: 'application/json',
		data: {
			'uUserName': username,
			'uMailbox': mailbox,
			'uPhone': phonne
		},
		success: function(data) {
			//服务器返回响应，根据响应结果，分析是否登录成功；
			receivedNetData = data;

			fucnRegisterSuccess();
			plus.nativeUI.closeWaiting(); //关闭原生等待框
		},
		error: function(err, type) {
			plus.nativeUI.closeWaiting(); //关闭原生等待框

			if(type == "timeout") {
				mui.toast(LangRes.Prop('Timeout'));
			} else {
				var responseJson = JSON.parse(err.response);
				//异常处理
				if(responseJson.ErrorCode == undefined) {
					mui.toast(LangRes.Prop('SubmitRegisterInfoFailed'));
				} else {
					if(localStorage.Language != 'zh_CN') {
						mui.toast(responseJson.ErrorDescEN)
					} else {
						mui.toast(responseJson.ErrorDescCH)
					}
				}
			}

		}
	});
}