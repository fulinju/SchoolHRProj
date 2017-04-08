package sl.hr_client.utils.constant;

import android.os.Environment;

/**
 * Created by xuzhijix on 2017/3/28.
 */
public class ConstantData {
    //错误值
//    public static final Object default_obj = new StringBuilder("Oh!Error!");
    public static final String default_String = "Oh!Error!";
    public static final int default_int = -789456123;
    public static final boolean default_boolean = false;

    public static final int requestTimeOut = 6000;



    //语言设置
    public static final String languageKey = "languageKey";
    public static final String languageEN = "en";
    public static final String languageZH = "zh";

    //启动设置
    public static final String flag_first_open = "flag_first_open";//第一次打开
    public static final String flag_auto_login = "flag_auto_login"; //自动登录
    public static final String flag_ge_tui_clientId = "flag_ge_tui_clientId";//个推的ClientID



    public static String imgCachePath = Environment.getExternalStorageDirectory().getPath()+"/sl/hr_client/cache/image/";
}
