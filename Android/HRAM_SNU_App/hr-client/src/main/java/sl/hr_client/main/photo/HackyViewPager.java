package sl.hr_client.main.photo;

import android.content.Context;
import android.support.v4.view.ViewPager;
import android.util.AttributeSet;
import android.view.MotionEvent;

import sl.base.ui.jazzyviewpager.JazzyViewPager;

public class HackyViewPager extends JazzyViewPager {


	public HackyViewPager(Context context) {
		super(context);
	}
	
	public HackyViewPager(Context context, AttributeSet attrs) {
		super(context, attrs);
	}

	@Override
	public boolean onInterceptTouchEvent(MotionEvent ev) {
		try {
			return super.onInterceptTouchEvent(ev);
		} catch (IllegalArgumentException e) {
			//不理会
			return false;
		}catch(ArrayIndexOutOfBoundsException e ){
			//不理会
			return false;
		}
	}

}
