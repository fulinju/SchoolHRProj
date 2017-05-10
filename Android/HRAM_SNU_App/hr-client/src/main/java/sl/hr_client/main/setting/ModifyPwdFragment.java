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


import sl.base.ui.loading.AVLoadingIndicatorView;
import sl.base.utils.UtilsKeyBoard;
import sl.base.utils.UtilsLog;
import sl.base.utils.UtilsNet;
import sl.base.utils.UtilsPreference;
import sl.base.utils.UtilsToast;
import sl.hr_client.R;
import sl.hr_client.base.BaseFragment;
import sl.hr_client.data.DataUtils;
import sl.hr_client.data.bean.UserBean;
import sl.hr_client.main.account.LoginActivity;
import sl.hr_client.net.acc.modifypwd.ModifyPwdPresenter;
import sl.hr_client.net.acc.modifypwd.ModifyPwdView;
import sl.hr_client.utils.constant.ConstantData;
import sl.hr_client.utils.net.ResponseUtils;

/**
 * Created by Administrator on 2017/5/3.
 */

public class ModifyPwdFragment extends BaseFragment implements View.OnClickListener, ModifyPwdView {
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


    public void onViewCreated(View view, @Nullable Bundle savedInstanceState) {
        super.onViewCreated(view, savedInstanceState);

        tvTitle = (TextView) modifyPwdView.findViewById(R.id.tv_head);
        tvBack = (TextView) modifyPwdView.findViewById(R.id.tv_head_right);
        edtOldPwd = (EditText) modifyPwdView.findViewById(R.id.edt_old_pwd);
        edtNewPwd = (EditText) modifyPwdView.findViewById(R.id.edt_new_pwd);
        edtSurePwd = (EditText) modifyPwdView.findViewById(R.id.edt_new_pwd);
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

    }

    @Override
    public void hideLoading() {

    }

    @Override
    public void showError(String msg) {
        UtilsLog.logE(UtilsLog.getSte(), msg);
        ResponseUtils.showResponseOperate(ctx, msg);
    }
}
