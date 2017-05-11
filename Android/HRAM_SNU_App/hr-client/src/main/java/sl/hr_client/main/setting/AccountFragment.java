package sl.hr_client.main.setting;

import android.content.Context;
import android.content.Intent;
import android.os.Bundle;
import android.support.annotation.Nullable;
import android.view.Gravity;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.view.WindowManager;
import android.widget.Button;
import android.widget.PopupWindow;
import android.widget.RelativeLayout;
import android.widget.TextView;

import org.greenrobot.eventbus.EventBus;
import org.greenrobot.eventbus.Subscribe;
import org.greenrobot.eventbus.ThreadMode;

import sl.base.ui.switchbtn.SwitchView;
import sl.base.utils.UtilsIntent;
import sl.base.utils.UtilsLog;
import sl.base.utils.UtilsPreference;
import sl.base.utils.UtilsToast;
import sl.hr_client.R;
import sl.hr_client.base.ContentFragment;
import sl.hr_client.data.DataUtils;
import sl.hr_client.data.bean.UserBean;
import sl.hr_client.event.TransferEvent;
import sl.hr_client.main.MainActivity;
import sl.hr_client.main.account.LoginActivity;
import sl.hr_client.utils.constant.ConstantData;
import sl.hr_client.utils.constant.TransDefine;
import sl.hr_client.utils.ui.ExitPopMenu;

/**
 * Created by Administrator on 2017/5/3.
 */

public class AccountFragment extends ContentFragment implements View.OnClickListener, ExitPopMenu.OnItemClickListener {
    public static final String ACCOUNT = "Account";
    private View settingView;
    private Context ctx;

    private TextView tvNickName;
    private RelativeLayout rlAccountInfo;
    private RelativeLayout rlAccountSafe;
    private RelativeLayout rlAbout;
    private Button btnExit;

    private SwitchView svProtectScreen;

    private ExitPopMenu exitPopMenu;//退出的PopMenu

    public void onViewCreated(View view, @Nullable Bundle savedInstanceState) {
        super.onViewCreated(view, savedInstanceState);

        tvNickName = (TextView) view.findViewById(R.id.tv_nickname);
        rlAccountInfo = (RelativeLayout) view.findViewById(R.id.rl_account_info);
        rlAccountSafe = (RelativeLayout) view.findViewById(R.id.rl_account_safe);
        rlAbout = (RelativeLayout) view.findViewById(R.id.rl_about);
        svProtectScreen = (SwitchView) view.findViewById(R.id.sv_protect_screen);
        btnExit = (Button) view.findViewById(R.id.btn_exit);

        String nickName = DataUtils.getNowUserByUID().getUUserName() == null ? getString(R.string.null_value) : DataUtils.getNowUserByUID().getUUserName();
        tvNickName.setText(nickName);
        exitPopMenu = new ExitPopMenu(ctx);


        addListener();
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        settingView = inflater.inflate(R.layout.fragment_setting, container, false);

        ctx = settingView.getContext();

        //注册EventBus
        if (!EventBus.getDefault().isRegistered(this)) {
            EventBus.getDefault().register(this);
        }

        return settingView;
    }


    private void addListener() {
        btnExit.setOnClickListener(this);
        exitPopMenu.setOnItemClickListener(this);

        exitPopMenu.setOnDismissListener(new PopupWindow.OnDismissListener() {
            @Override
            public void onDismiss() {
                WindowManager.LayoutParams params = getActivity().getWindow().getAttributes();
                params.alpha = 1.0f;
                getActivity().getWindow().setAttributes(params);
            }
        });

        rlAccountInfo.setOnClickListener(this);
        rlAccountSafe.setOnClickListener(this);
        rlAbout.setOnClickListener(this);

        svProtectScreen.setOpened(UtilsPreference.getBoolean(ConstantData.FLAG_PROTECT_SCREEN, ConstantData.default_boolean));
        svProtectScreen.setOnStateChangedListener(new SwitchView.OnStateChangedListener() {
            @Override
            public void toggleToOn(SwitchView view) {
                svProtectScreen.setOpened(true);

                if (UtilsPreference.getBoolean(ConstantData.FLAG_PROTECT_SCREEN, ConstantData.default_boolean) == false) {
                    UtilsPreference.commitBoolean(ConstantData.FLAG_PROTECT_SCREEN, true);
                    UtilsToast.showToast(ctx, getString(R.string.protect_screen_open));
                }
            }

            @Override
            public void toggleToOff(SwitchView view) {
                svProtectScreen.setOpened(false);
                if (UtilsPreference.getBoolean(ConstantData.FLAG_PROTECT_SCREEN, ConstantData.default_boolean) == true) {
                    UtilsPreference.commitBoolean(ConstantData.FLAG_PROTECT_SCREEN, false);
                    UtilsToast.showToast(ctx, getString(R.string.protect_screen_close));
                }
            }
        });
    }

