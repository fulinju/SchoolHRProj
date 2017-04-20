package sl.hr_client.base;

import android.app.Application;

import com.android.volley.toolbox.Volley;
import com.igexin.sdk.PushManager;

import org.xutils.x;

import sl.hr_client.push.GeTuiIntentService;
import sl.hr_client.push.GeTuiPushService;
import sl.hr_client.utils.net.VolleyUtils;

/**
 * Created by xuzhijix on 2017/2/25.
 */
public class BaseApplication extends Application {
    public static BaseApplication instances;

    @Override
    public void onCreate() {
        super.onCreate();
        instances = this;//单例模式
        //1. 创建一个RequestQueue对象。
        VolleyUtils.requestQueue = Volley.newRequestQueue(getApplicationContext());

        // com.getui.demo.GeTuiPushService 为第三方自定义推送服务
        PushManager.getInstance().initialize(this.getApplicationContext(), GeTuiPushService.class);
        // com.getui.demo.GeTuiIntentService 为第三方自定义的推送服务事件接收类
        PushManager.getInstance().registerPushIntentService(this.getApplicationContext(), GeTuiIntentService.class);

        x.Ext.init(this);
    }

    public static BaseApplication getInstances() {
        return instances;
    }
}
