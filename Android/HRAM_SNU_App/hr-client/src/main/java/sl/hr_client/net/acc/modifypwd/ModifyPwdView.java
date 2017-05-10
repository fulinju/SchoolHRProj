package sl.hr_client.net.acc.modifypwd;

/**
 * Created by Administrator on 2017/5/2.
 */

public interface ModifyPwdView {
    void modifyPwdSuccessView(String str);

    void showLoading();

    void hideLoading();

    void showError(String msg);
}
