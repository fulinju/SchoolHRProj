package sl.base.utils;

import android.content.Context;
import android.content.Intent;
import android.os.Bundle;

/**
 * Created by xuzhijix on 2016/11/16.
 * Intent的方法
 */
public class UtilsIntent {
    /**
     * Activity之间的简单跳转
     * @param packageContext
     * @param cls
     */
    public static void actToAct(Context packageContext, Class<?> cls){
        Intent intent = new Intent(packageContext,cls);
        packageContext.startActivity(intent);
    }
}
