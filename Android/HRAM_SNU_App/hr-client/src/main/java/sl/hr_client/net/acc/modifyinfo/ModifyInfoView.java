package sl.hr_client.net.acc.modifyinfo;

/**
 * Created by Administrator on 2017/5/2.
 */

public interface ModifyInfoView {

    void modifyInfoSuccessView(String str);

    void showLoading();

    void hideLoading();

    void showError(String msg);
}
