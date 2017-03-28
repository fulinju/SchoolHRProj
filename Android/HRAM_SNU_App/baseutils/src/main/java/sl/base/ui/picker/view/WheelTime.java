package sl.base.ui.picker.view;

import android.content.Context;
import android.view.View;

import sl.base.R;
import sl.base.ui.picker.adapter.NumericWheelAdapter;
import sl.base.ui.picker.listener.OnItemSelectedListener;
import sl.base.ui.picker.view.TimePickerView.Type;
import sl.base.ui.picker.view.lib.WheelView;

import java.text.DateFormat;
import java.text.SimpleDateFormat;
import java.util.Arrays;
import java.util.List;


public class WheelTime {
    public static DateFormat dateFormat = new SimpleDateFormat("yyyy-MM-dd HH:mm");
    private View view;
    private WheelView wv_year;
    private WheelView wv_month;
    private WheelView wv_day;
    private WheelView wv_hours;
    private WheelView wv_mins;

    private Type type;
    public static final int DEFAULT_START_YEAR = 1990;
    public static final int DEFAULT_END_YEAR = 2100;


    //	private Calendar startYear  = new GregorianCalendar(); //开始时间
//	private Calendar timeEnd =new GregorianCalendar(); //结束时间
    private int startYear = DEFAULT_START_YEAR;
    private int endYear = DEFAULT_END_YEAR;

    private int startHour = 0;
    private int startMin = 0;
    public static int endHour = 23;
    private int endMin = 59;

    public WheelTime(View view) {
        super();
        this.view = view;
        type = Type.ALL;
        setView(view);

    }

    public WheelTime(View view, Type type) {
        super();
        this.view = view;
        this.type = type;
        setView(view);

    }


    public void setPicker(int year, int month, int day) {
        this.setPicker(year, month, day, 0, 0);
    }

