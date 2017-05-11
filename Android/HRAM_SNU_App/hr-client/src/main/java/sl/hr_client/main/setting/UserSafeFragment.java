package sl.hr_client.main.setting;

import android.content.Context;
import android.content.DialogInterface;
import android.os.Bundle;
import android.support.annotation.Nullable;
import android.support.v7.app.AlertDialog;
import android.text.Editable;
import android.text.TextWatcher;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.EditText;
import android.widget.RelativeLayout;
import android.widget.TextView;

import org.greenrobot.eventbus.EventBus;
import org.greenrobot.eventbus.Subscribe;
import org.greenrobot.eventbus.ThreadMode;

import sl.base.ui.loading.AVLoadingIndicatorView;
import sl.base.utils.UtilsKeyBoard;
import sl.base.utils.UtilsLog;
import sl.base.utils.UtilsMD5;
import sl.base.utils.UtilsNet;
import sl.base.utils.UtilsPreference;
import sl.base.utils.UtilsToast;
import sl.base.utils.UtilsValidate;
import sl.hr_client.R;
import sl.hr_client.base.BaseFragment;
import sl.hr_client.data.DataUtils;
import sl.hr_client.data.bean.UserBean;
import sl.hr_client.event.TransferEvent;
import sl.hr_client.imp.FragmentBackListener;
import sl.hr_client.main.MainActivity;
import sl.hr_client.net.acc.modifyinfo.ModifyInfoPresenter;
import sl.hr_client.net.acc.modifyinfo.ModifyInfoView;
import sl.hr_client.utils.constant.ConstantData;
import sl.hr_client.utils.constant.TransDefine;
import sl.hr_client.utils.net.ResponseUtils;
import sl.hr_client.utils.ui.WaitingDialog;

/**
 * Created by Administrator on 2017/5/3.
 */

public class UserSafeFragment extends BaseFragment implements TextWatcher, View.OnClickListener, ModifyInfoView, FragmentBackListener {
    public static final String USER_SAFE = "User_Safe";

    private View userSafeView;
    private Context ctx;

    private TextView tvTitle;
    private TextView tvBack;

    private EditText edtMailbox;
    private EditText edtPhone;
    private RelativeLayout rlSave;
    private AVLoadingIndicatorView vLoading;

    private RelativeLayout rlModifyPwd;

    private AlertDialog.Builder builder;

    private String mailbox;
    private String phone;

    private ModifyInfoPresenter modifyInfoPresenter;

    public void onViewCreated(View view, @Nullable Bundle savedInstanceState) {
        super.onViewCreated(view, savedInstanceState);

        tvTitle = (TextView) userSafeView.findViewById(R.id.tv_head);
        tvBack = (TextView) userSafeView.findViewById(R.id.tv_head_right);
        edtMailbox = (EditText) userSafeView.findViewById(R.id.edt_mailbox);
        edtPhone = (EditText) userSafeView.findViewById(R.id.edt_phone);
        rlSave = (RelativeLayout) userSafeView.findViewById(R.id.rl_save);
        vLoading = (AVLoadingIndicatorView) userSafeView.findViewById(R.id.v_loading);
        rlModifyPwd = (RelativeLayout) userSafeView.findViewById(R.id.rl_modify_password);

        tvTitle.setText(getString(R.string.account_safe));
        tvBack.setText(getString(R.string.back));
        tvBack.setVisibility(View.VISIBLE);

        mailbox = DataUtils.getNowUserByUID().getUMaiBox() == null ? getString(R.string.null_value) : DataUtils.getNowUserByUID().getUMaiBox();
        phone = DataUtils.getNowUserByUID().getUPhone() == null ? getString(R.string.null_value) : DataUtils.getNowUserByUID().getUPhone();

        edtMailbox.setText(mailbox);
        edtPhone.setText(phone);

        addListener();

        initChooseDialog();

        WaitingDialog.createWaitingDlg(ctx, getString(R.string.saving));
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        userSafeView = inflater.inflate(R.layout.fragment_user_safe, container, false);

        ctx = userSafeView.getContext();

        //注册EventBus
        if (!EventBus.getDefault().isRegistered(this)) {
            EventBus.getDefault().register(this);
        }

        modifyInfoPresenter = new ModifyInfoPresenter(this);
        return userSafeView;
    }

    private void addListener() {
        tvBack.setOnClickListener(this);
        rlSave.setOnClickListener(this);
        rlModifyPwd.setOnClickListener(this);

        edtMailbox.addTextChangedListener(this);
        edtPhone.addTextChangedListener(this);
    }

    @Override
    public void onClick(View view) {
        switch (view.getId()) {
            case R.id.tv_head_right:
                funcBack();
                break;
            case R.id.rl_save:
                funcSave();
                break;
            case R.id.rl_modify_password:
                funcModifyPwd();
                break;
            default:
                break;
        }
    }

    private void funcBack() {
        if (rlSave.getVisibility() == View.GONE) {
            getFragmentManager().popBackStack();
        } else {
            builder.show();
        }
    }

