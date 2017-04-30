package sl.base.utils;

import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.Calendar;
import java.util.Date;
import java.util.GregorianCalendar;
import java.util.Locale;

public class UtilDateTime {
	
	

	/**
	 * 获取当前日期是星期几<br>
	 * 
	 * @param dt
	 * @return 当前日期是星期几
	 */
	public static String getWeekOfDate(Date dt) {
		String[] weekDays = { "星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六" };
		Calendar cal = Calendar.getInstance();
		cal.setTime(dt);
		int w = cal.get(Calendar.DAY_OF_WEEK) - 1;
		if (w < 0)
			w = 0;
		return weekDays[w];
	}

	public static int getWeekOfDateId(Date dt) {
		Calendar cal = Calendar.getInstance();
		cal.setTime(dt);
		int w = cal.get(Calendar.DAY_OF_WEEK) - 1;
		if (w < 0)
			w = 0;
		return w;
	}
	
	public static String getNowTime() {
		SimpleDateFormat simpledateformat = new SimpleDateFormat(
				"yy-MM-dd HH:mm");
		Date date = Calendar.getInstance().getTime();
		return simpledateformat.format(date);
	}

	public static String getNowDate() {
		SimpleDateFormat simpledateformat = new SimpleDateFormat(
				"yyyyMMdd");
		Date date = Calendar.getInstance().getTime();
		return simpledateformat.format(date);
	}





	
	public static String getTime(long time) {
		SimpleDateFormat format = new SimpleDateFormat("yy-MM-dd HH:mm");
		return format.format(new Date(time));
	}

	public static String getHourAndMin(long time) {
		SimpleDateFormat format = new SimpleDateFormat("HH:mm");
		return format.format(new Date(time));
	}
	
	
	public static boolean isTimeBetween(String s, String s1) {
		SimpleDateFormat simpledateformat;
		String s2 = "yyyy-MM-dd HH:mm:ss";
		simpledateformat = new SimpleDateFormat(s2);
		boolean flag, flag2;
		Date date1;
		Date date2;

		try {

			Date date = simpledateformat.parse(s);
			date1 = simpledateformat.parse(s1);
			date2 = new Date();
			flag = date2.after(date);
			flag2 = date2.before(date1);
			return flag && flag2;

		} catch (ParseException e) {
		}

		return false;
	}


	    public static boolean isCloseEnough(long var0, long var2) {
	        long var4 = var0 - var2;
	        if(var4 < 0L) {
	            var4 = -var4;
	        }

	        return var4 < 30000L;
	    }



	    public static Date StringToDate(String var0, String var1) {
	        SimpleDateFormat var2 = new SimpleDateFormat(var1);
	        Date var3 = null;

	        try {
	            var3 = var2.parse(var0);
	        } catch (ParseException var5) {
	            var5.printStackTrace();
	        }

	        return var3;
	    }

	    public static String toTime(int var0) {
	        var0 /= 1000;
	        int var1 = var0 / 60;
	        boolean var2 = false;
	        if(var1 >= 60) {
	            int var4 = var1 / 60;
	            var1 %= 60;
	        }

	        int var3 = var0 % 60;
	        return String.format("%02d:%02d", new Object[]{Integer.valueOf(var1), Integer.valueOf(var3)});
	    }

	    public static String toTimeBySecond(int var0) {
	        int var1 = var0 / 60;
	        boolean var2 = false;
	        if(var1 >= 60) {
	            int var4 = var1 / 60;
	            var1 %= 60;
	        }

	        int var3 = var0 % 60;
	        return String.format("%02d:%02d", new Object[]{Integer.valueOf(var1), Integer.valueOf(var3)});
	    }

}
