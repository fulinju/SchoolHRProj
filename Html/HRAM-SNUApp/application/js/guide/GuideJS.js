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

/**
 * 初始化
 */
function funcInitView() {
	//重写mui.back()，什么都不执行，反之用户返回到入口页；
	mui.back = function() {
		plus.runtime.quit(); //退出APP
	};

	//获取系统状态栏高度
	var sh = plus.navigator.getStatusbarHeight();
	//获取设备屏幕高度分辨率以及宽度分辨率
	var h = plus.screen.resolutionHeight;
	var w = plus.screen.resolutionWidth;
	//设置图片高度，这里图片并不规范；实际开发中，建议制作iphone6plus规格的图片；
	var imgs = document.querySelectorAll(".guide-img");
	for(var i = 0, len = imgs.length; i < len; i++) {
		imgs[i].style.height = (h - sh) + "px";
		imgs[i].style.width = w + "px";
	}

	
	document.getElementById("btnStart").addEventListener("tap", function() {
		/**
		 * 向本地存储中设置launchFlag的值，即启动标识；
		 */
		plus.storage.setItem("launchFlag", "true");
		mui.openWindow({
			url: "../acc/LoginView.html",
			id: "LoginView"
		});

		//延时执行关闭GuideView
		mui.later(function() {
			plus.webview.currentWebview().close();
		}, 1000);
	});
}