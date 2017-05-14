// JavaScript Document
$(document).ready(function(e) {
	Reg();
	
});
//协会成员注册
function Reg(){
	
 //公司名称验证
   $('#regUser').focus(function(){		   
	   $('#reg #info1').show();
	   $('#reg #error1').hide();
	   $('#reg #succ1').hide();
   }).blur(function(){
	   if($.trim($(this).val())==''){
		   $('#reg #info1').hide();
		   $('#reg #error1').hide();
		   $('#reg #succ1').hide();
	   }else{
		  if(!check_user()){
			 $('#reg #info1').hide();
			 $('#reg #error1').show();
			 $('#reg #succ1').hide();
		  } else {
			 $('#reg #info1').hide();
			 $('#reg #error1').hide();
			 $('#reg #succ1').show();
		  }
		}
   });
   
   function check_user(){
      if(/[a-zA-Z0-9_\u4e00-\u9fa5]{2,20}/.test($.trim($('#regUser').val())))return true;
   }
   
  //组织机构代码验证
   $('#regPass').focus(function(){ 
	   $('#reg #info2').show();
	   $('#reg #error2').hide();
	   $('#reg #succ2').hide();
   }).blur(function(){
	   if($.trim($(this).val())==''){
		   $('#reg #info2').hide();
	   }else{
		   if(check_pass()){
			 $('#reg #info2').hide();
			 $('#reg #error2').hide();
			 $('#reg #succ2').show();
	       }else{
			 $('#reg #info2').hide();
			 $('#reg #error2').show();
			 $('#reg #succ2').hide();
		   }
	   } 
   });
   
   //密码强度认证
   $('#regPass').keyup(function(){
	   check_pass()
   });
   function check_pass(){
	   var value = $.trim($($('#regPass')).val());
	   var value_length=value.length;
	   var code_length=0;

	   //如果密码长度在6-20之间第一个圆点变绿
	   if(value_length>=6 && value_length<=20){   
		   $('#reg #info2 .q1').html('●').css('color','green');
	   }else{
		   $('#reg #info2 .q1').html('○').css('color','#CCC');  
	   }
	   //只能包含大小写字母、数字和非空格字符.第二个圆点变绿
	   if(value_length>0 && !/\s/.test(value)){
		   $('#reg #info2 .q2').html('●').css('color','green');
	   }else{
		   $('#reg #info2 .q2').html('○').css('color','#CCC');
	   }
	   //含大、小写字母、数字、非空字符两种以上
	   if(/[a-z]/.test(value)){
		   code_length++;
	   }
	   if(/[A-Z]/.test(value)){
		   code_length++;
	   }
	   if(/[0-9]/.test(value)){
		   code_length++;
	   }
	   if(/[^0-9a-zA-Z]/.test(value)){//任意非空字符
		   code_length++;
	   }
	   if(code_length>=2){
		   $('#reg #info2 .q3').html('●').css('color','green');	   
	   }else{
		   $('#reg #info2 .q3').html('○').css('color','#CCC');
	   }
  //密码安全级别验证
	   if(value_length>=10&&code_length>=3){ //高：密码长度大于等于10，三种不同类别字符
		   $('#reg #info2 .s1').css('color','green');
		   $('#reg #info2 .s2').css('color','green');
		   $('#reg #info2 .s3').css('color','green');	
		   $('#reg #info2 .s4').html('高').css('color','green');	
	   }else if(value_length>=8&&code_length>=2){//中：密码长度大于等于8，两种不同类别字符
		   $('#reg #info2 .s1').css('color','green');
		   $('#reg #info2 .s2').css('color','#f60');
		   $('#reg #info2 .s3').css('color','#ccc');	
		   $('#reg #info2 .s4').html('中').css('color','#f60');	
		}else if(value_length>=1){ //低：密码长度大于等于一个字符
		   $('#reg #info2 .s1').css('color','#f60');
		   $('#reg #info2 .s2').css('color','#f60');
		   $('#reg #info2 .s3').css('color','#ccc');	
		   $('#reg #info2 .s4').html('低').css('color','#f60');
		}else{
		   $('#reg #info2 .s1').css('color','#ccc');
		   $('#reg #info2 .s2').css('color','#ccc');
		   $('#reg #info2 .s3').css('color','#ccc');	
		   $('#reg #info2 .s4').html('')
		
		}
		//密码 验证
		if(value_length>=6 && value_length<=20 && !/\s/.test(value)&& code_length>=2 && !/[\u4e00-\u9fa5]/.test(value)){
		   return true;
		}else{
		   return false;
		}
   }
      
    //密码确认验证
   $('#notPass').focus(function(){ 
	   $('#reg #info8').show();
	   $('#reg #error8').hide();
	   $('#reg #succ8').hide();
	   
     }).blur(function(){
	     var value=$.trim($(this).val());
		 var value_length=value.length;
		 if(value==''){
			 $('#reg #info8').hide();
		  }else if(check_notPass()){//如果密码与前面输入一致
			   $('#reg #info8').hide();
			   $('#reg #error8').hide();
	     	   $('#reg #succ8').show();
		  }else{
			   $('#reg #info8').hide();
			   $('#reg #error8').show();
	     	   $('#reg #succ8').hide();
		  }
     });
	 function check_notPass(){
	     if( $('#notPass').val()==$.trim($('#regPass').val())){
		  return true;
		}
	 
	 }
   
   //单位地址验证
   $('#address').focus(function(){ 
	   $('#reg #info3').show();
	   $('#reg #error3').hide();
	   $('#reg #succ3').hide();
     }).blur(function(){
	     var value=$(this).val();
		 if(value==''){
			 $('#reg #info3').hide();
			 $('#reg #error3').hide();
			 $('#reg #succ3').hide();
		  }else{
			 $('#reg #info3').hide();
			 $('#reg #error3').hide();
			 $('#reg #succ3').show();
		  }
   });
   
   //法人姓名验证
      $('#law_man').focus(function(){ 
	   $('#reg #info4').show();
	   $('#reg #error4').hide();
	   $('#reg #succ4').hide();
     }).blur(function(){
	     var value=$.trim($(this).val());
		 if(value==''){
			 $('#reg #info4').hide();
			 $('#reg #error4').hide();
			 $('#reg #succ4').hide();
		  }else{
			  if(check_law_man()){
				 $('#reg #info4').hide();
				 $('#reg #error4').hide();
				 $('#reg #succ4').show();
			  }else{
				 $('#reg #info4').hide();
				 $('#reg #error4').show();
				 $('#reg #succ4').hide();
			  }
		  }
     });
	 function check_law_man(){
		 var value=$.trim($('#law_man').val());
		 var value_length=value.length;
	     if(value_length>=2&&value_length<=10){
	         return true;
		 }
		 
	 }
	 
   //法人身份证验证
   $('#ID_num').focus(function(){ 
	   $('#reg #info5').show();
	   $('#reg #error5').hide();
	   $('#reg #succ5').hide();
     }).blur(function(){
	     var value=$.trim($(this).val());
		 if(value==''){
			 $('#reg #info5').hide();
			 $('#reg #error5').hide();
			 $('#reg #succ5').hide();
		  }else{
			    //身份证一般是15位或18,非空，非汉字
			  if(check_ID_num()){
				 $('#reg #info5').hide();
				 $('#reg #error5').hide();
				 $('#reg #succ5').show();
			  }else{
				 $('#reg #info5').hide();
				 $('#reg #error5').show();
				 $('#reg #succ5').hide();
			  }
		  }
   });
   function check_ID_num(){
		 var value=$.trim($('#ID_num').val());
		 var value_length=value.length;
	     if((value_length==15||value_length==18)&&!/[\u4e00-\u9fa5]/.test(value)&& !/\s/.test(value)){
	         return true;
		 }
		 
	 }
   
    //联系电话验证
   $('#phone').focus(function(){ 
	   $('#reg #info6').show();
	   $('#reg #error6').hide();
	   $('#reg #succ6').hide();
     }).blur(function(){
	     var value=$.trim($(this).val());
		 var value_length=value.length;
		 if(value==''){
			 $('#reg #info6').hide();
			 $('#reg #error6').hide();
			 $('#reg #succ6').hide();
		  }else{
			    //电话号码一般是11位或8,非空，非负整数
			   if(check_phone()){
				 $('#reg #info6').hide();
				 $('#reg #error6').hide();
				 $('#reg #succ6').show();
			  }else{
				 $('#reg #info6').hide();
				 $('#reg #error6').show();
				 $('#reg #succ6').hide();
			  }
		  }
   });
    function check_phone(){
		 var value=$.trim($('#phone').val());
		 var value_length=value.length;
	     if((value_length==11||value_length==8)&&!/[\u4e00-\u9fa5]/.test(value)&& !/\s/.test(value)&&!/[a-zA-Z]/.test(value)){
	         return true;
		 }
		 
	 }
   
   
   
    //单位网址验证
   $('#company_url').focus(function(){ 
	   $('#reg #info7').show();
	   $('#reg #error7').hide();
	   $('#reg #succ7').hide();
     }).blur(function(){
	     var value=$.trim($(this).val());
		 var value_length=value.length;
		 if(value==''){
			 $('#reg #info7').hide();
			 $('#reg #error7').hide();
			 $('#reg #succ7').hide();
		  }else{
			   $('#reg #info7').hide();
			   $('#reg #error7').hide();
		
	
		  }
   });

	 
	 //公司简介
	 $('#text_info').keyup(function(){
		 check_ps();
	  }).focus(function(){//因为找不到触发粘贴的方法所以。。。
		 setTimeout(check_ps,50);
		 
	  });
	 //清除多余部分
	 $('.clear').click(function(){
	     $('#text_info').val(( $('#text_info').val().substring(0,400)));
		  $('#company_info #ps2 #num2').html(0).css('color','#ccc');
	 });

     function check_ps(){
		var num=400-$('#text_info').val().length;
		if(num>=0){
			$('#company_info #ps1 #num1').html(num);
		}else{
			$('#company_info #ps1').hide();
			$('#company_info #ps2 #num2').html(Math.abs(num)).css('color','#F00');//绝对值
			$('#company_info #ps2').show()
		}
	 }
	 
	 //单位性质验证
     $('#quesion').change(function(){
	     if(check_quesion()){
		   $('#reg #error9').hide();
		 }
	 });
	 
	 function check_quesion(){
	     if($('#quesion').val()!=0){//0是选项的序号
		     return true;
		 }
	 }
	 
	 //提交验证
	 $('.subBtn').click(function(){
		 var flag=true;
		 if(!check_user()){
			flag=false;
			$('#reg #error1').show();
		  }
		  if(!check_pass()){
			  flag=false;
		     $('#reg #error2').show();
		  }
		 
		  if(!check_notPass()){
		      flag=false;
			  $('#reg #error8').show(); 
		  }else if($('#notPass').val()==''){
			  flag=false;
			  $('#reg #error8').show(); 
		  }
		  
		  if(!check_quesion()){
		      flag=false;
			  $('#reg #error9').show();
		  }
		  //单位地址
		  if($('#address').val()=='' ){
			  flag=false;
		     $('#reg #error3').show();
		  }
		  //法人姓名
		if(!check_law_man()){
		     flag=false;
			 $('#reg #error4').show();
		}
		 //法人身法证
		 if(!check_ID_num()){
		     flag=false;
			 $('#reg #error5').show();
		 }
		 //联系电话
		 if(!check_phone()){
		     flag=false;
			 $('#reg #error6').show();
		 }
		 //网址验证
		 if($('#company_url').val()=='' ){
			  flag=false;
		     $('#reg #error7').show();
			
		  }
		 if(flag){
			$('#membership').hide();
			$('#reg').submit(function(e){
             
			});
			$('#reg_login').show();
			//点击登陆按钮
	       $('#btnLogin').click(function(event){
			  // event.stopPropagation();//阻止冒泡事件
			   var login_user=$('#login_user').val();
			   var login_pass=$('#login_pass').val();
			   var regUser=$('#regUser').val();
			   var regPass=$('#regPass').val();
			   if(login_user==regUser&&login_pass==regPass){
     			   Meg();
			   }/*else{
			      alert('密码错误！');
			   }*/
			});

		 }
	 });
	 
	 //公司信息管理
	//小导航
	$('#li1').click(function(){
	    $('#table1').show();
	    $('#table2').hide();
		$('#li1').css('background','#FFF');
		$('#li2').css('background','none');
	});
	$('#li2').click(function(){
	    $('#table2').show();
	    $('#table1').hide();
		$('#li2').css('background','#FFF');
		$('#li1').css('background','none');
	});
	//获取和填入公司信息
	function Meg(){
		$('#reg_login').hide();
		$('#login').hide();
		$('#member_info').show();
		$('#acount').show();
		$('#acount2').hide();

		$('#regUser1').html($('#regUser').val());
		$('#address1').html($('#address').val());
		$('#law_man1').html($('#law_man').val());
		$('#ID_num1').html($('#ID_num').val());
		$('#phone1').html($('#phone').val());
		$('#company_url1').html($('#company_url').val());
		$('#text_info1').html($('#text_info').val());
		
	    $('#quesion1').html( $('#quesion option').eq($('#quesion').val()).html() );
	}
	//点击注册成功的请登录
	$('#reg_logined').click(function(){
		$('#login').show();
		$('.close').click(function(){$('#login').hide()});
        	
	});


   //点击个人信息按钮
   $('#acount').click(function(){
	    Meg();
	});

	 
	 
}
