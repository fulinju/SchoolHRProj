using System;
using System.Collections.Generic;

using Google.ProtocolBuffers;
using com.igetui.api.openservice.igetui.template;
using com.gexin.rp.sdk.dto;
using com.igetui.api.openservice;
using com.igetui.api.openservice.igetui;
using com.igetui.api.openservice.payload;
using sl.web.constant;


namespace sl.web
{
    public class PushNotificationUtils
    {
        #region 发送推送
        /// <summary>
        /// 发送推送
        /// </summary>
        /// <param name="template"></param>
        /// <returns></returns>
        public static string PushNotificationToApp(string notificationTitle, string notificationText)
        {
            NotificationTemplate template = new NotificationTemplate();
            template.Title = notificationTitle;
            template.Text = notificationText;


            IGtPush push = new IGtPush(ConstantData.HOST, ConstantData.APPKEY, ConstantData.MASTERSECRET);
            // 定义"AppMessage"类型消息对象，设置消息内容模板、发送的目标App列表、是否支持离线发送、以及离线消息有效期(单位毫秒)
            AppMessage message = new AppMessage();

            NotificationTemplate notificationData = CommonNotificationTemplate(template);


            message.IsOffline = true;                         // 用户当前不在线时，是否离线存储,可选
            message.OfflineExpireTime = 1000 * 3600 * 12;     // 离线有效时间，单位为毫秒，可选
            message.Data = notificationData;
            //判断是否客户端是否wifi环境下推送，2:4G/3G/2G,1为在WIFI环境下，0为无限制环境
            // message.PushNetWorkType = 0; 
            // message.Speed = 1000;

            List<String> appIdList = new List<String>();
            appIdList.Add(ConstantData.APPID);

            List<String> phoneTypeList = new List<String>();   //通知接收者的手机操作系统类型
            //phoneTypeList.Add("ANDROID");
            //phoneTypeList.Add("IOS");

            List<String> provinceList = new List<String>();    //通知接收者所在省份
            //provinceList.Add("浙江");
            //provinceList.Add("上海");
            //provinceList.Add("北京");

            List<String> tagList = new List<string>();
            //tagList.Add("中文");

            message.AppIdList = appIdList;
            //message.PhoneTypeList = phoneTypeList;
            //message.ProvinceList = provinceList;
            //message.TagList = tagList;


            String pushResult = push.pushMessageToApp(message);
            //System.Console.WriteLine("-----------------------------------------------");
            //System.Console.WriteLine("服务端返回结果：" + pushResult);

            return pushResult;
            //String errormessage = "创建推送成功";
            //return Json(new JsonTip("1", errormessage));
        }

        #endregion

        #region 推送模板
        /// <summary>
        /// 推送模板
        /// </summary>
        /// <param name="template"></param>
        /// <returns></returns>
        public static NotificationTemplate CommonNotificationTemplate(NotificationTemplate template)
        {
            //NotificationTemplate template = new NotificationTemplate();
            template.AppId = ConstantData.APPID;
            template.AppKey = ConstantData.APPKEY;
            //通知栏显示本地图片
            template.Logo = "";
            //通知栏显示网络图标
            template.LogoURL = "";
            //应用启动类型，1：强制应用启动  2：等待应用启动
            template.TransmissionType = "1";
            //透传内容  
            template.TransmissionContent = "请填写透传内容";
            //接收到消息是否响铃，true：响铃 false：不响铃   
            template.IsRing = true;
            //接收到消息是否震动，true：震动 false：不震动   
            template.IsVibrate = true;
            //接收到消息是否可清除，true：可清除 false：不可清除    
            template.IsClearable = true;
            //设置通知定时展示时间，结束时间与开始时间相差需大于6分钟，消息推送后，客户端将在指定时间差内展示消息（误差6分钟）
            //String begin = "2016-12-27 12:28:10";
            //String end = "2016-12-27 16:38:20";
            //template.setDuration(begin, end);
            return template;
        }
        #endregion
    }
}