    /**
     * 设置 初始选中的时间 和空间初始化
     */
    public void setPicker(int year, int month, int day, int h, int m) {
        // 添加大小月月份并将其转换为list,方便之后的判断

        String[] months_big =
                {"1", "3", "5", "7", "8", "10", "12"};
        String[] months_little =
                {"4", "6", "9", "11"};

        final List<String> list_big = Arrays.asList(months_big);
        final List<String> list_little = Arrays.asList(months_little);

        Context context = view.getContext();
        // 年
        wv_year = (WheelView) view.findViewById(R.id.year);
        wv_year.setAdapter(new NumericWheelAdapter(startYear, endYear));// 设置"年"的显示数据
        wv_year.setLabel(context.getString(R.string.pickerView_year));// 添加文字
        wv_year.setCurrentItem(year - startYear);// 初始化时显示的数据

        // 月
        wv_month = (WheelView) view.findViewById(R.id.month);
        wv_month.setAdapter(new NumericWheelAdapter(1, 12, "%02d"));
        wv_month.setLabel(context.getString(R.string.pickerView_month));
        wv_month.setCurrentItem(month);

        // 日
        wv_day = (WheelView) view.findViewById(R.id.day);
        // 判断大小月及是否闰年,用来确定"日"的数据
        System.out.println("month->" + month);
        if (list_big.contains(String.valueOf(month + 1))) {
            wv_day.setAdapter(new NumericWheelAdapter(1, 31, "%02d"));
        } else if (list_little.contains(String.valueOf(month + 1))) {
            wv_day.setAdapter(new NumericWheelAdapter(1, 30, "%02d"));
        } else {
            // 闰年
            if ((year % 4 == 0 && year % 100 != 0) || year % 400 == 0)
                wv_day.setAdapter(new NumericWheelAdapter(1, 29, "%02d"));
            else
                wv_day.setAdapter(new NumericWheelAdapter(1, 28, "%02d"));
        }
        wv_day.setLabel(context.getString(R.string.pickerView_day));
        wv_day.setCurrentItem(day - 1);

        wv_hours = (WheelView) view.findViewById(R.id.hour);
        wv_hours.setAdapter(new NumericWheelAdapter(startHour, endHour, "%02d"));
        wv_hours.setLabel(context.getString(R.string.pickerView_hours));// 添加文字

        wv_hours.setCurrentItem(h - startHour);// 初始化时显示的数据

        wv_mins = (WheelView) view.findViewById(R.id.min);

        if (h == startHour) {
            wv_mins.setAdapter(new NumericWheelAdapter(startMin, 59, "%02d"));
            wv_mins.setCurrentItem(m - startMin);
        } else if (h == endHour) {
            wv_mins.setAdapter(new NumericWheelAdapter(0, endMin, "%02d"));
            wv_mins.setCurrentItem(m);
        } else {
            wv_mins.setAdapter(new NumericWheelAdapter(0, 59, "%02d"));
            wv_mins.setCurrentItem(m);
        }

        wv_mins.setLabel(context.getString(R.string.pickerView_minutes));// 添加文字


        // 添加"年"监听
        OnItemSelectedListener wheelListener_year = new OnItemSelectedListener() {
            @Override
            public void onItemSelected(int index) {
                int year_num = index;
                System.out.println("year_num--->" + year_num);
                // 判断大小月及是否闰年,用来确定"日"的数据
                int maxItem = 30;
                if (list_big.contains(String.valueOf(wv_month.getCurrentItem()))) {
                    wv_day.setAdapter(new NumericWheelAdapter(1, 31, "%02d"));
                    maxItem = 31;
                } else if (list_little.contains(String.valueOf(wv_month.getCurrentItem()))) {
                    wv_day.setAdapter(new NumericWheelAdapter(1, 30, "%02d"));
                    maxItem = 30;
                } else {
                    if ((year_num % 4 == 0 && year_num % 100 != 0) || year_num % 400 == 0) {
                        wv_day.setAdapter(new NumericWheelAdapter(1, 29, "%02d"));
                        maxItem = 29;
                    } else {
                        wv_day.setAdapter(new NumericWheelAdapter(1, 28, "%02d"));
                        maxItem = 28;
                    }
                }
                if (wv_day.getCurrentItem() > maxItem - 1) {
                    wv_day.setCurrentItem(maxItem - 1);
                }
            }
        };
        // 添加"月"监听
        OnItemSelectedListener wheelListener_month = new OnItemSelectedListener() {
            @Override
            public void onItemSelected(int index) {
                int month_num = index;
                System.out.println("month_num--->" + month_num);
                int maxItem = 30;
                // 判断大小月及是否闰年,用来确定"日"的数据
                if (list_big.contains(String.valueOf(month_num))) {
                    wv_day.setAdapter(new NumericWheelAdapter(1, 31, "%02d"));
                    maxItem = 31;
                } else if (list_little.contains(String.valueOf(month_num))) {
                    wv_day.setAdapter(new NumericWheelAdapter(1, 30, "%02d"));
                    maxItem = 30;
                } else {
                    if (((wv_year.getCurrentItem()) % 4 == 0 && (wv_year.getCurrentItem()) % 100 != 0) || (wv_year.getCurrentItem()) % 400 == 0) {
                        wv_day.setAdapter(new NumericWheelAdapter(1, 29, "%02d"));
                        maxItem = 29;
                    } else {
                        wv_day.setAdapter(new NumericWheelAdapter(1, 28, "%02d"));
                        maxItem = 28;
                    }
                }
                if (wv_day.getCurrentItem() > maxItem - 1) {
                    wv_day.setCurrentItem(maxItem - 1);
                }

            }
        };

        //添加小时监听 为了定制选择时间
        final OnItemSelectedListener wheelListener_hour = new OnItemSelectedListener() {

            @Override
            public void onItemSelected(int index) {
                int hour_num = index;
                int maxItem = 60;
                if (hour_num == startHour) { //为选择开始的小时
                    wv_mins.setAdapter(new NumericWheelAdapter(startMin, 59, "%02d"));
                    maxItem = 60 - startMin;
                    int curItem = wv_mins.getCurrentItem();
                    if (curItem <= maxItem) {
                        wv_mins.setCurrentItem(0);
                    }
                } else if (hour_num == endHour) { //为结束的小时
                    wv_mins.setAdapter(new NumericWheelAdapter(0, endMin, "%02d"));
                    maxItem = endMin;
                    int curItem = wv_mins.getCurrentItem();
                    if (curItem > maxItem) { //当前选中比总数大，说明超了 需要减少
                        wv_mins.setCurrentItem(maxItem);
                    }
                } else {
                    wv_mins.setAdapter(new NumericWheelAdapter(0, 59, "%02d"));
                }


            }
        };
        wv_year.setOnItemSelectedListener(wheelListener_year);
        wv_month.setOnItemSelectedListener(wheelListener_month);
        wv_hours.setOnItemSelectedListener(wheelListener_hour);


        // 根据屏幕密度来指定选择器字体的大小(不同屏幕可能不同)
        int textSize = 6;
        switch (type) {
            case ALL:
                textSize = textSize * 3;
                break;
            case YEAR_MONTH_DAY:
                textSize = textSize * 4;
                wv_hours.setVisibility(View.GONE);
                wv_mins.setVisibility(View.GONE);
                break;
            case HOURS_MINS:
                textSize = textSize * 4;
                wv_year.setVisibility(View.GONE);
                wv_month.setVisibility(View.GONE);
                wv_day.setVisibility(View.GONE);
                break;
            case MONTH_DAY_HOUR_MIN:
                textSize = textSize * 3;
                wv_year.setVisibility(View.GONE);
                break;
            case YEAR_MONTH:
                textSize = textSize * 4;
                wv_day.setVisibility(View.GONE);
                wv_hours.setVisibility(View.GONE);
                wv_mins.setVisibility(View.GONE);
        }
        wv_day.setTextSize(textSize);
        wv_month.setTextSize(textSize);
        wv_year.setTextSize(textSize);
        wv_hours.setTextSize(textSize);
        wv_mins.setTextSize(textSize);
    }


