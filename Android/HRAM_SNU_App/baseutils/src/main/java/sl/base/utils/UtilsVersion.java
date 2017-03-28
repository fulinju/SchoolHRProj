package sl.base.utils;

import android.content.Context;
import android.content.pm.PackageInfo;
import android.content.pm.PackageManager;

import sl.base.R;


/**
 * Created by xuzhijix on 2017/3/17.
 */
public class UtilsVersion {
    /**
     * 获取版本号
     * @return 当前应用的版本号
     */
    public static String getVersion(Context context) {
        try {
            PackageManager manager = context.getPackageManager();
            PackageInfo info = manager.getPackageInfo(context.getPackageName(), 0);
            String version = info.versionName;
            return  version;
        } catch (Exception e) {
            e.printStackTrace();
            return context.getString(R.string.can_not_find_version_name);
        }
    }
}
