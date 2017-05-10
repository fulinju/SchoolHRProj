package sl.hr_client.net.download;

/**
 * Created by Administrator on 2017/4/12.
 */

public interface DownloadView {
    void updateDownloadsView(String str);

    void loadMoreDownloadsView(String str);

    void showLoading();

    void showLoadingFailed();

    void hideLoading();

    void showError(String msg);
}
