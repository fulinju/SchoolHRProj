package sl.base.utils;

import android.content.Context;
import android.content.SharedPreferences;

/**
 * Created by xuzhijix on 2016/11/14.
 * 工具类
 */
public class UtilsPreference {
    private static SharedPreferences mSharedPreferences = null;
    private static SharedPreferences.Editor mEditor = null;

    public static void init(Context context){
        if (null == mSharedPreferences) {
            mSharedPreferences = android.preference.PreferenceManager.getDefaultSharedPreferences(context) ;
        }
    }

    public static void removeKey(String key){
        mEditor = mSharedPreferences.edit();
        mEditor.remove(key);
        mEditor.commit();
    }

    public static void removeAll(){
        mEditor = mSharedPreferences.edit();
        mEditor.clear();
        mEditor.commit();
    }

    public static void commitString(String key, String value){
        mEditor = mSharedPreferences.edit();
        mEditor.putString(key, value);
        mEditor.commit();
    }

    public static String getString(String key, String failValue){
        return mSharedPreferences.getString(key, failValue);
    }

    public static void commitInt(String key, int value){
        mEditor = mSharedPreferences.edit();
        mEditor.putInt(key, value);
        mEditor.commit();
    }

    public static int getInt(String key, int failValue){
        return mSharedPreferences.getInt(key, failValue);
    }

    public static void commitLong(String key, long value){
        mEditor = mSharedPreferences.edit();
        mEditor.putLong(key, value);
        mEditor.commit();
    }

    public static long getLong(String key, long failValue) {
        return mSharedPreferences.getLong(key, failValue);
    }

    public static void commitBoolean(String key, boolean value){
        mEditor = mSharedPreferences.edit();
        mEditor.putBoolean(key, value);
        mEditor.commit();
    }

    public static Boolean getBoolean(String key, boolean failValue){
        return mSharedPreferences.getBoolean(key, failValue);
    }
}
