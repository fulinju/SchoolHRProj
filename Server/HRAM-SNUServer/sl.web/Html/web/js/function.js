

window.onload = function () {
	
	//二级菜单
	  //关于协会	
/*	  $().getClass('nav_li1').hover(function () {
		$().getClass('second1').show();
	}, function () {
		$().getClass('second1').hide();
	});
		  //服务大厅
	$().getClass('nav_li5').hover(function () {
		$().getClass('second2').show();
		//服务大厅三级菜单
	    //信息查询
		$().getClass('second2_li1').hover(function () {
		$().getClass('third1').show();
		}, function () {
			$().getClass('third1').hide();
		});
			 //协会服务
		$().getClass('second2_li2').hover(function () {
			$().getClass('third2').show();
		}, function () {
			$().getClass('third2').hide();
		});
		
	}, function () {
		$().getClass('second2').hide();
	});   
		  //会员中心
	$().getClass('nav_li6').hover(function () {
		$().getClass('second3').show();
	}, function () {
		$().getClass('second3').hide();
	});
	
	*/
	
	//登录框
	var login = $().getId('login');
	var screen=$().getId('screen');
	login.center(350, 250).resize(function () {
		login.center(350, 250);
	});
	$().getClass('login').click(function () {
		login.css('display', 'block');
		//锁屏操作
	   screen .lock();
	});
	$().getClass('close').click(function () {
		login.css('display', 'none');
		//关闭锁屏操作
	    screen.unlock();
	});
	
	
	
	// 首页 友情链接
/*	
	$().getId('link_list1').hover(function () {
		$().getId('link_second1').show();
	}, function () {
		$().getId('link_second1').hide();
	});
	
	$().getId('link_list2').hover(function () {
		$().getId('link_second2').show();
	}, function () {
		$().getId('link_second2').hide();
	});
	
	$().getId('link_list3').hover(function () {
		$().getId('link_second3').show();
	}, function () {
		$().getId('link_second3').hide();
	});
	
	$().getId('link_list4').hover(function () {
		$().getId('link_second4').show();
	}, function () {
		$().getId('link_second4').hide();
	});*/
	
	//首页  政策法规和行业动态
     $().getId('news_first1').hover(function () {
				$().getId('news_second1').show();
			}, function () {
				
			});	
     $().getId('news_first2').hover(function () {
				$().getId('news_second2').show();
			}, function () {
				$().getId('news_second2').hide();
			});
	 //政策法规
     $().getId('left_list1').hover(function () {
				$().getId('right1').show();
			}, function () {
				$().getId('right1').hide();
	});
     $().getId('left_list2').hover(function () {
				$().getId('right2').show();
			}, function () {
				$().getId('right2').hide();
	});
	
	//资料下载 
	 $().getId('down_first1').hover(function () {
				$().getId('down_left').show();
			}, function () {
				
			});	
     $().getId('down_first2').hover(function () {
				$().getId('down_right').show();
			}, function () {
				$().getId('down_right').hide();
			});

     //会员申请表单
	 	//表单验证
	

	
	$('form').form('user').bind('focus', function () {
		$('#reg .two').css('display', 'block');
		$('#reg .error').css('display', 'none');
		$('#reg .succ').css('display', 'none');
	}).bind('blur', function () {
		if (trim($(this).value()) == '') {
			$('#reg .two').css('display', 'none');
			$('#reg .error').css('display', 'none');
			$('#reg .succ').css('display', 'none');
		} else if (!/[a-zA-Z0-9_]{2,20}/.test(trim($(this).value()))) {
			$('#reg .error').css('display', 'block');
			$('#reg .two').css('display', 'none');
			$('#reg .succ').css('display', 'none');
		} else {
			$('#reg .succ').css('display', 'block');
			$('#reg .error').css('display', 'none');
			$('#reg .two').css('display', 'none');
		}
	});
	 
};
