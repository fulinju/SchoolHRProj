package sl.base.utils;

import android.util.Log;

/**
 * Created by xuzhijix on 2016/11/25.
 */
public class UtilsLog {
    private static String DebugTag = "xzj";

    public static void logE(StackTraceElement ste,Object msg) {
        Log.e(DebugTag, getLineInfo(ste)+msg.toString());
    }

    public static String getLineInfo(StackTraceElement ste)
    {
//        StackTraceElement ste = new Throwable().getStackTrace()[1];
        return ste.getFileName() + ",Line " + ste.getLineNumber()+":";
    }

//    public static StackTraceElement ste = new Throwable().getStackTrace()[1];

    public static StackTraceElement getSte(){
        StackTraceElement ste = new Throwable().getStackTrace()[1];
        return ste;
    }

}
