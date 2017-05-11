package sl.hr_client.main.account;

import android.content.Context;
import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.EditText;
import android.widget.TextView;

import sl.base.ui.edttext.NoMenuEditText;
import sl.base.ui.loading.AVLoadingIndicatorView;
import sl.base.utils.UtilsKeyBoard;
import sl.base.utils.UtilsLog;
import sl.base.utils.UtilsMD5;
import sl.base.utils.UtilsNet;
import sl.base.utils.UtilsPreference;
import sl.base.utils.UtilsToast;
import sl.base.utils.UtilsValidate;
import sl.hr_client.R;
import sl.hr_client.base.BaseActivity;
import sl.hr_client.data.DataUtils;
import sl.hr_client.data.bean.UserBean;
import sl.hr_client.data.GsonUtils;
import sl.hr_client.main.MainActivity;
import sl.hr_client.net.acc.login.LoginPresenter;
import sl.hr_client.net.acc.login.LoginView;
import sl.hr_client.utils.constant.ConstantData;
import sl.hr_client.utils.net.ResponseUtils;

/**
 * Created by xuzhijix on 2017/3/28.
 */
public class LoginActivity extends BaseActivity implements LoginView {

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
    private NoMenuEditText edtPassword;
    private TextView tvLogin;
    private TextView tvTitle;
    private TextView tvRight;
    private TextView tvForgetPwd;
    private AVLoadingIndicatorView vLoading;

    private LoginPresenter loginPresenter;


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_login);
//        ButterKnife.bind(this);
        ctx = this;
        loginPresenter = new LoginPresenter(this);
        initView();

    }

    private void initView() {
        edtUser = (EditText) findViewById(R.id.edt_user);
        //出于安全考虑 密码防复制粘贴
        edtPassword = (NoMenuEditText) findViewById(R.id.edt_password);
        tvLogin = (TextView) findViewById(R.id.tv_login);

        tvTitle = (TextView) findViewById(R.id.tv_head);
        tvRight = (TextView) findViewById(R.id.tv_head_right);

        vLoading = (AVLoadingIndicatorView) findViewById(R.id.v_loading);
        tvForgetPwd = (TextView) findViewById(R.id.tv_forget_pwd);
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

        tvForgetPwd.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                funcForgetPwd();
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
            boolean inputFlag = checkInput(user, pwd);
            if (inputFlag) {
                String pwdMd5 = UtilsMD5.getMD5_UpperCase(pwd);
                loginPresenter.pLogin(ctx, user, pwdMd5, UtilsPreference.getString(ConstantData.FLAG_GE_TUI_CLIENTID, ConstantData.default_String));
            }
        } else {
            UtilsToast.showToast(ctx, getString(R.string.network_err));
        }
    }

    /**
     * 检查输入
     *
     * @return
     */
    private boolean checkInput(String user, String pwd) {
        UtilsKeyBoard.hideKeyBoard(this);

        if (!UtilsValidate.isUsername(user)) {
            UtilsToast.showToast(ctx, String.format(getString(R.string.format_error),
                    getString(R.string.username)));
            return false;
        }

        if (!UtilsValidate.isPassword(pwd)) {
            UtilsToast.showToast(ctx, String.format(getString(R.string.format_error),
                    getString(R.string.password)));
            return false;
        }

        return true;
    }

    private void funcForgetPwd() {
        startActivity(new Intent(ctx, ForgetPwdActivity.class));
    }

    @Override
    public void loginSuccessView(String str) {
        UtilsLog.logE(UtilsLog.getSte(), str);
        UserBean login = GsonUtils.parseUser(str);

        UserBean had = DataUtils.getUserByUID(login.getUID());
        if (had != null) {
            DataUtils.updateUser(login);
        } else {
            DataUtils.addUser(login);//添加一个 可以优化成已登陆过列表的
        }

        UtilsPreference.commitString(ConstantData.FLAG_NOW_USER_ID, login.getUID()); //提交当前登录的

        startActivity(new Intent(ctx, MainActivity.class));
        finish();
    }

    @Override
    public void showLoading() {
        tvLogin.setVisibility(View.GONE);
        vLoading.setVisibility(View.VISIBLE);
    }

    @Override
    public void hideLoading() {
        tvLogin.setVisibility(View.VISIBLE);
        vLoading.setVisibility(View.GONE);
    }

    @Override
    public void showError(String msg) {
        UtilsLog.logE(UtilsLog.getSte(), msg);
        ResponseUtils.showResponseOperate(ctx, msg);
        hideLoading(); //自行执行
    }

//    @OnClick({R.id.tv_login})
//    public void clickLogin(){
//        UtilsToast.showToast(ctx,"登录");
//    }


}
