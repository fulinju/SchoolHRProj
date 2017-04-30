package sl.hr_client.net.acc.register;

/**
 * Created by Administrator on 2017/4/25.
 */

public interface RegisterView {
    void registerSuccessView(String str);

    void showLoading();

    void hideLoading();

    void showError(String msg);
}
