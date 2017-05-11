package sl.hr_client.main.setting;

import android.content.Context;
import android.content.Intent;
import android.os.Bundle;
import android.support.annotation.Nullable;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.EditText;
import android.widget.TextView;


import org.greenrobot.eventbus.EventBus;

import sl.base.ui.loading.AVLoadingIndicatorView;
import sl.base.utils.UtilsKeyBoard;
import sl.base.utils.UtilsLog;
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
import sl.hr_client.main.account.LoginActivity;
import sl.hr_client.net.acc.modifypwd.ModifyPwdPresenter;
import sl.hr_client.net.acc.modifypwd.ModifyPwdView;
import sl.hr_client.utils.constant.ConstantData;
import sl.hr_client.utils.constant.TransDefine;
import sl.hr_client.utils.net.ResponseUtils;

/**
 * Created by Administrator on 2017/5/3.
 */

public class ModifyPwdFragment extends BaseFragment implements View.OnClickListener, ModifyPwdView, FragmentBackListener {
    public static final String MODIFY_PWD = " Modify_Pwd";

    private View modifyPwdView;
    private Context ctx;

    private TextView tvTitle;
    private TextView tvBack;

    private EditText edtOldPwd;
    private EditText edtNewPwd;
    private EditText edtSurePwd;

    private TextView tvModifyPwd;
    private AVLoadingIndicatorView vLoading;

    private ModifyPwdPresenter modifyPwdPresenter;
    private String shouldResetListener;


    public void onViewCreated(View view, @Nullable Bundle savedInstanceState) {
        super.onViewCreated(view, savedInstanceState);

        tvTitle = (TextView) modifyPwdView.findViewById(R.id.tv_head);
        tvBack = (TextView) modifyPwdView.findViewById(R.id.tv_head_right);
        edtOldPwd = (EditText) modifyPwdView.findViewById(R.id.edt_old_pwd);
        edtNewPwd = (EditText) modifyPwdView.findViewById(R.id.edt_new_pwd);
        edtSurePwd = (EditText) modifyPwdView.findViewById(R.id.edt_sure_pwd);
        tvModifyPwd = (TextView) modifyPwdView.findViewById(R.id.tv_modify_pwd);
        vLoading = (AVLoadingIndicatorView) modifyPwdView.findViewById(R.id.v_loading);

        tvTitle.setText(getString(R.string.modify_password));
        tvBack.setText(getString(R.string.back));
        tvBack.setVisibility(View.VISIBLE);

        addListener();
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        modifyPwdView = inflater.inflate(R.layout.fragment_modify_pwd, container, false);

        ctx = modifyPwdView.getContext();
        modifyPwdPresenter = new ModifyPwdPresenter(this);

        shouldResetListener = getArguments().getString(TransDefine.Bundle_Should_Reset_Back_Listener)
                == null ? getString(R.string.null_value) : getArguments().getString(TransDefine.Bundle_Should_Reset_Back_Listener);
        return modifyPwdView;
    }

    private void addListener() {
        tvBack.setOnClickListener(this);
        tvModifyPwd.setOnClickListener(this);
    }

    @Override
    public void onClick(View view) {
        switch (view.getId()) {
            case R.id.tv_head_right:
                funcBack();
                break;
            case R.id.tv_modify_pwd:
                funcModifyPwd();
                break;
            default:
                break;
        }
    }

    private void funcBack() {
        getFragmentManager().popBackStack();
    }

    private void funcModifyPwd() {

        if (UtilsNet.isNetworkAvailable(ctx)) {
            String uID = DataUtils.getNowUserByUID().getUID() == null ? getString(R.string.null_value) : DataUtils.getNowUserByUID().getUID();
            String uLoginStr = DataUtils.getNowUserByUID().getULoginStr() == null ? getString(R.string.null_value) : DataUtils.getNowUserByUID().getULoginStr();

            String oldPwd = edtOldPwd.getText().toString() == null ? getString(R.string.null_value) : edtOldPwd.getText().toString();
            String newPwd = edtNewPwd.getText().toString() == null ? getString(R.string.null_value) : edtNewPwd.getText().toString();
            String surePwd = edtSurePwd.getText().toString() == null ? getString(R.string.null_value) : edtSurePwd.getText().toString();

            String uClientKey = UtilsPreference.getString(ConstantData.FLAG_GE_TUI_CLIENTID, ConstantData.default_String);

            boolean inputFlag = checkInput(oldPwd, newPwd, surePwd);
            if (inputFlag) {
                modifyPwdPresenter.pModifyPwd(ctx, uID, uLoginStr, oldPwd, newPwd, uClientKey);
            } else {
                UtilsToast.showToast(ctx, getString(R.string.network_err));
            }
        }

    }

    private boolean checkInput(String oldPwd, String newPwd, String surePwd) {
        UtilsKeyBoard.hideKeyBoard(getActivity());

        if (!UtilsValidate.isPassword(oldPwd)) {
            UtilsToast.showToast(ctx, String.format(getString(R.string.format_error),
                    getString(R.string.old_password)));
            return false;
        }

        if (!UtilsValidate.isPassword(newPwd)) {
            UtilsToast.showToast(ctx, String.format(getString(R.string.format_error),
                    getString(R.string.new_password)));
            return false;
        }

        if (!newPwd.equals(surePwd)) {
            UtilsToast.showToast(ctx, getString(R.string.password_different));
            return false;
        }

        if (oldPwd.equals(newPwd)) {
            UtilsToast.showToast(ctx, getString(R.string.password_different));
            return false;
        }

        return true;
    }

    @Override
    public void modifyPwdSuccessView(String str) {
        UtilsLog.logE(UtilsLog.getSte(), str);
        ResponseUtils.showResponseOperate(ctx, str);

        UserBean user = DataUtils.getNowUserByUID();
        if (user != null) {
            UtilsPreference.commitString(ConstantData.FLAG_NOW_USER_ID, ConstantData.default_String);
        }

        Intent intentToLogin = new Intent(ctx, LoginActivity.class);
        startActivity(intentToLogin);
        getActivity().finish();
    }

    @Override
    public void showLoading() {
        tvModifyPwd.setVisibility(View.GONE);
        vLoading.setVisibility(View.VISIBLE);
    }

    @Override
    public void hideLoading() {
        tvModifyPwd.setVisibility(View.VISIBLE);
        vLoading.setVisibility(View.GONE);
    }

    @Override
    public void showError(String msg) {
        UtilsLog.logE(UtilsLog.getSte(), msg);
        ResponseUtils.showResponseOperate(ctx, msg);
        hideLoading();
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

        if (shouldResetListener.equals(TransDefine.Bundle_Should_Reset_Back_Listener_True)) {
            EventBus.getDefault().post(
                    new TransferEvent(TransDefine.EVENT_RESET_USER_SAFE_PRESS_BACK_LISTENER)); //重置友情链接的返回键监听
        }
    }

    @Override
    public void onBackForward() {
        funcBack();
    }
}
