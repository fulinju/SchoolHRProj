package sl.hr_client.net.news;

/**
 * Created by Administrator on 2017/4/6.
 */

public interface NewsView {
    void updateNewsView(String str);

    void showLoading();

    void showLoadingFailed();

    void hideLoading();

    void showError(String msg);
}
