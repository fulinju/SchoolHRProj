package sl.hr_client.main.account;

import android.content.Context;
import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.EditText;
import android.widget.TextView;

import sl.base.ui.loading.AVLoadingIndicatorView;
import sl.base.utils.UtilsCheck;
import sl.base.utils.UtilsLog;
import sl.base.utils.UtilsMD5;
import sl.base.utils.UtilsNet;
import sl.base.utils.UtilsPreference;
import sl.base.utils.UtilsToast;
import sl.hr_client.R;
import sl.hr_client.base.BaseActivity;
import sl.hr_client.net.acc.login.LoginPresenter;
import sl.hr_client.net.acc.login.LoginView;
import sl.hr_client.utils.constant.ConstantData;

/**
 * Created by xuzhijix on 2017/3/28.
 */
public class LoginActivity extends BaseActivity implements LoginView{

//    @BindView(R.id.tv_head)
//    TextView tvHead;
//    @BindView(R.id.tv_head_right)
//    TextView tvHeadRight;
//    @BindView(R.id.tv_user)
//    TextView tvUser;
//    @BindView(R.id.edt_phone)
//    EditText edtPhone;
//    @BindView(R.id.edt_password)
//    EditText edtPassword;
//    @BindView(R.id.rl_password)
//    RelativeLayout rlPassword;
//    @BindView(R.id.tv_login)
//    TextView tvLogin;

    private Context ctx;

    private EditText edtUser;
    private EditText edtPassword;
    private TextView tvLogin;
    private TextView tvTitle;
    private TextView tvRight;
    private AVLoadingIndicatorView vLoading;

    private LoginPresenter loginPresenter;


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_login);
//        ButterKnife.bind(this);
        ctx= this;
        loginPresenter = new LoginPresenter(this);
        initView();

    }

    private void initView(){
        edtUser = (EditText) findViewById(R.id.edt_user);
        edtPassword = (EditText) findViewById(R.id.edt_password);
        tvLogin = (TextView) findViewById(R.id.tv_login);

        tvTitle = (TextView) findViewById(R.id.tv_head);
        tvRight = (TextView) findViewById(R.id.tv_head_right);

        vLoading = (AVLoadingIndicatorView) findViewById(R.id.v_loading);

//        vLoading.setVisibility(View.VISIBLE);

        tvTitle.setText(ctx.getString(R.string.login));

        tvRight.setText(ctx.getString(R.string.register));
        tvRight.setVisibility(View.VISIBLE);
        tvRight.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                startActivity(new Intent(ctx, RegisterActivity.class));
            }
        });

        tvLogin.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                funcLogin();
            }
        });
    }
    /**
     * 登录
     */
    private void funcLogin() {
        if (UtilsNet.isNetworkAvailable(ctx)) {
            String user = edtUser.getText().toString();
            String pwd = edtPassword.getText().toString();
            boolean inputFlag = checkInput(pwd);
            if (inputFlag) {
                String pwdMd5 = UtilsMD5.getMD5_UpperCase(pwd);
                loginPresenter.pLogin(ctx, user, pwdMd5, UtilsPreference.getString(ConstantData.flag_ge_tui_clientId,ConstantData.default_String));
            }
        } else {
            UtilsToast.showToast(ctx, getString(R.string.network_err));
        }
    }

    /**
     * 检查输入
     * @return
     */
    private boolean checkInput(String pwd) {

        if (!UtilsCheck.checkInputRange(pwd, 0, ConstantData.PWD_LIMITED_LENGTH)) {
            UtilsToast.showToast(ctx, "请输入长度为1-" + ConstantData.PWD_LIMITED_LENGTH + "位密码");
            return false;
        }
        return true;
    }


    @Override
    public void loginSuccessView(String str) {
        UtilsLog.logE(UtilsLog.getSte(), str);
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
    }

//    @OnClick({R.id.tv_login})
//    public void clickLogin(){
//        UtilsToast.showToast(ctx,"登录");
//    }


}
