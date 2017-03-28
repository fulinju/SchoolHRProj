package sl.base.utils;

import java.text.SimpleDateFormat;
import java.util.Calendar;
import java.util.Date;

/**
 * Created by xuzhijix on 2016/11/21.
 * 获取时间的工具
 */
public class UtilsTime {
    /**
     * 获取本地时间：格式与后台统一
     * xxxx-xx-xx xx:xx:xx (2016-11-18 02:33:04)
     *
     * @return
     */
    public static String getDateTime() {
        SimpleDateFormat sDateFormat = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss");//与后台统一的格式
        Date curDate = new Date(System.currentTimeMillis());//获取当前时间
        String timeStr = sDateFormat.format(curDate);
        return timeStr;
    }

    /**
     * 获取本地时间：格式与后台统一
     * 2017-03-10T10:53:21
     * @return
     */
    public static String getDateTimeIncludeT() {
        SimpleDateFormat sDateFormat = new SimpleDateFormat("yyyy-MM-ddTHH:mm:ss");//与后台统一的格式
        Date curDate = new Date(System.currentTimeMillis());//获取当前时间
        String timeStr = sDateFormat.format(curDate);
        return timeStr;
    }

    /**
     * 获取哪一天
     *
     * @return
     */
    public static String getDate() {
        SimpleDateFormat sDateFormat = new SimpleDateFormat("yyyy-MM-dd");//与后台统一的格式
        Date curDate = new Date(System.currentTimeMillis());//获取当前时间
        String dateStr = sDateFormat.format(curDate);
        return dateStr;
    }

    public static String getTime() {
        SimpleDateFormat sDateFormat = new SimpleDateFormat("HH:mm");//与后台统一的格式
        Date curDate = new Date(System.currentTimeMillis());//获取当前时间
        String timeStr = sDateFormat.format(curDate);
        return timeStr;
    }

    /**
     * 获取几分钟后的时间
     *
     * @param mins
     * @return
     */
    public static String getTimeMinsLate(int mins) {
        SimpleDateFormat df = new SimpleDateFormat("HH:mm");//与后台统一的格式
        Calendar calendar = Calendar.getInstance();
        /* HOUR_OF_DAY 指示一天中的小时 */
        calendar.set(Calendar.MINUTE, calendar.get(Calendar.MINUTE) + mins);
        String time = df.format(calendar.getTime());
        return time;
    }

    /**
     * 获取若干分钟后的小时
     *
     * @param mins
     * @return
     */
    public static int getHourMinsLate(int mins) {
        int hour = 0;
        Calendar calendar = Calendar.getInstance();
        calendar.set(Calendar.MINUTE, calendar.get(Calendar.MINUTE) + mins);
        hour = calendar.get(Calendar.HOUR_OF_DAY);
        return hour;
    }

    /**
     * 获取若干分钟后的分钟
     *
     * @param mins
     * @return
     */
    public static int getMinMinsLate(int mins) {
        int min = 0;
        Calendar calendar = Calendar.getInstance();
        calendar.set(Calendar.MINUTE, calendar.get(Calendar.MINUTE) + mins);
        min = calendar.get(Calendar.MINUTE);
        return min;
    }

    /**
     * 获取信息：xx:xx格式的小时
     * @param time
     * @return
     */
    public static int getTimeHour(String time){
        String hour = time.substring(0,2);
        return Integer.valueOf(hour);
    }

    /**
     * 获取信息： xx:xx格式的分钟
     * @param time
     * @return
     */
    public static int getTimeMin(String time){
        String min = time.substring(3,5);
        return Integer.valueOf(min);
    }
}
