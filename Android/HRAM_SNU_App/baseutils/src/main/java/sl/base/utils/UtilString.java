package sl.base.utils;

import java.util.ArrayList;
import java.util.List;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

/**
 * Created by xuzhijix on 2017/3/15.
 */
public class UtilString {

    /**
     * 以点分割
     *
     * @param s
     * @return
     */
    public static String[] getStringsSplitByDot(String s) {
        String str[] = s.split(".");
        return str;
    }

    /**
     * 得到int数组
     * @param s
     * @return
     */
    public static int[] getIntegersSplitByDot(String s) {
        String str[] = s.split("\\.");
        int[] num = new int[str.length];
        for (int i = 0; i < str.length; i++) {
            num[i] = Integer.parseInt(str[i]);
        }
        return num;
    }

    /**
     * 获取中括号内的
     *
     * @param s
     */
    public static List<String> getFromMidBrackets(String s) {
        List<String> target = new ArrayList<>();
        Pattern p = Pattern.compile("\\[([^\\[\\]]+)\\]");
        Matcher m = p.matcher(s);

        while (m.find()) {
            target.add(m.group());
        }
        return target;
    }

    /**
     * 获取大括号内的
     *
     * @param s
     */
    public static List<String> getFromBigBrackets(String s) {
        List<String> target = new ArrayList<>();
        Pattern p = Pattern.compile("\\{[\\s\\S]*?\\}");

        Matcher m = p.matcher(s);

        while (m.find()) {
            target.add(m.group());
        }
        return target;
    }


    /**
     * 转化时间String XXXX-XX-XXTXX:XX:XX -> XX-XX-XX XX:XX
     * @param timeStr
     * @return
     */
    public static String shorterTimeString(String  timeStr){
        return  timeStr.replace("T"," ").substring(2,timeStr.length()-3);
    }

    /**
     * 转化时间String XXXX-XX-XX XX:XX -> XX-XX-XX XX:XX
     * @param timeStr
     * @return
     */
    public static String shorterTimeString2(String  timeStr){
        return  timeStr.substring(2,timeStr.length());
    }
}
