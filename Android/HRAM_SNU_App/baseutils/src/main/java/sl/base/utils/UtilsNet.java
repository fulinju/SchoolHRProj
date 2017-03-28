package sl.base.utils;

import android.content.Context;
import android.net.ConnectivityManager;

/**
 * Created by xuzhijix on 2016/11/21.
 * 检查网络状态
 */
public class UtilsNet {
    /**
     * 判断网络状态
     * @param context
     * @return
     */
    public static boolean isNetworkAvailable(final Context context) {
        ConnectivityManager cm = (ConnectivityManager) context
                .getSystemService(Context.CONNECTIVITY_SERVICE);
        if (cm == null || cm.getActiveNetworkInfo() == null) {

        } else {
            return cm.getActiveNetworkInfo() == null?false:true;
        }
        return false;
    }

    /**
     * 通过ping来判断
     * @param context
     * @return
     */
    public static boolean isNetWorkAvailableByPing(final Context context) {
        Runtime runtime = Runtime.getRuntime();
        try {
            Process pingProcess = runtime.exec("/system/bin/ping -c 1 https://employeeportal.intel.com");
            int exitCode = pingProcess.waitFor();
            return (exitCode == 0);
        } catch (Exception e) {
            e.printStackTrace();
        }
        return false;
    }
}
