package sl.hr_client.net.news.detail;

/**
 * Created by Administrator on 2017/4/12.
 */

public interface NewsDetailView {
    void updateNewsDetailView(String str);

    void showLoading();

    void showLoadingFailed();

    void hideLoading();

    void showError(String msg);
}
