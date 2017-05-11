package sl.hr_client.utils.net;

import com.android.volley.Request;
import com.android.volley.RequestQueue;
import com.android.volley.toolbox.ImageLoader;

/**
 * Created by xuzhijix on 2017/3/28.
 */
public class VolleyUtils {
    public static final String ErrVolleyType = "ErrVolleyType";//Volley返回data为空的ID

//    public static final String ServerIP = "http://123.57.26.127";
    public static final String ServerIP = "http://192.168.1.107:666";

    public static final int VOLLEY_POST = Request.Method.POST; //POST请求
    public static final int VOLLEY_GET = Request.Method.GET; //GET请求


    public static RequestQueue requestQueue; //请求的队列
    public static ImageLoader imageLoader; //加载图片的ImageLoader

    //登录
    public static final String loginUrl = ServerIP + "/api/Account/Login/";
    //注册http://123.57.26.127/api/FriendlyLink/FindFriendlyLinkListByTypeTop12
    public static final String registerByMailUrl = ServerIP + "/api/Account/RegisterByMailbox/";
    public static final String registerByNameAndPwdUrl = ServerIP + "/api/Account/RegisterByNameAndPwd/";
    //新闻列表
    public static final String newsUrl = ServerIP + "/api/Article/GetPublishList/";
    //下载列表
    public static final String downloadsUrl = ServerIP + "/api/Download/GetDownloadList/";
    //更新下载数量
    public static final String updateDownloadNumUrl = ServerIP + "/api/Download/AddDownloadNum/";
    //新闻详情
    public static final String newsDetailUrl = ServerIP + "/api/Article/GetPublishDetail/";
    //链接详情
    public static final String linksUrl = ServerIP + "/api/FriendlyLink/GetFriendlyLinkList/";
    //会员列表
    public static final String membersUrl = ServerIP + "/api/Member/GetMemberList/";
    //会员详情
    public static final String memberDetailUrl = ServerIP + "/api/Member/GetMemberDetail/";

    //修改密码
    public static final String modifyPasswordUrl = ServerIP + "/api/Account/ModifyPassword/";

    //修改用户信息
    public static final String modifyUserInfoUrl = ServerIP + "/api/Account/ModifyUserInfo/";

    //修改用户信息
    public static final String forgetPwdUrl = ServerIP + "/api/Account/ForgetPassword/";
}
