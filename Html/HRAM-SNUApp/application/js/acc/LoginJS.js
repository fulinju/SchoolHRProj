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
		if(localStorage.autoLogin) {
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

	mui.openWindow({
		url: "../main/MainView.html",
		id: "MainView",
		waiting: {
			autoShow: false
		}
	});

})