package sl.base.utils;

import android.content.Context;
import android.widget.Toast;

/**
 * Created by xuzhijix on 2016/11/16.
 * Toast的方法
 */
public class UtilsToast {
    /**
     * 短时间显示Toast
     * @param ctx
     * @param msg
     */
    public static void showToast(Context ctx, String msg){
        Toast.makeText(ctx,msg,Toast.LENGTH_SHORT).show();
    }

    /**
     * 长时间显示Toast
     * @param ctx
     * @param msg
     */
    public static void showToastLong(Context ctx,String msg){
        Toast.makeText(ctx,msg,Toast.LENGTH_LONG).show();
    }
}
