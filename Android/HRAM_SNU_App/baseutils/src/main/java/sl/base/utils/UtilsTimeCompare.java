package sl.base.utils;

/**
 * Created by xuzhijix on 2016/11/24.
 * 时间比较
 */
public class UtilsTimeCompare {
    /**
     * 前面的时间更早
     *
     * @param aheadHour
     * @param aheadMin
     * @param hinderHour
     * @param hinderMin
     * @return
     */
    public static boolean aheadIsEarlier(int aheadHour, int aheadMin, int hinderHour, int hinderMin) {
        boolean flag = false;
        if (getTimeMinLenth(aheadHour, aheadMin) <= getTimeMinLenth(hinderHour, hinderMin)) {//前面的时间更小
            flag = true;
        }
        return flag;
    }

    /**
     * 获取时长
     *
     * @param hour
     * @param min
     * @return
     */
    public static int getTimeMinLenth(int hour, int min) {
        int mins = 0;
        mins = hour * 60 + min;
        return mins;
    }
}
