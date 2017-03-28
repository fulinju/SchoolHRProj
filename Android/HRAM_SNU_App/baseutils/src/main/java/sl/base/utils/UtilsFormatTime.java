package sl.base.utils;

/**
 * Created by xuzhijix on 2016/11/24.
 * 格式化时间
 */
public class UtilsFormatTime {
    /**
     * 传入小时和分钟格式化
     *
     * @param hour
     * @param minute
     * @return
     */
    public static String formatTimeWithHHMM(int hour, int minute) {
        String timeStr = "00:00";
        timeStr = addZero2(hour) + ":" + addZero2(minute);
        return timeStr;
    }

    /**
     * 数字补全0
     *
     * @param num
     * @return
     */
    public static String addZero2(int num) {
        String numStr = "00";
        if (num < 10 && num >= 0) {
            numStr = "0" + num;
        } else {
            numStr = "" + num;
        }
        return numStr;
    }
}
