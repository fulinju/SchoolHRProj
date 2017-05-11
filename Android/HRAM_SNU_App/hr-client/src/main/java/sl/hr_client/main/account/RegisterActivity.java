package sl.hr_client.main.account;

import android.content.Context;
import android.os.Bundle;
import android.view.View;
import android.widget.EditText;
import android.widget.LinearLayout;
import android.widget.RadioButton;
import android.widget.RadioGroup;
import android.widget.RelativeLayout;
import android.widget.TextView;

import sl.base.ui.loading.AVLoadingIndicatorView;
import sl.base.ui.segment.SegmentedGroup;
import sl.base.utils.UtilsKeyBoard;
import sl.base.utils.UtilsLog;
import sl.base.utils.UtilsMD5;
import sl.base.utils.UtilsNet;
import sl.base.utils.UtilsPreference;
import sl.base.utils.UtilsToast;
import sl.base.utils.UtilsValidate;
import sl.hr_client.R;
import sl.hr_client.base.BaseActivity;
import sl.hr_client.data.bean.ResponseBean;
import sl.hr_client.net.acc.register.RegisterPresenter;
import sl.hr_client.net.acc.register.RegisterView;
import sl.hr_client.utils.constant.ConstantData;
import sl.hr_client.utils.net.ResponseUtils;

/**
 * Created by Administrator on 2017/4/25.
 */

public class RegisterActivity extends BaseActivity implements View.OnClickListener, RegisterView {
    private Context ctx;

    private TextView tvTitle;
    private LinearLayout llBack;

    private SegmentedGroup seGroupChooseRegisterType;
    private RadioButton rdoBtnByMail;
    private RelativeLayout rlRegisterByMail;
    private RelativeLayout rlRegisterByUsername;
    private EditText edtMail;
    private EditText edtUser;
    private EditText edtPassword;
    private EditText edtSurePwd;

    private TextView tvRegister;
    private AVLoadingIndicatorView vLoading;

    private RegisterPresenter registerPresenter;

    private final int IS_ERROR = 0;
    private final int IS_BY_MAIL = 1;
    private final int IS_BY_USERNAME = 2;
    private final int USERNAME_MIN_LENGTH = 6;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_register);
        initView();

        ctx = this;
        registerPresenter = new RegisterPresenter(this);
    }

    private void initView() {
        tvTitle = (TextView) findViewById(R.id.tv_head);
        llBack = (LinearLayout) findViewById(R.id.ll_head_left);

        seGroupChooseRegisterType = (SegmentedGroup) findViewById(R.id.seGroup_chooseRegisterType);
        rdoBtnByMail = (RadioButton) findViewById(R.id.rdoBtn_byMail);

        rlRegisterByMail = (RelativeLayout) findViewById(R.id.rl_register_by_mail);
        rlRegisterByUsername = (RelativeLayout) findViewById(R.id.rl_register_by_username);
        edtMail = (EditText) findViewById(R.id.edt_mail);
        edtUser = (EditText) findViewById(R.id.edt_user);
        edtPassword = (EditText) findViewById(R.id.edt_password);
        edtSurePwd = (EditText) findViewById(R.id.edt_sure_pwd);
        tvRegister = (TextView) findViewById(R.id.tv_register);
        vLoading = (AVLoadingIndicatorView) findViewById(R.id.v_loading);

        addListener();

        tvTitle.setText(getString(R.string.register));
        llBack.setVisibility(View.VISIBLE);

        rdoBtnByMail.setChecked(true);

        seGroupChooseRegisterType.setOnCheckedChangeListener(new RadioGroup.OnCheckedChangeListener() {
            @Override
            public void onCheckedChanged(RadioGroup group, int checkedId) {
                switch (checkedId) {
                    case R.id.rdoBtn_byMail:
                        rlRegisterByMail.setVisibility(View.VISIBLE);
                        rlRegisterByUsername.setVisibility(View.GONE);
                        break;
                    case R.id.rdoBtn_byUserName:
                        rlRegisterByMail.setVisibility(View.GONE);
                        rlRegisterByUsername.setVisibility(View.VISIBLE);
                        break;
                    default:
                        break;
                }
            }
        });
    }

    private void addListener() {
        llBack.setOnClickListener(this);
        tvRegister.setOnClickListener(this);
    }


    private void funcRegister() {
        if (UtilsNet.isNetworkAvailable(ctx)) {
            String mailbox = edtMail.getText().toString();
            String username = edtUser.getText().toString();
            String password = edtPassword.getText().toString();
            String surePwd = edtSurePwd.getText().toString();
            int inputFlag = checkInput(mailbox, username, password, surePwd);

            switch (inputFlag) {
                case IS_BY_MAIL: //必须是final
                    registerPresenter.pRegisterByMail(ctx, mailbox);
                    break;
                case IS_BY_USERNAME:
                    String pwdMd5 = UtilsMD5.getMD5_UpperCase(password);
                    registerPresenter.pRegisterByRegisterByNameAndPwd(ctx, username, pwdMd5);
                    break;
                case IS_ERROR:

                    break;
                default:
                    break;
            }

        } else {
            UtilsToast.showToast(ctx, getString(R.string.network_err));
        }
    }

    private int checkInput(String mail, String username, String password, String surePwd) {
        UtilsKeyBoard.hideKeyBoard(this);
        if (rdoBtnByMail.isChecked()) {
            if (!UtilsValidate.isEmail(mail)) {
                UtilsToast.showToast(ctx, String.format(getString(R.string.format_error),
                        getString(R.string.mailbox)));
                return IS_ERROR;
            }

            return IS_BY_MAIL;
        }

        if (!rdoBtnByMail.isChecked()
                ) {
            if (!UtilsValidate.isUsername(username)) {
                UtilsToast.showToast(ctx, String.format(getString(R.string.format_error),
                        getString(R.string.username)));
                return IS_ERROR;
            }

            if (!UtilsValidate.isPassword(password)) {
                UtilsToast.showToast(ctx, String.format(getString(R.string.format_error),
                        getString(R.string.password)));
                return IS_ERROR;
            }

            if (!password.equals(surePwd)) {
                UtilsToast.showToast(ctx, getString(R.string.password_different));
                return IS_ERROR;
            }

            return IS_BY_USERNAME;
        }

        return IS_ERROR;
    }

    @Override
    public void onClick(View view) {
        switch (view.getId()) {
            case R.id.ll_head_left:
                funcBack();
                break;
            case R.id.tv_register:
                funcRegister();
                break;
            default:
                break;
        }
    }

    private void funcBack() {
        finish();
    }


    @Override
    public void registerSuccessView(String str) {
        UtilsLog.logE(UtilsLog.getSte(), str);
        ResponseUtils.showResponseOperate(ctx, str);
//        UtilsToast.showToast(ctx, edtPhone.getText().toString() + " " + ok.getMessage());
        finish();
    }

    @Override
    public void showLoading() {
        tvRegister.setVisibility(View.GONE);
        vLoading.setVisibility(View.VISIBLE);
    }

    @Override
    public void hideLoading() {
        tvRegister.setVisibility(View.VISIBLE);
        vLoading.setVisibility(View.GONE);
    }

    @Override
    public void showError(String msg) {
        UtilsLog.logE(UtilsLog.getSte(), msg);
        ResponseUtils.showResponseOperate(ctx, msg);
        hideLoading();
    }
}
