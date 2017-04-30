package sl.hr_client.main.start;

import android.content.Intent;
import android.os.Bundle;
import android.os.Handler;

import sl.base.utils.UtilsNet;
import sl.base.utils.UtilsPreference;
import sl.base.utils.UtilsToast;
import sl.hr_client.R;
import sl.hr_client.base.BaseActivity;
import sl.hr_client.main.MainActivity;
import sl.hr_client.utils.constant.ConstantData;

/**
 * Created by xuzhijix on 2017/2/25.
 *  起始页
 */
public class SplashActivity extends BaseActivity{
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_splash);
        String clientID = UtilsPreference.getString(ConstantData.flag_ge_tui_clientId,ConstantData.default_String);

        if (UtilsNet.isNetworkAvailable(this) == false) {
            UtilsToast.showToast(this, getString(R.string.network_err));
            new Handler().postDelayed(new Runnable()
            {
                public void run()
                {
                    funcEnterApp();
                }
            }, 2000);
        }else if (clientID.equals(ConstantData.default_String)) {
            new Handler().postDelayed(new Runnable()
            {
                public void run()
                {
                    funcEnterApp();
                }
            }, 2000); //暂未加入个推
        }else{
            new Handler().postDelayed(new Runnable()
            {
                public void run()
                {
                    funcEnterApp();
                }
            }, 2000);
        }

        // 判断是否是第一次开启应用
        boolean isFirstOpen = UtilsPreference.getBoolean(ConstantData.flag_first_open, ConstantData.default_boolean); //缺省为false
        // 如果是第一次启动，则先进入功能引导页
        if (!isFirstOpen) {
            Intent intent = new Intent(this, GuideActivity.class);
            startActivity(intent);
            finish();
            return;
        }
//        无网络获取不到ClientID

        // 如果不是第一次启动app，则正常显示启动屏
        setContentView(R.layout.activity_splash);
//        new Handler().postDelayed(new Runnable() {
//
//            @Override
//            public void run() {
//                enterHomeActivity();
//            }
//        }, 2000);
    }

    /**
     * 进入App
     */
    private void funcEnterApp() {
        boolean isAutoLogin = UtilsPreference.getBoolean(ConstantData.flag_auto_login, ConstantData.default_boolean);

        Intent intent = new Intent(this, MainActivity.class);
        startActivity(intent);

//        if (isAutoLogin) {//如果是自动登录,并且所需资料齐全
//            Intent intent = new Intent(this, MainActivity.class);
//            startActivity(intent);
//        } else {
//            Intent intent = new Intent(this, LoginActivity.class);
//            startActivity(intent);
//        }

        finish();
    }

    @Override
    public void onDestroy() {
        super.onDestroy();

    }
}
