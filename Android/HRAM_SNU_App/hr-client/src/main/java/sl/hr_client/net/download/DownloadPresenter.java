package sl.hr_client.net.download;

import android.content.Context;

import rx.Subscriber;
import rx.android.schedulers.AndroidSchedulers;
import rx.schedulers.Schedulers;

/**
 * Created by Administrator on 2017/4/12.
 */

public class DownloadPresenter {
    private DownloadView downloadView;
    private DownloadModel downloadModel;

    public DownloadPresenter(DownloadView downloadView) {
        this.downloadView = downloadView;
        downloadModel = new DownloadModel();
    }

    public void getPDownload(Context context, int pageIndex, int pageSize, String dmTypeID) {
        downloadView.showLoading();

        downloadModel.getDownloads(context, pageIndex, pageSize, dmTypeID)
                .subscribeOn(Schedulers.io())// 在非UI线程中执行getUser
                .observeOn(AndroidSchedulers.mainThread())// 在UI线程中执行结果
                .subscribe(new Subscriber<String>() {
                    @Override
                    public void onNext(String s) {
                        downloadView.updateDownloadsView(s);
                    }

                    @Override
                    public void onCompleted() {
                        downloadView.hideLoading();
                    }

                    @Override
                    public void onError(Throwable e) {
                        downloadView.showError(e.getMessage());
                    }
                });
    }

    public void loadMorePDownload(Context context, int pageIndex, int pageSize, String dmTypeID) {
        downloadView.showLoading();

        downloadModel.getDownloads(context, pageIndex, pageSize, dmTypeID)
                .subscribeOn(Schedulers.io())// 在非UI线程中执行getUser
                .observeOn(AndroidSchedulers.mainThread())// 在UI线程中执行结果
                .subscribe(new Subscriber<String>() {
                    @Override
                    public void onNext(String s) {
                        downloadView.loadMoreDownloadsView(s);
                    }

                    @Override
                    public void onCompleted() {
                        downloadView.hideLoading();
                    }

                    @Override
                    public void onError(Throwable e) {
                        downloadView.showError(e.getMessage());
                    }
                });
    }

    /**
     * 仅改变数量，不作处理
     * @param context
     * @param downloadID
     */
    public void updatePDownloadNum(Context context, String downloadID) {

        downloadModel.updateDownloadNum(context, downloadID)
                .subscribeOn(Schedulers.io())// 在非UI线程中执行getUser
                .observeOn(AndroidSchedulers.mainThread())// 在UI线程中执行结果
                .subscribe(new Subscriber<String>() {
                    @Override
                    public void onNext(String s) {
                    }

                    @Override
                    public void onCompleted() {
                    }

                    @Override
                    public void onError(Throwable e) {
                    }
                });
    }
}