    private void funcSave() {

        if (UtilsNet.isNetworkAvailable(ctx)) {
            String uID = DataUtils.getNowUserByUID().getUID() == null ? getString(R.string.null_value) : DataUtils.getNowUserByUID().getUID();
            String uUsername = DataUtils.getNowUserByUID().getUUserName() == null ? getString(R.string.null_value) : DataUtils.getNowUserByUID().getUUserName();

            String uMailbox = edtMailbox.getText() == null ? getString(R.string.null_value) : edtMailbox.getText().toString();
            String uPhone = edtPhone.getText() == null ? getString(R.string.null_value) : edtPhone.getText().toString();

            String uClientKey = UtilsPreference.getString(ConstantData.FLAG_GE_TUI_CLIENTID, ConstantData.default_String);

            boolean inputFlag = checkInput(uMailbox, uPhone);
            if (inputFlag) {
                modifyInfoPresenter.pModifyInfo(ctx, uID, uMailbox, uPhone, uUsername, uClientKey);

                WaitingDialog.showWaitingDlg();
            }
        } else {
            UtilsToast.showToast(ctx, getString(R.string.network_err));
        }
    }

    private boolean checkInput(String uMailbox, String uPhone) {
        UtilsKeyBoard.hideKeyBoard(getActivity());

        if (!UtilsValidate.isEmail(uMailbox)) {
            UtilsToast.showToast(ctx, String.format(getString(R.string.format_error),
                    getString(R.string.mailbox)));
            return false;
        }

        if (!UtilsValidate.isMobile(uPhone)) {
            UtilsToast.showToast(ctx, String.format(getString(R.string.format_error),
                    getString(R.string.phone)));
            return false;
        }

        return true;
    }

    private void funcModifyPwd() {
        Bundle trans = new Bundle();
        trans.putString(TransDefine.Bundle_Should_Reset_Back_Listener,TransDefine.Bundle_Should_Reset_Back_Listener_True); //需要重置

        ModifyPwdFragment transFragment = new ModifyPwdFragment();

        transFragment.setArguments(trans);
        getFragmentManager().beginTransaction()
                .addToBackStack(null)  //将当前fragment加入到返回栈中
                .setCustomAnimations(R.anim.push_bottom_in, R.anim.push_bottom_out, R.anim.push_right_in, R.anim.push_right_out)
                .replace(R.id.content_frame, transFragment, ModifyPwdFragment.MODIFY_PWD)
                .commit();
    }

    private void initChooseDialog() {
        builder = new AlertDialog.Builder(ctx);
        builder.setTitle(getString(R.string.warm_prompt));
        builder.setMessage(getString(R.string.save_info_exit));
        builder.setPositiveButton(getString(R.string.sure), new DialogInterface.OnClickListener() {
            @Override
            public void onClick(DialogInterface dialog, int which) {
                getFragmentManager().popBackStack();
            }
        });

        builder.setNegativeButton(getString(R.string.cancel), new DialogInterface.OnClickListener() {
            @Override
            public void onClick(DialogInterface dialog, int which) {
                dialog.dismiss();
            }
        });
        builder.create();
    }

    @Override
    public void modifyInfoSuccessView(String str) {
        UtilsLog.logE(UtilsLog.getSte(), str);
        ResponseUtils.showResponseOperate(ctx, str);
        UserBean user = DataUtils.getNowUserByUID();
        String mailbox = edtMailbox.getText() == null ? getString(R.string.null_value) : edtMailbox.getText().toString();
        String phone = edtPhone.getText() == null ? getString(R.string.null_value) : edtPhone.getText().toString();

        user.setUMaiBox(mailbox);
        user.setUPhone(phone);

        DataUtils.updateUser(user);

        rlSave.setVisibility(View.GONE);

        WaitingDialog.dismissWaitingDlg();
    }

    @Override
    public void showLoading() {

    }

    @Override
    public void hideLoading() {

    }

    @Override
    public void showError(String msg) {
        UtilsLog.logE(UtilsLog.getSte(), msg);
        ResponseUtils.showResponseOperate(ctx, msg);
        WaitingDialog.dismissWaitingDlg();
    }

    @Override
    public void beforeTextChanged(CharSequence charSequence, int i, int i1, int i2) {

    }

    @Override
    public void onTextChanged(CharSequence charSequence, int i, int i1, int i2) {

    }

    @Override
    public void afterTextChanged(Editable editable) {
        if (edtPhone.getText().toString().equals(phone) && edtMailbox.getText().toString().equals(mailbox)) {
            rlSave.setVisibility(View.GONE);
        } else {
            rlSave.setVisibility(View.VISIBLE);
        }
    }

    @Override
    public void onAttach(Context context) {
        super.onAttach(context);

        if (context instanceof MainActivity) {
            ((MainActivity) context).setBackListener(this);
            ((MainActivity) context).setInterception(true);
        }
    }

    @Override
    public void onDetach() {
        super.onDetach();
        if (getActivity() instanceof MainActivity) {
            ((MainActivity) getActivity()).setBackListener(null);
            ((MainActivity) getActivity()).setInterception(false);
        }
    }

    @Override
    public void onBackForward() {
        funcBack();
    }

    @Subscribe(threadMode = ThreadMode.MAIN)
    public void onEventResetBackListener(TransferEvent event) {
        if (event.getTargetTag().equals(TransDefine.EVENT_RESET_USER_SAFE_PRESS_BACK_LISTENER)) {
            onAttach(ctx);
        }
    }

    @Override
    public void onDestroy() {
        super.onDestroy();
        EventBus.getDefault().unregister(this);
    }

}
