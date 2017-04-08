package sl.hr_client.utils.net;

import com.android.volley.Request;
import com.android.volley.RequestQueue;
import com.android.volley.toolbox.ImageLoader;

/**
 * Created by xuzhijix on 2017/3/28.
 */
public class VolleyUtils {
    public static final String ServerIP = "http://123.57.26.127/";

    public static final int VOLLEY_POST = Request.Method.POST; //POST请求
    public static final int VOLLEY_GET = Request.Method.GET; //GET请求


    public static RequestQueue requestQueue; //请求的队列
    public static ImageLoader imageLoader; //加载图片的ImageLoader


    //新闻列表
    public static final String newsUrl = ServerIP+"api/Article/GetPublishList/";

}
