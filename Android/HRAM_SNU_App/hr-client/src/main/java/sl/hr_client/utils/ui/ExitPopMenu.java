package sl.hr_client.utils.ui;

import android.annotation.SuppressLint;
import android.content.Context;
import android.graphics.drawable.ColorDrawable;
import android.view.LayoutInflater;
import android.view.MotionEvent;
import android.view.View;
import android.widget.Button;
import android.widget.PopupWindow;
import android.widget.RelativeLayout;

import sl.hr_client.R;


/**
 * Created by xuzhijix on 2017/2/13.
 */
public class ExitPopMenu extends PopupWindow implements View.OnClickListener{
    private Button btnSheetExitApp, btnSheetLogOff, btnSheetCancel;
    private View mPopView;
    private OnItemClickListener mListener;

    public ExitPopMenu(Context context) {
        super(context);
        // TODO Auto-generated constructor stub
        init(context);
        setPopupWindow();
        btnSheetExitApp.setOnClickListener(this);
        btnSheetLogOff.setOnClickListener(this);
        btnSheetCancel.setOnClickListener(this);

    }

    /**
     * 初始化
     *
     * @param context
     */
    private void init(Context context) {
        // TODO Auto-generated method stub
        LayoutInflater inflater = LayoutInflater.from(context);
        //绑定布局
        mPopView = inflater.inflate(R.layout.pop_exit, null);

        btnSheetExitApp = (Button) mPopView.findViewById(R.id.btn_sheet_exit_app);
        btnSheetLogOff = (Button) mPopView.findViewById(R.id.btn_sheet_log_off);
        btnSheetCancel = (Button) mPopView.findViewById(R.id.btn_sheet_cancel);
    }

    /**
     * 设置窗口的相关属性
     */
    @SuppressLint("InlinedApi")
    private void setPopupWindow() {
        this.setContentView(mPopView);// 设置View
        this.setWidth(RelativeLayout.LayoutParams.MATCH_PARENT);// 设置弹出窗口的宽
        this.setHeight(RelativeLayout.LayoutParams.WRAP_CONTENT);// 设置弹出窗口的高
        this.setFocusable(true);// 设置弹出窗口可
        this.setAnimationStyle(R.style.pop_exit_anim);// 设置动画
        this.setBackgroundDrawable(new ColorDrawable(0x00000000));// 设置背景半透明
        mPopView.setOnTouchListener(new View.OnTouchListener() {// 如果触摸位置在窗口外面则销毁

            @Override
            public boolean onTouch(View v, MotionEvent event) {
                // TODO Auto-generated method stub
                int height = mPopView.findViewById(R.id.rl_pop_exit).getTop();
                int y = (int) event.getY();
                if (event.getAction() == MotionEvent.ACTION_UP) {
                    if (y < height) {
                        dismiss();
                    }
                }
                return true;
            }
        });
    }

    /**
     * 定义一个接口，公布出去 在Activity中操作按钮的单击事件
     */
    public interface OnItemClickListener {
        void setOnItemClick(View v);
    }

    public void setOnItemClickListener(OnItemClickListener listener) {
        this.mListener = listener;
    }

    @Override
    public void onClick(View v) {
        // TODO Auto-generated method stub
        if (mListener != null) {
            mListener.setOnItemClick(v);
        }
    }

}
