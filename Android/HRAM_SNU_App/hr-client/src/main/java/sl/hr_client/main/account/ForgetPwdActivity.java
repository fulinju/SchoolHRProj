package sl.hr_client.main.account;

import android.content.Context;
import android.os.Bundle;
import android.view.View;
import android.widget.EditText;
import android.widget.LinearLayout;
import android.widget.TextView;

import sl.base.ui.loading.AVLoadingIndicatorView;
import sl.base.utils.UtilsKeyBoard;
import sl.base.utils.UtilsLog;
import sl.base.utils.UtilsNet;
import sl.base.utils.UtilsPreference;
import sl.base.utils.UtilsToast;
import sl.hr_client.R;
import sl.hr_client.base.BaseActivity;
import sl.hr_client.data.DataUtils;
import sl.hr_client.net.acc.forgetpwd.ForgetPwdPresenter;
import sl.hr_client.net.acc.forgetpwd.ForgetPwdView;
import sl.hr_client.net.acc.login.LoginPresenter;
import sl.hr_client.utils.constant.ConstantData;
import sl.hr_client.utils.net.ResponseUtils;

/**
 * Created by Administrator on 2017/4/25.
 */

public class ForgetPwdActivity extends BaseActivity implements ForgetPwdView {
    private Context ctx;

    private ForgetPwdPresenter forgetPwdPresenter;

    private TextView tvTitle;
    private LinearLayout llBack;

    private EditText edtUsername;
    private EditText edtMailbox;

    private TextView tvSendToMail;
    private AVLoadingIndicatorView vLoading;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);

        setContentView(R.layout.activity_forget_pwd);
        ctx = this;
        forgetPwdPresenter = new ForgetPwdPresenter(this);
        initView();
    }

    private void initView() {
        tvTitle = (TextView) findViewById(R.id.tv_head);
        llBack = (LinearLayout) findViewById(R.id.ll_head_left);
        edtUsername = (EditText) findViewById(R.id.edt_username);
        edtMailbox = (EditText) findViewById(R.id.edt_mailbox);
        tvSendToMail = (TextView) findViewById(R.id.tv_send_to_mail);
        vLoading = (AVLoadingIndicatorView) findViewById(R.id.v_loading);

        tvTitle.setText(getString(R.string.forget_pwd));
        llBack.setVisibility(View.VISIBLE);
        addListener();
    }

    private void addListener() {
        llBack.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                funcBack();
            }
        });

        tvSendToMail.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                funcSendToMail();
            }
        });
    }

    private void funcBack() {
        getFragmentManager().popBackStack();
    }

    private void funcSendToMail() {
        if (UtilsNet.isNetworkAvailable(ctx)) {
            String uLoginStr = edtUsername.getText() == null ? getString(R.string.null_value) : edtUsername.getText().toString();
            String uMailbox = edtMailbox.getText() == null ? getString(R.string.null_value) : edtMailbox.getText().toString();

            String uClientKey = UtilsPreference.getString(ConstantData.FLAG_GE_TUI_CLIENTID, ConstantData.default_String);

            boolean inputFlag = checkInput(uLoginStr, uMailbox);
            if (inputFlag) {
                forgetPwdPresenter.pForgetPwd(ctx, uMailbox, uLoginStr, uClientKey);
            }
        } else {
            UtilsToast.showToast(ctx, getString(R.string.network_err));
        }
    }

    private boolean checkInput(String uLoginStr, String uMailbox) {
        UtilsKeyBoard.hideKeyBoard(this);

        return true;
    }

    @Override
    public void resetSuccessView(String str) {
        UtilsLog.logE(UtilsLog.getSte(), str);
        ResponseUtils.showResponseOperate(ctx, str);

        funcBack();
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
