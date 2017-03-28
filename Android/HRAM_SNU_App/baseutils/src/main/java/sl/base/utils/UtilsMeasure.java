package sl.base.utils;

import android.content.Context;
import android.content.res.Resources;
import android.util.DisplayMetrics;

/**
 * Created by xuzhijix on 2016/12/1.
 * 获取宽高信息
 */
public class UtilsMeasure {
    /**
     * 获取屏幕的宽 px
     * @param context
     * @return
     */
    public static int getScreenWidth(Context context){
        Resources resources = context.getResources();
        DisplayMetrics dm = resources.getDisplayMetrics();
        int width = dm.widthPixels;
        return width;
    }

    /**
     * 获取屏幕的高 px
     * @param context
     * @return
     */
    public static int getScreenHeight(Context context){
        Resources resources = context.getResources();
        DisplayMetrics dm = resources.getDisplayMetrics();
        int height = dm.heightPixels;
        return height;
    }
}
