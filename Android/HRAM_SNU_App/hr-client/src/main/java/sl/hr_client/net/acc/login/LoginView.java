package sl.hr_client.net.acc.login;

/**
 * Created by Administrator on 2017/4/25.
 */

public interface LoginView {

    void loginSuccessView(String str);

    void showLoading();

    void hideLoading();

    void showError(String msg);
}
