package sl.hr_client.main.start;

import android.content.Intent;
import android.os.Bundle;
import android.os.Handler;

import org.greenrobot.eventbus.EventBus;
import org.greenrobot.eventbus.Subscribe;
import org.greenrobot.eventbus.ThreadMode;

import sl.base.utils.UtilsNet;
import sl.base.utils.UtilsPreference;
import sl.base.utils.UtilsToast;
import sl.hr_client.R;
import sl.hr_client.base.BaseActivity;
import sl.hr_client.data.DataUtils;
import sl.hr_client.event.TransferEvent;
import sl.hr_client.main.MainActivity;
import sl.hr_client.main.account.LoginActivity;
import sl.hr_client.utils.constant.ConstantData;
import sl.hr_client.utils.constant.TransDefine;

/**
 * Created by xuzhijix on 2017/2/25.
 *  起始页
 */
public class SplashActivity extends BaseActivity{
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_splash);
//        String clientID = UtilsPreference.getString(ConstantData.FLAG_GE_TUI_CLIENTID,ConstantData.default_String);

        // 判断是否是第一次开启应用
        boolean isFirstOpen = UtilsPreference.getBoolean(ConstantData.FLAG_FIRST_OPEN, ConstantData.default_boolean); //缺省为false
        // 如果是第一次启动，则先进入功能引导页
        if (!isFirstOpen) {
            Intent intent = new Intent(this, GuideActivity.class);
            startActivity(intent);
            finish();
            return;
        }

        if (UtilsNet.isNetworkAvailable(this) == false) {
            UtilsToast.showToast(this, getString(R.string.network_err));
            new Handler().postDelayed(new Runnable()
            {
                public void run()
                {
                    funcEnterApp();
                }
            }, 2000);
        }
//        else if (clientID.equals(ConstantData.default_String)) {
//            new Handler().postDelayed(new Runnable()
//            {
//                public void run()
//                {
//                    funcEnterApp();
//                }
//            }, 2000); //暂未加入个推
//        }
        else{
            new Handler().postDelayed(new Runnable()
            {
                public void run()
                {
                    funcEnterApp();
                }
            }, 2000);
        }
//        无网络获取不到ClientID

    }

    /**
     * 进入App
     */
    private void funcEnterApp() {
        String uId = UtilsPreference.getString(ConstantData.FLAG_NOW_USER_ID, ConstantData.default_String);

        if(uId.equals(ConstantData.default_String)){
            Intent intent = new Intent(this, LoginActivity.class);
            startActivity(intent);
        }else{
            Intent intent = new Intent(this, MainActivity.class);
            startActivity(intent);
        }

//        if (isAutoLogin) {//如果是自动登录,并且所需资料齐全
//            Intent intent = new Intent(this, MainActivity.class);
//            startActivity(intent);
//        } else {
//            Intent intent = new Intent(this, LoginActivity.class);
//            startActivity(intent);
//        }

        finish();
    }

    //暂时不是很需要ClientID
    //    @Subscribe(threadMode = ThreadMode.MAIN)
//    public void onEventGetPushID(TransferEvent event) {
//        if (event.getTargetTag().equals(TransDefine.EVENT_GET_PUSH_ID)) {
//            String clientID = UtilsPreference.getString(ConstantData.FLAG_GE_TUI_CLIENTID,ConstantData.default_String);
//        }
//    }
//
//    @Override
//    public void onDestroy() {
//        super.onDestroy();
//        EventBus.getDefault().unregister(this);
//    }

}
