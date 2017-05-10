package sl.hr_client.net.acc.forgetpwd;

/**
 * Created by Administrator on 2017/5/2.
 */

public interface ForgetPwdView {
    void resetSuccessView(String str);

    void showLoading();

    void hideLoading();

    void showError(String msg);
}
