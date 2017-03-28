package sl.base.utils;

import android.content.Context;

import java.util.Locale;

/**
 * Created by xuzhijix on 2016/11/22.
 * 获取当前设置的语言
 */
public class UtilsNowLanguage {
    /**
     * 获取当前的语言
     * @param context
     * @return SIMPLIFIED_CHINESE/ENGLISH
     */
    public static Locale getNowLanguage(Context context){
        Locale curLocale = context.getResources().getConfiguration().locale;
        return curLocale;
    }
}
