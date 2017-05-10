package sl.base.utils;

import android.app.Activity;
import android.content.Context;
import android.view.inputmethod.InputMethodManager;

/**
 * Created by xuzhijix on 2016/12/2.
 * 键盘的方法
 */
public class UtilsKeyBoard {
    /**
     * 隐藏软键盘
     * @param activity
     */
    public static void hideKeyBoard(Activity activity){
        InputMethodManager imm = (InputMethodManager) activity.getSystemService(Context.INPUT_METHOD_SERVICE);
        if (imm != null) {
            imm.hideSoftInputFromWindow(activity.getWindow().getDecorView().getWindowToken(),
                    0);
        }
    }
}