    @Override
    public void onClick(View view) {
        switch (view.getId()) {
            case R.id.rl_account_info:
                funcAccountInfo();
                break;
            case R.id.rl_account_safe:
                funcAccountSafe();
                break;
            case R.id.rl_about:
                funcAbout();
                break;
            case R.id.btn_exit: //退出
                funcExit();
                break;
            default:
                break;
        }
    }

    //点击账户信息
    private void funcAccountInfo() {
        Bundle trans = new Bundle();
//        trans.putString(TransDefine.Bundle_NewsID, publishID);
        UserInfoFragment transFragment = new UserInfoFragment();

        transFragment.setArguments(trans);
        getFragmentManager().beginTransaction()
                .addToBackStack(null)  //将当前fragment加入到返回栈中
                .setCustomAnimations(R.anim.push_bottom_in, R.anim.push_bottom_out, R.anim.push_right_in, R.anim.push_right_out)
                .replace(R.id.content_frame, transFragment, UserInfoFragment.USER_INFO)
                .commit();
    }

    //点击账户安全
    private void funcAccountSafe() {
        Bundle trans = new Bundle();
//        trans.putString(TransDefine.Bundle_NewsID, publishID);
        UserSafeFragment transFragment = new UserSafeFragment();

        transFragment.setArguments(trans);
        getFragmentManager().beginTransaction()
                .addToBackStack(null)  //将当前fragment加入到返回栈中
                .setCustomAnimations(R.anim.push_bottom_in, R.anim.push_bottom_out, R.anim.push_right_in, R.anim.push_right_out)
                .replace(R.id.content_frame, transFragment, UserSafeFragment.USER_SAFE)
                .commit();
    }

    //点击相关
    private void funcAbout() {
        Bundle trans = new Bundle();
//        trans.putString(TransDefine.Bundle_NewsID, publishID);
        AboutFragment transFragment = new AboutFragment();

        transFragment.setArguments(trans);
        getFragmentManager().beginTransaction()
                .addToBackStack(null)  //将当前fragment加入到返回栈中
                .setCustomAnimations(R.anim.push_bottom_in, R.anim.push_bottom_out, R.anim.push_right_in, R.anim.push_right_out)
                .replace(R.id.content_frame, transFragment, AboutFragment.ABOUT)
                .commit();
    }

    //点击退出
    private void funcExit() {
        //设置PopupWindow中的位置
        exitPopMenu.showAtLocation(settingView.findViewById(R.id.btn_exit), Gravity.BOTTOM | Gravity.CENTER_HORIZONTAL, 0, 0);
        WindowManager.LayoutParams params = getActivity().getWindow().getAttributes();
        params.alpha = 0.7f;
        getActivity().getWindow().setAttributes(params); //必须设置Activity的 设置Fragment颜色重叠
    }

    @Override
    public void setOnItemClick(View v) {
        switch (v.getId()) {
            case R.id.btn_sheet_exit_app:
                Intent intent = new Intent(ctx, MainActivity.class);
                intent.putExtra(TransDefine.EXIT_APP, true);
                startActivity(intent);
                exitPopMenu.dismiss();
                break;
            case R.id.btn_sheet_log_off:
                UserBean user = DataUtils.getNowUserByUID();
                if (user != null) {
                    UtilsPreference.commitString(ConstantData.FLAG_NOW_USER_ID, ConstantData.default_String);
                }

                Intent intentToLogin = new Intent(ctx, LoginActivity.class);
                startActivity(intentToLogin);
                getActivity().finish();
                exitPopMenu.dismiss();
                break;
            case R.id.btn_sheet_cancel:
                exitPopMenu.dismiss();
                break;
            default:
                break;
        }
    }

    @Subscribe(threadMode = ThreadMode.MAIN)
    public void onEventModifyUserInfo(TransferEvent event) {
        if (event.getTargetTag().equals(TransDefine.EVENT_MODIFY_USER_INFO)) {
            String uUsername = DataUtils.getNowUserByUID().getUUserName() == null ? getString(R.string.null_value) : DataUtils.getNowUserByUID().getUUserName();
            tvNickName.setText(uUsername);
        }
    }

    @Override
    public void onDestroy() {
        super.onDestroy();
        EventBus.getDefault().unregister(this);
    }
}
