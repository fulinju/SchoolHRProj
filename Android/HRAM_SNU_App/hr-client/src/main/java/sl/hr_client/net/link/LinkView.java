package sl.hr_client.net.link;

/**
 * Created by Administrator on 2017/4/24.
 */

public interface LinkView {
    void updateLinksView(String str);

    void updateLoadMoreLinksView(String str);

    void showLoading();

    void showLoadingFailed();

    void hideLoading();

    void showError(String msg);
}