    /**
     * 设置是否循环滚动
     *
     * @param cyclic
     */
    public void setCyclic(boolean cyclic) {
        wv_year.setCyclic(cyclic);
        wv_month.setCyclic(cyclic);
        wv_day.setCyclic(cyclic);
        wv_hours.setCyclic(cyclic);
        wv_mins.setCyclic(cyclic);
    }

    public String getTime() {
        StringBuffer sb = new StringBuffer();
        sb.append((wv_year.getCurrentItem())).append("-").append((wv_month.getCurrentItem())).append("-").append((wv_day.getCurrentItem())).append(" ").append(wv_hours.getCurrentItem()).append(":").append(wv_mins.getCurrentItem());
        return sb.toString();
    }

    public View getView() {
        return view;
    }

    public void setView(View view) {
        this.view = view;
    }

    public int getStartYear() {
        return startYear;
    }

    public void setStartYear(int startYear) {
        this.startYear = startYear;
    }

    public int getEndYear() {
        return endYear;
    }

    public void setEndYear(int endYear) {
        this.endYear = endYear;
    }

    public int getStartHour() {
        return startHour;
    }

    public void setStartHour(int startHour) {
        this.startHour = startHour;
    }

    public int getStartMin() {
        return startMin;
    }

    public void setStartMin(int startMin) {
        this.startMin = startMin;
    }

    public int getEndHour() {
        return endHour;
    }

    public void setEndHour(int endHour) {
        this.endHour = endHour;
    }

    public int getEndMin() {
        return endMin;
    }

    public void setEndMin(int endMin) {
        this.endMin = endMin;
    }
}
